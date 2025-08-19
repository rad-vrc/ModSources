using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Arena;
using Terraria;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Arena
{
	// Token: 0x02000174 RID: 372
	public class PermanentShadowCandle : IPermanentBuffItem
	{
		// Token: 0x06000774 RID: 1908 RVA: 0x00014637 File Offset: 0x00012837
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
			this.buffIDForSprite = 350;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00014650 File Offset: 0x00012850
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00014670 File Offset: 0x00012870
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(5322, 3);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x000146C9 File Offset: 0x000128C9
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new ShadowCandleEffect());
		}
	}
}
