using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Flasks;
using Terraria;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Flasks
{
	// Token: 0x0200016A RID: 362
	public class PermanentFlaskOfNanites : IPermanentBuffItem
	{
		// Token: 0x06000741 RID: 1857 RVA: 0x00013F9C File Offset: 0x0001219C
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
			this.buffIDForSprite = 77;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00013FB4 File Offset: 0x000121B4
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00013FD4 File Offset: 0x000121D4
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(1357, 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001402E File Offset: 0x0001222E
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new WeaponImbueNanitesEffect());
		}
	}
}
