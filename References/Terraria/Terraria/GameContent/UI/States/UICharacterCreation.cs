using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using ReLogic.OS;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Initializers;
using Terraria.IO;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x0200034C RID: 844
	public class UICharacterCreation : UIState
	{
		// Token: 0x06002680 RID: 9856 RVA: 0x00576680 File Offset: 0x00574880
		public UICharacterCreation(Player player)
		{
			this._player = player;
			this._player.difficulty = 0;
			this.BuildPage();
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x005766D0 File Offset: 0x005748D0
		private void BuildPage()
		{
			base.RemoveAllChildren();
			int num = 4;
			UIElement uielement = new UIElement
			{
				Width = StyleDimension.FromPixels(500f),
				Height = StyleDimension.FromPixels((float)(380 + num)),
				Top = StyleDimension.FromPixels(220f),
				HAlign = 0.5f,
				VAlign = 0f
			};
			uielement.SetPadding(0f);
			base.Append(uielement);
			UIPanel uipanel = new UIPanel
			{
				Width = StyleDimension.FromPercent(1f),
				Height = StyleDimension.FromPixels(uielement.Height.Pixels - 150f - (float)num),
				Top = StyleDimension.FromPixels(50f),
				BackgroundColor = new Color(33, 43, 79) * 0.8f
			};
			uipanel.SetPadding(0f);
			uielement.Append(uipanel);
			this.MakeBackAndCreatebuttons(uielement);
			this.MakeCharPreview(uipanel);
			UIElement uielement2 = new UIElement
			{
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(50f, 0f)
			};
			uielement2.SetPadding(0f);
			uielement2.PaddingTop = 4f;
			uielement2.PaddingBottom = 0f;
			uipanel.Append(uielement2);
			UIElement uielement3 = new UIElement
			{
				Top = StyleDimension.FromPixelsAndPercent(uielement2.Height.Pixels + 6f, 0f),
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(uipanel.Height.Pixels - 70f, 0f)
			};
			uielement3.SetPadding(0f);
			uielement3.PaddingTop = 3f;
			uielement3.PaddingBottom = 0f;
			uipanel.Append(uielement3);
			this._topContainer = uielement2;
			this._middleContainer = uielement3;
			this.MakeInfoMenu(uielement3);
			this.MakeHSLMenu(uielement3);
			this.MakeHairsylesMenu(uielement3);
			this.MakeClothStylesMenu(uielement3);
			this.MakeCategoriesBar(uielement2);
			this.Click_CharInfo(null, null);
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x005768E8 File Offset: 0x00574AE8
		private void MakeCharPreview(UIPanel container)
		{
			float num = 70f;
			for (float num2 = 0f; num2 <= 1f; num2 += 1f)
			{
				UICharacter element = new UICharacter(this._player, true, false, 1.5f, false)
				{
					Width = StyleDimension.FromPixels(80f),
					Height = StyleDimension.FromPixelsAndPercent(80f, 0f),
					Top = StyleDimension.FromPixelsAndPercent(-num, 0f),
					VAlign = 0f,
					HAlign = 0.5f
				};
				container.Append(element);
			}
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x0057697C File Offset: 0x00574B7C
		private void MakeHairsylesMenu(UIElement middleInnerPanel)
		{
			Main.Hairstyles.UpdateUnlocks();
			UIElement uielement = new UIElement
			{
				Width = StyleDimension.FromPixelsAndPercent(-10f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				HAlign = 0.5f,
				VAlign = 0.5f,
				Top = StyleDimension.FromPixels(6f)
			};
			middleInnerPanel.Append(uielement);
			uielement.SetPadding(0f);
			UIList uilist = new UIList
			{
				Width = StyleDimension.FromPixelsAndPercent(-18f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(-6f, 1f)
			};
			uilist.SetPadding(4f);
			uielement.Append(uilist);
			UIScrollbar uiscrollbar = new UIScrollbar
			{
				HAlign = 1f,
				Height = StyleDimension.FromPixelsAndPercent(-30f, 1f),
				Top = StyleDimension.FromPixels(10f)
			};
			uiscrollbar.SetView(100f, 1000f);
			uilist.SetScrollbar(uiscrollbar);
			uielement.Append(uiscrollbar);
			int count = Main.Hairstyles.AvailableHairstyles.Count;
			UIElement uielement2 = new UIElement
			{
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent((float)(48 * (count / 10 + ((count % 10 == 0) ? 0 : 1))), 0f)
			};
			uilist.Add(uielement2);
			uielement2.SetPadding(0f);
			for (int i = 0; i < count; i++)
			{
				UIHairStyleButton uihairStyleButton = new UIHairStyleButton(this._player, Main.Hairstyles.AvailableHairstyles[i])
				{
					Left = StyleDimension.FromPixels((float)(i % 10) * 46f + 6f),
					Top = StyleDimension.FromPixels((float)(i / 10) * 48f + 1f)
				};
				uihairStyleButton.SetSnapPoint("Middle", i, null, null);
				uihairStyleButton.SkipRenderingContent(i);
				uielement2.Append(uihairStyleButton);
			}
			this._hairstylesContainer = uielement;
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x00576BA0 File Offset: 0x00574DA0
		private void MakeClothStylesMenu(UIElement middleInnerPanel)
		{
			UIElement uielement = new UIElement
			{
				Width = StyleDimension.FromPixelsAndPercent(-10f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				HAlign = 0.5f,
				VAlign = 0.5f
			};
			middleInnerPanel.Append(uielement);
			uielement.SetPadding(0f);
			int num = 15;
			for (int i = 0; i < this._validClothStyles.Length; i++)
			{
				int num2 = 0;
				if (i >= this._validClothStyles.Length / 2)
				{
					num2 = 20;
				}
				UIClothStyleButton uiclothStyleButton = new UIClothStyleButton(this._player, this._validClothStyles[i])
				{
					Left = StyleDimension.FromPixels((float)i * 46f + (float)num2 + 6f),
					Top = StyleDimension.FromPixels((float)num)
				};
				uiclothStyleButton.OnLeftMouseDown += this.Click_CharClothStyle;
				uiclothStyleButton.SetSnapPoint("Middle", i, null, null);
				uielement.Append(uiclothStyleButton);
			}
			for (int j = 0; j < 2; j++)
			{
				int num3 = 0;
				if (j >= 1)
				{
					num3 = 20;
				}
				UIHorizontalSeparator element = new UIHorizontalSeparator(2, true)
				{
					Left = StyleDimension.FromPixels((float)j * 230f + (float)num3 + 6f),
					Top = StyleDimension.FromPixels((float)(num + 86)),
					Width = StyleDimension.FromPixelsAndPercent(230f, 0f),
					Color = Color.Lerp(Color.White, new Color(63, 65, 151, 255), 0.85f) * 0.9f
				};
				uielement.Append(element);
				UIColoredImageButton uicoloredImageButton = this.CreatePickerWithoutClick(UICharacterCreation.CategoryId.Clothing, "Images/UI/CharCreation/" + ((j == 0) ? "ClothStyleMale" : "ClothStyleFemale"), 0f, 0f);
				uicoloredImageButton.Top = StyleDimension.FromPixelsAndPercent((float)(num + 92), 0f);
				uicoloredImageButton.Left = StyleDimension.FromPixels((float)j * 230f + 92f + (float)num3 + 6f);
				uicoloredImageButton.HAlign = 0f;
				uicoloredImageButton.VAlign = 0f;
				uielement.Append(uicoloredImageButton);
				if (j == 0)
				{
					uicoloredImageButton.OnLeftMouseDown += this.Click_CharGenderMale;
					this._genderMale = uicoloredImageButton;
				}
				else
				{
					uicoloredImageButton.OnLeftMouseDown += this.Click_CharGenderFemale;
					this._genderFemale = uicoloredImageButton;
				}
				uicoloredImageButton.SetSnapPoint("Low", j * 4, null, null);
			}
			UIElement uielement2 = new UIElement
			{
				Width = StyleDimension.FromPixels(130f),
				Height = StyleDimension.FromPixels(50f),
				HAlign = 0.5f,
				VAlign = 1f
			};
			uielement.Append(uielement2);
			UIColoredImageButton uicoloredImageButton2 = new UIColoredImageButton(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Copy", 1), true)
			{
				VAlign = 0.5f,
				HAlign = 0f,
				Left = StyleDimension.FromPixelsAndPercent(0f, 0f)
			};
			uicoloredImageButton2.OnLeftMouseDown += this.Click_CopyPlayerTemplate;
			uielement2.Append(uicoloredImageButton2);
			this._copyTemplateButton = uicoloredImageButton2;
			UIColoredImageButton uicoloredImageButton3 = new UIColoredImageButton(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Paste", 1), true)
			{
				VAlign = 0.5f,
				HAlign = 0.5f
			};
			uicoloredImageButton3.OnLeftMouseDown += this.Click_PastePlayerTemplate;
			uielement2.Append(uicoloredImageButton3);
			this._pasteTemplateButton = uicoloredImageButton3;
			UIColoredImageButton uicoloredImageButton4 = new UIColoredImageButton(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Randomize", 1), true)
			{
				VAlign = 0.5f,
				HAlign = 1f
			};
			uicoloredImageButton4.OnLeftMouseDown += this.Click_RandomizePlayer;
			uielement2.Append(uicoloredImageButton4);
			this._randomizePlayerButton = uicoloredImageButton4;
			uicoloredImageButton2.SetSnapPoint("Low", 1, null, null);
			uicoloredImageButton3.SetSnapPoint("Low", 2, null, null);
			uicoloredImageButton4.SetSnapPoint("Low", 3, null, null);
			this._clothStylesContainer = uielement;
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x00576FF8 File Offset: 0x005751F8
		private void MakeCategoriesBar(UIElement categoryContainer)
		{
			float xPositionStart = -240f;
			float xPositionPerId = 48f;
			this._colorPickers = new UIColoredImageButton[10];
			categoryContainer.Append(this.CreateColorPicker(UICharacterCreation.CategoryId.HairColor, "Images/UI/CharCreation/ColorHair", xPositionStart, xPositionPerId));
			categoryContainer.Append(this.CreateColorPicker(UICharacterCreation.CategoryId.Eye, "Images/UI/CharCreation/ColorEye", xPositionStart, xPositionPerId));
			categoryContainer.Append(this.CreateColorPicker(UICharacterCreation.CategoryId.Skin, "Images/UI/CharCreation/ColorSkin", xPositionStart, xPositionPerId));
			categoryContainer.Append(this.CreateColorPicker(UICharacterCreation.CategoryId.Shirt, "Images/UI/CharCreation/ColorShirt", xPositionStart, xPositionPerId));
			categoryContainer.Append(this.CreateColorPicker(UICharacterCreation.CategoryId.Undershirt, "Images/UI/CharCreation/ColorUndershirt", xPositionStart, xPositionPerId));
			categoryContainer.Append(this.CreateColorPicker(UICharacterCreation.CategoryId.Pants, "Images/UI/CharCreation/ColorPants", xPositionStart, xPositionPerId));
			categoryContainer.Append(this.CreateColorPicker(UICharacterCreation.CategoryId.Shoes, "Images/UI/CharCreation/ColorShoes", xPositionStart, xPositionPerId));
			this._colorPickers[4].SetMiddleTexture(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/ColorEyeBack", 1));
			this._clothingStylesCategoryButton = this.CreatePickerWithoutClick(UICharacterCreation.CategoryId.Clothing, "Images/UI/CharCreation/ClothStyleMale", xPositionStart, xPositionPerId);
			this._clothingStylesCategoryButton.OnLeftMouseDown += this.Click_ClothStyles;
			this._clothingStylesCategoryButton.SetSnapPoint("Top", 1, null, null);
			categoryContainer.Append(this._clothingStylesCategoryButton);
			this._hairStylesCategoryButton = this.CreatePickerWithoutClick(UICharacterCreation.CategoryId.HairStyle, "Images/UI/CharCreation/HairStyle_Hair", xPositionStart, xPositionPerId);
			this._hairStylesCategoryButton.OnLeftMouseDown += this.Click_HairStyles;
			this._hairStylesCategoryButton.SetMiddleTexture(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/HairStyle_Arrow", 1));
			this._hairStylesCategoryButton.SetSnapPoint("Top", 2, null, null);
			categoryContainer.Append(this._hairStylesCategoryButton);
			this._charInfoCategoryButton = this.CreatePickerWithoutClick(UICharacterCreation.CategoryId.CharInfo, "Images/UI/CharCreation/CharInfo", xPositionStart, xPositionPerId);
			this._charInfoCategoryButton.OnLeftMouseDown += this.Click_CharInfo;
			this._charInfoCategoryButton.SetSnapPoint("Top", 0, null, null);
			categoryContainer.Append(this._charInfoCategoryButton);
			this.UpdateColorPickers();
			UIHorizontalSeparator element = new UIHorizontalSeparator(2, true)
			{
				Width = StyleDimension.FromPixelsAndPercent(-20f, 1f),
				Top = StyleDimension.FromPixels(6f),
				VAlign = 1f,
				HAlign = 0.5f,
				Color = Color.Lerp(Color.White, new Color(63, 65, 151, 255), 0.85f) * 0.9f
			};
			categoryContainer.Append(element);
			int num = 21;
			UIText uitext = new UIText(PlayerInput.GenerateInputTag_ForCurrentGamemode(false, "HotbarMinus"), 1f, false)
			{
				Left = new StyleDimension((float)(-(float)num), 0f),
				VAlign = 0.5f,
				Top = new StyleDimension(-4f, 0f)
			};
			categoryContainer.Append(uitext);
			UIText uitext2 = new UIText(PlayerInput.GenerateInputTag_ForCurrentGamemode(false, "HotbarMinus"), 1f, false)
			{
				HAlign = 1f,
				Left = new StyleDimension((float)(12 + num), 0f),
				VAlign = 0.5f,
				Top = new StyleDimension(-4f, 0f)
			};
			categoryContainer.Append(uitext2);
			this._helpGlyphLeft = uitext;
			this._helpGlyphRight = uitext2;
			categoryContainer.OnUpdate += this.UpdateHelpGlyphs;
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x00577354 File Offset: 0x00575554
		private void UpdateHelpGlyphs(UIElement element)
		{
			string text = "";
			string text2 = "";
			if (PlayerInput.UsingGamepad)
			{
				text = PlayerInput.GenerateInputTag_ForCurrentGamemode(false, "HotbarMinus");
				text2 = PlayerInput.GenerateInputTag_ForCurrentGamemode(false, "HotbarPlus");
			}
			this._helpGlyphLeft.SetText(text);
			this._helpGlyphRight.SetText(text2);
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x005773A4 File Offset: 0x005755A4
		private UIColoredImageButton CreateColorPicker(UICharacterCreation.CategoryId id, string texturePath, float xPositionStart, float xPositionPerId)
		{
			UIColoredImageButton uicoloredImageButton = new UIColoredImageButton(Main.Assets.Request<Texture2D>(texturePath, 1), false);
			this._colorPickers[(int)id] = uicoloredImageButton;
			uicoloredImageButton.VAlign = 0f;
			uicoloredImageButton.HAlign = 0f;
			uicoloredImageButton.Left.Set(xPositionStart + (float)id * xPositionPerId, 0.5f);
			uicoloredImageButton.OnLeftMouseDown += this.Click_ColorPicker;
			uicoloredImageButton.SetSnapPoint("Top", (int)id, null, null);
			return uicoloredImageButton;
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x0057742C File Offset: 0x0057562C
		private UIColoredImageButton CreatePickerWithoutClick(UICharacterCreation.CategoryId id, string texturePath, float xPositionStart, float xPositionPerId)
		{
			UIColoredImageButton uicoloredImageButton = new UIColoredImageButton(Main.Assets.Request<Texture2D>(texturePath, 1), false);
			uicoloredImageButton.VAlign = 0f;
			uicoloredImageButton.HAlign = 0f;
			uicoloredImageButton.Left.Set(xPositionStart + (float)id * xPositionPerId, 0.5f);
			return uicoloredImageButton;
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x00577478 File Offset: 0x00575678
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
			UICharacterNameButton uicharacterNameButton = new UICharacterNameButton(Language.GetText("UI.WorldCreationName"), Language.GetText("UI.PlayerEmptyName"), null);
			uicharacterNameButton.Width = StyleDimension.FromPixelsAndPercent(0f, 1f);
			uicharacterNameButton.HAlign = 0.5f;
			uielement.Append(uicharacterNameButton);
			this._charName = uicharacterNameButton;
			uicharacterNameButton.OnLeftMouseDown += this.Click_Naming;
			uicharacterNameButton.SetSnapPoint("Middle", 0, null, null);
			float num = 4f;
			float num2 = 0f;
			float num3 = 0.4f;
			UIElement uielement2 = new UIElement
			{
				HAlign = 0f,
				VAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent(-num, num3),
				Height = StyleDimension.FromPixelsAndPercent(-50f, 1f)
			};
			uielement2.SetPadding(0f);
			uielement.Append(uielement2);
			UISlicedImage uislicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", 1))
			{
				HAlign = 1f,
				VAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent(-num * 2f, 1f - num3),
				Left = StyleDimension.FromPixels(-num),
				Height = StyleDimension.FromPixelsAndPercent(uielement2.Height.Pixels, uielement2.Height.Precent)
			};
			uislicedImage.SetSliceDepths(10);
			uislicedImage.Color = Color.LightGray * 0.7f;
			uielement.Append(uislicedImage);
			float num4 = 4f;
			UIDifficultyButton uidifficultyButton = new UIDifficultyButton(this._player, Lang.menu[26], Lang.menu[31], 0, Color.Cyan)
			{
				HAlign = 0f,
				VAlign = 1f / (num4 - 1f),
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(-num2, 1f / num4)
			};
			UIDifficultyButton uidifficultyButton2 = new UIDifficultyButton(this._player, Lang.menu[25], Lang.menu[30], 1, Main.mcColor)
			{
				HAlign = 0f,
				VAlign = 2f / (num4 - 1f),
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(-num2, 1f / num4)
			};
			UIDifficultyButton uidifficultyButton3 = new UIDifficultyButton(this._player, Lang.menu[24], Lang.menu[29], 2, Main.hcColor)
			{
				HAlign = 0f,
				VAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(-num2, 1f / num4)
			};
			UIDifficultyButton uidifficultyButton4 = new UIDifficultyButton(this._player, Language.GetText("UI.Creative"), Language.GetText("UI.CreativeDescriptionPlayer"), 3, Main.creativeModeColor)
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(-num2, 1f / num4)
			};
			UIText uitext = new UIText(Lang.menu[26], 1f, false)
			{
				HAlign = 0f,
				VAlign = 0.5f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Top = StyleDimension.FromPixelsAndPercent(15f, 0f),
				IsWrapped = true
			};
			uitext.PaddingLeft = 20f;
			uitext.PaddingRight = 20f;
			uislicedImage.Append(uitext);
			uielement2.Append(uidifficultyButton4);
			uielement2.Append(uidifficultyButton);
			uielement2.Append(uidifficultyButton2);
			uielement2.Append(uidifficultyButton3);
			this._infoContainer = uielement;
			this._difficultyDescriptionText = uitext;
			uidifficultyButton4.OnLeftMouseDown += this.UpdateDifficultyDescription;
			uidifficultyButton.OnLeftMouseDown += this.UpdateDifficultyDescription;
			uidifficultyButton2.OnLeftMouseDown += this.UpdateDifficultyDescription;
			uidifficultyButton3.OnLeftMouseDown += this.UpdateDifficultyDescription;
			this.UpdateDifficultyDescription(null, null);
			uidifficultyButton4.SetSnapPoint("Middle", 1, null, null);
			uidifficultyButton.SetSnapPoint("Middle", 2, null, null);
			uidifficultyButton2.SetSnapPoint("Middle", 3, null, null);
			uidifficultyButton3.SetSnapPoint("Middle", 4, null, null);
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x005779BC File Offset: 0x00575BBC
		private void UpdateDifficultyDescription(UIMouseEvent evt, UIElement listeningElement)
		{
			LocalizedText text = Lang.menu[31];
			switch (this._player.difficulty)
			{
			case 0:
				text = Lang.menu[31];
				break;
			case 1:
				text = Lang.menu[30];
				break;
			case 2:
				text = Lang.menu[29];
				break;
			case 3:
				text = Language.GetText("UI.CreativeDescriptionPlayer");
				break;
			}
			this._difficultyDescriptionText.SetText(text);
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x00577A30 File Offset: 0x00575C30
		private void MakeHSLMenu(UIElement parentContainer)
		{
			UIElement uielement = new UIElement
			{
				Width = StyleDimension.FromPixelsAndPercent(220f, 0f),
				Height = StyleDimension.FromPixelsAndPercent(158f, 0f),
				HAlign = 0.5f,
				VAlign = 0f
			};
			uielement.SetPadding(0f);
			parentContainer.Append(uielement);
			UIElement uielement2 = new UIPanel
			{
				Width = StyleDimension.FromPixelsAndPercent(220f, 0f),
				Height = StyleDimension.FromPixelsAndPercent(104f, 0f),
				HAlign = 0.5f,
				VAlign = 0f,
				Top = StyleDimension.FromPixelsAndPercent(10f, 0f)
			};
			uielement2.SetPadding(0f);
			uielement2.PaddingTop = 3f;
			uielement.Append(uielement2);
			uielement2.Append(this.CreateHSLSlider(UICharacterCreation.HSLSliderId.Hue));
			uielement2.Append(this.CreateHSLSlider(UICharacterCreation.HSLSliderId.Saturation));
			uielement2.Append(this.CreateHSLSlider(UICharacterCreation.HSLSliderId.Luminance));
			UIPanel uipanel = new UIPanel
			{
				VAlign = 1f,
				HAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent(100f, 0f),
				Height = StyleDimension.FromPixelsAndPercent(32f, 0f)
			};
			UIText uitext = new UIText("FFFFFF", 1f, false)
			{
				VAlign = 0.5f,
				HAlign = 0.5f
			};
			uipanel.Append(uitext);
			uielement.Append(uipanel);
			UIColoredImageButton uicoloredImageButton = new UIColoredImageButton(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Copy", 1), true)
			{
				VAlign = 1f,
				HAlign = 0f,
				Left = StyleDimension.FromPixelsAndPercent(0f, 0f)
			};
			uicoloredImageButton.OnLeftMouseDown += this.Click_CopyHex;
			uielement.Append(uicoloredImageButton);
			this._copyHexButton = uicoloredImageButton;
			UIColoredImageButton uicoloredImageButton2 = new UIColoredImageButton(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Paste", 1), true)
			{
				VAlign = 1f,
				HAlign = 0f,
				Left = StyleDimension.FromPixelsAndPercent(40f, 0f)
			};
			uicoloredImageButton2.OnLeftMouseDown += this.Click_PasteHex;
			uielement.Append(uicoloredImageButton2);
			this._pasteHexButton = uicoloredImageButton2;
			UIColoredImageButton uicoloredImageButton3 = new UIColoredImageButton(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Randomize", 1), true)
			{
				VAlign = 1f,
				HAlign = 0f,
				Left = StyleDimension.FromPixelsAndPercent(80f, 0f)
			};
			uicoloredImageButton3.OnLeftMouseDown += this.Click_RandomizeSingleColor;
			uielement.Append(uicoloredImageButton3);
			this._randomColorButton = uicoloredImageButton3;
			this._hslContainer = uielement;
			this._hslHexText = uitext;
			uicoloredImageButton.SetSnapPoint("Low", 0, null, null);
			uicoloredImageButton2.SetSnapPoint("Low", 1, null, null);
			uicoloredImageButton3.SetSnapPoint("Low", 2, null, null);
		}

		// Token: 0x0600268C RID: 9868 RVA: 0x00577D54 File Offset: 0x00575F54
		private UIColoredSlider CreateHSLSlider(UICharacterCreation.HSLSliderId id)
		{
			UIColoredSlider uicoloredSlider = this.CreateHSLSliderButtonBase(id);
			uicoloredSlider.VAlign = 0f;
			uicoloredSlider.HAlign = 0f;
			uicoloredSlider.Width = StyleDimension.FromPixelsAndPercent(-10f, 1f);
			uicoloredSlider.Top.Set((float)((UICharacterCreation.HSLSliderId)30 * id), 0f);
			uicoloredSlider.OnLeftMouseDown += this.Click_ColorPicker;
			uicoloredSlider.SetSnapPoint("Middle", (int)id, null, new Vector2?(new Vector2(0f, 20f)));
			return uicoloredSlider;
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x00577DE4 File Offset: 0x00575FE4
		private UIColoredSlider CreateHSLSliderButtonBase(UICharacterCreation.HSLSliderId id)
		{
			UIColoredSlider result;
			if (id != UICharacterCreation.HSLSliderId.Saturation)
			{
				if (id != UICharacterCreation.HSLSliderId.Luminance)
				{
					result = new UIColoredSlider(LocalizedText.Empty, () => this.GetHSLSliderPosition(UICharacterCreation.HSLSliderId.Hue), delegate(float x)
					{
						this.UpdateHSLValue(UICharacterCreation.HSLSliderId.Hue, x);
					}, new Action(this.UpdateHSL_H), (float x) => this.GetHSLSliderColorAt(UICharacterCreation.HSLSliderId.Hue, x), Color.Transparent);
				}
				else
				{
					result = new UIColoredSlider(LocalizedText.Empty, () => this.GetHSLSliderPosition(UICharacterCreation.HSLSliderId.Luminance), delegate(float x)
					{
						this.UpdateHSLValue(UICharacterCreation.HSLSliderId.Luminance, x);
					}, new Action(this.UpdateHSL_L), (float x) => this.GetHSLSliderColorAt(UICharacterCreation.HSLSliderId.Luminance, x), Color.Transparent);
				}
			}
			else
			{
				result = new UIColoredSlider(LocalizedText.Empty, () => this.GetHSLSliderPosition(UICharacterCreation.HSLSliderId.Saturation), delegate(float x)
				{
					this.UpdateHSLValue(UICharacterCreation.HSLSliderId.Saturation, x);
				}, new Action(this.UpdateHSL_S), (float x) => this.GetHSLSliderColorAt(UICharacterCreation.HSLSliderId.Saturation, x), Color.Transparent);
			}
			return result;
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x00577EC8 File Offset: 0x005760C8
		private void UpdateHSL_H()
		{
			float value = UILinksInitializer.HandleSliderHorizontalInput(this._currentColorHSL.X, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
			this.UpdateHSLValue(UICharacterCreation.HSLSliderId.Hue, value);
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x00577F08 File Offset: 0x00576108
		private void UpdateHSL_S()
		{
			float value = UILinksInitializer.HandleSliderHorizontalInput(this._currentColorHSL.Y, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
			this.UpdateHSLValue(UICharacterCreation.HSLSliderId.Saturation, value);
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x00577F48 File Offset: 0x00576148
		private void UpdateHSL_L()
		{
			float value = UILinksInitializer.HandleSliderHorizontalInput(this._currentColorHSL.Z, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
			this.UpdateHSLValue(UICharacterCreation.HSLSliderId.Luminance, value);
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x00577F87 File Offset: 0x00576187
		private float GetHSLSliderPosition(UICharacterCreation.HSLSliderId id)
		{
			switch (id)
			{
			case UICharacterCreation.HSLSliderId.Hue:
				return this._currentColorHSL.X;
			case UICharacterCreation.HSLSliderId.Saturation:
				return this._currentColorHSL.Y;
			case UICharacterCreation.HSLSliderId.Luminance:
				return this._currentColorHSL.Z;
			default:
				return 1f;
			}
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x00577FC8 File Offset: 0x005761C8
		private void UpdateHSLValue(UICharacterCreation.HSLSliderId id, float value)
		{
			switch (id)
			{
			case UICharacterCreation.HSLSliderId.Hue:
				this._currentColorHSL.X = value;
				break;
			case UICharacterCreation.HSLSliderId.Saturation:
				this._currentColorHSL.Y = value;
				break;
			case UICharacterCreation.HSLSliderId.Luminance:
				this._currentColorHSL.Z = value;
				break;
			}
			Color color = UICharacterCreation.ScaledHslToRgb(this._currentColorHSL.X, this._currentColorHSL.Y, this._currentColorHSL.Z);
			this.ApplyPendingColor(color);
			UIColoredImageButton uicoloredImageButton = this._colorPickers[(int)this._selectedPicker];
			if (uicoloredImageButton != null)
			{
				uicoloredImageButton.SetColor(color);
			}
			if (this._selectedPicker == UICharacterCreation.CategoryId.HairColor)
			{
				this._hairStylesCategoryButton.SetColor(color);
			}
			this.UpdateHexText(color);
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x00578074 File Offset: 0x00576274
		private Color GetHSLSliderColorAt(UICharacterCreation.HSLSliderId id, float pointAt)
		{
			switch (id)
			{
			case UICharacterCreation.HSLSliderId.Hue:
				return UICharacterCreation.ScaledHslToRgb(pointAt, 1f, 0.5f);
			case UICharacterCreation.HSLSliderId.Saturation:
				return UICharacterCreation.ScaledHslToRgb(this._currentColorHSL.X, pointAt, this._currentColorHSL.Z);
			case UICharacterCreation.HSLSliderId.Luminance:
				return UICharacterCreation.ScaledHslToRgb(this._currentColorHSL.X, this._currentColorHSL.Y, pointAt);
			default:
				return Color.White;
			}
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x005780E8 File Offset: 0x005762E8
		private void ApplyPendingColor(Color pendingColor)
		{
			switch (this._selectedPicker)
			{
			case UICharacterCreation.CategoryId.HairColor:
				this._player.hairColor = pendingColor;
				return;
			case UICharacterCreation.CategoryId.Eye:
				this._player.eyeColor = pendingColor;
				return;
			case UICharacterCreation.CategoryId.Skin:
				this._player.skinColor = pendingColor;
				return;
			case UICharacterCreation.CategoryId.Shirt:
				this._player.shirtColor = pendingColor;
				return;
			case UICharacterCreation.CategoryId.Undershirt:
				this._player.underShirtColor = pendingColor;
				return;
			case UICharacterCreation.CategoryId.Pants:
				this._player.pantsColor = pendingColor;
				return;
			case UICharacterCreation.CategoryId.Shoes:
				this._player.shoeColor = pendingColor;
				return;
			default:
				return;
			}
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x0057817B File Offset: 0x0057637B
		private void UpdateHexText(Color pendingColor)
		{
			this._hslHexText.SetText(UICharacterCreation.GetHexText(pendingColor));
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x0057818E File Offset: 0x0057638E
		private static string GetHexText(Color pendingColor)
		{
			return "#" + pendingColor.Hex3().ToUpper();
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x005781A8 File Offset: 0x005763A8
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

		// Token: 0x06002698 RID: 9880 RVA: 0x0057832D File Offset: 0x0057652D
		private void Click_GoBack(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.OpenCharacterSelectUI();
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x0057834C File Offset: 0x0057654C
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x0056C33D File Offset: 0x0056A53D
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x005783A4 File Offset: 0x005765A4
		private void Click_ColorPicker(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			for (int i = 0; i < this._colorPickers.Length; i++)
			{
				if (this._colorPickers[i] == evt.Target)
				{
					this.SelectColorPicker((UICharacterCreation.CategoryId)i);
					return;
				}
			}
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x005783F4 File Offset: 0x005765F4
		private void Click_ClothStyles(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this.UnselectAllCategories();
			this._selectedPicker = UICharacterCreation.CategoryId.Clothing;
			this._middleContainer.Append(this._clothStylesContainer);
			this._clothingStylesCategoryButton.SetSelected(true);
			this.UpdateSelectedGender();
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x00578448 File Offset: 0x00576648
		private void Click_HairStyles(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this.UnselectAllCategories();
			this._selectedPicker = UICharacterCreation.CategoryId.HairStyle;
			this._middleContainer.Append(this._hairstylesContainer);
			this._hairStylesCategoryButton.SetSelected(true);
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x00578494 File Offset: 0x00576694
		private void Click_CharInfo(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this.UnselectAllCategories();
			this._selectedPicker = UICharacterCreation.CategoryId.CharInfo;
			this._middleContainer.Append(this._infoContainer);
			this._charInfoCategoryButton.SetSelected(true);
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x005784E0 File Offset: 0x005766E0
		private void Click_CharClothStyle(UIMouseEvent evt, UIElement listeningElement)
		{
			this._clothingStylesCategoryButton.SetImageWithoutSettingSize(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/" + (this._player.Male ? "ClothStyleMale" : "ClothStyleFemale"), 1));
			this.UpdateSelectedGender();
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x0057852C File Offset: 0x0057672C
		private void Click_CharGenderMale(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this._player.Male = true;
			this.Click_CharClothStyle(evt, listeningElement);
			this.UpdateSelectedGender();
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x0057855D File Offset: 0x0057675D
		private void Click_CharGenderFemale(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this._player.Male = false;
			this.Click_CharClothStyle(evt, listeningElement);
			this.UpdateSelectedGender();
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x0057858E File Offset: 0x0057678E
		private void UpdateSelectedGender()
		{
			this._genderMale.SetSelected(this._player.Male);
			this._genderFemale.SetSelected(!this._player.Male);
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x005785BF File Offset: 0x005767BF
		private void Click_CopyHex(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			Platform.Get<IClipboard>().Value = this._hslHexText.Text;
		}

		// Token: 0x060026A4 RID: 9892 RVA: 0x005785EC File Offset: 0x005767EC
		private void Click_PasteHex(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			string value = Platform.Get<IClipboard>().Value;
			Vector3 vector;
			if (this.GetHexColor(value, out vector))
			{
				this.ApplyPendingColor(UICharacterCreation.ScaledHslToRgb(vector.X, vector.Y, vector.Z));
				this._currentColorHSL = vector;
				this.UpdateHexText(UICharacterCreation.ScaledHslToRgb(vector.X, vector.Y, vector.Z));
				this.UpdateColorPickers();
			}
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x0057866C File Offset: 0x0057686C
		private void Click_CopyPlayerTemplate(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			string value = JsonConvert.SerializeObject(new Dictionary<string, object>
			{
				{
					"version",
					1
				},
				{
					"hairStyle",
					this._player.hair
				},
				{
					"clothingStyle",
					this._player.skinVariant
				},
				{
					"hairColor",
					UICharacterCreation.GetHexText(this._player.hairColor)
				},
				{
					"eyeColor",
					UICharacterCreation.GetHexText(this._player.eyeColor)
				},
				{
					"skinColor",
					UICharacterCreation.GetHexText(this._player.skinColor)
				},
				{
					"shirtColor",
					UICharacterCreation.GetHexText(this._player.shirtColor)
				},
				{
					"underShirtColor",
					UICharacterCreation.GetHexText(this._player.underShirtColor)
				},
				{
					"pantsColor",
					UICharacterCreation.GetHexText(this._player.pantsColor)
				},
				{
					"shoeColor",
					UICharacterCreation.GetHexText(this._player.shoeColor)
				}
			}, new JsonSerializerSettings
			{
				TypeNameHandling = 4,
				MetadataPropertyHandling = 1,
				Formatting = 1
			});
			PlayerInput.PrettyPrintProfiles(ref value);
			Platform.Get<IClipboard>().Value = value;
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x005787CC File Offset: 0x005769CC
		private void Click_PastePlayerTemplate(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			try
			{
				string text = Platform.Get<IClipboard>().Value;
				int num = text.IndexOf("{");
				if (num != -1)
				{
					text = text.Substring(num);
					int num2 = text.LastIndexOf("}");
					if (num2 != -1)
					{
						text = text.Substring(0, num2 + 1);
						Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(text);
						if (dictionary != null)
						{
							Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
							foreach (KeyValuePair<string, object> keyValuePair in dictionary)
							{
								dictionary2[keyValuePair.Key.ToLower()] = keyValuePair.Value;
							}
							object obj;
							if (dictionary2.TryGetValue("version", out obj))
							{
								long num3 = (long)obj;
							}
							if (dictionary2.TryGetValue("hairstyle", out obj))
							{
								int num4 = (int)((long)obj);
								if (Main.Hairstyles.AvailableHairstyles.Contains(num4))
								{
									this._player.hair = num4;
								}
							}
							if (dictionary2.TryGetValue("clothingstyle", out obj))
							{
								int num5 = (int)((long)obj);
								if (this._validClothStyles.Contains(num5))
								{
									this._player.skinVariant = num5;
								}
							}
							Vector3 hsl;
							if (dictionary2.TryGetValue("haircolor", out obj) && this.GetHexColor((string)obj, out hsl))
							{
								this._player.hairColor = UICharacterCreation.ScaledHslToRgb(hsl);
							}
							if (dictionary2.TryGetValue("eyecolor", out obj) && this.GetHexColor((string)obj, out hsl))
							{
								this._player.eyeColor = UICharacterCreation.ScaledHslToRgb(hsl);
							}
							if (dictionary2.TryGetValue("skincolor", out obj) && this.GetHexColor((string)obj, out hsl))
							{
								this._player.skinColor = UICharacterCreation.ScaledHslToRgb(hsl);
							}
							if (dictionary2.TryGetValue("shirtcolor", out obj) && this.GetHexColor((string)obj, out hsl))
							{
								this._player.shirtColor = UICharacterCreation.ScaledHslToRgb(hsl);
							}
							if (dictionary2.TryGetValue("undershirtcolor", out obj) && this.GetHexColor((string)obj, out hsl))
							{
								this._player.underShirtColor = UICharacterCreation.ScaledHslToRgb(hsl);
							}
							if (dictionary2.TryGetValue("pantscolor", out obj) && this.GetHexColor((string)obj, out hsl))
							{
								this._player.pantsColor = UICharacterCreation.ScaledHslToRgb(hsl);
							}
							if (dictionary2.TryGetValue("shoecolor", out obj) && this.GetHexColor((string)obj, out hsl))
							{
								this._player.shoeColor = UICharacterCreation.ScaledHslToRgb(hsl);
							}
							this.Click_CharClothStyle(null, null);
							this.UpdateColorPickers();
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x00578AC4 File Offset: 0x00576CC4
		private void Click_RandomizePlayer(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			Player player = this._player;
			int index = Main.rand.Next(Main.Hairstyles.AvailableHairstyles.Count);
			player.hair = Main.Hairstyles.AvailableHairstyles[index];
			player.eyeColor = UICharacterCreation.ScaledHslToRgb(UICharacterCreation.GetRandomColorVector());
			while ((int)(player.eyeColor.R + player.eyeColor.G + player.eyeColor.B) > 300)
			{
				player.eyeColor = UICharacterCreation.ScaledHslToRgb(UICharacterCreation.GetRandomColorVector());
			}
			float num = (float)Main.rand.Next(60, 120) * 0.01f;
			if (num > 1f)
			{
				num = 1f;
			}
			player.skinColor.R = (byte)((float)Main.rand.Next(240, 255) * num);
			player.skinColor.G = (byte)((float)Main.rand.Next(110, 140) * num);
			player.skinColor.B = (byte)((float)Main.rand.Next(75, 110) * num);
			player.hairColor = UICharacterCreation.ScaledHslToRgb(UICharacterCreation.GetRandomColorVector());
			player.shirtColor = UICharacterCreation.ScaledHslToRgb(UICharacterCreation.GetRandomColorVector());
			player.underShirtColor = UICharacterCreation.ScaledHslToRgb(UICharacterCreation.GetRandomColorVector());
			player.pantsColor = UICharacterCreation.ScaledHslToRgb(UICharacterCreation.GetRandomColorVector());
			player.shoeColor = UICharacterCreation.ScaledHslToRgb(UICharacterCreation.GetRandomColorVector());
			player.skinVariant = this._validClothStyles[Main.rand.Next(this._validClothStyles.Length)];
			int num2 = player.hair + 1;
			if (num2 <= 135)
			{
				if (num2 <= 124)
				{
					switch (num2)
					{
					case 5:
					case 6:
					case 7:
					case 10:
					case 12:
					case 19:
					case 22:
					case 23:
					case 26:
					case 27:
					case 30:
					case 33:
					case 34:
					case 35:
					case 37:
					case 38:
					case 39:
					case 40:
					case 41:
					case 44:
					case 45:
					case 46:
					case 47:
					case 48:
					case 49:
					case 51:
					case 56:
					case 65:
					case 66:
					case 67:
					case 68:
					case 69:
					case 70:
					case 71:
					case 72:
					case 73:
					case 74:
					case 79:
					case 80:
					case 81:
					case 82:
					case 84:
					case 85:
					case 86:
					case 87:
					case 88:
					case 90:
					case 91:
					case 92:
					case 93:
					case 95:
					case 96:
					case 98:
					case 100:
					case 102:
					case 104:
					case 107:
					case 108:
					case 113:
						break;
					case 8:
					case 9:
					case 11:
					case 13:
					case 14:
					case 15:
					case 16:
					case 17:
					case 18:
					case 20:
					case 21:
					case 24:
					case 25:
					case 28:
					case 29:
					case 31:
					case 32:
					case 36:
					case 42:
					case 43:
					case 50:
					case 52:
					case 53:
					case 54:
					case 55:
					case 57:
					case 58:
					case 59:
					case 60:
					case 61:
					case 62:
					case 63:
					case 64:
					case 75:
					case 76:
					case 77:
					case 78:
					case 83:
					case 89:
					case 94:
					case 97:
					case 99:
					case 101:
					case 103:
					case 105:
					case 106:
					case 109:
					case 110:
					case 111:
					case 112:
						goto IL_3B0;
					default:
						if (num2 != 124)
						{
							goto IL_3B0;
						}
						break;
					}
				}
				else if (num2 != 126 && num2 - 133 > 2)
				{
					goto IL_3B0;
				}
			}
			else if (num2 <= 147)
			{
				if (num2 != 144 && num2 - 146 > 1)
				{
					goto IL_3B0;
				}
			}
			else if (num2 != 163 && num2 != 165)
			{
				goto IL_3B0;
			}
			player.Male = false;
			goto IL_3B7;
			IL_3B0:
			player.Male = true;
			IL_3B7:
			this.Click_CharClothStyle(null, null);
			this.UpdateSelectedGender();
			this.UpdateColorPickers();
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x00578E9C File Offset: 0x0057709C
		private void Click_Naming(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			this._player.name = "";
			Main.clrInput();
			UIVirtualKeyboard uivirtualKeyboard = new UIVirtualKeyboard(Lang.menu[45].Value, "", new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedNaming), new Action(this.OnCanceledNaming), 0, true);
			uivirtualKeyboard.SetMaxInputLength(20);
			Main.MenuUI.SetState(uivirtualKeyboard);
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x00578F18 File Offset: 0x00577118
		private void Click_NamingAndCreating(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			if (string.IsNullOrEmpty(this._player.name))
			{
				this._player.name = "";
				Main.clrInput();
				UIVirtualKeyboard uivirtualKeyboard = new UIVirtualKeyboard(Lang.menu[45].Value, "", new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedNamingAndCreating), new Action(this.OnCanceledNaming), 0, false);
				uivirtualKeyboard.SetMaxInputLength(20);
				Main.MenuUI.SetState(uivirtualKeyboard);
				return;
			}
			this.FinishCreatingCharacter();
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x00578FAD File Offset: 0x005771AD
		private void OnFinishedNaming(string name)
		{
			this._player.name = name.Trim();
			Main.MenuUI.SetState(this);
			this._charName.SetContents(this._player.name);
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x00575BBC File Offset: 0x00573DBC
		private void OnCanceledNaming()
		{
			Main.MenuUI.SetState(this);
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x00578FE1 File Offset: 0x005771E1
		private void OnFinishedNamingAndCreating(string name)
		{
			this._player.name = name.Trim();
			Main.MenuUI.SetState(this);
			this._charName.SetContents(this._player.name);
			this.FinishCreatingCharacter();
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x0057901B File Offset: 0x0057721B
		private void FinishCreatingCharacter()
		{
			this.SetupPlayerStatsAndInventoryBasedOnDifficulty();
			PlayerFileData.CreateAndSave(this._player);
			Main.LoadPlayers();
			Main.menuMode = 1;
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x0057903C File Offset: 0x0057723C
		private void SetupPlayerStatsAndInventoryBasedOnDifficulty()
		{
			int num = 0;
			byte difficulty = this._player.difficulty;
			if (difficulty == 3)
			{
				this._player.statLife = (this._player.statLifeMax = 100);
				this._player.statMana = (this._player.statManaMax = 20);
				this._player.inventory[num].SetDefaults(6);
				this._player.inventory[num++].Prefix(-1);
				this._player.inventory[num].SetDefaults(1);
				this._player.inventory[num++].Prefix(-1);
				this._player.inventory[num].SetDefaults(10);
				this._player.inventory[num++].Prefix(-1);
				this._player.inventory[num].SetDefaults(7);
				this._player.inventory[num++].Prefix(-1);
				this._player.inventory[num].SetDefaults(4281);
				this._player.inventory[num++].Prefix(-1);
				this._player.inventory[num].SetDefaults(8);
				this._player.inventory[num++].stack = 100;
				this._player.inventory[num].SetDefaults(965);
				this._player.inventory[num++].stack = 100;
				this._player.inventory[num++].SetDefaults(50);
				this._player.inventory[num++].SetDefaults(84);
				this._player.armor[3].SetDefaults(4978);
				this._player.armor[3].Prefix(-1);
				if (this._player.name == "Wolf Pet" || this._player.name == "Wolfpet")
				{
					this._player.miscEquips[3].SetDefaults(5130);
				}
				this._player.AddBuff(216, 3600, true, false);
			}
			else
			{
				this._player.inventory[num].SetDefaults(3507);
				this._player.inventory[num++].Prefix(-1);
				this._player.inventory[num].SetDefaults(3509);
				this._player.inventory[num++].Prefix(-1);
				this._player.inventory[num].SetDefaults(3506);
				this._player.inventory[num++].Prefix(-1);
			}
			if (Main.runningCollectorsEdition)
			{
				this._player.inventory[num++].SetDefaults(603);
			}
			this._player.savedPerPlayerFieldsThatArentInThePlayerClass = new Player.SavedPlayerDataWithAnnoyingRules();
			CreativePowerManager.Instance.ResetDataForNewPlayer(this._player);
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x00579358 File Offset: 0x00577558
		private bool GetHexColor(string hexString, out Vector3 hsl)
		{
			if (hexString.StartsWith("#"))
			{
				hexString = hexString.Substring(1);
			}
			uint num;
			if (hexString.Length <= 6 && uint.TryParse(hexString, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out num))
			{
				uint b = num & 255U;
				uint g = num >> 8 & 255U;
				uint r = num >> 16 & 255U;
				hsl = UICharacterCreation.RgbToScaledHsl(new Color((int)r, (int)g, (int)b));
				return true;
			}
			hsl = Vector3.Zero;
			return false;
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x005793D8 File Offset: 0x005775D8
		private void Click_RandomizeSingleColor(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			Vector3 randomColorVector = UICharacterCreation.GetRandomColorVector();
			this.ApplyPendingColor(UICharacterCreation.ScaledHslToRgb(randomColorVector.X, randomColorVector.Y, randomColorVector.Z));
			this._currentColorHSL = randomColorVector;
			this.UpdateHexText(UICharacterCreation.ScaledHslToRgb(randomColorVector.X, randomColorVector.Y, randomColorVector.Z));
			this.UpdateColorPickers();
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x00579447 File Offset: 0x00577647
		private static Vector3 GetRandomColorVector()
		{
			return new Vector3(Main.rand.NextFloat(), Main.rand.NextFloat(), Main.rand.NextFloat());
		}

		// Token: 0x060026B2 RID: 9906 RVA: 0x0057946C File Offset: 0x0057766C
		private void UnselectAllCategories()
		{
			foreach (UIColoredImageButton uicoloredImageButton in this._colorPickers)
			{
				if (uicoloredImageButton != null)
				{
					uicoloredImageButton.SetSelected(false);
				}
			}
			this._clothingStylesCategoryButton.SetSelected(false);
			this._hairStylesCategoryButton.SetSelected(false);
			this._charInfoCategoryButton.SetSelected(false);
			this._hslContainer.Remove();
			this._hairstylesContainer.Remove();
			this._clothStylesContainer.Remove();
			this._infoContainer.Remove();
		}

		// Token: 0x060026B3 RID: 9907 RVA: 0x005794EC File Offset: 0x005776EC
		private void SelectColorPicker(UICharacterCreation.CategoryId selection)
		{
			this._selectedPicker = selection;
			if (selection == UICharacterCreation.CategoryId.CharInfo)
			{
				this.Click_CharInfo(null, null);
				return;
			}
			if (selection == UICharacterCreation.CategoryId.Clothing)
			{
				this.Click_ClothStyles(null, null);
				return;
			}
			if (selection == UICharacterCreation.CategoryId.HairStyle)
			{
				this.Click_HairStyles(null, null);
				return;
			}
			this.UnselectAllCategories();
			this._middleContainer.Append(this._hslContainer);
			for (int i = 0; i < this._colorPickers.Length; i++)
			{
				if (this._colorPickers[i] != null)
				{
					this._colorPickers[i].SetSelected(i == (int)selection);
				}
			}
			Vector3 vector = Vector3.One;
			switch (this._selectedPicker)
			{
			case UICharacterCreation.CategoryId.HairColor:
				vector = UICharacterCreation.RgbToScaledHsl(this._player.hairColor);
				break;
			case UICharacterCreation.CategoryId.Eye:
				vector = UICharacterCreation.RgbToScaledHsl(this._player.eyeColor);
				break;
			case UICharacterCreation.CategoryId.Skin:
				vector = UICharacterCreation.RgbToScaledHsl(this._player.skinColor);
				break;
			case UICharacterCreation.CategoryId.Shirt:
				vector = UICharacterCreation.RgbToScaledHsl(this._player.shirtColor);
				break;
			case UICharacterCreation.CategoryId.Undershirt:
				vector = UICharacterCreation.RgbToScaledHsl(this._player.underShirtColor);
				break;
			case UICharacterCreation.CategoryId.Pants:
				vector = UICharacterCreation.RgbToScaledHsl(this._player.pantsColor);
				break;
			case UICharacterCreation.CategoryId.Shoes:
				vector = UICharacterCreation.RgbToScaledHsl(this._player.shoeColor);
				break;
			}
			this._currentColorHSL = vector;
			this.UpdateHexText(UICharacterCreation.ScaledHslToRgb(vector.X, vector.Y, vector.Z));
		}

		// Token: 0x060026B4 RID: 9908 RVA: 0x00579648 File Offset: 0x00577848
		private void UpdateColorPickers()
		{
			UICharacterCreation.CategoryId selectedPicker = this._selectedPicker;
			this._colorPickers[3].SetColor(this._player.hairColor);
			this._hairStylesCategoryButton.SetColor(this._player.hairColor);
			this._colorPickers[4].SetColor(this._player.eyeColor);
			this._colorPickers[5].SetColor(this._player.skinColor);
			this._colorPickers[6].SetColor(this._player.shirtColor);
			this._colorPickers[7].SetColor(this._player.underShirtColor);
			this._colorPickers[8].SetColor(this._player.pantsColor);
			this._colorPickers[9].SetColor(this._player.shoeColor);
		}

		// Token: 0x060026B5 RID: 9909 RVA: 0x0057971C File Offset: 0x0057791C
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			string text = null;
			if (this._copyHexButton.IsMouseHovering)
			{
				text = Language.GetTextValue("UI.CopyColorToClipboard");
			}
			if (this._pasteHexButton.IsMouseHovering)
			{
				text = Language.GetTextValue("UI.PasteColorFromClipboard");
			}
			if (this._randomColorButton.IsMouseHovering)
			{
				text = Language.GetTextValue("UI.RandomizeColor");
			}
			if (this._copyTemplateButton.IsMouseHovering)
			{
				text = Language.GetTextValue("UI.CopyPlayerToClipboard");
			}
			if (this._pasteTemplateButton.IsMouseHovering)
			{
				text = Language.GetTextValue("UI.PastePlayerFromClipboard");
			}
			if (this._randomizePlayerButton.IsMouseHovering)
			{
				text = Language.GetTextValue("UI.RandomizePlayer");
			}
			if (text != null)
			{
				float x = FontAssets.MouseText.Value.MeasureString(text).X;
				Vector2 vector = new Vector2((float)Main.mouseX, (float)Main.mouseY) + new Vector2(16f);
				if (vector.Y > (float)(Main.screenHeight - 30))
				{
					vector.Y = (float)(Main.screenHeight - 30);
				}
				if (vector.X > (float)Main.screenWidth - x)
				{
					vector.X = (float)(Main.screenWidth - 460);
				}
				Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, text, vector.X, vector.Y, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, Vector2.Zero, 1f);
			}
			this.SetupGamepadPoints(spriteBatch);
		}

		// Token: 0x060026B6 RID: 9910 RVA: 0x00579890 File Offset: 0x00577A90
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 1;
			int num = 3000;
			int num2 = num + 20;
			int num3 = num;
			List<SnapPoint> snapPoints = this.GetSnapPoints();
			SnapPoint snapPoint = snapPoints.First((SnapPoint a) => a.Name == "Back");
			SnapPoint snapPoint2 = snapPoints.First((SnapPoint a) => a.Name == "Create");
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[num3];
			uilinkPoint.Unlink();
			UILinkPointNavigator.SetPosition(num3, snapPoint.Position);
			num3++;
			UILinkPoint uilinkPoint2 = UILinkPointNavigator.Points[num3];
			uilinkPoint2.Unlink();
			UILinkPointNavigator.SetPosition(num3, snapPoint2.Position);
			num3++;
			uilinkPoint.Right = uilinkPoint2.ID;
			uilinkPoint2.Left = uilinkPoint.ID;
			this._foundPoints.Clear();
			this._foundPoints.Add(uilinkPoint.ID);
			this._foundPoints.Add(uilinkPoint2.ID);
			List<SnapPoint> list = (from a in snapPoints
			where a.Name == "Top"
			select a).ToList<SnapPoint>();
			list.Sort(new Comparison<SnapPoint>(this.SortPoints));
			for (int i = 0; i < list.Count; i++)
			{
				UILinkPoint uilinkPoint3 = UILinkPointNavigator.Points[num3];
				uilinkPoint3.Unlink();
				UILinkPointNavigator.SetPosition(num3, list[i].Position);
				uilinkPoint3.Left = num3 - 1;
				uilinkPoint3.Right = num3 + 1;
				uilinkPoint3.Down = num2;
				if (i == 0)
				{
					uilinkPoint3.Left = -3;
				}
				if (i == list.Count - 1)
				{
					uilinkPoint3.Right = -4;
				}
				if (this._selectedPicker == UICharacterCreation.CategoryId.HairStyle || this._selectedPicker == UICharacterCreation.CategoryId.Clothing)
				{
					uilinkPoint3.Down = num2 + i;
				}
				this._foundPoints.Add(num3);
				num3++;
			}
			List<SnapPoint> list2 = (from a in snapPoints
			where a.Name == "Middle"
			select a).ToList<SnapPoint>();
			list2.Sort(new Comparison<SnapPoint>(this.SortPoints));
			num3 = num2;
			switch (this._selectedPicker)
			{
			case UICharacterCreation.CategoryId.CharInfo:
				for (int j = 0; j < list2.Count; j++)
				{
					UILinkPoint andSet = this.GetAndSet(num3, list2[j]);
					andSet.Up = andSet.ID - 1;
					andSet.Down = andSet.ID + 1;
					if (j == 0)
					{
						andSet.Up = num + 2;
					}
					if (j == list2.Count - 1)
					{
						andSet.Down = uilinkPoint.ID;
						uilinkPoint.Up = andSet.ID;
						uilinkPoint2.Up = andSet.ID;
					}
					this._foundPoints.Add(num3);
					num3++;
				}
				break;
			case UICharacterCreation.CategoryId.Clothing:
			{
				List<SnapPoint> list3 = (from a in snapPoints
				where a.Name == "Low"
				select a).ToList<SnapPoint>();
				list3.Sort(new Comparison<SnapPoint>(this.SortPoints));
				int down = -2;
				int down2 = -2;
				num3 = num2 + 20;
				for (int k = 0; k < list3.Count; k++)
				{
					UILinkPoint andSet2 = this.GetAndSet(num3, list3[k]);
					andSet2.Up = num2 + k + 2;
					andSet2.Down = uilinkPoint.ID;
					if (k >= 3)
					{
						andSet2.Up++;
						andSet2.Down = uilinkPoint2.ID;
					}
					andSet2.Left = andSet2.ID - 1;
					andSet2.Right = andSet2.ID + 1;
					if (k == 0)
					{
						down = andSet2.ID;
						andSet2.Left = andSet2.ID + 4;
						uilinkPoint.Up = andSet2.ID;
					}
					if (k == list3.Count - 1)
					{
						down2 = andSet2.ID;
						andSet2.Right = andSet2.ID - 4;
						uilinkPoint2.Up = andSet2.ID;
					}
					this._foundPoints.Add(num3);
					num3++;
				}
				num3 = num2;
				for (int l = 0; l < list2.Count; l++)
				{
					UILinkPoint andSet3 = this.GetAndSet(num3, list2[l]);
					andSet3.Up = num + 2 + l;
					andSet3.Left = andSet3.ID - 1;
					andSet3.Right = andSet3.ID + 1;
					if (l == 0)
					{
						andSet3.Left = andSet3.ID + 9;
					}
					if (l == list2.Count - 1)
					{
						andSet3.Right = andSet3.ID - 9;
					}
					andSet3.Down = down;
					if (l >= 5)
					{
						andSet3.Down = down2;
					}
					this._foundPoints.Add(num3);
					num3++;
				}
				break;
			}
			case UICharacterCreation.CategoryId.HairStyle:
				if (list2.Count != 0)
				{
					this._helper.CullPointsOutOfElementArea(spriteBatch, list2, this._hairstylesContainer);
					SnapPoint snapPoint3 = list2[list2.Count - 1];
					int num4 = snapPoint3.Id / 10;
					int num5 = snapPoint3.Id % 10;
					int count = Main.Hairstyles.AvailableHairstyles.Count;
					for (int m = 0; m < list2.Count; m++)
					{
						SnapPoint snapPoint4 = list2[m];
						UILinkPoint andSet4 = this.GetAndSet(num3, snapPoint4);
						andSet4.Left = andSet4.ID - 1;
						if (snapPoint4.Id == 0)
						{
							andSet4.Left = -3;
						}
						andSet4.Right = andSet4.ID + 1;
						if (snapPoint4.Id == count - 1)
						{
							andSet4.Right = -4;
						}
						andSet4.Up = andSet4.ID - 10;
						if (m < 10)
						{
							andSet4.Up = num + 2 + m;
						}
						andSet4.Down = andSet4.ID + 10;
						if (snapPoint4.Id + 10 > snapPoint3.Id)
						{
							if (snapPoint4.Id % 10 < 5)
							{
								andSet4.Down = uilinkPoint.ID;
							}
							else
							{
								andSet4.Down = uilinkPoint2.ID;
							}
						}
						if (m == list2.Count - 1)
						{
							uilinkPoint.Up = andSet4.ID;
							uilinkPoint2.Up = andSet4.ID;
						}
						this._foundPoints.Add(num3);
						num3++;
					}
				}
				break;
			default:
			{
				List<SnapPoint> list4 = (from a in snapPoints
				where a.Name == "Low"
				select a).ToList<SnapPoint>();
				list4.Sort(new Comparison<SnapPoint>(this.SortPoints));
				num3 = num2 + 20;
				for (int n = 0; n < list4.Count; n++)
				{
					UILinkPoint andSet5 = this.GetAndSet(num3, list4[n]);
					andSet5.Up = num2 + 2;
					andSet5.Down = uilinkPoint.ID;
					andSet5.Left = andSet5.ID - 1;
					andSet5.Right = andSet5.ID + 1;
					if (n == 0)
					{
						andSet5.Left = andSet5.ID + 2;
						uilinkPoint.Up = andSet5.ID;
					}
					if (n == list4.Count - 1)
					{
						andSet5.Right = andSet5.ID - 2;
						uilinkPoint2.Up = andSet5.ID;
					}
					this._foundPoints.Add(num3);
					num3++;
				}
				num3 = num2;
				for (int num6 = 0; num6 < list2.Count; num6++)
				{
					UILinkPoint andSet6 = this.GetAndSet(num3, list2[num6]);
					andSet6.Up = andSet6.ID - 1;
					andSet6.Down = andSet6.ID + 1;
					if (num6 == 0)
					{
						andSet6.Up = num + 2 + 5;
					}
					if (num6 == list2.Count - 1)
					{
						andSet6.Down = num2 + 20 + 2;
					}
					this._foundPoints.Add(num3);
					num3++;
				}
				break;
			}
			}
			if (PlayerInput.UsingGamepadUI && !this._foundPoints.Contains(UILinkPointNavigator.CurrentPoint))
			{
				this.MoveToVisuallyClosestPoint();
			}
		}

		// Token: 0x060026B7 RID: 9911 RVA: 0x0057A0C4 File Offset: 0x005782C4
		private void MoveToVisuallyClosestPoint()
		{
			Dictionary<int, UILinkPoint> points = UILinkPointNavigator.Points;
			Vector2 mouseScreen = Main.MouseScreen;
			UILinkPoint uilinkPoint = null;
			foreach (int key in this._foundPoints)
			{
				UILinkPoint uilinkPoint2;
				if (!points.TryGetValue(key, out uilinkPoint2))
				{
					return;
				}
				if (uilinkPoint == null || Vector2.Distance(mouseScreen, uilinkPoint.Position) > Vector2.Distance(mouseScreen, uilinkPoint2.Position))
				{
					uilinkPoint = uilinkPoint2;
				}
			}
			if (uilinkPoint != null)
			{
				UILinkPointNavigator.ChangePoint(uilinkPoint.ID);
			}
		}

		// Token: 0x060026B8 RID: 9912 RVA: 0x0057A15C File Offset: 0x0057835C
		public void TryMovingCategory(int direction)
		{
			int num = (int)((this._selectedPicker + direction) % UICharacterCreation.CategoryId.Count);
			if (num < 0)
			{
				num += 10;
			}
			this.SelectColorPicker((UICharacterCreation.CategoryId)num);
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x0057A185 File Offset: 0x00578385
		private UILinkPoint GetAndSet(int ptid, SnapPoint snap)
		{
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[ptid];
			uilinkPoint.Unlink();
			UILinkPointNavigator.SetPosition(uilinkPoint.ID, snap.Position);
			return uilinkPoint;
		}

		// Token: 0x060026BA RID: 9914 RVA: 0x0057A1A9 File Offset: 0x005783A9
		private bool PointWithName(SnapPoint a, string comp)
		{
			return a.Name == comp;
		}

		// Token: 0x060026BB RID: 9915 RVA: 0x0057A1B8 File Offset: 0x005783B8
		private int SortPoints(SnapPoint a, SnapPoint b)
		{
			return a.Id.CompareTo(b.Id);
		}

		// Token: 0x060026BC RID: 9916 RVA: 0x0057A1D9 File Offset: 0x005783D9
		private static Color ScaledHslToRgb(Vector3 hsl)
		{
			return UICharacterCreation.ScaledHslToRgb(hsl.X, hsl.Y, hsl.Z);
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x0057A1F2 File Offset: 0x005783F2
		private static Color ScaledHslToRgb(float hue, float saturation, float luminosity)
		{
			return Main.hslToRgb(hue, saturation, luminosity * 0.85f + 0.15f, byte.MaxValue);
		}

		// Token: 0x060026BE RID: 9918 RVA: 0x0057A210 File Offset: 0x00578410
		private static Vector3 RgbToScaledHsl(Color color)
		{
			Vector3 vector = Main.rgbToHsl(color);
			vector.Z = (vector.Z - 0.15f) / 0.85f;
			vector = Vector3.Clamp(vector, Vector3.Zero, Vector3.One);
			return vector;
		}

		// Token: 0x04004A43 RID: 19011
		private int[] _validClothStyles = new int[]
		{
			0,
			2,
			1,
			3,
			8,
			4,
			6,
			5,
			7,
			9
		};

		// Token: 0x04004A44 RID: 19012
		private readonly Player _player;

		// Token: 0x04004A45 RID: 19013
		private UIColoredImageButton[] _colorPickers;

		// Token: 0x04004A46 RID: 19014
		private UICharacterCreation.CategoryId _selectedPicker;

		// Token: 0x04004A47 RID: 19015
		private Vector3 _currentColorHSL;

		// Token: 0x04004A48 RID: 19016
		private UIColoredImageButton _clothingStylesCategoryButton;

		// Token: 0x04004A49 RID: 19017
		private UIColoredImageButton _hairStylesCategoryButton;

		// Token: 0x04004A4A RID: 19018
		private UIColoredImageButton _charInfoCategoryButton;

		// Token: 0x04004A4B RID: 19019
		private UIElement _topContainer;

		// Token: 0x04004A4C RID: 19020
		private UIElement _middleContainer;

		// Token: 0x04004A4D RID: 19021
		private UIElement _hslContainer;

		// Token: 0x04004A4E RID: 19022
		private UIElement _hairstylesContainer;

		// Token: 0x04004A4F RID: 19023
		private UIElement _clothStylesContainer;

		// Token: 0x04004A50 RID: 19024
		private UIElement _infoContainer;

		// Token: 0x04004A51 RID: 19025
		private UIText _hslHexText;

		// Token: 0x04004A52 RID: 19026
		private UIText _difficultyDescriptionText;

		// Token: 0x04004A53 RID: 19027
		private UIElement _copyHexButton;

		// Token: 0x04004A54 RID: 19028
		private UIElement _pasteHexButton;

		// Token: 0x04004A55 RID: 19029
		private UIElement _randomColorButton;

		// Token: 0x04004A56 RID: 19030
		private UIElement _copyTemplateButton;

		// Token: 0x04004A57 RID: 19031
		private UIElement _pasteTemplateButton;

		// Token: 0x04004A58 RID: 19032
		private UIElement _randomizePlayerButton;

		// Token: 0x04004A59 RID: 19033
		private UIColoredImageButton _genderMale;

		// Token: 0x04004A5A RID: 19034
		private UIColoredImageButton _genderFemale;

		// Token: 0x04004A5B RID: 19035
		private UICharacterNameButton _charName;

		// Token: 0x04004A5C RID: 19036
		private UIText _helpGlyphLeft;

		// Token: 0x04004A5D RID: 19037
		private UIText _helpGlyphRight;

		// Token: 0x04004A5E RID: 19038
		public const int MAX_NAME_LENGTH = 20;

		// Token: 0x04004A5F RID: 19039
		private UIGamepadHelper _helper;

		// Token: 0x04004A60 RID: 19040
		private List<int> _foundPoints = new List<int>();

		// Token: 0x02000735 RID: 1845
		private enum CategoryId
		{
			// Token: 0x04006390 RID: 25488
			CharInfo,
			// Token: 0x04006391 RID: 25489
			Clothing,
			// Token: 0x04006392 RID: 25490
			HairStyle,
			// Token: 0x04006393 RID: 25491
			HairColor,
			// Token: 0x04006394 RID: 25492
			Eye,
			// Token: 0x04006395 RID: 25493
			Skin,
			// Token: 0x04006396 RID: 25494
			Shirt,
			// Token: 0x04006397 RID: 25495
			Undershirt,
			// Token: 0x04006398 RID: 25496
			Pants,
			// Token: 0x04006399 RID: 25497
			Shoes,
			// Token: 0x0400639A RID: 25498
			Count
		}

		// Token: 0x02000736 RID: 1846
		private enum HSLSliderId
		{
			// Token: 0x0400639C RID: 25500
			Hue,
			// Token: 0x0400639D RID: 25501
			Saturation,
			// Token: 0x0400639E RID: 25502
			Luminance
		}
	}
}
