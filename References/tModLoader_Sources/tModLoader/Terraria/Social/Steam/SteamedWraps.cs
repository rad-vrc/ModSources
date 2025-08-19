using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using log4net;
using ReLogic.OS;
using Steamworks;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Engine;
using Terraria.ModLoader.UI.DownloadManager;
using Terraria.ModLoader.UI.ModBrowser;
using Terraria.Social.Base;

namespace Terraria.Social.Steam
{
	// Token: 0x020000EE RID: 238
	public static class SteamedWraps
	{
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600182D RID: 6189 RVA: 0x004BAB35 File Offset: 0x004B8D35
		// (set) Token: 0x0600182E RID: 6190 RVA: 0x004BAB3C File Offset: 0x004B8D3C
		public static bool SteamClient { get; set; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600182F RID: 6191 RVA: 0x004BAB44 File Offset: 0x004B8D44
		// (set) Token: 0x06001830 RID: 6192 RVA: 0x004BAB4B File Offset: 0x004B8D4B
		public static bool FamilyShared { get; set; } = false;

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06001831 RID: 6193 RVA: 0x004BAB53 File Offset: 0x004B8D53
		// (set) Token: 0x06001832 RID: 6194 RVA: 0x004BAB5A File Offset: 0x004B8D5A
		internal static bool SteamAvailable { get; set; }

		// Token: 0x06001833 RID: 6195 RVA: 0x004BAB64 File Offset: 0x004B8D64
		internal static string GetCurrentSteamLangKey()
		{
			string result;
			switch (LanguageManager.Instance.ActiveCulture.LegacyId)
			{
			case 2:
				result = "german";
				break;
			case 3:
				result = "italian";
				break;
			case 4:
				result = "french";
				break;
			case 5:
				result = "spanish";
				break;
			case 6:
				result = "russian";
				break;
			case 7:
				result = "schinese";
				break;
			case 8:
				result = "portuguese";
				break;
			case 9:
				result = "polish";
				break;
			default:
				result = "english";
				break;
			}
			return result;
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x004BABF4 File Offset: 0x004B8DF4
		internal static void ReportCheckSteamLogs()
		{
			string workshopLogLoc = "";
			if (Platform.IsWindows)
			{
				workshopLogLoc = "C:/Program Files (x86)/Steam/logs/workshop_log.txt";
			}
			else if (Platform.IsOSX)
			{
				workshopLogLoc = "~/Library/Application Support/Steam/logs/workshop_log.txt";
			}
			else if (Platform.IsLinux)
			{
				workshopLogLoc = "/home/user/.local/share/Steam/logs/workshop_log.txt";
			}
			Utils.LogAndConsoleInfoMessage(Language.GetTextValue("tModLoader.ConsultSteamLogs", workshopLogLoc));
			Utils.LogAndConsoleInfoMessage("See https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Usage-FAQ#mod-browser for even more suggestions.");
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x004BAC4C File Offset: 0x004B8E4C
		public static void QueueForceValidateSteamInstall()
		{
			if (!SteamedWraps.SteamClient)
			{
				return;
			}
			if (Environment.GetEnvironmentVariable("SteamClientLaunch") != "1")
			{
				Logging.tML.Info("Launched Outside of Steam. Skipping attempt to trigger 'verify local files' in Steam. If error persists, please attempt this manually");
				return;
			}
			SteamApps.MarkContentCorrupt(false);
			Logging.tML.Info("Marked tModLoader installation files as corrupt in Steam. On Next Launch, User will have 'Verify Local Files' ran");
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x004BACA0 File Offset: 0x004B8EA0
		internal static void Initialize()
		{
			SteamedWraps.InitializeModTags();
			if (!SteamedWraps.FamilyShared && SocialAPI.Mode == SocialMode.Steam)
			{
				SteamedWraps.SteamAvailable = true;
				SteamedWraps.SteamClient = true;
				Logging.tML.Info("SteamBackend: Running standard Steam Desktop Client API");
				return;
			}
			if (!Main.dedServ && !SteamedWraps.TryInitViaGameServer())
			{
				Utils.ShowFancyErrorMessage("Steam Game Server failed to Init. Steam Workshop downloading on GoG is unavailable. Make sure Steam is installed", 10002, null);
			}
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x004BACFC File Offset: 0x004B8EFC
		public static bool TryInitViaGameServer()
		{
			Steam.SetAppId(Steam.TMLAppID_t);
			try
			{
				if (!GameServer.Init(0U, 7775, 7774, 1, "0.11.9.0"))
				{
					return false;
				}
				SteamGameServer.SetGameDescription("tModLoader Mod Browser");
				SteamGameServer.SetProduct(1281930U.ToString());
				SteamGameServer.LogOnAnonymous();
			}
			catch (Exception e)
			{
				Logging.tML.Error(e);
				return false;
			}
			Logging.tML.Info("SteamBackend: Running non-standard Steam GameServer API");
			SteamedWraps.SteamAvailable = true;
			return true;
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x004BAD8C File Offset: 0x004B8F8C
		public static void ReleaseWorkshopHandle(UGCQueryHandle_t handle)
		{
			if (SteamedWraps.SteamClient)
			{
				SteamUGC.ReleaseQueryUGCRequest(handle);
				return;
			}
			if (SteamedWraps.SteamAvailable)
			{
				SteamGameServerUGC.ReleaseQueryUGCRequest(handle);
			}
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x004BADAC File Offset: 0x004B8FAC
		public static SteamUGCDetails_t FetchItemDetails(UGCQueryHandle_t handle, uint index)
		{
			SteamUGCDetails_t pDetails = default(SteamUGCDetails_t);
			if (SteamedWraps.SteamClient)
			{
				SteamUGC.GetQueryUGCResult(handle, index, ref pDetails);
			}
			else if (SteamedWraps.SteamAvailable)
			{
				SteamGameServerUGC.GetQueryUGCResult(handle, index, ref pDetails);
			}
			return pDetails;
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x004BADE8 File Offset: 0x004B8FE8
		public static PublishedFileId_t[] FetchItemDependencies(UGCQueryHandle_t handle, uint index, uint numChildren)
		{
			PublishedFileId_t[] deps = new PublishedFileId_t[numChildren];
			if (SteamedWraps.SteamClient)
			{
				SteamUGC.GetQueryUGCChildren(handle, index, deps, numChildren);
			}
			else if (SteamedWraps.SteamAvailable)
			{
				SteamGameServerUGC.GetQueryUGCChildren(handle, index, deps, numChildren);
			}
			return deps;
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x004BAE24 File Offset: 0x004B9024
		private static void ModifyQueryHandle(ref UGCQueryHandle_t qHandle, QueryParameters qP)
		{
			SteamedWraps.FilterByText(ref qHandle, qP.searchGeneric);
			SteamedWraps.FilterByText(ref qHandle, qP.searchAuthor ?? "");
			SteamedWraps.FilterByTags(ref qHandle, qP.searchTags);
			SteamedWraps.FilterModSide(ref qHandle, qP.modSideFilter);
			if (SteamedWraps.SteamClient)
			{
				SteamUGC.SetAllowCachedResponse(qHandle, 0U);
				SteamUGC.SetRankedByTrendDays(qHandle, qP.days);
				SteamUGC.SetLanguage(qHandle, SteamedWraps.GetCurrentSteamLangKey());
				SteamUGC.SetReturnChildren(qHandle, true);
				SteamUGC.SetReturnKeyValueTags(qHandle, true);
				SteamUGC.SetReturnPlaytimeStats(qHandle, 30U);
				return;
			}
			if (SteamedWraps.SteamAvailable)
			{
				SteamGameServerUGC.SetAllowCachedResponse(qHandle, 0U);
				SteamGameServerUGC.SetRankedByTrendDays(qHandle, qP.days);
				SteamGameServerUGC.SetLanguage(qHandle, SteamedWraps.GetCurrentSteamLangKey());
				SteamGameServerUGC.SetReturnChildren(qHandle, true);
				SteamGameServerUGC.SetReturnKeyValueTags(qHandle, true);
				SteamGameServerUGC.SetReturnPlaytimeStats(qHandle, 30U);
			}
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x004BAF29 File Offset: 0x004B9129
		private static void FilterModSide(ref UGCQueryHandle_t qHandle, ModSideFilter side)
		{
			if (side == ModSideFilter.All)
			{
				return;
			}
			SteamedWraps.FilterByTags(ref qHandle, new string[]
			{
				side.ToString()
			});
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x004BAF4C File Offset: 0x004B914C
		private static void FilterByTags(ref UGCQueryHandle_t qHandle, string[] tags)
		{
			if (tags == null)
			{
				return;
			}
			foreach (string tag in tags)
			{
				if (SteamedWraps.SteamClient)
				{
					SteamUGC.AddRequiredTag(qHandle, tag);
				}
				else if (SteamedWraps.SteamAvailable)
				{
					SteamGameServerUGC.AddRequiredTag(qHandle, tag);
				}
			}
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x004BAF9B File Offset: 0x004B919B
		private static void FilterByInternalName(ref UGCQueryHandle_t qHandle, string internalName)
		{
			if (internalName == null)
			{
				return;
			}
			if (SteamedWraps.SteamClient)
			{
				SteamUGC.AddRequiredKeyValueTag(qHandle, "name", internalName);
				return;
			}
			if (SteamedWraps.SteamAvailable)
			{
				SteamGameServerUGC.AddRequiredKeyValueTag(qHandle, "name", internalName);
			}
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x004BAFD4 File Offset: 0x004B91D4
		private static void FilterByText(ref UGCQueryHandle_t qHandle, string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			if (SteamedWraps.SteamClient)
			{
				SteamUGC.SetSearchText(qHandle, text);
				return;
			}
			if (SteamedWraps.SteamAvailable)
			{
				SteamGameServerUGC.SetSearchText(qHandle, text);
			}
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x004BB008 File Offset: 0x004B9208
		public static SteamAPICall_t GenerateDirectItemsQuery(string[] modId)
		{
			PublishedFileId_t[] publishId = Array.ConvertAll<string, PublishedFileId_t>(modId, (string s) => new PublishedFileId_t(ulong.Parse(s)));
			if (SteamedWraps.SteamClient)
			{
				UGCQueryHandle_t qHandle = SteamUGC.CreateQueryUGCDetailsRequest(publishId, (uint)publishId.Length);
				SteamedWraps.ModifyQueryHandle(ref qHandle, default(QueryParameters));
				return SteamUGC.SendQueryUGCRequest(qHandle);
			}
			if (SteamedWraps.SteamAvailable)
			{
				UGCQueryHandle_t qHandle2 = SteamGameServerUGC.CreateQueryUGCDetailsRequest(publishId, (uint)publishId.Length);
				SteamedWraps.ModifyQueryHandle(ref qHandle2, default(QueryParameters));
				return SteamGameServerUGC.SendQueryUGCRequest(qHandle2);
			}
			return default(SteamAPICall_t);
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x004BB098 File Offset: 0x004B9298
		public static EUGCQuery CalculateQuerySort(QueryParameters qParams)
		{
			if ((!string.IsNullOrEmpty(qParams.searchGeneric) || !string.IsNullOrEmpty(qParams.searchAuthor)) && qParams.sortingParamater == ModBrowserSortMode.Hot)
			{
				return 11;
			}
			EUGCQuery result;
			switch (qParams.sortingParamater)
			{
			case ModBrowserSortMode.DownloadsDescending:
				result = 12;
				break;
			case ModBrowserSortMode.RecentlyPublished:
				result = 1;
				break;
			case ModBrowserSortMode.RecentlyUpdated:
				result = 19;
				break;
			case ModBrowserSortMode.Hot:
				if (qParams.days == 0U)
				{
					result = 0;
				}
				else
				{
					result = 3;
				}
				break;
			default:
				result = 11;
				break;
			}
			return result;
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x004BB10C File Offset: 0x004B930C
		public static SteamAPICall_t GenerateAndSubmitModBrowserQuery(uint page, QueryParameters qP, string internalName = null)
		{
			UGCQueryHandle_t qHandle = SteamedWraps.GetQueryHandle(page, qP);
			if (qHandle == default(UGCQueryHandle_t))
			{
				return default(SteamAPICall_t);
			}
			if (SteamedWraps.SteamClient)
			{
				SteamedWraps.ModifyQueryHandle(ref qHandle, qP);
				SteamedWraps.FilterByInternalName(ref qHandle, internalName);
				return SteamUGC.SendQueryUGCRequest(qHandle);
			}
			SteamedWraps.ModifyQueryHandle(ref qHandle, qP);
			SteamedWraps.FilterByInternalName(ref qHandle, internalName);
			return SteamGameServerUGC.SendQueryUGCRequest(qHandle);
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x004BB170 File Offset: 0x004B9370
		public static UGCQueryHandle_t GetQueryHandle(uint page, QueryParameters qP)
		{
			if (SteamedWraps.SteamClient && qP.queryType == QueryType.SearchUserPublishedOnly)
			{
				return SteamUGC.CreateQueryUserUGCRequest(SteamUser.GetSteamID().GetAccountID(), 0, 0, 0, new AppId_t(1281930U), new AppId_t(1281930U), page);
			}
			if (SteamedWraps.SteamClient)
			{
				string searchAuthor = qP.searchAuthor;
				ulong steamID64;
				if (searchAuthor != null && searchAuthor.Length == 17 && ulong.TryParse(qP.searchAuthor, out steamID64))
				{
					return SteamUGC.CreateQueryUserUGCRequest(new AccountID_t((uint)(steamID64 & (ulong)-1)), 0, 0, 0, new AppId_t(1281930U), new AppId_t(1281930U), page);
				}
			}
			if (SteamedWraps.SteamClient)
			{
				return SteamUGC.CreateQueryAllUGCRequest(SteamedWraps.CalculateQuerySort(qP), 0, new AppId_t(1281930U), new AppId_t(1281930U), page);
			}
			if (SteamedWraps.SteamAvailable)
			{
				return SteamGameServerUGC.CreateQueryAllUGCRequest(SteamedWraps.CalculateQuerySort(qP), 0, new AppId_t(1281930U), new AppId_t(1281930U), page);
			}
			return default(UGCQueryHandle_t);
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x004BB26C File Offset: 0x004B946C
		public static void FetchPlayTimeStats(UGCQueryHandle_t handle, uint index, out ulong hot, out ulong downloads)
		{
			if (SteamedWraps.SteamClient)
			{
				SteamUGC.GetQueryUGCStatistic(handle, index, 3, ref downloads);
				SteamUGC.GetQueryUGCStatistic(handle, index, 11, ref hot);
				return;
			}
			if (SteamedWraps.SteamAvailable)
			{
				SteamGameServerUGC.GetQueryUGCStatistic(handle, index, 3, ref downloads);
				SteamGameServerUGC.GetQueryUGCStatistic(handle, index, 11, ref hot);
				return;
			}
			hot = 0UL;
			downloads = 0UL;
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x004BB2BB File Offset: 0x004B94BB
		public static void FetchPreviewImageUrl(UGCQueryHandle_t handle, uint index, out string modIconUrl)
		{
			if (SteamedWraps.SteamClient)
			{
				SteamUGC.GetQueryUGCPreviewURL(handle, index, ref modIconUrl, 1000U);
				return;
			}
			if (SteamedWraps.SteamAvailable)
			{
				SteamGameServerUGC.GetQueryUGCPreviewURL(handle, index, ref modIconUrl, 1000U);
				return;
			}
			modIconUrl = null;
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x004BB2EC File Offset: 0x004B94EC
		public static void FetchMetadata(UGCQueryHandle_t handle, uint index, out NameValueCollection metadata)
		{
			metadata = new NameValueCollection();
			uint keyCount;
			if (SteamedWraps.SteamClient)
			{
				keyCount = SteamUGC.GetQueryUGCNumKeyValueTags(handle, index);
			}
			else if (SteamedWraps.SteamAvailable)
			{
				keyCount = SteamGameServerUGC.GetQueryUGCNumKeyValueTags(handle, index);
			}
			else
			{
				keyCount = 0U;
			}
			for (uint i = 0U; i < keyCount; i += 1U)
			{
				string key;
				string val;
				if (SteamedWraps.SteamClient)
				{
					SteamUGC.GetQueryUGCKeyValueTag(handle, index, i, ref key, 255U, ref val, 255U);
				}
				else
				{
					SteamGameServerUGC.GetQueryUGCKeyValueTag(handle, index, i, ref key, 255U, ref val, 255U);
				}
				metadata[key] = val;
			}
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x004BB370 File Offset: 0x004B9570
		public static void RunCallbacks()
		{
			if (SteamedWraps.SteamClient)
			{
				SteamAPI.RunCallbacks();
				return;
			}
			if (SteamedWraps.SteamAvailable)
			{
				GameServer.RunCallbacks();
			}
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x004BB38B File Offset: 0x004B958B
		public static void StopPlaytimeTracking()
		{
			if (Program.LaunchParameters.ContainsKey("-disableugcplaytime"))
			{
				return;
			}
			if (SteamedWraps.SteamClient)
			{
				SteamUGC.StopPlaytimeTrackingForAllItems();
				return;
			}
			if (SteamedWraps.SteamAvailable)
			{
				SteamGameServerUGC.StopPlaytimeTrackingForAllItems();
			}
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x004BB3BC File Offset: 0x004B95BC
		public static void BeginPlaytimeTracking()
		{
			if (!SteamedWraps.SteamAvailable || Program.LaunchParameters.ContainsKey("-disableugcplaytime"))
			{
				return;
			}
			List<PublishedFileId_t> list = new List<PublishedFileId_t>();
			Mod[] mods = ModLoader.Mods;
			for (int j = 0; j < mods.Length; j++)
			{
				ulong publishId;
				if (WorkshopHelper.GetPublishIdLocal(mods[j].File, out publishId))
				{
					list.Add(new PublishedFileId_t(publishId));
				}
			}
			int count = list.Count;
			if (count == 0)
			{
				return;
			}
			int pg = count / 100;
			int rem = count % 100;
			for (int i = 0; i < pg + 1; i++)
			{
				List<PublishedFileId_t> pgList = list.GetRange(i * 100, (i == pg) ? rem : 100);
				if (SteamedWraps.SteamClient)
				{
					SteamUGC.StartPlaytimeTracking(pgList.ToArray(), (uint)pgList.Count);
				}
				else if (SteamedWraps.SteamAvailable)
				{
					SteamGameServerUGC.StartPlaytimeTracking(pgList.ToArray(), (uint)pgList.Count);
				}
			}
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x004BB498 File Offset: 0x004B9698
		internal static void OnGameExitCleanup()
		{
			if (!SteamedWraps.SteamAvailable)
			{
				SteamedWraps.CleanupACF();
				return;
			}
			if (SteamedWraps.SteamClient)
			{
				SteamAPI.Shutdown();
				return;
			}
			SteamGameServer.LogOff();
			Thread.Sleep(1000);
			GameServer.Shutdown();
			SteamedWraps.CleanupACF();
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x004BB4CD File Offset: 0x004B96CD
		public static uint GetWorkshopItemState(PublishedFileId_t publishId)
		{
			if (SteamedWraps.SteamClient)
			{
				return SteamUGC.GetItemState(publishId);
			}
			if (SteamedWraps.SteamAvailable)
			{
				return SteamGameServerUGC.GetItemState(publishId);
			}
			return 0U;
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x004BB4EC File Offset: 0x004B96EC
		public static SteamedWraps.ItemInstallInfo GetInstallInfo(PublishedFileId_t publishId)
		{
			string installPath = null;
			uint lastUpdatedTime = 0U;
			if (SteamedWraps.SteamClient)
			{
				ulong installSize;
				SteamUGC.GetItemInstallInfo(publishId, ref installSize, ref installPath, 1000U, ref lastUpdatedTime);
			}
			else if (SteamedWraps.SteamAvailable)
			{
				ulong installSize2;
				SteamGameServerUGC.GetItemInstallInfo(publishId, ref installSize2, ref installPath, 1000U, ref lastUpdatedTime);
			}
			return new SteamedWraps.ItemInstallInfo
			{
				installPath = installPath,
				lastUpdatedTime = lastUpdatedTime
			};
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x004BB54C File Offset: 0x004B974C
		public static void UninstallWorkshopItem(PublishedFileId_t publishId, string installPath = null)
		{
			if (string.IsNullOrEmpty(installPath))
			{
				installPath = SteamedWraps.GetInstallInfo(publishId).installPath;
			}
			if (!Directory.Exists(installPath))
			{
				return;
			}
			if (SteamedWraps.SteamClient)
			{
				SteamUGC.UnsubscribeItem(publishId);
			}
			Directory.Delete(installPath, true);
			if (!SteamedWraps.SteamClient)
			{
				SteamedWraps.deletedItems.Add(publishId);
			}
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x004BB5A0 File Offset: 0x004B97A0
		private static void CleanupACF()
		{
			foreach (PublishedFileId_t publishId in SteamedWraps.deletedItems)
			{
				SteamedWraps.UninstallACF(publishId);
			}
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x004BB5F0 File Offset: 0x004B97F0
		private static void UninstallACF(PublishedFileId_t publishId)
		{
			string path = Path.Combine(Directory.GetCurrentDirectory(), "steamapps", "workshop", "appworkshop_" + 1281930U.ToString() + ".acf");
			string[] acf = File.ReadAllLines(path);
			using (StreamWriter w = new StreamWriter(path))
			{
				int blockLines = 5;
				int skip = 0;
				for (int i = 0; i < acf.Length; i++)
				{
					if (acf[i].Contains(publishId.ToString()))
					{
						skip = blockLines;
					}
					else if (skip > 0)
					{
						skip--;
					}
					else
					{
						w.WriteLine(acf[i]);
					}
				}
			}
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x004BB6A0 File Offset: 0x004B98A0
		public static bool IsWorkshopItemInstalled(PublishedFileId_t publishId)
		{
			uint workshopItemState = SteamedWraps.GetWorkshopItemState(publishId);
			bool installed = (workshopItemState & 4U) > 0U;
			bool downloading = (workshopItemState & 48U) > 0U;
			return installed && !downloading;
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x004BB6CC File Offset: 0x004B98CC
		public static bool DoesWorkshopItemNeedUpdate(PublishedFileId_t publishId)
		{
			uint currState = SteamedWraps.GetWorkshopItemState(publishId);
			return (currState & 8U) != 0U || currState == 0U || (currState & 32U) > 0U;
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x004BB6F4 File Offset: 0x004B98F4
		internal static void ModifyUgcUpdateHandleCommon(ref UGCUpdateHandle_t uGCUpdateHandle_t, WorkshopHelper.UGCBased.SteamWorkshopItem _entryData)
		{
			if (!SteamedWraps.SteamClient)
			{
				throw new Exception("Invalid Call to ModifyUgcUpdateHandleTModLoader. Steam Client API not initialized!");
			}
			if (_entryData.Title != null)
			{
				SteamUGC.SetItemTitle(uGCUpdateHandle_t, _entryData.Title);
			}
			if (!string.IsNullOrEmpty(_entryData.Description))
			{
				SteamUGC.SetItemDescription(uGCUpdateHandle_t, _entryData.Description);
			}
			Logging.tML.Info("Adding tags and visibility");
			SteamUGC.SetItemContent(uGCUpdateHandle_t, _entryData.ContentFolderPath);
			SteamUGC.SetItemTags(uGCUpdateHandle_t, _entryData.Tags);
			if (_entryData.PreviewImagePath != null)
			{
				SteamUGC.SetItemPreview(uGCUpdateHandle_t, _entryData.PreviewImagePath);
			}
			if (_entryData.Visibility != null)
			{
				SteamUGC.SetItemVisibility(uGCUpdateHandle_t, _entryData.Visibility.Value);
			}
			Logging.tML.Info("Setting the language for default description");
			SteamUGC.SetItemUpdateLanguage(uGCUpdateHandle_t, SteamedWraps.GetCurrentSteamLangKey());
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x004BB7E0 File Offset: 0x004B99E0
		internal static void ModifyUgcUpdateHandleTModLoader(ref UGCUpdateHandle_t uGCUpdateHandle_t, WorkshopHelper.UGCBased.SteamWorkshopItem _entryData, PublishedFileId_t _publishedFileID)
		{
			if (!SteamedWraps.SteamClient)
			{
				throw new Exception("Invalid Call to ModifyUgcUpdateHandleTModLoader. Steam Client API not initialized!");
			}
			Logging.tML.Info("Adding tModLoader Metadata to Workshop Upload");
			foreach (string key in WorkshopHelper.MetadataKeys)
			{
				SteamUGC.RemoveItemKeyValueTags(uGCUpdateHandle_t, key);
				SteamUGC.AddItemKeyValueTag(uGCUpdateHandle_t, key, _entryData.BuildData[key]);
			}
			string refs = _entryData.BuildData["workshopdeps"];
			if (!string.IsNullOrWhiteSpace(refs))
			{
				Logging.tML.Info("Adding dependencies to Workshop Upload");
				foreach (string dependency in refs.Split(",", StringSplitOptions.TrimEntries))
				{
					try
					{
						PublishedFileId_t child;
						child..ctor((ulong)uint.Parse(dependency));
						SteamUGC.AddDependency(_publishedFileID, child);
					}
					catch (Exception)
					{
						ILog tML = Logging.tML;
						string str = "Failed to add Workshop dependency: ";
						string str2 = dependency;
						string str3 = " to ";
						PublishedFileId_t publishedFileId_t = _publishedFileID;
						tML.Error(str + str2 + str3 + publishedFileId_t.ToString());
					}
				}
			}
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x004BB8EC File Offset: 0x004B9AEC
		private static void InitializeModTags()
		{
			SteamedWraps.AddModTag("tModLoader.TagsContent", "New Content");
			SteamedWraps.AddModTag("tModLoader.TagsUtility", "Utilities");
			SteamedWraps.AddModTag("tModLoader.TagsLibrary", "Library");
			SteamedWraps.AddModTag("tModLoader.TagsQoL", "Quality of Life");
			SteamedWraps.AddModTag("tModLoader.TagsGameplay", "Gameplay Tweaks");
			SteamedWraps.AddModTag("tModLoader.TagsAudio", "Audio Tweaks");
			SteamedWraps.AddModTag("tModLoader.TagsVisual", "Visual Tweaks");
			SteamedWraps.AddModTag("tModLoader.TagsGen", "Custom World Gen");
			SteamedWraps.AddModTag("tModLoader.TagsLanguage_English", "English");
			SteamedWraps.AddModTag("tModLoader.TagsLanguage_German", "German");
			SteamedWraps.AddModTag("tModLoader.TagsLanguage_Italian", "Italian");
			SteamedWraps.AddModTag("tModLoader.TagsLanguage_French", "French");
			SteamedWraps.AddModTag("tModLoader.TagsLanguage_Spanish", "Spanish");
			SteamedWraps.AddModTag("tModLoader.TagsLanguage_Russian", "Russian");
			SteamedWraps.AddModTag("tModLoader.TagsLanguage_Chinese", "Chinese");
			SteamedWraps.AddModTag("tModLoader.TagsLanguage_Portuguese", "Portuguese");
			SteamedWraps.AddModTag("tModLoader.TagsLanguage_Polish", "Polish");
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x004BB9F8 File Offset: 0x004B9BF8
		private static void AddModTag(string tagNameKey, string tagInternalName)
		{
			SteamedWraps.ModTags.Add(new WorkshopTagOption(tagNameKey, tagInternalName));
		}

		// Token: 0x04001358 RID: 4952
		internal const uint thisApp = 1281930U;

		// Token: 0x0400135C RID: 4956
		private const int PlaytimePagingConst = 100;

		// Token: 0x0400135D RID: 4957
		private static List<PublishedFileId_t> deletedItems = new List<PublishedFileId_t>();

		// Token: 0x0400135E RID: 4958
		public static readonly List<WorkshopTagOption> ModTags = new List<WorkshopTagOption>();

		// Token: 0x02000888 RID: 2184
		public struct ItemInstallInfo
		{
			// Token: 0x040069E9 RID: 27113
			public string installPath;

			// Token: 0x040069EA RID: 27114
			public uint lastUpdatedTime;
		}

		// Token: 0x02000889 RID: 2185
		internal class ModDownloadInstance
		{
			// Token: 0x060051BB RID: 20923 RVA: 0x006979AC File Offset: 0x00695BAC
			public ModDownloadInstance()
			{
				if (SteamedWraps.SteamClient)
				{
					this._downloadHook = Callback<DownloadItemResult_t>.Create(new Callback<DownloadItemResult_t>.DispatchDelegate(this.MarkDownloadComplete));
					return;
				}
				this._downloadHook = Callback<DownloadItemResult_t>.CreateGameServer(new Callback<DownloadItemResult_t>.DispatchDelegate(this.MarkDownloadComplete));
			}

			// Token: 0x060051BC RID: 20924 RVA: 0x006979EC File Offset: 0x00695BEC
			internal void MarkDownloadComplete(DownloadItemResult_t result)
			{
				this._downloadCallback = result.m_eResult;
				ILog tML = Logging.tML;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(39, 1);
				defaultInterpolatedStringHandler.AppendLiteral("Download Callback Received From Steam: ");
				defaultInterpolatedStringHandler.AppendFormatted<EResult>(this._downloadCallback);
				tML.Debug(defaultInterpolatedStringHandler.ToStringAndClear());
			}

			/// <summary>
			/// Updates and/or Downloads the Item specified by publishId
			/// </summary>
			// Token: 0x060051BD RID: 20925 RVA: 0x00697A3C File Offset: 0x00695C3C
			internal void Download(PublishedFileId_t publishId, IDownloadProgress uiProgress = null, bool forceUpdate = false)
			{
				if (!SteamedWraps.SteamAvailable)
				{
					return;
				}
				if (SteamedWraps.SteamClient)
				{
					SteamUGC.SubscribeItem(publishId);
				}
				if (!SteamedWraps.DoesWorkshopItemNeedUpdate(publishId) && !forceUpdate)
				{
					Utils.LogAndConsoleErrorMessage(Language.GetTextValue("tModLoader.SteamRejectUpdate", publishId));
					return;
				}
				Utils.LogAndConsoleInfoMessage(Language.GetTextValue("tModLoader.SteamDownloader"));
				bool downloadStarted;
				if (SteamedWraps.SteamClient)
				{
					downloadStarted = SteamUGC.DownloadItem(publishId, true);
				}
				else
				{
					downloadStarted = SteamGameServerUGC.DownloadItem(publishId, true);
				}
				if (!downloadStarted)
				{
					SteamedWraps.ReportCheckSteamLogs();
					throw new ArgumentException("Downloading Workshop Item failed due to unknown reasons");
				}
				this.InnerDownloadHandler(uiProgress, publishId);
				Utils.LogAndConsoleInfoMessage(Language.GetTextValue("tModLoader.EndDownload"));
			}

			// Token: 0x060051BE RID: 20926 RVA: 0x00697AD4 File Offset: 0x00695CD4
			private void InnerDownloadHandler(IDownloadProgress uiProgress, PublishedFileId_t publishId)
			{
				int nextPercentageToLog = 10;
				int numFailures = 0;
				while (!SteamedWraps.IsWorkshopItemInstalled(publishId))
				{
					ulong dlBytes;
					ulong totalBytes;
					if (SteamedWraps.SteamClient)
					{
						SteamUGC.GetItemDownloadInfo(publishId, ref dlBytes, ref totalBytes);
					}
					else
					{
						SteamGameServerUGC.GetItemDownloadInfo(publishId, ref dlBytes, ref totalBytes);
					}
					if (totalBytes == 0UL)
					{
						if (numFailures++ < 10)
						{
							Thread.Sleep(100);
							continue;
						}
					}
					else
					{
						if (uiProgress != null)
						{
							uiProgress.UpdateDownloadProgress(dlBytes / Math.Max(totalBytes, 1UL), (long)dlBytes, (long)totalBytes);
						}
						int percentage = (int)MathF.Round(dlBytes / totalBytes * 100f);
						if (percentage >= nextPercentageToLog)
						{
							Utils.LogAndConsoleInfoMessage(Language.GetTextValue("tModLoader.DownloadProgress", percentage));
							nextPercentageToLog = percentage + 10;
							if (nextPercentageToLog > 100 && nextPercentageToLog != 110)
							{
								nextPercentageToLog = 100;
							}
						}
						if (dlBytes / totalBytes != 1f)
						{
							continue;
						}
					}
					IL_C2:
					while (this._downloadCallback == null)
					{
						Thread.Sleep(100);
						SteamedWraps.RunCallbacks();
					}
					if (this._downloadCallback != 1)
					{
						SteamedWraps.ReportCheckSteamLogs();
						ILog tML = Logging.tML;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(55, 2);
						defaultInterpolatedStringHandler.AppendLiteral("Mod with ID ");
						defaultInterpolatedStringHandler.AppendFormatted<PublishedFileId_t>(publishId);
						defaultInterpolatedStringHandler.AppendLiteral(" failed to install with Steam Error Result ");
						defaultInterpolatedStringHandler.AppendFormatted<EResult>(this._downloadCallback);
						tML.Error(defaultInterpolatedStringHandler.ToStringAndClear());
					}
					return;
				}
				goto IL_C2;
			}

			// Token: 0x040069EB RID: 27115
			private EResult _downloadCallback;

			// Token: 0x040069EC RID: 27116
			protected Callback<DownloadItemResult_t> _downloadHook;
		}
	}
}
