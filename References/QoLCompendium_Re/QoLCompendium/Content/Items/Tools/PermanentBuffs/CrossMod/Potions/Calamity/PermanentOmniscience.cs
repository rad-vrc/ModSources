using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Potions;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Calamity
{
	// Token: 0x020000F6 RID: 246
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class PermanentOmniscience : IPermanentModdedBuffItem
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x0000E8AE File Offset: 0x0000CAAE
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "Omniscience"));
			}
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0000E8CC File Offset: 0x0000CACC
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000E8EC File Offset: 0x0000CAEC
		public override void AddRecipes()
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.calamityMod, "PotionofOmniscience"), 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
			Recipe itemRecipe2 = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe2.AddIngredient(304, 30);
			itemRecipe2.AddIngredient(296, 30);
			itemRecipe2.AddIngredient(2329, 30);
			itemRecipe2.AddTile(96);
			itemRecipe2.Register();
			Recipe itemRecipe3 = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe3.AddIngredient(ModContent.ItemType<PermanentHunter>(), 1);
			itemRecipe3.AddIngredient(ModContent.ItemType<PermanentSpelunker>(), 1);
			itemRecipe3.AddIngredient(ModContent.ItemType<PermanentDangersense>(), 1);
			itemRecipe3.AddTile(96);
			itemRecipe3.Register();
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0000EA27 File Offset: 0x0000CC27
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new OmniscienceEffect());
		}
	}
}
