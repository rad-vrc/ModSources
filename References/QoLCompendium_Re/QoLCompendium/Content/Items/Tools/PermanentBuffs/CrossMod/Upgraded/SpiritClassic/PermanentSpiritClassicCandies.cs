using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.SpiritClassic.Candies;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.SpiritClassic;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.SpiritClassic
{
	// Token: 0x02000086 RID: 134
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class PermanentSpiritClassicCandies : IPermanentModdedBuffItem
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000218 RID: 536 RVA: 0x000094D6 File Offset: 0x000076D6
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentSpiritClassicCandies";
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000094E0 File Offset: 0x000076E0
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00009500 File Offset: 0x00007700
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentCandy>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentChocolateBar>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentHealthCandy>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentLollipop>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentManaCandy>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentTaffy>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000959A File Offset: 0x0000779A
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new SpiritClassicCandyEffect());
		}
	}
}
