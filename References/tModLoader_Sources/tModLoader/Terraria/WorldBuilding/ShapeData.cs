using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000080 RID: 128
	public class ShapeData
	{
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060013FA RID: 5114 RVA: 0x0049FBE9 File Offset: 0x0049DDE9
		public int Count
		{
			get
			{
				return this._points.Count;
			}
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x0049FBF6 File Offset: 0x0049DDF6
		public ShapeData()
		{
			this._points = new HashSet<Point16>();
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x0049FC09 File Offset: 0x0049DE09
		public ShapeData(ShapeData original)
		{
			this._points = new HashSet<Point16>(original._points);
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x0049FC22 File Offset: 0x0049DE22
		public void Add(int x, int y)
		{
			this._points.Add(new Point16(x, y));
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x0049FC37 File Offset: 0x0049DE37
		public void Remove(int x, int y)
		{
			this._points.Remove(new Point16(x, y));
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x0049FC4C File Offset: 0x0049DE4C
		public HashSet<Point16> GetData()
		{
			return this._points;
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x0049FC54 File Offset: 0x0049DE54
		public void Clear()
		{
			this._points.Clear();
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x0049FC61 File Offset: 0x0049DE61
		public bool Contains(int x, int y)
		{
			return this._points.Contains(new Point16(x, y));
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x0049FC78 File Offset: 0x0049DE78
		public void Add(ShapeData shapeData, Point localOrigin, Point remoteOrigin)
		{
			foreach (Point16 datum in shapeData.GetData())
			{
				this.Add(remoteOrigin.X - localOrigin.X + (int)datum.X, remoteOrigin.Y - localOrigin.Y + (int)datum.Y);
			}
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x0049FCF4 File Offset: 0x0049DEF4
		public void Subtract(ShapeData shapeData, Point localOrigin, Point remoteOrigin)
		{
			foreach (Point16 datum in shapeData.GetData())
			{
				this.Remove(remoteOrigin.X - localOrigin.X + (int)datum.X, remoteOrigin.Y - localOrigin.Y + (int)datum.Y);
			}
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x0049FD70 File Offset: 0x0049DF70
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

		// Token: 0x0400108C RID: 4236
		private HashSet<Point16> _points;
	}
}
