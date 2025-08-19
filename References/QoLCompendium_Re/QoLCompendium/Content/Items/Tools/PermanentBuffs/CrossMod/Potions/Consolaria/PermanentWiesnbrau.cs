using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Consolaria;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Consolaria
{
	// Token: 0x020000E5 RID: 229
	[JITWhenModsEnabled(new string[]
	{
		"Consolaria"
	})]
	[ExtendsFromMod(new string[]
	{
		"Consolaria"
	})]
	public class PermanentWiesnbrau : IPermanentModdedBuffItem
	{
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x0000DBCA File Offset: 0x0000BDCA
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.consolariaMod, Common.GetModBuff(ModConditions.consolariaMod, "Drunk"));
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000DBE8 File Offset: 0x0000BDE8
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000DC08 File Offset: 0x0000BE08
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.consolariaMod, "Wiesnbrau"), 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000DC6C File Offset: 0x0000BE6C
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new WiesnbrauEffect());
		}
	}
}
