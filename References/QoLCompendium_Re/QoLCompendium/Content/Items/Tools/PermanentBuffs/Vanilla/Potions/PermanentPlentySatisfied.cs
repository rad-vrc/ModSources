using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Potions
{
	// Token: 0x02000157 RID: 343
	public class PermanentPlentySatisfied : IPermanentBuffItem
	{
		// Token: 0x060006E2 RID: 1762 RVA: 0x000132D4 File Offset: 0x000114D4
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
			this.buffIDForSprite = 206;
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x000132EC File Offset: 0x000114EC
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0001330C File Offset: 0x0001150C
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddRecipeGroup("QoLCompendium:PlentySatisfied", 30);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentWellFed>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00013373 File Offset: 0x00011573
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new PlentySatisfiedEffect());
		}
	}
}
