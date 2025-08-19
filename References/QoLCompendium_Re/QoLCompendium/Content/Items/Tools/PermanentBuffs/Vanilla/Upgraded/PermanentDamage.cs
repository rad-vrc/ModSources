using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Potions;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Upgraded
{
	// Token: 0x0200012E RID: 302
	public class PermanentDamage : IPermanentBuffItem
	{
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x00011405 File Offset: 0x0000F605
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentDamage";
			}
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001140C File Offset: 0x0000F60C
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0001142C File Offset: 0x0000F62C
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentAmmoReservation>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentArchery>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentBattle>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentLucky>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentMagicPower>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentManaRegeneration>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSummoning>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentTipsy>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentTitan>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentRage>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentWrath>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00011507 File Offset: 0x0000F707
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new DamageEffect());
		}
	}
}
