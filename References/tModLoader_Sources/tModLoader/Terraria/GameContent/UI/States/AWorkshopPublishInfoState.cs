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
using Terraria.ModLoader.Core;
using Terraria.Social;
using Terraria.Social.Base;
using Terraria.UI;
using Terraria.UI.Gamepad;
using Terraria.Utilities.FileBrowser;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020004D4 RID: 1236
	public abstract class AWorkshopPublishInfoState<TPublishedObjectType> : UIState, IHaveBackButtonCommand
	{
		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06003AD4 RID: 15060 RVA: 0x005AD51A File Offset: 0x005AB71A
		// (set) Token: 0x06003AD5 RID: 15061 RVA: 0x005AD522 File Offset: 0x005AB722
		public UIState PreviousUIState
		{
			get
			{
				return this._previousUIState;
			}
			set
			{
				this._previousUIState = value;
			}
		}

		// Token: 0x06003AD6 RID: 15062 RVA: 0x005AD52B File Offset: 0x005AB72B
		public AWorkshopPublishInfoState(UIState stateToGoBackTo, TPublishedObjectType dataObject)
		{
			this._previousUIState = stateToGoBackTo;
			this._dataObject = dataObject;
			if (dataObject is TmodFile)
			{
				this._isMod = true;
			}
		}

		// Token: 0x06003AD7 RID: 15063 RVA: 0x005AD55C File Offset: 0x005AB75C
		public override void OnInitialize()
		{
			base.OnInitialize();
			int num = 40;
			int num2 = 200;
			int num3 = 50 + num + 10;
			int num4 = 70;
			UIElement uIElement = new UIElement();
			uIElement.Width.Set(600f, 0f);
			uIElement.Top.Set((float)num2, 0f);
			uIElement.Height.Set((float)(-(float)num2), 1f);
			uIElement.HAlign = 0.5f;
			UIPanel uIPanel = new UIPanel();
			uIPanel.Width.Set(0f, 1f);
			uIPanel.Height.Set((float)(-(float)num3), 1f);
			uIPanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
			this.AddBackButton(num, uIElement);
			int num5 = 6 + num4;
			UIList uiList = this.AddUIList(uIPanel, (float)num5);
			this.FillUIList(uiList);
			this.AddHorizontalSeparator(uIPanel, 0f, 0).Top = new StyleDimension((float)(-(float)num4 + 3), 1f);
			this.AddDescriptionPanel(uIPanel, (float)(num4 - 6), "desc");
			uIElement.Append(uIPanel);
			base.Append(uIElement);
			this.SetDefaultOptions();
			this.AddPublishButton(num, uIElement);
		}

		// Token: 0x06003AD8 RID: 15064 RVA: 0x005AD698 File Offset: 0x005AB898
		private void SetDefaultOptions()
		{
			this.SetTagsFromFoundEntry();
			GroupOptionButton<WorkshopItemPublicSettingId>[] publicityOptions = this._publicityOptions;
			for (int i = 0; i < publicityOptions.Length; i++)
			{
				publicityOptions[i].SetCurrentOption(this._optionPublicity);
			}
			this.UpdateImagePreview();
		}

		// Token: 0x06003AD9 RID: 15065 RVA: 0x005AD6D4 File Offset: 0x005AB8D4
		private void FillUIList(UIList uiList)
		{
			UIElement uIElement = new UIElement
			{
				Width = new StyleDimension(0f, 0f),
				Height = new StyleDimension(0f, 0f)
			};
			uIElement.SetPadding(0f);
			uiList.Add(uIElement);
			uiList.Add(this.CreateSteamDisclaimer("disclaimer"));
			if (this._isMod)
			{
				uiList.Add((this as WorkshopPublishInfoStateForMods).CreateTmlDisclaimer("disclaimer"));
				(this as WorkshopPublishInfoStateForMods).AddNonModOwnerPublishWarning(uiList);
			}
			uiList.Add(this.CreatePreviewImageSelectionPanel("image"));
			uiList.Add(this.CreatePublicSettingsRow(0f, 44f, "public"));
			uiList.Add(this.CreateTagOptionsPanel(0f, 44, "tags"));
		}

		// Token: 0x06003ADA RID: 15066 RVA: 0x005AD7A4 File Offset: 0x005AB9A4
		private UIElement CreatePreviewImageSelectionPanel(string tagGroup)
		{
			UIElement uielement = new UIElement();
			uielement.Width = new StyleDimension(0f, 1f);
			uielement.Height = new StyleDimension(80f, 0f);
			UIElement uIElement = new UIElement
			{
				Width = new StyleDimension(72f, 0f),
				Height = new StyleDimension(72f, 0f),
				HAlign = 1f,
				VAlign = 0.5f,
				Left = new StyleDimension(-6f, 0f),
				Top = new StyleDimension(0f, 0f)
			};
			uIElement.SetPadding(0f);
			uielement.Append(uIElement);
			float num = 86f;
			if (!this._isMod)
			{
				this._defaultPreviewImageTexture = Main.Assets.Request<Texture2D>("Images/UI/Workshop/DefaultPreviewImage");
			}
			else
			{
				this._defaultPreviewImageTexture = Main.Assets.Request<Texture2D>("Images/UI/DefaultResourcePackIcon");
			}
			UIImage uIImage = new UIImage(this._defaultPreviewImageTexture)
			{
				Width = new StyleDimension(-4f, 1f),
				Height = new StyleDimension(-4f, 1f),
				HAlign = 0.5f,
				VAlign = 0.5f,
				ScaleToFit = true,
				AllowResizingDimensions = false
			};
			UIImage element = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders"))
			{
				HAlign = 0.5f,
				VAlign = 0.5f
			};
			uIElement.Append(uIImage);
			if (!this._isMod)
			{
				uIElement.Append(element);
			}
			this._previewImageUIElement = uIImage;
			UICharacterNameButton uICharacterNameButton = new UICharacterNameButton(Language.GetText("Workshop.PreviewImagePathTitle"), Language.GetText("Workshop.PreviewImagePathEmpty"), Language.GetText("Workshop.PreviewImagePathDescription"))
			{
				Width = StyleDimension.FromPixelsAndPercent(0f - num, 1f),
				Height = new StyleDimension(0f, 1f)
			};
			uICharacterNameButton.OnLeftMouseDown += this.Click_SetPreviewImage;
			uICharacterNameButton.OnMouseOver += this.ShowOptionDescription;
			uICharacterNameButton.OnMouseOut += this.ClearOptionDescription;
			uICharacterNameButton.SetSnapPoint(tagGroup, 0, null, null);
			uielement.Append(uICharacterNameButton);
			this._previewImagePathPlate = uICharacterNameButton;
			return uielement;
		}

		// Token: 0x06003ADB RID: 15067 RVA: 0x005AD9F8 File Offset: 0x005ABBF8
		private void SetTagsFromFoundEntry()
		{
			FoundWorkshopEntryInfo info;
			if (!this.TryFindingTags(out info))
			{
				return;
			}
			this._isUpdate = true;
			if (info.tags != null)
			{
				foreach (GroupOptionButton<WorkshopTagOption> tagOption in this._tagOptions)
				{
					bool flag = info.tags.Contains(tagOption.OptionValue.InternalNameForAPIs);
					tagOption.SetCurrentOption(flag ? tagOption.OptionValue : null);
					tagOption.SetColor(tagOption.IsSelected ? new Color(152, 175, 235) : Colors.InventoryDefaultColor, 1f);
				}
			}
			this._optionPublicity = info.publicity;
		}

		// Token: 0x06003ADC RID: 15068
		protected abstract bool TryFindingTags(out FoundWorkshopEntryInfo info);

		// Token: 0x06003ADD RID: 15069 RVA: 0x005ADAC4 File Offset: 0x005ABCC4
		private void Click_SetPreviewImage(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			this.OpenFileDialogueToSelectPreviewImage();
		}

		// Token: 0x06003ADE RID: 15070 RVA: 0x005ADAE4 File Offset: 0x005ABCE4
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
			UIElement uIElement = new UIElement
			{
				HAlign = 0.5f,
				VAlign = 1f,
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(num, 0f)
			};
			groupOptionButton.Append(uIElement);
			UIText uIText = new UIText(Language.GetText("Workshop.SteamDisclaimer"), 1f, false)
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(-40f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				TextColor = Color.Cyan,
				IgnoresMouseInteraction = true
			};
			uIText.PaddingLeft = 20f;
			uIText.PaddingRight = 20f;
			uIText.PaddingTop = 4f;
			uIText.IsWrapped = true;
			this._disclaimerText = uIText;
			groupOptionButton.OnLeftClick += this.steamDisclaimerText_OnClick;
			groupOptionButton.OnMouseOver += this.steamDisclaimerText_OnMouseOver;
			groupOptionButton.OnMouseOut += this.steamDisclaimerText_OnMouseOut;
			uIElement.Append(uIText);
			uIText.SetSnapPoint(tagGroup, 0, null, null);
			this._steamDisclaimerButton = uIText;
			return groupOptionButton;
		}

		// Token: 0x06003ADF RID: 15071 RVA: 0x005ADCE1 File Offset: 0x005ABEE1
		private void steamDisclaimerText_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			this._disclaimerText.TextColor = Color.Cyan;
			this.ClearOptionDescription(evt, listeningElement);
		}

		// Token: 0x06003AE0 RID: 15072 RVA: 0x005ADCFB File Offset: 0x005ABEFB
		private void steamDisclaimerText_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this._disclaimerText.TextColor = Color.LightCyan;
			this.ShowOptionDescription(evt, listeningElement);
		}

		// Token: 0x06003AE1 RID: 15073 RVA: 0x005ADD2C File Offset: 0x005ABF2C
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

		// Token: 0x06003AE2 RID: 15074 RVA: 0x005ADD60 File Offset: 0x005ABF60
		public override void Recalculate()
		{
			this.UpdateScrollbar();
			base.Recalculate();
		}

		// Token: 0x06003AE3 RID: 15075 RVA: 0x005ADD70 File Offset: 0x005ABF70
		private void UpdateScrollbar()
		{
			if (this._scrollbar != null)
			{
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
		}

		// Token: 0x06003AE4 RID: 15076 RVA: 0x005ADE18 File Offset: 0x005AC018
		private UIList AddUIList(UIElement container, float antiHeight)
		{
			this._uiListContainer = container;
			float num = 0f;
			UIElement uielement = new UIElement();
			uielement.HAlign = 0f;
			uielement.VAlign = 0f;
			uielement.Width = StyleDimension.FromPixelsAndPercent((0f - num) * 2f, 1f);
			uielement.Left = StyleDimension.FromPixels(0f - num);
			uielement.Height = StyleDimension.FromPixelsAndPercent(-2f - antiHeight, 1f);
			uielement.OverflowHidden = true;
			UIElement uielement2 = uielement;
			this._listContainer = uielement;
			UIElement uIElement = uielement2;
			UISlicedImage uISlicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/Workshop/ListBackground"))
			{
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(0f, 1f),
				Color = Color.White * 0.7f
			};
			uISlicedImage.SetSliceDepths(4);
			container.Append(uIElement);
			uIElement.Append(uISlicedImage);
			UIList uIList = new UIList
			{
				Width = StyleDimension.FromPixelsAndPercent(-10f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(-4f, 1f),
				HAlign = 0.5f,
				VAlign = 0.5f,
				OverflowHidden = true
			};
			uIList.ManualSortMethod = new Action<List<UIElement>>(this.ManualIfnoSortingMethod);
			uIList.ListPadding = 5f;
			uIElement.Append(uIList);
			UIScrollbar uIScrollbar = new UIScrollbar
			{
				HAlign = 1f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent((0f - num) * 2f, 1f),
				Left = StyleDimension.FromPixels(0f - num),
				Height = StyleDimension.FromPixelsAndPercent(-14f - antiHeight, 1f),
				Top = StyleDimension.FromPixels(6f)
			};
			uIScrollbar.SetView(100f, 1000f);
			uIList.SetScrollbar(uIScrollbar);
			this._uiListRect = uIElement;
			this._scrollbar = uIScrollbar;
			return uIList;
		}

		// Token: 0x06003AE5 RID: 15077 RVA: 0x005AE01C File Offset: 0x005AC21C
		private void ManualIfnoSortingMethod(List<UIElement> list)
		{
		}

		// Token: 0x06003AE6 RID: 15078 RVA: 0x005AE020 File Offset: 0x005AC220
		private UIElement CreatePublicSettingsRow(float accumulatedHeight, float height, string tagGroup)
		{
			UIElement entirePanel;
			UIElement innerPanel;
			this.CreateStylizedCategoryPanel(height, "Workshop.CategoryTitlePublicity", out entirePanel, out innerPanel);
			WorkshopItemPublicSettingId[] array = new WorkshopItemPublicSettingId[]
			{
				WorkshopItemPublicSettingId.Public,
				WorkshopItemPublicSettingId.FriendsOnly,
				WorkshopItemPublicSettingId.Private,
				WorkshopItemPublicSettingId.Unlisted
			};
			LocalizedText[] array2 = new LocalizedText[]
			{
				Language.GetText("Workshop.SettingsPublicityPublic"),
				Language.GetText("Workshop.SettingsPublicityFriendsOnly"),
				Language.GetText("Workshop.SettingsPublicityPrivate"),
				Language.GetText("tModLoader.SettingsPublicityUnlisted")
			};
			LocalizedText[] array3 = new LocalizedText[]
			{
				Language.GetText("Workshop.SettingsPublicityPublicDescription"),
				Language.GetText("Workshop.SettingsPublicityFriendsOnlyDescription"),
				Language.GetText("Workshop.SettingsPublicityPrivateDescription"),
				Language.GetText("tModLoader.SettingsPublicityUnlistedDescription")
			};
			Color[] array4 = new Color[]
			{
				Color.White,
				Color.White,
				Color.White,
				Color.White
			};
			string[] array5 = new string[]
			{
				"Images/UI/Workshop/PublicityPublic",
				"Images/UI/Workshop/PublicityFriendsOnly",
				"Images/UI/Workshop/PublicityPrivate",
				"Images/UI/WorldCreation/IconRandomName"
			};
			float num = 0.98f;
			GroupOptionButton<WorkshopItemPublicSettingId>[] array6 = new GroupOptionButton<WorkshopItemPublicSettingId>[array.Length];
			for (int i = 0; i < array6.Length; i++)
			{
				GroupOptionButton<WorkshopItemPublicSettingId> groupOptionButton = new GroupOptionButton<WorkshopItemPublicSettingId>(array[i], array2[i], array3[i], array4[i], array5[i], 0.8f, 1f, 16f);
				groupOptionButton.Width = StyleDimension.FromPixelsAndPercent((float)(-2 * (array6.Length - 1)), 1f / (float)array6.Length * num);
				groupOptionButton.HAlign = (float)i / (float)(array6.Length - 1);
				groupOptionButton.Left = StyleDimension.FromPercent((1f - num) * (1f - groupOptionButton.HAlign * 2f));
				groupOptionButton.Top.Set(accumulatedHeight, 0f);
				groupOptionButton.OnLeftMouseDown += this.ClickPublicityOption;
				groupOptionButton.OnMouseOver += this.ShowOptionDescription;
				groupOptionButton.OnMouseOut += this.ClearOptionDescription;
				groupOptionButton.SetSnapPoint(tagGroup, i, null, null);
				innerPanel.Append(groupOptionButton);
				array6[i] = groupOptionButton;
			}
			this._publicityOptions = array6;
			return entirePanel;
		}

		// Token: 0x06003AE7 RID: 15079 RVA: 0x005AE260 File Offset: 0x005AC460
		private UIElement CreateTagOptionsPanel(float accumulatedHeight, int heightPerRow, string tagGroup)
		{
			List<WorkshopTagOption> tagsToShow = this.GetTagsToShow();
			int num = 3;
			int num2 = (int)Math.Ceiling((double)((float)tagsToShow.Count / (float)num)) + 1;
			int num3 = heightPerRow * num2;
			UIElement entirePanel;
			UIElement innerPanel;
			this.CreateStylizedCategoryPanel((float)num3, "Workshop.CategoryTitleTags", out entirePanel, out innerPanel);
			float num4 = 0.98f;
			int additionalIOffset = 0;
			List<GroupOptionButton<WorkshopTagOption>> list = new List<GroupOptionButton<WorkshopTagOption>>();
			for (int i = 0; i < tagsToShow.Count; i++)
			{
				WorkshopTagOption workshopTagOption = tagsToShow[i];
				GroupOptionButton<WorkshopTagOption> groupOptionButton = new GroupOptionButton<WorkshopTagOption>(workshopTagOption, Language.GetText(workshopTagOption.NameKey), Language.GetText(workshopTagOption.NameKey + "Description"), Color.White, null, 1f, 0.5f, 16f);
				groupOptionButton.ShowHighlightWhenSelected = false;
				groupOptionButton.SetCurrentOption(null);
				int num5 = (i + additionalIOffset) / num;
				if (workshopTagOption.InternalNameForAPIs == "English")
				{
					num5++;
					if ((i + additionalIOffset) % num != 0)
					{
						additionalIOffset += 3;
						num5++;
					}
					additionalIOffset += 3 - (i + additionalIOffset) % num;
					this.AddHorizontalSeparator(entirePanel, (float)(44 + num5 * heightPerRow), 4);
					UIText uIText = new UIText(Language.GetText("tModLoader.TagsCategoryLanguage"), 1f, false)
					{
						HAlign = 0f,
						VAlign = 0f,
						Width = StyleDimension.FromPixelsAndPercent(-40f, 1f),
						Height = StyleDimension.FromPixelsAndPercent(44f, 0f),
						Top = StyleDimension.FromPixelsAndPercent(5f + (float)(num5 * heightPerRow), 0f)
					};
					uIText.PaddingLeft = 20f;
					uIText.PaddingRight = 20f;
					uIText.PaddingTop = 6f;
					uIText.IsWrapped = false;
					entirePanel.Append(uIText);
				}
				num5 = (i + additionalIOffset) / num;
				int num6 = (i + additionalIOffset) % num;
				groupOptionButton.Width = StyleDimension.FromPixelsAndPercent((float)(-4 * (num - 1)), 1f / (float)num * num4);
				groupOptionButton.HAlign = (float)num6 / (float)(num - 1);
				groupOptionButton.Left = StyleDimension.FromPercent((1f - num4) * (1f - groupOptionButton.HAlign * 2f));
				groupOptionButton.Top.Set((float)(num5 * heightPerRow), 0f);
				groupOptionButton.OnLeftMouseDown += this.ClickTagOption;
				groupOptionButton.OnMouseOver += this.ShowOptionDescription;
				groupOptionButton.OnMouseOut += this.ClearOptionDescription;
				groupOptionButton.SetSnapPoint(tagGroup, i, null, null);
				innerPanel.Append(groupOptionButton);
				list.Add(groupOptionButton);
			}
			this._tagOptions = list;
			return entirePanel;
		}

		// Token: 0x06003AE8 RID: 15080 RVA: 0x005AE518 File Offset: 0x005AC718
		private void CreateStylizedCategoryPanel(float height, string titleTextKey, out UIElement entirePanel, out UIElement innerPanel)
		{
			float num = 44f;
			UISlicedImage uISlicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanel"))
			{
				HAlign = 0.5f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Left = StyleDimension.FromPixels(0f),
				Height = StyleDimension.FromPixelsAndPercent(height + num + 4f, 0f),
				Top = StyleDimension.FromPixels(0f)
			};
			uISlicedImage.SetSliceDepths(8);
			uISlicedImage.Color = Color.White * 0.7f;
			innerPanel = new UIElement
			{
				HAlign = 0.5f,
				VAlign = 1f,
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(height, 0f)
			};
			uISlicedImage.Append(innerPanel);
			this.AddHorizontalSeparator(uISlicedImage, num, 4);
			UIText uIText = new UIText(Language.GetText(titleTextKey), 1f, false)
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(-40f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(num, 0f),
				Top = StyleDimension.FromPixelsAndPercent(5f, 0f)
			};
			uIText.PaddingLeft = 20f;
			uIText.PaddingRight = 20f;
			uIText.PaddingTop = 6f;
			uIText.IsWrapped = false;
			uISlicedImage.Append(uIText);
			entirePanel = uISlicedImage;
		}

		// Token: 0x06003AE9 RID: 15081 RVA: 0x005AE6AC File Offset: 0x005AC8AC
		private void ClickTagOption(UIMouseEvent evt, UIElement listeningElement)
		{
			GroupOptionButton<WorkshopTagOption> groupOptionButton = (GroupOptionButton<WorkshopTagOption>)listeningElement;
			groupOptionButton.SetCurrentOption(groupOptionButton.IsSelected ? null : groupOptionButton.OptionValue);
			groupOptionButton.SetColor(groupOptionButton.IsSelected ? new Color(152, 175, 235) : Colors.InventoryDefaultColor, 1f);
		}

		// Token: 0x06003AEA RID: 15082 RVA: 0x005AE708 File Offset: 0x005AC908
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

		// Token: 0x06003AEB RID: 15083 RVA: 0x005AE74C File Offset: 0x005AC94C
		public void ShowOptionDescription(UIMouseEvent evt, UIElement listeningElement)
		{
			LocalizedText localizedText = null;
			GroupOptionButton<WorkshopItemPublicSettingId> groupOptionButton = listeningElement as GroupOptionButton<WorkshopItemPublicSettingId>;
			if (groupOptionButton != null)
			{
				localizedText = groupOptionButton.Description;
			}
			UICharacterNameButton uICharacterNameButton = listeningElement as UICharacterNameButton;
			if (uICharacterNameButton != null)
			{
				localizedText = uICharacterNameButton.Description;
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

		// Token: 0x06003AEC RID: 15084 RVA: 0x005AE7C5 File Offset: 0x005AC9C5
		public void ClearOptionDescription(UIMouseEvent evt, UIElement listeningElement)
		{
			this._descriptionText.SetText(Language.GetText("Workshop.InfoDescriptionDefault"));
		}

		// Token: 0x06003AED RID: 15085 RVA: 0x005AE7DC File Offset: 0x005AC9DC
		private UIElement CreateInsturctionsPanel(float accumulatedHeight, float height, string tagGroup)
		{
			float num = 0f;
			UISlicedImage uislicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight"));
			uislicedImage.HAlign = 0.5f;
			uislicedImage.VAlign = 0f;
			uislicedImage.Width = StyleDimension.FromPixelsAndPercent((0f - num) * 2f, 1f);
			uislicedImage.Left = StyleDimension.FromPixels(0f - num);
			uislicedImage.Height = StyleDimension.FromPixelsAndPercent(height, 0f);
			uislicedImage.Top = StyleDimension.FromPixels(accumulatedHeight);
			uislicedImage.SetSliceDepths(10);
			uislicedImage.Color = Color.LightGray * 0.7f;
			UIText uIText = new UIText(Language.GetText(this._instructionsTextKey), 1f, false)
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(-40f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Top = StyleDimension.FromPixelsAndPercent(5f, 0f)
			};
			uIText.PaddingLeft = 20f;
			uIText.PaddingRight = 20f;
			uIText.PaddingTop = 6f;
			uIText.IsWrapped = true;
			uislicedImage.Append(uIText);
			return uislicedImage;
		}

		// Token: 0x06003AEE RID: 15086 RVA: 0x005AE920 File Offset: 0x005ACB20
		private void AddDescriptionPanel(UIElement container, float height, string tagGroup)
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
			UIText uIText = new UIText(Language.GetText("Workshop.InfoDescriptionDefault"), 0.85f, false)
			{
				HAlign = 0f,
				VAlign = 1f,
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(0f, 1f)
			};
			uIText.PaddingLeft = 4f;
			uIText.PaddingRight = 4f;
			uIText.PaddingTop = 4f;
			uIText.IsWrapped = true;
			uISlicedImage.Append(uIText);
			this._descriptionText = uIText;
		}

		// Token: 0x06003AEF RID: 15087
		protected abstract string GetPublishedObjectDisplayName();

		// Token: 0x06003AF0 RID: 15088
		protected abstract List<WorkshopTagOption> GetTagsToShow();

		// Token: 0x06003AF1 RID: 15089 RVA: 0x005AEA5E File Offset: 0x005ACC5E
		private void Click_GoBack(UIMouseEvent evt, UIElement listeningElement)
		{
			this.HandleBackButtonUsage();
		}

		// Token: 0x06003AF2 RID: 15090 RVA: 0x005AEA66 File Offset: 0x005ACC66
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

		// Token: 0x06003AF3 RID: 15091 RVA: 0x005AEA91 File Offset: 0x005ACC91
		private void Click_Publish(UIMouseEvent evt, UIElement listeningElement)
		{
			this.GoToPublishConfirmation();
		}

		// Token: 0x06003AF4 RID: 15092
		protected abstract void GoToPublishConfirmation();

		// Token: 0x06003AF5 RID: 15093 RVA: 0x005AEA9C File Offset: 0x005ACC9C
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06003AF6 RID: 15094 RVA: 0x005AEAF1 File Offset: 0x005ACCF1
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06003AF7 RID: 15095 RVA: 0x005AEB30 File Offset: 0x005ACD30
		private void AddPublishButton(int backButtonYLift, UIElement outerContainer)
		{
			UITextPanel<LocalizedText> uITextPanel = new UITextPanel<LocalizedText>(Language.GetText(this._isUpdate ? "tModLoader.PublishUpdateButton" : "tModLoader.PublishNewButton"), 0.7f, true);
			uITextPanel.TextColor = (this._isUpdate ? Color.White : Color.Orange);
			uITextPanel.Width.Set(-10f, 0.5f);
			uITextPanel.Height.Set(50f, 0f);
			uITextPanel.VAlign = 1f;
			uITextPanel.Top.Set((float)(-(float)backButtonYLift), 0f);
			uITextPanel.HAlign = 1f;
			uITextPanel.OnMouseOver += this.FadedMouseOver;
			uITextPanel.OnMouseOut += this.FadedMouseOut;
			uITextPanel.OnLeftClick += this.Click_Publish;
			uITextPanel.SetSnapPoint("publish", 0, null, null);
			outerContainer.Append(uITextPanel);
			this._publishButton = uITextPanel;
		}

		// Token: 0x06003AF8 RID: 15096 RVA: 0x005AEC34 File Offset: 0x005ACE34
		private void AddBackButton(int backButtonYLift, UIElement outerContainer)
		{
			UITextPanel<LocalizedText> uITextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
			uITextPanel.Width.Set(-10f, 0.5f);
			uITextPanel.Height.Set(50f, 0f);
			uITextPanel.VAlign = 1f;
			uITextPanel.Top.Set((float)(-(float)backButtonYLift), 0f);
			uITextPanel.HAlign = 0f;
			uITextPanel.OnMouseOver += this.FadedMouseOver;
			uITextPanel.OnMouseOut += this.FadedMouseOut;
			uITextPanel.OnLeftClick += this.Click_GoBack;
			uITextPanel.SetSnapPoint("back", 0, null, null);
			outerContainer.Append(uITextPanel);
			this._backButton = uITextPanel;
		}

		// Token: 0x06003AF9 RID: 15097 RVA: 0x005AED0C File Offset: 0x005ACF0C
		private UIElement AddHorizontalSeparator(UIElement Container, float accumualtedHeight, int widthReduction = 0)
		{
			UIHorizontalSeparator uIHorizontalSeparator = new UIHorizontalSeparator(2, true)
			{
				Width = StyleDimension.FromPixelsAndPercent((float)(-(float)widthReduction), 1f),
				HAlign = 0.5f,
				Top = StyleDimension.FromPixels(accumualtedHeight - 8f),
				Color = Color.Lerp(Color.White, new Color(63, 65, 151, 255), 0.85f) * 0.9f
			};
			Container.Append(uIHorizontalSeparator);
			return uIHorizontalSeparator;
		}

		// Token: 0x06003AFA RID: 15098 RVA: 0x005AED8C File Offset: 0x005ACF8C
		protected WorkshopItemPublishSettings GetPublishSettings()
		{
			WorkshopItemPublishSettings workshopItemPublishSettings = new WorkshopItemPublishSettings();
			workshopItemPublishSettings.Publicity = this._optionPublicity;
			workshopItemPublishSettings.UsedTags = (from x in this._tagOptions
			where x.IsSelected
			select x.OptionValue).ToArray<WorkshopTagOption>();
			workshopItemPublishSettings.PreviewImagePath = this._previewImagePath;
			workshopItemPublishSettings.ChangeNotes = (this as WorkshopPublishInfoStateForMods).changeNotes;
			return workshopItemPublishSettings;
		}

		// Token: 0x06003AFB RID: 15099 RVA: 0x005AEE20 File Offset: 0x005AD020
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

		// Token: 0x06003AFC RID: 15100 RVA: 0x005AEE80 File Offset: 0x005AD080
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

		// Token: 0x06003AFD RID: 15101 RVA: 0x005AEEE0 File Offset: 0x005AD0E0
		protected virtual void UpdateImagePreview()
		{
			Texture2D texture2D = null;
			string contents = this.PrettifyPath(this._previewImagePath);
			this._previewImagePathPlate.SetContents(contents);
			if (this._previewImagePath != null)
			{
				try
				{
					using (FileStream stream = File.OpenRead(this._previewImagePath))
					{
						texture2D = Texture2D.FromStream(Main.graphics.GraphicsDevice, stream);
					}
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

		// Token: 0x06003AFE RID: 15102 RVA: 0x005AF020 File Offset: 0x005AD220
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.SetupGamepadPoints(spriteBatch);
		}

		// Token: 0x06003AFF RID: 15103 RVA: 0x005AF030 File Offset: 0x005AD230
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
			int num = 3000;
			int id = num;
			List<SnapPoint> snapPoints = this.GetSnapPoints();
			this._helper.RemovePointsOutOfView(snapPoints, this._listContainer, spriteBatch);
			UILinkPoint linkPoint = this._helper.GetLinkPoint(id++, this._backButton);
			UILinkPoint linkPoint2 = this._helper.GetLinkPoint(id++, this._publishButton);
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
			UILinkPoint upSide = this._helper.TryMakeLinkPoint(ref id, snap);
			UILinkPoint uILinkPoint = this._helper.TryMakeLinkPoint(ref id, snap2);
			this._helper.PairLeftRight(linkPoint, linkPoint2);
			this._helper.PairUpDown(upSide, uILinkPoint);
			UILinkPoint[] array = this._helper.CreateUILinkStripHorizontal(ref id, (from x in snapPoints
			where x.Name == "public"
			select x).ToList<SnapPoint>());
			if (array.Length != 0)
			{
				this._helper.LinkHorizontalStripUpSideToSingle(array, uILinkPoint);
			}
			UILinkPoint topLinkPoint = (array.Length != 0) ? array[0] : null;
			UILinkPoint bottomLinkPoint = linkPoint;
			List<SnapPoint> pointsForGrid = (from x in snapPoints
			where x.Name == "tags"
			select x).ToList<SnapPoint>();
			UILinkPoint[,] array2 = this._helper.CreateUILinkPointGrid(ref id, pointsForGrid, 3, topLinkPoint, null, null, bottomLinkPoint);
			int num2 = array2.GetLength(1) - 1;
			if (num2 >= 0)
			{
				this._helper.LinkHorizontalStripBottomSideToSingle(array, array2[0, 0]);
				for (int num3 = array2.GetLength(0) - 1; num3 >= 0; num3--)
				{
					if (array2[num3, num2] != null)
					{
						this._helper.PairUpDown(array2[num3, num2], linkPoint2);
						break;
					}
				}
			}
			UILinkPoint upSide2 = UILinkPointNavigator.Points[id - 1];
			this._helper.PairUpDown(upSide2, linkPoint);
			this._helper.MoveToVisuallyClosestPoint(num, id);
		}

		// Token: 0x040054CD RID: 21709
		protected UIState _previousUIState;

		// Token: 0x040054CE RID: 21710
		protected TPublishedObjectType _dataObject;

		// Token: 0x040054CF RID: 21711
		protected string _publishedObjectNameDescriptorTexKey;

		// Token: 0x040054D0 RID: 21712
		protected string _instructionsTextKey;

		// Token: 0x040054D1 RID: 21713
		private UIElement _uiListContainer;

		// Token: 0x040054D2 RID: 21714
		private UIElement _uiListRect;

		// Token: 0x040054D3 RID: 21715
		private UIScrollbar _scrollbar;

		// Token: 0x040054D4 RID: 21716
		private bool _isScrollbarAttached;

		// Token: 0x040054D5 RID: 21717
		private UIText _descriptionText;

		// Token: 0x040054D6 RID: 21718
		private UIElement _listContainer;

		// Token: 0x040054D7 RID: 21719
		private UIElement _backButton;

		// Token: 0x040054D8 RID: 21720
		private UIElement _publishButton;

		// Token: 0x040054D9 RID: 21721
		private WorkshopItemPublicSettingId _optionPublicity = WorkshopItemPublicSettingId.Public;

		// Token: 0x040054DA RID: 21722
		private GroupOptionButton<WorkshopItemPublicSettingId>[] _publicityOptions;

		// Token: 0x040054DB RID: 21723
		protected List<GroupOptionButton<WorkshopTagOption>> _tagOptions;

		// Token: 0x040054DC RID: 21724
		protected UICharacterNameButton _previewImagePathPlate;

		// Token: 0x040054DD RID: 21725
		protected Texture2D _previewImageTransientTexture;

		// Token: 0x040054DE RID: 21726
		protected UIImage _previewImageUIElement;

		// Token: 0x040054DF RID: 21727
		protected string _previewImagePath;

		// Token: 0x040054E0 RID: 21728
		private Asset<Texture2D> _defaultPreviewImageTexture;

		// Token: 0x040054E1 RID: 21729
		private UIElement _steamDisclaimerButton;

		// Token: 0x040054E2 RID: 21730
		protected UIText _disclaimerText;

		// Token: 0x040054E3 RID: 21731
		private UIGamepadHelper _helper;

		// Token: 0x040054E4 RID: 21732
		protected UIText _tMLDisclaimerText;

		// Token: 0x040054E5 RID: 21733
		protected UIElement _tMLDisclaimerButton;

		// Token: 0x040054E6 RID: 21734
		private bool _isMod;

		// Token: 0x040054E7 RID: 21735
		private bool _isUpdate;
	}
}
