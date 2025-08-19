using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Achievements;
using Terraria.ID;

namespace Terraria.GameContent
{
	// Token: 0x020004A5 RID: 1189
	public class MinecartDiggerHelper
	{
		// Token: 0x06003966 RID: 14694 RVA: 0x00597DC8 File Offset: 0x00595FC8
		public unsafe void TryDigging(Player player, Vector2 trackWorldPosition, int digDirectionX, int digDirectionY)
		{
			digDirectionY = 0;
			Point point = trackWorldPosition.ToTileCoordinates();
			if (*Framing.GetTileSafely(point).type != 314 || (double)point.Y < Main.worldSurface)
			{
				return;
			}
			Point point2 = point;
			point2.X += digDirectionX;
			point2.Y += digDirectionY;
			if (this.AlreadyLeadsIntoWantedTrack(point, point2) || (digDirectionY == 0 && (this.AlreadyLeadsIntoWantedTrack(point, new Point(point2.X, point2.Y - 1)) || this.AlreadyLeadsIntoWantedTrack(point, new Point(point2.X, point2.Y + 1)))))
			{
				return;
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
			if (this.CanConsumeATrackItem(player))
			{
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
		}

		// Token: 0x06003967 RID: 14695 RVA: 0x00597F47 File Offset: 0x00596147
		private bool CanConsumeATrackItem(Player player)
		{
			return this.FindMinecartTrackItem(player) != null;
		}

		// Token: 0x06003968 RID: 14696 RVA: 0x00597F54 File Offset: 0x00596154
		private void ConsumeATrackItem(Player player)
		{
			Item item = this.FindMinecartTrackItem(player);
			item.stack--;
			if (item.stack == 0)
			{
				item.TurnToAir(false);
			}
		}

		// Token: 0x06003969 RID: 14697 RVA: 0x00597F88 File Offset: 0x00596188
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

		// Token: 0x0600396A RID: 14698 RVA: 0x00597FEC File Offset: 0x005961EC
		private unsafe void PoundTrack(Point spot)
		{
			if (*Main.tile[spot.X, spot.Y].type == 314 && Minecart.FrameTrack(spot.X, spot.Y, true, false) && Main.netMode == 1)
			{
				NetMessage.SendData(17, -1, -1, null, 15, (float)spot.X, (float)spot.Y, 1f, 0, 0, 0);
			}
		}

		// Token: 0x0600396B RID: 14699 RVA: 0x00598060 File Offset: 0x00596260
		private unsafe bool AlreadyLeadsIntoWantedTrack(Point tileCoordsOfFrontWheel, Point tileCoordsWeWantToReach)
		{
			Tile tileSafely = Framing.GetTileSafely(tileCoordsOfFrontWheel);
			Tile tileSafely2 = Framing.GetTileSafely(tileCoordsWeWantToReach);
			if (!tileSafely.active() || *tileSafely.type != 314)
			{
				return false;
			}
			if (!tileSafely2.active() || *tileSafely2.type != 314)
			{
				return false;
			}
			int? expectedStartLeft;
			int? expectedStartRight;
			int? expectedEndLeft;
			int? expectedEndRight;
			MinecartDiggerHelper.GetExpectedDirections(tileCoordsOfFrontWheel, tileCoordsWeWantToReach, out expectedStartLeft, out expectedStartRight, out expectedEndLeft, out expectedEndRight);
			return Minecart.GetAreExpectationsForSidesMet(tileCoordsOfFrontWheel, expectedStartLeft, expectedStartRight) && Minecart.GetAreExpectationsForSidesMet(tileCoordsWeWantToReach, expectedEndLeft, expectedEndRight);
		}

		// Token: 0x0600396C RID: 14700 RVA: 0x005980DC File Offset: 0x005962DC
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

		// Token: 0x0600396D RID: 14701 RVA: 0x0059815D File Offset: 0x0059635D
		private bool DoTheTracksConnectProperly(Point tileCoordsOfFrontWheel, Point tileCoordsWeWantToReach)
		{
			return this.AlreadyLeadsIntoWantedTrack(tileCoordsOfFrontWheel, tileCoordsWeWantToReach);
		}

		// Token: 0x0600396E RID: 14702 RVA: 0x00598168 File Offset: 0x00596368
		private unsafe void CorrectTrackConnections(Point startCoords, Point endCoords)
		{
			int? expectedStartLeft;
			int? expectedStartRight;
			int? expectedEndLeft;
			int? expectedEndRight;
			MinecartDiggerHelper.GetExpectedDirections(startCoords, endCoords, out expectedStartLeft, out expectedStartRight, out expectedEndLeft, out expectedEndRight);
			Tile tileSafely = Framing.GetTileSafely(startCoords);
			Tile tileSafely2 = Framing.GetTileSafely(endCoords);
			if (tileSafely.active() && *tileSafely.type == 314)
			{
				Minecart.TryFittingTileOrientation(startCoords, expectedStartLeft, expectedStartRight);
			}
			if (tileSafely2.active() && *tileSafely2.type == 314)
			{
				Minecart.TryFittingTileOrientation(endCoords, expectedEndLeft, expectedEndRight);
			}
		}

		// Token: 0x0600396F RID: 14703 RVA: 0x005981D4 File Offset: 0x005963D4
		private bool HasPickPower(Player player, int x, int y)
		{
			return player.HasEnoughPickPowerToHurtTile(x, y);
		}

		// Token: 0x06003970 RID: 14704 RVA: 0x005981E4 File Offset: 0x005963E4
		private unsafe bool CanGetPastTile(int x, int y)
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
			return !tile.active() || (!TileID.Sets.Falling[(int)(*tile.type)] && (*tile.type != 26 || Main.hardMode) && WorldGen.CanKillTile(x, y));
		}

		// Token: 0x06003971 RID: 14705 RVA: 0x0059824C File Offset: 0x0059644C
		private void PlaceATrack(int x, int y)
		{
			int num = 314;
			int num2 = 0;
			if (WorldGen.PlaceTile(x, y, num, false, false, Main.myPlayer, num2))
			{
				NetMessage.SendData(17, -1, -1, null, 1, (float)x, (float)y, (float)num, num2, 0, 0);
			}
		}

		// Token: 0x06003972 RID: 14706 RVA: 0x00598288 File Offset: 0x00596488
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

		// Token: 0x0400525B RID: 21083
		public static MinecartDiggerHelper Instance = new MinecartDiggerHelper();
	}
}
