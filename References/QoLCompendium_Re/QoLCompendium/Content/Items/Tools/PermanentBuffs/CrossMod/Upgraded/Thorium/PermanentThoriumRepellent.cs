using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Thorium;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.Thorium
{
	// Token: 0x02000081 RID: 129
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class PermanentThoriumRepellent : IPermanentModdedBuffItem
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00009109 File Offset: 0x00007309
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentThoriumRepellent";
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00009110 File Offset: 0x00007310
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00009130 File Offset: 0x00007330
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentBatRepellent>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentFishRepellent>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentInsectRepellent>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSkeletonRepellent>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentZombieRepellent>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000091BD File Offset: 0x000073BD
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new ThoriumRepellentEffect());
		}
	}
}
