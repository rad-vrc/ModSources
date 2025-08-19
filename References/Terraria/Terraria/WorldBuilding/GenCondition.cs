using System;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000062 RID: 98
	public abstract class GenCondition : GenBase
	{
		// Token: 0x06001132 RID: 4402 RVA: 0x0048C800 File Offset: 0x0048AA00
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

		// Token: 0x06001133 RID: 4403 RVA: 0x0048C8C3 File Offset: 0x0048AAC3
		public GenCondition Not()
		{
			this.InvertResults = !this.InvertResults;
			return this;
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x0048C8D5 File Offset: 0x0048AAD5
		public GenCondition AreaOr(int width, int height)
		{
			this._areaType = GenCondition.AreaType.Or;
			this._width = width;
			this._height = height;
			return this;
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x0048C8ED File Offset: 0x0048AAED
		public GenCondition AreaAnd(int width, int height)
		{
			this._areaType = GenCondition.AreaType.And;
			this._width = width;
			this._height = height;
			return this;
		}

		// Token: 0x06001136 RID: 4406
		protected abstract bool CheckValidity(int x, int y);

		// Token: 0x04000F05 RID: 3845
		private bool InvertResults;

		// Token: 0x04000F06 RID: 3846
		private int _width;

		// Token: 0x04000F07 RID: 3847
		private int _height;

		// Token: 0x04000F08 RID: 3848
		private GenCondition.AreaType _areaType = GenCondition.AreaType.None;

		// Token: 0x0200050B RID: 1291
		private enum AreaType
		{
			// Token: 0x040057D4 RID: 22484
			And,
			// Token: 0x040057D5 RID: 22485
			Or,
			// Token: 0x040057D6 RID: 22486
			None
		}
	}
}
