using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Arena.CalamityEntropy;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.CalamityEntropy;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.CalamityEntropy;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.CalamityEntropy
{
	// Token: 0x02000090 RID: 144
	[JITWhenModsEnabled(new string[]
	{
		"CalamityEntropy"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityEntropy"
	})]
	public class PermanentCalamityEntropy : IPermanentModdedBuffItem
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00009D19 File Offset: 0x00007F19
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentCalamityEntropy";
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00009D20 File Offset: 0x00007F20
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00009D40 File Offset: 0x00007F40
		public override void AddRecipes()
		{
			if (!ModConditions.calamityEntropyLoaded)
			{
				return;
			}
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentVoidCandle>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentYharimsStimulants>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSoyMilk>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00009DBB File Offset: 0x00007FBB
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new CalamityEntropyEffect());
		}
	}
}
