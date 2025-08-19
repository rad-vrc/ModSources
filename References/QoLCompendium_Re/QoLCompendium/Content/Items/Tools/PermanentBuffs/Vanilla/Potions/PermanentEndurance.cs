using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;
using Terraria;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Potions
{
	// Token: 0x02000144 RID: 324
	public class PermanentEndurance : IPermanentBuffItem
	{
		// Token: 0x06000683 RID: 1667 RVA: 0x000125FC File Offset: 0x000107FC
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
			this.buffIDForSprite = 114;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00012614 File Offset: 0x00010814
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00012634 File Offset: 0x00010834
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(2346, 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001268E File Offset: 0x0001088E
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new EnduranceEffect());
		}
	}
}
