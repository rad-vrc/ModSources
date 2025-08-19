using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using Terraria.Social;
using Terraria.UI;
using Terraria.Utilities;

namespace Terraria.Achievements
{
	// Token: 0x02000491 RID: 1169
	public class AchievementManager
	{
		// Token: 0x1400005A RID: 90
		// (add) Token: 0x06002E80 RID: 11904 RVA: 0x005C423C File Offset: 0x005C243C
		// (remove) Token: 0x06002E81 RID: 11905 RVA: 0x005C4274 File Offset: 0x005C2474
		public event Achievement.AchievementCompleted OnAchievementCompleted;

		// Token: 0x06002E82 RID: 11906 RVA: 0x005C42AC File Offset: 0x005C24AC
		public AchievementManager()
		{
			if (SocialAPI.Achievements != null)
			{
				this._savePath = SocialAPI.Achievements.GetSavePath();
				this._isCloudSave = true;
				this._cryptoKey = SocialAPI.Achievements.GetEncryptionKey();
				return;
			}
			this._savePath = Main.SavePath + Path.DirectorySeparatorChar.ToString() + "achievements.dat";
			this._isCloudSave = false;
			this._cryptoKey = Encoding.ASCII.GetBytes("RELOGIC-TERRARIA");
		}

		// Token: 0x06002E83 RID: 11907 RVA: 0x005C434D File Offset: 0x005C254D
		public void Save()
		{
			FileUtilities.ProtectedInvoke(delegate
			{
				this.Save(this._savePath, this._isCloudSave);
			});
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x005C4360 File Offset: 0x005C2560
		private void Save(string path, bool cloud)
		{
			object ioLock = AchievementManager._ioLock;
			lock (ioLock)
			{
				if (SocialAPI.Achievements != null)
				{
					SocialAPI.Achievements.StoreStats();
				}
				try
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						using (CryptoStream cryptoStream = new CryptoStream(memoryStream, new RijndaelManaged().CreateEncryptor(this._cryptoKey, this._cryptoKey), CryptoStreamMode.Write))
						{
							using (BsonWriter bsonWriter = new BsonWriter(cryptoStream))
							{
								JsonSerializer.Create(this._serializerSettings).Serialize(bsonWriter, this._achievements);
								bsonWriter.Flush();
								cryptoStream.FlushFinalBlock();
								FileUtilities.WriteAllBytes(path, memoryStream.ToArray(), cloud);
							}
						}
					}
				}
				catch (Exception exception)
				{
					FancyErrorPrinter.ShowFileSavingFailError(exception, this._savePath);
				}
			}
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x005C446C File Offset: 0x005C266C
		public List<Achievement> CreateAchievementsList()
		{
			return this._achievements.Values.ToList<Achievement>();
		}

		// Token: 0x06002E86 RID: 11910 RVA: 0x005C447E File Offset: 0x005C267E
		public void Load()
		{
			this.Load(this._savePath, this._isCloudSave);
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x005C4494 File Offset: 0x005C2694
		private void Load(string path, bool cloud)
		{
			bool flag = false;
			object ioLock = AchievementManager._ioLock;
			lock (ioLock)
			{
				if (!FileUtilities.Exists(path, cloud))
				{
					return;
				}
				byte[] buffer = FileUtilities.ReadAllBytes(path, cloud);
				Dictionary<string, AchievementManager.StoredAchievement> dictionary = null;
				try
				{
					using (MemoryStream memoryStream = new MemoryStream(buffer))
					{
						using (CryptoStream cryptoStream = new CryptoStream(memoryStream, new RijndaelManaged().CreateDecryptor(this._cryptoKey, this._cryptoKey), CryptoStreamMode.Read))
						{
							using (BsonReader bsonReader = new BsonReader(cryptoStream))
							{
								dictionary = JsonSerializer.Create(this._serializerSettings).Deserialize<Dictionary<string, AchievementManager.StoredAchievement>>(bsonReader);
							}
						}
					}
				}
				catch (Exception)
				{
					FileUtilities.Delete(path, cloud, false);
					return;
				}
				if (dictionary == null)
				{
					return;
				}
				foreach (KeyValuePair<string, AchievementManager.StoredAchievement> keyValuePair in dictionary)
				{
					if (this._achievements.ContainsKey(keyValuePair.Key))
					{
						this._achievements[keyValuePair.Key].Load(keyValuePair.Value.Conditions);
					}
				}
				if (SocialAPI.Achievements != null)
				{
					foreach (KeyValuePair<string, Achievement> keyValuePair2 in this._achievements)
					{
						if (keyValuePair2.Value.IsCompleted && !SocialAPI.Achievements.IsAchievementCompleted(keyValuePair2.Key))
						{
							flag = true;
							keyValuePair2.Value.ClearProgress();
						}
					}
				}
			}
			if (flag)
			{
				this.Save();
			}
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x005C46E0 File Offset: 0x005C28E0
		public void ClearAll()
		{
			if (SocialAPI.Achievements != null)
			{
				return;
			}
			foreach (KeyValuePair<string, Achievement> keyValuePair in this._achievements)
			{
				keyValuePair.Value.ClearProgress();
			}
			this.Save();
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x005C4748 File Offset: 0x005C2948
		private void AchievementCompleted(Achievement achievement)
		{
			this.Save();
			if (this.OnAchievementCompleted != null)
			{
				this.OnAchievementCompleted(achievement);
			}
		}

		// Token: 0x06002E8A RID: 11914 RVA: 0x005C4764 File Offset: 0x005C2964
		public void Register(Achievement achievement)
		{
			this._achievements.Add(achievement.Name, achievement);
			achievement.OnCompleted += this.AchievementCompleted;
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x005C478A File Offset: 0x005C298A
		public void RegisterIconIndex(string achievementName, int iconIndex)
		{
			this._achievementIconIndexes.Add(achievementName, iconIndex);
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x005C4799 File Offset: 0x005C2999
		public void RegisterAchievementCategory(string achievementName, AchievementCategory category)
		{
			this._achievements[achievementName].SetCategory(category);
		}

		// Token: 0x06002E8D RID: 11917 RVA: 0x005C47B0 File Offset: 0x005C29B0
		public Achievement GetAchievement(string achievementName)
		{
			Achievement result;
			if (this._achievements.TryGetValue(achievementName, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x005C47D0 File Offset: 0x005C29D0
		public T GetCondition<T>(string achievementName, string conditionName) where T : AchievementCondition
		{
			return this.GetCondition(achievementName, conditionName) as T;
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x005C47E4 File Offset: 0x005C29E4
		public AchievementCondition GetCondition(string achievementName, string conditionName)
		{
			Achievement achievement;
			if (this._achievements.TryGetValue(achievementName, out achievement))
			{
				return achievement.GetCondition(conditionName);
			}
			return null;
		}

		// Token: 0x06002E90 RID: 11920 RVA: 0x005C480C File Offset: 0x005C2A0C
		public int GetIconIndex(string achievementName)
		{
			int result;
			if (this._achievementIconIndexes.TryGetValue(achievementName, out result))
			{
				return result;
			}
			return 0;
		}

		// Token: 0x040051DB RID: 20955
		private string _savePath;

		// Token: 0x040051DC RID: 20956
		private bool _isCloudSave;

		// Token: 0x040051DE RID: 20958
		private Dictionary<string, Achievement> _achievements = new Dictionary<string, Achievement>();

		// Token: 0x040051DF RID: 20959
		private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings();

		// Token: 0x040051E0 RID: 20960
		private byte[] _cryptoKey;

		// Token: 0x040051E1 RID: 20961
		private Dictionary<string, int> _achievementIconIndexes = new Dictionary<string, int>();

		// Token: 0x040051E2 RID: 20962
		private static object _ioLock = new object();

		// Token: 0x02000787 RID: 1927
		private class StoredAchievement
		{
			// Token: 0x040064AF RID: 25775
			[JsonProperty]
			public Dictionary<string, JObject> Conditions;
		}
	}
}
