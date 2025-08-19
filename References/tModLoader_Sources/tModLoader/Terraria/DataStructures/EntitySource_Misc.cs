using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// To be used in cases where no entity is present. See <see cref="T:Terraria.ID.ItemSourceID" /> and <see cref="T:Terraria.ID.ProjectileSourceID" /> for vanilla values<para />
	/// <b>NOTE:</b> Unlike most other entity sources, this one requires <see cref="P:Terraria.DataStructures.EntitySource_Misc.Context" /> to be specified.
	/// </summary>
	// Token: 0x020006F0 RID: 1776
	[NullableContext(1)]
	[Nullable(0)]
	public class EntitySource_Misc : IEntitySource
	{
		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06004965 RID: 18789 RVA: 0x0064D894 File Offset: 0x0064BA94
		public string Context { get; }

		// Token: 0x06004966 RID: 18790 RVA: 0x0064D89C File Offset: 0x0064BA9C
		public EntitySource_Misc(string context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context", "The EntitySource_Misc type always expects a context string to be present.");
			}
			this.Context = context;
		}
	}
}
