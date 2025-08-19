using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.ThoriumBossRework;
using Terraria;
using Terraria.ModLoader;
using ThoriumRework;
using ThoriumRework.Buffs;
using ThoriumRework.Items;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.ThoriumBossRework
{
	// Token: 0x020000A0 RID: 160
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumRework"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumRework"
	})]
	public class PermanentInspirationRegeneration : IPermanentModdedBuffItem
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x0000A88D File Offset: 0x00008A8D
		public override bool IsLoadingEnabled(Mod mod)
		{
			return ModContent.GetInstance<CompatConfig>().extraPotions;
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000A940 File Offset: 0x00008B40
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.thoriumBossReworkMod, ModContent.BuffType<Inspired>());
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000A954 File Offset: 0x00008B54
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000A974 File Offset: 0x00008B74
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<InspirationRegenerationPotion>(), 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000A9CE File Offset: 0x00008BCE
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new InspirationRegenerationEffect());
		}
	}
}
