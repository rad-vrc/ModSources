using System;
using System.Collections.Generic;
using System.IO;
using Steamworks;
using Terraria.IO;
using Terraria.Social.Base;
using Terraria.Utilities;

namespace Terraria.Social.Steam
{
	// Token: 0x0200016F RID: 367
	public class WorkshopHelper
	{
		// Token: 0x020005B8 RID: 1464
		public class UGCBased
		{
			// Token: 0x04005A82 RID: 23170
			public const string ManifestFileName = "workshop.json";

			// Token: 0x02000816 RID: 2070
			public struct SteamWorkshopItem
			{
				// Token: 0x040064F5 RID: 25845
				public string ContentFolderPath;

				// Token: 0x040064F6 RID: 25846
				public string Description;

				// Token: 0x040064F7 RID: 25847
				public string PreviewImagePath;

				// Token: 0x040064F8 RID: 25848
				public string[] Tags;

				// Token: 0x040064F9 RID: 25849
				public string Title;

				// Token: 0x040064FA RID: 25850
				public ERemoteStoragePublishedFileVisibility? Visibility;
			}

			// Token: 0x02000817 RID: 2071
			public class Downloader
			{
				// Token: 0x1700041B RID: 1051
				// (get) Token: 0x06003A05 RID: 14853 RVA: 0x006187D8 File Offset: 0x006169D8
				// (set) Token: 0x06003A06 RID: 14854 RVA: 0x006187E0 File Offset: 0x006169E0
				public List<string> ResourcePackPaths { get; private set; }

				// Token: 0x1700041C RID: 1052
				// (get) Token: 0x06003A07 RID: 14855 RVA: 0x006187E9 File Offset: 0x006169E9
				// (set) Token: 0x06003A08 RID: 14856 RVA: 0x006187F1 File Offset: 0x006169F1
				public List<string> WorldPaths { get; private set; }

				// Token: 0x06003A09 RID: 14857 RVA: 0x006187FA File Offset: 0x006169FA
				public Downloader()
				{
					this.ResourcePackPaths = new List<string>();
					this.WorldPaths = new List<string>();
				}

				// Token: 0x06003A0A RID: 14858 RVA: 0x00618818 File Offset: 0x00616A18
				public static WorkshopHelper.UGCBased.Downloader Create()
				{
					return new WorkshopHelper.UGCBased.Downloader();
				}

				// Token: 0x06003A0B RID: 14859 RVA: 0x00618820 File Offset: 0x00616A20
				public List<string> GetListOfSubscribedItemsPaths()
				{
					PublishedFileId_t[] array = new PublishedFileId_t[SteamUGC.GetNumSubscribedItems()];
					SteamUGC.GetSubscribedItems(array, (uint)array.Length);
					ulong num = 0UL;
					string empty = string.Empty;
					uint num2 = 0U;
					List<string> list = new List<string>();
					PublishedFileId_t[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						if (SteamUGC.GetItemInstallInfo(array2[i], ref num, ref empty, 1024U, ref num2))
						{
							list.Add(empty);
						}
					}
					return list;
				}

				// Token: 0x06003A0C RID: 14860 RVA: 0x0061888A File Offset: 0x00616A8A
				public bool Prepare(WorkshopIssueReporter issueReporter)
				{
					return this.Refresh(issueReporter);
				}

				// Token: 0x06003A0D RID: 14861 RVA: 0x00618894 File Offset: 0x00616A94
				public bool Refresh(WorkshopIssueReporter issueReporter)
				{
					this.ResourcePackPaths.Clear();
					this.WorldPaths.Clear();
					foreach (string text in this.GetListOfSubscribedItemsPaths())
					{
						if (text != null)
						{
							try
							{
								string path = text + Path.DirectorySeparatorChar.ToString() + "workshop.json";
								if (File.Exists(path))
								{
									string a = AWorkshopEntry.ReadHeader(File.ReadAllText(path));
									if (!(a == "World"))
									{
										if (a == "ResourcePack")
										{
											this.ResourcePackPaths.Add(text);
										}
									}
									else
									{
										this.WorldPaths.Add(text);
									}
								}
							}
							catch (Exception exception)
							{
								issueReporter.ReportDownloadProblem("Workshop.ReportIssue_FailedToLoadSubscribedFile", text, exception);
								return false;
							}
						}
					}
					return true;
				}
			}

			// Token: 0x02000818 RID: 2072
			public class PublishedItemsFinder
			{
				// Token: 0x06003A0E RID: 14862 RVA: 0x00618990 File Offset: 0x00616B90
				public bool HasItemOfId(ulong id)
				{
					return this._items.ContainsKey(id);
				}

				// Token: 0x06003A0F RID: 14863 RVA: 0x0061899E File Offset: 0x00616B9E
				public static WorkshopHelper.UGCBased.PublishedItemsFinder Create()
				{
					WorkshopHelper.UGCBased.PublishedItemsFinder publishedItemsFinder = new WorkshopHelper.UGCBased.PublishedItemsFinder();
					publishedItemsFinder.LoadHooks();
					return publishedItemsFinder;
				}

				// Token: 0x06003A10 RID: 14864 RVA: 0x006189AB File Offset: 0x00616BAB
				private void LoadHooks()
				{
					this.OnSteamUGCQueryCompletedCallResult = CallResult<SteamUGCQueryCompleted_t>.Create(new CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate(this.OnSteamUGCQueryCompleted));
					this.OnSteamUGCRequestUGCDetailsResultCallResult = CallResult<SteamUGCRequestUGCDetailsResult_t>.Create(new CallResult<SteamUGCRequestUGCDetailsResult_t>.APIDispatchDelegate(this.OnSteamUGCRequestUGCDetailsResult));
				}

				// Token: 0x06003A11 RID: 14865 RVA: 0x006189DB File Offset: 0x00616BDB
				public void Prepare()
				{
					this.Refresh();
				}

				// Token: 0x06003A12 RID: 14866 RVA: 0x006189E4 File Offset: 0x00616BE4
				public void Refresh()
				{
					this.m_UGCQueryHandle = SteamUGC.CreateQueryUserUGCRequest(SteamUser.GetSteamID().GetAccountID(), 0, -1, 0, SteamUtils.GetAppID(), SteamUtils.GetAppID(), 1U);
					CoreSocialModule.SetSkipPulsing(true);
					SteamAPICall_t steamAPICall_t = SteamUGC.SendQueryUGCRequest(this.m_UGCQueryHandle);
					this.OnSteamUGCQueryCompletedCallResult.Set(steamAPICall_t, new CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate(this.OnSteamUGCQueryCompleted));
					CoreSocialModule.SetSkipPulsing(false);
				}

				// Token: 0x06003A13 RID: 14867 RVA: 0x00618A48 File Offset: 0x00616C48
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
						SteamUGCDetails_t steamUGCDetails_t;
						SteamUGC.GetQueryUGCResult(this.m_UGCQueryHandle, num, ref steamUGCDetails_t);
						ulong publishedFileId = steamUGCDetails_t.m_nPublishedFileId.m_PublishedFileId;
						WorkshopHelper.UGCBased.SteamWorkshopItem value = new WorkshopHelper.UGCBased.SteamWorkshopItem
						{
							Title = steamUGCDetails_t.m_rgchTitle,
							Description = steamUGCDetails_t.m_rgchDescription
						};
						this._items.Add(publishedFileId, value);
					}
					SteamUGC.ReleaseQueryUGCRequest(this.m_UGCQueryHandle);
				}

				// Token: 0x06003A14 RID: 14868 RVA: 0x0003C3EC File Offset: 0x0003A5EC
				private void OnSteamUGCRequestUGCDetailsResult(SteamUGCRequestUGCDetailsResult_t pCallback, bool bIOFailure)
				{
				}

				// Token: 0x040064FD RID: 25853
				private Dictionary<ulong, WorkshopHelper.UGCBased.SteamWorkshopItem> _items = new Dictionary<ulong, WorkshopHelper.UGCBased.SteamWorkshopItem>();

				// Token: 0x040064FE RID: 25854
				private UGCQueryHandle_t m_UGCQueryHandle;

				// Token: 0x040064FF RID: 25855
				private CallResult<SteamUGCQueryCompleted_t> OnSteamUGCQueryCompletedCallResult;

				// Token: 0x04006500 RID: 25856
				private CallResult<SteamUGCRequestUGCDetailsResult_t> OnSteamUGCRequestUGCDetailsResultCallResult;
			}

			// Token: 0x02000819 RID: 2073
			public abstract class APublisherInstance
			{
				// Token: 0x06003A16 RID: 14870 RVA: 0x00618B00 File Offset: 0x00616D00
				public void PublishContent(WorkshopHelper.UGCBased.PublishedItemsFinder finder, WorkshopIssueReporter issueReporter, WorkshopHelper.UGCBased.APublisherInstance.FinishedPublishingAction endAction, string itemTitle, string itemDescription, string contentFolderPath, string previewImagePath, WorkshopItemPublicSettingId publicity, string[] tags)
				{
					this._issueReporter = issueReporter;
					this._endAction = endAction;
					this._createItemHook = CallResult<CreateItemResult_t>.Create(new CallResult<CreateItemResult_t>.APIDispatchDelegate(this.CreateItemResult));
					this._updateItemHook = CallResult<SubmitItemUpdateResult_t>.Create(new CallResult<SubmitItemUpdateResult_t>.APIDispatchDelegate(this.UpdateItemResult));
					ERemoteStoragePublishedFileVisibility visibility = this.GetVisibility(publicity);
					this._entryData = new WorkshopHelper.UGCBased.SteamWorkshopItem
					{
						Title = itemTitle,
						Description = itemDescription,
						ContentFolderPath = contentFolderPath,
						Tags = tags,
						PreviewImagePath = previewImagePath,
						Visibility = new ERemoteStoragePublishedFileVisibility?(visibility)
					};
					ulong? num = null;
					FoundWorkshopEntryInfo foundWorkshopEntryInfo;
					if (AWorkshopEntry.TryReadingManifest(contentFolderPath + Path.DirectorySeparatorChar.ToString() + "workshop.json", out foundWorkshopEntryInfo))
					{
						num = new ulong?(foundWorkshopEntryInfo.workshopEntryId);
					}
					if (num != null && finder.HasItemOfId(num.Value))
					{
						this._publishedFileID = new PublishedFileId_t(num.Value);
						this.PreventUpdatingCertainThings();
						this.UpdateItem();
						return;
					}
					this.CreateItem();
				}

				// Token: 0x06003A17 RID: 14871 RVA: 0x00618C13 File Offset: 0x00616E13
				private void PreventUpdatingCertainThings()
				{
					this._entryData.Title = null;
					this._entryData.Description = null;
				}

				// Token: 0x06003A18 RID: 14872 RVA: 0x00618C2D File Offset: 0x00616E2D
				private ERemoteStoragePublishedFileVisibility GetVisibility(WorkshopItemPublicSettingId publicityId)
				{
					switch (publicityId)
					{
					default:
						return 2;
					case WorkshopItemPublicSettingId.FriendsOnly:
						return 1;
					case WorkshopItemPublicSettingId.Public:
						return 0;
					}
				}

				// Token: 0x06003A19 RID: 14873 RVA: 0x00618C48 File Offset: 0x00616E48
				private void CreateItem()
				{
					CoreSocialModule.SetSkipPulsing(true);
					SteamAPICall_t steamAPICall_t = SteamUGC.CreateItem(SteamUtils.GetAppID(), 0);
					this._createItemHook.Set(steamAPICall_t, new CallResult<CreateItemResult_t>.APIDispatchDelegate(this.CreateItemResult));
					CoreSocialModule.SetSkipPulsing(false);
				}

				// Token: 0x06003A1A RID: 14874 RVA: 0x00618C88 File Offset: 0x00616E88
				private void CreateItemResult(CreateItemResult_t param, bool bIOFailure)
				{
					if (param.m_bUserNeedsToAcceptWorkshopLegalAgreement)
					{
						this._issueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_FailedToPublish_UserDidNotAcceptWorkshopTermsOfService");
						this._endAction(this);
						return;
					}
					if (param.m_eResult == 1)
					{
						this._publishedFileID = param.m_nPublishedFileId;
						this.UpdateItem();
						return;
					}
					this._issueReporter.ReportDelayedUploadProblemWithoutKnownReason("Workshop.ReportIssue_FailedToPublish_WithoutKnownReason", param.m_eResult.ToString());
					this._endAction(this);
				}

				// Token: 0x06003A1B RID: 14875
				protected abstract string GetHeaderText();

				// Token: 0x06003A1C RID: 14876
				protected abstract void PrepareContentForUpdate();

				// Token: 0x06003A1D RID: 14877 RVA: 0x00618D04 File Offset: 0x00616F04
				private void UpdateItem()
				{
					string headerText = this.GetHeaderText();
					if (!this.TryWritingManifestToFolder(this._entryData.ContentFolderPath, headerText))
					{
						this._endAction(this);
						return;
					}
					this.PrepareContentForUpdate();
					UGCUpdateHandle_t ugcupdateHandle_t = SteamUGC.StartItemUpdate(SteamUtils.GetAppID(), this._publishedFileID);
					if (this._entryData.Title != null)
					{
						SteamUGC.SetItemTitle(ugcupdateHandle_t, this._entryData.Title);
					}
					if (this._entryData.Description != null)
					{
						SteamUGC.SetItemDescription(ugcupdateHandle_t, this._entryData.Description);
					}
					SteamUGC.SetItemContent(ugcupdateHandle_t, this._entryData.ContentFolderPath);
					SteamUGC.SetItemTags(ugcupdateHandle_t, this._entryData.Tags);
					if (this._entryData.PreviewImagePath != null)
					{
						SteamUGC.SetItemPreview(ugcupdateHandle_t, this._entryData.PreviewImagePath);
					}
					if (this._entryData.Visibility != null)
					{
						SteamUGC.SetItemVisibility(ugcupdateHandle_t, this._entryData.Visibility.Value);
					}
					CoreSocialModule.SetSkipPulsing(true);
					SteamAPICall_t steamAPICall_t = SteamUGC.SubmitItemUpdate(ugcupdateHandle_t, "");
					this._updateHandle = ugcupdateHandle_t;
					this._updateItemHook.Set(steamAPICall_t, new CallResult<SubmitItemUpdateResult_t>.APIDispatchDelegate(this.UpdateItemResult));
					CoreSocialModule.SetSkipPulsing(false);
				}

				// Token: 0x06003A1E RID: 14878 RVA: 0x00618E34 File Offset: 0x00617034
				private void UpdateItemResult(SubmitItemUpdateResult_t param, bool bIOFailure)
				{
					if (param.m_bUserNeedsToAcceptWorkshopLegalAgreement)
					{
						this._issueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_FailedToPublish_UserDidNotAcceptWorkshopTermsOfService");
						this._endAction(this);
						return;
					}
					EResult eResult = param.m_eResult;
					if (eResult <= 9)
					{
						if (eResult == 1)
						{
							SteamFriends.ActivateGameOverlayToWebPage("steam://url/CommunityFilePage/" + this._publishedFileID.m_PublishedFileId, 0);
							goto IL_F5;
						}
						if (eResult == 8)
						{
							this._issueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_FailedToPublish_InvalidParametersForPublishing");
							goto IL_F5;
						}
						if (eResult == 9)
						{
							this._issueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_FailedToPublish_CouldNotFindFolderToUpload");
							goto IL_F5;
						}
					}
					else
					{
						if (eResult == 15)
						{
							this._issueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_FailedToPublish_AccessDeniedBecauseUserDoesntOwnLicenseForApp");
							goto IL_F5;
						}
						if (eResult == 25)
						{
							this._issueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_FailedToPublish_LimitExceeded");
							goto IL_F5;
						}
						if (eResult == 33)
						{
							this._issueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_FailedToPublish_SteamFileLockFailed");
							goto IL_F5;
						}
					}
					this._issueReporter.ReportDelayedUploadProblemWithoutKnownReason("Workshop.ReportIssue_FailedToPublish_WithoutKnownReason", param.m_eResult.ToString());
					IL_F5:
					this._endAction(this);
				}

				// Token: 0x06003A1F RID: 14879 RVA: 0x00618F44 File Offset: 0x00617144
				private bool TryWritingManifestToFolder(string folderPath, string manifestText)
				{
					string path = folderPath + Path.DirectorySeparatorChar.ToString() + "workshop.json";
					bool result = true;
					try
					{
						File.WriteAllText(path, manifestText);
					}
					catch (Exception exception)
					{
						this._issueReporter.ReportManifestCreationProblem("Workshop.ReportIssue_CouldNotCreateResourcePackManifestFile", exception);
						result = false;
					}
					return result;
				}

				// Token: 0x06003A20 RID: 14880 RVA: 0x00618F9C File Offset: 0x0061719C
				public bool TryGetProgress(out float progress)
				{
					progress = 0f;
					if (this._updateHandle == default(UGCUpdateHandle_t))
					{
						return false;
					}
					ulong num;
					ulong num2;
					SteamUGC.GetItemUpdateProgress(this._updateHandle, ref num, ref num2);
					if (num2 == 0UL)
					{
						return false;
					}
					progress = (float)(num / num2);
					return true;
				}

				// Token: 0x04006501 RID: 25857
				protected WorkshopItemPublicSettingId _publicity;

				// Token: 0x04006502 RID: 25858
				protected WorkshopHelper.UGCBased.SteamWorkshopItem _entryData;

				// Token: 0x04006503 RID: 25859
				protected PublishedFileId_t _publishedFileID;

				// Token: 0x04006504 RID: 25860
				private UGCUpdateHandle_t _updateHandle;

				// Token: 0x04006505 RID: 25861
				private CallResult<CreateItemResult_t> _createItemHook;

				// Token: 0x04006506 RID: 25862
				private CallResult<SubmitItemUpdateResult_t> _updateItemHook;

				// Token: 0x04006507 RID: 25863
				private WorkshopHelper.UGCBased.APublisherInstance.FinishedPublishingAction _endAction;

				// Token: 0x04006508 RID: 25864
				private WorkshopIssueReporter _issueReporter;

				// Token: 0x02000868 RID: 2152
				// (Invoke) Token: 0x06003B5A RID: 15194
				public delegate void FinishedPublishingAction(WorkshopHelper.UGCBased.APublisherInstance instance);
			}

			// Token: 0x0200081A RID: 2074
			public class ResourcePackPublisherInstance : WorkshopHelper.UGCBased.APublisherInstance
			{
				// Token: 0x06003A22 RID: 14882 RVA: 0x00618FE8 File Offset: 0x006171E8
				public ResourcePackPublisherInstance(ResourcePack resourcePack)
				{
					this._resourcePack = resourcePack;
				}

				// Token: 0x06003A23 RID: 14883 RVA: 0x00618FF7 File Offset: 0x006171F7
				protected override string GetHeaderText()
				{
					return TexturePackWorkshopEntry.GetHeaderTextFor(this._resourcePack, this._publishedFileID.m_PublishedFileId, this._entryData.Tags, this._publicity, this._entryData.PreviewImagePath);
				}

				// Token: 0x06003A24 RID: 14884 RVA: 0x0003C3EC File Offset: 0x0003A5EC
				protected override void PrepareContentForUpdate()
				{
				}

				// Token: 0x04006509 RID: 25865
				private ResourcePack _resourcePack;
			}

			// Token: 0x0200081B RID: 2075
			public class WorldPublisherInstance : WorkshopHelper.UGCBased.APublisherInstance
			{
				// Token: 0x06003A25 RID: 14885 RVA: 0x0061902B File Offset: 0x0061722B
				public WorldPublisherInstance(WorldFileData world)
				{
					this._world = world;
				}

				// Token: 0x06003A26 RID: 14886 RVA: 0x0061903A File Offset: 0x0061723A
				protected override string GetHeaderText()
				{
					return WorldWorkshopEntry.GetHeaderTextFor(this._world, this._publishedFileID.m_PublishedFileId, this._entryData.Tags, this._publicity, this._entryData.PreviewImagePath);
				}

				// Token: 0x06003A27 RID: 14887 RVA: 0x00619070 File Offset: 0x00617270
				protected override void PrepareContentForUpdate()
				{
					if (this._world.IsCloudSave)
					{
						FileUtilities.CopyToLocal(this._world.Path, this._entryData.ContentFolderPath + Path.DirectorySeparatorChar.ToString() + "world.wld");
						return;
					}
					FileUtilities.Copy(this._world.Path, this._entryData.ContentFolderPath + Path.DirectorySeparatorChar.ToString() + "world.wld", false, true);
				}

				// Token: 0x0400650A RID: 25866
				private WorldFileData _world;
			}
		}
	}
}
