using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Clamity;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Clamity
{
	// Token: 0x020000E7 RID: 231
	[JITWhenModsEnabled(new string[]
	{
		"Clamity"
	})]
	[ExtendsFromMod(new string[]
	{
		"Clamity"
	})]
	public class PermanentExoBaguette : IPermanentModdedBuffItem
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000DD42 File Offset: 0x0000BF42
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.clamityAddonMod, Common.GetModBuff(ModConditions.clamityAddonMod, "ExoBaguetteBuff"));
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000DD60 File Offset: 0x0000BF60
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000DD80 File Offset: 0x0000BF80
		public override void AddRecipes()
		{
			if (!ModConditions.clamityAddonLoaded)
			{
				return;
			}
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.clamityAddonMod, "ExoBaguette"), 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000DDEC File Offset: 0x0000BFEC
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new ExoBaguetteEffect());
		}
	}
}
