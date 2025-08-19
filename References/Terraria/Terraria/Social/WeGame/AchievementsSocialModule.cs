using System;
using System.Threading;
using rail;
using Terraria.Social.Base;

namespace Terraria.Social.WeGame
{
	// Token: 0x02000154 RID: 340
	public class AchievementsSocialModule : AchievementsSocialModule
	{
		// Token: 0x06001940 RID: 6464 RVA: 0x004E07C4 File Offset: 0x004DE9C4
		public override void Initialize()
		{
			this._callbackHelper.RegisterCallback(2001, new RailEventCallBackHandler(this.RailEventCallBack));
			this._callbackHelper.RegisterCallback(2101, new RailEventCallBackHandler(this.RailEventCallBack));
			IRailPlayerStats myPlayerStats = this.GetMyPlayerStats();
			IRailPlayerAchievement myPlayerAchievement = this.GetMyPlayerAchievement();
			if (myPlayerStats != null && myPlayerAchievement != null)
			{
				myPlayerStats.AsyncRequestStats("");
				myPlayerAchievement.AsyncRequestAchievement("");
				while (!this._areStatsReceived && !this._areAchievementReceived)
				{
					CoreSocialModule.RailEventTick();
					Thread.Sleep(10);
				}
			}
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x004E0853 File Offset: 0x004DEA53
		public override void Shutdown()
		{
			this.StoreStats();
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x004E085C File Offset: 0x004DEA5C
		private IRailPlayerStats GetMyPlayerStats()
		{
			if (this._playerStats == null)
			{
				IRailStatisticHelper railStatisticHelper = rail_api.RailFactory().RailStatisticHelper();
				if (railStatisticHelper != null)
				{
					this._playerStats = railStatisticHelper.CreatePlayerStats(new RailID
					{
						id_ = 0UL
					});
				}
			}
			return this._playerStats;
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x004E08A0 File Offset: 0x004DEAA0
		private IRailPlayerAchievement GetMyPlayerAchievement()
		{
			if (this._playerAchievement == null)
			{
				IRailAchievementHelper railAchievementHelper = rail_api.RailFactory().RailAchievementHelper();
				if (railAchievementHelper != null)
				{
					this._playerAchievement = railAchievementHelper.CreatePlayerAchievement(new RailID
					{
						id_ = 0UL
					});
				}
			}
			return this._playerAchievement;
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x004E08E4 File Offset: 0x004DEAE4
		public void RailEventCallBack(RAILEventID eventId, EventBase data)
		{
			if (eventId == 2001)
			{
				this._areStatsReceived = true;
				return;
			}
			if (eventId != 2101)
			{
				return;
			}
			this._areAchievementReceived = true;
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x004E0908 File Offset: 0x004DEB08
		public override bool IsAchievementCompleted(string name)
		{
			bool flag = false;
			RailResult railResult = 1;
			IRailPlayerAchievement myPlayerAchievement = this.GetMyPlayerAchievement();
			if (myPlayerAchievement != null)
			{
				railResult = myPlayerAchievement.HasAchieved(name, ref flag);
			}
			return flag && railResult == 0;
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x004E0938 File Offset: 0x004DEB38
		public override byte[] GetEncryptionKey()
		{
			RailComparableID railID = rail_api.RailFactory().RailPlayer().GetRailID();
			byte[] array = new byte[16];
			byte[] bytes = BitConverter.GetBytes(railID.id_);
			Array.Copy(bytes, array, 8);
			Array.Copy(bytes, 0, array, 8, 8);
			return array;
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x004E0978 File Offset: 0x004DEB78
		public override string GetSavePath()
		{
			return "/achievements-wegame.dat";
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x004E0980 File Offset: 0x004DEB80
		private int GetIntStat(string name)
		{
			int result = 0;
			IRailPlayerStats myPlayerStats = this.GetMyPlayerStats();
			if (myPlayerStats != null)
			{
				myPlayerStats.GetStatValue(name, ref result);
			}
			return result;
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x004E09A4 File Offset: 0x004DEBA4
		private float GetFloatStat(string name)
		{
			double num = 0.0;
			IRailPlayerStats myPlayerStats = this.GetMyPlayerStats();
			if (myPlayerStats != null)
			{
				myPlayerStats.GetStatValue(name, ref num);
			}
			return (float)num;
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x004E09D4 File Offset: 0x004DEBD4
		private bool SetFloatStat(string name, float value)
		{
			IRailPlayerStats myPlayerStats = this.GetMyPlayerStats();
			RailResult railResult = 1;
			if (myPlayerStats != null)
			{
				railResult = myPlayerStats.SetStatValue(name, (double)value);
			}
			return railResult == 0;
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x004E09FC File Offset: 0x004DEBFC
		public override void UpdateIntStat(string name, int value)
		{
			IRailPlayerStats myPlayerStats = this.GetMyPlayerStats();
			if (myPlayerStats != null)
			{
				int num = 0;
				if (myPlayerStats.GetStatValue(name, ref num) == null && num < value)
				{
					myPlayerStats.SetStatValue(name, value);
				}
			}
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x004E0A30 File Offset: 0x004DEC30
		private bool SetIntStat(string name, int value)
		{
			IRailPlayerStats myPlayerStats = this.GetMyPlayerStats();
			RailResult railResult = 1;
			if (myPlayerStats != null)
			{
				railResult = myPlayerStats.SetStatValue(name, value);
			}
			return railResult == 0;
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x004E0A58 File Offset: 0x004DEC58
		public override void UpdateFloatStat(string name, float value)
		{
			IRailPlayerStats myPlayerStats = this.GetMyPlayerStats();
			if (myPlayerStats != null)
			{
				double num = 0.0;
				if (myPlayerStats.GetStatValue(name, ref num) == null && (float)num < value)
				{
					myPlayerStats.SetStatValue(name, (double)value);
				}
			}
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x004E0A93 File Offset: 0x004DEC93
		public override void StoreStats()
		{
			this.SaveStats();
			this.SaveAchievement();
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x004E0AA4 File Offset: 0x004DECA4
		private void SaveStats()
		{
			IRailPlayerStats myPlayerStats = this.GetMyPlayerStats();
			if (myPlayerStats != null)
			{
				myPlayerStats.AsyncStoreStats("");
			}
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x004E0AC8 File Offset: 0x004DECC8
		private void SaveAchievement()
		{
			IRailPlayerAchievement myPlayerAchievement = this.GetMyPlayerAchievement();
			if (myPlayerAchievement != null)
			{
				myPlayerAchievement.AsyncStoreAchievement("");
			}
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x004E0AEC File Offset: 0x004DECEC
		public override void CompleteAchievement(string name)
		{
			IRailPlayerAchievement myPlayerAchievement = this.GetMyPlayerAchievement();
			if (myPlayerAchievement != null)
			{
				myPlayerAchievement.MakeAchievement(name);
			}
		}

		// Token: 0x04001535 RID: 5429
		private const string FILE_NAME = "/achievements-wegame.dat";

		// Token: 0x04001536 RID: 5430
		private bool _areStatsReceived;

		// Token: 0x04001537 RID: 5431
		private bool _areAchievementReceived;

		// Token: 0x04001538 RID: 5432
		private RailCallBackHelper _callbackHelper = new RailCallBackHelper();

		// Token: 0x04001539 RID: 5433
		private IRailPlayerAchievement _playerAchievement;

		// Token: 0x0400153A RID: 5434
		private IRailPlayerStats _playerStats;
	}
}
