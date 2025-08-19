using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Stations.MartinsOrder;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.MartinsOrder;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.MartinsOrder
{
	// Token: 0x0200008E RID: 142
	[JITWhenModsEnabled(new string[]
	{
		"MartainsOrder"
	})]
	[ExtendsFromMod(new string[]
	{
		"MartainsOrder"
	})]
	public class PermanentMartinsOrderStations : IPermanentModdedBuffItem
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00009BB5 File Offset: 0x00007DB5
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentMartinsOrderStations";
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00009BBC File Offset: 0x00007DBC
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00009BDC File Offset: 0x00007DDC
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentArcheology>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSporeFarm>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00009C42 File Offset: 0x00007E42
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new MartinsOrderStationsEffect());
		}
	}
}
