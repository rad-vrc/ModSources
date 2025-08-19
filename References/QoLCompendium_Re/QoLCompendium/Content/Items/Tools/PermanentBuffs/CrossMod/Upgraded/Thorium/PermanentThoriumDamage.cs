using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Thorium;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.Thorium
{
	// Token: 0x0200007E RID: 126
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class PermanentThoriumDamage : IPermanentModdedBuffItem
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00008EC3 File Offset: 0x000070C3
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentThoriumDamage";
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00008ECC File Offset: 0x000070CC
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00008EEC File Offset: 0x000070EC
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentArtillery>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentBouncingFlames>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentCactusFruit>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentConflagration>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentFrenzy>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentWarmonger>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00008F86 File Offset: 0x00007186
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new ThoriumDamageEffect());
		}
	}
}
