using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SpiritClassic;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.SpiritClassic
{
	// Token: 0x020000BF RID: 191
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class PermanentSporecoid : IPermanentModdedBuffItem
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000BFEE File Offset: 0x0000A1EE
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "MushroomPotionBuff"));
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000C00C File Offset: 0x0000A20C
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000C02C File Offset: 0x0000A22C
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.spiritMod, "MushroomPotion"), 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000C090 File Offset: 0x0000A290
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new SporecoidEffect());
		}
	}
}
