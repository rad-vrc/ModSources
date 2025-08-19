using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Used when an NPC or Projectile is killed and drops loot. <br />
	/// Vanilla projectile examples are geodes and life crystal boulders
	/// </summary>
	// Token: 0x020006EF RID: 1775
	public class EntitySource_Loot : EntitySource_Parent
	{
		// Token: 0x06004964 RID: 18788 RVA: 0x0064D88A File Offset: 0x0064BA8A
		[NullableContext(1)]
		public EntitySource_Loot(Entity entity, [Nullable(2)] string context = null) : base(entity, context)
		{
		}
	}
}
