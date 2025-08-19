using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Used when an NPC is respawned from the <see cref="T:Terraria.GameContent.CoinLossRevengeSystem" />
	/// </summary>
	// Token: 0x020006FD RID: 1789
	[NullableContext(2)]
	[Nullable(0)]
	public class EntitySource_RevengeSystem : IEntitySource
	{
		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06004980 RID: 18816 RVA: 0x0064D9B2 File Offset: 0x0064BBB2
		public string Context { get; }

		// Token: 0x06004981 RID: 18817 RVA: 0x0064D9BA File Offset: 0x0064BBBA
		public EntitySource_RevengeSystem(string context = null)
		{
			this.Context = context;
		}
	}
}
