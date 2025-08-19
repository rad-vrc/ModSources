using System;
using Terraria.IO;

namespace Terraria.Social.Base
{
	// Token: 0x02000109 RID: 265
	public class TexturePackWorkshopEntry : AWorkshopEntry
	{
		// Token: 0x06001909 RID: 6409 RVA: 0x004BE8E1 File Offset: 0x004BCAE1
		public static string GetHeaderTextFor(ResourcePack resourcePack, ulong workshopEntryId, string[] tags, WorkshopItemPublicSettingId publicity, string previewImagePath)
		{
			return AWorkshopEntry.CreateHeaderJson("ResourcePack", workshopEntryId, tags, publicity, previewImagePath);
		}
	}
}
