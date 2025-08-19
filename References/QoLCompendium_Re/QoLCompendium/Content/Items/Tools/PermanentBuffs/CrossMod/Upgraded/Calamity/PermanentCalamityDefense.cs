using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Calamity;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Calamity;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.Calamity
{
	// Token: 0x02000095 RID: 149
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class PermanentCalamityDefense : IPermanentModdedBuffItem
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000A114 File Offset: 0x00008314
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentCalamityDefense";
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000A11C File Offset: 0x0000831C
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000A13C File Offset: 0x0000833C
		public override void AddRecipes()
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentBaguette>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentBloodfin>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentPhotosynthesis>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentTesla>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000A1C4 File Offset: 0x000083C4
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new CalamityDefenseEffect());
		}
	}
}
