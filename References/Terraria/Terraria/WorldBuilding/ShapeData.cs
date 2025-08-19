using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000072 RID: 114
	public class ShapeData
	{
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x0048CDA6 File Offset: 0x0048AFA6
		public int Count
		{
			get
			{
				return this._points.Count;
			}
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x0048CDB3 File Offset: 0x0048AFB3
		public ShapeData()
		{
			this._points = new HashSet<Point16>();
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x0048CDC6 File Offset: 0x0048AFC6
		public ShapeData(ShapeData original)
		{
			this._points = new HashSet<Point16>(original._points);
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x0048CDDF File Offset: 0x0048AFDF
		public void Add(int x, int y)
		{
			this._points.Add(new Point16(x, y));
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x0048CDF4 File Offset: 0x0048AFF4
		public void Remove(int x, int y)
		{
			this._points.Remove(new Point16(x, y));
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x0048CE09 File Offset: 0x0048B009
		public HashSet<Point16> GetData()
		{
			return this._points;
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x0048CE11 File Offset: 0x0048B011
		public void Clear()
		{
			this._points.Clear();
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x0048CE1E File Offset: 0x0048B01E
		public bool Contains(int x, int y)
		{
			return this._points.Contains(new Point16(x, y));
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x0048CE34 File Offset: 0x0048B034
		public void Add(ShapeData shapeData, Point localOrigin, Point remoteOrigin)
		{
			foreach (Point16 point in shapeData.GetData())
			{
				this.Add(remoteOrigin.X - localOrigin.X + (int)point.X, remoteOrigin.Y - localOrigin.Y + (int)point.Y);
			}
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x0048CEB0 File Offset: 0x0048B0B0
		public void Subtract(ShapeData shapeData, Point localOrigin, Point remoteOrigin)
		{
			foreach (Point16 point in shapeData.GetData())
			{
				this.Remove(remoteOrigin.X - localOrigin.X + (int)point.X, remoteOrigin.Y - localOrigin.Y + (int)point.Y);
			}
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x0048CF2C File Offset: 0x0048B12C
		public static Rectangle GetBounds(Point origin, params ShapeData[] shapes)
		{
			int num = (int)shapes[0]._points.First<Point16>().X;
			int num2 = num;
			int num3 = (int)shapes[0]._points.First<Point16>().Y;
			int num4 = num3;
			for (int i = 0; i < shapes.Length; i++)
			{
				foreach (Point16 point in shapes[i]._points)
				{
					num = Math.Max(num, (int)point.X);
					num2 = Math.Min(num2, (int)point.X);
					num3 = Math.Max(num3, (int)point.Y);
					num4 = Math.Min(num4, (int)point.Y);
				}
			}
			return new Rectangle(num2 + origin.X, num4 + origin.Y, num - num2, num3 - num4);
		}

		// Token: 0x04000FAF RID: 4015
		private HashSet<Point16> _points;
	}
}
