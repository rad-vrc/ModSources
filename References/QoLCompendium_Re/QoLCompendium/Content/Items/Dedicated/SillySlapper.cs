using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using QoLCompendium.Content.Projectiles.Dedicated;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Dedicated
{
	// Token: 0x020001F7 RID: 503
	public class SillySlapper : ModItem
	{
		// Token: 0x06000B30 RID: 2864 RVA: 0x00020AFF File Offset: 0x0001ECFF
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.DedicatedItems;
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00021598 File Offset: 0x0001F798
		public override void SetDefaults()
		{
			base.Item.width = 14;
			base.Item.height = 19;
			base.Item.accessory = true;
			base.Item.autoReuse = true;
			base.Item.useStyle = 1;
			base.Item.useAnimation = 30;
			base.Item.useTime = 30;
			base.Item.noMelee = true;
			base.Item.noUseGraphic = true;
			base.Item.UseSound = new SoundStyle?(SoundID.Item152);
			base.Item.damage = 100;
			base.Item.DamageType = DamageClass.Generic;
			base.Item.knockBack = 2f;
			base.Item.shootSpeed = 5f;
			base.Item.shoot = ModContent.ProjectileType<SillySlapperWhip>();
			base.Item.SetShopValues(ItemRarityColor.StrongRed10, Item.buyPrice(0, 8, 0, 0));
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0002168D File Offset: 0x0001F88D
		public unsafe override void UpdateAccessory(Player player, bool hideVisual)
		{
			*player.GetDamage(DamageClass.Generic) *= 2f;
			player.GetModPlayer<QoLCPlayer>().sillySlapper = true;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0002168D File Offset: 0x0001F88D
		public unsafe override void UpdateVanity(Player player)
		{
			*player.GetDamage(DamageClass.Generic) *= 2f;
			player.GetModPlayer<QoLCPlayer>().sillySlapper = true;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0002168D File Offset: 0x0001F88D
		public unsafe override void HoldItem(Player player)
		{
			*player.GetDamage(DamageClass.Generic) *= 2f;
			player.GetModPlayer<QoLCPlayer>().sillySlapper = true;
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x000216BC File Offset: 0x0001F8BC
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine dedicated = new TooltipLine(base.Mod, "Dedicated", Language.GetTextValue("Mods.QoLCompendium.DedicatedTooltips.Quinn"))
			{
				OverrideColor = new Color?(Common.ColorSwap(Color.LightSeaGreen, Color.Aquamarine, 3f))
			};
			tooltips.Add(dedicated);
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.DedicatedItems);
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00021720 File Offset: 0x0001F920
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.DedicatedItems, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(23, 100);
			itemRecipe.AddIngredient(75, 50);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}
	}
}
