using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Inconsistently used by vanilla when spawning more exotic things like the lunatic pillars, cultist ritual, fairies etc. <br />
	/// Don't expect this to distinguish between NPCs spawned due to an event/invasion. Most of those use <see cref="T:Terraria.DataStructures.EntitySource_SpawnNPC" />
	/// </summary>
	// Token: 0x02000706 RID: 1798
	[NullableContext(2)]
	[Nullable(0)]
	public class EntitySource_WorldEvent : IEntitySource
	{
		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x0600498F RID: 18831 RVA: 0x0064DA64 File Offset: 0x0064BC64
		public string Context { get; }

		// Token: 0x06004990 RID: 18832 RVA: 0x0064DA6C File Offset: 0x0064BC6C
		public EntitySource_WorldEvent(string context = null)
		{
			this.Context = context;
		}
	}
}
