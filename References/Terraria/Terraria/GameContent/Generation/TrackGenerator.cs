using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation
{
	// Token: 0x020003E5 RID: 997
	public class TrackGenerator
	{
		// Token: 0x06002AC4 RID: 10948 RVA: 0x0059BF88 File Offset: 0x0059A188
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

		// Token: 0x06002AC5 RID: 10949 RVA: 0x0059BFB0 File Offset: 0x0059A1B0
		private void PlacePath()
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
					if (Main.tile[(int)this._history[i].X, (int)this._history[i].Y - j - 1].wall == 244)
					{
						Main.tile[(int)this._history[i].X, (int)this._history[i].Y - j - 1].wall = 0;
					}
					if (Main.tile[(int)this._history[i].X, (int)this._history[i].Y - j].wall == 244)
					{
						Main.tile[(int)this._history[i].X, (int)this._history[i].Y - j].wall = 0;
					}
					if (Main.tile[(int)this._history[i].X, (int)this._history[i].Y - j + 1].wall == 244)
					{
						Main.tile[(int)this._history[i].X, (int)this._history[i].Y - j + 1].wall = 0;
					}
					if (Main.tile[(int)this._history[i].X, (int)this._history[i].Y - j].type == 135)
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

		// Token: 0x06002AC6 RID: 10950 RVA: 0x0059C3B0 File Offset: 0x0059A5B0
		private void CreateTrackStart(Point origin)
		{
			this._xDirection = ((origin.X > Main.maxTilesX / 2) ? -1 : 1);
			this._length = 1;
			for (int i = 0; i < this._history.Length; i++)
			{
				this._history[i] = new TrackGenerator.TrackHistory(origin.X + i * this._xDirection, origin.Y + i, TrackGenerator.TrackSlope.Down);
			}
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x0059C41C File Offset: 0x0059A61C
		private bool FindPath(int minLength, int maxLength)
		{
			int length = this._length;
			while (this._length < this._history.Length - 100)
			{
				TrackGenerator.TrackSlope slope = (this._history[this._length - 1].Slope == TrackGenerator.TrackSlope.Up) ? TrackGenerator.TrackSlope.Straight : TrackGenerator.TrackSlope.Down;
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

		// Token: 0x06002AC8 RID: 10952 RVA: 0x0059C4D4 File Offset: 0x0059A6D4
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
				for (int i = 1; i < num; i++)
				{
					trackPlacementState2 = TrackGenerator.CalculateStateForLocation(x + i * this._xDirection, y + i * (int)trackSlope2);
					if (trackPlacementState2 == TrackGenerator.TrackPlacementState.Invalid)
					{
						break;
					}
					if (trackPlacementState2 != TrackGenerator.TrackPlacementState.Obstructed)
					{
						trackSlope = trackSlope2;
						num = i;
						trackPlacementState = trackPlacementState2;
						break;
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

		// Token: 0x06002AC9 RID: 10953 RVA: 0x0059C5F0 File Offset: 0x0059A7F0
		private void AppendToHistory(TrackGenerator.TrackSlope slope, TrackGenerator.TrackMode mode = TrackGenerator.TrackMode.Normal)
		{
			this._history[this._length] = new TrackGenerator.TrackHistory((int)this._history[this._length - 1].X + this._xDirection, (int)((sbyte)this._history[this._length - 1].Y + slope), slope);
			this._history[this._length].Mode = mode;
			this._length++;
		}

		// Token: 0x06002ACA RID: 10954 RVA: 0x0059C674 File Offset: 0x0059A874
		private TrackGenerator.TrackPlacementState TryRewriteHistoryToAvoidTiles()
		{
			int i = this._length - 1;
			int num = Math.Min(this._length, this._rewriteHistory.Length);
			for (int j = 0; j < num; j++)
			{
				this._rewriteHistory[j] = this._history[i - j];
			}
			while (i >= this._length - num)
			{
				if (this._history[i].Slope == TrackGenerator.TrackSlope.Down)
				{
					TrackGenerator.TrackPlacementState historySegmentPlacementState = this.GetHistorySegmentPlacementState(i, this._length - i);
					if (historySegmentPlacementState == TrackGenerator.TrackPlacementState.Available)
					{
						return historySegmentPlacementState;
					}
					this.RewriteSlopeDirection(i, TrackGenerator.TrackSlope.Straight);
				}
				i--;
			}
			if (this.GetHistorySegmentPlacementState(i + 1, this._length - (i + 1)) == TrackGenerator.TrackPlacementState.Available)
			{
				return TrackGenerator.TrackPlacementState.Available;
			}
			for (i = this._length - 1; i >= this._length - num + 1; i--)
			{
				if (this._history[i].Slope == TrackGenerator.TrackSlope.Straight)
				{
					TrackGenerator.TrackPlacementState historySegmentPlacementState2 = this.GetHistorySegmentPlacementState(this._length - num, num);
					if (historySegmentPlacementState2 == TrackGenerator.TrackPlacementState.Available)
					{
						return historySegmentPlacementState2;
					}
					this.RewriteSlopeDirection(i, TrackGenerator.TrackSlope.Up);
				}
			}
			for (int k = 0; k < num; k++)
			{
				this._history[this._length - 1 - k] = this._rewriteHistory[k];
			}
			this.RewriteSlopeDirection(this._length - 1, TrackGenerator.TrackSlope.Straight);
			return this.GetHistorySegmentPlacementState(i + 1, this._length - (i + 1));
		}

		// Token: 0x06002ACB RID: 10955 RVA: 0x0059C7C4 File Offset: 0x0059A9C4
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

		// Token: 0x06002ACC RID: 10956 RVA: 0x0059C824 File Offset: 0x0059AA24
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

		// Token: 0x06002ACD RID: 10957 RVA: 0x0059C88C File Offset: 0x0059AA8C
		private void SmoothTrack()
		{
			int num = this._length - 1;
			bool flag = false;
			for (int i = this._length - 1; i >= 0; i--)
			{
				if (flag)
				{
					num = Math.Min(i + 15, num);
					if (this._history[i].Y >= this._history[num].Y)
					{
						int num2 = i + 1;
						while (this._history[num2].Y > this._history[i].Y)
						{
							this._history[num2].Y = this._history[i].Y;
							this._history[num2].Slope = TrackGenerator.TrackSlope.Straight;
							num2++;
						}
						if (this._history[i].Y == this._history[num].Y)
						{
							flag = false;
						}
					}
				}
				else if (this._history[i].Y > this._history[num].Y)
				{
					flag = true;
				}
				else
				{
					num = i;
				}
			}
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x0059C9A9 File Offset: 0x0059ABA9
		private static bool CanSlopesTouch(TrackGenerator.TrackSlope leftSlope, TrackGenerator.TrackSlope rightSlope)
		{
			return leftSlope == rightSlope || leftSlope == TrackGenerator.TrackSlope.Straight || rightSlope == TrackGenerator.TrackSlope.Straight;
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x0059C9B8 File Offset: 0x0059ABB8
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

		// Token: 0x06002AD0 RID: 10960 RVA: 0x0059CA10 File Offset: 0x0059AC10
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

		// Token: 0x06002AD1 RID: 10961 RVA: 0x0059CA7C File Offset: 0x0059AC7C
		private static bool IsMinecartTrack(int x, int y)
		{
			return Main.tile[x, y].active() && Main.tile[x, y].type == 314;
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x0059CAAC File Offset: 0x0059ACAC
		private static bool IsLocationInvalid(int x, int y)
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
			ushort wall = Main.tile[x, y].wall;
			for (int i = 0; i < TrackGenerator.InvalidWalls.Length; i++)
			{
				if (wall == TrackGenerator.InvalidWalls[i] && (!WorldGen.notTheBees || wall != 108))
				{
					return true;
				}
			}
			ushort type = Main.tile[x, y].type;
			for (int j = 0; j < TrackGenerator.InvalidTiles.Length; j++)
			{
				if (type == TrackGenerator.InvalidTiles[j])
				{
					return true;
				}
			}
			for (int k = -1; k <= 1; k++)
			{
				if (Main.tile[x + k, y].active() && (Main.tile[x + k, y].type == 314 || !TileID.Sets.GeneralPlacementTiles[(int)Main.tile[x + k, y].type]) && (!WorldGen.notTheBees || Main.tile[x + k, y].type != 225))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		[Conditional("DEBUG")]
		private void DrawPause()
		{
		}

		// Token: 0x04004D61 RID: 19809
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

		// Token: 0x04004D62 RID: 19810
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

		// Token: 0x04004D63 RID: 19811
		private readonly TrackGenerator.TrackHistory[] _history = new TrackGenerator.TrackHistory[4096];

		// Token: 0x04004D64 RID: 19812
		private readonly TrackGenerator.TrackHistory[] _rewriteHistory = new TrackGenerator.TrackHistory[25];

		// Token: 0x04004D65 RID: 19813
		private int _xDirection;

		// Token: 0x04004D66 RID: 19814
		private int _length;

		// Token: 0x04004D67 RID: 19815
		private int playerHeight = 6;

		// Token: 0x02000767 RID: 1895
		private enum TrackPlacementState
		{
			// Token: 0x04006461 RID: 25697
			Available,
			// Token: 0x04006462 RID: 25698
			Obstructed,
			// Token: 0x04006463 RID: 25699
			Invalid
		}

		// Token: 0x02000768 RID: 1896
		private enum TrackSlope : sbyte
		{
			// Token: 0x04006465 RID: 25701
			Up = -1,
			// Token: 0x04006466 RID: 25702
			Straight,
			// Token: 0x04006467 RID: 25703
			Down
		}

		// Token: 0x02000769 RID: 1897
		private enum TrackMode : byte
		{
			// Token: 0x04006469 RID: 25705
			Normal,
			// Token: 0x0400646A RID: 25706
			Tunnel
		}

		// Token: 0x0200076A RID: 1898
		[DebuggerDisplay("X = {X}, Y = {Y}, Slope = {Slope}")]
		private struct TrackHistory
		{
			// Token: 0x060038E8 RID: 14568 RVA: 0x006147C5 File Offset: 0x006129C5
			public TrackHistory(int x, int y, TrackGenerator.TrackSlope slope)
			{
				this.X = (short)x;
				this.Y = (short)y;
				this.Slope = slope;
				this.Mode = TrackGenerator.TrackMode.Normal;
			}

			// Token: 0x0400646B RID: 25707
			public short X;

			// Token: 0x0400646C RID: 25708
			public short Y;

			// Token: 0x0400646D RID: 25709
			public TrackGenerator.TrackSlope Slope;

			// Token: 0x0400646E RID: 25710
			public TrackGenerator.TrackMode Mode;
		}
	}
}
