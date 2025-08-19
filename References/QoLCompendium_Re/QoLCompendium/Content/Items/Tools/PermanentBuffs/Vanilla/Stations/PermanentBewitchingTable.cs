using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Stations;
using Terraria;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Stations
{
	// Token: 0x02000137 RID: 311
	public class PermanentBewitchingTable : IPermanentBuffItem
	{
		// Token: 0x06000642 RID: 1602 RVA: 0x00011D47 File Offset: 0x0000FF47
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
			this.buffIDForSprite = 150;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00011D60 File Offset: 0x0000FF60
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00011D80 File Offset: 0x0000FF80
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(2999, 3);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00011DD9 File Offset: 0x0000FFD9
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new BewitchingTableEffect());
		}
	}
}
