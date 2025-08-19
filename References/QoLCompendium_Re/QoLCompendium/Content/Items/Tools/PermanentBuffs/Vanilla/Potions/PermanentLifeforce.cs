using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;
using Terraria;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Potions
{
	// Token: 0x02000150 RID: 336
	public class PermanentLifeforce : IPermanentBuffItem
	{
		// Token: 0x060006BF RID: 1727 RVA: 0x00012E10 File Offset: 0x00011010
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
			this.buffIDForSprite = 113;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00012E28 File Offset: 0x00011028
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00012E48 File Offset: 0x00011048
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(2345, 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00012EA2 File Offset: 0x000110A2
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new LifeforceEffect());
		}
	}
}
