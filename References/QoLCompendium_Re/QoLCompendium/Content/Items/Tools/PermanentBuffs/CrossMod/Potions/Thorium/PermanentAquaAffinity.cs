using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Thorium
{
	// Token: 0x020000A1 RID: 161
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class PermanentAquaAffinity : IPermanentModdedBuffItem
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000A9E8 File Offset: 0x00008BE8
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "AquaAffinity"));
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000AA04 File Offset: 0x00008C04
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000AA24 File Offset: 0x00008C24
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.thoriumMod, "AquaPotion"), 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000AA88 File Offset: 0x00008C88
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new AquaAffinityEffect());
		}
	}
}
