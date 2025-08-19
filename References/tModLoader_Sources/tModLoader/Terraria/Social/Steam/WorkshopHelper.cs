using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using Steamworks;
using Terraria.GameContent.UI.States;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.Engine;
using Terraria.ModLoader.UI;
using Terraria.ModLoader.UI.ModBrowser;
using Terraria.Social.Base;
using Terraria.Utilities;

namespace Terraria.Social.Steam
{
	// Token: 0x020000F3 RID: 243
	public class WorkshopHelper
	{
		/// <summary>
		/// <code>
		/// Priority is given to "-steamworkshopfolder" argument to ensure if someone has a custom steamapps/workshop folder away from tml, it can be found
		/// If SteamClient is true (ie it is a steam user running a client or host and play),
		/// 	InstallDir: SteamFiles/Steamapps/common/tModLoader is GetAppInstallDir
		/// 	WorkshopFolder: SteamFiles/Steamapps/workshop is Path.Combine(GetAppInstallDir, .., .., Workshop)
		/// If SteamClient is false, SteamAvailable = True -&gt; Is FamilyShare or GoG Client. SteamedWraps.FamilyShare differentiates if needed
		/// 	InstallDir: anywhere, manual.
		/// 	WorkshopFolder: InstallDir/Steamapps/workshop
		/// If Main.DedServ is true
		/// 	Use SteamClient reference path if it exists &amp;&amp; Not "-nosteam" supplied
		/// 	Use NotSteamClient working folder path if "-nosteam" supplied or SteamClient ref path not exists
		/// </code>
		/// </summary>
		// Token: 0x06001874 RID: 6260 RVA: 0x004BC490 File Offset: 0x004BA690
		public static string GetWorkshopFolder(AppId_t app)
		{
			string workshopLocCustom;
			if (Program.LaunchParameters.TryGetValue("-steamworkshopfolder", out workshopLocCustom))
			{
				if (Directory.Exists(workshopLocCustom))
				{
					return workshopLocCustom;
				}
				Logging.tML.Warn("-steamworkshopfolder path not found: " + workshopLocCustom);
			}
			string steamClientPath = null;
			if (SteamedWraps.SteamClient)
			{
				SteamApps.GetAppInstallDir(app, ref steamClientPath, 1000U);
			}
			if (steamClientPath == null)
			{
				steamClientPath = ".";
			}
			steamClientPath = Path.Combine(steamClientPath, "..", "..", "workshop");
			if (SteamedWraps.SteamClient || (!SteamedWraps.SteamAvailable && !Program.LaunchParameters.ContainsKey("-nosteam") && Directory.Exists(steamClientPath)))
			{
				return steamClientPath;
			}
			return Path.Combine("steamapps", "workshop");
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x004BC540 File Offset: 0x004BA740
		internal static bool TryGetModDownloadItem(string modSlug, out ModDownloadItem item)
		{
			item = null;
			WorkshopHelper.QueryHelper.AQueryInstance query = new WorkshopHelper.QueryHelper.AQueryInstance(new QueryParameters
			{
				queryType = QueryType.SearchDirect
			});
			if (!query.TrySearchByInternalName(modSlug, out item))
			{
				return false;
			}
			if (item == null)
			{
				query.queryParameters.queryType = QueryType.SearchUserPublishedOnly;
				if (!query.TrySearchByInternalName(modSlug, out item))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x004BC590 File Offset: 0x004BA790
		internal static bool GetPublishIdLocal(TmodFile modFile, out ulong publishId)
		{
			publishId = 0UL;
			FoundWorkshopEntryInfo info;
			if (modFile == null || !ModOrganizer.TryReadManifest(ModOrganizer.GetParentDir(modFile.path), out info))
			{
				return false;
			}
			publishId = info.workshopEntryId;
			return true;
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x004BC5C4 File Offset: 0x004BA7C4
		internal static bool TryGetGroupPublishIdsByInternalName(QueryParameters query, out List<string> modIds)
		{
			modIds = new List<string>();
			List<ModDownloadItem> items;
			if (!new WorkshopHelper.QueryHelper.AQueryInstance(query).TryGroupSearchByInternalName(out items))
			{
				return false;
			}
			for (int i = 0; i < query.searchModSlugs.Length; i++)
			{
				if (items[i] == null)
				{
					Logging.tML.Info("Unable to find the PublishID for " + query.searchModSlugs[i]);
					modIds.Add("0");
				}
				else
				{
					modIds.Add(items[i].PublishId.m_ModPubId);
				}
			}
			return true;
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x004BC648 File Offset: 0x004BA848
		[return: TupleElementNames(new string[]
		{
			"modV",
			"tmlV"
		})]
		private static ValueTuple<Version, Version> CalculateRelevantVersion(string mbDescription, NameValueCollection metadata)
		{
			ValueTuple<Version, Version> selectVersion = new ValueTuple<Version, Version>(new Version(metadata["version"].Replace("v", "")), new Version(metadata["modloaderversion"].Replace("tModLoader v", "")));
			if (!metadata["versionsummary"].Contains(':'))
			{
				return selectVersion;
			}
			WorkshopHelper.InnerCalculateRelevantVersion(ref selectVersion, metadata["versionsummary"]);
			Match match = WorkshopHelper.MetadataInDescriptionFallbackRegex.Match(mbDescription);
			if (match.Success)
			{
				WorkshopHelper.InnerCalculateRelevantVersion(ref selectVersion, match.Groups[1].Value);
			}
			return selectVersion;
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x004BC6F0 File Offset: 0x004BA8F0
		private static void InnerCalculateRelevantVersion([TupleElementNames(new string[]
		{
			"modV",
			"tmlV"
		})] ref ValueTuple<Version, Version> selectVersion, string versionSummary)
		{
			foreach (ValueTuple<Version, Version> item in WorkshopHelper.VersionSummaryToArray(versionSummary))
			{
				if (!(item.Item1.MajorMinor() > BuildInfo.tMLVersion.MajorMinor()) && (selectVersion.Item1 < item.Item2 || selectVersion.Item2.MajorMinor() < item.Item1.MajorMinor()))
				{
					selectVersion.Item1 = item.Item2;
					selectVersion.Item2 = item.Item1;
				}
			}
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x004BC77E File Offset: 0x004BA97E
		[return: TupleElementNames(new string[]
		{
			"tmlVersion",
			"modVersion"
		})]
		private static ValueTuple<Version, Version>[] VersionSummaryToArray(string versionSummary)
		{
			return (from s in versionSummary.Split(";", StringSplitOptions.None)
			select new ValueTuple<Version, Version>(new Version(s.Split(":", StringSplitOptions.None)[0]), new Version(s.Split(":", StringSplitOptions.None)[1]))).ToArray<ValueTuple<Version, Version>>();
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x004BC7B8 File Offset: 0x004BA9B8
		internal static void PublishMod(LocalMod mod, string iconPath)
		{
			TmodFile modFile = mod.modFile;
			BuildProperties bp = mod.properties;
			if (bp.buildVersion != modFile.TModLoaderVersion)
			{
				throw new WebException(Language.GetTextValue("tModLoader.OutdatedModCantPublishError"));
			}
			string changeLogFile = Path.Combine(bp.modSource, "changelog.txt");
			string changeLog;
			if (File.Exists(changeLogFile))
			{
				changeLog = File.ReadAllText(changeLogFile);
			}
			else
			{
				changeLog = "";
			}
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection.Add("displayname", bp.displayName);
			nameValueCollection.Add("displaynameclean", Utils.CleanChatTags(bp.displayName));
			nameValueCollection.Add("name", modFile.Name);
			string name = "version";
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
			defaultInterpolatedStringHandler.AppendFormatted<Version>(bp.version);
			nameValueCollection.Add(name, defaultInterpolatedStringHandler.ToStringAndClear());
			nameValueCollection.Add("author", bp.author);
			nameValueCollection.Add("homepage", bp.homepage);
			nameValueCollection.Add("description", bp.description);
			nameValueCollection.Add("iconpath", iconPath);
			nameValueCollection.Add("sourcesfolder", bp.modSource);
			string name2 = "modloaderversion";
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
			defaultInterpolatedStringHandler.AppendFormatted<Version>(modFile.TModLoaderVersion);
			nameValueCollection.Add(name2, defaultInterpolatedStringHandler.ToStringAndClear());
			nameValueCollection.Add("modreferences", string.Join(", ", from x in bp.modReferences
			select x.mod));
			nameValueCollection.Add("modside", bp.side.ToFriendlyString());
			nameValueCollection.Add("changelog", changeLog);
			NameValueCollection values = nameValueCollection;
			if (string.IsNullOrWhiteSpace(values["author"]))
			{
				throw new WebException("You need to specify an author in build.txt");
			}
			if (string.IsNullOrWhiteSpace(values["version"]))
			{
				throw new WebException("You need to specify a version in build.txt");
			}
			if (!Main.dedServ)
			{
				Main.MenuUI.SetState(new WorkshopPublishInfoStateForMods(Interface.modSources, modFile, values));
				return;
			}
			try
			{
				SocialAPI.Workshop = new WorkshopSocialModule();
				SocialAPI.Workshop.Initialize();
				if (SteamedWraps.SteamClient)
				{
					Thread.Sleep(1500);
					WorkshopTagOption[] usedTags = Array.Empty<WorkshopTagOption>();
					WorkshopItemPublicSettingId publicity = WorkshopItemPublicSettingId.Public;
					FoundWorkshopEntryInfo info;
					if (SocialAPI.Workshop.TryGetInfoForMod(modFile, out info))
					{
						usedTags = (from tag in info.tags
						select new WorkshopTagOption(tag, tag)).ToArray<WorkshopTagOption>();
						publicity = info.publicity;
					}
					WorkshopItemPublishSettings publishSetttings = new WorkshopItemPublishSettings
					{
						Publicity = publicity,
						UsedTags = usedTags,
						PreviewImagePath = iconPath
					};
					SocialAPI.Workshop.PublishMod(modFile, values, publishSetttings);
				}
			}
			finally
			{
				SteamedWraps.OnGameExitCleanup();
			}
		}

		// Token: 0x04001370 RID: 4976
		internal static string[] MetadataKeys = new string[]
		{
			"name",
			"author",
			"modside",
			"homepage",
			"modloaderversion",
			"version",
			"modreferences",
			"versionsummary"
		};

		// Token: 0x04001371 RID: 4977
		private static readonly Regex MetadataInDescriptionFallbackRegex = new Regex("\\[quote=GithubActions\\(Don't Modify\\)\\]Version Summary: (.*) \\[/quote\\]", RegexOptions.Compiled);

		// Token: 0x02000890 RID: 2192
		public class UGCBased
		{
			// Token: 0x04006A09 RID: 27145
			public const string ManifestFileName = "workshop.json";

			// Token: 0x02000E13 RID: 3603
			public struct SteamWorkshopItem
			{
				// Token: 0x04007BFD RID: 31741
				public string ContentFolderPath;

				// Token: 0x04007BFE RID: 31742
				public string Description;

				// Token: 0x04007BFF RID: 31743
				public string PreviewImagePath;

				// Token: 0x04007C00 RID: 31744
				public string[] Tags;

				// Token: 0x04007C01 RID: 31745
				public string Title;

				// Token: 0x04007C02 RID: 31746
				public ERemoteStoragePublishedFileVisibility? Visibility;

				// Token: 0x04007C03 RID: 31747
				public NameValueCollection BuildData;

				// Token: 0x04007C04 RID: 31748
				public string ChangeNotes;
			}

			// Token: 0x02000E14 RID: 3604
			public class Downloader
			{
				// Token: 0x170009A9 RID: 2473
				// (get) Token: 0x06006508 RID: 25864 RVA: 0x006DD3FF File Offset: 0x006DB5FF
				// (set) Token: 0x06006509 RID: 25865 RVA: 0x006DD407 File Offset: 0x006DB607
				public List<string> ResourcePackPaths { get; private set; }

				// Token: 0x170009AA RID: 2474
				// (get) Token: 0x0600650A RID: 25866 RVA: 0x006DD410 File Offset: 0x006DB610
				// (set) Token: 0x0600650B RID: 25867 RVA: 0x006DD418 File Offset: 0x006DB618
				public List<string> WorldPaths { get; private set; }

				// Token: 0x170009AB RID: 2475
				// (get) Token: 0x0600650C RID: 25868 RVA: 0x006DD421 File Offset: 0x006DB621
				// (set) Token: 0x0600650D RID: 25869 RVA: 0x006DD429 File Offset: 0x006DB629
				public List<string> ModPaths { get; private set; }

				// Token: 0x0600650E RID: 25870 RVA: 0x006DD432 File Offset: 0x006DB632
				public Downloader()
				{
					this.ResourcePackPaths = new List<string>();
					this.WorldPaths = new List<string>();
					this.ModPaths = new List<string>();
				}

				// Token: 0x0600650F RID: 25871 RVA: 0x006DD45B File Offset: 0x006DB65B
				public static WorkshopHelper.UGCBased.Downloader Create()
				{
					return new WorkshopHelper.UGCBased.Downloader();
				}

				// Token: 0x06006510 RID: 25872 RVA: 0x006DD464 File Offset: 0x006DB664
				public List<string> GetListOfSubscribedItemsPaths()
				{
					IEnumerable<string> source = from app in new AppId_t[]
					{
						Steam.TMLAppID_t,
						Steam.TerrariaAppId_t
					}
					select Path.Combine(WorkshopHelper.GetWorkshopFolder(app), "content", app.ToString());
					Func<string, bool> predicate;
					if ((predicate = WorkshopHelper.UGCBased.Downloader.<>O.<0>__Exists) == null)
					{
						predicate = (WorkshopHelper.UGCBased.Downloader.<>O.<0>__Exists = new Func<string, bool>(Directory.Exists));
					}
					IEnumerable<string> source2 = source.Where(predicate);
					Func<string, IEnumerable<string>> selector;
					if ((selector = WorkshopHelper.UGCBased.Downloader.<>O.<1>__EnumerateDirectories) == null)
					{
						selector = (WorkshopHelper.UGCBased.Downloader.<>O.<1>__EnumerateDirectories = new Func<string, IEnumerable<string>>(Directory.EnumerateDirectories));
					}
					return source2.SelectMany(selector).ToList<string>();
				}

				// Token: 0x06006511 RID: 25873 RVA: 0x006DD4F8 File Offset: 0x006DB6F8
				public bool Prepare(WorkshopIssueReporter issueReporter)
				{
					return this.Refresh(issueReporter);
				}

				// Token: 0x06006512 RID: 25874 RVA: 0x006DD504 File Offset: 0x006DB704
				public bool Refresh(WorkshopIssueReporter issueReporter)
				{
					this.ResourcePackPaths.Clear();
					this.WorldPaths.Clear();
					this.ModPaths.Clear();
					foreach (string listOfSubscribedItemsPath in this.GetListOfSubscribedItemsPaths())
					{
						if (listOfSubscribedItemsPath != null)
						{
							try
							{
								string path = listOfSubscribedItemsPath + Path.DirectorySeparatorChar.ToString() + "workshop.json";
								if (File.Exists(path))
								{
									string text = AWorkshopEntry.ReadHeader(File.ReadAllText(path));
									if (!(text == "World"))
									{
										if (text == "ResourcePack")
										{
											this.ResourcePackPaths.Add(listOfSubscribedItemsPath);
										}
										else if (text == "Mod")
										{
											this.ModPaths.Add(listOfSubscribedItemsPath);
										}
									}
									else
									{
										this.WorldPaths.Add(listOfSubscribedItemsPath);
									}
								}
							}
							catch (Exception exception)
							{
								issueReporter.ReportDownloadProblem("Workshop.ReportIssue_FailedToLoadSubscribedFile", listOfSubscribedItemsPath, exception);
								return false;
							}
						}
					}
					return true;
				}

				// Token: 0x02000E8A RID: 3722
				[CompilerGenerated]
				private static class <>O
				{
					// Token: 0x04007DBE RID: 32190
					public static Func<string, bool> <0>__Exists;

					// Token: 0x04007DBF RID: 32191
					[Nullable(new byte[]
					{
						0,
						0,
						1,
						0
					})]
					public static Func<string, IEnumerable<string>> <1>__EnumerateDirectories;
				}
			}

			// Token: 0x02000E15 RID: 3605
			public class PublishedItemsFinder
			{
				// Token: 0x06006513 RID: 25875 RVA: 0x006DD620 File Offset: 0x006DB820
				public bool HasItemOfId(ulong id)
				{
					return this._items.ContainsKey(id);
				}

				// Token: 0x06006514 RID: 25876 RVA: 0x006DD62E File Offset: 0x006DB82E
				public static WorkshopHelper.UGCBased.PublishedItemsFinder Create()
				{
					WorkshopHelper.UGCBased.PublishedItemsFinder publishedItemsFinder = new WorkshopHelper.UGCBased.PublishedItemsFinder();
					publishedItemsFinder.LoadHooks();
					return publishedItemsFinder;
				}

				// Token: 0x06006515 RID: 25877 RVA: 0x006DD63B File Offset: 0x006DB83B
				private void LoadHooks()
				{
					this.OnSteamUGCQueryCompletedCallResult = CallResult<SteamUGCQueryCompleted_t>.Create(new CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate(this.OnSteamUGCQueryCompleted));
					this.OnSteamUGCRequestUGCDetailsResultCallResult = CallResult<SteamUGCRequestUGCDetailsResult_t>.Create(new CallResult<SteamUGCRequestUGCDetailsResult_t>.APIDispatchDelegate(this.OnSteamUGCRequestUGCDetailsResult));
				}

				// Token: 0x06006516 RID: 25878 RVA: 0x006DD66B File Offset: 0x006DB86B
				public void Prepare()
				{
					this.Refresh();
				}

				// Token: 0x06006517 RID: 25879 RVA: 0x006DD674 File Offset: 0x006DB874
				public void Refresh()
				{
					this.m_UGCQueryHandle = SteamUGC.CreateQueryUserUGCRequest(SteamUser.GetSteamID().GetAccountID(), 0, -1, 0, SteamUtils.GetAppID(), SteamUtils.GetAppID(), 1U);
					CoreSocialModule.SetSkipPulsing(true);
					SteamAPICall_t hAPICall = SteamUGC.SendQueryUGCRequest(this.m_UGCQueryHandle);
					this.OnSteamUGCQueryCompletedCallResult.Set(hAPICall, new CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate(this.OnSteamUGCQueryCompleted));
					CoreSocialModule.SetSkipPulsing(false);
				}

				// Token: 0x06006518 RID: 25880 RVA: 0x006DD6D8 File Offset: 0x006DB8D8
				private void OnSteamUGCQueryCompleted(SteamUGCQueryCompleted_t pCallback, bool bIOFailure)
				{
					this._items.Clear();
					if (bIOFailure || pCallback.m_eResult != 1)
					{
						SteamUGC.ReleaseQueryUGCRequest(this.m_UGCQueryHandle);
						return;
					}
					for (uint num = 0U; num < pCallback.m_unNumResultsReturned; num += 1U)
					{
						SteamUGCDetails_t pDetails;
						SteamUGC.GetQueryUGCResult(this.m_UGCQueryHandle, num, ref pDetails);
						ulong publishedFileId = pDetails.m_nPublishedFileId.m_PublishedFileId;
						WorkshopHelper.UGCBased.SteamWorkshopItem value = new WorkshopHelper.UGCBased.SteamWorkshopItem
						{
							Title = pDetails.m_rgchTitle,
							Description = pDetails.m_rgchDescription
						};
						this._items.Add(publishedFileId, value);
					}
					SteamUGC.ReleaseQueryUGCRequest(this.m_UGCQueryHandle);
				}

				// Token: 0x06006519 RID: 25881 RVA: 0x006DD776 File Offset: 0x006DB976
				private void OnSteamUGCRequestUGCDetailsResult(SteamUGCRequestUGCDetailsResult_t pCallback, bool bIOFailure)
				{
				}

				// Token: 0x04007C08 RID: 31752
				private Dictionary<ulong, WorkshopHelper.UGCBased.SteamWorkshopItem> _items = new Dictionary<ulong, WorkshopHelper.UGCBased.SteamWorkshopItem>();

				// Token: 0x04007C09 RID: 31753
				private UGCQueryHandle_t m_UGCQueryHandle;

				// Token: 0x04007C0A RID: 31754
				private CallResult<SteamUGCQueryCompleted_t> OnSteamUGCQueryCompletedCallResult;

				// Token: 0x04007C0B RID: 31755
				private CallResult<SteamUGCRequestUGCDetailsResult_t> OnSteamUGCRequestUGCDetailsResultCallResult;
			}

			// Token: 0x02000E16 RID: 3606
			public abstract class APublisherInstance
			{
				// Token: 0x0600651B RID: 25883 RVA: 0x006DD78C File Offset: 0x006DB98C
				public void PublishContent(WorkshopHelper.UGCBased.PublishedItemsFinder finder, WorkshopIssueReporter issueReporter, WorkshopHelper.UGCBased.APublisherInstance.FinishedPublishingAction endAction, string itemTitle, string itemDescription, string contentFolderPath, string previewImagePath, WorkshopItemPublicSettingId publicity, string[] tags, NameValueCollection buildData = null, ulong existingID = 0UL, string changeNotes = null)
				{
					Utils.LogAndConsoleInfoMessage(Language.GetTextValueWith("tModLoader.PublishItem", this._entryData.Title));
					this._issueReporter = issueReporter;
					this._endAction = endAction;
					this._createItemHook = CallResult<CreateItemResult_t>.Create(new CallResult<CreateItemResult_t>.APIDispatchDelegate(this.CreateItemResult));
					this._updateItemHook = CallResult<SubmitItemUpdateResult_t>.Create(new CallResult<SubmitItemUpdateResult_t>.APIDispatchDelegate(this.UpdateItemResult));
					this._publicity = publicity;
					ERemoteStoragePublishedFileVisibility visibility = this.GetVisibility(publicity);
					this._entryData = new WorkshopHelper.UGCBased.SteamWorkshopItem
					{
						Title = itemTitle,
						Description = itemDescription,
						ContentFolderPath = contentFolderPath,
						Tags = tags,
						PreviewImagePath = previewImagePath,
						Visibility = new ERemoteStoragePublishedFileVisibility?(visibility),
						BuildData = buildData,
						ChangeNotes = changeNotes
					};
					if (!File.Exists(previewImagePath))
					{
						this._issueReporter.ReportInstantUploadProblem("Workshop.ReportIssue_FailedToPublish_CouldNotFindFolderToUpload");
						return;
					}
					if (!Directory.Exists(contentFolderPath))
					{
						this._issueReporter.ReportInstantUploadProblem("Workshop.ReportIssue_FailedToPublish_CouldNotFindFolderToUpload");
						return;
					}
					this._publishedFileID = new PublishedFileId_t(existingID);
					if (!this.WrappedWriteManifest())
					{
						return;
					}
					if (this._publishedFileID.m_PublishedFileId != 0UL)
					{
						if (buildData == null)
						{
							this.PreventUpdatingCertainThings();
						}
						this.UpdateItem(false);
						return;
					}
					this.cleanTemporaryFolder = true;
					this.CreateItem();
				}

				// Token: 0x0600651C RID: 25884 RVA: 0x006DD8D1 File Offset: 0x006DBAD1
				private void PreventUpdatingCertainThings()
				{
					this._entryData.Title = null;
					this._entryData.Description = null;
				}

				// Token: 0x0600651D RID: 25885 RVA: 0x006DD8EB File Offset: 0x006DBAEB
				private ERemoteStoragePublishedFileVisibility GetVisibility(WorkshopItemPublicSettingId publicityId)
				{
					switch (publicityId)
					{
					case WorkshopItemPublicSettingId.FriendsOnly:
						return 1;
					case WorkshopItemPublicSettingId.Public:
						return 0;
					case WorkshopItemPublicSettingId.Unlisted:
						return 3;
					default:
						return 2;
					}
				}

				// Token: 0x0600651E RID: 25886 RVA: 0x006DD908 File Offset: 0x006DBB08
				private void CreateItem()
				{
					Utils.LogAndConsoleInfoMessage(Language.GetTextValue("tModLoader.CreateItem", this._entryData.Title));
					this._createCallback = 0;
					CoreSocialModule.SetSkipPulsing(true);
					SteamAPICall_t hAPICall = SteamUGC.CreateItem(SteamUtils.GetAppID(), 0);
					this._createItemHook.Set(hAPICall, new CallResult<CreateItemResult_t>.APIDispatchDelegate(this.CreateItemResult));
					CoreSocialModule.SetSkipPulsing(false);
					if (!Main.dedServ)
					{
						return;
					}
					do
					{
						Thread.Sleep(1);
						SteamedWraps.RunCallbacks();
					}
					while (this._createCallback == null);
					if (this._createCallback != 1)
					{
						return;
					}
					this.UpdateItem(true);
					do
					{
						Thread.Sleep(1);
						SteamedWraps.RunCallbacks();
					}
					while (this._createCallback == null);
				}

				// Token: 0x0600651F RID: 25887 RVA: 0x006DD9A8 File Offset: 0x006DBBA8
				private void CreateItemResult(CreateItemResult_t param, bool bIOFailure)
				{
					this._createCallback = param.m_eResult;
					if (param.m_eResult != 1)
					{
						this._issueReporter.ReportDelayedUploadProblemWithoutKnownReason("Workshop.ReportIssue_FailedToPublish_WithoutKnownReason", param.m_eResult.ToString());
						SteamedWraps.ReportCheckSteamLogs();
						this._endAction(this);
						return;
					}
					this._publishedFileID = param.m_nPublishedFileId;
					this.WrappedWriteManifest();
					if (param.m_bUserNeedsToAcceptWorkshopLegalAgreement)
					{
						this._issueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_FailedToPublish_UserDidNotAcceptWorkshopTermsOfService");
						this._endAction(this);
						return;
					}
					if (!Main.dedServ)
					{
						this.UpdateItem(false);
					}
				}

				// Token: 0x06006520 RID: 25888
				protected abstract string GetHeaderText();

				// Token: 0x06006521 RID: 25889
				protected abstract void PrepareContentForUpdate();

				// Token: 0x06006522 RID: 25890 RVA: 0x006DDA44 File Offset: 0x006DBC44
				private void UpdateItem(bool creatingItem = false)
				{
					this.WrappedWriteManifest();
					Utils.LogAndConsoleInfoMessage(Language.GetTextValue("tModLoader.UpdateItem", this._entryData.Title));
					this.PrepareContentForUpdate();
					UGCUpdateHandle_t uGCUpdateHandle_t = this.GenerateUgcUpdateHandle();
					this._updateCallback = 0;
					Logging.tML.Info("Submitting workshop update handle to Steam");
					CoreSocialModule.SetSkipPulsing(true);
					SteamAPICall_t hAPICall = SteamUGC.SubmitItemUpdate(uGCUpdateHandle_t, this._entryData.ChangeNotes);
					this._updateHandle = uGCUpdateHandle_t;
					this._updateItemHook.Set(hAPICall, new CallResult<SubmitItemUpdateResult_t>.APIDispatchDelegate(this.UpdateItemResult));
					CoreSocialModule.SetSkipPulsing(false);
					Logging.tML.Info("Handle submitted. Waiting on Steam");
					if (!Main.dedServ || creatingItem)
					{
						return;
					}
					do
					{
						Thread.Sleep(1);
						SteamedWraps.RunCallbacks();
					}
					while (this._updateCallback == null);
				}

				// Token: 0x06006523 RID: 25891 RVA: 0x006DDB04 File Offset: 0x006DBD04
				internal UGCUpdateHandle_t GenerateUgcUpdateHandle()
				{
					UGCUpdateHandle_t uGCUpdateHandle_t = SteamUGC.StartItemUpdate(SteamUtils.GetAppID(), this._publishedFileID);
					SteamedWraps.ModifyUgcUpdateHandleCommon(ref uGCUpdateHandle_t, this._entryData);
					if (this._entryData.BuildData != null)
					{
						this._entryData.BuildData["version"] = "0.0.0";
						SteamedWraps.ModifyUgcUpdateHandleTModLoader(ref uGCUpdateHandle_t, this._entryData, this._publishedFileID);
					}
					return uGCUpdateHandle_t;
				}

				// Token: 0x06006524 RID: 25892 RVA: 0x006DDB6C File Offset: 0x006DBD6C
				private void UpdateItemResult(SubmitItemUpdateResult_t param, bool bIOFailure)
				{
					this._updateCallback = param.m_eResult;
					if (param.m_bUserNeedsToAcceptWorkshopLegalAgreement)
					{
						this._issueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_FailedToPublish_UserDidNotAcceptWorkshopTermsOfService");
						this._endAction(this);
						return;
					}
					if (this._updateCallback != 1)
					{
						SteamedWraps.ReportCheckSteamLogs();
					}
					EResult updateCallback = this._updateCallback;
					if (updateCallback <= 9)
					{
						if (updateCallback == 1)
						{
							SteamFriends.ActivateGameOverlayToWebPage("steam://url/CommunityFilePage/" + this._publishedFileID.m_PublishedFileId.ToString(), 0);
							goto IL_10F;
						}
						if (updateCallback == 8)
						{
							this._issueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_FailedToPublish_InvalidParametersForPublishing");
							goto IL_10F;
						}
						if (updateCallback == 9)
						{
							this._issueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_FailedToPublish_CouldNotFindFolderToUpload");
							goto IL_10F;
						}
					}
					else
					{
						if (updateCallback == 15)
						{
							this._issueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_FailedToPublish_AccessDeniedBecauseUserDoesntOwnLicenseForApp");
							goto IL_10F;
						}
						if (updateCallback == 25)
						{
							this._issueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_FailedToPublish_LimitExceeded");
							goto IL_10F;
						}
						if (updateCallback == 33)
						{
							this._issueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_FailedToPublish_SteamFileLockFailed");
							goto IL_10F;
						}
					}
					this._issueReporter.ReportDelayedUploadProblemWithoutKnownReason("Workshop.ReportIssue_FailedToPublish_WithoutKnownReason", param.m_eResult.ToString());
					IL_10F:
					SteamUGC.SubscribeItem(this._publishedFileID);
					if (this.cleanTemporaryFolder)
					{
						Directory.Delete(this._entryData.ContentFolderPath, true);
					}
					this._endAction(this);
				}

				// Token: 0x06006525 RID: 25893 RVA: 0x006DDCBC File Offset: 0x006DBEBC
				private bool TryWritingManifestToFolder(string folderPath, string manifestText)
				{
					string path = folderPath + Path.DirectorySeparatorChar.ToString() + "workshop.json";
					bool result = true;
					bool result2;
					try
					{
						File.WriteAllText(path, manifestText);
						result2 = result;
					}
					catch (Exception exception)
					{
						this._issueReporter.ReportManifestCreationProblem("Workshop.ReportIssue_CouldNotCreateResourcePackManifestFile", exception);
						result2 = false;
					}
					return result2;
				}

				// Token: 0x06006526 RID: 25894 RVA: 0x006DDD14 File Offset: 0x006DBF14
				private bool WrappedWriteManifest()
				{
					string headerText = this.GetHeaderText();
					if (this.TryWritingManifestToFolder(this._entryData.ContentFolderPath, headerText))
					{
						return true;
					}
					this._endAction(this);
					return false;
				}

				// Token: 0x06006527 RID: 25895 RVA: 0x006DDD4C File Offset: 0x006DBF4C
				public bool TryGetProgress(out float progress)
				{
					progress = 0f;
					if (this._updateHandle == default(UGCUpdateHandle_t))
					{
						return false;
					}
					ulong punBytesProcessed;
					ulong punBytesTotal;
					SteamUGC.GetItemUpdateProgress(this._updateHandle, ref punBytesProcessed, ref punBytesTotal);
					if (punBytesTotal == 0UL)
					{
						return false;
					}
					progress = (float)(punBytesProcessed / punBytesTotal);
					return true;
				}

				// Token: 0x04007C0C RID: 31756
				protected WorkshopItemPublicSettingId _publicity;

				// Token: 0x04007C0D RID: 31757
				protected WorkshopHelper.UGCBased.SteamWorkshopItem _entryData;

				// Token: 0x04007C0E RID: 31758
				protected bool _isOwner;

				// Token: 0x04007C0F RID: 31759
				protected PublishedFileId_t _publishedFileID;

				// Token: 0x04007C10 RID: 31760
				protected EResult _createCallback;

				// Token: 0x04007C11 RID: 31761
				protected EResult _updateCallback;

				// Token: 0x04007C12 RID: 31762
				private bool cleanTemporaryFolder;

				// Token: 0x04007C13 RID: 31763
				private UGCUpdateHandle_t _updateHandle;

				// Token: 0x04007C14 RID: 31764
				private CallResult<CreateItemResult_t> _createItemHook;

				// Token: 0x04007C15 RID: 31765
				private CallResult<SubmitItemUpdateResult_t> _updateItemHook;

				// Token: 0x04007C16 RID: 31766
				private WorkshopHelper.UGCBased.APublisherInstance.FinishedPublishingAction _endAction;

				// Token: 0x04007C17 RID: 31767
				private WorkshopIssueReporter _issueReporter;

				// Token: 0x02000E8C RID: 3724
				// (Invoke) Token: 0x060066D5 RID: 26325
				public delegate void FinishedPublishingAction(WorkshopHelper.UGCBased.APublisherInstance instance);
			}

			// Token: 0x02000E17 RID: 3607
			public class ResourcePackPublisherInstance : WorkshopHelper.UGCBased.APublisherInstance
			{
				// Token: 0x06006529 RID: 25897 RVA: 0x006DDDA0 File Offset: 0x006DBFA0
				public ResourcePackPublisherInstance(ResourcePack resourcePack)
				{
					this._resourcePack = resourcePack;
				}

				// Token: 0x0600652A RID: 25898 RVA: 0x006DDDAF File Offset: 0x006DBFAF
				protected override string GetHeaderText()
				{
					return TexturePackWorkshopEntry.GetHeaderTextFor(this._resourcePack, this._publishedFileID.m_PublishedFileId, this._entryData.Tags, this._publicity, this._entryData.PreviewImagePath);
				}

				// Token: 0x0600652B RID: 25899 RVA: 0x006DDDE3 File Offset: 0x006DBFE3
				protected override void PrepareContentForUpdate()
				{
				}

				// Token: 0x04007C18 RID: 31768
				private ResourcePack _resourcePack;
			}

			// Token: 0x02000E18 RID: 3608
			public class WorldPublisherInstance : WorkshopHelper.UGCBased.APublisherInstance
			{
				// Token: 0x0600652C RID: 25900 RVA: 0x006DDDE5 File Offset: 0x006DBFE5
				public WorldPublisherInstance(WorldFileData world)
				{
					this._world = world;
				}

				// Token: 0x0600652D RID: 25901 RVA: 0x006DDDF4 File Offset: 0x006DBFF4
				protected override string GetHeaderText()
				{
					return WorldWorkshopEntry.GetHeaderTextFor(this._world, this._publishedFileID.m_PublishedFileId, this._entryData.Tags, this._publicity, this._entryData.PreviewImagePath);
				}

				// Token: 0x0600652E RID: 25902 RVA: 0x006DDE28 File Offset: 0x006DC028
				protected override void PrepareContentForUpdate()
				{
					if (this._world.IsCloudSave)
					{
						FileUtilities.CopyToLocal(this._world.Path, this._entryData.ContentFolderPath + Path.DirectorySeparatorChar.ToString() + "world.wld");
						return;
					}
					FileUtilities.Copy(this._world.Path, this._entryData.ContentFolderPath + Path.DirectorySeparatorChar.ToString() + "world.wld", false, true);
				}

				// Token: 0x04007C19 RID: 31769
				private WorldFileData _world;
			}
		}

		// Token: 0x02000891 RID: 2193
		public class ModPublisherInstance : WorkshopHelper.UGCBased.APublisherInstance
		{
			// Token: 0x060051DA RID: 20954 RVA: 0x0069866E File Offset: 0x0069686E
			protected override string GetHeaderText()
			{
				return ModWorkshopEntry.GetHeaderTextFor(this._publishedFileID.m_PublishedFileId, this._entryData.Tags, this._publicity, this._entryData.PreviewImagePath);
			}

			// Token: 0x060051DB RID: 20955 RVA: 0x0069869C File Offset: 0x0069689C
			protected override void PrepareContentForUpdate()
			{
			}
		}

		// Token: 0x02000892 RID: 2194
		internal static class QueryHelper
		{
			// Token: 0x060051DD RID: 20957 RVA: 0x006986A6 File Offset: 0x006968A6
			[AsyncIteratorStateMachine(typeof(WorkshopHelper.QueryHelper.<QueryWorkshop>d__0))]
			internal static IAsyncEnumerable<ModDownloadItem> QueryWorkshop(QueryParameters queryParams, [EnumeratorCancellation] CancellationToken token)
			{
				WorkshopHelper.QueryHelper.<QueryWorkshop>d__0 <QueryWorkshop>d__ = new WorkshopHelper.QueryHelper.<QueryWorkshop>d__0(-2);
				<QueryWorkshop>d__.<>3__queryParams = queryParams;
				<QueryWorkshop>d__.<>3__token = token;
				return <QueryWorkshop>d__;
			}

			// Token: 0x02000E19 RID: 3609
			internal class AQueryInstance
			{
				// Token: 0x0600652F RID: 25903 RVA: 0x006DDEA4 File Offset: 0x006DC0A4
				internal AQueryInstance(QueryParameters queryParameters)
				{
					this._queryHook = CallResult<SteamUGCQueryCompleted_t>.Create(new CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate(this.OnWorkshopQueryInitialized));
					this.queryParameters = queryParameters;
				}

				// Token: 0x06006530 RID: 25904 RVA: 0x006DDED8 File Offset: 0x006DC0D8
				private void OnWorkshopQueryInitialized(SteamUGCQueryCompleted_t pCallback, bool bIOFailure)
				{
					this._primaryUGCHandle = pCallback.m_handle;
					this._primaryQueryResult = pCallback.m_eResult;
					this._queryReturnCount = pCallback.m_unNumResultsReturned;
					if (this.totalItemsQueried == 0U && pCallback.m_unTotalMatchingResults > 0U)
					{
						this.totalItemsQueried = pCallback.m_unTotalMatchingResults;
						this.numberPages = (int)Math.Ceiling(this.totalItemsQueried / 50.0);
					}
				}

				/// <summary>
				/// Ought be called to release the existing query when we are done with it. Frees memory associated with the handle.
				/// </summary>
				// Token: 0x06006531 RID: 25905 RVA: 0x006DDF44 File Offset: 0x006DC144
				private void ReleaseWorkshopQuery()
				{
					SteamedWraps.ReleaseWorkshopHandle(this._primaryUGCHandle);
				}

				/// <summary>
				/// For direct information gathering of particular mod/workshop items. Synchronous.
				/// Note that the List size is 1 to 1 with the provided array.
				/// If the Mod is not found, the space is filled with a null.
				/// </summary>
				// Token: 0x06006532 RID: 25906 RVA: 0x006DDF54 File Offset: 0x006DC154
				internal List<ModDownloadItem> QueryItemsSynchronously(out List<string> missingMods)
				{
					double numPages = Math.Ceiling((double)((float)this.queryParameters.searchModIds.Length / 50f));
					List<ModDownloadItem> items = new List<ModDownloadItem>();
					missingMods = new List<string>();
					int i = 0;
					while ((double)i < numPages)
					{
						string[] idArray = (from x in this.queryParameters.searchModIds.Take(new Range(i * 50, 50 * (i + 1)))
						select x.m_ModPubId).ToArray<string>();
						try
						{
							this.WaitForQueryResult(SteamedWraps.GenerateDirectItemsQuery(idArray));
							int j = 0;
							while ((long)j < (long)((ulong)this._queryReturnCount))
							{
								ModDownloadItem item = this.GenerateModDownloadItemFromQuery((uint)j);
								if (item == null)
								{
									Logging.tML.Warn("Unable to find Mod with ID " + idArray[j] + " on the Steam Workshop");
									missingMods.Add(idArray[j]);
								}
								else
								{
									item.UpdateInstallState();
									items.Add(item);
								}
								j++;
							}
						}
						finally
						{
							this.ReleaseWorkshopQuery();
						}
						i++;
					}
					return items;
				}

				// Token: 0x06006533 RID: 25907 RVA: 0x006DE074 File Offset: 0x006DC274
				[AsyncIteratorStateMachine(typeof(WorkshopHelper.QueryHelper.AQueryInstance.<QueryAllWorkshopItems>d__12))]
				internal IAsyncEnumerable<ModDownloadItem> QueryAllWorkshopItems([EnumeratorCancellation] CancellationToken token = default(CancellationToken))
				{
					WorkshopHelper.QueryHelper.AQueryInstance.<QueryAllWorkshopItems>d__12 <QueryAllWorkshopItems>d__ = new WorkshopHelper.QueryHelper.AQueryInstance.<QueryAllWorkshopItems>d__12(-2);
					<QueryAllWorkshopItems>d__.<>4__this = this;
					<QueryAllWorkshopItems>d__.<>3__token = token;
					return <QueryAllWorkshopItems>d__;
				}

				// Token: 0x06006534 RID: 25908 RVA: 0x006DE08B File Offset: 0x006DC28B
				private IEnumerable<ModDownloadItem> ProcessPageResult()
				{
					uint num;
					for (uint i = 0U; i < this._queryReturnCount; i = num + 1U)
					{
						ModDownloadItem mod = this.GenerateModDownloadItemFromQuery(i);
						if (mod != null)
						{
							yield return mod;
						}
						num = i;
					}
					yield break;
				}

				/// <summary>
				/// Only Use if we don't have a PublishID source.
				/// Outputs a List of ModDownloadItems of equal length to QueryParameters.SearchModSlugs
				/// Uses null entries to fill gaps to ensure length consistency
				/// </summary>
				// Token: 0x06006535 RID: 25909 RVA: 0x006DE09C File Offset: 0x006DC29C
				internal bool TryGroupSearchByInternalName(out List<ModDownloadItem> items)
				{
					items = new List<ModDownloadItem>();
					foreach (string slug in this.queryParameters.searchModSlugs)
					{
						ModDownloadItem item;
						if (!this.TrySearchByInternalName(slug, out item))
						{
							return false;
						}
						items.Add(item);
					}
					return true;
				}

				/// <summary>
				/// Only Use if we don't have a PublishID source.
				/// Returns false if unable to check Workshop and outs item as null if item not found
				/// </summary>
				// Token: 0x06006536 RID: 25910 RVA: 0x006DE0E4 File Offset: 0x006DC2E4
				internal bool TrySearchByInternalName(string slug, out ModDownloadItem item)
				{
					item = null;
					bool result;
					try
					{
						this.WaitForQueryResult(SteamedWraps.GenerateAndSubmitModBrowserQuery(1U, this.queryParameters, slug));
						if (this._queryReturnCount == 0U)
						{
							Logging.tML.Info("No Mod on Workshop with internal name: " + slug);
							result = true;
						}
						else
						{
							item = this.GenerateModDownloadItemFromQuery(0U);
							result = true;
						}
					}
					catch
					{
						result = false;
					}
					finally
					{
						this.ReleaseWorkshopQuery();
					}
					return result;
				}

				// Token: 0x06006537 RID: 25911 RVA: 0x006DE160 File Offset: 0x006DC360
				internal Task WaitForQueryResultAsync(SteamAPICall_t query, CancellationToken token = default(CancellationToken))
				{
					WorkshopHelper.QueryHelper.AQueryInstance.<WaitForQueryResultAsync>d__16 <WaitForQueryResultAsync>d__;
					<WaitForQueryResultAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
					<WaitForQueryResultAsync>d__.<>4__this = this;
					<WaitForQueryResultAsync>d__.query = query;
					<WaitForQueryResultAsync>d__.token = token;
					<WaitForQueryResultAsync>d__.<>1__state = -1;
					<WaitForQueryResultAsync>d__.<>t__builder.Start<WorkshopHelper.QueryHelper.AQueryInstance.<WaitForQueryResultAsync>d__16>(ref <WaitForQueryResultAsync>d__);
					return <WaitForQueryResultAsync>d__.<>t__builder.Task;
				}

				// Token: 0x06006538 RID: 25912 RVA: 0x006DE1B4 File Offset: 0x006DC3B4
				[Obsolete("Should not be used because it hides syncronous waiting")]
				internal void WaitForQueryResult(SteamAPICall_t query)
				{
					this.WaitForQueryResultAsync(query, default(CancellationToken)).GetAwaiter().GetResult();
				}

				// Token: 0x06006539 RID: 25913 RVA: 0x006DE1E0 File Offset: 0x006DC3E0
				internal ModDownloadItem GenerateModDownloadItemFromQuery(uint i)
				{
					SteamUGCDetails_t pDetails = SteamedWraps.FetchItemDetails(this._primaryUGCHandle, i);
					PublishedFileId_t id = pDetails.m_nPublishedFileId;
					if (pDetails.m_eResult != 1)
					{
						ILog tML = Logging.tML;
						string str = "Unable to fetch mod PublishId#";
						PublishedFileId_t publishedFileId_t = id;
						tML.Warn(str + publishedFileId_t.ToString() + " information. " + pDetails.m_eResult.ToString());
						return null;
					}
					string ownerId = pDetails.m_ulSteamIDOwner.ToString();
					DateTime lastUpdate = Utils.UnixTimeStampToDateTime((long)((ulong)pDetails.m_rtimeUpdated));
					string displayname = pDetails.m_rgchTitle;
					NameValueCollection metadata;
					SteamedWraps.FetchMetadata(this._primaryUGCHandle, i, out metadata);
					if (metadata["versionsummary"] == null)
					{
						metadata["versionsummary"] = metadata["version"];
					}
					string[] missingKeys = (from k in WorkshopHelper.MetadataKeys
					where metadata.Get(k) == null
					select k).ToArray<string>();
					if (missingKeys.Length != 0)
					{
						ILog tML2 = Logging.tML;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 2);
						defaultInterpolatedStringHandler.AppendLiteral("Mod '");
						defaultInterpolatedStringHandler.AppendFormatted(displayname);
						defaultInterpolatedStringHandler.AppendLiteral("' is missing required metadata: ");
						defaultInterpolatedStringHandler.AppendFormatted(string.Join<string>(',', from k in missingKeys
						select "'" + k + "'"));
						defaultInterpolatedStringHandler.AppendLiteral(".");
						tML2.Warn(defaultInterpolatedStringHandler.ToStringAndClear());
						return null;
					}
					if (string.IsNullOrWhiteSpace(metadata["name"]))
					{
						ILog tML3 = Logging.tML;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 1);
						defaultInterpolatedStringHandler.AppendLiteral("Mod has no name: ");
						defaultInterpolatedStringHandler.AppendFormatted<PublishedFileId_t>(id);
						tML3.Warn(defaultInterpolatedStringHandler.ToStringAndClear());
						return null;
					}
					string[] refsById = (from x in SteamedWraps.FetchItemDependencies(this._primaryUGCHandle, i, pDetails.m_unNumChildren)
					select x.m_PublishedFileId.ToString()).ToArray<string>();
					ValueTuple<Version, Version> cVersion = WorkshopHelper.CalculateRelevantVersion(pDetails.m_rgchDescription, metadata);
					ModSide modside = ModSide.Both;
					if (metadata["modside"] == "Client")
					{
						modside = ModSide.Client;
					}
					if (metadata["modside"] == "Server")
					{
						modside = ModSide.Server;
					}
					if (metadata["modside"] == "NoSync")
					{
						modside = ModSide.NoSync;
					}
					string modIconURL;
					SteamedWraps.FetchPreviewImageUrl(this._primaryUGCHandle, i, out modIconURL);
					ulong hot;
					ulong downloads;
					SteamedWraps.FetchPlayTimeStats(this._primaryUGCHandle, i, out hot, out downloads);
					return new ModDownloadItem(displayname, metadata["name"], cVersion.Item1, metadata["author"], metadata["modreferences"], modside, modIconURL, id.m_PublishedFileId.ToString(), (int)downloads, (int)hot, lastUpdate, cVersion.Item2, metadata["homepage"], ownerId, refsById);
				}

				// Token: 0x04007C1A RID: 31770
				private CallResult<SteamUGCQueryCompleted_t> _queryHook;

				// Token: 0x04007C1B RID: 31771
				protected UGCQueryHandle_t _primaryUGCHandle;

				// Token: 0x04007C1C RID: 31772
				protected EResult _primaryQueryResult;

				// Token: 0x04007C1D RID: 31773
				protected uint _queryReturnCount;

				// Token: 0x04007C1E RID: 31774
				internal List<ulong> ugcChildren = new List<ulong>();

				// Token: 0x04007C1F RID: 31775
				internal QueryParameters queryParameters;

				// Token: 0x04007C20 RID: 31776
				internal int numberPages;

				// Token: 0x04007C21 RID: 31777
				internal uint totalItemsQueried;
			}
		}
	}
}
