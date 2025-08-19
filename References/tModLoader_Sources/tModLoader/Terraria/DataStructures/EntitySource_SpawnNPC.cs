using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Used when an NPC is spawned from <see cref="M:Terraria.NPC.SpawnNPC" /> as part of natural biome/event based spawning. <br /><br />
	/// Note that some bosses incorrectly use this source to spawn minions, and remix world pots use it to spawn slimes. (to be fixed in 1.4.5)
	/// </summary>
	// Token: 0x020006FF RID: 1791
	[NullableContext(2)]
	[Nullable(0)]
	public class EntitySource_SpawnNPC : IEntitySource
	{
		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06004983 RID: 18819 RVA: 0x0064D9D4 File Offset: 0x0064BBD4
		public string Context { get; }

		// Token: 0x06004984 RID: 18820 RVA: 0x0064D9DC File Offset: 0x0064BBDC
		public EntitySource_SpawnNPC(string context = null)
		{
			this.Context = context;
		}
	}
}
