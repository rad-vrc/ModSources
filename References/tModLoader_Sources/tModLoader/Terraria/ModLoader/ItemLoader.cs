using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Prefixes;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.IO;
using Terraria.UI;
using Terraria.Utilities;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This serves as the central class from which item-related functions are carried out. It also stores a list of mod items by ID.
	/// </summary>
	// Token: 0x02000185 RID: 389
	public static class ItemLoader
	{
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06001E48 RID: 7752 RVA: 0x004D5A24 File Offset: 0x004D3C24
		// (set) Token: 0x06001E49 RID: 7753 RVA: 0x004D5A2B File Offset: 0x004D3C2B
		public static int ItemCount { get; private set; } = (int)ItemID.Count;

		// Token: 0x06001E4A RID: 7754 RVA: 0x004D5A34 File Offset: 0x004D3C34
		private static GlobalHookList<GlobalItem> AddHook<F>(Expression<Func<GlobalItem, F>> func) where F : Delegate
		{
			GlobalHookList<GlobalItem> hook = GlobalHookList<GlobalItem>.Create<F>(func);
			ItemLoader.hooks.Add(hook);
			return hook;
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x004D5A54 File Offset: 0x004D3C54
		public static T AddModHook<T>(T hook) where T : GlobalHookList<GlobalItem>
		{
			ItemLoader.modHooks.Add(hook);
			return hook;
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x004D5A67 File Offset: 0x004D3C67
		internal static int Register(ModItem item)
		{
			ItemLoader.items.Add(item);
			return ItemLoader.ItemCount++;
		}

		/// <summary>
		/// Gets the ModItem template instance corresponding to the specified type (not the clone/new instance which gets added to Items as the game is played). Returns null if no modded item has the given type.
		/// </summary>
		// Token: 0x06001E4D RID: 7757 RVA: 0x004D5A81 File Offset: 0x004D3C81
		public static ModItem GetItem(int type)
		{
			if (type < (int)ItemID.Count || type >= ItemLoader.ItemCount)
			{
				return null;
			}
			return ItemLoader.items[type - (int)ItemID.Count];
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x004D5AA8 File Offset: 0x004D3CA8
		internal static void ResizeArrays(bool unloading)
		{
			if (!unloading)
			{
				GlobalList<GlobalItem>.FinishLoading(ItemLoader.ItemCount);
			}
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.Item, ItemLoader.ItemCount);
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.ItemFlame, ItemLoader.ItemCount);
			LoaderUtils.ResetStaticMembers(typeof(ItemID), true);
			LoaderUtils.ResetStaticMembers(typeof(AmmoID), true);
			LoaderUtils.ResetStaticMembers(typeof(PrefixLegacy.ItemSets), true);
			Array.Resize<int>(ref Item.cachedItemSpawnsByType, ItemLoader.ItemCount);
			Array.Resize<bool>(ref Item.staff, ItemLoader.ItemCount);
			Array.Resize<bool>(ref Item.claw, ItemLoader.ItemCount);
			Array.Resize<LocalizedText>(ref Lang._itemNameCache, ItemLoader.ItemCount);
			Array.Resize<ItemTooltip>(ref Lang._itemTooltipCache, ItemLoader.ItemCount);
			Array.Resize<Recipe>(ref RecipeLoader.FirstRecipeForItem, ItemLoader.ItemCount);
			for (int i = (int)ItemID.Count; i < ItemLoader.ItemCount; i++)
			{
				Lang._itemNameCache[i] = LocalizedText.Empty;
				Lang._itemTooltipCache[i] = ItemTooltip.None;
				Item.cachedItemSpawnsByType[i] = -1;
			}
			List<int> itemAnimationsRegistered = Main.itemAnimationsRegistered;
			lock (itemAnimationsRegistered)
			{
				Array.Resize<DrawAnimation>(ref Main.itemAnimations, ItemLoader.ItemCount);
				Main.InitializeItemAnimations();
			}
			if (unloading)
			{
				Array.Resize<int>(ref Main.anglerQuestItemNetIDs, ItemLoader.vanillaQuestFishCount);
				return;
			}
			Main.anglerQuestItemNetIDs = Main.anglerQuestItemNetIDs.Concat(from modItem in ItemLoader.items
			where modItem.IsQuestFish()
			select modItem.Type).ToArray<int>();
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x004D5C54 File Offset: 0x004D3E54
		internal static void FinishSetup()
		{
			GlobalLoaderUtils<GlobalItem, Item>.BuildTypeLookups(new Action<int>(new Item().SetDefaults));
			ItemLoader.UpdateHookLists();
			GlobalTypeLookups<GlobalItem>.LogStats();
			foreach (ModItem item in ItemLoader.items)
			{
				Lang._itemNameCache[item.Type] = item.DisplayName;
				Lang._itemTooltipCache[item.Type] = ItemTooltip.FromLocalization(item.Tooltip);
				ContentSamples.ItemsByType[item.Type].RebuildTooltip();
			}
			ItemLoader.ValidateDropsSet();
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x004D5CFC File Offset: 0x004D3EFC
		private static void UpdateHookLists()
		{
			foreach (GlobalHookList<GlobalItem> globalHookList in ItemLoader.hooks.Union(ItemLoader.modHooks))
			{
				globalHookList.Update();
			}
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x004D5D50 File Offset: 0x004D3F50
		internal static void ValidateDropsSet()
		{
			foreach (KeyValuePair<int, ValueTuple<int, int>> pair in ItemID.Sets.GeodeDrops)
			{
				string exceptionCommon = Lang.GetItemNameValue(pair.Key) + " registered in 'ItemID.Sets.GeodeDrops'";
				if (pair.Value.Item1 < 1)
				{
					throw new Exception(exceptionCommon + " must have minStack bigger than 0");
				}
				if (pair.Value.Item2 <= pair.Value.Item1)
				{
					throw new Exception(exceptionCommon + " must have maxStack bigger than minStack");
				}
			}
			foreach (KeyValuePair<int, ValueTuple<int, int>> pair2 in ItemID.Sets.OreDropsFromSlime)
			{
				string exceptionCommon2 = Lang.GetItemNameValue(pair2.Key) + " registered in 'ItemID.Sets.OreDropsFromSlime'";
				if (pair2.Value.Item1 < 1)
				{
					throw new Exception(exceptionCommon2 + " must have minStack bigger than 0");
				}
				if (pair2.Value.Item2 < pair2.Value.Item1)
				{
					throw new Exception(exceptionCommon2 + " must have maxStack bigger than or equal to minStack");
				}
			}
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x004D5E9C File Offset: 0x004D409C
		internal static void Unload()
		{
			ItemLoader.ItemCount = (int)ItemID.Count;
			ItemLoader.items.Clear();
			FlexibleTileWand.Reload();
			GlobalList<GlobalItem>.Reset();
			ItemLoader.modHooks.Clear();
			ItemLoader.UpdateHookLists();
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x004D5ECB File Offset: 0x004D40CB
		internal static bool IsModItem(int index)
		{
			return index >= (int)ItemID.Count;
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x004D5ED8 File Offset: 0x004D40D8
		internal static bool MeleePrefix(Item item)
		{
			return item.ModItem != null && item.ModItem.MeleePrefix();
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x004D5EEF File Offset: 0x004D40EF
		internal static bool WeaponPrefix(Item item)
		{
			return item.ModItem != null && item.ModItem.WeaponPrefix();
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x004D5F06 File Offset: 0x004D4106
		internal static bool RangedPrefix(Item item)
		{
			return item.ModItem != null && item.ModItem.RangedPrefix();
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x004D5F1D File Offset: 0x004D411D
		internal static bool MagicPrefix(Item item)
		{
			return item.ModItem != null && item.ModItem.MagicPrefix();
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x004D5F34 File Offset: 0x004D4134
		internal static void SetDefaults(Item item, bool createModItem = true)
		{
			if (ItemLoader.IsModItem(item.type) && createModItem)
			{
				item.ModItem = ItemLoader.GetItem(item.type).NewInstance(item);
			}
			GlobalLoaderUtils<GlobalItem, Item>.SetDefaults(item, ref item._globals, delegate(Item i)
			{
				ModItem modItem = i.ModItem;
				if (modItem != null)
				{
					modItem.AutoDefaults();
				}
				ModItem modItem2 = i.ModItem;
				if (modItem2 == null)
				{
					return;
				}
				modItem2.SetDefaults();
			});
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x004D5F94 File Offset: 0x004D4194
		internal static void OnSpawn(Item item, IEntitySource source)
		{
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.OnSpawn(source);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookOnSpawn.Enumerate(item))
			{
				globalItem.OnSpawn(item, source);
			}
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x004D5FE0 File Offset: 0x004D41E0
		public static void OnCreated(Item item, ItemCreationContext context)
		{
			foreach (GlobalItem globalItem in ItemLoader.HookOnCreate.Enumerate(item))
			{
				globalItem.OnCreated(item, context);
			}
			ModItem modItem = item.ModItem;
			if (modItem == null)
			{
				return;
			}
			modItem.OnCreated(context);
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x004D602C File Offset: 0x004D422C
		public static int ChoosePrefix(Item item, UnifiedRandom rand)
		{
			foreach (GlobalItem globalItem in ItemLoader.HookChoosePrefix.Enumerate(item))
			{
				int pre = globalItem.ChoosePrefix(item, rand);
				if (pre > 0)
				{
					return pre;
				}
			}
			if (item.ModItem != null)
			{
				int pre2 = item.ModItem.ChoosePrefix(rand);
				if (pre2 > 0)
				{
					return pre2;
				}
			}
			return -1;
		}

		/// <summary>
		/// Allows for blocking, forcing and altering chance of prefix rolling.
		/// False (block) takes precedence over True (force).
		/// Null gives vanilla behavior
		/// </summary>
		// Token: 0x06001E5C RID: 7772 RVA: 0x004D608C File Offset: 0x004D428C
		public static bool? PrefixChance(Item item, int pre, UnifiedRandom rand)
		{
			bool? result = null;
			foreach (GlobalItem globalItem in ItemLoader.HookPrefixChance.Enumerate(item))
			{
				bool? r = globalItem.PrefixChance(item, pre, rand);
				if (r != null)
				{
					result = new bool?(r.Value && result.GetValueOrDefault(true));
				}
			}
			if (item.ModItem != null)
			{
				bool? r2 = item.ModItem.PrefixChance(pre, rand);
				if (r2 != null)
				{
					result = new bool?(r2.Value && result.GetValueOrDefault(true));
				}
			}
			return result;
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x004D6130 File Offset: 0x004D4330
		public static bool AllowPrefix(Item item, int pre)
		{
			bool result = true;
			foreach (GlobalItem g in ItemLoader.HookAllowPrefix.Enumerate(item))
			{
				result &= g.AllowPrefix(item, pre);
			}
			if (item.ModItem != null)
			{
				result &= item.ModItem.AllowPrefix(pre);
			}
			return result;
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x004D618C File Offset: 0x004D438C
		public static bool CanUseItem(Item item, Player player)
		{
			if (item.ModItem != null && !item.ModItem.CanUseItem(player))
			{
				return false;
			}
			EntityGlobalsEnumerator<GlobalItem> enumerator = ItemLoader.HookCanUseItem.Enumerate(item).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.CanUseItem(item, player))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x004D61E4 File Offset: 0x004D43E4
		public static bool? CanAutoReuseItem(Item item, Player player)
		{
			bool? flag = null;
			foreach (GlobalItem globalItem in ItemLoader.HookCanAutoReuseItem.Enumerate(item))
			{
				bool? allow = globalItem.CanAutoReuseItem(item, player);
				if (allow != null)
				{
					if (!allow.Value)
					{
						return new bool?(false);
					}
					flag = new bool?(true);
				}
			}
			if (item.ModItem != null)
			{
				bool? allow2 = item.ModItem.CanAutoReuseItem(player);
				if (allow2 != null)
				{
					if (!allow2.Value)
					{
						return new bool?(false);
					}
					flag = new bool?(true);
				}
			}
			return flag;
		}

		/// <summary>
		/// Calls ModItem.UseStyle and all GlobalItem.UseStyle hooks.
		/// </summary>
		// Token: 0x06001E60 RID: 7776 RVA: 0x004D6280 File Offset: 0x004D4480
		public static void UseStyle(Item item, Player player, Rectangle heldItemFrame)
		{
			if (item.IsAir)
			{
				return;
			}
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.UseStyle(player, heldItemFrame);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookUseStyle.Enumerate(item))
			{
				globalItem.UseStyle(item, player, heldItemFrame);
			}
		}

		/// <summary>
		/// If the player is not holding onto a rope and is not in the middle of using an item, calls ModItem.HoldStyle and all GlobalItem.HoldStyle hooks.
		/// <br /> Returns whether or not the vanilla logic should be skipped.
		/// </summary>
		// Token: 0x06001E61 RID: 7777 RVA: 0x004D62D8 File Offset: 0x004D44D8
		public static void HoldStyle(Item item, Player player, Rectangle heldItemFrame)
		{
			if (item.IsAir || player.pulley || player.ItemAnimationActive)
			{
				return;
			}
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.HoldStyle(player, heldItemFrame);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookHoldStyle.Enumerate(item))
			{
				globalItem.HoldStyle(item, player, heldItemFrame);
			}
		}

		/// <summary>
		/// Calls ModItem.HoldItem and all GlobalItem.HoldItem hooks.
		/// </summary>
		// Token: 0x06001E62 RID: 7778 RVA: 0x004D6340 File Offset: 0x004D4540
		public static void HoldItem(Item item, Player player)
		{
			if (item.IsAir)
			{
				return;
			}
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.HoldItem(player);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookHoldItem.Enumerate(item))
			{
				globalItem.HoldItem(item, player);
			}
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x004D6398 File Offset: 0x004D4598
		public static float UseTimeMultiplier(Item item, Player player)
		{
			if (item.IsAir)
			{
				return 1f;
			}
			ModItem modItem = item.ModItem;
			float multiplier = (modItem != null) ? modItem.UseTimeMultiplier(player) : 1f;
			foreach (GlobalItem g in ItemLoader.HookUseTimeMultiplier.Enumerate(item))
			{
				multiplier *= g.UseTimeMultiplier(item, player);
			}
			return multiplier;
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x004D6400 File Offset: 0x004D4600
		public static float UseAnimationMultiplier(Item item, Player player)
		{
			if (item.IsAir)
			{
				return 1f;
			}
			ModItem modItem = item.ModItem;
			float multiplier = (modItem != null) ? modItem.UseAnimationMultiplier(player) : 1f;
			foreach (GlobalItem g in ItemLoader.HookUseAnimationMultiplier.Enumerate(item))
			{
				multiplier *= g.UseAnimationMultiplier(item, player);
			}
			return multiplier;
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x004D6468 File Offset: 0x004D4668
		public static float UseSpeedMultiplier(Item item, Player player)
		{
			if (item.IsAir)
			{
				return 1f;
			}
			ModItem modItem = item.ModItem;
			float multiplier = (modItem != null) ? modItem.UseSpeedMultiplier(player) : 1f;
			foreach (GlobalItem g in ItemLoader.HookUseSpeedMultiplier.Enumerate(item))
			{
				multiplier *= g.UseSpeedMultiplier(item, player);
			}
			return multiplier;
		}

		/// <summary>
		/// Calls ModItem.GetHealLife, then all GlobalItem.GetHealLife hooks.
		/// </summary>
		// Token: 0x06001E66 RID: 7782 RVA: 0x004D64D0 File Offset: 0x004D46D0
		public static void GetHealLife(Item item, Player player, bool quickHeal, ref int healValue)
		{
			if (item.IsAir)
			{
				return;
			}
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.GetHealLife(player, quickHeal, ref healValue);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookGetHealLife.Enumerate(item))
			{
				globalItem.GetHealLife(item, player, quickHeal, ref healValue);
			}
		}

		/// <summary>
		/// Calls ModItem.GetHealMana, then all GlobalItem.GetHealMana hooks.
		/// </summary>
		// Token: 0x06001E67 RID: 7783 RVA: 0x004D652C File Offset: 0x004D472C
		public static void GetHealMana(Item item, Player player, bool quickHeal, ref int healValue)
		{
			if (item.IsAir)
			{
				return;
			}
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.GetHealMana(player, quickHeal, ref healValue);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookGetHealMana.Enumerate(item))
			{
				globalItem.GetHealMana(item, player, quickHeal, ref healValue);
			}
		}

		/// <summary>
		/// Calls ModItem.ModifyManaCost, then all GlobalItem.ModifyManaCost hooks.
		/// </summary>
		// Token: 0x06001E68 RID: 7784 RVA: 0x004D6588 File Offset: 0x004D4788
		public static void ModifyManaCost(Item item, Player player, ref float reduce, ref float mult)
		{
			if (item.IsAir)
			{
				return;
			}
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.ModifyManaCost(player, ref reduce, ref mult);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookModifyManaCost.Enumerate(item))
			{
				globalItem.ModifyManaCost(item, player, ref reduce, ref mult);
			}
		}

		/// <summary>
		/// Calls ModItem.OnMissingMana, then all GlobalItem.OnMissingMana hooks.
		/// </summary>
		// Token: 0x06001E69 RID: 7785 RVA: 0x004D65E4 File Offset: 0x004D47E4
		public static void OnMissingMana(Item item, Player player, int neededMana)
		{
			if (item.IsAir)
			{
				return;
			}
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.OnMissingMana(player, neededMana);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookOnMissingMana.Enumerate(item))
			{
				globalItem.OnMissingMana(item, player, neededMana);
			}
		}

		/// <summary>
		/// Calls ModItem.OnConsumeMana, then all GlobalItem.OnConsumeMana hooks.
		/// </summary>
		// Token: 0x06001E6A RID: 7786 RVA: 0x004D663C File Offset: 0x004D483C
		public static void OnConsumeMana(Item item, Player player, int manaConsumed)
		{
			if (item.IsAir)
			{
				return;
			}
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.OnConsumeMana(player, manaConsumed);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookOnConsumeMana.Enumerate(item))
			{
				globalItem.OnConsumeMana(item, player, manaConsumed);
			}
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x004D6694 File Offset: 0x004D4894
		public static bool? CanConsumeBait(Player player, Item bait)
		{
			ModItem modItem = bait.ModItem;
			bool? ret = (modItem != null) ? modItem.CanConsumeBait(player) : null;
			foreach (GlobalItem globalItem in ItemLoader.HookCanConsumeBait.Enumerate(bait))
			{
				bool? flag = globalItem.CanConsumeBait(player, bait);
				if (flag != null)
				{
					bool b = flag.GetValueOrDefault();
					ret = new bool?(ret.GetValueOrDefault(true) && b);
				}
			}
			return ret;
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x004D6710 File Offset: 0x004D4910
		public static void ModifyResearchSorting(Item item, ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
		{
			if (item.IsAir)
			{
				return;
			}
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.ModifyResearchSorting(ref itemGroup);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookModifyResearchSorting.Enumerate(item))
			{
				globalItem.ModifyResearchSorting(item, ref itemGroup);
			}
		}

		/// <summary>
		/// Hook that determines if an item will be prevented from being consumed by the research function. 
		/// </summary>
		/// <param name="item">The item to be consumed or not</param>
		// Token: 0x06001E6D RID: 7789 RVA: 0x004D6768 File Offset: 0x004D4968
		public static bool CanResearch(Item item)
		{
			if (item.ModItem != null && !item.ModItem.CanResearch())
			{
				return false;
			}
			EntityGlobalsEnumerator<GlobalItem> enumerator = ItemLoader.HookCanResearch.Enumerate(item).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.Instance(item).CanResearch(item))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x004D67C4 File Offset: 0x004D49C4
		public static void OnResearched(Item item, bool fullyResearched)
		{
			if (item.IsAir)
			{
				return;
			}
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.OnResearched(fullyResearched);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookOnResearched.Enumerate(item))
			{
				globalItem.Instance(item).OnResearched(item, fullyResearched);
			}
		}

		/// <summary>
		/// Calls ModItem.HookModifyWeaponDamage, then all GlobalItem.HookModifyWeaponDamage hooks.
		/// </summary>
		// Token: 0x06001E6F RID: 7791 RVA: 0x004D6820 File Offset: 0x004D4A20
		public static void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
		{
			if (item.IsAir)
			{
				return;
			}
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.ModifyWeaponDamage(player, ref damage);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookModifyWeaponDamage.Enumerate(item))
			{
				globalItem.ModifyWeaponDamage(item, player, ref damage);
			}
		}

		/// <summary>
		/// Calls ModItem.ModifyWeaponKnockback, then all GlobalItem.ModifyWeaponKnockback hooks.
		/// </summary>
		// Token: 0x06001E70 RID: 7792 RVA: 0x004D6878 File Offset: 0x004D4A78
		public static void ModifyWeaponKnockback(Item item, Player player, ref StatModifier knockback)
		{
			if (item.IsAir)
			{
				return;
			}
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.ModifyWeaponKnockback(player, ref knockback);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookModifyWeaponKnockback.Enumerate(item))
			{
				globalItem.ModifyWeaponKnockback(item, player, ref knockback);
			}
		}

		/// <summary>
		/// Calls ModItem.ModifyWeaponCrit, then all GlobalItem.ModifyWeaponCrit hooks.
		/// </summary>
		// Token: 0x06001E71 RID: 7793 RVA: 0x004D68D0 File Offset: 0x004D4AD0
		public static void ModifyWeaponCrit(Item item, Player player, ref float crit)
		{
			if (item.IsAir)
			{
				return;
			}
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.ModifyWeaponCrit(player, ref crit);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookModifyWeaponCrit.Enumerate(item))
			{
				globalItem.ModifyWeaponCrit(item, player, ref crit);
			}
		}

		/// <summary>
		/// Calls ModItem.NeedsAmmo, then all GlobalItem.NeedsAmmo hooks, until any of them returns false.
		/// </summary>
		// Token: 0x06001E72 RID: 7794 RVA: 0x004D6928 File Offset: 0x004D4B28
		public static bool NeedsAmmo(Item weapon, Player player)
		{
			ModItem modItem = weapon.ModItem;
			if (modItem != null && !modItem.NeedsAmmo(player))
			{
				return false;
			}
			EntityGlobalsEnumerator<GlobalItem> enumerator = ItemLoader.HookNeedsAmmo.Enumerate(weapon).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.NeedsAmmo(weapon, player))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Calls ModItem.PickAmmo, then all GlobalItem.PickAmmo hooks.
		/// </summary>
		// Token: 0x06001E73 RID: 7795 RVA: 0x004D6984 File Offset: 0x004D4B84
		public static void PickAmmo(Item weapon, Item ammo, Player player, ref int type, ref float speed, ref StatModifier damage, ref float knockback)
		{
			ModItem modItem = ammo.ModItem;
			if (modItem != null)
			{
				modItem.PickAmmo(weapon, player, ref type, ref speed, ref damage, ref knockback);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookPickAmmo.Enumerate(ammo))
			{
				globalItem.PickAmmo(weapon, ammo, player, ref type, ref speed, ref damage, ref knockback);
			}
		}

		/// <summary>
		/// Calls each <see cref="M:Terraria.ModLoader.GlobalItem.CanChooseAmmo(Terraria.Item,Terraria.Item,Terraria.Player)" /> hook for the weapon, and each <see cref="M:Terraria.ModLoader.GlobalItem.CanBeChosenAsAmmo(Terraria.Item,Terraria.Item,Terraria.Player)" /> hook for the ammo,<br></br>
		/// then each corresponding hook in <see cref="T:Terraria.ModLoader.ModItem" /> if applicable for the weapon and/or ammo, until one of them returns a concrete false value.<br></br>
		/// If all of them fail to do this, returns either true (if one returned true prior) or <c>ammo.ammo == weapon.useAmmo</c>.
		/// </summary>
		// Token: 0x06001E74 RID: 7796 RVA: 0x004D69E0 File Offset: 0x004D4BE0
		public static bool CanChooseAmmo(Item weapon, Item ammo, Player player)
		{
			bool? result = null;
			bool? flag;
			foreach (GlobalItem globalItem in ItemLoader.HookCanChooseAmmo.Enumerate(weapon))
			{
				bool? r = globalItem.CanChooseAmmo(weapon, ammo, player);
				if (!(r ?? true))
				{
					return false;
				}
				flag = result;
				if (flag == null)
				{
					result = r;
				}
			}
			foreach (GlobalItem globalItem2 in ItemLoader.HookCanBeChosenAsAmmo.Enumerate(ammo))
			{
				bool? r2 = globalItem2.CanBeChosenAsAmmo(ammo, weapon, player);
				if (!(r2 ?? true))
				{
					return false;
				}
				flag = result;
				if (flag == null)
				{
					result = r2;
				}
			}
			if (weapon.ModItem != null)
			{
				bool? r3 = weapon.ModItem.CanChooseAmmo(ammo, player);
				if (!(r3 ?? true))
				{
					return false;
				}
				flag = result;
				if (flag == null)
				{
					result = r3;
				}
			}
			if (ammo.ModItem != null)
			{
				bool? r4 = ammo.ModItem.CanBeChosenAsAmmo(weapon, player);
				if (!(r4 ?? true))
				{
					return false;
				}
				flag = result;
				if (flag == null)
				{
					result = r4;
				}
			}
			flag = result;
			if (flag == null)
			{
				return ammo.ammo == weapon.useAmmo;
			}
			return flag.GetValueOrDefault();
		}

		/// <summary>
		/// Calls each <see cref="M:Terraria.ModLoader.GlobalItem.CanConsumeAmmo(Terraria.Item,Terraria.Item,Terraria.Player)" /> hook for the weapon, and each <see cref="M:Terraria.ModLoader.GlobalItem.CanBeConsumedAsAmmo(Terraria.Item,Terraria.Item,Terraria.Player)" /> hook for the ammo,<br></br>
		/// then each corresponding hook in <see cref="T:Terraria.ModLoader.ModItem" /> if applicable for the weapon and/or ammo, until one of them returns a concrete false value.<br></br>
		/// If all of them fail to do this, returns true.
		/// </summary>
		// Token: 0x06001E75 RID: 7797 RVA: 0x004D6B30 File Offset: 0x004D4D30
		public static bool CanConsumeAmmo(Item weapon, Item ammo, Player player)
		{
			EntityGlobalsEnumerator<GlobalItem> enumerator = ItemLoader.HookCanConsumeAmmo.Enumerate(weapon).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.CanConsumeAmmo(weapon, ammo, player))
				{
					return false;
				}
			}
			enumerator = ItemLoader.HookCanBeConsumedAsAmmo.Enumerate(ammo).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.CanBeConsumedAsAmmo(ammo, weapon, player))
				{
					return false;
				}
			}
			return (weapon.ModItem == null || weapon.ModItem.CanConsumeAmmo(ammo, player)) && (ammo.ModItem == null || ammo.ModItem.CanBeConsumedAsAmmo(weapon, player));
		}

		/// <summary>
		/// Calls <see cref="M:Terraria.ModLoader.ModItem.OnConsumeAmmo(Terraria.Item,Terraria.Player)" /> for the weapon, <see cref="M:Terraria.ModLoader.ModItem.OnConsumedAsAmmo(Terraria.Item,Terraria.Player)" /> for the ammo,
		/// then each corresponding hook for the weapon and ammo.
		/// </summary>
		// Token: 0x06001E76 RID: 7798 RVA: 0x004D6BD4 File Offset: 0x004D4DD4
		public static void OnConsumeAmmo(Item weapon, Item ammo, Player player)
		{
			if (weapon.IsAir)
			{
				return;
			}
			ModItem modItem = weapon.ModItem;
			if (modItem != null)
			{
				modItem.OnConsumeAmmo(ammo, player);
			}
			ModItem modItem2 = ammo.ModItem;
			if (modItem2 != null)
			{
				modItem2.OnConsumedAsAmmo(weapon, player);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookOnConsumeAmmo.Enumerate(weapon))
			{
				globalItem.OnConsumeAmmo(weapon, ammo, player);
			}
			foreach (GlobalItem globalItem2 in ItemLoader.HookOnConsumedAsAmmo.Enumerate(ammo))
			{
				globalItem2.OnConsumedAsAmmo(ammo, weapon, player);
			}
		}

		/// <summary>
		/// Calls each GlobalItem.CanShoot hook, then ModItem.CanShoot, until one of them returns false. If all of them return true, returns true.
		/// </summary>
		// Token: 0x06001E77 RID: 7799 RVA: 0x004D6C6C File Offset: 0x004D4E6C
		public static bool CanShoot(Item item, Player player)
		{
			EntityGlobalsEnumerator<GlobalItem> enumerator = ItemLoader.HookCanShoot.Enumerate(item).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.CanShoot(item, player))
				{
					return false;
				}
			}
			ModItem modItem = item.ModItem;
			return modItem == null || modItem.CanShoot(player);
		}

		/// <summary>
		/// Calls ModItem.ModifyShootStats, then each GlobalItem.ModifyShootStats hook.
		/// </summary>
		// Token: 0x06001E78 RID: 7800 RVA: 0x004D6CBC File Offset: 0x004D4EBC
		public static void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookModifyShootStats.Enumerate(item))
			{
				globalItem.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);
			}
		}

		/// <summary>
		/// Calls each GlobalItem.Shoot hook then, if none of them returns false, calls the ModItem.Shoot hook and returns its value.
		/// </summary>
		// Token: 0x06001E79 RID: 7801 RVA: 0x004D6D18 File Offset: 0x004D4F18
		public static bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback, bool defaultResult = true)
		{
			foreach (GlobalItem g in ItemLoader.HookShoot.Enumerate(item))
			{
				defaultResult &= g.Shoot(item, player, source, position, velocity, type, damage, knockback);
			}
			if (defaultResult)
			{
				ModItem modItem = item.ModItem;
				return modItem == null || modItem.Shoot(player, source, position, velocity, type, damage, knockback);
			}
			return false;
		}

		/// <summary>
		/// Calls ModItem.UseItemHitbox, then all GlobalItem.UseItemHitbox hooks.
		/// </summary>
		// Token: 0x06001E7A RID: 7802 RVA: 0x004D6D88 File Offset: 0x004D4F88
		public static void UseItemHitbox(Item item, Player player, ref Rectangle hitbox, ref bool noHitbox)
		{
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.UseItemHitbox(player, ref hitbox, ref noHitbox);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookUseItemHitbox.Enumerate(item))
			{
				globalItem.UseItemHitbox(item, player, ref hitbox, ref noHitbox);
			}
		}

		/// <summary>
		/// Calls ModItem.MeleeEffects and all GlobalItem.MeleeEffects hooks.
		/// </summary>
		// Token: 0x06001E7B RID: 7803 RVA: 0x004D6DD8 File Offset: 0x004D4FD8
		public static void MeleeEffects(Item item, Player player, Rectangle hitbox)
		{
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.MeleeEffects(player, hitbox);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookMeleeEffects.Enumerate(item))
			{
				globalItem.MeleeEffects(item, player, hitbox);
			}
		}

		/// <summary>
		/// Gathers the results of all <see cref="M:Terraria.ModLoader.GlobalItem.CanCatchNPC(Terraria.Item,Terraria.NPC,Terraria.Player)" /> hooks, then the <see cref="M:Terraria.ModLoader.ModItem.CanCatchNPC(Terraria.NPC,Terraria.Player)" /> hook if applicable.<br></br>
		/// If any of them returns false, this returns false.<br></br>
		/// Otherwise, if any of them returns true, then this returns true.<br></br>
		/// If all of them return null, this returns null.<br></br>
		/// </summary>
		// Token: 0x06001E7C RID: 7804 RVA: 0x004D6E28 File Offset: 0x004D5028
		public static bool? CanCatchNPC(Item item, NPC target, Player player)
		{
			bool? canCatchOverall = null;
			foreach (GlobalItem globalItem in ItemLoader.HookCanCatchNPC.Enumerate(item))
			{
				bool? canCatchFromGlobalItem = globalItem.CanCatchNPC(item, target, player);
				if (canCatchFromGlobalItem != null)
				{
					if (!canCatchFromGlobalItem.Value)
					{
						return new bool?(false);
					}
					canCatchOverall = new bool?(true);
				}
			}
			if (item.ModItem != null)
			{
				bool? canCatchAsModItem = item.ModItem.CanCatchNPC(target, player);
				if (canCatchAsModItem != null)
				{
					if (!canCatchAsModItem.Value)
					{
						return new bool?(false);
					}
					canCatchOverall = new bool?(true);
				}
			}
			return canCatchOverall;
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x004D6EC8 File Offset: 0x004D50C8
		public static void OnCatchNPC(Item item, NPC npc, Player player, bool failed)
		{
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.OnCatchNPC(npc, player, failed);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookOnCatchNPC.Enumerate(item))
			{
				globalItem.OnCatchNPC(item, npc, player, failed);
			}
		}

		/// <summary>
		/// Calls <see cref="M:Terraria.ModLoader.ModItem.ModifyItemScale(Terraria.Player,System.Single@)" /> if applicable, then all applicable <see cref="M:Terraria.ModLoader.GlobalItem.ModifyItemScale(Terraria.Item,Terraria.Player,System.Single@)" /> instances.
		/// </summary>
		// Token: 0x06001E7E RID: 7806 RVA: 0x004D6F18 File Offset: 0x004D5118
		public static void ModifyItemScale(Item item, Player player, ref float scale)
		{
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.ModifyItemScale(player, ref scale);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookModifyItemScale.Enumerate(item))
			{
				globalItem.ModifyItemScale(item, player, ref scale);
			}
		}

		/// <summary>
		/// Gathers the results of ModItem.CanHitNPC and all GlobalItem.CanHitNPC hooks.
		/// If any of them returns false, this returns false.
		/// Otherwise, if any of them returns true then this returns true.
		/// If all of them return null, this returns null.
		/// </summary>
		// Token: 0x06001E7F RID: 7807 RVA: 0x004D6F68 File Offset: 0x004D5168
		public static bool? CanHitNPC(Item item, Player player, NPC target)
		{
			bool? flag = null;
			foreach (GlobalItem globalItem in ItemLoader.HookCanHitNPC.Enumerate(item))
			{
				bool? canHit = globalItem.CanHitNPC(item, player, target);
				if (canHit != null)
				{
					if (!canHit.Value)
					{
						return new bool?(false);
					}
					flag = new bool?(true);
				}
			}
			if (item.ModItem != null)
			{
				bool? canHit2 = item.ModItem.CanHitNPC(player, target);
				if (canHit2 != null)
				{
					if (!canHit2.Value)
					{
						return new bool?(false);
					}
					flag = new bool?(true);
				}
			}
			return flag;
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x004D7008 File Offset: 0x004D5208
		public static bool? CanMeleeAttackCollideWithNPC(Item item, Rectangle meleeAttackHitbox, Player player, NPC target)
		{
			bool? flag = null;
			foreach (GlobalItem globalItem in ItemLoader.HookCanCollideNPC.Enumerate(item))
			{
				bool? canCollide = globalItem.CanMeleeAttackCollideWithNPC(item, meleeAttackHitbox, player, target);
				if (canCollide != null)
				{
					if (!canCollide.Value)
					{
						return new bool?(false);
					}
					flag = new bool?(true);
				}
			}
			if (item.ModItem != null)
			{
				bool? canHit = item.ModItem.CanMeleeAttackCollideWithNPC(meleeAttackHitbox, player, target);
				if (canHit != null)
				{
					if (!canHit.Value)
					{
						return new bool?(false);
					}
					flag = new bool?(true);
				}
			}
			return flag;
		}

		/// <summary>
		/// Calls ModItem.ModifyHitNPC, then all GlobalItem.ModifyHitNPC hooks.
		/// </summary>
		// Token: 0x06001E81 RID: 7809 RVA: 0x004D70A8 File Offset: 0x004D52A8
		public static void ModifyHitNPC(Item item, Player player, NPC target, ref NPC.HitModifiers modifiers)
		{
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.ModifyHitNPC(player, target, ref modifiers);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookModifyHitNPC.Enumerate(item))
			{
				globalItem.ModifyHitNPC(item, player, target, ref modifiers);
			}
		}

		/// <summary>
		/// Calls ModItem.OnHitNPC and all GlobalItem.OnHitNPC hooks.
		/// </summary>
		// Token: 0x06001E82 RID: 7810 RVA: 0x004D70F8 File Offset: 0x004D52F8
		public static void OnHitNPC(Item item, Player player, NPC target, in NPC.HitInfo hit, int damageDone)
		{
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.OnHitNPC(player, target, hit, damageDone);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookOnHitNPC.Enumerate(item))
			{
				globalItem.OnHitNPC(item, player, target, hit, damageDone);
			}
		}

		/// <summary>
		/// Calls all GlobalItem.CanHitPvp hooks, then ModItem.CanHitPvp, until one of them returns false.
		/// If all of them return true, this returns true.
		/// </summary>
		// Token: 0x06001E83 RID: 7811 RVA: 0x004D7158 File Offset: 0x004D5358
		public static bool CanHitPvp(Item item, Player player, Player target)
		{
			EntityGlobalsEnumerator<GlobalItem> enumerator = ItemLoader.HookCanHitPvp.Enumerate(item).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.CanHitPvp(item, player, target))
				{
					return false;
				}
			}
			return item.ModItem == null || item.ModItem.CanHitPvp(player, target);
		}

		/// <summary>
		/// Calls ModItem.ModifyHitPvp, then all GlobalItem.ModifyHitPvp hooks.
		/// </summary>
		// Token: 0x06001E84 RID: 7812 RVA: 0x004D71B0 File Offset: 0x004D53B0
		public static void ModifyHitPvp(Item item, Player player, Player target, ref Player.HurtModifiers modifiers)
		{
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.ModifyHitPvp(player, target, ref modifiers);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookModifyHitPvp.Enumerate(item))
			{
				globalItem.ModifyHitPvp(item, player, target, ref modifiers);
			}
		}

		/// <summary>
		/// Calls ModItem.OnHitPvp and all GlobalItem.OnHitPvp hooks. <br />
		/// Called on local, server and remote clients. <br />
		/// </summary>
		// Token: 0x06001E85 RID: 7813 RVA: 0x004D7200 File Offset: 0x004D5400
		public static void OnHitPvp(Item item, Player player, Player target, Player.HurtInfo hurtInfo)
		{
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.OnHitPvp(player, target, hurtInfo);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookOnHitPvp.Enumerate(item))
			{
				globalItem.OnHitPvp(item, player, target, hurtInfo);
			}
		}

		/// <summary>
		/// Returns false if any of ModItem.UseItem or GlobalItem.UseItem return false.
		/// Returns true if anything returns true without returning false.
		/// Returns null by default.
		/// Does not fail fast (calls every hook)
		/// </summary>
		// Token: 0x06001E86 RID: 7814 RVA: 0x004D7250 File Offset: 0x004D5450
		public static bool? UseItem(Item item, Player player)
		{
			if (item.IsAir)
			{
				return null;
			}
			bool? result = null;
			bool? result2;
			foreach (GlobalItem globalItem in ItemLoader.HookUseItem.Enumerate(item))
			{
				bool? useItem = globalItem.UseItem(item, player);
				if (useItem != null)
				{
					result2 = result;
					bool flag = false;
					if (!(result2.GetValueOrDefault() == flag & result2 != null))
					{
						result = new bool?(useItem.Value);
					}
				}
			}
			ModItem modItem = item.ModItem;
			bool? modItemResult = (modItem != null) ? modItem.UseItem(player) : null;
			result2 = result;
			if (result2 == null)
			{
				return modItemResult;
			}
			return result2;
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x004D7304 File Offset: 0x004D5504
		public static void UseAnimation(Item item, Player player)
		{
			foreach (GlobalItem globalItem in ItemLoader.HookUseAnimation.Enumerate(item))
			{
				globalItem.Instance(item).UseAnimation(item, player);
			}
			ModItem modItem = item.ModItem;
			if (modItem == null)
			{
				return;
			}
			modItem.UseAnimation(player);
		}

		/// <summary>
		/// If ModItem.ConsumeItem or any of the GlobalItem.ConsumeItem hooks returns false, sets consume to false.
		/// </summary>
		// Token: 0x06001E88 RID: 7816 RVA: 0x004D7358 File Offset: 0x004D5558
		public static bool ConsumeItem(Item item, Player player)
		{
			if (item.IsAir)
			{
				return true;
			}
			if (item.ModItem != null && !item.ModItem.ConsumeItem(player))
			{
				return false;
			}
			EntityGlobalsEnumerator<GlobalItem> enumerator = ItemLoader.HookConsumeItem.Enumerate(item).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.ConsumeItem(item, player))
				{
					return false;
				}
			}
			ItemLoader.OnConsumeItem(item, player);
			return true;
		}

		/// <summary>
		/// Calls ModItem.OnConsumeItem and all GlobalItem.OnConsumeItem hooks.
		/// </summary>
		// Token: 0x06001E89 RID: 7817 RVA: 0x004D73C0 File Offset: 0x004D55C0
		public static void OnConsumeItem(Item item, Player player)
		{
			if (item.IsAir)
			{
				return;
			}
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.OnConsumeItem(player);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookOnConsumeItem.Enumerate(item))
			{
				globalItem.OnConsumeItem(item, player);
			}
		}

		/// <summary>
		/// Calls ModItem.UseItemFrame, then all GlobalItem.UseItemFrame hooks, until one of them returns true. Returns whether any of the hooks returned true.
		/// </summary>
		// Token: 0x06001E8A RID: 7818 RVA: 0x004D7418 File Offset: 0x004D5618
		public static void UseItemFrame(Item item, Player player)
		{
			if (item.IsAir)
			{
				return;
			}
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.UseItemFrame(player);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookUseItemFrame.Enumerate(item))
			{
				globalItem.UseItemFrame(item, player);
			}
		}

		/// <summary>
		/// Calls ModItem.HoldItemFrame, then all GlobalItem.HoldItemFrame hooks, until one of them returns true. Returns whether any of the hooks returned true.
		/// </summary>
		// Token: 0x06001E8B RID: 7819 RVA: 0x004D7470 File Offset: 0x004D5670
		public static void HoldItemFrame(Item item, Player player)
		{
			if (item.IsAir)
			{
				return;
			}
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.HoldItemFrame(player);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookHoldItemFrame.Enumerate(item))
			{
				globalItem.HoldItemFrame(item, player);
			}
		}

		/// <summary>
		/// Calls ModItem.AltFunctionUse, then all GlobalItem.AltFunctionUse hooks, until one of them returns true. Returns whether any of the hooks returned true.
		/// </summary>
		// Token: 0x06001E8C RID: 7820 RVA: 0x004D74C8 File Offset: 0x004D56C8
		public static bool AltFunctionUse(Item item, Player player)
		{
			if (item.IsAir)
			{
				return false;
			}
			if (item.ModItem != null && item.ModItem.AltFunctionUse(player))
			{
				return true;
			}
			EntityGlobalsEnumerator<GlobalItem> enumerator = ItemLoader.HookAltFunctionUse.Enumerate(item).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.AltFunctionUse(item, player))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Calls ModItem.UpdateInventory and all GlobalItem.UpdateInventory hooks.
		/// </summary>
		// Token: 0x06001E8D RID: 7821 RVA: 0x004D752C File Offset: 0x004D572C
		public static void UpdateInventory(Item item, Player player)
		{
			if (item.IsAir)
			{
				return;
			}
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.UpdateInventory(player);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookUpdateInventory.Enumerate(item))
			{
				globalItem.UpdateInventory(item, player);
			}
		}

		/// <summary>
		/// Calls ModItem.UpdateInfoAccessory and all GlobalItem.UpdateInfoAccessory hooks.
		/// </summary>
		// Token: 0x06001E8E RID: 7822 RVA: 0x004D7584 File Offset: 0x004D5784
		public static void UpdateInfoAccessory(Item item, Player player)
		{
			if (item.IsAir)
			{
				return;
			}
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.UpdateInfoAccessory(player);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookUpdateInfoAccessory.Enumerate(item))
			{
				globalItem.UpdateInfoAccessory(item, player);
			}
		}

		/// <summary>
		/// Hook at the end of Player.VanillaUpdateEquip can be called to apply additional code related to accessory slots for a particular item
		/// </summary>
		// Token: 0x06001E8F RID: 7823 RVA: 0x004D75DC File Offset: 0x004D57DC
		public static void UpdateEquip(Item item, Player player)
		{
			if (item.IsAir)
			{
				return;
			}
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.UpdateEquip(player);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookUpdateEquip.Enumerate(item))
			{
				globalItem.UpdateEquip(item, player);
			}
		}

		/// <summary>
		/// Hook at the end of Player.ApplyEquipFunctional can be called to apply additional code related to accessory slots for a particular item.
		/// </summary>
		// Token: 0x06001E90 RID: 7824 RVA: 0x004D7634 File Offset: 0x004D5834
		public static void UpdateAccessory(Item item, Player player, bool hideVisual)
		{
			if (item.IsAir)
			{
				return;
			}
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.UpdateAccessory(player, hideVisual);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookUpdateAccessory.Enumerate(item))
			{
				globalItem.UpdateAccessory(item, player, hideVisual);
			}
		}

		/// <summary>
		/// Hook at the end of Player.ApplyEquipVanity can be called to apply additional code related to accessory slots for a particular item
		/// </summary>
		// Token: 0x06001E91 RID: 7825 RVA: 0x004D768C File Offset: 0x004D588C
		public static void UpdateVanity(Item item, Player player)
		{
			if (item.IsAir)
			{
				return;
			}
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.UpdateVanity(player);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookUpdateVanity.Enumerate(item))
			{
				globalItem.UpdateVanity(item, player);
			}
		}

		/// <summary>
		/// Hook at the end of Player.UpdateVisibleAccessory that can be called to set flags related to player drawing.
		/// </summary>
		// Token: 0x06001E92 RID: 7826 RVA: 0x004D76E4 File Offset: 0x004D58E4
		public static void UpdateVisibleAccessory(Item item, Player player, bool hideVisual)
		{
			if (item.IsAir)
			{
				return;
			}
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.UpdateVisibleAccessory(player, hideVisual);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookUpdateVisibleAccessory.Enumerate(item))
			{
				globalItem.UpdateVisibleAccessory(item, player, hideVisual);
			}
		}

		// Token: 0x06001E93 RID: 7827 RVA: 0x004D773C File Offset: 0x004D593C
		public static void UpdateItemDye(Item item, Player player, int dye, bool hideVisual)
		{
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.UpdateItemDye(player, dye, hideVisual);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookUpdateItemDye.Enumerate(item))
			{
				globalItem.UpdateItemDye(item, player, dye, hideVisual);
			}
		}

		/// <summary>
		/// If the head's ModItem.IsArmorSet returns true, calls the head's ModItem.UpdateArmorSet. This is then repeated for the body, then the legs. Then for each GlobalItem, if GlobalItem.IsArmorSet returns a non-empty string, calls GlobalItem.UpdateArmorSet with that string.
		/// </summary>
		// Token: 0x06001E94 RID: 7828 RVA: 0x004D778C File Offset: 0x004D598C
		public unsafe static void UpdateArmorSet(Player player, Item head, Item body, Item legs)
		{
			if (head.ModItem != null && head.ModItem.IsArmorSet(head, body, legs))
			{
				head.ModItem.UpdateArmorSet(player);
			}
			if (body.ModItem != null && body.ModItem.IsArmorSet(head, body, legs))
			{
				body.ModItem.UpdateArmorSet(player);
			}
			if (legs.ModItem != null && legs.ModItem.IsArmorSet(head, body, legs))
			{
				legs.ModItem.UpdateArmorSet(player);
			}
			ReadOnlySpan<GlobalItem> readOnlySpan = ItemLoader.HookUpdateArmorSet.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				GlobalItem g = *readOnlySpan[i];
				string set = g.IsArmorSet(head, body, legs);
				if (!string.IsNullOrEmpty(set))
				{
					g.UpdateArmorSet(player, set);
				}
			}
		}

		/// <summary>
		/// If the player's head texture's IsVanitySet returns true, calls the equipment texture's PreUpdateVanitySet. This is then repeated for the player's body, then the legs. Then for each GlobalItem, if GlobalItem.IsVanitySet returns a non-empty string, calls GlobalItem.PreUpdateVanitySet, using player.head, player.body, and player.legs.
		/// </summary>
		// Token: 0x06001E95 RID: 7829 RVA: 0x004D7848 File Offset: 0x004D5A48
		public unsafe static void PreUpdateVanitySet(Player player)
		{
			EquipTexture headTexture = EquipLoader.GetEquipTexture(EquipType.Head, player.head);
			EquipTexture bodyTexture = EquipLoader.GetEquipTexture(EquipType.Body, player.body);
			EquipTexture legTexture = EquipLoader.GetEquipTexture(EquipType.Legs, player.legs);
			if (headTexture != null && headTexture.IsVanitySet(player.head, player.body, player.legs))
			{
				headTexture.PreUpdateVanitySet(player);
			}
			if (bodyTexture != null && bodyTexture.IsVanitySet(player.head, player.body, player.legs))
			{
				bodyTexture.PreUpdateVanitySet(player);
			}
			if (legTexture != null && legTexture.IsVanitySet(player.head, player.body, player.legs))
			{
				legTexture.PreUpdateVanitySet(player);
			}
			ReadOnlySpan<GlobalItem> readOnlySpan = ItemLoader.HookPreUpdateVanitySet.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				GlobalItem g = *readOnlySpan[i];
				string set = g.IsVanitySet(player.head, player.body, player.legs);
				if (!string.IsNullOrEmpty(set))
				{
					g.PreUpdateVanitySet(player, set);
				}
			}
		}

		/// <summary>
		/// If the player's head texture's IsVanitySet returns true, calls the equipment texture's UpdateVanitySet. This is then repeated for the player's body, then the legs. Then for each GlobalItem, if GlobalItem.IsVanitySet returns a non-empty string, calls GlobalItem.UpdateVanitySet, using player.head, player.body, and player.legs.
		/// </summary>
		// Token: 0x06001E96 RID: 7830 RVA: 0x004D7944 File Offset: 0x004D5B44
		public unsafe static void UpdateVanitySet(Player player)
		{
			EquipTexture headTexture = EquipLoader.GetEquipTexture(EquipType.Head, player.head);
			EquipTexture bodyTexture = EquipLoader.GetEquipTexture(EquipType.Body, player.body);
			EquipTexture legTexture = EquipLoader.GetEquipTexture(EquipType.Legs, player.legs);
			if (headTexture != null && headTexture.IsVanitySet(player.head, player.body, player.legs))
			{
				headTexture.UpdateVanitySet(player);
			}
			if (bodyTexture != null && bodyTexture.IsVanitySet(player.head, player.body, player.legs))
			{
				bodyTexture.UpdateVanitySet(player);
			}
			if (legTexture != null && legTexture.IsVanitySet(player.head, player.body, player.legs))
			{
				legTexture.UpdateVanitySet(player);
			}
			ReadOnlySpan<GlobalItem> readOnlySpan = ItemLoader.HookUpdateVanitySet.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				GlobalItem g = *readOnlySpan[i];
				string set = g.IsVanitySet(player.head, player.body, player.legs);
				if (!string.IsNullOrEmpty(set))
				{
					g.UpdateVanitySet(player, set);
				}
			}
		}

		/// <summary>
		/// If the player's head texture's IsVanitySet returns true, calls the equipment texture's ArmorSetShadows. This is then repeated for the player's body, then the legs. Then for each GlobalItem, if GlobalItem.IsVanitySet returns a non-empty string, calls GlobalItem.ArmorSetShadows, using player.head, player.body, and player.legs.
		/// </summary>
		// Token: 0x06001E97 RID: 7831 RVA: 0x004D7A40 File Offset: 0x004D5C40
		public unsafe static void ArmorSetShadows(Player player)
		{
			EquipTexture headTexture = EquipLoader.GetEquipTexture(EquipType.Head, player.head);
			EquipTexture bodyTexture = EquipLoader.GetEquipTexture(EquipType.Body, player.body);
			EquipTexture legTexture = EquipLoader.GetEquipTexture(EquipType.Legs, player.legs);
			if (headTexture != null && headTexture.IsVanitySet(player.head, player.body, player.legs))
			{
				headTexture.ArmorSetShadows(player);
			}
			if (bodyTexture != null && bodyTexture.IsVanitySet(player.head, player.body, player.legs))
			{
				bodyTexture.ArmorSetShadows(player);
			}
			if (legTexture != null && legTexture.IsVanitySet(player.head, player.body, player.legs))
			{
				legTexture.ArmorSetShadows(player);
			}
			ReadOnlySpan<GlobalItem> readOnlySpan = ItemLoader.HookArmorSetShadows.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				GlobalItem g = *readOnlySpan[i];
				string set = g.IsVanitySet(player.head, player.body, player.legs);
				if (!string.IsNullOrEmpty(set))
				{
					g.ArmorSetShadows(player, set);
				}
			}
		}

		/// <summary>
		/// Calls EquipTexture.SetMatch, then all GlobalItem.SetMatch hooks.
		/// </summary>
		// Token: 0x06001E98 RID: 7832 RVA: 0x004D7B3C File Offset: 0x004D5D3C
		public unsafe static void SetMatch(int armorSlot, int type, bool male, ref int equipSlot, ref bool robes)
		{
			EquipTexture equipTexture = EquipLoader.GetEquipTexture((EquipType)armorSlot, type);
			if (equipTexture != null)
			{
				equipTexture.SetMatch(male, ref equipSlot, ref robes);
			}
			ReadOnlySpan<GlobalItem> readOnlySpan = ItemLoader.HookSetMatch.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->SetMatch(armorSlot, type, male, ref equipSlot, ref robes);
			}
		}

		/// <summary>
		/// Calls ModItem.CanRightClick, then all GlobalItem.CanRightClick hooks, until one of the returns true.
		/// Also returns true if ItemID.Sets.OpenableBag
		/// </summary>
		// Token: 0x06001E99 RID: 7833 RVA: 0x004D7B90 File Offset: 0x004D5D90
		public static bool CanRightClick(Item item)
		{
			if (item.IsAir)
			{
				return false;
			}
			if (ItemID.Sets.OpenableBag[item.type])
			{
				return true;
			}
			if (item.ModItem != null && item.ModItem.CanRightClick())
			{
				return true;
			}
			EntityGlobalsEnumerator<GlobalItem> enumerator = ItemLoader.HookCanRightClick.Enumerate(item).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.CanRightClick(item))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 1. Call ModItem.RightClick
		/// 2. Calls all GlobalItem.RightClick hooks
		/// 3. Call ItemLoader.ConsumeItem, and if it returns true, decrements the item's stack
		/// 4. Sets the item's type to 0 if the item's stack is 0
		/// 5. Plays the item-grabbing sound
		/// 6. Sets Main.stackSplit to 30
		/// 7. Sets Main.mouseRightRelease to false
		/// 8. Calls Recipe.FindRecipes.
		/// </summary>
		// Token: 0x06001E9A RID: 7834 RVA: 0x004D7C00 File Offset: 0x004D5E00
		public static void RightClick(Item item, Player player)
		{
			ItemLoader.RightClickCallHooks(item, player);
			if (ItemLoader.ConsumeItem(item, player))
			{
				int num = item.stack - 1;
				item.stack = num;
				if (num == 0)
				{
					item.SetDefaults(0);
				}
			}
			SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
			Main.stackSplit = 30;
			Main.mouseRightRelease = false;
			Recipe.FindRecipes(false);
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x004D7C60 File Offset: 0x004D5E60
		internal static void RightClickCallHooks(Item item, Player player)
		{
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.RightClick(player);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookRightClick.Enumerate(item))
			{
				globalItem.RightClick(item, player);
			}
		}

		/// <summary>
		/// Calls each GlobalItem.ModifyItemLoot hooks.
		/// </summary>
		// Token: 0x06001E9C RID: 7836 RVA: 0x004D7CAC File Offset: 0x004D5EAC
		public static void ModifyItemLoot(Item item, ItemLoot itemLoot)
		{
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.ModifyItemLoot(itemLoot);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookModifyItemLoot.Enumerate(item))
			{
				globalItem.ModifyItemLoot(item, itemLoot);
			}
		}

		/// <summary>
		/// Returns false if item prefixes don't match. Then calls all GlobalItem.CanStack hooks until one returns false then ModItem.CanStack. Returns whether any of the hooks returned false.
		/// </summary>
		/// <param name="destination">The item instance that <paramref name="source" /> will attempt to stack onto</param>
		/// <param name="source">The item instance being stacked onto <paramref name="destination" /></param>
		/// <returns>Whether or not the items are allowed to stack</returns>
		// Token: 0x06001E9D RID: 7837 RVA: 0x004D7CF8 File Offset: 0x004D5EF8
		public static bool CanStack(Item destination, Item source)
		{
			if (destination.prefix != source.prefix)
			{
				return false;
			}
			EntityGlobalsEnumerator<GlobalItem> enumerator = ItemLoader.HookCanStack.Enumerate(destination).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.CanStack(destination, source))
				{
					return false;
				}
			}
			ModItem modItem = destination.ModItem;
			return modItem == null || modItem.CanStack(source);
		}

		/// <summary>
		/// Calls all GlobalItem.CanStackInWorld hooks until one returns false then ModItem.CanStackInWorld. Returns whether any of the hooks returned false.
		/// </summary>
		/// <param name="destination">The item instance that <paramref name="source" /> will attempt to stack onto</param>
		/// <param name="source">The item instance being stacked onto <paramref name="destination" /></param>
		/// <returns>Whether or not the items are allowed to stack</returns>
		// Token: 0x06001E9E RID: 7838 RVA: 0x004D7D58 File Offset: 0x004D5F58
		public static bool CanStackInWorld(Item destination, Item source)
		{
			EntityGlobalsEnumerator<GlobalItem> enumerator = ItemLoader.HookCanStackInWorld.Enumerate(destination).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.CanStackInWorld(destination, source))
				{
					return false;
				}
			}
			ModItem modItem = destination.ModItem;
			return modItem == null || modItem.CanStackInWorld(source);
		}

		/// <summary>
		/// Stacks <paramref name="source" /> onto <paramref name="destination" /> if CanStack permits the transfer
		/// </summary>
		/// <param name="destination">The item instance that <paramref name="source" /> will attempt to stack onto</param>
		/// <param name="source">The item instance being stacked onto <paramref name="destination" /></param>
		/// <param name="numTransferred">The quantity of <paramref name="source" /> that was transferred to <paramref name="destination" /></param>
		/// <param name="infiniteSource">If true, <paramref name="source" />.stack will not be decreased</param>
		/// <returns>Whether or not the items were allowed to stack</returns>
		// Token: 0x06001E9F RID: 7839 RVA: 0x004D7DA8 File Offset: 0x004D5FA8
		public static bool TryStackItems(Item destination, Item source, out int numTransferred, bool infiniteSource = false)
		{
			numTransferred = 0;
			if (!ItemLoader.CanStack(destination, source))
			{
				return false;
			}
			ItemLoader.StackItems(destination, source, out numTransferred, infiniteSource, null);
			return true;
		}

		/// <summary>
		/// Stacks <paramref name="destination" /> onto <paramref name="source" /><br />
		/// This method should not be called unless <see cref="M:Terraria.ModLoader.ItemLoader.CanStack(Terraria.Item,Terraria.Item)" /> returns true.  See: <see cref="M:Terraria.ModLoader.ItemLoader.TryStackItems(Terraria.Item,Terraria.Item,System.Int32@,System.Boolean)" />
		/// </summary>
		/// <param name="destination">The item instance that <paramref name="source" /> will attempt to stack onto</param>
		/// <param name="source">The item instance being stacked onto <paramref name="destination" /></param>
		/// <param name="numTransferred">The quantity of <paramref name="source" /> that was transferred to <paramref name="destination" /></param>
		/// <param name="infiniteSource">If true, <paramref name="source" />.stack will not be decreased</param>
		/// <param name="numToTransfer">
		/// An optional argument used to specify the quantity of items to transfer from <paramref name="source" /> to <paramref name="destination" />.<br />
		/// By default, as many items as possible will be transferred.  That is, either source will be empty, or destination will be full (maxStack)
		/// </param>
		// Token: 0x06001EA0 RID: 7840 RVA: 0x004D7DD8 File Offset: 0x004D5FD8
		public static void StackItems(Item destination, Item source, out int numTransferred, bool infiniteSource = false, int? numToTransfer = null)
		{
			numTransferred = (numToTransfer ?? Math.Min(source.stack, destination.maxStack - destination.stack));
			ItemLoader.OnStack(destination, source, numTransferred);
			bool isSplittingToHand = numTransferred < source.stack && destination == Main.mouseItem;
			if (source.favorited && !isSplittingToHand)
			{
				destination.favorited = true;
				source.favorited = false;
			}
			int? shopCustomPrice = destination.shopCustomPrice;
			int? shopCustomPrice2 = source.shopCustomPrice;
			if (!(shopCustomPrice.GetValueOrDefault() == shopCustomPrice2.GetValueOrDefault() & shopCustomPrice != null == (shopCustomPrice2 != null)))
			{
				destination.shopCustomPrice = null;
			}
			destination.stack += numTransferred;
			if (!infiniteSource)
			{
				source.stack -= numTransferred;
			}
		}

		/// <summary>
		/// Calls the GlobalItem.OnStack hooks in <paramref name="destination" />, then the ModItem.OnStack hook in <paramref name="destination" /><br />
		/// OnStack is called before the items are transferred from <paramref name="source" /> to <paramref name="destination" />
		/// </summary>
		/// <param name="destination">The item instance that <paramref name="source" /> will attempt to stack onto</param>
		/// <param name="source">The item instance being stacked onto <paramref name="destination" /></param>
		/// <param name="numToTransfer">The quantity of <paramref name="source" /> that will be transferred to <paramref name="destination" /></param>
		// Token: 0x06001EA1 RID: 7841 RVA: 0x004D7EAC File Offset: 0x004D60AC
		public static void OnStack(Item destination, Item source, int numToTransfer)
		{
			foreach (GlobalItem globalItem in ItemLoader.HookOnStack.Enumerate(destination))
			{
				globalItem.OnStack(destination, source, numToTransfer);
			}
			ModItem modItem = destination.ModItem;
			if (modItem == null)
			{
				return;
			}
			modItem.OnStack(source, numToTransfer);
		}

		/// <summary>
		/// Extract up to <paramref name="limit" /> items from <paramref name="source" />. If some items remain, <see cref="M:Terraria.ModLoader.ItemLoader.SplitStack(Terraria.Item,Terraria.Item,System.Int32)" /> will be used.
		/// </summary>
		/// <param name="source">The original item instance</param>
		/// <param name="limit">How many items should be transferred</param>
		// Token: 0x06001EA2 RID: 7842 RVA: 0x004D7EFC File Offset: 0x004D60FC
		public static Item TransferWithLimit(Item source, int limit)
		{
			Item destination = source.Clone();
			if (source.stack <= limit)
			{
				source.TurnToAir(false);
			}
			else
			{
				ItemLoader.SplitStack(destination, source, limit);
			}
			return destination;
		}

		/// <summary>
		/// Called when splitting a stack of items.
		/// </summary>
		/// <param name="destination">
		/// The item instance that <paramref name="source" /> will transfer items to, and is usually a clone of <paramref name="source" />.<br />
		/// This parameter's stack will be set to zero before any transfer occurs.
		/// </param>
		/// <param name="source">The item instance being stacked onto <paramref name="destination" /></param>
		/// <param name="numToTransfer">The quantity of <paramref name="source" /> that will be transferred to <paramref name="destination" /></param>
		// Token: 0x06001EA3 RID: 7843 RVA: 0x004D7F2C File Offset: 0x004D612C
		public static void SplitStack(Item destination, Item source, int numToTransfer)
		{
			destination.stack = 0;
			destination.favorited = false;
			foreach (GlobalItem globalItem in ItemLoader.HookSplitStack.Enumerate(destination))
			{
				globalItem.SplitStack(destination, source, numToTransfer);
			}
			ModItem modItem = destination.ModItem;
			if (modItem != null)
			{
				modItem.SplitStack(source, numToTransfer);
			}
			destination.stack += numToTransfer;
			source.stack -= numToTransfer;
		}

		/// <summary>
		/// Call all ModItem.ReforgePrice, then GlobalItem.ReforgePrice hooks.
		/// </summary>
		/// <param name="item"></param>
		/// <param name="reforgePrice"></param>
		/// <param name="canApplyDiscount"></param>
		/// <returns></returns>
		// Token: 0x06001EA4 RID: 7844 RVA: 0x004D7FA4 File Offset: 0x004D61A4
		public static bool ReforgePrice(Item item, ref int reforgePrice, ref bool canApplyDiscount)
		{
			ModItem modItem = item.ModItem;
			bool b = modItem == null || modItem.ReforgePrice(ref reforgePrice, ref canApplyDiscount);
			foreach (GlobalItem g in ItemLoader.HookReforgePrice.Enumerate(item))
			{
				b &= g.ReforgePrice(item, ref reforgePrice, ref canApplyDiscount);
			}
			return b;
		}

		/// <summary>
		/// Calls ModItem.CanReforge, then all GlobalItem.CanReforge hooks. If any return false then false is returned.
		/// </summary>
		// Token: 0x06001EA5 RID: 7845 RVA: 0x004D7FFC File Offset: 0x004D61FC
		public static bool CanReforge(Item item)
		{
			ModItem modItem = item.ModItem;
			bool b = modItem == null || modItem.CanReforge();
			foreach (GlobalItem g in ItemLoader.HookCanReforge.Enumerate(item))
			{
				b &= g.CanReforge(item);
			}
			return b;
		}

		/// <summary>
		/// Calls ModItem.PreReforge, then all GlobalItem.PreReforge hooks.
		/// </summary>
		// Token: 0x06001EA6 RID: 7846 RVA: 0x004D8050 File Offset: 0x004D6250
		public static void PreReforge(Item item)
		{
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.PreReforge();
			}
			foreach (GlobalItem globalItem in ItemLoader.HookPreReforge.Enumerate(item))
			{
				globalItem.PreReforge(item);
			}
		}

		/// <summary>
		/// Calls ModItem.PostReforge, then all GlobalItem.PostReforge hooks.
		/// </summary>
		// Token: 0x06001EA7 RID: 7847 RVA: 0x004D809C File Offset: 0x004D629C
		public static void PostReforge(Item item)
		{
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.PostReforge();
			}
			foreach (GlobalItem globalItem in ItemLoader.HookPostReforge.Enumerate(item))
			{
				globalItem.PostReforge(item);
			}
		}

		/// <summary>
		/// Calls the item's equipment texture's DrawArmorColor hook, then all GlobalItem.DrawArmorColor hooks.
		/// </summary>
		// Token: 0x06001EA8 RID: 7848 RVA: 0x004D80E8 File Offset: 0x004D62E8
		public unsafe static void DrawArmorColor(EquipType type, int slot, Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor)
		{
			EquipTexture equipTexture = EquipLoader.GetEquipTexture(type, slot);
			if (equipTexture != null)
			{
				equipTexture.DrawArmorColor(drawPlayer, shadow, ref color, ref glowMask, ref glowMaskColor);
			}
			ReadOnlySpan<GlobalItem> readOnlySpan = ItemLoader.HookDrawArmorColor.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->DrawArmorColor(type, slot, drawPlayer, shadow, ref color, ref glowMask, ref glowMaskColor);
			}
		}

		/// <summary>
		/// Calls the item's body equipment texture's ArmorArmGlowMask hook, then all GlobalItem.ArmorArmGlowMask hooks.
		/// </summary>
		// Token: 0x06001EA9 RID: 7849 RVA: 0x004D8144 File Offset: 0x004D6344
		public unsafe static void ArmorArmGlowMask(int slot, Player drawPlayer, float shadow, ref int glowMask, ref Color color)
		{
			EquipTexture equipTexture = EquipLoader.GetEquipTexture(EquipType.Body, slot);
			if (equipTexture != null)
			{
				equipTexture.ArmorArmGlowMask(drawPlayer, shadow, ref glowMask, ref color);
			}
			ReadOnlySpan<GlobalItem> readOnlySpan = ItemLoader.HookArmorArmGlowMask.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->ArmorArmGlowMask(slot, drawPlayer, shadow, ref glowMask, ref color);
			}
		}

		/// <summary>
		/// If the player is using wings, this uses the result of GetWing, and calls ModItem.VerticalWingSpeeds then all GlobalItem.VerticalWingSpeeds hooks.
		/// </summary>
		// Token: 0x06001EAA RID: 7850 RVA: 0x004D819C File Offset: 0x004D639C
		public static void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			Item item = player.equippedWings;
			if (item != null)
			{
				ModItem modItem = item.ModItem;
				if (modItem != null)
				{
					modItem.VerticalWingSpeeds(player, ref ascentWhenFalling, ref ascentWhenRising, ref maxCanAscendMultiplier, ref maxAscentMultiplier, ref constantAscend);
				}
				foreach (GlobalItem globalItem in ItemLoader.HookVerticalWingSpeeds.Enumerate(item))
				{
					globalItem.VerticalWingSpeeds(item, player, ref ascentWhenFalling, ref ascentWhenRising, ref maxCanAscendMultiplier, ref maxAscentMultiplier, ref constantAscend);
				}
				return;
			}
			EquipTexture equipTexture = EquipLoader.GetEquipTexture(EquipType.Wings, player.wingsLogic);
			if (equipTexture == null)
			{
				return;
			}
			equipTexture.VerticalWingSpeeds(player, ref ascentWhenFalling, ref ascentWhenRising, ref maxCanAscendMultiplier, ref maxAscentMultiplier, ref constantAscend);
		}

		/// <summary>
		/// If the player is using wings, this uses the result of GetWing, and calls ModItem.HorizontalWingSpeeds then all GlobalItem.HorizontalWingSpeeds hooks.
		/// </summary>
		// Token: 0x06001EAB RID: 7851 RVA: 0x004D8220 File Offset: 0x004D6420
		public static void HorizontalWingSpeeds(Player player)
		{
			Item item = player.equippedWings;
			if (item != null)
			{
				ModItem modItem = item.ModItem;
				if (modItem != null)
				{
					modItem.HorizontalWingSpeeds(player, ref player.accRunSpeed, ref player.runAcceleration);
				}
				foreach (GlobalItem globalItem in ItemLoader.HookHorizontalWingSpeeds.Enumerate(item))
				{
					globalItem.HorizontalWingSpeeds(item, player, ref player.accRunSpeed, ref player.runAcceleration);
				}
				return;
			}
			EquipTexture equipTexture = EquipLoader.GetEquipTexture(EquipType.Wings, player.wingsLogic);
			if (equipTexture == null)
			{
				return;
			}
			equipTexture.HorizontalWingSpeeds(player, ref player.accRunSpeed, ref player.runAcceleration);
		}

		/// <summary>
		/// If wings can be seen on the player, calls the player's wing's equipment texture's WingUpdate and all GlobalItem.WingUpdate hooks.
		/// </summary>
		// Token: 0x06001EAC RID: 7852 RVA: 0x004D82B4 File Offset: 0x004D64B4
		public unsafe static bool WingUpdate(Player player, bool inUse)
		{
			if (player.wings <= 0)
			{
				return false;
			}
			EquipTexture equipTexture = EquipLoader.GetEquipTexture(EquipType.Wings, player.wings);
			bool? retVal = (equipTexture != null) ? new bool?(equipTexture.WingUpdate(player, inUse)) : null;
			ReadOnlySpan<GlobalItem> readOnlySpan = ItemLoader.HookWingUpdate.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				GlobalItem globalItem = *readOnlySpan[i];
				retVal |= globalItem.WingUpdate(player.wings, player, inUse);
			}
			return retVal.GetValueOrDefault();
		}

		/// <summary>
		/// Calls ModItem.Update, then all GlobalItem.Update hooks.
		/// </summary>
		// Token: 0x06001EAD RID: 7853 RVA: 0x004D8340 File Offset: 0x004D6540
		public static void Update(Item item, ref float gravity, ref float maxFallSpeed)
		{
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.Update(ref gravity, ref maxFallSpeed);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookUpdate.Enumerate(item))
			{
				globalItem.Update(item, ref gravity, ref maxFallSpeed);
			}
		}

		/// <summary>
		/// Calls ModItem.PostUpdate and all GlobalItem.PostUpdate hooks.
		/// </summary>
		// Token: 0x06001EAE RID: 7854 RVA: 0x004D8390 File Offset: 0x004D6590
		public static void PostUpdate(Item item)
		{
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.PostUpdate();
			}
			foreach (GlobalItem globalItem in ItemLoader.HookPostUpdate.Enumerate(item))
			{
				globalItem.PostUpdate(item);
			}
		}

		/// <summary>
		/// Calls ModItem.GrabRange, then all GlobalItem.GrabRange hooks.
		/// </summary>
		// Token: 0x06001EAF RID: 7855 RVA: 0x004D83DC File Offset: 0x004D65DC
		public static void GrabRange(Item item, Player player, ref int grabRange)
		{
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.GrabRange(player, ref grabRange);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookGrabRange.Enumerate(item))
			{
				globalItem.GrabRange(item, player, ref grabRange);
			}
		}

		/// <summary>
		/// Calls all GlobalItem.GrabStyle hooks then ModItem.GrabStyle, until one of them returns true. Returns whether any of the hooks returned true.
		/// </summary>
		// Token: 0x06001EB0 RID: 7856 RVA: 0x004D842C File Offset: 0x004D662C
		public static bool GrabStyle(Item item, Player player)
		{
			EntityGlobalsEnumerator<GlobalItem> enumerator = ItemLoader.HookGrabStyle.Enumerate(item).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.GrabStyle(item, player))
				{
					return true;
				}
			}
			return item.ModItem != null && item.ModItem.GrabStyle(player);
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x004D8480 File Offset: 0x004D6680
		public static bool CanPickup(Item item, Player player)
		{
			EntityGlobalsEnumerator<GlobalItem> enumerator = ItemLoader.HookCanPickup.Enumerate(item).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.CanPickup(item, player))
				{
					return false;
				}
			}
			ModItem modItem = item.ModItem;
			return modItem == null || modItem.CanPickup(player);
		}

		/// <summary>
		/// Calls all GlobalItem.OnPickup hooks then ModItem.OnPickup, until one of the returns false. Returns true if all of the hooks return true.
		/// </summary>
		// Token: 0x06001EB2 RID: 7858 RVA: 0x004D84D0 File Offset: 0x004D66D0
		public static bool OnPickup(Item item, Player player)
		{
			EntityGlobalsEnumerator<GlobalItem> enumerator = ItemLoader.HookOnPickup.Enumerate(item).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.OnPickup(item, player))
				{
					return false;
				}
			}
			ModItem modItem = item.ModItem;
			return modItem == null || modItem.OnPickup(player);
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x004D8520 File Offset: 0x004D6720
		public static bool ItemSpace(Item item, Player player)
		{
			EntityGlobalsEnumerator<GlobalItem> enumerator = ItemLoader.HookItemSpace.Enumerate(item).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.ItemSpace(item, player))
				{
					return true;
				}
			}
			ModItem modItem = item.ModItem;
			return modItem != null && modItem.ItemSpace(player);
		}

		/// <summary>
		/// Calls all GlobalItem.GetAlpha hooks then ModItem.GetAlpha, until one of them returns a color, and returns that color. Returns null if all of the hooks return null.
		/// </summary>
		// Token: 0x06001EB4 RID: 7860 RVA: 0x004D8570 File Offset: 0x004D6770
		public static Color? GetAlpha(Item item, Color lightColor)
		{
			if (item.IsAir)
			{
				return null;
			}
			foreach (GlobalItem globalItem in ItemLoader.HookGetAlpha.Enumerate(item))
			{
				Color? color = globalItem.GetAlpha(item, lightColor);
				if (color != null)
				{
					return color;
				}
			}
			ModItem modItem = item.ModItem;
			if (modItem == null)
			{
				return null;
			}
			return modItem.GetAlpha(lightColor);
		}

		/// <summary>
		/// Returns the "and" operator on the results of ModItem.PreDrawInWorld and all GlobalItem.PreDrawInWorld hooks.
		/// </summary>
		// Token: 0x06001EB5 RID: 7861 RVA: 0x004D85E4 File Offset: 0x004D67E4
		public static bool PreDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			bool flag = true;
			if (item.ModItem != null)
			{
				flag &= item.ModItem.PreDrawInWorld(spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
			}
			foreach (GlobalItem g in ItemLoader.HookPreDrawInWorld.Enumerate(item))
			{
				flag &= g.PreDrawInWorld(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
			}
			return flag;
		}

		/// <summary>
		/// Calls ModItem.PostDrawInWorld, then all GlobalItem.PostDrawInWorld hooks.
		/// </summary>
		// Token: 0x06001EB6 RID: 7862 RVA: 0x004D8650 File Offset: 0x004D6850
		public static void PostDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.PostDrawInWorld(spriteBatch, lightColor, alphaColor, rotation, scale, whoAmI);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookPostDrawInWorld.Enumerate(item))
			{
				globalItem.PostDrawInWorld(item, spriteBatch, lightColor, alphaColor, rotation, scale, whoAmI);
			}
		}

		/// <summary>
		/// Returns the "and" operator on the results of all GlobalItem.PreDrawInInventory hooks and ModItem.PreDrawInInventory.
		/// </summary>
		// Token: 0x06001EB7 RID: 7863 RVA: 0x004D86AC File Offset: 0x004D68AC
		public static bool PreDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			bool flag = true;
			foreach (GlobalItem g in ItemLoader.HookPreDrawInInventory.Enumerate(item))
			{
				flag &= g.PreDrawInInventory(item, spriteBatch, position, frame, drawColor, itemColor, origin, scale);
			}
			if (item.ModItem != null)
			{
				flag &= item.ModItem.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
			}
			return flag;
		}

		/// <summary>
		/// Calls ModItem.PostDrawInInventory, then all GlobalItem.PostDrawInInventory hooks.
		/// </summary>
		// Token: 0x06001EB8 RID: 7864 RVA: 0x004D871C File Offset: 0x004D691C
		public static void PostDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.PostDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookPostDrawInInventory.Enumerate(item))
			{
				globalItem.PostDrawInInventory(item, spriteBatch, position, frame, drawColor, itemColor, origin, scale);
			}
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x004D877C File Offset: 0x004D697C
		public unsafe static void HoldoutOffset(float gravDir, int type, ref Vector2 offset)
		{
			ModItem modItem = ItemLoader.GetItem(type);
			if (modItem != null)
			{
				Vector2? modOffset = modItem.HoldoutOffset();
				if (modOffset != null)
				{
					offset.X = modOffset.Value.X;
					offset.Y += gravDir * modOffset.Value.Y;
				}
			}
			ReadOnlySpan<GlobalItem> readOnlySpan = ItemLoader.HookHoldoutOffset.Enumerate(type);
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				Vector2? modOffset2 = readOnlySpan[i]->HoldoutOffset(type);
				if (modOffset2 != null)
				{
					offset.X = modOffset2.Value.X;
					offset.Y = (float)TextureAssets.Item[type].Value.Height / 2f + gravDir * modOffset2.Value.Y;
				}
			}
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x004D8844 File Offset: 0x004D6A44
		public static void HoldoutOrigin(Player player, ref Vector2 origin)
		{
			Item item = player.HeldItem;
			Vector2 modOrigin = Vector2.Zero;
			if (item.ModItem != null)
			{
				Vector2? modOrigin2 = item.ModItem.HoldoutOrigin();
				if (modOrigin2 != null)
				{
					modOrigin = modOrigin2.Value;
				}
			}
			foreach (GlobalItem globalItem in ItemLoader.HookHoldoutOrigin.Enumerate(item))
			{
				Vector2? modOrigin3 = globalItem.HoldoutOrigin(item.type);
				if (modOrigin3 != null)
				{
					modOrigin = modOrigin3.Value;
				}
			}
			modOrigin.X *= (float)player.direction;
			modOrigin.Y *= -player.gravDir;
			origin += modOrigin;
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x004D8900 File Offset: 0x004D6B00
		public static bool CanEquipAccessory(Item item, int slot, bool modded)
		{
			Player player = Main.player[Main.myPlayer];
			if (item.ModItem != null && !item.ModItem.CanEquipAccessory(player, slot, modded))
			{
				return false;
			}
			EntityGlobalsEnumerator<GlobalItem> enumerator = ItemLoader.HookCanEquipAccessory.Enumerate(item).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.CanEquipAccessory(item, player, slot, modded))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x004D8968 File Offset: 0x004D6B68
		public static bool CanEquipAccessory(Player player, Item item, int slot, bool modded)
		{
			if (item.ModItem != null && !item.ModItem.CanEquipAccessory(player, slot, modded))
			{
				return false;
			}
			EntityGlobalsEnumerator<GlobalItem> enumerator = ItemLoader.HookCanEquipAccessory.Enumerate(item).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.CanEquipAccessory(item, player, slot, modded))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x004D89C4 File Offset: 0x004D6BC4
		public static bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem)
		{
			Player player = Main.player[Main.myPlayer];
			return ItemLoader.CanAccessoryBeEquippedWith(equippedItem, incomingItem, player) && ItemLoader.CanAccessoryBeEquippedWith(incomingItem, equippedItem, player);
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x004D89F1 File Offset: 0x004D6BF1
		public static bool CanAccessoryBeEquippedWith(Player player, Item equippedItem, Item incomingItem)
		{
			return ItemLoader.CanAccessoryBeEquippedWith(equippedItem, incomingItem, player) && ItemLoader.CanAccessoryBeEquippedWith(incomingItem, equippedItem, player);
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x004D8A08 File Offset: 0x004D6C08
		private static bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
		{
			if (equippedItem.ModItem != null && !equippedItem.ModItem.CanAccessoryBeEquippedWith(equippedItem, incomingItem, player))
			{
				return false;
			}
			if (incomingItem.ModItem != null && !incomingItem.ModItem.CanAccessoryBeEquippedWith(equippedItem, incomingItem, player))
			{
				return false;
			}
			EntityGlobalsEnumerator<GlobalItem> enumerator = ItemLoader.HookCanAccessoryBeEquippedWith.Enumerate(incomingItem).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.CanAccessoryBeEquippedWith(equippedItem, incomingItem, player))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x004D8A7C File Offset: 0x004D6C7C
		public unsafe static void ExtractinatorUse(ref int resultType, ref int resultStack, int extractType, int extractinatorBlockType)
		{
			ModItem item = ItemLoader.GetItem(extractType);
			if (item != null)
			{
				item.ExtractinatorUse(extractinatorBlockType, ref resultType, ref resultStack);
			}
			ReadOnlySpan<GlobalItem> readOnlySpan = ItemLoader.HookExtractinatorUse.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->ExtractinatorUse(extractType, extractinatorBlockType, ref resultType, ref resultStack);
			}
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x004D8ACC File Offset: 0x004D6CCC
		public static void CaughtFishStack(Item item)
		{
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.CaughtFishStack(ref item.stack);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookCaughtFishStack.Enumerate(item))
			{
				globalItem.CaughtFishStack(item.type, ref item.stack);
			}
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x004D8B28 File Offset: 0x004D6D28
		public unsafe static void IsAnglerQuestAvailable(int itemID, ref bool notAvailable)
		{
			ModItem modItem = ItemLoader.GetItem(itemID);
			if (modItem != null)
			{
				notAvailable |= !modItem.IsAnglerQuestAvailable();
			}
			ReadOnlySpan<GlobalItem> readOnlySpan = ItemLoader.HookIsAnglerQuestAvailable.Enumerate(itemID);
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				GlobalItem g = *readOnlySpan[i];
				notAvailable |= !g.IsAnglerQuestAvailable(itemID);
			}
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x004D8B84 File Offset: 0x004D6D84
		public unsafe static string AnglerChat(int type)
		{
			string chat = "";
			string catchLocation = "";
			ModItem item = ItemLoader.GetItem(type);
			if (item != null)
			{
				item.AnglerQuestChat(ref chat, ref catchLocation);
			}
			ReadOnlySpan<GlobalItem> readOnlySpan = ItemLoader.HookAnglerChat.Enumerate(type);
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->AnglerChat(type, ref chat, ref catchLocation);
			}
			if (string.IsNullOrEmpty(chat) || string.IsNullOrEmpty(catchLocation))
			{
				return null;
			}
			return chat + "\n\n(" + catchLocation + ")";
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x004D8C08 File Offset: 0x004D6E08
		public static bool PreDrawTooltip(Item item, ReadOnlyCollection<TooltipLine> lines, ref int x, ref int y)
		{
			ModItem modItem = item.ModItem;
			bool ret = modItem == null || modItem.PreDrawTooltip(lines, ref x, ref y);
			foreach (GlobalItem g in ItemLoader.HookPreDrawTooltip.Enumerate(item))
			{
				ret &= g.PreDrawTooltip(item, lines, ref x, ref y);
			}
			return ret;
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x004D8C60 File Offset: 0x004D6E60
		public static void PostDrawTooltip(Item item, ReadOnlyCollection<DrawableTooltipLine> lines)
		{
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.PostDrawTooltip(lines);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookPostDrawTooltip.Enumerate(item))
			{
				globalItem.PostDrawTooltip(item, lines);
			}
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x004D8CAC File Offset: 0x004D6EAC
		public static bool PreDrawTooltipLine(Item item, DrawableTooltipLine line, ref int yOffset)
		{
			ModItem modItem = item.ModItem;
			bool ret = modItem == null || modItem.PreDrawTooltipLine(line, ref yOffset);
			foreach (GlobalItem g in ItemLoader.HookPreDrawTooltipLine.Enumerate(item))
			{
				ret &= g.PreDrawTooltipLine(item, line, ref yOffset);
			}
			return ret;
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x004D8D04 File Offset: 0x004D6F04
		public static void PostDrawTooltipLine(Item item, DrawableTooltipLine line)
		{
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.PostDrawTooltipLine(line);
			}
			foreach (GlobalItem globalItem in ItemLoader.HookPostDrawTooltipLine.Enumerate(item))
			{
				globalItem.PostDrawTooltipLine(item, line);
			}
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x004D8D50 File Offset: 0x004D6F50
		public static List<TooltipLine> ModifyTooltips(Item item, ref int numTooltips, string[] names, ref string[] text, ref bool[] modifier, ref bool[] badModifier, ref int oneDropLogo, out Color?[] overrideColor, int prefixlineIndex)
		{
			List<TooltipLine> tooltips = new List<TooltipLine>();
			for (int i = 0; i < numTooltips; i++)
			{
				TooltipLine tooltip = new TooltipLine(names[i], text[i]);
				tooltip.IsModifier = modifier[i];
				tooltip.IsModifierBad = badModifier[i];
				if (i == oneDropLogo)
				{
					tooltip.OneDropLogo = true;
				}
				tooltips.Add(tooltip);
			}
			if (item.prefix >= PrefixID.Count && prefixlineIndex != -1)
			{
				ModPrefix prefix = PrefixLoader.GetPrefix(item.prefix);
				IEnumerable<TooltipLine> tooltipLines = (prefix != null) ? prefix.GetTooltipLines(item) : null;
				if (tooltipLines != null)
				{
					foreach (TooltipLine line in tooltipLines)
					{
						tooltips.Insert(prefixlineIndex, line);
						prefixlineIndex++;
					}
				}
			}
			ModItem modItem = item.ModItem;
			if (modItem != null)
			{
				modItem.ModifyTooltips(tooltips);
			}
			if (!item.IsAir)
			{
				foreach (GlobalItem globalItem in ItemLoader.HookModifyTooltips.Enumerate(item))
				{
					globalItem.ModifyTooltips(item, tooltips);
				}
			}
			tooltips.RemoveAll((TooltipLine x) => !x.Visible);
			numTooltips = tooltips.Count;
			text = new string[numTooltips];
			modifier = new bool[numTooltips];
			badModifier = new bool[numTooltips];
			oneDropLogo = -1;
			overrideColor = new Color?[numTooltips];
			for (int j = 0; j < numTooltips; j++)
			{
				text[j] = tooltips[j].Text;
				modifier[j] = tooltips[j].IsModifier;
				badModifier[j] = tooltips[j].IsModifierBad;
				if (tooltips[j].OneDropLogo)
				{
					oneDropLogo = j;
				}
				overrideColor[j] = tooltips[j].OverrideColor;
			}
			return tooltips;
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x004D8F38 File Offset: 0x004D7138
		public static void ModifyFishingLine(Projectile projectile, ref float polePosX, ref float polePosY, ref Color lineColor)
		{
			Player player = Main.player[projectile.owner];
			Item item = player.inventory[player.selectedItem];
			if (item.ModItem == null)
			{
				return;
			}
			Vector2 lineOriginOffset = Vector2.Zero;
			item.ModItem.ModifyFishingLine(projectile, ref lineOriginOffset, ref lineColor);
			polePosX += lineOriginOffset.X * (float)player.direction;
			if (player.direction < 0)
			{
				polePosX -= 13f;
			}
			polePosY += lineOriginOffset.Y * player.gravDir;
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x004D8FB6 File Offset: 0x004D71B6
		internal static bool NeedsModSaving(Item item)
		{
			return item.type > 0 && (item.ModItem != null || item.prefix >= PrefixID.Count);
		}

		// Token: 0x040015D3 RID: 5587
		private static readonly IList<ModItem> items = new List<ModItem>();

		// Token: 0x040015D4 RID: 5588
		private static readonly List<GlobalHookList<GlobalItem>> hooks = new List<GlobalHookList<GlobalItem>>();

		// Token: 0x040015D5 RID: 5589
		private static readonly List<GlobalHookList<GlobalItem>> modHooks = new List<GlobalHookList<GlobalItem>>();

		// Token: 0x040015D6 RID: 5590
		internal static readonly int vanillaQuestFishCount = 41;

		// Token: 0x040015D7 RID: 5591
		private static GlobalHookList<GlobalItem> HookOnSpawn = ItemLoader.AddHook<Action<Item, IEntitySource>>((GlobalItem g) => (Action<Item, IEntitySource>)methodof(GlobalItem.OnSpawn(Item, IEntitySource)).CreateDelegate(typeof(Action<Item, IEntitySource>), g));

		// Token: 0x040015D8 RID: 5592
		private static GlobalHookList<GlobalItem> HookOnCreate = ItemLoader.AddHook<Action<Item, ItemCreationContext>>((GlobalItem g) => (Action<Item, ItemCreationContext>)methodof(GlobalItem.OnCreated(Item, ItemCreationContext)).CreateDelegate(typeof(Action<Item, ItemCreationContext>), g));

		// Token: 0x040015D9 RID: 5593
		private static GlobalHookList<GlobalItem> HookChoosePrefix = ItemLoader.AddHook<Func<Item, UnifiedRandom, int>>((GlobalItem g) => (Func<Item, UnifiedRandom, int>)methodof(GlobalItem.ChoosePrefix(Item, UnifiedRandom)).CreateDelegate(typeof(Func<Item, UnifiedRandom, int>), g));

		// Token: 0x040015DA RID: 5594
		private static GlobalHookList<GlobalItem> HookPrefixChance = ItemLoader.AddHook<Func<Item, int, UnifiedRandom, bool?>>((GlobalItem g) => (Func<Item, int, UnifiedRandom, bool?>)methodof(GlobalItem.PrefixChance(Item, int, UnifiedRandom)).CreateDelegate(typeof(Func<Item, int, UnifiedRandom, bool?>), g));

		// Token: 0x040015DB RID: 5595
		private static GlobalHookList<GlobalItem> HookAllowPrefix = ItemLoader.AddHook<Func<Item, int, bool>>((GlobalItem g) => (Func<Item, int, bool>)methodof(GlobalItem.AllowPrefix(Item, int)).CreateDelegate(typeof(Func<Item, int, bool>), g));

		// Token: 0x040015DC RID: 5596
		private static GlobalHookList<GlobalItem> HookCanUseItem = ItemLoader.AddHook<Func<Item, Player, bool>>((GlobalItem g) => (Func<Item, Player, bool>)methodof(GlobalItem.CanUseItem(Item, Player)).CreateDelegate(typeof(Func<Item, Player, bool>), g));

		// Token: 0x040015DD RID: 5597
		private static GlobalHookList<GlobalItem> HookCanAutoReuseItem = ItemLoader.AddHook<Func<Item, Player, bool?>>((GlobalItem g) => (Func<Item, Player, bool?>)methodof(GlobalItem.CanAutoReuseItem(Item, Player)).CreateDelegate(typeof(Func<Item, Player, bool?>), g));

		// Token: 0x040015DE RID: 5598
		private static GlobalHookList<GlobalItem> HookUseStyle = ItemLoader.AddHook<Action<Item, Player, Rectangle>>((GlobalItem g) => (Action<Item, Player, Rectangle>)methodof(GlobalItem.UseStyle(Item, Player, Rectangle)).CreateDelegate(typeof(Action<Item, Player, Rectangle>), g));

		// Token: 0x040015DF RID: 5599
		private static GlobalHookList<GlobalItem> HookHoldStyle = ItemLoader.AddHook<Action<Item, Player, Rectangle>>((GlobalItem g) => (Action<Item, Player, Rectangle>)methodof(GlobalItem.HoldStyle(Item, Player, Rectangle)).CreateDelegate(typeof(Action<Item, Player, Rectangle>), g));

		// Token: 0x040015E0 RID: 5600
		private static GlobalHookList<GlobalItem> HookHoldItem = ItemLoader.AddHook<Action<Item, Player>>((GlobalItem g) => (Action<Item, Player>)methodof(GlobalItem.HoldItem(Item, Player)).CreateDelegate(typeof(Action<Item, Player>), g));

		// Token: 0x040015E1 RID: 5601
		private static GlobalHookList<GlobalItem> HookUseTimeMultiplier = ItemLoader.AddHook<Func<Item, Player, float>>((GlobalItem g) => (Func<Item, Player, float>)methodof(GlobalItem.UseTimeMultiplier(Item, Player)).CreateDelegate(typeof(Func<Item, Player, float>), g));

		// Token: 0x040015E2 RID: 5602
		private static GlobalHookList<GlobalItem> HookUseAnimationMultiplier = ItemLoader.AddHook<Func<Item, Player, float>>((GlobalItem g) => (Func<Item, Player, float>)methodof(GlobalItem.UseAnimationMultiplier(Item, Player)).CreateDelegate(typeof(Func<Item, Player, float>), g));

		// Token: 0x040015E3 RID: 5603
		private static GlobalHookList<GlobalItem> HookUseSpeedMultiplier = ItemLoader.AddHook<Func<Item, Player, float>>((GlobalItem g) => (Func<Item, Player, float>)methodof(GlobalItem.UseSpeedMultiplier(Item, Player)).CreateDelegate(typeof(Func<Item, Player, float>), g));

		// Token: 0x040015E4 RID: 5604
		private static GlobalHookList<GlobalItem> HookGetHealLife = ItemLoader.AddHook<ItemLoader.DelegateGetHealLife>((GlobalItem g) => (ItemLoader.DelegateGetHealLife)methodof(GlobalItem.GetHealLife(Item, Player, bool, int*)).CreateDelegate(typeof(ItemLoader.DelegateGetHealLife), g));

		// Token: 0x040015E5 RID: 5605
		private static GlobalHookList<GlobalItem> HookGetHealMana = ItemLoader.AddHook<ItemLoader.DelegateGetHealMana>((GlobalItem g) => (ItemLoader.DelegateGetHealMana)methodof(GlobalItem.GetHealMana(Item, Player, bool, int*)).CreateDelegate(typeof(ItemLoader.DelegateGetHealMana), g));

		// Token: 0x040015E6 RID: 5606
		private static GlobalHookList<GlobalItem> HookModifyManaCost = ItemLoader.AddHook<ItemLoader.DelegateModifyManaCost>((GlobalItem g) => (ItemLoader.DelegateModifyManaCost)methodof(GlobalItem.ModifyManaCost(Item, Player, float*, float*)).CreateDelegate(typeof(ItemLoader.DelegateModifyManaCost), g));

		// Token: 0x040015E7 RID: 5607
		private static GlobalHookList<GlobalItem> HookOnMissingMana = ItemLoader.AddHook<Action<Item, Player, int>>((GlobalItem g) => (Action<Item, Player, int>)methodof(GlobalItem.OnMissingMana(Item, Player, int)).CreateDelegate(typeof(Action<Item, Player, int>), g));

		// Token: 0x040015E8 RID: 5608
		private static GlobalHookList<GlobalItem> HookOnConsumeMana = ItemLoader.AddHook<Action<Item, Player, int>>((GlobalItem g) => (Action<Item, Player, int>)methodof(GlobalItem.OnConsumeMana(Item, Player, int)).CreateDelegate(typeof(Action<Item, Player, int>), g));

		// Token: 0x040015E9 RID: 5609
		private static GlobalHookList<GlobalItem> HookCanConsumeBait = ItemLoader.AddHook<ItemLoader.DelegateCanConsumeBait>((GlobalItem g) => (ItemLoader.DelegateCanConsumeBait)methodof(GlobalItem.CanConsumeBait(Player, Item)).CreateDelegate(typeof(ItemLoader.DelegateCanConsumeBait), g));

		// Token: 0x040015EA RID: 5610
		private static GlobalHookList<GlobalItem> HookModifyResearchSorting = ItemLoader.AddHook<ItemLoader.DelegateModifyResearchSorting>((GlobalItem g) => (ItemLoader.DelegateModifyResearchSorting)methodof(GlobalItem.ModifyResearchSorting(Item, ContentSamples.CreativeHelper.ItemGroup*)).CreateDelegate(typeof(ItemLoader.DelegateModifyResearchSorting), g));

		// Token: 0x040015EB RID: 5611
		private static GlobalHookList<GlobalItem> HookCanResearch = ItemLoader.AddHook<ItemLoader.DelegateCanResearch>((GlobalItem g) => (ItemLoader.DelegateCanResearch)methodof(GlobalItem.CanResearch(Item)).CreateDelegate(typeof(ItemLoader.DelegateCanResearch), g));

		// Token: 0x040015EC RID: 5612
		private static GlobalHookList<GlobalItem> HookOnResearched = ItemLoader.AddHook<ItemLoader.DelegateOnResearched>((GlobalItem g) => (ItemLoader.DelegateOnResearched)methodof(GlobalItem.OnResearched(Item, bool)).CreateDelegate(typeof(ItemLoader.DelegateOnResearched), g));

		// Token: 0x040015ED RID: 5613
		private static GlobalHookList<GlobalItem> HookModifyWeaponDamage = ItemLoader.AddHook<ItemLoader.DelegateModifyWeaponDamage>((GlobalItem g) => (ItemLoader.DelegateModifyWeaponDamage)methodof(GlobalItem.ModifyWeaponDamage(Item, Player, StatModifier*)).CreateDelegate(typeof(ItemLoader.DelegateModifyWeaponDamage), g));

		// Token: 0x040015EE RID: 5614
		private static GlobalHookList<GlobalItem> HookModifyWeaponKnockback = ItemLoader.AddHook<ItemLoader.DelegateModifyWeaponKnockback>((GlobalItem g) => (ItemLoader.DelegateModifyWeaponKnockback)methodof(GlobalItem.ModifyWeaponKnockback(Item, Player, StatModifier*)).CreateDelegate(typeof(ItemLoader.DelegateModifyWeaponKnockback), g));

		// Token: 0x040015EF RID: 5615
		private static GlobalHookList<GlobalItem> HookModifyWeaponCrit = ItemLoader.AddHook<ItemLoader.DelegateModifyWeaponCrit>((GlobalItem g) => (ItemLoader.DelegateModifyWeaponCrit)methodof(GlobalItem.ModifyWeaponCrit(Item, Player, float*)).CreateDelegate(typeof(ItemLoader.DelegateModifyWeaponCrit), g));

		// Token: 0x040015F0 RID: 5616
		private static GlobalHookList<GlobalItem> HookNeedsAmmo = ItemLoader.AddHook<Func<Item, Player, bool>>((GlobalItem g) => (Func<Item, Player, bool>)methodof(GlobalItem.NeedsAmmo(Item, Player)).CreateDelegate(typeof(Func<Item, Player, bool>), g));

		// Token: 0x040015F1 RID: 5617
		private static GlobalHookList<GlobalItem> HookPickAmmo = ItemLoader.AddHook<ItemLoader.DelegatePickAmmo>((GlobalItem g) => (ItemLoader.DelegatePickAmmo)methodof(GlobalItem.PickAmmo(Item, Item, Player, int*, float*, StatModifier*, float*)).CreateDelegate(typeof(ItemLoader.DelegatePickAmmo), g));

		// Token: 0x040015F2 RID: 5618
		private static GlobalHookList<GlobalItem> HookCanChooseAmmo = ItemLoader.AddHook<Func<Item, Item, Player, bool?>>((GlobalItem g) => (Func<Item, Item, Player, bool?>)methodof(GlobalItem.CanChooseAmmo(Item, Item, Player)).CreateDelegate(typeof(Func<Item, Item, Player, bool?>), g));

		// Token: 0x040015F3 RID: 5619
		private static GlobalHookList<GlobalItem> HookCanBeChosenAsAmmo = ItemLoader.AddHook<Func<Item, Item, Player, bool?>>((GlobalItem g) => (Func<Item, Item, Player, bool?>)methodof(GlobalItem.CanBeChosenAsAmmo(Item, Item, Player)).CreateDelegate(typeof(Func<Item, Item, Player, bool?>), g));

		// Token: 0x040015F4 RID: 5620
		private static GlobalHookList<GlobalItem> HookCanConsumeAmmo = ItemLoader.AddHook<Func<Item, Item, Player, bool>>((GlobalItem g) => (Func<Item, Item, Player, bool>)methodof(GlobalItem.CanConsumeAmmo(Item, Item, Player)).CreateDelegate(typeof(Func<Item, Item, Player, bool>), g));

		// Token: 0x040015F5 RID: 5621
		private static GlobalHookList<GlobalItem> HookCanBeConsumedAsAmmo = ItemLoader.AddHook<Func<Item, Item, Player, bool>>((GlobalItem g) => (Func<Item, Item, Player, bool>)methodof(GlobalItem.CanBeConsumedAsAmmo(Item, Item, Player)).CreateDelegate(typeof(Func<Item, Item, Player, bool>), g));

		// Token: 0x040015F6 RID: 5622
		private static GlobalHookList<GlobalItem> HookOnConsumeAmmo = ItemLoader.AddHook<Action<Item, Item, Player>>((GlobalItem g) => (Action<Item, Item, Player>)methodof(GlobalItem.OnConsumeAmmo(Item, Item, Player)).CreateDelegate(typeof(Action<Item, Item, Player>), g));

		// Token: 0x040015F7 RID: 5623
		private static GlobalHookList<GlobalItem> HookOnConsumedAsAmmo = ItemLoader.AddHook<Action<Item, Item, Player>>((GlobalItem g) => (Action<Item, Item, Player>)methodof(GlobalItem.OnConsumedAsAmmo(Item, Item, Player)).CreateDelegate(typeof(Action<Item, Item, Player>), g));

		// Token: 0x040015F8 RID: 5624
		private static GlobalHookList<GlobalItem> HookCanShoot = ItemLoader.AddHook<Func<Item, Player, bool>>((GlobalItem g) => (Func<Item, Player, bool>)methodof(GlobalItem.CanShoot(Item, Player)).CreateDelegate(typeof(Func<Item, Player, bool>), g));

		// Token: 0x040015F9 RID: 5625
		private static GlobalHookList<GlobalItem> HookModifyShootStats = ItemLoader.AddHook<ItemLoader.DelegateModifyShootStats>((GlobalItem g) => (ItemLoader.DelegateModifyShootStats)methodof(GlobalItem.ModifyShootStats(Item, Player, Vector2*, Vector2*, int*, int*, float*)).CreateDelegate(typeof(ItemLoader.DelegateModifyShootStats), g));

		// Token: 0x040015FA RID: 5626
		private static GlobalHookList<GlobalItem> HookShoot = ItemLoader.AddHook<Func<Item, Player, EntitySource_ItemUse_WithAmmo, Vector2, Vector2, int, int, float, bool>>((GlobalItem g) => (Func<Item, Player, EntitySource_ItemUse_WithAmmo, Vector2, Vector2, int, int, float, bool>)methodof(GlobalItem.Shoot(Item, Player, EntitySource_ItemUse_WithAmmo, Vector2, Vector2, int, int, float)).CreateDelegate(typeof(Func<Item, Player, EntitySource_ItemUse_WithAmmo, Vector2, Vector2, int, int, float, bool>), g));

		// Token: 0x040015FB RID: 5627
		private static GlobalHookList<GlobalItem> HookUseItemHitbox = ItemLoader.AddHook<ItemLoader.DelegateUseItemHitbox>((GlobalItem g) => (ItemLoader.DelegateUseItemHitbox)methodof(GlobalItem.UseItemHitbox(Item, Player, Rectangle*, bool*)).CreateDelegate(typeof(ItemLoader.DelegateUseItemHitbox), g));

		// Token: 0x040015FC RID: 5628
		private static GlobalHookList<GlobalItem> HookMeleeEffects = ItemLoader.AddHook<Action<Item, Player, Rectangle>>((GlobalItem g) => (Action<Item, Player, Rectangle>)methodof(GlobalItem.MeleeEffects(Item, Player, Rectangle)).CreateDelegate(typeof(Action<Item, Player, Rectangle>), g));

		// Token: 0x040015FD RID: 5629
		private static GlobalHookList<GlobalItem> HookCanCatchNPC = ItemLoader.AddHook<Func<Item, NPC, Player, bool?>>((GlobalItem g) => (Func<Item, NPC, Player, bool?>)methodof(GlobalItem.CanCatchNPC(Item, NPC, Player)).CreateDelegate(typeof(Func<Item, NPC, Player, bool?>), g));

		// Token: 0x040015FE RID: 5630
		private static GlobalHookList<GlobalItem> HookOnCatchNPC = ItemLoader.AddHook<Action<Item, NPC, Player, bool>>((GlobalItem g) => (Action<Item, NPC, Player, bool>)methodof(GlobalItem.OnCatchNPC(Item, NPC, Player, bool)).CreateDelegate(typeof(Action<Item, NPC, Player, bool>), g));

		// Token: 0x040015FF RID: 5631
		private static GlobalHookList<GlobalItem> HookModifyItemScale = ItemLoader.AddHook<ItemLoader.DelegateModifyItemScale>((GlobalItem g) => (ItemLoader.DelegateModifyItemScale)methodof(GlobalItem.ModifyItemScale(Item, Player, float*)).CreateDelegate(typeof(ItemLoader.DelegateModifyItemScale), g));

		// Token: 0x04001600 RID: 5632
		private static GlobalHookList<GlobalItem> HookCanHitNPC = ItemLoader.AddHook<Func<Item, Player, NPC, bool?>>((GlobalItem g) => (Func<Item, Player, NPC, bool?>)methodof(GlobalItem.CanHitNPC(Item, Player, NPC)).CreateDelegate(typeof(Func<Item, Player, NPC, bool?>), g));

		// Token: 0x04001601 RID: 5633
		private static GlobalHookList<GlobalItem> HookCanCollideNPC = ItemLoader.AddHook<Func<Item, Rectangle, Player, NPC, bool?>>((GlobalItem g) => (Func<Item, Rectangle, Player, NPC, bool?>)methodof(GlobalItem.CanMeleeAttackCollideWithNPC(Item, Rectangle, Player, NPC)).CreateDelegate(typeof(Func<Item, Rectangle, Player, NPC, bool?>), g));

		// Token: 0x04001602 RID: 5634
		private static GlobalHookList<GlobalItem> HookModifyHitNPC = ItemLoader.AddHook<ItemLoader.DelegateModifyHitNPC>((GlobalItem g) => (ItemLoader.DelegateModifyHitNPC)methodof(GlobalItem.ModifyHitNPC(Item, Player, NPC, NPC.HitModifiers*)).CreateDelegate(typeof(ItemLoader.DelegateModifyHitNPC), g));

		// Token: 0x04001603 RID: 5635
		private static GlobalHookList<GlobalItem> HookOnHitNPC = ItemLoader.AddHook<Action<Item, Player, NPC, NPC.HitInfo, int>>((GlobalItem g) => (Action<Item, Player, NPC, NPC.HitInfo, int>)methodof(GlobalItem.OnHitNPC(Item, Player, NPC, NPC.HitInfo, int)).CreateDelegate(typeof(Action<Item, Player, NPC, NPC.HitInfo, int>), g));

		// Token: 0x04001604 RID: 5636
		private static GlobalHookList<GlobalItem> HookCanHitPvp = ItemLoader.AddHook<Func<Item, Player, Player, bool>>((GlobalItem g) => (Func<Item, Player, Player, bool>)methodof(GlobalItem.CanHitPvp(Item, Player, Player)).CreateDelegate(typeof(Func<Item, Player, Player, bool>), g));

		// Token: 0x04001605 RID: 5637
		private static GlobalHookList<GlobalItem> HookModifyHitPvp = ItemLoader.AddHook<ItemLoader.DelegateModifyHitPvp>((GlobalItem g) => (ItemLoader.DelegateModifyHitPvp)methodof(GlobalItem.ModifyHitPvp(Item, Player, Player, Player.HurtModifiers*)).CreateDelegate(typeof(ItemLoader.DelegateModifyHitPvp), g));

		// Token: 0x04001606 RID: 5638
		private static GlobalHookList<GlobalItem> HookOnHitPvp = ItemLoader.AddHook<Action<Item, Player, Player, Player.HurtInfo>>((GlobalItem g) => (Action<Item, Player, Player, Player.HurtInfo>)methodof(GlobalItem.OnHitPvp(Item, Player, Player, Player.HurtInfo)).CreateDelegate(typeof(Action<Item, Player, Player, Player.HurtInfo>), g));

		// Token: 0x04001607 RID: 5639
		private static GlobalHookList<GlobalItem> HookUseItem = ItemLoader.AddHook<Func<Item, Player, bool?>>((GlobalItem g) => (Func<Item, Player, bool?>)methodof(GlobalItem.UseItem(Item, Player)).CreateDelegate(typeof(Func<Item, Player, bool?>), g));

		// Token: 0x04001608 RID: 5640
		private static GlobalHookList<GlobalItem> HookUseAnimation = ItemLoader.AddHook<Action<Item, Player>>((GlobalItem g) => (Action<Item, Player>)methodof(GlobalItem.UseAnimation(Item, Player)).CreateDelegate(typeof(Action<Item, Player>), g));

		// Token: 0x04001609 RID: 5641
		private static GlobalHookList<GlobalItem> HookConsumeItem = ItemLoader.AddHook<Func<Item, Player, bool>>((GlobalItem g) => (Func<Item, Player, bool>)methodof(GlobalItem.ConsumeItem(Item, Player)).CreateDelegate(typeof(Func<Item, Player, bool>), g));

		// Token: 0x0400160A RID: 5642
		private static GlobalHookList<GlobalItem> HookOnConsumeItem = ItemLoader.AddHook<Action<Item, Player>>((GlobalItem g) => (Action<Item, Player>)methodof(GlobalItem.OnConsumeItem(Item, Player)).CreateDelegate(typeof(Action<Item, Player>), g));

		// Token: 0x0400160B RID: 5643
		private static GlobalHookList<GlobalItem> HookUseItemFrame = ItemLoader.AddHook<Action<Item, Player>>((GlobalItem g) => (Action<Item, Player>)methodof(GlobalItem.UseItemFrame(Item, Player)).CreateDelegate(typeof(Action<Item, Player>), g));

		// Token: 0x0400160C RID: 5644
		private static GlobalHookList<GlobalItem> HookHoldItemFrame = ItemLoader.AddHook<Action<Item, Player>>((GlobalItem g) => (Action<Item, Player>)methodof(GlobalItem.HoldItemFrame(Item, Player)).CreateDelegate(typeof(Action<Item, Player>), g));

		// Token: 0x0400160D RID: 5645
		private static GlobalHookList<GlobalItem> HookAltFunctionUse = ItemLoader.AddHook<Func<Item, Player, bool>>((GlobalItem g) => (Func<Item, Player, bool>)methodof(GlobalItem.AltFunctionUse(Item, Player)).CreateDelegate(typeof(Func<Item, Player, bool>), g));

		// Token: 0x0400160E RID: 5646
		private static GlobalHookList<GlobalItem> HookUpdateInventory = ItemLoader.AddHook<Action<Item, Player>>((GlobalItem g) => (Action<Item, Player>)methodof(GlobalItem.UpdateInventory(Item, Player)).CreateDelegate(typeof(Action<Item, Player>), g));

		// Token: 0x0400160F RID: 5647
		private static GlobalHookList<GlobalItem> HookUpdateInfoAccessory = ItemLoader.AddHook<Action<Item, Player>>((GlobalItem g) => (Action<Item, Player>)methodof(GlobalItem.UpdateInfoAccessory(Item, Player)).CreateDelegate(typeof(Action<Item, Player>), g));

		// Token: 0x04001610 RID: 5648
		private static GlobalHookList<GlobalItem> HookUpdateEquip = ItemLoader.AddHook<Action<Item, Player>>((GlobalItem g) => (Action<Item, Player>)methodof(GlobalItem.UpdateEquip(Item, Player)).CreateDelegate(typeof(Action<Item, Player>), g));

		// Token: 0x04001611 RID: 5649
		private static GlobalHookList<GlobalItem> HookUpdateAccessory = ItemLoader.AddHook<Action<Item, Player, bool>>((GlobalItem g) => (Action<Item, Player, bool>)methodof(GlobalItem.UpdateAccessory(Item, Player, bool)).CreateDelegate(typeof(Action<Item, Player, bool>), g));

		// Token: 0x04001612 RID: 5650
		private static GlobalHookList<GlobalItem> HookUpdateVanity = ItemLoader.AddHook<Action<Item, Player>>((GlobalItem g) => (Action<Item, Player>)methodof(GlobalItem.UpdateVanity(Item, Player)).CreateDelegate(typeof(Action<Item, Player>), g));

		// Token: 0x04001613 RID: 5651
		private static GlobalHookList<GlobalItem> HookUpdateVisibleAccessory = ItemLoader.AddHook<Action<Item, Player, bool>>((GlobalItem g) => (Action<Item, Player, bool>)methodof(GlobalItem.UpdateVisibleAccessory(Item, Player, bool)).CreateDelegate(typeof(Action<Item, Player, bool>), g));

		// Token: 0x04001614 RID: 5652
		private static GlobalHookList<GlobalItem> HookUpdateItemDye = ItemLoader.AddHook<Action<Item, Player, int, bool>>((GlobalItem g) => (Action<Item, Player, int, bool>)methodof(GlobalItem.UpdateItemDye(Item, Player, int, bool)).CreateDelegate(typeof(Action<Item, Player, int, bool>), g));

		// Token: 0x04001615 RID: 5653
		private static GlobalHookList<GlobalItem> HookUpdateArmorSet = ItemLoader.AddHook<Action<Player, string>>((GlobalItem g) => (Action<Player, string>)methodof(GlobalItem.UpdateArmorSet(Player, string)).CreateDelegate(typeof(Action<Player, string>), g));

		// Token: 0x04001616 RID: 5654
		private static GlobalHookList<GlobalItem> HookPreUpdateVanitySet = ItemLoader.AddHook<Action<Player, string>>((GlobalItem g) => (Action<Player, string>)methodof(GlobalItem.PreUpdateVanitySet(Player, string)).CreateDelegate(typeof(Action<Player, string>), g));

		// Token: 0x04001617 RID: 5655
		private static GlobalHookList<GlobalItem> HookUpdateVanitySet = ItemLoader.AddHook<Action<Player, string>>((GlobalItem g) => (Action<Player, string>)methodof(GlobalItem.UpdateVanitySet(Player, string)).CreateDelegate(typeof(Action<Player, string>), g));

		// Token: 0x04001618 RID: 5656
		private static GlobalHookList<GlobalItem> HookArmorSetShadows = ItemLoader.AddHook<Action<Player, string>>((GlobalItem g) => (Action<Player, string>)methodof(GlobalItem.ArmorSetShadows(Player, string)).CreateDelegate(typeof(Action<Player, string>), g));

		// Token: 0x04001619 RID: 5657
		private static GlobalHookList<GlobalItem> HookSetMatch = ItemLoader.AddHook<ItemLoader.DelegateSetMatch>((GlobalItem g) => (ItemLoader.DelegateSetMatch)methodof(GlobalItem.SetMatch(int, int, bool, int*, bool*)).CreateDelegate(typeof(ItemLoader.DelegateSetMatch), g));

		// Token: 0x0400161A RID: 5658
		private static GlobalHookList<GlobalItem> HookCanRightClick = ItemLoader.AddHook<Func<Item, bool>>((GlobalItem g) => (Func<Item, bool>)methodof(GlobalItem.CanRightClick(Item)).CreateDelegate(typeof(Func<Item, bool>), g));

		// Token: 0x0400161B RID: 5659
		private static GlobalHookList<GlobalItem> HookRightClick = ItemLoader.AddHook<Action<Item, Player>>((GlobalItem g) => (Action<Item, Player>)methodof(GlobalItem.RightClick(Item, Player)).CreateDelegate(typeof(Action<Item, Player>), g));

		// Token: 0x0400161C RID: 5660
		private static GlobalHookList<GlobalItem> HookModifyItemLoot = ItemLoader.AddHook<Action<Item, ItemLoot>>((GlobalItem g) => (Action<Item, ItemLoot>)methodof(GlobalItem.ModifyItemLoot(Item, ItemLoot)).CreateDelegate(typeof(Action<Item, ItemLoot>), g));

		// Token: 0x0400161D RID: 5661
		private static GlobalHookList<GlobalItem> HookCanStack = ItemLoader.AddHook<Func<Item, Item, bool>>((GlobalItem g) => (Func<Item, Item, bool>)methodof(GlobalItem.CanStack(Item, Item)).CreateDelegate(typeof(Func<Item, Item, bool>), g));

		// Token: 0x0400161E RID: 5662
		private static GlobalHookList<GlobalItem> HookCanStackInWorld = ItemLoader.AddHook<Func<Item, Item, bool>>((GlobalItem g) => (Func<Item, Item, bool>)methodof(GlobalItem.CanStackInWorld(Item, Item)).CreateDelegate(typeof(Func<Item, Item, bool>), g));

		// Token: 0x0400161F RID: 5663
		private static GlobalHookList<GlobalItem> HookOnStack = ItemLoader.AddHook<Action<Item, Item, int>>((GlobalItem g) => (Action<Item, Item, int>)methodof(GlobalItem.OnStack(Item, Item, int)).CreateDelegate(typeof(Action<Item, Item, int>), g));

		// Token: 0x04001620 RID: 5664
		private static GlobalHookList<GlobalItem> HookSplitStack = ItemLoader.AddHook<Action<Item, Item, int>>((GlobalItem g) => (Action<Item, Item, int>)methodof(GlobalItem.SplitStack(Item, Item, int)).CreateDelegate(typeof(Action<Item, Item, int>), g));

		// Token: 0x04001621 RID: 5665
		private static GlobalHookList<GlobalItem> HookReforgePrice = ItemLoader.AddHook<ItemLoader.DelegateReforgePrice>((GlobalItem g) => (ItemLoader.DelegateReforgePrice)methodof(GlobalItem.ReforgePrice(Item, int*, bool*)).CreateDelegate(typeof(ItemLoader.DelegateReforgePrice), g));

		// Token: 0x04001622 RID: 5666
		private static GlobalHookList<GlobalItem> HookCanReforge = ItemLoader.AddHook<Func<Item, bool>>((GlobalItem g) => (Func<Item, bool>)methodof(GlobalItem.CanReforge(Item)).CreateDelegate(typeof(Func<Item, bool>), g));

		// Token: 0x04001623 RID: 5667
		private static GlobalHookList<GlobalItem> HookPreReforge = ItemLoader.AddHook<Action<Item>>((GlobalItem g) => (Action<Item>)methodof(GlobalItem.PreReforge(Item)).CreateDelegate(typeof(Action<Item>), g));

		// Token: 0x04001624 RID: 5668
		private static GlobalHookList<GlobalItem> HookPostReforge = ItemLoader.AddHook<Action<Item>>((GlobalItem g) => (Action<Item>)methodof(GlobalItem.PostReforge(Item)).CreateDelegate(typeof(Action<Item>), g));

		// Token: 0x04001625 RID: 5669
		private static GlobalHookList<GlobalItem> HookDrawArmorColor = ItemLoader.AddHook<ItemLoader.DelegateDrawArmorColor>((GlobalItem g) => (ItemLoader.DelegateDrawArmorColor)methodof(GlobalItem.DrawArmorColor(EquipType, int, Player, float, Color*, int*, Color*)).CreateDelegate(typeof(ItemLoader.DelegateDrawArmorColor), g));

		// Token: 0x04001626 RID: 5670
		private static GlobalHookList<GlobalItem> HookArmorArmGlowMask = ItemLoader.AddHook<ItemLoader.DelegateArmorArmGlowMask>((GlobalItem g) => (ItemLoader.DelegateArmorArmGlowMask)methodof(GlobalItem.ArmorArmGlowMask(int, Player, float, int*, Color*)).CreateDelegate(typeof(ItemLoader.DelegateArmorArmGlowMask), g));

		// Token: 0x04001627 RID: 5671
		private static GlobalHookList<GlobalItem> HookVerticalWingSpeeds = ItemLoader.AddHook<ItemLoader.DelegateVerticalWingSpeeds>((GlobalItem g) => (ItemLoader.DelegateVerticalWingSpeeds)methodof(GlobalItem.VerticalWingSpeeds(Item, Player, float*, float*, float*, float*, float*)).CreateDelegate(typeof(ItemLoader.DelegateVerticalWingSpeeds), g));

		// Token: 0x04001628 RID: 5672
		private static GlobalHookList<GlobalItem> HookHorizontalWingSpeeds = ItemLoader.AddHook<ItemLoader.DelegateHorizontalWingSpeeds>((GlobalItem g) => (ItemLoader.DelegateHorizontalWingSpeeds)methodof(GlobalItem.HorizontalWingSpeeds(Item, Player, float*, float*)).CreateDelegate(typeof(ItemLoader.DelegateHorizontalWingSpeeds), g));

		// Token: 0x04001629 RID: 5673
		private static GlobalHookList<GlobalItem> HookWingUpdate = ItemLoader.AddHook<Func<int, Player, bool, bool>>((GlobalItem g) => (Func<int, Player, bool, bool>)methodof(GlobalItem.WingUpdate(int, Player, bool)).CreateDelegate(typeof(Func<int, Player, bool, bool>), g));

		// Token: 0x0400162A RID: 5674
		private static GlobalHookList<GlobalItem> HookUpdate = ItemLoader.AddHook<ItemLoader.DelegateUpdate>((GlobalItem g) => (ItemLoader.DelegateUpdate)methodof(GlobalItem.Update(Item, float*, float*)).CreateDelegate(typeof(ItemLoader.DelegateUpdate), g));

		// Token: 0x0400162B RID: 5675
		private static GlobalHookList<GlobalItem> HookPostUpdate = ItemLoader.AddHook<Action<Item>>((GlobalItem g) => (Action<Item>)methodof(GlobalItem.PostUpdate(Item)).CreateDelegate(typeof(Action<Item>), g));

		// Token: 0x0400162C RID: 5676
		private static GlobalHookList<GlobalItem> HookGrabRange = ItemLoader.AddHook<ItemLoader.DelegateGrabRange>((GlobalItem g) => (ItemLoader.DelegateGrabRange)methodof(GlobalItem.GrabRange(Item, Player, int*)).CreateDelegate(typeof(ItemLoader.DelegateGrabRange), g));

		// Token: 0x0400162D RID: 5677
		private static GlobalHookList<GlobalItem> HookGrabStyle = ItemLoader.AddHook<Func<Item, Player, bool>>((GlobalItem g) => (Func<Item, Player, bool>)methodof(GlobalItem.GrabStyle(Item, Player)).CreateDelegate(typeof(Func<Item, Player, bool>), g));

		// Token: 0x0400162E RID: 5678
		private static GlobalHookList<GlobalItem> HookCanPickup = ItemLoader.AddHook<Func<Item, Player, bool>>((GlobalItem g) => (Func<Item, Player, bool>)methodof(GlobalItem.CanPickup(Item, Player)).CreateDelegate(typeof(Func<Item, Player, bool>), g));

		// Token: 0x0400162F RID: 5679
		private static GlobalHookList<GlobalItem> HookOnPickup = ItemLoader.AddHook<Func<Item, Player, bool>>((GlobalItem g) => (Func<Item, Player, bool>)methodof(GlobalItem.OnPickup(Item, Player)).CreateDelegate(typeof(Func<Item, Player, bool>), g));

		// Token: 0x04001630 RID: 5680
		private static GlobalHookList<GlobalItem> HookItemSpace = ItemLoader.AddHook<Func<Item, Player, bool>>((GlobalItem g) => (Func<Item, Player, bool>)methodof(GlobalItem.ItemSpace(Item, Player)).CreateDelegate(typeof(Func<Item, Player, bool>), g));

		// Token: 0x04001631 RID: 5681
		private static GlobalHookList<GlobalItem> HookGetAlpha = ItemLoader.AddHook<Func<Item, Color, Color?>>((GlobalItem g) => (Func<Item, Color, Color?>)methodof(GlobalItem.GetAlpha(Item, Color)).CreateDelegate(typeof(Func<Item, Color, Color?>), g));

		// Token: 0x04001632 RID: 5682
		private static GlobalHookList<GlobalItem> HookPreDrawInWorld = ItemLoader.AddHook<ItemLoader.DelegatePreDrawInWorld>((GlobalItem g) => (ItemLoader.DelegatePreDrawInWorld)methodof(GlobalItem.PreDrawInWorld(Item, SpriteBatch, Color, Color, float*, float*, int)).CreateDelegate(typeof(ItemLoader.DelegatePreDrawInWorld), g));

		// Token: 0x04001633 RID: 5683
		private static GlobalHookList<GlobalItem> HookPostDrawInWorld = ItemLoader.AddHook<Action<Item, SpriteBatch, Color, Color, float, float, int>>((GlobalItem g) => (Action<Item, SpriteBatch, Color, Color, float, float, int>)methodof(GlobalItem.PostDrawInWorld(Item, SpriteBatch, Color, Color, float, float, int)).CreateDelegate(typeof(Action<Item, SpriteBatch, Color, Color, float, float, int>), g));

		// Token: 0x04001634 RID: 5684
		private static GlobalHookList<GlobalItem> HookPreDrawInInventory = ItemLoader.AddHook<Func<Item, SpriteBatch, Vector2, Rectangle, Color, Color, Vector2, float, bool>>((GlobalItem g) => (Func<Item, SpriteBatch, Vector2, Rectangle, Color, Color, Vector2, float, bool>)methodof(GlobalItem.PreDrawInInventory(Item, SpriteBatch, Vector2, Rectangle, Color, Color, Vector2, float)).CreateDelegate(typeof(Func<Item, SpriteBatch, Vector2, Rectangle, Color, Color, Vector2, float, bool>), g));

		// Token: 0x04001635 RID: 5685
		private static GlobalHookList<GlobalItem> HookPostDrawInInventory = ItemLoader.AddHook<Action<Item, SpriteBatch, Vector2, Rectangle, Color, Color, Vector2, float>>((GlobalItem g) => (Action<Item, SpriteBatch, Vector2, Rectangle, Color, Color, Vector2, float>)methodof(GlobalItem.PostDrawInInventory(Item, SpriteBatch, Vector2, Rectangle, Color, Color, Vector2, float)).CreateDelegate(typeof(Action<Item, SpriteBatch, Vector2, Rectangle, Color, Color, Vector2, float>), g));

		// Token: 0x04001636 RID: 5686
		private static GlobalHookList<GlobalItem> HookHoldoutOffset = ItemLoader.AddHook<Func<int, Vector2?>>((GlobalItem g) => (Func<int, Vector2?>)methodof(GlobalItem.HoldoutOffset(int)).CreateDelegate(typeof(Func<int, Vector2?>), g));

		// Token: 0x04001637 RID: 5687
		private static GlobalHookList<GlobalItem> HookHoldoutOrigin = ItemLoader.AddHook<Func<int, Vector2?>>((GlobalItem g) => (Func<int, Vector2?>)methodof(GlobalItem.HoldoutOrigin(int)).CreateDelegate(typeof(Func<int, Vector2?>), g));

		// Token: 0x04001638 RID: 5688
		private static GlobalHookList<GlobalItem> HookCanEquipAccessory = ItemLoader.AddHook<Func<Item, Player, int, bool, bool>>((GlobalItem g) => (Func<Item, Player, int, bool, bool>)methodof(GlobalItem.CanEquipAccessory(Item, Player, int, bool)).CreateDelegate(typeof(Func<Item, Player, int, bool, bool>), g));

		// Token: 0x04001639 RID: 5689
		private static GlobalHookList<GlobalItem> HookCanAccessoryBeEquippedWith = ItemLoader.AddHook<Func<Item, Item, Player, bool>>((GlobalItem g) => (Func<Item, Item, Player, bool>)methodof(GlobalItem.CanAccessoryBeEquippedWith(Item, Item, Player)).CreateDelegate(typeof(Func<Item, Item, Player, bool>), g));

		// Token: 0x0400163A RID: 5690
		private static GlobalHookList<GlobalItem> HookExtractinatorUse = ItemLoader.AddHook<ItemLoader.DelegateExtractinatorUse>((GlobalItem g) => (ItemLoader.DelegateExtractinatorUse)methodof(GlobalItem.ExtractinatorUse(int, int, int*, int*)).CreateDelegate(typeof(ItemLoader.DelegateExtractinatorUse), g));

		// Token: 0x0400163B RID: 5691
		private static GlobalHookList<GlobalItem> HookCaughtFishStack = ItemLoader.AddHook<ItemLoader.DelegateCaughtFishStack>((GlobalItem g) => (ItemLoader.DelegateCaughtFishStack)methodof(GlobalItem.CaughtFishStack(int, int*)).CreateDelegate(typeof(ItemLoader.DelegateCaughtFishStack), g));

		// Token: 0x0400163C RID: 5692
		private static GlobalHookList<GlobalItem> HookIsAnglerQuestAvailable = ItemLoader.AddHook<Func<int, bool>>((GlobalItem g) => (Func<int, bool>)methodof(GlobalItem.IsAnglerQuestAvailable(int)).CreateDelegate(typeof(Func<int, bool>), g));

		// Token: 0x0400163D RID: 5693
		private static GlobalHookList<GlobalItem> HookAnglerChat = ItemLoader.AddHook<ItemLoader.DelegateAnglerChat>((GlobalItem g) => (ItemLoader.DelegateAnglerChat)methodof(GlobalItem.AnglerChat(int, string*, string*)).CreateDelegate(typeof(ItemLoader.DelegateAnglerChat), g));

		// Token: 0x0400163E RID: 5694
		private static GlobalHookList<GlobalItem> HookPreDrawTooltip = ItemLoader.AddHook<ItemLoader.DelegatePreDrawTooltip>((GlobalItem g) => (ItemLoader.DelegatePreDrawTooltip)methodof(GlobalItem.PreDrawTooltip(Item, ReadOnlyCollection<TooltipLine>, int*, int*)).CreateDelegate(typeof(ItemLoader.DelegatePreDrawTooltip), g));

		// Token: 0x0400163F RID: 5695
		private static GlobalHookList<GlobalItem> HookPostDrawTooltip = ItemLoader.AddHook<ItemLoader.DelegatePostDrawTooltip>((GlobalItem g) => (ItemLoader.DelegatePostDrawTooltip)methodof(GlobalItem.PostDrawTooltip(Item, ReadOnlyCollection<DrawableTooltipLine>)).CreateDelegate(typeof(ItemLoader.DelegatePostDrawTooltip), g));

		// Token: 0x04001640 RID: 5696
		private static GlobalHookList<GlobalItem> HookPreDrawTooltipLine = ItemLoader.AddHook<ItemLoader.DelegatePreDrawTooltipLine>((GlobalItem g) => (ItemLoader.DelegatePreDrawTooltipLine)methodof(GlobalItem.PreDrawTooltipLine(Item, DrawableTooltipLine, int*)).CreateDelegate(typeof(ItemLoader.DelegatePreDrawTooltipLine), g));

		// Token: 0x04001641 RID: 5697
		private static GlobalHookList<GlobalItem> HookPostDrawTooltipLine = ItemLoader.AddHook<ItemLoader.DelegatePostDrawTooltipLine>((GlobalItem g) => (ItemLoader.DelegatePostDrawTooltipLine)methodof(GlobalItem.PostDrawTooltipLine(Item, DrawableTooltipLine)).CreateDelegate(typeof(ItemLoader.DelegatePostDrawTooltipLine), g));

		// Token: 0x04001642 RID: 5698
		private static GlobalHookList<GlobalItem> HookModifyTooltips = ItemLoader.AddHook<Action<Item, List<TooltipLine>>>((GlobalItem g) => (Action<Item, List<TooltipLine>>)methodof(GlobalItem.ModifyTooltips(Item, List<TooltipLine>)).CreateDelegate(typeof(Action<Item, List<TooltipLine>>), g));

		// Token: 0x04001643 RID: 5699
		internal static GlobalHookList<GlobalItem> HookSaveData = ItemLoader.AddHook<Action<Item, TagCompound>>((GlobalItem g) => (Action<Item, TagCompound>)methodof(GlobalItem.SaveData(Item, TagCompound)).CreateDelegate(typeof(Action<Item, TagCompound>), g));

		// Token: 0x04001644 RID: 5700
		internal static GlobalHookList<GlobalItem> HookNetSend = ItemLoader.AddHook<Action<Item, BinaryWriter>>((GlobalItem g) => (Action<Item, BinaryWriter>)methodof(GlobalItem.NetSend(Item, BinaryWriter)).CreateDelegate(typeof(Action<Item, BinaryWriter>), g));

		// Token: 0x04001645 RID: 5701
		internal static GlobalHookList<GlobalItem> HookNetReceive = ItemLoader.AddHook<Action<Item, BinaryReader>>((GlobalItem g) => (Action<Item, BinaryReader>)methodof(GlobalItem.NetReceive(Item, BinaryReader)).CreateDelegate(typeof(Action<Item, BinaryReader>), g));

		// Token: 0x020008DC RID: 2268
		// (Invoke) Token: 0x06005295 RID: 21141
		private delegate void DelegateGetHealLife(Item item, Player player, bool quickHeal, ref int healValue);

		// Token: 0x020008DD RID: 2269
		// (Invoke) Token: 0x06005299 RID: 21145
		private delegate void DelegateGetHealMana(Item item, Player player, bool quickHeal, ref int healValue);

		// Token: 0x020008DE RID: 2270
		// (Invoke) Token: 0x0600529D RID: 21149
		private delegate void DelegateModifyManaCost(Item item, Player player, ref float reduce, ref float mult);

		// Token: 0x020008DF RID: 2271
		// (Invoke) Token: 0x060052A1 RID: 21153
		private delegate bool? DelegateCanConsumeBait(Player baiter, Item bait);

		// Token: 0x020008E0 RID: 2272
		// (Invoke) Token: 0x060052A5 RID: 21157
		private delegate void DelegateModifyResearchSorting(Item item, ref ContentSamples.CreativeHelper.ItemGroup itemGroup);

		// Token: 0x020008E1 RID: 2273
		// (Invoke) Token: 0x060052A9 RID: 21161
		private delegate bool DelegateCanResearch(Item item);

		// Token: 0x020008E2 RID: 2274
		// (Invoke) Token: 0x060052AD RID: 21165
		private delegate void DelegateOnResearched(Item item, bool fullyResearched);

		// Token: 0x020008E3 RID: 2275
		// (Invoke) Token: 0x060052B1 RID: 21169
		private delegate void DelegateModifyWeaponDamage(Item item, Player player, ref StatModifier damage);

		// Token: 0x020008E4 RID: 2276
		// (Invoke) Token: 0x060052B5 RID: 21173
		private delegate void DelegateModifyWeaponKnockback(Item item, Player player, ref StatModifier knockback);

		// Token: 0x020008E5 RID: 2277
		// (Invoke) Token: 0x060052B9 RID: 21177
		private delegate void DelegateModifyWeaponCrit(Item item, Player player, ref float crit);

		// Token: 0x020008E6 RID: 2278
		// (Invoke) Token: 0x060052BD RID: 21181
		private delegate void DelegatePickAmmo(Item weapon, Item ammo, Player player, ref int type, ref float speed, ref StatModifier damage, ref float knockback);

		// Token: 0x020008E7 RID: 2279
		// (Invoke) Token: 0x060052C1 RID: 21185
		private delegate void DelegateModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockBack);

		// Token: 0x020008E8 RID: 2280
		// (Invoke) Token: 0x060052C5 RID: 21189
		private delegate void DelegateUseItemHitbox(Item item, Player player, ref Rectangle hitbox, ref bool noHitbox);

		// Token: 0x020008E9 RID: 2281
		// (Invoke) Token: 0x060052C9 RID: 21193
		private delegate void DelegateModifyItemScale(Item item, Player player, ref float scale);

		// Token: 0x020008EA RID: 2282
		// (Invoke) Token: 0x060052CD RID: 21197
		private delegate void DelegateModifyHitNPC(Item item, Player player, NPC target, ref NPC.HitModifiers modifiers);

		// Token: 0x020008EB RID: 2283
		// (Invoke) Token: 0x060052D1 RID: 21201
		private delegate void DelegateModifyHitPvp(Item item, Player player, Player target, ref Player.HurtModifiers modifiers);

		// Token: 0x020008EC RID: 2284
		// (Invoke) Token: 0x060052D5 RID: 21205
		private delegate void DelegateSetMatch(int armorSlot, int type, bool male, ref int equipSlot, ref bool robes);

		// Token: 0x020008ED RID: 2285
		// (Invoke) Token: 0x060052D9 RID: 21209
		private delegate bool DelegateReforgePrice(Item item, ref int reforgePrice, ref bool canApplyDiscount);

		// Token: 0x020008EE RID: 2286
		// (Invoke) Token: 0x060052DD RID: 21213
		private delegate void DelegateDrawArmorColor(EquipType type, int slot, Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor);

		// Token: 0x020008EF RID: 2287
		// (Invoke) Token: 0x060052E1 RID: 21217
		private delegate void DelegateArmorArmGlowMask(int slot, Player drawPlayer, float shadow, ref int glowMask, ref Color color);

		// Token: 0x020008F0 RID: 2288
		// (Invoke) Token: 0x060052E5 RID: 21221
		private delegate void DelegateVerticalWingSpeeds(Item item, Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend);

		// Token: 0x020008F1 RID: 2289
		// (Invoke) Token: 0x060052E9 RID: 21225
		private delegate void DelegateHorizontalWingSpeeds(Item item, Player player, ref float speed, ref float acceleration);

		// Token: 0x020008F2 RID: 2290
		// (Invoke) Token: 0x060052ED RID: 21229
		private delegate void DelegateUpdate(Item item, ref float gravity, ref float maxFallSpeed);

		// Token: 0x020008F3 RID: 2291
		// (Invoke) Token: 0x060052F1 RID: 21233
		private delegate void DelegateGrabRange(Item item, Player player, ref int grabRange);

		// Token: 0x020008F4 RID: 2292
		// (Invoke) Token: 0x060052F5 RID: 21237
		private delegate bool DelegatePreDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI);

		// Token: 0x020008F5 RID: 2293
		// (Invoke) Token: 0x060052F9 RID: 21241
		private delegate void DelegateExtractinatorUse(int extractType, int extractinatorBlockType, ref int resultType, ref int resultStack);

		// Token: 0x020008F6 RID: 2294
		// (Invoke) Token: 0x060052FD RID: 21245
		private delegate void DelegateCaughtFishStack(int type, ref int stack);

		// Token: 0x020008F7 RID: 2295
		// (Invoke) Token: 0x06005301 RID: 21249
		private delegate void DelegateAnglerChat(int type, ref string chat, ref string catchLocation);

		// Token: 0x020008F8 RID: 2296
		// (Invoke) Token: 0x06005305 RID: 21253
		private delegate bool DelegatePreDrawTooltip(Item item, ReadOnlyCollection<TooltipLine> lines, ref int x, ref int y);

		// Token: 0x020008F9 RID: 2297
		// (Invoke) Token: 0x06005309 RID: 21257
		private delegate void DelegatePostDrawTooltip(Item item, ReadOnlyCollection<DrawableTooltipLine> lines);

		// Token: 0x020008FA RID: 2298
		// (Invoke) Token: 0x0600530D RID: 21261
		private delegate bool DelegatePreDrawTooltipLine(Item item, DrawableTooltipLine line, ref int yOffset);

		// Token: 0x020008FB RID: 2299
		// (Invoke) Token: 0x06005311 RID: 21265
		private delegate void DelegatePostDrawTooltipLine(Item item, DrawableTooltipLine line);
	}
}
