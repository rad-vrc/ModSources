using System;
using Terraria.IO;

namespace Terraria.Social.Base
{
	// Token: 0x02000111 RID: 273
	public class WorldWorkshopEntry : AWorkshopEntry
	{
		// Token: 0x06001942 RID: 6466 RVA: 0x004BED8D File Offset: 0x004BCF8D
		public static string GetHeaderTextFor(WorldFileData world, ulong workshopEntryId, string[] tags, WorkshopItemPublicSettingId publicity, string previewImagePath)
		{
			return AWorkshopEntry.CreateHeaderJson("World", workshopEntryId, tags, publicity, previewImagePath);
		}
	}
}
