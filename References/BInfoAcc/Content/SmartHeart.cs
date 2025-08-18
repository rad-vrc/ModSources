using BInfoAcc.Common;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BInfoAcc.Content
{
	public class SmartHeart : ModItem
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
			player.GetModPlayer<InfoPlayer>().regenDisplay = true;
		}
	}
}