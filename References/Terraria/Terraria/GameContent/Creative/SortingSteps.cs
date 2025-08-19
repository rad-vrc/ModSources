using System;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.GameContent.Creative
{
	// Token: 0x020002BD RID: 701
	public static class SortingSteps
	{
		// Token: 0x020006A9 RID: 1705
		public abstract class ACreativeItemSortStep : ICreativeItemSortStep, IEntrySortStep<int>, IComparer<int>, IComparer<Item>
		{
			// Token: 0x06003569 RID: 13673
			public abstract string GetDisplayNameKey();

			// Token: 0x0600356A RID: 13674 RVA: 0x0060A182 File Offset: 0x00608382
			public int Compare(int x, int y)
			{
				return this.Compare(ContentSamples.ItemsByType[x], ContentSamples.ItemsByType[y]);
			}

			// Token: 0x0600356B RID: 13675
			public abstract int Compare(Item x, Item y);
		}

		// Token: 0x020006AA RID: 1706
		public abstract class AStepByFittingFilter : SortingSteps.ACreativeItemSortStep
		{
			// Token: 0x0600356D RID: 13677 RVA: 0x0060A1A0 File Offset: 0x006083A0
			public override int Compare(Item x, Item y)
			{
				int num = this.FitsFilter(x).CompareTo(this.FitsFilter(y));
				if (num == 0)
				{
					num = 1;
				}
				return num;
			}

			// Token: 0x0600356E RID: 13678
			public abstract bool FitsFilter(Item item);

			// Token: 0x0600356F RID: 13679 RVA: 0x0060A1CA File Offset: 0x006083CA
			public virtual int CompareWhenBothFit(Item x, Item y)
			{
				return string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x020006AB RID: 1707
		public class Blocks : SortingSteps.AStepByFittingFilter
		{
			// Token: 0x06003571 RID: 13681 RVA: 0x0060A1E6 File Offset: 0x006083E6
			public override string GetDisplayNameKey()
			{
				return "CreativePowers.Sort_Blocks";
			}

			// Token: 0x06003572 RID: 13682 RVA: 0x0060A1ED File Offset: 0x006083ED
			public override bool FitsFilter(Item item)
			{
				return item.createTile >= 0 && !Main.tileFrameImportant[item.createTile];
			}
		}

		// Token: 0x020006AC RID: 1708
		public class Walls : SortingSteps.AStepByFittingFilter
		{
			// Token: 0x06003574 RID: 13684 RVA: 0x0060A211 File Offset: 0x00608411
			public override string GetDisplayNameKey()
			{
				return "CreativePowers.Sort_Walls";
			}

			// Token: 0x06003575 RID: 13685 RVA: 0x0060A218 File Offset: 0x00608418
			public override bool FitsFilter(Item item)
			{
				return item.createWall >= 0;
			}
		}

		// Token: 0x020006AD RID: 1709
		public class PlacableObjects : SortingSteps.AStepByFittingFilter
		{
			// Token: 0x06003577 RID: 13687 RVA: 0x0060A226 File Offset: 0x00608426
			public override string GetDisplayNameKey()
			{
				return "CreativePowers.Sort_PlacableObjects";
			}

			// Token: 0x06003578 RID: 13688 RVA: 0x0060A22D File Offset: 0x0060842D
			public override bool FitsFilter(Item item)
			{
				return item.createTile >= 0 && Main.tileFrameImportant[item.createTile];
			}
		}

		// Token: 0x020006AE RID: 1710
		public class ByCreativeSortingId : SortingSteps.ACreativeItemSortStep
		{
			// Token: 0x0600357A RID: 13690 RVA: 0x0060A246 File Offset: 0x00608446
			public override string GetDisplayNameKey()
			{
				return "CreativePowers.Sort_SortingID";
			}

			// Token: 0x0600357B RID: 13691 RVA: 0x0060A250 File Offset: 0x00608450
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

		// Token: 0x020006AF RID: 1711
		public class Alphabetical : SortingSteps.ACreativeItemSortStep
		{
			// Token: 0x0600357D RID: 13693 RVA: 0x0060A2B4 File Offset: 0x006084B4
			public override string GetDisplayNameKey()
			{
				return "CreativePowers.Sort_Alphabetical";
			}

			// Token: 0x0600357E RID: 13694 RVA: 0x0060A2BC File Offset: 0x006084BC
			public override int Compare(Item x, Item y)
			{
				string name = x.Name;
				string name2 = y.Name;
				return name.CompareTo(name2);
			}
		}
	}
}
