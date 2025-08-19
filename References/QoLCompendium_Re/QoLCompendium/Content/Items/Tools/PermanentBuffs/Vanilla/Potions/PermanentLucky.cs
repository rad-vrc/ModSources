using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;
using Terraria;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Potions
{
	// Token: 0x02000151 RID: 337
	public class PermanentLucky : IPermanentBuffItem
	{
		// Token: 0x060006C4 RID: 1732 RVA: 0x00012EBC File Offset: 0x000110BC
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
			this.buffIDForSprite = 257;
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00012ED4 File Offset: 0x000110D4
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00012EF4 File Offset: 0x000110F4
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(4477, 30);
			itemRecipe.AddIngredient(4478, 20);
			itemRecipe.AddIngredient(4479, 10);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00012F6A File Offset: 0x0001116A
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new LuckyEffect());
		}
	}
}
