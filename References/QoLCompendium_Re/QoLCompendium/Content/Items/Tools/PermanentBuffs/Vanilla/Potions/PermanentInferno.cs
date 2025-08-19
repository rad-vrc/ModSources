using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;
using Terraria;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Potions
{
	// Token: 0x0200014D RID: 333
	public class PermanentInferno : IPermanentBuffItem
	{
		// Token: 0x060006B0 RID: 1712 RVA: 0x00012C10 File Offset: 0x00010E10
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
			this.buffIDForSprite = 116;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00012C28 File Offset: 0x00010E28
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00012C48 File Offset: 0x00010E48
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(2348, 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00012CA2 File Offset: 0x00010EA2
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new InfernoEffect());
		}
	}
}
