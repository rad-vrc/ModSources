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
	// Token: 0x02000191 RID: 401
	public class GiantClamSummon : ModItem
	{
		// Token: 0x06000851 RID: 2129 RVA: 0x00017610 File Offset: 0x00015810
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.BossSummons;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x000181C8 File Offset: 0x000163C8
		public override void SetStaticDefaults()
		{
			ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 2;
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x000181E4 File Offset: 0x000163E4
		public override void SetDefaults()
		{
			base.Item.width = 16;
			base.Item.height = 14;
			base.Item.maxStack = Item.CommonMaxStack;
			base.Item.useAnimation = 30;
			base.Item.useTime = 30;
			base.Item.useStyle = 4;
			base.Item.consumable = true;
			base.Item.shoot = ModContent.ProjectileType<NPCSpawner>();
			base.Item.SetShopValues(ItemRarityColor.Pink5, Item.sellPrice(0, 0, 0, 0));
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00018274 File Offset: 0x00016474
		public override bool CanUseItem(Player player)
		{
			ModBiome SunkenSeaBiome;
			return ModConditions.calamityLoaded && ModConditions.calamityMod.TryFind<ModBiome>("SunkenSeaBiome", out SunkenSeaBiome) && SunkenSeaBiome != null && Main.LocalPlayer.InModBiome(SunkenSeaBiome) && ModConditions.DownedDesertScourge.IsMet() && !NPC.AnyNPCs(Common.GetModNPC(ModConditions.calamityMod, "GiantClam"));
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x000182D4 File Offset: 0x000164D4
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 spawnPosition = player.Center - Vector2.UnitY * 800f;
			Projectile.NewProjectile(player.GetSource_ItemUse(base.Item, null), spawnPosition, Vector2.Zero, ModContent.ProjectileType<NPCSpawner>(), 0, 0f, player.whoAmI, (float)Common.GetModNPC(ModConditions.calamityMod, "GiantClam"), 0f, 0f);
			SoundEngine.PlaySound(SoundID.Roar, new Vector2?(player.position), null);
			return false;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0001811C File Offset: 0x0001631C
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.CrossModItems);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00018358 File Offset: 0x00016558
		public override void AddRecipes()
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.CrossModItems, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.calamityMod, "SeaPrism"), 3);
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.calamityMod, "Navystone"), 5);
			itemRecipe.Register();
		}
	}
}
