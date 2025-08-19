using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Calamity;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Calamity;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.Calamity
{
	// Token: 0x02000094 RID: 148
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class PermanentCalamityDamage : IPermanentModdedBuffItem
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000A065 File Offset: 0x00008265
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentCalamityDamage";
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000A06C File Offset: 0x0000826C
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000A08C File Offset: 0x0000828C
		public override void AddRecipes()
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentAstralInjection>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentShadow>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000A0FA File Offset: 0x000082FA
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new CalamityDamageEffect());
		}
	}
}
