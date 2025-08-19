using System;
using Microsoft.Xna.Framework;

namespace Terraria.WorldBuilding
{
	// Token: 0x0200006A RID: 106
	public abstract class GenSearch : GenBase
	{
		// Token: 0x06001145 RID: 4421 RVA: 0x0048CA3A File Offset: 0x0048AC3A
		public GenSearch Conditions(params GenCondition[] conditions)
		{
			this._conditions = conditions;
			return this;
		}

		// Token: 0x06001146 RID: 4422
		public abstract Point Find(Point origin);

		// Token: 0x06001147 RID: 4423 RVA: 0x0048CA44 File Offset: 0x0048AC44
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

		// Token: 0x06001148 RID: 4424 RVA: 0x0048CA8C File Offset: 0x0048AC8C
		public GenSearch RequireAll(bool mode)
		{
			this._requireAll = mode;
			return this;
		}

		// Token: 0x04000F11 RID: 3857
		public static Point NOT_FOUND = new Point(int.MaxValue, int.MaxValue);

		// Token: 0x04000F12 RID: 3858
		private bool _requireAll = true;

		// Token: 0x04000F13 RID: 3859
		private GenCondition[] _conditions;
	}
}
