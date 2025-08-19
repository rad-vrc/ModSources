using System;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terraria.GameContent.Tile_Entities
{
	// Token: 0x02000561 RID: 1377
	public sealed class TETeleportationPylon : TileEntity, IPylonTileEntity
	{
		// Token: 0x060040F6 RID: 16630 RVA: 0x005E3CCE File Offset: 0x005E1ECE
		public override void RegisterTileEntityID(int assignedID)
		{
			TETeleportationPylon._myEntityID = (byte)assignedID;
		}

		// Token: 0x060040F7 RID: 16631 RVA: 0x005E3CD7 File Offset: 0x005E1ED7
		public override TileEntity GenerateInstance()
		{
			return new TETeleportationPylon();
		}

		// Token: 0x060040F8 RID: 16632 RVA: 0x005E3CE0 File Offset: 0x005E1EE0
		public override void NetPlaceEntityAttempt(int x, int y)
		{
			TeleportPylonType pylonType;
			if (!this.TryGetPylonTypeFromTileCoords(x, y, out pylonType))
			{
				TETeleportationPylon.RejectPlacementFromNet(x, y);
				return;
			}
			if (!(PylonLoader.PreCanPlacePylon(x, y, 597, pylonType) ?? (!Main.PylonSystem.HasPylonOfType(pylonType))))
			{
				TETeleportationPylon.RejectPlacementFromNet(x, y);
				return;
			}
			int number = TETeleportationPylon.Place(x, y);
			NetMessage.SendData(86, -1, -1, null, number, (float)x, (float)y, 0f, 0, 0, 0);
		}

		// Token: 0x060040F9 RID: 16633 RVA: 0x005E3D58 File Offset: 0x005E1F58
		public bool TryGetPylonType(out TeleportPylonType pylonType)
		{
			return this.TryGetPylonTypeFromTileCoords((int)this.Position.X, (int)this.Position.Y, out pylonType);
		}

		// Token: 0x060040FA RID: 16634 RVA: 0x005E3D78 File Offset: 0x005E1F78
		private static void RejectPlacementFromNet(int x, int y)
		{
			WorldGen.KillTile(x, y, false, false, false);
			if (Main.netMode == 2)
			{
				NetMessage.SendData(17, -1, -1, null, 0, (float)x, (float)y, 0f, 0, 0, 0);
			}
		}

		// Token: 0x060040FB RID: 16635 RVA: 0x005E3DB0 File Offset: 0x005E1FB0
		public static int Place(int x, int y)
		{
			TETeleportationPylon tETeleportationPylon = new TETeleportationPylon();
			tETeleportationPylon.Position = new Point16(x, y);
			tETeleportationPylon.ID = TileEntity.AssignNewID();
			tETeleportationPylon.type = TETeleportationPylon._myEntityID;
			object entityCreationLock = TileEntity.EntityCreationLock;
			lock (entityCreationLock)
			{
				TileEntity.ByID[tETeleportationPylon.ID] = tETeleportationPylon;
				TileEntity.ByPosition[tETeleportationPylon.Position] = tETeleportationPylon;
			}
			Main.PylonSystem.RequestImmediateUpdate();
			return tETeleportationPylon.ID;
		}

		// Token: 0x060040FC RID: 16636 RVA: 0x005E3E44 File Offset: 0x005E2044
		public static void Kill(int x, int y)
		{
			TileEntity value;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out value) && value.type == TETeleportationPylon._myEntityID)
			{
				object entityCreationLock = TileEntity.EntityCreationLock;
				lock (entityCreationLock)
				{
					TileEntity.ByID.Remove(value.ID);
					TileEntity.ByPosition.Remove(new Point16(x, y));
				}
				Main.PylonSystem.RequestImmediateUpdate();
			}
		}

		// Token: 0x060040FD RID: 16637 RVA: 0x005E3ECC File Offset: 0x005E20CC
		public override string ToString()
		{
			return this.Position.X.ToString() + "x  " + this.Position.Y.ToString() + "y";
		}

		// Token: 0x060040FE RID: 16638 RVA: 0x005E3F00 File Offset: 0x005E2100
		public unsafe static void Framing_CheckTile(int callX, int callY)
		{
			if (WorldGen.destroyObject)
			{
				return;
			}
			Tile tileSafely = Framing.GetTileSafely(callX, callY);
			int num = callX - (int)(*tileSafely.frameX / 18 % 3);
			int num2 = callY - (int)(*tileSafely.frameY / 18 % 4);
			int pylonStyleFromTile = TETeleportationPylon.GetPylonStyleFromTile(tileSafely);
			bool flag = false;
			for (int i = num; i < num + 3; i++)
			{
				for (int j = num2; j < num2 + 4; j++)
				{
					Tile tile = Main.tile[i, j];
					if (!tile.active() || *tile.type != 597)
					{
						flag = true;
					}
				}
			}
			if (!WorldGen.SolidTileAllowBottomSlope(num, num2 + 4) || !WorldGen.SolidTileAllowBottomSlope(num + 1, num2 + 4) || !WorldGen.SolidTileAllowBottomSlope(num + 2, num2 + 4))
			{
				flag = true;
			}
			if (!flag)
			{
				return;
			}
			TETeleportationPylon.Kill(num, num2);
			int pylonItemTypeFromTileStyle = TETeleportationPylon.GetPylonItemTypeFromTileStyle(pylonStyleFromTile);
			if (TileLoader.Drop(callX, callY, 470, true))
			{
				Item.NewItem(new EntitySource_TileBreak(num, num2, null), num * 16, num2 * 16, 48, 64, pylonItemTypeFromTileStyle, 1, false, 0, false, false);
			}
			WorldGen.destroyObject = true;
			for (int k = num; k < num + 3; k++)
			{
				for (int l = num2; l < num2 + 4; l++)
				{
					if (Main.tile[k, l].active() && *Main.tile[k, l].type == 597)
					{
						WorldGen.KillTile(k, l, false, false, false);
					}
				}
			}
			WorldGen.destroyObject = false;
		}

		// Token: 0x060040FF RID: 16639 RVA: 0x005E407A File Offset: 0x005E227A
		public unsafe static int GetPylonStyleFromTile(Tile tile)
		{
			return (int)(*tile.frameX / 54);
		}

		// Token: 0x06004100 RID: 16640 RVA: 0x005E4088 File Offset: 0x005E2288
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

		// Token: 0x06004101 RID: 16641 RVA: 0x005E40F4 File Offset: 0x005E22F4
		public unsafe override bool IsTileValidForEntity(int x, int y)
		{
			return Main.tile[x, y].active() && *Main.tile[x, y].type == 597 && *Main.tile[x, y].frameY == 0 && *Main.tile[x, y].frameX % 54 == 0;
		}

		// Token: 0x06004102 RID: 16642 RVA: 0x005E4168 File Offset: 0x005E2368
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

		// Token: 0x06004103 RID: 16643 RVA: 0x005E41C0 File Offset: 0x005E23C0
		public static int PlacementPreviewHook_CheckIfCanPlace(int x, int y, int type = 597, int style = 0, int direction = 1, int alternate = 0)
		{
			bool? flag = PylonLoader.PreCanPlacePylon(x, y, type, TETeleportationPylon.GetPylonTypeFromPylonTileStyle(style));
			if (flag != null)
			{
				bool value = flag.GetValueOrDefault();
				return (!value) ? 1 : 0;
			}
			TeleportPylonType pylonTypeFromPylonTileStyle = TETeleportationPylon.GetPylonTypeFromPylonTileStyle(style);
			if (Main.PylonSystem.HasPylonOfType(pylonTypeFromPylonTileStyle))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06004104 RID: 16644 RVA: 0x005E420C File Offset: 0x005E240C
		private unsafe bool TryGetPylonTypeFromTileCoords(int x, int y, out TeleportPylonType pylonType)
		{
			pylonType = TeleportPylonType.SurfacePurity;
			Tile tile = Main.tile[x, y];
			if (tile == null || !tile.active() || *tile.type != 597)
			{
				return false;
			}
			int pylonStyle = (int)(*tile.frameX / 54);
			pylonType = TETeleportationPylon.GetPylonTypeFromPylonTileStyle(pylonStyle);
			return true;
		}

		// Token: 0x06004105 RID: 16645 RVA: 0x005E4261 File Offset: 0x005E2461
		private static TeleportPylonType GetPylonTypeFromPylonTileStyle(int pylonStyle)
		{
			return (TeleportPylonType)pylonStyle;
		}

		// Token: 0x06004106 RID: 16646 RVA: 0x005E4268 File Offset: 0x005E2468
		public static int Find(int x, int y)
		{
			TileEntity value;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out value) && value.type == TETeleportationPylon._myEntityID)
			{
				return value.ID;
			}
			return -1;
		}

		// Token: 0x0400589C RID: 22684
		private static byte _myEntityID;

		// Token: 0x0400589D RID: 22685
		private const int MyTileID = 597;

		// Token: 0x0400589E RID: 22686
		private const int entityTileWidth = 3;

		// Token: 0x0400589F RID: 22687
		private const int entityTileHeight = 4;
	}
}
