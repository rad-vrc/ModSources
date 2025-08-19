using System;
using Terraria.ID;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000684 RID: 1668
	public class GoldCritterUICollectionInfoProvider : IBestiaryUICollectionInfoProvider
	{
		// Token: 0x060047E4 RID: 18404 RVA: 0x00646C0C File Offset: 0x00644E0C
		public GoldCritterUICollectionInfoProvider(int[] normalCritterPersistentId, string goldCritterPersistentId)
		{
			this._normalCritterPersistentId = new string[normalCritterPersistentId.Length];
			for (int i = 0; i < normalCritterPersistentId.Length; i++)
			{
				this._normalCritterPersistentId[i] = ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[normalCritterPersistentId[i]];
			}
			this._goldCritterPersistentId = goldCritterPersistentId;
		}

		// Token: 0x060047E5 RID: 18405 RVA: 0x00646C58 File Offset: 0x00644E58
		public BestiaryUICollectionInfo GetEntryUICollectionInfo()
		{
			BestiaryEntryUnlockState unlockStateForCritter = this.GetUnlockStateForCritter(this._goldCritterPersistentId);
			BestiaryEntryUnlockState bestiaryEntryUnlockState = BestiaryEntryUnlockState.NotKnownAtAll_0;
			if (unlockStateForCritter > bestiaryEntryUnlockState)
			{
				bestiaryEntryUnlockState = unlockStateForCritter;
			}
			foreach (string persistentId in this._normalCritterPersistentId)
			{
				BestiaryEntryUnlockState unlockStateForCritter2 = this.GetUnlockStateForCritter(persistentId);
				if (unlockStateForCritter2 > bestiaryEntryUnlockState)
				{
					bestiaryEntryUnlockState = unlockStateForCritter2;
				}
			}
			BestiaryUICollectionInfo result = new BestiaryUICollectionInfo
			{
				UnlockState = bestiaryEntryUnlockState
			};
			if (bestiaryEntryUnlockState == BestiaryEntryUnlockState.NotKnownAtAll_0)
			{
				return result;
			}
			if (!this.TryFindingOneGoldCritterThatIsAlreadyUnlocked())
			{
				return new BestiaryUICollectionInfo
				{
					UnlockState = BestiaryEntryUnlockState.NotKnownAtAll_0
				};
			}
			return result;
		}

		// Token: 0x060047E6 RID: 18406 RVA: 0x00646CE0 File Offset: 0x00644EE0
		private bool TryFindingOneGoldCritterThatIsAlreadyUnlocked()
		{
			for (int i = 0; i < NPCID.Sets.GoldCrittersCollection.Count; i++)
			{
				int key = NPCID.Sets.GoldCrittersCollection[i];
				string persistentId = ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[key];
				if (this.GetUnlockStateForCritter(persistentId) > BestiaryEntryUnlockState.NotKnownAtAll_0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060047E7 RID: 18407 RVA: 0x00646D27 File Offset: 0x00644F27
		private BestiaryEntryUnlockState GetUnlockStateForCritter(string persistentId)
		{
			if (!Main.BestiaryTracker.Sights.GetWasNearbyBefore(persistentId))
			{
				return BestiaryEntryUnlockState.NotKnownAtAll_0;
			}
			return BestiaryEntryUnlockState.CanShowDropsWithDropRates_4;
		}

		// Token: 0x04005C09 RID: 23561
		private string[] _normalCritterPersistentId;

		// Token: 0x04005C0A RID: 23562
		private string _goldCritterPersistentId;
	}
}
