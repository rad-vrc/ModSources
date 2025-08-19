using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Used for a effects from a mounted player
	/// </summary>
	// Token: 0x020006F1 RID: 1777
	public class EntitySource_Mount : EntitySource_Parent
	{
		/// <summary>
		/// A <see cref="T:Terraria.ID.MountID" /> or <see cref="M:Terraria.ModLoader.ModContent.MountType``1" />
		/// </summary>
		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06004967 RID: 18791 RVA: 0x0064D8BF File Offset: 0x0064BABF
		public int MountId { get; }

		// Token: 0x06004968 RID: 18792 RVA: 0x0064D8C7 File Offset: 0x0064BAC7
		[NullableContext(1)]
		public EntitySource_Mount(Player player, int mountId, [Nullable(2)] string context = null) : base(player, context)
		{
			this.MountId = mountId;
		}
	}
}
