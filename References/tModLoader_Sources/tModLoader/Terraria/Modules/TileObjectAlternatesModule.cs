using System;
using System.Collections.Generic;
using Terraria.ObjectData;

namespace Terraria.Modules
{
	// Token: 0x02000131 RID: 305
	public class TileObjectAlternatesModule
	{
		// Token: 0x06001A7D RID: 6781 RVA: 0x004CBAFC File Offset: 0x004C9CFC
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

		// Token: 0x04001440 RID: 5184
		public List<TileObjectData> data;
	}
}
