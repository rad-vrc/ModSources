using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent.Items
{
	// Token: 0x020003DA RID: 986
	public static class ItemVariants
	{
		// Token: 0x06002AA2 RID: 10914 RVA: 0x0059A584 File Offset: 0x00598784
		public static IEnumerable<ItemVariants.VariantEntry> GetVariants(int itemId)
		{
			if (!ItemVariants._variants.IndexInRange(itemId))
			{
				return Enumerable.Empty<ItemVariants.VariantEntry>();
			}
			IEnumerable<ItemVariants.VariantEntry> enumerable = ItemVariants._variants[itemId];
			return enumerable ?? Enumerable.Empty<ItemVariants.VariantEntry>();
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x0059A5B8 File Offset: 0x005987B8
		private static ItemVariants.VariantEntry GetEntry(int itemId, ItemVariant variant)
		{
			return ItemVariants.GetVariants(itemId).SingleOrDefault((ItemVariants.VariantEntry v) => v.Variant == variant);
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x0059A5EC File Offset: 0x005987EC
		public static void AddVariant(int itemId, ItemVariant variant, params ItemVariantCondition[] conditions)
		{
			ItemVariants.VariantEntry variantEntry = ItemVariants.GetEntry(itemId, variant);
			if (variantEntry == null)
			{
				List<ItemVariants.VariantEntry> list = ItemVariants._variants[itemId];
				if (list == null)
				{
					list = (ItemVariants._variants[itemId] = new List<ItemVariants.VariantEntry>());
				}
				list.Add(variantEntry = new ItemVariants.VariantEntry(variant));
			}
			variantEntry.AddConditions(conditions);
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x0059A632 File Offset: 0x00598832
		public static bool HasVariant(int itemId, ItemVariant variant)
		{
			return ItemVariants.GetEntry(itemId, variant) != null;
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x0059A640 File Offset: 0x00598840
		public static ItemVariant SelectVariant(int itemId)
		{
			if (!ItemVariants._variants.IndexInRange(itemId))
			{
				return null;
			}
			List<ItemVariants.VariantEntry> list = ItemVariants._variants[itemId];
			if (list == null)
			{
				return null;
			}
			foreach (ItemVariants.VariantEntry variantEntry in list)
			{
				if (variantEntry.AnyConditionMet())
				{
					return variantEntry.Variant;
				}
			}
			return null;
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x0059A6B8 File Offset: 0x005988B8
		static ItemVariants()
		{
			ItemVariants.AddVariant(112, ItemVariants.StrongerVariant, new ItemVariantCondition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(157, ItemVariants.StrongerVariant, new ItemVariantCondition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(1319, ItemVariants.StrongerVariant, new ItemVariantCondition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(1325, ItemVariants.StrongerVariant, new ItemVariantCondition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(2273, ItemVariants.StrongerVariant, new ItemVariantCondition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(3069, ItemVariants.StrongerVariant, new ItemVariantCondition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(5147, ItemVariants.StrongerVariant, new ItemVariantCondition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(517, ItemVariants.WeakerVariant, new ItemVariantCondition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(671, ItemVariants.WeakerVariant, new ItemVariantCondition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(683, ItemVariants.WeakerVariant, new ItemVariantCondition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(725, ItemVariants.WeakerVariant, new ItemVariantCondition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(1314, ItemVariants.WeakerVariant, new ItemVariantCondition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(2623, ItemVariants.WeakerVariant, new ItemVariantCondition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(5279, ItemVariants.WeakerVariant, new ItemVariantCondition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(5280, ItemVariants.WeakerVariant, new ItemVariantCondition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(5281, ItemVariants.WeakerVariant, new ItemVariantCondition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(5282, ItemVariants.WeakerVariant, new ItemVariantCondition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(5283, ItemVariants.WeakerVariant, new ItemVariantCondition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(5284, ItemVariants.WeakerVariant, new ItemVariantCondition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(197, ItemVariants.RebalancedVariant, new ItemVariantCondition[]
			{
				ItemVariants.GetGoodWorld
			});
			ItemVariants.AddVariant(4060, ItemVariants.RebalancedVariant, new ItemVariantCondition[]
			{
				ItemVariants.GetGoodWorld
			});
			ItemVariants.AddVariant(556, ItemVariants.DisabledBossSummonVariant, new ItemVariantCondition[]
			{
				ItemVariants.EverythingWorld
			});
			ItemVariants.AddVariant(557, ItemVariants.DisabledBossSummonVariant, new ItemVariantCondition[]
			{
				ItemVariants.EverythingWorld
			});
			ItemVariants.AddVariant(544, ItemVariants.DisabledBossSummonVariant, new ItemVariantCondition[]
			{
				ItemVariants.EverythingWorld
			});
			ItemVariants.AddVariant(5334, ItemVariants.EnabledVariant, new ItemVariantCondition[]
			{
				ItemVariants.EverythingWorld
			});
		}

		// Token: 0x04004D48 RID: 19784
		private static List<ItemVariants.VariantEntry>[] _variants = new List<ItemVariants.VariantEntry>[(int)ItemID.Count];

		// Token: 0x04004D49 RID: 19785
		public static ItemVariant StrongerVariant = new ItemVariant(NetworkText.FromKey("ItemVariant.Stronger", new object[0]));

		// Token: 0x04004D4A RID: 19786
		public static ItemVariant WeakerVariant = new ItemVariant(NetworkText.FromKey("ItemVariant.Weaker", new object[0]));

		// Token: 0x04004D4B RID: 19787
		public static ItemVariant RebalancedVariant = new ItemVariant(NetworkText.FromKey("ItemVariant.Rebalanced", new object[0]));

		// Token: 0x04004D4C RID: 19788
		public static ItemVariant EnabledVariant = new ItemVariant(NetworkText.FromKey("ItemVariant.Enabled", new object[0]));

		// Token: 0x04004D4D RID: 19789
		public static ItemVariant DisabledBossSummonVariant = new ItemVariant(NetworkText.FromKey("ItemVariant.DisabledBossSummon", new object[0]));

		// Token: 0x04004D4E RID: 19790
		public static ItemVariantCondition RemixWorld = new ItemVariantCondition(NetworkText.FromKey("ItemVariantCondition.RemixWorld", new object[0]), () => Main.remixWorld);

		// Token: 0x04004D4F RID: 19791
		public static ItemVariantCondition GetGoodWorld = new ItemVariantCondition(NetworkText.FromKey("ItemVariantCondition.GetGoodWorld", new object[0]), () => Main.getGoodWorld);

		// Token: 0x04004D50 RID: 19792
		public static ItemVariantCondition EverythingWorld = new ItemVariantCondition(NetworkText.FromKey("ItemVariantCondition.EverythingWorld", new object[0]), () => Main.getGoodWorld && Main.remixWorld);

		// Token: 0x02000763 RID: 1891
		public class VariantEntry
		{
			// Token: 0x17000410 RID: 1040
			// (get) Token: 0x060038DB RID: 14555 RVA: 0x00614703 File Offset: 0x00612903
			public IEnumerable<ItemVariantCondition> Conditions
			{
				get
				{
					return this._conditions;
				}
			}

			// Token: 0x060038DC RID: 14556 RVA: 0x0061470B File Offset: 0x0061290B
			public VariantEntry(ItemVariant variant)
			{
				this.Variant = variant;
			}

			// Token: 0x060038DD RID: 14557 RVA: 0x00614725 File Offset: 0x00612925
			internal void AddConditions(IEnumerable<ItemVariantCondition> conditions)
			{
				this._conditions.AddRange(conditions);
			}

			// Token: 0x060038DE RID: 14558 RVA: 0x00614733 File Offset: 0x00612933
			public bool AnyConditionMet()
			{
				return this.Conditions.Any((ItemVariantCondition c) => c.IsMet());
			}

			// Token: 0x04006458 RID: 25688
			public readonly ItemVariant Variant;

			// Token: 0x04006459 RID: 25689
			private readonly List<ItemVariantCondition> _conditions = new List<ItemVariantCondition>();
		}
	}
}
