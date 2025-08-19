using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.SpiritClassic;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Arena.SpiritClassic
{
	// Token: 0x0200011D RID: 285
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class PermanentCoiledEnergizer : IPermanentModdedBuffItem
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00010725 File Offset: 0x0000E925
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "OverDrive"));
			}
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00010740 File Offset: 0x0000E940
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00010760 File Offset: 0x0000E960
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.spiritMod, "CoilEnergizerItem"), 3);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x000107C3 File Offset: 0x0000E9C3
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new CoiledEnergizerEffect());
		}
	}
}
