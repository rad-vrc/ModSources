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

namespace QoLCompendium.Content.Items.Tools.Summons.CrossMod
{
	// Token: 0x02000193 RID: 403
	public class OldDukeSummon : ModItem
	{
		// Token: 0x06000861 RID: 2145 RVA: 0x00017610 File Offset: 0x00015810
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.BossSummons;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00018589 File Offset: 0x00016789
		public override void SetStaticDefaults()
		{
			ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 19;
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x000185A8 File Offset: 0x000167A8
		public override void SetDefaults()
		{
			base.Item.width = 14;
			base.Item.height = 15;
			base.Item.maxStack = Item.CommonMaxStack;
			base.Item.useAnimation = 30;
			base.Item.useTime = 30;
			base.Item.useStyle = 4;
			base.Item.consumable = true;
			base.Item.shoot = ModContent.ProjectileType<NPCSpawner>();
			base.Item.SetShopValues(ItemRarityColor.StrongRed10, Item.sellPrice(0, 0, 0, 0));
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00018637 File Offset: 0x00016837
		public override bool CanUseItem(Player player)
		{
			return ModConditions.calamityLoaded && ModConditions.DownedPolterghast.IsMet() && player.ZoneBeach && !NPC.AnyNPCs(Common.GetModNPC(ModConditions.calamityMod, "OldDuke"));
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00018670 File Offset: 0x00016870
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 spawnPosition = player.Center - Vector2.UnitY * 800f;
			Projectile.NewProjectile(player.GetSource_ItemUse(base.Item, null), spawnPosition, Vector2.Zero, ModContent.ProjectileType<NPCSpawner>(), 0, 0f, player.whoAmI, (float)Common.GetModNPC(ModConditions.calamityMod, "OldDuke"), 0f, 0f);
			SoundEngine.PlaySound(SoundID.Roar, new Vector2?(player.position), null);
			return false;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0001811C File Offset: 0x0001631C
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.CrossModItems);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000186F4 File Offset: 0x000168F4
		public override void AddRecipes()
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.CrossModItems, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.calamityMod, "BloodwormItem"), 1);
			itemRecipe.AddIngredient(356, 1);
			itemRecipe.AddTile(412);
			itemRecipe.Register();
		}
	}
}
