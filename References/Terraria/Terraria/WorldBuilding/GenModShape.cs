using System;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000066 RID: 102
	public abstract class GenModShape : GenShape
	{
		// Token: 0x06001141 RID: 4417 RVA: 0x0048CA0B File Offset: 0x0048AC0B
		public GenModShape(ShapeData data)
		{
			this._data = data;
		}

		// Token: 0x04000F0E RID: 3854
		protected ShapeData _data;
	}
}
