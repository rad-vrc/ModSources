using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Calamity;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Calamity;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.Calamity
{
	// Token: 0x02000092 RID: 146
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class PermanentCalamityAbyss : IPermanentModdedBuffItem
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00009EB8 File Offset: 0x000080B8
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentCalamityAbyss";
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00009EC0 File Offset: 0x000080C0
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00009EE0 File Offset: 0x000080E0
		public override void AddRecipes()
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentAnechoicCoating>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentOmniscience>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSulphurskin>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00009F5B File Offset: 0x0000815B
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new CalamityAbyssEffect());
		}
	}
}
