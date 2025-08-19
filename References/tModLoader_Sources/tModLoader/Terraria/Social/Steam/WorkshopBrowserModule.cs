using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Steamworks;
using Terraria.Localization;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.UI.DownloadManager;
using Terraria.ModLoader.UI.ModBrowser;
using Terraria.Social.Base;

namespace Terraria.Social.Steam
{
	// Token: 0x020000F2 RID: 242
	internal class WorkshopBrowserModule : SocialBrowserModule
	{
		// Token: 0x06001863 RID: 6243 RVA: 0x004BC24F File Offset: 0x004BA44F
		private PublishedFileId_t GetId(ModPubId_t modId)
		{
			return new PublishedFileId_t(ulong.Parse(modId.m_ModPubId));
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x004BC261 File Offset: 0x004BA461
		public WorkshopBrowserModule()
		{
			ModOrganizer.OnLocalModsChanged += this.OnLocalModsChanged;
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x004BC285 File Offset: 0x004BA485
		public bool Initialize()
		{
			this.OnLocalModsChanged(null, false);
			return true;
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x004BC290 File Offset: 0x004BA490
		private void OnLocalModsChanged(HashSet<string> modSlugs, bool isDeletion)
		{
			this.InstalledItems = ModOrganizer.FindWorkshopMods();
			if (SteamedWraps.SteamAvailable)
			{
				this.CachedInstalledModDownloadItems = ((SocialBrowserModule)this).DirectQueryInstalledMDItems(default(QueryParameters));
			}
			if (!isDeletion)
			{
				return;
			}
			foreach (string item in modSlugs)
			{
				this.intermediateInstallStateMods.Add(item);
			}
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x004BC310 File Offset: 0x004BA510
		public IReadOnlyList<LocalMod> GetInstalledMods()
		{
			if (this.InstalledItems == null)
			{
				this.InstalledItems = ModOrganizer.FindWorkshopMods();
			}
			return this.InstalledItems;
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06001868 RID: 6248 RVA: 0x004BC32B File Offset: 0x004BA52B
		// (set) Token: 0x06001869 RID: 6249 RVA: 0x004BC333 File Offset: 0x004BA533
		public List<ModDownloadItem> CachedInstalledModDownloadItems { get; set; }

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x0600186A RID: 6250 RVA: 0x004BC33C File Offset: 0x004BA53C
		// (set) Token: 0x0600186B RID: 6251 RVA: 0x004BC344 File Offset: 0x004BA544
		private IReadOnlyList<LocalMod> InstalledItems { get; set; }

		// Token: 0x0600186C RID: 6252 RVA: 0x004BC350 File Offset: 0x004BA550
		public bool GetModIdFromLocalFiles(TmodFile modFile, out ModPubId_t modId)
		{
			ulong publishId;
			bool publishIdLocal = WorkshopHelper.GetPublishIdLocal(modFile, out publishId);
			modId = new ModPubId_t
			{
				m_ModPubId = publishId.ToString()
			};
			return publishIdLocal;
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x004BC382 File Offset: 0x004BA582
		public bool DoesItemNeedUpdate(ModPubId_t modId, LocalMod installed, Version webVersion)
		{
			return installed.Version < webVersion || (SteamedWraps.SteamAvailable && SteamedWraps.DoesWorkshopItemNeedUpdate(this.GetId(modId)));
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x004BC3AC File Offset: 0x004BA5AC
		public bool DoesAppNeedRestartToReinstallItem(ModPubId_t modId)
		{
			return SteamedWraps.IsWorkshopItemInstalled(this.GetId(modId));
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x004BC3BC File Offset: 0x004BA5BC
		public void DownloadItem(ModDownloadItem item, IDownloadProgress uiProgress)
		{
			item.UpdateInstallState();
			PublishedFileId_t publishId;
			publishId..ctor(ulong.Parse(item.PublishId.m_ModPubId));
			bool forceUpdate = item.NeedUpdate || !SteamedWraps.IsWorkshopItemInstalled(publishId);
			if (uiProgress != null)
			{
				uiProgress.DownloadStarted(item.DisplayName);
			}
			Utils.LogAndConsoleInfoMessage(Language.GetTextValue("tModLoader.BeginDownload", item.DisplayName));
			new SteamedWraps.ModDownloadInstance().Download(publishId, uiProgress, forceUpdate);
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x004BC42C File Offset: 0x004BA62C
		public string GetModWebPage(ModPubId_t modId)
		{
			return "https://steamcommunity.com/sharedfiles/filedetails/?id=" + modId.m_ModPubId;
		}

		/// <summary>
		/// Assumes Intialize has been run prior to use.
		/// </summary>
		// Token: 0x06001871 RID: 6257 RVA: 0x004BC43E File Offset: 0x004BA63E
		[AsyncIteratorStateMachine(typeof(WorkshopBrowserModule.<QueryBrowser>d__20))]
		public IAsyncEnumerable<ModDownloadItem> QueryBrowser(QueryParameters queryParams, [EnumeratorCancellation] CancellationToken token = default(CancellationToken))
		{
			WorkshopBrowserModule.<QueryBrowser>d__20 <QueryBrowser>d__ = new WorkshopBrowserModule.<QueryBrowser>d__20(-2);
			<QueryBrowser>d__.<>4__this = this;
			<QueryBrowser>d__.<>3__queryParams = queryParams;
			<QueryBrowser>d__.<>3__token = token;
			return <QueryBrowser>d__;
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x004BC45C File Offset: 0x004BA65C
		public List<ModDownloadItem> DirectQueryItems(QueryParameters queryParams, out List<string> missingMods)
		{
			if (queryParams.searchModIds == null || !SteamedWraps.SteamAvailable)
			{
				throw new Exception("Unexpected Call of DirectQueryItems while either Steam is not initialized or query parameters.searchModIds is null");
			}
			return new WorkshopHelper.QueryHelper.AQueryInstance(queryParams).QueryItemsSynchronously(out missingMods);
		}

		// Token: 0x0400136C RID: 4972
		public static WorkshopBrowserModule Instance = new WorkshopBrowserModule();

		// Token: 0x0400136E RID: 4974
		private HashSet<string> intermediateInstallStateMods = new HashSet<string>();
	}
}
