using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Thorium;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.Thorium
{
	// Token: 0x0200007F RID: 127
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class PermanentThoriumHealer : IPermanentModdedBuffItem
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00008FA0 File Offset: 0x000071A0
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentThoriumHealer";
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00008FA8 File Offset: 0x000071A8
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00008FC8 File Offset: 0x000071C8
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentArcane>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentGlowing>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentHoly>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000903B File Offset: 0x0000723B
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new ThoriumHealerEffect());
		}
	}
}
