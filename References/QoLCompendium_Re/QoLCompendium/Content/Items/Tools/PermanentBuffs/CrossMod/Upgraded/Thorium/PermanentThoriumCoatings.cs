using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Flasks.Thorium;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.Thorium
{
	// Token: 0x0200007D RID: 125
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class PermanentThoriumCoatings : IPermanentModdedBuffItem
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00008D05 File Offset: 0x00006F05
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentThoriumCoatings";
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00008D0C File Offset: 0x00006F0C
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
			if (player.GetModPlayer<QoLCPlayer>().thoriumCoatingMode == 0)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PermanentThoriumCoatings.DeepFreezeCoating"));
			}
			if (player.GetModPlayer<QoLCPlayer>().thoriumCoatingMode == 1)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PermanentThoriumCoatings.ExplosiveCoating"));
			}
			if (player.GetModPlayer<QoLCPlayer>().thoriumCoatingMode == 2)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PermanentThoriumCoatings.GorgonCoating"));
			}
			if (player.GetModPlayer<QoLCPlayer>().thoriumCoatingMode == 3)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PermanentThoriumCoatings.SporeCoating"));
			}
			if (player.GetModPlayer<QoLCPlayer>().thoriumCoatingMode == 4)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PermanentThoriumCoatings.ToxicCoating"));
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00008DD8 File Offset: 0x00006FD8
		public override void OnConsumeItem(Player player)
		{
			base.Item.stack++;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00002430 File Offset: 0x00000630
		public override bool CanRightClick()
		{
			return true;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00008DED File Offset: 0x00006FED
		public override void RightClick(Player player)
		{
			player.GetModPlayer<QoLCPlayer>().thoriumCoatingMode++;
			if (player.GetModPlayer<QoLCPlayer>().thoriumCoatingMode > 4)
			{
				player.GetModPlayer<QoLCPlayer>().thoriumCoatingMode = 0;
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00008E1C File Offset: 0x0000701C
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentDeepFreezeCoating>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentExplosiveCoating>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentGorgonCoating>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSporeCoating>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentToxicCoating>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00008EA9 File Offset: 0x000070A9
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new ThoriumCoatingEffect());
		}
	}
}
