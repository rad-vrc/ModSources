using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000421 RID: 1057
	public class EntitySource_ItemUse_WithAmmo : EntitySource_ItemUse
	{
		// Token: 0x06002B80 RID: 11136 RVA: 0x0059E75C File Offset: 0x0059C95C
		public EntitySource_ItemUse_WithAmmo(Entity entity, Item item, int ammoItemIdUsed) : base(entity, item)
		{
			this.AmmoItemIdUsed = ammoItemIdUsed;
		}

		// Token: 0x04004FB4 RID: 20404
		public readonly int AmmoItemIdUsed;
	}
}
