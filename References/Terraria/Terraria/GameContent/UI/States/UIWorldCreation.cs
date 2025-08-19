using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.Social;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x0200034B RID: 843
	public class UIWorldCreation : UIState
	{
		// Token: 0x06002657 RID: 9815 RVA: 0x0057468D File Offset: 0x0057288D
		public UIWorldCreation()
		{
			this.BuildPage();
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x0057469C File Offset: 0x0057289C
		private void BuildPage()
		{
			int num = 18;
			base.RemoveAllChildren();
			UIElement uielement = new UIElement
			{
				Width = StyleDimension.FromPixels(500f),
				Height = StyleDimension.FromPixels(434f + (float)num),
				Top = StyleDimension.FromPixels(170f - (float)num),
				HAlign = 0.5f,
				VAlign = 0f
			};
			uielement.SetPadding(0f);
			base.Append(uielement);
			UIPanel uipanel = new UIPanel
			{
				Width = StyleDimension.FromPercent(1f),
				Height = StyleDimension.FromPixels((float)(280 + num)),
				Top = StyleDimension.FromPixels(50f),
				BackgroundColor = new Color(33, 43, 79) * 0.8f
			};
			uipanel.SetPadding(0f);
			uielement.Append(uipanel);
			this.MakeBackAndCreatebuttons(uielement);
			UIElement uielement2 = new UIElement
			{
				Top = StyleDimension.FromPixelsAndPercent(0f, 0f),
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				HAlign = 1f
			};
			uielement2.SetPadding(0f);
			uielement2.PaddingTop = 8f;
			uielement2.PaddingBottom = 12f;
			uipanel.Append(uielement2);
			this.MakeInfoMenu(uielement2);
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x00574804 File Offset: 0x00572A04
		private void MakeInfoMenu(UIElement parentContainer)
		{
			UIElement uielement = new UIElement
			{
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				HAlign = 0.5f,
				VAlign = 0f
			};
			uielement.SetPadding(10f);
			uielement.PaddingBottom = 0f;
			uielement.PaddingTop = 0f;
			parentContainer.Append(uielement);
			float num = 0f;
			float num2 = 88f;
			float num3 = 44f;
			float num4 = num2 + num3;
			float pixels = num3;
			GroupOptionButton<bool> groupOptionButton = new GroupOptionButton<bool>(true, null, Language.GetText("UI.WorldCreationRandomizeNameDescription"), Color.White, "Images/UI/WorldCreation/IconRandomName", 1f, 0.5f, 10f)
			{
				Width = StyleDimension.FromPixelsAndPercent(40f, 0f),
				Height = new StyleDimension(40f, 0f),
				HAlign = 0f,
				Top = StyleDimension.FromPixelsAndPercent(num, 0f),
				ShowHighlightWhenSelected = false
			};
			groupOptionButton.OnLeftMouseDown += this.ClickRandomizeName;
			groupOptionButton.OnMouseOver += this.ShowOptionDescription;
			groupOptionButton.OnMouseOut += this.ClearOptionDescription;
			groupOptionButton.SetSnapPoint("RandomizeName", 0, null, null);
			uielement.Append(groupOptionButton);
			UICharacterNameButton uicharacterNameButton = new UICharacterNameButton(Language.GetText("UI.WorldCreationName"), Language.GetText("UI.WorldCreationNameEmpty"), Language.GetText("UI.WorldDescriptionName"))
			{
				Width = StyleDimension.FromPixelsAndPercent(-num4, 1f),
				HAlign = 0f,
				Left = new StyleDimension(pixels, 0f),
				Top = StyleDimension.FromPixelsAndPercent(num, 0f)
			};
			uicharacterNameButton.OnLeftMouseDown += this.Click_SetName;
			uicharacterNameButton.OnMouseOver += this.ShowOptionDescription;
			uicharacterNameButton.OnMouseOut += this.ClearOptionDescription;
			uicharacterNameButton.SetSnapPoint("Name", 0, null, null);
			uielement.Append(uicharacterNameButton);
			this._namePlate = uicharacterNameButton;
			CalculatedStyle dimensions = uicharacterNameButton.GetDimensions();
			num += dimensions.Height + 4f;
			GroupOptionButton<bool> groupOptionButton2 = new GroupOptionButton<bool>(true, null, Language.GetText("UI.WorldCreationRandomizeSeedDescription"), Color.White, "Images/UI/WorldCreation/IconRandomSeed", 1f, 0.5f, 10f)
			{
				Width = StyleDimension.FromPixelsAndPercent(40f, 0f),
				Height = new StyleDimension(40f, 0f),
				HAlign = 0f,
				Top = StyleDimension.FromPixelsAndPercent(num, 0f),
				ShowHighlightWhenSelected = false
			};
			groupOptionButton2.OnLeftMouseDown += this.ClickRandomizeSeed;
			groupOptionButton2.OnMouseOver += this.ShowOptionDescription;
			groupOptionButton2.OnMouseOut += this.ClearOptionDescription;
			groupOptionButton2.SetSnapPoint("RandomizeSeed", 0, null, null);
			uielement.Append(groupOptionButton2);
			UICharacterNameButton uicharacterNameButton2 = new UICharacterNameButton(Language.GetText("UI.WorldCreationSeed"), Language.GetText("UI.WorldCreationSeedEmpty"), Language.GetText("UI.WorldDescriptionSeed"))
			{
				Width = StyleDimension.FromPixelsAndPercent(-num4, 1f),
				HAlign = 0f,
				Left = new StyleDimension(pixels, 0f),
				Top = StyleDimension.FromPixelsAndPercent(num, 0f),
				DistanceFromTitleToOption = 29f
			};
			uicharacterNameButton2.OnLeftMouseDown += this.Click_SetSeed;
			uicharacterNameButton2.OnMouseOver += this.ShowOptionDescription;
			uicharacterNameButton2.OnMouseOut += this.ClearOptionDescription;
			uicharacterNameButton2.SetSnapPoint("Seed", 0, null, null);
			uielement.Append(uicharacterNameButton2);
			this._seedPlate = uicharacterNameButton2;
			UIWorldCreationPreview uiworldCreationPreview = new UIWorldCreationPreview
			{
				Width = StyleDimension.FromPixels(84f),
				Height = StyleDimension.FromPixels(84f),
				HAlign = 1f,
				VAlign = 0f
			};
			uielement.Append(uiworldCreationPreview);
			this._previewPlate = uiworldCreationPreview;
			dimensions = uicharacterNameButton2.GetDimensions();
			num += dimensions.Height + 10f;
			UIWorldCreation.AddHorizontalSeparator(uielement, num + 2f);
			float usableWidthPercent = 1f;
			this.AddWorldSizeOptions(uielement, num, new UIElement.MouseEvent(this.ClickSizeOption), "size", usableWidthPercent);
			num += 48f;
			UIWorldCreation.AddHorizontalSeparator(uielement, num);
			this.AddWorldDifficultyOptions(uielement, num, new UIElement.MouseEvent(this.ClickDifficultyOption), "difficulty", usableWidthPercent);
			num += 48f;
			UIWorldCreation.AddHorizontalSeparator(uielement, num);
			this.AddWorldEvilOptions(uielement, num, new UIElement.MouseEvent(this.ClickEvilOption), "evil", usableWidthPercent);
			num += 48f;
			UIWorldCreation.AddHorizontalSeparator(uielement, num);
			this.AddDescriptionPanel(uielement, num, "desc");
			this.SetDefaultOptions();
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x00574D18 File Offset: 0x00572F18
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

		// Token: 0x0600265B RID: 9819 RVA: 0x00574D88 File Offset: 0x00572F88
		private void SetDefaultOptions()
		{
			this.AssignRandomWorldName();
			this.AssignRandomWorldSeed();
			this.UpdateInputFields();
			GroupOptionButton<UIWorldCreation.WorldSizeId>[] sizeButtons = this._sizeButtons;
			for (int i = 0; i < sizeButtons.Length; i++)
			{
				sizeButtons[i].SetCurrentOption(UIWorldCreation.WorldSizeId.Medium);
			}
			this._optionSize = UIWorldCreation.WorldSizeId.Medium;
			if (Main.ActivePlayerFileData.Player.difficulty == 3)
			{
				GroupOptionButton<UIWorldCreation.WorldDifficultyId>[] difficultyButtons = this._difficultyButtons;
				for (int i = 0; i < difficultyButtons.Length; i++)
				{
					difficultyButtons[i].SetCurrentOption(UIWorldCreation.WorldDifficultyId.Creative);
				}
				this._optionDifficulty = UIWorldCreation.WorldDifficultyId.Creative;
				this.UpdatePreviewPlate();
			}
			else
			{
				GroupOptionButton<UIWorldCreation.WorldDifficultyId>[] difficultyButtons = this._difficultyButtons;
				for (int i = 0; i < difficultyButtons.Length; i++)
				{
					difficultyButtons[i].SetCurrentOption(UIWorldCreation.WorldDifficultyId.Normal);
				}
			}
			GroupOptionButton<UIWorldCreation.WorldEvilId>[] evilButtons = this._evilButtons;
			for (int i = 0; i < evilButtons.Length; i++)
			{
				evilButtons[i].SetCurrentOption(UIWorldCreation.WorldEvilId.Random);
			}
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x00574E48 File Offset: 0x00573048
		private void AddDescriptionPanel(UIElement container, float accumulatedHeight, string tagGroup)
		{
			float num = 0f;
			UISlicedImage uislicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", 1))
			{
				HAlign = 0.5f,
				VAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent(-num * 2f, 1f),
				Left = StyleDimension.FromPixels(-num),
				Height = StyleDimension.FromPixelsAndPercent(40f, 0f),
				Top = StyleDimension.FromPixels(2f)
			};
			uislicedImage.SetSliceDepths(10);
			uislicedImage.Color = Color.LightGray * 0.7f;
			container.Append(uislicedImage);
			UIText uitext = new UIText(Language.GetText("UI.WorldDescriptionDefault"), 0.82f, false)
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Top = StyleDimension.FromPixelsAndPercent(5f, 0f)
			};
			uitext.PaddingLeft = 20f;
			uitext.PaddingRight = 20f;
			uitext.PaddingTop = 6f;
			uislicedImage.Append(uitext);
			this._descriptionText = uitext;
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x00574F90 File Offset: 0x00573190
		private void AddWorldSizeOptions(UIElement container, float accumualtedHeight, UIElement.MouseEvent clickEvent, string tagGroup, float usableWidthPercent)
		{
			UIWorldCreation.WorldSizeId[] array = new UIWorldCreation.WorldSizeId[]
			{
				UIWorldCreation.WorldSizeId.Small,
				UIWorldCreation.WorldSizeId.Medium,
				UIWorldCreation.WorldSizeId.Large
			};
			LocalizedText[] array2 = new LocalizedText[]
			{
				Lang.menu[92],
				Lang.menu[93],
				Lang.menu[94]
			};
			LocalizedText[] array3 = new LocalizedText[]
			{
				Language.GetText("UI.WorldDescriptionSizeSmall"),
				Language.GetText("UI.WorldDescriptionSizeMedium"),
				Language.GetText("UI.WorldDescriptionSizeLarge")
			};
			Color[] array4 = new Color[]
			{
				Color.Cyan,
				Color.Lerp(Color.Cyan, Color.LimeGreen, 0.5f),
				Color.LimeGreen
			};
			string[] array5 = new string[]
			{
				"Images/UI/WorldCreation/IconSizeSmall",
				"Images/UI/WorldCreation/IconSizeMedium",
				"Images/UI/WorldCreation/IconSizeLarge"
			};
			GroupOptionButton<UIWorldCreation.WorldSizeId>[] array6 = new GroupOptionButton<UIWorldCreation.WorldSizeId>[array.Length];
			for (int i = 0; i < array6.Length; i++)
			{
				GroupOptionButton<UIWorldCreation.WorldSizeId> groupOptionButton = new GroupOptionButton<UIWorldCreation.WorldSizeId>(array[i], array2[i], array3[i], array4[i], array5[i], 1f, 1f, 16f);
				groupOptionButton.Width = StyleDimension.FromPixelsAndPercent((float)(-4 * (array6.Length - 1)), 1f / (float)array6.Length * usableWidthPercent);
				groupOptionButton.Left = StyleDimension.FromPercent(1f - usableWidthPercent);
				groupOptionButton.HAlign = (float)i / (float)(array6.Length - 1);
				groupOptionButton.Top.Set(accumualtedHeight, 0f);
				groupOptionButton.OnLeftMouseDown += clickEvent;
				groupOptionButton.OnMouseOver += this.ShowOptionDescription;
				groupOptionButton.OnMouseOut += this.ClearOptionDescription;
				groupOptionButton.SetSnapPoint(tagGroup, i, null, null);
				container.Append(groupOptionButton);
				array6[i] = groupOptionButton;
			}
			this._sizeButtons = array6;
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x00575170 File Offset: 0x00573370
		private void AddWorldDifficultyOptions(UIElement container, float accumualtedHeight, UIElement.MouseEvent clickEvent, string tagGroup, float usableWidthPercent)
		{
			UIWorldCreation.WorldDifficultyId[] array = new UIWorldCreation.WorldDifficultyId[]
			{
				UIWorldCreation.WorldDifficultyId.Creative,
				UIWorldCreation.WorldDifficultyId.Normal,
				UIWorldCreation.WorldDifficultyId.Expert,
				UIWorldCreation.WorldDifficultyId.Master
			};
			LocalizedText[] array2 = new LocalizedText[]
			{
				Language.GetText("UI.Creative"),
				Language.GetText("UI.Normal"),
				Language.GetText("UI.Expert"),
				Language.GetText("UI.Master")
			};
			LocalizedText[] array3 = new LocalizedText[]
			{
				Language.GetText("UI.WorldDescriptionCreative"),
				Language.GetText("UI.WorldDescriptionNormal"),
				Language.GetText("UI.WorldDescriptionExpert"),
				Language.GetText("UI.WorldDescriptionMaster")
			};
			Color[] array4 = new Color[]
			{
				Main.creativeModeColor,
				Color.White,
				Main.mcColor,
				Main.hcColor
			};
			string[] array5 = new string[]
			{
				"Images/UI/WorldCreation/IconDifficultyCreative",
				"Images/UI/WorldCreation/IconDifficultyNormal",
				"Images/UI/WorldCreation/IconDifficultyExpert",
				"Images/UI/WorldCreation/IconDifficultyMaster"
			};
			GroupOptionButton<UIWorldCreation.WorldDifficultyId>[] array6 = new GroupOptionButton<UIWorldCreation.WorldDifficultyId>[array.Length];
			for (int i = 0; i < array6.Length; i++)
			{
				GroupOptionButton<UIWorldCreation.WorldDifficultyId> groupOptionButton = new GroupOptionButton<UIWorldCreation.WorldDifficultyId>(array[i], array2[i], array3[i], array4[i], array5[i], 1f, 1f, 16f);
				groupOptionButton.Width = StyleDimension.FromPixelsAndPercent((float)(-1 * (array6.Length - 1)), 1f / (float)array6.Length * usableWidthPercent);
				groupOptionButton.Left = StyleDimension.FromPercent(1f - usableWidthPercent);
				groupOptionButton.HAlign = (float)i / (float)(array6.Length - 1);
				groupOptionButton.Top.Set(accumualtedHeight, 0f);
				groupOptionButton.OnLeftMouseDown += clickEvent;
				groupOptionButton.OnMouseOver += this.ShowOptionDescription;
				groupOptionButton.OnMouseOut += this.ClearOptionDescription;
				groupOptionButton.SetSnapPoint(tagGroup, i, null, null);
				container.Append(groupOptionButton);
				array6[i] = groupOptionButton;
			}
			this._difficultyButtons = array6;
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x00575378 File Offset: 0x00573578
		private void AddWorldEvilOptions(UIElement container, float accumualtedHeight, UIElement.MouseEvent clickEvent, string tagGroup, float usableWidthPercent)
		{
			UIWorldCreation.WorldEvilId[] array = new UIWorldCreation.WorldEvilId[]
			{
				UIWorldCreation.WorldEvilId.Random,
				UIWorldCreation.WorldEvilId.Corruption,
				UIWorldCreation.WorldEvilId.Crimson
			};
			LocalizedText[] array2 = new LocalizedText[]
			{
				Lang.misc[103],
				Lang.misc[101],
				Lang.misc[102]
			};
			LocalizedText[] array3 = new LocalizedText[]
			{
				Language.GetText("UI.WorldDescriptionEvilRandom"),
				Language.GetText("UI.WorldDescriptionEvilCorrupt"),
				Language.GetText("UI.WorldDescriptionEvilCrimson")
			};
			Color[] array4 = new Color[]
			{
				Color.White,
				Color.MediumPurple,
				Color.IndianRed
			};
			string[] array5 = new string[]
			{
				"Images/UI/WorldCreation/IconEvilRandom",
				"Images/UI/WorldCreation/IconEvilCorruption",
				"Images/UI/WorldCreation/IconEvilCrimson"
			};
			GroupOptionButton<UIWorldCreation.WorldEvilId>[] array6 = new GroupOptionButton<UIWorldCreation.WorldEvilId>[array.Length];
			for (int i = 0; i < array6.Length; i++)
			{
				GroupOptionButton<UIWorldCreation.WorldEvilId> groupOptionButton = new GroupOptionButton<UIWorldCreation.WorldEvilId>(array[i], array2[i], array3[i], array4[i], array5[i], 1f, 1f, 16f);
				groupOptionButton.Width = StyleDimension.FromPixelsAndPercent((float)(-4 * (array6.Length - 1)), 1f / (float)array6.Length * usableWidthPercent);
				groupOptionButton.Left = StyleDimension.FromPercent(1f - usableWidthPercent);
				groupOptionButton.HAlign = (float)i / (float)(array6.Length - 1);
				groupOptionButton.Top.Set(accumualtedHeight, 0f);
				groupOptionButton.OnLeftMouseDown += clickEvent;
				groupOptionButton.OnMouseOver += this.ShowOptionDescription;
				groupOptionButton.OnMouseOut += this.ClearOptionDescription;
				groupOptionButton.SetSnapPoint(tagGroup, i, null, null);
				container.Append(groupOptionButton);
				array6[i] = groupOptionButton;
			}
			this._evilButtons = array6;
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x00575547 File Offset: 0x00573747
		private void ClickRandomizeName(UIMouseEvent evt, UIElement listeningElement)
		{
			this.AssignRandomWorldName();
			this.UpdateInputFields();
			this.UpdateSliders();
			this.UpdatePreviewPlate();
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x00575561 File Offset: 0x00573761
		private void ClickRandomizeSeed(UIMouseEvent evt, UIElement listeningElement)
		{
			this.AssignRandomWorldSeed();
			this.UpdateInputFields();
			this.UpdateSliders();
			this.UpdatePreviewPlate();
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x0057557C File Offset: 0x0057377C
		private void ClickSizeOption(UIMouseEvent evt, UIElement listeningElement)
		{
			GroupOptionButton<UIWorldCreation.WorldSizeId> groupOptionButton = (GroupOptionButton<UIWorldCreation.WorldSizeId>)listeningElement;
			this._optionSize = groupOptionButton.OptionValue;
			GroupOptionButton<UIWorldCreation.WorldSizeId>[] sizeButtons = this._sizeButtons;
			for (int i = 0; i < sizeButtons.Length; i++)
			{
				sizeButtons[i].SetCurrentOption(groupOptionButton.OptionValue);
			}
			this.UpdatePreviewPlate();
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x005755C8 File Offset: 0x005737C8
		private void ClickDifficultyOption(UIMouseEvent evt, UIElement listeningElement)
		{
			GroupOptionButton<UIWorldCreation.WorldDifficultyId> groupOptionButton = (GroupOptionButton<UIWorldCreation.WorldDifficultyId>)listeningElement;
			this._optionDifficulty = groupOptionButton.OptionValue;
			GroupOptionButton<UIWorldCreation.WorldDifficultyId>[] difficultyButtons = this._difficultyButtons;
			for (int i = 0; i < difficultyButtons.Length; i++)
			{
				difficultyButtons[i].SetCurrentOption(groupOptionButton.OptionValue);
			}
			this.UpdatePreviewPlate();
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x00575614 File Offset: 0x00573814
		private void ClickEvilOption(UIMouseEvent evt, UIElement listeningElement)
		{
			GroupOptionButton<UIWorldCreation.WorldEvilId> groupOptionButton = (GroupOptionButton<UIWorldCreation.WorldEvilId>)listeningElement;
			this._optionEvil = groupOptionButton.OptionValue;
			GroupOptionButton<UIWorldCreation.WorldEvilId>[] evilButtons = this._evilButtons;
			for (int i = 0; i < evilButtons.Length; i++)
			{
				evilButtons[i].SetCurrentOption(groupOptionButton.OptionValue);
			}
			this.UpdatePreviewPlate();
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x0057565D File Offset: 0x0057385D
		private void UpdatePreviewPlate()
		{
			this._previewPlate.UpdateOption((byte)this._optionDifficulty, (byte)this._optionEvil, (byte)this._optionSize);
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x00575680 File Offset: 0x00573880
		private void UpdateSliders()
		{
			GroupOptionButton<UIWorldCreation.WorldSizeId>[] sizeButtons = this._sizeButtons;
			for (int i = 0; i < sizeButtons.Length; i++)
			{
				sizeButtons[i].SetCurrentOption(this._optionSize);
			}
			GroupOptionButton<UIWorldCreation.WorldDifficultyId>[] difficultyButtons = this._difficultyButtons;
			for (int i = 0; i < difficultyButtons.Length; i++)
			{
				difficultyButtons[i].SetCurrentOption(this._optionDifficulty);
			}
			GroupOptionButton<UIWorldCreation.WorldEvilId>[] evilButtons = this._evilButtons;
			for (int i = 0; i < evilButtons.Length; i++)
			{
				evilButtons[i].SetCurrentOption(this._optionEvil);
			}
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x005756F8 File Offset: 0x005738F8
		public void ShowOptionDescription(UIMouseEvent evt, UIElement listeningElement)
		{
			LocalizedText localizedText = null;
			GroupOptionButton<UIWorldCreation.WorldSizeId> groupOptionButton = listeningElement as GroupOptionButton<UIWorldCreation.WorldSizeId>;
			if (groupOptionButton != null)
			{
				localizedText = groupOptionButton.Description;
			}
			GroupOptionButton<UIWorldCreation.WorldDifficultyId> groupOptionButton2 = listeningElement as GroupOptionButton<UIWorldCreation.WorldDifficultyId>;
			if (groupOptionButton2 != null)
			{
				localizedText = groupOptionButton2.Description;
			}
			GroupOptionButton<UIWorldCreation.WorldEvilId> groupOptionButton3 = listeningElement as GroupOptionButton<UIWorldCreation.WorldEvilId>;
			if (groupOptionButton3 != null)
			{
				localizedText = groupOptionButton3.Description;
			}
			UICharacterNameButton uicharacterNameButton = listeningElement as UICharacterNameButton;
			if (uicharacterNameButton != null)
			{
				localizedText = uicharacterNameButton.Description;
			}
			GroupOptionButton<bool> groupOptionButton4 = listeningElement as GroupOptionButton<bool>;
			if (groupOptionButton4 != null)
			{
				localizedText = groupOptionButton4.Description;
			}
			if (localizedText != null)
			{
				this._descriptionText.SetText(localizedText);
			}
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x00575771 File Offset: 0x00573971
		public void ClearOptionDescription(UIMouseEvent evt, UIElement listeningElement)
		{
			this._descriptionText.SetText(Language.GetText("UI.WorldDescriptionDefault"));
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x00575788 File Offset: 0x00573988
		private void MakeBackAndCreatebuttons(UIElement outerContainer)
		{
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true)
			{
				Width = StyleDimension.FromPixelsAndPercent(-10f, 0.5f),
				Height = StyleDimension.FromPixels(50f),
				VAlign = 1f,
				HAlign = 0f,
				Top = StyleDimension.FromPixels(-45f)
			};
			uitextPanel.OnMouseOver += this.FadedMouseOver;
			uitextPanel.OnMouseOut += this.FadedMouseOut;
			uitextPanel.OnLeftMouseDown += this.Click_GoBack;
			uitextPanel.SetSnapPoint("Back", 0, null, null);
			outerContainer.Append(uitextPanel);
			UITextPanel<LocalizedText> uitextPanel2 = new UITextPanel<LocalizedText>(Language.GetText("UI.Create"), 0.7f, true)
			{
				Width = StyleDimension.FromPixelsAndPercent(-10f, 0.5f),
				Height = StyleDimension.FromPixels(50f),
				VAlign = 1f,
				HAlign = 1f,
				Top = StyleDimension.FromPixels(-45f)
			};
			uitextPanel2.OnMouseOver += this.FadedMouseOver;
			uitextPanel2.OnMouseOut += this.FadedMouseOut;
			uitextPanel2.OnLeftMouseDown += this.Click_NamingAndCreating;
			uitextPanel2.SetSnapPoint("Create", 0, null, null);
			outerContainer.Append(uitextPanel2);
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x0057590D File Offset: 0x00573B0D
		private void Click_GoBack(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.OpenWorldSelectUI();
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x0057592C File Offset: 0x00573B2C
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x0056C33D File Offset: 0x0056A53D
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x00575984 File Offset: 0x00573B84
		private void Click_SetName(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.clrInput();
			UIVirtualKeyboard uivirtualKeyboard = new UIVirtualKeyboard(Lang.menu[48].Value, "", new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedSettingName), new Action(this.GoBackHere), 0, true);
			uivirtualKeyboard.SetMaxInputLength(27);
			Main.MenuUI.SetState(uivirtualKeyboard);
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x005759F0 File Offset: 0x00573BF0
		private void Click_SetSeed(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.clrInput();
			UIVirtualKeyboard uivirtualKeyboard = new UIVirtualKeyboard(Language.GetTextValue("UI.EnterSeed"), "", new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedSettingSeed), new Action(this.GoBackHere), 0, true);
			uivirtualKeyboard.SetMaxInputLength(40);
			Main.MenuUI.SetState(uivirtualKeyboard);
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x00575A5C File Offset: 0x00573C5C
		private void Click_NamingAndCreating(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			if (string.IsNullOrEmpty(this._optionwWorldName))
			{
				this._optionwWorldName = "";
				Main.clrInput();
				UIVirtualKeyboard uivirtualKeyboard = new UIVirtualKeyboard(Lang.menu[48].Value, "", new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedNamingAndCreating), new Action(this.GoBackHere), 0, false);
				uivirtualKeyboard.SetMaxInputLength(27);
				Main.MenuUI.SetState(uivirtualKeyboard);
				return;
			}
			this.FinishCreatingWorld();
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x00575AE7 File Offset: 0x00573CE7
		private void OnFinishedSettingName(string name)
		{
			this._optionwWorldName = name.Trim();
			this.UpdateInputFields();
			this.GoBackHere();
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x00575B04 File Offset: 0x00573D04
		private void UpdateInputFields()
		{
			this._namePlate.SetContents(this._optionwWorldName);
			this._namePlate.Recalculate();
			this._namePlate.TrimDisplayIfOverElementDimensions(27);
			this._namePlate.Recalculate();
			this._seedPlate.SetContents(this._optionSeed);
			this._seedPlate.Recalculate();
			this._seedPlate.TrimDisplayIfOverElementDimensions(40);
			this._seedPlate.Recalculate();
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x00575B7C File Offset: 0x00573D7C
		private void OnFinishedSettingSeed(string seed)
		{
			this._optionSeed = seed.Trim();
			string optionSeed;
			this.ProcessSeed(out optionSeed);
			this._optionSeed = optionSeed;
			this.UpdateInputFields();
			this.UpdateSliders();
			this.UpdatePreviewPlate();
			this.GoBackHere();
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x00575BBC File Offset: 0x00573DBC
		private void GoBackHere()
		{
			Main.MenuUI.SetState(this);
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x00575BC9 File Offset: 0x00573DC9
		private void OnFinishedNamingAndCreating(string name)
		{
			this.OnFinishedSettingName(name);
			this.FinishCreatingWorld();
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x00575BD8 File Offset: 0x00573DD8
		private void FinishCreatingWorld()
		{
			string text;
			this.ProcessSeed(out text);
			switch (this._optionSize)
			{
			case UIWorldCreation.WorldSizeId.Small:
				Main.maxTilesX = 4200;
				Main.maxTilesY = 1200;
				break;
			case UIWorldCreation.WorldSizeId.Medium:
				Main.maxTilesX = 6400;
				Main.maxTilesY = 1800;
				break;
			case UIWorldCreation.WorldSizeId.Large:
				Main.maxTilesX = 8400;
				Main.maxTilesY = 2400;
				break;
			}
			WorldGen.setWorldSize();
			switch (this._optionDifficulty)
			{
			case UIWorldCreation.WorldDifficultyId.Normal:
				Main.GameMode = 0;
				break;
			case UIWorldCreation.WorldDifficultyId.Expert:
				Main.GameMode = 1;
				break;
			case UIWorldCreation.WorldDifficultyId.Master:
				Main.GameMode = 2;
				break;
			case UIWorldCreation.WorldDifficultyId.Creative:
				Main.GameMode = 3;
				break;
			}
			switch (this._optionEvil)
			{
			case UIWorldCreation.WorldEvilId.Random:
				WorldGen.WorldGenParam_Evil = -1;
				break;
			case UIWorldCreation.WorldEvilId.Corruption:
				WorldGen.WorldGenParam_Evil = 0;
				break;
			case UIWorldCreation.WorldEvilId.Crimson:
				WorldGen.WorldGenParam_Evil = 1;
				break;
			}
			Main.ActiveWorldFileData = WorldFile.CreateMetadata(Main.worldName = this._optionwWorldName.Trim(), SocialAPI.Cloud != null && SocialAPI.Cloud.EnabledByDefault, Main.GameMode);
			if (text.Length == 0)
			{
				Main.ActiveWorldFileData.SetSeedToRandom();
			}
			else
			{
				Main.ActiveWorldFileData.SetSeed(text);
			}
			Main.menuMode = 10;
			WorldGen.CreateNewWorld(null);
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x00575D1C File Offset: 0x00573F1C
		public static void ProcessSpecialWorldSeeds(string processedSeed)
		{
			WorldGen.noTrapsWorldGen = false;
			WorldGen.notTheBees = false;
			WorldGen.getGoodWorldGen = false;
			WorldGen.tenthAnniversaryWorldGen = false;
			WorldGen.dontStarveWorldGen = false;
			WorldGen.tempRemixWorldGen = false;
			WorldGen.tempTenthAnniversaryWorldGen = false;
			WorldGen.everythingWorldGen = false;
			if (processedSeed.ToLower() == "no traps" || processedSeed.ToLower() == "notraps")
			{
				WorldGen.noTrapsWorldGen = true;
			}
			if (processedSeed.ToLower() == "not the bees" || processedSeed.ToLower() == "not the bees!" || processedSeed.ToLower() == "notthebees")
			{
				WorldGen.notTheBees = true;
			}
			if (processedSeed.ToLower() == "for the worthy" || processedSeed.ToLower() == "fortheworthy")
			{
				WorldGen.getGoodWorldGen = true;
			}
			if (processedSeed.ToLower() == "don't dig up" || processedSeed.ToLower() == "dont dig up" || processedSeed.ToLower() == "dontdigup")
			{
				WorldGen.tempRemixWorldGen = true;
			}
			if (processedSeed.ToLower() == "celebrationmk10")
			{
				WorldGen.tempTenthAnniversaryWorldGen = true;
			}
			if (processedSeed.ToLower() == "constant" || processedSeed.ToLower() == "theconstant" || processedSeed.ToLower() == "the constant" || processedSeed.ToLower() == "eye4aneye" || processedSeed.ToLower() == "eyeforaneye")
			{
				WorldGen.dontStarveWorldGen = true;
			}
			if (processedSeed.ToLower() == "get fixed boi" || processedSeed.ToLower() == "getfixedboi")
			{
				WorldGen.noTrapsWorldGen = true;
				WorldGen.notTheBees = true;
				WorldGen.getGoodWorldGen = true;
				WorldGen.tempTenthAnniversaryWorldGen = true;
				WorldGen.dontStarveWorldGen = true;
				WorldGen.tempRemixWorldGen = true;
				WorldGen.everythingWorldGen = true;
			}
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x00575EEC File Offset: 0x005740EC
		private void ProcessSeed(out string processedSeed)
		{
			processedSeed = this._optionSeed;
			UIWorldCreation.ProcessSpecialWorldSeeds(processedSeed);
			string[] array = this._optionSeed.Split(new char[]
			{
				'.'
			});
			if (array.Length != 4)
			{
				return;
			}
			int num;
			if (int.TryParse(array[0], out num))
			{
				switch (num)
				{
				case 1:
					this._optionSize = UIWorldCreation.WorldSizeId.Small;
					break;
				case 2:
					this._optionSize = UIWorldCreation.WorldSizeId.Medium;
					break;
				case 3:
					this._optionSize = UIWorldCreation.WorldSizeId.Large;
					break;
				}
			}
			if (int.TryParse(array[1], out num))
			{
				switch (num)
				{
				case 1:
					this._optionDifficulty = UIWorldCreation.WorldDifficultyId.Normal;
					break;
				case 2:
					this._optionDifficulty = UIWorldCreation.WorldDifficultyId.Expert;
					break;
				case 3:
					this._optionDifficulty = UIWorldCreation.WorldDifficultyId.Master;
					break;
				case 4:
					this._optionDifficulty = UIWorldCreation.WorldDifficultyId.Creative;
					break;
				}
			}
			if (int.TryParse(array[2], out num))
			{
				if (num != 1)
				{
					if (num == 2)
					{
						this._optionEvil = UIWorldCreation.WorldEvilId.Crimson;
					}
				}
				else
				{
					this._optionEvil = UIWorldCreation.WorldEvilId.Corruption;
				}
			}
			processedSeed = array[3];
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x00575FD4 File Offset: 0x005741D4
		private void AssignRandomWorldName()
		{
			do
			{
				LocalizedText localizedText = Language.SelectRandom(Lang.CreateDialogFilter("RandomWorldName_Composition."), null);
				LocalizedText localizedText2 = Language.SelectRandom(Lang.CreateDialogFilter("RandomWorldName_Adjective."), null);
				LocalizedText localizedText3 = Language.SelectRandom(Lang.CreateDialogFilter("RandomWorldName_Location."), null);
				LocalizedText localizedText4 = Language.SelectRandom(Lang.CreateDialogFilter("RandomWorldName_Noun."), null);
				var obj = new
				{
					Adjective = localizedText2.Value,
					Location = localizedText3.Value,
					Noun = localizedText4.Value
				};
				this._optionwWorldName = localizedText.FormatWith(obj);
				if (Main.rand.Next(10000) == 0)
				{
					this._optionwWorldName = Language.GetTextValue("SpecialWorldName.TheConstant");
				}
			}
			while (this._optionwWorldName.Length > 27);
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x0057607C File Offset: 0x0057427C
		private void AssignRandomWorldSeed()
		{
			this._optionSeed = Main.rand.Next().ToString();
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x005760A1 File Offset: 0x005742A1
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.SetupGamepadPoints(spriteBatch);
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x005760B4 File Offset: 0x005742B4
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 1;
			int num = 3000;
			List<SnapPoint> snapPoints = this.GetSnapPoints();
			SnapPoint snapPoint = null;
			SnapPoint snapPoint2 = null;
			SnapPoint snapPoint3 = null;
			SnapPoint snapPoint4 = null;
			SnapPoint snapPoint5 = null;
			SnapPoint snapPoint6 = null;
			for (int i = 0; i < snapPoints.Count; i++)
			{
				SnapPoint snapPoint7 = snapPoints[i];
				string name = snapPoint7.Name;
				if (!(name == "Back"))
				{
					if (!(name == "Create"))
					{
						if (!(name == "Name"))
						{
							if (!(name == "Seed"))
							{
								if (!(name == "RandomizeName"))
								{
									if (name == "RandomizeSeed")
									{
										snapPoint6 = snapPoint7;
									}
								}
								else
								{
									snapPoint5 = snapPoint7;
								}
							}
							else
							{
								snapPoint4 = snapPoint7;
							}
						}
						else
						{
							snapPoint3 = snapPoint7;
						}
					}
					else
					{
						snapPoint2 = snapPoint7;
					}
				}
				else
				{
					snapPoint = snapPoint7;
				}
			}
			List<SnapPoint> snapGroup = this.GetSnapGroup(snapPoints, "size");
			List<SnapPoint> snapGroup2 = this.GetSnapGroup(snapPoints, "difficulty");
			List<SnapPoint> snapGroup3 = this.GetSnapGroup(snapPoints, "evil");
			UILinkPointNavigator.SetPosition(num, snapPoint.Position);
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[num];
			uilinkPoint.Unlink();
			UILinkPoint uilinkPoint2 = uilinkPoint;
			num++;
			UILinkPointNavigator.SetPosition(num, snapPoint2.Position);
			uilinkPoint = UILinkPointNavigator.Points[num];
			uilinkPoint.Unlink();
			UILinkPoint uilinkPoint3 = uilinkPoint;
			num++;
			UILinkPointNavigator.SetPosition(num, snapPoint5.Position);
			uilinkPoint = UILinkPointNavigator.Points[num];
			uilinkPoint.Unlink();
			UILinkPoint uilinkPoint4 = uilinkPoint;
			num++;
			UILinkPointNavigator.SetPosition(num, snapPoint3.Position);
			uilinkPoint = UILinkPointNavigator.Points[num];
			uilinkPoint.Unlink();
			UILinkPoint uilinkPoint5 = uilinkPoint;
			num++;
			UILinkPointNavigator.SetPosition(num, snapPoint6.Position);
			uilinkPoint = UILinkPointNavigator.Points[num];
			uilinkPoint.Unlink();
			UILinkPoint uilinkPoint6 = uilinkPoint;
			num++;
			UILinkPointNavigator.SetPosition(num, snapPoint4.Position);
			uilinkPoint = UILinkPointNavigator.Points[num];
			uilinkPoint.Unlink();
			UILinkPoint uilinkPoint7 = uilinkPoint;
			num++;
			UILinkPoint[] array = new UILinkPoint[snapGroup.Count];
			for (int j = 0; j < snapGroup.Count; j++)
			{
				UILinkPointNavigator.SetPosition(num, snapGroup[j].Position);
				uilinkPoint = UILinkPointNavigator.Points[num];
				uilinkPoint.Unlink();
				array[j] = uilinkPoint;
				num++;
			}
			UILinkPoint[] array2 = new UILinkPoint[snapGroup2.Count];
			for (int k = 0; k < snapGroup2.Count; k++)
			{
				UILinkPointNavigator.SetPosition(num, snapGroup2[k].Position);
				uilinkPoint = UILinkPointNavigator.Points[num];
				uilinkPoint.Unlink();
				array2[k] = uilinkPoint;
				num++;
			}
			UILinkPoint[] array3 = new UILinkPoint[snapGroup3.Count];
			for (int l = 0; l < snapGroup3.Count; l++)
			{
				UILinkPointNavigator.SetPosition(num, snapGroup3[l].Position);
				uilinkPoint = UILinkPointNavigator.Points[num];
				uilinkPoint.Unlink();
				array3[l] = uilinkPoint;
				num++;
			}
			this.LoopHorizontalLineLinks(array);
			this.LoopHorizontalLineLinks(array2);
			this.EstablishUpDownRelationship(array, array2);
			for (int m = 0; m < array.Length; m++)
			{
				array[m].Up = uilinkPoint7.ID;
			}
			if (true)
			{
				this.LoopHorizontalLineLinks(array3);
				this.EstablishUpDownRelationship(array2, array3);
				for (int n = 0; n < array3.Length; n++)
				{
					array3[n].Down = uilinkPoint2.ID;
				}
				array3[array3.Length - 1].Down = uilinkPoint3.ID;
				uilinkPoint3.Up = array3[array3.Length - 1].ID;
				uilinkPoint2.Up = array3[0].ID;
			}
			else
			{
				for (int num2 = 0; num2 < array2.Length; num2++)
				{
					array2[num2].Down = uilinkPoint2.ID;
				}
				array2[array2.Length - 1].Down = uilinkPoint3.ID;
				uilinkPoint3.Up = array2[array2.Length - 1].ID;
				uilinkPoint2.Up = array2[0].ID;
			}
			uilinkPoint3.Left = uilinkPoint2.ID;
			uilinkPoint2.Right = uilinkPoint3.ID;
			uilinkPoint5.Down = uilinkPoint7.ID;
			uilinkPoint5.Left = uilinkPoint4.ID;
			uilinkPoint4.Right = uilinkPoint5.ID;
			uilinkPoint7.Up = uilinkPoint5.ID;
			uilinkPoint7.Down = array[0].ID;
			uilinkPoint7.Left = uilinkPoint6.ID;
			uilinkPoint6.Right = uilinkPoint7.ID;
			uilinkPoint6.Up = uilinkPoint4.ID;
			uilinkPoint6.Down = array[0].ID;
			uilinkPoint4.Down = uilinkPoint6.ID;
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x00576550 File Offset: 0x00574750
		private void EstablishUpDownRelationship(UILinkPoint[] topSide, UILinkPoint[] bottomSide)
		{
			int num = Math.Max(topSide.Length, bottomSide.Length);
			for (int i = 0; i < num; i++)
			{
				int num2 = Math.Min(i, topSide.Length - 1);
				int num3 = Math.Min(i, bottomSide.Length - 1);
				topSide[num2].Down = bottomSide[num3].ID;
				bottomSide[num3].Up = topSide[num2].ID;
			}
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x005765B0 File Offset: 0x005747B0
		private void LoopHorizontalLineLinks(UILinkPoint[] pointsLine)
		{
			for (int i = 1; i < pointsLine.Length - 1; i++)
			{
				pointsLine[i - 1].Right = pointsLine[i].ID;
				pointsLine[i].Left = pointsLine[i - 1].ID;
				pointsLine[i].Right = pointsLine[i + 1].ID;
				pointsLine[i + 1].Left = pointsLine[i].ID;
			}
		}

		// Token: 0x0600267E RID: 9854 RVA: 0x00576618 File Offset: 0x00574818
		private List<SnapPoint> GetSnapGroup(List<SnapPoint> ptsOnPage, string groupName)
		{
			List<SnapPoint> list = (from a in ptsOnPage
			where a.Name == groupName
			select a).ToList<SnapPoint>();
			list.Sort(new Comparison<SnapPoint>(this.SortPoints));
			return list;
		}

		// Token: 0x0600267F RID: 9855 RVA: 0x0057665C File Offset: 0x0057485C
		private int SortPoints(SnapPoint a, SnapPoint b)
		{
			return a.Id.CompareTo(b.Id);
		}

		// Token: 0x04004A35 RID: 18997
		private UIWorldCreation.WorldSizeId _optionSize;

		// Token: 0x04004A36 RID: 18998
		private UIWorldCreation.WorldDifficultyId _optionDifficulty;

		// Token: 0x04004A37 RID: 18999
		private UIWorldCreation.WorldEvilId _optionEvil;

		// Token: 0x04004A38 RID: 19000
		private string _optionwWorldName;

		// Token: 0x04004A39 RID: 19001
		private string _optionSeed;

		// Token: 0x04004A3A RID: 19002
		private UICharacterNameButton _namePlate;

		// Token: 0x04004A3B RID: 19003
		private UICharacterNameButton _seedPlate;

		// Token: 0x04004A3C RID: 19004
		private UIWorldCreationPreview _previewPlate;

		// Token: 0x04004A3D RID: 19005
		private GroupOptionButton<UIWorldCreation.WorldSizeId>[] _sizeButtons;

		// Token: 0x04004A3E RID: 19006
		private GroupOptionButton<UIWorldCreation.WorldDifficultyId>[] _difficultyButtons;

		// Token: 0x04004A3F RID: 19007
		private GroupOptionButton<UIWorldCreation.WorldEvilId>[] _evilButtons;

		// Token: 0x04004A40 RID: 19008
		private UIText _descriptionText;

		// Token: 0x04004A41 RID: 19009
		public const int MAX_NAME_LENGTH = 27;

		// Token: 0x04004A42 RID: 19010
		public const int MAX_SEED_LENGTH = 40;

		// Token: 0x02000731 RID: 1841
		private enum WorldSizeId
		{
			// Token: 0x04006382 RID: 25474
			Small,
			// Token: 0x04006383 RID: 25475
			Medium,
			// Token: 0x04006384 RID: 25476
			Large
		}

		// Token: 0x02000732 RID: 1842
		private enum WorldDifficultyId
		{
			// Token: 0x04006386 RID: 25478
			Normal,
			// Token: 0x04006387 RID: 25479
			Expert,
			// Token: 0x04006388 RID: 25480
			Master,
			// Token: 0x04006389 RID: 25481
			Creative
		}

		// Token: 0x02000733 RID: 1843
		private enum WorldEvilId
		{
			// Token: 0x0400638B RID: 25483
			Random,
			// Token: 0x0400638C RID: 25484
			Corruption,
			// Token: 0x0400638D RID: 25485
			Crimson
		}
	}
}
