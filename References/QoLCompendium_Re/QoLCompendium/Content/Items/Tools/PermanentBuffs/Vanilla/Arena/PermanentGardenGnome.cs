using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Arena;
using Terraria;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Arena
{
	// Token: 0x02000170 RID: 368
	public class PermanentGardenGnome : IPermanentBuffItem
	{
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x0001439F File Offset: 0x0001259F
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentGardenGnome";
			}
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x000143A8 File Offset: 0x000125A8
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x000143C8 File Offset: 0x000125C8
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(4609, 3);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00014421 File Offset: 0x00012621
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new GardenGnomeEffect());
		}
	}
}
