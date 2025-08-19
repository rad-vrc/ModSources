using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace QoLCompendium.Content.Tiles.CraftingStations
{
	// Token: 0x02000019 RID: 25
	public class SpecializedMonolithTile : ModTile
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00003A00 File Offset: 0x00001C00
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
				300,
				302,
				308,
				306,
				304,
				305,
				220,
				622,
				355,
				114,
				243,
				228,
				77,
				85
			};
			TileID.Sets.CountsAsLavaSource[(int)base.Type] = true;
			TileID.Sets.CountsAsHoneySource[(int)base.Type] = true;
			TileID.Sets.CountsAsWaterSource[(int)base.Type] = true;
			base.DustType = -1;
		}
	}
}
