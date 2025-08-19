using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Achievements;
using Terraria.ID;

namespace Terraria.GameContent
{
	// Token: 0x020001E2 RID: 482
	public class MinecartDiggerHelper
	{
		// Token: 0x06001C56 RID: 7254 RVA: 0x004F2E58 File Offset: 0x004F1058
		public void TryDigging(Player player, Vector2 trackWorldPosition, int digDirectionX, int digDirectionY)
		{
			digDirectionY = 0;
			Point point = trackWorldPosition.ToTileCoordinates();
			if (Framing.GetTileSafely(point).type != 314)
			{
				return;
			}
			if ((double)point.Y < Main.worldSurface)
			{
				return;
			}
			Point point2 = point;
			point2.X += digDirectionX;
			point2.Y += digDirectionY;
			if (this.AlreadyLeadsIntoWantedTrack(point, point2))
			{
				return;
			}
			if (digDirectionY == 0)
			{
				if (this.AlreadyLeadsIntoWantedTrack(point, new Point(point2.X, point2.Y - 1)))
				{
					return;
				}
				if (this.AlreadyLeadsIntoWantedTrack(point, new Point(point2.X, point2.Y + 1)))
				{
					return;
				}
			}
			int num = 5;
			if (digDirectionY != 0)
			{
				num = 5;
			}
			Point point3 = point2;
			Point point4 = point3;
			point4.Y -= num - 1;
			int x = point4.X;
			for (int i = point4.Y; i <= point3.Y; i++)
			{
				if (!this.CanGetPastTile(x, i) || !this.HasPickPower(player, x, i))
				{
					return;
				}
			}
			if (!this.CanConsumeATrackItem(player))
			{
				return;
			}
			int x2 = point4.X;
			for (int j = point4.Y; j <= point3.Y; j++)
			{
				this.MineTheTileIfNecessary(x2, j);
			}
			this.ConsumeATrackItem(player);
			this.PlaceATrack(point2.X, point2.Y);
			player.velocity.X = MathHelper.Clamp(player.velocity.X, -1f, 1f);
			if (!this.DoTheTracksConnectProperly(point, point2))
			{
				this.CorrectTrackConnections(point, point2);
			}
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x004F2FD6 File Offset: 0x004F11D6
		private bool CanConsumeATrackItem(Player player)
		{
			return this.FindMinecartTrackItem(player) != null;
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x004F2FE4 File Offset: 0x004F11E4
		private void ConsumeATrackItem(Player player)
		{
			Item item = this.FindMinecartTrackItem(player);
			item.stack--;
			if (item.stack == 0)
			{
				item.TurnToAir(false);
			}
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x004F3018 File Offset: 0x004F1218
		private Item FindMinecartTrackItem(Player player)
		{
			Item result = null;
			for (int i = 0; i < 58; i++)
			{
				if (player.selectedItem != i || (player.itemAnimation <= 0 && player.reuseDelay <= 0 && player.itemTime <= 0))
				{
					Item item = player.inventory[i];
					if (item.type == 2340 && item.stack > 0)
					{
						result = item;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x004F307C File Offset: 0x004F127C
		private void PoundTrack(Point spot)
		{
			if (Main.tile[spot.X, spot.Y].type != 314)
			{
				return;
			}
			if (Minecart.FrameTrack(spot.X, spot.Y, true, false) && Main.netMode == 1)
			{
				NetMessage.SendData(17, -1, -1, null, 15, (float)spot.X, (float)spot.Y, 1f, 0, 0, 0);
			}
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x004F30EC File Offset: 0x004F12EC
		private bool AlreadyLeadsIntoWantedTrack(Point tileCoordsOfFrontWheel, Point tileCoordsWeWantToReach)
		{
			Tile tileSafely = Framing.GetTileSafely(tileCoordsOfFrontWheel);
			Tile tileSafely2 = Framing.GetTileSafely(tileCoordsWeWantToReach);
			if (!tileSafely.active() || tileSafely.type != 314)
			{
				return false;
			}
			if (!tileSafely2.active() || tileSafely2.type != 314)
			{
				return false;
			}
			int? expectedYOffsetForLeft;
			int? expectedYOffsetForRight;
			int? expectedYOffsetForLeft2;
			int? expectedYOffsetForRight2;
			MinecartDiggerHelper.GetExpectedDirections(tileCoordsOfFrontWheel, tileCoordsWeWantToReach, out expectedYOffsetForLeft, out expectedYOffsetForRight, out expectedYOffsetForLeft2, out expectedYOffsetForRight2);
			return Minecart.GetAreExpectationsForSidesMet(tileCoordsOfFrontWheel, expectedYOffsetForLeft, expectedYOffsetForRight) && Minecart.GetAreExpectationsForSidesMet(tileCoordsWeWantToReach, expectedYOffsetForLeft2, expectedYOffsetForRight2);
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x004F3160 File Offset: 0x004F1360
		private static void GetExpectedDirections(Point startCoords, Point endCoords, out int? expectedStartLeft, out int? expectedStartRight, out int? expectedEndLeft, out int? expectedEndRight)
		{
			int num = endCoords.Y - startCoords.Y;
			int num2 = endCoords.X - startCoords.X;
			expectedStartLeft = null;
			expectedStartRight = null;
			expectedEndLeft = null;
			expectedEndRight = null;
			if (num2 == -1)
			{
				expectedStartLeft = new int?(num);
				expectedEndRight = new int?(-num);
			}
			if (num2 == 1)
			{
				expectedStartRight = new int?(num);
				expectedEndLeft = new int?(-num);
			}
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x004F31E1 File Offset: 0x004F13E1
		private bool DoTheTracksConnectProperly(Point tileCoordsOfFrontWheel, Point tileCoordsWeWantToReach)
		{
			return this.AlreadyLeadsIntoWantedTrack(tileCoordsOfFrontWheel, tileCoordsWeWantToReach);
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x004F31EC File Offset: 0x004F13EC
		private void CorrectTrackConnections(Point startCoords, Point endCoords)
		{
			int? expectedYOffsetForLeft;
			int? expectedYOffsetForRight;
			int? expectedYOffsetForLeft2;
			int? expectedYOffsetForRight2;
			MinecartDiggerHelper.GetExpectedDirections(startCoords, endCoords, out expectedYOffsetForLeft, out expectedYOffsetForRight, out expectedYOffsetForLeft2, out expectedYOffsetForRight2);
			Tile tileSafely = Framing.GetTileSafely(startCoords);
			Tile tileSafely2 = Framing.GetTileSafely(endCoords);
			if (tileSafely.active() && tileSafely.type == 314)
			{
				Minecart.TryFittingTileOrientation(startCoords, expectedYOffsetForLeft, expectedYOffsetForRight);
			}
			if (tileSafely2.active() && tileSafely2.type == 314)
			{
				Minecart.TryFittingTileOrientation(endCoords, expectedYOffsetForLeft2, expectedYOffsetForRight2);
			}
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x004F3256 File Offset: 0x004F1456
		private bool HasPickPower(Player player, int x, int y)
		{
			return player.HasEnoughPickPowerToHurtTile(x, y);
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x004F3268 File Offset: 0x004F1468
		private bool CanGetPastTile(int x, int y)
		{
			if (WorldGen.CheckTileBreakability(x, y) != 0)
			{
				return false;
			}
			if (WorldGen.CheckTileBreakability2_ShouldTileSurvive(x, y))
			{
				return false;
			}
			Tile tile = Main.tile[x, y];
			return !tile.active() || (!TileID.Sets.Falling[(int)tile.type] && (tile.type != 26 || Main.hardMode) && WorldGen.CanKillTile(x, y));
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x004F32CC File Offset: 0x004F14CC
		private void PlaceATrack(int x, int y)
		{
			int num = 314;
			int num2 = 0;
			if (!WorldGen.PlaceTile(x, y, num, false, false, Main.myPlayer, num2))
			{
				return;
			}
			NetMessage.SendData(17, -1, -1, null, 1, (float)x, (float)y, (float)num, num2, 0, 0);
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x004F3308 File Offset: 0x004F1508
		private void MineTheTileIfNecessary(int x, int y)
		{
			AchievementsHelper.CurrentlyMining = true;
			if (Main.tile[x, y].active())
			{
				WorldGen.KillTile(x, y, false, false, false);
				NetMessage.SendData(17, -1, -1, null, 0, (float)x, (float)y, 0f, 0, 0, 0);
			}
			AchievementsHelper.CurrentlyMining = false;
		}

		// Token: 0x0400439B RID: 17307
		public static MinecartDiggerHelper Instance = new MinecartDiggerHelper();
	}
}
