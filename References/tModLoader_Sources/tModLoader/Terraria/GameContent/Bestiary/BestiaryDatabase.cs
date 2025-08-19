using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000673 RID: 1651
	public class BestiaryDatabase
	{
		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06004788 RID: 18312 RVA: 0x0063E428 File Offset: 0x0063C628
		public List<BestiaryEntry> Entries
		{
			get
			{
				return this._entries;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06004789 RID: 18313 RVA: 0x0063E430 File Offset: 0x0063C630
		public List<IBestiaryEntryFilter> Filters
		{
			get
			{
				return this._filters;
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x0600478A RID: 18314 RVA: 0x0063E438 File Offset: 0x0063C638
		public List<IBestiarySortStep> SortSteps
		{
			get
			{
				return this._sortSteps;
			}
		}

		// Token: 0x0600478B RID: 18315 RVA: 0x0063E440 File Offset: 0x0063C640
		public BestiaryEntry Register(BestiaryEntry entry)
		{
			this._entries.Add(entry);
			for (int i = 0; i < entry.Info.Count; i++)
			{
				NPCNetIdBestiaryInfoElement nPCNetIdBestiaryInfoElement = entry.Info[i] as NPCNetIdBestiaryInfoElement;
				if (nPCNetIdBestiaryInfoElement != null)
				{
					this._byNpcId[nPCNetIdBestiaryInfoElement.NetId] = entry;
				}
			}
			ModNPC modNPC = ContentSamples.NpcsByNetId[((NPCNetIdBestiaryInfoElement)entry.Info[0]).NetId].ModNPC;
			Mod mod = (modNPC != null) ? modNPC.Mod : null;
			if (mod == null)
			{
				this._vanillaEntries.Add(entry);
			}
			else if (this._byMod.ContainsKey(mod))
			{
				this._byMod[mod].Add(entry);
			}
			else
			{
				this._byMod.Add(mod, new List<BestiaryEntry>
				{
					entry
				});
			}
			return entry;
		}

		// Token: 0x0600478C RID: 18316 RVA: 0x0063E514 File Offset: 0x0063C714
		public IBestiaryEntryFilter Register(IBestiaryEntryFilter filter)
		{
			this._filters.Add(filter);
			return filter;
		}

		// Token: 0x0600478D RID: 18317 RVA: 0x0063E523 File Offset: 0x0063C723
		public IBestiarySortStep Register(IBestiarySortStep sortStep)
		{
			this._sortSteps.Add(sortStep);
			return sortStep;
		}

		// Token: 0x0600478E RID: 18318 RVA: 0x0063E534 File Offset: 0x0063C734
		public BestiaryEntry FindEntryByNPCID(int npcNetId)
		{
			BestiaryEntry value;
			if (this._byNpcId.TryGetValue(npcNetId, out value))
			{
				return value;
			}
			this._trashEntry.Info.Clear();
			return this._trashEntry;
		}

		// Token: 0x0600478F RID: 18319 RVA: 0x0063E56C File Offset: 0x0063C76C
		public void Merge(ItemDropDatabase dropsDatabase)
		{
			for (int i = -65; i < NPCLoader.NPCCount; i++)
			{
				this.ExtractDropsForNPC(dropsDatabase, i);
			}
		}

		// Token: 0x06004790 RID: 18320 RVA: 0x0063E594 File Offset: 0x0063C794
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
			foreach (DropRateInfo item2 in list)
			{
				bestiaryEntry.Info.Add(new ItemDropBestiaryInfoElement(item2));
			}
		}

		// Token: 0x06004791 RID: 18321 RVA: 0x0063E650 File Offset: 0x0063C850
		public void ApplyPass(BestiaryDatabase.BestiaryEntriesPass pass)
		{
			for (int i = 0; i < this._entries.Count; i++)
			{
				pass(this._entries[i]);
			}
		}

		/// <summary>
		/// Gets entries from the database created by the mod specified
		/// </summary>
		/// <param name="mod">The mod to find entries from (null for Terraria)</param>
		/// <returns>A list of the entries created by the mod specified or null if it created none</returns>
		// Token: 0x06004792 RID: 18322 RVA: 0x0063E688 File Offset: 0x0063C888
		public List<BestiaryEntry> GetBestiaryEntriesByMod(Mod mod)
		{
			if (mod == null)
			{
				return this._vanillaEntries;
			}
			List<BestiaryEntry> value;
			this._byMod.TryGetValue(mod, out value);
			return value;
		}

		/// <summary>
		/// Gets the completed percent of the given mod's bestiary
		/// </summary>
		/// <param name="mod">The mod to calculate bestiary completeness (null for Terraria)</param>
		/// <returns>A float ranging from 0 to 1 representing the completeness of the bestiary or returns -1 if the mod has no entries</returns>
		// Token: 0x06004793 RID: 18323 RVA: 0x0063E6B0 File Offset: 0x0063C8B0
		public float GetCompletedPercentByMod(Mod mod)
		{
			if (mod == null)
			{
				return (float)this._vanillaEntries.Count((BestiaryEntry e) => e.UIInfoProvider.GetEntryUICollectionInfo().UnlockState > BestiaryEntryUnlockState.NotKnownAtAll_0) / (float)this._vanillaEntries.Count;
			}
			List<BestiaryEntry> value;
			if (this._byMod.TryGetValue(mod, out value))
			{
				return (float)value.Count((BestiaryEntry e) => e.UIInfoProvider.GetEntryUICollectionInfo().UnlockState > BestiaryEntryUnlockState.NotKnownAtAll_0) / (float)value.Count;
			}
			return -1f;
		}

		// Token: 0x04005BDC RID: 23516
		private List<BestiaryEntry> _entries = new List<BestiaryEntry>();

		// Token: 0x04005BDD RID: 23517
		private List<IBestiaryEntryFilter> _filters = new List<IBestiaryEntryFilter>();

		// Token: 0x04005BDE RID: 23518
		private List<IBestiarySortStep> _sortSteps = new List<IBestiarySortStep>();

		// Token: 0x04005BDF RID: 23519
		private Dictionary<int, BestiaryEntry> _byNpcId = new Dictionary<int, BestiaryEntry>();

		// Token: 0x04005BE0 RID: 23520
		private BestiaryEntry _trashEntry = new BestiaryEntry();

		// Token: 0x04005BE1 RID: 23521
		private List<BestiaryEntry> _vanillaEntries = new List<BestiaryEntry>();

		// Token: 0x04005BE2 RID: 23522
		private Dictionary<Mod, List<BestiaryEntry>> _byMod = new Dictionary<Mod, List<BestiaryEntry>>();

		// Token: 0x02000D1E RID: 3358
		// (Invoke) Token: 0x0600631C RID: 25372
		public delegate void BestiaryEntriesPass(BestiaryEntry entry);
	}
}
