using System;
using Terraria;

namespace QoLCompendium.Core.Changes.BuffChanges
{
	// Token: 0x02000266 RID: 614
	internal readonly struct ItemInfo
	{
		// Token: 0x06000E4D RID: 3661 RVA: 0x000735E3 File Offset: 0x000717E3
		public ItemInfo(Item item)
		{
			this.type = item.type;
			this.buffType = item.buffType;
			this.stack = item.stack;
			this.createTile = item.createTile;
			this.placeStyle = item.placeStyle;
		}

		// Token: 0x040005BD RID: 1469
		public readonly int type;

		// Token: 0x040005BE RID: 1470
		public readonly int buffType;

		// Token: 0x040005BF RID: 1471
		public readonly int stack;

		// Token: 0x040005C0 RID: 1472
		public readonly int createTile;

		// Token: 0x040005C1 RID: 1473
		public readonly int placeStyle;
	}
}
