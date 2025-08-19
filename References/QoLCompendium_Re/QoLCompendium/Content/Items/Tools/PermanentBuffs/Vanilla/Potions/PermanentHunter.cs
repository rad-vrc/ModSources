using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;
using Terraria;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Potions
{
	// Token: 0x0200014C RID: 332
	public class PermanentHunter : IPermanentBuffItem
	{
		// Token: 0x060006AB RID: 1707 RVA: 0x00012B64 File Offset: 0x00010D64
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
			this.buffIDForSprite = 17;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00012B7C File Offset: 0x00010D7C
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00012B9C File Offset: 0x00010D9C
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(304, 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00012BF6 File Offset: 0x00010DF6
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new HunterEffect());
		}
	}
}
