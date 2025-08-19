using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.SpiritClassic;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.SpiritClassic;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.SpiritClassic
{
	// Token: 0x02000089 RID: 137
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class PermanentSpiritClassicMovement : IPermanentModdedBuffItem
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00009739 File Offset: 0x00007939
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentSpiritClassicMovement";
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00009740 File Offset: 0x00007940
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00009760 File Offset: 0x00007960
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentJump>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSpiritSoaring>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentZephyr>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000097D3 File Offset: 0x000079D3
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new SpiritClassicMovementEffect());
		}
	}
}
