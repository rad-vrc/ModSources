using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Used when an <see cref="T:Terraria.Item" /> is spawned on the server as a result of <see cref="F:Terraria.ID.MessageID.SyncItemCannotBeTakenByEnemies" /> <br />
	/// Only used by vanilla for lucky coin. Note that the item is spawned client-side with <see cref="P:Terraria.DataStructures.IEntitySource.Context" /> = <see cref="F:Terraria.ID.ItemSourceID.LuckyCoin" />
	/// </summary>
	// Token: 0x02000700 RID: 1792
	[NullableContext(2)]
	[Nullable(0)]
	public class EntitySource_Sync : IEntitySource
	{
		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06004985 RID: 18821 RVA: 0x0064D9EB File Offset: 0x0064BBEB
		public string Context { get; }

		// Token: 0x06004986 RID: 18822 RVA: 0x0064D9F3 File Offset: 0x0064BBF3
		public EntitySource_Sync(string context = null)
		{
			this.Context = context;
		}
	}
}
