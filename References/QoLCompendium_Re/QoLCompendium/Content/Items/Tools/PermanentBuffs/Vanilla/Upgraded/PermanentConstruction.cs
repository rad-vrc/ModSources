using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Potions;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Upgraded
{
	// Token: 0x0200012D RID: 301
	public class PermanentConstruction : IPermanentBuffItem
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x00011350 File Offset: 0x0000F550
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentConstruction";
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00011358 File Offset: 0x0000F558
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00011378 File Offset: 0x0000F578
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentBuilder>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentCalm>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentMining>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x000113EB File Offset: 0x0000F5EB
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new ConstructionEffect());
		}
	}
}
