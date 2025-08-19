using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terraria.GameContent
{
	// Token: 0x020004AC RID: 1196
	public struct PlayerSleepingHelper
	{
		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06003993 RID: 14739 RVA: 0x005990BB File Offset: 0x005972BB
		public bool FullyFallenAsleep
		{
			get
			{
				return this.isSleeping && this.timeSleeping >= 120;
			}
		}

		// Token: 0x06003994 RID: 14740 RVA: 0x005990D4 File Offset: 0x005972D4
		public void GetSleepingOffsetInfo(Player player, out Vector2 posOffset)
		{
			if (this.isSleeping)
			{
				posOffset = this.visualOffsetOfBedBase * player.Directions + new Vector2(0f, (float)this.sleepingIndex * player.gravDir * -4f);
				return;
			}
			posOffset = Vector2.Zero;
		}

		// Token: 0x06003995 RID: 14741 RVA: 0x0059912F File Offset: 0x0059732F
		private bool DoesPlayerHaveReasonToActUpInBed(Player player)
		{
			return NPC.AnyDanger(true, false) || (Main.bloodMoon && !Main.dayTime) || (Main.eclipse && Main.dayTime) || player.itemAnimation > 0;
		}

		// Token: 0x06003996 RID: 14742 RVA: 0x00599168 File Offset: 0x00597368
		public void SetIsSleepingAndAdjustPlayerRotation(Player player, bool state)
		{
			if (this.isSleeping != state)
			{
				this.isSleeping = state;
				if (state)
				{
					player.fullRotation = 1.5707964f * (float)(-(float)player.direction);
					return;
				}
				player.fullRotation = 0f;
				this.visualOffsetOfBedBase = default(Vector2);
			}
		}

		// Token: 0x06003997 RID: 14743 RVA: 0x005991B4 File Offset: 0x005973B4
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
			Point coords = (player.Bottom + new Vector2(0f, -2f)).ToTileCoordinates();
			int targetDirection;
			Vector2 vector;
			Vector2 visualoffset;
			if (!PlayerSleepingHelper.GetSleepingTargetInfo(coords.X, coords.Y, out targetDirection, out vector, out visualoffset))
			{
				this.StopSleeping(player, true);
				return;
			}
			if (player.controlLeft || player.controlRight || player.controlUp || player.controlDown || player.controlJump || player.pulley || player.mount.Active || targetDirection != player.direction)
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
			if (Main.sleepingManager.GetNextPlayerStackIndexInCoords(coords) >= 2)
			{
				this.StopSleeping(player, true);
			}
			if (this.isSleeping)
			{
				this.visualOffsetOfBedBase = visualoffset;
				Main.sleepingManager.AddPlayerAndGetItsStackedIndexInCoords(player.whoAmI, coords, out this.sleepingIndex);
			}
		}

		// Token: 0x06003998 RID: 14744 RVA: 0x00599320 File Offset: 0x00597520
		public void StopSleeping(Player player, bool multiplayerBroadcast = true)
		{
			if (this.isSleeping)
			{
				this.SetIsSleepingAndAdjustPlayerRotation(player, false);
				this.timeSleeping = 0;
				this.sleepingIndex = -1;
				this.visualOffsetOfBedBase = default(Vector2);
				if (multiplayerBroadcast && Main.myPlayer == player.whoAmI)
				{
					NetMessage.SendData(13, -1, -1, null, player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
				}
			}
		}

		// Token: 0x06003999 RID: 14745 RVA: 0x0059938C File Offset: 0x0059758C
		public void StartSleeping(Player player, int x, int y)
		{
			int targetDirection;
			Vector2 anchorPosition;
			Vector2 visualoffset;
			PlayerSleepingHelper.GetSleepingTargetInfo(x, y, out targetDirection, out anchorPosition, out visualoffset);
			Vector2 offset = anchorPosition - player.Bottom;
			bool flag = player.CanSnapToPosition(offset);
			if (flag)
			{
				flag &= (Main.sleepingManager.GetNextPlayerStackIndexInCoords((anchorPosition + new Vector2(0f, -2f)).ToTileCoordinates()) < 2);
			}
			if (!flag)
			{
				return;
			}
			if (this.isSleeping && player.Bottom == anchorPosition)
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
			player.Bottom = anchorPosition;
			player.ChangeDir(targetDirection);
			Main.sleepingManager.AddPlayerAndGetItsStackedIndexInCoords(player.whoAmI, new Point(x, y), out this.sleepingIndex);
			player.velocity = Vector2.Zero;
			player.gravDir = 1f;
			this.SetIsSleepingAndAdjustPlayerRotation(player, true);
			this.visualOffsetOfBedBase = visualoffset;
			if (Main.myPlayer == player.whoAmI)
			{
				NetMessage.SendData(13, -1, -1, null, player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x0600399A RID: 14746 RVA: 0x005994BC File Offset: 0x005976BC
		public unsafe static bool GetSleepingTargetInfo(int x, int y, out int targetDirection, out Vector2 anchorPosition, out Vector2 visualoffset)
		{
			Tile tileSafely = Framing.GetTileSafely(x, y);
			if (!TileID.Sets.CanBeSleptIn[(int)(*tileSafely.type)] || !tileSafely.active())
			{
				targetDirection = 1;
				anchorPosition = default(Vector2);
				visualoffset = default(Vector2);
				return false;
			}
			int num = y;
			int num4 = x - (int)(*tileSafely.frameX % 72 / 18);
			if (*tileSafely.frameY % 36 != 0)
			{
				num--;
			}
			targetDirection = 1;
			int num2 = (int)(*tileSafely.frameX / 72);
			int num3 = num4;
			if (num2 != 0)
			{
				if (num2 == 1)
				{
					num3 += 2;
				}
			}
			else
			{
				targetDirection = -1;
				num3++;
			}
			visualoffset = PlayerSleepingHelper.SetOffsetbyBed((int)(*tileSafely.frameY / 36));
			TileRestingInfo info = new TileRestingInfo(null, new Point(num3, num), visualoffset, targetDirection, 0, default(Vector2), default(ExtraSeatInfo));
			TileLoader.ModifySleepingTargetInfo(x, y, (int)(*tileSafely.type), ref info);
			num3 = info.AnchorTilePosition.X;
			num = info.AnchorTilePosition.Y;
			int directionOffset = info.DirectionOffset;
			targetDirection = info.TargetDirection;
			visualoffset = info.VisualOffset;
			Vector2 finalOffset = info.FinalOffset;
			anchorPosition = new Point(num3, num + 1).ToWorldCoordinates(8f, 16f);
			anchorPosition.X += (float)(targetDirection * directionOffset);
			anchorPosition += finalOffset;
			return true;
		}

		// Token: 0x0600399B RID: 14747 RVA: 0x00599624 File Offset: 0x00597824
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

		// Token: 0x0400526A RID: 21098
		public const int BedSleepingMaxDistance = 96;

		// Token: 0x0400526B RID: 21099
		public const int TimeToFullyFallAsleep = 120;

		// Token: 0x0400526C RID: 21100
		public bool isSleeping;

		// Token: 0x0400526D RID: 21101
		public int sleepingIndex;

		// Token: 0x0400526E RID: 21102
		public int timeSleeping;

		// Token: 0x0400526F RID: 21103
		public Vector2 visualOffsetOfBedBase;
	}
}
