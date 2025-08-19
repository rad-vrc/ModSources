using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation
{
	// Token: 0x020003E2 RID: 994
	public class ShapeBranch : GenShape
	{
		// Token: 0x06002AB9 RID: 10937 RVA: 0x0059B8F9 File Offset: 0x00599AF9
		public ShapeBranch()
		{
			this._offset = new Point(10, -5);
		}

		// Token: 0x06002ABA RID: 10938 RVA: 0x0059B910 File Offset: 0x00599B10
		public ShapeBranch(Point offset)
		{
			this._offset = offset;
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x0059B91F File Offset: 0x00599B1F
		public ShapeBranch(double angle, double distance)
		{
			this._offset = new Point((int)(Math.Cos(angle) * distance), (int)(Math.Sin(angle) * distance));
		}

		// Token: 0x06002ABC RID: 10940 RVA: 0x0059B944 File Offset: 0x00599B44
		private bool PerformSegment(Point origin, GenAction action, Point start, Point end, int size)
		{
			size = Math.Max(1, size);
			Utils.TileActionAttempt <>9__0;
			for (int i = -(size >> 1); i < size - (size >> 1); i++)
			{
				for (int j = -(size >> 1); j < size - (size >> 1); j++)
				{
					Point p = new Point(start.X + i, start.Y + j);
					Utils.TileActionAttempt plot;
					if ((plot = <>9__0) == null)
					{
						plot = (<>9__0 = ((int tileX, int tileY) => this.UnitApply(action, origin, tileX, tileY, new object[0]) || !this._quitOnFail));
					}
					if (!Utils.PlotLine(p, end, plot, false))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002ABD RID: 10941 RVA: 0x0059B9E4 File Offset: 0x00599BE4
		public override bool Perform(Point origin, GenAction action)
		{
			Vector2D vector2D;
			vector2D..ctor((double)this._offset.X, (double)this._offset.Y);
			double num = vector2D.Length();
			int num2 = (int)(num / 6.0);
			if (this._endPoints != null)
			{
				this._endPoints.Add(new Point(origin.X + this._offset.X, origin.Y + this._offset.Y));
			}
			if (!this.PerformSegment(origin, action, origin, new Point(origin.X + this._offset.X, origin.Y + this._offset.Y), num2))
			{
				return false;
			}
			int num3 = (int)(num / 8.0);
			for (int i = 0; i < num3; i++)
			{
				double num4 = ((double)i + 1.0) / ((double)num3 + 1.0);
				Point point = new Point((int)(num4 * (double)this._offset.X), (int)(num4 * (double)this._offset.Y));
				Vector2D vector2D2;
				vector2D2..ctor((double)(this._offset.X - point.X), (double)(this._offset.Y - point.Y));
				vector2D2 = vector2D2.RotatedBy((GenBase._random.NextDouble() * 0.5 + 1.0) * (double)((GenBase._random.Next(2) == 0) ? -1 : 1), default(Vector2D)) * 0.75;
				Point point2 = new Point((int)vector2D2.X + point.X, (int)vector2D2.Y + point.Y);
				if (this._endPoints != null)
				{
					this._endPoints.Add(new Point(point2.X + origin.X, point2.Y + origin.Y));
				}
				if (!this.PerformSegment(origin, action, new Point(point.X + origin.X, point.Y + origin.Y), new Point(point2.X + origin.X, point2.Y + origin.Y), num2 - 1))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002ABE RID: 10942 RVA: 0x0059BC2C File Offset: 0x00599E2C
		public ShapeBranch OutputEndpoints(List<Point> endpoints)
		{
			this._endPoints = endpoints;
			return this;
		}

		// Token: 0x04004D5A RID: 19802
		private Point _offset;

		// Token: 0x04004D5B RID: 19803
		private List<Point> _endPoints;
	}
}
