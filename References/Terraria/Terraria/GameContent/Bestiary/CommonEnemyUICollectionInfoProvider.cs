using System;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000304 RID: 772
	public class CommonEnemyUICollectionInfoProvider : IBestiaryUICollectionInfoProvider
	{
		// Token: 0x060023D2 RID: 9170 RVA: 0x00558170 File Offset: 0x00556370
		public CommonEnemyUICollectionInfoProvider(string persistentId, bool quickUnlock)
		{
			this._persistentIdentifierToCheck = persistentId;
			this._quickUnlock = quickUnlock;
			this._killCountNeededToFullyUnlock = CommonEnemyUICollectionInfoProvider.GetKillCountNeeded(persistentId);
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x00558194 File Offset: 0x00556394
		public static int GetKillCountNeeded(string persistentId)
		{
			int defaultKillsForBannerNeeded = ItemID.Sets.DefaultKillsForBannerNeeded;
			int key;
			if (!ContentSamples.NpcNetIdsByPersistentIds.TryGetValue(persistentId, out key))
			{
				return defaultKillsForBannerNeeded;
			}
			NPC npc;
			if (!ContentSamples.NpcsByNetId.TryGetValue(key, out npc))
			{
				return defaultKillsForBannerNeeded;
			}
			int num = Item.BannerToItem(Item.NPCtoBanner(npc.BannerID()));
			return ItemID.Sets.KillsToBanner[num];
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x005581E4 File Offset: 0x005563E4
		public BestiaryUICollectionInfo GetEntryUICollectionInfo()
		{
			int killCount = Main.BestiaryTracker.Kills.GetKillCount(this._persistentIdentifierToCheck);
			BestiaryEntryUnlockState unlockStateByKillCount = this.GetUnlockStateByKillCount(killCount, this._quickUnlock);
			return new BestiaryUICollectionInfo
			{
				UnlockState = unlockStateByKillCount
			};
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x00558228 File Offset: 0x00556428
		public BestiaryEntryUnlockState GetUnlockStateByKillCount(int killCount, bool quickUnlock)
		{
			int killCountNeededToFullyUnlock = this._killCountNeededToFullyUnlock;
			return CommonEnemyUICollectionInfoProvider.GetUnlockStateByKillCount(killCount, quickUnlock, killCountNeededToFullyUnlock);
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x00558244 File Offset: 0x00556444
		public static BestiaryEntryUnlockState GetUnlockStateByKillCount(int killCount, bool quickUnlock, int fullKillCountNeeded)
		{
			int num = fullKillCountNeeded / 2;
			int num2 = fullKillCountNeeded / 5;
			BestiaryEntryUnlockState result;
			if (quickUnlock && killCount > 0)
			{
				result = BestiaryEntryUnlockState.CanShowDropsWithDropRates_4;
			}
			else if (killCount >= fullKillCountNeeded)
			{
				result = BestiaryEntryUnlockState.CanShowDropsWithDropRates_4;
			}
			else if (killCount >= num)
			{
				result = BestiaryEntryUnlockState.CanShowDropsWithoutDropRates_3;
			}
			else if (killCount >= num2)
			{
				result = BestiaryEntryUnlockState.CanShowStats_2;
			}
			else if (killCount >= 1)
			{
				result = BestiaryEntryUnlockState.CanShowPortraitOnly_1;
			}
			else
			{
				result = BestiaryEntryUnlockState.NotKnownAtAll_0;
			}
			return result;
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x0006A9EF File Offset: 0x00068BEF
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x04004851 RID: 18513
		private string _persistentIdentifierToCheck;

		// Token: 0x04004852 RID: 18514
		private bool _quickUnlock;

		// Token: 0x04004853 RID: 18515
		private int _killCountNeededToFullyUnlock;
	}
}
