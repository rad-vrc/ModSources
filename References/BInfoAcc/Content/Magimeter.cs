using BInfoAcc.Common;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BInfoAcc.Content
{
	public class Magimeter : ModItem
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
			player.GetModPlayer<InfoPlayer>().manaDisplay = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.ManaCrystal)
				.AddIngredient(ItemID.Glass , 10)
				.AddIngredient(ItemID.GoldBar, 10)
                .AddIngredient(ItemID.Diamond)
                .AddTile(TileID.WorkBenches)
				.Register();

			CreateRecipe()
				.AddIngredient(ItemID.ManaCrystal)
                .AddIngredient(ItemID.Glass, 10)
                .AddIngredient(ItemID.PlatinumBar, 10)
                .AddIngredient(ItemID.Diamond)
                .AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}