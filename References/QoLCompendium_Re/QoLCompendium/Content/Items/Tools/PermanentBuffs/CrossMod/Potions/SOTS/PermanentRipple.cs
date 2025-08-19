using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SOTS;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.SOTS
{
	// Token: 0x020000D0 RID: 208
	[JITWhenModsEnabled(new string[]
	{
		"SOTS"
	})]
	[ExtendsFromMod(new string[]
	{
		"SOTS"
	})]
	public class PermanentRipple : IPermanentModdedBuffItem
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000CC6A File Offset: 0x0000AE6A
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.secretsOfTheShadowsMod, Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "RippleBuff"));
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000CC88 File Offset: 0x0000AE88
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000CCA8 File Offset: 0x0000AEA8
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "RipplePotion"), 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000CD0C File Offset: 0x0000AF0C
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new RippleEffect());
		}
	}
}
