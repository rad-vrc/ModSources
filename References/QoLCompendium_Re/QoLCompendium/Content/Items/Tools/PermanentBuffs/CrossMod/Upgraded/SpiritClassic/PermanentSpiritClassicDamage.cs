using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.SpiritClassic;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.SpiritClassic;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.SpiritClassic
{
	// Token: 0x02000087 RID: 135
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class PermanentSpiritClassicDamage : IPermanentModdedBuffItem
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600021E RID: 542 RVA: 0x000095B4 File Offset: 0x000077B4
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentSpiritClassicDamage";
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000095BC File Offset: 0x000077BC
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000095DC File Offset: 0x000077DC
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentRunescribe>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSoulguard>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentSpirit>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentStarburn>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentToxin>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00009669 File Offset: 0x00007869
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new SpiritClassicDamageEffect());
		}
	}
}
