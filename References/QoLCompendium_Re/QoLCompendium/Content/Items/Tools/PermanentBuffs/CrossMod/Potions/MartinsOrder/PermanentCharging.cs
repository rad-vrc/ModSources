using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.MartinsOrder;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.MartinsOrder
{
	// Token: 0x020000D5 RID: 213
	[JITWhenModsEnabled(new string[]
	{
		"MartainsOrder"
	})]
	[ExtendsFromMod(new string[]
	{
		"MartainsOrder"
	})]
	public class PermanentCharging : IPermanentModdedBuffItem
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000D016 File Offset: 0x0000B216
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "Charging"));
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000D034 File Offset: 0x0000B234
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000D054 File Offset: 0x0000B254
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.martainsOrderMod, "BlueBerryJam"), 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000D0B8 File Offset: 0x0000B2B8
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new ChargingEffect());
		}
	}
}
