using System;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000072 RID: 114
	public abstract class GenCondition : GenBase
	{
		// Token: 0x060013D2 RID: 5074 RVA: 0x0049F60C File Offset: 0x0049D80C
		public bool IsValid(int x, int y)
		{
			switch (this._areaType)
			{
			case GenCondition.AreaType.And:
				for (int i = x; i < x + this._width; i++)
				{
					for (int j = y; j < y + this._height; j++)
					{
						if (!this.CheckValidity(i, j))
						{
							return this.InvertResults;
						}
					}
				}
				return !this.InvertResults;
			case GenCondition.AreaType.Or:
				for (int k = x; k < x + this._width; k++)
				{
					for (int l = y; l < y + this._height; l++)
					{
						if (this.CheckValidity(k, l))
						{
							return !this.InvertResults;
						}
					}
				}
				return this.InvertResults;
			case GenCondition.AreaType.None:
				return this.CheckValidity(x, y) ^ this.InvertResults;
			default:
				return true;
			}
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x0049F6CF File Offset: 0x0049D8CF
		public GenCondition Not()
		{
			this.InvertResults = !this.InvertResults;
			return this;
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0049F6E1 File Offset: 0x0049D8E1
		public GenCondition AreaOr(int width, int height)
		{
			this._areaType = GenCondition.AreaType.Or;
			this._width = width;
			this._height = height;
			return this;
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x0049F6F9 File Offset: 0x0049D8F9
		public GenCondition AreaAnd(int width, int height)
		{
			this._areaType = GenCondition.AreaType.And;
			this._width = width;
			this._height = height;
			return this;
		}

		// Token: 0x060013D6 RID: 5078
		protected abstract bool CheckValidity(int x, int y);

		// Token: 0x04000FE1 RID: 4065
		private bool InvertResults;

		// Token: 0x04000FE2 RID: 4066
		private int _width;

		// Token: 0x04000FE3 RID: 4067
		private int _height;

		// Token: 0x04000FE4 RID: 4068
		private GenCondition.AreaType _areaType = GenCondition.AreaType.None;

		// Token: 0x0200082C RID: 2092
		private enum AreaType
		{
			// Token: 0x0400688D RID: 26765
			And,
			// Token: 0x0400688E RID: 26766
			Or,
			// Token: 0x0400688F RID: 26767
			None
		}
	}
}
