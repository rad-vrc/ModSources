using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Used when attempting to add an item to the player's inventory, but it cannot fit so it spawns in the world instead. <br />
	/// Used in vanilla when a fished item can't fit in the player's inventory.
	/// </summary>
	// Token: 0x020006FB RID: 1787
	[NullableContext(1)]
	[Nullable(0)]
	public class EntitySource_OverfullInventory : IEntitySource
	{
		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x0600497A RID: 18810 RVA: 0x0064D966 File Offset: 0x0064BB66
		public Player Player { get; }

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x0600497B RID: 18811 RVA: 0x0064D96E File Offset: 0x0064BB6E
		[Nullable(2)]
		public string Context { [NullableContext(2)] get; }

		// Token: 0x0600497C RID: 18812 RVA: 0x0064D976 File Offset: 0x0064BB76
		public EntitySource_OverfullInventory(Player player, [Nullable(2)] string context = null)
		{
			this.Player = player;
			this.Context = context;
		}
	}
}
