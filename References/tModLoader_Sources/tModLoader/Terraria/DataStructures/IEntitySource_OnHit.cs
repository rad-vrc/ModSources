using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Used along with <see cref="T:Terraria.DataStructures.EntitySource_Parent" />. The <see cref="P:Terraria.DataStructures.IEntitySource_OnHit.Attacker" /> is also the <see cref="P:Terraria.DataStructures.EntitySource_Parent.Entity" /> (owner of the effect)
	/// </summary>
	// Token: 0x020006F6 RID: 1782
	[NullableContext(1)]
	public interface IEntitySource_OnHit
	{
		/// <summary>
		/// The attacking entity. Note that this may be a <see cref="T:Terraria.Projectile" /> (possibly owned by a player), a <see cref="T:Terraria.Player" /> or even a <see cref="T:Terraria.NPC" />
		/// </summary>
		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x0600496E RID: 18798
		Entity Attacker { get; }

		/// <summary>
		/// The entity being attacked. Normally an NPC, but could be an Player if a mod decides to use this source in such a way
		/// </summary>
		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x0600496F RID: 18799
		Entity Victim { get; }
	}
}
