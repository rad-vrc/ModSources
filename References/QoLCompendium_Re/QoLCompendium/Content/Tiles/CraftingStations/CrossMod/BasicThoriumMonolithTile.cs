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
	// Token: 0x0200001B RID: 27
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class BasicThoriumMonolithTile : ModTile
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00003B8C File Offset: 0x00001D8C
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
				ModContent.TileType<GrimPedestal>()
			};
			base.DustType = -1;
		}
	}
}
