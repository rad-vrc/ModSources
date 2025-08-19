using System;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200067D RID: 1661
	public class CommonEnemyUICollectionInfoProvider : IBestiaryUICollectionInfoProvider
	{
		// Token: 0x060047C5 RID: 18373 RVA: 0x006465CA File Offset: 0x006447CA
		public CommonEnemyUICollectionInfoProvider(string persistentId, bool quickUnlock)
		{
			this._persistentIdentifierToCheck = persistentId;
			this._quickUnlock = quickUnlock;
			this._killCountNeededToFullyUnlock = CommonEnemyUICollectionInfoProvider.GetKillCountNeeded(persistentId);
		}

		// Token: 0x060047C6 RID: 18374 RVA: 0x006465EC File Offset: 0x006447EC
		public static int GetKillCountNeeded(string persistentId)
		{
			int defaultKillsForBannerNeeded = ItemID.Sets.DefaultKillsForBannerNeeded;
			int value;
			if (!ContentSamples.NpcNetIdsByPersistentIds.TryGetValue(persistentId, out value))
			{
				return defaultKillsForBannerNeeded;
			}
			NPC value2;
			if (!ContentSamples.NpcsByNetId.TryGetValue(value, out value2))
			{
				return defaultKillsForBannerNeeded;
			}
			int num = Item.BannerToItem(Item.NPCtoBanner(value2.BannerID()));
			return ItemID.Sets.KillsToBanner[num];
		}

		// Token: 0x060047C7 RID: 18375 RVA: 0x0064663C File Offset: 0x0064483C
		public BestiaryUICollectionInfo GetEntryUICollectionInfo()
		{
			int killCount = Main.BestiaryTracker.Kills.GetKillCount(this._persistentIdentifierToCheck);
			BestiaryEntryUnlockState unlockStateByKillCount = this.GetUnlockStateByKillCount(killCount, this._quickUnlock);
			return new BestiaryUICollectionInfo
			{
				UnlockState = unlockStateByKillCount
			};
		}

		// Token: 0x060047C8 RID: 18376 RVA: 0x00646680 File Offset: 0x00644880
		public BestiaryEntryUnlockState GetUnlockStateByKillCount(int killCount, bool quickUnlock)
		{
			int killCountNeededToFullyUnlock = this._killCountNeededToFullyUnlock;
			return CommonEnemyUICollectionInfoProvider.GetUnlockStateByKillCount(killCount, quickUnlock, killCountNeededToFullyUnlock);
		}

		// Token: 0x060047C9 RID: 18377 RVA: 0x0064669C File Offset: 0x0064489C
		public static BestiaryEntryUnlockState GetUnlockStateByKillCount(int killCount, bool quickUnlock, int fullKillCountNeeded)
		{
			int num = fullKillCountNeeded / 2;
			int num2 = fullKillCountNeeded / 5;
			if (quickUnlock && killCount > 0)
			{
				return BestiaryEntryUnlockState.CanShowDropsWithDropRates_4;
			}
			if (killCount >= fullKillCountNeeded)
			{
				return BestiaryEntryUnlockState.CanShowDropsWithDropRates_4;
			}
			if (killCount >= num)
			{
				return BestiaryEntryUnlockState.CanShowDropsWithoutDropRates_3;
			}
			if (killCount >= num2)
			{
				return BestiaryEntryUnlockState.CanShowStats_2;
			}
			if (killCount >= 1)
			{
				return BestiaryEntryUnlockState.CanShowPortraitOnly_1;
			}
			return BestiaryEntryUnlockState.NotKnownAtAll_0;
		}

		// Token: 0x060047CA RID: 18378 RVA: 0x006466D3 File Offset: 0x006448D3
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x04005BF7 RID: 23543
		private string _persistentIdentifierToCheck;

		// Token: 0x04005BF8 RID: 23544
		private bool _quickUnlock;

		// Token: 0x04005BF9 RID: 23545
		private int _killCountNeededToFullyUnlock;
	}
}
