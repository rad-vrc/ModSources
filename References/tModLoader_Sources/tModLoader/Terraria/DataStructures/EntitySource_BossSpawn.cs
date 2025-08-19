using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Used for most vanilla bosses, conveys their initial target, which is normally the closest player.
	/// </summary>
	// Token: 0x020006E1 RID: 1761
	[NullableContext(1)]
	[Nullable(0)]
	public class EntitySource_BossSpawn : IEntitySource
	{
		/// <summary>
		/// The entity which this boss spawn on. <br /><br />
		/// In the vast majority of cases, this will be a <see cref="T:Terraria.Player" />. Often <see cref="F:Terraria.NPC.target" /> will be set to <c>Player.whoAmI</c>
		/// </summary>
		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06004944 RID: 18756 RVA: 0x0064D717 File Offset: 0x0064B917
		public Entity Target { get; }

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06004945 RID: 18757 RVA: 0x0064D71F File Offset: 0x0064B91F
		[Nullable(2)]
		public string Context { [NullableContext(2)] get; }

		// Token: 0x06004946 RID: 18758 RVA: 0x0064D727 File Offset: 0x0064B927
		public EntitySource_BossSpawn(Entity target, [Nullable(2)] string context = null)
		{
			this.Target = target;
			this.Context = context;
		}
	}
}
