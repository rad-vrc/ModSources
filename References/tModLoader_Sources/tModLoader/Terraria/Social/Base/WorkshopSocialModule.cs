using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Terraria.IO;
using Terraria.ModLoader.Core;

namespace Terraria.Social.Base
{
	// Token: 0x0200010F RID: 271
	public abstract class WorkshopSocialModule : ISocialModule
	{
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x0600192A RID: 6442 RVA: 0x004BED2B File Offset: 0x004BCF2B
		// (set) Token: 0x0600192B RID: 6443 RVA: 0x004BED33 File Offset: 0x004BCF33
		public WorkshopBranding Branding { get; protected set; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x0600192C RID: 6444 RVA: 0x004BED3C File Offset: 0x004BCF3C
		// (set) Token: 0x0600192D RID: 6445 RVA: 0x004BED44 File Offset: 0x004BCF44
		public AWorkshopProgressReporter ProgressReporter { get; protected set; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600192E RID: 6446 RVA: 0x004BED4D File Offset: 0x004BCF4D
		// (set) Token: 0x0600192F RID: 6447 RVA: 0x004BED55 File Offset: 0x004BCF55
		public AWorkshopTagsCollection SupportedTags { get; protected set; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06001930 RID: 6448 RVA: 0x004BED5E File Offset: 0x004BCF5E
		// (set) Token: 0x06001931 RID: 6449 RVA: 0x004BED66 File Offset: 0x004BCF66
		public WorkshopIssueReporter IssueReporter { get; protected set; }

		// Token: 0x06001932 RID: 6450
		public abstract void Initialize();

		// Token: 0x06001933 RID: 6451
		public abstract void Shutdown();

		// Token: 0x06001934 RID: 6452
		public abstract void PublishWorld(WorldFileData world, WorkshopItemPublishSettings settings);

		// Token: 0x06001935 RID: 6453
		public abstract void PublishResourcePack(ResourcePack resourcePack, WorkshopItemPublishSettings settings);

		// Token: 0x06001936 RID: 6454
		public abstract bool TryGetInfoForWorld(WorldFileData world, out FoundWorkshopEntryInfo info);

		// Token: 0x06001937 RID: 6455
		public abstract bool TryGetInfoForResourcePack(ResourcePack resourcePack, out FoundWorkshopEntryInfo info);

		// Token: 0x06001938 RID: 6456
		public abstract void LoadEarlyContent();

		// Token: 0x06001939 RID: 6457
		public abstract List<string> GetListOfSubscribedResourcePackPaths();

		// Token: 0x0600193A RID: 6458
		public abstract List<string> GetListOfSubscribedWorldPaths();

		// Token: 0x0600193B RID: 6459
		public abstract bool TryGetPath(string pathEnd, out string fullPathFound);

		// Token: 0x0600193C RID: 6460
		public abstract void ImportDownloadedWorldToLocalSaves(WorldFileData world, string newFileName = null, string newDisplayName = null);

		// Token: 0x0600193D RID: 6461
		public abstract bool PublishMod(TmodFile modFile, NameValueCollection data, WorkshopItemPublishSettings settings);

		// Token: 0x0600193E RID: 6462
		public abstract bool TryGetInfoForMod(TmodFile modFile, out FoundWorkshopEntryInfo info);

		// Token: 0x0600193F RID: 6463
		public abstract List<string> GetListOfMods();
	}
}
