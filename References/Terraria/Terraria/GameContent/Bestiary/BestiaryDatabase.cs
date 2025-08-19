using System;
using System.Collections.Generic;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x020002E9 RID: 745
	public class BestiaryDatabase
	{
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x0600235C RID: 9052 RVA: 0x0054EA54 File Offset: 0x0054CC54
		public List<BestiaryEntry> Entries
		{
			get
			{
				return this._entries;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x0600235D RID: 9053 RVA: 0x0054EA5C File Offset: 0x0054CC5C
		public List<IBestiaryEntryFilter> Filters
		{
			get
			{
				return this._filters;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x0600235E RID: 9054 RVA: 0x0054EA64 File Offset: 0x0054CC64
		public List<IBestiarySortStep> SortSteps
		{
			get
			{
				return this._sortSteps;
			}
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x0054EA6C File Offset: 0x0054CC6C
		public BestiaryEntry Register(BestiaryEntry entry)
		{
			this._entries.Add(entry);
			for (int i = 0; i < entry.Info.Count; i++)
			{
				NPCNetIdBestiaryInfoElement npcnetIdBestiaryInfoElement = entry.Info[i] as NPCNetIdBestiaryInfoElement;
				if (npcnetIdBestiaryInfoElement != null)
				{
					this._byNpcId[npcnetIdBestiaryInfoElement.NetId] = entry;
				}
			}
			return entry;
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x0054EAC3 File Offset: 0x0054CCC3
		public IBestiaryEntryFilter Register(IBestiaryEntryFilter filter)
		{
			this._filters.Add(filter);
			return filter;
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x0054EAD2 File Offset: 0x0054CCD2
		public IBestiarySortStep Register(IBestiarySortStep sortStep)
		{
			this._sortSteps.Add(sortStep);
			return sortStep;
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x0054EAE4 File Offset: 0x0054CCE4
		public BestiaryEntry FindEntryByNPCID(int npcNetId)
		{
			BestiaryEntry result;
			if (this._byNpcId.TryGetValue(npcNetId, out result))
			{
				return result;
			}
			this._trashEntry.Info.Clear();
			return this._trashEntry;
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x0054EB1C File Offset: 0x0054CD1C
		public void Merge(ItemDropDatabase dropsDatabase)
		{
			for (int i = -65; i < (int)NPCID.Count; i++)
			{
				this.ExtractDropsForNPC(dropsDatabase, i);
			}
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x0054EB44 File Offset: 0x0054CD44
		private void ExtractDropsForNPC(ItemDropDatabase dropsDatabase, int npcId)
		{
			BestiaryEntry bestiaryEntry = this.FindEntryByNPCID(npcId);
			if (bestiaryEntry == null)
			{
				return;
			}
			List<IItemDropRule> rulesForNPCID = dropsDatabase.GetRulesForNPCID(npcId, false);
			List<DropRateInfo> list = new List<DropRateInfo>();
			DropRateInfoChainFeed ratesInfo = new DropRateInfoChainFeed(1f);
			foreach (IItemDropRule itemDropRule in rulesForNPCID)
			{
				itemDropRule.ReportDroprates(list, ratesInfo);
			}
			foreach (DropRateInfo info in list)
			{
				bestiaryEntry.Info.Add(new ItemDropBestiaryInfoElement(info));
			}
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x0054EC00 File Offset: 0x0054CE00
		public void ApplyPass(BestiaryDatabase.BestiaryEntriesPass pass)
		{
			for (int i = 0; i < this._entries.Count; i++)
			{
				pass(this._entries[i]);
			}
		}

		// Token: 0x04004823 RID: 18467
		private List<BestiaryEntry> _entries = new List<BestiaryEntry>();

		// Token: 0x04004824 RID: 18468
		private List<IBestiaryEntryFilter> _filters = new List<IBestiaryEntryFilter>();

		// Token: 0x04004825 RID: 18469
		private List<IBestiarySortStep> _sortSteps = new List<IBestiarySortStep>();

		// Token: 0x04004826 RID: 18470
		private Dictionary<int, BestiaryEntry> _byNpcId = new Dictionary<int, BestiaryEntry>();

		// Token: 0x04004827 RID: 18471
		private BestiaryEntry _trashEntry = new BestiaryEntry();

		// Token: 0x020006D6 RID: 1750
		// (Invoke) Token: 0x060036AB RID: 13995
		public delegate void BestiaryEntriesPass(BestiaryEntry entry);
	}
}
