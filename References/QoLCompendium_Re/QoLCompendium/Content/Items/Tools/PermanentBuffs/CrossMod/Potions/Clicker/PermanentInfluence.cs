using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Clicker;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Clicker
{
	// Token: 0x020000E6 RID: 230
	[JITWhenModsEnabled(new string[]
	{
		"ClickerClass"
	})]
	[ExtendsFromMod(new string[]
	{
		"ClickerClass"
	})]
	public class PermanentInfluence : IPermanentModdedBuffItem
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0000DC86 File Offset: 0x0000BE86
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.clickerClassMod, Common.GetModBuff(ModConditions.clickerClassMod, "InfluenceBuff"));
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000DCA4 File Offset: 0x0000BEA4
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000DCC4 File Offset: 0x0000BEC4
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.clickerClassMod, "InfluencePotion"), 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000DD28 File Offset: 0x0000BF28
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new InfluenceEffect());
		}
	}
}
