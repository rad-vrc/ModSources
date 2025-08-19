using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation
{
	// Token: 0x02000624 RID: 1572
	public class ShapeBranch : GenShape
	{
		// Token: 0x060044EC RID: 17644 RVA: 0x0060D0C9 File Offset: 0x0060B2C9
		public ShapeBranch()
		{
			this._offset = new Point(10, -5);
		}

		// Token: 0x060044ED RID: 17645 RVA: 0x0060D0E0 File Offset: 0x0060B2E0
		public ShapeBranch(Point offset)
		{
			this._offset = offset;
		}

		// Token: 0x060044EE RID: 17646 RVA: 0x0060D0EF File Offset: 0x0060B2EF
		public ShapeBranch(double angle, double distance)
		{
			this._offset = new Point((int)(Math.Cos(angle) * distance), (int)(Math.Sin(angle) * distance));
		}

		// Token: 0x060044EF RID: 17647 RVA: 0x0060D114 File Offset: 0x0060B314
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
						plot = (<>9__0 = ((int tileX, int tileY) => this.UnitApply(action, origin, tileX, tileY, Array.Empty<object>()) || !this._quitOnFail));
					}
					if (!Utils.PlotLine(p, end, plot, false))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060044F0 RID: 17648 RVA: 0x0060D1B4 File Offset: 0x0060B3B4
		public override bool Perform(Point origin, GenAction action)
		{
			double num = new Vector2D((double)this._offset.X, (double)this._offset.Y).Length();
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
				Point point;
				point..ctor((int)(num4 * (double)this._offset.X), (int)(num4 * (double)this._offset.Y));
				Vector2D spinningpoint;
				spinningpoint..ctor((double)(this._offset.X - point.X), (double)(this._offset.Y - point.Y));
				spinningpoint = spinningpoint.RotatedBy((GenBase._random.NextDouble() * 0.5 + 1.0) * (double)((GenBase._random.Next(2) != 0) ? 1 : -1), default(Vector2D)) * 0.75;
				Point point2;
				point2..ctor((int)spinningpoint.X + point.X, (int)spinningpoint.Y + point.Y);
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

		// Token: 0x060044F1 RID: 17649 RVA: 0x0060D3FA File Offset: 0x0060B5FA
		public ShapeBranch OutputEndpoints(List<Point> endpoints)
		{
			this._endPoints = endpoints;
			return this;
		}

		// Token: 0x04005AB8 RID: 23224
		private Point _offset;

		// Token: 0x04005AB9 RID: 23225
		private List<Point> _endPoints;
	}
}
