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
	// Token: 0x02000774 RID: 1908
	public class AchievementManager
	{
		// Token: 0x14000077 RID: 119
		// (add) Token: 0x06004D2A RID: 19754 RVA: 0x00672870 File Offset: 0x00670A70
		// (remove) Token: 0x06004D2B RID: 19755 RVA: 0x006728A8 File Offset: 0x00670AA8
		public event Achievement.AchievementCompleted OnAchievementCompleted;

		// Token: 0x06004D2C RID: 19756 RVA: 0x006728E0 File Offset: 0x00670AE0
		public AchievementManager()
		{
			this._savePath = Main.SavePath + Path.DirectorySeparatorChar.ToString() + "achievements.dat";
			this._isCloudSave = false;
			this._cryptoKey = Encoding.ASCII.GetBytes("RELOGIC-TERRARIA");
		}

		// Token: 0x06004D2D RID: 19757 RVA: 0x0067294F File Offset: 0x00670B4F
		public void Save()
		{
			FileUtilities.ProtectedInvoke(delegate
			{
				this.Save(this._savePath, this._isCloudSave);
			});
		}

		// Token: 0x06004D2E RID: 19758 RVA: 0x00672964 File Offset: 0x00670B64
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

		// Token: 0x06004D2F RID: 19759 RVA: 0x00672A70 File Offset: 0x00670C70
		public List<Achievement> CreateAchievementsList()
		{
			return this._achievements.Values.ToList<Achievement>();
		}

		// Token: 0x06004D30 RID: 19760 RVA: 0x00672A82 File Offset: 0x00670C82
		public void Load()
		{
			this.Load(this._savePath, this._isCloudSave);
		}

		// Token: 0x06004D31 RID: 19761 RVA: 0x00672A98 File Offset: 0x00670C98
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
					using (MemoryStream stream = new MemoryStream(buffer))
					{
						using (CryptoStream stream2 = new CryptoStream(stream, new RijndaelManaged().CreateDecryptor(this._cryptoKey, this._cryptoKey), CryptoStreamMode.Read))
						{
							using (BsonReader reader = new BsonReader(stream2))
							{
								dictionary = JsonSerializer.Create(this._serializerSettings).Deserialize<Dictionary<string, AchievementManager.StoredAchievement>>(reader);
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
				foreach (KeyValuePair<string, AchievementManager.StoredAchievement> item in dictionary)
				{
					if (this._achievements.ContainsKey(item.Key))
					{
						this._achievements[item.Key].Load(item.Value.Conditions);
					}
				}
				if (SocialAPI.Achievements != null)
				{
					foreach (KeyValuePair<string, Achievement> achievement in this._achievements)
					{
						if (achievement.Value.IsCompleted && !SocialAPI.Achievements.IsAchievementCompleted(achievement.Key))
						{
							flag = true;
							achievement.Value.ClearProgress();
						}
					}
				}
			}
			if (flag)
			{
				this.Save();
			}
		}

		// Token: 0x06004D32 RID: 19762 RVA: 0x00672CE4 File Offset: 0x00670EE4
		public void ClearAll()
		{
			if (SocialAPI.Achievements != null)
			{
				return;
			}
			foreach (KeyValuePair<string, Achievement> achievement in this._achievements)
			{
				achievement.Value.ClearProgress();
			}
			this.Save();
		}

		// Token: 0x06004D33 RID: 19763 RVA: 0x00672D4C File Offset: 0x00670F4C
		private void AchievementCompleted(Achievement achievement)
		{
			this.Save();
			if (this.OnAchievementCompleted != null)
			{
				this.OnAchievementCompleted(achievement);
			}
		}

		// Token: 0x06004D34 RID: 19764 RVA: 0x00672D68 File Offset: 0x00670F68
		public void Register(Achievement achievement)
		{
			this._achievements.Add(achievement.Name, achievement);
			achievement.OnCompleted += this.AchievementCompleted;
		}

		// Token: 0x06004D35 RID: 19765 RVA: 0x00672D8E File Offset: 0x00670F8E
		public void RegisterIconIndex(string achievementName, int iconIndex)
		{
			this._achievementIconIndexes.Add(achievementName, iconIndex);
		}

		// Token: 0x06004D36 RID: 19766 RVA: 0x00672D9D File Offset: 0x00670F9D
		public void RegisterAchievementCategory(string achievementName, AchievementCategory category)
		{
			this._achievements[achievementName].SetCategory(category);
		}

		// Token: 0x06004D37 RID: 19767 RVA: 0x00672DB4 File Offset: 0x00670FB4
		public Achievement GetAchievement(string achievementName)
		{
			Achievement value;
			if (this._achievements.TryGetValue(achievementName, out value))
			{
				return value;
			}
			return null;
		}

		// Token: 0x06004D38 RID: 19768 RVA: 0x00672DD4 File Offset: 0x00670FD4
		public T GetCondition<T>(string achievementName, string conditionName) where T : AchievementCondition
		{
			return this.GetCondition(achievementName, conditionName) as T;
		}

		// Token: 0x06004D39 RID: 19769 RVA: 0x00672DE8 File Offset: 0x00670FE8
		public AchievementCondition GetCondition(string achievementName, string conditionName)
		{
			Achievement value;
			if (this._achievements.TryGetValue(achievementName, out value))
			{
				return value.GetCondition(conditionName);
			}
			return null;
		}

		// Token: 0x06004D3A RID: 19770 RVA: 0x00672E10 File Offset: 0x00671010
		public int GetIconIndex(string achievementName)
		{
			int value;
			if (this._achievementIconIndexes.TryGetValue(achievementName, out value))
			{
				return value;
			}
			return 0;
		}

		// Token: 0x04006154 RID: 24916
		private string _savePath;

		// Token: 0x04006155 RID: 24917
		private bool _isCloudSave;

		// Token: 0x04006156 RID: 24918
		private Dictionary<string, Achievement> _achievements = new Dictionary<string, Achievement>();

		// Token: 0x04006157 RID: 24919
		private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings();

		// Token: 0x04006158 RID: 24920
		private byte[] _cryptoKey;

		// Token: 0x04006159 RID: 24921
		private Dictionary<string, int> _achievementIconIndexes = new Dictionary<string, int>();

		// Token: 0x0400615A RID: 24922
		private static object _ioLock = new object();

		// Token: 0x02000D74 RID: 3444
		private class StoredAchievement
		{
			// Token: 0x04007BAC RID: 31660
			[JsonProperty]
			public Dictionary<string, JObject> Conditions;
		}
	}
}
