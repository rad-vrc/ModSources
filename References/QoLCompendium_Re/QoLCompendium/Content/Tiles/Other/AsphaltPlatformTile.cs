using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace QoLCompendium.Content.Tiles.Other
{
	// Token: 0x02000011 RID: 17
	public class AsphaltPlatformTile : ModTile
	{
		// Token: 0x0600005E RID: 94 RVA: 0x000033D0 File Offset: 0x000015D0
		public override void SetStaticDefaults()
		{
			Main.tileLighted[(int)base.Type] = true;
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileSolidTop[(int)base.Type] = true;
			Main.tileSolid[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileTable[(int)base.Type] = true;
			TileID.Sets.Platforms[(int)base.Type] = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleMultiplier = 27;
			TileObjectData.newTile.StyleWrapLimit = 27;
			TileObjectData.newTile.UsesCustomCanPlace = false;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile((int)base.Type);
			base.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
			base.AddMapEntry(new Color(47, 51, 58), null);
			base.DustType = 54;
			base.AdjTiles = new int[]
			{
				19
			};
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000034DE File Offset: 0x000016DE
		public override void PostSetDefaults()
		{
			Main.tileNoSunLight[(int)base.Type] = false;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000034ED File Offset: 0x000016ED
		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 10;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000034F4 File Offset: 0x000016F4
		public override void FloorVisuals(Player player)
		{
			player.powerrun = true;
		}
	}
}
