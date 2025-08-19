using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Thorium;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.Thorium
{
	// Token: 0x02000083 RID: 131
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class PermanentThoriumThrower : IPermanentModdedBuffItem
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000929A File Offset: 0x0000749A
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentThoriumThrower";
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000092A4 File Offset: 0x000074A4
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000092C4 File Offset: 0x000074C4
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentAssassin>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentHydration>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000932A File Offset: 0x0000752A
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new ThoriumThrowerEffect());
		}
	}
}
