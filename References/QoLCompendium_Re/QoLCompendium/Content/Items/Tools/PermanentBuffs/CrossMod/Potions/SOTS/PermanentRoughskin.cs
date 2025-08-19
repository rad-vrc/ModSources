using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SOTS;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.SOTS
{
	// Token: 0x020000D1 RID: 209
	[JITWhenModsEnabled(new string[]
	{
		"SOTS"
	})]
	[ExtendsFromMod(new string[]
	{
		"SOTS"
	})]
	public class PermanentRoughskin : IPermanentModdedBuffItem
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000CD26 File Offset: 0x0000AF26
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.secretsOfTheShadowsMod, Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Roughskin"));
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000CD44 File Offset: 0x0000AF44
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000CD64 File Offset: 0x0000AF64
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "RoughskinPotion"), 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000CDC8 File Offset: 0x0000AFC8
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new RoughskinEffect());
		}
	}
}
