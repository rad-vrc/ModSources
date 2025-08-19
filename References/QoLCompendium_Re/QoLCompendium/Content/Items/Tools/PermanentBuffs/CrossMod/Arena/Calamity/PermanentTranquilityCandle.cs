using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.Calamity;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Arena.Calamity
{
	// Token: 0x02000128 RID: 296
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class PermanentTranquilityCandle : IPermanentModdedBuffItem
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x00010F45 File Offset: 0x0000F145
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "TranquilityCandleBuff"));
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00010F60 File Offset: 0x0000F160
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00010F80 File Offset: 0x0000F180
		public override void AddRecipes()
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.calamityMod, "TranquilityCandle"), 3);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00010FEB File Offset: 0x0000F1EB
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new TranquilityCandleEffect());
		}
	}
}
