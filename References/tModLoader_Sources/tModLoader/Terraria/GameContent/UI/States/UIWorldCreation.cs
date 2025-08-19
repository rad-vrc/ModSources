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
	// Token: 0x020004E6 RID: 1254
	public class UIWorldCreation : UIState
	{
		// Token: 0x06003CC5 RID: 15557 RVA: 0x005C3B50 File Offset: 0x005C1D50
		public UIWorldCreation()
		{
			this.BuildPage();
		}

		// Token: 0x06003CC6 RID: 15558 RVA: 0x005C3B60 File Offset: 0x005C1D60
		private void BuildPage()
		{
			int num = 18;
			base.RemoveAllChildren();
			UIElement uIElement = new UIElement
			{
				Width = StyleDimension.FromPixels(500f),
				Height = StyleDimension.FromPixels(434f + (float)num),
				Top = StyleDimension.FromPixels(170f - (float)num),
				HAlign = 0.5f,
				VAlign = 0f
			};
			uIElement.SetPadding(0f);
			base.Append(uIElement);
			UIPanel uIPanel = new UIPanel
			{
				Width = StyleDimension.FromPercent(1f),
				Height = StyleDimension.FromPixels((float)(280 + num)),
				Top = StyleDimension.FromPixels(50f),
				BackgroundColor = new Color(33, 43, 79) * 0.8f
			};
			uIPanel.SetPadding(0f);
			uIElement.Append(uIPanel);
			this.MakeBackAndCreatebuttons(uIElement);
			UIElement uIElement2 = new UIElement
			{
				Top = StyleDimension.FromPixelsAndPercent(0f, 0f),
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				HAlign = 1f
			};
			uIElement2.SetPadding(0f);
			uIElement2.PaddingTop = 8f;
			uIElement2.PaddingBottom = 12f;
			uIPanel.Append(uIElement2);
			this.MakeInfoMenu(uIElement2);
		}

		// Token: 0x06003CC7 RID: 15559 RVA: 0x005C3CC8 File Offset: 0x005C1EC8
		private void MakeInfoMenu(UIElement parentContainer)
		{
			UIElement uIElement = new UIElement
			{
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				HAlign = 0.5f,
				VAlign = 0f
			};
			uIElement.SetPadding(10f);
			uIElement.PaddingBottom = 0f;
			uIElement.PaddingTop = 0f;
			parentContainer.Append(uIElement);
			float num = 0f;
			float num2 = 44f;
			float num3 = 88f + num2;
			float pixels = num2;
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
			uIElement.Append(groupOptionButton);
			UICharacterNameButton uICharacterNameButton = new UICharacterNameButton(Language.GetText("UI.WorldCreationName"), Language.GetText("UI.WorldCreationNameEmpty"), Language.GetText("UI.WorldDescriptionName"))
			{
				Width = StyleDimension.FromPixelsAndPercent(0f - num3, 1f),
				HAlign = 0f,
				Left = new StyleDimension(pixels, 0f),
				Top = StyleDimension.FromPixelsAndPercent(num, 0f)
			};
			uICharacterNameButton.OnLeftMouseDown += this.Click_SetName;
			uICharacterNameButton.OnMouseOver += this.ShowOptionDescription;
			uICharacterNameButton.OnMouseOut += this.ClearOptionDescription;
			uICharacterNameButton.SetSnapPoint("Name", 0, null, null);
			uIElement.Append(uICharacterNameButton);
			this._namePlate = uICharacterNameButton;
			num += uICharacterNameButton.GetDimensions().Height + 4f;
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
			uIElement.Append(groupOptionButton2);
			UICharacterNameButton uICharacterNameButton2 = new UICharacterNameButton(Language.GetText("UI.WorldCreationSeed"), Language.GetText("UI.WorldCreationSeedEmpty"), Language.GetText("UI.WorldDescriptionSeed"))
			{
				Width = StyleDimension.FromPixelsAndPercent(0f - num3, 1f),
				HAlign = 0f,
				Left = new StyleDimension(pixels, 0f),
				Top = StyleDimension.FromPixelsAndPercent(num, 0f),
				DistanceFromTitleToOption = 29f
			};
			uICharacterNameButton2.OnLeftMouseDown += this.Click_SetSeed;
			uICharacterNameButton2.OnMouseOver += this.ShowOptionDescription;
			uICharacterNameButton2.OnMouseOut += this.ClearOptionDescription;
			uICharacterNameButton2.SetSnapPoint("Seed", 0, null, null);
			uIElement.Append(uICharacterNameButton2);
			this._seedPlate = uICharacterNameButton2;
			UIWorldCreationPreview uIWorldCreationPreview = new UIWorldCreationPreview
			{
				Width = StyleDimension.FromPixels(84f),
				Height = StyleDimension.FromPixels(84f),
				HAlign = 1f,
				VAlign = 0f
			};
			uIElement.Append(uIWorldCreationPreview);
			this._previewPlate = uIWorldCreationPreview;
			num += uICharacterNameButton2.GetDimensions().Height + 10f;
			UIWorldCreation.AddHorizontalSeparator(uIElement, num + 2f);
			float usableWidthPercent = 1f;
			this.AddWorldSizeOptions(uIElement, num, new UIElement.MouseEvent(this.ClickSizeOption), "size", usableWidthPercent);
			num += 48f;
			UIWorldCreation.AddHorizontalSeparator(uIElement, num);
			this.AddWorldDifficultyOptions(uIElement, num, new UIElement.MouseEvent(this.ClickDifficultyOption), "difficulty", usableWidthPercent);
			num += 48f;
			UIWorldCreation.AddHorizontalSeparator(uIElement, num);
			this.AddWorldEvilOptions(uIElement, num, new UIElement.MouseEvent(this.ClickEvilOption), "evil", usableWidthPercent);
			num += 48f;
			UIWorldCreation.AddHorizontalSeparator(uIElement, num);
			this.AddDescriptionPanel(uIElement, num, "desc");
			this.SetDefaultOptions();
		}

		// Token: 0x06003CC8 RID: 15560 RVA: 0x005C41E0 File Offset: 0x005C23E0
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

		// Token: 0x06003CC9 RID: 15561 RVA: 0x005C4250 File Offset: 0x005C2450
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
				for (int j = 0; j < difficultyButtons.Length; j++)
				{
					difficultyButtons[j].SetCurrentOption(UIWorldCreation.WorldDifficultyId.Creative);
				}
				this._optionDifficulty = UIWorldCreation.WorldDifficultyId.Creative;
				this.UpdatePreviewPlate();
			}
			else
			{
				GroupOptionButton<UIWorldCreation.WorldDifficultyId>[] difficultyButtons2 = this._difficultyButtons;
				for (int k = 0; k < difficultyButtons2.Length; k++)
				{
					difficultyButtons2[k].SetCurrentOption(UIWorldCreation.WorldDifficultyId.Normal);
				}
			}
			GroupOptionButton<UIWorldCreation.WorldEvilId>[] evilButtons = this._evilButtons;
			for (int l = 0; l < evilButtons.Length; l++)
			{
				evilButtons[l].SetCurrentOption(UIWorldCreation.WorldEvilId.Random);
			}
		}

		// Token: 0x06003CCA RID: 15562 RVA: 0x005C4324 File Offset: 0x005C2524
		private void AddDescriptionPanel(UIElement container, float accumulatedHeight, string tagGroup)
		{
			float num = 0f;
			UISlicedImage uISlicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight"))
			{
				HAlign = 0.5f,
				VAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent((0f - num) * 2f, 1f),
				Left = StyleDimension.FromPixels(0f - num),
				Height = StyleDimension.FromPixelsAndPercent(40f, 0f),
				Top = StyleDimension.FromPixels(2f)
			};
			uISlicedImage.SetSliceDepths(10);
			uISlicedImage.Color = Color.LightGray * 0.7f;
			container.Append(uISlicedImage);
			UIText uIText = new UIText(Language.GetText("UI.WorldDescriptionDefault"), 0.82f, false)
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
			uISlicedImage.Append(uIText);
			this._descriptionText = uIText;
		}

		// Token: 0x06003CCB RID: 15563 RVA: 0x005C4474 File Offset: 0x005C2674
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

		// Token: 0x06003CCC RID: 15564 RVA: 0x005C4654 File Offset: 0x005C2854
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

		// Token: 0x06003CCD RID: 15565 RVA: 0x005C485C File Offset: 0x005C2A5C
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

		// Token: 0x06003CCE RID: 15566 RVA: 0x005C4A2B File Offset: 0x005C2C2B
		private void ClickRandomizeName(UIMouseEvent evt, UIElement listeningElement)
		{
			this.AssignRandomWorldName();
			this.UpdateInputFields();
			this.UpdateSliders();
			this.UpdatePreviewPlate();
		}

		// Token: 0x06003CCF RID: 15567 RVA: 0x005C4A45 File Offset: 0x005C2C45
		private void ClickRandomizeSeed(UIMouseEvent evt, UIElement listeningElement)
		{
			this.AssignRandomWorldSeed();
			this.UpdateInputFields();
			this.UpdateSliders();
			this.UpdatePreviewPlate();
		}

		// Token: 0x06003CD0 RID: 15568 RVA: 0x005C4A60 File Offset: 0x005C2C60
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

		// Token: 0x06003CD1 RID: 15569 RVA: 0x005C4AAC File Offset: 0x005C2CAC
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

		// Token: 0x06003CD2 RID: 15570 RVA: 0x005C4AF8 File Offset: 0x005C2CF8
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

		// Token: 0x06003CD3 RID: 15571 RVA: 0x005C4B41 File Offset: 0x005C2D41
		private void UpdatePreviewPlate()
		{
			this._previewPlate.UpdateOption((byte)this._optionDifficulty, (byte)this._optionEvil, (byte)this._optionSize);
		}

		// Token: 0x06003CD4 RID: 15572 RVA: 0x005C4B64 File Offset: 0x005C2D64
		private void UpdateSliders()
		{
			GroupOptionButton<UIWorldCreation.WorldSizeId>[] sizeButtons = this._sizeButtons;
			for (int i = 0; i < sizeButtons.Length; i++)
			{
				sizeButtons[i].SetCurrentOption(this._optionSize);
			}
			GroupOptionButton<UIWorldCreation.WorldDifficultyId>[] difficultyButtons = this._difficultyButtons;
			for (int j = 0; j < difficultyButtons.Length; j++)
			{
				difficultyButtons[j].SetCurrentOption(this._optionDifficulty);
			}
			GroupOptionButton<UIWorldCreation.WorldEvilId>[] evilButtons = this._evilButtons;
			for (int k = 0; k < evilButtons.Length; k++)
			{
				evilButtons[k].SetCurrentOption(this._optionEvil);
			}
		}

		// Token: 0x06003CD5 RID: 15573 RVA: 0x005C4BE4 File Offset: 0x005C2DE4
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
			UICharacterNameButton uICharacterNameButton = listeningElement as UICharacterNameButton;
			if (uICharacterNameButton != null)
			{
				localizedText = uICharacterNameButton.Description;
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

		// Token: 0x06003CD6 RID: 15574 RVA: 0x005C4C5D File Offset: 0x005C2E5D
		public void ClearOptionDescription(UIMouseEvent evt, UIElement listeningElement)
		{
			this._descriptionText.SetText(Language.GetText("UI.WorldDescriptionDefault"));
		}

		// Token: 0x06003CD7 RID: 15575 RVA: 0x005C4C74 File Offset: 0x005C2E74
		private void MakeBackAndCreatebuttons(UIElement outerContainer)
		{
			UITextPanel<LocalizedText> uITextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true)
			{
				Width = StyleDimension.FromPixelsAndPercent(-10f, 0.5f),
				Height = StyleDimension.FromPixels(50f),
				VAlign = 1f,
				HAlign = 0f,
				Top = StyleDimension.FromPixels(-45f)
			};
			uITextPanel.OnMouseOver += this.FadedMouseOver;
			uITextPanel.OnMouseOut += this.FadedMouseOut;
			uITextPanel.OnLeftMouseDown += this.Click_GoBack;
			uITextPanel.SetSnapPoint("Back", 0, null, null);
			outerContainer.Append(uITextPanel);
			UITextPanel<LocalizedText> uITextPanel2 = new UITextPanel<LocalizedText>(Language.GetText("UI.Create"), 0.7f, true)
			{
				Width = StyleDimension.FromPixelsAndPercent(-10f, 0.5f),
				Height = StyleDimension.FromPixels(50f),
				VAlign = 1f,
				HAlign = 1f,
				Top = StyleDimension.FromPixels(-45f)
			};
			uITextPanel2.OnMouseOver += this.FadedMouseOver;
			uITextPanel2.OnMouseOut += this.FadedMouseOut;
			uITextPanel2.OnLeftMouseDown += this.Click_NamingAndCreating;
			uITextPanel2.SetSnapPoint("Create", 0, null, null);
			outerContainer.Append(uITextPanel2);
		}

		// Token: 0x06003CD8 RID: 15576 RVA: 0x005C4DF9 File Offset: 0x005C2FF9
		private void Click_GoBack(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.OpenWorldSelectUI();
		}

		// Token: 0x06003CD9 RID: 15577 RVA: 0x005C4E18 File Offset: 0x005C3018
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06003CDA RID: 15578 RVA: 0x005C4E6D File Offset: 0x005C306D
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06003CDB RID: 15579 RVA: 0x005C4EAC File Offset: 0x005C30AC
		private void Click_SetName(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.clrInput();
			UIVirtualKeyboard uIVirtualKeyboard = new UIVirtualKeyboard(Lang.menu[48].Value, "", new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedSettingName), new Action(this.GoBackHere), 0, true);
			uIVirtualKeyboard.SetMaxInputLength(27);
			Main.MenuUI.SetState(uIVirtualKeyboard);
		}

		// Token: 0x06003CDC RID: 15580 RVA: 0x005C4F18 File Offset: 0x005C3118
		private void Click_SetSeed(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.clrInput();
			UIVirtualKeyboard uIVirtualKeyboard = new UIVirtualKeyboard(Language.GetTextValue("UI.EnterSeed"), "", new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedSettingSeed), new Action(this.GoBackHere), 0, true);
			uIVirtualKeyboard.SetMaxInputLength(40);
			Main.MenuUI.SetState(uIVirtualKeyboard);
		}

		// Token: 0x06003CDD RID: 15581 RVA: 0x005C4F84 File Offset: 0x005C3184
		private void Click_NamingAndCreating(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			if (string.IsNullOrEmpty(this._optionwWorldName))
			{
				this._optionwWorldName = "";
				Main.clrInput();
				UIVirtualKeyboard uIVirtualKeyboard = new UIVirtualKeyboard(Lang.menu[48].Value, "", new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedNamingAndCreating), new Action(this.GoBackHere), 0, false);
				uIVirtualKeyboard.SetMaxInputLength(27);
				Main.MenuUI.SetState(uIVirtualKeyboard);
				return;
			}
			this.FinishCreatingWorld();
		}

		// Token: 0x06003CDE RID: 15582 RVA: 0x005C500F File Offset: 0x005C320F
		private void OnFinishedSettingName(string name)
		{
			this._optionwWorldName = name.Trim();
			this.UpdateInputFields();
			this.GoBackHere();
		}

		// Token: 0x06003CDF RID: 15583 RVA: 0x005C502C File Offset: 0x005C322C
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

		// Token: 0x06003CE0 RID: 15584 RVA: 0x005C50A4 File Offset: 0x005C32A4
		private void OnFinishedSettingSeed(string seed)
		{
			this._optionSeed = seed.Trim();
			string processedSeed;
			this.ProcessSeed(out processedSeed);
			this._optionSeed = processedSeed;
			this.UpdateInputFields();
			this.UpdateSliders();
			this.UpdatePreviewPlate();
			this.GoBackHere();
		}

		// Token: 0x06003CE1 RID: 15585 RVA: 0x005C50E4 File Offset: 0x005C32E4
		private void GoBackHere()
		{
			Main.MenuUI.SetState(this);
		}

		// Token: 0x06003CE2 RID: 15586 RVA: 0x005C50F1 File Offset: 0x005C32F1
		private void OnFinishedNamingAndCreating(string name)
		{
			this.OnFinishedSettingName(name);
			this.FinishCreatingWorld();
		}

		// Token: 0x06003CE3 RID: 15587 RVA: 0x005C5100 File Offset: 0x005C3300
		private void FinishCreatingWorld()
		{
			string processedSeed;
			this.ProcessSeed(out processedSeed);
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
			if (processedSeed.Length == 0)
			{
				Main.ActiveWorldFileData.SetSeedToRandom();
			}
			else
			{
				Main.ActiveWorldFileData.SetSeed(processedSeed);
			}
			Main.menuMode = 10;
			WorldGen.CreateNewWorld(null);
		}

		// Token: 0x06003CE4 RID: 15588 RVA: 0x005C5244 File Offset: 0x005C3444
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

		// Token: 0x06003CE5 RID: 15589 RVA: 0x005C5414 File Offset: 0x005C3614
		private void ProcessSeed(out string processedSeed)
		{
			processedSeed = this._optionSeed;
			UIWorldCreation.ProcessSpecialWorldSeeds(processedSeed);
			string[] array = this._optionSeed.Split('.', StringSplitOptions.None);
			if (array.Length != 4)
			{
				return;
			}
			int result;
			if (int.TryParse(array[0], out result))
			{
				switch (result)
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
			if (int.TryParse(array[1], out result))
			{
				switch (result)
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
			if (int.TryParse(array[2], out result))
			{
				if (result != 1)
				{
					if (result == 2)
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

		// Token: 0x06003CE6 RID: 15590 RVA: 0x005C54F4 File Offset: 0x005C36F4
		private void AssignRandomWorldName()
		{
			do
			{
				LocalizedText localizedText = Language.SelectRandom(Lang.CreateDialogFilter("RandomWorldName_Composition."), null);
				LocalizedText localizedText4 = Language.SelectRandom(Lang.CreateDialogFilter("RandomWorldName_Adjective."), null);
				LocalizedText localizedText2 = Language.SelectRandom(Lang.CreateDialogFilter("RandomWorldName_Location."), null);
				LocalizedText localizedText3 = Language.SelectRandom(Lang.CreateDialogFilter("RandomWorldName_Noun."), null);
				var obj = new
				{
					Adjective = localizedText4.Value,
					Location = localizedText2.Value,
					Noun = localizedText3.Value
				};
				this._optionwWorldName = localizedText.FormatWith(obj);
				if (Main.rand.Next(10000) == 0)
				{
					this._optionwWorldName = Language.GetTextValue("SpecialWorldName.TheConstant");
				}
			}
			while (this._optionwWorldName.Length > 27);
		}

		// Token: 0x06003CE7 RID: 15591 RVA: 0x005C559C File Offset: 0x005C379C
		private void AssignRandomWorldSeed()
		{
			this._optionSeed = Main.rand.Next().ToString();
		}

		// Token: 0x06003CE8 RID: 15592 RVA: 0x005C55C1 File Offset: 0x005C37C1
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.SetupGamepadPoints(spriteBatch);
		}

		// Token: 0x06003CE9 RID: 15593 RVA: 0x005C55D4 File Offset: 0x005C37D4
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
			UILinkPoint uILinkPoint = UILinkPointNavigator.Points[num];
			uILinkPoint.Unlink();
			UILinkPoint uILinkPoint2 = uILinkPoint;
			num++;
			UILinkPointNavigator.SetPosition(num, snapPoint2.Position);
			uILinkPoint = UILinkPointNavigator.Points[num];
			uILinkPoint.Unlink();
			UILinkPoint uILinkPoint3 = uILinkPoint;
			num++;
			UILinkPointNavigator.SetPosition(num, snapPoint5.Position);
			uILinkPoint = UILinkPointNavigator.Points[num];
			uILinkPoint.Unlink();
			UILinkPoint uILinkPoint4 = uILinkPoint;
			num++;
			UILinkPointNavigator.SetPosition(num, snapPoint3.Position);
			uILinkPoint = UILinkPointNavigator.Points[num];
			uILinkPoint.Unlink();
			UILinkPoint uILinkPoint5 = uILinkPoint;
			num++;
			UILinkPointNavigator.SetPosition(num, snapPoint6.Position);
			uILinkPoint = UILinkPointNavigator.Points[num];
			uILinkPoint.Unlink();
			UILinkPoint uILinkPoint6 = uILinkPoint;
			num++;
			UILinkPointNavigator.SetPosition(num, snapPoint4.Position);
			uILinkPoint = UILinkPointNavigator.Points[num];
			uILinkPoint.Unlink();
			UILinkPoint uILinkPoint7 = uILinkPoint;
			num++;
			UILinkPoint[] array = new UILinkPoint[snapGroup.Count];
			for (int j = 0; j < snapGroup.Count; j++)
			{
				UILinkPointNavigator.SetPosition(num, snapGroup[j].Position);
				uILinkPoint = UILinkPointNavigator.Points[num];
				uILinkPoint.Unlink();
				array[j] = uILinkPoint;
				num++;
			}
			UILinkPoint[] array2 = new UILinkPoint[snapGroup2.Count];
			for (int k = 0; k < snapGroup2.Count; k++)
			{
				UILinkPointNavigator.SetPosition(num, snapGroup2[k].Position);
				uILinkPoint = UILinkPointNavigator.Points[num];
				uILinkPoint.Unlink();
				array2[k] = uILinkPoint;
				num++;
			}
			UILinkPoint[] array3 = new UILinkPoint[snapGroup3.Count];
			for (int l = 0; l < snapGroup3.Count; l++)
			{
				UILinkPointNavigator.SetPosition(num, snapGroup3[l].Position);
				uILinkPoint = UILinkPointNavigator.Points[num];
				uILinkPoint.Unlink();
				array3[l] = uILinkPoint;
				num++;
			}
			this.LoopHorizontalLineLinks(array);
			this.LoopHorizontalLineLinks(array2);
			this.EstablishUpDownRelationship(array, array2);
			for (int m = 0; m < array.Length; m++)
			{
				array[m].Up = uILinkPoint7.ID;
			}
			this.LoopHorizontalLineLinks(array3);
			this.EstablishUpDownRelationship(array2, array3);
			for (int n = 0; n < array3.Length; n++)
			{
				array3[n].Down = uILinkPoint2.ID;
			}
			array3[array3.Length - 1].Down = uILinkPoint3.ID;
			uILinkPoint3.Up = array3[array3.Length - 1].ID;
			uILinkPoint2.Up = array3[0].ID;
			uILinkPoint3.Left = uILinkPoint2.ID;
			uILinkPoint2.Right = uILinkPoint3.ID;
			uILinkPoint5.Down = uILinkPoint7.ID;
			uILinkPoint5.Left = uILinkPoint4.ID;
			uILinkPoint4.Right = uILinkPoint5.ID;
			uILinkPoint7.Up = uILinkPoint5.ID;
			uILinkPoint7.Down = array[0].ID;
			uILinkPoint7.Left = uILinkPoint6.ID;
			uILinkPoint6.Right = uILinkPoint7.ID;
			uILinkPoint6.Up = uILinkPoint4.ID;
			uILinkPoint6.Down = array[0].ID;
			uILinkPoint4.Down = uILinkPoint6.ID;
		}

		// Token: 0x06003CEA RID: 15594 RVA: 0x005C5A28 File Offset: 0x005C3C28
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

		// Token: 0x06003CEB RID: 15595 RVA: 0x005C5A88 File Offset: 0x005C3C88
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

		// Token: 0x06003CEC RID: 15596 RVA: 0x005C5AF0 File Offset: 0x005C3CF0
		private List<SnapPoint> GetSnapGroup(List<SnapPoint> ptsOnPage, string groupName)
		{
			List<SnapPoint> list = (from a in ptsOnPage
			where a.Name == groupName
			select a).ToList<SnapPoint>();
			list.Sort(new Comparison<SnapPoint>(this.SortPoints));
			return list;
		}

		// Token: 0x06003CED RID: 15597 RVA: 0x005C5B34 File Offset: 0x005C3D34
		private int SortPoints(SnapPoint a, SnapPoint b)
		{
			return a.Id.CompareTo(b.Id);
		}

		// Token: 0x040055D9 RID: 21977
		private UIWorldCreation.WorldSizeId _optionSize;

		// Token: 0x040055DA RID: 21978
		private UIWorldCreation.WorldDifficultyId _optionDifficulty;

		// Token: 0x040055DB RID: 21979
		private UIWorldCreation.WorldEvilId _optionEvil;

		// Token: 0x040055DC RID: 21980
		private string _optionwWorldName;

		// Token: 0x040055DD RID: 21981
		private string _optionSeed;

		// Token: 0x040055DE RID: 21982
		private UICharacterNameButton _namePlate;

		// Token: 0x040055DF RID: 21983
		private UICharacterNameButton _seedPlate;

		// Token: 0x040055E0 RID: 21984
		private UIWorldCreationPreview _previewPlate;

		// Token: 0x040055E1 RID: 21985
		private GroupOptionButton<UIWorldCreation.WorldSizeId>[] _sizeButtons;

		// Token: 0x040055E2 RID: 21986
		private GroupOptionButton<UIWorldCreation.WorldDifficultyId>[] _difficultyButtons;

		// Token: 0x040055E3 RID: 21987
		private GroupOptionButton<UIWorldCreation.WorldEvilId>[] _evilButtons;

		// Token: 0x040055E4 RID: 21988
		private UIText _descriptionText;

		// Token: 0x040055E5 RID: 21989
		public const int MAX_NAME_LENGTH = 27;

		// Token: 0x040055E6 RID: 21990
		public const int MAX_SEED_LENGTH = 40;

		// Token: 0x02000C00 RID: 3072
		private enum WorldSizeId
		{
			// Token: 0x04007821 RID: 30753
			Small,
			// Token: 0x04007822 RID: 30754
			Medium,
			// Token: 0x04007823 RID: 30755
			Large
		}

		// Token: 0x02000C01 RID: 3073
		private enum WorldDifficultyId
		{
			// Token: 0x04007825 RID: 30757
			Normal,
			// Token: 0x04007826 RID: 30758
			Expert,
			// Token: 0x04007827 RID: 30759
			Master,
			// Token: 0x04007828 RID: 30760
			Creative
		}

		// Token: 0x02000C02 RID: 3074
		private enum WorldEvilId
		{
			// Token: 0x0400782A RID: 30762
			Random,
			// Token: 0x0400782B RID: 30763
			Corruption,
			// Token: 0x0400782C RID: 30764
			Crimson
		}
	}
}
