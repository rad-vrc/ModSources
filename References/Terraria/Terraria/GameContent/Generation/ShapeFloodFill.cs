using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation
{
	// Token: 0x020003E3 RID: 995
	public class ShapeFloodFill : GenShape
	{
		// Token: 0x06002ABF RID: 10943 RVA: 0x0059BC36 File Offset: 0x00599E36
		public ShapeFloodFill(int maximumActions = 100)
		{
			this._maximumActions = maximumActions;
		}

		// Token: 0x06002AC0 RID: 10944 RVA: 0x0059BC48 File Offset: 0x00599E48
		public override bool Perform(Point origin, GenAction action)
		{
			Queue<Point> queue = new Queue<Point>();
			HashSet<Point16> hashSet = new HashSet<Point16>();
			queue.Enqueue(origin);
			int num = this._maximumActions;
			while (queue.Count > 0)
			{
				if (num <= 0)
				{
					break;
				}
				Point point = queue.Dequeue();
				if (!hashSet.Contains(new Point16(point.X, point.Y)) && base.UnitApply(action, origin, point.X, point.Y, new object[0]))
				{
					hashSet.Add(new Point16(point));
					num--;
					if (point.X + 1 < Main.maxTilesX - 1)
					{
						queue.Enqueue(new Point(point.X + 1, point.Y));
					}
					if (point.X - 1 >= 1)
					{
						queue.Enqueue(new Point(point.X - 1, point.Y));
					}
					if (point.Y + 1 < Main.maxTilesY - 1)
					{
						queue.Enqueue(new Point(point.X, point.Y + 1));
					}
					if (point.Y - 1 >= 1)
					{
						queue.Enqueue(new Point(point.X, point.Y - 1));
					}
				}
			}
			while (queue.Count > 0)
			{
				Point point2 = queue.Dequeue();
				if (!hashSet.Contains(new Point16(point2.X, point2.Y)))
				{
					queue.Enqueue(point2);
					break;
				}
			}
			return queue.Count == 0;
		}

		// Token: 0x04004D5C RID: 19804
		private int _maximumActions;
	}
}
