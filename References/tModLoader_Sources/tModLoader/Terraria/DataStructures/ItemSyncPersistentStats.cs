using System;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x02000719 RID: 1817
	public struct ItemSyncPersistentStats
	{
		// Token: 0x060049DB RID: 18907 RVA: 0x0064E562 File Offset: 0x0064C762
		public void CopyFrom(Item item)
		{
			this.type = item.type;
			this.color = item.color;
		}

		// Token: 0x060049DC RID: 18908 RVA: 0x0064E57C File Offset: 0x0064C77C
		public void PasteInto(Item item)
		{
			if (this.type == item.type)
			{
				item.color = this.color;
			}
		}

		// Token: 0x04005F09 RID: 24329
		private Color color;

		// Token: 0x04005F0A RID: 24330
		private int type;
	}
}
