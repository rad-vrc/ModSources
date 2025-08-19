using System;
using System.Collections.Generic;
using System.Threading;
using Steamworks;
using Terraria.Social.Base;

namespace Terraria.Social.Steam
{
	// Token: 0x020000E3 RID: 227
	public class AchievementsSocialModule : AchievementsSocialModule
	{
		// Token: 0x060017B9 RID: 6073 RVA: 0x004B9258 File Offset: 0x004B7458
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

		// Token: 0x060017BA RID: 6074 RVA: 0x004B928D File Offset: 0x004B748D
		public override void Shutdown()
		{
			this._userStatsReceived.Unregister();
			this.StoreStats();
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x004B92A0 File Offset: 0x004B74A0
		public override bool IsAchievementCompleted(string name)
		{
			bool pbAchieved;
			return SteamUserStats.GetAchievement(name, ref pbAchieved) && pbAchieved;
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x004B92B8 File Offset: 0x004B74B8
		public override byte[] GetEncryptionKey()
		{
			byte[] array = new byte[16];
			byte[] bytes = BitConverter.GetBytes(SteamUser.GetSteamID().m_SteamID);
			Array.Copy(bytes, array, 8);
			Array.Copy(bytes, 0, array, 8, 8);
			return array;
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x004B92EE File Offset: 0x004B74EE
		public override string GetSavePath()
		{
			return "/achievements-steam.dat";
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x004B92F8 File Offset: 0x004B74F8
		private int GetIntStat(string name)
		{
			int value;
			if (this._intStatCache.TryGetValue(name, out value))
			{
				return value;
			}
			if (SteamUserStats.GetStat(name, ref value))
			{
				this._intStatCache.Add(name, value);
			}
			return value;
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x004B9330 File Offset: 0x004B7530
		private float GetFloatStat(string name)
		{
			float value;
			if (this._floatStatCache.TryGetValue(name, out value))
			{
				return value;
			}
			if (SteamUserStats.GetStat(name, ref value))
			{
				this._floatStatCache.Add(name, value);
			}
			return value;
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x004B9367 File Offset: 0x004B7567
		private bool SetFloatStat(string name, float value)
		{
			this._floatStatCache[name] = value;
			return SteamUserStats.SetStat(name, value);
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x004B937D File Offset: 0x004B757D
		public override void UpdateIntStat(string name, int value)
		{
			if (this.GetIntStat(name) < value)
			{
				this.SetIntStat(name, value);
			}
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x004B9392 File Offset: 0x004B7592
		private bool SetIntStat(string name, int value)
		{
			this._intStatCache[name] = value;
			return SteamUserStats.SetStat(name, value);
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x004B93A8 File Offset: 0x004B75A8
		public override void UpdateFloatStat(string name, float value)
		{
			if (this.GetFloatStat(name) < value)
			{
				this.SetFloatStat(name, value);
			}
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x004B93BD File Offset: 0x004B75BD
		public override void StoreStats()
		{
			SteamUserStats.StoreStats();
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x004B93C5 File Offset: 0x004B75C5
		public override void CompleteAchievement(string name)
		{
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x004B93C7 File Offset: 0x004B75C7
		private void OnUserStatsReceived(UserStatsReceived_t results)
		{
			if (results.m_nGameID == 1281930UL && results.m_steamIDUser == SteamUser.GetSteamID())
			{
				this._areStatsReceived = true;
			}
		}

		// Token: 0x04001321 RID: 4897
		private const string FILE_NAME = "/achievements-steam.dat";

		// Token: 0x04001322 RID: 4898
		private Callback<UserStatsReceived_t> _userStatsReceived;

		// Token: 0x04001323 RID: 4899
		private bool _areStatsReceived;

		// Token: 0x04001324 RID: 4900
		private Dictionary<string, int> _intStatCache = new Dictionary<string, int>();

		// Token: 0x04001325 RID: 4901
		private Dictionary<string, float> _floatStatCache = new Dictionary<string, float>();
	}
}
