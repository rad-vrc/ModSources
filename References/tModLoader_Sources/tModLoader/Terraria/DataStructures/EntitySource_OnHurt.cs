using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Use the interface, <see cref="T:Terraria.DataStructures.IEntitySource_OnHurt" /> instead when checking entity sources in <c>OnSpawn</c> <br /><br />
	///
	/// Recommend setting <see cref="P:Terraria.DataStructures.IEntitySource.Context" /> to indicate the effect.
	/// </summary>
	// Token: 0x020006F9 RID: 1785
	[NullableContext(2)]
	[Nullable(0)]
	public class EntitySource_OnHurt : EntitySource_Parent, IEntitySource_OnHurt
	{
		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06004975 RID: 18805 RVA: 0x0064D92A File Offset: 0x0064BB2A
		public Entity Attacker { get; }

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06004976 RID: 18806 RVA: 0x0064D932 File Offset: 0x0064BB32
		[Nullable(1)]
		public Entity Victim
		{
			[NullableContext(1)]
			get
			{
				return base.Entity;
			}
		}

		// Token: 0x06004977 RID: 18807 RVA: 0x0064D93A File Offset: 0x0064BB3A
		public EntitySource_OnHurt([Nullable(1)] Entity victim, Entity attacker, string context = null) : base(victim, context)
		{
			this.Attacker = attacker;
		}
	}
}
