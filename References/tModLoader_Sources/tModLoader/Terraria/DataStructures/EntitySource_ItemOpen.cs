using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Used when opening bags or gifts. Passed to <see cref="M:Terraria.Player.QuickSpawnItem(Terraria.DataStructures.IEntitySource,Terraria.Item,System.Int32)" />
	/// </summary>
	// Token: 0x020006EA RID: 1770
	[NullableContext(1)]
	[Nullable(0)]
	public class EntitySource_ItemOpen : IEntitySource
	{
		/// <summary>
		/// The player opening the item
		/// </summary>
		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06004956 RID: 18774 RVA: 0x0064D7F1 File Offset: 0x0064B9F1
		public Player Player { get; }

		/// <summary>
		/// The type of item being opened
		/// </summary>
		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06004957 RID: 18775 RVA: 0x0064D7F9 File Offset: 0x0064B9F9
		public int ItemType { get; }

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06004958 RID: 18776 RVA: 0x0064D801 File Offset: 0x0064BA01
		[Nullable(2)]
		public string Context { [NullableContext(2)] get; }

		// Token: 0x06004959 RID: 18777 RVA: 0x0064D809 File Offset: 0x0064BA09
		public EntitySource_ItemOpen(Player player, int itemType, [Nullable(2)] string context = null)
		{
			this.Player = player;
			this.ItemType = itemType;
			this.Context = context;
		}
	}
}
