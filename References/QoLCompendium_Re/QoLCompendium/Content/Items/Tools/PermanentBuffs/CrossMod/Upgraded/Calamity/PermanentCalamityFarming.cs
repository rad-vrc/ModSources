using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Arena.Calamity;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Calamity;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Calamity;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.Calamity
{
	// Token: 0x02000096 RID: 150
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class PermanentCalamityFarming : IPermanentModdedBuffItem
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000A1DE File Offset: 0x000083DE
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentCalamityFarming";
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000A1E8 File Offset: 0x000083E8
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000A208 File Offset: 0x00008408
		public override void AddRecipes()
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentZen>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentTranquilityCandle>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentZerg>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentChaosCandle>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentCeaselessHunger>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000A29D File Offset: 0x0000849D
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new CalamityFarmingEffect());
		}
	}
}
