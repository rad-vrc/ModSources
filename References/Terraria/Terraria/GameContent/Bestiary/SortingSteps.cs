using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x020002EF RID: 751
	public static class SortingSteps
	{
		// Token: 0x020006E0 RID: 1760
		public class ByNetId : IBestiarySortStep, IEntrySortStep<BestiaryEntry>, IComparer<BestiaryEntry>
		{
			// Token: 0x170003FA RID: 1018
			// (get) Token: 0x060036D5 RID: 14037 RVA: 0x0003266D File Offset: 0x0003086D
			public bool HiddenFromSortOptions
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060036D6 RID: 14038 RVA: 0x0060D594 File Offset: 0x0060B794
			public int Compare(BestiaryEntry x, BestiaryEntry y)
			{
				NPCNetIdBestiaryInfoElement npcnetIdBestiaryInfoElement = x.Info.FirstOrDefault((IBestiaryInfoElement element) => element is NPCNetIdBestiaryInfoElement) as NPCNetIdBestiaryInfoElement;
				NPCNetIdBestiaryInfoElement npcnetIdBestiaryInfoElement2 = y.Info.FirstOrDefault((IBestiaryInfoElement element) => element is NPCNetIdBestiaryInfoElement) as NPCNetIdBestiaryInfoElement;
				if (npcnetIdBestiaryInfoElement == null && npcnetIdBestiaryInfoElement2 != null)
				{
					return 1;
				}
				if (npcnetIdBestiaryInfoElement2 == null && npcnetIdBestiaryInfoElement != null)
				{
					return -1;
				}
				if (npcnetIdBestiaryInfoElement == null || npcnetIdBestiaryInfoElement2 == null)
				{
					return 0;
				}
				return npcnetIdBestiaryInfoElement.NetId.CompareTo(npcnetIdBestiaryInfoElement2.NetId);
			}

			// Token: 0x060036D7 RID: 14039 RVA: 0x0060D62D File Offset: 0x0060B82D
			public string GetDisplayNameKey()
			{
				return "BestiaryInfo.Sort_ID";
			}
		}

		// Token: 0x020006E1 RID: 1761
		public class ByUnlockState : IBestiarySortStep, IEntrySortStep<BestiaryEntry>, IComparer<BestiaryEntry>
		{
			// Token: 0x170003FB RID: 1019
			// (get) Token: 0x060036D9 RID: 14041 RVA: 0x0003266D File Offset: 0x0003086D
			public bool HiddenFromSortOptions
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060036DA RID: 14042 RVA: 0x0060D634 File Offset: 0x0060B834
			public int Compare(BestiaryEntry x, BestiaryEntry y)
			{
				BestiaryUICollectionInfo entryUICollectionInfo = x.UIInfoProvider.GetEntryUICollectionInfo();
				BestiaryUICollectionInfo entryUICollectionInfo2 = y.UIInfoProvider.GetEntryUICollectionInfo();
				return y.Icon.GetUnlockState(entryUICollectionInfo2).CompareTo(x.Icon.GetUnlockState(entryUICollectionInfo));
			}

			// Token: 0x060036DB RID: 14043 RVA: 0x0060D679 File Offset: 0x0060B879
			public string GetDisplayNameKey()
			{
				return "BestiaryInfo.Sort_Unlocks";
			}
		}

		// Token: 0x020006E2 RID: 1762
		public class ByBestiarySortingId : IBestiarySortStep, IEntrySortStep<BestiaryEntry>, IComparer<BestiaryEntry>
		{
			// Token: 0x170003FC RID: 1020
			// (get) Token: 0x060036DD RID: 14045 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
			public bool HiddenFromSortOptions
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060036DE RID: 14046 RVA: 0x0060D680 File Offset: 0x0060B880
			public int Compare(BestiaryEntry x, BestiaryEntry y)
			{
				NPCNetIdBestiaryInfoElement npcnetIdBestiaryInfoElement = x.Info.FirstOrDefault((IBestiaryInfoElement element) => element is NPCNetIdBestiaryInfoElement) as NPCNetIdBestiaryInfoElement;
				NPCNetIdBestiaryInfoElement npcnetIdBestiaryInfoElement2 = y.Info.FirstOrDefault((IBestiaryInfoElement element) => element is NPCNetIdBestiaryInfoElement) as NPCNetIdBestiaryInfoElement;
				if (npcnetIdBestiaryInfoElement == null && npcnetIdBestiaryInfoElement2 != null)
				{
					return 1;
				}
				if (npcnetIdBestiaryInfoElement2 == null && npcnetIdBestiaryInfoElement != null)
				{
					return -1;
				}
				if (npcnetIdBestiaryInfoElement == null || npcnetIdBestiaryInfoElement2 == null)
				{
					return 0;
				}
				int num = ContentSamples.NpcBestiarySortingId[npcnetIdBestiaryInfoElement.NetId];
				int value = ContentSamples.NpcBestiarySortingId[npcnetIdBestiaryInfoElement2.NetId];
				return num.CompareTo(value);
			}

			// Token: 0x060036DF RID: 14047 RVA: 0x0060D72F File Offset: 0x0060B92F
			public string GetDisplayNameKey()
			{
				return "BestiaryInfo.Sort_BestiaryID";
			}
		}

		// Token: 0x020006E3 RID: 1763
		public class ByBestiaryRarity : IBestiarySortStep, IEntrySortStep<BestiaryEntry>, IComparer<BestiaryEntry>
		{
			// Token: 0x170003FD RID: 1021
			// (get) Token: 0x060036E1 RID: 14049 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
			public bool HiddenFromSortOptions
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060036E2 RID: 14050 RVA: 0x0060D738 File Offset: 0x0060B938
			public int Compare(BestiaryEntry x, BestiaryEntry y)
			{
				NPCNetIdBestiaryInfoElement npcnetIdBestiaryInfoElement = x.Info.FirstOrDefault((IBestiaryInfoElement element) => element is NPCNetIdBestiaryInfoElement) as NPCNetIdBestiaryInfoElement;
				NPCNetIdBestiaryInfoElement npcnetIdBestiaryInfoElement2 = y.Info.FirstOrDefault((IBestiaryInfoElement element) => element is NPCNetIdBestiaryInfoElement) as NPCNetIdBestiaryInfoElement;
				if (npcnetIdBestiaryInfoElement == null && npcnetIdBestiaryInfoElement2 != null)
				{
					return 1;
				}
				if (npcnetIdBestiaryInfoElement2 == null && npcnetIdBestiaryInfoElement != null)
				{
					return -1;
				}
				if (npcnetIdBestiaryInfoElement == null || npcnetIdBestiaryInfoElement2 == null)
				{
					return 0;
				}
				int value = ContentSamples.NpcBestiaryRarityStars[npcnetIdBestiaryInfoElement.NetId];
				return ContentSamples.NpcBestiaryRarityStars[npcnetIdBestiaryInfoElement2.NetId].CompareTo(value);
			}

			// Token: 0x060036E3 RID: 14051 RVA: 0x0060D7E7 File Offset: 0x0060B9E7
			public string GetDisplayNameKey()
			{
				return "BestiaryInfo.Sort_Rarity";
			}
		}

		// Token: 0x020006E4 RID: 1764
		public class Alphabetical : IBestiarySortStep, IEntrySortStep<BestiaryEntry>, IComparer<BestiaryEntry>
		{
			// Token: 0x170003FE RID: 1022
			// (get) Token: 0x060036E5 RID: 14053 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
			public bool HiddenFromSortOptions
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060036E6 RID: 14054 RVA: 0x0060D7F0 File Offset: 0x0060B9F0
			public int Compare(BestiaryEntry x, BestiaryEntry y)
			{
				NPCNetIdBestiaryInfoElement npcnetIdBestiaryInfoElement = x.Info.FirstOrDefault((IBestiaryInfoElement element) => element is NPCNetIdBestiaryInfoElement) as NPCNetIdBestiaryInfoElement;
				NPCNetIdBestiaryInfoElement npcnetIdBestiaryInfoElement2 = y.Info.FirstOrDefault((IBestiaryInfoElement element) => element is NPCNetIdBestiaryInfoElement) as NPCNetIdBestiaryInfoElement;
				if (npcnetIdBestiaryInfoElement == null && npcnetIdBestiaryInfoElement2 != null)
				{
					return 1;
				}
				if (npcnetIdBestiaryInfoElement2 == null && npcnetIdBestiaryInfoElement != null)
				{
					return -1;
				}
				if (npcnetIdBestiaryInfoElement == null || npcnetIdBestiaryInfoElement2 == null)
				{
					return 0;
				}
				string textValue = Language.GetTextValue(ContentSamples.NpcsByNetId[npcnetIdBestiaryInfoElement.NetId].TypeName);
				string textValue2 = Language.GetTextValue(ContentSamples.NpcsByNetId[npcnetIdBestiaryInfoElement2.NetId].TypeName);
				return textValue.CompareTo(textValue2);
			}

			// Token: 0x060036E7 RID: 14055 RVA: 0x0060D8B0 File Offset: 0x0060BAB0
			public string GetDisplayNameKey()
			{
				return "BestiaryInfo.Sort_Alphabetical";
			}
		}

		// Token: 0x020006E5 RID: 1765
		public abstract class ByStat : IBestiarySortStep, IEntrySortStep<BestiaryEntry>, IComparer<BestiaryEntry>
		{
			// Token: 0x170003FF RID: 1023
			// (get) Token: 0x060036E9 RID: 14057 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
			public bool HiddenFromSortOptions
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060036EA RID: 14058 RVA: 0x0060D8B8 File Offset: 0x0060BAB8
			public int Compare(BestiaryEntry x, BestiaryEntry y)
			{
				NPCStatsReportInfoElement npcstatsReportInfoElement = x.Info.FirstOrDefault((IBestiaryInfoElement element) => this.IsAStatsCardINeed(element)) as NPCStatsReportInfoElement;
				NPCStatsReportInfoElement npcstatsReportInfoElement2 = y.Info.FirstOrDefault((IBestiaryInfoElement element) => this.IsAStatsCardINeed(element)) as NPCStatsReportInfoElement;
				if (npcstatsReportInfoElement == null && npcstatsReportInfoElement2 != null)
				{
					return 1;
				}
				if (npcstatsReportInfoElement2 == null && npcstatsReportInfoElement != null)
				{
					return -1;
				}
				if (npcstatsReportInfoElement == null || npcstatsReportInfoElement2 == null)
				{
					return 0;
				}
				return this.Compare(npcstatsReportInfoElement, npcstatsReportInfoElement2);
			}

			// Token: 0x060036EB RID: 14059
			public abstract int Compare(NPCStatsReportInfoElement cardX, NPCStatsReportInfoElement cardY);

			// Token: 0x060036EC RID: 14060
			public abstract string GetDisplayNameKey();

			// Token: 0x060036ED RID: 14061 RVA: 0x0060D91F File Offset: 0x0060BB1F
			private bool IsAStatsCardINeed(IBestiaryInfoElement element)
			{
				return element is NPCStatsReportInfoElement;
			}
		}

		// Token: 0x020006E6 RID: 1766
		public class ByAttack : SortingSteps.ByStat
		{
			// Token: 0x060036F1 RID: 14065 RVA: 0x0060D935 File Offset: 0x0060BB35
			public override int Compare(NPCStatsReportInfoElement cardX, NPCStatsReportInfoElement cardY)
			{
				return cardY.Damage.CompareTo(cardX.Damage);
			}

			// Token: 0x060036F2 RID: 14066 RVA: 0x0060D948 File Offset: 0x0060BB48
			public override string GetDisplayNameKey()
			{
				return "BestiaryInfo.Sort_Attack";
			}
		}

		// Token: 0x020006E7 RID: 1767
		public class ByDefense : SortingSteps.ByStat
		{
			// Token: 0x060036F4 RID: 14068 RVA: 0x0060D957 File Offset: 0x0060BB57
			public override int Compare(NPCStatsReportInfoElement cardX, NPCStatsReportInfoElement cardY)
			{
				return cardY.Defense.CompareTo(cardX.Defense);
			}

			// Token: 0x060036F5 RID: 14069 RVA: 0x0060D96A File Offset: 0x0060BB6A
			public override string GetDisplayNameKey()
			{
				return "BestiaryInfo.Sort_Defense";
			}
		}

		// Token: 0x020006E8 RID: 1768
		public class ByCoins : SortingSteps.ByStat
		{
			// Token: 0x060036F7 RID: 14071 RVA: 0x0060D971 File Offset: 0x0060BB71
			public override int Compare(NPCStatsReportInfoElement cardX, NPCStatsReportInfoElement cardY)
			{
				return cardY.MonetaryValue.CompareTo(cardX.MonetaryValue);
			}

			// Token: 0x060036F8 RID: 14072 RVA: 0x0060D984 File Offset: 0x0060BB84
			public override string GetDisplayNameKey()
			{
				return "BestiaryInfo.Sort_Coins";
			}
		}

		// Token: 0x020006E9 RID: 1769
		public class ByHP : SortingSteps.ByStat
		{
			// Token: 0x060036FA RID: 14074 RVA: 0x0060D98B File Offset: 0x0060BB8B
			public override int Compare(NPCStatsReportInfoElement cardX, NPCStatsReportInfoElement cardY)
			{
				return cardY.LifeMax.CompareTo(cardX.LifeMax);
			}

			// Token: 0x060036FB RID: 14075 RVA: 0x0060D99E File Offset: 0x0060BB9E
			public override string GetDisplayNameKey()
			{
				return "BestiaryInfo.Sort_HitPoints";
			}
		}
	}
}
