using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Arena;
using Terraria;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Arena
{
	// Token: 0x02000175 RID: 373
	public class PermanentStarInABottle : IPermanentBuffItem
	{
		// Token: 0x06000779 RID: 1913 RVA: 0x000146E3 File Offset: 0x000128E3
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
			this.buffIDForSprite = 158;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x000146FC File Offset: 0x000128FC
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0001471C File Offset: 0x0001291C
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(1431, 3);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00014775 File Offset: 0x00012975
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new StarInABottleEffect());
		}
	}
}
