using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.GameContent.Prefixes;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Core;
using Terraria.Utilities;

namespace Terraria.ModLoader
{
	// Token: 0x020001EC RID: 492
	public static class PrefixLoader
	{
		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x0600268D RID: 9869 RVA: 0x004FE21A File Offset: 0x004FC41A
		// (set) Token: 0x0600268E RID: 9870 RVA: 0x004FE221 File Offset: 0x004FC421
		public static int PrefixCount { get; private set; } = PrefixID.Count;

		// Token: 0x0600268F RID: 9871 RVA: 0x004FE22C File Offset: 0x004FC42C
		static PrefixLoader()
		{
			foreach (object obj in Enum.GetValues(typeof(PrefixCategory)))
			{
				PrefixCategory category = (PrefixCategory)obj;
				PrefixLoader.categoryPrefixes[category] = new List<ModPrefix>();
			}
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x004FE2C4 File Offset: 0x004FC4C4
		internal static void RegisterPrefix(ModPrefix prefix)
		{
			PrefixLoader.prefixes.Add(prefix);
			PrefixLoader.categoryPrefixes[prefix.Category].Add(prefix);
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x004FE2E7 File Offset: 0x004FC4E7
		internal static int ReservePrefixID()
		{
			return PrefixLoader.PrefixCount++;
		}

		/// <summary>
		/// Returns the ModPrefix associated with specified type
		/// If not a ModPrefix, returns null.
		/// </summary>
		// Token: 0x06002692 RID: 9874 RVA: 0x004FE2F6 File Offset: 0x004FC4F6
		public static ModPrefix GetPrefix(int type)
		{
			if (type < PrefixID.Count || type >= PrefixLoader.PrefixCount)
			{
				return null;
			}
			return PrefixLoader.prefixes[type - PrefixID.Count];
		}

		/// <summary>
		/// Returns a list of all modded prefixes of a certain category.
		/// </summary>
		// Token: 0x06002693 RID: 9875 RVA: 0x004FE31B File Offset: 0x004FC51B
		public static IReadOnlyList<ModPrefix> GetPrefixesInCategory(PrefixCategory category)
		{
			return PrefixLoader.categoryPrefixes[category];
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x004FE328 File Offset: 0x004FC528
		internal static void ResizeArrays()
		{
			LoaderUtils.ResetStaticMembers(typeof(PrefixID), true);
			Array.Resize<LocalizedText>(ref Lang.prefix, PrefixLoader.PrefixCount);
			Array.Resize<List<PrefixCategory>>(ref PrefixLoader.itemPrefixesByType, ItemLoader.ItemCount);
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x004FE358 File Offset: 0x004FC558
		internal static void FinishSetup()
		{
			foreach (ModPrefix prefix in PrefixLoader.prefixes)
			{
				Lang.prefix[prefix.Type] = prefix.DisplayName;
			}
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x004FE3B0 File Offset: 0x004FC5B0
		internal static void Unload()
		{
			PrefixLoader.prefixes.Clear();
			PrefixLoader.PrefixCount = PrefixID.Count;
			foreach (object obj in Enum.GetValues(typeof(PrefixCategory)))
			{
				PrefixCategory category = (PrefixCategory)obj;
				PrefixLoader.categoryPrefixes[category].Clear();
			}
			PrefixLoader.itemPrefixesByType = new List<PrefixCategory>[(int)ItemID.Count];
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x004FE440 File Offset: 0x004FC640
		public static bool CanRoll(Item item, int prefix)
		{
			if (!ItemLoader.AllowPrefix(item, prefix))
			{
				return false;
			}
			ModPrefix modPrefix = PrefixLoader.GetPrefix(prefix);
			if (modPrefix != null)
			{
				return modPrefix.CanRoll(item) && (modPrefix.Category == PrefixCategory.Custom || item.GetPrefixCategories().Contains(modPrefix.Category));
			}
			using (List<PrefixCategory>.Enumerator enumerator = item.GetPrefixCategories().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (Item.GetVanillaPrefixes(enumerator.Current).Contains(prefix))
					{
						return true;
					}
				}
			}
			return PrefixLegacy.ItemSets.ItemsThatCanHaveLegendary2[item.type] && prefix == 84;
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x004FE4F4 File Offset: 0x004FC6F4
		public static bool Roll(Item item, UnifiedRandom unifiedRandom, out int prefix, bool justCheck)
		{
			PrefixLoader.<>c__DisplayClass16_0 CS$<>8__locals1 = new PrefixLoader.<>c__DisplayClass16_0();
			CS$<>8__locals1.item = item;
			int forcedPrefix = ItemLoader.ChoosePrefix(CS$<>8__locals1.item, unifiedRandom);
			if (forcedPrefix > 0 && PrefixLoader.CanRoll(CS$<>8__locals1.item, forcedPrefix))
			{
				prefix = forcedPrefix;
				return true;
			}
			prefix = 0;
			CS$<>8__locals1.wr = new WeightedRandom<int>(unifiedRandom);
			CS$<>8__locals1.addedPrefixes = new HashSet<int>();
			List<PrefixCategory> categories = CS$<>8__locals1.item.GetPrefixCategories();
			if (categories.Count == 0)
			{
				return false;
			}
			if (justCheck)
			{
				return true;
			}
			foreach (PrefixCategory category in categories)
			{
				foreach (int pre in Item.GetVanillaPrefixes(category))
				{
					if (CS$<>8__locals1.addedPrefixes.Add(pre))
					{
						CS$<>8__locals1.wr.Add(pre, 1.0);
					}
				}
				CS$<>8__locals1.<Roll>g__AddCategory|0(category);
			}
			if (PrefixLegacy.ItemSets.ItemsThatCanHaveLegendary2[CS$<>8__locals1.item.type])
			{
				CS$<>8__locals1.wr.Add(84, 1.0);
			}
			for (int i = 0; i < 50; i++)
			{
				prefix = CS$<>8__locals1.wr.Get();
				if (ItemLoader.AllowPrefix(CS$<>8__locals1.item, prefix))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x004FE64C File Offset: 0x004FC84C
		public static bool IsWeaponSubCategory(PrefixCategory category)
		{
			return category == PrefixCategory.Melee || category == PrefixCategory.Ranged || category == PrefixCategory.Magic;
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x004FE65C File Offset: 0x004FC85C
		public static void ApplyAccessoryEffects(Player player, Item item)
		{
			ModPrefix prefix = PrefixLoader.GetPrefix(item.prefix);
			if (prefix != null)
			{
				prefix.ApplyAccessoryEffects(player);
			}
		}

		// Token: 0x04001865 RID: 6245
		internal static readonly IList<ModPrefix> prefixes = new List<ModPrefix>();

		// Token: 0x04001866 RID: 6246
		internal static readonly IDictionary<PrefixCategory, List<ModPrefix>> categoryPrefixes = new Dictionary<PrefixCategory, List<ModPrefix>>();

		// Token: 0x04001867 RID: 6247
		internal static List<PrefixCategory>[] itemPrefixesByType = new List<PrefixCategory>[(int)ItemID.Count];
	}
}
