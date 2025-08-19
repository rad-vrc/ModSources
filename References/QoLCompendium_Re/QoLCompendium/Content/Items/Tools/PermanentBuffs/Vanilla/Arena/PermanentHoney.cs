using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Arena;
using Terraria;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Arena
{
	// Token: 0x02000172 RID: 370
	public class PermanentHoney : IPermanentBuffItem
	{
		// Token: 0x0600076A RID: 1898 RVA: 0x000144E3 File Offset: 0x000126E3
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
			this.buffIDForSprite = 48;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x000144F8 File Offset: 0x000126F8
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00014518 File Offset: 0x00012718
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(1128, 3);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00014571 File Offset: 0x00012771
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new HoneyEffect());
		}
	}
}
