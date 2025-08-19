using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Stations;
using Terraria;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Stations
{
	// Token: 0x02000136 RID: 310
	public class PermanentAmmoBox : IPermanentBuffItem
	{
		// Token: 0x0600063D RID: 1597 RVA: 0x00011C9D File Offset: 0x0000FE9D
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
			this.buffIDForSprite = 93;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00011CB4 File Offset: 0x0000FEB4
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00011CD4 File Offset: 0x0000FED4
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(2177, 3);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00011D2D File Offset: 0x0000FF2D
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new AmmoBoxEffect());
		}
	}
}
