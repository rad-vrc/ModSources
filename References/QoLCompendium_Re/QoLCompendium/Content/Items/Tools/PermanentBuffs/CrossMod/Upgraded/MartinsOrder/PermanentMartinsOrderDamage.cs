using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.MartinsOrder;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.MartinsOrder;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.MartinsOrder
{
	// Token: 0x0200008C RID: 140
	[JITWhenModsEnabled(new string[]
	{
		"MartainsOrder"
	})]
	[ExtendsFromMod(new string[]
	{
		"MartainsOrder"
	})]
	public class PermanentMartinsOrderDamage : IPermanentModdedBuffItem
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600023C RID: 572 RVA: 0x000099BD File Offset: 0x00007BBD
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentMartinsOrderDamage";
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000099C4 File Offset: 0x00007BC4
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000099E4 File Offset: 0x00007BE4
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentDefender>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentEmpowerment>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentEvocation>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentHaste>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentShooter>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSpellCaster>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentStarreach>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSweeper>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentThrower>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentWhipper>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00009AB2 File Offset: 0x00007CB2
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new MartinsOrderDamageEffect());
		}
	}
}
