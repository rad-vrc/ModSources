using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.ModLoader.UI
{
	// Token: 0x02000249 RID: 585
	internal class UIInfoMessage : UIState, IHaveBackButtonCommand
	{
		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060029A3 RID: 10659 RVA: 0x005138A6 File Offset: 0x00511AA6
		// (set) Token: 0x060029A4 RID: 10660 RVA: 0x005138AE File Offset: 0x00511AAE
		public UIState PreviousUIState { get; set; }

		// Token: 0x060029A5 RID: 10661 RVA: 0x005138B8 File Offset: 0x00511AB8
		public override void OnInitialize()
		{
			UIElement uielement = new UIElement();
			uielement.Width.Percent = 0.8f;
			uielement.Top.Pixels = 200f;
			uielement.Height.Pixels = -240f;
			uielement.Height.Percent = 1f;
			uielement.HAlign = 0.5f;
			this._area = uielement;
			UIPanel uipanel = new UIPanel();
			uipanel.Width.Percent = 1f;
			uipanel.Height.Pixels = -110f;
			uipanel.Height.Percent = 1f;
			uipanel.BackgroundColor = UICommon.MainPanelBackground;
			UIPanel uIPanel = uipanel;
			this._area.Append(uIPanel);
			UIMessageBox uimessageBox = new UIMessageBox(string.Empty);
			uimessageBox.Width.Pixels = -25f;
			uimessageBox.Width.Percent = 1f;
			uimessageBox.Height.Percent = 1f;
			this._messageBox = uimessageBox;
			uIPanel.Append(this._messageBox);
			UIScrollbar uiscrollbar = new UIScrollbar();
			uiscrollbar.Height.Pixels = -12f;
			uiscrollbar.Height.Percent = 1f;
			uiscrollbar.VAlign = 0.5f;
			uiscrollbar.HAlign = 1f;
			UIScrollbar uIScrollbar = uiscrollbar.WithView(100f, 1000f);
			uIPanel.Append(uIScrollbar);
			this._messageBox.SetScrollbar(uIScrollbar);
			UITextPanel<string> uitextPanel = new UITextPanel<string>(Language.GetTextValue("tModLoader.OK"), 0.7f, true);
			uitextPanel.Width.Pixels = -10f;
			uitextPanel.Width.Percent = 0.5f;
			uitextPanel.Height.Pixels = 50f;
			uitextPanel.Left.Percent = 0.25f;
			uitextPanel.VAlign = 1f;
			uitextPanel.Top.Pixels = -30f;
			this._button = uitextPanel.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this._button.OnLeftClick += this.OKClick;
			this._area.Append(this._button);
			UITextPanel<string> uitextPanel2 = new UITextPanel<string>("???", 0.7f, true);
			uitextPanel2.Width.Pixels = -10f;
			uitextPanel2.Width.Percent = 0.5f;
			uitextPanel2.Height.Pixels = 50f;
			uitextPanel2.Left.Percent = 0.5f;
			uitextPanel2.VAlign = 1f;
			uitextPanel2.Top.Pixels = -30f;
			this._buttonAlt = uitextPanel2.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this._buttonAlt.OnLeftClick += this.AltClick;
			this._area.Append(this._buttonAlt);
			base.Append(this._area);
		}

		// Token: 0x060029A6 RID: 10662 RVA: 0x00513BA8 File Offset: 0x00511DA8
		public override void OnActivate()
		{
			this._messageBox.SetText(this._message);
			this._buttonAlt.SetText(this._altText);
			if (this._okText != null)
			{
				this._button.SetText(this._okText);
			}
			bool showAlt = !string.IsNullOrEmpty(this._altText);
			this._button.Left.Percent = (showAlt ? 0f : 0.25f);
			this._area.AddOrRemoveChild(this._buttonAlt, showAlt);
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x00513C30 File Offset: 0x00511E30
		internal void Show(string message, int gotoMenu, UIState gotoState = null, string altButtonText = "", Action altButtonAction = null, string okButtonText = null)
		{
			if (!Program.IsMainThread)
			{
				Main.QueueMainThreadAction(delegate
				{
					this.Show(message, gotoMenu, gotoState, altButtonText, altButtonAction, okButtonText);
				});
				return;
			}
			this._message = message;
			this._gotoMenu = gotoMenu;
			this._gotoState = gotoState;
			this._altText = altButtonText;
			this._altAction = altButtonAction;
			this._okText = okButtonText;
			Main.menuMode = 10013;
			Main.MenuUI.SetState(null);
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x00513CED File Offset: 0x00511EED
		private void OKClick(UIMouseEvent evt, UIElement listeningElement)
		{
			this.HandleBackButtonUsage();
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x00513CF8 File Offset: 0x00511EF8
		public void HandleBackButtonUsage()
		{
			SoundEngine.PlaySound(SoundID.MenuOpen, null, null);
			Main.menuMode = this._gotoMenu;
			if (this._gotoState != null)
			{
				Main.MenuUI.SetState(this._gotoState);
			}
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x00513D3D File Offset: 0x00511F3D
		private void AltClick(UIMouseEvent evt, UIElement listeningElement)
		{
			this.HandleBackButtonUsage();
			this._altAction();
		}

		// Token: 0x060029AB RID: 10667 RVA: 0x00513D50 File Offset: 0x00511F50
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
		}

		// Token: 0x04001A6D RID: 6765
		protected UIElement _area;

		// Token: 0x04001A6E RID: 6766
		private UIMessageBox _messageBox;

		// Token: 0x04001A6F RID: 6767
		protected UITextPanel<string> _button;

		// Token: 0x04001A70 RID: 6768
		private UITextPanel<string> _buttonAlt;

		// Token: 0x04001A71 RID: 6769
		private UIState _gotoState;

		// Token: 0x04001A72 RID: 6770
		private string _message;

		// Token: 0x04001A73 RID: 6771
		private int _gotoMenu;

		// Token: 0x04001A74 RID: 6772
		private Action _altAction;

		// Token: 0x04001A75 RID: 6773
		private string _altText;

		// Token: 0x04001A76 RID: 6774
		private string _okText;
	}
}
