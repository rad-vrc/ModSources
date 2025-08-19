using System;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x020006A4 RID: 1700
	public class SalamanderShellyDadUICollectionInfoProvider : IBestiaryUICollectionInfoProvider
	{
		// Token: 0x0600485A RID: 18522 RVA: 0x00648F46 File Offset: 0x00647146
		public SalamanderShellyDadUICollectionInfoProvider(string persistentId)
		{
			this._persistentIdentifierToCheck = persistentId;
			this._killCountNeededToFullyUnlock = CommonEnemyUICollectionInfoProvider.GetKillCountNeeded(persistentId);
		}

		// Token: 0x0600485B RID: 18523 RVA: 0x00648F64 File Offset: 0x00647164
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

		// Token: 0x0600485C RID: 18524 RVA: 0x00648FB4 File Offset: 0x006471B4
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

		// Token: 0x0600485D RID: 18525 RVA: 0x0064902C File Offset: 0x0064722C
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

		// Token: 0x0600485E RID: 18526 RVA: 0x00649096 File Offset: 0x00647296
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x04005C2B RID: 23595
		private string _persistentIdentifierToCheck;

		// Token: 0x04005C2C RID: 23596
		private int _killCountNeededToFullyUnlock;
	}
}
