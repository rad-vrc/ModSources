using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace QoLCompendium.Content.Tiles.CraftingStations
{
	// Token: 0x02000016 RID: 22
	public class DemonAltarTile : ModTile
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000036CB File Offset: 0x000018CB
		public override string Texture
		{
			get
			{
				return "Terraria/Images/Tiles_26";
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000037B0 File Offset: 0x000019B0
		public override void SetStaticDefaults()
		{
			Main.tileLighted[(int)base.Type] = true;
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
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
			base.AdjTiles = new int[]
			{
				26
			};
			base.DustType = -1;
		}
	}
}
