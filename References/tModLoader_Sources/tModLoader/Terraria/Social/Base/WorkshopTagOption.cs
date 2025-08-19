using System;

namespace Terraria.Social.Base
{
	// Token: 0x02000110 RID: 272
	public class WorkshopTagOption
	{
		// Token: 0x06001941 RID: 6465 RVA: 0x004BED77 File Offset: 0x004BCF77
		public WorkshopTagOption(string nameKey, string internalName)
		{
			this.NameKey = nameKey;
			this.InternalNameForAPIs = internalName;
		}

		// Token: 0x040013B6 RID: 5046
		public readonly string NameKey;

		// Token: 0x040013B7 RID: 5047
		public readonly string InternalNameForAPIs;
	}
}
