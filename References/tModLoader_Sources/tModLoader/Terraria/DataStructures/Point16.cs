using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x0200072A RID: 1834
	public struct Point16
	{
		// Token: 0x06004A97 RID: 19095 RVA: 0x00667E80 File Offset: 0x00666080
		public Point16(Point point)
		{
			this.X = (short)point.X;
			this.Y = (short)point.Y;
		}

		// Token: 0x06004A98 RID: 19096 RVA: 0x00667E9C File Offset: 0x0066609C
		public Point16(int X, int Y)
		{
			this.X = (short)X;
			this.Y = (short)Y;
		}

		// Token: 0x06004A99 RID: 19097 RVA: 0x00667EAE File Offset: 0x006660AE
		public Point16(short X, short Y)
		{
			this.X = X;
			this.Y = Y;
		}

		// Token: 0x06004A9A RID: 19098 RVA: 0x00667EBE File Offset: 0x006660BE
		public static Point16 Max(int firstX, int firstY, int secondX, int secondY)
		{
			return new Point16((firstX > secondX) ? firstX : secondX, (firstY > secondY) ? firstY : secondY);
		}

		// Token: 0x06004A9B RID: 19099 RVA: 0x00667ED5 File Offset: 0x006660D5
		public Point16 Max(int compareX, int compareY)
		{
			return new Point16(((int)this.X > compareX) ? ((int)this.X) : compareX, ((int)this.Y > compareY) ? ((int)this.Y) : compareY);
		}

		// Token: 0x06004A9C RID: 19100 RVA: 0x00667F00 File Offset: 0x00666100
		public Point16 Max(Point16 compareTo)
		{
			return new Point16((this.X > compareTo.X) ? this.X : compareTo.X, (this.Y > compareTo.Y) ? this.Y : compareTo.Y);
		}

		// Token: 0x06004A9D RID: 19101 RVA: 0x00667F3F File Offset: 0x0066613F
		public static bool operator ==(Point16 first, Point16 second)
		{
			return first.X == second.X && first.Y == second.Y;
		}

		// Token: 0x06004A9E RID: 19102 RVA: 0x00667F5F File Offset: 0x0066615F
		public static bool operator !=(Point16 first, Point16 second)
		{
			return first.X != second.X || first.Y != second.Y;
		}

		// Token: 0x06004A9F RID: 19103 RVA: 0x00667F84 File Offset: 0x00666184
		public override bool Equals(object obj)
		{
			Point16 point = (Point16)obj;
			return this.X == point.X && this.Y == point.Y;
		}

		// Token: 0x06004AA0 RID: 19104 RVA: 0x00667FB7 File Offset: 0x006661B7
		public override int GetHashCode()
		{
			return (int)this.X << 16 | (int)((ushort)this.Y);
		}

		// Token: 0x06004AA1 RID: 19105 RVA: 0x00667FCC File Offset: 0x006661CC
		public override string ToString()
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 2);
			defaultInterpolatedStringHandler.AppendLiteral("{");
			defaultInterpolatedStringHandler.AppendFormatted<short>(this.X);
			defaultInterpolatedStringHandler.AppendLiteral(", ");
			defaultInterpolatedStringHandler.AppendFormatted<short>(this.Y);
			defaultInterpolatedStringHandler.AppendLiteral("}");
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06004AA2 RID: 19106 RVA: 0x00668027 File Offset: 0x00666227
		public Point16(int size)
		{
			this.X = (short)size;
			this.Y = (short)size;
		}

		// Token: 0x06004AA3 RID: 19107 RVA: 0x00668039 File Offset: 0x00666239
		public Point16(short size)
		{
			this.X = size;
			this.Y = size;
		}

		// Token: 0x06004AA4 RID: 19108 RVA: 0x00668049 File Offset: 0x00666249
		public static Point16 operator +(Point16 first, Point16 second)
		{
			return new Point16((int)(first.X + second.X), (int)(first.Y + second.Y));
		}

		// Token: 0x06004AA5 RID: 19109 RVA: 0x0066806A File Offset: 0x0066626A
		public static Point16 operator -(Point16 first, Point16 second)
		{
			return new Point16((int)(first.X - second.X), (int)(first.Y - second.Y));
		}

		// Token: 0x06004AA6 RID: 19110 RVA: 0x0066808B File Offset: 0x0066628B
		public static Point16 operator *(Point16 first, Point16 second)
		{
			return new Point16((int)(first.X * second.X), (int)(first.Y * second.Y));
		}

		// Token: 0x06004AA7 RID: 19111 RVA: 0x006680AC File Offset: 0x006662AC
		public static Point16 operator /(Point16 first, Point16 second)
		{
			return new Point16((int)(first.X / second.X), (int)(first.Y / second.Y));
		}

		// Token: 0x06004AA8 RID: 19112 RVA: 0x006680CD File Offset: 0x006662CD
		public static Point16 operator %(Point16 first, int num)
		{
			return new Point16((int)first.X % num, (int)first.Y % num);
		}

		// Token: 0x06004AA9 RID: 19113 RVA: 0x006680E4 File Offset: 0x006662E4
		public void Deconstruct(out short x, out short y)
		{
			x = this.X;
			y = this.Y;
		}

		// Token: 0x04005FED RID: 24557
		public readonly short X;

		// Token: 0x04005FEE RID: 24558
		public readonly short Y;

		// Token: 0x04005FEF RID: 24559
		public static Point16 Zero = new Point16(0, 0);

		// Token: 0x04005FF0 RID: 24560
		public static Point16 NegativeOne = new Point16(-1, -1);
	}
}
