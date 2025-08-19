using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace QoLCompendium.Content.Tiles.CraftingStations
{
	// Token: 0x02000017 RID: 23
	public class HardmodeMonolithTile : ModTile
	{
		// Token: 0x06000070 RID: 112 RVA: 0x0000386C File Offset: 0x00001A6C
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
				134,
				133,
				101,
				125,
				301,
				499,
				307,
				217,
				218
			};
			base.DustType = -1;
		}
	}
}
