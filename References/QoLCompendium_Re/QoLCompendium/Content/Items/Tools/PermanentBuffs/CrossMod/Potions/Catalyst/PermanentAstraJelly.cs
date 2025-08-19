using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Catalyst;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Catalyst
{
	// Token: 0x020000EB RID: 235
	[JITWhenModsEnabled(new string[]
	{
		"CatalystMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CatalystMod"
	})]
	public class PermanentAstraJelly : IPermanentModdedBuffItem
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0000DF8E File Offset: 0x0000C18E
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.catalystMod, Common.GetModBuff(ModConditions.catalystMod, "AstralJellyBuff"));
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000E060 File Offset: 0x0000C260
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000E080 File Offset: 0x0000C280
		public override void AddRecipes()
		{
			if (!ModConditions.catalystLoaded)
			{
				return;
			}
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.catalystMod, "AstraJelly"), 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000E0EC File Offset: 0x0000C2EC
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new AstraJellyEffect());
		}
	}
}
