using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Arena.Thorium;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Stations.Thorium;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.Thorium
{
	// Token: 0x02000082 RID: 130
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class PermanentThoriumStations : IPermanentModdedBuffItem
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000200 RID: 512 RVA: 0x000091D7 File Offset: 0x000073D7
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentThoriumStations";
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000091E0 File Offset: 0x000073E0
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00009200 File Offset: 0x00007400
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentAltar>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentConductorsStand>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentMistletoe>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentNinjaRack>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00009280 File Offset: 0x00007480
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new ThoriumStationsEffect());
		}
	}
}
