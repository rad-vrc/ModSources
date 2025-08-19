using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;
using Terraria;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Potions
{
	// Token: 0x0200014F RID: 335
	public class PermanentIronskin : IPermanentBuffItem
	{
		// Token: 0x060006BA RID: 1722 RVA: 0x00012D68 File Offset: 0x00010F68
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
			this.buffIDForSprite = 5;
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00012D7C File Offset: 0x00010F7C
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00012D9C File Offset: 0x00010F9C
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(292, 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00012DF6 File Offset: 0x00010FF6
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new IronskinEffect());
		}
	}
}
