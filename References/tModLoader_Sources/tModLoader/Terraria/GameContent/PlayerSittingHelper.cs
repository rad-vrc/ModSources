using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terraria.GameContent
{
	// Token: 0x020004AB RID: 1195
	public struct PlayerSittingHelper
	{
		// Token: 0x0600398D RID: 14733 RVA: 0x005987D4 File Offset: 0x005969D4
		public void GetSittingOffsetInfo(Player player, out Vector2 posOffset, out float seatAdjustment)
		{
			if (this.isSitting)
			{
				posOffset = new Vector2((float)(this.sittingIndex * player.direction * 8), (float)this.sittingIndex * player.gravDir * -4f);
				seatAdjustment = -4f;
				seatAdjustment += (float)((int)this.offsetForSeat.Y);
				posOffset += this.offsetForSeat * player.Directions;
				return;
			}
			posOffset = Vector2.Zero;
			seatAdjustment = 0f;
		}

		// Token: 0x0600398E RID: 14734 RVA: 0x00598868 File Offset: 0x00596A68
		public bool TryGetSittingBlock(Player player, out Tile tile)
		{
			tile = default(Tile);
			if (!this.isSitting)
			{
				return false;
			}
			Point pt = (player.Bottom + new Vector2(0f, -2f)).ToTileCoordinates();
			int num;
			Vector2 vector;
			Vector2 vector2;
			ExtraSeatInfo extraSeatInfo;
			if (!PlayerSittingHelper.GetSittingTargetInfo(player, pt.X, pt.Y, out num, out vector, out vector2, out extraSeatInfo))
			{
				return false;
			}
			tile = Framing.GetTileSafely(pt);
			return true;
		}

		// Token: 0x0600398F RID: 14735 RVA: 0x005988D4 File Offset: 0x00596AD4
		public void UpdateSitting(Player player)
		{
			if (!this.isSitting)
			{
				return;
			}
			Point coords = (player.Bottom + new Vector2(0f, -2f)).ToTileCoordinates();
			int targetDirection;
			Vector2 vector;
			Vector2 seatDownOffset;
			ExtraSeatInfo extraInfo;
			if (!PlayerSittingHelper.GetSittingTargetInfo(player, coords.X, coords.Y, out targetDirection, out vector, out seatDownOffset, out extraInfo))
			{
				this.SitUp(player, true);
				return;
			}
			if (player.controlLeft || player.controlRight || player.controlUp || player.controlDown || player.controlJump || player.pulley || player.mount.Active || targetDirection != player.direction)
			{
				this.SitUp(player, true);
			}
			if (Main.sittingManager.GetNextPlayerStackIndexInCoords(coords) >= 2)
			{
				this.SitUp(player, true);
			}
			if (this.isSitting)
			{
				this.offsetForSeat = seatDownOffset;
				this.details = extraInfo;
				Main.sittingManager.AddPlayerAndGetItsStackedIndexInCoords(player.whoAmI, coords, out this.sittingIndex);
			}
		}

		// Token: 0x06003990 RID: 14736 RVA: 0x005989C0 File Offset: 0x00596BC0
		public void SitUp(Player player, bool multiplayerBroadcast = true)
		{
			if (this.isSitting)
			{
				this.isSitting = false;
				this.offsetForSeat = Vector2.Zero;
				this.sittingIndex = -1;
				this.details = default(ExtraSeatInfo);
				if (multiplayerBroadcast && Main.myPlayer == player.whoAmI)
				{
					NetMessage.SendData(13, -1, -1, null, player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
				}
			}
		}

		// Token: 0x06003991 RID: 14737 RVA: 0x00598A2C File Offset: 0x00596C2C
		public void SitDown(Player player, int x, int y)
		{
			int targetDirection;
			Vector2 playerSittingPosition;
			Vector2 seatDownOffset;
			ExtraSeatInfo extraInfo;
			if (!PlayerSittingHelper.GetSittingTargetInfo(player, x, y, out targetDirection, out playerSittingPosition, out seatDownOffset, out extraInfo))
			{
				return;
			}
			Vector2 offset = playerSittingPosition - player.Bottom;
			bool flag = player.CanSnapToPosition(offset);
			if (flag)
			{
				flag &= (Main.sittingManager.GetNextPlayerStackIndexInCoords((playerSittingPosition + new Vector2(0f, -2f)).ToTileCoordinates()) < 2);
			}
			if (!flag)
			{
				return;
			}
			if (this.isSitting && player.Bottom == playerSittingPosition)
			{
				this.SitUp(player, true);
				return;
			}
			player.StopVanityActions(true);
			player.RemoveAllGrapplingHooks();
			if (player.mount.Active)
			{
				player.mount.Dismount(player);
			}
			player.Bottom = playerSittingPosition;
			player.ChangeDir(targetDirection);
			this.isSitting = true;
			this.details = extraInfo;
			this.offsetForSeat = seatDownOffset;
			Main.sittingManager.AddPlayerAndGetItsStackedIndexInCoords(player.whoAmI, new Point(x, y), out this.sittingIndex);
			player.velocity = Vector2.Zero;
			player.gravDir = 1f;
			if (Main.myPlayer == player.whoAmI)
			{
				NetMessage.SendData(13, -1, -1, null, player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06003992 RID: 14738 RVA: 0x00598B64 File Offset: 0x00596D64
		public unsafe static bool GetSittingTargetInfo(Player player, int x, int y, out int targetDirection, out Vector2 playerSittingPosition, out Vector2 seatDownOffset, out ExtraSeatInfo extraInfo)
		{
			extraInfo = default(ExtraSeatInfo);
			Tile tileSafely = Framing.GetTileSafely(x, y);
			if (!TileID.Sets.CanBeSatOnForPlayers[(int)(*tileSafely.type)] || !tileSafely.active())
			{
				targetDirection = 1;
				seatDownOffset = Vector2.Zero;
				playerSittingPosition = default(Vector2);
				return false;
			}
			int num = x;
			int num2 = y;
			targetDirection = 1;
			seatDownOffset = Vector2.Zero;
			int num3 = 6;
			Vector2 zero = Vector2.Zero;
			ushort num5 = *tileSafely.type;
			if (num5 <= 89)
			{
				if (num5 != 15)
				{
					if (num5 != 89)
					{
						goto IL_484;
					}
					targetDirection = player.direction;
					num3 = 0;
					Vector2 vector;
					vector..ctor(-4f, 2f);
					Vector2 vector2;
					vector2..ctor(4f, 2f);
					Vector2 vector3;
					vector3..ctor(0f, 2f);
					Vector2 zero2 = Vector2.Zero;
					zero2.X = 1f;
					zero.X = -1f;
					switch (*tileSafely.frameX / 54)
					{
					case 0:
						vector3.Y = (vector.Y = (vector2.Y = 1f));
						break;
					case 1:
						vector3.Y = 1f;
						break;
					case 2:
					case 14:
					case 15:
					case 17:
					case 20:
					case 21:
					case 22:
					case 23:
					case 25:
					case 26:
					case 27:
					case 28:
					case 35:
					case 37:
					case 38:
					case 39:
					case 40:
					case 41:
					case 42:
						vector3.Y = (vector.Y = (vector2.Y = 1f));
						break;
					case 3:
					case 4:
					case 5:
					case 7:
					case 8:
					case 9:
					case 10:
					case 11:
					case 12:
					case 13:
					case 16:
					case 18:
					case 19:
					case 36:
						vector3.Y = (vector.Y = (vector2.Y = 0f));
						break;
					case 6:
						vector3.Y = (vector.Y = (vector2.Y = -1f));
						break;
					case 24:
						vector3.Y = 0f;
						vector.Y = -4f;
						vector.X = 0f;
						vector2.X = 0f;
						vector2.Y = -4f;
						break;
					}
					if (*tileSafely.frameY % 40 != 0)
					{
						num2--;
					}
					if ((*tileSafely.frameX % 54 == 0 && targetDirection == -1) || (*tileSafely.frameX % 54 == 36 && targetDirection == 1))
					{
						seatDownOffset = vector;
					}
					else if ((*tileSafely.frameX % 54 == 0 && targetDirection == 1) || (*tileSafely.frameX % 54 == 36 && targetDirection == -1))
					{
						seatDownOffset = vector2;
					}
					else
					{
						seatDownOffset = vector3;
					}
					seatDownOffset += zero2;
					goto IL_484;
				}
			}
			else
			{
				if (num5 == 102)
				{
					short num6 = *tileSafely.frameX / 18;
					if (num6 == 0)
					{
						num++;
					}
					if (num6 == 2)
					{
						num--;
					}
					short num7 = *tileSafely.frameY / 18;
					if (num7 == 0)
					{
						num2 += 2;
					}
					if (num7 == 1)
					{
						num2++;
					}
					if (num7 == 3)
					{
						num2--;
					}
					targetDirection = player.direction;
					num3 = 0;
					goto IL_484;
				}
				if (num5 == 487)
				{
					int num4 = (int)(*tileSafely.frameX % 72 / 18);
					if (num4 == 1)
					{
						num--;
					}
					if (num4 == 2)
					{
						num++;
					}
					if (*tileSafely.frameY / 18 != 0)
					{
						num2--;
					}
					targetDirection = (num4 <= 1).ToDirectionInt();
					num3 = 0;
					seatDownOffset.Y -= 1f;
					goto IL_484;
				}
				if (num5 != 497)
				{
					goto IL_484;
				}
			}
			bool flag = *tileSafely.type == 15 && (*tileSafely.frameY / 40 == 1 || *tileSafely.frameY / 40 == 20);
			bool value = *tileSafely.type == 15 && *tileSafely.frameY / 40 == 27;
			seatDownOffset.Y = (float)(value.ToInt() * 4);
			if (*tileSafely.frameY % 40 != 0)
			{
				num2--;
			}
			targetDirection = -1;
			if (*tileSafely.frameX != 0)
			{
				targetDirection = 1;
			}
			if (flag || *tileSafely.type == 497)
			{
				extraInfo.IsAToilet = true;
			}
			IL_484:
			TileRestingInfo info = new TileRestingInfo(player, new Point(num, num2), seatDownOffset, targetDirection, num3, zero, extraInfo);
			TileLoader.ModifySittingTargetInfo(x, y, (int)(*tileSafely.type), ref info);
			num = info.AnchorTilePosition.X;
			num2 = info.AnchorTilePosition.Y;
			num3 = info.DirectionOffset;
			targetDirection = info.TargetDirection;
			seatDownOffset = info.VisualOffset;
			zero = info.FinalOffset;
			extraInfo = info.ExtraInfo;
			playerSittingPosition = new Point(num, num2).ToWorldCoordinates(8f, 16f);
			playerSittingPosition.X += (float)(targetDirection * num3);
			playerSittingPosition += zero;
			return true;
		}

		// Token: 0x04005265 RID: 21093
		public const int ChairSittingMaxDistance = 40;

		// Token: 0x04005266 RID: 21094
		public bool isSitting;

		// Token: 0x04005267 RID: 21095
		public ExtraSeatInfo details;

		// Token: 0x04005268 RID: 21096
		public Vector2 offsetForSeat;

		// Token: 0x04005269 RID: 21097
		public int sittingIndex;
	}
}
