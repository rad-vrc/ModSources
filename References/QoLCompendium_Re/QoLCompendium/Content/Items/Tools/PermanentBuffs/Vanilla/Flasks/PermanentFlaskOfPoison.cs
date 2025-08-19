using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Flasks;
using Terraria;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Flasks
{
	// Token: 0x0200016C RID: 364
	public class PermanentFlaskOfPoison : IPermanentBuffItem
	{
		// Token: 0x0600074B RID: 1867 RVA: 0x000140F4 File Offset: 0x000122F4
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
			this.buffIDForSprite = 79;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0001410C File Offset: 0x0001230C
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0001412C File Offset: 0x0001232C
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(1359, 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00014186 File Offset: 0x00012386
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new WeaponImbuePoisonEffect());
		}
	}
}
