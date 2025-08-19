using System;
using Microsoft.Xna.Framework;

namespace Terraria.WorldBuilding
{
	// Token: 0x0200005F RID: 95
	public abstract class GenAction : GenBase
	{
		// Token: 0x06001125 RID: 4389
		public abstract bool Apply(Point origin, int x, int y, params object[] args);

		// Token: 0x06001126 RID: 4390 RVA: 0x0048C73E File Offset: 0x0048A93E
		protected bool UnitApply(Point origin, int x, int y, params object[] args)
		{
			if (this.OutputData != null)
			{
				this.OutputData.Add(x - origin.X, y - origin.Y);
			}
			return this.NextAction == null || this.NextAction.Apply(origin, x, y, args);
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0048C77D File Offset: 0x0048A97D
		public GenAction IgnoreFailures()
		{
			this._returnFalseOnFailure = false;
			return this;
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x0048C787 File Offset: 0x0048A987
		protected bool Fail()
		{
			return !this._returnFalseOnFailure;
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x0048C792 File Offset: 0x0048A992
		public GenAction Output(ShapeData data)
		{
			this.OutputData = data;
			return this;
		}

		// Token: 0x04000F02 RID: 3842
		public GenAction NextAction;

		// Token: 0x04000F03 RID: 3843
		public ShapeData OutputData;

		// Token: 0x04000F04 RID: 3844
		private bool _returnFalseOnFailure = true;
	}
}
