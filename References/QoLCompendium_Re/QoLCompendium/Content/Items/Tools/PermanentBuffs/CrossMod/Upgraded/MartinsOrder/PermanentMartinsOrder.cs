using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.MartinsOrder;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.MartinsOrder
{
	// Token: 0x0200008B RID: 139
	[JITWhenModsEnabled(new string[]
	{
		"MartainsOrder"
	})]
	[ExtendsFromMod(new string[]
	{
		"MartainsOrder"
	})]
	public class PermanentMartinsOrder : IPermanentModdedBuffItem
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00009909 File Offset: 0x00007B09
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentMartinsOrder";
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00009910 File Offset: 0x00007B10
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00009930 File Offset: 0x00007B30
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentMartinsOrderDamage>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentMartinsOrderDefense>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentMartinsOrderStations>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000099A3 File Offset: 0x00007BA3
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new MartinsOrderEffect());
		}
	}
}
