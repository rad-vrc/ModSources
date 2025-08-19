using System;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x0200045F RID: 1119
	public struct Point16
	{
		// Token: 0x06002CD5 RID: 11477 RVA: 0x005BC1F2 File Offset: 0x005BA3F2
		public Point16(Point point)
		{
			this.X = (short)point.X;
			this.Y = (short)point.Y;
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x005BC20E File Offset: 0x005BA40E
		public Point16(int X, int Y)
		{
			this.X = (short)X;
			this.Y = (short)Y;
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x005BC220 File Offset: 0x005BA420
		public Point16(short X, short Y)
		{
			this.X = X;
			this.Y = Y;
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x005BC230 File Offset: 0x005BA430
		public static Point16 Max(int firstX, int firstY, int secondX, int secondY)
		{
			return new Point16((firstX > secondX) ? firstX : secondX, (firstY > secondY) ? firstY : secondY);
		}

		// Token: 0x06002CD9 RID: 11481 RVA: 0x005BC247 File Offset: 0x005BA447
		public Point16 Max(int compareX, int compareY)
		{
			return new Point16(((int)this.X > compareX) ? ((int)this.X) : compareX, ((int)this.Y > compareY) ? ((int)this.Y) : compareY);
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x005BC272 File Offset: 0x005BA472
		public Point16 Max(Point16 compareTo)
		{
			return new Point16((this.X > compareTo.X) ? this.X : compareTo.X, (this.Y > compareTo.Y) ? this.Y : compareTo.Y);
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x005BC2B1 File Offset: 0x005BA4B1
		public static bool operator ==(Point16 first, Point16 second)
		{
			return first.X == second.X && first.Y == second.Y;
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x005BC2D1 File Offset: 0x005BA4D1
		public static bool operator !=(Point16 first, Point16 second)
		{
			return first.X != second.X || first.Y != second.Y;
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x005BC2F4 File Offset: 0x005BA4F4
		public override bool Equals(object obj)
		{
			Point16 point = (Point16)obj;
			return this.X == point.X && this.Y == point.Y;
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x005BC327 File Offset: 0x005BA527
		public override int GetHashCode()
		{
			return (int)this.X << 16 | (int)((ushort)this.Y);
		}

		// Token: 0x06002CDF RID: 11487 RVA: 0x005BC33A File Offset: 0x005BA53A
		public override string ToString()
		{
			return string.Format("{{{0}, {1}}}", this.X, this.Y);
		}

		// Token: 0x0400511A RID: 20762
		public readonly short X;

		// Token: 0x0400511B RID: 20763
		public readonly short Y;

		// Token: 0x0400511C RID: 20764
		public static Point16 Zero = new Point16(0, 0);

		// Token: 0x0400511D RID: 20765
		public static Point16 NegativeOne = new Point16(-1, -1);
	}
}
