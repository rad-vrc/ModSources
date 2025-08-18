using BInfoAcc.Common;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BInfoAcc.Content
{
	public class AttendanceLog : ModItem
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
			player.GetModPlayer<InfoPlayer>().minionDisplay = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Book)
				.AddIngredient(ItemID.BlackInk)
				.AddIngredient(ItemID.Gel, 20)
				.AddTile(TileID.Chairs)
				.AddTile(TileID.Tables)
				.Register();

			// Alt. recipe using black paint
            CreateRecipe()
                .AddIngredient(ItemID.Book)
                .AddIngredient(ItemID.BlackPaint, 10)
                .AddIngredient(ItemID.Gel, 20)
                .AddTile(TileID.Chairs)
                .AddTile(TileID.Tables)
                .Register();
        }
	}
}