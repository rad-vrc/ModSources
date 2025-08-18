using BInfoAcc.Common;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace BInfoAcc.Content
{
	public class SafteyScanner : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.accessory = true;
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.buyPrice(0, 5, 0, 0);
		}

		public override void UpdateInfoAccessory(Player player)
		{
			player.GetModPlayer<InfoPlayer>().spawnRateDisplay = true;
		}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if(Main.netMode == NetmodeID.MultiplayerClient)
			{
				tooltips.Add(new TooltipLine(Mod, "MultiplayerError", Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.BrokenSpawnRate")) 
				{ OverrideColor = new Color(190, 120, 120) });
			}
        }
    }
}