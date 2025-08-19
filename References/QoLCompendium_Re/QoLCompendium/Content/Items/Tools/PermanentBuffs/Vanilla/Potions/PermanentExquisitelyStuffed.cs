using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Potions
{
	// Token: 0x02000145 RID: 325
	public class PermanentExquisitelyStuffed : IPermanentBuffItem
	{
		// Token: 0x06000688 RID: 1672 RVA: 0x000126A8 File Offset: 0x000108A8
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
			this.buffIDForSprite = 207;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x000126C0 File Offset: 0x000108C0
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x000126E0 File Offset: 0x000108E0
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddRecipeGroup("QoLCompendium:ExquisitelyStuffed", 30);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentPlentySatisfied>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00012747 File Offset: 0x00010947
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new ExquisitelyStuffedEffect());
		}
	}
}
