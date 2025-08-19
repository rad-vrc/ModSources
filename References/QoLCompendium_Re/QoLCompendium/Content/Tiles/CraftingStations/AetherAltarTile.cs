using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace QoLCompendium.Content.Tiles.CraftingStations
{
	// Token: 0x02000013 RID: 19
	public class AetherAltarTile : ModTile
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00003568 File Offset: 0x00001768
		public override void SetStaticDefaults()
		{
			Main.tileLighted[(int)base.Type] = true;
			Main.tileFrameImportant[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			Main.tileNoAttach[(int)base.Type] = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16
			};
			TileObjectData.addTile((int)base.Type);
			LocalizedText name = base.CreateMapEntryName();
			base.AddMapEntry(new Color(200, 200, 200), name);
			TileID.Sets.DisableSmartCursor[(int)base.Type] = true;
			TileID.Sets.CountsAsShimmerSource[(int)base.Type] = true;
			base.DustType = -1;
		}
	}
}
