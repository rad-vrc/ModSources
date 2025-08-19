using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent.Items
{
	/// <summary>
	/// Handles conditional variants for <see cref="T:Terraria.Item" />s, commonly used for secret seeds.
	/// </summary>
	// Token: 0x020005EB RID: 1515
	public static class ItemVariants
	{
		/// <summary>
		/// Gets all of the <see cref="T:Terraria.GameContent.Items.ItemVariant" />s associated with <paramref name="itemId" />.
		/// </summary>
		/// <param name="itemId">The <see cref="F:Terraria.Item.type" /> to get <see cref="T:Terraria.GameContent.Items.ItemVariant" />s for.</param>
		/// <returns>A list of all registered <see cref="T:Terraria.GameContent.Items.ItemVariant" />s for <paramref name="itemId" />.</returns>
		// Token: 0x0600436A RID: 17258 RVA: 0x005FF524 File Offset: 0x005FD724
		public static IEnumerable<ItemVariants.VariantEntry> GetVariants(int itemId)
		{
			if (!ItemVariants._variants.IndexInRange(itemId))
			{
				return Enumerable.Empty<ItemVariants.VariantEntry>();
			}
			IEnumerable<ItemVariants.VariantEntry> enumerable = ItemVariants._variants[itemId];
			return enumerable ?? Enumerable.Empty<ItemVariants.VariantEntry>();
		}

		// Token: 0x0600436B RID: 17259 RVA: 0x005FF558 File Offset: 0x005FD758
		private static ItemVariants.VariantEntry GetEntry(int itemId, ItemVariant variant)
		{
			return ItemVariants.GetVariants(itemId).SingleOrDefault((ItemVariants.VariantEntry v) => v.Variant == variant);
		}

		/// <summary>
		/// Registers an <see cref="T:Terraria.GameContent.Items.ItemVariant" /> for the given <see cref="F:Terraria.Item.type" />.
		/// </summary>
		/// <param name="itemId">The <see cref="F:Terraria.Item.type" /> to register the <see cref="T:Terraria.GameContent.Items.ItemVariant" /> for.</param>
		/// <param name="variant">The <see cref="T:Terraria.GameContent.Items.ItemVariant" /> to register to <paramref name="itemId" />.</param>
		/// <param name="conditions">The conditions under which <see cref="T:Terraria.Item" />s of type <paramref name="itemId" /> will have <paramref name="variant" /> applied. (<see cref="M:Terraria.GameContent.Items.ItemVariants.SelectVariant(System.Int32)" />)</param>
		// Token: 0x0600436C RID: 17260 RVA: 0x005FF58C File Offset: 0x005FD78C
		public static void AddVariant(int itemId, ItemVariant variant, params Condition[] conditions)
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

		/// <summary>
		/// Determines if an <see cref="F:Terraria.Item.type" /> has a particular <see cref="T:Terraria.GameContent.Items.ItemVariant" />.
		/// </summary>
		/// <param name="itemId">The <see cref="F:Terraria.Item.type" /> to check.</param>
		/// <param name="variant">The <see cref="T:Terraria.GameContent.Items.ItemVariant" /> to check for.</param>
		/// <returns><see langword="true" /> if <paramref name="itemId" /> has a registered <see cref="T:Terraria.GameContent.Items.ItemVariant" /> of type <paramref name="variant" />, <see langword="false" /> otherwise.</returns>
		/// <remarks>This method only checks if the given <see cref="T:Terraria.GameContent.Items.ItemVariant" /> exists, not if it will be applied.</remarks>
		// Token: 0x0600436D RID: 17261 RVA: 0x005FF5D4 File Offset: 0x005FD7D4
		public static bool HasVariant(int itemId, ItemVariant variant)
		{
			return ItemVariants.GetEntry(itemId, variant) != null;
		}

		/// <summary>
		/// Determines which <see cref="T:Terraria.GameContent.Items.ItemVariant" /> should be applied to an item of type <paramref name="itemId" />.
		/// </summary>
		/// <param name="itemId">The <see cref="F:Terraria.Item.type" /> to check.</param>
		/// <returns>The <see cref="T:Terraria.GameContent.Items.ItemVariant" /> to use under the current conditions, or <see langword="null" /> if no appropriate <see cref="T:Terraria.GameContent.Items.ItemVariant" /> exists.</returns>
		// Token: 0x0600436E RID: 17262 RVA: 0x005FF5E0 File Offset: 0x005FD7E0
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
			foreach (ItemVariants.VariantEntry item in list)
			{
				if (item.AnyConditionMet())
				{
					return item.Variant;
				}
			}
			return null;
		}

		// Token: 0x0600436F RID: 17263 RVA: 0x005FF658 File Offset: 0x005FD858
		static ItemVariants()
		{
			ItemVariants.AddVariant(112, ItemVariants.StrongerVariant, new Condition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(157, ItemVariants.StrongerVariant, new Condition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(1319, ItemVariants.StrongerVariant, new Condition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(1325, ItemVariants.StrongerVariant, new Condition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(2273, ItemVariants.StrongerVariant, new Condition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(3069, ItemVariants.StrongerVariant, new Condition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(5147, ItemVariants.StrongerVariant, new Condition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(517, ItemVariants.WeakerVariant, new Condition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(671, ItemVariants.WeakerVariant, new Condition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(683, ItemVariants.WeakerVariant, new Condition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(725, ItemVariants.WeakerVariant, new Condition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(1314, ItemVariants.WeakerVariant, new Condition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(2623, ItemVariants.WeakerVariant, new Condition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(5279, ItemVariants.WeakerVariant, new Condition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(5280, ItemVariants.WeakerVariant, new Condition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(5281, ItemVariants.WeakerVariant, new Condition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(5282, ItemVariants.WeakerVariant, new Condition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(5283, ItemVariants.WeakerVariant, new Condition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(5284, ItemVariants.WeakerVariant, new Condition[]
			{
				ItemVariants.RemixWorld
			});
			ItemVariants.AddVariant(197, ItemVariants.RebalancedVariant, new Condition[]
			{
				ItemVariants.GetGoodWorld
			});
			ItemVariants.AddVariant(4060, ItemVariants.RebalancedVariant, new Condition[]
			{
				ItemVariants.GetGoodWorld
			});
			ItemVariants.AddVariant(556, ItemVariants.DisabledBossSummonVariant, new Condition[]
			{
				ItemVariants.EverythingWorld
			});
			ItemVariants.AddVariant(557, ItemVariants.DisabledBossSummonVariant, new Condition[]
			{
				ItemVariants.EverythingWorld
			});
			ItemVariants.AddVariant(544, ItemVariants.DisabledBossSummonVariant, new Condition[]
			{
				ItemVariants.EverythingWorld
			});
			ItemVariants.AddVariant(5334, ItemVariants.EnabledVariant, new Condition[]
			{
				ItemVariants.EverythingWorld
			});
		}

		// Token: 0x04005A22 RID: 23074
		private static List<ItemVariants.VariantEntry>[] _variants = new List<ItemVariants.VariantEntry>[(int)ItemID.Count];

		/// <summary>
		/// Represents the stronger variant of items used on the Remix seed.
		/// </summary>
		// Token: 0x04005A23 RID: 23075
		public static ItemVariant StrongerVariant = new ItemVariant(Language.GetText("ItemVariant.Stronger"));

		/// <summary>
		/// Represents the weaker variant of items used on the Remix seed.
		/// </summary>
		// Token: 0x04005A24 RID: 23076
		public static ItemVariant WeakerVariant = new ItemVariant(Language.GetText("ItemVariant.Weaker"));

		/// <summary>
		/// Represents the rebalanced variant of items used on the "For the Worthy" seed.
		/// </summary>
		// Token: 0x04005A25 RID: 23077
		public static ItemVariant RebalancedVariant = new ItemVariant(Language.GetText("ItemVariant.Rebalanced"));

		/// <summary>
		/// Represents a variant of an item that is conditionally enabled.
		/// </summary>
		// Token: 0x04005A26 RID: 23078
		public static ItemVariant EnabledVariant = new ItemVariant(Language.GetText("ItemVariant.Enabled"));

		/// <summary>
		/// Represents a variant of a boss summoning item that is conditionally disabled.
		/// </summary>
		// Token: 0x04005A27 RID: 23079
		public static ItemVariant DisabledBossSummonVariant = new ItemVariant(Language.GetText("ItemVariant.DisabledBossSummon"));

		// Token: 0x04005A28 RID: 23080
		private static Condition RemixWorld = Condition.RemixWorld;

		// Token: 0x04005A29 RID: 23081
		private static Condition GetGoodWorld = Condition.ForTheWorthyWorld;

		// Token: 0x04005A2A RID: 23082
		private static Condition EverythingWorld = Condition.ZenithWorld;

		// Token: 0x02000C76 RID: 3190
		public class VariantEntry
		{
			// Token: 0x1700096A RID: 2410
			// (get) Token: 0x06006027 RID: 24615 RVA: 0x006D1E44 File Offset: 0x006D0044
			public IEnumerable<Condition> Conditions
			{
				get
				{
					return this._conditions;
				}
			}

			// Token: 0x06006028 RID: 24616 RVA: 0x006D1E4C File Offset: 0x006D004C
			public VariantEntry(ItemVariant variant)
			{
				this.Variant = variant;
			}

			// Token: 0x06006029 RID: 24617 RVA: 0x006D1E66 File Offset: 0x006D0066
			internal void AddConditions(IEnumerable<Condition> conditions)
			{
				this._conditions.AddRange(conditions);
			}

			// Token: 0x0600602A RID: 24618 RVA: 0x006D1E74 File Offset: 0x006D0074
			public bool AnyConditionMet()
			{
				return this.Conditions.Any((Condition c) => c.IsMet());
			}

			// Token: 0x040079D8 RID: 31192
			public readonly ItemVariant Variant;

			// Token: 0x040079D9 RID: 31193
			private readonly List<Condition> _conditions = new List<Condition>();
		}
	}
}
