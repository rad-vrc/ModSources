using System;
using System.Linq;

namespace Terraria.Social.Base
{
	// Token: 0x0200018B RID: 395
	public class WorkshopItemPublishSettings
	{
		// Token: 0x06001B17 RID: 6935 RVA: 0x004E6E98 File Offset: 0x004E5098
		public string[] GetUsedTagsInternalNames()
		{
			return (from x in this.UsedTags
			select x.InternalNameForAPIs).ToArray<string>();
		}

		// Token: 0x040015FC RID: 5628
		public WorkshopTagOption[] UsedTags = new WorkshopTagOption[0];

		// Token: 0x040015FD RID: 5629
		public WorkshopItemPublicSettingId Publicity;

		// Token: 0x040015FE RID: 5630
		public string PreviewImagePath;
	}
}
