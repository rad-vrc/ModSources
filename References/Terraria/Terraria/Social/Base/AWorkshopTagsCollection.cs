using System;
using System.Collections.Generic;

namespace Terraria.Social.Base
{
	// Token: 0x0200018E RID: 398
	public abstract class AWorkshopTagsCollection
	{
		// Token: 0x06001B2E RID: 6958 RVA: 0x004E6F37 File Offset: 0x004E5137
		protected void AddWorldTag(string tagNameKey, string tagInternalName)
		{
			this.WorldTags.Add(new WorkshopTagOption(tagNameKey, tagInternalName));
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x004E6F4B File Offset: 0x004E514B
		protected void AddResourcePackTag(string tagNameKey, string tagInternalName)
		{
			this.ResourcePackTags.Add(new WorkshopTagOption(tagNameKey, tagInternalName));
		}

		// Token: 0x04001605 RID: 5637
		public readonly List<WorkshopTagOption> WorldTags = new List<WorkshopTagOption>();

		// Token: 0x04001606 RID: 5638
		public readonly List<WorkshopTagOption> ResourcePackTags = new List<WorkshopTagOption>();
	}
}
