using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.UI.ModBrowser;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.ModLoader.UI
{
	// Token: 0x02000254 RID: 596
	internal class UIMods : UIState, IHaveBackButtonCommand
	{
		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06002A2B RID: 10795 RVA: 0x00519934 File Offset: 0x00517B34
		// (set) Token: 0x06002A2C RID: 10796 RVA: 0x0051993C File Offset: 0x00517B3C
		public UIState PreviousUIState { get; set; }

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06002A2D RID: 10797 RVA: 0x00519945 File Offset: 0x00517B45
		private bool forceReloadHidden
		{
			get
			{
				return ModLoader.autoReloadRequiredModsLeavingModsScreen && !ModCompile.DeveloperMode;
			}
		}

		// Token: 0x06002A2E RID: 10798 RVA: 0x00519958 File Offset: 0x00517B58
		public override void OnInitialize()
		{
			UIElement uielement = new UIElement();
			uielement.Width.Percent = 0.8f;
			uielement.MaxWidth = UICommon.MaxPanelWidth;
			uielement.Top.Pixels = 220f;
			uielement.Height.Pixels = -220f;
			uielement.Height.Percent = 1f;
			uielement.HAlign = 0.5f;
			this.uIElement = uielement;
			UIPanel uipanel = new UIPanel();
			uipanel.Width.Percent = 1f;
			uipanel.Height.Pixels = -110f;
			uipanel.Height.Percent = 1f;
			uipanel.BackgroundColor = UICommon.MainPanelBackground;
			uipanel.PaddingTop = 0f;
			this.uIPanel = uipanel;
			this.uIElement.Append(this.uIPanel);
			this.uiLoader = new UILoaderAnimatedImage(0.5f, 0.5f, 1f);
			UIList uilist = new UIList();
			uilist.Width.Pixels = -25f;
			uilist.Width.Percent = 1f;
			uilist.Height.Pixels = -50f;
			uilist.Height.Percent = 1f;
			uilist.Top.Pixels = 50f;
			uilist.ListPadding = 5f;
			this.modList = uilist;
			this.uIPanel.Append(this.modList);
			UIMemoryBar uimemoryBar = new UIMemoryBar();
			uimemoryBar.Top.Pixels = 44f;
			this.ramUsage = uimemoryBar;
			UIScrollbar uiscrollbar = new UIScrollbar();
			uiscrollbar.Height.Pixels = -50f;
			uiscrollbar.Height.Percent = 1f;
			uiscrollbar.Top.Pixels = 50f;
			uiscrollbar.HAlign = 1f;
			this.uIScrollbar = uiscrollbar.WithView(100f, 1000f);
			this.uIPanel.Append(this.uIScrollbar);
			this.modList.SetScrollbar(this.uIScrollbar);
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("tModLoader.ModsModsList"), 0.8f, true);
			uitextPanel.HAlign = 0.5f;
			uitextPanel.Top.Pixels = -35f;
			uitextPanel.BackgroundColor = UICommon.DefaultUIBlue;
			UITextPanel<LocalizedText> uIHeaderTexTPanel = uitextPanel.WithPadding(15f);
			this.uIElement.Append(uIHeaderTexTPanel);
			UIAutoScaleTextTextPanel<LocalizedText> uiautoScaleTextTextPanel = new UIAutoScaleTextTextPanel<LocalizedText>(Language.GetText("tModLoader.ModsEnableAll"), 1f, false);
			uiautoScaleTextTextPanel.TextColor = Color.Green;
			uiautoScaleTextTextPanel.Width = new StyleDimension(-10f, 0.33333334f);
			uiautoScaleTextTextPanel.Height.Pixels = 40f;
			uiautoScaleTextTextPanel.VAlign = 1f;
			uiautoScaleTextTextPanel.Top.Pixels = -65f;
			this.buttonEA = uiautoScaleTextTextPanel.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this.buttonEA.OnLeftClick += this.QuickEnableAll;
			this.uIElement.Append(this.buttonEA);
			this.buttonDA = new UIAutoScaleTextTextPanel<LocalizedText>(Language.GetText("tModLoader.ModsDisableAll"), 1f, false);
			this.buttonDA.CopyStyle(this.buttonEA);
			this.buttonDA.TextColor = Color.Red;
			this.buttonDA.HAlign = 0.5f;
			this.buttonDA.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this.buttonDA.OnLeftClick += this.QuickDisableAll;
			this.uIElement.Append(this.buttonDA);
			this.buttonRM = new UIAutoScaleTextTextPanel<LocalizedText>(Language.GetText("tModLoader.ModsForceReload"), 1f, false);
			this.buttonRM.CopyStyle(this.buttonEA);
			this.buttonRM.Width = new StyleDimension(-10f, 0.33333334f);
			this.buttonRM.HAlign = 1f;
			this.buttonRM.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this.buttonRM.OnLeftClick += this.ReloadMods;
			this.uIElement.Append(this.buttonRM);
			this.UpdateTopRowButtons();
			UIAutoScaleTextTextPanel<LocalizedText> uiautoScaleTextTextPanel2 = new UIAutoScaleTextTextPanel<LocalizedText>(Language.GetText("UI.Back"), 1f, false);
			uiautoScaleTextTextPanel2.Width = new StyleDimension(-10f, 0.33333334f);
			uiautoScaleTextTextPanel2.Height.Pixels = 40f;
			uiautoScaleTextTextPanel2.VAlign = 1f;
			uiautoScaleTextTextPanel2.Top.Pixels = -20f;
			this.buttonB = uiautoScaleTextTextPanel2.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this.buttonB.OnLeftClick += delegate(UIMouseEvent _, UIElement _)
			{
				this.HandleBackButtonUsage();
			};
			this.uIElement.Append(this.buttonB);
			this.buttonOMF = new UIAutoScaleTextTextPanel<LocalizedText>(Language.GetText("tModLoader.ModsOpenModsFolders"), 1f, false);
			this.buttonOMF.CopyStyle(this.buttonB);
			this.buttonOMF.HAlign = 0.5f;
			this.buttonOMF.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			UIElement uielement2 = this.buttonOMF;
			UIElement.MouseEvent value;
			if ((value = UIMods.<>O.<0>__OpenModsFolder) == null)
			{
				value = (UIMods.<>O.<0>__OpenModsFolder = new UIElement.MouseEvent(UIMods.OpenModsFolder));
			}
			uielement2.OnLeftClick += value;
			this.uIElement.Append(this.buttonOMF);
			Asset<Texture2D> texture = UICommon.ModBrowserIconsTexture;
			UIElement uielement3 = new UIElement();
			uielement3.Width.Percent = 1f;
			uielement3.Height.Pixels = 32f;
			uielement3.Top.Pixels = 10f;
			UIElement upperMenuContainer = uielement3;
			for (int i = 0; i < 4; i++)
			{
				UICycleImage toggleImage;
				if (i == 0)
				{
					toggleImage = new UICycleImage(texture, 3, 32, 32, 102, 0, 2);
					toggleImage.SetCurrentState((int)this.sortMode);
					toggleImage.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
					{
						this.sortMode = this.sortMode.NextEnum<ModsMenuSortMode>();
						this.updateNeeded = true;
					};
					toggleImage.OnRightClick += delegate(UIMouseEvent a, UIElement b)
					{
						this.sortMode = this.sortMode.PreviousEnum<ModsMenuSortMode>();
						this.updateNeeded = true;
					};
				}
				else if (i == 1)
				{
					toggleImage = new UICycleImage(texture, 3, 32, 32, 136, 0, 2);
					toggleImage.SetCurrentState((int)this.enabledFilterMode);
					toggleImage.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
					{
						this.enabledFilterMode = this.enabledFilterMode.NextEnum<EnabledFilter>();
						this.updateNeeded = true;
					};
					toggleImage.OnRightClick += delegate(UIMouseEvent a, UIElement b)
					{
						this.enabledFilterMode = this.enabledFilterMode.PreviousEnum<EnabledFilter>();
						this.updateNeeded = true;
					};
				}
				else if (i == 2)
				{
					toggleImage = new UICycleImage(texture, 5, 32, 32, 170, 0, 2);
					toggleImage.SetCurrentState((int)this.modSideFilterMode);
					toggleImage.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
					{
						this.modSideFilterMode = this.modSideFilterMode.NextEnum<ModSideFilter>();
						this.updateNeeded = true;
					};
					toggleImage.OnRightClick += delegate(UIMouseEvent a, UIElement b)
					{
						this.modSideFilterMode = this.modSideFilterMode.PreviousEnum<ModSideFilter>();
						this.updateNeeded = true;
					};
				}
				else
				{
					toggleImage = new UICycleImage(texture, 2, 32, 32, 238, 0, 2);
					toggleImage.SetCurrentState(this.showRamUsage.ToInt());
					toggleImage.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
					{
						this.<OnInitialize>g__ToggleRamButtonAction|39_12();
					};
					toggleImage.OnRightClick += delegate(UIMouseEvent a, UIElement b)
					{
						this.<OnInitialize>g__ToggleRamButtonAction|39_12();
					};
				}
				toggleImage.Left.Pixels = (float)(i * 36);
				this._categoryButtons.Add(toggleImage);
				upperMenuContainer.Append(toggleImage);
			}
			UIPanel uipanel2 = new UIPanel();
			uipanel2.Top.Percent = 0f;
			uipanel2.Left.Pixels = -186f;
			uipanel2.Left.Percent = 1f;
			uipanel2.Width.Pixels = 150f;
			uipanel2.Height.Pixels = 40f;
			UIPanel filterTextBoxBackground = uipanel2;
			filterTextBoxBackground.SetPadding(0f);
			filterTextBoxBackground.OnRightClick += this.ClearSearchField;
			upperMenuContainer.Append(filterTextBoxBackground);
			UIInputTextField uiinputTextField = new UIInputTextField(Language.GetTextValue("tModLoader.ModsTypeToSearch"));
			uiinputTextField.Top.Pixels = 5f;
			uiinputTextField.Height.Percent = 1f;
			uiinputTextField.Width.Percent = 1f;
			uiinputTextField.Left.Pixels = 5f;
			uiinputTextField.VAlign = 0.5f;
			this.filterTextBox = uiinputTextField;
			this.filterTextBox.OnTextChange += delegate(object a, EventArgs b)
			{
				this.updateNeeded = true;
			};
			filterTextBoxBackground.Append(this.filterTextBox);
			UIImageButton clearSearchButton = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/SearchCancel"))
			{
				HAlign = 1f,
				VAlign = 0.5f,
				Left = new StyleDimension(-2f, 0f)
			};
			clearSearchButton.OnLeftClick += this.ClearSearchField;
			filterTextBoxBackground.Append(clearSearchButton);
			UICycleImage uicycleImage = new UICycleImage(texture, 2, 32, 32, 68, 0, 2);
			uicycleImage.Left.Pixels = 544f;
			this.SearchFilterToggle = uicycleImage;
			this.SearchFilterToggle.SetCurrentState((int)this.searchFilterMode);
			this.SearchFilterToggle.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
			{
				this.searchFilterMode = this.searchFilterMode.NextEnum<SearchFilter>();
				this.updateNeeded = true;
			};
			this.SearchFilterToggle.OnRightClick += delegate(UIMouseEvent a, UIElement b)
			{
				this.searchFilterMode = this.searchFilterMode.PreviousEnum<SearchFilter>();
				this.updateNeeded = true;
			};
			this._categoryButtons.Add(this.SearchFilterToggle);
			upperMenuContainer.Append(this.SearchFilterToggle);
			this.buttonCL = new UIAutoScaleTextTextPanel<LocalizedText>(Language.GetText("tModLoader.ModConfiguration"), 1f, false);
			this.buttonCL.CopyStyle(this.buttonOMF);
			this.buttonCL.HAlign = 1f;
			this.buttonCL.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this.buttonCL.OnLeftClick += this.GotoModConfigList;
			this.uIElement.Append(this.buttonCL);
			this.uIPanel.Append(upperMenuContainer);
			base.Append(this.uIElement);
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x0051A33D File Offset: 0x0051853D
		private void ClearSearchField(UIMouseEvent evt, UIElement listeningElement)
		{
			this.filterTextBox.Text = "";
		}

		// Token: 0x06002A30 RID: 10800 RVA: 0x0051A350 File Offset: 0x00518550
		private void UpdateTopRowButtons()
		{
			StyleDimension buttonWidth = new StyleDimension(-10f, 1f / (this.forceReloadHidden ? 2f : 3f));
			this.buttonEA.Width = buttonWidth;
			this.buttonDA.Width = buttonWidth;
			this.buttonDA.HAlign = (this.forceReloadHidden ? 1f : 0.5f);
			this.uIElement.AddOrRemoveChild(this.buttonRM, ModCompile.DeveloperMode || !this.forceReloadHidden);
		}

		// Token: 0x06002A31 RID: 10801 RVA: 0x0051A3E0 File Offset: 0x005185E0
		public void HandleBackButtonUsage()
		{
			if (ConfigManager.AnyModNeedsReload())
			{
				Main.menuMode = 10006;
				return;
			}
			if (ModLoader.autoReloadRequiredModsLeavingModsScreen)
			{
				if (this.items.Count((UIModItem i) => i.NeedsReload) > 0)
				{
					Main.menuMode = 10006;
					return;
				}
			}
			ConfigManager.OnChangedAll();
			IHaveBackButtonCommand.GoBackTo(this.PreviousUIState);
		}

		// Token: 0x06002A32 RID: 10802 RVA: 0x0051A44E File Offset: 0x0051864E
		private void ReloadMods(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			if (this.items.Count > 0)
			{
				ModLoader.Reload();
			}
		}

		// Token: 0x06002A33 RID: 10803 RVA: 0x0051A478 File Offset: 0x00518678
		private static void OpenModsFolder(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Directory.CreateDirectory(ModLoader.ModPath);
			Utils.OpenFolder(ModLoader.ModPath);
			if (ModOrganizer.WorkshopFileFinder.ModPaths.Any<string>())
			{
				Utils.OpenFolder(Directory.GetParent(ModOrganizer.WorkshopFileFinder.ModPaths[0]).ToString());
			}
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x0051A4E0 File Offset: 0x005186E0
		private void CloseConfirmDialog(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(SoundID.MenuClose, null, null);
			UIImage blockInput = this._blockInput;
			if (blockInput != null)
			{
				blockInput.Remove();
			}
			UIPanel toggleModsDialog = this._toggleModsDialog;
			if (toggleModsDialog == null)
			{
				return;
			}
			toggleModsDialog.Remove();
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x0051A524 File Offset: 0x00518724
		private void QuickEnableAll(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.keyState.PressingShift() || !ModLoader.showConfirmationWindowWhenEnableDisableAllMods)
			{
				this.EnableAll(evt, listeningElement);
				return;
			}
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			this.ShowConfirmationWindow(new UIElement.MouseEvent(this.EnableAll), "tModLoader.ModsEnableAllConfirm");
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x0051A57C File Offset: 0x0051877C
		private void EnableAll(UIMouseEvent evt, UIElement listeningElement)
		{
			foreach (UIModItem modItem in this.items)
			{
				if (modItem.tMLUpdateRequired == null)
				{
					modItem.Enable();
				}
			}
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x0051A5D8 File Offset: 0x005187D8
		private void QuickDisableAll(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.keyState.PressingShift() || !ModLoader.showConfirmationWindowWhenEnableDisableAllMods)
			{
				this.DisableAll(evt, listeningElement);
				return;
			}
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			this.ShowConfirmationWindow(new UIElement.MouseEvent(this.DisableAll), "tModLoader.ModsDisableAllConfirm");
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x0051A630 File Offset: 0x00518830
		private void ShowConfirmationWindow(UIElement.MouseEvent yesAction, string confirmDialogTextKey)
		{
			UIImage uiimage = new UIImage(TextureAssets.Extra[190]);
			uiimage.Width.Percent = 1f;
			uiimage.Height.Percent = 1f;
			uiimage.Color = new Color(0, 0, 0, 0);
			uiimage.ScaleToFit = true;
			this._blockInput = uiimage;
			this._blockInput.OnLeftMouseDown += this.CloseConfirmDialog;
			Interface.modsMenu.Append(this._blockInput);
			UIPanel uipanel = new UIPanel();
			uipanel.Width.Percent = 0.3f;
			uipanel.Height.Percent = 0.3f;
			uipanel.HAlign = 0.5f;
			uipanel.VAlign = 0.5f;
			uipanel.BackgroundColor = new Color(63, 82, 151);
			uipanel.BorderColor = Color.Black;
			this._toggleModsDialog = uipanel;
			this._toggleModsDialog.SetPadding(6f);
			Interface.modsMenu.Append(this._toggleModsDialog);
			UIAutoScaleTextTextPanel<LocalizedText> uiautoScaleTextTextPanel = new UIAutoScaleTextTextPanel<LocalizedText>(Language.GetText("LegacyMenu.104"), 1f, false);
			uiautoScaleTextTextPanel.TextColor = Color.White;
			uiautoScaleTextTextPanel.Width = new StyleDimension(-10f, 0.33333334f);
			uiautoScaleTextTextPanel.Height.Pixels = 40f;
			uiautoScaleTextTextPanel.VAlign = 0.6f;
			uiautoScaleTextTextPanel.HAlign = 0.15f;
			this._confirmDialogYesButton = uiautoScaleTextTextPanel.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this._confirmDialogYesButton.OnLeftClick += yesAction;
			this._confirmDialogYesButton.OnLeftClick += this.CloseConfirmDialog;
			this._toggleModsDialog.Append(this._confirmDialogYesButton);
			UIAutoScaleTextTextPanel<LocalizedText> uiautoScaleTextTextPanel2 = new UIAutoScaleTextTextPanel<LocalizedText>(Language.GetText("LegacyMenu.105"), 1f, false);
			uiautoScaleTextTextPanel2.TextColor = Color.White;
			uiautoScaleTextTextPanel2.Width = new StyleDimension(-10f, 0.33333334f);
			uiautoScaleTextTextPanel2.Height.Pixels = 40f;
			uiautoScaleTextTextPanel2.VAlign = 0.6f;
			uiautoScaleTextTextPanel2.HAlign = 0.85f;
			this._confirmDialogNoButton = uiautoScaleTextTextPanel2.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this._confirmDialogNoButton.OnLeftClick += this.CloseConfirmDialog;
			this._toggleModsDialog.Append(this._confirmDialogNoButton);
			UIAutoScaleTextTextPanel<LocalizedText> uiautoScaleTextTextPanel3 = new UIAutoScaleTextTextPanel<LocalizedText>(Language.GetText("tModLoader.YesDontAskAgain"), 1f, false);
			uiautoScaleTextTextPanel3.TextColor = Color.White;
			uiautoScaleTextTextPanel3.Width = new StyleDimension(0f, 0.6666667f);
			uiautoScaleTextTextPanel3.Height.Pixels = 40f;
			uiautoScaleTextTextPanel3.VAlign = 0.95f;
			uiautoScaleTextTextPanel3.HAlign = 0.5f;
			UIAutoScaleTextTextPanel<LocalizedText> yesDontAskAgainButton = uiautoScaleTextTextPanel3.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			yesDontAskAgainButton.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
			{
				ModLoader.showConfirmationWindowWhenEnableDisableAllMods = false;
			};
			yesDontAskAgainButton.OnLeftClick += yesAction;
			yesDontAskAgainButton.OnLeftClick += this.CloseConfirmDialog;
			this._toggleModsDialog.Append(yesDontAskAgainButton);
			UIText uitext = new UIText(Language.GetTextValue(confirmDialogTextKey), 1f, false);
			uitext.Width.Percent = 0.75f;
			uitext.HAlign = 0.5f;
			uitext.VAlign = 0.2f;
			uitext.IsWrapped = true;
			this._confirmDialogText = uitext;
			this._toggleModsDialog.Append(this._confirmDialogText);
			this.Recalculate();
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x0051A9CC File Offset: 0x00518BCC
		private void DisableAll(UIMouseEvent evt, UIElement listeningElement)
		{
			foreach (UIModItem uimodItem in this.items)
			{
				uimodItem.Disable();
			}
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x0051AA1C File Offset: 0x00518C1C
		private void GotoModConfigList(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.menuMode = 10027;
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x0051AA40 File Offset: 0x00518C40
		public UIModItem FindUIModItem(string modName)
		{
			return this.items.SingleOrDefault((UIModItem m) => m.ModName == modName);
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x0051AA74 File Offset: 0x00518C74
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			UIModBrowser.PageUpDownSupport(this.modList);
			Task<List<UIModItem>> task = this.modItemsTask;
			if (task != null && task.IsCompleted)
			{
				List<UIModItem> result = this.modItemsTask.Result;
				this.items.AddRange(result);
				foreach (UIModItem uimodItem in this.items)
				{
					uimodItem.Activate();
				}
				this.needToRemoveLoading = true;
				this.updateNeeded = true;
				this.loading = false;
				this.modItemsTask = null;
			}
			if (this.needToRemoveLoading)
			{
				this.needToRemoveLoading = false;
				this.uIPanel.RemoveChild(this.uiLoader);
			}
			if (!this.updateNeeded)
			{
				return;
			}
			this.updateNeeded = false;
			this.filter = this.filterTextBox.Text;
			this.modList.Clear();
			UIModsFilterResults filterResults = new UIModsFilterResults();
			List<UIModItem> visibleItems = (from item in this.items
			where item.PassFilters(filterResults)
			select item).ToList<UIModItem>();
			if (filterResults.AnyFiltered)
			{
				UIPanel panel = new UIPanel();
				panel.Width.Set(0f, 1f);
				this.modList.Add(panel);
				List<string> filterMessages = new List<string>();
				if (filterResults.filteredByEnabled > 0)
				{
					filterMessages.Add(Language.GetTextValue("tModLoader.ModsXModsFilteredByEnabled", filterResults.filteredByEnabled));
				}
				if (filterResults.filteredByModSide > 0)
				{
					filterMessages.Add(Language.GetTextValue("tModLoader.ModsXModsFilteredByModSide", filterResults.filteredByModSide));
				}
				if (filterResults.filteredBySearch > 0)
				{
					filterMessages.Add(Language.GetTextValue("tModLoader.ModsXModsFilteredBySearch", filterResults.filteredBySearch));
				}
				UIText text = new UIText(string.Join("\n", filterMessages), 1f, false);
				text.Width.Set(0f, 1f);
				text.IsWrapped = true;
				text.WrappedTextBottomPadding = 0f;
				text.TextOriginX = 0f;
				text.Recalculate();
				panel.Append(text);
				panel.Height.Set(text.MinHeight.Pixels + panel.PaddingTop, 0f);
			}
			this.modList.AddRange(visibleItems);
			this.Recalculate();
			this.modList.ViewPosition = this.modListViewPosition;
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x0051AD0C File Offset: 0x00518F0C
		public override void Draw(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
			base.Draw(spriteBatch);
			for (int i = 0; i < this._categoryButtons.Count; i++)
			{
				if (this._categoryButtons[i].IsMouseHovering)
				{
					string text;
					switch (i)
					{
					case 0:
						text = this.sortMode.ToFriendlyString();
						break;
					case 1:
						text = this.enabledFilterMode.ToFriendlyString();
						break;
					case 2:
						text = this.modSideFilterMode.ToFriendlyString();
						break;
					case 3:
						text = Language.GetTextValue("tModLoader.ShowMemoryEstimates" + (this.showRamUsage ? "Yes" : "No"));
						break;
					case 4:
						text = this.searchFilterMode.ToFriendlyString();
						break;
					default:
						text = "None";
						break;
					}
					UICommon.TooltipMouseText(text);
					return;
				}
			}
			if (this.buttonOMF.IsMouseHovering)
			{
				UICommon.TooltipMouseText(Language.GetTextValue("tModLoader.ModsOpenModsFoldersTooltip"));
			}
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x0051ADFC File Offset: 0x00518FFC
		public override void OnActivate()
		{
			this._cts = new CancellationTokenSource();
			Main.clrInput();
			this.modList.Clear();
			this.items.Clear();
			this.loading = true;
			this.uIPanel.Append(this.uiLoader);
			ConfigManager.LoadAll();
			this.Populate();
			this.UpdateTopRowButtons();
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x0051AE58 File Offset: 0x00519058
		public override void OnDeactivate()
		{
			CancellationTokenSource cts = this._cts;
			if (cts != null)
			{
				cts.Cancel(false);
			}
			CancellationTokenSource cts2 = this._cts;
			if (cts2 != null)
			{
				cts2.Dispose();
			}
			this._cts = null;
			this.modListViewPosition = this.modList.ViewPosition;
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x0051AE95 File Offset: 0x00519095
		internal void StoreCurrentScrollPosition()
		{
			this.modListViewPosition = this.modList.ViewPosition;
		}

		// Token: 0x06002A41 RID: 10817 RVA: 0x0051AEA8 File Offset: 0x005190A8
		internal void Populate()
		{
			this.modItemsTask = Task.Run<List<UIModItem>>(delegate()
			{
				LocalMod[] array = ModOrganizer.FindMods(true);
				List<UIModItem> pendingUIModItems = new List<UIModItem>();
				LocalMod[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					UIModItem modItem = new UIModItem(array2[i]);
					pendingUIModItems.Add(modItem);
				}
				return pendingUIModItems;
			}, this._cts.Token);
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x0051AFB4 File Offset: 0x005191B4
		[CompilerGenerated]
		private void <OnInitialize>g__ToggleRamButtonAction|39_12()
		{
			this.showRamUsage = !this.showRamUsage;
			this.uIPanel.AddOrRemoveChild(this.ramUsage, this.showRamUsage);
			if (this.showRamUsage)
			{
				this.ramUsage.Show();
			}
			int ramUsageSpace = this.showRamUsage ? 72 : 50;
			this.modList.Height.Pixels = (float)(-(float)ramUsageSpace);
			this.modList.Top.Pixels = (float)ramUsageSpace;
			this.uIScrollbar.Height.Pixels = (float)(-(float)ramUsageSpace);
			this.uIScrollbar.Top.Pixels = (float)ramUsageSpace;
			this.uIScrollbar.Recalculate();
		}

		// Token: 0x04001AED RID: 6893
		private UIElement uIElement;

		// Token: 0x04001AEE RID: 6894
		private UIPanel uIPanel;

		// Token: 0x04001AEF RID: 6895
		private UILoaderAnimatedImage uiLoader;

		// Token: 0x04001AF0 RID: 6896
		private bool needToRemoveLoading;

		// Token: 0x04001AF1 RID: 6897
		private UIList modList;

		// Token: 0x04001AF2 RID: 6898
		private UIScrollbar uIScrollbar;

		// Token: 0x04001AF3 RID: 6899
		private float modListViewPosition;

		// Token: 0x04001AF4 RID: 6900
		private readonly List<UIModItem> items = new List<UIModItem>();

		// Token: 0x04001AF5 RID: 6901
		private Task<List<UIModItem>> modItemsTask;

		// Token: 0x04001AF6 RID: 6902
		private bool updateNeeded;

		// Token: 0x04001AF7 RID: 6903
		private UIMemoryBar ramUsage;

		// Token: 0x04001AF8 RID: 6904
		private bool showRamUsage;

		// Token: 0x04001AF9 RID: 6905
		public bool loading;

		// Token: 0x04001AFA RID: 6906
		private UIInputTextField filterTextBox;

		// Token: 0x04001AFB RID: 6907
		public UICycleImage SearchFilterToggle;

		// Token: 0x04001AFC RID: 6908
		public ModsMenuSortMode sortMode;

		// Token: 0x04001AFD RID: 6909
		public EnabledFilter enabledFilterMode;

		// Token: 0x04001AFE RID: 6910
		public ModSideFilter modSideFilterMode;

		// Token: 0x04001AFF RID: 6911
		public SearchFilter searchFilterMode;

		// Token: 0x04001B00 RID: 6912
		internal readonly List<UICycleImage> _categoryButtons = new List<UICycleImage>();

		// Token: 0x04001B01 RID: 6913
		internal string filter;

		// Token: 0x04001B02 RID: 6914
		private UIAutoScaleTextTextPanel<LocalizedText> buttonEA;

		// Token: 0x04001B03 RID: 6915
		private UIAutoScaleTextTextPanel<LocalizedText> buttonDA;

		// Token: 0x04001B04 RID: 6916
		private UIAutoScaleTextTextPanel<LocalizedText> buttonRM;

		// Token: 0x04001B05 RID: 6917
		private UIAutoScaleTextTextPanel<LocalizedText> buttonB;

		// Token: 0x04001B06 RID: 6918
		private UIAutoScaleTextTextPanel<LocalizedText> buttonOMF;

		// Token: 0x04001B07 RID: 6919
		private UIAutoScaleTextTextPanel<LocalizedText> buttonCL;

		// Token: 0x04001B08 RID: 6920
		private UIAutoScaleTextTextPanel<LocalizedText> _confirmDialogYesButton;

		// Token: 0x04001B09 RID: 6921
		private UIAutoScaleTextTextPanel<LocalizedText> _confirmDialogNoButton;

		// Token: 0x04001B0A RID: 6922
		private UIText _confirmDialogText;

		// Token: 0x04001B0B RID: 6923
		private UIImage _blockInput;

		// Token: 0x04001B0C RID: 6924
		private UIPanel _toggleModsDialog;

		// Token: 0x04001B0D RID: 6925
		private CancellationTokenSource _cts;

		// Token: 0x02000A0E RID: 2574
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006C40 RID: 27712
			public static UIElement.MouseEvent <0>__OpenModsFolder;
		}
	}
}
