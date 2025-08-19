using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace QoLCompendium.Content.Projectiles.Explosives
{
	// Token: 0x0200002F RID: 47
	public class Destruction : GlobalTile
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x00006718 File Offset: 0x00004918
		internal unsafe static void DestroyChest(int x, int y)
		{
			int chestType = 1;
			int chest = Chest.FindChest(x, y);
			if (chest != -1)
			{
				for (int i = 0; i < 40; i++)
				{
					Main.chest[chest].item[i] = new Item();
				}
				Main.chest[chest] = null;
				if (*Main.tile[x, y].TileType == 467)
				{
					chestType = 5;
				}
				if (*Main.tile[x, y].TileType >= TileID.Count)
				{
					chestType = 101;
				}
			}
			for (int j = x; j < x + 2; j++)
			{
				for (int k = y; k < y + 2; k++)
				{
					*Main.tile[j, k].TileType = 0;
					*Main.tile[j, k].TileFrameX = 0;
					*Main.tile[j, k].TileFrameY = 0;
				}
			}
			if (Main.netMode != 0)
			{
				if (chest != -1)
				{
					NetMessage.SendData(34, -1, -1, null, chestType, (float)x, (float)y, 0f, chest, (int)(*Main.tile[x, y].TileType), 0);
				}
				NetMessage.SendTileSquare(-1, x, y, 3, TileChangeType.None);
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00006844 File Offset: 0x00004A44
		internal unsafe static Point16 FindChestTopLeft(int x, int y, bool destroy)
		{
			Tile tile = Main.tile[x, y];
			if (TileID.Sets.BasicChest[(int)(*tile.TileType)])
			{
				TileObjectData data = TileObjectData.GetTileData((int)(*tile.TileType), 0, 0);
				x -= (int)(*tile.TileFrameX / 18) % data.Width;
				y -= (int)(*tile.TileFrameY / 18) % data.Height;
				if (destroy)
				{
					Destruction.DestroyChest(x, y);
				}
				return new Point16(x, y);
			}
			return Point16.NegativeOne;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000068C4 File Offset: 0x00004AC4
		internal unsafe static void ClearTileAndLiquid(int x, int y, bool sendData = true)
		{
			Destruction.FindChestTopLeft(x, y, true);
			Tile tile = Main.tile[x, y];
			bool hadLiquid = *tile.LiquidAmount > 0;
			WorldGen.KillTile(x, y, false, false, true);
			tile.Clear(TileDataType.Tile);
			tile.Clear(TileDataType.Liquid);
			if (Main.netMode == 2)
			{
				if (hadLiquid)
				{
					NetMessage.sendWater(x, y);
				}
				if (sendData)
				{
					NetMessage.SendTileSquare(-1, x, y, 1, TileChangeType.None);
				}
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00006930 File Offset: 0x00004B30
		internal unsafe static void ClearEverything(int x, int y, bool sendData = true)
		{
			Destruction.FindChestTopLeft(x, y, true);
			Tile tile = Main.tile[x, y];
			bool hadLiquid = *tile.LiquidAmount > 0;
			WorldGen.KillTile(x, y, false, false, true);
			tile.ClearEverything();
			if (Main.netMode == 2)
			{
				if (hadLiquid)
				{
					NetMessage.sendWater(x, y);
				}
				if (sendData)
				{
					NetMessage.SendTileSquare(-1, x, y, 1, TileChangeType.None);
				}
			}
		}
	}
}
