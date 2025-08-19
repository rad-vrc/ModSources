using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Thorium;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.Thorium
{
	// Token: 0x02000080 RID: 128
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class PermanentThoriumMovement : IPermanentModdedBuffItem
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00009055 File Offset: 0x00007255
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentThoriumMovement";
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000905C File Offset: 0x0000725C
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000907C File Offset: 0x0000727C
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentAquaAffinity>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentBloodRush>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentKinetic>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000090EF File Offset: 0x000072EF
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new ThoriumMovementEffect());
		}
	}
}
