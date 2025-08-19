using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.SOTS;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Stations.SOTS;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.SOTS;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.SOTS
{
	// Token: 0x0200008A RID: 138
	[JITWhenModsEnabled(new string[]
	{
		"SOTS"
	})]
	[ExtendsFromMod(new string[]
	{
		"SOTS"
	})]
	public class PermanentSecretsOfTheShadows : IPermanentModdedBuffItem
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000230 RID: 560 RVA: 0x000097ED File Offset: 0x000079ED
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentSecretsOfTheShadows";
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000097F4 File Offset: 0x000079F4
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00009814 File Offset: 0x00007A14
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentAssassination>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentBluefire>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentBrittle>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentDoubleVision>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentHarmony>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentNightmare>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentRipple>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentRoughskin>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSoulAccess>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentVibe>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentDigitalDisplay>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000234 RID: 564 RVA: 0x000098EF File Offset: 0x00007AEF
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new SecretsOfTheShadowsEffect());
		}
	}
}
