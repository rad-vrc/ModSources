using System;
using System.Linq;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Core;
using Terraria.Social.Base;
using Terraria.Social.Steam;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.ModLoader.UI
{
	// Token: 0x0200024F RID: 591
	internal class UIModInfo : UIState
	{
		// Token: 0x060029CB RID: 10699 RVA: 0x00514AE8 File Offset: 0x00512CE8
		public override void OnInitialize()
		{
			UIElement uielement = new UIElement();
			uielement.Width.Percent = 0.8f;
			uielement.MaxWidth = new StyleDimension(800f, 0f);
			uielement.Top.Pixels = 220f;
			uielement.Height.Pixels = -220f;
			uielement.Height.Percent = 1f;
			uielement.HAlign = 0.5f;
			this._uIElement = uielement;
			UIPanel uipanel = new UIPanel();
			uipanel.Width.Percent = 1f;
			uipanel.Height.Pixels = -110f;
			uipanel.Height.Percent = 1f;
			uipanel.BackgroundColor = UICommon.MainPanelBackground;
			UIPanel uIPanel = uipanel;
			this._uIElement.Append(uIPanel);
			UIMessageBox uimessageBox = new UIMessageBox(string.Empty);
			uimessageBox.Width.Pixels = -25f;
			uimessageBox.Width.Percent = 1f;
			uimessageBox.Height.Percent = 1f;
			this._modInfo = uimessageBox;
			uIPanel.Append(this._modInfo);
			UIScrollbar uiscrollbar = new UIScrollbar();
			uiscrollbar.Height.Pixels = -12f;
			uiscrollbar.Height.Percent = 1f;
			uiscrollbar.VAlign = 0.5f;
			uiscrollbar.HAlign = 1f;
			UIScrollbar uIScrollbar = uiscrollbar.WithView(100f, 1000f);
			uIPanel.Append(uIScrollbar);
			this._modInfo.SetScrollbar(uIScrollbar);
			UITextPanel<string> uitextPanel = new UITextPanel<string>(Language.GetTextValue("tModLoader.ModInfoHeader"), 0.8f, true);
			uitextPanel.HAlign = 0.5f;
			uitextPanel.Top.Pixels = -35f;
			uitextPanel.BackgroundColor = UICommon.DefaultUIBlue;
			this._uITextPanel = uitextPanel.WithPadding(15f);
			this._uIElement.Append(this._uITextPanel);
			UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("tModLoader.ModInfoVisitHomepage"), 1f, false);
			uiautoScaleTextTextPanel.Width.Pixels = -10f;
			uiautoScaleTextTextPanel.Width.Percent = 0.333f;
			uiautoScaleTextTextPanel.Height.Pixels = 40f;
			uiautoScaleTextTextPanel.HAlign = 0.5f;
			uiautoScaleTextTextPanel.VAlign = 1f;
			uiautoScaleTextTextPanel.Top.Pixels = -65f;
			this._modHomepageButton = uiautoScaleTextTextPanel.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this._modHomepageButton.OnLeftClick += this.VisitModHomePage;
			UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel2 = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("tModLoader.ModInfoVisitSteampage"), 1f, false);
			uiautoScaleTextTextPanel2.Width.Pixels = -10f;
			uiautoScaleTextTextPanel2.Width.Percent = 0.333f;
			uiautoScaleTextTextPanel2.Height.Pixels = 40f;
			uiautoScaleTextTextPanel2.HAlign = 0f;
			uiautoScaleTextTextPanel2.VAlign = 1f;
			uiautoScaleTextTextPanel2.Top.Pixels = -65f;
			this._modSteamButton = uiautoScaleTextTextPanel2.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this._modSteamButton.OnLeftClick += this.VisitModHostPage;
			UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel3 = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("tModLoader.ModInfoExtractLocalization"), 1f, false);
			uiautoScaleTextTextPanel3.Width.Pixels = -10f;
			uiautoScaleTextTextPanel3.Width.Percent = 0.333f;
			uiautoScaleTextTextPanel3.Height.Pixels = 40f;
			uiautoScaleTextTextPanel3.HAlign = 1f;
			uiautoScaleTextTextPanel3.VAlign = 1f;
			uiautoScaleTextTextPanel3.Top.Pixels = -65f;
			this.extractLocalizationButton = uiautoScaleTextTextPanel3.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this.extractLocalizationButton.OnLeftClick += this.ExtractLocalization;
			UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel4 = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("tModLoader.ModInfoExtractLocalization"), 1f, false);
			uiautoScaleTextTextPanel4.Width.Pixels = -10f;
			uiautoScaleTextTextPanel4.Width.Percent = 0.333f;
			uiautoScaleTextTextPanel4.Height.Pixels = 40f;
			uiautoScaleTextTextPanel4.HAlign = 1f;
			uiautoScaleTextTextPanel4.VAlign = 1f;
			uiautoScaleTextTextPanel4.Top.Pixels = -65f;
			this.fakeExtractLocalizationButton = uiautoScaleTextTextPanel4;
			this.fakeExtractLocalizationButton.BackgroundColor = Color.Gray;
			UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel5 = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("UI.Back"), 1f, false);
			uiautoScaleTextTextPanel5.Width.Pixels = -10f;
			uiautoScaleTextTextPanel5.Width.Percent = 0.333f;
			uiautoScaleTextTextPanel5.Height.Pixels = 40f;
			uiautoScaleTextTextPanel5.VAlign = 1f;
			uiautoScaleTextTextPanel5.Top.Pixels = -20f;
			UIAutoScaleTextTextPanel<string> backButton = uiautoScaleTextTextPanel5.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			backButton.OnLeftClick += this.BackClick;
			this._uIElement.Append(backButton);
			UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel6 = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("tModLoader.ModInfoExtract"), 1f, false);
			uiautoScaleTextTextPanel6.Width.Pixels = -10f;
			uiautoScaleTextTextPanel6.Width.Percent = 0.333f;
			uiautoScaleTextTextPanel6.Height.Pixels = 40f;
			uiautoScaleTextTextPanel6.VAlign = 1f;
			uiautoScaleTextTextPanel6.HAlign = 0.5f;
			uiautoScaleTextTextPanel6.Top.Pixels = -20f;
			this._extractButton = uiautoScaleTextTextPanel6.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this._extractButton.OnLeftClick += this.ExtractMod;
			UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel7 = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("UI.Delete"), 1f, false);
			uiautoScaleTextTextPanel7.Width.Pixels = -10f;
			uiautoScaleTextTextPanel7.Width.Percent = 0.333f;
			uiautoScaleTextTextPanel7.Height.Pixels = 40f;
			uiautoScaleTextTextPanel7.VAlign = 1f;
			uiautoScaleTextTextPanel7.HAlign = 1f;
			uiautoScaleTextTextPanel7.Top.Pixels = -20f;
			this._deleteButton = uiautoScaleTextTextPanel7.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this._deleteButton.OnLeftClick += this.DeleteMod;
			UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel8 = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("UI.Delete"), 1f, false);
			uiautoScaleTextTextPanel8.Width.Pixels = -10f;
			uiautoScaleTextTextPanel8.Width.Percent = 0.333f;
			uiautoScaleTextTextPanel8.Height.Pixels = 40f;
			uiautoScaleTextTextPanel8.VAlign = 1f;
			uiautoScaleTextTextPanel8.HAlign = 1f;
			uiautoScaleTextTextPanel8.Top.Pixels = -20f;
			this._fakeDeleteButton = uiautoScaleTextTextPanel8;
			this._fakeDeleteButton.BackgroundColor = Color.Gray;
			base.Append(this._uIElement);
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x005151E0 File Offset: 0x005133E0
		private void ExtractLocalization(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(SoundID.MenuOpen, null, null);
			if (LocalizationLoader.ExtractLocalizationFiles(this._modName))
			{
				this.extractLocalizationButton.SetText(Language.GetTextValue("tModLoader.ModInfoExtracted"));
			}
		}

		// Token: 0x060029CD RID: 10701 RVA: 0x00515224 File Offset: 0x00513424
		internal void Show(string modName, string displayName, int gotoMenu, LocalMod localMod, string description = "", string url = "")
		{
			this._modName = modName;
			this._modDisplayName = displayName;
			this._gotoMenu = gotoMenu;
			this._localMod = localMod;
			this._info = description;
			if (this._info.Equals(""))
			{
				this._info = Language.GetTextValue("tModLoader.ModInfoNoDescriptionAvailable");
			}
			this._url = url;
			ulong publishId;
			if (localMod != null && WorkshopHelper.GetPublishIdLocal(localMod.modFile, out publishId))
			{
				this._publishedFileId = new ModPubId_t
				{
					m_ModPubId = publishId.ToString()
				};
			}
			else
			{
				this._publishedFileId = default(ModPubId_t);
			}
			Main.gameMenu = true;
			Main.menuMode = 10008;
		}

		// Token: 0x060029CE RID: 10702 RVA: 0x005152D0 File Offset: 0x005134D0
		public override void OnDeactivate()
		{
			base.OnDeactivate();
			this._info = string.Empty;
			this._localMod = null;
			this._gotoMenu = 0;
			this._modName = string.Empty;
			this._modDisplayName = string.Empty;
			this._url = string.Empty;
			this._modHomepageButton.Remove();
			this._modSteamButton.Remove();
			this.extractLocalizationButton.Remove();
			this.fakeExtractLocalizationButton.Remove();
			this._deleteButton.Remove();
			this._fakeDeleteButton.Remove();
			this._extractButton.Remove();
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x0051536C File Offset: 0x0051356C
		private void BackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(SoundID.MenuClose, null, null);
			Main.menuMode = this._gotoMenu;
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x0051539C File Offset: 0x0051359C
		private void ExtractMod(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(SoundID.MenuOpen, null, null);
			Interface.extractMod.Show(this._localMod, this._gotoMenu);
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x005153D4 File Offset: 0x005135D4
		private void DeleteMod(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(SoundID.MenuClose, null, null);
			ModOrganizer.DeleteMod(this._localMod);
			Main.menuMode = this._gotoMenu;
		}

		// Token: 0x060029D2 RID: 10706 RVA: 0x0051540C File Offset: 0x0051360C
		private void VisitModHomePage(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Utils.OpenToURL(this._url);
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x0051542E File Offset: 0x0051362E
		private void VisitModHostPage(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			this.VisitModHostPageInner();
		}

		// Token: 0x060029D4 RID: 10708 RVA: 0x0051544B File Offset: 0x0051364B
		private void VisitModHostPageInner()
		{
			Utils.OpenToURL(Interface.modBrowser.SocialBackend.GetModWebPage(this._publishedFileId));
		}

		// Token: 0x060029D5 RID: 10709 RVA: 0x00515468 File Offset: 0x00513668
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 100;
			UILinkPointNavigator.Shortcuts.BackButtonGoto = this._gotoMenu;
			if (this._modHomepageButton.IsMouseHovering)
			{
				UICommon.TooltipMouseText(this._url);
			}
			if (this._fakeDeleteButton.IsMouseHovering)
			{
				UICommon.TooltipMouseText(Language.GetTextValue("tModLoader.ModInfoDisableModToDelete"));
			}
			if (this.fakeExtractLocalizationButton.IsMouseHovering)
			{
				UICommon.TooltipMouseText(Language.GetTextValue("tModLoader.ModInfoEnableModToExtractLocalizationFiles"));
			}
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x005154E0 File Offset: 0x005136E0
		public override void OnActivate()
		{
			this._modInfo.SetText(this._info);
			this._uITextPanel.SetText(Language.GetTextValue("tModLoader.ModInfoHeader") + this._modDisplayName, 0.8f, true);
			this._loading = false;
			this._ready = true;
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x00515534 File Offset: 0x00513734
		public override void Update(GameTime gameTime)
		{
			if (!this._loading && this._ready)
			{
				this._modInfo.SetText(this._info);
				if (!string.IsNullOrEmpty(this._url))
				{
					this._uIElement.Append(this._modHomepageButton);
				}
				if (!string.IsNullOrEmpty(this._publishedFileId.m_ModPubId))
				{
					this._uIElement.Append(this._modSteamButton);
				}
				if (this._localMod != null)
				{
					bool realDeleteButton = ModLoader.Mods.All((Mod x) => x.Name != this._localMod.Name);
					this._uIElement.AddOrRemoveChild(this._deleteButton, realDeleteButton);
					this._uIElement.AddOrRemoveChild(this._fakeDeleteButton, !realDeleteButton);
					this._uIElement.AddOrRemoveChild(this.extractLocalizationButton, !realDeleteButton);
					this._uIElement.AddOrRemoveChild(this.fakeExtractLocalizationButton, realDeleteButton);
					this.extractLocalizationButton.SetText(Language.GetTextValue("tModLoader.ModInfoExtractLocalization"));
					this._uIElement.Append(this._extractButton);
				}
				this.Recalculate();
				this._modInfo.RemoveChild(this._loaderElement);
				this._ready = false;
			}
			base.Update(gameTime);
		}

		// Token: 0x04001A90 RID: 6800
		private UIElement _uIElement;

		// Token: 0x04001A91 RID: 6801
		private UIMessageBox _modInfo;

		// Token: 0x04001A92 RID: 6802
		private UITextPanel<string> _uITextPanel;

		// Token: 0x04001A93 RID: 6803
		private UIAutoScaleTextTextPanel<string> _modHomepageButton;

		// Token: 0x04001A94 RID: 6804
		private UIAutoScaleTextTextPanel<string> _modSteamButton;

		// Token: 0x04001A95 RID: 6805
		private UIAutoScaleTextTextPanel<string> extractLocalizationButton;

		// Token: 0x04001A96 RID: 6806
		private UIAutoScaleTextTextPanel<string> fakeExtractLocalizationButton;

		// Token: 0x04001A97 RID: 6807
		private UIAutoScaleTextTextPanel<string> _extractButton;

		// Token: 0x04001A98 RID: 6808
		private UIAutoScaleTextTextPanel<string> _deleteButton;

		// Token: 0x04001A99 RID: 6809
		private UIAutoScaleTextTextPanel<string> _fakeDeleteButton;

		// Token: 0x04001A9A RID: 6810
		private readonly UILoaderAnimatedImage _loaderElement = new UILoaderAnimatedImage(0.5f, 0.5f, 1f);

		// Token: 0x04001A9B RID: 6811
		private int _gotoMenu;

		// Token: 0x04001A9C RID: 6812
		private LocalMod _localMod;

		// Token: 0x04001A9D RID: 6813
		private string _url = string.Empty;

		// Token: 0x04001A9E RID: 6814
		private string _info = string.Empty;

		// Token: 0x04001A9F RID: 6815
		private string _modName = string.Empty;

		// Token: 0x04001AA0 RID: 6816
		private string _modDisplayName = string.Empty;

		// Token: 0x04001AA1 RID: 6817
		private ModPubId_t _publishedFileId;

		// Token: 0x04001AA2 RID: 6818
		private bool _loading;

		// Token: 0x04001AA3 RID: 6819
		private bool _ready;

		// Token: 0x04001AA4 RID: 6820
		private CancellationTokenSource _cts;
	}
}
