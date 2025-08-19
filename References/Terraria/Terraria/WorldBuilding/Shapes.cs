using System;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000070 RID: 112
	public static class Shapes
	{
		// Token: 0x02000532 RID: 1330
		public class Circle : GenShape
		{
			// Token: 0x06003098 RID: 12440 RVA: 0x005E32EA File Offset: 0x005E14EA
			public Circle(int radius)
			{
				this._verticalRadius = radius;
				this._horizontalRadius = radius;
			}

			// Token: 0x06003099 RID: 12441 RVA: 0x005E3300 File Offset: 0x005E1500
			public Circle(int horizontalRadius, int verticalRadius)
			{
				this._horizontalRadius = horizontalRadius;
				this._verticalRadius = verticalRadius;
			}

			// Token: 0x0600309A RID: 12442 RVA: 0x005E3316 File Offset: 0x005E1516
			public void SetRadius(int radius)
			{
				this._verticalRadius = radius;
				this._horizontalRadius = radius;
			}

			// Token: 0x0600309B RID: 12443 RVA: 0x005E3328 File Offset: 0x005E1528
			public override bool Perform(Point origin, GenAction action)
			{
				int num = (this._horizontalRadius + 1) * (this._horizontalRadius + 1);
				for (int i = origin.Y - this._verticalRadius; i <= origin.Y + this._verticalRadius; i++)
				{
					double num2 = (double)this._horizontalRadius / (double)this._verticalRadius * (double)(i - origin.Y);
					int num3 = Math.Min(this._horizontalRadius, (int)Math.Sqrt((double)num - num2 * num2));
					for (int j = origin.X - num3; j <= origin.X + num3; j++)
					{
						if (!base.UnitApply(action, origin, j, i, new object[0]) && this._quitOnFail)
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x04005809 RID: 22537
			private int _verticalRadius;

			// Token: 0x0400580A RID: 22538
			private int _horizontalRadius;
		}

		// Token: 0x02000533 RID: 1331
		public class HalfCircle : GenShape
		{
			// Token: 0x0600309C RID: 12444 RVA: 0x005E33DC File Offset: 0x005E15DC
			public HalfCircle(int radius)
			{
				this._radius = radius;
			}

			// Token: 0x0600309D RID: 12445 RVA: 0x005E33EC File Offset: 0x005E15EC
			public override bool Perform(Point origin, GenAction action)
			{
				int num = (this._radius + 1) * (this._radius + 1);
				for (int i = origin.Y - this._radius; i <= origin.Y; i++)
				{
					int num2 = Math.Min(this._radius, (int)Math.Sqrt((double)(num - (i - origin.Y) * (i - origin.Y))));
					for (int j = origin.X - num2; j <= origin.X + num2; j++)
					{
						if (!base.UnitApply(action, origin, j, i, new object[0]) && this._quitOnFail)
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x0400580B RID: 22539
			private int _radius;
		}

		// Token: 0x02000534 RID: 1332
		public class Slime : GenShape
		{
			// Token: 0x0600309E RID: 12446 RVA: 0x005E3485 File Offset: 0x005E1685
			public Slime(int radius)
			{
				this._radius = radius;
				this._xScale = 1.0;
				this._yScale = 1.0;
			}

			// Token: 0x0600309F RID: 12447 RVA: 0x005E34B2 File Offset: 0x005E16B2
			public Slime(int radius, double xScale, double yScale)
			{
				this._radius = radius;
				this._xScale = xScale;
				this._yScale = yScale;
			}

			// Token: 0x060030A0 RID: 12448 RVA: 0x005E34D0 File Offset: 0x005E16D0
			public override bool Perform(Point origin, GenAction action)
			{
				double num = (double)this._radius;
				int num2 = (this._radius + 1) * (this._radius + 1);
				for (int i = origin.Y - (int)(num * this._yScale); i <= origin.Y; i++)
				{
					double num3 = (double)(i - origin.Y) / this._yScale;
					int num4 = (int)Math.Min((double)this._radius * this._xScale, this._xScale * Math.Sqrt((double)num2 - num3 * num3));
					for (int j = origin.X - num4; j <= origin.X + num4; j++)
					{
						if (!base.UnitApply(action, origin, j, i, new object[0]) && this._quitOnFail)
						{
							return false;
						}
					}
				}
				for (int k = origin.Y + 1; k <= origin.Y + (int)(num * this._yScale * 0.5) - 1; k++)
				{
					double num5 = (double)(k - origin.Y) * (2.0 / this._yScale);
					int num6 = (int)Math.Min((double)this._radius * this._xScale, this._xScale * Math.Sqrt((double)num2 - num5 * num5));
					for (int l = origin.X - num6; l <= origin.X + num6; l++)
					{
						if (!base.UnitApply(action, origin, l, k, new object[0]) && this._quitOnFail)
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x0400580C RID: 22540
			private int _radius;

			// Token: 0x0400580D RID: 22541
			private double _xScale;

			// Token: 0x0400580E RID: 22542
			private double _yScale;
		}

		// Token: 0x02000535 RID: 1333
		public class Rectangle : GenShape
		{
			// Token: 0x060030A1 RID: 12449 RVA: 0x005E364E File Offset: 0x005E184E
			public Rectangle(Microsoft.Xna.Framework.Rectangle area)
			{
				this._area = area;
			}

			// Token: 0x060030A2 RID: 12450 RVA: 0x005E365D File Offset: 0x005E185D
			public Rectangle(int width, int height)
			{
				this._area = new Microsoft.Xna.Framework.Rectangle(0, 0, width, height);
			}

			// Token: 0x060030A3 RID: 12451 RVA: 0x005E3674 File Offset: 0x005E1874
			public void SetArea(Microsoft.Xna.Framework.Rectangle area)
			{
				this._area = area;
			}

			// Token: 0x060030A4 RID: 12452 RVA: 0x005E3680 File Offset: 0x005E1880
			public override bool Perform(Point origin, GenAction action)
			{
				for (int i = origin.X + this._area.Left; i < origin.X + this._area.Right; i++)
				{
					for (int j = origin.Y + this._area.Top; j < origin.Y + this._area.Bottom; j++)
					{
						if (!base.UnitApply(action, origin, i, j, new object[0]) && this._quitOnFail)
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x0400580F RID: 22543
			private Microsoft.Xna.Framework.Rectangle _area;
		}

		// Token: 0x02000536 RID: 1334
		public class Tail : GenShape
		{
			// Token: 0x060030A5 RID: 12453 RVA: 0x005E3706 File Offset: 0x005E1906
			public Tail(double width, Vector2D endOffset)
			{
				this._width = width * 16.0;
				this._endOffset = endOffset * 16.0;
			}

			// Token: 0x060030A6 RID: 12454 RVA: 0x005E3734 File Offset: 0x005E1934
			public override bool Perform(Point origin, GenAction action)
			{
				Vector2D vector2D = new Vector2D((double)(origin.X << 4), (double)(origin.Y << 4));
				return Utils.PlotTileTale(vector2D, vector2D + this._endOffset, this._width, (int x, int y) => this.UnitApply(action, origin, x, y, new object[0]) || !this._quitOnFail);
			}

			// Token: 0x04005810 RID: 22544
			private double _width;

			// Token: 0x04005811 RID: 22545
			private Vector2D _endOffset;
		}

		// Token: 0x02000537 RID: 1335
		public class Mound : GenShape
		{
			// Token: 0x060030A7 RID: 12455 RVA: 0x005E37A0 File Offset: 0x005E19A0
			public Mound(int halfWidth, int height)
			{
				this._halfWidth = halfWidth;
				this._height = height;
			}

			// Token: 0x060030A8 RID: 12456 RVA: 0x005E37B8 File Offset: 0x005E19B8
			public override bool Perform(Point origin, GenAction action)
			{
				int height = this._height;
				double num = (double)this._halfWidth;
				for (int i = -this._halfWidth; i <= this._halfWidth; i++)
				{
					int num2 = Math.Min(this._height, (int)(-((double)(this._height + 1) / (num * num)) * ((double)i + num) * ((double)i - num)));
					for (int j = 0; j < num2; j++)
					{
						if (!base.UnitApply(action, origin, i + origin.X, origin.Y - j, new object[0]) && this._quitOnFail)
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x04005812 RID: 22546
			private int _halfWidth;

			// Token: 0x04005813 RID: 22547
			private int _height;
		}
	}
}
