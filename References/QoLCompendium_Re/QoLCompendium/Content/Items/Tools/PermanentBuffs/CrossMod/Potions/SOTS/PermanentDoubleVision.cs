using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SOTS;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.SOTS
{
	// Token: 0x020000CD RID: 205
	[JITWhenModsEnabled(new string[]
	{
		"SOTS"
	})]
	[ExtendsFromMod(new string[]
	{
		"SOTS"
	})]
	public class PermanentDoubleVision : IPermanentModdedBuffItem
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000CA36 File Offset: 0x0000AC36
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.secretsOfTheShadowsMod, Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "DoubleVision"));
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000CA54 File Offset: 0x0000AC54
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000CA74 File Offset: 0x0000AC74
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "DoubleVisionPotion"), 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000CAD8 File Offset: 0x0000ACD8
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new DoubleVisionEffect());
		}
	}
}
