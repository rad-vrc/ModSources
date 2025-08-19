using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SOTS;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.SOTS
{
	// Token: 0x020000D2 RID: 210
	[JITWhenModsEnabled(new string[]
	{
		"SOTS"
	})]
	[ExtendsFromMod(new string[]
	{
		"SOTS"
	})]
	public class PermanentSoulAccess : IPermanentModdedBuffItem
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000CDE2 File Offset: 0x0000AFE2
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.secretsOfTheShadowsMod, Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "SoulAccess"));
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000CE00 File Offset: 0x0000B000
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000CE20 File Offset: 0x0000B020
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "SoulAccessPotion"), 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000CE84 File Offset: 0x0000B084
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new SoulAccessEffect());
		}
	}
}
