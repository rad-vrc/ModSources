using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.SpiritClassic;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.SpiritClassic;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.SpiritClassic
{
	// Token: 0x02000088 RID: 136
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class PermanentSpiritClassicDefense : IPermanentModdedBuffItem
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00009683 File Offset: 0x00007883
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentSpiritClassicDefense";
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000968C File Offset: 0x0000788C
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000096AC File Offset: 0x000078AC
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentMirrorCoat>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSporecoid>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSteadfast>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000971F File Offset: 0x0000791F
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new SpiritClassicDefenseEffect());
		}
	}
}
