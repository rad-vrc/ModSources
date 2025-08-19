using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Thorium;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.Thorium
{
	// Token: 0x0200007C RID: 124
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class PermanentThoriumBard : IPermanentModdedBuffItem
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00008C51 File Offset: 0x00006E51
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentThoriumBard";
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00008C58 File Offset: 0x00006E58
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00008C78 File Offset: 0x00006E78
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentCreativity>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentEarworm>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentInspirationalReach>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00008CEB File Offset: 0x00006EEB
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new ThoriumBardEffect());
		}
	}
}
