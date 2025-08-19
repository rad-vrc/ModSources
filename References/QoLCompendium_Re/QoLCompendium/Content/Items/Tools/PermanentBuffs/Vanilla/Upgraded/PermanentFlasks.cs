using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Flasks;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Upgraded
{
	// Token: 0x02000130 RID: 304
	public class PermanentFlasks : IPermanentBuffItem
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x00011630 File Offset: 0x0000F830
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentFlasks";
			}
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00011638 File Offset: 0x0000F838
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
			if (player.GetModPlayer<QoLCPlayer>().flaskEffectMode == 0)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PermanentFlasks.FlaskOfCursedFlames"));
			}
			if (player.GetModPlayer<QoLCPlayer>().flaskEffectMode == 1)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PermanentFlasks.FlaskOfFire"));
			}
			if (player.GetModPlayer<QoLCPlayer>().flaskEffectMode == 2)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PermanentFlasks.FlaskOfGold"));
			}
			if (player.GetModPlayer<QoLCPlayer>().flaskEffectMode == 3)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PermanentFlasks.FlaskOfIchor"));
			}
			if (player.GetModPlayer<QoLCPlayer>().flaskEffectMode == 4)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PermanentFlasks.FlaskOfNanites"));
			}
			if (player.GetModPlayer<QoLCPlayer>().flaskEffectMode == 5)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PermanentFlasks.FlaskOfParty"));
			}
			if (player.GetModPlayer<QoLCPlayer>().flaskEffectMode == 6)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PermanentFlasks.FlaskOfPoison"));
			}
			if (player.GetModPlayer<QoLCPlayer>().flaskEffectMode == 7)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PermanentFlasks.FlaskOfVenom"));
			}
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00008DD8 File Offset: 0x00006FD8
		public override void OnConsumeItem(Player player)
		{
			base.Item.stack++;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00002430 File Offset: 0x00000630
		public override bool CanRightClick()
		{
			return true;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001176D File Offset: 0x0000F96D
		public override void RightClick(Player player)
		{
			player.GetModPlayer<QoLCPlayer>().flaskEffectMode++;
			if (player.GetModPlayer<QoLCPlayer>().flaskEffectMode > 7)
			{
				player.GetModPlayer<QoLCPlayer>().flaskEffectMode = 0;
			}
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001179C File Offset: 0x0000F99C
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentFlaskOfCursedFlames>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentFlaskOfFire>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentFlaskOfGold>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentFlaskOfIchor>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentFlaskOfNanites>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentFlaskOfParty>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentFlaskOfPoison>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentFlaskOfVenom>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00011850 File Offset: 0x0000FA50
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.potionEffects.Add(new FlaskEffect());
		}
	}
}
