using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Arena;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Upgraded
{
	// Token: 0x0200012C RID: 300
	public class PermanentArena : IPermanentBuffItem
	{
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x00011241 File Offset: 0x0000F441
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentArena";
			}
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00011248 File Offset: 0x0000F448
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00011268 File Offset: 0x0000F468
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentBastStatue>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentCampfire>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentGardenGnome>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentHeartLantern>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentHoney>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentPeaceCandle>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentShadowCandle>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentStarInABottle>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSunflower>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentWaterCandle>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00011336 File Offset: 0x0000F536
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new ArenaEffect());
		}
	}
}
