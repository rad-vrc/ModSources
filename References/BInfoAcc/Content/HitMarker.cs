using BInfoAcc.Common;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BInfoAcc.Content
{
	public class HitMarker : ModItem
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
			player.GetModPlayer<InfoPlayer>().comboDisplay = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.CopperWatch)
				.AddIngredient(ItemID.Lens , 3)
				.AddIngredient(ItemID.DemoniteBar, 5)
				.AddTile(TileID.Anvils)
				.AddDecraftCondition(Condition.CorruptWorld)
				.Register();

			CreateRecipe()
				.AddIngredient(ItemID.TinWatch)
				.AddIngredient(ItemID.Lens, 3)
				.AddIngredient(ItemID.DemoniteBar, 5)
				.AddTile(TileID.Anvils)
				.AddDecraftCondition(Condition.CorruptWorld)
				.Register();

			CreateRecipe()
				.AddIngredient(ItemID.CopperWatch)
				.AddIngredient(ItemID.Lens, 3)
				.AddIngredient(ItemID.CrimtaneBar, 5)
				.AddTile(TileID.Anvils)
				.AddDecraftCondition(Condition.CrimsonWorld)
				.Register();

			CreateRecipe()
				.AddIngredient(ItemID.TinWatch)
				.AddIngredient(ItemID.Lens, 3)
				.AddIngredient(ItemID.CrimtaneBar, 5)
				.AddTile(TileID.Anvils)
				.AddDecraftCondition(Condition.CrimsonWorld)
				.Register();
		}
	}
}