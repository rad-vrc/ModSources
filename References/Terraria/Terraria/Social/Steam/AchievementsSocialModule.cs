using System;
using System.Collections.Generic;
using System.Threading;
using Steamworks;
using Terraria.Social.Base;

namespace Terraria.Social.Steam
{
	// Token: 0x02000173 RID: 371
	public class AchievementsSocialModule : AchievementsSocialModule
	{
		// Token: 0x06001A66 RID: 6758 RVA: 0x004E4846 File Offset: 0x004E2A46
		public override void Initialize()
		{
			this._userStatsReceived = Callback<UserStatsReceived_t>.Create(new Callback<UserStatsReceived_t>.DispatchDelegate(this.OnUserStatsReceived));
			SteamUserStats.RequestCurrentStats();
			while (!this._areStatsReceived)
			{
				CoreSocialModule.Pulse();
				Thread.Sleep(10);
			}
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x004E487B File Offset: 0x004E2A7B
		public override void Shutdown()
		{
			this._userStatsReceived.Unregister();
			this.StoreStats();
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x004E4890 File Offset: 0x004E2A90
		public override bool IsAchievementCompleted(string name)
		{
			bool flag;
			return SteamUserStats.GetAchievement(name, ref flag) && flag;
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x004E48A8 File Offset: 0x004E2AA8
		public override byte[] GetEncryptionKey()
		{
			byte[] array = new byte[16];
			byte[] bytes = BitConverter.GetBytes(SteamUser.GetSteamID().m_SteamID);
			Array.Copy(bytes, array, 8);
			Array.Copy(bytes, 0, array, 8, 8);
			return array;
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x004E48DE File Offset: 0x004E2ADE
		public override string GetSavePath()
		{
			return "/achievements-steam.dat";
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x004E48E8 File Offset: 0x004E2AE8
		private int GetIntStat(string name)
		{
			int num;
			if (this._intStatCache.TryGetValue(name, out num))
			{
				return num;
			}
			if (SteamUserStats.GetStat(name, ref num))
			{
				this._intStatCache.Add(name, num);
			}
			return num;
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x004E4920 File Offset: 0x004E2B20
		private float GetFloatStat(string name)
		{
			float num;
			if (this._floatStatCache.TryGetValue(name, out num))
			{
				return num;
			}
			if (SteamUserStats.GetStat(name, ref num))
			{
				this._floatStatCache.Add(name, num);
			}
			return num;
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x004E4957 File Offset: 0x004E2B57
		private bool SetFloatStat(string name, float value)
		{
			this._floatStatCache[name] = value;
			return SteamUserStats.SetStat(name, value);
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x004E496D File Offset: 0x004E2B6D
		public override void UpdateIntStat(string name, int value)
		{
			if (this.GetIntStat(name) < value)
			{
				this.SetIntStat(name, value);
			}
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x004E4982 File Offset: 0x004E2B82
		private bool SetIntStat(string name, int value)
		{
			this._intStatCache[name] = value;
			return SteamUserStats.SetStat(name, value);
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x004E4998 File Offset: 0x004E2B98
		public override void UpdateFloatStat(string name, float value)
		{
			if (this.GetFloatStat(name) < value)
			{
				this.SetFloatStat(name, value);
			}
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x004E49AD File Offset: 0x004E2BAD
		public override void StoreStats()
		{
			SteamUserStats.StoreStats();
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x004E49B5 File Offset: 0x004E2BB5
		public override void CompleteAchievement(string name)
		{
			SteamUserStats.SetAchievement(name);
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x004E49BE File Offset: 0x004E2BBE
		private void OnUserStatsReceived(UserStatsReceived_t results)
		{
			if (results.m_nGameID == 105600UL && results.m_steamIDUser == SteamUser.GetSteamID())
			{
				this._areStatsReceived = true;
			}
		}

		// Token: 0x04001597 RID: 5527
		private const string FILE_NAME = "/achievements-steam.dat";

		// Token: 0x04001598 RID: 5528
		private Callback<UserStatsReceived_t> _userStatsReceived;

		// Token: 0x04001599 RID: 5529
		private bool _areStatsReceived;

		// Token: 0x0400159A RID: 5530
		private Dictionary<string, int> _intStatCache = new Dictionary<string, int>();

		// Token: 0x0400159B RID: 5531
		private Dictionary<string, float> _floatStatCache = new Dictionary<string, float>();
	}
}
