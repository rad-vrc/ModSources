using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.CalamityEntropy;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.CalamityEntropy
{
	// Token: 0x020000EC RID: 236
	[JITWhenModsEnabled(new string[]
	{
		"CalamityEntropy"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityEntropy"
	})]
	public class PermanentSoyMilk : IPermanentModdedBuffItem
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0000E106 File Offset: 0x0000C306
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.calamityEntropyMod, Common.GetModBuff(ModConditions.calamityEntropyMod, "SoyMilkBuff"));
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000E124 File Offset: 0x0000C324
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000E144 File Offset: 0x0000C344
		public override void AddRecipes()
		{
			if (!ModConditions.calamityEntropyLoaded)
			{
				return;
			}
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.calamityEntropyMod, "SoyMilk"), 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000E1B0 File Offset: 0x0000C3B0
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new SoyMilkEffect());
		}
	}
}
