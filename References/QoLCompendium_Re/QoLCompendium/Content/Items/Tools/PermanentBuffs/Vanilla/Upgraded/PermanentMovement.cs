using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Potions;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Upgraded
{
	// Token: 0x02000131 RID: 305
	public class PermanentMovement : IPermanentBuffItem
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x0001186A File Offset: 0x0000FA6A
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentMovement";
			}
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00011874 File Offset: 0x0000FA74
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00011894 File Offset: 0x0000FA94
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentFeatherfall>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentGravitation>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSwiftness>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00011907 File Offset: 0x0000FB07
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new MovementEffect());
		}
	}
}
