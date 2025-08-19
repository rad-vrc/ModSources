using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Potions;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Upgraded
{
	// Token: 0x0200012B RID: 299
	public class PermanentAquatic : IPermanentBuffItem
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x00011185 File Offset: 0x0000F385
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentAquatic";
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0001118C File Offset: 0x0000F38C
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x000111AC File Offset: 0x0000F3AC
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentFlipper>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentGills>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentWaterWalking>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0001121F File Offset: 0x0000F41F
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new AquaticEffect());
		}
	}
}
