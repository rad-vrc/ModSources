using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.OS;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Social;
using Terraria.Social.Base;
using Terraria.UI;
using Terraria.UI.Gamepad;
using Terraria.Utilities.FileBrowser;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x0200033E RID: 830
	public abstract class AWorkshopPublishInfoState<TPublishedObjectType> : UIState, IHaveBackButtonCommand
	{
		// Token: 0x0600254C RID: 9548 RVA: 0x0056AF26 File Offset: 0x00569126
		public AWorkshopPublishInfoState(UIState stateToGoBackTo, TPublishedObjectType dataObject)
		{
			this._previousUIState = stateToGoBackTo;
			this._dataObject = dataObject;
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x0056AF44 File Offset: 0x00569144
		public override void OnInitialize()
		{
			base.OnInitialize();
			int num = 40;
			int num2 = 200;
			int num3 = 50 + num + 10;
			int num4 = 70;
			UIElement uielement = new UIElement();
			uielement.Width.Set(600f, 0f);
			uielement.Top.Set((float)num2, 0f);
			uielement.Height.Set((float)(-(float)num2), 1f);
			uielement.HAlign = 0.5f;
			UIPanel uipanel = new UIPanel();
			uipanel.Width.Set(0f, 1f);
			uipanel.Height.Set((float)(-(float)num3), 1f);
			uipanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
			this.AddBackButton(num, uielement);
			this.AddPublishButton(num, uielement);
			int num5 = 6 + num4;
			UIList uiList = this.AddUIList(uipanel, (float)num5);
			this.FillUIList(uiList);
			this.AddHorizontalSeparator(uipanel, 0f, 0).Top = new StyleDimension((float)(-(float)num4 + 3), 1f);
			this.AddDescriptionPanel(uipanel, (float)(num4 - 6), "desc");
			uielement.Append(uipanel);
			base.Append(uielement);
			this.SetDefaultOptions();
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x0056B080 File Offset: 0x00569280
		private void SetDefaultOptions()
		{
			this._optionPublicity = WorkshopItemPublicSettingId.Public;
			GroupOptionButton<WorkshopItemPublicSettingId>[] publicityOptions = this._publicityOptions;
			for (int i = 0; i < publicityOptions.Length; i++)
			{
				publicityOptions[i].SetCurrentOption(this._optionPublicity);
			}
			this.SetTagsFromFoundEntry();
			this.UpdateImagePreview();
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x0056B0C4 File Offset: 0x005692C4
		private void FillUIList(UIList uiList)
		{
			UIElement uielement = new UIElement
			{
				Width = new StyleDimension(0f, 0f),
				Height = new StyleDimension(0f, 0f)
			};
			uielement.SetPadding(0f);
			uiList.Add(uielement);
			uiList.Add(this.CreateSteamDisclaimer("disclaimer"));
			uiList.Add(this.CreatePreviewImageSelectionPanel("image"));
			uiList.Add(this.CreatePublicSettingsRow(0f, 44f, "public"));
			uiList.Add(this.CreateTagOptionsPanel(0f, 44, "tags"));
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x0056B168 File Offset: 0x00569368
		private UIElement CreatePreviewImageSelectionPanel(string tagGroup)
		{
			UIElement uielement = new UIElement();
			uielement.Width = new StyleDimension(0f, 1f);
			uielement.Height = new StyleDimension(80f, 0f);
			UIElement uielement2 = new UIElement
			{
				Width = new StyleDimension(72f, 0f),
				Height = new StyleDimension(72f, 0f),
				HAlign = 1f,
				VAlign = 0.5f,
				Left = new StyleDimension(-6f, 0f),
				Top = new StyleDimension(0f, 0f)
			};
			uielement2.SetPadding(0f);
			uielement.Append(uielement2);
			float num = 86f;
			this._defaultPreviewImageTexture = Main.Assets.Request<Texture2D>("Images/UI/Workshop/DefaultPreviewImage", 1);
			UIImage uiimage = new UIImage(this._defaultPreviewImageTexture)
			{
				Width = new StyleDimension(-4f, 1f),
				Height = new StyleDimension(-4f, 1f),
				HAlign = 0.5f,
				VAlign = 0.5f,
				ScaleToFit = true,
				AllowResizingDimensions = false
			};
			UIImage element = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders", 1))
			{
				HAlign = 0.5f,
				VAlign = 0.5f
			};
			uielement2.Append(uiimage);
			uielement2.Append(element);
			this._previewImageUIElement = uiimage;
			UICharacterNameButton uicharacterNameButton = new UICharacterNameButton(Language.GetText("Workshop.PreviewImagePathTitle"), Language.GetText("Workshop.PreviewImagePathEmpty"), Language.GetText("Workshop.PreviewImagePathDescription"))
			{
				Width = StyleDimension.FromPixelsAndPercent(-num, 1f),
				Height = new StyleDimension(0f, 1f)
			};
			uicharacterNameButton.OnLeftMouseDown += this.Click_SetPreviewImage;
			uicharacterNameButton.OnMouseOver += this.ShowOptionDescription;
			uicharacterNameButton.OnMouseOut += this.ClearOptionDescription;
			uicharacterNameButton.SetSnapPoint(tagGroup, 0, null, null);
			uielement.Append(uicharacterNameButton);
			this._previewImagePathPlate = uicharacterNameButton;
			return uielement;
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x0056B390 File Offset: 0x00569590
		private void SetTagsFromFoundEntry()
		{
			FoundWorkshopEntryInfo foundWorkshopEntryInfo;
			if (!this.TryFindingTags(out foundWorkshopEntryInfo))
			{
				return;
			}
			if (foundWorkshopEntryInfo.tags != null)
			{
				foreach (GroupOptionButton<WorkshopTagOption> groupOptionButton in this._tagOptions)
				{
					bool flag = foundWorkshopEntryInfo.tags.Contains(groupOptionButton.OptionValue.InternalNameForAPIs);
					groupOptionButton.SetCurrentOption(flag ? groupOptionButton.OptionValue : null);
					groupOptionButton.SetColor(groupOptionButton.IsSelected ? new Color(152, 175, 235) : Colors.InventoryDefaultColor, 1f);
				}
			}
			GroupOptionButton<WorkshopItemPublicSettingId>[] publicityOptions = this._publicityOptions;
			for (int i = 0; i < publicityOptions.Length; i++)
			{
				publicityOptions[i].SetCurrentOption(foundWorkshopEntryInfo.publicity);
			}
		}

		// Token: 0x06002552 RID: 9554
		protected abstract bool TryFindingTags(out FoundWorkshopEntryInfo info);

		// Token: 0x06002553 RID: 9555 RVA: 0x0056B474 File Offset: 0x00569674
		private void Click_SetPreviewImage(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			this.OpenFileDialogueToSelectPreviewImage();
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x0056B494 File Offset: 0x00569694
		private UIElement CreateSteamDisclaimer(string tagGroup)
		{
			float num = 60f;
			float num2 = 0f + num;
			GroupOptionButton<bool> groupOptionButton = new GroupOptionButton<bool>(true, null, null, Color.White, null, 1f, 0.5f, 16f);
			groupOptionButton.HAlign = 0.5f;
			groupOptionButton.VAlign = 0f;
			groupOptionButton.Width = StyleDimension.FromPixelsAndPercent(0f, 1f);
			groupOptionButton.Left = StyleDimension.FromPixels(0f);
			groupOptionButton.Height = StyleDimension.FromPixelsAndPercent(num2 + 4f, 0f);
			groupOptionButton.Top = StyleDimension.FromPixels(0f);
			groupOptionButton.ShowHighlightWhenSelected = false;
			groupOptionButton.SetCurrentOption(false);
			groupOptionButton.Width.Set(0f, 1f);
			UIElement uielement = new UIElement
			{
				HAlign = 0.5f,
				VAlign = 1f,
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(num, 0f)
			};
			groupOptionButton.Append(uielement);
			UIText uitext = new UIText(Language.GetText("Workshop.SteamDisclaimer"), 1f, false)
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(-40f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				TextColor = Color.Cyan,
				IgnoresMouseInteraction = true
			};
			uitext.PaddingLeft = 20f;
			uitext.PaddingRight = 20f;
			uitext.PaddingTop = 4f;
			uitext.IsWrapped = true;
			this._disclaimerText = uitext;
			groupOptionButton.OnLeftClick += this.steamDisclaimerText_OnClick;
			groupOptionButton.OnMouseOver += this.steamDisclaimerText_OnMouseOver;
			groupOptionButton.OnMouseOut += this.steamDisclaimerText_OnMouseOut;
			uielement.Append(uitext);
			uitext.SetSnapPoint(tagGroup, 0, null, null);
			this._steamDisclaimerButton = uitext;
			return groupOptionButton;
		}

		// Token: 0x06002555 RID: 9557 RVA: 0x0056B691 File Offset: 0x00569891
		private void steamDisclaimerText_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			this._disclaimerText.TextColor = Color.Cyan;
			this.ClearOptionDescription(evt, listeningElement);
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x0056B6AB File Offset: 0x005698AB
		private void steamDisclaimerText_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this._disclaimerText.TextColor = Color.LightCyan;
			this.ShowOptionDescription(evt, listeningElement);
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x0056B6DC File Offset: 0x005698DC
		private void steamDisclaimerText_OnClick(UIMouseEvent evt, UIElement listeningElement)
		{
			try
			{
				Platform.Get<IPathService>().OpenURL("https://steamcommunity.com/sharedfiles/workshoplegalagreement");
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x0056B710 File Offset: 0x00569910
		public override void Recalculate()
		{
			this.UpdateScrollbar();
			base.Recalculate();
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x0056B720 File Offset: 0x00569920
		private void UpdateScrollbar()
		{
			if (this._scrollbar == null)
			{
				return;
			}
			if (this._isScrollbarAttached && !this._scrollbar.CanScroll)
			{
				this._uiListContainer.RemoveChild(this._scrollbar);
				this._isScrollbarAttached = false;
				this._uiListRect.Width.Set(0f, 1f);
				return;
			}
			if (!this._isScrollbarAttached && this._scrollbar.CanScroll)
			{
				this._uiListContainer.Append(this._scrollbar);
				this._isScrollbarAttached = true;
				this._uiListRect.Width.Set(-25f, 1f);
			}
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x0056B7C8 File Offset: 0x005699C8
		private UIList AddUIList(UIElement container, float antiHeight)
		{
			this._uiListContainer = container;
			float num = 0f;
			UIElement uielement = new UIElement
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(-num * 2f, 1f),
				Left = StyleDimension.FromPixels(-num),
				Height = StyleDimension.FromPixelsAndPercent(-2f - antiHeight, 1f),
				OverflowHidden = true
			};
			this._listContainer = uielement;
			UISlicedImage uislicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/Workshop/ListBackground", 1))
			{
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(0f, 1f),
				Color = Color.White * 0.7f
			};
			uislicedImage.SetSliceDepths(4);
			container.Append(uielement);
			uielement.Append(uislicedImage);
			UIList uilist = new UIList
			{
				Width = StyleDimension.FromPixelsAndPercent(-10f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(-4f, 1f),
				HAlign = 0.5f,
				VAlign = 0.5f,
				OverflowHidden = true
			};
			uilist.ManualSortMethod = new Action<List<UIElement>>(this.ManualIfnoSortingMethod);
			uilist.ListPadding = 5f;
			uielement.Append(uilist);
			UIScrollbar uiscrollbar = new UIScrollbar
			{
				HAlign = 1f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(-num * 2f, 1f),
				Left = StyleDimension.FromPixels(-num),
				Height = StyleDimension.FromPixelsAndPercent(-14f - antiHeight, 1f),
				Top = StyleDimension.FromPixels(6f)
			};
			uiscrollbar.SetView(100f, 1000f);
			uilist.SetScrollbar(uiscrollbar);
			this._uiListRect = uielement;
			this._scrollbar = uiscrollbar;
			return uilist;
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		private void ManualIfnoSortingMethod(List<UIElement> list)
		{
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x0056B9B8 File Offset: 0x00569BB8
		private UIElement CreatePublicSettingsRow(float accumulatedHeight, float height, string tagGroup)
		{
			UIElement result;
			UIElement uielement;
			this.CreateStylizedCategoryPanel(height, "Workshop.CategoryTitlePublicity", out result, out uielement);
			WorkshopItemPublicSettingId[] array = new WorkshopItemPublicSettingId[3];
			array[0] = WorkshopItemPublicSettingId.Public;
			array[1] = WorkshopItemPublicSettingId.FriendsOnly;
			WorkshopItemPublicSettingId[] array2 = array;
			LocalizedText[] array3 = new LocalizedText[]
			{
				Language.GetText("Workshop.SettingsPublicityPublic"),
				Language.GetText("Workshop.SettingsPublicityFriendsOnly"),
				Language.GetText("Workshop.SettingsPublicityPrivate")
			};
			LocalizedText[] array4 = new LocalizedText[]
			{
				Language.GetText("Workshop.SettingsPublicityPublicDescription"),
				Language.GetText("Workshop.SettingsPublicityFriendsOnlyDescription"),
				Language.GetText("Workshop.SettingsPublicityPrivateDescription")
			};
			Color[] array5 = new Color[]
			{
				Color.White,
				Color.White,
				Color.White
			};
			string[] array6 = new string[]
			{
				"Images/UI/Workshop/PublicityPublic",
				"Images/UI/Workshop/PublicityFriendsOnly",
				"Images/UI/Workshop/PublicityPrivate"
			};
			float num = 0.98f;
			GroupOptionButton<WorkshopItemPublicSettingId>[] array7 = new GroupOptionButton<WorkshopItemPublicSettingId>[array2.Length];
			for (int i = 0; i < array7.Length; i++)
			{
				GroupOptionButton<WorkshopItemPublicSettingId> groupOptionButton = new GroupOptionButton<WorkshopItemPublicSettingId>(array2[i], array3[i], array4[i], array5[i], array6[i], 1f, 1f, 16f);
				groupOptionButton.Width = StyleDimension.FromPixelsAndPercent((float)(-4 * (array7.Length - 1)), 1f / (float)array7.Length * num);
				groupOptionButton.HAlign = (float)i / (float)(array7.Length - 1);
				groupOptionButton.Left = StyleDimension.FromPercent((1f - num) * (1f - groupOptionButton.HAlign * 2f));
				groupOptionButton.Top.Set(accumulatedHeight, 0f);
				groupOptionButton.OnLeftMouseDown += this.ClickPublicityOption;
				groupOptionButton.OnMouseOver += this.ShowOptionDescription;
				groupOptionButton.OnMouseOut += this.ClearOptionDescription;
				groupOptionButton.SetSnapPoint(tagGroup, i, null, null);
				uielement.Append(groupOptionButton);
				array7[i] = groupOptionButton;
			}
			this._publicityOptions = array7;
			return result;
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x0056BBC8 File Offset: 0x00569DC8
		private UIElement CreateTagOptionsPanel(float accumulatedHeight, int heightPerRow, string tagGroup)
		{
			List<WorkshopTagOption> tagsToShow = this.GetTagsToShow();
			int num = 3;
			int num2 = (int)Math.Ceiling((double)((float)tagsToShow.Count / (float)num));
			int num3 = heightPerRow * num2;
			UIElement result;
			UIElement uielement;
			this.CreateStylizedCategoryPanel((float)num3, "Workshop.CategoryTitleTags", out result, out uielement);
			float num4 = 0.98f;
			List<GroupOptionButton<WorkshopTagOption>> list = new List<GroupOptionButton<WorkshopTagOption>>();
			for (int i = 0; i < tagsToShow.Count; i++)
			{
				WorkshopTagOption workshopTagOption = tagsToShow[i];
				GroupOptionButton<WorkshopTagOption> groupOptionButton = new GroupOptionButton<WorkshopTagOption>(workshopTagOption, Language.GetText(workshopTagOption.NameKey), Language.GetText(workshopTagOption.NameKey + "Description"), Color.White, null, 1f, 0.5f, 16f);
				groupOptionButton.ShowHighlightWhenSelected = false;
				groupOptionButton.SetCurrentOption(null);
				int num5 = i / num;
				int num6 = i - num5 * num;
				groupOptionButton.Width = StyleDimension.FromPixelsAndPercent((float)(-4 * (num - 1)), 1f / (float)num * num4);
				groupOptionButton.HAlign = (float)num6 / (float)(num - 1);
				groupOptionButton.Left = StyleDimension.FromPercent((1f - num4) * (1f - groupOptionButton.HAlign * 2f));
				groupOptionButton.Top.Set((float)(num5 * heightPerRow), 0f);
				groupOptionButton.OnLeftMouseDown += this.ClickTagOption;
				groupOptionButton.OnMouseOver += this.ShowOptionDescription;
				groupOptionButton.OnMouseOut += this.ClearOptionDescription;
				groupOptionButton.SetSnapPoint(tagGroup, i, null, null);
				uielement.Append(groupOptionButton);
				list.Add(groupOptionButton);
			}
			this._tagOptions = list;
			return result;
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x0056BD78 File Offset: 0x00569F78
		private void CreateStylizedCategoryPanel(float height, string titleTextKey, out UIElement entirePanel, out UIElement innerPanel)
		{
			float num = 44f;
			UISlicedImage uislicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanel", 1))
			{
				HAlign = 0.5f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Left = StyleDimension.FromPixels(0f),
				Height = StyleDimension.FromPixelsAndPercent(height + num + 4f, 0f),
				Top = StyleDimension.FromPixels(0f)
			};
			uislicedImage.SetSliceDepths(8);
			uislicedImage.Color = Color.White * 0.7f;
			innerPanel = new UIElement
			{
				HAlign = 0.5f,
				VAlign = 1f,
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(height, 0f)
			};
			uislicedImage.Append(innerPanel);
			this.AddHorizontalSeparator(uislicedImage, num, 4);
			UIText uitext = new UIText(Language.GetText(titleTextKey), 1f, false)
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(-40f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(num, 0f),
				Top = StyleDimension.FromPixelsAndPercent(5f, 0f)
			};
			uitext.PaddingLeft = 20f;
			uitext.PaddingRight = 20f;
			uitext.PaddingTop = 6f;
			uitext.IsWrapped = false;
			uislicedImage.Append(uitext);
			entirePanel = uislicedImage;
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x0056BF10 File Offset: 0x0056A110
		private void ClickTagOption(UIMouseEvent evt, UIElement listeningElement)
		{
			GroupOptionButton<WorkshopTagOption> groupOptionButton = (GroupOptionButton<WorkshopTagOption>)listeningElement;
			groupOptionButton.SetCurrentOption(groupOptionButton.IsSelected ? null : groupOptionButton.OptionValue);
			groupOptionButton.SetColor(groupOptionButton.IsSelected ? new Color(152, 175, 235) : Colors.InventoryDefaultColor, 1f);
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x0056BF6C File Offset: 0x0056A16C
		private void ClickPublicityOption(UIMouseEvent evt, UIElement listeningElement)
		{
			GroupOptionButton<WorkshopItemPublicSettingId> groupOptionButton = (GroupOptionButton<WorkshopItemPublicSettingId>)listeningElement;
			this._optionPublicity = groupOptionButton.OptionValue;
			GroupOptionButton<WorkshopItemPublicSettingId>[] publicityOptions = this._publicityOptions;
			for (int i = 0; i < publicityOptions.Length; i++)
			{
				publicityOptions[i].SetCurrentOption(groupOptionButton.OptionValue);
			}
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x0056BFB0 File Offset: 0x0056A1B0
		public void ShowOptionDescription(UIMouseEvent evt, UIElement listeningElement)
		{
			LocalizedText localizedText = null;
			GroupOptionButton<WorkshopItemPublicSettingId> groupOptionButton = listeningElement as GroupOptionButton<WorkshopItemPublicSettingId>;
			if (groupOptionButton != null)
			{
				localizedText = groupOptionButton.Description;
			}
			UICharacterNameButton uicharacterNameButton = listeningElement as UICharacterNameButton;
			if (uicharacterNameButton != null)
			{
				localizedText = uicharacterNameButton.Description;
			}
			GroupOptionButton<bool> groupOptionButton2 = listeningElement as GroupOptionButton<bool>;
			if (groupOptionButton2 != null)
			{
				localizedText = groupOptionButton2.Description;
			}
			GroupOptionButton<WorkshopTagOption> groupOptionButton3 = listeningElement as GroupOptionButton<WorkshopTagOption>;
			if (groupOptionButton3 != null)
			{
				localizedText = groupOptionButton3.Description;
			}
			if (listeningElement == this._steamDisclaimerButton)
			{
				localizedText = Language.GetText("Workshop.SteamDisclaimerDescrpition");
			}
			if (localizedText != null)
			{
				this._descriptionText.SetText(localizedText);
			}
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x0056C029 File Offset: 0x0056A229
		public void ClearOptionDescription(UIMouseEvent evt, UIElement listeningElement)
		{
			this._descriptionText.SetText(Language.GetText("Workshop.InfoDescriptionDefault"));
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x0056C040 File Offset: 0x0056A240
		private UIElement CreateInsturctionsPanel(float accumulatedHeight, float height, string tagGroup)
		{
			float num = 0f;
			UISlicedImage uislicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", 1));
			uislicedImage.HAlign = 0.5f;
			uislicedImage.VAlign = 0f;
			uislicedImage.Width = StyleDimension.FromPixelsAndPercent(-num * 2f, 1f);
			uislicedImage.Left = StyleDimension.FromPixels(-num);
			uislicedImage.Height = StyleDimension.FromPixelsAndPercent(height, 0f);
			uislicedImage.Top = StyleDimension.FromPixels(accumulatedHeight);
			uislicedImage.SetSliceDepths(10);
			uislicedImage.Color = Color.LightGray * 0.7f;
			UIText uitext = new UIText(Language.GetText(this._instructionsTextKey), 1f, false)
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(-40f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Top = StyleDimension.FromPixelsAndPercent(5f, 0f)
			};
			uitext.PaddingLeft = 20f;
			uitext.PaddingRight = 20f;
			uitext.PaddingTop = 6f;
			uitext.IsWrapped = true;
			uislicedImage.Append(uitext);
			return uislicedImage;
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x0056C178 File Offset: 0x0056A378
		private void AddDescriptionPanel(UIElement container, float height, string tagGroup)
		{
			float num = 0f;
			UISlicedImage uislicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", 1))
			{
				HAlign = 0.5f,
				VAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent(-num * 2f, 1f),
				Left = StyleDimension.FromPixels(-num),
				Height = StyleDimension.FromPixelsAndPercent(height, 0f),
				Top = StyleDimension.FromPixels(2f)
			};
			uislicedImage.SetSliceDepths(10);
			uislicedImage.Color = Color.LightGray * 0.7f;
			container.Append(uislicedImage);
			UIText uitext = new UIText(Language.GetText("Workshop.InfoDescriptionDefault"), 0.85f, false)
			{
				HAlign = 0f,
				VAlign = 1f,
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(0f, 1f)
			};
			uitext.PaddingLeft = 4f;
			uitext.PaddingRight = 4f;
			uitext.PaddingTop = 4f;
			uitext.IsWrapped = true;
			uislicedImage.Append(uitext);
			this._descriptionText = uitext;
		}

		// Token: 0x06002565 RID: 9573
		protected abstract string GetPublishedObjectDisplayName();

		// Token: 0x06002566 RID: 9574
		protected abstract List<WorkshopTagOption> GetTagsToShow();

		// Token: 0x06002567 RID: 9575 RVA: 0x0056C2AD File Offset: 0x0056A4AD
		private void Click_GoBack(UIMouseEvent evt, UIElement listeningElement)
		{
			this.HandleBackButtonUsage();
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x0056C2B5 File Offset: 0x0056A4B5
		public void HandleBackButtonUsage()
		{
			if (this._previousUIState == null)
			{
				Main.menuMode = 0;
				return;
			}
			Main.menuMode = 888;
			Main.MenuUI.SetState(this._previousUIState);
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x0056C2E0 File Offset: 0x0056A4E0
		private void Click_Publish(UIMouseEvent evt, UIElement listeningElement)
		{
			this.GoToPublishConfirmation();
		}

		// Token: 0x0600256A RID: 9578
		protected abstract void GoToPublishConfirmation();

		// Token: 0x0600256B RID: 9579 RVA: 0x0056C2E8 File Offset: 0x0056A4E8
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x0056C33D File Offset: 0x0056A53D
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x0056C37C File Offset: 0x0056A57C
		private void AddPublishButton(int backButtonYLift, UIElement outerContainer)
		{
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("Workshop.Publish"), 0.7f, true);
			uitextPanel.Width.Set(-10f, 0.5f);
			uitextPanel.Height.Set(50f, 0f);
			uitextPanel.VAlign = 1f;
			uitextPanel.Top.Set((float)(-(float)backButtonYLift), 0f);
			uitextPanel.HAlign = 1f;
			uitextPanel.OnMouseOver += this.FadedMouseOver;
			uitextPanel.OnMouseOut += this.FadedMouseOut;
			uitextPanel.OnLeftClick += this.Click_Publish;
			uitextPanel.SetSnapPoint("publish", 0, null, null);
			outerContainer.Append(uitextPanel);
			this._publishButton = uitextPanel;
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x0056C454 File Offset: 0x0056A654
		private void AddBackButton(int backButtonYLift, UIElement outerContainer)
		{
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
			uitextPanel.Width.Set(-10f, 0.5f);
			uitextPanel.Height.Set(50f, 0f);
			uitextPanel.VAlign = 1f;
			uitextPanel.Top.Set((float)(-(float)backButtonYLift), 0f);
			uitextPanel.HAlign = 0f;
			uitextPanel.OnMouseOver += this.FadedMouseOver;
			uitextPanel.OnMouseOut += this.FadedMouseOut;
			uitextPanel.OnLeftClick += this.Click_GoBack;
			uitextPanel.SetSnapPoint("back", 0, null, null);
			outerContainer.Append(uitextPanel);
			this._backButton = uitextPanel;
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x0056C52C File Offset: 0x0056A72C
		private UIElement AddHorizontalSeparator(UIElement Container, float accumualtedHeight, int widthReduction = 0)
		{
			UIHorizontalSeparator uihorizontalSeparator = new UIHorizontalSeparator(2, true)
			{
				Width = StyleDimension.FromPixelsAndPercent((float)(-(float)widthReduction), 1f),
				HAlign = 0.5f,
				Top = StyleDimension.FromPixels(accumualtedHeight - 8f),
				Color = Color.Lerp(Color.White, new Color(63, 65, 151, 255), 0.85f) * 0.9f
			};
			Container.Append(uihorizontalSeparator);
			return uihorizontalSeparator;
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x0056C5AC File Offset: 0x0056A7AC
		protected WorkshopItemPublishSettings GetPublishSettings()
		{
			WorkshopItemPublishSettings workshopItemPublishSettings = new WorkshopItemPublishSettings();
			workshopItemPublishSettings.Publicity = this._optionPublicity;
			workshopItemPublishSettings.UsedTags = (from x in this._tagOptions
			where x.IsSelected
			select x.OptionValue).ToArray<WorkshopTagOption>();
			workshopItemPublishSettings.PreviewImagePath = this._previewImagePath;
			return workshopItemPublishSettings;
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x0056C630 File Offset: 0x0056A830
		private void OpenFileDialogueToSelectPreviewImage()
		{
			ExtensionFilter[] extensions = new ExtensionFilter[]
			{
				new ExtensionFilter("Image files", new string[]
				{
					"png",
					"jpg",
					"jpeg"
				})
			};
			string text = FileBrowser.OpenFilePanel("Open icon", extensions);
			if (text != null)
			{
				this._previewImagePath = text;
				this.UpdateImagePreview();
			}
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x0056C690 File Offset: 0x0056A890
		private string PrettifyPath(string path)
		{
			if (path == null)
			{
				return path;
			}
			char[] anyOf = new char[]
			{
				Path.DirectorySeparatorChar,
				Path.AltDirectorySeparatorChar
			};
			int num = path.LastIndexOfAny(anyOf);
			if (num != -1)
			{
				path = path.Substring(num + 1);
			}
			if (path.Length > 30)
			{
				path = path.Substring(0, 30) + "…";
			}
			return path;
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x0056C6F0 File Offset: 0x0056A8F0
		private void UpdateImagePreview()
		{
			Texture2D texture2D = null;
			string contents = this.PrettifyPath(this._previewImagePath);
			this._previewImagePathPlate.SetContents(contents);
			if (this._previewImagePath != null)
			{
				try
				{
					FileStream stream = File.OpenRead(this._previewImagePath);
					texture2D = Texture2D.FromStream(Main.graphics.GraphicsDevice, stream);
				}
				catch (Exception exception)
				{
					FancyErrorPrinter.ShowFailedToLoadAssetError(exception, this._previewImagePath);
				}
			}
			if (texture2D != null && (texture2D.Width > 512 || texture2D.Height > 512))
			{
				object obj = new
				{
					texture2D.Width,
					texture2D.Height
				};
				string textValueWith = Language.GetTextValueWith("Workshop.ReportIssue_FailedToPublish_ImageSizeIsTooLarge", obj);
				if (SocialAPI.Workshop != null)
				{
					SocialAPI.Workshop.IssueReporter.ReportInstantUploadProblemFromValue(textValueWith);
				}
				this._previewImagePath = null;
				this._previewImagePathPlate.SetContents(null);
				this._previewImageUIElement.SetImage(this._defaultPreviewImageTexture);
				return;
			}
			if (this._previewImageTransientTexture != null)
			{
				this._previewImageTransientTexture.Dispose();
				this._previewImageTransientTexture = null;
			}
			if (texture2D != null)
			{
				this._previewImageUIElement.SetImage(texture2D);
				this._previewImageTransientTexture = texture2D;
				return;
			}
			this._previewImageUIElement.SetImage(this._defaultPreviewImageTexture);
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x0056C818 File Offset: 0x0056AA18
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.SetupGamepadPoints(spriteBatch);
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x0056C828 File Offset: 0x0056AA28
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
			int num = 3000;
			int num2 = num;
			List<SnapPoint> snapPoints = this.GetSnapPoints();
			this._helper.RemovePointsOutOfView(snapPoints, this._listContainer, spriteBatch);
			UILinkPoint linkPoint = this._helper.GetLinkPoint(num2++, this._backButton);
			UILinkPoint linkPoint2 = this._helper.GetLinkPoint(num2++, this._publishButton);
			SnapPoint snap = null;
			SnapPoint snap2 = null;
			for (int i = 0; i < snapPoints.Count; i++)
			{
				SnapPoint snapPoint = snapPoints[i];
				string name = snapPoint.Name;
				if (!(name == "disclaimer"))
				{
					if (name == "image")
					{
						snap2 = snapPoint;
					}
				}
				else
				{
					snap = snapPoint;
				}
			}
			UILinkPoint upSide = this._helper.TryMakeLinkPoint(ref num2, snap);
			UILinkPoint uilinkPoint = this._helper.TryMakeLinkPoint(ref num2, snap2);
			this._helper.PairLeftRight(linkPoint, linkPoint2);
			this._helper.PairUpDown(upSide, uilinkPoint);
			UILinkPoint[] array = this._helper.CreateUILinkStripHorizontal(ref num2, (from x in snapPoints
			where x.Name == "public"
			select x).ToList<SnapPoint>());
			if (array.Length != 0)
			{
				this._helper.LinkHorizontalStripUpSideToSingle(array, uilinkPoint);
			}
			UILinkPoint topLinkPoint = (array.Length != 0) ? array[0] : null;
			UILinkPoint bottomLinkPoint = linkPoint;
			List<SnapPoint> pointsForGrid = (from x in snapPoints
			where x.Name == "tags"
			select x).ToList<SnapPoint>();
			UILinkPoint[,] array2 = this._helper.CreateUILinkPointGrid(ref num2, pointsForGrid, 3, topLinkPoint, null, null, bottomLinkPoint);
			int num3 = array2.GetLength(1) - 1;
			if (num3 >= 0)
			{
				this._helper.LinkHorizontalStripBottomSideToSingle(array, array2[0, 0]);
				for (int j = array2.GetLength(0) - 1; j >= 0; j--)
				{
					if (array2[j, num3] != null)
					{
						this._helper.PairUpDown(array2[j, num3], linkPoint2);
						break;
					}
				}
			}
			UILinkPoint upSide2 = UILinkPointNavigator.Points[num2 - 1];
			this._helper.PairUpDown(upSide2, linkPoint);
			this._helper.MoveToVisuallyClosestPoint(num, num2);
		}

		// Token: 0x040049BF RID: 18879
		protected UIState _previousUIState;

		// Token: 0x040049C0 RID: 18880
		protected TPublishedObjectType _dataObject;

		// Token: 0x040049C1 RID: 18881
		protected string _publishedObjectNameDescriptorTexKey;

		// Token: 0x040049C2 RID: 18882
		protected string _instructionsTextKey;

		// Token: 0x040049C3 RID: 18883
		private UIElement _uiListContainer;

		// Token: 0x040049C4 RID: 18884
		private UIElement _uiListRect;

		// Token: 0x040049C5 RID: 18885
		private UIScrollbar _scrollbar;

		// Token: 0x040049C6 RID: 18886
		private bool _isScrollbarAttached;

		// Token: 0x040049C7 RID: 18887
		private UIText _descriptionText;

		// Token: 0x040049C8 RID: 18888
		private UIElement _listContainer;

		// Token: 0x040049C9 RID: 18889
		private UIElement _backButton;

		// Token: 0x040049CA RID: 18890
		private UIElement _publishButton;

		// Token: 0x040049CB RID: 18891
		private WorkshopItemPublicSettingId _optionPublicity = WorkshopItemPublicSettingId.Public;

		// Token: 0x040049CC RID: 18892
		private GroupOptionButton<WorkshopItemPublicSettingId>[] _publicityOptions;

		// Token: 0x040049CD RID: 18893
		private List<GroupOptionButton<WorkshopTagOption>> _tagOptions;

		// Token: 0x040049CE RID: 18894
		private UICharacterNameButton _previewImagePathPlate;

		// Token: 0x040049CF RID: 18895
		private Texture2D _previewImageTransientTexture;

		// Token: 0x040049D0 RID: 18896
		private UIImage _previewImageUIElement;

		// Token: 0x040049D1 RID: 18897
		private string _previewImagePath;

		// Token: 0x040049D2 RID: 18898
		private Asset<Texture2D> _defaultPreviewImageTexture;

		// Token: 0x040049D3 RID: 18899
		private UIElement _steamDisclaimerButton;

		// Token: 0x040049D4 RID: 18900
		private UIText _disclaimerText;

		// Token: 0x040049D5 RID: 18901
		private UIGamepadHelper _helper;
	}
}
