using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace Terraria
{
	// Token: 0x0200006C RID: 108
	public class WorldSections
	{
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x0049EDAF File Offset: 0x0049CFAF
		public bool AnyUnfinishedSections
		{
			get
			{
				return this.frameSectionsLeft > 0;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x0049EDBA File Offset: 0x0049CFBA
		public bool AnyNeedRefresh
		{
			get
			{
				return this._sectionsNeedingRefresh > 0;
			}
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x0049EDC8 File Offset: 0x0049CFC8
		public WorldSections(int numSectionsX, int numSectionsY)
		{
			this.width = numSectionsX;
			this.height = numSectionsY;
			this.data = new BitsByte[this.width * this.height];
			this.mapSectionsLeft = this.width * this.height;
			this.prevFrame.Reset();
			this.prevMap.Reset();
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x0049EE2C File Offset: 0x0049D02C
		public void SetSectionAsRefreshed(int x, int y)
		{
			if (x >= 0)
			{
				int num = this.width;
			}
			if (y >= 0)
			{
				int num2 = this.height;
			}
			if (this.data[y * this.width + x][3])
			{
				this.data[y * this.width + x][3] = false;
				this._sectionsNeedingRefresh--;
			}
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x0049EE96 File Offset: 0x0049D096
		public bool SectionNeedsRefresh(int x, int y)
		{
			return x >= 0 && x < this.width && y >= 0 && y < this.height && this.data[y * this.width + x][3];
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x0049EED4 File Offset: 0x0049D0D4
		public void SetAllFramedSectionsAsNeedingRefresh()
		{
			for (int i = 0; i < this.data.Length; i++)
			{
				if (this.data[i][1])
				{
					this.data[i][3] = true;
					this._sectionsNeedingRefresh++;
				}
			}
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x0049EF29 File Offset: 0x0049D129
		public bool SectionLoaded(int x, int y)
		{
			return x >= 0 && x < this.width && y >= 0 && y < this.height && this.data[y * this.width + x][0];
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0049EF64 File Offset: 0x0049D164
		public bool SectionFramed(int x, int y)
		{
			return x >= 0 && x < this.width && y >= 0 && y < this.height && this.data[y * this.width + x][1];
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0049EF9F File Offset: 0x0049D19F
		public bool MapSectionDrawn(int x, int y)
		{
			return x >= 0 && x < this.width && y >= 0 && y < this.height && this.data[y * this.width + x][2];
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x0049EFDC File Offset: 0x0049D1DC
		public void ClearMapDraw()
		{
			for (int i = 0; i < this.data.Length; i++)
			{
				this.data[i][2] = false;
			}
			this.prevMap.Reset();
			this.mapSectionsLeft = this.data.Length;
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x0049F028 File Offset: 0x0049D228
		public void SetSectionFramed(int x, int y)
		{
			if (x >= 0 && x < this.width && y >= 0 && y < this.height)
			{
				BitsByte bitsByte = this.data[y * this.width + x];
				if (bitsByte[0] && !bitsByte[1])
				{
					bitsByte[1] = true;
					this.data[y * this.width + x] = bitsByte;
					this.frameSectionsLeft--;
				}
			}
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x0049F0A6 File Offset: 0x0049D2A6
		public void SetSectionLoaded(int x, int y)
		{
			if (x >= 0 && x < this.width && y >= 0 && y < this.height)
			{
				this.SetSectionLoaded(ref this.data[y * this.width + x]);
			}
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x0049F0E0 File Offset: 0x0049D2E0
		private void SetSectionLoaded(ref BitsByte section)
		{
			if (!section[0])
			{
				section[0] = true;
				this.frameSectionsLeft++;
				return;
			}
			if (section[1])
			{
				section[1] = false;
				this.frameSectionsLeft++;
			}
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x0049F12C File Offset: 0x0049D32C
		public void SetAllSectionsLoaded()
		{
			for (int i = 0; i < this.data.Length; i++)
			{
				this.SetSectionLoaded(ref this.data[i]);
			}
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0049F160 File Offset: 0x0049D360
		public void SetTilesLoaded(int startX, int startY, int endXInclusive, int endYInclusive)
		{
			int sectionX3 = Netplay.GetSectionX(startX);
			int sectionY = Netplay.GetSectionY(startY);
			int sectionX2 = Netplay.GetSectionX(endXInclusive);
			int sectionY2 = Netplay.GetSectionY(endYInclusive);
			for (int i = sectionX3; i <= sectionX2; i++)
			{
				for (int j = sectionY; j <= sectionY2; j++)
				{
					this.SetSectionLoaded(i, j);
				}
			}
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0049F1B0 File Offset: 0x0049D3B0
		public bool GetNextMapDraw(Vector2 playerPos, out int x, out int y)
		{
			if (this.mapSectionsLeft <= 0)
			{
				x = -1;
				y = -1;
				return false;
			}
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			int num = 0;
			int num2 = 0;
			Vector2 vector = this.prevMap.centerPos;
			playerPos *= 0.0625f;
			int sectionX = Netplay.GetSectionX((int)playerPos.X);
			int sectionY = Netplay.GetSectionY((int)playerPos.Y);
			int num3 = Netplay.GetSectionX((int)vector.X);
			int num4 = Netplay.GetSectionY((int)vector.Y);
			int num5;
			if (num3 != sectionX || num4 != sectionY)
			{
				vector = playerPos;
				num3 = sectionX;
				num4 = sectionY;
				num5 = 4;
				x = sectionX;
				y = sectionY;
			}
			else
			{
				num5 = this.prevMap.leg;
				x = this.prevMap.X;
				y = this.prevMap.Y;
				num = this.prevMap.xDir;
				num2 = this.prevMap.yDir;
			}
			int num6 = (int)(playerPos.X - ((float)num3 + 0.5f) * 200f);
			int num7 = (int)(playerPos.Y - ((float)num4 + 0.5f) * 150f);
			if (num == 0)
			{
				num = ((num6 <= 0) ? 1 : -1);
				num2 = ((num7 <= 0) ? 1 : -1);
			}
			int num8 = 0;
			bool flag = false;
			bool flag2 = false;
			for (;;)
			{
				if (num8 == 4)
				{
					if (flag2)
					{
						break;
					}
					flag2 = true;
					x = num3;
					y = num4;
					num6 = (int)(vector.X - ((float)num3 + 0.5f) * 200f);
					num7 = (int)(vector.Y - ((float)num4 + 0.5f) * 150f);
					num = ((num6 <= 0) ? 1 : -1);
					num2 = ((num7 <= 0) ? 1 : -1);
					num5 = 4;
					num8 = 0;
				}
				if (y >= 0 && y < this.height && x >= 0 && x < this.width)
				{
					flag = false;
					if (!this.data[y * this.width + x][2])
					{
						goto IL_2A0;
					}
				}
				int num9 = x - num3;
				int num10 = y - num4;
				if (num9 == 0 || num10 == 0)
				{
					if (num5 == 4)
					{
						if (num9 == 0 && num10 == 0)
						{
							if (Math.Abs(num6) > Math.Abs(num7))
							{
								y -= num2;
							}
							else
							{
								x -= num;
							}
						}
						else
						{
							if (num9 != 0)
							{
								x += num9 / Math.Abs(num9);
							}
							if (num10 != 0)
							{
								y += num10 / Math.Abs(num10);
							}
						}
						num5 = 0;
						num8 = -2;
						flag = true;
					}
					else
					{
						if (num9 != 0)
						{
							num = ((num9 <= 0) ? 1 : -1);
						}
						else
						{
							num2 = ((num10 <= 0) ? 1 : -1);
						}
						x += num;
						y += num2;
						num5++;
					}
					if (flag)
					{
						num8++;
					}
					else
					{
						flag = true;
					}
				}
				else
				{
					x += num;
					y += num2;
				}
			}
			throw new Exception("Infinite loop in WorldSections.GetNextMapDraw");
			IL_2A0:
			this.data[y * this.width + x][2] = true;
			this.mapSectionsLeft--;
			this.prevMap.centerPos = playerPos;
			this.prevMap.X = x;
			this.prevMap.Y = y;
			this.prevMap.leg = num5;
			this.prevMap.xDir = num;
			this.prevMap.yDir = num2;
			stopwatch.Stop();
			return true;
		}

		/// <summary>
		/// Checks if the tile at the specified tile coordinate has been loaded for this client in a multiplayer game session. In multiplayer, sections of tiles are sent when the player visits them, so much of the map will not be loaded for a client. until visited.
		/// <para /> Modders may need to check this for client code that could potentially access unloaded tiles. 
		/// </summary>
		// Token: 0x060013C3 RID: 5059 RVA: 0x0049F4DB File Offset: 0x0049D6DB
		public bool TileLoaded(int tileX, int tileY)
		{
			return this.SectionLoaded(Netplay.GetSectionX(tileX), Netplay.GetSectionY(tileY));
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x0049F4F0 File Offset: 0x0049D6F0
		public bool TilesLoaded(int startX, int startY, int endXInclusive, int endYInclusive)
		{
			int sectionX = Netplay.GetSectionX(startX);
			int sY = Netplay.GetSectionY(startY);
			int eX = Netplay.GetSectionX(endXInclusive);
			int eY = Netplay.GetSectionY(endYInclusive);
			for (int x = sectionX; x <= eX; x++)
			{
				for (int y = sY; y <= eY; y++)
				{
					if (!this.SectionLoaded(x, y))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x04000FCD RID: 4045
		public const int BitIndex_SectionLoaded = 0;

		// Token: 0x04000FCE RID: 4046
		public const int BitIndex_SectionFramed = 1;

		// Token: 0x04000FCF RID: 4047
		public const int BitIndex_SectionMapDrawn = 2;

		// Token: 0x04000FD0 RID: 4048
		public const int BitIndex_SectionNeedsRefresh = 3;

		// Token: 0x04000FD1 RID: 4049
		private int width;

		// Token: 0x04000FD2 RID: 4050
		private int height;

		// Token: 0x04000FD3 RID: 4051
		private BitsByte[] data;

		// Token: 0x04000FD4 RID: 4052
		private int mapSectionsLeft;

		// Token: 0x04000FD5 RID: 4053
		private int frameSectionsLeft;

		// Token: 0x04000FD6 RID: 4054
		private int _sectionsNeedingRefresh;

		// Token: 0x04000FD7 RID: 4055
		private WorldSections.IterationState prevFrame;

		// Token: 0x04000FD8 RID: 4056
		private WorldSections.IterationState prevMap;

		// Token: 0x0200080C RID: 2060
		private struct IterationState
		{
			// Token: 0x0600506F RID: 20591 RVA: 0x00693E0C File Offset: 0x0069200C
			public void Reset()
			{
				this.centerPos = new Vector2(-3200f, -2400f);
				this.X = 0;
				this.Y = 0;
				this.leg = 0;
				this.xDir = 0;
				this.yDir = 0;
			}

			// Token: 0x04006869 RID: 26729
			public Vector2 centerPos;

			// Token: 0x0400686A RID: 26730
			public int X;

			// Token: 0x0400686B RID: 26731
			public int Y;

			// Token: 0x0400686C RID: 26732
			public int leg;

			// Token: 0x0400686D RID: 26733
			public int xDir;

			// Token: 0x0400686E RID: 26734
			public int yDir;
		}
	}
}
