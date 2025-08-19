using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Catalyst;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Catalyst
{
	// Token: 0x020000EA RID: 234
	[JITWhenModsEnabled(new string[]
	{
		"CatalystMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CatalystMod"
	})]
	public class PermanentAstracola : IPermanentModdedBuffItem
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0000DF8E File Offset: 0x0000C18E
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.catalystMod, Common.GetModBuff(ModConditions.catalystMod, "AstralJellyBuff"));
			}
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000DFAC File Offset: 0x0000C1AC
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000DFCC File Offset: 0x0000C1CC
		public override void AddRecipes()
		{
			if (!ModConditions.catalystLoaded)
			{
				return;
			}
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.catalystMod, "Lean"), 30);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentAstraJelly>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000E045 File Offset: 0x0000C245
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new AstracolaEffect());
		}
	}
}
