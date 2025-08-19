using System;
using Terraria.IO;

namespace Terraria.Social.Base
{
	// Token: 0x02000187 RID: 391
	public class TexturePackWorkshopEntry : AWorkshopEntry
	{
		// Token: 0x06001B03 RID: 6915 RVA: 0x004E6C36 File Offset: 0x004E4E36
		public static string GetHeaderTextFor(ResourcePack resourcePack, ulong workshopEntryId, string[] tags, WorkshopItemPublicSettingId publicity, string previewImagePath)
		{
			return AWorkshopEntry.CreateHeaderJson("ResourcePack", workshopEntryId, tags, publicity, previewImagePath);
		}
	}
}
