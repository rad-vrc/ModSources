using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	// Token: 0x020006E7 RID: 1767
	[NullableContext(2)]
	[Nullable(0)]
	public class EntitySource_Film : IEntitySource
	{
		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06004950 RID: 18768 RVA: 0x0064D7AA File Offset: 0x0064B9AA
		public string Context { get; }

		// Token: 0x06004951 RID: 18769 RVA: 0x0064D7B2 File Offset: 0x0064B9B2
		public EntitySource_Film(string context = null)
		{
			this.Context = context;
		}
	}
}
