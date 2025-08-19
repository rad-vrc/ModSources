using System;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000081 RID: 129
	public static class Shapes
	{
		// Token: 0x0200084D RID: 2125
		public class Circle : GenShape
		{
			// Token: 0x060050FD RID: 20733 RVA: 0x00695782 File Offset: 0x00693982
			public Circle(int radius)
			{
				this._verticalRadius = radius;
				this._horizontalRadius = radius;
			}

			// Token: 0x060050FE RID: 20734 RVA: 0x00695798 File Offset: 0x00693998
			public Circle(int horizontalRadius, int verticalRadius)
			{
				this._horizontalRadius = horizontalRadius;
				this._verticalRadius = verticalRadius;
			}

			// Token: 0x060050FF RID: 20735 RVA: 0x006957AE File Offset: 0x006939AE
			public void SetRadius(int radius)
			{
				this._verticalRadius = radius;
				this._horizontalRadius = radius;
			}

			// Token: 0x06005100 RID: 20736 RVA: 0x006957C0 File Offset: 0x006939C0
			public override bool Perform(Point origin, GenAction action)
			{
				int num = (this._horizontalRadius + 1) * (this._horizontalRadius + 1);
				for (int i = origin.Y - this._verticalRadius; i <= origin.Y + this._verticalRadius; i++)
				{
					double num2 = (double)this._horizontalRadius / (double)this._verticalRadius * (double)(i - origin.Y);
					int num3 = Math.Min(this._horizontalRadius, (int)Math.Sqrt((double)num - num2 * num2));
					for (int j = origin.X - num3; j <= origin.X + num3; j++)
					{
						if (!base.UnitApply(action, origin, j, i, Array.Empty<object>()) && this._quitOnFail)
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x040068C1 RID: 26817
			private int _verticalRadius;

			// Token: 0x040068C2 RID: 26818
			private int _horizontalRadius;
		}

		// Token: 0x0200084E RID: 2126
		public class HalfCircle : GenShape
		{
			// Token: 0x06005101 RID: 20737 RVA: 0x00695870 File Offset: 0x00693A70
			public HalfCircle(int radius)
			{
				this._radius = radius;
			}

			// Token: 0x06005102 RID: 20738 RVA: 0x00695880 File Offset: 0x00693A80
			public override bool Perform(Point origin, GenAction action)
			{
				int num = (this._radius + 1) * (this._radius + 1);
				for (int i = origin.Y - this._radius; i <= origin.Y; i++)
				{
					int num2 = Math.Min(this._radius, (int)Math.Sqrt((double)(num - (i - origin.Y) * (i - origin.Y))));
					for (int j = origin.X - num2; j <= origin.X + num2; j++)
					{
						if (!base.UnitApply(action, origin, j, i, Array.Empty<object>()) && this._quitOnFail)
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x040068C3 RID: 26819
			private int _radius;
		}

		// Token: 0x0200084F RID: 2127
		public class Slime : GenShape
		{
			// Token: 0x06005103 RID: 20739 RVA: 0x00695918 File Offset: 0x00693B18
			public Slime(int radius)
			{
				this._radius = radius;
				this._xScale = 1.0;
				this._yScale = 1.0;
			}

			// Token: 0x06005104 RID: 20740 RVA: 0x00695945 File Offset: 0x00693B45
			public Slime(int radius, double xScale, double yScale)
			{
				this._radius = radius;
				this._xScale = xScale;
				this._yScale = yScale;
			}

			// Token: 0x06005105 RID: 20741 RVA: 0x00695964 File Offset: 0x00693B64
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
						if (!base.UnitApply(action, origin, j, i, Array.Empty<object>()) && this._quitOnFail)
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
						if (!base.UnitApply(action, origin, l, k, Array.Empty<object>()) && this._quitOnFail)
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x040068C4 RID: 26820
			private int _radius;

			// Token: 0x040068C5 RID: 26821
			private double _xScale;

			// Token: 0x040068C6 RID: 26822
			private double _yScale;
		}

		// Token: 0x02000850 RID: 2128
		public class Rectangle : GenShape
		{
			// Token: 0x06005106 RID: 20742 RVA: 0x00695AE0 File Offset: 0x00693CE0
			public Rectangle(Microsoft.Xna.Framework.Rectangle area)
			{
				this._area = area;
			}

			// Token: 0x06005107 RID: 20743 RVA: 0x00695AEF File Offset: 0x00693CEF
			public Rectangle(int width, int height)
			{
				this._area = new Microsoft.Xna.Framework.Rectangle(0, 0, width, height);
			}

			// Token: 0x06005108 RID: 20744 RVA: 0x00695B06 File Offset: 0x00693D06
			public void SetArea(Microsoft.Xna.Framework.Rectangle area)
			{
				this._area = area;
			}

			// Token: 0x06005109 RID: 20745 RVA: 0x00695B10 File Offset: 0x00693D10
			public override bool Perform(Point origin, GenAction action)
			{
				for (int i = origin.X + this._area.Left; i < origin.X + this._area.Right; i++)
				{
					for (int j = origin.Y + this._area.Top; j < origin.Y + this._area.Bottom; j++)
					{
						if (!base.UnitApply(action, origin, i, j, Array.Empty<object>()) && this._quitOnFail)
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x040068C7 RID: 26823
			private Microsoft.Xna.Framework.Rectangle _area;
		}

		// Token: 0x02000851 RID: 2129
		public class Tail : GenShape
		{
			// Token: 0x0600510A RID: 20746 RVA: 0x00695B95 File Offset: 0x00693D95
			public Tail(double width, Vector2D endOffset)
			{
				this._width = width * 16.0;
				this._endOffset = endOffset * 16.0;
			}

			// Token: 0x0600510B RID: 20747 RVA: 0x00695BC4 File Offset: 0x00693DC4
			public override bool Perform(Point origin, GenAction action)
			{
				Vector2D vector2D = new Vector2D((double)(origin.X << 4), (double)(origin.Y << 4));
				return Utils.PlotTileTale(vector2D, vector2D + this._endOffset, this._width, (int x, int y) => this.UnitApply(action, origin, x, y, Array.Empty<object>()) || !this._quitOnFail);
			}

			// Token: 0x040068C8 RID: 26824
			private double _width;

			// Token: 0x040068C9 RID: 26825
			private Vector2D _endOffset;
		}

		// Token: 0x02000852 RID: 2130
		public class Mound : GenShape
		{
			// Token: 0x0600510C RID: 20748 RVA: 0x00695C30 File Offset: 0x00693E30
			public Mound(int halfWidth, int height)
			{
				this._halfWidth = halfWidth;
				this._height = height;
			}

			// Token: 0x0600510D RID: 20749 RVA: 0x00695C48 File Offset: 0x00693E48
			public override bool Perform(Point origin, GenAction action)
			{
				int height = this._height;
				double num = (double)this._halfWidth;
				for (int i = -this._halfWidth; i <= this._halfWidth; i++)
				{
					int num2 = Math.Min(this._height, (int)((0.0 - (double)(this._height + 1) / (num * num)) * ((double)i + num) * ((double)i - num)));
					for (int j = 0; j < num2; j++)
					{
						if (!base.UnitApply(action, origin, i + origin.X, origin.Y - j, Array.Empty<object>()) && this._quitOnFail)
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x040068CA RID: 26826
			private int _halfWidth;

			// Token: 0x040068CB RID: 26827
			private int _height;
		}
	}
}
