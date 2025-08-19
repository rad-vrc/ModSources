using System;
using System.Linq;

namespace Terraria.Social.Base
{
	// Token: 0x0200010E RID: 270
	public class WorkshopItemPublishSettings
	{
		// Token: 0x06001928 RID: 6440 RVA: 0x004BECE6 File Offset: 0x004BCEE6
		public string[] GetUsedTagsInternalNames()
		{
			return (from x in this.UsedTags
			select x.InternalNameForAPIs).ToArray<string>();
		}

		// Token: 0x040013AE RID: 5038
		public WorkshopTagOption[] UsedTags = new WorkshopTagOption[0];

		// Token: 0x040013AF RID: 5039
		public WorkshopItemPublicSettingId Publicity;

		// Token: 0x040013B0 RID: 5040
		public string PreviewImagePath;

		// Token: 0x040013B1 RID: 5041
		public string ChangeNotes;
	}
}
