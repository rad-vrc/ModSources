using System;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader.IO;
using Terraria.ObjectData;

namespace Terraria.ModLoader.Default
{
	// Token: 0x020002D4 RID: 724
	public class UnloadedNonSolidTile : UnloadedTile
	{
		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06002DF7 RID: 11767 RVA: 0x005308FA File Offset: 0x0052EAFA
		public override string Texture
		{
			get
			{
				return "ModLoader/UnloadedNonSolidTile";
			}
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x00530904 File Offset: 0x0052EB04
		public override void SetStaticDefaults()
		{
			TileIO.Tiles.unloadedTypes.Add(base.Type);
			Main.tileFrameImportant[(int)base.Type] = true;
			TileID.Sets.DisableSmartCursor[(int)base.Type] = true;
			Main.tileSolid[(int)base.Type] = false;
			Main.tileNoAttach[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.None, 0, 0);
			TileObjectData.addTile((int)base.Type);
		}
	}
}
