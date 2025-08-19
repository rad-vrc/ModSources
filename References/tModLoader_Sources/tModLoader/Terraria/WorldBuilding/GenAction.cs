using System;
using Microsoft.Xna.Framework;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000070 RID: 112
	public abstract class GenAction : GenBase
	{
		// Token: 0x060013C7 RID: 5063
		public abstract bool Apply(Point origin, int x, int y, params object[] args);

		// Token: 0x060013C8 RID: 5064 RVA: 0x0049F579 File Offset: 0x0049D779
		protected bool UnitApply(Point origin, int x, int y, params object[] args)
		{
			if (this.OutputData != null)
			{
				this.OutputData.Add(x - origin.X, y - origin.Y);
			}
			return this.NextAction == null || this.NextAction.Apply(origin, x, y, args);
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x0049F5B8 File Offset: 0x0049D7B8
		public GenAction IgnoreFailures()
		{
			this._returnFalseOnFailure = false;
			return this;
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0049F5C2 File Offset: 0x0049D7C2
		protected bool Fail()
		{
			return !this._returnFalseOnFailure;
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x0049F5CD File Offset: 0x0049D7CD
		public GenAction Output(ShapeData data)
		{
			this.OutputData = data;
			return this;
		}

		// Token: 0x04000FDE RID: 4062
		public GenAction NextAction;

		// Token: 0x04000FDF RID: 4063
		public ShapeData OutputData;

		// Token: 0x04000FE0 RID: 4064
		private bool _returnFalseOnFailure = true;
	}
}
