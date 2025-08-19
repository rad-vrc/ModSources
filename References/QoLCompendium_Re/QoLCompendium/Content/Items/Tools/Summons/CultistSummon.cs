using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using QoLCompendium.Content.Projectiles.Other;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Summons
{
	// Token: 0x0200018A RID: 394
	public class CultistSummon : ModItem
	{
		// Token: 0x06000819 RID: 2073 RVA: 0x00017610 File Offset: 0x00015810
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.BossSummons;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0001762A File Offset: 0x0001582A
		public override void SetStaticDefaults()
		{
			ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 16;
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00017648 File Offset: 0x00015848
		public override void SetDefaults()
		{
			base.Item.width = 21;
			base.Item.height = 21;
			base.Item.maxStack = Item.CommonMaxStack;
			base.Item.useAnimation = 30;
			base.Item.useTime = 30;
			base.Item.useStyle = 4;
			base.Item.consumable = true;
			base.Item.shoot = ModContent.ProjectileType<NPCSpawner>();
			base.Item.SetShopValues(ItemRarityColor.StrongRed10, Item.sellPrice(0, 0, 0, 0));
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x000176D7 File Offset: 0x000158D7
		public override bool CanUseItem(Player player)
		{
			return Main.hardMode && NPC.downedGolemBoss && !NPC.AnyNPCs(439);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x000176F8 File Offset: 0x000158F8
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 spawnPosition = player.Center - Vector2.UnitY * 800f;
			Projectile.NewProjectile(player.GetSource_ItemUse(base.Item, null), spawnPosition, Vector2.Zero, ModContent.ProjectileType<NPCSpawner>(), 0, 0f, player.whoAmI, 439f, 0f, 0f);
			SoundEngine.PlaySound(SoundID.Roar, new Vector2?(player.position), null);
			return false;
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00017771 File Offset: 0x00015971
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.BossSummons);
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0001778C File Offset: 0x0001598C
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.BossSummons, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(1101, 5);
			itemRecipe.AddIngredient(1508, 5);
			itemRecipe.AddTile(134);
			itemRecipe.Register();
		}
	}
}
