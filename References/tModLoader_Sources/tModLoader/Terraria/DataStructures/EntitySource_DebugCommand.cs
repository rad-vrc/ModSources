using System;

namespace Terraria.DataStructures
{
	// Token: 0x020006E5 RID: 1765
	public class EntitySource_DebugCommand : IEntitySource
	{
		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x0600494D RID: 18765 RVA: 0x0064D789 File Offset: 0x0064B989
		public string Context { get; }

		// Token: 0x0600494E RID: 18766 RVA: 0x0064D791 File Offset: 0x0064B991
		public EntitySource_DebugCommand(string context)
		{
			this.Context = context;
		}
	}
}
