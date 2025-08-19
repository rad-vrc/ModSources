using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace QoLCompendium.Content.Tiles.CraftingStations
{
	// Token: 0x02000018 RID: 24
	public class LunarMonolithTile : ModTile
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00003918 File Offset: 0x00001B18
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
				18,
				283,
				17,
				16,
				13,
				106,
				86,
				14,
				15,
				96,
				172,
				94,
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
				85,
				134,
				133,
				101,
				125,
				301,
				499,
				307,
				217,
				218,
				412,
				247,
				303,
				26,
				0
			};
			TileID.Sets.CountsAsWaterSource[(int)base.Type] = true;
			TileID.Sets.CountsAsLavaSource[(int)base.Type] = true;
			TileID.Sets.CountsAsHoneySource[(int)base.Type] = true;
			TileID.Sets.CountsAsShimmerSource[(int)base.Type] = true;
			base.DustType = -1;
		}
	}
}
