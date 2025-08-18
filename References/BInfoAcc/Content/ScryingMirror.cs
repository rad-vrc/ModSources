using BInfoAcc.Common;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BInfoAcc.Content
{
	public class ScryingMirror : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.accessory = true;
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.buyPrice(0, 15, 0, 0);
		}

		public override void UpdateInfoAccessory(Player player)
		{
			player.GetModPlayer<InfoPlayer>().minionDisplay = true;
			player.GetModPlayer<InfoPlayer>().sentryDisplay = true;
			player.GetModPlayer<InfoPlayer>().luckDisplay = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<FortuneMirror>()
				.AddIngredient<AttendanceLog>()
				.AddIngredient<EngiRegistry>()
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}