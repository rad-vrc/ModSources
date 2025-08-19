using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Potions;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Upgraded
{
	// Token: 0x02000135 RID: 309
	public class PermanentVision : IPermanentBuffItem
	{
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x00011BB3 File Offset: 0x0000FDB3
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentVision";
			}
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00011BBC File Offset: 0x0000FDBC
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00011BDC File Offset: 0x0000FDDC
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentBiomeSight>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentDangersense>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentHunter>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentInvisibility>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentNightOwl>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentShine>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSpelunker>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00011C83 File Offset: 0x0000FE83
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new VisionEffect());
		}
	}
}
