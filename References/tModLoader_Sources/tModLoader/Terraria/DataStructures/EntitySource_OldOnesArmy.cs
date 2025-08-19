using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	// Token: 0x020006F5 RID: 1781
	[NullableContext(2)]
	[Nullable(0)]
	public class EntitySource_OldOnesArmy : IEntitySource
	{
		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x0600496C RID: 18796 RVA: 0x0064D8F2 File Offset: 0x0064BAF2
		public string Context { get; }

		// Token: 0x0600496D RID: 18797 RVA: 0x0064D8FA File Offset: 0x0064BAFA
		public EntitySource_OldOnesArmy(string context = null)
		{
			this.Context = context;
		}
	}
}
