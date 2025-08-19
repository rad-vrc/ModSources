using System;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Terraria.GameContent
{
	// Token: 0x020001E5 RID: 485
	public struct PlayerSleepingHelper
	{
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06001C6B RID: 7275 RVA: 0x004F3BA9 File Offset: 0x004F1DA9
		public bool FullyFallenAsleep
		{
			get
			{
				return this.isSleeping && this.timeSleeping >= 120;
			}
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x004F3BC4 File Offset: 0x004F1DC4
		public void GetSleepingOffsetInfo(Player player, out Vector2 posOffset)
		{
			if (this.isSleeping)
			{
				posOffset = this.visualOffsetOfBedBase * player.Directions + new Vector2(0f, (float)this.sleepingIndex * player.gravDir * -4f);
				return;
			}
			posOffset = Vector2.Zero;
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x004F3C1F File Offset: 0x004F1E1F
		private bool DoesPlayerHaveReasonToActUpInBed(Player player)
		{
			return NPC.AnyDanger(true, false) || (Main.bloodMoon && !Main.dayTime) || (Main.eclipse && Main.dayTime) || player.itemAnimation > 0;
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x004F3C58 File Offset: 0x004F1E58
		public void SetIsSleepingAndAdjustPlayerRotation(Player player, bool state)
		{
			if (this.isSleeping == state)
			{
				return;
			}
			this.isSleeping = state;
			if (state)
			{
				player.fullRotation = 1.5707964f * (float)(-(float)player.direction);
				return;
			}
			player.fullRotation = 0f;
			this.visualOffsetOfBedBase = default(Vector2);
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x004F3CA8 File Offset: 0x004F1EA8
		public void UpdateState(Player player)
		{
			if (!this.isSleeping)
			{
				this.timeSleeping = 0;
				return;
			}
			this.timeSleeping++;
			if (this.DoesPlayerHaveReasonToActUpInBed(player))
			{
				this.timeSleeping = 0;
			}
			Point point = (player.Bottom + new Vector2(0f, -2f)).ToTileCoordinates();
			int num;
			Vector2 vector;
			Vector2 vector2;
			if (!PlayerSleepingHelper.GetSleepingTargetInfo(point.X, point.Y, out num, out vector, out vector2))
			{
				this.StopSleeping(player, true);
				return;
			}
			if (player.controlLeft || player.controlRight || player.controlUp || player.controlDown || player.controlJump || player.pulley || player.mount.Active || num != player.direction)
			{
				this.StopSleeping(player, true);
			}
			bool flag = false;
			if (player.itemAnimation > 0)
			{
				Item heldItem = player.HeldItem;
				if (heldItem.damage > 0 && !heldItem.noMelee)
				{
					flag = true;
				}
				if (heldItem.fishingPole > 0)
				{
					flag = true;
				}
				bool? flag2 = ItemID.Sets.ForcesBreaksSleeping[heldItem.type];
				if (flag2 != null)
				{
					flag = flag2.Value;
				}
			}
			if (flag)
			{
				this.StopSleeping(player, true);
			}
			if (Main.sleepingManager.GetNextPlayerStackIndexInCoords(point) >= 2)
			{
				this.StopSleeping(player, true);
			}
			if (!this.isSleeping)
			{
				return;
			}
			this.visualOffsetOfBedBase = vector2;
			Main.sleepingManager.AddPlayerAndGetItsStackedIndexInCoords(player.whoAmI, point, out this.sleepingIndex);
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x004F3E20 File Offset: 0x004F2020
		public void StopSleeping(Player player, bool multiplayerBroadcast = true)
		{
			if (!this.isSleeping)
			{
				return;
			}
			this.SetIsSleepingAndAdjustPlayerRotation(player, false);
			this.timeSleeping = 0;
			this.sleepingIndex = -1;
			this.visualOffsetOfBedBase = default(Vector2);
			if (multiplayerBroadcast && Main.myPlayer == player.whoAmI)
			{
				NetMessage.SendData(13, -1, -1, null, player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x004F3E8C File Offset: 0x004F208C
		public void StartSleeping(Player player, int x, int y)
		{
			int dir;
			Vector2 vector;
			Vector2 vector2;
			PlayerSleepingHelper.GetSleepingTargetInfo(x, y, out dir, out vector, out vector2);
			Vector2 offset = vector - player.Bottom;
			bool flag = player.CanSnapToPosition(offset);
			if (flag)
			{
				flag &= (Main.sleepingManager.GetNextPlayerStackIndexInCoords((vector + new Vector2(0f, -2f)).ToTileCoordinates()) < 2);
			}
			if (!flag)
			{
				return;
			}
			if (this.isSleeping && player.Bottom == vector)
			{
				this.StopSleeping(player, true);
				return;
			}
			player.StopVanityActions(true);
			player.RemoveAllGrapplingHooks();
			player.RemoveAllFishingBobbers();
			if (player.mount.Active)
			{
				player.mount.Dismount(player);
			}
			player.Bottom = vector;
			player.ChangeDir(dir);
			Main.sleepingManager.AddPlayerAndGetItsStackedIndexInCoords(player.whoAmI, new Point(x, y), out this.sleepingIndex);
			player.velocity = Vector2.Zero;
			player.gravDir = 1f;
			this.SetIsSleepingAndAdjustPlayerRotation(player, true);
			this.visualOffsetOfBedBase = vector2;
			if (Main.myPlayer == player.whoAmI)
			{
				NetMessage.SendData(13, -1, -1, null, player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x004F3FBC File Offset: 0x004F21BC
		public static bool GetSleepingTargetInfo(int x, int y, out int targetDirection, out Vector2 anchorPosition, out Vector2 visualoffset)
		{
			Tile tileSafely = Framing.GetTileSafely(x, y);
			if (!TileID.Sets.CanBeSleptIn[(int)tileSafely.type] || !tileSafely.active())
			{
				targetDirection = 1;
				anchorPosition = default(Vector2);
				visualoffset = default(Vector2);
				return false;
			}
			int num = y;
			int num2 = x - (int)(tileSafely.frameX % 72 / 18);
			if (tileSafely.frameY % 36 != 0)
			{
				num--;
			}
			targetDirection = 1;
			int num3 = (int)(tileSafely.frameX / 72);
			int num4 = num2;
			if (num3 != 0)
			{
				if (num3 == 1)
				{
					num4 += 2;
				}
			}
			else
			{
				targetDirection = -1;
				num4++;
			}
			anchorPosition = new Point(num4, num + 1).ToWorldCoordinates(8f, 16f);
			visualoffset = PlayerSleepingHelper.SetOffsetbyBed((int)(tileSafely.frameY / 36));
			return true;
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x004F4074 File Offset: 0x004F2274
		private static Vector2 SetOffsetbyBed(int bedStyle)
		{
			switch (bedStyle)
			{
			case 8:
				return new Vector2(-11f, 1f);
			default:
				return new Vector2(-9f, 1f);
			case 10:
				return new Vector2(-9f, -1f);
			case 11:
				return new Vector2(-11f, 1f);
			case 13:
				return new Vector2(-11f, -3f);
			case 15:
			case 16:
			case 17:
				return new Vector2(-7f, -3f);
			case 18:
				return new Vector2(-9f, -3f);
			case 19:
				return new Vector2(-3f, -1f);
			case 20:
				return new Vector2(-9f, -5f);
			case 21:
				return new Vector2(-9f, 5f);
			case 22:
				return new Vector2(-7f, 1f);
			case 23:
				return new Vector2(-5f, -1f);
			case 24:
			case 25:
				return new Vector2(-7f, 1f);
			case 27:
				return new Vector2(-9f, 3f);
			case 28:
				return new Vector2(-9f, 5f);
			case 29:
				return new Vector2(-11f, -1f);
			case 30:
				return new Vector2(-9f, 3f);
			case 31:
				return new Vector2(-7f, 5f);
			case 32:
				return new Vector2(-7f, -1f);
			case 34:
			case 35:
			case 36:
			case 37:
				return new Vector2(-13f, 1f);
			case 38:
				return new Vector2(-11f, -3f);
			}
		}

		// Token: 0x040043A2 RID: 17314
		public const int BedSleepingMaxDistance = 96;

		// Token: 0x040043A3 RID: 17315
		public const int TimeToFullyFallAsleep = 120;

		// Token: 0x040043A4 RID: 17316
		public bool isSleeping;

		// Token: 0x040043A5 RID: 17317
		public int sleepingIndex;

		// Token: 0x040043A6 RID: 17318
		public int timeSleeping;

		// Token: 0x040043A7 RID: 17319
		public Vector2 visualOffsetOfBedBase;
	}
}
