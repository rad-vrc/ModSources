using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;
using Terraria;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Potions
{
	// Token: 0x02000148 RID: 328
	public class PermanentFlipper : IPermanentBuffItem
	{
		// Token: 0x06000697 RID: 1687 RVA: 0x000128B8 File Offset: 0x00010AB8
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
			this.buffIDForSprite = 109;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x000128D0 File Offset: 0x00010AD0
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x000128F0 File Offset: 0x00010AF0
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(2327, 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001294A File Offset: 0x00010B4A
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new FlipperEffect());
		}
	}
}
