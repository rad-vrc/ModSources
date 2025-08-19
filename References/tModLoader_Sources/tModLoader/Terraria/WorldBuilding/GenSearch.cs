using System;
using Microsoft.Xna.Framework;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000076 RID: 118
	public abstract class GenSearch : GenBase
	{
		// Token: 0x060013E9 RID: 5097 RVA: 0x0049F870 File Offset: 0x0049DA70
		public GenSearch Conditions(params GenCondition[] conditions)
		{
			this._conditions = conditions;
			return this;
		}

		// Token: 0x060013EA RID: 5098
		public abstract Point Find(Point origin);

		// Token: 0x060013EB RID: 5099 RVA: 0x0049F87C File Offset: 0x0049DA7C
		protected bool Check(int x, int y)
		{
			for (int i = 0; i < this._conditions.Length; i++)
			{
				if (this._requireAll ^ this._conditions[i].IsValid(x, y))
				{
					return !this._requireAll;
				}
			}
			return this._requireAll;
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x0049F8C4 File Offset: 0x0049DAC4
		public GenSearch RequireAll(bool mode)
		{
			this._requireAll = mode;
			return this;
		}

		// Token: 0x04000FEE RID: 4078
		public static Point NOT_FOUND = new Point(int.MaxValue, int.MaxValue);

		// Token: 0x04000FEF RID: 4079
		private bool _requireAll = true;

		// Token: 0x04000FF0 RID: 4080
		private GenCondition[] _conditions;
	}
}
