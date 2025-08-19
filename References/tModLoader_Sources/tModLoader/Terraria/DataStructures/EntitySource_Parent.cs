using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Use when the parent can be considered the 'owner' or 'responsible for' the effect. <br />
	/// This is the <b>most important</b> entity source. If in doubt, use it.
	/// </summary>
	// Token: 0x020006FC RID: 1788
	[NullableContext(1)]
	[Nullable(0)]
	public class EntitySource_Parent : IEntitySource
	{
		/// <summary>
		/// The entity which is the source of the effect or action.
		/// In many cases, it makes sense to consider buffs or effects applied to the parent entity, and make changes to the spawned entity.
		/// </summary>
		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x0600497D RID: 18813 RVA: 0x0064D98C File Offset: 0x0064BB8C
		public Entity Entity { get; }

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x0600497E RID: 18814 RVA: 0x0064D994 File Offset: 0x0064BB94
		[Nullable(2)]
		public string Context { [NullableContext(2)] get; }

		// Token: 0x0600497F RID: 18815 RVA: 0x0064D99C File Offset: 0x0064BB9C
		public EntitySource_Parent(Entity entity, [Nullable(2)] string context = null)
		{
			this.Entity = entity;
			this.Context = context;
		}
	}
}
