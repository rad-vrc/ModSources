using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.SpiritClassic;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.SpiritClassic
{
	// Token: 0x02000084 RID: 132
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class PermanentSpiritClassic : IPermanentModdedBuffItem
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00009344 File Offset: 0x00007544
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentSpiritClassic";
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000934C File Offset: 0x0000754C
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000936C File Offset: 0x0000756C
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSpiritClassicArena>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSpiritClassicCandies>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSpiritClassicDamage>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSpiritClassicDefense>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSpiritClassicMovement>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000093F9 File Offset: 0x000075F9
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new SpiritClassicEffect());
		}
	}
}
