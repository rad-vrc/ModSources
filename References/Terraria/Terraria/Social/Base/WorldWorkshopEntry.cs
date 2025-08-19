using System;
using Terraria.IO;

namespace Terraria.Social.Base
{
	// Token: 0x02000186 RID: 390
	public class WorldWorkshopEntry : AWorkshopEntry
	{
		// Token: 0x06001B01 RID: 6913 RVA: 0x004E6C1D File Offset: 0x004E4E1D
		public static string GetHeaderTextFor(WorldFileData world, ulong workshopEntryId, string[] tags, WorkshopItemPublicSettingId publicity, string previewImagePath)
		{
			return AWorkshopEntry.CreateHeaderJson("World", workshopEntryId, tags, publicity, previewImagePath);
		}
	}
}
