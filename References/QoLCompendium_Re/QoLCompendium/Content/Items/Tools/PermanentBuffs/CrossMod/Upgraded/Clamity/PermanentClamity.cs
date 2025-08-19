using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Clamity;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Clamity;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.Clamity
{
	// Token: 0x0200008F RID: 143
	[JITWhenModsEnabled(new string[]
	{
		"Clamity"
	})]
	[ExtendsFromMod(new string[]
	{
		"Clamity"
	})]
	public class PermanentClamity : IPermanentModdedBuffItem
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00009C5C File Offset: 0x00007E5C
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentClamity";
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00009C64 File Offset: 0x00007E64
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00009C84 File Offset: 0x00007E84
		public override void AddRecipes()
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentExoBaguette>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSupremeLuck>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentTitanScale>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00009CFF File Offset: 0x00007EFF
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new ClamityEffect());
		}
	}
}
