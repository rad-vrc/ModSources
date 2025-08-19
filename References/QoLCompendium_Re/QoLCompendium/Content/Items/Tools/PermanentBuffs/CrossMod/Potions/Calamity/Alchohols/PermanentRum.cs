using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity.Alchohols;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Calamity.Alchohols
{
	// Token: 0x0200010B RID: 267
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class PermanentRum : IPermanentModdedBuffItem
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x0000F98E File Offset: 0x0000DB8E
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "RumBuff"));
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0000F9AC File Offset: 0x0000DBAC
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0000F9CC File Offset: 0x0000DBCC
		public override void AddRecipes()
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.calamityMod, "Rum"), 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0000FA38 File Offset: 0x0000DC38
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new RumEffect());
		}
	}
}
