using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Use the interface, <see cref="T:Terraria.DataStructures.IEntitySource_WithStatsFromItem" /> instead when checking entity sources in <c>OnSpawn</c> <br /><br />
	///
	/// Used when a player uses an item or an accessory.
	/// </summary>
	// Token: 0x020006EC RID: 1772
	[NullableContext(1)]
	[Nullable(0)]
	public class EntitySource_ItemUse : EntitySource_Parent, IEntitySource_WithStatsFromItem
	{
		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x0600495C RID: 18780 RVA: 0x0064D826 File Offset: 0x0064BA26
		public Player Player
		{
			get
			{
				return (Player)base.Entity;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x0600495D RID: 18781 RVA: 0x0064D833 File Offset: 0x0064BA33
		public Item Item { get; }

		// Token: 0x0600495E RID: 18782 RVA: 0x0064D83B File Offset: 0x0064BA3B
		public EntitySource_ItemUse(Player player, Item item, [Nullable(2)] string context = null) : base(player, context)
		{
			this.Item = item;
		}
	}
}
