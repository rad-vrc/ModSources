using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Steamworks;
using Terraria.DataStructures;
using Terraria.GameContent.UI.States;
using Terraria.IO;
using Terraria.Social.Base;

namespace Terraria.Social.Steam
{
	// Token: 0x02000171 RID: 369
	public class WorkshopSocialModule : WorkshopSocialModule
	{
		// Token: 0x06001A4B RID: 6731 RVA: 0x004E42A0 File Offset: 0x004E24A0
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

		// Token: 0x06001A4C RID: 6732 RVA: 0x004E4360 File Offset: 0x004E2560
		private void _issueReporter_OnNeedToNotifyUI()
		{
			Main.IssueReporterIndicator.AttemptLettingPlayerKnow();
			Main.WorkshopPublishingIndicator.Hide();
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x004E4376 File Offset: 0x004E2576
		private void _issueReporter_OnNeedToOpenUI()
		{
			Main.OpenReportsMenu();
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void Shutdown()
		{
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x004E437D File Offset: 0x004E257D
		public override void LoadEarlyContent()
		{
			this.RefreshSubscriptionsAndPublishings();
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x004E4385 File Offset: 0x004E2585
		private void RefreshSubscriptionsAndPublishings()
		{
			this._downloader.Refresh(base.IssueReporter);
			this._publishedItems.Refresh();
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x004E43A4 File Offset: 0x004E25A4
		public override List<string> GetListOfSubscribedWorldPaths()
		{
			return (from folderPath in this._downloader.WorldPaths
			select folderPath + Path.DirectorySeparatorChar.ToString() + "world.wld").ToList<string>();
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x004E43DA File Offset: 0x004E25DA
		public override List<string> GetListOfSubscribedResourcePackPaths()
		{
			return this._downloader.ResourcePackPaths;
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x004E43E8 File Offset: 0x004E25E8
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

		// Token: 0x06001A54 RID: 6740 RVA: 0x004E442B File Offset: 0x004E262B
		private void Forget(WorkshopHelper.UGCBased.APublisherInstance instance)
		{
			this._publisherInstances.Remove(instance);
			this.RefreshSubscriptionsAndPublishings();
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x004E4440 File Offset: 0x004E2640
		public override void PublishWorld(WorldFileData world, WorkshopItemPublishSettings settings)
		{
			string name = world.Name;
			string textForWorld = this.GetTextForWorld(world);
			string[] usedTagsInternalNames = settings.GetUsedTagsInternalNames();
			string text = this.GetTemporaryFolderPath() + world.GetFileName(false);
			if (!this.MakeTemporaryFolder(text))
			{
				return;
			}
			WorkshopHelper.UGCBased.WorldPublisherInstance worldPublisherInstance = new WorkshopHelper.UGCBased.WorldPublisherInstance(world);
			this._publisherInstances.Add(worldPublisherInstance);
			worldPublisherInstance.PublishContent(this._publishedItems, base.IssueReporter, new WorkshopHelper.UGCBased.APublisherInstance.FinishedPublishingAction(this.Forget), name, textForWorld, text, settings.PreviewImagePath, settings.Publicity, usedTagsInternalNames);
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x004E44C4 File Offset: 0x004E26C4
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

		// Token: 0x06001A57 RID: 6743 RVA: 0x004E45F0 File Offset: 0x004E27F0
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
			resourcePackPublisherInstance.PublishContent(this._publishedItems, base.IssueReporter, new WorkshopHelper.UGCBased.APublisherInstance.FinishedPublishingAction(this.Forget), name, text, fullPath, settings.PreviewImagePath, settings.Publicity, usedTagsInternalNames);
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x004E4684 File Offset: 0x004E2884
		private string GetTemporaryFolderPath()
		{
			ulong steamID = SteamUser.GetSteamID().m_SteamID;
			return this._contentBaseFolder + Path.DirectorySeparatorChar.ToString() + steamID.ToString() + Path.DirectorySeparatorChar.ToString();
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x004E46C8 File Offset: 0x004E28C8
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

		// Token: 0x06001A5A RID: 6746 RVA: 0x004E46F2 File Offset: 0x004E28F2
		public override void ImportDownloadedWorldToLocalSaves(WorldFileData world, string newFileName = null, string newDisplayName = null)
		{
			Main.menuMode = 10;
			world.CopyToLocal(newFileName, newDisplayName);
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x004E4704 File Offset: 0x004E2904
		public List<IssueReport> GetReports()
		{
			List<IssueReport> list = new List<IssueReport>();
			if (base.IssueReporter != null)
			{
				list.AddRange(base.IssueReporter.GetReports());
			}
			return list;
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x004E4734 File Offset: 0x004E2934
		public override bool TryGetInfoForWorld(WorldFileData world, out FoundWorkshopEntryInfo info)
		{
			info = null;
			string text = this.GetTemporaryFolderPath() + world.GetFileName(false);
			return Directory.Exists(text) && AWorkshopEntry.TryReadingManifest(text + Path.DirectorySeparatorChar.ToString() + "workshop.json", out info);
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x004E4784 File Offset: 0x004E2984
		public override bool TryGetInfoForResourcePack(ResourcePack resourcePack, out FoundWorkshopEntryInfo info)
		{
			info = null;
			string fullPath = resourcePack.FullPath;
			return Directory.Exists(fullPath) && AWorkshopEntry.TryReadingManifest(fullPath + Path.DirectorySeparatorChar.ToString() + "workshop.json", out info);
		}

		// Token: 0x04001591 RID: 5521
		private WorkshopHelper.UGCBased.Downloader _downloader;

		// Token: 0x04001592 RID: 5522
		private WorkshopHelper.UGCBased.PublishedItemsFinder _publishedItems;

		// Token: 0x04001593 RID: 5523
		private List<WorkshopHelper.UGCBased.APublisherInstance> _publisherInstances;

		// Token: 0x04001594 RID: 5524
		private string _contentBaseFolder;
	}
}
