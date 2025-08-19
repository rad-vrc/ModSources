using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Used when NPCs or pets/minions give gifts or rewards to a player.
	/// </summary>
	// Token: 0x020006E9 RID: 1769
	public class EntitySource_Gift : EntitySource_Parent
	{
		// Token: 0x06004955 RID: 18773 RVA: 0x0064D7E7 File Offset: 0x0064B9E7
		[NullableContext(1)]
		public EntitySource_Gift(Entity entity, [Nullable(2)] string context = null) : base(entity, context)
		{
		}
	}
}
