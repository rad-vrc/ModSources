using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Steamworks;
using Terraria.DataStructures;
using Terraria.GameContent.UI.States;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.UI.ModBrowser;
using Terraria.Social.Base;
using Terraria.Utilities;

namespace Terraria.Social.Steam
{
	// Token: 0x020000F5 RID: 245
	public class WorkshopSocialModule : WorkshopSocialModule
	{
		// Token: 0x06001881 RID: 6273 RVA: 0x004BCB78 File Offset: 0x004BAD78
		public override void Initialize()
		{
			base.Branding = new WorkshopBranding
			{
				ResourcePackBrand = ResourcePack.BrandingType.SteamWorkshop
			};
			this._publisherInstances = new List<WorkshopHelper.UGCBased.APublisherInstance>();
			base.ProgressReporter = new WorkshopProgressReporter(this._publisherInstances);
			base.SupportedTags = new SupportedWorkshopTags();
			this._contentBaseFolder = Main.SavePath + Path.DirectorySeparatorChar.ToString() + "Workshop";
			this._downloader = WorkshopHelper.UGCBased.Downloader.Create();
			this._publishedItems = WorkshopHelper.UGCBased.PublishedItemsFinder.Create();
			WorkshopIssueReporter workshopIssueReporter = new WorkshopIssueReporter();
			workshopIssueReporter.OnNeedToOpenUI += this._issueReporter_OnNeedToOpenUI;
			workshopIssueReporter.OnNeedToNotifyUI += this._issueReporter_OnNeedToNotifyUI;
			base.IssueReporter = workshopIssueReporter;
			UIWorkshopHub.OnWorkshopHubMenuOpened += this.RefreshSubscriptionsAndPublishings;
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x004BCC35 File Offset: 0x004BAE35
		private void _issueReporter_OnNeedToNotifyUI()
		{
			Main.IssueReporterIndicator.AttemptLettingPlayerKnow();
			Main.WorkshopPublishingIndicator.Hide();
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x004BCC4B File Offset: 0x004BAE4B
		private void _issueReporter_OnNeedToOpenUI()
		{
			Main.OpenReportsMenu();
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x004BCC52 File Offset: 0x004BAE52
		public override void Shutdown()
		{
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x004BCC54 File Offset: 0x004BAE54
		public override void LoadEarlyContent()
		{
			this.RefreshSubscriptionsAndPublishings();
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x004BCC5C File Offset: 0x004BAE5C
		private void RefreshSubscriptionsAndPublishings()
		{
			this._downloader.Refresh(base.IssueReporter);
			this._publishedItems.Refresh();
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x004BCC7B File Offset: 0x004BAE7B
		public override List<string> GetListOfSubscribedWorldPaths()
		{
			return (from folderPath in this._downloader.WorldPaths
			select folderPath + Path.DirectorySeparatorChar.ToString() + "world.wld").ToList<string>();
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x004BCCB1 File Offset: 0x004BAEB1
		public override List<string> GetListOfSubscribedResourcePackPaths()
		{
			return this._downloader.ResourcePackPaths;
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x004BCCC0 File Offset: 0x004BAEC0
		public override bool TryGetPath(string pathEnd, out string fullPathFound)
		{
			fullPathFound = null;
			string text = this._downloader.ResourcePackPaths.FirstOrDefault((string x) => x.EndsWith(pathEnd));
			if (text == null)
			{
				return false;
			}
			fullPathFound = text;
			return true;
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x004BCD03 File Offset: 0x004BAF03
		private void Forget(WorkshopHelper.UGCBased.APublisherInstance instance)
		{
			this._publisherInstances.Remove(instance);
			this.RefreshSubscriptionsAndPublishings();
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x004BCD18 File Offset: 0x004BAF18
		public override void PublishWorld(WorldFileData world, WorkshopItemPublishSettings settings)
		{
			string name = world.Name;
			string textForWorld = this.GetTextForWorld(world);
			string[] usedTagsInternalNames = settings.GetUsedTagsInternalNames();
			string text = this.GetTemporaryFolderPath() + world.GetFileName(false);
			if (this.MakeTemporaryFolder(text))
			{
				WorkshopHelper.UGCBased.WorldPublisherInstance worldPublisherInstance = new WorkshopHelper.UGCBased.WorldPublisherInstance(world);
				this._publisherInstances.Add(worldPublisherInstance);
				worldPublisherInstance.PublishContent(this._publishedItems, base.IssueReporter, new WorkshopHelper.UGCBased.APublisherInstance.FinishedPublishingAction(this.Forget), name, textForWorld, text, settings.PreviewImagePath, settings.Publicity, usedTagsInternalNames, null, 0UL, null);
			}
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x004BCDA0 File Offset: 0x004BAFA0
		private string GetTextForWorld(WorldFileData world)
		{
			string text = "This is \"";
			text += world.Name;
			int worldSizeX = world.WorldSizeX;
			string text2;
			if (worldSizeX != 4200)
			{
				if (worldSizeX != 6400)
				{
					if (worldSizeX != 8400)
					{
						text2 = "custom";
					}
					else
					{
						text2 = "large";
					}
				}
				else
				{
					text2 = "medium";
				}
			}
			else
			{
				text2 = "small";
			}
			string text3;
			switch (world.GameMode)
			{
			case 0:
				text3 = "classic";
				break;
			case 1:
				text3 = "expert";
				break;
			case 2:
				text3 = "master";
				break;
			case 3:
				text3 = "journey";
				break;
			default:
				text3 = "custom";
				break;
			}
			text = string.Concat(new string[]
			{
				text,
				"\", a ",
				text2.ToLower(),
				" ",
				text3.ToLower(),
				" world"
			});
			text = text + " infected by the " + (world.HasCorruption ? "corruption" : "crimson");
			if (world.IsHardMode)
			{
				text += ", in hardmode";
			}
			return text + ".";
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x004BCECC File Offset: 0x004BB0CC
		public override void PublishResourcePack(ResourcePack resourcePack, WorkshopItemPublishSettings settings)
		{
			if (resourcePack.IsCompressed)
			{
				base.IssueReporter.ReportInstantUploadProblem("Workshop.ReportIssue_CannotPublishZips");
				return;
			}
			string name = resourcePack.Name;
			string text = resourcePack.Description;
			if (string.IsNullOrWhiteSpace(text))
			{
				text = "";
			}
			string[] usedTagsInternalNames = settings.GetUsedTagsInternalNames();
			string fullPath = resourcePack.FullPath;
			WorkshopHelper.UGCBased.ResourcePackPublisherInstance resourcePackPublisherInstance = new WorkshopHelper.UGCBased.ResourcePackPublisherInstance(resourcePack);
			this._publisherInstances.Add(resourcePackPublisherInstance);
			resourcePackPublisherInstance.PublishContent(this._publishedItems, base.IssueReporter, new WorkshopHelper.UGCBased.APublisherInstance.FinishedPublishingAction(this.Forget), name, text, fullPath, settings.PreviewImagePath, settings.Publicity, usedTagsInternalNames, null, 0UL, null);
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x004BCF64 File Offset: 0x004BB164
		private string GetTemporaryFolderPath()
		{
			string str = SteamUser.GetSteamID().m_SteamID.ToString();
			return this._contentBaseFolder + Path.DirectorySeparatorChar.ToString() + str + Path.DirectorySeparatorChar.ToString();
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x004BCFA4 File Offset: 0x004BB1A4
		private bool MakeTemporaryFolder(string temporaryFolderPath)
		{
			bool result = true;
			if (!Utils.TryCreatingDirectory(temporaryFolderPath))
			{
				base.IssueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_CouldNotCreateTemporaryFolder!");
				result = false;
			}
			return result;
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x004BCFCE File Offset: 0x004BB1CE
		public override void ImportDownloadedWorldToLocalSaves(WorldFileData world, string newFileName = null, string newDisplayName = null)
		{
			Main.menuMode = 10;
			world.CopyToLocal(newFileName, newDisplayName);
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x004BCFE0 File Offset: 0x004BB1E0
		public List<IssueReport> GetReports()
		{
			List<IssueReport> list = new List<IssueReport>();
			if (base.IssueReporter != null)
			{
				list.AddRange(base.IssueReporter.GetReports());
			}
			return list;
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x004BD010 File Offset: 0x004BB210
		public override bool TryGetInfoForWorld(WorldFileData world, out FoundWorkshopEntryInfo info)
		{
			info = null;
			string text = this.GetTemporaryFolderPath() + world.GetFileName(false);
			return Directory.Exists(text) && AWorkshopEntry.TryReadingManifest(text + Path.DirectorySeparatorChar.ToString() + "workshop.json", out info);
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x004BD060 File Offset: 0x004BB260
		public override bool TryGetInfoForResourcePack(ResourcePack resourcePack, out FoundWorkshopEntryInfo info)
		{
			info = null;
			string fullPath = resourcePack.FullPath;
			return Directory.Exists(fullPath) && AWorkshopEntry.TryReadingManifest(fullPath + Path.DirectorySeparatorChar.ToString() + "workshop.json", out info);
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x004BD0A1 File Offset: 0x004BB2A1
		public override List<string> GetListOfMods()
		{
			return this._downloader.ModPaths;
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x004BD0B0 File Offset: 0x004BB2B0
		public override bool TryGetInfoForMod(TmodFile modFile, out FoundWorkshopEntryInfo info)
		{
			info = null;
			default(QueryParameters).queryType = QueryType.SearchDirect;
			ModDownloadItem mod;
			if (!WorkshopHelper.TryGetModDownloadItem(modFile.Name, out mod))
			{
				base.IssueReporter.ReportInstantUploadProblem("tModLoader.NoWorkshopAccess");
				return false;
			}
			this.currPublishID = 0UL;
			if (mod == null)
			{
				return false;
			}
			this.currPublishID = ulong.Parse(mod.PublishId.m_ModPubId);
			WorkshopBrowserModule.Instance.DownloadItem(mod, null);
			ModOrganizer.WorkshopFileFinder.Refresh(new WorkshopIssueReporter());
			string path = Directory.GetParent(ModOrganizer.WorkshopFileFinder.ModPaths[0]).ToString();
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
			defaultInterpolatedStringHandler.AppendFormatted<ulong>(this.currPublishID);
			return ModOrganizer.TryReadManifest(Path.Combine(path, defaultInterpolatedStringHandler.ToStringAndClear()), out info);
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x004BD174 File Offset: 0x004BB374
		public override bool PublishMod(TmodFile modFile, NameValueCollection buildData, WorkshopItemPublishSettings settings)
		{
			bool result;
			try
			{
				result = this._PublishMod(modFile, buildData, settings);
			}
			catch (Exception e)
			{
				base.IssueReporter.ReportInstantUploadProblem(e.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x004BD1B4 File Offset: 0x004BB3B4
		private bool _PublishMod(TmodFile modFile, NameValueCollection buildData, WorkshopItemPublishSettings settings)
		{
			if (!SteamedWraps.SteamClient)
			{
				base.IssueReporter.ReportInstantUploadProblem("tModLoader.SteamPublishingLimit");
				return false;
			}
			if (modFile.TModLoaderVersion.MajorMinor() != BuildInfo.tMLVersion.MajorMinor())
			{
				base.IssueReporter.ReportInstantUploadProblem("tModLoader.WrongVersionCantPublishError");
				return false;
			}
			if (!modFile.HasFile("description.txt"))
			{
				base.IssueReporter.ReportInstantUploadProblemFromValue(Language.GetTextValue("tModLoader.ModDescriptionMissing", "description.txt"));
				return false;
			}
			bool result;
			using (StreamReader defaultDescriptionStream = new StreamReader(typeof(ModLoader).Assembly.GetManifestResourceStream("Terraria/ModLoader/Templates/description.txt")))
			{
				string defaultDescription = defaultDescriptionStream.ReadToEnd();
				string modDescription = Encoding.UTF8.GetString(modFile.GetBytes("description.txt"));
				if (modDescription == defaultDescription)
				{
					base.IssueReporter.ReportInstantUploadProblemFromValue(Language.GetTextValue("tModLoader.ModDescriptionInvalid", "description.txt"));
					result = false;
				}
				else
				{
					IEnumerable<char> source = modDescription;
					Func<char, bool> predicate;
					if ((predicate = WorkshopSocialModule.<>O.<0>__IsLetterOrDigit) == null)
					{
						predicate = (WorkshopSocialModule.<>O.<0>__IsLetterOrDigit = new Func<char, bool>(char.IsLetterOrDigit));
					}
					if (source.Count(predicate) < 50)
					{
						base.IssueReporter.ReportInstantUploadProblemFromValue(Language.GetTextValue("tModLoader.ModDescriptionLengthTooShort", "description.txt"));
						result = false;
					}
					else
					{
						if (modFile.HasFile("description_workshop.txt"))
						{
							using (StreamReader defaultWorkshopDescriptionStream = new StreamReader(typeof(ModLoader).Assembly.GetManifestResourceStream("Terraria/ModLoader/Templates/description_workshop.txt")))
							{
								string defaultWorkshopDescription = defaultWorkshopDescriptionStream.ReadToEnd();
								string workshopDescription = Encoding.UTF8.GetString(modFile.GetBytes("description_workshop.txt"));
								if (workshopDescription == defaultWorkshopDescription || workshopDescription.Contains("https://steamcommunity.com/comment/Guide/formattinghelp"))
								{
									base.IssueReporter.ReportInstantUploadProblemFromValue(Language.GetTextValue("tModLoader.ModWorkshopDescriptionInvalid", "description_workshop.txt", "description.txt"));
									return false;
								}
							}
						}
						using (Stream defaultIconStream = typeof(ModLoader).Assembly.GetManifestResourceStream("Terraria/ModLoader/Templates/icon.png"))
						{
							using (MemoryStream defaultIconMemoryStream = new MemoryStream((int)defaultIconStream.Length))
							{
								defaultIconStream.CopyTo(defaultIconMemoryStream);
								ReadOnlySpan<byte> defaultIconBytes = defaultIconMemoryStream.GetBuffer();
								if (modFile.GetBytes("icon.png").SequenceEqual(defaultIconBytes))
								{
									base.IssueReporter.ReportInstantUploadProblemFromValue(Language.GetTextValue("tModLoader.ModUsesDefaultIcon", "icon.png"));
									result = false;
								}
								else if (BuildInfo.IsDev)
								{
									base.IssueReporter.ReportInstantUploadProblem("tModLoader.BetaModCantPublishError");
									result = false;
								}
								else
								{
									string workshopFolderPath = this.GetTemporaryFolderPath() + modFile.Name;
									NameValueCollection nameValueCollection = buildData;
									string name2 = "versionsummary";
									DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
									defaultInterpolatedStringHandler.AppendFormatted<Version>(new Version(buildData["modloaderversion"]));
									defaultInterpolatedStringHandler.AppendLiteral(":");
									defaultInterpolatedStringHandler.AppendFormatted(buildData["version"]);
									nameValueCollection[name2] = defaultInterpolatedStringHandler.ToStringAndClear();
									buildData["trueversion"] = buildData["version"];
									if (this.currPublishID != 0UL)
									{
										string path = Directory.GetParent(ModOrganizer.WorkshopFileFinder.ModPaths[0]).ToString();
										defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
										defaultInterpolatedStringHandler.AppendFormatted<ulong>(this.currPublishID);
										workshopFolderPath = Path.Combine(path, defaultInterpolatedStringHandler.ToStringAndClear());
										WorkshopSocialModule.FixErrorsInWorkshopFolder(workshopFolderPath);
										string failureMessage;
										if (!WorkshopSocialModule.CalculateVersionsData(workshopFolderPath, ref buildData, out failureMessage))
										{
											base.IssueReporter.ReportInstantUploadProblem(failureMessage);
											return false;
										}
									}
									string name = buildData["displaynameclean"];
									if (name.Length >= 129)
									{
										base.IssueReporter.ReportInstantUploadProblem("tModLoader.TitleLengthExceedLimit");
										result = false;
									}
									else
									{
										string description = WorkshopSocialModule.CalculateDescriptionAndChangeNotes(false, buildData, ref settings.ChangeNotes);
										List<string> tagsList = new List<string>();
										tagsList.AddRange(settings.GetUsedTagsInternalNames());
										tagsList.Add(buildData["modside"]);
										if (!WorkshopSocialModule.TryCalculateWorkshopDeps(ref buildData))
										{
											base.IssueReporter.ReportInstantUploadProblem("tModLoader.NoWorkshopAccess");
											result = false;
										}
										else
										{
											defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 3);
											defaultInterpolatedStringHandler.AppendFormatted(workshopFolderPath);
											defaultInterpolatedStringHandler.AppendLiteral("/");
											defaultInterpolatedStringHandler.AppendFormatted<int>(BuildInfo.tMLVersion.Major);
											defaultInterpolatedStringHandler.AppendLiteral(".");
											defaultInterpolatedStringHandler.AppendFormatted<int>(BuildInfo.tMLVersion.Minor);
											string contentFolderPath = defaultInterpolatedStringHandler.ToStringAndClear();
											if (this.MakeTemporaryFolder(contentFolderPath))
											{
												string modPath = Path.Combine(contentFolderPath, modFile.Name + ".tmod");
												File.Copy(modFile.path, modPath, true);
												ModOrganizer.CleanupOldPublish(workshopFolderPath);
												tagsList.AddRange(WorkshopSocialModule.DetermineSupportedVersionsFromWorkshop(workshopFolderPath));
												WorkshopHelper.ModPublisherInstance modPublisherInstance = new WorkshopHelper.ModPublisherInstance();
												this._publisherInstances.Add(modPublisherInstance);
												modPublisherInstance.PublishContent(this._publishedItems, base.IssueReporter, new WorkshopHelper.UGCBased.APublisherInstance.FinishedPublishingAction(this.Forget), name, description, workshopFolderPath, settings.PreviewImagePath, settings.Publicity, tagsList.ToArray(), buildData, this.currPublishID, settings.ChangeNotes);
												result = true;
											}
											else
											{
												result = false;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x004BD710 File Offset: 0x004BB910
		public static bool CalculateVersionsData(string workshopPath, ref NameValueCollection buildData, out string failureMessage)
		{
			Version buildVersion = new Version(buildData["version"]);
			foreach (string path in Directory.EnumerateFiles(workshopPath, "*.tmod*", SearchOption.AllDirectories))
			{
				LocalMod mod = WorkshopSocialModule.OpenModFile(path);
				if (mod.tModLoaderVersion.MajorMinor() <= BuildInfo.tMLVersion.MajorMinor() && mod.Version >= buildVersion)
				{
					failureMessage = Language.GetTextValue("tModLoader.ModVersionTooSmall", buildVersion, mod.Version);
					if (mod.Version.Minor > buildVersion.Minor)
					{
						string str = failureMessage;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(35, 2);
						defaultInterpolatedStringHandler.AppendLiteral("\nThe 2nd number \"");
						defaultInterpolatedStringHandler.AppendFormatted<int>(buildVersion.Minor);
						defaultInterpolatedStringHandler.AppendLiteral("\" is less than \"");
						defaultInterpolatedStringHandler.AppendFormatted<int>(mod.Version.Minor);
						defaultInterpolatedStringHandler.AppendLiteral("\".");
						failureMessage = str + defaultInterpolatedStringHandler.ToStringAndClear();
					}
					else if (mod.Version.Revision > buildVersion.Revision)
					{
						string str2 = failureMessage;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(34, 2);
						defaultInterpolatedStringHandler.AppendLiteral("\nThe 3rd number \"");
						defaultInterpolatedStringHandler.AppendFormatted<int>(buildVersion.Revision);
						defaultInterpolatedStringHandler.AppendLiteral("\" is less than ");
						defaultInterpolatedStringHandler.AppendFormatted<int>(mod.Version.Revision);
						defaultInterpolatedStringHandler.AppendLiteral("\".");
						failureMessage = str2 + defaultInterpolatedStringHandler.ToStringAndClear();
					}
					else if (mod.Version.MinorRevision > buildVersion.MinorRevision)
					{
						string str3 = failureMessage;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(34, 2);
						defaultInterpolatedStringHandler.AppendLiteral("\nThe 4th number \"");
						defaultInterpolatedStringHandler.AppendFormatted<short>(buildVersion.MinorRevision);
						defaultInterpolatedStringHandler.AppendLiteral("\" is less than ");
						defaultInterpolatedStringHandler.AppendFormatted<short>(mod.Version.MinorRevision);
						defaultInterpolatedStringHandler.AppendLiteral("\".");
						failureMessage = str3 + defaultInterpolatedStringHandler.ToStringAndClear();
					}
					return false;
				}
				if (mod.tModLoaderVersion.MajorMinor() > BuildInfo.tMLVersion.MajorMinor() && mod.Version < buildVersion)
				{
					failureMessage = Language.GetTextValue("tModLoader.ModVersionLargerThanFutureVersions", buildVersion, mod.Version, mod.tModLoaderVersion.MajorMinor());
					return false;
				}
				if (mod.tModLoaderVersion.MajorMinor() != BuildInfo.tMLVersion.MajorMinor())
				{
					NameValueCollection nameValueCollection = buildData;
					NameValueCollection nameValueCollection2 = nameValueCollection;
					string name = "versionsummary";
					string str4 = nameValueCollection["versionsummary"];
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
					defaultInterpolatedStringHandler.AppendLiteral(";");
					defaultInterpolatedStringHandler.AppendFormatted<Version>(mod.tModLoaderVersion);
					defaultInterpolatedStringHandler.AppendLiteral(":");
					defaultInterpolatedStringHandler.AppendFormatted<Version>(mod.Version);
					nameValueCollection2[name] = str4 + defaultInterpolatedStringHandler.ToStringAndClear();
				}
			}
			failureMessage = string.Empty;
			return true;
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x004BDA00 File Offset: 0x004BBC00
		internal static HashSet<string> DetermineSupportedVersionsFromWorkshop(string repo)
		{
			return (from info in ModOrganizer.AnalyzeWorkshopTmods(repo)
			select SocialBrowserModule.GetBrowserVersionNumber(info.Item2)).ToHashSet<string>();
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x004BDA34 File Offset: 0x004BBC34
		internal static LocalMod OpenModFile(string path)
		{
			TmodFile sModFile = new TmodFile(path, null, null);
			LocalMod result;
			using (sModFile.Open())
			{
				result = new LocalMod(ModLocation.Workshop, sModFile);
			}
			return result;
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x004BDA78 File Offset: 0x004BBC78
		private static bool TryCalculateWorkshopDeps(ref NameValueCollection buildData)
		{
			string workshopDeps = "";
			if (buildData["modreferences"].Length > 0)
			{
				List<string> modIds;
				if (!WorkshopHelper.TryGetGroupPublishIdsByInternalName(new QueryParameters
				{
					searchModSlugs = buildData["modreferences"].Split(",", StringSplitOptions.None)
				}, out modIds))
				{
					return false;
				}
				foreach (string modRef in modIds)
				{
					if (modRef != "0")
					{
						workshopDeps = workshopDeps + modRef + ",";
					}
				}
			}
			buildData["workshopdeps"] = workshopDeps;
			return true;
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x004BDB38 File Offset: 0x004BBD38
		public static void FixErrorsInWorkshopFolder(string workshopFolderPath)
		{
			if (Directory.Exists(Path.Combine(workshopFolderPath, "bin")))
			{
				foreach (string path in Directory.EnumerateFiles(workshopFolderPath))
				{
					File.Delete(path);
				}
				foreach (string sourceFolder in Directory.EnumerateDirectories(workshopFolderPath))
				{
					if (!sourceFolder.Contains("2022.0"))
					{
						Directory.Delete(sourceFolder, true);
					}
				}
			}
			string devRemnant = Path.Combine(workshopFolderPath, "9999.0");
			if (Directory.Exists(devRemnant))
			{
				Directory.Delete(devRemnant, true);
			}
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x004BDBF8 File Offset: 0x004BBDF8
		private static string CalculateDescriptionAndChangeNotes(bool isCi, NameValueCollection buildData, ref string changeNotes)
		{
			string workshopDescFile = Path.Combine(buildData["sourcesfolder"], "description_workshop.txt");
			string workshopDesc;
			if (!File.Exists(workshopDescFile))
			{
				workshopDesc = buildData["description"];
			}
			else
			{
				workshopDesc = File.ReadAllText(workshopDescFile);
			}
			string descriptionFinal = "";
			if (isCi)
			{
				descriptionFinal = descriptionFinal + "[quote=GithubActions(Don't Modify)]Version Summary " + buildData["versionsummary"] + "[/quote]";
			}
			string str = descriptionFinal;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(41, 3);
			defaultInterpolatedStringHandler.AppendFormatted(workshopDesc);
			defaultInterpolatedStringHandler.AppendLiteral("[quote=tModLoader ");
			defaultInterpolatedStringHandler.AppendFormatted(buildData["name"]);
			defaultInterpolatedStringHandler.AppendLiteral("]\nDeveloped By ");
			defaultInterpolatedStringHandler.AppendFormatted(buildData["author"]);
			defaultInterpolatedStringHandler.AppendLiteral("[/quote]");
			descriptionFinal = str + defaultInterpolatedStringHandler.ToStringAndClear();
			ModCompile.UpdateSubstitutedDescriptionValues(ref descriptionFinal, buildData["trueversion"], buildData["homepage"]);
			if (descriptionFinal.Length >= 8000)
			{
				throw new Exception(Language.GetTextValue("tModLoader.DescriptionLengthExceedLimit", 8000));
			}
			if (string.IsNullOrWhiteSpace(changeNotes))
			{
				changeNotes = "Version {ModVersion} has been published to {tMLBuildPurpose} tModLoader v{tMLVersion}";
				if (!string.IsNullOrWhiteSpace(buildData["homepage"]))
				{
					changeNotes += ", learn more at the [url={ModHomepage}]homepage[/url]";
				}
			}
			ModCompile.UpdateSubstitutedDescriptionValues(ref changeNotes, buildData["trueversion"], buildData["homepage"]);
			return descriptionFinal;
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x004BDD58 File Offset: 0x004BBF58
		public static void SteamCMDPublishPreparer(string modFolder)
		{
			if (!Program.LaunchParameters.ContainsKey("-ciprep") || !Program.LaunchParameters.ContainsKey("-publishedmodfiles"))
			{
				return;
			}
			Console.WriteLine("Preparing Files for CI...");
			string changeNotes;
			Program.LaunchParameters.TryGetValue("-ciprep", out changeNotes);
			string publishedModFiles;
			Program.LaunchParameters.TryGetValue("-publishedmodfiles", out publishedModFiles);
			string uploadFolder;
			Program.LaunchParameters.TryGetValue("-uploadfolder", out uploadFolder);
			string publishFolder = ModOrganizer.modPath + "/Workshop";
			string modName = Directory.GetParent(modFolder).Name;
			string newModPath = Path.Combine(ModOrganizer.modPath, modName + ".tmod");
			LocalMod newMod = WorkshopSocialModule.OpenModFile(newModPath);
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["version"] = newMod.Version.ToString();
			string name = "versionsummary";
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler.AppendFormatted<Version>(newMod.tModLoaderVersion);
			defaultInterpolatedStringHandler.AppendLiteral(":");
			defaultInterpolatedStringHandler.AppendFormatted<Version>(newMod.Version);
			nameValueCollection[name] = defaultInterpolatedStringHandler.ToStringAndClear();
			nameValueCollection["description"] = newMod.properties.description;
			nameValueCollection["homepage"] = newMod.properties.homepage;
			nameValueCollection["sourcesfolder"] = modFolder;
			NameValueCollection buildData = nameValueCollection;
			buildData["trueversion"] = buildData["version"];
			string failureMessage;
			if (!WorkshopSocialModule.CalculateVersionsData(publishedModFiles, ref buildData, out failureMessage))
			{
				Utils.LogAndConsoleErrorMessage(failureMessage);
				Console.WriteLine(failureMessage);
				return;
			}
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(41, 2);
			defaultInterpolatedStringHandler.AppendLiteral("Built Mod Version is: ");
			defaultInterpolatedStringHandler.AppendFormatted(buildData["trueversion"]);
			defaultInterpolatedStringHandler.AppendLiteral(". tMod Version is: ");
			defaultInterpolatedStringHandler.AppendFormatted<Version>(BuildInfo.tMLVersion);
			Console.WriteLine(defaultInterpolatedStringHandler.ToStringAndClear());
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler.AppendFormatted(publishFolder);
			defaultInterpolatedStringHandler.AppendLiteral("/");
			defaultInterpolatedStringHandler.AppendFormatted<Version>(BuildInfo.tMLVersion.MajorMinor());
			string contentFolder = defaultInterpolatedStringHandler.ToStringAndClear();
			if (!Directory.Exists(contentFolder))
			{
				Directory.CreateDirectory(contentFolder);
			}
			FileUtilities.CopyFolder(publishedModFiles, publishFolder);
			File.Copy(newModPath, Path.Combine(contentFolder, modName + ".tmod"), true);
			ModOrganizer.CleanupOldPublish(publishFolder);
			string descriptionFinal = WorkshopSocialModule.CalculateDescriptionAndChangeNotes(true, buildData, ref changeNotes);
			FoundWorkshopEntryInfo steamInfo;
			AWorkshopEntry.TryReadingManifest(Path.Combine(publishedModFiles, "workshop.json"), out steamInfo);
			string vdf = ModOrganizer.modPath + "/publish.vdf";
			string[] lines = new string[]
			{
				"\"workshopitem\"",
				"{",
				"\"appid\" \"1281930\"",
				"\"publishedfileid\" \"" + steamInfo.workshopEntryId.ToString() + "\"",
				"\"contentfolder\" \"" + uploadFolder + "/Workshop\"",
				"\"changenote\" \"" + changeNotes + "\"",
				"\"description\" \"" + descriptionFinal + "\"",
				"}"
			};
			if (File.Exists(vdf))
			{
				File.Delete(vdf);
			}
			File.WriteAllLines(vdf, lines);
			Console.WriteLine("CI Files Prepared");
		}

		// Token: 0x04001373 RID: 4979
		private WorkshopHelper.UGCBased.Downloader _downloader;

		// Token: 0x04001374 RID: 4980
		private WorkshopHelper.UGCBased.PublishedItemsFinder _publishedItems;

		// Token: 0x04001375 RID: 4981
		private List<WorkshopHelper.UGCBased.APublisherInstance> _publisherInstances;

		// Token: 0x04001376 RID: 4982
		private string _contentBaseFolder;

		// Token: 0x04001377 RID: 4983
		private ulong currPublishID;

		// Token: 0x02000894 RID: 2196
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006A0E RID: 27150
			public static Func<char, bool> <0>__IsLetterOrDigit;
		}
	}
}
