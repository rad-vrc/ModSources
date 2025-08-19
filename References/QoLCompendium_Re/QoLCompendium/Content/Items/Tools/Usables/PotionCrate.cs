using System;
using System.Collections.Generic;
using System.Linq;
using Humanizer;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace QoLCompendium.Content.Items.Tools.Usables
{
	// Token: 0x02000181 RID: 385
	public class PotionCrate : ModItem
	{
		// Token: 0x060007D0 RID: 2000 RVA: 0x000159CF File Offset: 0x00013BCF
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.PotionCrate;
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x000159E9 File Offset: 0x00013BE9
		public override void SetDefaults()
		{
			base.Item.width = 16;
			base.Item.height = 16;
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 0, 20, 0));
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00015A1B File Offset: 0x00013C1B
		public override void SaveData(TagCompound tag)
		{
			tag.Add("BuffIDList", PotionCrate.BuffIDList);
			tag.Add("ItemIDList", PotionCrate.ItemIDList);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00015A3D File Offset: 0x00013C3D
		public override void LoadData(TagCompound tag)
		{
			PotionCrate.BuffIDList = (List<int>)tag.GetList<int>("BuffIDList");
			PotionCrate.ItemIDList = (List<int>)tag.GetList<int>("ItemIDList");
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0000404D File Offset: 0x0000224D
		public override bool ConsumeItem(Player player)
		{
			return false;
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00002430 File Offset: 0x00000630
		public override bool CanRightClick()
		{
			return true;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00015A6C File Offset: 0x00013C6C
		public override void RightClick(Player player)
		{
			if (Main.mouseItem.buffType > 0 && Main.mouseItem.stack >= QoLCompendium.mainConfig.EndlessBuffAmount && !PotionCrate.BuffIDList.Contains(Main.mouseItem.buffType) && !PotionCrate.ItemIDList.Contains(Main.mouseItem.type))
			{
				PotionCrate.BuffIDList.Add(Main.mouseItem.buffType);
				PotionCrate.ItemIDList.Add(Main.mouseItem.type);
				Main.mouseItem.stack -= QoLCompendium.mainConfig.EndlessBuffAmount;
				if (Main.mouseItem.stack == 0)
				{
					Main.mouseItem.TurnToAir(false);
					return;
				}
			}
			else
			{
				if (PotionCrate.BuffIDList.Count == 0 && PotionCrate.ItemIDList.Count == 0)
				{
					return;
				}
				if (PotionCrate.BuffIDList.Count > 0 && PotionCrate.ItemIDList.Count > 0)
				{
					Item.NewItem(base.Item.GetSource_FromThis(null), player.position, PotionCrate.ItemIDList.Last<int>(), QoLCompendium.mainConfig.EndlessBuffAmount, false, 0, false, false);
					PotionCrate.BuffIDList.RemoveAt(PotionCrate.BuffIDList.Count - 1);
					PotionCrate.ItemIDList.RemoveAt(PotionCrate.ItemIDList.Count - 1);
				}
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00015BBC File Offset: 0x00013DBC
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine tip0 = tooltips.Find((TooltipLine l) => l.Name == "Tooltip0");
			TooltipLine text = new TooltipLine(base.Mod, "PotionCrateTooltip", StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.PotionCrateTooltip"), new object[]
			{
				QoLCompendium.mainConfig.EndlessBuffAmount
			}));
			tooltips.Insert(tooltips.IndexOf(tip0) + 1, text);
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.PotionCrate);
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00015C50 File Offset: 0x00013E50
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PotionCrate, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(9, 12);
			itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 4);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}

		// Token: 0x04000037 RID: 55
		public static List<int> BuffIDList = new List<int>();

		// Token: 0x04000038 RID: 56
		public static List<int> ItemIDList = new List<int>();
	}
}
