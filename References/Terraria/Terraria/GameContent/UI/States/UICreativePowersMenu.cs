using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Creative;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x02000348 RID: 840
	public class UICreativePowersMenu : UIState
	{
		// Token: 0x170002FD RID: 765
		// (get) Token: 0x0600260D RID: 9741 RVA: 0x00571C78 File Offset: 0x0056FE78
		public bool IsShowingResearchMenu
		{
			get
			{
				return this._mainCategory.CurrentOption == 2;
			}
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x00571C88 File Offset: 0x0056FE88
		public override void OnActivate()
		{
			this.InitializePage();
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x00571C90 File Offset: 0x0056FE90
		private void InitializePage()
		{
			int num = 270;
			int num2 = 20;
			this._container = new UIElement
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension((float)(-(float)num - num2), 1f),
				Top = new StyleDimension((float)num, 0f)
			};
			base.Append(this._container);
			List<UIElement> buttons = this.CreateMainPowerStrip();
			PowerStripUIElement powerStripUIElement = new PowerStripUIElement("strip 0", buttons)
			{
				HAlign = 0f,
				VAlign = 0.5f,
				Left = new StyleDimension(20f, 0f)
			};
			powerStripUIElement.OnMouseOver += this.strip_OnMouseOver;
			powerStripUIElement.OnMouseOut += this.strip_OnMouseOut;
			this._mainPowerStrip = powerStripUIElement;
			List<UIElement> buttons2 = this.CreateTimePowerStrip();
			PowerStripUIElement powerStripUIElement2 = new PowerStripUIElement("strip 1", buttons2)
			{
				HAlign = 0f,
				VAlign = 0.5f,
				Left = new StyleDimension(80f, 0f)
			};
			powerStripUIElement2.OnMouseOver += this.strip_OnMouseOver;
			powerStripUIElement2.OnMouseOut += this.strip_OnMouseOut;
			this._timePowersStrip = powerStripUIElement2;
			List<UIElement> buttons3 = this.CreateWeatherPowerStrip();
			PowerStripUIElement powerStripUIElement3 = new PowerStripUIElement("strip 1", buttons3)
			{
				HAlign = 0f,
				VAlign = 0.5f,
				Left = new StyleDimension(80f, 0f)
			};
			powerStripUIElement3.OnMouseOver += this.strip_OnMouseOver;
			powerStripUIElement3.OnMouseOut += this.strip_OnMouseOut;
			this._weatherPowersStrip = powerStripUIElement3;
			List<UIElement> buttons4 = this.CreatePersonalPowerStrip();
			PowerStripUIElement powerStripUIElement4 = new PowerStripUIElement("strip 1", buttons4)
			{
				HAlign = 0f,
				VAlign = 0.5f,
				Left = new StyleDimension(80f, 0f)
			};
			powerStripUIElement4.OnMouseOver += this.strip_OnMouseOver;
			powerStripUIElement4.OnMouseOut += this.strip_OnMouseOut;
			this._personalPowersStrip = powerStripUIElement4;
			this._infiniteItemsWindow = new UICreativeInfiniteItemsDisplay(this)
			{
				HAlign = 0f,
				VAlign = 0.5f,
				Left = new StyleDimension(80f, 0f),
				Width = new StyleDimension(480f, 0f),
				Height = new StyleDimension(-88f, 1f)
			};
			this.RefreshElementsOrder();
			base.OnUpdate += this.UICreativePowersMenu_OnUpdate;
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x00571F40 File Offset: 0x00570140
		private List<UIElement> CreateMainPowerStrip()
		{
			UICreativePowersMenu.MenuTree<UICreativePowersMenu.OpenMainSubCategory> mainCategory = this._mainCategory;
			mainCategory.Buttons.Clear();
			List<UIElement> list = new List<UIElement>();
			CreativePowerUIElementRequestInfo info = new CreativePowerUIElementRequestInfo
			{
				PreferredButtonWidth = 40,
				PreferredButtonHeight = 40
			};
			GroupOptionButton<int> groupOptionButton = CreativePowersHelper.CreateCategoryButton<int>(info, 1, 0);
			groupOptionButton.Append(CreativePowersHelper.GetIconImage(CreativePowersHelper.CreativePowerIconLocations.ItemDuplication));
			groupOptionButton.OnLeftClick += this.MainCategoryButtonClick;
			groupOptionButton.OnUpdate += this.itemsWindowButton_OnUpdate;
			mainCategory.Buttons.Add(1, groupOptionButton);
			list.Add(groupOptionButton);
			GroupOptionButton<int> groupOptionButton2 = CreativePowersHelper.CreateCategoryButton<int>(info, 2, 0);
			groupOptionButton2.Append(CreativePowersHelper.GetIconImage(CreativePowersHelper.CreativePowerIconLocations.ItemResearch));
			groupOptionButton2.OnLeftClick += this.MainCategoryButtonClick;
			groupOptionButton2.OnUpdate += this.researchWindowButton_OnUpdate;
			mainCategory.Buttons.Add(2, groupOptionButton2);
			list.Add(groupOptionButton2);
			GroupOptionButton<int> groupOptionButton3 = CreativePowersHelper.CreateCategoryButton<int>(info, 3, 0);
			groupOptionButton3.Append(CreativePowersHelper.GetIconImage(CreativePowersHelper.CreativePowerIconLocations.TimeCategory));
			groupOptionButton3.OnLeftClick += this.MainCategoryButtonClick;
			groupOptionButton3.OnUpdate += this.timeCategoryButton_OnUpdate;
			mainCategory.Buttons.Add(3, groupOptionButton3);
			list.Add(groupOptionButton3);
			GroupOptionButton<int> groupOptionButton4 = CreativePowersHelper.CreateCategoryButton<int>(info, 4, 0);
			groupOptionButton4.Append(CreativePowersHelper.GetIconImage(CreativePowersHelper.CreativePowerIconLocations.WeatherCategory));
			groupOptionButton4.OnLeftClick += this.MainCategoryButtonClick;
			groupOptionButton4.OnUpdate += this.weatherCategoryButton_OnUpdate;
			mainCategory.Buttons.Add(4, groupOptionButton4);
			list.Add(groupOptionButton4);
			GroupOptionButton<int> groupOptionButton5 = CreativePowersHelper.CreateCategoryButton<int>(info, 6, 0);
			groupOptionButton5.Append(CreativePowersHelper.GetIconImage(CreativePowersHelper.CreativePowerIconLocations.PersonalCategory));
			groupOptionButton5.OnLeftClick += this.MainCategoryButtonClick;
			groupOptionButton5.OnUpdate += this.personalCategoryButton_OnUpdate;
			mainCategory.Buttons.Add(6, groupOptionButton5);
			list.Add(groupOptionButton5);
			CreativePowerManager.Instance.GetPower<CreativePowers.StopBiomeSpreadPower>().ProvidePowerButtons(info, list);
			GroupOptionButton<int> groupOptionButton6 = this.CreateSubcategoryButton<CreativePowers.DifficultySliderPower>(ref info, 1, "strip 1", 5, 0, mainCategory.Buttons, mainCategory.Sliders);
			groupOptionButton6.OnLeftClick += this.MainCategoryButtonClick;
			list.Add(groupOptionButton6);
			return list;
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x0057217C File Offset: 0x0057037C
		private static void CategoryButton_OnUpdate_DisplayTooltips(UIElement affectedElement, string categoryNameKey)
		{
			GroupOptionButton<int> groupOptionButton = affectedElement as GroupOptionButton<int>;
			if (affectedElement.IsMouseHovering)
			{
				string textValue = Language.GetTextValue(groupOptionButton.IsSelected ? (categoryNameKey + "Opened") : (categoryNameKey + "Closed"));
				CreativePowersHelper.AddDescriptionIfNeeded(ref textValue, categoryNameKey);
				Main.instance.MouseTextNoOverride(textValue, 0, 0, -1, -1, -1, -1, 0);
			}
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x005721D8 File Offset: 0x005703D8
		private void itemsWindowButton_OnUpdate(UIElement affectedElement)
		{
			UICreativePowersMenu.CategoryButton_OnUpdate_DisplayTooltips(affectedElement, "CreativePowers.InfiniteItemsCategory");
		}

		// Token: 0x06002613 RID: 9747 RVA: 0x005721E5 File Offset: 0x005703E5
		private void researchWindowButton_OnUpdate(UIElement affectedElement)
		{
			UICreativePowersMenu.CategoryButton_OnUpdate_DisplayTooltips(affectedElement, "CreativePowers.ResearchItemsCategory");
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x005721F2 File Offset: 0x005703F2
		private void timeCategoryButton_OnUpdate(UIElement affectedElement)
		{
			UICreativePowersMenu.CategoryButton_OnUpdate_DisplayTooltips(affectedElement, "CreativePowers.TimeCategory");
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x005721FF File Offset: 0x005703FF
		private void weatherCategoryButton_OnUpdate(UIElement affectedElement)
		{
			UICreativePowersMenu.CategoryButton_OnUpdate_DisplayTooltips(affectedElement, "CreativePowers.WeatherCategory");
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x0057220C File Offset: 0x0057040C
		private void personalCategoryButton_OnUpdate(UIElement affectedElement)
		{
			UICreativePowersMenu.CategoryButton_OnUpdate_DisplayTooltips(affectedElement, "CreativePowers.PersonalCategory");
		}

		// Token: 0x06002617 RID: 9751 RVA: 0x00572219 File Offset: 0x00570419
		private void UICreativePowersMenu_OnUpdate(UIElement affectedElement)
		{
			if (this._hovered)
			{
				Main.LocalPlayer.mouseInterface = true;
			}
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x0057222E File Offset: 0x0057042E
		private void strip_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			this._hovered = false;
		}

		// Token: 0x06002619 RID: 9753 RVA: 0x00572237 File Offset: 0x00570437
		private void strip_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this._hovered = true;
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x00572240 File Offset: 0x00570440
		private void MainCategoryButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			GroupOptionButton<int> groupOptionButton = listeningElement as GroupOptionButton<int>;
			this.ToggleMainCategory(groupOptionButton.OptionValue);
			this.RefreshElementsOrder();
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x00572266 File Offset: 0x00570466
		private void ToggleMainCategory(int option)
		{
			this.ToggleCategory<UICreativePowersMenu.OpenMainSubCategory>(this._mainCategory, option, UICreativePowersMenu.OpenMainSubCategory.None);
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x00572276 File Offset: 0x00570476
		private void ToggleWeatherCategory(int option)
		{
			this.ToggleCategory<UICreativePowersMenu.WeatherSubcategory>(this._weatherCategory, option, UICreativePowersMenu.WeatherSubcategory.None);
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x00572286 File Offset: 0x00570486
		private void ToggleTimeCategory(int option)
		{
			this.ToggleCategory<UICreativePowersMenu.TimeSubcategory>(this._timeCategory, option, UICreativePowersMenu.TimeSubcategory.None);
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x00572296 File Offset: 0x00570496
		private void TogglePersonalCategory(int option)
		{
			this.ToggleCategory<UICreativePowersMenu.PersonalSubcategory>(this._personalCategory, option, UICreativePowersMenu.PersonalSubcategory.None);
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x005722A6 File Offset: 0x005704A6
		public void SacrificeWhatsInResearchMenu()
		{
			this._infiniteItemsWindow.SacrificeWhatYouCan();
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x005722B3 File Offset: 0x005704B3
		public void StopPlayingResearchAnimations()
		{
			this._infiniteItemsWindow.StopPlayingAnimation();
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x005722C0 File Offset: 0x005704C0
		private void ToggleCategory<TEnum>(UICreativePowersMenu.MenuTree<TEnum> tree, int option, TEnum defaultOption) where TEnum : struct, IConvertible
		{
			if (tree.CurrentOption == option)
			{
				option = defaultOption.ToInt32(null);
			}
			tree.CurrentOption = option;
			foreach (GroupOptionButton<int> groupOptionButton in tree.Buttons.Values)
			{
				groupOptionButton.SetCurrentOption(option);
			}
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x00572338 File Offset: 0x00570538
		private List<UIElement> CreateTimePowerStrip()
		{
			UICreativePowersMenu.MenuTree<UICreativePowersMenu.TimeSubcategory> timeCategory = this._timeCategory;
			List<UIElement> list = new List<UIElement>();
			CreativePowerUIElementRequestInfo info = new CreativePowerUIElementRequestInfo
			{
				PreferredButtonWidth = 40,
				PreferredButtonHeight = 40
			};
			CreativePowerManager.Instance.GetPower<CreativePowers.FreezeTime>().ProvidePowerButtons(info, list);
			CreativePowerManager.Instance.GetPower<CreativePowers.StartDayImmediately>().ProvidePowerButtons(info, list);
			CreativePowerManager.Instance.GetPower<CreativePowers.StartNoonImmediately>().ProvidePowerButtons(info, list);
			CreativePowerManager.Instance.GetPower<CreativePowers.StartNightImmediately>().ProvidePowerButtons(info, list);
			CreativePowerManager.Instance.GetPower<CreativePowers.StartMidnightImmediately>().ProvidePowerButtons(info, list);
			GroupOptionButton<int> groupOptionButton = this.CreateSubcategoryButton<CreativePowers.ModifyTimeRate>(ref info, 2, "strip 2", 1, 0, timeCategory.Buttons, timeCategory.Sliders);
			groupOptionButton.OnLeftClick += this.TimeCategoryButtonClick;
			list.Add(groupOptionButton);
			return list;
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x005723FC File Offset: 0x005705FC
		private List<UIElement> CreatePersonalPowerStrip()
		{
			UICreativePowersMenu.MenuTree<UICreativePowersMenu.PersonalSubcategory> personalCategory = this._personalCategory;
			List<UIElement> list = new List<UIElement>();
			CreativePowerUIElementRequestInfo info = new CreativePowerUIElementRequestInfo
			{
				PreferredButtonWidth = 40,
				PreferredButtonHeight = 40
			};
			CreativePowerManager.Instance.GetPower<CreativePowers.GodmodePower>().ProvidePowerButtons(info, list);
			CreativePowerManager.Instance.GetPower<CreativePowers.FarPlacementRangePower>().ProvidePowerButtons(info, list);
			GroupOptionButton<int> groupOptionButton = this.CreateSubcategoryButton<CreativePowers.SpawnRateSliderPerPlayerPower>(ref info, 2, "strip 2", 1, 0, personalCategory.Buttons, personalCategory.Sliders);
			groupOptionButton.OnLeftClick += this.PersonalCategoryButtonClick;
			list.Add(groupOptionButton);
			return list;
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x0057248C File Offset: 0x0057068C
		private List<UIElement> CreateWeatherPowerStrip()
		{
			UICreativePowersMenu.MenuTree<UICreativePowersMenu.WeatherSubcategory> weatherCategory = this._weatherCategory;
			List<UIElement> list = new List<UIElement>();
			CreativePowerUIElementRequestInfo info = new CreativePowerUIElementRequestInfo
			{
				PreferredButtonWidth = 40,
				PreferredButtonHeight = 40
			};
			GroupOptionButton<int> groupOptionButton = this.CreateSubcategoryButton<CreativePowers.ModifyWindDirectionAndStrength>(ref info, 2, "strip 2", 1, 0, weatherCategory.Buttons, weatherCategory.Sliders);
			groupOptionButton.OnLeftClick += this.WeatherCategoryButtonClick;
			list.Add(groupOptionButton);
			CreativePowerManager.Instance.GetPower<CreativePowers.FreezeWindDirectionAndStrength>().ProvidePowerButtons(info, list);
			GroupOptionButton<int> groupOptionButton2 = this.CreateSubcategoryButton<CreativePowers.ModifyRainPower>(ref info, 2, "strip 2", 2, 0, weatherCategory.Buttons, weatherCategory.Sliders);
			groupOptionButton2.OnLeftClick += this.WeatherCategoryButtonClick;
			list.Add(groupOptionButton2);
			CreativePowerManager.Instance.GetPower<CreativePowers.FreezeRainPower>().ProvidePowerButtons(info, list);
			return list;
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x00572558 File Offset: 0x00570758
		private GroupOptionButton<int> CreateSubcategoryButton<T>(ref CreativePowerUIElementRequestInfo request, int subcategoryDepth, string subcategoryName, int subcategoryIndex, int currentSelectedInSubcategory, Dictionary<int, GroupOptionButton<int>> subcategoryButtons, Dictionary<int, UIElement> slidersSet) where T : ICreativePower, IProvideSliderElement, IPowerSubcategoryElement
		{
			T power = CreativePowerManager.Instance.GetPower<T>();
			UIElement uielement = power.ProvideSlider();
			uielement.Left = new StyleDimension((float)(20 + subcategoryDepth * 60), 0f);
			slidersSet[subcategoryIndex] = uielement;
			uielement.SetSnapPoint(subcategoryName, 0, new Vector2?(new Vector2(0f, 0.5f)), new Vector2?(new Vector2(28f, 0f)));
			GroupOptionButton<int> optionButton = power.GetOptionButton(request, subcategoryIndex, currentSelectedInSubcategory);
			subcategoryButtons[subcategoryIndex] = optionButton;
			CreativePowersHelper.UpdateUnlockStateByPower(power, optionButton, CreativePowersHelper.CommonSelectedColor);
			return optionButton;
		}

		// Token: 0x06002626 RID: 9766 RVA: 0x00572604 File Offset: 0x00570804
		private void WeatherCategoryButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			GroupOptionButton<int> groupOptionButton = listeningElement as GroupOptionButton<int>;
			int optionValue = groupOptionButton.OptionValue;
			if (optionValue != 1)
			{
				if (optionValue == 2 && !CreativePowerManager.Instance.GetPower<CreativePowers.ModifyRainPower>().GetIsUnlocked())
				{
					return;
				}
			}
			else if (!CreativePowerManager.Instance.GetPower<CreativePowers.ModifyWindDirectionAndStrength>().GetIsUnlocked())
			{
				return;
			}
			this.ToggleWeatherCategory(groupOptionButton.OptionValue);
			this.RefreshElementsOrder();
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x00572660 File Offset: 0x00570860
		private void TimeCategoryButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			GroupOptionButton<int> groupOptionButton = listeningElement as GroupOptionButton<int>;
			int optionValue = groupOptionButton.OptionValue;
			if (optionValue == 1 && !CreativePowerManager.Instance.GetPower<CreativePowers.ModifyTimeRate>().GetIsUnlocked())
			{
				return;
			}
			this.ToggleTimeCategory(groupOptionButton.OptionValue);
			this.RefreshElementsOrder();
		}

		// Token: 0x06002628 RID: 9768 RVA: 0x005726A4 File Offset: 0x005708A4
		private void PersonalCategoryButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			GroupOptionButton<int> groupOptionButton = listeningElement as GroupOptionButton<int>;
			int optionValue = groupOptionButton.OptionValue;
			if (optionValue == 1 && !CreativePowerManager.Instance.GetPower<CreativePowers.SpawnRateSliderPerPlayerPower>().GetIsUnlocked())
			{
				return;
			}
			this.TogglePersonalCategory(groupOptionButton.OptionValue);
			this.RefreshElementsOrder();
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x005726E8 File Offset: 0x005708E8
		private void RefreshElementsOrder()
		{
			this._container.RemoveAllChildren();
			this._container.Append(this._mainPowerStrip);
			UIElement element = null;
			UICreativePowersMenu.MenuTree<UICreativePowersMenu.OpenMainSubCategory> mainCategory = this._mainCategory;
			if (mainCategory.Sliders.TryGetValue(mainCategory.CurrentOption, out element))
			{
				this._container.Append(element);
			}
			if (mainCategory.CurrentOption == 1)
			{
				this._infiniteItemsWindow.SetPageTypeToShow(UICreativeInfiniteItemsDisplay.InfiniteItemsDisplayPage.InfiniteItemsPickup);
				this._container.Append(this._infiniteItemsWindow);
			}
			if (mainCategory.CurrentOption == 2)
			{
				this._infiniteItemsWindow.SetPageTypeToShow(UICreativeInfiniteItemsDisplay.InfiniteItemsDisplayPage.InfiniteItemsResearch);
				this._container.Append(this._infiniteItemsWindow);
			}
			if (mainCategory.CurrentOption == 3)
			{
				this._container.Append(this._timePowersStrip);
				UICreativePowersMenu.MenuTree<UICreativePowersMenu.TimeSubcategory> timeCategory = this._timeCategory;
				if (timeCategory.Sliders.TryGetValue(timeCategory.CurrentOption, out element))
				{
					this._container.Append(element);
				}
			}
			if (mainCategory.CurrentOption == 4)
			{
				this._container.Append(this._weatherPowersStrip);
				UICreativePowersMenu.MenuTree<UICreativePowersMenu.WeatherSubcategory> weatherCategory = this._weatherCategory;
				if (weatherCategory.Sliders.TryGetValue(weatherCategory.CurrentOption, out element))
				{
					this._container.Append(element);
				}
			}
			if (mainCategory.CurrentOption == 6)
			{
				this._container.Append(this._personalPowersStrip);
				UICreativePowersMenu.MenuTree<UICreativePowersMenu.PersonalSubcategory> personalCategory = this._personalCategory;
				if (personalCategory.Sliders.TryGetValue(personalCategory.CurrentOption, out element))
				{
					this._container.Append(element);
				}
			}
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x00572850 File Offset: 0x00570A50
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.SetupGamepadPoints(spriteBatch);
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x00572860 File Offset: 0x00570A60
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			int num = 10000;
			List<SnapPoint> snapPoints = this.GetSnapPoints();
			List<SnapPoint> orderedPointsByCategoryName = this._helper.GetOrderedPointsByCategoryName(snapPoints, "strip 0");
			List<SnapPoint> orderedPointsByCategoryName2 = this._helper.GetOrderedPointsByCategoryName(snapPoints, "strip 1");
			List<SnapPoint> orderedPointsByCategoryName3 = this._helper.GetOrderedPointsByCategoryName(snapPoints, "strip 2");
			UILinkPoint[] array = null;
			UILinkPoint[] array2 = null;
			UILinkPoint[] array3 = null;
			if (orderedPointsByCategoryName.Count > 0)
			{
				array = this._helper.CreateUILinkStripVertical(ref num, orderedPointsByCategoryName);
			}
			if (orderedPointsByCategoryName2.Count > 0)
			{
				array2 = this._helper.CreateUILinkStripVertical(ref num, orderedPointsByCategoryName2);
			}
			if (orderedPointsByCategoryName3.Count > 0)
			{
				array3 = this._helper.CreateUILinkStripVertical(ref num, orderedPointsByCategoryName3);
			}
			if (array != null && array2 != null)
			{
				this._helper.LinkVerticalStrips(array, array2, (array.Length - array2.Length) / 2);
			}
			if (array2 != null && array3 != null)
			{
				this._helper.LinkVerticalStrips(array2, array3, (array.Length - array2.Length) / 2);
			}
			UILinkPoint uilinkPoint = null;
			UILinkPoint uilinkPoint2 = null;
			for (int i = 0; i < snapPoints.Count; i++)
			{
				SnapPoint snapPoint = snapPoints[i];
				string name = snapPoint.Name;
				if (!(name == "CreativeSacrificeConfirm"))
				{
					if (name == "CreativeInfinitesSearch")
					{
						uilinkPoint2 = this._helper.MakeLinkPointFromSnapPoint(num++, snapPoint);
						Main.CreativeMenu.GamepadPointIdForInfiniteItemSearchHack = uilinkPoint2.ID;
					}
				}
				else
				{
					uilinkPoint = this._helper.MakeLinkPointFromSnapPoint(num++, snapPoint);
				}
			}
			UILinkPoint uilinkPoint3 = UILinkPointNavigator.Points[15000];
			List<SnapPoint> orderedPointsByCategoryName4 = this._helper.GetOrderedPointsByCategoryName(snapPoints, "CreativeInfinitesFilter");
			if (orderedPointsByCategoryName4.Count > 0)
			{
				UILinkPoint[] array4 = this._helper.CreateUILinkStripHorizontal(ref num, orderedPointsByCategoryName4);
				if (uilinkPoint2 != null)
				{
					uilinkPoint2.Up = array4[0].ID;
					for (int j = 0; j < array4.Length; j++)
					{
						array4[j].Down = uilinkPoint2.ID;
					}
				}
			}
			List<SnapPoint> orderedPointsByCategoryName5 = this._helper.GetOrderedPointsByCategoryName(snapPoints, "CreativeInfinitesSlot");
			UILinkPoint[,] array5 = null;
			if (orderedPointsByCategoryName5.Count > 0)
			{
				array5 = this._helper.CreateUILinkPointGrid(ref num, orderedPointsByCategoryName5, this._infiniteItemsWindow.GetItemsPerLine(), uilinkPoint2, array[0], null, null);
				this._helper.LinkVerticalStripRightSideToSingle(array, array5[0, 0]);
			}
			else if (uilinkPoint2 != null)
			{
				this._helper.LinkVerticalStripRightSideToSingle(array, uilinkPoint2);
			}
			if (uilinkPoint2 != null && array5 != null)
			{
				this._helper.PairUpDown(uilinkPoint2, array5[0, 0]);
			}
			if (uilinkPoint3 != null && this.IsShowingResearchMenu)
			{
				this._helper.LinkVerticalStripRightSideToSingle(array, uilinkPoint3);
			}
			if (uilinkPoint != null)
			{
				this._helper.PairUpDown(uilinkPoint3, uilinkPoint);
				uilinkPoint.Left = array[0].ID;
			}
			if (Main.CreativeMenu.GamepadMoveToSearchButtonHack)
			{
				Main.CreativeMenu.GamepadMoveToSearchButtonHack = false;
				if (uilinkPoint2 != null)
				{
					UILinkPointNavigator.ChangePoint(uilinkPoint2.ID);
				}
			}
		}

		// Token: 0x04004A1E RID: 18974
		private bool _hovered;

		// Token: 0x04004A1F RID: 18975
		private PowerStripUIElement _mainPowerStrip;

		// Token: 0x04004A20 RID: 18976
		private PowerStripUIElement _timePowersStrip;

		// Token: 0x04004A21 RID: 18977
		private PowerStripUIElement _weatherPowersStrip;

		// Token: 0x04004A22 RID: 18978
		private PowerStripUIElement _personalPowersStrip;

		// Token: 0x04004A23 RID: 18979
		private UICreativeInfiniteItemsDisplay _infiniteItemsWindow;

		// Token: 0x04004A24 RID: 18980
		private UIElement _container;

		// Token: 0x04004A25 RID: 18981
		private UICreativePowersMenu.MenuTree<UICreativePowersMenu.OpenMainSubCategory> _mainCategory = new UICreativePowersMenu.MenuTree<UICreativePowersMenu.OpenMainSubCategory>(UICreativePowersMenu.OpenMainSubCategory.None);

		// Token: 0x04004A26 RID: 18982
		private UICreativePowersMenu.MenuTree<UICreativePowersMenu.WeatherSubcategory> _weatherCategory = new UICreativePowersMenu.MenuTree<UICreativePowersMenu.WeatherSubcategory>(UICreativePowersMenu.WeatherSubcategory.None);

		// Token: 0x04004A27 RID: 18983
		private UICreativePowersMenu.MenuTree<UICreativePowersMenu.TimeSubcategory> _timeCategory = new UICreativePowersMenu.MenuTree<UICreativePowersMenu.TimeSubcategory>(UICreativePowersMenu.TimeSubcategory.None);

		// Token: 0x04004A28 RID: 18984
		private UICreativePowersMenu.MenuTree<UICreativePowersMenu.PersonalSubcategory> _personalCategory = new UICreativePowersMenu.MenuTree<UICreativePowersMenu.PersonalSubcategory>(UICreativePowersMenu.PersonalSubcategory.None);

		// Token: 0x04004A29 RID: 18985
		private const int INITIAL_LEFT_PIXELS = 20;

		// Token: 0x04004A2A RID: 18986
		private const int LEFT_PIXELS_PER_STRIP_DEPTH = 60;

		// Token: 0x04004A2B RID: 18987
		private const string STRIP_MAIN = "strip 0";

		// Token: 0x04004A2C RID: 18988
		private const string STRIP_DEPTH_1 = "strip 1";

		// Token: 0x04004A2D RID: 18989
		private const string STRIP_DEPTH_2 = "strip 2";

		// Token: 0x04004A2E RID: 18990
		private UIGamepadHelper _helper;

		// Token: 0x02000729 RID: 1833
		private class MenuTree<TEnum> where TEnum : struct, IConvertible
		{
			// Token: 0x060037FE RID: 14334 RVA: 0x00612CDB File Offset: 0x00610EDB
			public MenuTree(TEnum defaultValue)
			{
				this.CurrentOption = defaultValue.ToInt32(null);
			}

			// Token: 0x04006368 RID: 25448
			public int CurrentOption;

			// Token: 0x04006369 RID: 25449
			public Dictionary<int, GroupOptionButton<int>> Buttons = new Dictionary<int, GroupOptionButton<int>>();

			// Token: 0x0400636A RID: 25450
			public Dictionary<int, UIElement> Sliders = new Dictionary<int, UIElement>();
		}

		// Token: 0x0200072A RID: 1834
		private enum OpenMainSubCategory
		{
			// Token: 0x0400636C RID: 25452
			None,
			// Token: 0x0400636D RID: 25453
			InfiniteItems,
			// Token: 0x0400636E RID: 25454
			ResearchWindow,
			// Token: 0x0400636F RID: 25455
			Time,
			// Token: 0x04006370 RID: 25456
			Weather,
			// Token: 0x04006371 RID: 25457
			EnemyStrengthSlider,
			// Token: 0x04006372 RID: 25458
			PersonalPowers
		}

		// Token: 0x0200072B RID: 1835
		private enum WeatherSubcategory
		{
			// Token: 0x04006374 RID: 25460
			None,
			// Token: 0x04006375 RID: 25461
			WindSlider,
			// Token: 0x04006376 RID: 25462
			RainSlider
		}

		// Token: 0x0200072C RID: 1836
		private enum TimeSubcategory
		{
			// Token: 0x04006378 RID: 25464
			None,
			// Token: 0x04006379 RID: 25465
			TimeRate
		}

		// Token: 0x0200072D RID: 1837
		private enum PersonalSubcategory
		{
			// Token: 0x0400637B RID: 25467
			None,
			// Token: 0x0400637C RID: 25468
			EnemySpawnRateSlider
		}
	}
}
