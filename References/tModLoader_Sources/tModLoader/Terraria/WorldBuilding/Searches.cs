using System;
using Microsoft.Xna.Framework;

namespace Terraria.WorldBuilding
{
	// Token: 0x0200007F RID: 127
	public static class Searches
	{
		// Token: 0x060013F9 RID: 5113 RVA: 0x0049FBE0 File Offset: 0x0049DDE0
		public static GenSearch Chain(GenSearch search, params GenCondition[] conditions)
		{
			return search.Conditions(conditions);
		}

		// Token: 0x02000848 RID: 2120
		public class Left : GenSearch
		{
			// Token: 0x060050F3 RID: 20723 RVA: 0x00695588 File Offset: 0x00693788
			public Left(int maxDistance)
			{
				this._maxDistance = maxDistance;
			}

			// Token: 0x060050F4 RID: 20724 RVA: 0x00695598 File Offset: 0x00693798
			public override Point Find(Point origin)
			{
				for (int i = 0; i < this._maxDistance; i++)
				{
					if (base.Check(origin.X - i, origin.Y))
					{
						return new Point(origin.X - i, origin.Y);
					}
				}
				return GenSearch.NOT_FOUND;
			}

			// Token: 0x040068BB RID: 26811
			private int _maxDistance;
		}

		// Token: 0x02000849 RID: 2121
		public class Right : GenSearch
		{
			// Token: 0x060050F5 RID: 20725 RVA: 0x006955E5 File Offset: 0x006937E5
			public Right(int maxDistance)
			{
				this._maxDistance = maxDistance;
			}

			// Token: 0x060050F6 RID: 20726 RVA: 0x006955F4 File Offset: 0x006937F4
			public override Point Find(Point origin)
			{
				for (int i = 0; i < this._maxDistance; i++)
				{
					if (base.Check(origin.X + i, origin.Y))
					{
						return new Point(origin.X + i, origin.Y);
					}
				}
				return GenSearch.NOT_FOUND;
			}

			// Token: 0x040068BC RID: 26812
			private int _maxDistance;
		}

		// Token: 0x0200084A RID: 2122
		public class Down : GenSearch
		{
			// Token: 0x060050F7 RID: 20727 RVA: 0x00695641 File Offset: 0x00693841
			public Down(int maxDistance)
			{
				this._maxDistance = maxDistance;
			}

			// Token: 0x060050F8 RID: 20728 RVA: 0x00695650 File Offset: 0x00693850
			public override Point Find(Point origin)
			{
				int i = 0;
				while (i < this._maxDistance && origin.Y + i < Main.maxTilesY)
				{
					if (base.Check(origin.X, origin.Y + i))
					{
						return new Point(origin.X, origin.Y + i);
					}
					i++;
				}
				return GenSearch.NOT_FOUND;
			}

			// Token: 0x040068BD RID: 26813
			private int _maxDistance;
		}

		// Token: 0x0200084B RID: 2123
		public class Up : GenSearch
		{
			// Token: 0x060050F9 RID: 20729 RVA: 0x006956AC File Offset: 0x006938AC
			public Up(int maxDistance)
			{
				this._maxDistance = maxDistance;
			}

			// Token: 0x060050FA RID: 20730 RVA: 0x006956BC File Offset: 0x006938BC
			public override Point Find(Point origin)
			{
				for (int i = 0; i < this._maxDistance; i++)
				{
					if (base.Check(origin.X, origin.Y - i))
					{
						return new Point(origin.X, origin.Y - i);
					}
				}
				return GenSearch.NOT_FOUND;
			}

			// Token: 0x040068BE RID: 26814
			private int _maxDistance;
		}

		// Token: 0x0200084C RID: 2124
		public class Rectangle : GenSearch
		{
			// Token: 0x060050FB RID: 20731 RVA: 0x00695709 File Offset: 0x00693909
			public Rectangle(int width, int height)
			{
				this._width = width;
				this._height = height;
			}

			// Token: 0x060050FC RID: 20732 RVA: 0x00695720 File Offset: 0x00693920
			public override Point Find(Point origin)
			{
				for (int i = 0; i < this._width; i++)
				{
					for (int j = 0; j < this._height; j++)
					{
						if (base.Check(origin.X + i, origin.Y + j))
						{
							return new Point(origin.X + i, origin.Y + j);
						}
					}
				}
				return GenSearch.NOT_FOUND;
			}

			// Token: 0x040068BF RID: 26815
			private int _width;

			// Token: 0x040068C0 RID: 26816
			private int _height;
		}
	}
}
