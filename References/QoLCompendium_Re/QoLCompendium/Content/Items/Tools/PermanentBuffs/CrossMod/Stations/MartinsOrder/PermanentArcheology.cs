using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Stations.MartinsOrder;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Stations.MartinsOrder
{
	// Token: 0x0200009C RID: 156
	[JITWhenModsEnabled(new string[]
	{
		"MartainsOrder"
	})]
	[ExtendsFromMod(new string[]
	{
		"MartainsOrder"
	})]
	public class PermanentArcheology : IPermanentModdedBuffItem
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000A665 File Offset: 0x00008865
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "ReschBuff"));
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000A680 File Offset: 0x00008880
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000A6A0 File Offset: 0x000088A0
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.martainsOrderMod, "ArcheologyTable"), 3);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000A703 File Offset: 0x00008903
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new ArcheologyEffect());
		}
	}
}
