using System;
using Microsoft.Xna.Framework;

namespace Terraria.WorldBuilding
{
	// Token: 0x0200006B RID: 107
	public static class Searches
	{
		// Token: 0x0600114B RID: 4427 RVA: 0x0048CABB File Offset: 0x0048ACBB
		public static GenSearch Chain(GenSearch search, params GenCondition[] conditions)
		{
			return search.Conditions(conditions);
		}

		// Token: 0x0200052D RID: 1325
		public class Left : GenSearch
		{
			// Token: 0x0600308E RID: 12430 RVA: 0x005E30F1 File Offset: 0x005E12F1
			public Left(int maxDistance)
			{
				this._maxDistance = maxDistance;
			}

			// Token: 0x0600308F RID: 12431 RVA: 0x005E3100 File Offset: 0x005E1300
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

			// Token: 0x04005803 RID: 22531
			private int _maxDistance;
		}

		// Token: 0x0200052E RID: 1326
		public class Right : GenSearch
		{
			// Token: 0x06003090 RID: 12432 RVA: 0x005E314D File Offset: 0x005E134D
			public Right(int maxDistance)
			{
				this._maxDistance = maxDistance;
			}

			// Token: 0x06003091 RID: 12433 RVA: 0x005E315C File Offset: 0x005E135C
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

			// Token: 0x04005804 RID: 22532
			private int _maxDistance;
		}

		// Token: 0x0200052F RID: 1327
		public class Down : GenSearch
		{
			// Token: 0x06003092 RID: 12434 RVA: 0x005E31A9 File Offset: 0x005E13A9
			public Down(int maxDistance)
			{
				this._maxDistance = maxDistance;
			}

			// Token: 0x06003093 RID: 12435 RVA: 0x005E31B8 File Offset: 0x005E13B8
			public override Point Find(Point origin)
			{
				int num = 0;
				while (num < this._maxDistance && origin.Y + num < Main.maxTilesY)
				{
					if (base.Check(origin.X, origin.Y + num))
					{
						return new Point(origin.X, origin.Y + num);
					}
					num++;
				}
				return GenSearch.NOT_FOUND;
			}

			// Token: 0x04005805 RID: 22533
			private int _maxDistance;
		}

		// Token: 0x02000530 RID: 1328
		public class Up : GenSearch
		{
			// Token: 0x06003094 RID: 12436 RVA: 0x005E3214 File Offset: 0x005E1414
			public Up(int maxDistance)
			{
				this._maxDistance = maxDistance;
			}

			// Token: 0x06003095 RID: 12437 RVA: 0x005E3224 File Offset: 0x005E1424
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

			// Token: 0x04005806 RID: 22534
			private int _maxDistance;
		}

		// Token: 0x02000531 RID: 1329
		public class Rectangle : GenSearch
		{
			// Token: 0x06003096 RID: 12438 RVA: 0x005E3271 File Offset: 0x005E1471
			public Rectangle(int width, int height)
			{
				this._width = width;
				this._height = height;
			}

			// Token: 0x06003097 RID: 12439 RVA: 0x005E3288 File Offset: 0x005E1488
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

			// Token: 0x04005807 RID: 22535
			private int _width;

			// Token: 0x04005808 RID: 22536
			private int _height;
		}
	}
}
