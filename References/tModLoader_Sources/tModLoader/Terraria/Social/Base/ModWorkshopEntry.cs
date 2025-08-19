using System;

namespace Terraria.Social.Base
{
	// Token: 0x020000FD RID: 253
	public class ModWorkshopEntry : AWorkshopEntry
	{
		// Token: 0x060018CD RID: 6349 RVA: 0x004BE3A0 File Offset: 0x004BC5A0
		public static string GetHeaderTextFor(ulong workshopEntryId, string[] tags, WorkshopItemPublicSettingId publicity, string previewImagePath)
		{
			return AWorkshopEntry.CreateHeaderJson("Mod", workshopEntryId, tags, publicity, previewImagePath);
		}
	}
}
