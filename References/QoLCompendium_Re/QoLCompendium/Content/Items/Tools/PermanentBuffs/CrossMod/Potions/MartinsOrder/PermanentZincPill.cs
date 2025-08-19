using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.MartinsOrder;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.MartinsOrder
{
	// Token: 0x020000E4 RID: 228
	[JITWhenModsEnabled(new string[]
	{
		"MartainsOrder"
	})]
	[ExtendsFromMod(new string[]
	{
		"MartainsOrder"
	})]
	public class PermanentZincPill : IPermanentModdedBuffItem
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0000DB0E File Offset: 0x0000BD0E
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "ZincPillBuff"));
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000DB2C File Offset: 0x0000BD2C
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000DB4C File Offset: 0x0000BD4C
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.martainsOrderMod, "ZincPill"), 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000DBB0 File Offset: 0x0000BDB0
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new ZincPillEffect());
		}
	}
}
