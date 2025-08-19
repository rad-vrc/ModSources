using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terraria.GameContent
{
	// Token: 0x02000496 RID: 1174
	public class DoorOpeningHelper
	{
		// Token: 0x06003917 RID: 14615 RVA: 0x00596511 File Offset: 0x00594711
		public void AllowOpeningDoorsByVelocityAloneForATime(int timeInFramesToAllow)
		{
			this._timeWeCanOpenDoorsUsingVelocityAlone = timeInFramesToAllow;
		}

		// Token: 0x06003918 RID: 14616 RVA: 0x0059651A File Offset: 0x0059471A
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

		// Token: 0x06003919 RID: 14617 RVA: 0x0059654C File Offset: 0x0059474C
		private bool ShouldTryOpeningDoors()
		{
			DoorOpeningHelper.DoorAutoOpeningPreference preferenceSettings = DoorOpeningHelper.PreferenceSettings;
			if (preferenceSettings != DoorOpeningHelper.DoorAutoOpeningPreference.EnabledForGamepadOnly)
			{
				return preferenceSettings == DoorOpeningHelper.DoorAutoOpeningPreference.EnabledForEverything;
			}
			return PlayerInput.UsingGamepad;
		}

		// Token: 0x0600391A RID: 14618 RVA: 0x00596570 File Offset: 0x00594770
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

		// Token: 0x0600391B RID: 14619 RVA: 0x005965AC File Offset: 0x005947AC
		public void LookForDoorsToClose(Player player)
		{
			DoorOpeningHelper.PlayerInfoForClosingDoors playerInfoForClosingDoor = this.GetPlayerInfoForClosingDoor(player);
			for (int num = this._ongoingOpenDoors.Count - 1; num >= 0; num--)
			{
				DoorOpeningHelper.DoorOpenCloseTogglingInfo info = this._ongoingOpenDoors[num];
				if (info.handler.TryCloseDoor(info, playerInfoForClosingDoor) != DoorOpeningHelper.DoorCloseAttemptResult.StillInDoorArea)
				{
					this._ongoingOpenDoors.RemoveAt(num);
				}
			}
		}

		// Token: 0x0600391C RID: 14620 RVA: 0x00596604 File Offset: 0x00594804
		private DoorOpeningHelper.PlayerInfoForClosingDoors GetPlayerInfoForClosingDoor(Player player)
		{
			return new DoorOpeningHelper.PlayerInfoForClosingDoors
			{
				hitboxToNotCloseDoor = player.Hitbox
			};
		}

		// Token: 0x0600391D RID: 14621 RVA: 0x00596628 File Offset: 0x00594828
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

		// Token: 0x0600391E RID: 14622 RVA: 0x005966BC File Offset: 0x005948BC
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
			Rectangle rectangle = hitbox2 = player.Hitbox;
			hitbox2.X += num3;
			Rectangle r = Rectangle.Union(rectangle, hitbox2);
			Point point = r.TopLeft().ToTileCoordinates();
			Point point2 = r.BottomRight().ToTileCoordinates();
			Rectangle tileCoordSpaceForCheckingForDoors;
			tileCoordSpaceForCheckingForDoors..ctor(point.X, point.Y, point2.X - point.X, point2.Y - point.Y);
			return new DoorOpeningHelper.PlayerInfoForOpeningDoors
			{
				hitboxToOpenDoor = hitbox,
				intendedOpeningDirection = num,
				playerGravityDirection = playerGravityDirection,
				tileCoordSpaceForCheckingForDoors = tileCoordSpaceForCheckingForDoors
			};
		}

		// Token: 0x0600391F RID: 14623 RVA: 0x005967F0 File Offset: 0x005949F0
		private void TryAutoOpeningDoor(Point tileCoords, DoorOpeningHelper.PlayerInfoForOpeningDoors playerInfo)
		{
			DoorOpeningHelper.DoorAutoHandler infoProvider;
			if (this.TryGetHandler(tileCoords, out infoProvider))
			{
				DoorOpeningHelper.DoorOpenCloseTogglingInfo doorOpenCloseTogglingInfo = infoProvider.ProvideInfo(tileCoords);
				if (infoProvider.TryOpenDoor(doorOpenCloseTogglingInfo, playerInfo))
				{
					this._ongoingOpenDoors.Add(doorOpenCloseTogglingInfo);
				}
			}
		}

		// Token: 0x06003920 RID: 14624 RVA: 0x00596828 File Offset: 0x00594A28
		private unsafe bool TryGetHandler(Point tileCoords, out DoorOpeningHelper.DoorAutoHandler infoProvider)
		{
			infoProvider = null;
			if (!WorldGen.InWorld(tileCoords.X, tileCoords.Y, 3))
			{
				return false;
			}
			Tile tile = Main.tile[tileCoords.X, tileCoords.Y];
			if (tile == null)
			{
				return false;
			}
			int type = (int)(*tile.type);
			ModTile modTile = ModContent.GetModTile(type);
			if (modTile != null && TileID.Sets.OpenDoorID[(int)modTile.Type] > -1)
			{
				type = 10;
			}
			return this._handlerByTileType.TryGetValue(type, out infoProvider);
		}

		// Token: 0x0400523B RID: 21051
		public static DoorOpeningHelper.DoorAutoOpeningPreference PreferenceSettings = DoorOpeningHelper.DoorAutoOpeningPreference.EnabledForEverything;

		// Token: 0x0400523C RID: 21052
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

		// Token: 0x0400523D RID: 21053
		private List<DoorOpeningHelper.DoorOpenCloseTogglingInfo> _ongoingOpenDoors = new List<DoorOpeningHelper.DoorOpenCloseTogglingInfo>();

		// Token: 0x0400523E RID: 21054
		private int _timeWeCanOpenDoorsUsingVelocityAlone;

		// Token: 0x02000B9F RID: 2975
		public enum DoorAutoOpeningPreference
		{
			// Token: 0x040076A5 RID: 30373
			Disabled,
			// Token: 0x040076A6 RID: 30374
			EnabledForGamepadOnly,
			// Token: 0x040076A7 RID: 30375
			EnabledForEverything
		}

		// Token: 0x02000BA0 RID: 2976
		private enum DoorCloseAttemptResult
		{
			// Token: 0x040076A9 RID: 30377
			StillInDoorArea,
			// Token: 0x040076AA RID: 30378
			ClosedDoor,
			// Token: 0x040076AB RID: 30379
			FailedToCloseDoor,
			// Token: 0x040076AC RID: 30380
			DoorIsInvalidated
		}

		// Token: 0x02000BA1 RID: 2977
		private struct DoorOpenCloseTogglingInfo
		{
			// Token: 0x040076AD RID: 30381
			public Point tileCoordsForToggling;

			// Token: 0x040076AE RID: 30382
			public DoorOpeningHelper.DoorAutoHandler handler;
		}

		// Token: 0x02000BA2 RID: 2978
		private struct PlayerInfoForOpeningDoors
		{
			// Token: 0x040076AF RID: 30383
			public Rectangle hitboxToOpenDoor;

			// Token: 0x040076B0 RID: 30384
			public int intendedOpeningDirection;

			// Token: 0x040076B1 RID: 30385
			public int playerGravityDirection;

			// Token: 0x040076B2 RID: 30386
			public Rectangle tileCoordSpaceForCheckingForDoors;
		}

		// Token: 0x02000BA3 RID: 2979
		private struct PlayerInfoForClosingDoors
		{
			// Token: 0x040076B3 RID: 30387
			public Rectangle hitboxToNotCloseDoor;
		}

		// Token: 0x02000BA4 RID: 2980
		private interface DoorAutoHandler
		{
			// Token: 0x06005D70 RID: 23920
			DoorOpeningHelper.DoorOpenCloseTogglingInfo ProvideInfo(Point tileCoords);

			// Token: 0x06005D71 RID: 23921
			bool TryOpenDoor(DoorOpeningHelper.DoorOpenCloseTogglingInfo info, DoorOpeningHelper.PlayerInfoForOpeningDoors playerInfo);

			// Token: 0x06005D72 RID: 23922
			DoorOpeningHelper.DoorCloseAttemptResult TryCloseDoor(DoorOpeningHelper.DoorOpenCloseTogglingInfo info, DoorOpeningHelper.PlayerInfoForClosingDoors playerInfo);
		}

		// Token: 0x02000BA5 RID: 2981
		private class CommonDoorOpeningInfoProvider : DoorOpeningHelper.DoorAutoHandler
		{
			// Token: 0x06005D73 RID: 23923 RVA: 0x006C8604 File Offset: 0x006C6804
			public unsafe DoorOpeningHelper.DoorOpenCloseTogglingInfo ProvideInfo(Point tileCoords)
			{
				Tile tile = Main.tile[tileCoords.X, tileCoords.Y];
				Point tileCoordsForToggling = tileCoords;
				tileCoordsForToggling.Y -= (int)(*tile.frameY % 54 / 18);
				return new DoorOpeningHelper.DoorOpenCloseTogglingInfo
				{
					handler = this,
					tileCoordsForToggling = tileCoordsForToggling
				};
			}

			// Token: 0x06005D74 RID: 23924 RVA: 0x006C865C File Offset: 0x006C685C
			public unsafe bool TryOpenDoor(DoorOpeningHelper.DoorOpenCloseTogglingInfo doorInfo, DoorOpeningHelper.PlayerInfoForOpeningDoors playerInfo)
			{
				Point tileCoordsForToggling = doorInfo.tileCoordsForToggling;
				int intendedOpeningDirection = playerInfo.intendedOpeningDirection;
				Rectangle rectangle;
				rectangle..ctor(doorInfo.tileCoordsForToggling.X * 16, doorInfo.tileCoordsForToggling.Y * 16, 16, 48);
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
				int originalClosedDoorType = 10;
				ModTile modTile = ModContent.GetModTile((int)(*Main.tile[tileCoordsForToggling.X, tileCoordsForToggling.Y].type));
				if (modTile != null && TileID.Sets.OpenDoorID[(int)modTile.Type] > -1)
				{
					originalClosedDoorType = (int)modTile.Type;
				}
				WorldGen.OpenDoor(tileCoordsForToggling.X, tileCoordsForToggling.Y, intendedOpeningDirection);
				if ((int)(*Main.tile[tileCoordsForToggling.X, tileCoordsForToggling.Y].type) != originalClosedDoorType)
				{
					NetMessage.SendData(19, -1, -1, null, 0, (float)tileCoordsForToggling.X, (float)tileCoordsForToggling.Y, (float)intendedOpeningDirection, 0, 0, 0);
					return true;
				}
				WorldGen.OpenDoor(tileCoordsForToggling.X, tileCoordsForToggling.Y, -intendedOpeningDirection);
				if ((int)(*Main.tile[tileCoordsForToggling.X, tileCoordsForToggling.Y].type) != originalClosedDoorType)
				{
					NetMessage.SendData(19, -1, -1, null, 0, (float)tileCoordsForToggling.X, (float)tileCoordsForToggling.Y, (float)(-(float)intendedOpeningDirection), 0, 0, 0);
					return true;
				}
				return false;
			}

			// Token: 0x06005D75 RID: 23925 RVA: 0x006C880C File Offset: 0x006C6A0C
			public unsafe DoorOpeningHelper.DoorCloseAttemptResult TryCloseDoor(DoorOpeningHelper.DoorOpenCloseTogglingInfo info, DoorOpeningHelper.PlayerInfoForClosingDoors playerInfo)
			{
				Point tileCoordsForToggling = info.tileCoordsForToggling;
				Tile tile = Main.tile[tileCoordsForToggling.X, tileCoordsForToggling.Y];
				if (!tile.active() || TileLoader.IsClosedDoor(tile))
				{
					return DoorOpeningHelper.DoorCloseAttemptResult.DoorIsInvalidated;
				}
				int num = (int)(*tile.frameX % 72 / 18);
				Rectangle value;
				value..ctor(tileCoordsForToggling.X * 16, tileCoordsForToggling.Y * 16, 16, 48);
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

		// Token: 0x02000BA6 RID: 2982
		private class TallGateOpeningInfoProvider : DoorOpeningHelper.DoorAutoHandler
		{
			// Token: 0x06005D77 RID: 23927 RVA: 0x006C892C File Offset: 0x006C6B2C
			public unsafe DoorOpeningHelper.DoorOpenCloseTogglingInfo ProvideInfo(Point tileCoords)
			{
				Tile tile = Main.tile[tileCoords.X, tileCoords.Y];
				Point tileCoordsForToggling = tileCoords;
				tileCoordsForToggling.Y -= (int)(*tile.frameY % 90 / 18);
				return new DoorOpeningHelper.DoorOpenCloseTogglingInfo
				{
					handler = this,
					tileCoordsForToggling = tileCoordsForToggling
				};
			}

			// Token: 0x06005D78 RID: 23928 RVA: 0x006C8984 File Offset: 0x006C6B84
			public bool TryOpenDoor(DoorOpeningHelper.DoorOpenCloseTogglingInfo doorInfo, DoorOpeningHelper.PlayerInfoForOpeningDoors playerInfo)
			{
				Point tileCoordsForToggling = doorInfo.tileCoordsForToggling;
				Rectangle rectangle;
				rectangle..ctor(doorInfo.tileCoordsForToggling.X * 16, doorInfo.tileCoordsForToggling.Y * 16, 16, 80);
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

			// Token: 0x06005D79 RID: 23929 RVA: 0x006C8A78 File Offset: 0x006C6C78
			public unsafe DoorOpeningHelper.DoorCloseAttemptResult TryCloseDoor(DoorOpeningHelper.DoorOpenCloseTogglingInfo info, DoorOpeningHelper.PlayerInfoForClosingDoors playerInfo)
			{
				Point tileCoordsForToggling = info.tileCoordsForToggling;
				Tile tile = Main.tile[tileCoordsForToggling.X, tileCoordsForToggling.Y];
				if (!tile.active() || *tile.type != 389)
				{
					return DoorOpeningHelper.DoorCloseAttemptResult.DoorIsInvalidated;
				}
				short num = *tile.frameY % 90 / 18;
				Rectangle value;
				value..ctor(tileCoordsForToggling.X * 16, tileCoordsForToggling.Y * 16, 16, 80);
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
