using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SOTS;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.SOTS
{
	// Token: 0x020000CE RID: 206
	[JITWhenModsEnabled(new string[]
	{
		"SOTS"
	})]
	[ExtendsFromMod(new string[]
	{
		"SOTS"
	})]
	public class PermanentHarmony : IPermanentModdedBuffItem
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0000CAF2 File Offset: 0x0000ACF2
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.secretsOfTheShadowsMod, Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Harmony"));
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000CB10 File Offset: 0x0000AD10
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000CB30 File Offset: 0x0000AD30
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "HarmonyPotion"), 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000CB94 File Offset: 0x0000AD94
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new HarmonyEffect());
		}
	}
}
