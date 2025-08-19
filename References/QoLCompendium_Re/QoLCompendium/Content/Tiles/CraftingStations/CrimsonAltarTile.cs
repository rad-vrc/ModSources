using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace QoLCompendium.Content.Tiles.CraftingStations
{
	// Token: 0x02000015 RID: 21
	public class CrimsonAltarTile : ModTile
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000036CB File Offset: 0x000018CB
		public override string Texture
		{
			get
			{
				return "Terraria/Images/Tiles_26";
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000036D4 File Offset: 0x000018D4
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
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.DrawStyleOffset = 1;
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

		// Token: 0x0600006B RID: 107 RVA: 0x000037A6 File Offset: 0x000019A6
		public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
		{
			frameXOffset = 54;
		}
	}
}
