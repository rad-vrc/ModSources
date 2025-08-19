using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Potions;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Upgraded
{
	// Token: 0x0200012F RID: 303
	public class PermanentDefense : IPermanentBuffItem
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x00011521 File Offset: 0x0000F721
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentDefense";
			}
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00011528 File Offset: 0x0000F728
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00011548 File Offset: 0x0000F748
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentEndurance>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentExquisitelyStuffed>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentHeartreach>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentInferno>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentIronskin>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentLifeforce>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentObsidianSkin>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentRegeneration>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentThorns>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentWarmth>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00011616 File Offset: 0x0000F816
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new DefenseEffect());
		}
	}
}
