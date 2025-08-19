using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity.Alchohols;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Calamity.Alchohols
{
	// Token: 0x0200010A RID: 266
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class PermanentRedWine : IPermanentModdedBuffItem
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0000F8CA File Offset: 0x0000DACA
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "RedWineBuff"));
			}
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0000F8E8 File Offset: 0x0000DAE8
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0000F908 File Offset: 0x0000DB08
		public override void AddRecipes()
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.calamityMod, "RedWine"), 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0000F974 File Offset: 0x0000DB74
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new RedWineEffect());
		}
	}
}
