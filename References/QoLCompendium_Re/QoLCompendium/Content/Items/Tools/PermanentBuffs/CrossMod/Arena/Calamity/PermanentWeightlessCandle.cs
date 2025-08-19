using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.Calamity;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Arena.Calamity
{
	// Token: 0x0200012A RID: 298
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class PermanentWeightlessCandle : IPermanentModdedBuffItem
	{
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x000110C5 File Offset: 0x0000F2C5
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "BlueCandleBuff"));
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x000110E0 File Offset: 0x0000F2E0
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00011100 File Offset: 0x0000F300
		public override void AddRecipes()
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.calamityMod, "WeightlessCandle"), 3);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001116B File Offset: 0x0000F36B
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new WeightlessCandleEffect());
		}
	}
}
