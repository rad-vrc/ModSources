using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Calamity;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.Calamity
{
	// Token: 0x02000091 RID: 145
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class PermanentCalamity : IPermanentModdedBuffItem
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00009DD5 File Offset: 0x00007FD5
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentCalamity";
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00009DDC File Offset: 0x00007FDC
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00009DFC File Offset: 0x00007FFC
		public override void AddRecipes()
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentCalamityAbyss>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentCalamityArena>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentCalamityDamage>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentCalamityDefense>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentCalamityFarming>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentCalamityMovement>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00009E9E File Offset: 0x0000809E
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new CalamityEffect());
		}
	}
}
