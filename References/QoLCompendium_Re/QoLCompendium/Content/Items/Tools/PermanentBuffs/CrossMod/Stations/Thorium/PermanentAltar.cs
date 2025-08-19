using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Stations.Thorium;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Stations.Thorium
{
	// Token: 0x02000098 RID: 152
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class PermanentAltar : IPermanentModdedBuffItem
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000A382 File Offset: 0x00008582
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "AltarBuff"));
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000A3A0 File Offset: 0x000085A0
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000A3C0 File Offset: 0x000085C0
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.thoriumMod, "Altar"), 3);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000A423 File Offset: 0x00008623
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new AltarEffect());
		}
	}
}
