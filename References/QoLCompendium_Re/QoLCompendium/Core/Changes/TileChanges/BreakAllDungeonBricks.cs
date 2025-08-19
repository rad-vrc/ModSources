using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.TileChanges
{
	// Token: 0x02000218 RID: 536
	public class BreakAllDungeonBricks : GlobalTile
	{
		// Token: 0x06000D19 RID: 3353 RVA: 0x00066878 File Offset: 0x00064A78
		public unsafe override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
		{
			EntitySource_TileBreak src = new EntitySource_TileBreak(i, j, null);
			Vector2 projPos;
			projPos..ctor((float)(Common.ToPixels((float)i) + 8), (float)(Common.ToPixels((float)j) + 8));
			if (!QoLCompendium.mainConfig.BreakAllDungeonBricks)
			{
				return;
			}
			if (!Main.tileCracked[type] || Main.netMode == 1)
			{
				return;
			}
			for (int k = 0; k < 8; k++)
			{
				int x = i;
				int y = j;
				switch (k)
				{
				case 0:
					x--;
					break;
				case 1:
					x++;
					break;
				case 2:
					y--;
					break;
				case 3:
					y++;
					break;
				case 4:
					x--;
					y--;
					break;
				case 5:
					x++;
					y--;
					break;
				case 6:
					x--;
					y++;
					break;
				case 7:
					x++;
					y++;
					break;
				}
				Tile tile = Main.tile[x, y];
				if (tile.HasTile && Main.tileCracked[(int)(*tile.TileType)])
				{
					Main.tile[i, j].Get<TileWallWireStateData>().HasTile = false;
					WorldGen.KillTile(x, y, false, false, true);
					if (Main.netMode == 2)
					{
						NetMessage.TrySendData(17, -1, -1, null, 20, (float)x, (float)y, 0f, 0, 0, 0);
					}
				}
			}
			int projType = type - 481 + 736;
			if (Main.netMode == 0)
			{
				Projectile.NewProjectile(src, projPos, Vector2.Zero, projType, 20, 0f, Main.myPlayer, 0f, 0f, 0f);
				return;
			}
			if (Main.netMode == 2)
			{
				Projectile.NewProjectileDirect(src, projPos, Vector2.Zero, projType, 20, 0f, Main.myPlayer, 0f, 0f, 0f).netUpdate = true;
			}
		}
	}
}
