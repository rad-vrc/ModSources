using System;
using Microsoft.Xna.Framework;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000077 RID: 119
	public abstract class GenShape : GenBase
	{
		// Token: 0x060013EF RID: 5103
		public abstract bool Perform(Point origin, GenAction action);

		// Token: 0x060013F0 RID: 5104 RVA: 0x0049F8F3 File Offset: 0x0049DAF3
		protected bool UnitApply(GenAction action, Point origin, int x, int y, params object[] args)
		{
			if (this._outputData != null)
			{
				this._outputData.Add(x - origin.X, y - origin.Y);
			}
			return action.Apply(origin, x, y, args);
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x0049F925 File Offset: 0x0049DB25
		public GenShape Output(ShapeData outputData)
		{
			this._outputData = outputData;
			return this;
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0049F92F File Offset: 0x0049DB2F
		public GenShape QuitOnFail(bool value = true)
		{
			this._quitOnFail = value;
			return this;
		}

		// Token: 0x04000FF1 RID: 4081
		private ShapeData _outputData;

		// Token: 0x04000FF2 RID: 4082
		protected bool _quitOnFail;
	}
}
