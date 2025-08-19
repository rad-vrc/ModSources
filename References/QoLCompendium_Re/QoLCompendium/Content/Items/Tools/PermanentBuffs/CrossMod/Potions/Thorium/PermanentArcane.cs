using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Thorium
{
	// Token: 0x020000A2 RID: 162
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class PermanentArcane : IPermanentModdedBuffItem
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000AAA2 File Offset: 0x00008CA2
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "ArcanePotionBuff"));
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000AAC0 File Offset: 0x00008CC0
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000AAE0 File Offset: 0x00008CE0
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.thoriumMod, "ArcanePotion"), 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000AB44 File Offset: 0x00008D44
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new ArcaneEffect());
		}
	}
}
