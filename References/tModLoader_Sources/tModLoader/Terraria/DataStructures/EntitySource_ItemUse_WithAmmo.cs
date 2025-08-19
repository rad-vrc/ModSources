using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	// Token: 0x020006EE RID: 1774
	public class EntitySource_ItemUse_WithAmmo : EntitySource_ItemUse
	{
		/// <summary>
		/// A <see cref="T:Terraria.ID.ItemID" /> or <see cref="M:Terraria.ModLoader.ModContent.ItemType``1" />
		/// </summary>
		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06004962 RID: 18786 RVA: 0x0064D86F File Offset: 0x0064BA6F
		public int AmmoItemIdUsed { get; }

		// Token: 0x06004963 RID: 18787 RVA: 0x0064D877 File Offset: 0x0064BA77
		[NullableContext(1)]
		public EntitySource_ItemUse_WithAmmo(Player player, Item item, int ammoItemId, [Nullable(2)] string context = null) : base(player, item, context)
		{
			this.AmmoItemIdUsed = ammoItemId;
		}
	}
}
