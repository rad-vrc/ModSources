using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;
using Terraria;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Potions
{
	// Token: 0x02000143 RID: 323
	public class PermanentDangersense : IPermanentBuffItem
	{
		// Token: 0x0600067E RID: 1662 RVA: 0x00012550 File Offset: 0x00010750
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
			this.buffIDForSprite = 111;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00012568 File Offset: 0x00010768
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00012588 File Offset: 0x00010788
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(2329, 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x000125E2 File Offset: 0x000107E2
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new DangersenseEffect());
		}
	}
}
