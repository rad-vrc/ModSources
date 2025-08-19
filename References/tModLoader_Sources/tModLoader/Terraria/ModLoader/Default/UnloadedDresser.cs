using System;
using Terraria.ID;
using Terraria.ModLoader.IO;
using Terraria.ObjectData;

namespace Terraria.ModLoader.Default
{
	// Token: 0x020002D0 RID: 720
	public class UnloadedDresser : UnloadedTile
	{
		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06002DD9 RID: 11737 RVA: 0x00530535 File Offset: 0x0052E735
		public override string Texture
		{
			get
			{
				return "ModLoader/UnloadedDresser";
			}
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x0053053C File Offset: 0x0052E73C
		public override void SetStaticDefaults()
		{
			TileIO.Tiles.unloadedTypes.Add(base.Type);
			Main.tileFrameImportant[(int)base.Type] = true;
			TileID.Sets.DisableSmartCursor[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileTable[(int)base.Type] = true;
			Main.tileSolidTop[(int)base.Type] = true;
			Main.tileContainer[(int)base.Type] = true;
			TileID.Sets.BasicDresser[(int)base.Type] = true;
			base.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.addTile((int)base.Type);
			base.AdjTiles = new int[]
			{
				88
			};
		}
	}
}
