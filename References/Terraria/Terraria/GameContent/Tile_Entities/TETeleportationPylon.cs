using System;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.GameContent.Tile_Entities
{
	// Token: 0x02000213 RID: 531
	public class TETeleportationPylon : TileEntity
	{
		// Token: 0x06001E0B RID: 7691 RVA: 0x00507C82 File Offset: 0x00505E82
		public override void RegisterTileEntityID(int assignedID)
		{
			TETeleportationPylon._myEntityID = (byte)assignedID;
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x00507C93 File Offset: 0x00505E93
		public override TileEntity GenerateInstance()
		{
			return new TETeleportationPylon();
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x00507C9C File Offset: 0x00505E9C
		public override void NetPlaceEntityAttempt(int x, int y)
		{
			TeleportPylonType pylonType;
			if (!this.TryGetPylonTypeFromTileCoords(x, y, out pylonType))
			{
				TETeleportationPylon.RejectPlacementFromNet(x, y);
				return;
			}
			if (Main.PylonSystem.HasPylonOfType(pylonType))
			{
				TETeleportationPylon.RejectPlacementFromNet(x, y);
				return;
			}
			int number = TETeleportationPylon.Place(x, y);
			NetMessage.SendData(86, -1, -1, null, number, (float)x, (float)y, 0f, 0, 0, 0);
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x00507CF1 File Offset: 0x00505EF1
		public bool TryGetPylonType(out TeleportPylonType pylonType)
		{
			return this.TryGetPylonTypeFromTileCoords((int)this.Position.X, (int)this.Position.Y, out pylonType);
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x00507D10 File Offset: 0x00505F10
		private static void RejectPlacementFromNet(int x, int y)
		{
			WorldGen.KillTile(x, y, false, false, false);
			if (Main.netMode == 2)
			{
				NetMessage.SendData(17, -1, -1, null, 0, (float)x, (float)y, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x00507D48 File Offset: 0x00505F48
		public static int Place(int x, int y)
		{
			TETeleportationPylon teteleportationPylon = new TETeleportationPylon();
			teteleportationPylon.Position = new Point16(x, y);
			teteleportationPylon.ID = TileEntity.AssignNewID();
			teteleportationPylon.type = TETeleportationPylon._myEntityID;
			object entityCreationLock = TileEntity.EntityCreationLock;
			lock (entityCreationLock)
			{
				TileEntity.ByID[teteleportationPylon.ID] = teteleportationPylon;
				TileEntity.ByPosition[teteleportationPylon.Position] = teteleportationPylon;
			}
			Main.PylonSystem.RequestImmediateUpdate();
			return teteleportationPylon.ID;
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x00507DDC File Offset: 0x00505FDC
		public static void Kill(int x, int y)
		{
			TileEntity tileEntity;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out tileEntity) && tileEntity.type == TETeleportationPylon._myEntityID)
			{
				object entityCreationLock = TileEntity.EntityCreationLock;
				lock (entityCreationLock)
				{
					TileEntity.ByID.Remove(tileEntity.ID);
					TileEntity.ByPosition.Remove(new Point16(x, y));
				}
				Main.PylonSystem.RequestImmediateUpdate();
			}
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x00507E64 File Offset: 0x00506064
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.Position.X,
				"x  ",
				this.Position.Y,
				"y"
			});
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x00507EB4 File Offset: 0x005060B4
		public static void Framing_CheckTile(int callX, int callY)
		{
			if (WorldGen.destroyObject)
			{
				return;
			}
			Tile tileSafely = Framing.GetTileSafely(callX, callY);
			int num = callX - (int)(tileSafely.frameX / 18 % 3);
			int num2 = callY - (int)(tileSafely.frameY / 18 % 4);
			int pylonStyleFromTile = TETeleportationPylon.GetPylonStyleFromTile(tileSafely);
			bool flag = false;
			for (int i = num; i < num + 3; i++)
			{
				for (int j = num2; j < num2 + 4; j++)
				{
					Tile tile = Main.tile[i, j];
					if (!tile.active() || tile.type != 597)
					{
						flag = true;
					}
				}
			}
			if (!WorldGen.SolidTileAllowBottomSlope(num, num2 + 4) || !WorldGen.SolidTileAllowBottomSlope(num + 1, num2 + 4) || !WorldGen.SolidTileAllowBottomSlope(num + 2, num2 + 4))
			{
				flag = true;
			}
			if (flag)
			{
				TETeleportationPylon.Kill(num, num2);
				int pylonItemTypeFromTileStyle = TETeleportationPylon.GetPylonItemTypeFromTileStyle(pylonStyleFromTile);
				Item.NewItem(new EntitySource_TileBreak(num, num2), num * 16, num2 * 16, 48, 64, pylonItemTypeFromTileStyle, 1, false, 0, false, false);
				WorldGen.destroyObject = true;
				for (int k = num; k < num + 3; k++)
				{
					for (int l = num2; l < num2 + 4; l++)
					{
						if (Main.tile[k, l].active() && Main.tile[k, l].type == 597)
						{
							WorldGen.KillTile(k, l, false, false, false);
						}
					}
				}
				WorldGen.destroyObject = false;
			}
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x00508012 File Offset: 0x00506212
		public static int GetPylonStyleFromTile(Tile tile)
		{
			return (int)(tile.frameX / 54);
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x00508020 File Offset: 0x00506220
		public static int GetPylonItemTypeFromTileStyle(int style)
		{
			switch (style)
			{
			case 1:
				return 4875;
			case 2:
				return 4916;
			case 3:
				return 4917;
			case 4:
				return 4918;
			case 5:
				return 4919;
			case 6:
				return 4920;
			case 7:
				return 4921;
			case 8:
				return 4951;
			default:
				return 4876;
			}
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x0050808C File Offset: 0x0050628C
		public override bool IsTileValidForEntity(int x, int y)
		{
			return Main.tile[x, y].active() && Main.tile[x, y].type == 597 && Main.tile[x, y].frameY == 0 && Main.tile[x, y].frameX % 54 == 0;
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x005080F0 File Offset: 0x005062F0
		public static int PlacementPreviewHook_AfterPlacement(int x, int y, int type = 597, int style = 0, int direction = 1, int alternate = 0)
		{
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(Main.myPlayer, x - 1, y - 3, 3, 4, TileChangeType.None);
				NetMessage.SendData(87, -1, -1, null, x + -1, (float)(y + -3), (float)TETeleportationPylon._myEntityID, 0f, 0, 0, 0);
				return -1;
			}
			return TETeleportationPylon.Place(x + -1, y + -3);
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x00508148 File Offset: 0x00506348
		public static int PlacementPreviewHook_CheckIfCanPlace(int x, int y, int type = 597, int style = 0, int direction = 1, int alternate = 0)
		{
			TeleportPylonType pylonTypeFromPylonTileStyle = TETeleportationPylon.GetPylonTypeFromPylonTileStyle(style);
			if (Main.PylonSystem.HasPylonOfType(pylonTypeFromPylonTileStyle))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x0050816C File Offset: 0x0050636C
		private bool TryGetPylonTypeFromTileCoords(int x, int y, out TeleportPylonType pylonType)
		{
			pylonType = TeleportPylonType.SurfacePurity;
			Tile tile = Main.tile[x, y];
			if (tile == null || !tile.active() || tile.type != 597)
			{
				return false;
			}
			int pylonStyle = (int)(tile.frameX / 54);
			pylonType = TETeleportationPylon.GetPylonTypeFromPylonTileStyle(pylonStyle);
			return true;
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x005081B6 File Offset: 0x005063B6
		private static TeleportPylonType GetPylonTypeFromPylonTileStyle(int pylonStyle)
		{
			return (TeleportPylonType)pylonStyle;
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x005081BC File Offset: 0x005063BC
		public static int Find(int x, int y)
		{
			TileEntity tileEntity;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out tileEntity) && tileEntity.type == TETeleportationPylon._myEntityID)
			{
				return tileEntity.ID;
			}
			return -1;
		}

		// Token: 0x0400458C RID: 17804
		private static byte _myEntityID;

		// Token: 0x0400458D RID: 17805
		private const int MyTileID = 597;

		// Token: 0x0400458E RID: 17806
		private const int entityTileWidth = 3;

		// Token: 0x0400458F RID: 17807
		private const int entityTileHeight = 4;
	}
}
