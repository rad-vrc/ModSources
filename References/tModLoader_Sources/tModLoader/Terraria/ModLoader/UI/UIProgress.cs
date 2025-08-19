using System;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.ModLoader.UI
{
	// Token: 0x0200025D RID: 605
	internal class UIProgress : UIState
	{
		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06002A8E RID: 10894 RVA: 0x0051D330 File Offset: 0x0051B530
		// (remove) Token: 0x06002A8F RID: 10895 RVA: 0x0051D368 File Offset: 0x0051B568
		public event Action OnCancel;

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06002A90 RID: 10896 RVA: 0x0051D39D File Offset: 0x0051B59D
		// (set) Token: 0x06002A91 RID: 10897 RVA: 0x0051D3B4 File Offset: 0x0051B5B4
		public float Progress
		{
			get
			{
				UIProgressBar progressBar = this._progressBar;
				if (progressBar == null)
				{
					return 0f;
				}
				return progressBar.Progress;
			}
			set
			{
				UIProgressBar progressBar = this._progressBar;
				if (progressBar == null)
				{
					return;
				}
				progressBar.UpdateProgress(value);
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (set) Token: 0x06002A92 RID: 10898 RVA: 0x0051D3C7 File Offset: 0x0051B5C7
		public string SubProgressText
		{
			set
			{
				UIText uitext = this.subProgress;
				if (uitext == null)
				{
					return;
				}
				uitext.SetText(value);
			}
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x0051D3DC File Offset: 0x0051B5DC
		public override void OnInitialize()
		{
			UIProgressBar uiprogressBar = new UIProgressBar();
			uiprogressBar.Width.Percent = 0.8f;
			uiprogressBar.MaxWidth = UICommon.MaxPanelWidth;
			uiprogressBar.Height.Pixels = 150f;
			uiprogressBar.HAlign = 0.5f;
			uiprogressBar.VAlign = 0.5f;
			uiprogressBar.Top.Pixels = 10f;
			this._progressBar = uiprogressBar;
			base.Append(this._progressBar);
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Cancel"), 0.75f, true);
			uitextPanel.VAlign = 0.5f;
			uitextPanel.HAlign = 0.5f;
			uitextPanel.Top.Pixels = 170f;
			this._cancelButton = uitextPanel.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this._cancelButton.OnLeftClick += this.CancelClick;
			base.Append(this._cancelButton);
			UIText uitext = new UIText("", 0.5f, true);
			uitext.Top.Pixels = 65f;
			uitext.HAlign = 0.5f;
			uitext.VAlign = 0.5f;
			this.subProgress = uitext;
			base.Append(this.subProgress);
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x0051D528 File Offset: 0x0051B728
		private void CancelClick(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(SoundID.MenuOpen, null, null);
			Main.menuMode = this.gotoMenu;
			Action onCancel = this.OnCancel;
			if (onCancel == null)
			{
				return;
			}
			onCancel();
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x0051D565 File Offset: 0x0051B765
		public void Show(string displayText = "", int gotoMenu = 0, Action cancel = null)
		{
			if (Main.MenuUI.CurrentState == this)
			{
				Main.MenuUI.RefreshState();
			}
			else
			{
				Main.menuMode = 10023;
			}
			this.DisplayText = displayText;
			this.gotoMenu = gotoMenu;
			if (cancel != null)
			{
				this.OnCancel += cancel;
			}
		}

		// Token: 0x06002A96 RID: 10902 RVA: 0x0051D5A2 File Offset: 0x0051B7A2
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			this._progressBar.DisplayText = this.DisplayText;
		}

		// Token: 0x06002A97 RID: 10903 RVA: 0x0051D5BC File Offset: 0x0051B7BC
		public override void OnDeactivate()
		{
			this.DisplayText = null;
			this.OnCancel = null;
			this.gotoMenu = 0;
			this.Progress = 0f;
		}

		// Token: 0x04001B34 RID: 6964
		public int gotoMenu;

		// Token: 0x04001B35 RID: 6965
		protected UIProgressBar _progressBar;

		// Token: 0x04001B36 RID: 6966
		protected UITextPanel<LocalizedText> _cancelButton;

		// Token: 0x04001B37 RID: 6967
		public string DisplayText;

		// Token: 0x04001B38 RID: 6968
		protected UIText subProgress;
	}
}
