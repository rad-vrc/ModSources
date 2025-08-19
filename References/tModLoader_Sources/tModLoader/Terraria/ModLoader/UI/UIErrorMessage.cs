using System;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.ModLoader.UI
{
	// Token: 0x02000243 RID: 579
	internal class UIErrorMessage : UIState
	{
		// Token: 0x06002971 RID: 10609 RVA: 0x005121B8 File Offset: 0x005103B8
		public override void OnInitialize()
		{
			UIElement uielement = new UIElement();
			uielement.Width.Percent = 0.8f;
			uielement.Top.Pixels = 200f;
			uielement.Height.Pixels = -210f;
			uielement.Height.Percent = 1f;
			uielement.HAlign = 0.5f;
			this.area = uielement;
			UIPanel uipanel = new UIPanel();
			uipanel.Width.Percent = 1f;
			uipanel.Height.Pixels = -110f;
			uipanel.Height.Percent = 1f;
			uipanel.BackgroundColor = UICommon.MainPanelBackground;
			UIPanel uIPanel = uipanel;
			this.area.Append(uIPanel);
			UIMessageBox uimessageBox = new UIMessageBox(string.Empty);
			uimessageBox.Width.Pixels = -25f;
			uimessageBox.Width.Percent = 1f;
			uimessageBox.Height.Percent = 1f;
			this.messageBox = uimessageBox;
			uIPanel.Append(this.messageBox);
			UIScrollbar uiscrollbar = new UIScrollbar();
			uiscrollbar.Height.Pixels = -12f;
			uiscrollbar.Height.Percent = 1f;
			uiscrollbar.VAlign = 0.5f;
			uiscrollbar.HAlign = 1f;
			UIScrollbar uIScrollbar = uiscrollbar.WithView(100f, 1000f);
			uIPanel.Append(uIScrollbar);
			this.messageBox.SetScrollbar(uIScrollbar);
			UITextPanel<string> uitextPanel = new UITextPanel<string>("", 0.7f, true);
			uitextPanel.Width.Pixels = -10f;
			uitextPanel.Width.Percent = 0.5f;
			uitextPanel.Height.Pixels = 50f;
			uitextPanel.Top.Pixels = -108f;
			uitextPanel.Top.Percent = 1f;
			this.continueButton = uitextPanel;
			this.continueButton.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this.continueButton.OnLeftClick += this.ContinueClick;
			this.area.Append(this.continueButton);
			UITextPanel<string> openLogsButton = new UITextPanel<string>(Language.GetTextValue("tModLoader.OpenLogs"), 0.7f, true);
			openLogsButton.CopyStyle(this.continueButton);
			openLogsButton.HAlign = 1f;
			openLogsButton.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			openLogsButton.OnLeftClick += this.OpenFile;
			this.area.Append(openLogsButton);
			this.webHelpButton = new UITextPanel<string>(Language.GetTextValue("tModLoader.OpenWebHelp"), 0.7f, true);
			this.webHelpButton.CopyStyle(openLogsButton);
			this.webHelpButton.Top.Set(-55f, 1f);
			this.webHelpButton.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this.webHelpButton.OnLeftClick += this.VisitRegisterWebpage;
			this.area.Append(this.webHelpButton);
			this.skipLoadButton = new UITextPanel<string>(Language.GetTextValue("tModLoader.SkipToMainMenu"), 0.7f, true);
			this.skipLoadButton.CopyStyle(this.continueButton);
			this.skipLoadButton.Top.Set(-55f, 1f);
			this.skipLoadButton.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this.skipLoadButton.OnLeftClick += this.SkipLoad;
			this.area.Append(this.skipLoadButton);
			this.exitAndDisableAllButton = new UITextPanel<string>(Language.GetTextValue("tModLoader.ExitAndDisableAll"), 0.7f, true);
			this.exitAndDisableAllButton.CopyStyle(this.skipLoadButton);
			this.exitAndDisableAllButton.TextColor = Color.Red;
			this.exitAndDisableAllButton.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this.exitAndDisableAllButton.OnLeftClick += this.ExitAndDisableAll;
			this.retryButton = new UITextPanel<string>("Retry", 0.7f, true);
			this.retryButton.CopyStyle(this.continueButton);
			this.retryButton.Top.Set(-50f, 1f);
			this.retryButton.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this.retryButton.OnLeftClick += delegate(UIMouseEvent evt, UIElement elem)
			{
				this.retryAction();
			};
			base.Append(this.area);
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x00512694 File Offset: 0x00510894
		public override void OnActivate()
		{
			Netplay.Disconnect = true;
			this.messageBox.SetText(this.message);
			string continueKey = (this.gotoMenu < 0) ? "Exit" : (this.continueIsRetry ? "Retry" : "Continue");
			this.continueButton.SetText(Language.GetTextValue("tModLoader." + continueKey));
			this.continueButton.TextColor = ((this.gotoMenu >= 0) ? Color.White : Color.Red);
			this.area.AddOrRemoveChild(this.webHelpButton, !string.IsNullOrEmpty(this.webHelpURL));
			this.area.AddOrRemoveChild(this.skipLoadButton, this.showSkip);
			this.area.AddOrRemoveChild(this.exitAndDisableAllButton, this.gotoMenu < 0);
			this.area.AddOrRemoveChild(this.retryButton, this.retryAction != null);
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x00512782 File Offset: 0x00510982
		public override void OnDeactivate()
		{
			this.retryAction = null;
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x0051278C File Offset: 0x0051098C
		internal void Show(string message, int gotoMenu, UIState gotoState = null, string webHelpURL = "", bool continueIsRetry = false, bool showSkip = false, Action retryAction = null)
		{
			if (!Program.IsMainThread)
			{
				Main.QueueMainThreadAction(delegate
				{
					this.Show(message, gotoMenu, gotoState, webHelpURL, continueIsRetry, showSkip, retryAction);
				});
				return;
			}
			this.message = message;
			this.gotoMenu = gotoMenu;
			this.gotoState = gotoState;
			this.webHelpURL = webHelpURL;
			this.continueIsRetry = continueIsRetry;
			this.showSkip = showSkip;
			this.retryAction = retryAction;
			Main.gameMenu = true;
			Main.menuMode = 10005;
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x00512858 File Offset: 0x00510A58
		private void ContinueClick(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(SoundID.MenuOpen, null, null);
			if (this.gotoMenu < 0)
			{
				Main.instance.Exit();
			}
			Main.menuMode = this.gotoMenu;
			if (this.gotoState != null)
			{
				Main.MenuUI.SetState(this.gotoState);
			}
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x005128B0 File Offset: 0x00510AB0
		private void ExitAndDisableAll(UIMouseEvent evt, UIElement listeningElement)
		{
			ModLoader.DisableAllMods();
			Main.instance.Exit();
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x005128C4 File Offset: 0x00510AC4
		private void OpenFile(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(SoundID.MenuOpen, null, null);
			Utils.OpenFolder(Logging.LogDir);
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x005128F0 File Offset: 0x00510AF0
		private void VisitRegisterWebpage(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(SoundID.MenuOpen, null, null);
			Utils.OpenToURL(this.webHelpURL);
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x0051291D File Offset: 0x00510B1D
		private void SkipLoad(UIMouseEvent evt, UIElement listeningElement)
		{
			this.ContinueClick(evt, listeningElement);
			ModLoader.skipLoad = true;
		}

		// Token: 0x04001A45 RID: 6725
		private UIMessageBox messageBox;

		// Token: 0x04001A46 RID: 6726
		private UIElement area;

		// Token: 0x04001A47 RID: 6727
		private UITextPanel<string> continueButton;

		// Token: 0x04001A48 RID: 6728
		private UITextPanel<string> exitAndDisableAllButton;

		// Token: 0x04001A49 RID: 6729
		private UITextPanel<string> webHelpButton;

		// Token: 0x04001A4A RID: 6730
		private UITextPanel<string> skipLoadButton;

		// Token: 0x04001A4B RID: 6731
		private UITextPanel<string> retryButton;

		// Token: 0x04001A4C RID: 6732
		private string message;

		// Token: 0x04001A4D RID: 6733
		private int gotoMenu;

		// Token: 0x04001A4E RID: 6734
		private UIState gotoState;

		// Token: 0x04001A4F RID: 6735
		private string webHelpURL;

		// Token: 0x04001A50 RID: 6736
		private bool continueIsRetry;

		// Token: 0x04001A51 RID: 6737
		private bool showSkip;

		// Token: 0x04001A52 RID: 6738
		private Action retryAction;
	}
}
