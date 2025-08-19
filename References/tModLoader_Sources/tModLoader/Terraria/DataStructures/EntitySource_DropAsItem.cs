using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Used when projectiles convert themselves to items in the world as a result of hitting a block.
	/// </summary>
	// Token: 0x020006E6 RID: 1766
	public class EntitySource_DropAsItem : EntitySource_Parent
	{
		// Token: 0x0600494F RID: 18767 RVA: 0x0064D7A0 File Offset: 0x0064B9A0
		[NullableContext(1)]
		public EntitySource_DropAsItem(Entity entity, [Nullable(2)] string context = null) : base(entity, context)
		{
		}
	}
}
