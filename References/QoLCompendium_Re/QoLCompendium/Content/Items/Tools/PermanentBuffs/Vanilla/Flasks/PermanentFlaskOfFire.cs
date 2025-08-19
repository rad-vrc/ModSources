using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Flasks;
using Terraria;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Flasks
{
	// Token: 0x02000167 RID: 359
	public class PermanentFlaskOfFire : IPermanentBuffItem
	{
		// Token: 0x06000732 RID: 1842 RVA: 0x00013D98 File Offset: 0x00011F98
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
			this.buffIDForSprite = 74;
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00013DB0 File Offset: 0x00011FB0
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00013DD0 File Offset: 0x00011FD0
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(1354, 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00013E2A File Offset: 0x0001202A
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new WeaponImbueFireEffect());
		}
	}
}
