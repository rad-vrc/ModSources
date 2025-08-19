using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Arena;
using Terraria;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Arena
{
	// Token: 0x02000177 RID: 375
	public class PermanentWaterCandle : IPermanentBuffItem
	{
		// Token: 0x06000783 RID: 1923 RVA: 0x00014838 File Offset: 0x00012A38
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
			this.buffIDForSprite = 86;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00014850 File Offset: 0x00012A50
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00014870 File Offset: 0x00012A70
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(148, 3);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x000148C9 File Offset: 0x00012AC9
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new WaterCandleEffect());
		}
	}
}
