using System;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000307 RID: 775
	public class SalamanderShellyDadUICollectionInfoProvider : IBestiaryUICollectionInfoProvider
	{
		// Token: 0x060023DF RID: 9183 RVA: 0x00558436 File Offset: 0x00556636
		public SalamanderShellyDadUICollectionInfoProvider(string persistentId)
		{
			this._persistentIdentifierToCheck = persistentId;
			this._killCountNeededToFullyUnlock = CommonEnemyUICollectionInfoProvider.GetKillCountNeeded(persistentId);
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x00558454 File Offset: 0x00556654
		public BestiaryUICollectionInfo GetEntryUICollectionInfo()
		{
			BestiaryEntryUnlockState bestiaryEntryUnlockState = CommonEnemyUICollectionInfoProvider.GetUnlockStateByKillCount(Main.BestiaryTracker.Kills.GetKillCount(this._persistentIdentifierToCheck), false, this._killCountNeededToFullyUnlock);
			if (!this.IsIncludedInCurrentWorld())
			{
				bestiaryEntryUnlockState = this.GetLowestAvailableUnlockStateFromEntriesThatAreInWorld(bestiaryEntryUnlockState);
			}
			return new BestiaryUICollectionInfo
			{
				UnlockState = bestiaryEntryUnlockState
			};
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x005584A4 File Offset: 0x005566A4
		private BestiaryEntryUnlockState GetLowestAvailableUnlockStateFromEntriesThatAreInWorld(BestiaryEntryUnlockState unlockstatus)
		{
			BestiaryEntryUnlockState bestiaryEntryUnlockState = BestiaryEntryUnlockState.CanShowDropsWithDropRates_4;
			int[,] cavernMonsterType = NPC.cavernMonsterType;
			for (int i = 0; i < cavernMonsterType.GetLength(0); i++)
			{
				for (int j = 0; j < cavernMonsterType.GetLength(1); j++)
				{
					string persistentId = ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[cavernMonsterType[i, j]];
					BestiaryEntryUnlockState unlockStateByKillCount = CommonEnemyUICollectionInfoProvider.GetUnlockStateByKillCount(Main.BestiaryTracker.Kills.GetKillCount(persistentId), false, this._killCountNeededToFullyUnlock);
					if (bestiaryEntryUnlockState > unlockStateByKillCount)
					{
						bestiaryEntryUnlockState = unlockStateByKillCount;
					}
				}
			}
			return bestiaryEntryUnlockState;
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x0055851C File Offset: 0x0055671C
		private bool IsIncludedInCurrentWorld()
		{
			int num = ContentSamples.NpcNetIdsByPersistentIds[this._persistentIdentifierToCheck];
			int[,] cavernMonsterType = NPC.cavernMonsterType;
			for (int i = 0; i < cavernMonsterType.GetLength(0); i++)
			{
				for (int j = 0; j < cavernMonsterType.GetLength(1); j++)
				{
					if (ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[cavernMonsterType[i, j]] == this._persistentIdentifierToCheck)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x0006A9EF File Offset: 0x00068BEF
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x04004858 RID: 18520
		private string _persistentIdentifierToCheck;

		// Token: 0x04004859 RID: 18521
		private int _killCountNeededToFullyUnlock;
	}
}
