using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Intended for mods to use when spawning projectiles periodically from buffs. <br /><br />
	///
	/// Note that this can be used for both NPCs and Players
	/// </summary>
	// Token: 0x020006E2 RID: 1762
	public class EntitySource_Buff : EntitySource_Parent
	{
		/// <summary>
		/// The type of the buff (<see cref="T:Terraria.ID.BuffID" /> or <see cref="M:Terraria.ModLoader.ModContent.BuffType``1" />)
		/// </summary>
		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06004947 RID: 18759 RVA: 0x0064D73D File Offset: 0x0064B93D
		public int BuffId { get; }

		/// <summary>
		/// The index of the buff in the entity's <c>buffType</c> and <c>buffTime</c> arrays.
		/// </summary>
		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06004948 RID: 18760 RVA: 0x0064D745 File Offset: 0x0064B945
		public int BuffIndex { get; }

		// Token: 0x06004949 RID: 18761 RVA: 0x0064D74D File Offset: 0x0064B94D
		[NullableContext(1)]
		public EntitySource_Buff(Entity entity, int buffId, int buffIndex, [Nullable(2)] string context = null) : base(entity, context)
		{
			this.BuffId = buffId;
			this.BuffIndex = buffIndex;
		}
	}
}
