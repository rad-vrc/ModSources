using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Thorium
{
	// Token: 0x020000B5 RID: 181
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class PermanentSkeletonRepellent : IPermanentModdedBuffItem
	{
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000B896 File Offset: 0x00009A96
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "SkeletonRepellentBuff"));
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000B8B4 File Offset: 0x00009AB4
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000B8D4 File Offset: 0x00009AD4
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.thoriumMod, "SkeletonRepellent"), 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000B938 File Offset: 0x00009B38
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new SkeletonRepellentEffect());
		}
	}
}
