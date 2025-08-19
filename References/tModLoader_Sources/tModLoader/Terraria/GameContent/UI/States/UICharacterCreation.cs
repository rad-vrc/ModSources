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
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020004D7 RID: 1239
	public class UICharacterCreation : UIState
	{
		// Token: 0x06003B45 RID: 15173 RVA: 0x005B17C8 File Offset: 0x005AF9C8
		public UICharacterCreation(Player player)
		{
			this._player = player;
			this._player.difficulty = 0;
			this.BuildPage();
		}

		// Token: 0x06003B46 RID: 15174 RVA: 0x005B1818 File Offset: 0x005AFA18
		private void BuildPage()
		{
			base.RemoveAllChildren();
			int num = 4;
			UIElement uIElement = new UIElement
			{
				Width = StyleDimension.FromPixels(500f),
				Height = StyleDimension.FromPixels((float)(380 + num)),
				Top = StyleDimension.FromPixels(220f),
				HAlign = 0.5f,
				VAlign = 0f
			};
			uIElement.SetPadding(0f);
			base.Append(uIElement);
			UIPanel uIPanel = new UIPanel
			{
				Width = StyleDimension.FromPercent(1f),
				Height = StyleDimension.FromPixels(uIElement.Height.Pixels - 150f - (float)num),
				Top = StyleDimension.FromPixels(50f),
				BackgroundColor = new Color(33, 43, 79) * 0.8f
			};
			uIPanel.SetPadding(0f);
			uIElement.Append(uIPanel);
			this.MakeBackAndCreatebuttons(uIElement);
			this.MakeCharPreview(uIPanel);
			UIElement uIElement2 = new UIElement
			{
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(50f, 0f)
			};
			uIElement2.SetPadding(0f);
			uIElement2.PaddingTop = 4f;
			uIElement2.PaddingBottom = 0f;
			uIPanel.Append(uIElement2);
			UIElement uIElement3 = new UIElement
			{
				Top = StyleDimension.FromPixelsAndPercent(uIElement2.Height.Pixels + 6f, 0f),
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(uIPanel.Height.Pixels - 70f, 0f)
			};
			uIElement3.SetPadding(0f);
			uIElement3.PaddingTop = 3f;
			uIElement3.PaddingBottom = 0f;
			uIPanel.Append(uIElement3);
			this._topContainer = uIElement2;
			this._middleContainer = uIElement3;
			this.MakeInfoMenu(uIElement3);
			this.MakeHSLMenu(uIElement3);
			this.MakeHairsylesMenu(uIElement3);
			this.MakeClothStylesMenu(uIElement3);
			this.MakeCategoriesBar(uIElement2);
			this.Click_CharInfo(null, null);
		}

		// Token: 0x06003B47 RID: 15175 RVA: 0x005B1A30 File Offset: 0x005AFC30
		private void MakeCharPreview(UIPanel container)
		{
			float num = 70f;
			for (float num2 = 0f; num2 <= 1f; num2 += 1f)
			{
				UICharacter element = new UICharacter(this._player, true, false, 1.5f, false)
				{
					Width = StyleDimension.FromPixels(80f),
					Height = StyleDimension.FromPixelsAndPercent(80f, 0f),
					Top = StyleDimension.FromPixelsAndPercent(0f - num, 0f),
					VAlign = 0f,
					HAlign = 0.5f
				};
				container.Append(element);
			}
		}

		// Token: 0x06003B48 RID: 15176 RVA: 0x005B1AC8 File Offset: 0x005AFCC8
		private void MakeHairsylesMenu(UIElement middleInnerPanel)
		{
			Main.Hairstyles.UpdateUnlocks();
			UIElement uIElement = new UIElement
			{
				Width = StyleDimension.FromPixelsAndPercent(-10f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				HAlign = 0.5f,
				VAlign = 0.5f,
				Top = StyleDimension.FromPixels(6f)
			};
			middleInnerPanel.Append(uIElement);
			uIElement.SetPadding(0f);
			UIList uIList = new UIList
			{
				Width = StyleDimension.FromPixelsAndPercent(-18f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(-6f, 1f)
			};
			uIList.SetPadding(4f);
			uIElement.Append(uIList);
			UIScrollbar uIScrollbar = new UIScrollbar
			{
				HAlign = 1f,
				Height = StyleDimension.FromPixelsAndPercent(-30f, 1f),
				Top = StyleDimension.FromPixels(10f)
			};
			uIScrollbar.SetView(100f, 1000f);
			uIList.SetScrollbar(uIScrollbar);
			uIElement.Append(uIScrollbar);
			int count = Main.Hairstyles.AvailableHairstyles.Count;
			UIElement uIElement2 = new UIElement
			{
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent((float)(48 * (count / 10 + ((count % 10 != 0) ? 1 : 0))), 0f)
			};
			uIList.Add(uIElement2);
			uIElement2.SetPadding(0f);
			for (int i = 0; i < count; i++)
			{
				UIHairStyleButton uIHairStyleButton = new UIHairStyleButton(this._player, Main.Hairstyles.AvailableHairstyles[i])
				{
					Left = StyleDimension.FromPixels((float)(i % 10) * 46f + 6f),
					Top = StyleDimension.FromPixels((float)(i / 10) * 48f + 1f)
				};
				uIHairStyleButton.SetSnapPoint("Middle", i, null, null);
				uIHairStyleButton.SkipRenderingContent(i);
				uIElement2.Append(uIHairStyleButton);
			}
			this._hairstylesContainer = uIElement;
		}

		// Token: 0x06003B49 RID: 15177 RVA: 0x005B1CE8 File Offset: 0x005AFEE8
		private void MakeClothStylesMenu(UIElement middleInnerPanel)
		{
			UIElement uIElement = new UIElement
			{
				Width = StyleDimension.FromPixelsAndPercent(-10f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				HAlign = 0.5f,
				VAlign = 0.5f
			};
			middleInnerPanel.Append(uIElement);
			uIElement.SetPadding(0f);
			int num = 15;
			for (int i = 0; i < this._validClothStyles.Length; i++)
			{
				int num2 = 0;
				if (i >= this._validClothStyles.Length / 2)
				{
					num2 = 20;
				}
				UIClothStyleButton uIClothStyleButton = new UIClothStyleButton(this._player, this._validClothStyles[i])
				{
					Left = StyleDimension.FromPixels((float)i * 46f + (float)num2 + 6f),
					Top = StyleDimension.FromPixels((float)num)
				};
				uIClothStyleButton.OnLeftMouseDown += this.Click_CharClothStyle;
				uIClothStyleButton.SetSnapPoint("Middle", i, null, null);
				uIElement.Append(uIClothStyleButton);
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
				uIElement.Append(element);
				UIColoredImageButton uIColoredImageButton = this.CreatePickerWithoutClick(UICharacterCreation.CategoryId.Clothing, "Images/UI/CharCreation/" + ((j == 0) ? "ClothStyleMale" : "ClothStyleFemale"), 0f, 0f);
				uIColoredImageButton.Top = StyleDimension.FromPixelsAndPercent((float)(num + 92), 0f);
				uIColoredImageButton.Left = StyleDimension.FromPixels((float)j * 230f + 92f + (float)num3 + 6f);
				uIColoredImageButton.HAlign = 0f;
				uIColoredImageButton.VAlign = 0f;
				uIElement.Append(uIColoredImageButton);
				if (j == 0)
				{
					uIColoredImageButton.OnLeftMouseDown += this.Click_CharGenderMale;
					this._genderMale = uIColoredImageButton;
				}
				else
				{
					uIColoredImageButton.OnLeftMouseDown += this.Click_CharGenderFemale;
					this._genderFemale = uIColoredImageButton;
				}
				uIColoredImageButton.SetSnapPoint("Low", j * 4, null, null);
			}
			UIElement uIElement2 = new UIElement
			{
				Width = StyleDimension.FromPixels(130f),
				Height = StyleDimension.FromPixels(50f),
				HAlign = 0.5f,
				VAlign = 1f
			};
			uIElement.Append(uIElement2);
			UIColoredImageButton uIColoredImageButton2 = new UIColoredImageButton(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Copy"), true)
			{
				VAlign = 0.5f,
				HAlign = 0f,
				Left = StyleDimension.FromPixelsAndPercent(0f, 0f)
			};
			uIColoredImageButton2.OnLeftMouseDown += this.Click_CopyPlayerTemplate;
			uIElement2.Append(uIColoredImageButton2);
			this._copyTemplateButton = uIColoredImageButton2;
			UIColoredImageButton uIColoredImageButton3 = new UIColoredImageButton(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Paste"), true)
			{
				VAlign = 0.5f,
				HAlign = 0.5f
			};
			uIColoredImageButton3.OnLeftMouseDown += this.Click_PastePlayerTemplate;
			uIElement2.Append(uIColoredImageButton3);
			this._pasteTemplateButton = uIColoredImageButton3;
			UIColoredImageButton uIColoredImageButton4 = new UIColoredImageButton(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Randomize"), true)
			{
				VAlign = 0.5f,
				HAlign = 1f
			};
			uIColoredImageButton4.OnLeftMouseDown += this.Click_RandomizePlayer;
			uIElement2.Append(uIColoredImageButton4);
			this._randomizePlayerButton = uIColoredImageButton4;
			uIColoredImageButton2.SetSnapPoint("Low", 1, null, null);
			uIColoredImageButton3.SetSnapPoint("Low", 2, null, null);
			uIColoredImageButton4.SetSnapPoint("Low", 3, null, null);
			this._clothStylesContainer = uIElement;
		}

		// Token: 0x06003B4A RID: 15178 RVA: 0x005B2140 File Offset: 0x005B0340
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
			this._colorPickers[4].SetMiddleTexture(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/ColorEyeBack"));
			this._clothingStylesCategoryButton = this.CreatePickerWithoutClick(UICharacterCreation.CategoryId.Clothing, "Images/UI/CharCreation/ClothStyleMale", xPositionStart, xPositionPerId);
			this._clothingStylesCategoryButton.OnLeftMouseDown += this.Click_ClothStyles;
			this._clothingStylesCategoryButton.SetSnapPoint("Top", 1, null, null);
			categoryContainer.Append(this._clothingStylesCategoryButton);
			this._hairStylesCategoryButton = this.CreatePickerWithoutClick(UICharacterCreation.CategoryId.HairStyle, "Images/UI/CharCreation/HairStyle_Hair", xPositionStart, xPositionPerId);
			this._hairStylesCategoryButton.OnLeftMouseDown += this.Click_HairStyles;
			this._hairStylesCategoryButton.SetMiddleTexture(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/HairStyle_Arrow"));
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
			UIText uIText = new UIText(PlayerInput.GenerateInputTag_ForCurrentGamemode(false, "HotbarMinus"), 1f, false)
			{
				Left = new StyleDimension((float)(-(float)num), 0f),
				VAlign = 0.5f,
				Top = new StyleDimension(-4f, 0f)
			};
			categoryContainer.Append(uIText);
			UIText uIText2 = new UIText(PlayerInput.GenerateInputTag_ForCurrentGamemode(false, "HotbarMinus"), 1f, false)
			{
				HAlign = 1f,
				Left = new StyleDimension((float)(12 + num), 0f),
				VAlign = 0.5f,
				Top = new StyleDimension(-4f, 0f)
			};
			categoryContainer.Append(uIText2);
			this._helpGlyphLeft = uIText;
			this._helpGlyphRight = uIText2;
			categoryContainer.OnUpdate += this.UpdateHelpGlyphs;
		}

		// Token: 0x06003B4B RID: 15179 RVA: 0x005B2498 File Offset: 0x005B0698
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

		// Token: 0x06003B4C RID: 15180 RVA: 0x005B24E8 File Offset: 0x005B06E8
		private UIColoredImageButton CreateColorPicker(UICharacterCreation.CategoryId id, string texturePath, float xPositionStart, float xPositionPerId)
		{
			UIColoredImageButton uIColoredImageButton = new UIColoredImageButton(Main.Assets.Request<Texture2D>(texturePath), false);
			this._colorPickers[(int)id] = uIColoredImageButton;
			uIColoredImageButton.VAlign = 0f;
			uIColoredImageButton.HAlign = 0f;
			uIColoredImageButton.Left.Set(xPositionStart + (float)id * xPositionPerId, 0.5f);
			uIColoredImageButton.OnLeftMouseDown += this.Click_ColorPicker;
			uIColoredImageButton.SetSnapPoint("Top", (int)id, null, null);
			return uIColoredImageButton;
		}

		// Token: 0x06003B4D RID: 15181 RVA: 0x005B256E File Offset: 0x005B076E
		private UIColoredImageButton CreatePickerWithoutClick(UICharacterCreation.CategoryId id, string texturePath, float xPositionStart, float xPositionPerId)
		{
			UIColoredImageButton uicoloredImageButton = new UIColoredImageButton(Main.Assets.Request<Texture2D>(texturePath), false);
			uicoloredImageButton.VAlign = 0f;
			uicoloredImageButton.HAlign = 0f;
			uicoloredImageButton.Left.Set(xPositionStart + (float)id * xPositionPerId, 0.5f);
			return uicoloredImageButton;
		}

		// Token: 0x06003B4E RID: 15182 RVA: 0x005B25B0 File Offset: 0x005B07B0
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
			UICharacterNameButton uICharacterNameButton = new UICharacterNameButton(Language.GetText("UI.WorldCreationName"), Language.GetText("UI.PlayerEmptyName"), null);
			uICharacterNameButton.Width = StyleDimension.FromPixelsAndPercent(0f, 1f);
			uICharacterNameButton.HAlign = 0.5f;
			uIElement.Append(uICharacterNameButton);
			this._charName = uICharacterNameButton;
			uICharacterNameButton.OnLeftMouseDown += this.Click_Naming;
			uICharacterNameButton.SetSnapPoint("Middle", 0, null, null);
			float num = 4f;
			float num2 = 0f;
			float num3 = 0.4f;
			UIElement uIElement2 = new UIElement
			{
				HAlign = 0f,
				VAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent(0f - num, num3),
				Height = StyleDimension.FromPixelsAndPercent(-50f, 1f)
			};
			uIElement2.SetPadding(0f);
			uIElement.Append(uIElement2);
			UISlicedImage uISlicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight"))
			{
				HAlign = 1f,
				VAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent((0f - num) * 2f, 1f - num3),
				Left = StyleDimension.FromPixels(0f - num),
				Height = StyleDimension.FromPixelsAndPercent(uIElement2.Height.Pixels, uIElement2.Height.Precent)
			};
			uISlicedImage.SetSliceDepths(10);
			uISlicedImage.Color = Color.LightGray * 0.7f;
			uIElement.Append(uISlicedImage);
			float num4 = 4f;
			UIDifficultyButton uIDifficultyButton = new UIDifficultyButton(this._player, Lang.menu[26], Lang.menu[31], 0, Color.Cyan)
			{
				HAlign = 0f,
				VAlign = 1f / (num4 - 1f),
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f - num2, 1f / num4)
			};
			UIDifficultyButton uIDifficultyButton2 = new UIDifficultyButton(this._player, Lang.menu[25], Lang.menu[30], 1, Main.mcColor)
			{
				HAlign = 0f,
				VAlign = 2f / (num4 - 1f),
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f - num2, 1f / num4)
			};
			UIDifficultyButton uIDifficultyButton3 = new UIDifficultyButton(this._player, Lang.menu[24], Lang.menu[29], 2, Main.hcColor)
			{
				HAlign = 0f,
				VAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f - num2, 1f / num4)
			};
			UIDifficultyButton uIDifficultyButton4 = new UIDifficultyButton(this._player, Language.GetText("UI.Creative"), Language.GetText("UI.CreativeDescriptionPlayer"), 3, Main.creativeModeColor)
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f - num2, 1f / num4)
			};
			UIText uIText = new UIText(Lang.menu[26], 1f, false)
			{
				HAlign = 0f,
				VAlign = 0.5f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Top = StyleDimension.FromPixelsAndPercent(15f, 0f),
				IsWrapped = true
			};
			uIText.PaddingLeft = 20f;
			uIText.PaddingRight = 20f;
			uISlicedImage.Append(uIText);
			uIElement2.Append(uIDifficultyButton4);
			uIElement2.Append(uIDifficultyButton);
			uIElement2.Append(uIDifficultyButton2);
			uIElement2.Append(uIDifficultyButton3);
			this._infoContainer = uIElement;
			this._difficultyDescriptionText = uIText;
			uIDifficultyButton4.OnLeftMouseDown += this.UpdateDifficultyDescription;
			uIDifficultyButton.OnLeftMouseDown += this.UpdateDifficultyDescription;
			uIDifficultyButton2.OnLeftMouseDown += this.UpdateDifficultyDescription;
			uIDifficultyButton3.OnLeftMouseDown += this.UpdateDifficultyDescription;
			this.UpdateDifficultyDescription(null, null);
			uIDifficultyButton4.SetSnapPoint("Middle", 1, null, null);
			uIDifficultyButton.SetSnapPoint("Middle", 2, null, null);
			uIDifficultyButton2.SetSnapPoint("Middle", 3, null, null);
			uIDifficultyButton3.SetSnapPoint("Middle", 4, null, null);
		}

		// Token: 0x06003B4F RID: 15183 RVA: 0x005B2B18 File Offset: 0x005B0D18
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

		// Token: 0x06003B50 RID: 15184 RVA: 0x005B2B8C File Offset: 0x005B0D8C
		private void MakeHSLMenu(UIElement parentContainer)
		{
			UIElement uIElement = new UIElement
			{
				Width = StyleDimension.FromPixelsAndPercent(220f, 0f),
				Height = StyleDimension.FromPixelsAndPercent(158f, 0f),
				HAlign = 0.5f,
				VAlign = 0f
			};
			uIElement.SetPadding(0f);
			parentContainer.Append(uIElement);
			UIElement uIElement2 = new UIPanel
			{
				Width = StyleDimension.FromPixelsAndPercent(220f, 0f),
				Height = StyleDimension.FromPixelsAndPercent(104f, 0f),
				HAlign = 0.5f,
				VAlign = 0f,
				Top = StyleDimension.FromPixelsAndPercent(10f, 0f)
			};
			uIElement2.SetPadding(0f);
			uIElement2.PaddingTop = 3f;
			uIElement.Append(uIElement2);
			uIElement2.Append(this.CreateHSLSlider(UICharacterCreation.HSLSliderId.Hue));
			uIElement2.Append(this.CreateHSLSlider(UICharacterCreation.HSLSliderId.Saturation));
			uIElement2.Append(this.CreateHSLSlider(UICharacterCreation.HSLSliderId.Luminance));
			UIPanel uIPanel = new UIPanel
			{
				VAlign = 1f,
				HAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent(100f, 0f),
				Height = StyleDimension.FromPixelsAndPercent(32f, 0f)
			};
			UIText uIText = new UIText("FFFFFF", 1f, false)
			{
				VAlign = 0.5f,
				HAlign = 0.5f
			};
			uIPanel.Append(uIText);
			uIElement.Append(uIPanel);
			UIColoredImageButton uIColoredImageButton = new UIColoredImageButton(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Copy"), true)
			{
				VAlign = 1f,
				HAlign = 0f,
				Left = StyleDimension.FromPixelsAndPercent(0f, 0f)
			};
			uIColoredImageButton.OnLeftMouseDown += this.Click_CopyHex;
			uIElement.Append(uIColoredImageButton);
			this._copyHexButton = uIColoredImageButton;
			UIColoredImageButton uIColoredImageButton2 = new UIColoredImageButton(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Paste"), true)
			{
				VAlign = 1f,
				HAlign = 0f,
				Left = StyleDimension.FromPixelsAndPercent(40f, 0f)
			};
			uIColoredImageButton2.OnLeftMouseDown += this.Click_PasteHex;
			uIElement.Append(uIColoredImageButton2);
			this._pasteHexButton = uIColoredImageButton2;
			UIColoredImageButton uIColoredImageButton3 = new UIColoredImageButton(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Randomize"), true)
			{
				VAlign = 1f,
				HAlign = 0f,
				Left = StyleDimension.FromPixelsAndPercent(80f, 0f)
			};
			uIColoredImageButton3.OnLeftMouseDown += this.Click_RandomizeSingleColor;
			uIElement.Append(uIColoredImageButton3);
			this._randomColorButton = uIColoredImageButton3;
			this._hslContainer = uIElement;
			this._hslHexText = uIText;
			uIColoredImageButton.SetSnapPoint("Low", 0, null, null);
			uIColoredImageButton2.SetSnapPoint("Low", 1, null, null);
			uIColoredImageButton3.SetSnapPoint("Low", 2, null, null);
		}

		// Token: 0x06003B51 RID: 15185 RVA: 0x005B2EAC File Offset: 0x005B10AC
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

		// Token: 0x06003B52 RID: 15186 RVA: 0x005B2F3C File Offset: 0x005B113C
		private UIColoredSlider CreateHSLSliderButtonBase(UICharacterCreation.HSLSliderId id)
		{
			if (id == UICharacterCreation.HSLSliderId.Saturation)
			{
				return new UIColoredSlider(LocalizedText.Empty, () => this.GetHSLSliderPosition(UICharacterCreation.HSLSliderId.Saturation), delegate(float x)
				{
					this.UpdateHSLValue(UICharacterCreation.HSLSliderId.Saturation, x);
				}, new Action(this.UpdateHSL_S), (float x) => this.GetHSLSliderColorAt(UICharacterCreation.HSLSliderId.Saturation, x), Color.Transparent);
			}
			if (id != UICharacterCreation.HSLSliderId.Luminance)
			{
				return new UIColoredSlider(LocalizedText.Empty, () => this.GetHSLSliderPosition(UICharacterCreation.HSLSliderId.Hue), delegate(float x)
				{
					this.UpdateHSLValue(UICharacterCreation.HSLSliderId.Hue, x);
				}, new Action(this.UpdateHSL_H), (float x) => this.GetHSLSliderColorAt(UICharacterCreation.HSLSliderId.Hue, x), Color.Transparent);
			}
			return new UIColoredSlider(LocalizedText.Empty, () => this.GetHSLSliderPosition(UICharacterCreation.HSLSliderId.Luminance), delegate(float x)
			{
				this.UpdateHSLValue(UICharacterCreation.HSLSliderId.Luminance, x);
			}, new Action(this.UpdateHSL_L), (float x) => this.GetHSLSliderColorAt(UICharacterCreation.HSLSliderId.Luminance, x), Color.Transparent);
		}

		// Token: 0x06003B53 RID: 15187 RVA: 0x005B3018 File Offset: 0x005B1218
		private void UpdateHSL_H()
		{
			float value = UILinksInitializer.HandleSliderHorizontalInput(this._currentColorHSL.X, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
			this.UpdateHSLValue(UICharacterCreation.HSLSliderId.Hue, value);
		}

		// Token: 0x06003B54 RID: 15188 RVA: 0x005B3058 File Offset: 0x005B1258
		private void UpdateHSL_S()
		{
			float value = UILinksInitializer.HandleSliderHorizontalInput(this._currentColorHSL.Y, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
			this.UpdateHSLValue(UICharacterCreation.HSLSliderId.Saturation, value);
		}

		// Token: 0x06003B55 RID: 15189 RVA: 0x005B3098 File Offset: 0x005B1298
		private void UpdateHSL_L()
		{
			float value = UILinksInitializer.HandleSliderHorizontalInput(this._currentColorHSL.Z, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
			this.UpdateHSLValue(UICharacterCreation.HSLSliderId.Luminance, value);
		}

		// Token: 0x06003B56 RID: 15190 RVA: 0x005B30D7 File Offset: 0x005B12D7
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

		// Token: 0x06003B57 RID: 15191 RVA: 0x005B3118 File Offset: 0x005B1318
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

		// Token: 0x06003B58 RID: 15192 RVA: 0x005B31C4 File Offset: 0x005B13C4
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

		// Token: 0x06003B59 RID: 15193 RVA: 0x005B3238 File Offset: 0x005B1438
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

		// Token: 0x06003B5A RID: 15194 RVA: 0x005B32CB File Offset: 0x005B14CB
		private void UpdateHexText(Color pendingColor)
		{
			this._hslHexText.SetText(UICharacterCreation.GetHexText(pendingColor));
		}

		// Token: 0x06003B5B RID: 15195 RVA: 0x005B32DE File Offset: 0x005B14DE
		private static string GetHexText(Color pendingColor)
		{
			return "#" + pendingColor.Hex3().ToUpper();
		}

		// Token: 0x06003B5C RID: 15196 RVA: 0x005B32F8 File Offset: 0x005B14F8
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

		// Token: 0x06003B5D RID: 15197 RVA: 0x005B347D File Offset: 0x005B167D
		private void Click_GoBack(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.OpenCharacterSelectUI();
		}

		// Token: 0x06003B5E RID: 15198 RVA: 0x005B349C File Offset: 0x005B169C
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06003B5F RID: 15199 RVA: 0x005B34F1 File Offset: 0x005B16F1
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06003B60 RID: 15200 RVA: 0x005B3530 File Offset: 0x005B1730
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

		// Token: 0x06003B61 RID: 15201 RVA: 0x005B3580 File Offset: 0x005B1780
		private void Click_ClothStyles(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this.UnselectAllCategories();
			this._selectedPicker = UICharacterCreation.CategoryId.Clothing;
			this._middleContainer.Append(this._clothStylesContainer);
			this._clothingStylesCategoryButton.SetSelected(true);
			this.UpdateSelectedGender();
		}

		// Token: 0x06003B62 RID: 15202 RVA: 0x005B35D4 File Offset: 0x005B17D4
		private void Click_HairStyles(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this.UnselectAllCategories();
			this._selectedPicker = UICharacterCreation.CategoryId.HairStyle;
			this._middleContainer.Append(this._hairstylesContainer);
			this._hairStylesCategoryButton.SetSelected(true);
		}

		// Token: 0x06003B63 RID: 15203 RVA: 0x005B3620 File Offset: 0x005B1820
		private void Click_CharInfo(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this.UnselectAllCategories();
			this._selectedPicker = UICharacterCreation.CategoryId.CharInfo;
			this._middleContainer.Append(this._infoContainer);
			this._charInfoCategoryButton.SetSelected(true);
		}

		// Token: 0x06003B64 RID: 15204 RVA: 0x005B366C File Offset: 0x005B186C
		private void Click_CharClothStyle(UIMouseEvent evt, UIElement listeningElement)
		{
			this._clothingStylesCategoryButton.SetImageWithoutSettingSize(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/" + (this._player.Male ? "ClothStyleMale" : "ClothStyleFemale")));
			this.UpdateSelectedGender();
		}

		// Token: 0x06003B65 RID: 15205 RVA: 0x005B36AC File Offset: 0x005B18AC
		private void Click_CharGenderMale(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this._player.Male = true;
			this.Click_CharClothStyle(evt, listeningElement);
			this.UpdateSelectedGender();
		}

		// Token: 0x06003B66 RID: 15206 RVA: 0x005B36DD File Offset: 0x005B18DD
		private void Click_CharGenderFemale(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this._player.Male = false;
			this.Click_CharClothStyle(evt, listeningElement);
			this.UpdateSelectedGender();
		}

		// Token: 0x06003B67 RID: 15207 RVA: 0x005B370E File Offset: 0x005B190E
		private void UpdateSelectedGender()
		{
			this._genderMale.SetSelected(this._player.Male);
			this._genderFemale.SetSelected(!this._player.Male);
		}

		// Token: 0x06003B68 RID: 15208 RVA: 0x005B373F File Offset: 0x005B193F
		private void Click_CopyHex(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			Platform.Get<IClipboard>().Value = this._hslHexText.Text;
		}

		// Token: 0x06003B69 RID: 15209 RVA: 0x005B376C File Offset: 0x005B196C
		private void Click_PasteHex(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			string value = Platform.Get<IClipboard>().Value;
			Vector3 hsl;
			if (this.GetHexColor(value, out hsl))
			{
				this.ApplyPendingColor(UICharacterCreation.ScaledHslToRgb(hsl.X, hsl.Y, hsl.Z));
				this._currentColorHSL = hsl;
				this.UpdateHexText(UICharacterCreation.ScaledHslToRgb(hsl.X, hsl.Y, hsl.Z));
				this.UpdateColorPickers();
			}
		}

		// Token: 0x06003B6A RID: 15210 RVA: 0x005B37EC File Offset: 0x005B19EC
		private void Click_CopyPlayerTemplate(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			string text = JsonConvert.SerializeObject(new Dictionary<string, object>
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
			PlayerInput.PrettyPrintProfiles(ref text);
			Platform.Get<IClipboard>().Value = text;
		}

		// Token: 0x06003B6B RID: 15211 RVA: 0x005B394C File Offset: 0x005B1B4C
		private void Click_PastePlayerTemplate(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			try
			{
				string value = Platform.Get<IClipboard>().Value;
				int num = value.IndexOf("{");
				if (num != -1)
				{
					value = value.Substring(num);
					int num2 = value.LastIndexOf("}");
					if (num2 != -1)
					{
						value = value.Substring(0, num2 + 1);
						Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(value);
						if (dictionary != null)
						{
							Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
							foreach (KeyValuePair<string, object> item in dictionary)
							{
								dictionary2[item.Key.ToLower()] = item.Value;
							}
							object value2;
							if (dictionary2.TryGetValue("version", out value2))
							{
								long num5 = (long)value2;
							}
							if (dictionary2.TryGetValue("hairstyle", out value2))
							{
								int num3 = (int)((long)value2);
								if (Main.Hairstyles.AvailableHairstyles.Contains(num3))
								{
									this._player.hair = num3;
								}
							}
							if (dictionary2.TryGetValue("clothingstyle", out value2))
							{
								int num4 = (int)((long)value2);
								if (this._validClothStyles.Contains(num4))
								{
									this._player.skinVariant = num4;
								}
							}
							Vector3 hsl;
							if (dictionary2.TryGetValue("haircolor", out value2) && this.GetHexColor((string)value2, out hsl))
							{
								this._player.hairColor = UICharacterCreation.ScaledHslToRgb(hsl);
							}
							if (dictionary2.TryGetValue("eyecolor", out value2) && this.GetHexColor((string)value2, out hsl))
							{
								this._player.eyeColor = UICharacterCreation.ScaledHslToRgb(hsl);
							}
							if (dictionary2.TryGetValue("skincolor", out value2) && this.GetHexColor((string)value2, out hsl))
							{
								this._player.skinColor = UICharacterCreation.ScaledHslToRgb(hsl);
							}
							if (dictionary2.TryGetValue("shirtcolor", out value2) && this.GetHexColor((string)value2, out hsl))
							{
								this._player.shirtColor = UICharacterCreation.ScaledHslToRgb(hsl);
							}
							if (dictionary2.TryGetValue("undershirtcolor", out value2) && this.GetHexColor((string)value2, out hsl))
							{
								this._player.underShirtColor = UICharacterCreation.ScaledHslToRgb(hsl);
							}
							if (dictionary2.TryGetValue("pantscolor", out value2) && this.GetHexColor((string)value2, out hsl))
							{
								this._player.pantsColor = UICharacterCreation.ScaledHslToRgb(hsl);
							}
							if (dictionary2.TryGetValue("shoecolor", out value2) && this.GetHexColor((string)value2, out hsl))
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

		// Token: 0x06003B6C RID: 15212 RVA: 0x005B3C48 File Offset: 0x005B1E48
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
			if (player.hair >= HairID.Count)
			{
				Player player2 = player;
				bool male;
				switch (HairLoader.GetHair(player.hair).RandomizedCharacterCreationGender)
				{
				case Gender.Unspecified:
					male = Main.rand.NextBool();
					break;
				case Gender.Male:
					male = true;
					break;
				case Gender.Female:
					male = false;
					break;
				default:
					male = Main.rand.NextBool();
					break;
				}
				player2.Male = male;
			}
			this.Click_CharClothStyle(null, null);
			this.UpdateSelectedGender();
			this.UpdateColorPickers();
		}

		// Token: 0x06003B6D RID: 15213 RVA: 0x005B4084 File Offset: 0x005B2284
		private void Click_Naming(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			this._player.name = "";
			Main.clrInput();
			UIVirtualKeyboard uIVirtualKeyboard = new UIVirtualKeyboard(Lang.menu[45].Value, "", new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedNaming), new Action(this.OnCanceledNaming), 0, true);
			uIVirtualKeyboard.SetMaxInputLength(20);
			Main.MenuUI.SetState(uIVirtualKeyboard);
		}

		// Token: 0x06003B6E RID: 15214 RVA: 0x005B4100 File Offset: 0x005B2300
		private void Click_NamingAndCreating(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			if (string.IsNullOrEmpty(this._player.name))
			{
				this._player.name = "";
				Main.clrInput();
				UIVirtualKeyboard uIVirtualKeyboard = new UIVirtualKeyboard(Lang.menu[45].Value, "", new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedNamingAndCreating), new Action(this.OnCanceledNaming), 0, false);
				uIVirtualKeyboard.SetMaxInputLength(20);
				Main.MenuUI.SetState(uIVirtualKeyboard);
				return;
			}
			this.FinishCreatingCharacter();
		}

		// Token: 0x06003B6F RID: 15215 RVA: 0x005B4195 File Offset: 0x005B2395
		private void OnFinishedNaming(string name)
		{
			this._player.name = name.Trim();
			Main.MenuUI.SetState(this);
			this._charName.SetContents(this._player.name);
		}

		// Token: 0x06003B70 RID: 15216 RVA: 0x005B41C9 File Offset: 0x005B23C9
		private void OnCanceledNaming()
		{
			Main.MenuUI.SetState(this);
		}

		// Token: 0x06003B71 RID: 15217 RVA: 0x005B41D6 File Offset: 0x005B23D6
		private void OnFinishedNamingAndCreating(string name)
		{
			this._player.name = name.Trim();
			Main.MenuUI.SetState(this);
			this._charName.SetContents(this._player.name);
			this.FinishCreatingCharacter();
		}

		// Token: 0x06003B72 RID: 15218 RVA: 0x005B4210 File Offset: 0x005B2410
		private void FinishCreatingCharacter()
		{
			this.SetupPlayerStatsAndInventoryBasedOnDifficulty();
			PlayerFileData.CreateAndSave(this._player);
			Main.LoadPlayers();
			Main.menuMode = 1;
		}

		// Token: 0x06003B73 RID: 15219 RVA: 0x005B4230 File Offset: 0x005B2430
		private void SetupPlayerStatsAndInventoryBasedOnDifficulty()
		{
			int num = 0;
			if (this._player.difficulty == 3)
			{
				PlayerLoader.ModifyMaxStats(this._player);
				this._player.statLife = this._player.statLifeMax;
				this._player.statMana = this._player.statManaMax;
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
			IEnumerable<Item> vanillaItems = from item in this._player.inventory
			where !item.IsAir
			select item into x
			select x.Clone();
			List<Item> startingItems = PlayerLoader.GetStartingItems(this._player, vanillaItems, false);
			PlayerLoader.SetStartInventory(this._player, startingItems);
		}

		// Token: 0x06003B74 RID: 15220 RVA: 0x005B45B8 File Offset: 0x005B27B8
		private bool GetHexColor(string hexString, out Vector3 hsl)
		{
			if (hexString.StartsWith("#"))
			{
				hexString = hexString.Substring(1);
			}
			uint result;
			if (hexString.Length <= 6 && uint.TryParse(hexString, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out result))
			{
				uint b = result & 255U;
				uint g = result >> 8 & 255U;
				uint r = result >> 16 & 255U;
				hsl = UICharacterCreation.RgbToScaledHsl(new Color((int)r, (int)g, (int)b));
				return true;
			}
			hsl = Vector3.Zero;
			return false;
		}

		// Token: 0x06003B75 RID: 15221 RVA: 0x005B4638 File Offset: 0x005B2838
		private void Click_RandomizeSingleColor(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			Vector3 randomColorVector = UICharacterCreation.GetRandomColorVector();
			this.ApplyPendingColor(UICharacterCreation.ScaledHslToRgb(randomColorVector.X, randomColorVector.Y, randomColorVector.Z));
			this._currentColorHSL = randomColorVector;
			this.UpdateHexText(UICharacterCreation.ScaledHslToRgb(randomColorVector.X, randomColorVector.Y, randomColorVector.Z));
			this.UpdateColorPickers();
		}

		// Token: 0x06003B76 RID: 15222 RVA: 0x005B46A7 File Offset: 0x005B28A7
		private static Vector3 GetRandomColorVector()
		{
			return new Vector3(Main.rand.NextFloat(), Main.rand.NextFloat(), Main.rand.NextFloat());
		}

		// Token: 0x06003B77 RID: 15223 RVA: 0x005B46CC File Offset: 0x005B28CC
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

		// Token: 0x06003B78 RID: 15224 RVA: 0x005B4750 File Offset: 0x005B2950
		private void SelectColorPicker(UICharacterCreation.CategoryId selection)
		{
			this._selectedPicker = selection;
			switch (selection)
			{
			case UICharacterCreation.CategoryId.CharInfo:
				this.Click_CharInfo(null, null);
				return;
			case UICharacterCreation.CategoryId.Clothing:
				this.Click_ClothStyles(null, null);
				return;
			case UICharacterCreation.CategoryId.HairStyle:
				this.Click_HairStyles(null, null);
				return;
			default:
			{
				this.UnselectAllCategories();
				this._middleContainer.Append(this._hslContainer);
				for (int i = 0; i < this._colorPickers.Length; i++)
				{
					if (this._colorPickers[i] != null)
					{
						this._colorPickers[i].SetSelected(i == (int)selection);
					}
				}
				Vector3 currentColorHSL = Vector3.One;
				switch (this._selectedPicker)
				{
				case UICharacterCreation.CategoryId.HairColor:
					currentColorHSL = UICharacterCreation.RgbToScaledHsl(this._player.hairColor);
					break;
				case UICharacterCreation.CategoryId.Eye:
					currentColorHSL = UICharacterCreation.RgbToScaledHsl(this._player.eyeColor);
					break;
				case UICharacterCreation.CategoryId.Skin:
					currentColorHSL = UICharacterCreation.RgbToScaledHsl(this._player.skinColor);
					break;
				case UICharacterCreation.CategoryId.Shirt:
					currentColorHSL = UICharacterCreation.RgbToScaledHsl(this._player.shirtColor);
					break;
				case UICharacterCreation.CategoryId.Undershirt:
					currentColorHSL = UICharacterCreation.RgbToScaledHsl(this._player.underShirtColor);
					break;
				case UICharacterCreation.CategoryId.Pants:
					currentColorHSL = UICharacterCreation.RgbToScaledHsl(this._player.pantsColor);
					break;
				case UICharacterCreation.CategoryId.Shoes:
					currentColorHSL = UICharacterCreation.RgbToScaledHsl(this._player.shoeColor);
					break;
				}
				this._currentColorHSL = currentColorHSL;
				this.UpdateHexText(UICharacterCreation.ScaledHslToRgb(currentColorHSL.X, currentColorHSL.Y, currentColorHSL.Z));
				return;
			}
			}
		}

		// Token: 0x06003B79 RID: 15225 RVA: 0x005B48B8 File Offset: 0x005B2AB8
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

		// Token: 0x06003B7A RID: 15226 RVA: 0x005B498C File Offset: 0x005B2B8C
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

		// Token: 0x06003B7B RID: 15227 RVA: 0x005B4B00 File Offset: 0x005B2D00
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 1;
			int num = 3000;
			int num2 = num + 20;
			int num3 = num;
			List<SnapPoint> snapPoints = this.GetSnapPoints();
			SnapPoint snapPoint = snapPoints.First((SnapPoint a) => a.Name == "Back");
			SnapPoint snapPoint2 = snapPoints.First((SnapPoint a) => a.Name == "Create");
			UILinkPoint uILinkPoint = UILinkPointNavigator.Points[num3];
			uILinkPoint.Unlink();
			UILinkPointNavigator.SetPosition(num3, snapPoint.Position);
			num3++;
			UILinkPoint uILinkPoint2 = UILinkPointNavigator.Points[num3];
			uILinkPoint2.Unlink();
			UILinkPointNavigator.SetPosition(num3, snapPoint2.Position);
			num3++;
			uILinkPoint.Right = uILinkPoint2.ID;
			uILinkPoint2.Left = uILinkPoint.ID;
			this._foundPoints.Clear();
			this._foundPoints.Add(uILinkPoint.ID);
			this._foundPoints.Add(uILinkPoint2.ID);
			List<SnapPoint> list = (from a in snapPoints
			where a.Name == "Top"
			select a).ToList<SnapPoint>();
			list.Sort(new Comparison<SnapPoint>(this.SortPoints));
			for (int i = 0; i < list.Count; i++)
			{
				UILinkPoint uILinkPoint3 = UILinkPointNavigator.Points[num3];
				uILinkPoint3.Unlink();
				UILinkPointNavigator.SetPosition(num3, list[i].Position);
				uILinkPoint3.Left = num3 - 1;
				uILinkPoint3.Right = num3 + 1;
				uILinkPoint3.Down = num2;
				if (i == 0)
				{
					uILinkPoint3.Left = -3;
				}
				if (i == list.Count - 1)
				{
					uILinkPoint3.Right = -4;
				}
				if (this._selectedPicker == UICharacterCreation.CategoryId.HairStyle || this._selectedPicker == UICharacterCreation.CategoryId.Clothing)
				{
					uILinkPoint3.Down = num2 + i;
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
					UILinkPoint andSet3 = this.GetAndSet(num3, list2[j]);
					andSet3.Up = andSet3.ID - 1;
					andSet3.Down = andSet3.ID + 1;
					if (j == 0)
					{
						andSet3.Up = num + 2;
					}
					if (j == list2.Count - 1)
					{
						andSet3.Down = uILinkPoint.ID;
						uILinkPoint.Up = andSet3.ID;
						uILinkPoint2.Up = andSet3.ID;
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
					UILinkPoint andSet4 = this.GetAndSet(num3, list3[k]);
					andSet4.Up = num2 + k + 2;
					andSet4.Down = uILinkPoint.ID;
					if (k >= 3)
					{
						andSet4.Up++;
						andSet4.Down = uILinkPoint2.ID;
					}
					andSet4.Left = andSet4.ID - 1;
					andSet4.Right = andSet4.ID + 1;
					if (k == 0)
					{
						down = andSet4.ID;
						andSet4.Left = andSet4.ID + 4;
						uILinkPoint.Up = andSet4.ID;
					}
					if (k == list3.Count - 1)
					{
						down2 = andSet4.ID;
						andSet4.Right = andSet4.ID - 4;
						uILinkPoint2.Up = andSet4.ID;
					}
					this._foundPoints.Add(num3);
					num3++;
				}
				num3 = num2;
				for (int l = 0; l < list2.Count; l++)
				{
					UILinkPoint andSet5 = this.GetAndSet(num3, list2[l]);
					andSet5.Up = num + 2 + l;
					andSet5.Left = andSet5.ID - 1;
					andSet5.Right = andSet5.ID + 1;
					if (l == 0)
					{
						andSet5.Left = andSet5.ID + 9;
					}
					if (l == list2.Count - 1)
					{
						andSet5.Right = andSet5.ID - 9;
					}
					andSet5.Down = down;
					if (l >= 5)
					{
						andSet5.Down = down2;
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
					int num5 = snapPoint3.Id / 10;
					int num6 = snapPoint3.Id % 10;
					int count = Main.Hairstyles.AvailableHairstyles.Count;
					for (int m = 0; m < list2.Count; m++)
					{
						SnapPoint snapPoint4 = list2[m];
						UILinkPoint andSet6 = this.GetAndSet(num3, snapPoint4);
						andSet6.Left = andSet6.ID - 1;
						if (snapPoint4.Id == 0)
						{
							andSet6.Left = -3;
						}
						andSet6.Right = andSet6.ID + 1;
						if (snapPoint4.Id == count - 1)
						{
							andSet6.Right = -4;
						}
						andSet6.Up = andSet6.ID - 10;
						if (m < 10)
						{
							andSet6.Up = num + 2 + m;
						}
						andSet6.Down = andSet6.ID + 10;
						if (snapPoint4.Id + 10 > snapPoint3.Id)
						{
							if (snapPoint4.Id % 10 < 5)
							{
								andSet6.Down = uILinkPoint.ID;
							}
							else
							{
								andSet6.Down = uILinkPoint2.ID;
							}
						}
						if (m == list2.Count - 1)
						{
							uILinkPoint.Up = andSet6.ID;
							uILinkPoint2.Up = andSet6.ID;
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
					UILinkPoint andSet7 = this.GetAndSet(num3, list4[n]);
					andSet7.Up = num2 + 2;
					andSet7.Down = uILinkPoint.ID;
					andSet7.Left = andSet7.ID - 1;
					andSet7.Right = andSet7.ID + 1;
					if (n == 0)
					{
						andSet7.Left = andSet7.ID + 2;
						uILinkPoint.Up = andSet7.ID;
					}
					if (n == list4.Count - 1)
					{
						andSet7.Right = andSet7.ID - 2;
						uILinkPoint2.Up = andSet7.ID;
					}
					this._foundPoints.Add(num3);
					num3++;
				}
				num3 = num2;
				for (int num4 = 0; num4 < list2.Count; num4++)
				{
					UILinkPoint andSet8 = this.GetAndSet(num3, list2[num4]);
					andSet8.Up = andSet8.ID - 1;
					andSet8.Down = andSet8.ID + 1;
					if (num4 == 0)
					{
						andSet8.Up = num + 2 + 5;
					}
					if (num4 == list2.Count - 1)
					{
						andSet8.Down = num2 + 20 + 2;
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

		// Token: 0x06003B7C RID: 15228 RVA: 0x005B5334 File Offset: 0x005B3534
		private void MoveToVisuallyClosestPoint()
		{
			Dictionary<int, UILinkPoint> points = UILinkPointNavigator.Points;
			Vector2 mouseScreen = Main.MouseScreen;
			UILinkPoint uILinkPoint = null;
			foreach (int foundPoint in this._foundPoints)
			{
				UILinkPoint value;
				if (!points.TryGetValue(foundPoint, out value))
				{
					return;
				}
				if (uILinkPoint == null || Vector2.Distance(mouseScreen, uILinkPoint.Position) > Vector2.Distance(mouseScreen, value.Position))
				{
					uILinkPoint = value;
				}
			}
			if (uILinkPoint != null)
			{
				UILinkPointNavigator.ChangePoint(uILinkPoint.ID);
			}
		}

		// Token: 0x06003B7D RID: 15229 RVA: 0x005B53CC File Offset: 0x005B35CC
		public void TryMovingCategory(int direction)
		{
			int num = (int)((this._selectedPicker + direction) % UICharacterCreation.CategoryId.Count);
			if (num < 0)
			{
				num += 10;
			}
			this.SelectColorPicker((UICharacterCreation.CategoryId)num);
		}

		// Token: 0x06003B7E RID: 15230 RVA: 0x005B53F5 File Offset: 0x005B35F5
		private UILinkPoint GetAndSet(int ptid, SnapPoint snap)
		{
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[ptid];
			uilinkPoint.Unlink();
			UILinkPointNavigator.SetPosition(uilinkPoint.ID, snap.Position);
			return uilinkPoint;
		}

		// Token: 0x06003B7F RID: 15231 RVA: 0x005B5419 File Offset: 0x005B3619
		private bool PointWithName(SnapPoint a, string comp)
		{
			return a.Name == comp;
		}

		// Token: 0x06003B80 RID: 15232 RVA: 0x005B5428 File Offset: 0x005B3628
		private int SortPoints(SnapPoint a, SnapPoint b)
		{
			return a.Id.CompareTo(b.Id);
		}

		// Token: 0x06003B81 RID: 15233 RVA: 0x005B5449 File Offset: 0x005B3649
		private static Color ScaledHslToRgb(Vector3 hsl)
		{
			return UICharacterCreation.ScaledHslToRgb(hsl.X, hsl.Y, hsl.Z);
		}

		// Token: 0x06003B82 RID: 15234 RVA: 0x005B5462 File Offset: 0x005B3662
		private static Color ScaledHslToRgb(float hue, float saturation, float luminosity)
		{
			return Main.hslToRgb(hue, saturation, luminosity * 0.85f + 0.15f, byte.MaxValue);
		}

		// Token: 0x06003B83 RID: 15235 RVA: 0x005B5480 File Offset: 0x005B3680
		private static Vector3 RgbToScaledHsl(Color color)
		{
			Vector3 value = Main.rgbToHsl(color);
			value.Z = (value.Z - 0.15f) / 0.85f;
			return Vector3.Clamp(value, Vector3.Zero, Vector3.One);
		}

		// Token: 0x04005502 RID: 21762
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

		// Token: 0x04005503 RID: 21763
		private readonly Player _player;

		// Token: 0x04005504 RID: 21764
		private UIColoredImageButton[] _colorPickers;

		// Token: 0x04005505 RID: 21765
		private UICharacterCreation.CategoryId _selectedPicker;

		// Token: 0x04005506 RID: 21766
		private Vector3 _currentColorHSL;

		// Token: 0x04005507 RID: 21767
		private UIColoredImageButton _clothingStylesCategoryButton;

		// Token: 0x04005508 RID: 21768
		private UIColoredImageButton _hairStylesCategoryButton;

		// Token: 0x04005509 RID: 21769
		private UIColoredImageButton _charInfoCategoryButton;

		// Token: 0x0400550A RID: 21770
		private UIElement _topContainer;

		// Token: 0x0400550B RID: 21771
		private UIElement _middleContainer;

		// Token: 0x0400550C RID: 21772
		private UIElement _hslContainer;

		// Token: 0x0400550D RID: 21773
		private UIElement _hairstylesContainer;

		// Token: 0x0400550E RID: 21774
		private UIElement _clothStylesContainer;

		// Token: 0x0400550F RID: 21775
		private UIElement _infoContainer;

		// Token: 0x04005510 RID: 21776
		private UIText _hslHexText;

		// Token: 0x04005511 RID: 21777
		private UIText _difficultyDescriptionText;

		// Token: 0x04005512 RID: 21778
		private UIElement _copyHexButton;

		// Token: 0x04005513 RID: 21779
		private UIElement _pasteHexButton;

		// Token: 0x04005514 RID: 21780
		private UIElement _randomColorButton;

		// Token: 0x04005515 RID: 21781
		private UIElement _copyTemplateButton;

		// Token: 0x04005516 RID: 21782
		private UIElement _pasteTemplateButton;

		// Token: 0x04005517 RID: 21783
		private UIElement _randomizePlayerButton;

		// Token: 0x04005518 RID: 21784
		private UIColoredImageButton _genderMale;

		// Token: 0x04005519 RID: 21785
		private UIColoredImageButton _genderFemale;

		// Token: 0x0400551A RID: 21786
		private UICharacterNameButton _charName;

		// Token: 0x0400551B RID: 21787
		private UIText _helpGlyphLeft;

		// Token: 0x0400551C RID: 21788
		private UIText _helpGlyphRight;

		// Token: 0x0400551D RID: 21789
		public const int MAX_NAME_LENGTH = 20;

		// Token: 0x0400551E RID: 21790
		private UIGamepadHelper _helper;

		// Token: 0x0400551F RID: 21791
		private List<int> _foundPoints = new List<int>();

		// Token: 0x02000BDC RID: 3036
		private enum CategoryId
		{
			// Token: 0x04007767 RID: 30567
			CharInfo,
			// Token: 0x04007768 RID: 30568
			Clothing,
			// Token: 0x04007769 RID: 30569
			HairStyle,
			// Token: 0x0400776A RID: 30570
			HairColor,
			// Token: 0x0400776B RID: 30571
			Eye,
			// Token: 0x0400776C RID: 30572
			Skin,
			// Token: 0x0400776D RID: 30573
			Shirt,
			// Token: 0x0400776E RID: 30574
			Undershirt,
			// Token: 0x0400776F RID: 30575
			Pants,
			// Token: 0x04007770 RID: 30576
			Shoes,
			// Token: 0x04007771 RID: 30577
			Count
		}

		// Token: 0x02000BDD RID: 3037
		private enum HSLSliderId
		{
			// Token: 0x04007773 RID: 30579
			Hue,
			// Token: 0x04007774 RID: 30580
			Saturation,
			// Token: 0x04007775 RID: 30581
			Luminance
		}
	}
}
