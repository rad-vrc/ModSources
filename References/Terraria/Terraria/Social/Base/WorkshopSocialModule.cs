using System;
using System.Collections.Generic;
using Terraria.IO;

namespace Terraria.Social.Base
{
	// Token: 0x0200018C RID: 396
	public abstract class WorkshopSocialModule : ISocialModule
	{
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06001B19 RID: 6937 RVA: 0x004E6EDD File Offset: 0x004E50DD
		// (set) Token: 0x06001B1A RID: 6938 RVA: 0x004E6EE5 File Offset: 0x004E50E5
		public WorkshopBranding Branding { get; protected set; }

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06001B1B RID: 6939 RVA: 0x004E6EEE File Offset: 0x004E50EE
		// (set) Token: 0x06001B1C RID: 6940 RVA: 0x004E6EF6 File Offset: 0x004E50F6
		public AWorkshopProgressReporter ProgressReporter { get; protected set; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06001B1D RID: 6941 RVA: 0x004E6EFF File Offset: 0x004E50FF
		// (set) Token: 0x06001B1E RID: 6942 RVA: 0x004E6F07 File Offset: 0x004E5107
		public AWorkshopTagsCollection SupportedTags { get; protected set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06001B1F RID: 6943 RVA: 0x004E6F10 File Offset: 0x004E5110
		// (set) Token: 0x06001B20 RID: 6944 RVA: 0x004E6F18 File Offset: 0x004E5118
		public WorkshopIssueReporter IssueReporter { get; protected set; }

		// Token: 0x06001B21 RID: 6945
		public abstract void Initialize();

		// Token: 0x06001B22 RID: 6946
		public abstract void Shutdown();

		// Token: 0x06001B23 RID: 6947
		public abstract void PublishWorld(WorldFileData world, WorkshopItemPublishSettings settings);

		// Token: 0x06001B24 RID: 6948
		public abstract void PublishResourcePack(ResourcePack resourcePack, WorkshopItemPublishSettings settings);

		// Token: 0x06001B25 RID: 6949
		public abstract bool TryGetInfoForWorld(WorldFileData world, out FoundWorkshopEntryInfo info);

		// Token: 0x06001B26 RID: 6950
		public abstract bool TryGetInfoForResourcePack(ResourcePack resourcePack, out FoundWorkshopEntryInfo info);

		// Token: 0x06001B27 RID: 6951
		public abstract void LoadEarlyContent();

		// Token: 0x06001B28 RID: 6952
		public abstract List<string> GetListOfSubscribedResourcePackPaths();

		// Token: 0x06001B29 RID: 6953
		public abstract List<string> GetListOfSubscribedWorldPaths();

		// Token: 0x06001B2A RID: 6954
		public abstract bool TryGetPath(string pathEnd, out string fullPathFound);

		// Token: 0x06001B2B RID: 6955
		public abstract void ImportDownloadedWorldToLocalSaves(WorldFileData world, string newFileName = null, string newDisplayName = null);
	}
}
