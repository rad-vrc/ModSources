using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Upgraded
{
	// Token: 0x02000134 RID: 308
	public class PermanentVanilla : IPermanentBuffItem
	{
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x00011AB1 File Offset: 0x0000FCB1
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentVanilla";
			}
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00011AB8 File Offset: 0x0000FCB8
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00011AD8 File Offset: 0x0000FCD8
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentAquatic>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentArena>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentConstruction>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentDamage>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentDefense>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentMovement>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentStation>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentTrawler>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentVision>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00011B99 File Offset: 0x0000FD99
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new VanillaEffect());
		}
	}
}
