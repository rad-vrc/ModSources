using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.PlayerChanges;
using QoLCompendium.Core.Changes.TooltipChanges;
using QoLCompendium.Core.UI.Panels;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace QoLCompendium.Content.Items.Tools.MobileStorages
{
	// Token: 0x0200019F RID: 415
	public class AllInOneAccess : ModItem
	{
		// Token: 0x060008BF RID: 2239 RVA: 0x0001A3BC File Offset: 0x000185BC
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.MobileStorages;
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0001A3D6 File Offset: 0x000185D6
		public override void SetStaticDefaults()
		{
			Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(4, 8, false));
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0001A3FC File Offset: 0x000185FC
		public override void SetDefaults()
		{
			base.Item.width = 16;
			base.Item.height = 24;
			base.Item.maxStack = 1;
			base.Item.consumable = false;
			base.Item.useStyle = 4;
			base.Item.UseSound = new SoundStyle?(SoundID.MenuOpen);
			base.Item.useAnimation = 20;
			base.Item.useTime = 20;
			base.Item.SetShopValues(ItemRarityColor.Orange3, Item.buyPrice(0, 10, 0, 0));
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0001A48C File Offset: 0x0001868C
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.MobileStorages);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00008DD8 File Offset: 0x00006FD8
		public override void OnConsumeItem(Player player)
		{
			base.Item.stack++;
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00002430 File Offset: 0x00000630
		public override bool CanRightClick()
		{
			return true;
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0001A4A4 File Offset: 0x000186A4
		public override void RightClick(Player player)
		{
			SoundEngine.PlaySound(SoundID.Item130, new Vector2?(player.Center), null);
			this.Mode++;
			if (this.Mode > 1)
			{
				this.Mode = 0;
			}
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0001A4DB File Offset: 0x000186DB
		public override void SaveData(TagCompound tag)
		{
			tag["AllInOneAccessMode"] = this.Mode;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0001A4F3 File Offset: 0x000186F3
		public override void LoadData(TagCompound tag)
		{
			this.Mode = tag.GetInt("AllInOneAccessMode");
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0001A506 File Offset: 0x00018706
		public override void HoldItem(Player player)
		{
			if (AllInOneAccessUI.visible)
			{
				player.GetModPlayer<BankPlayer>().chests = true;
			}
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0001A51C File Offset: 0x0001871C
		public override void UpdateInventory(Player player)
		{
			if (AllInOneAccessUI.visible)
			{
				player.GetModPlayer<BankPlayer>().chests = true;
			}
			if (this.Mode == 0)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.AllInOneAccess.Open"));
				player.IsVoidVaultEnabled = true;
			}
			if (this.Mode == 1)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.AllInOneAccess.Closed"));
				player.IsVoidVaultEnabled = false;
			}
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0001A585 File Offset: 0x00018785
		public override bool? UseItem(Player player)
		{
			if (!AllInOneAccessUI.visible)
			{
				AllInOneAccessUI.visible = true;
			}
			return new bool?(true);
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0001A59A File Offset: 0x0001879A
		public override bool CanUseItem(Player player)
		{
			return !AllInOneAccessUI.visible;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0001A5A8 File Offset: 0x000187A8
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.MobileStorages, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(3213, 1);
			itemRecipe.AddIngredient(ModContent.ItemType<EtherianConstruct>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<FlyingSafe>(), 1);
			itemRecipe.AddIngredient(4131, 1);
			itemRecipe.AddTile(114);
			itemRecipe.Register();
		}

		// Token: 0x0400003F RID: 63
		public int Mode;
	}
}
