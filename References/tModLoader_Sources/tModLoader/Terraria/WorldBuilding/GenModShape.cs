using System;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000074 RID: 116
	public abstract class GenModShape : GenShape
	{
		// Token: 0x060013E1 RID: 5089 RVA: 0x0049F817 File Offset: 0x0049DA17
		public GenModShape(ShapeData data)
		{
			this._data = data;
		}

		// Token: 0x04000FEA RID: 4074
		protected ShapeData _data;
	}
}
