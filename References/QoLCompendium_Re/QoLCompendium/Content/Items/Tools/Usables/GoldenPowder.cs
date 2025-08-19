using System;
using QoLCompendium.Content.Projectiles.Other;
using QoLCompendium.Core;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Usables
{
	// Token: 0x0200017E RID: 382
	public class GoldenPowder : ModItem
	{
		// Token: 0x060007BA RID: 1978 RVA: 0x000153EC File Offset: 0x000135EC
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.GoldenPowder;
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00015406 File Offset: 0x00013606
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 99;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00015418 File Offset: 0x00013618
		public override void SetDefaults()
		{
			base.Item.consumable = true;
			base.Item.maxStack = Item.CommonMaxStack;
			base.Item.useAnimation = 15;
			base.Item.useTime = 15;
			base.Item.shootSpeed = 4f;
			base.Item.shoot = ModContent.ProjectileType<GoldenPowderProjectile>();
			base.Item.useStyle = 1;
			base.Item.width = 11;
			base.Item.height = 26;
			base.Item.UseSound = new SoundStyle?(SoundID.Item1);
			base.Item.noMelee = true;
			base.Item.useTurn = true;
			base.Item.autoReuse = true;
			base.Item.SetShopValues(ItemRarityColor.White0, Item.sellPrice(0, 0, 10, 0));
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x000154F0 File Offset: 0x000136F0
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.GoldenPowder, base.Type, 10, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(66, 10);
			itemRecipe.AddIngredient(73, 1);
			itemRecipe.AddTile(13);
			itemRecipe.Register();
		}
	}
}
