using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.GameInput;

namespace Terraria.GameContent
{
	// Token: 0x020001D1 RID: 465
	public class DoorOpeningHelper
	{
		// Token: 0x06001BFA RID: 7162 RVA: 0x004F0D9B File Offset: 0x004EEF9B
		public void AllowOpeningDoorsByVelocityAloneForATime(int timeInFramesToAllow)
		{
			this._timeWeCanOpenDoorsUsingVelocityAlone = timeInFramesToAllow;
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x004F0DA4 File Offset: 0x004EEFA4
		public void Update(Player player)
		{
			this.LookForDoorsToClose(player);
			if (this.ShouldTryOpeningDoors())
			{
				this.LookForDoorsToOpen(player);
			}
			if (this._timeWeCanOpenDoorsUsingVelocityAlone > 0)
			{
				this._timeWeCanOpenDoorsUsingVelocityAlone--;
			}
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x004F0DD4 File Offset: 0x004EEFD4
		private bool ShouldTryOpeningDoors()
		{
			switch (DoorOpeningHelper.PreferenceSettings)
			{
			default:
				return false;
			case DoorOpeningHelper.DoorAutoOpeningPreference.EnabledForGamepadOnly:
				return PlayerInput.UsingGamepad;
			case DoorOpeningHelper.DoorAutoOpeningPreference.EnabledForEverything:
				return true;
			}
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x004F0E04 File Offset: 0x004EF004
		public static void CyclePreferences()
		{
			switch (DoorOpeningHelper.PreferenceSettings)
			{
			case DoorOpeningHelper.DoorAutoOpeningPreference.Disabled:
				DoorOpeningHelper.PreferenceSettings = DoorOpeningHelper.DoorAutoOpeningPreference.EnabledForEverything;
				return;
			case DoorOpeningHelper.DoorAutoOpeningPreference.EnabledForGamepadOnly:
				DoorOpeningHelper.PreferenceSettings = DoorOpeningHelper.DoorAutoOpeningPreference.Disabled;
				return;
			case DoorOpeningHelper.DoorAutoOpeningPreference.EnabledForEverything:
				DoorOpeningHelper.PreferenceSettings = DoorOpeningHelper.DoorAutoOpeningPreference.EnabledForGamepadOnly;
				return;
			default:
				return;
			}
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x004F0E40 File Offset: 0x004EF040
		public void LookForDoorsToClose(Player player)
		{
			DoorOpeningHelper.PlayerInfoForClosingDoors playerInfoForClosingDoor = this.GetPlayerInfoForClosingDoor(player);
			for (int i = this._ongoingOpenDoors.Count - 1; i >= 0; i--)
			{
				DoorOpeningHelper.DoorOpenCloseTogglingInfo doorOpenCloseTogglingInfo = this._ongoingOpenDoors[i];
				DoorOpeningHelper.DoorCloseAttemptResult doorCloseAttemptResult = doorOpenCloseTogglingInfo.handler.TryCloseDoor(doorOpenCloseTogglingInfo, playerInfoForClosingDoor);
				if (doorCloseAttemptResult != DoorOpeningHelper.DoorCloseAttemptResult.StillInDoorArea)
				{
					this._ongoingOpenDoors.RemoveAt(i);
				}
			}
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x004F0E98 File Offset: 0x004EF098
		private DoorOpeningHelper.PlayerInfoForClosingDoors GetPlayerInfoForClosingDoor(Player player)
		{
			return new DoorOpeningHelper.PlayerInfoForClosingDoors
			{
				hitboxToNotCloseDoor = player.Hitbox
			};
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x004F0EBC File Offset: 0x004EF0BC
		public void LookForDoorsToOpen(Player player)
		{
			DoorOpeningHelper.PlayerInfoForOpeningDoors playerInfoForOpeningDoor = this.GetPlayerInfoForOpeningDoor(player);
			if (playerInfoForOpeningDoor.intendedOpeningDirection == 0 && player.velocity.X == 0f)
			{
				return;
			}
			Point tileCoords = default(Point);
			for (int i = playerInfoForOpeningDoor.tileCoordSpaceForCheckingForDoors.Left; i <= playerInfoForOpeningDoor.tileCoordSpaceForCheckingForDoors.Right; i++)
			{
				for (int j = playerInfoForOpeningDoor.tileCoordSpaceForCheckingForDoors.Top; j <= playerInfoForOpeningDoor.tileCoordSpaceForCheckingForDoors.Bottom; j++)
				{
					tileCoords.X = i;
					tileCoords.Y = j;
					this.TryAutoOpeningDoor(tileCoords, playerInfoForOpeningDoor);
				}
			}
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x004F0F50 File Offset: 0x004EF150
		private DoorOpeningHelper.PlayerInfoForOpeningDoors GetPlayerInfoForOpeningDoor(Player player)
		{
			int num = player.controlRight.ToInt() - player.controlLeft.ToInt();
			int playerGravityDirection = (int)player.gravDir;
			Rectangle hitbox = player.Hitbox;
			hitbox.Y -= -1;
			hitbox.Height += -2;
			float num2 = player.velocity.X;
			if (num == 0 && this._timeWeCanOpenDoorsUsingVelocityAlone == 0)
			{
				num2 = 0f;
			}
			float value = (float)num + num2;
			int num3 = Math.Sign(value) * (int)Math.Ceiling((double)Math.Abs(value));
			hitbox.X += num3;
			if (num == 0)
			{
				num = Math.Sign(value);
			}
			Rectangle hitbox2;
			Rectangle value2 = hitbox2 = player.Hitbox;
			hitbox2.X += num3;
			Rectangle r = Rectangle.Union(value2, hitbox2);
			Point point = r.TopLeft().ToTileCoordinates();
			Point point2 = r.BottomRight().ToTileCoordinates();
			Rectangle tileCoordSpaceForCheckingForDoors = new Rectangle(point.X, point.Y, point2.X - point.X, point2.Y - point.Y);
			return new DoorOpeningHelper.PlayerInfoForOpeningDoors
			{
				hitboxToOpenDoor = hitbox,
				intendedOpeningDirection = num,
				playerGravityDirection = playerGravityDirection,
				tileCoordSpaceForCheckingForDoors = tileCoordSpaceForCheckingForDoors
			};
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x004F1084 File Offset: 0x004EF284
		private void TryAutoOpeningDoor(Point tileCoords, DoorOpeningHelper.PlayerInfoForOpeningDoors playerInfo)
		{
			DoorOpeningHelper.DoorAutoHandler doorAutoHandler;
			if (!this.TryGetHandler(tileCoords, out doorAutoHandler))
			{
				return;
			}
			DoorOpeningHelper.DoorOpenCloseTogglingInfo doorOpenCloseTogglingInfo = doorAutoHandler.ProvideInfo(tileCoords);
			if (!doorAutoHandler.TryOpenDoor(doorOpenCloseTogglingInfo, playerInfo))
			{
				return;
			}
			this._ongoingOpenDoors.Add(doorOpenCloseTogglingInfo);
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x004F10BC File Offset: 0x004EF2BC
		private bool TryGetHandler(Point tileCoords, out DoorOpeningHelper.DoorAutoHandler infoProvider)
		{
			infoProvider = null;
			if (!WorldGen.InWorld(tileCoords.X, tileCoords.Y, 3))
			{
				return false;
			}
			Tile tile = Main.tile[tileCoords.X, tileCoords.Y];
			return tile != null && this._handlerByTileType.TryGetValue((int)tile.type, out infoProvider);
		}

		// Token: 0x0400435A RID: 17242
		public static DoorOpeningHelper.DoorAutoOpeningPreference PreferenceSettings = DoorOpeningHelper.DoorAutoOpeningPreference.EnabledForEverything;

		// Token: 0x0400435B RID: 17243
		private Dictionary<int, DoorOpeningHelper.DoorAutoHandler> _handlerByTileType = new Dictionary<int, DoorOpeningHelper.DoorAutoHandler>
		{
			{
				10,
				new DoorOpeningHelper.CommonDoorOpeningInfoProvider()
			},
			{
				388,
				new DoorOpeningHelper.TallGateOpeningInfoProvider()
			}
		};

		// Token: 0x0400435C RID: 17244
		private List<DoorOpeningHelper.DoorOpenCloseTogglingInfo> _ongoingOpenDoors = new List<DoorOpeningHelper.DoorOpenCloseTogglingInfo>();

		// Token: 0x0400435D RID: 17245
		private int _timeWeCanOpenDoorsUsingVelocityAlone;

		// Token: 0x020005F4 RID: 1524
		public enum DoorAutoOpeningPreference
		{
			// Token: 0x0400600A RID: 24586
			Disabled,
			// Token: 0x0400600B RID: 24587
			EnabledForGamepadOnly,
			// Token: 0x0400600C RID: 24588
			EnabledForEverything
		}

		// Token: 0x020005F5 RID: 1525
		private enum DoorCloseAttemptResult
		{
			// Token: 0x0400600E RID: 24590
			StillInDoorArea,
			// Token: 0x0400600F RID: 24591
			ClosedDoor,
			// Token: 0x04006010 RID: 24592
			FailedToCloseDoor,
			// Token: 0x04006011 RID: 24593
			DoorIsInvalidated
		}

		// Token: 0x020005F6 RID: 1526
		private struct DoorOpenCloseTogglingInfo
		{
			// Token: 0x04006012 RID: 24594
			public Point tileCoordsForToggling;

			// Token: 0x04006013 RID: 24595
			public DoorOpeningHelper.DoorAutoHandler handler;
		}

		// Token: 0x020005F7 RID: 1527
		private struct PlayerInfoForOpeningDoors
		{
			// Token: 0x04006014 RID: 24596
			public Rectangle hitboxToOpenDoor;

			// Token: 0x04006015 RID: 24597
			public int intendedOpeningDirection;

			// Token: 0x04006016 RID: 24598
			public int playerGravityDirection;

			// Token: 0x04006017 RID: 24599
			public Rectangle tileCoordSpaceForCheckingForDoors;
		}

		// Token: 0x020005F8 RID: 1528
		private struct PlayerInfoForClosingDoors
		{
			// Token: 0x04006018 RID: 24600
			public Rectangle hitboxToNotCloseDoor;
		}

		// Token: 0x020005F9 RID: 1529
		private interface DoorAutoHandler
		{
			// Token: 0x0600330E RID: 13070
			DoorOpeningHelper.DoorOpenCloseTogglingInfo ProvideInfo(Point tileCoords);

			// Token: 0x0600330F RID: 13071
			bool TryOpenDoor(DoorOpeningHelper.DoorOpenCloseTogglingInfo info, DoorOpeningHelper.PlayerInfoForOpeningDoors playerInfo);

			// Token: 0x06003310 RID: 13072
			DoorOpeningHelper.DoorCloseAttemptResult TryCloseDoor(DoorOpeningHelper.DoorOpenCloseTogglingInfo info, DoorOpeningHelper.PlayerInfoForClosingDoors playerInfo);
		}

		// Token: 0x020005FA RID: 1530
		private class CommonDoorOpeningInfoProvider : DoorOpeningHelper.DoorAutoHandler
		{
			// Token: 0x06003311 RID: 13073 RVA: 0x00604D6C File Offset: 0x00602F6C
			public DoorOpeningHelper.DoorOpenCloseTogglingInfo ProvideInfo(Point tileCoords)
			{
				Tile tile = Main.tile[tileCoords.X, tileCoords.Y];
				Point tileCoordsForToggling = tileCoords;
				tileCoordsForToggling.Y -= (int)(tile.frameY % 54 / 18);
				return new DoorOpeningHelper.DoorOpenCloseTogglingInfo
				{
					handler = this,
					tileCoordsForToggling = tileCoordsForToggling
				};
			}

			// Token: 0x06003312 RID: 13074 RVA: 0x00604DC4 File Offset: 0x00602FC4
			public bool TryOpenDoor(DoorOpeningHelper.DoorOpenCloseTogglingInfo doorInfo, DoorOpeningHelper.PlayerInfoForOpeningDoors playerInfo)
			{
				Point tileCoordsForToggling = doorInfo.tileCoordsForToggling;
				int intendedOpeningDirection = playerInfo.intendedOpeningDirection;
				Rectangle rectangle = new Rectangle(doorInfo.tileCoordsForToggling.X * 16, doorInfo.tileCoordsForToggling.Y * 16, 16, 48);
				int playerGravityDirection = playerInfo.playerGravityDirection;
				if (playerGravityDirection != -1)
				{
					if (playerGravityDirection == 1)
					{
						rectangle.Height += 16;
					}
				}
				else
				{
					rectangle.Y -= 16;
					rectangle.Height += 16;
				}
				if (!rectangle.Intersects(playerInfo.hitboxToOpenDoor))
				{
					return false;
				}
				if (playerInfo.hitboxToOpenDoor.Top < rectangle.Top || playerInfo.hitboxToOpenDoor.Bottom > rectangle.Bottom)
				{
					return false;
				}
				WorldGen.OpenDoor(tileCoordsForToggling.X, tileCoordsForToggling.Y, intendedOpeningDirection);
				if (Main.tile[tileCoordsForToggling.X, tileCoordsForToggling.Y].type != 10)
				{
					NetMessage.SendData(19, -1, -1, null, 0, (float)tileCoordsForToggling.X, (float)tileCoordsForToggling.Y, (float)intendedOpeningDirection, 0, 0, 0);
					return true;
				}
				WorldGen.OpenDoor(tileCoordsForToggling.X, tileCoordsForToggling.Y, -intendedOpeningDirection);
				if (Main.tile[tileCoordsForToggling.X, tileCoordsForToggling.Y].type != 10)
				{
					NetMessage.SendData(19, -1, -1, null, 0, (float)tileCoordsForToggling.X, (float)tileCoordsForToggling.Y, (float)(-(float)intendedOpeningDirection), 0, 0, 0);
					return true;
				}
				return false;
			}

			// Token: 0x06003313 RID: 13075 RVA: 0x00604F24 File Offset: 0x00603124
			public DoorOpeningHelper.DoorCloseAttemptResult TryCloseDoor(DoorOpeningHelper.DoorOpenCloseTogglingInfo info, DoorOpeningHelper.PlayerInfoForClosingDoors playerInfo)
			{
				Point tileCoordsForToggling = info.tileCoordsForToggling;
				Tile tile = Main.tile[tileCoordsForToggling.X, tileCoordsForToggling.Y];
				if (!tile.active() || tile.type != 11)
				{
					return DoorOpeningHelper.DoorCloseAttemptResult.DoorIsInvalidated;
				}
				int num = (int)(tile.frameX % 72 / 18);
				Rectangle value = new Rectangle(tileCoordsForToggling.X * 16, tileCoordsForToggling.Y * 16, 16, 48);
				if (num != 1)
				{
					if (num == 2)
					{
						value.X += 16;
					}
				}
				else
				{
					value.X -= 16;
				}
				value.Inflate(1, 0);
				Rectangle rectangle = Rectangle.Intersect(value, playerInfo.hitboxToNotCloseDoor);
				if (rectangle.Width > 0 || rectangle.Height > 0)
				{
					return DoorOpeningHelper.DoorCloseAttemptResult.StillInDoorArea;
				}
				if (WorldGen.CloseDoor(tileCoordsForToggling.X, tileCoordsForToggling.Y, false))
				{
					NetMessage.SendData(13, -1, -1, null, Main.myPlayer, 0f, 0f, 0f, 0, 0, 0);
					NetMessage.SendData(19, -1, -1, null, 1, (float)tileCoordsForToggling.X, (float)tileCoordsForToggling.Y, 1f, 0, 0, 0);
					return DoorOpeningHelper.DoorCloseAttemptResult.ClosedDoor;
				}
				return DoorOpeningHelper.DoorCloseAttemptResult.FailedToCloseDoor;
			}
		}

		// Token: 0x020005FB RID: 1531
		private class TallGateOpeningInfoProvider : DoorOpeningHelper.DoorAutoHandler
		{
			// Token: 0x06003315 RID: 13077 RVA: 0x0060503C File Offset: 0x0060323C
			public DoorOpeningHelper.DoorOpenCloseTogglingInfo ProvideInfo(Point tileCoords)
			{
				Tile tile = Main.tile[tileCoords.X, tileCoords.Y];
				Point tileCoordsForToggling = tileCoords;
				tileCoordsForToggling.Y -= (int)(tile.frameY % 90 / 18);
				return new DoorOpeningHelper.DoorOpenCloseTogglingInfo
				{
					handler = this,
					tileCoordsForToggling = tileCoordsForToggling
				};
			}

			// Token: 0x06003316 RID: 13078 RVA: 0x00605094 File Offset: 0x00603294
			public bool TryOpenDoor(DoorOpeningHelper.DoorOpenCloseTogglingInfo doorInfo, DoorOpeningHelper.PlayerInfoForOpeningDoors playerInfo)
			{
				Point tileCoordsForToggling = doorInfo.tileCoordsForToggling;
				Rectangle rectangle = new Rectangle(doorInfo.tileCoordsForToggling.X * 16, doorInfo.tileCoordsForToggling.Y * 16, 16, 80);
				int playerGravityDirection = playerInfo.playerGravityDirection;
				if (playerGravityDirection != -1)
				{
					if (playerGravityDirection == 1)
					{
						rectangle.Height += 16;
					}
				}
				else
				{
					rectangle.Y -= 16;
					rectangle.Height += 16;
				}
				if (!rectangle.Intersects(playerInfo.hitboxToOpenDoor))
				{
					return false;
				}
				if (playerInfo.hitboxToOpenDoor.Top < rectangle.Top || playerInfo.hitboxToOpenDoor.Bottom > rectangle.Bottom)
				{
					return false;
				}
				bool flag = false;
				if (WorldGen.ShiftTallGate(tileCoordsForToggling.X, tileCoordsForToggling.Y, flag, false))
				{
					NetMessage.SendData(19, -1, -1, null, 4 + flag.ToInt(), (float)tileCoordsForToggling.X, (float)tileCoordsForToggling.Y, 0f, 0, 0, 0);
					return true;
				}
				return false;
			}

			// Token: 0x06003317 RID: 13079 RVA: 0x00605188 File Offset: 0x00603388
			public DoorOpeningHelper.DoorCloseAttemptResult TryCloseDoor(DoorOpeningHelper.DoorOpenCloseTogglingInfo info, DoorOpeningHelper.PlayerInfoForClosingDoors playerInfo)
			{
				Point tileCoordsForToggling = info.tileCoordsForToggling;
				Tile tile = Main.tile[tileCoordsForToggling.X, tileCoordsForToggling.Y];
				if (!tile.active() || tile.type != 389)
				{
					return DoorOpeningHelper.DoorCloseAttemptResult.DoorIsInvalidated;
				}
				short num = tile.frameY % 90 / 18;
				Rectangle value = new Rectangle(tileCoordsForToggling.X * 16, tileCoordsForToggling.Y * 16, 16, 80);
				value.Inflate(1, 0);
				Rectangle rectangle = Rectangle.Intersect(value, playerInfo.hitboxToNotCloseDoor);
				if (rectangle.Width > 0 || rectangle.Height > 0)
				{
					return DoorOpeningHelper.DoorCloseAttemptResult.StillInDoorArea;
				}
				bool flag = true;
				if (WorldGen.ShiftTallGate(tileCoordsForToggling.X, tileCoordsForToggling.Y, flag, false))
				{
					NetMessage.SendData(13, -1, -1, null, Main.myPlayer, 0f, 0f, 0f, 0, 0, 0);
					NetMessage.SendData(19, -1, -1, null, 4 + flag.ToInt(), (float)tileCoordsForToggling.X, (float)tileCoordsForToggling.Y, 0f, 0, 0, 0);
					return DoorOpeningHelper.DoorCloseAttemptResult.ClosedDoor;
				}
				return DoorOpeningHelper.DoorCloseAttemptResult.FailedToCloseDoor;
			}
		}
	}
}
