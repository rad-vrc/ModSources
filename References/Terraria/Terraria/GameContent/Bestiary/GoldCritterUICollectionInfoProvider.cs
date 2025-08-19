using System;
using Terraria.ID;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000306 RID: 774
	public class GoldCritterUICollectionInfoProvider : IBestiaryUICollectionInfoProvider
	{
		// Token: 0x060023DB RID: 9179 RVA: 0x00558304 File Offset: 0x00556504
		public GoldCritterUICollectionInfoProvider(int[] normalCritterPersistentId, string goldCritterPersistentId)
		{
			this._normalCritterPersistentId = new string[normalCritterPersistentId.Length];
			for (int i = 0; i < normalCritterPersistentId.Length; i++)
			{
				this._normalCritterPersistentId[i] = ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[normalCritterPersistentId[i]];
			}
			this._goldCritterPersistentId = goldCritterPersistentId;
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x00558350 File Offset: 0x00556550
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

		// Token: 0x060023DD RID: 9181 RVA: 0x005583D8 File Offset: 0x005565D8
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

		// Token: 0x060023DE RID: 9182 RVA: 0x0055841F File Offset: 0x0055661F
		private BestiaryEntryUnlockState GetUnlockStateForCritter(string persistentId)
		{
			if (!Main.BestiaryTracker.Sights.GetWasNearbyBefore(persistentId))
			{
				return BestiaryEntryUnlockState.NotKnownAtAll_0;
			}
			return BestiaryEntryUnlockState.CanShowDropsWithDropRates_4;
		}

		// Token: 0x04004856 RID: 18518
		private string[] _normalCritterPersistentId;

		// Token: 0x04004857 RID: 18519
		private string _goldCritterPersistentId;
	}
}
