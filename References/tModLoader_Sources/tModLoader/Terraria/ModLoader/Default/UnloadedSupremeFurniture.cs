using System;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader.IO;
using Terraria.ObjectData;

namespace Terraria.ModLoader.Default
{
	// Token: 0x020002D9 RID: 729
	public class UnloadedSupremeFurniture : UnloadedTile
	{
		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06002E08 RID: 11784 RVA: 0x00530BF9 File Offset: 0x0052EDF9
		public override string Texture
		{
			get
			{
				return "ModLoader/UnloadedSupremeFurniture";
			}
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x00530C00 File Offset: 0x0052EE00
		public override void SetStaticDefaults()
		{
			TileIO.Tiles.unloadedTypes.Add(base.Type);
			Main.tileFrameImportant[(int)base.Type] = true;
			TileID.Sets.DisableSmartCursor[(int)base.Type] = true;
			TileID.Sets.IgnoredByNpcStepUp[(int)base.Type] = true;
			Main.tileSolidTop[(int)base.Type] = true;
			Main.tileSolid[(int)base.Type] = true;
			Main.tileTable[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			base.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsChair);
			base.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
			base.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
			base.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.None, 0, 0);
			TileObjectData.addTile((int)base.Type);
		}
	}
}
