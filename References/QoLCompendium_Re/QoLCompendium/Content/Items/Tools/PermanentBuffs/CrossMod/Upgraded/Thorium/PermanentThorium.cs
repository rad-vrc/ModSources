using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.Thorium
{
	// Token: 0x0200007B RID: 123
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class PermanentThorium : IPermanentModdedBuffItem
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00008B52 File Offset: 0x00006D52
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentThorium";
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00008B68 File Offset: 0x00006D68
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00008B88 File Offset: 0x00006D88
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentThoriumBard>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentThoriumDamage>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentThoriumHealer>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentThoriumMovement>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentThoriumRepellent>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentThoriumStations>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentThoriumThrower>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00008C2F File Offset: 0x00006E2F
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new ThoriumEffect());
		}
	}
}
