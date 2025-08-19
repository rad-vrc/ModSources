using System;
using System.Collections.Generic;
using Terraria.ObjectData;

namespace Terraria.Modules
{
	// Token: 0x02000056 RID: 86
	public class TileObjectAlternatesModule
	{
		// Token: 0x0600111C RID: 4380 RVA: 0x0048C29C File Offset: 0x0048A49C
		public TileObjectAlternatesModule(TileObjectAlternatesModule copyFrom = null)
		{
			if (copyFrom == null)
			{
				this.data = null;
				return;
			}
			if (copyFrom.data == null)
			{
				this.data = null;
				return;
			}
			this.data = new List<TileObjectData>(copyFrom.data.Count);
			for (int i = 0; i < copyFrom.data.Count; i++)
			{
				this.data.Add(new TileObjectData(copyFrom.data[i]));
			}
		}

		// Token: 0x04000EDD RID: 3805
		public List<TileObjectData> data;
	}
}
