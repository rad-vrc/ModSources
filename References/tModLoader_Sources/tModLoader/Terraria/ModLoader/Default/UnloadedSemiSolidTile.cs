using System;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader.IO;
using Terraria.ObjectData;

namespace Terraria.ModLoader.Default
{
	// Token: 0x020002D7 RID: 727
	public class UnloadedSemiSolidTile : UnloadedTile
	{
		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06002E02 RID: 11778 RVA: 0x00530AB9 File Offset: 0x0052ECB9
		public override string Texture
		{
			get
			{
				return "ModLoader/UnloadedSemiSolidTile";
			}
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x00530AC0 File Offset: 0x0052ECC0
		public override void SetStaticDefaults()
		{
			TileIO.Tiles.unloadedTypes.Add(base.Type);
			Main.tileFrameImportant[(int)base.Type] = true;
			TileID.Sets.DisableSmartCursor[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileTable[(int)base.Type] = true;
			Main.tileSolidTop[(int)base.Type] = true;
			TileID.Sets.Platforms[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.None, 0, 0);
			TileObjectData.addTile((int)base.Type);
			base.AdjTiles = new int[]
			{
				19
			};
		}
	}
}
