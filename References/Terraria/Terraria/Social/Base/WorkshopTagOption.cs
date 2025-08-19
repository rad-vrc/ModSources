using System;

namespace Terraria.Social.Base
{
	// Token: 0x0200018D RID: 397
	public class WorkshopTagOption
	{
		// Token: 0x06001B2D RID: 6957 RVA: 0x004E6F21 File Offset: 0x004E5121
		public WorkshopTagOption(string nameKey, string internalName)
		{
			this.NameKey = nameKey;
			this.InternalNameForAPIs = internalName;
		}

		// Token: 0x04001603 RID: 5635
		public readonly string NameKey;

		// Token: 0x04001604 RID: 5636
		public readonly string InternalNameForAPIs;
	}
}
