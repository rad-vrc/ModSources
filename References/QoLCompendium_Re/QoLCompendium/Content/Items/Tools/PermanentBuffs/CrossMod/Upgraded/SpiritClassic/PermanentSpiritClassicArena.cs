using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Arena.SpiritClassic;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.SpiritClassic;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.SpiritClassic
{
	// Token: 0x02000085 RID: 133
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class PermanentSpiritClassicArena : IPermanentModdedBuffItem
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00009413 File Offset: 0x00007613
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentSpiritClassicArena";
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000941C File Offset: 0x0000761C
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000943C File Offset: 0x0000763C
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentCoiledEnergizer>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentKoiTotem>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSunPot>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentTheCouch>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000094BC File Offset: 0x000076BC
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new SpiritClassicArenaEffect());
		}
	}
}
