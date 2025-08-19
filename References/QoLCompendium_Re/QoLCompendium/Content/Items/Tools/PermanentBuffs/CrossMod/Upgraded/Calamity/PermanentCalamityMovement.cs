using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Calamity;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Calamity;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.Calamity
{
	// Token: 0x02000097 RID: 151
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class PermanentCalamityMovement : IPermanentModdedBuffItem
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000A2B7 File Offset: 0x000084B7
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentCalamityMovement";
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000A2C0 File Offset: 0x000084C0
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000A2E0 File Offset: 0x000084E0
		public override void AddRecipes()
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentBounding>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentCalcium>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentGravityNormalizer>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSoaring>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000A368 File Offset: 0x00008568
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new CalamityMovementEffect());
		}
	}
}
