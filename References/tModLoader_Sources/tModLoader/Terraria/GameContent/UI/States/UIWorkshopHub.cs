using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020004E2 RID: 1250
	public class UIWorkshopHub : UIState, IHaveBackButtonCommand
	{
		// Token: 0x14000060 RID: 96
		// (add) Token: 0x06003C72 RID: 15474 RVA: 0x005C13A4 File Offset: 0x005BF5A4
		// (remove) Token: 0x06003C73 RID: 15475 RVA: 0x005C13D8 File Offset: 0x005BF5D8
		public static event Action OnWorkshopHubMenuOpened;

		// Token: 0x06003C74 RID: 15476 RVA: 0x005C140B File Offset: 0x005BF60B
		public UIWorkshopHub(UIState stateToGoBackTo)
		{
			this._previousUIState = stateToGoBackTo;
		}

		// Token: 0x06003C75 RID: 15477 RVA: 0x005C141A File Offset: 0x005BF61A
		public void EnterHub()
		{
			UIWorkshopHub.OnWorkshopHubMenuOpened();
		}

		// Token: 0x06003C76 RID: 15478 RVA: 0x005C1428 File Offset: 0x005BF628
		public override void OnInitialize()
		{
			base.OnInitialize();
			int num = 20;
			int num2 = 250;
			int num3 = 50 + num * 2;
			int num4 = Main.minScreenH;
			int num5 = num4 - num2 - num3;
			UIElement uIElement = new UIElement();
			uIElement.Width.Set(600f, 0f);
			uIElement.Top.Set((float)num2, 0f);
			uIElement.Height.Set((float)(num4 - num2), 0f);
			uIElement.HAlign = 0.5f;
			int num6 = 284;
			UIPanel uIPanel = new UIPanel();
			uIPanel.Width.Set(0f, 1f);
			uIPanel.Height.Set((float)num5, 0f);
			uIPanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
			UIElement uIElement2 = new UIElement();
			uIElement2.Width.Set(0f, 1f);
			uIElement2.Height.Set((float)num6, 0f);
			uIElement2.SetPadding(0f);
			UITextPanel<LocalizedText> uITextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.WorkshopHub"), 0.8f, true);
			uITextPanel.HAlign = 0.5f;
			uITextPanel.Top.Set(-46f, 0f);
			uITextPanel.SetPadding(15f);
			uITextPanel.BackgroundColor = new Color(73, 94, 171);
			UITextPanel<LocalizedText> uITextPanel2 = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
			uITextPanel2.Width.Set(-10f, 0.5f);
			uITextPanel2.Height.Set(50f, 0f);
			uITextPanel2.VAlign = 1f;
			uITextPanel2.HAlign = 0f;
			uITextPanel2.Top.Set((float)(-(float)num), 0f);
			uITextPanel2.OnMouseOver += this.FadedMouseOver;
			uITextPanel2.OnMouseOut += this.FadedMouseOut;
			uITextPanel2.OnLeftClick += this.GoBackClick;
			uITextPanel2.SetSnapPoint("Back", 0, null, null);
			uIElement.Append(uITextPanel2);
			this._buttonBack = uITextPanel2;
			UITextPanel<LocalizedText> uITextPanel3 = new UITextPanel<LocalizedText>(Language.GetText("Workshop.ReportLogsButton"), 0.7f, true);
			uITextPanel3.Width.Set(-10f, 0.5f);
			uITextPanel3.Height.Set(50f, 0f);
			uITextPanel3.VAlign = 1f;
			uITextPanel3.HAlign = 1f;
			uITextPanel3.Top.Set((float)(-(float)num), 0f);
			uITextPanel3.OnMouseOver += this.FadedMouseOver;
			uITextPanel3.OnMouseOut += this.FadedMouseOut;
			uITextPanel3.OnLeftClick += this.GoLogsClick;
			uITextPanel3.SetSnapPoint("Logs", 0, null, null);
			uIElement.Append(uITextPanel3);
			this._buttonLogs = uITextPanel3;
			UIElement uIElement3 = this.MakeButton_OpenWorkshopWorldsImportMenu();
			uIElement3.HAlign = 0f;
			uIElement3.VAlign = 1f;
			uIElement2.Append(uIElement3);
			uIElement3 = this.MakeButton_OpenUseResourcePacksMenu();
			uIElement3.HAlign = 1f;
			uIElement3.VAlign = 1f;
			uIElement2.Append(uIElement3);
			this.AppendTmlElements(uIElement2);
			UIWorkshopHub.AddHorizontalSeparator(uIPanel, (float)(num6 + 6 + 6));
			this.AddDescriptionPanel(uIPanel, (float)(num6 + 8 + 6 + 6), (float)(num5 - num6 - 12 - 12 - 8), "desc");
			uIPanel.Append(uIElement2);
			uIElement.Append(uIPanel);
			uIElement.Append(uITextPanel);
			base.Append(uIElement);
		}

		// Token: 0x06003C77 RID: 15479 RVA: 0x005C17F8 File Offset: 0x005BF9F8
		private UIElement MakeButton_OpenUseResourcePacksMenu()
		{
			UIElement uIElement = this.MakeFancyButton("Images/UI/Workshop/HubResourcepacks", "Workshop.HubResourcePacks");
			uIElement.OnLeftClick += this.Click_OpenResourcePacksMenu;
			this._buttonUseResourcePacks = uIElement;
			return uIElement;
		}

		// Token: 0x06003C78 RID: 15480 RVA: 0x005C1830 File Offset: 0x005BFA30
		private void Click_OpenResourcePacksMenu(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.OpenResourcePacksMenu(this);
		}

		// Token: 0x06003C79 RID: 15481 RVA: 0x005C1850 File Offset: 0x005BFA50
		private UIElement MakeButton_OpenWorkshopWorldsImportMenu()
		{
			UIElement uIElement = this.MakeFancyButton("Images/UI/Workshop/HubWorlds", "Workshop.HubWorlds");
			uIElement.OnLeftClick += this.Click_OpenWorldImportMenu;
			this._buttonImportWorlds = uIElement;
			return uIElement;
		}

		// Token: 0x06003C7A RID: 15482 RVA: 0x005C1888 File Offset: 0x005BFA88
		private void Click_OpenWorldImportMenu(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.MenuUI.SetState(new UIWorkshopWorldImport(this));
		}

		// Token: 0x06003C7B RID: 15483 RVA: 0x005C18B0 File Offset: 0x005BFAB0
		private UIElement MakeButton_OpenPublishResourcePacksMenu()
		{
			UIElement uIElement = this.MakeFancyButton("Images/UI/Workshop/HubPublishResourcepacks", "Workshop.HubPublishResourcePacks");
			uIElement.OnLeftClick += this.Click_OpenResourcePackPublishMenu;
			this._buttonPublishResourcePacks = uIElement;
			return uIElement;
		}

		// Token: 0x06003C7C RID: 15484 RVA: 0x005C18E8 File Offset: 0x005BFAE8
		private void Click_OpenResourcePackPublishMenu(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.MenuUI.SetState(new UIWorkshopSelectResourcePackToPublish(this));
		}

		// Token: 0x06003C7D RID: 15485 RVA: 0x005C1910 File Offset: 0x005BFB10
		private UIElement MakeButton_OpenPublishWorldsMenu()
		{
			UIElement uIElement = this.MakeFancyButton("Images/UI/Workshop/HubPublishWorlds", "Workshop.HubPublishWorlds");
			uIElement.OnLeftClick += this.Click_OpenWorldPublishMenu;
			this._buttonPublishWorlds = uIElement;
			return uIElement;
		}

		// Token: 0x06003C7E RID: 15486 RVA: 0x005C1948 File Offset: 0x005BFB48
		private void Click_OpenWorldPublishMenu(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.MenuUI.SetState(new UIWorkshopSelectWorldToPublish(this));
		}

		// Token: 0x06003C7F RID: 15487 RVA: 0x005C1970 File Offset: 0x005BFB70
		private static void AddHorizontalSeparator(UIElement Container, float accumualtedHeight)
		{
			UIHorizontalSeparator element = new UIHorizontalSeparator(2, true)
			{
				Width = StyleDimension.FromPercent(1f),
				Top = StyleDimension.FromPixels(accumualtedHeight - 8f),
				Color = Color.Lerp(Color.White, new Color(63, 65, 151, 255), 0.85f) * 0.9f
			};
			Container.Append(element);
		}

		// Token: 0x06003C80 RID: 15488 RVA: 0x005C19E0 File Offset: 0x005BFBE0
		public void ShowOptionDescription(UIMouseEvent evt, UIElement listeningElement)
		{
			LocalizedText localizedText = null;
			this.OnChooseOptionDescription(listeningElement, ref localizedText);
			if (listeningElement == this._buttonUseResourcePacks)
			{
				localizedText = Language.GetText("Workshop.HubDescriptionUseResourcePacks");
			}
			if (listeningElement == this._buttonPublishResourcePacks)
			{
				localizedText = Language.GetText("Workshop.HubDescriptionPublishResourcePacks");
			}
			if (listeningElement == this._buttonImportWorlds)
			{
				localizedText = Language.GetText("Workshop.HubDescriptionImportWorlds");
			}
			if (listeningElement == this._buttonPublishWorlds)
			{
				localizedText = Language.GetText("Workshop.HubDescriptionPublishWorlds");
			}
			if (localizedText != null)
			{
				this._descriptionText.SetText(localizedText);
			}
		}

		// Token: 0x06003C81 RID: 15489 RVA: 0x005C1A57 File Offset: 0x005BFC57
		public void ClearOptionDescription(UIMouseEvent evt, UIElement listeningElement)
		{
			this._descriptionText.SetText(Language.GetText("Workshop.HubDescriptionDefault"));
		}

		// Token: 0x06003C82 RID: 15490 RVA: 0x005C1A70 File Offset: 0x005BFC70
		private void AddDescriptionPanel(UIElement container, float accumulatedHeight, float height, string tagGroup)
		{
			float num = 0f;
			UISlicedImage uISlicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight"))
			{
				HAlign = 0.5f,
				VAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent((0f - num) * 2f, 1f),
				Left = StyleDimension.FromPixels(0f - num),
				Height = StyleDimension.FromPixelsAndPercent(height, 0f),
				Top = StyleDimension.FromPixels(2f)
			};
			uISlicedImage.SetSliceDepths(10);
			uISlicedImage.Color = Color.LightGray * 0.7f;
			container.Append(uISlicedImage);
			UIText uIText = new UIText(Language.GetText("Workshop.HubDescriptionDefault"), 1f, false)
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Top = StyleDimension.FromPixelsAndPercent(5f, 0f)
			};
			uIText.PaddingLeft = 20f;
			uIText.PaddingRight = 20f;
			uIText.PaddingTop = 6f;
			uIText.IsWrapped = true;
			uISlicedImage.Append(uIText);
			this._descriptionText = uIText;
		}

		// Token: 0x06003C83 RID: 15491 RVA: 0x005C1BC3 File Offset: 0x005BFDC3
		private UIElement MakeFancyButton(string iconImagePath, string textKey)
		{
			return this.MakeFancyButton_Inner(Main.Assets.Request<Texture2D>(iconImagePath), textKey);
		}

		// Token: 0x06003C84 RID: 15492 RVA: 0x005C1BD8 File Offset: 0x005BFDD8
		private UIElement MakeFancyButton_Inner(Asset<Texture2D> iconImage, string textKey)
		{
			UIPanel uipanel = new UIPanel();
			int num = -3;
			int num2 = -3;
			uipanel.Width = StyleDimension.FromPixelsAndPercent((float)num, 0.5f);
			uipanel.Height = StyleDimension.FromPixelsAndPercent((float)num2, 0.33f);
			uipanel.OnMouseOver += this.SetColorsToHovered;
			uipanel.OnMouseOut += this.SetColorsToNotHovered;
			uipanel.BackgroundColor = new Color(63, 82, 151) * 0.7f;
			uipanel.BorderColor = new Color(89, 116, 213) * 0.7f;
			uipanel.SetPadding(6f);
			UIImage uIImage = new UIImage(iconImage)
			{
				IgnoresMouseInteraction = true,
				VAlign = 0.5f
			};
			uIImage.Left.Set(2f, 0f);
			uipanel.Append(uIImage);
			uipanel.OnMouseOver += this.ShowOptionDescription;
			uipanel.OnMouseOut += this.ClearOptionDescription;
			UIText uIText = new UIText(Language.GetText(textKey), 0.45f, true)
			{
				HAlign = 0f,
				VAlign = 0.5f,
				Width = StyleDimension.FromPixelsAndPercent(-80f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Top = StyleDimension.FromPixelsAndPercent(5f, 0f),
				Left = StyleDimension.FromPixels(80f),
				IgnoresMouseInteraction = true,
				TextOriginX = 0f,
				TextOriginY = 0f
			};
			uIText.PaddingLeft = 0f;
			uIText.PaddingRight = 20f;
			uIText.PaddingTop = 10f;
			uIText.IsWrapped = true;
			uipanel.Append(uIText);
			uipanel.SetSnapPoint("Button", 0, null, null);
			return uipanel;
		}

		// Token: 0x06003C85 RID: 15493 RVA: 0x005C1DBB File Offset: 0x005BFFBB
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			this.HandleBackButtonUsage();
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
		}

		// Token: 0x06003C86 RID: 15494 RVA: 0x005C1DD8 File Offset: 0x005BFFD8
		private void GoLogsClick(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.IssueReporterIndicator.Hide();
			Main.OpenReportsMenu();
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
		}

		// Token: 0x06003C87 RID: 15495 RVA: 0x005C1E00 File Offset: 0x005C0000
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06003C88 RID: 15496 RVA: 0x005C1E55 File Offset: 0x005C0055
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06003C89 RID: 15497 RVA: 0x005C1E94 File Offset: 0x005C0094
		private void SetColorsToHovered(UIMouseEvent evt, UIElement listeningElement)
		{
			UIPanel uipanel = (UIPanel)evt.Target;
			uipanel.BackgroundColor = new Color(73, 94, 171);
			uipanel.BorderColor = new Color(89, 116, 213);
		}

		// Token: 0x06003C8A RID: 15498 RVA: 0x005C1EC8 File Offset: 0x005C00C8
		private void SetColorsToNotHovered(UIMouseEvent evt, UIElement listeningElement)
		{
			UIPanel uipanel = (UIPanel)evt.Target;
			uipanel.BackgroundColor = new Color(63, 82, 151) * 0.7f;
			uipanel.BorderColor = new Color(89, 116, 213) * 0.7f;
		}

		// Token: 0x06003C8B RID: 15499 RVA: 0x005C1F1B File Offset: 0x005C011B
		public void HandleBackButtonUsage()
		{
			if (this.PreviousUIState == null)
			{
				Main.menuMode = 0;
				return;
			}
			Main.menuMode = 888;
			Main.MenuUI.SetState(this.PreviousUIState);
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
		}

		// Token: 0x06003C8C RID: 15500 RVA: 0x005C1F5B File Offset: 0x005C015B
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.SetupGamepadPoints(spriteBatch);
		}

		// Token: 0x06003C8D RID: 15501 RVA: 0x005C1F6B File Offset: 0x005C016B
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
		}

		// Token: 0x06003C8E RID: 15502 RVA: 0x005C1F73 File Offset: 0x005C0173
		static UIWorkshopHub()
		{
			UIWorkshopHub.OnWorkshopHubMenuOpened = delegate()
			{
			};
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06003C8F RID: 15503 RVA: 0x005C1F8A File Offset: 0x005C018A
		// (set) Token: 0x06003C90 RID: 15504 RVA: 0x005C1F92 File Offset: 0x005C0192
		public UIState PreviousUIState { get; set; }

		// Token: 0x06003C91 RID: 15505 RVA: 0x005C1F9C File Offset: 0x005C019C
		private void AppendTmlElements(UIElement uiElement)
		{
			UIElement modsMenu = this.MakeButton_OpenModsMenu();
			modsMenu.HAlign = 0f;
			modsMenu.VAlign = 0f;
			uiElement.Append(modsMenu);
			UIElement modSources = this.MakeButton_OpenModSourcesMenu();
			modSources.HAlign = 1f;
			modSources.VAlign = 0f;
			uiElement.Append(modSources);
			UIElement modBrowser = this.MakeButton_OpenModBrowserMenu();
			modBrowser.HAlign = 0f;
			modBrowser.VAlign = 0.5f;
			uiElement.Append(modBrowser);
			UIElement tbd = this.MakeButton_ModPackMenu();
			tbd.HAlign = 1f;
			tbd.VAlign = 0.5f;
			uiElement.Append(tbd);
		}

		// Token: 0x06003C92 RID: 15506 RVA: 0x005C203C File Offset: 0x005C023C
		private void OnChooseOptionDescription(UIElement listeningElement, ref LocalizedText localizedText)
		{
			if (listeningElement == this._buttonMods)
			{
				localizedText = Language.GetText("tModLoader.MenuManageModsDescription");
			}
			if (listeningElement == this._buttonModSources)
			{
				localizedText = Language.GetText("tModLoader.MenuDevelopModsDescription");
			}
			if (listeningElement == this._buttonModBrowser)
			{
				localizedText = Language.GetText("tModLoader.MenuDownloadModsDescription");
			}
			if (listeningElement == this._buttonModPack)
			{
				localizedText = Language.GetText("tModLoader.MenuModPackDescription");
			}
		}

		// Token: 0x06003C93 RID: 15507 RVA: 0x005C20A0 File Offset: 0x005C02A0
		private UIElement MakeButton_OpenModsMenu()
		{
			UIElement uIElement = this.MakeFancyButtonMod("Terraria.GameContent.UI.States.HubManageMods", "tModLoader.MenuManageMods");
			uIElement.OnLeftClick += this.Click_OpenModsMenu;
			this._buttonMods = uIElement;
			return uIElement;
		}

		// Token: 0x06003C94 RID: 15508 RVA: 0x005C20D8 File Offset: 0x005C02D8
		private void Click_OpenModsMenu(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Interface.modsMenu.PreviousUIState = this;
			Main.MenuUI.SetState(Interface.modsMenu);
		}

		// Token: 0x06003C95 RID: 15509 RVA: 0x005C210C File Offset: 0x005C030C
		private UIElement MakeButton_OpenModSourcesMenu()
		{
			UIElement uIElement = this.MakeFancyButtonMod("Terraria.GameContent.UI.States.HubDevelopMods", "tModLoader.MenuDevelopMods");
			uIElement.OnLeftClick += this.Click_OpenModSourcesMenu;
			this._buttonModSources = uIElement;
			return uIElement;
		}

		// Token: 0x06003C96 RID: 15510 RVA: 0x005C2144 File Offset: 0x005C0344
		private void Click_OpenModSourcesMenu(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Interface.modSources.PreviousUIState = this;
			Main.MenuUI.SetState(Interface.modSources);
		}

		// Token: 0x06003C97 RID: 15511 RVA: 0x005C2178 File Offset: 0x005C0378
		private UIElement MakeButton_OpenModBrowserMenu()
		{
			UIElement uIElement = this.MakeFancyButtonMod("Terraria.GameContent.UI.States.HubDownloadMods", "tModLoader.MenuDownloadMods");
			uIElement.OnLeftClick += this.Click_OpenModBrowserMenu;
			this._buttonModBrowser = uIElement;
			return uIElement;
		}

		// Token: 0x06003C98 RID: 15512 RVA: 0x005C21B0 File Offset: 0x005C03B0
		private void Click_OpenModBrowserMenu(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Interface.modBrowser.PreviousUIState = this;
			Main.MenuUI.SetState(Interface.modBrowser);
		}

		// Token: 0x06003C99 RID: 15513 RVA: 0x005C21E4 File Offset: 0x005C03E4
		private UIElement MakeButton_ModPackMenu()
		{
			UIElement uIElement = this.MakeFancyButtonMod("Terraria.GameContent.UI.States.HubModPacks", "tModLoader.ModsModPacks");
			uIElement.OnLeftClick += this.Click_OpenModPackMenu;
			this._buttonModPack = uIElement;
			return uIElement;
		}

		// Token: 0x06003C9A RID: 15514 RVA: 0x005C221C File Offset: 0x005C041C
		private void Click_OpenModPackMenu(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Interface.modPacksMenu.PreviousUIState = this;
			Main.MenuUI.SetState(Interface.modPacksMenu);
		}

		// Token: 0x06003C9B RID: 15515 RVA: 0x005C224D File Offset: 0x005C044D
		private UIElement MakeFancyButtonMod(string path, string textKey)
		{
			return this.MakeFancyButton_Inner(ModLoader.ManifestAssets.Request<Texture2D>(path), textKey);
		}

		// Token: 0x040055B2 RID: 21938
		private UIState _previousUIState;

		// Token: 0x040055B3 RID: 21939
		private UIText _descriptionText;

		// Token: 0x040055B4 RID: 21940
		private UIElement _buttonUseResourcePacks;

		// Token: 0x040055B5 RID: 21941
		private UIElement _buttonPublishResourcePacks;

		// Token: 0x040055B6 RID: 21942
		private UIElement _buttonImportWorlds;

		// Token: 0x040055B7 RID: 21943
		private UIElement _buttonPublishWorlds;

		// Token: 0x040055B8 RID: 21944
		private UIElement _buttonBack;

		// Token: 0x040055B9 RID: 21945
		private UIElement _buttonLogs;

		// Token: 0x040055BA RID: 21946
		private UIGamepadHelper _helper;

		// Token: 0x040055BC RID: 21948
		private UIElement _buttonMods;

		// Token: 0x040055BD RID: 21949
		private UIElement _buttonModSources;

		// Token: 0x040055BE RID: 21950
		private UIElement _buttonModBrowser;

		// Token: 0x040055BF RID: 21951
		private UIElement _buttonModPack;
	}
}
