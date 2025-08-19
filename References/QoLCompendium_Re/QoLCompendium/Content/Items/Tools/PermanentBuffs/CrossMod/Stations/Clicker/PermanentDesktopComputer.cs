using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Stations.Clicker;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Stations.Clicker
{
	// Token: 0x0200009E RID: 158
	[JITWhenModsEnabled(new string[]
	{
		"ClickerClass"
	})]
	[ExtendsFromMod(new string[]
	{
		"ClickerClass"
	})]
	public class PermanentDesktopComputer : IPermanentModdedBuffItem
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000A7D5 File Offset: 0x000089D5
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.clickerClassMod, Common.GetModBuff(ModConditions.clickerClassMod, "DesktopComputerBuff"));
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000A7F0 File Offset: 0x000089F0
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000A810 File Offset: 0x00008A10
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.clickerClassMod, "DesktopComputer"), 3);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000A873 File Offset: 0x00008A73
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new DesktopComputerEffect());
		}
	}
}
