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
	// Token: 0x0200009F RID: 159
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumRework"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumRework"
	})]
	public class PermanentDeathsinger : IPermanentModdedBuffItem
	{
		// Token: 0x060002AE RID: 686 RVA: 0x0000A88D File Offset: 0x00008A8D
		public override bool IsLoadingEnabled(Mod mod)
		{
			return ModContent.GetInstance<CompatConfig>().extraPotions;
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000A899 File Offset: 0x00008A99
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.thoriumBossReworkMod, ModContent.BuffType<Deathsinger>());
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000A8AC File Offset: 0x00008AAC
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000A8CC File Offset: 0x00008ACC
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<DeathsingerPotion>(), 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000A926 File Offset: 0x00008B26
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new DeathsingerEffect());
		}
	}
}
