using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Used along with <see cref="T:Terraria.DataStructures.EntitySource_Parent" /> to indicate that stats from the item should be transferred to spawned entities. <br />
	/// When used to spawn projectiles, a snapshot of the Player and Item stats will be stored on the projectile. See <see cref="M:Terraria.Projectile.ApplyStatsFromSource(Terraria.DataStructures.IEntitySource)" /> for implementation
	/// </summary>
	// Token: 0x020006EB RID: 1771
	[NullableContext(1)]
	public interface IEntitySource_WithStatsFromItem
	{
		/// <summary>
		/// The Player using the Item. Equal to <see cref="P:Terraria.DataStructures.EntitySource_Parent.Entity" />
		/// </summary>
		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x0600495A RID: 18778
		Player Player { get; }

		/// <summary>
		/// The item being used
		/// </summary>
		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x0600495B RID: 18779
		Item Item { get; }
	}
}
