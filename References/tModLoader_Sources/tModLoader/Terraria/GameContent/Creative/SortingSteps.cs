using System;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.GameContent.Creative
{
	// Token: 0x0200064F RID: 1615
	public static class SortingSteps
	{
		// Token: 0x02000D03 RID: 3331
		public abstract class ACreativeItemSortStep : ICreativeItemSortStep, IEntrySortStep<int>, IComparer<int>, IComparer<Item>
		{
			// Token: 0x060062D1 RID: 25297
			public abstract string GetDisplayNameKey();

			// Token: 0x060062D2 RID: 25298 RVA: 0x006D7546 File Offset: 0x006D5746
			public int Compare(int x, int y)
			{
				return this.Compare(ContentSamples.ItemsByType[x], ContentSamples.ItemsByType[y]);
			}

			// Token: 0x060062D3 RID: 25299
			public abstract int Compare(Item x, Item y);
		}

		// Token: 0x02000D04 RID: 3332
		public abstract class AStepByFittingFilter : SortingSteps.ACreativeItemSortStep
		{
			// Token: 0x060062D5 RID: 25301 RVA: 0x006D756C File Offset: 0x006D576C
			public override int Compare(Item x, Item y)
			{
				int num = this.FitsFilter(x).CompareTo(this.FitsFilter(y));
				if (num == 0)
				{
					num = 1;
				}
				return num;
			}

			// Token: 0x060062D6 RID: 25302
			public abstract bool FitsFilter(Item item);

			// Token: 0x060062D7 RID: 25303 RVA: 0x006D7596 File Offset: 0x006D5796
			public virtual int CompareWhenBothFit(Item x, Item y)
			{
				return string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x02000D05 RID: 3333
		public class Blocks : SortingSteps.AStepByFittingFilter
		{
			// Token: 0x060062D9 RID: 25305 RVA: 0x006D75B2 File Offset: 0x006D57B2
			public override string GetDisplayNameKey()
			{
				return "CreativePowers.Sort_Blocks";
			}

			// Token: 0x060062DA RID: 25306 RVA: 0x006D75B9 File Offset: 0x006D57B9
			public override bool FitsFilter(Item item)
			{
				return item.createTile >= 0 && !Main.tileFrameImportant[item.createTile];
			}
		}

		// Token: 0x02000D06 RID: 3334
		public class Walls : SortingSteps.AStepByFittingFilter
		{
			// Token: 0x060062DC RID: 25308 RVA: 0x006D75DD File Offset: 0x006D57DD
			public override string GetDisplayNameKey()
			{
				return "CreativePowers.Sort_Walls";
			}

			// Token: 0x060062DD RID: 25309 RVA: 0x006D75E4 File Offset: 0x006D57E4
			public override bool FitsFilter(Item item)
			{
				return item.createWall >= 0;
			}
		}

		// Token: 0x02000D07 RID: 3335
		public class PlacableObjects : SortingSteps.AStepByFittingFilter
		{
			// Token: 0x060062DF RID: 25311 RVA: 0x006D75FA File Offset: 0x006D57FA
			public override string GetDisplayNameKey()
			{
				return "CreativePowers.Sort_PlacableObjects";
			}

			// Token: 0x060062E0 RID: 25312 RVA: 0x006D7601 File Offset: 0x006D5801
			public override bool FitsFilter(Item item)
			{
				return item.createTile >= 0 && Main.tileFrameImportant[item.createTile];
			}
		}

		// Token: 0x02000D08 RID: 3336
		public class ByCreativeSortingId : SortingSteps.ACreativeItemSortStep
		{
			// Token: 0x060062E2 RID: 25314 RVA: 0x006D7622 File Offset: 0x006D5822
			public override string GetDisplayNameKey()
			{
				return "CreativePowers.Sort_SortingID";
			}

			// Token: 0x060062E3 RID: 25315 RVA: 0x006D762C File Offset: 0x006D582C
			public override int Compare(Item x, Item y)
			{
				ContentSamples.CreativeHelper.ItemGroupAndOrderInGroup itemGroupAndOrderInGroup = ContentSamples.ItemCreativeSortingId[x.type];
				ContentSamples.CreativeHelper.ItemGroupAndOrderInGroup itemGroupAndOrderInGroup2 = ContentSamples.ItemCreativeSortingId[y.type];
				int num = itemGroupAndOrderInGroup.Group.CompareTo(itemGroupAndOrderInGroup2.Group);
				if (num == 0)
				{
					num = itemGroupAndOrderInGroup.OrderInGroup.CompareTo(itemGroupAndOrderInGroup2.OrderInGroup);
				}
				return num;
			}
		}

		// Token: 0x02000D09 RID: 3337
		public class Alphabetical : SortingSteps.ACreativeItemSortStep
		{
			// Token: 0x060062E5 RID: 25317 RVA: 0x006D7698 File Offset: 0x006D5898
			public override string GetDisplayNameKey()
			{
				return "CreativePowers.Sort_Alphabetical";
			}

			// Token: 0x060062E6 RID: 25318 RVA: 0x006D76A0 File Offset: 0x006D58A0
			public override int Compare(Item x, Item y)
			{
				string name3 = x.Name;
				string name2 = y.Name;
				return name3.CompareTo(name2);
			}
		}
	}
}
