using System;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x0200040F RID: 1039
	public struct ItemSyncPersistentStats
	{
		// Token: 0x06002B33 RID: 11059 RVA: 0x0059DBCE File Offset: 0x0059BDCE
		public void CopyFrom(Item item)
		{
			this.type = item.type;
			this.color = item.color;
		}

		// Token: 0x06002B34 RID: 11060 RVA: 0x0059DBE8 File Offset: 0x0059BDE8
		public void PasteInto(Item item)
		{
			if (this.type != item.type)
			{
				return;
			}
			item.color = this.color;
		}

		// Token: 0x04004F5F RID: 20319
		private Color color;

		// Token: 0x04004F60 RID: 20320
		private int type;
	}
}
