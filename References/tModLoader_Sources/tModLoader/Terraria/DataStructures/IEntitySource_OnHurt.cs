using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Used along with <see cref="T:Terraria.DataStructures.EntitySource_Parent" />. The <see cref="P:Terraria.DataStructures.IEntitySource_OnHurt.Victim" /> is also the <see cref="P:Terraria.DataStructures.EntitySource_Parent.Entity" /> (owner of the effect)
	/// </summary>
	// Token: 0x020006F8 RID: 1784
	[NullableContext(1)]
	public interface IEntitySource_OnHurt
	{
		/// <summary>
		/// The attacking entity. Note that this may be a <see cref="T:Terraria.Projectile" /> (possibly owned by a player), a <see cref="T:Terraria.Player" /> or even a <see cref="T:Terraria.NPC" />
		/// </summary>
		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06004973 RID: 18803
		[Nullable(2)]
		Entity Attacker { [NullableContext(2)] get; }

		/// <summary>
		/// The entity being attacked. Normally a Player, but could be an NPC if a mod decides to use this source in such a way
		/// </summary>
		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06004974 RID: 18804
		Entity Victim { get; }
	}
}
