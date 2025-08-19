using System;
using System.Threading;
using rail;
using Terraria.Social.Base;

namespace Terraria.Social.WeGame
{
	// Token: 0x020000CB RID: 203
	public class AchievementsSocialModule : AchievementsSocialModule
	{
		// Token: 0x060016B2 RID: 5810 RVA: 0x004B57C8 File Offset: 0x004B39C8
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

		// Token: 0x060016B3 RID: 5811 RVA: 0x004B5857 File Offset: 0x004B3A57
		public override void Shutdown()
		{
			this.StoreStats();
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x004B5860 File Offset: 0x004B3A60
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

		// Token: 0x060016B5 RID: 5813 RVA: 0x004B58A4 File Offset: 0x004B3AA4
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

		// Token: 0x060016B6 RID: 5814 RVA: 0x004B58E8 File Offset: 0x004B3AE8
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

		// Token: 0x060016B7 RID: 5815 RVA: 0x004B590C File Offset: 0x004B3B0C
		public override bool IsAchievementCompleted(string name)
		{
			bool achieved = false;
			RailResult railResult = 1;
			IRailPlayerAchievement myPlayerAchievement = this.GetMyPlayerAchievement();
			if (myPlayerAchievement != null)
			{
				railResult = myPlayerAchievement.HasAchieved(name, ref achieved);
			}
			return achieved && railResult == 0;
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x004B593C File Offset: 0x004B3B3C
		public override byte[] GetEncryptionKey()
		{
			RailComparableID railID = rail_api.RailFactory().RailPlayer().GetRailID();
			byte[] array = new byte[16];
			byte[] bytes = BitConverter.GetBytes(railID.id_);
			Array.Copy(bytes, array, 8);
			Array.Copy(bytes, 0, array, 8, 8);
			return array;
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x004B597C File Offset: 0x004B3B7C
		public override string GetSavePath()
		{
			return "/achievements-wegame.dat";
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x004B5984 File Offset: 0x004B3B84
		private int GetIntStat(string name)
		{
			int data = 0;
			IRailPlayerStats myPlayerStats = this.GetMyPlayerStats();
			if (myPlayerStats != null)
			{
				myPlayerStats.GetStatValue(name, ref data);
			}
			return data;
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x004B59AC File Offset: 0x004B3BAC
		private float GetFloatStat(string name)
		{
			double data = 0.0;
			IRailPlayerStats myPlayerStats = this.GetMyPlayerStats();
			if (myPlayerStats != null)
			{
				myPlayerStats.GetStatValue(name, ref data);
			}
			return (float)data;
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x004B59DC File Offset: 0x004B3BDC
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

		// Token: 0x060016BD RID: 5821 RVA: 0x004B5A04 File Offset: 0x004B3C04
		public override void UpdateIntStat(string name, int value)
		{
			IRailPlayerStats myPlayerStats = this.GetMyPlayerStats();
			if (myPlayerStats != null)
			{
				int data = 0;
				if (myPlayerStats.GetStatValue(name, ref data) == null && data < value)
				{
					myPlayerStats.SetStatValue(name, value);
				}
			}
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x004B5A38 File Offset: 0x004B3C38
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

		// Token: 0x060016BF RID: 5823 RVA: 0x004B5A60 File Offset: 0x004B3C60
		public override void UpdateFloatStat(string name, float value)
		{
			IRailPlayerStats myPlayerStats = this.GetMyPlayerStats();
			if (myPlayerStats != null)
			{
				double data = 0.0;
				if (myPlayerStats.GetStatValue(name, ref data) == null && (float)data < value)
				{
					myPlayerStats.SetStatValue(name, (double)value);
				}
			}
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x004B5A9B File Offset: 0x004B3C9B
		public override void StoreStats()
		{
			this.SaveStats();
			this.SaveAchievement();
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x004B5AA9 File Offset: 0x004B3CA9
		private void SaveStats()
		{
			IRailPlayerStats myPlayerStats = this.GetMyPlayerStats();
			if (myPlayerStats == null)
			{
				return;
			}
			myPlayerStats.AsyncStoreStats("");
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x004B5AC1 File Offset: 0x004B3CC1
		private void SaveAchievement()
		{
			IRailPlayerAchievement myPlayerAchievement = this.GetMyPlayerAchievement();
			if (myPlayerAchievement == null)
			{
				return;
			}
			myPlayerAchievement.AsyncStoreAchievement("");
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x004B5AD9 File Offset: 0x004B3CD9
		public override void CompleteAchievement(string name)
		{
			IRailPlayerAchievement myPlayerAchievement = this.GetMyPlayerAchievement();
			if (myPlayerAchievement == null)
			{
				return;
			}
			myPlayerAchievement.MakeAchievement(name);
		}

		// Token: 0x040012C7 RID: 4807
		private const string FILE_NAME = "/achievements-wegame.dat";

		// Token: 0x040012C8 RID: 4808
		private bool _areStatsReceived;

		// Token: 0x040012C9 RID: 4809
		private bool _areAchievementReceived;

		// Token: 0x040012CA RID: 4810
		private RailCallBackHelper _callbackHelper = new RailCallBackHelper();

		// Token: 0x040012CB RID: 4811
		private IRailPlayerAchievement _playerAchievement;

		// Token: 0x040012CC RID: 4812
		private IRailPlayerStats _playerStats;
	}
}
