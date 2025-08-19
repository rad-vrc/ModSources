using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Stations;
using Terraria;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Stations
{
	// Token: 0x02000139 RID: 313
	public class PermanentSharpeningStation : IPermanentBuffItem
	{
		// Token: 0x0600064C RID: 1612 RVA: 0x00011E9B File Offset: 0x0001009B
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
			this.buffIDForSprite = 159;
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00011EB4 File Offset: 0x000100B4
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00011ED4 File Offset: 0x000100D4
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(3198, 3);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00011F2D File Offset: 0x0001012D
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new SharpeningStationEffect());
		}
	}
}
