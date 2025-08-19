using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.MartinsOrder;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.MartinsOrder
{
	// Token: 0x020000E0 RID: 224
	[JITWhenModsEnabled(new string[]
	{
		"MartainsOrder"
	})]
	[ExtendsFromMod(new string[]
	{
		"MartainsOrder"
	})]
	public class PermanentStarreach : IPermanentModdedBuffItem
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000D81E File Offset: 0x0000BA1E
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "Starreach"));
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000D83C File Offset: 0x0000BA3C
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000D85C File Offset: 0x0000BA5C
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.martainsOrderMod, "StarreachPotion"), 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000D8C0 File Offset: 0x0000BAC0
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new StarreachEffect());
		}
	}
}
