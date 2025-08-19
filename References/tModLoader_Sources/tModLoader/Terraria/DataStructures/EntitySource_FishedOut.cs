using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Used when an NPC spawns as a result of fishing.
	/// </summary>
	// Token: 0x020006E8 RID: 1768
	[NullableContext(1)]
	[Nullable(0)]
	public class EntitySource_FishedOut : IEntitySource
	{
		/// <summary>
		/// The entity which was fishing. Normally a <see cref="T:Terraria.Player" />
		/// </summary>
		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06004952 RID: 18770 RVA: 0x0064D7C1 File Offset: 0x0064B9C1
		public Entity Fisher { get; }

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06004953 RID: 18771 RVA: 0x0064D7C9 File Offset: 0x0064B9C9
		[Nullable(2)]
		public string Context { [NullableContext(2)] get; }

		// Token: 0x06004954 RID: 18772 RVA: 0x0064D7D1 File Offset: 0x0064B9D1
		public EntitySource_FishedOut(Entity fisher, [Nullable(2)] string context = null)
		{
			this.Fisher = fisher;
			this.Context = context;
		}
	}
}
