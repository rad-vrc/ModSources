using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace Terraria
{
	// Token: 0x02000050 RID: 80
	public class WorldSections
	{
		// Token: 0x06001078 RID: 4216 RVA: 0x004802D8 File Offset: 0x0047E4D8
		public WorldSections(int numSectionsX, int numSectionsY)
		{
			this.width = numSectionsX;
			this.height = numSectionsY;
			this.data = new BitsByte[this.width * this.height];
			this.mapSectionsLeft = this.width * this.height;
			this.prevFrame.Reset();
			this.prevMap.Reset();
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x0048033A File Offset: 0x0047E53A
		public bool AnyUnfinishedSections
		{
			get
			{
				return this.frameSectionsLeft > 0;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x00480345 File Offset: 0x0047E545
		public bool AnyNeedRefresh
		{
			get
			{
				return this._sectionsNeedingRefresh > 0;
			}
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x00480350 File Offset: 0x0047E550
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
			if (!this.data[y * this.width + x][3])
			{
				return;
			}
			this.data[y * this.width + x][3] = false;
			this._sectionsNeedingRefresh--;
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x004803BF File Offset: 0x0047E5BF
		public bool SectionNeedsRefresh(int x, int y)
		{
			return x >= 0 && x < this.width && y >= 0 && y < this.height && this.data[y * this.width + x][3];
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x004803FC File Offset: 0x0047E5FC
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

		// Token: 0x0600107E RID: 4222 RVA: 0x00480451 File Offset: 0x0047E651
		public bool SectionLoaded(int x, int y)
		{
			return x >= 0 && x < this.width && y >= 0 && y < this.height && this.data[y * this.width + x][0];
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0048048C File Offset: 0x0047E68C
		public bool SectionFramed(int x, int y)
		{
			return x >= 0 && x < this.width && y >= 0 && y < this.height && this.data[y * this.width + x][1];
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x004804C7 File Offset: 0x0047E6C7
		public bool MapSectionDrawn(int x, int y)
		{
			return x >= 0 && x < this.width && y >= 0 && y < this.height && this.data[y * this.width + x][2];
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x00480504 File Offset: 0x0047E704
		public void ClearMapDraw()
		{
			for (int i = 0; i < this.data.Length; i++)
			{
				this.data[i][2] = false;
			}
			this.prevMap.Reset();
			this.mapSectionsLeft = this.data.Length;
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00480550 File Offset: 0x0047E750
		public void SetSectionFramed(int x, int y)
		{
			if (x < 0 || x >= this.width)
			{
				return;
			}
			if (y < 0 || y >= this.height)
			{
				return;
			}
			BitsByte bitsByte = this.data[y * this.width + x];
			if (bitsByte[0] && !bitsByte[1])
			{
				bitsByte[1] = true;
				this.data[y * this.width + x] = bitsByte;
				this.frameSectionsLeft--;
			}
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x004805D0 File Offset: 0x0047E7D0
		public void SetSectionLoaded(int x, int y)
		{
			if (x < 0 || x >= this.width)
			{
				return;
			}
			if (y < 0 || y >= this.height)
			{
				return;
			}
			this.SetSectionLoaded(ref this.data[y * this.width + x]);
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0048060C File Offset: 0x0047E80C
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

		// Token: 0x06001085 RID: 4229 RVA: 0x00480658 File Offset: 0x0047E858
		public void SetAllSectionsLoaded()
		{
			for (int i = 0; i < this.data.Length; i++)
			{
				this.SetSectionLoaded(ref this.data[i]);
			}
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x0048068C File Offset: 0x0047E88C
		public void SetTilesLoaded(int startX, int startY, int endXInclusive, int endYInclusive)
		{
			int sectionX = Netplay.GetSectionX(startX);
			int sectionY = Netplay.GetSectionY(startY);
			int sectionX2 = Netplay.GetSectionX(endXInclusive);
			int sectionY2 = Netplay.GetSectionY(endYInclusive);
			for (int i = sectionX; i <= sectionX2; i++)
			{
				for (int j = sectionY; j <= sectionY2; j++)
				{
					this.SetSectionLoaded(i, j);
				}
			}
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x004806DC File Offset: 0x0047E8DC
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
				if (num6 > 0)
				{
					num = -1;
				}
				else
				{
					num = 1;
				}
				if (num7 > 0)
				{
					num2 = -1;
				}
				else
				{
					num2 = 1;
				}
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
					if (num6 > 0)
					{
						num = -1;
					}
					else
					{
						num = 1;
					}
					if (num7 > 0)
					{
						num2 = -1;
					}
					else
					{
						num2 = 1;
					}
					num5 = 4;
					num8 = 0;
				}
				if (y >= 0 && y < this.height && x >= 0 && x < this.width)
				{
					flag = false;
					if (!this.data[y * this.width + x][2])
					{
						goto Block_14;
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
						if (num9 == 0)
						{
							if (num10 > 0)
							{
								num2 = -1;
							}
							else
							{
								num2 = 1;
							}
						}
						else if (num9 > 0)
						{
							num = -1;
						}
						else
						{
							num = 1;
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
			Block_14:
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

		// Token: 0x04000E81 RID: 3713
		public const int BitIndex_SectionLoaded = 0;

		// Token: 0x04000E82 RID: 3714
		public const int BitIndex_SectionFramed = 1;

		// Token: 0x04000E83 RID: 3715
		public const int BitIndex_SectionMapDrawn = 2;

		// Token: 0x04000E84 RID: 3716
		public const int BitIndex_SectionNeedsRefresh = 3;

		// Token: 0x04000E85 RID: 3717
		private int width;

		// Token: 0x04000E86 RID: 3718
		private int height;

		// Token: 0x04000E87 RID: 3719
		private BitsByte[] data;

		// Token: 0x04000E88 RID: 3720
		private int mapSectionsLeft;

		// Token: 0x04000E89 RID: 3721
		private int frameSectionsLeft;

		// Token: 0x04000E8A RID: 3722
		private int _sectionsNeedingRefresh;

		// Token: 0x04000E8B RID: 3723
		private WorldSections.IterationState prevFrame;

		// Token: 0x04000E8C RID: 3724
		private WorldSections.IterationState prevMap;

		// Token: 0x020004F0 RID: 1264
		private struct IterationState
		{
			// Token: 0x06003008 RID: 12296 RVA: 0x005E1A59 File Offset: 0x005DFC59
			public void Reset()
			{
				this.centerPos = new Vector2(-3200f, -2400f);
				this.X = 0;
				this.Y = 0;
				this.leg = 0;
				this.xDir = 0;
				this.yDir = 0;
			}

			// Token: 0x040057AD RID: 22445
			public Vector2 centerPos;

			// Token: 0x040057AE RID: 22446
			public int X;

			// Token: 0x040057AF RID: 22447
			public int Y;

			// Token: 0x040057B0 RID: 22448
			public int leg;

			// Token: 0x040057B1 RID: 22449
			public int xDir;

			// Token: 0x040057B2 RID: 22450
			public int yDir;
		}
	}
}
