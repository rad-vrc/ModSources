using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.CalamityEntropy;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.CalamityEntropy
{
	// Token: 0x020000ED RID: 237
	[JITWhenModsEnabled(new string[]
	{
		"CalamityEntropy"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityEntropy"
	})]
	public class PermanentYharimsStimulants : IPermanentModdedBuffItem
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0000E1CA File Offset: 0x0000C3CA
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.calamityEntropyMod, Common.GetModBuff(ModConditions.calamityEntropyMod, "YharimPower"));
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000E1E8 File Offset: 0x0000C3E8
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000E208 File Offset: 0x0000C408
		public override void AddRecipes()
		{
			if (!ModConditions.calamityEntropyLoaded)
			{
				return;
			}
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.calamityEntropyMod, "YharimsStimulants"), 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000E274 File Offset: 0x0000C474
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new YharimsStimulantsEffect());
		}
	}
}
