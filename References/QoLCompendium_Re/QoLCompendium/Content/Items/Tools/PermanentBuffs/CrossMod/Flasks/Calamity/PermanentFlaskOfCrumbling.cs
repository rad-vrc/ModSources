using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Flasks.Calamity;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Flasks.Calamity
{
	// Token: 0x0200011A RID: 282
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class PermanentFlaskOfCrumbling : IPermanentModdedBuffItem
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x000104E2 File Offset: 0x0000E6E2
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "WeaponImbueCrumbling"));
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00010500 File Offset: 0x0000E700
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00010520 File Offset: 0x0000E720
		public override void AddRecipes()
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.calamityMod, "FlaskOfCrumbling"), 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001058C File Offset: 0x0000E78C
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new WeaponImbueCrumblingEffect());
		}
	}
}
