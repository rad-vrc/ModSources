using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.MartinsOrder;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.MartinsOrder;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.MartinsOrder
{
	// Token: 0x0200008D RID: 141
	[JITWhenModsEnabled(new string[]
	{
		"MartainsOrder"
	})]
	[ExtendsFromMod(new string[]
	{
		"MartainsOrder"
	})]
	public class PermanentMartinsOrderDefense : IPermanentModdedBuffItem
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00009ACC File Offset: 0x00007CCC
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentMartinsOrderDefense";
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00009AD4 File Offset: 0x00007CD4
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00009AF4 File Offset: 0x00007CF4
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentBlackHole>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentCharging>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentGourmetFlavor>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentHealing>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentRockskin>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSoul>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentZincPill>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00009B9B File Offset: 0x00007D9B
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new MartinsOrderDefenseEffect());
		}
	}
}
