using System;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader.IO;
using Terraria.ObjectData;

namespace Terraria.ModLoader.Default
{
	// Token: 0x020002D8 RID: 728
	public class UnloadedSolidTile : UnloadedTile
	{
		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06002E05 RID: 11781 RVA: 0x00530B75 File Offset: 0x0052ED75
		public override string Texture
		{
			get
			{
				return "ModLoader/UnloadedSolidTile";
			}
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x00530B7C File Offset: 0x0052ED7C
		public override void SetStaticDefaults()
		{
			TileIO.Tiles.unloadedTypes.Add(base.Type);
			Main.tileFrameImportant[(int)base.Type] = true;
			TileID.Sets.DisableSmartCursor[(int)base.Type] = true;
			Main.tileSolid[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.None, 0, 0);
			TileObjectData.addTile((int)base.Type);
		}
	}
}
