using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Use the interface, <see cref="T:Terraria.DataStructures.IEntitySource_OnHit" /> instead when checking entity sources in <c>OnSpawn</c> <br /><br />
	///
	/// Recommend setting <see cref="P:Terraria.DataStructures.IEntitySource.Context" /> to indicate the effect. Many vanilla set bonuses or accessories use this source.
	/// </summary>
	// Token: 0x020006F7 RID: 1783
	[NullableContext(1)]
	[Nullable(0)]
	public class EntitySource_OnHit : EntitySource_Parent, IEntitySource_OnHit
	{
		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06004970 RID: 18800 RVA: 0x0064D909 File Offset: 0x0064BB09
		public Entity Attacker
		{
			get
			{
				return base.Entity;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06004971 RID: 18801 RVA: 0x0064D911 File Offset: 0x0064BB11
		public Entity Victim { get; }

		// Token: 0x06004972 RID: 18802 RVA: 0x0064D919 File Offset: 0x0064BB19
		public EntitySource_OnHit(Entity attacker, Entity victim, [Nullable(2)] string context = null) : base(attacker, context)
		{
			this.Victim = victim;
		}
	}
}
