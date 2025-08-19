using System;
using System.Collections.Generic;

namespace Terraria.Social.Base
{
	// Token: 0x020000F9 RID: 249
	public abstract class AWorkshopTagsCollection
	{
		// Token: 0x060018B3 RID: 6323 RVA: 0x004BE291 File Offset: 0x004BC491
		protected void AddWorldTag(string tagNameKey, string tagInternalName)
		{
			this.WorldTags.Add(new WorkshopTagOption(tagNameKey, tagInternalName));
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x004BE2A5 File Offset: 0x004BC4A5
		protected void AddResourcePackTag(string tagNameKey, string tagInternalName)
		{
			this.ResourcePackTags.Add(new WorkshopTagOption(tagNameKey, tagInternalName));
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x004BE2B9 File Offset: 0x004BC4B9
		protected void AddModTag(string tagNameKey, string tagInternalName)
		{
			this.ModTags.Add(new WorkshopTagOption(tagNameKey, tagInternalName));
		}

		// Token: 0x04001383 RID: 4995
		public readonly List<WorkshopTagOption> WorldTags = new List<WorkshopTagOption>();

		// Token: 0x04001384 RID: 4996
		public readonly List<WorkshopTagOption> ResourcePackTags = new List<WorkshopTagOption>();

		// Token: 0x04001385 RID: 4997
		[Obsolete("Use SteamedWraps.ModTags instead")]
		public readonly List<WorkshopTagOption> ModTags = new List<WorkshopTagOption>();
	}
}
