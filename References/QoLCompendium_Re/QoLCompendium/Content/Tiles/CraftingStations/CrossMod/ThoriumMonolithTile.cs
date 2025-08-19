using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using ThoriumMod.Tiles;

namespace QoLCompendium.Content.Tiles.CraftingStations.CrossMod
{
	// Token: 0x0200001E RID: 30
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class ThoriumMonolithTile : ModTile
	{
		// Token: 0x0600007E RID: 126 RVA: 0x00003E2C File Offset: 0x0000202C
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
			Main.tileNoAttach[(int)base.Type] = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				16
			};
			TileObjectData.addTile((int)base.Type);
			LocalizedText name = base.CreateMapEntryName();
			base.AddMapEntry(new Color(200, 200, 200), name);
			TileID.Sets.DisableSmartCursor[(int)base.Type] = true;
			base.AdjTiles = new int[]
			{
				ModContent.TileType<ThoriumAnvil>(),
				ModContent.TileType<ArcaneArmorFabricator>(),
				ModContent.TileType<GrimPedestal>(),
				ModContent.TileType<SoulForge>(),
				ModContent.TileType<GuidesFinalGiftTile>()
			};
			base.DustType = -1;
		}
	}
}
