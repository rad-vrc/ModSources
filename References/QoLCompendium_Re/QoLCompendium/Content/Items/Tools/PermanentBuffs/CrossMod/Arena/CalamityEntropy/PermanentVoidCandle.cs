using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.CalamityEntropy;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Arena.CalamityEntropy
{
	// Token: 0x02000121 RID: 289
	[JITWhenModsEnabled(new string[]
	{
		"CalamityEntropy"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityEntropy"
	})]
	public class PermanentVoidCandle : IPermanentModdedBuffItem
	{
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x00010A05 File Offset: 0x0000EC05
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.calamityEntropyMod, Common.GetModBuff(ModConditions.calamityEntropyMod, "VoidCandleBuff"));
			}
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00010A20 File Offset: 0x0000EC20
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00010A40 File Offset: 0x0000EC40
		public override void AddRecipes()
		{
			if (!ModConditions.calamityEntropyLoaded)
			{
				return;
			}
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.calamityEntropyMod, "VoidCandle"), 3);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00010AAB File Offset: 0x0000ECAB
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new VoidCandleEffect());
		}
	}
}
