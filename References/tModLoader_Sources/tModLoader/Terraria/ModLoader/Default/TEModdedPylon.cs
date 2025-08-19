using System;
using System.Linq;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ObjectData;

namespace Terraria.ModLoader.Default
{
	/// <summary>
	/// This is a TML provided Tile Entity that acts extremely similar to vanilla's pylon TEs by default. If you plan
	/// to make a pylon tile in any capacity, you must extend this TE at least once.
	/// </summary>
	// Token: 0x020002CD RID: 717
	public abstract class TEModdedPylon : ModTileEntity, IPylonTileEntity
	{
		// Token: 0x06002DC4 RID: 11716 RVA: 0x00530030 File Offset: 0x0052E230
		public override void NetPlaceEntityAttempt(int x, int y)
		{
			ModPylon pylon;
			if (!TEModdedPylon.GetModPylonFromCoords(x, y, out pylon))
			{
				TEModdedPylon.RejectPlacementFromNet(x, y);
				return;
			}
			if (!(PylonLoader.PreCanPlacePylon(x, y, (int)pylon.Type, pylon.PylonType) ?? pylon.CanPlacePylon()))
			{
				TEModdedPylon.RejectPlacementFromNet(x, y);
				return;
			}
			int ID = this.Place(x, y);
			((ModTileEntity)TileEntity.ByID[ID]).OnNetPlace();
			NetMessage.SendData(86, -1, -1, null, ID, (float)x, (float)y, 0f, 0, 0, 0);
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x005300BB File Offset: 0x0052E2BB
		public bool TryGetModPylon(out ModPylon modPylon)
		{
			return TEModdedPylon.GetModPylonFromCoords((int)this.Position.X, (int)this.Position.Y, out modPylon);
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x005300DC File Offset: 0x0052E2DC
		private static void RejectPlacementFromNet(int x, int y)
		{
			WorldGen.KillTile(x, y, false, false, false);
			if (Main.netMode == 2)
			{
				NetMessage.SendData(17, -1, -1, null, 0, (float)x, (float)y, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x00530112 File Offset: 0x0052E312
		public new int Place(int i, int j)
		{
			int result = base.Place(i, j);
			Main.PylonSystem.RequestImmediateUpdate();
			return result;
		}

		// Token: 0x06002DC8 RID: 11720 RVA: 0x00530126 File Offset: 0x0052E326
		public new void Kill(int x, int y)
		{
			base.Kill(x, y);
			Main.PylonSystem.RequestImmediateUpdate();
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x0053013A File Offset: 0x0052E33A
		public override string ToString()
		{
			return this.Position.X.ToString() + "x  " + this.Position.Y.ToString() + "y";
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x0053016C File Offset: 0x0052E36C
		public unsafe override bool IsTileValidForEntity(int x, int y)
		{
			TileObjectData tileData = TileObjectData.GetTileData(Main.tile[x, y]);
			return Main.tile[x, y].active() && TileID.Sets.CountsAsPylon.Contains((int)(*Main.tile[x, y].type)) && *Main.tile[x, y].frameY == 0 && (int)(*Main.tile[x, y].frameX) % tileData.CoordinateFullWidth == 0;
		}

		// Token: 0x06002DCB RID: 11723 RVA: 0x005301FC File Offset: 0x0052E3FC
		public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction, int alternate)
		{
			TileObjectData tileData = TileObjectData.GetTileData(type, style, alternate);
			int topLeftX = i - (int)tileData.Origin.X;
			int topLeftY = j - (int)tileData.Origin.Y;
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(Main.myPlayer, topLeftX, topLeftY, tileData.Width, tileData.Height, TileChangeType.None);
				NetMessage.SendData(87, -1, -1, null, topLeftX, (float)topLeftY, (float)base.Type, 0f, 0, 0, 0);
				return -1;
			}
			return this.Place(topLeftX, topLeftY);
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x00530278 File Offset: 0x0052E478
		public int PlacementPreviewHook_CheckIfCanPlace(int x, int y, int type, int style = 0, int direction = 1, int alternate = 0)
		{
			ModPylon pylon = TileLoader.GetTile(type) as ModPylon;
			bool? flag = PylonLoader.PreCanPlacePylon(x, y, type, pylon.PylonType);
			if (flag != null)
			{
				bool value = flag.GetValueOrDefault();
				return (!value) ? 1 : 0;
			}
			return (!pylon.CanPlacePylon()) ? 1 : 0;
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x005302C0 File Offset: 0x0052E4C0
		public unsafe static bool GetModPylonFromCoords(int x, int y, out ModPylon modPylon)
		{
			Tile tile = Main.tile[x, y];
			if (tile.active())
			{
				ModPylon p = TileLoader.GetTile((int)(*tile.type)) as ModPylon;
				if (p != null)
				{
					modPylon = p;
					return true;
				}
			}
			modPylon = null;
			return false;
		}
	}
}
