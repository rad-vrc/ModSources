using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x020006A6 RID: 1702
	public static class SortingSteps
	{
		// Token: 0x02000D32 RID: 3378
		public class ByNetId : IBestiarySortStep, IEntrySortStep<BestiaryEntry>, IComparer<BestiaryEntry>
		{
			// Token: 0x17000997 RID: 2455
			// (get) Token: 0x06006360 RID: 25440 RVA: 0x006D8832 File Offset: 0x006D6A32
			public bool HiddenFromSortOptions
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06006361 RID: 25441 RVA: 0x006D8838 File Offset: 0x006D6A38
			public int Compare(BestiaryEntry x, BestiaryEntry y)
			{
				NPCNetIdBestiaryInfoElement nPCNetIdBestiaryInfoElement = x.Info.FirstOrDefault((IBestiaryInfoElement element) => element is NPCNetIdBestiaryInfoElement) as NPCNetIdBestiaryInfoElement;
				NPCNetIdBestiaryInfoElement nPCNetIdBestiaryInfoElement2 = y.Info.FirstOrDefault((IBestiaryInfoElement element) => element is NPCNetIdBestiaryInfoElement) as NPCNetIdBestiaryInfoElement;
				if (nPCNetIdBestiaryInfoElement == null && nPCNetIdBestiaryInfoElement2 != null)
				{
					return 1;
				}
				if (nPCNetIdBestiaryInfoElement2 == null && nPCNetIdBestiaryInfoElement != null)
				{
					return -1;
				}
				if (nPCNetIdBestiaryInfoElement == null || nPCNetIdBestiaryInfoElement2 == null)
				{
					return 0;
				}
				return nPCNetIdBestiaryInfoElement.NetId.CompareTo(nPCNetIdBestiaryInfoElement2.NetId);
			}

			// Token: 0x06006362 RID: 25442 RVA: 0x006D88D1 File Offset: 0x006D6AD1
			public string GetDisplayNameKey()
			{
				return "BestiaryInfo.Sort_ID";
			}
		}

		// Token: 0x02000D33 RID: 3379
		public class ByUnlockState : IBestiarySortStep, IEntrySortStep<BestiaryEntry>, IComparer<BestiaryEntry>
		{
			// Token: 0x17000998 RID: 2456
			// (get) Token: 0x06006364 RID: 25444 RVA: 0x006D88E0 File Offset: 0x006D6AE0
			public bool HiddenFromSortOptions
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06006365 RID: 25445 RVA: 0x006D88E4 File Offset: 0x006D6AE4
			public int Compare(BestiaryEntry x, BestiaryEntry y)
			{
				BestiaryUICollectionInfo entryUICollectionInfo = x.UIInfoProvider.GetEntryUICollectionInfo();
				BestiaryUICollectionInfo entryUICollectionInfo2 = y.UIInfoProvider.GetEntryUICollectionInfo();
				return y.Icon.GetUnlockState(entryUICollectionInfo2).CompareTo(x.Icon.GetUnlockState(entryUICollectionInfo));
			}

			// Token: 0x06006366 RID: 25446 RVA: 0x006D8929 File Offset: 0x006D6B29
			public string GetDisplayNameKey()
			{
				return "BestiaryInfo.Sort_Unlocks";
			}
		}

		// Token: 0x02000D34 RID: 3380
		public class ByBestiarySortingId : IBestiarySortStep, IEntrySortStep<BestiaryEntry>, IComparer<BestiaryEntry>
		{
			// Token: 0x17000999 RID: 2457
			// (get) Token: 0x06006368 RID: 25448 RVA: 0x006D8938 File Offset: 0x006D6B38
			public bool HiddenFromSortOptions
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06006369 RID: 25449 RVA: 0x006D893C File Offset: 0x006D6B3C
			public int Compare(BestiaryEntry x, BestiaryEntry y)
			{
				NPCNetIdBestiaryInfoElement nPCNetIdBestiaryInfoElement = x.Info.FirstOrDefault((IBestiaryInfoElement element) => element is NPCNetIdBestiaryInfoElement) as NPCNetIdBestiaryInfoElement;
				NPCNetIdBestiaryInfoElement nPCNetIdBestiaryInfoElement2 = y.Info.FirstOrDefault((IBestiaryInfoElement element) => element is NPCNetIdBestiaryInfoElement) as NPCNetIdBestiaryInfoElement;
				if (nPCNetIdBestiaryInfoElement == null && nPCNetIdBestiaryInfoElement2 != null)
				{
					return 1;
				}
				if (nPCNetIdBestiaryInfoElement2 == null && nPCNetIdBestiaryInfoElement != null)
				{
					return -1;
				}
				if (nPCNetIdBestiaryInfoElement == null || nPCNetIdBestiaryInfoElement2 == null)
				{
					return 0;
				}
				int num = ContentSamples.NpcBestiarySortingId[nPCNetIdBestiaryInfoElement.NetId];
				int value = ContentSamples.NpcBestiarySortingId[nPCNetIdBestiaryInfoElement2.NetId];
				return num.CompareTo(value);
			}

			// Token: 0x0600636A RID: 25450 RVA: 0x006D89EB File Offset: 0x006D6BEB
			public string GetDisplayNameKey()
			{
				return "BestiaryInfo.Sort_BestiaryID";
			}
		}

		// Token: 0x02000D35 RID: 3381
		public class ByBestiaryRarity : IBestiarySortStep, IEntrySortStep<BestiaryEntry>, IComparer<BestiaryEntry>
		{
			// Token: 0x1700099A RID: 2458
			// (get) Token: 0x0600636C RID: 25452 RVA: 0x006D89FA File Offset: 0x006D6BFA
			public bool HiddenFromSortOptions
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600636D RID: 25453 RVA: 0x006D8A00 File Offset: 0x006D6C00
			public int Compare(BestiaryEntry x, BestiaryEntry y)
			{
				NPCNetIdBestiaryInfoElement nPCNetIdBestiaryInfoElement = x.Info.FirstOrDefault((IBestiaryInfoElement element) => element is NPCNetIdBestiaryInfoElement) as NPCNetIdBestiaryInfoElement;
				NPCNetIdBestiaryInfoElement nPCNetIdBestiaryInfoElement2 = y.Info.FirstOrDefault((IBestiaryInfoElement element) => element is NPCNetIdBestiaryInfoElement) as NPCNetIdBestiaryInfoElement;
				if (nPCNetIdBestiaryInfoElement == null && nPCNetIdBestiaryInfoElement2 != null)
				{
					return 1;
				}
				if (nPCNetIdBestiaryInfoElement2 == null && nPCNetIdBestiaryInfoElement != null)
				{
					return -1;
				}
				if (nPCNetIdBestiaryInfoElement == null || nPCNetIdBestiaryInfoElement2 == null)
				{
					return 0;
				}
				int value = ContentSamples.NpcBestiaryRarityStars[nPCNetIdBestiaryInfoElement.NetId];
				return ContentSamples.NpcBestiaryRarityStars[nPCNetIdBestiaryInfoElement2.NetId].CompareTo(value);
			}

			// Token: 0x0600636E RID: 25454 RVA: 0x006D8AAF File Offset: 0x006D6CAF
			public string GetDisplayNameKey()
			{
				return "BestiaryInfo.Sort_Rarity";
			}
		}

		// Token: 0x02000D36 RID: 3382
		public class Alphabetical : IBestiarySortStep, IEntrySortStep<BestiaryEntry>, IComparer<BestiaryEntry>
		{
			// Token: 0x1700099B RID: 2459
			// (get) Token: 0x06006370 RID: 25456 RVA: 0x006D8ABE File Offset: 0x006D6CBE
			public bool HiddenFromSortOptions
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06006371 RID: 25457 RVA: 0x006D8AC4 File Offset: 0x006D6CC4
			public int Compare(BestiaryEntry x, BestiaryEntry y)
			{
				NPCNetIdBestiaryInfoElement nPCNetIdBestiaryInfoElement = x.Info.FirstOrDefault((IBestiaryInfoElement element) => element is NPCNetIdBestiaryInfoElement) as NPCNetIdBestiaryInfoElement;
				NPCNetIdBestiaryInfoElement nPCNetIdBestiaryInfoElement2 = y.Info.FirstOrDefault((IBestiaryInfoElement element) => element is NPCNetIdBestiaryInfoElement) as NPCNetIdBestiaryInfoElement;
				if (nPCNetIdBestiaryInfoElement == null && nPCNetIdBestiaryInfoElement2 != null)
				{
					return 1;
				}
				if (nPCNetIdBestiaryInfoElement2 == null && nPCNetIdBestiaryInfoElement != null)
				{
					return -1;
				}
				if (nPCNetIdBestiaryInfoElement == null || nPCNetIdBestiaryInfoElement2 == null)
				{
					return 0;
				}
				string textValue3 = Language.GetTextValue(ContentSamples.NpcsByNetId[nPCNetIdBestiaryInfoElement.NetId].TypeName);
				string textValue2 = Language.GetTextValue(ContentSamples.NpcsByNetId[nPCNetIdBestiaryInfoElement2.NetId].TypeName);
				return textValue3.CompareTo(textValue2);
			}

			// Token: 0x06006372 RID: 25458 RVA: 0x006D8B84 File Offset: 0x006D6D84
			public string GetDisplayNameKey()
			{
				return "BestiaryInfo.Sort_Alphabetical";
			}
		}

		// Token: 0x02000D37 RID: 3383
		public abstract class ByStat : IBestiarySortStep, IEntrySortStep<BestiaryEntry>, IComparer<BestiaryEntry>
		{
			// Token: 0x1700099C RID: 2460
			// (get) Token: 0x06006374 RID: 25460 RVA: 0x006D8B93 File Offset: 0x006D6D93
			public bool HiddenFromSortOptions
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06006375 RID: 25461 RVA: 0x006D8B98 File Offset: 0x006D6D98
			public int Compare(BestiaryEntry x, BestiaryEntry y)
			{
				NPCStatsReportInfoElement nPCStatsReportInfoElement = x.Info.FirstOrDefault((IBestiaryInfoElement element) => this.IsAStatsCardINeed(element)) as NPCStatsReportInfoElement;
				NPCStatsReportInfoElement nPCStatsReportInfoElement2 = y.Info.FirstOrDefault((IBestiaryInfoElement element) => this.IsAStatsCardINeed(element)) as NPCStatsReportInfoElement;
				if (nPCStatsReportInfoElement == null && nPCStatsReportInfoElement2 != null)
				{
					return 1;
				}
				if (nPCStatsReportInfoElement2 == null && nPCStatsReportInfoElement != null)
				{
					return -1;
				}
				if (nPCStatsReportInfoElement == null || nPCStatsReportInfoElement2 == null)
				{
					return 0;
				}
				return this.Compare(nPCStatsReportInfoElement, nPCStatsReportInfoElement2);
			}

			// Token: 0x06006376 RID: 25462
			public abstract int Compare(NPCStatsReportInfoElement cardX, NPCStatsReportInfoElement cardY);

			// Token: 0x06006377 RID: 25463
			public abstract string GetDisplayNameKey();

			// Token: 0x06006378 RID: 25464 RVA: 0x006D8BFF File Offset: 0x006D6DFF
			private bool IsAStatsCardINeed(IBestiaryInfoElement element)
			{
				return element is NPCStatsReportInfoElement;
			}
		}

		// Token: 0x02000D38 RID: 3384
		public class ByAttack : SortingSteps.ByStat
		{
			// Token: 0x0600637C RID: 25468 RVA: 0x006D8C26 File Offset: 0x006D6E26
			public override int Compare(NPCStatsReportInfoElement cardX, NPCStatsReportInfoElement cardY)
			{
				return cardY.Damage.CompareTo(cardX.Damage);
			}

			// Token: 0x0600637D RID: 25469 RVA: 0x006D8C39 File Offset: 0x006D6E39
			public override string GetDisplayNameKey()
			{
				return "BestiaryInfo.Sort_Attack";
			}
		}

		// Token: 0x02000D39 RID: 3385
		public class ByDefense : SortingSteps.ByStat
		{
			// Token: 0x0600637F RID: 25471 RVA: 0x006D8C48 File Offset: 0x006D6E48
			public override int Compare(NPCStatsReportInfoElement cardX, NPCStatsReportInfoElement cardY)
			{
				return cardY.Defense.CompareTo(cardX.Defense);
			}

			// Token: 0x06006380 RID: 25472 RVA: 0x006D8C5B File Offset: 0x006D6E5B
			public override string GetDisplayNameKey()
			{
				return "BestiaryInfo.Sort_Defense";
			}
		}

		// Token: 0x02000D3A RID: 3386
		public class ByCoins : SortingSteps.ByStat
		{
			// Token: 0x06006382 RID: 25474 RVA: 0x006D8C6A File Offset: 0x006D6E6A
			public override int Compare(NPCStatsReportInfoElement cardX, NPCStatsReportInfoElement cardY)
			{
				return cardY.MonetaryValue.CompareTo(cardX.MonetaryValue);
			}

			// Token: 0x06006383 RID: 25475 RVA: 0x006D8C7D File Offset: 0x006D6E7D
			public override string GetDisplayNameKey()
			{
				return "BestiaryInfo.Sort_Coins";
			}
		}

		// Token: 0x02000D3B RID: 3387
		public class ByHP : SortingSteps.ByStat
		{
			// Token: 0x06006385 RID: 25477 RVA: 0x006D8C8C File Offset: 0x006D6E8C
			public override int Compare(NPCStatsReportInfoElement cardX, NPCStatsReportInfoElement cardY)
			{
				return cardY.LifeMax.CompareTo(cardX.LifeMax);
			}

			// Token: 0x06006386 RID: 25478 RVA: 0x006D8C9F File Offset: 0x006D6E9F
			public override string GetDisplayNameKey()
			{
				return "BestiaryInfo.Sort_HitPoints";
			}
		}
	}
}
