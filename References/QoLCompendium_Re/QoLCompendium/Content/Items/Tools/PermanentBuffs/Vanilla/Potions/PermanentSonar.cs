using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;
using Terraria;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Potions
{
	// Token: 0x0200015B RID: 347
	public class PermanentSonar : IPermanentBuffItem
	{
		// Token: 0x060006F6 RID: 1782 RVA: 0x0001358C File Offset: 0x0001178C
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
			this.buffIDForSprite = 122;
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x000135A4 File Offset: 0x000117A4
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x000135C4 File Offset: 0x000117C4
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(2355, 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001361E File Offset: 0x0001181E
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new SonarEffect());
		}
	}
}
