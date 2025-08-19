using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Used when spawning Town NPCs during world generation
	/// </summary>
	// Token: 0x02000707 RID: 1799
	[NullableContext(2)]
	[Nullable(0)]
	public class EntitySource_WorldGen : IEntitySource
	{
		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06004991 RID: 18833 RVA: 0x0064DA7B File Offset: 0x0064BC7B
		public string Context { get; }

		// Token: 0x06004992 RID: 18834 RVA: 0x0064DA83 File Offset: 0x0064BC83
		public EntitySource_WorldGen(string context = null)
		{
			this.Context = context;
		}
	}
}
