using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Clamity;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Clamity
{
	// Token: 0x020000E9 RID: 233
	[JITWhenModsEnabled(new string[]
	{
		"Clamity"
	})]
	[ExtendsFromMod(new string[]
	{
		"Clamity"
	})]
	public class PermanentTitanScale : IPermanentModdedBuffItem
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0000DECA File Offset: 0x0000C0CA
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.clamityAddonMod, Common.GetModBuff(ModConditions.clamityAddonMod, "TitanScalePotionBuff"));
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000DEE8 File Offset: 0x0000C0E8
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000DF08 File Offset: 0x0000C108
		public override void AddRecipes()
		{
			if (!ModConditions.clamityAddonLoaded)
			{
				return;
			}
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.clamityAddonMod, "TitanScalePotion"), 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000DF74 File Offset: 0x0000C174
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new TitanScaleEffect());
		}
	}
}
