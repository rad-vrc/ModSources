using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Potions;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Upgraded
{
	// Token: 0x02000133 RID: 307
	public class PermanentTrawler : IPermanentBuffItem
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x000119FC File Offset: 0x0000FBFC
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentTrawler";
			}
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00011A04 File Offset: 0x0000FC04
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00011A24 File Offset: 0x0000FC24
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentCrate>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentFishing>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSonar>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00011A97 File Offset: 0x0000FC97
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new TrawlerEffect());
		}
	}
}
