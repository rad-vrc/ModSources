using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Arena;
using Terraria;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Arena
{
	// Token: 0x02000176 RID: 374
	public class PermanentSunflower : IPermanentBuffItem
	{
		// Token: 0x0600077E RID: 1918 RVA: 0x0001478F File Offset: 0x0001298F
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
			this.buffIDForSprite = 146;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x000147A8 File Offset: 0x000129A8
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x000147C8 File Offset: 0x000129C8
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(63, 3);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001481E File Offset: 0x00012A1E
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new SunflowerEffect());
		}
	}
}
