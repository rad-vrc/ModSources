using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Used when dropping coins and items when a player dies. <br />
	/// Recommended for use by mods when spawning gore.
	/// </summary>
	// Token: 0x020006E4 RID: 1764
	public class EntitySource_Death : EntitySource_Parent
	{
		// Token: 0x0600494C RID: 18764 RVA: 0x0064D77F File Offset: 0x0064B97F
		[NullableContext(1)]
		public EntitySource_Death(Entity entity, [Nullable(2)] string context = null) : base(entity, context)
		{
		}
	}
}
