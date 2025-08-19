using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.UI.Elements;
using Terraria.Social.Base;
using Terraria.Social.Steam;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.ModLoader.UI.ModBrowser
{
	// Token: 0x0200026F RID: 623
	internal class UIModBrowser : UIState, IHaveBackButtonCommand
	{
		// Token: 0x06002AE8 RID: 10984 RVA: 0x0051F11C File Offset: 0x0051D31C
		public UIModBrowser(SocialBrowserModule socialBackend)
		{
			ModOrganizer.PostLocalModsChanged += this.CbLocalModsChanged;
			this.SocialBackend = socialBackend;
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06002AE9 RID: 10985 RVA: 0x0051F18A File Offset: 0x0051D38A
		public static bool PlatformSupportsTls12
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06002AEA RID: 10986 RVA: 0x0051F18D File Offset: 0x0051D38D
		public bool Loading
		{
			get
			{
				return !this.ModList.State.IsFinished();
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06002AEB RID: 10987 RVA: 0x0051F1A2 File Offset: 0x0051D3A2
		// (set) Token: 0x06002AEC RID: 10988 RVA: 0x0051F1AA File Offset: 0x0051D3AA
		public UIState PreviousUIState { get; set; }

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06002AED RID: 10989 RVA: 0x0051F1B4 File Offset: 0x0051D3B4
		private QueryParameters FilterParameters
		{
			get
			{
				QueryParameters result = default(QueryParameters);
				result.searchTags = this.GetSearchTags();
				List<ModPubId_t> specialModPackFilter = this.SpecialModPackFilter;
				result.searchModIds = ((specialModPackFilter != null) ? specialModPackFilter.ToArray() : null);
				result.searchModSlugs = null;
				result.searchGeneric = ((this.SearchFilterMode == SearchFilter.Name) ? this.Filter : null);
				result.searchAuthor = ((this.SearchFilterMode == SearchFilter.Author) ? this.Filter : null);
				result.sortingParamater = this.SortMode;
				result.updateStatusFilter = this.UpdateFilterMode;
				result.modSideFilter = this.ModSideFilterMode;
				uint days;
				switch (this.TimePeriodMode)
				{
				case ModBrowserTimePeriod.Today:
					days = 1U;
					break;
				case ModBrowserTimePeriod.OneWeek:
					days = 7U;
					break;
				case ModBrowserTimePeriod.ThreeMonths:
					days = 90U;
					break;
				case ModBrowserTimePeriod.SixMonths:
					days = 180U;
					break;
				case ModBrowserTimePeriod.OneYear:
					days = 365U;
					break;
				case ModBrowserTimePeriod.AllTime:
					days = 0U;
					break;
				default:
					throw new NotImplementedException();
				}
				result.days = days;
				result.queryType = QueryType.SearchAll;
				return result;
			}
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x0051F2B0 File Offset: 0x0051D4B0
		private string[] GetSearchTags()
		{
			List<string> tags = new List<string>
			{
				SocialBrowserModule.GetBrowserVersionNumber(BuildInfo.tMLVersion)
			};
			foreach (int tagIndex in this.CategoryTagsFilter)
			{
				tags.Add(SteamedWraps.ModTags[tagIndex].InternalNameForAPIs);
			}
			if (this.LanguageTagFilter != -1)
			{
				tags.Add(SteamedWraps.ModTags[this.LanguageTagFilter].InternalNameForAPIs);
			}
			return tags.ToArray();
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06002AEF RID: 10991 RVA: 0x0051F354 File Offset: 0x0051D554
		internal string Filter
		{
			get
			{
				return this.FilterTextBox.Text;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06002AF0 RID: 10992 RVA: 0x0051F361 File Offset: 0x0051D561
		// (set) Token: 0x06002AF1 RID: 10993 RVA: 0x0051F36E File Offset: 0x0051D56E
		public ModBrowserSortMode SortMode
		{
			get
			{
				return this.SortModeFilterToggle.State;
			}
			set
			{
				this.SortModeFilterToggle.SetCurrentState(value);
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06002AF2 RID: 10994 RVA: 0x0051F37C File Offset: 0x0051D57C
		// (set) Token: 0x06002AF3 RID: 10995 RVA: 0x0051F389 File Offset: 0x0051D589
		public ModBrowserTimePeriod TimePeriodMode
		{
			get
			{
				return this.TimePeriodToggle.State;
			}
			set
			{
				this.TimePeriodToggle.SetCurrentState(value);
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06002AF4 RID: 10996 RVA: 0x0051F397 File Offset: 0x0051D597
		// (set) Token: 0x06002AF5 RID: 10997 RVA: 0x0051F3A4 File Offset: 0x0051D5A4
		public UpdateFilter UpdateFilterMode
		{
			get
			{
				return this.UpdateFilterToggle.State;
			}
			set
			{
				this.UpdateFilterToggle.SetCurrentState(value);
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06002AF6 RID: 10998 RVA: 0x0051F3B2 File Offset: 0x0051D5B2
		// (set) Token: 0x06002AF7 RID: 10999 RVA: 0x0051F3BF File Offset: 0x0051D5BF
		public SearchFilter SearchFilterMode
		{
			get
			{
				return this.SearchFilterToggle.State;
			}
			set
			{
				this.SearchFilterToggle.SetCurrentState(value);
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06002AF8 RID: 11000 RVA: 0x0051F3CD File Offset: 0x0051D5CD
		// (set) Token: 0x06002AF9 RID: 11001 RVA: 0x0051F3DA File Offset: 0x0051D5DA
		public ModSideFilter ModSideFilterMode
		{
			get
			{
				return this.ModSideFilterToggle.State;
			}
			set
			{
				this.ModSideFilterToggle.SetCurrentState(value);
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06002AFA RID: 11002 RVA: 0x0051F3E8 File Offset: 0x0051D5E8
		// (set) Token: 0x06002AFB RID: 11003 RVA: 0x0051F3F0 File Offset: 0x0051D5F0
		internal string SpecialModPackFilterTitle
		{
			get
			{
				return this._specialModPackFilterTitle;
			}
			set
			{
				this._clearButton.SetText(Language.GetTextValue("tModLoader.MBClearSpecialFilter", value));
				this._specialModPackFilterTitle = value;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06002AFC RID: 11004 RVA: 0x0051F40F File Offset: 0x0051D60F
		// (set) Token: 0x06002AFD RID: 11005 RVA: 0x0051F418 File Offset: 0x0051D618
		public List<ModPubId_t> SpecialModPackFilter
		{
			get
			{
				return this._specialModPackFilter;
			}
			set
			{
				if (this._specialModPackFilter != null && value == null)
				{
					this._backgroundElement.BackgroundColor = UICommon.MainPanelBackground;
					this._rootElement.RemoveChild(this._clearButton);
					this._rootElement.RemoveChild(this._downloadAllButton);
				}
				else if (this._specialModPackFilter == null && value != null)
				{
					this._backgroundElement.BackgroundColor = Color.Purple * 0.7f;
					this._rootElement.Append(this._clearButton);
					this._rootElement.Append(this._downloadAllButton);
				}
				this._specialModPackFilter = value;
				if (!this._firstLoad)
				{
					this.ModList.SetEnumerable(this.SocialBackend.QueryBrowser(this.FilterParameters, default(CancellationToken)));
					return;
				}
				throw new NotImplementedException("The ModPack 'View In Browser' option is only valid after one-time opening of Mod Browser");
			}
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x0051F4EC File Offset: 0x0051D6EC
		private void CheckIfAnyModUpdateIsAvailable()
		{
			this._rootElement.RemoveChild(this._updateAllButton);
			List<ModDownloadItem> imods = this.SocialBackend.GetInstalledModDownloadItems();
			foreach (ModDownloadItem modDownloadItem in imods)
			{
				modDownloadItem.UpdateInstallState();
			}
			if (this.SpecialModPackFilter == null)
			{
				if (imods.Any((ModDownloadItem item) => item.NeedUpdate))
				{
					this._rootElement.Append(this._updateAllButton);
				}
			}
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x0051F594 File Offset: 0x0051D794
		private void UpdateAllMods(UIMouseEvent @event, UIElement element)
		{
			List<ModPubId_t> relevantMods = (from item in this.SocialBackend.GetInstalledModDownloadItems()
			where item.NeedUpdate
			select item.PublishId).ToList<ModPubId_t>();
			this.DownloadModsAndReturnToBrowser(relevantMods);
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x0051F604 File Offset: 0x0051D804
		private void ClearTextFilters(UIMouseEvent @event, UIElement element)
		{
			this.PopulateModBrowser();
			SoundEngine.PlaySound(SoundID.MenuTick, null, null);
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x0051F62C File Offset: 0x0051D82C
		private void DownloadAllFilteredMods(UIMouseEvent @event, UIElement element)
		{
			this.DownloadModsAndReturnToBrowser(this.SpecialModPackFilter);
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x0051F63C File Offset: 0x0051D83C
		public override void Draw(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
			base.Draw(spriteBatch);
			for (int i = 0; i < this.CategoryButtons.Count; i++)
			{
				if (this.CategoryButtons[i].IsMouseHovering)
				{
					string text;
					switch (i)
					{
					case 0:
						text = this.SortMode.ToFriendlyString();
						break;
					case 1:
					{
						string timePeriodText = this.TimePeriodMode.ToFriendlyString();
						text = (this.TimePeriodToggle.Disabled ? Language.GetTextValue("tModLoader.MBTimePeriodToggleDisabled", "646464", timePeriodText) : timePeriodText);
						break;
					}
					case 2:
						text = this.UpdateFilterMode.ToFriendlyString();
						break;
					case 3:
						text = this.ModSideFilterMode.ToFriendlyString();
						break;
					case 4:
						text = this.SearchFilterMode.ToFriendlyString();
						break;
					default:
						text = "None";
						break;
					}
					UICommon.TooltipMouseText(text);
					break;
				}
			}
			if (this.TagFilterToggle.IsMouseHovering)
			{
				if (this.CategoryTagsFilter.Any<int>() || this.LanguageTagFilter != -1)
				{
					string tagFilterHoverText = Language.GetTextValue("tModLoader.MBTagsSelected");
					if (this.CategoryTagsFilter.Any<int>())
					{
						tagFilterHoverText = tagFilterHoverText + "\n  " + Language.GetTextValue("tModLoader.MBTagsCategories", string.Join(", ", from x in this.CategoryTagsFilter
						select Language.GetTextValue(SteamedWraps.ModTags[x].NameKey)));
					}
					if (this.LanguageTagFilter != -1)
					{
						tagFilterHoverText = tagFilterHoverText + "\n  " + Language.GetTextValue("tModLoader.MBTagsLanguage", Language.GetTextValue(SteamedWraps.ModTags[this.LanguageTagFilter].NameKey));
					}
					UICommon.TooltipMouseText(tagFilterHoverText);
				}
				else
				{
					UICommon.TooltipMouseText(Language.GetTextValue("tModLoader.MBTagsSelected") + " " + Lang.inter[23].Value);
				}
			}
			if (this._browserStatus.IsMouseHovering && this.ModList.State != AsyncProviderState.Completed)
			{
				UICommon.TooltipMouseText(this.ModList.GetEndItemText());
			}
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x0051F838 File Offset: 0x0051DA38
		public void HandleBackButtonUsage()
		{
			try
			{
				this.CloseTagFilterDropdown();
				if (this.reloadOnExit)
				{
					Main.menuMode = 10006;
				}
				else if (this.newModInstalled && this.PreviousUIState == null)
				{
					Main.menuMode = 10000;
				}
				else
				{
					IHaveBackButtonCommand.GoBackTo(this.PreviousUIState);
				}
			}
			finally
			{
				this.reloadOnExit = false;
				this.newModInstalled = false;
			}
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x0051F8A8 File Offset: 0x0051DAA8
		private void ReloadList(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this.Loading)
			{
				SoundEngine.PlaySound(SoundID.MenuOpen, null, null);
				this.ModList.Cancel();
				return;
			}
			SoundEngine.PlaySound(SoundID.MenuOpen, null, null);
			this.PopulateModBrowser();
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x0051F8F9 File Offset: 0x0051DAF9
		private void ModListStartLoading(AsyncProviderState state)
		{
			this._browserStatus.SetCurrentState(state);
			this._reloadButton.SetText(Language.GetText("tModLoader.MBCancelLoading"));
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x0051F91C File Offset: 0x0051DB1C
		private void ModListFinished(AsyncProviderState state, Exception e)
		{
			this._browserStatus.SetCurrentState(state);
			this._reloadButton.SetText(Language.GetText("tModLoader.MBReloadBrowser"));
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x0051F940 File Offset: 0x0051DB40
		public override void OnActivate()
		{
			base.OnActivate();
			try
			{
				Main.clrInput();
				if (this._firstLoad)
				{
					this.SocialBackend.Initialize();
					this.PopulateModBrowser();
				}
				this.CheckIfAnyModUpdateIsAvailable();
				this.DebounceTimer = null;
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x0051F994 File Offset: 0x0051DB94
		private void CbLocalModsChanged(HashSet<string> modSlugs, bool isDeletion)
		{
			if (this._firstLoad)
			{
				return;
			}
			HashSet<string> obj = this.modSlugsToUpdateInstallInfo;
			lock (obj)
			{
				this.modSlugsToUpdateInstallInfo.UnionWith(modSlugs);
			}
			this.CheckIfAnyModUpdateIsAvailable();
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x0051F9EC File Offset: 0x0051DBEC
		public override void OnDeactivate()
		{
			this.DebounceTimer = null;
			base.OnDeactivate();
		}

		// Token: 0x06002B0A RID: 11018 RVA: 0x0051F9FC File Offset: 0x0051DBFC
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			UIModBrowser.PageUpDownSupport(this.ModList);
			HashSet<string> obj = this.modSlugsToUpdateInstallInfo;
			lock (obj)
			{
				foreach (UIModDownloadItem uimodDownloadItem in from d in this.ModList.ReceivedItems
				where this.modSlugsToUpdateInstallInfo.Contains(d.ModDownload.ModName)
				select d)
				{
					uimodDownloadItem.ModDownload.UpdateInstallState();
					uimodDownloadItem.UpdateInstallDisplayState();
				}
				this.modSlugsToUpdateInstallInfo.Clear();
			}
			if (this.DebounceTimer != null && this.DebounceTimer.Elapsed >= this.MinTimeBetweenUpdates)
			{
				this.DebounceTimer.Stop();
				this.DebounceTimer = null;
			}
			if (this.UpdateNeeded)
			{
				if (this.DebounceTimer == null)
				{
					this.UpdateNeeded = false;
					this.PopulateModBrowser();
					this.DebounceTimer = new Stopwatch();
					this.DebounceTimer.Start();
				}
				this.TimePeriodToggle.Disabled = (this.SortMode != ModBrowserSortMode.Hot || !string.IsNullOrEmpty(this.Filter));
			}
		}

		// Token: 0x06002B0B RID: 11019 RVA: 0x0051FB38 File Offset: 0x0051DD38
		internal static void PageUpDownSupport(UIList list)
		{
			if (Main.inputText.IsKeyDown(34) && !Main.oldInputText.IsKeyDown(34))
			{
				list.ViewPosition += list.GetInnerDimensions().Height;
			}
			if (Main.inputText.IsKeyDown(33) && !Main.oldInputText.IsKeyDown(33))
			{
				list.ViewPosition -= list.GetInnerDimensions().Height;
			}
		}

		// Token: 0x06002B0C RID: 11020 RVA: 0x0051FBB0 File Offset: 0x0051DDB0
		internal void PopulateModBrowser()
		{
			this._firstLoad = false;
			this.SpecialModPackFilter = null;
			this.SpecialModPackFilterTitle = null;
			this.SetHeading(Language.GetText("tModLoader.MenuModBrowser"));
			this.ModList.SetEnumerable(this.SocialBackend.QueryBrowser(this.FilterParameters, default(CancellationToken)));
		}

		/// <summary>
		///     Enqueues a list of mods, if found on the browser (also used for ModPacks)
		/// </summary>
		// Token: 0x06002B0D RID: 11021 RVA: 0x0051FC08 File Offset: 0x0051DE08
		internal void DownloadModsAndReturnToBrowser(List<ModPubId_t> modIds)
		{
			UIModBrowser.<DownloadModsAndReturnToBrowser>d__67 <DownloadModsAndReturnToBrowser>d__;
			<DownloadModsAndReturnToBrowser>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<DownloadModsAndReturnToBrowser>d__.<>4__this = this;
			<DownloadModsAndReturnToBrowser>d__.modIds = modIds;
			<DownloadModsAndReturnToBrowser>d__.<>1__state = -1;
			<DownloadModsAndReturnToBrowser>d__.<>t__builder.Start<UIModBrowser.<DownloadModsAndReturnToBrowser>d__67>(ref <DownloadModsAndReturnToBrowser>d__);
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x0051FC47 File Offset: 0x0051DE47
		internal Task<bool> DownloadMods(IEnumerable<ModDownloadItem> mods)
		{
			return UIModBrowser.DownloadMods(mods, 10007, delegate()
			{
				this.reloadOnExit = true;
			}, delegate(ModDownloadItem mod)
			{
				this.newModInstalled = true;
				bool autoReloadAndEnableModsLeavingModBrowser = ModLoader.autoReloadAndEnableModsLeavingModBrowser;
			});
		}

		/// <summary>
		/// Downloads all ModDownloadItems provided.
		/// </summary>
		// Token: 0x06002B0F RID: 11023 RVA: 0x0051FC6C File Offset: 0x0051DE6C
		internal static Task<bool> DownloadMods(IEnumerable<ModDownloadItem> mods, int previousMenuId, Action setReloadRequred = null, Action<ModDownloadItem> onNewModInstalled = null)
		{
			UIModBrowser.<DownloadMods>d__69 <DownloadMods>d__;
			<DownloadMods>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<DownloadMods>d__.mods = mods;
			<DownloadMods>d__.previousMenuId = previousMenuId;
			<DownloadMods>d__.setReloadRequred = setReloadRequred;
			<DownloadMods>d__.onNewModInstalled = onNewModInstalled;
			<DownloadMods>d__.<>1__state = -1;
			<DownloadMods>d__.<>t__builder.Start<UIModBrowser.<DownloadMods>d__69>(ref <DownloadMods>d__);
			return <DownloadMods>d__.<>t__builder.Task;
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x0051FCC7 File Offset: 0x0051DEC7
		private void SetHeading(LocalizedText heading)
		{
			this.HeaderTextPanel.SetText(heading, 0.8f, true);
			this.HeaderTextPanel.Recalculate();
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x0051FCE6 File Offset: 0x0051DEE6
		internal void ResetTagFilters()
		{
			HashSet<int> categoryTagsFilter = this.CategoryTagsFilter;
			if (categoryTagsFilter != null)
			{
				categoryTagsFilter.Clear();
			}
			this.LanguageTagFilter = -1;
			UIModTagFilterDropdown uimodTagFilterDropdown = this.modTagFilterDropdown;
			if (uimodTagFilterDropdown == null)
			{
				return;
			}
			uimodTagFilterDropdown.RefreshSelectionStates();
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x0051FD10 File Offset: 0x0051DF10
		internal static void LogModBrowserException(Exception e, int returnToMenu)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 3);
			defaultInterpolatedStringHandler.AppendFormatted(Language.GetTextValue("tModLoader.MBBrowserError"));
			defaultInterpolatedStringHandler.AppendLiteral("\n\n");
			defaultInterpolatedStringHandler.AppendFormatted(e.Message);
			defaultInterpolatedStringHandler.AppendLiteral("\n");
			defaultInterpolatedStringHandler.AppendFormatted(e.StackTrace);
			Utils.ShowFancyErrorMessage(defaultInterpolatedStringHandler.ToStringAndClear(), returnToMenu, null);
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x0051FD78 File Offset: 0x0051DF78
		internal void Reset()
		{
			UIModBrowser.UIAsyncList_ModDownloadItem modList = this.ModList;
			if (modList != null)
			{
				modList.SetEnumerable(null);
			}
			UIBrowserFilterToggle<SearchFilter> searchFilterToggle = this.SearchFilterToggle;
			if (searchFilterToggle != null)
			{
				searchFilterToggle.SetCurrentState(SearchFilter.Name);
			}
			UIBrowserFilterToggle<ModBrowserTimePeriod> timePeriodToggle = this.TimePeriodToggle;
			if (timePeriodToggle != null)
			{
				timePeriodToggle.SetCurrentState(ModBrowserTimePeriod.OneWeek);
			}
			UIBrowserFilterToggle<UpdateFilter> updateFilterToggle = this.UpdateFilterToggle;
			if (updateFilterToggle != null)
			{
				updateFilterToggle.SetCurrentState(UpdateFilter.All);
			}
			UIBrowserFilterToggle<ModSideFilter> modSideFilterToggle = this.ModSideFilterToggle;
			if (modSideFilterToggle != null)
			{
				modSideFilterToggle.SetCurrentState(ModSideFilter.All);
			}
			UIBrowserFilterToggle<ModBrowserSortMode> sortModeFilterToggle = this.SortModeFilterToggle;
			if (sortModeFilterToggle != null)
			{
				sortModeFilterToggle.SetCurrentState(ModBrowserSortMode.Hot);
			}
			this.ResetTagFilters();
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x0051FDF7 File Offset: 0x0051DFF7
		private void UpdateHandler(object sender, EventArgs e)
		{
			this.UpdateNeeded = true;
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x0051FE00 File Offset: 0x0051E000
		private void InitializeInteractions()
		{
			this._reloadButton.OnLeftClick += this.ReloadList;
			this._backButton.OnLeftClick += delegate(UIMouseEvent _, UIElement _)
			{
				this.HandleBackButtonUsage();
			};
			this._clearButton.OnLeftClick += this.ClearTextFilters;
			this._downloadAllButton.OnLeftClick += this.DownloadAllFilteredMods;
			this._updateAllButton.OnLeftClick += this.UpdateAllMods;
			this.ModList.OnStartLoading += this.ModListStartLoading;
			this.ModList.OnFinished += this.ModListFinished;
			this._filterTextBoxBackground.OnRightClick += delegate(UIMouseEvent a, UIElement b)
			{
				this.FilterTextBox.Text = "";
			};
			this.FilterTextBox.OnRightClick += delegate(UIMouseEvent a, UIElement b)
			{
				this.FilterTextBox.Text = "";
			};
			this.FilterTextBox.OnTextChange += this.UpdateHandler;
			foreach (UICycleImage uicycleImage in this.CategoryButtons)
			{
				uicycleImage.OnStateChanged += this.UpdateHandler;
			}
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x0051FF44 File Offset: 0x0051E144
		public override void OnInitialize()
		{
			UIElement uielement = new UIElement();
			uielement.Width.Percent = 0.8f;
			uielement.MaxWidth = UICommon.MaxPanelWidth;
			uielement.Top.Pixels = 220f;
			uielement.Height.Pixels = -220f;
			uielement.Height.Percent = 1f;
			uielement.HAlign = 0.5f;
			this._rootElement = uielement;
			UIPanel uipanel = new UIPanel();
			uipanel.Width.Percent = 1f;
			uipanel.Height.Pixels = -110f;
			uipanel.Height.Percent = 1f;
			uipanel.BackgroundColor = UICommon.MainPanelBackground;
			uipanel.PaddingTop = 0f;
			this._backgroundElement = uipanel;
			this._rootElement.Append(this._backgroundElement);
			UIModBrowser.UIAsyncList_ModDownloadItem uiasyncList_ModDownloadItem = new UIModBrowser.UIAsyncList_ModDownloadItem();
			uiasyncList_ModDownloadItem.Width.Pixels = -25f;
			uiasyncList_ModDownloadItem.Width.Percent = 1f;
			uiasyncList_ModDownloadItem.Height.Pixels = -50f;
			uiasyncList_ModDownloadItem.Height.Percent = 1f;
			uiasyncList_ModDownloadItem.Top.Pixels = 50f;
			uiasyncList_ModDownloadItem.ListPadding = 5f;
			this.ModList = uiasyncList_ModDownloadItem;
			UIScrollbar uiscrollbar = new UIScrollbar();
			uiscrollbar.Height.Pixels = -50f;
			uiscrollbar.Height.Percent = 1f;
			uiscrollbar.Top.Pixels = 50f;
			uiscrollbar.HAlign = 1f;
			UIScrollbar listScrollbar = uiscrollbar.WithView(100f, 1000f);
			this._backgroundElement.Append(listScrollbar);
			this._backgroundElement.Append(this.ModList);
			this.ModList.SetScrollbar(listScrollbar);
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("tModLoader.MenuModBrowser"), 0.8f, true);
			uitextPanel.HAlign = 0.5f;
			uitextPanel.Top.Pixels = -35f;
			uitextPanel.BackgroundColor = UICommon.DefaultUIBlue;
			this.HeaderTextPanel = uitextPanel.WithPadding(15f);
			this._backgroundElement.Append(this.HeaderTextPanel);
			UITextPanel<LocalizedText> uitextPanel2 = new UITextPanel<LocalizedText>(Language.GetText("tModLoader.MBCancelLoading"), 1f, false);
			uitextPanel2.Width.Pixels = -10f;
			uitextPanel2.Width.Percent = 0.5f;
			uitextPanel2.Height.Pixels = 25f;
			uitextPanel2.VAlign = 1f;
			uitextPanel2.Top.Pixels = -65f;
			this._reloadButton = uitextPanel2.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			UITextPanel<LocalizedText> uitextPanel3 = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 1f, false);
			uitextPanel3.Width.Pixels = -10f;
			uitextPanel3.Width.Percent = 0.5f;
			uitextPanel3.Height.Pixels = 25f;
			uitextPanel3.VAlign = 1f;
			uitextPanel3.Top.Pixels = -20f;
			this._backButton = uitextPanel3.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			UITextPanel<string> uitextPanel4 = new UITextPanel<string>(Language.GetTextValue("tModLoader.MBClearSpecialFilter", "??"), 1f, false);
			uitextPanel4.Width.Pixels = -10f;
			uitextPanel4.Width.Percent = 0.5f;
			uitextPanel4.Height.Pixels = 25f;
			uitextPanel4.HAlign = 1f;
			uitextPanel4.VAlign = 1f;
			uitextPanel4.Top.Pixels = -65f;
			uitextPanel4.BackgroundColor = Color.Purple * 0.7f;
			this._clearButton = uitextPanel4.WithFadedMouseOver(Color.Purple, Color.Purple * 0.7f, default(Color), default(Color));
			UITextPanel<LocalizedText> uitextPanel5 = new UITextPanel<LocalizedText>(Language.GetText("tModLoader.MBUpdateAll"), 1f, false);
			uitextPanel5.Width.Pixels = -10f;
			uitextPanel5.Width.Percent = 0.5f;
			uitextPanel5.Height.Pixels = 25f;
			uitextPanel5.HAlign = 1f;
			uitextPanel5.VAlign = 1f;
			uitextPanel5.Top.Pixels = -20f;
			uitextPanel5.BackgroundColor = Color.Orange * 0.7f;
			this._updateAllButton = uitextPanel5.WithFadedMouseOver(Color.Orange, Color.Orange * 0.7f, default(Color), default(Color));
			UITextPanel<LocalizedText> uitextPanel6 = new UITextPanel<LocalizedText>(Language.GetText("tModLoader.MBDownloadAll"), 1f, false);
			uitextPanel6.Width.Pixels = -10f;
			uitextPanel6.Width.Percent = 0.5f;
			uitextPanel6.Height.Pixels = 25f;
			uitextPanel6.HAlign = 1f;
			uitextPanel6.VAlign = 1f;
			uitextPanel6.Top.Pixels = -20f;
			uitextPanel6.BackgroundColor = Color.Azure * 0.7f;
			this._downloadAllButton = uitextPanel6.WithFadedMouseOver(Color.Azure, Color.Azure * 0.7f, default(Color), default(Color));
			this.NoModsFoundText = new UIText(Language.GetTextValue("tModLoader.MBNoModsFound"), 1f, false)
			{
				HAlign = 0.5f
			}.WithPadding(15f);
			UIInputTextField uiinputTextField = new UIInputTextField(Language.GetTextValue("tModLoader.ModsTypeToSearch"));
			uiinputTextField.Top.Pixels = 5f;
			uiinputTextField.Left.Pixels = -161f;
			uiinputTextField.Left.Percent = 1f;
			uiinputTextField.Width.Pixels = 100f;
			uiinputTextField.Height.Pixels = 20f;
			this.FilterTextBox = uiinputTextField;
			UIElement uielement2 = new UIElement();
			uielement2.Width.Percent = 1f;
			uielement2.Height.Pixels = 32f;
			uielement2.Top.Pixels = 10f;
			this._upperMenuContainer = uielement2;
			UIPanel uipanel2 = new UIPanel();
			uipanel2.Top.Percent = 0f;
			uipanel2.Left.Pixels = -170f;
			uipanel2.Left.Percent = 1f;
			uipanel2.Width.Pixels = 135f;
			uipanel2.Height.Pixels = 40f;
			this._filterTextBoxBackground = uipanel2;
			this.SortModeFilterToggle = new UIBrowserFilterToggle<ModBrowserSortMode>(0, 0, 2)
			{
				Left = new StyleDimension
				{
					Pixels = 0f
				}
			};
			this.TimePeriodToggle = new UIBrowserFilterToggle<ModBrowserTimePeriod>(272, 0, 2)
			{
				Left = new StyleDimension
				{
					Pixels = 36f
				}
			};
			this.UpdateFilterToggle = new UIBrowserFilterToggle<UpdateFilter>(34, 0, 2)
			{
				Left = new StyleDimension
				{
					Pixels = 72f
				}
			};
			this.SearchFilterToggle = new UIBrowserFilterToggle<SearchFilter>(68, 0, 2)
			{
				Left = new StyleDimension
				{
					Pixels = 544f
				}
			};
			this.ModSideFilterToggle = new UIBrowserFilterToggle<ModSideFilter>(170, 0, 2)
			{
				Left = new StyleDimension
				{
					Pixels = 108f
				}
			};
			this.TagFilterToggle = new UICycleImage(UICommon.ModBrowserIconsTexture, 2, 32, 32, 306, 0, 2)
			{
				Left = new StyleDimension
				{
					Pixels = 144f
				}
			};
			this.TagFilterToggle.OnLeftClick += this.OpenOrCloseTagFilterDropdown;
			this.TagFilterToggle.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
			{
				this.RefreshTagFilterState();
			};
			this.TagFilterToggle.OnRightClick += delegate(UIMouseEvent a, UIElement b)
			{
				this.RefreshTagFilterState();
			};
			this.Reset();
			this.modTagFilterDropdown = new UIModTagFilterDropdown();
			this.modTagFilterDropdown.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
			{
				if (a.Target == this.modTagFilterDropdown)
				{
					this.CloseTagFilterDropdown();
				}
			};
			base.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
			{
				if (a.Target == this)
				{
					this.CloseTagFilterDropdown();
				}
			};
			this.modTagFilterDropdown.OnClickingTag += delegate()
			{
				this.UpdateNeeded = true;
			};
			UIBrowserStatus uibrowserStatus = new UIBrowserStatus();
			uibrowserStatus.VAlign = 1f;
			uibrowserStatus.Top.Pixels = -72f;
			uibrowserStatus.Left.Pixels = 545f;
			this._browserStatus = uibrowserStatus;
			this._rootElement.Append(this._browserStatus);
			this._rootElement.Append(this._reloadButton);
			this._rootElement.Append(this._backButton);
			this.CategoryButtons.Add(this.SortModeFilterToggle);
			this._upperMenuContainer.Append(this.SortModeFilterToggle);
			this.CategoryButtons.Add(this.TimePeriodToggle);
			this._upperMenuContainer.Append(this.TimePeriodToggle);
			this.CategoryButtons.Add(this.UpdateFilterToggle);
			this._upperMenuContainer.Append(this.UpdateFilterToggle);
			this.CategoryButtons.Add(this.ModSideFilterToggle);
			this._upperMenuContainer.Append(this.ModSideFilterToggle);
			this._upperMenuContainer.Append(this.TagFilterToggle);
			this.CategoryButtons.Add(this.SearchFilterToggle);
			this._upperMenuContainer.Append(this.SearchFilterToggle);
			this.InitializeInteractions();
			this._upperMenuContainer.Append(this._filterTextBoxBackground);
			this._upperMenuContainer.Append(this.FilterTextBox);
			this._backgroundElement.Append(this._upperMenuContainer);
			base.Append(this._rootElement);
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x005208BD File Offset: 0x0051EABD
		private void CloseTagFilterDropdown()
		{
			this._backgroundElement.RemoveChild(this.modTagFilterDropdown);
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x005208D0 File Offset: 0x0051EAD0
		private void OpenOrCloseTagFilterDropdown(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this.modTagFilterDropdown.Parent != null)
			{
				this.CloseTagFilterDropdown();
				return;
			}
			this._backgroundElement.RemoveChild(this.modTagFilterDropdown);
			this._backgroundElement.Append(this.modTagFilterDropdown);
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x00520908 File Offset: 0x0051EB08
		internal void RefreshTagFilterState()
		{
			this.TagFilterToggle.SetCurrentState((this.CategoryTagsFilter.Any<int>() || this.LanguageTagFilter != -1) ? 1 : 0);
		}

		// Token: 0x04001B88 RID: 7048
		public SocialBrowserModule SocialBackend;

		// Token: 0x04001B89 RID: 7049
		public static bool AvoidGithub;

		// Token: 0x04001B8A RID: 7050
		public static bool AvoidImgur;

		// Token: 0x04001B8B RID: 7051
		public static bool EarlyAutoUpdate;

		// Token: 0x04001B8C RID: 7052
		public UIModDownloadItem SelectedItem;

		// Token: 0x04001B8D RID: 7053
		public bool reloadOnExit;

		// Token: 0x04001B8E RID: 7054
		public bool newModInstalled;

		// Token: 0x04001B8F RID: 7055
		private bool _firstLoad = true;

		// Token: 0x04001B90 RID: 7056
		private string _specialModPackFilterTitle;

		// Token: 0x04001B91 RID: 7057
		private List<ModPubId_t> _specialModPackFilter;

		// Token: 0x04001B92 RID: 7058
		private HashSet<string> modSlugsToUpdateInstallInfo = new HashSet<string>();

		// Token: 0x04001B93 RID: 7059
		public TimeSpan MinTimeBetweenUpdates = TimeSpan.FromMilliseconds(100.0);

		// Token: 0x04001B94 RID: 7060
		private Stopwatch DebounceTimer;

		// Token: 0x04001B95 RID: 7061
		internal bool UpdateNeeded;

		// Token: 0x04001B97 RID: 7063
		public HashSet<int> CategoryTagsFilter = new HashSet<int>();

		// Token: 0x04001B98 RID: 7064
		public int LanguageTagFilter = -1;

		// Token: 0x04001B99 RID: 7065
		private UIElement _rootElement;

		// Token: 0x04001B9A RID: 7066
		private UIPanel _backgroundElement;

		// Token: 0x04001B9B RID: 7067
		public UIModBrowser.UIAsyncList_ModDownloadItem ModList;

		// Token: 0x04001B9C RID: 7068
		public UIText NoModsFoundText;

		// Token: 0x04001B9D RID: 7069
		public UITextPanel<LocalizedText> HeaderTextPanel;

		// Token: 0x04001B9E RID: 7070
		private UIElement _upperMenuContainer;

		// Token: 0x04001B9F RID: 7071
		internal readonly List<UICycleImage> CategoryButtons = new List<UICycleImage>();

		// Token: 0x04001BA0 RID: 7072
		private UITextPanel<LocalizedText> _reloadButton;

		// Token: 0x04001BA1 RID: 7073
		private UITextPanel<LocalizedText> _backButton;

		// Token: 0x04001BA2 RID: 7074
		private UITextPanel<string> _clearButton;

		// Token: 0x04001BA3 RID: 7075
		private UITextPanel<LocalizedText> _downloadAllButton;

		// Token: 0x04001BA4 RID: 7076
		private UITextPanel<LocalizedText> _updateAllButton;

		// Token: 0x04001BA5 RID: 7077
		private UIPanel _filterTextBoxBackground;

		// Token: 0x04001BA6 RID: 7078
		internal UIInputTextField FilterTextBox;

		// Token: 0x04001BA7 RID: 7079
		private UIBrowserStatus _browserStatus;

		// Token: 0x04001BA8 RID: 7080
		private UIModTagFilterDropdown modTagFilterDropdown;

		// Token: 0x04001BA9 RID: 7081
		public UIBrowserFilterToggle<ModBrowserSortMode> SortModeFilterToggle;

		// Token: 0x04001BAA RID: 7082
		public UIBrowserFilterToggle<ModBrowserTimePeriod> TimePeriodToggle;

		// Token: 0x04001BAB RID: 7083
		public UIBrowserFilterToggle<UpdateFilter> UpdateFilterToggle;

		// Token: 0x04001BAC RID: 7084
		public UIBrowserFilterToggle<SearchFilter> SearchFilterToggle;

		// Token: 0x04001BAD RID: 7085
		public UIBrowserFilterToggle<ModSideFilter> ModSideFilterToggle;

		// Token: 0x04001BAE RID: 7086
		public UICycleImage TagFilterToggle;

		// Token: 0x02000A21 RID: 2593
		public class UIAsyncList_ModDownloadItem : UIAsyncList<ModDownloadItem, UIModDownloadItem>
		{
			// Token: 0x060057B6 RID: 22454 RVA: 0x0069E42B File Offset: 0x0069C62B
			protected override UIModDownloadItem GenElement(ModDownloadItem resource)
			{
				return new UIModDownloadItem(resource);
			}
		}
	}
}
