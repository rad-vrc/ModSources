using System;
using Microsoft.Xna.Framework;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation
{
	// Token: 0x020003E4 RID: 996
	public class ShapeRoot : GenShape
	{
		// Token: 0x06002AC1 RID: 10945 RVA: 0x0059BDB4 File Offset: 0x00599FB4
		public ShapeRoot(double angle, double distance = 10.0, double startingSize = 4.0, double endingSize = 1.0)
		{
			this._angle = angle;
			this._distance = distance;
			this._startingSize = startingSize;
			this._endingSize = endingSize;
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x0059BDDC File Offset: 0x00599FDC
		private bool DoRoot(Point origin, GenAction action, double angle, double distance, double startingSize)
		{
			double num = (double)origin.X;
			double num2 = (double)origin.Y;
			for (double num3 = 0.0; num3 < distance * 0.85; num3 += 1.0)
			{
				double num4 = num3 / distance;
				double num5 = Utils.Lerp(startingSize, this._endingSize, num4);
				num += Math.Cos(angle);
				num2 += Math.Sin(angle);
				angle += (double)GenBase._random.NextFloat() - 0.5 + (double)GenBase._random.NextFloat() * (this._angle - 1.5707963705062866) * 0.1 * (1.0 - num4);
				angle = angle * 0.4 + 0.45 * Utils.Clamp<double>(angle, this._angle - 2.0 * (1.0 - 0.5 * num4), this._angle + 2.0 * (1.0 - 0.5 * num4)) + Utils.Lerp(this._angle, 1.5707963705062866, num4) * 0.15;
				for (int i = 0; i < (int)num5; i++)
				{
					for (int j = 0; j < (int)num5; j++)
					{
						if (!base.UnitApply(action, origin, (int)num + i, (int)num2 + j, new object[0]) && this._quitOnFail)
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x0059BF6C File Offset: 0x0059A16C
		public override bool Perform(Point origin, GenAction action)
		{
			return this.DoRoot(origin, action, this._angle, this._distance, this._startingSize);
		}

		// Token: 0x04004D5D RID: 19805
		private double _angle;

		// Token: 0x04004D5E RID: 19806
		private double _startingSize;

		// Token: 0x04004D5F RID: 19807
		private double _endingSize;

		// Token: 0x04004D60 RID: 19808
		private double _distance;
	}
}
