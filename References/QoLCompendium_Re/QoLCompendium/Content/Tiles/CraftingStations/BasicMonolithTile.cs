using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace QoLCompendium.Content.Tiles.CraftingStations
{
	// Token: 0x02000014 RID: 20
	public class BasicMonolithTile : ModTile
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00003614 File Offset: 0x00001814
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
				94
			};
			TileID.Sets.CountsAsWaterSource[(int)base.Type] = true;
			base.DustType = -1;
		}
	}
}
