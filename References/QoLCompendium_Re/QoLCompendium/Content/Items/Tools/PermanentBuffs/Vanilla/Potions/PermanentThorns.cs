using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;
using Terraria;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Potions
{
	// Token: 0x0200015F RID: 351
	public class PermanentThorns : IPermanentBuffItem
	{
		// Token: 0x0600070A RID: 1802 RVA: 0x00013838 File Offset: 0x00011A38
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
			this.buffIDForSprite = 14;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00013850 File Offset: 0x00011A50
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00013870 File Offset: 0x00011A70
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(301, 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x000138CA File Offset: 0x00011ACA
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new ThornsEffect());
		}
	}
}
