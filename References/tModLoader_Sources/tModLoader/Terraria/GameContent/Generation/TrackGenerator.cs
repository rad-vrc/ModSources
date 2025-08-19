using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation
{
	// Token: 0x02000628 RID: 1576
	public class TrackGenerator
	{
		// Token: 0x060044F9 RID: 17657 RVA: 0x0060D9A6 File Offset: 0x0060BBA6
		public bool Place(Point origin, int minLength, int maxLength)
		{
			if (!TrackGenerator.FindSuitableOrigin(ref origin))
			{
				return false;
			}
			this.CreateTrackStart(origin);
			if (!this.FindPath(minLength, maxLength))
			{
				return false;
			}
			this.PlacePath();
			return true;
		}

		// Token: 0x060044FA RID: 17658 RVA: 0x0060D9D0 File Offset: 0x0060BBD0
		private unsafe void PlacePath()
		{
			bool[] array = new bool[this._length];
			for (int i = 0; i < this._length; i++)
			{
				if (WorldGen.genRand.Next(7) == 0)
				{
					this.playerHeight = WorldGen.genRand.Next(5, 9);
				}
				for (int j = 0; j < this.playerHeight; j++)
				{
					if (*Main.tile[(int)this._history[i].X, (int)this._history[i].Y - j - 1].wall == 244)
					{
						*Main.tile[(int)this._history[i].X, (int)this._history[i].Y - j - 1].wall = 0;
					}
					if (*Main.tile[(int)this._history[i].X, (int)this._history[i].Y - j].wall == 244)
					{
						*Main.tile[(int)this._history[i].X, (int)this._history[i].Y - j].wall = 0;
					}
					if (*Main.tile[(int)this._history[i].X, (int)this._history[i].Y - j + 1].wall == 244)
					{
						*Main.tile[(int)this._history[i].X, (int)this._history[i].Y - j + 1].wall = 0;
					}
					if (*Main.tile[(int)this._history[i].X, (int)this._history[i].Y - j].type == 135)
					{
						array[i] = true;
					}
					WorldGen.KillTile((int)this._history[i].X, (int)this._history[i].Y - j, false, false, true);
				}
			}
			for (int k = 0; k < this._length; k++)
			{
				if (WorldGen.genRand.Next(7) == 0)
				{
					this.playerHeight = WorldGen.genRand.Next(5, 9);
				}
				TrackGenerator.TrackHistory trackHistory = this._history[k];
				Tile.SmoothSlope((int)trackHistory.X, (int)(trackHistory.Y + 1), true, false);
				Tile.SmoothSlope((int)trackHistory.X, (int)trackHistory.Y - this.playerHeight, true, false);
				bool wire = Main.tile[(int)trackHistory.X, (int)trackHistory.Y].wire();
				if (array[k] && k < this._length && k > 0 && this._history[k - 1].Y == trackHistory.Y && this._history[k + 1].Y == trackHistory.Y)
				{
					Main.tile[(int)trackHistory.X, (int)trackHistory.Y].ClearEverything();
					WorldGen.PlaceTile((int)trackHistory.X, (int)trackHistory.Y, 314, false, true, -1, 1);
				}
				else
				{
					Main.tile[(int)trackHistory.X, (int)trackHistory.Y].ResetToType(314);
				}
				Main.tile[(int)trackHistory.X, (int)trackHistory.Y].wire(wire);
				if (k != 0)
				{
					for (int l = 0; l < 8; l++)
					{
						WorldUtils.TileFrame((int)this._history[k - 1].X, (int)this._history[k - 1].Y - l, true);
					}
					if (k == this._length - 1)
					{
						for (int m = 0; m < this.playerHeight; m++)
						{
							WorldUtils.TileFrame((int)trackHistory.X, (int)trackHistory.Y - m, true);
						}
					}
				}
			}
		}

		// Token: 0x060044FB RID: 17659 RVA: 0x0060DE04 File Offset: 0x0060C004
		private void CreateTrackStart(Point origin)
		{
			this._xDirection = ((origin.X <= Main.maxTilesX / 2) ? 1 : -1);
			this._length = 1;
			for (int i = 0; i < this._history.Length; i++)
			{
				this._history[i] = new TrackGenerator.TrackHistory(origin.X + i * this._xDirection, origin.Y + i, TrackGenerator.TrackSlope.Down);
			}
		}

		// Token: 0x060044FC RID: 17660 RVA: 0x0060DE70 File Offset: 0x0060C070
		private bool FindPath(int minLength, int maxLength)
		{
			int length = this._length;
			while (this._length < this._history.Length - 100)
			{
				TrackGenerator.TrackSlope slope = (this._history[this._length - 1].Slope != TrackGenerator.TrackSlope.Up) ? TrackGenerator.TrackSlope.Down : TrackGenerator.TrackSlope.Straight;
				this.AppendToHistory(slope, TrackGenerator.TrackMode.Normal);
				TrackGenerator.TrackPlacementState trackPlacementState = this.TryRewriteHistoryToAvoidTiles();
				if (trackPlacementState == TrackGenerator.TrackPlacementState.Invalid)
				{
					break;
				}
				length = this._length;
				TrackGenerator.TrackPlacementState trackPlacementState2 = trackPlacementState;
				while (trackPlacementState2 != TrackGenerator.TrackPlacementState.Available)
				{
					trackPlacementState2 = this.CreateTunnel();
					if (trackPlacementState2 == TrackGenerator.TrackPlacementState.Invalid)
					{
						break;
					}
					length = this._length;
				}
				if (this._length >= maxLength)
				{
					break;
				}
			}
			this._length = Math.Min(maxLength, length);
			if (this._length < minLength)
			{
				return false;
			}
			this.SmoothTrack();
			return this.GetHistorySegmentPlacementState(0, this._length) != TrackGenerator.TrackPlacementState.Invalid;
		}

		// Token: 0x060044FD RID: 17661 RVA: 0x0060DF28 File Offset: 0x0060C128
		private TrackGenerator.TrackPlacementState CreateTunnel()
		{
			TrackGenerator.TrackSlope trackSlope = TrackGenerator.TrackSlope.Straight;
			int num = 10;
			TrackGenerator.TrackPlacementState trackPlacementState = TrackGenerator.TrackPlacementState.Invalid;
			int x = (int)this._history[this._length - 1].X;
			int y = (int)this._history[this._length - 1].Y;
			for (TrackGenerator.TrackSlope trackSlope2 = TrackGenerator.TrackSlope.Up; trackSlope2 <= TrackGenerator.TrackSlope.Down; trackSlope2 += 1)
			{
				TrackGenerator.TrackPlacementState trackPlacementState2 = TrackGenerator.TrackPlacementState.Invalid;
				int i = 1;
				while (i < num)
				{
					trackPlacementState2 = TrackGenerator.CalculateStateForLocation(x + i * this._xDirection, y + i * (int)trackSlope2);
					if (trackPlacementState2 != TrackGenerator.TrackPlacementState.Obstructed)
					{
						if (trackPlacementState2 != TrackGenerator.TrackPlacementState.Invalid)
						{
							trackSlope = trackSlope2;
							num = i;
							trackPlacementState = trackPlacementState2;
							break;
						}
						break;
					}
					else
					{
						i++;
					}
				}
				if (trackPlacementState != TrackGenerator.TrackPlacementState.Available && trackPlacementState2 == TrackGenerator.TrackPlacementState.Obstructed && (trackPlacementState != TrackGenerator.TrackPlacementState.Obstructed || trackSlope != TrackGenerator.TrackSlope.Straight))
				{
					trackSlope = trackSlope2;
					num = 10;
					trackPlacementState = trackPlacementState2;
				}
			}
			if (this._length == 0 || !TrackGenerator.CanSlopesTouch(this._history[this._length - 1].Slope, trackSlope))
			{
				this.RewriteSlopeDirection(this._length - 1, TrackGenerator.TrackSlope.Straight);
			}
			this._history[this._length - 1].Mode = TrackGenerator.TrackMode.Tunnel;
			for (int j = 1; j < num; j++)
			{
				this.AppendToHistory(trackSlope, TrackGenerator.TrackMode.Tunnel);
			}
			return trackPlacementState;
		}

		// Token: 0x060044FE RID: 17662 RVA: 0x0060E044 File Offset: 0x0060C244
		private void AppendToHistory(TrackGenerator.TrackSlope slope, TrackGenerator.TrackMode mode = TrackGenerator.TrackMode.Normal)
		{
			this._history[this._length] = new TrackGenerator.TrackHistory((int)this._history[this._length - 1].X + this._xDirection, (int)((sbyte)this._history[this._length - 1].Y + slope), slope);
			this._history[this._length].Mode = mode;
			this._length++;
		}

		// Token: 0x060044FF RID: 17663 RVA: 0x0060E0C8 File Offset: 0x0060C2C8
		private TrackGenerator.TrackPlacementState TryRewriteHistoryToAvoidTiles()
		{
			int num = this._length - 1;
			int num2 = Math.Min(this._length, this._rewriteHistory.Length);
			for (int i = 0; i < num2; i++)
			{
				this._rewriteHistory[i] = this._history[num - i];
			}
			while (num >= this._length - num2)
			{
				if (this._history[num].Slope == TrackGenerator.TrackSlope.Down)
				{
					TrackGenerator.TrackPlacementState historySegmentPlacementState = this.GetHistorySegmentPlacementState(num, this._length - num);
					if (historySegmentPlacementState == TrackGenerator.TrackPlacementState.Available)
					{
						return historySegmentPlacementState;
					}
					this.RewriteSlopeDirection(num, TrackGenerator.TrackSlope.Straight);
				}
				num--;
			}
			if (this.GetHistorySegmentPlacementState(num + 1, this._length - (num + 1)) == TrackGenerator.TrackPlacementState.Available)
			{
				return TrackGenerator.TrackPlacementState.Available;
			}
			for (num = this._length - 1; num >= this._length - num2 + 1; num--)
			{
				if (this._history[num].Slope == TrackGenerator.TrackSlope.Straight)
				{
					TrackGenerator.TrackPlacementState historySegmentPlacementState2 = this.GetHistorySegmentPlacementState(this._length - num2, num2);
					if (historySegmentPlacementState2 == TrackGenerator.TrackPlacementState.Available)
					{
						return historySegmentPlacementState2;
					}
					this.RewriteSlopeDirection(num, TrackGenerator.TrackSlope.Up);
				}
			}
			for (int j = 0; j < num2; j++)
			{
				this._history[this._length - 1 - j] = this._rewriteHistory[j];
			}
			this.RewriteSlopeDirection(this._length - 1, TrackGenerator.TrackSlope.Straight);
			return this.GetHistorySegmentPlacementState(num + 1, this._length - (num + 1));
		}

		// Token: 0x06004500 RID: 17664 RVA: 0x0060E218 File Offset: 0x0060C418
		private void RewriteSlopeDirection(int index, TrackGenerator.TrackSlope slope)
		{
			int num = (int)(slope - this._history[index].Slope);
			this._history[index].Slope = slope;
			for (int i = index; i < this._length; i++)
			{
				TrackGenerator.TrackHistory[] history = this._history;
				int num2 = i;
				history[num2].Y = history[num2].Y + (short)num;
			}
		}

		// Token: 0x06004501 RID: 17665 RVA: 0x0060E278 File Offset: 0x0060C478
		private TrackGenerator.TrackPlacementState GetHistorySegmentPlacementState(int startIndex, int length)
		{
			TrackGenerator.TrackPlacementState result = TrackGenerator.TrackPlacementState.Available;
			for (int i = startIndex; i < startIndex + length; i++)
			{
				TrackGenerator.TrackPlacementState trackPlacementState = TrackGenerator.CalculateStateForLocation((int)this._history[i].X, (int)this._history[i].Y);
				if (trackPlacementState != TrackGenerator.TrackPlacementState.Obstructed)
				{
					if (trackPlacementState == TrackGenerator.TrackPlacementState.Invalid)
					{
						return trackPlacementState;
					}
				}
				else if (this._history[i].Mode != TrackGenerator.TrackMode.Tunnel)
				{
					result = trackPlacementState;
				}
			}
			return result;
		}

		// Token: 0x06004502 RID: 17666 RVA: 0x0060E2E0 File Offset: 0x0060C4E0
		private void SmoothTrack()
		{
			int num = this._length - 1;
			bool flag = false;
			for (int num2 = this._length - 1; num2 >= 0; num2--)
			{
				if (flag)
				{
					num = Math.Min(num2 + 15, num);
					if (this._history[num2].Y >= this._history[num].Y)
					{
						int i = num2 + 1;
						while (this._history[i].Y > this._history[num2].Y)
						{
							this._history[i].Y = this._history[num2].Y;
							this._history[i].Slope = TrackGenerator.TrackSlope.Straight;
							i++;
						}
						if (this._history[num2].Y == this._history[num].Y)
						{
							flag = false;
						}
					}
				}
				else if (this._history[num2].Y > this._history[num].Y)
				{
					flag = true;
				}
				else
				{
					num = num2;
				}
			}
		}

		// Token: 0x06004503 RID: 17667 RVA: 0x0060E3FD File Offset: 0x0060C5FD
		private static bool CanSlopesTouch(TrackGenerator.TrackSlope leftSlope, TrackGenerator.TrackSlope rightSlope)
		{
			return leftSlope == rightSlope || leftSlope == TrackGenerator.TrackSlope.Straight || rightSlope == TrackGenerator.TrackSlope.Straight;
		}

		// Token: 0x06004504 RID: 17668 RVA: 0x0060E40C File Offset: 0x0060C60C
		private static bool FindSuitableOrigin(ref Point origin)
		{
			TrackGenerator.TrackPlacementState trackPlacementState;
			while ((trackPlacementState = TrackGenerator.CalculateStateForLocation(origin.X, origin.Y)) != TrackGenerator.TrackPlacementState.Obstructed)
			{
				origin.Y++;
				if (trackPlacementState == TrackGenerator.TrackPlacementState.Invalid)
				{
					return false;
				}
			}
			origin.Y--;
			return TrackGenerator.CalculateStateForLocation(origin.X, origin.Y) == TrackGenerator.TrackPlacementState.Available;
		}

		// Token: 0x06004505 RID: 17669 RVA: 0x0060E464 File Offset: 0x0060C664
		private static TrackGenerator.TrackPlacementState CalculateStateForLocation(int x, int y)
		{
			for (int i = 0; i < 6; i++)
			{
				if (TrackGenerator.IsLocationInvalid(x, y - i))
				{
					return TrackGenerator.TrackPlacementState.Invalid;
				}
			}
			for (int j = 0; j < 6; j++)
			{
				if (TrackGenerator.IsMinecartTrack(x, y + j))
				{
					return TrackGenerator.TrackPlacementState.Invalid;
				}
			}
			for (int k = 0; k < 6; k++)
			{
				if (WorldGen.SolidTile(x, y - k, false))
				{
					return TrackGenerator.TrackPlacementState.Obstructed;
				}
			}
			if (WorldGen.IsTileNearby(x, y, 314, 30))
			{
				return TrackGenerator.TrackPlacementState.Invalid;
			}
			return TrackGenerator.TrackPlacementState.Available;
		}

		// Token: 0x06004506 RID: 17670 RVA: 0x0060E4D0 File Offset: 0x0060C6D0
		private unsafe static bool IsMinecartTrack(int x, int y)
		{
			return Main.tile[x, y].active() && *Main.tile[x, y].type == 314;
		}

		// Token: 0x06004507 RID: 17671 RVA: 0x0060E514 File Offset: 0x0060C714
		private unsafe static bool IsLocationInvalid(int x, int y)
		{
			if (y > Main.UnderworldLayer || x < 5 || y < (int)Main.worldSurface || x > Main.maxTilesX - 5)
			{
				return true;
			}
			if (Math.Abs((double)x - GenVars.shimmerPosition.X) < (double)(WorldGen.shimmerSafetyDistance / 2) && Math.Abs((double)y - GenVars.shimmerPosition.Y) < (double)(WorldGen.shimmerSafetyDistance / 2))
			{
				return true;
			}
			if (WorldGen.oceanDepths(x, y))
			{
				return true;
			}
			ushort wall = *Main.tile[x, y].wall;
			for (int i = 0; i < TrackGenerator.InvalidWalls.Length; i++)
			{
				if (wall == TrackGenerator.InvalidWalls[i] && (!WorldGen.notTheBees || wall != 108))
				{
					return true;
				}
			}
			ushort type = *Main.tile[x, y].type;
			for (int j = 0; j < TrackGenerator.InvalidTiles.Length; j++)
			{
				if (type == TrackGenerator.InvalidTiles[j])
				{
					return true;
				}
			}
			for (int k = -1; k <= 1; k++)
			{
				if (Main.tile[x + k, y].active() && (*Main.tile[x + k, y].type == 314 || !TileID.Sets.GeneralPlacementTiles[(int)(*Main.tile[x + k, y].type)]) && (!WorldGen.notTheBees || *Main.tile[x + k, y].type != 225))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004508 RID: 17672 RVA: 0x0060E697 File Offset: 0x0060C897
		[Conditional("DEBUG")]
		private void DrawPause()
		{
		}

		// Token: 0x04005AC2 RID: 23234
		private static readonly ushort[] InvalidWalls = new ushort[]
		{
			7,
			94,
			95,
			8,
			98,
			99,
			9,
			96,
			97,
			3,
			83,
			68,
			62,
			78,
			87,
			86,
			42,
			74,
			27,
			149
		};

		// Token: 0x04005AC3 RID: 23235
		private static readonly ushort[] InvalidTiles = new ushort[]
		{
			383,
			384,
			15,
			304,
			30,
			321,
			245,
			246,
			240,
			241,
			242,
			16,
			34,
			158,
			377,
			94,
			10,
			19,
			86,
			219,
			484,
			190,
			664,
			665
		};

		// Token: 0x04005AC4 RID: 23236
		private readonly TrackGenerator.TrackHistory[] _history = new TrackGenerator.TrackHistory[4096];

		// Token: 0x04005AC5 RID: 23237
		private readonly TrackGenerator.TrackHistory[] _rewriteHistory = new TrackGenerator.TrackHistory[25];

		// Token: 0x04005AC6 RID: 23238
		private int _xDirection;

		// Token: 0x04005AC7 RID: 23239
		private int _length;

		// Token: 0x04005AC8 RID: 23240
		private int playerHeight = 6;

		// Token: 0x02000CCE RID: 3278
		private enum TrackPlacementState
		{
			// Token: 0x04007A05 RID: 31237
			Available,
			// Token: 0x04007A06 RID: 31238
			Obstructed,
			// Token: 0x04007A07 RID: 31239
			Invalid
		}

		// Token: 0x02000CCF RID: 3279
		private enum TrackSlope : sbyte
		{
			// Token: 0x04007A09 RID: 31241
			Up = -1,
			// Token: 0x04007A0A RID: 31242
			Straight,
			// Token: 0x04007A0B RID: 31243
			Down
		}

		// Token: 0x02000CD0 RID: 3280
		private enum TrackMode : byte
		{
			// Token: 0x04007A0D RID: 31245
			Normal,
			// Token: 0x04007A0E RID: 31246
			Tunnel
		}

		// Token: 0x02000CD1 RID: 3281
		[DebuggerDisplay("X = {X}, Y = {Y}, Slope = {Slope}")]
		private struct TrackHistory
		{
			// Token: 0x06006180 RID: 24960 RVA: 0x006D406E File Offset: 0x006D226E
			public TrackHistory(int x, int y, TrackGenerator.TrackSlope slope)
			{
				this.X = (short)x;
				this.Y = (short)y;
				this.Slope = slope;
				this.Mode = TrackGenerator.TrackMode.Normal;
			}

			// Token: 0x04007A0F RID: 31247
			public short X;

			// Token: 0x04007A10 RID: 31248
			public short Y;

			// Token: 0x04007A11 RID: 31249
			public TrackGenerator.TrackSlope Slope;

			// Token: 0x04007A12 RID: 31250
			public TrackGenerator.TrackMode Mode;
		}
	}
}
