using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Arena;
using Terraria;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Arena
{
	// Token: 0x0200016F RID: 367
	public class PermanentCampfire : IPermanentBuffItem
	{
		// Token: 0x0600075A RID: 1882 RVA: 0x000142F7 File Offset: 0x000124F7
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
			this.buffIDForSprite = 87;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001430C File Offset: 0x0001250C
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0001432C File Offset: 0x0001252C
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(966, 3);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00014385 File Offset: 0x00012585
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new CampfireEffect());
		}
	}
}
