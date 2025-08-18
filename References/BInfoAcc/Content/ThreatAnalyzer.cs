using BInfoAcc.Common;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BInfoAcc.Content
{
	public class ThreatAnalyzer : ModItem
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
			player.GetModPlayer<InfoPlayer>().biomeDisplay = true;
			player.GetModPlayer<InfoPlayer>().spawnRateDisplay = true;
			player.GetModPlayer<InfoPlayer>().aggroDisplay = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<SafteyScanner>()
				.AddIngredient<BiomeCrystal>()
				.AddIngredient<WantedPoster>()
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}