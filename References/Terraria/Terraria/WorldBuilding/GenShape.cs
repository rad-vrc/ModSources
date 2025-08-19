using System;
using Microsoft.Xna.Framework;

namespace Terraria.WorldBuilding
{
	// Token: 0x0200006F RID: 111
	public abstract class GenShape : GenBase
	{
		// Token: 0x06001150 RID: 4432
		public abstract bool Perform(Point origin, GenAction action);

		// Token: 0x06001151 RID: 4433 RVA: 0x0048CD50 File Offset: 0x0048AF50
		protected bool UnitApply(GenAction action, Point origin, int x, int y, params object[] args)
		{
			if (this._outputData != null)
			{
				this._outputData.Add(x - origin.X, y - origin.Y);
			}
			return action.Apply(origin, x, y, args);
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x0048CD82 File Offset: 0x0048AF82
		public GenShape Output(ShapeData outputData)
		{
			this._outputData = outputData;
			return this;
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x0048CD8C File Offset: 0x0048AF8C
		public GenShape QuitOnFail(bool value = true)
		{
			this._quitOnFail = value;
			return this;
		}

		// Token: 0x04000FAB RID: 4011
		private ShapeData _outputData;

		// Token: 0x04000FAC RID: 4012
		protected bool _quitOnFail;
	}
}
