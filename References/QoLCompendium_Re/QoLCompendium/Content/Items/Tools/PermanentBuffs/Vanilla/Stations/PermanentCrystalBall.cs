using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Stations;
using Terraria;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Stations
{
	// Token: 0x02000138 RID: 312
	public class PermanentCrystalBall : IPermanentBuffItem
	{
		// Token: 0x06000647 RID: 1607 RVA: 0x00011DF3 File Offset: 0x0000FFF3
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
			this.buffIDForSprite = 29;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00011E08 File Offset: 0x00010008
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00011E28 File Offset: 0x00010028
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(487, 3);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00011E81 File Offset: 0x00010081
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new CrystalBallEffect());
		}
	}
}
