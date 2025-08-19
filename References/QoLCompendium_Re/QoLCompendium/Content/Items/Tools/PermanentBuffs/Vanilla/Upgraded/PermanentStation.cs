using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Stations;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Upgraded
{
	// Token: 0x02000132 RID: 306
	public class PermanentStation : IPermanentBuffItem
	{
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x00011921 File Offset: 0x0000FB21
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentStation";
			}
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00011928 File Offset: 0x0000FB28
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00011948 File Offset: 0x0000FB48
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentAmmoBox>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentBewitchingTable>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentCrystalBall>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSharpeningStation>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSliceOfCake>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentWarTable>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x000119E2 File Offset: 0x0000FBE2
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new StationEffect());
		}
	}
}
