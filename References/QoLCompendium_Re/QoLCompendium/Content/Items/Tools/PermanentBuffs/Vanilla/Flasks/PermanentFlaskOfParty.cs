using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Flasks;
using Terraria;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Flasks
{
	// Token: 0x0200016B RID: 363
	public class PermanentFlaskOfParty : IPermanentBuffItem
	{
		// Token: 0x06000746 RID: 1862 RVA: 0x00014048 File Offset: 0x00012248
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
			this.buffIDForSprite = 78;
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00014060 File Offset: 0x00012260
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00014080 File Offset: 0x00012280
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(1358, 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x000140DA File Offset: 0x000122DA
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new WeaponImbueConfettiEffect());
		}
	}
}
