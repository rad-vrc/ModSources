using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.UI.DownloadManager;
using Terraria.ModLoader.UI.ModBrowser;

namespace Terraria.Social.Base
{
	// Token: 0x02000106 RID: 262
	public interface SocialBrowserModule
	{
		// Token: 0x060018F9 RID: 6393
		bool Initialize();

		// Token: 0x060018FA RID: 6394
		IAsyncEnumerable<ModDownloadItem> QueryBrowser(QueryParameters queryParams, [EnumeratorCancellation] CancellationToken token = default(CancellationToken));

		// Token: 0x060018FB RID: 6395
		List<ModDownloadItem> DirectQueryItems(QueryParameters queryParams, out List<string> missingMods);

		// Token: 0x060018FC RID: 6396
		string GetModWebPage(ModPubId_t item);

		// Token: 0x060018FD RID: 6397
		bool GetModIdFromLocalFiles(TmodFile modFile, out ModPubId_t item);

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060018FE RID: 6398
		// (set) Token: 0x060018FF RID: 6399
		List<ModDownloadItem> CachedInstalledModDownloadItems { get; set; }

		// Token: 0x06001900 RID: 6400 RVA: 0x004BE6C4 File Offset: 0x004BC8C4
		List<ModDownloadItem> DirectQueryInstalledMDItems(QueryParameters qParams = default(QueryParameters))
		{
			IEnumerable<LocalMod> installedMods = this.GetInstalledMods();
			List<ModPubId_t> listIds = new List<ModPubId_t>();
			foreach (LocalMod mod in installedMods)
			{
				ModPubId_t id;
				if (this.GetModIdFromLocalFiles(mod.modFile, out id))
				{
					listIds.Add(id);
				}
			}
			qParams.searchModIds = listIds.ToArray();
			List<string> list;
			return this.DirectQueryItems(qParams, out list);
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x004BE740 File Offset: 0x004BC940
		List<ModDownloadItem> GetInstalledModDownloadItems()
		{
			if (this.CachedInstalledModDownloadItems == null)
			{
				this.CachedInstalledModDownloadItems = this.DirectQueryInstalledMDItems(default(QueryParameters));
			}
			return this.CachedInstalledModDownloadItems;
		}

		// Token: 0x06001902 RID: 6402
		bool DoesAppNeedRestartToReinstallItem(ModPubId_t modId);

		// Token: 0x06001903 RID: 6403
		bool DoesItemNeedUpdate(ModPubId_t modId, LocalMod installed, Version webVersion);

		// Token: 0x06001904 RID: 6404
		IReadOnlyList<LocalMod> GetInstalledMods();

		// Token: 0x06001905 RID: 6405 RVA: 0x004BE770 File Offset: 0x004BC970
		LocalMod IsItemInstalled(string slug)
		{
			return (from t in this.GetInstalledMods()
			where string.Equals(t.Name, slug, StringComparison.OrdinalIgnoreCase)
			select t).FirstOrDefault<LocalMod>();
		}

		// Token: 0x06001906 RID: 6406
		void DownloadItem(ModDownloadItem item, IDownloadProgress uiProgress);

		// Token: 0x06001907 RID: 6407 RVA: 0x004BE7A8 File Offset: 0x004BC9A8
		void GetDependenciesRecursive(HashSet<ModDownloadItem> set)
		{
			HashSet<ModPubId_t> fullList = (from x in set
			select x.PublishId).ToHashSet<ModPubId_t>();
			HashSet<ModPubId_t> iterationList = new HashSet<ModPubId_t>();
			HashSet<ModDownloadItem> iterationSet = set;
			for (;;)
			{
				foreach (ModDownloadItem item in iterationSet)
				{
					iterationList.UnionWith(item.ModReferenceByModId);
				}
				iterationList.ExceptWith(fullList);
				if (iterationList.Count <= 0)
				{
					break;
				}
				List<string> notFoundMods;
				iterationSet = this.DirectQueryItems(new QueryParameters
				{
					searchModIds = iterationList.ToArray<ModPubId_t>()
				}, out notFoundMods).ToHashSet<ModDownloadItem>();
				if (notFoundMods.Any<string>())
				{
					notFoundMods = notFoundMods;
				}
				fullList.UnionWith(iterationList);
				set.UnionWith(iterationSet);
			}
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x004BE884 File Offset: 0x004BCA84
		public static string GetBrowserVersionNumber(Version tmlVersion)
		{
			if (tmlVersion < new Version(0, 12))
			{
				return "1.3";
			}
			if (tmlVersion < new Version(2022, 10))
			{
				return "1.4.3";
			}
			if (tmlVersion < new Version(2023, 3, 85))
			{
				return "1.4.4-Transitive";
			}
			return "1.4.4";
		}
	}
}
