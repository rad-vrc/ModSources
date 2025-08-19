using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Arena.Calamity;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Calamity;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.Calamity
{
	// Token: 0x02000093 RID: 147
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class PermanentCalamityArena : IPermanentModdedBuffItem
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000266 RID: 614 RVA: 0x00009F75 File Offset: 0x00008175
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentCalamityArena";
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00009F7C File Offset: 0x0000817C
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00009F9C File Offset: 0x0000819C
		public override void AddRecipes()
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentCorruptionEffigy>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentCrimsonEffigy>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentEffigyOfDecay>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentResilientCandle>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSpitefulCandle>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentVigorousCandle>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentWeightlessCandle>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000A04B File Offset: 0x0000824B
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new CalamityArenaEffect());
		}
	}
}
