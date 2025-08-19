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
	// Token: 0x020004D9 RID: 1241
	public class UICreativePowersMenu : UIState
	{
		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06003BA0 RID: 15264 RVA: 0x005B6A83 File Offset: 0x005B4C83
		public bool IsShowingResearchMenu
		{
			get
			{
				return this._mainCategory.CurrentOption == 2;
			}
		}

		// Token: 0x06003BA1 RID: 15265 RVA: 0x005B6A93 File Offset: 0x005B4C93
		public override void OnActivate()
		{
			this.InitializePage();
		}

		// Token: 0x06003BA2 RID: 15266 RVA: 0x005B6A9C File Offset: 0x005B4C9C
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

		// Token: 0x06003BA3 RID: 15267 RVA: 0x005B6D4C File Offset: 0x005B4F4C
		private List<UIElement> CreateMainPowerStrip()
		{
			UICreativePowersMenu.MenuTree<UICreativePowersMenu.OpenMainSubCategory> mainCategory = this._mainCategory;
			mainCategory.Buttons.Clear();
			List<UIElement> list = new List<UIElement>();
			CreativePowerUIElementRequestInfo request = new CreativePowerUIElementRequestInfo
			{
				PreferredButtonWidth = 40,
				PreferredButtonHeight = 40
			};
			GroupOptionButton<int> groupOptionButton = CreativePowersHelper.CreateCategoryButton<int>(request, 1, 0);
			groupOptionButton.Append(CreativePowersHelper.GetIconImage(CreativePowersHelper.CreativePowerIconLocations.ItemDuplication));
			groupOptionButton.OnLeftClick += this.MainCategoryButtonClick;
			groupOptionButton.OnUpdate += this.itemsWindowButton_OnUpdate;
			mainCategory.Buttons.Add(1, groupOptionButton);
			list.Add(groupOptionButton);
			GroupOptionButton<int> groupOptionButton2 = CreativePowersHelper.CreateCategoryButton<int>(request, 2, 0);
			groupOptionButton2.Append(CreativePowersHelper.GetIconImage(CreativePowersHelper.CreativePowerIconLocations.ItemResearch));
			groupOptionButton2.OnLeftClick += this.MainCategoryButtonClick;
			groupOptionButton2.OnUpdate += this.researchWindowButton_OnUpdate;
			mainCategory.Buttons.Add(2, groupOptionButton2);
			list.Add(groupOptionButton2);
			GroupOptionButton<int> groupOptionButton3 = CreativePowersHelper.CreateCategoryButton<int>(request, 3, 0);
			groupOptionButton3.Append(CreativePowersHelper.GetIconImage(CreativePowersHelper.CreativePowerIconLocations.TimeCategory));
			groupOptionButton3.OnLeftClick += this.MainCategoryButtonClick;
			groupOptionButton3.OnUpdate += this.timeCategoryButton_OnUpdate;
			mainCategory.Buttons.Add(3, groupOptionButton3);
			list.Add(groupOptionButton3);
			GroupOptionButton<int> groupOptionButton4 = CreativePowersHelper.CreateCategoryButton<int>(request, 4, 0);
			groupOptionButton4.Append(CreativePowersHelper.GetIconImage(CreativePowersHelper.CreativePowerIconLocations.WeatherCategory));
			groupOptionButton4.OnLeftClick += this.MainCategoryButtonClick;
			groupOptionButton4.OnUpdate += this.weatherCategoryButton_OnUpdate;
			mainCategory.Buttons.Add(4, groupOptionButton4);
			list.Add(groupOptionButton4);
			GroupOptionButton<int> groupOptionButton5 = CreativePowersHelper.CreateCategoryButton<int>(request, 6, 0);
			groupOptionButton5.Append(CreativePowersHelper.GetIconImage(CreativePowersHelper.CreativePowerIconLocations.PersonalCategory));
			groupOptionButton5.OnLeftClick += this.MainCategoryButtonClick;
			groupOptionButton5.OnUpdate += this.personalCategoryButton_OnUpdate;
			mainCategory.Buttons.Add(6, groupOptionButton5);
			list.Add(groupOptionButton5);
			CreativePowerManager.Instance.GetPower<CreativePowers.StopBiomeSpreadPower>().ProvidePowerButtons(request, list);
			GroupOptionButton<int> groupOptionButton6 = this.CreateSubcategoryButton<CreativePowers.DifficultySliderPower>(ref request, 1, "strip 1", 5, 0, mainCategory.Buttons, mainCategory.Sliders);
			groupOptionButton6.OnLeftClick += this.MainCategoryButtonClick;
			list.Add(groupOptionButton6);
			return list;
		}

		// Token: 0x06003BA4 RID: 15268 RVA: 0x005B6F8C File Offset: 0x005B518C
		private static void CategoryButton_OnUpdate_DisplayTooltips(UIElement affectedElement, string categoryNameKey)
		{
			GroupOptionButton<int> groupOptionButton = affectedElement as GroupOptionButton<int>;
			if (affectedElement.IsMouseHovering)
			{
				string originalText = Language.GetTextValue(groupOptionButton.IsSelected ? (categoryNameKey + "Opened") : (categoryNameKey + "Closed"));
				CreativePowersHelper.AddDescriptionIfNeeded(ref originalText, categoryNameKey);
				Main.instance.MouseTextNoOverride(originalText, 0, 0, -1, -1, -1, -1, 0);
			}
		}

		// Token: 0x06003BA5 RID: 15269 RVA: 0x005B6FE8 File Offset: 0x005B51E8
		private void itemsWindowButton_OnUpdate(UIElement affectedElement)
		{
			UICreativePowersMenu.CategoryButton_OnUpdate_DisplayTooltips(affectedElement, "CreativePowers.InfiniteItemsCategory");
		}

		// Token: 0x06003BA6 RID: 15270 RVA: 0x005B6FF5 File Offset: 0x005B51F5
		private void researchWindowButton_OnUpdate(UIElement affectedElement)
		{
			UICreativePowersMenu.CategoryButton_OnUpdate_DisplayTooltips(affectedElement, "CreativePowers.ResearchItemsCategory");
		}

		// Token: 0x06003BA7 RID: 15271 RVA: 0x005B7002 File Offset: 0x005B5202
		private void timeCategoryButton_OnUpdate(UIElement affectedElement)
		{
			UICreativePowersMenu.CategoryButton_OnUpdate_DisplayTooltips(affectedElement, "CreativePowers.TimeCategory");
		}

		// Token: 0x06003BA8 RID: 15272 RVA: 0x005B700F File Offset: 0x005B520F
		private void weatherCategoryButton_OnUpdate(UIElement affectedElement)
		{
			UICreativePowersMenu.CategoryButton_OnUpdate_DisplayTooltips(affectedElement, "CreativePowers.WeatherCategory");
		}

		// Token: 0x06003BA9 RID: 15273 RVA: 0x005B701C File Offset: 0x005B521C
		private void personalCategoryButton_OnUpdate(UIElement affectedElement)
		{
			UICreativePowersMenu.CategoryButton_OnUpdate_DisplayTooltips(affectedElement, "CreativePowers.PersonalCategory");
		}

		// Token: 0x06003BAA RID: 15274 RVA: 0x005B7029 File Offset: 0x005B5229
		private void UICreativePowersMenu_OnUpdate(UIElement affectedElement)
		{
			if (this._hovered)
			{
				Main.LocalPlayer.mouseInterface = true;
			}
		}

		// Token: 0x06003BAB RID: 15275 RVA: 0x005B703E File Offset: 0x005B523E
		private void strip_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			this._hovered = false;
		}

		// Token: 0x06003BAC RID: 15276 RVA: 0x005B7047 File Offset: 0x005B5247
		private void strip_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this._hovered = true;
		}

		// Token: 0x06003BAD RID: 15277 RVA: 0x005B7050 File Offset: 0x005B5250
		private void MainCategoryButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			GroupOptionButton<int> groupOptionButton = listeningElement as GroupOptionButton<int>;
			this.ToggleMainCategory(groupOptionButton.OptionValue);
			this.RefreshElementsOrder();
		}

		// Token: 0x06003BAE RID: 15278 RVA: 0x005B7076 File Offset: 0x005B5276
		private void ToggleMainCategory(int option)
		{
			this.ToggleCategory<UICreativePowersMenu.OpenMainSubCategory>(this._mainCategory, option, UICreativePowersMenu.OpenMainSubCategory.None);
		}

		// Token: 0x06003BAF RID: 15279 RVA: 0x005B7086 File Offset: 0x005B5286
		private void ToggleWeatherCategory(int option)
		{
			this.ToggleCategory<UICreativePowersMenu.WeatherSubcategory>(this._weatherCategory, option, UICreativePowersMenu.WeatherSubcategory.None);
		}

		// Token: 0x06003BB0 RID: 15280 RVA: 0x005B7096 File Offset: 0x005B5296
		private void ToggleTimeCategory(int option)
		{
			this.ToggleCategory<UICreativePowersMenu.TimeSubcategory>(this._timeCategory, option, UICreativePowersMenu.TimeSubcategory.None);
		}

		// Token: 0x06003BB1 RID: 15281 RVA: 0x005B70A6 File Offset: 0x005B52A6
		private void TogglePersonalCategory(int option)
		{
			this.ToggleCategory<UICreativePowersMenu.PersonalSubcategory>(this._personalCategory, option, UICreativePowersMenu.PersonalSubcategory.None);
		}

		// Token: 0x06003BB2 RID: 15282 RVA: 0x005B70B6 File Offset: 0x005B52B6
		public void SacrificeWhatsInResearchMenu()
		{
			this._infiniteItemsWindow.SacrificeWhatYouCan();
		}

		// Token: 0x06003BB3 RID: 15283 RVA: 0x005B70C3 File Offset: 0x005B52C3
		public void StopPlayingResearchAnimations()
		{
			this._infiniteItemsWindow.StopPlayingAnimation();
		}

		// Token: 0x06003BB4 RID: 15284 RVA: 0x005B70D0 File Offset: 0x005B52D0
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

		// Token: 0x06003BB5 RID: 15285 RVA: 0x005B7148 File Offset: 0x005B5348
		private List<UIElement> CreateTimePowerStrip()
		{
			UICreativePowersMenu.MenuTree<UICreativePowersMenu.TimeSubcategory> timeCategory = this._timeCategory;
			List<UIElement> list = new List<UIElement>();
			CreativePowerUIElementRequestInfo request = new CreativePowerUIElementRequestInfo
			{
				PreferredButtonWidth = 40,
				PreferredButtonHeight = 40
			};
			CreativePowerManager.Instance.GetPower<CreativePowers.FreezeTime>().ProvidePowerButtons(request, list);
			CreativePowerManager.Instance.GetPower<CreativePowers.StartDayImmediately>().ProvidePowerButtons(request, list);
			CreativePowerManager.Instance.GetPower<CreativePowers.StartNoonImmediately>().ProvidePowerButtons(request, list);
			CreativePowerManager.Instance.GetPower<CreativePowers.StartNightImmediately>().ProvidePowerButtons(request, list);
			CreativePowerManager.Instance.GetPower<CreativePowers.StartMidnightImmediately>().ProvidePowerButtons(request, list);
			GroupOptionButton<int> groupOptionButton = this.CreateSubcategoryButton<CreativePowers.ModifyTimeRate>(ref request, 2, "strip 2", 1, 0, timeCategory.Buttons, timeCategory.Sliders);
			groupOptionButton.OnLeftClick += this.TimeCategoryButtonClick;
			list.Add(groupOptionButton);
			return list;
		}

		// Token: 0x06003BB6 RID: 15286 RVA: 0x005B7210 File Offset: 0x005B5410
		private List<UIElement> CreatePersonalPowerStrip()
		{
			UICreativePowersMenu.MenuTree<UICreativePowersMenu.PersonalSubcategory> personalCategory = this._personalCategory;
			List<UIElement> list = new List<UIElement>();
			CreativePowerUIElementRequestInfo request = new CreativePowerUIElementRequestInfo
			{
				PreferredButtonWidth = 40,
				PreferredButtonHeight = 40
			};
			CreativePowerManager.Instance.GetPower<CreativePowers.GodmodePower>().ProvidePowerButtons(request, list);
			CreativePowerManager.Instance.GetPower<CreativePowers.FarPlacementRangePower>().ProvidePowerButtons(request, list);
			GroupOptionButton<int> groupOptionButton = this.CreateSubcategoryButton<CreativePowers.SpawnRateSliderPerPlayerPower>(ref request, 2, "strip 2", 1, 0, personalCategory.Buttons, personalCategory.Sliders);
			groupOptionButton.OnLeftClick += this.PersonalCategoryButtonClick;
			list.Add(groupOptionButton);
			return list;
		}

		// Token: 0x06003BB7 RID: 15287 RVA: 0x005B72A4 File Offset: 0x005B54A4
		private List<UIElement> CreateWeatherPowerStrip()
		{
			UICreativePowersMenu.MenuTree<UICreativePowersMenu.WeatherSubcategory> weatherCategory = this._weatherCategory;
			List<UIElement> list = new List<UIElement>();
			CreativePowerUIElementRequestInfo request = new CreativePowerUIElementRequestInfo
			{
				PreferredButtonWidth = 40,
				PreferredButtonHeight = 40
			};
			GroupOptionButton<int> groupOptionButton = this.CreateSubcategoryButton<CreativePowers.ModifyWindDirectionAndStrength>(ref request, 2, "strip 2", 1, 0, weatherCategory.Buttons, weatherCategory.Sliders);
			groupOptionButton.OnLeftClick += this.WeatherCategoryButtonClick;
			list.Add(groupOptionButton);
			CreativePowerManager.Instance.GetPower<CreativePowers.FreezeWindDirectionAndStrength>().ProvidePowerButtons(request, list);
			GroupOptionButton<int> groupOptionButton2 = this.CreateSubcategoryButton<CreativePowers.ModifyRainPower>(ref request, 2, "strip 2", 2, 0, weatherCategory.Buttons, weatherCategory.Sliders);
			groupOptionButton2.OnLeftClick += this.WeatherCategoryButtonClick;
			list.Add(groupOptionButton2);
			CreativePowerManager.Instance.GetPower<CreativePowers.FreezeRainPower>().ProvidePowerButtons(request, list);
			return list;
		}

		// Token: 0x06003BB8 RID: 15288 RVA: 0x005B7370 File Offset: 0x005B5570
		private GroupOptionButton<int> CreateSubcategoryButton<T>(ref CreativePowerUIElementRequestInfo request, int subcategoryDepth, string subcategoryName, int subcategoryIndex, int currentSelectedInSubcategory, Dictionary<int, GroupOptionButton<int>> subcategoryButtons, Dictionary<int, UIElement> slidersSet) where T : ICreativePower, IProvideSliderElement, IPowerSubcategoryElement
		{
			T power = CreativePowerManager.Instance.GetPower<T>();
			UIElement uIElement = power.ProvideSlider();
			uIElement.Left = new StyleDimension((float)(20 + subcategoryDepth * 60), 0f);
			slidersSet[subcategoryIndex] = uIElement;
			uIElement.SetSnapPoint(subcategoryName, 0, new Vector2?(new Vector2(0f, 0.5f)), new Vector2?(new Vector2(28f, 0f)));
			GroupOptionButton<int> groupOptionButton = subcategoryButtons[subcategoryIndex] = power.GetOptionButton(request, subcategoryIndex, currentSelectedInSubcategory);
			CreativePowersHelper.UpdateUnlockStateByPower(power, groupOptionButton, CreativePowersHelper.CommonSelectedColor);
			return groupOptionButton;
		}

		// Token: 0x06003BB9 RID: 15289 RVA: 0x005B7420 File Offset: 0x005B5620
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

		// Token: 0x06003BBA RID: 15290 RVA: 0x005B747C File Offset: 0x005B567C
		private void TimeCategoryButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			GroupOptionButton<int> groupOptionButton = listeningElement as GroupOptionButton<int>;
			if (groupOptionButton.OptionValue != 1 || CreativePowerManager.Instance.GetPower<CreativePowers.ModifyTimeRate>().GetIsUnlocked())
			{
				this.ToggleTimeCategory(groupOptionButton.OptionValue);
				this.RefreshElementsOrder();
			}
		}

		// Token: 0x06003BBB RID: 15291 RVA: 0x005B74BC File Offset: 0x005B56BC
		private void PersonalCategoryButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			GroupOptionButton<int> groupOptionButton = listeningElement as GroupOptionButton<int>;
			if (groupOptionButton.OptionValue != 1 || CreativePowerManager.Instance.GetPower<CreativePowers.SpawnRateSliderPerPlayerPower>().GetIsUnlocked())
			{
				this.TogglePersonalCategory(groupOptionButton.OptionValue);
				this.RefreshElementsOrder();
			}
		}

		// Token: 0x06003BBC RID: 15292 RVA: 0x005B74FC File Offset: 0x005B56FC
		private void RefreshElementsOrder()
		{
			this._container.RemoveAllChildren();
			this._container.Append(this._mainPowerStrip);
			UIElement value = null;
			UICreativePowersMenu.MenuTree<UICreativePowersMenu.OpenMainSubCategory> mainCategory = this._mainCategory;
			if (mainCategory.Sliders.TryGetValue(mainCategory.CurrentOption, out value))
			{
				this._container.Append(value);
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
				if (timeCategory.Sliders.TryGetValue(timeCategory.CurrentOption, out value))
				{
					this._container.Append(value);
				}
			}
			if (mainCategory.CurrentOption == 4)
			{
				this._container.Append(this._weatherPowersStrip);
				UICreativePowersMenu.MenuTree<UICreativePowersMenu.WeatherSubcategory> weatherCategory = this._weatherCategory;
				if (weatherCategory.Sliders.TryGetValue(weatherCategory.CurrentOption, out value))
				{
					this._container.Append(value);
				}
			}
			if (mainCategory.CurrentOption == 6)
			{
				this._container.Append(this._personalPowersStrip);
				UICreativePowersMenu.MenuTree<UICreativePowersMenu.PersonalSubcategory> personalCategory = this._personalCategory;
				if (personalCategory.Sliders.TryGetValue(personalCategory.CurrentOption, out value))
				{
					this._container.Append(value);
				}
			}
		}

		// Token: 0x06003BBD RID: 15293 RVA: 0x005B7664 File Offset: 0x005B5864
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.SetupGamepadPoints(spriteBatch);
		}

		// Token: 0x06003BBE RID: 15294 RVA: 0x005B7674 File Offset: 0x005B5874
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			int currentID = 10000;
			List<SnapPoint> snapPoints = this.GetSnapPoints();
			List<SnapPoint> orderedPointsByCategoryName = this._helper.GetOrderedPointsByCategoryName(snapPoints, "strip 0");
			List<SnapPoint> orderedPointsByCategoryName2 = this._helper.GetOrderedPointsByCategoryName(snapPoints, "strip 1");
			List<SnapPoint> orderedPointsByCategoryName3 = this._helper.GetOrderedPointsByCategoryName(snapPoints, "strip 2");
			UILinkPoint[] array = null;
			UILinkPoint[] array2 = null;
			UILinkPoint[] array3 = null;
			if (orderedPointsByCategoryName.Count > 0)
			{
				array = this._helper.CreateUILinkStripVertical(ref currentID, orderedPointsByCategoryName);
			}
			if (orderedPointsByCategoryName2.Count > 0)
			{
				array2 = this._helper.CreateUILinkStripVertical(ref currentID, orderedPointsByCategoryName2);
			}
			if (orderedPointsByCategoryName3.Count > 0)
			{
				array3 = this._helper.CreateUILinkStripVertical(ref currentID, orderedPointsByCategoryName3);
			}
			if (array != null && array2 != null)
			{
				this._helper.LinkVerticalStrips(array, array2, (array.Length - array2.Length) / 2);
			}
			if (array2 != null && array3 != null)
			{
				this._helper.LinkVerticalStrips(array2, array3, (array.Length - array2.Length) / 2);
			}
			UILinkPoint uILinkPoint2 = null;
			UILinkPoint uILinkPoint3 = null;
			for (int i = 0; i < snapPoints.Count; i++)
			{
				SnapPoint snapPoint = snapPoints[i];
				string name = snapPoint.Name;
				if (!(name == "CreativeSacrificeConfirm"))
				{
					if (name == "CreativeInfinitesSearch")
					{
						uILinkPoint3 = this._helper.MakeLinkPointFromSnapPoint(currentID++, snapPoint);
						Main.CreativeMenu.GamepadPointIdForInfiniteItemSearchHack = uILinkPoint3.ID;
					}
				}
				else
				{
					uILinkPoint2 = this._helper.MakeLinkPointFromSnapPoint(currentID++, snapPoint);
				}
			}
			UILinkPoint uILinkPoint4 = UILinkPointNavigator.Points[15000];
			List<SnapPoint> orderedPointsByCategoryName4 = this._helper.GetOrderedPointsByCategoryName(snapPoints, "CreativeInfinitesFilter");
			if (orderedPointsByCategoryName4.Count > 0)
			{
				UILinkPoint[] array4 = this._helper.CreateUILinkStripHorizontal(ref currentID, orderedPointsByCategoryName4);
				if (uILinkPoint3 != null)
				{
					uILinkPoint3.Up = array4[0].ID;
					for (int j = 0; j < array4.Length; j++)
					{
						array4[j].Down = uILinkPoint3.ID;
					}
				}
			}
			List<SnapPoint> orderedPointsByCategoryName5 = this._helper.GetOrderedPointsByCategoryName(snapPoints, "CreativeInfinitesSlot");
			UILinkPoint[,] array5 = null;
			if (orderedPointsByCategoryName5.Count > 0)
			{
				array5 = this._helper.CreateUILinkPointGrid(ref currentID, orderedPointsByCategoryName5, this._infiniteItemsWindow.GetItemsPerLine(), uILinkPoint3, array[0], null, null);
				this._helper.LinkVerticalStripRightSideToSingle(array, array5[0, 0]);
			}
			else if (uILinkPoint3 != null)
			{
				this._helper.LinkVerticalStripRightSideToSingle(array, uILinkPoint3);
			}
			if (uILinkPoint3 != null && array5 != null)
			{
				this._helper.PairUpDown(uILinkPoint3, array5[0, 0]);
			}
			if (uILinkPoint4 != null && this.IsShowingResearchMenu)
			{
				this._helper.LinkVerticalStripRightSideToSingle(array, uILinkPoint4);
			}
			if (uILinkPoint2 != null)
			{
				this._helper.PairUpDown(uILinkPoint4, uILinkPoint2);
				uILinkPoint2.Left = array[0].ID;
			}
			if (Main.CreativeMenu.GamepadMoveToSearchButtonHack)
			{
				Main.CreativeMenu.GamepadMoveToSearchButtonHack = false;
				if (uILinkPoint3 != null)
				{
					UILinkPointNavigator.ChangePoint(uILinkPoint3.ID);
				}
			}
		}

		// Token: 0x0400552C RID: 21804
		private bool _hovered;

		// Token: 0x0400552D RID: 21805
		private PowerStripUIElement _mainPowerStrip;

		// Token: 0x0400552E RID: 21806
		private PowerStripUIElement _timePowersStrip;

		// Token: 0x0400552F RID: 21807
		private PowerStripUIElement _weatherPowersStrip;

		// Token: 0x04005530 RID: 21808
		private PowerStripUIElement _personalPowersStrip;

		// Token: 0x04005531 RID: 21809
		private UICreativeInfiniteItemsDisplay _infiniteItemsWindow;

		// Token: 0x04005532 RID: 21810
		private UIElement _container;

		// Token: 0x04005533 RID: 21811
		private UICreativePowersMenu.MenuTree<UICreativePowersMenu.OpenMainSubCategory> _mainCategory = new UICreativePowersMenu.MenuTree<UICreativePowersMenu.OpenMainSubCategory>(UICreativePowersMenu.OpenMainSubCategory.None);

		// Token: 0x04005534 RID: 21812
		private UICreativePowersMenu.MenuTree<UICreativePowersMenu.WeatherSubcategory> _weatherCategory = new UICreativePowersMenu.MenuTree<UICreativePowersMenu.WeatherSubcategory>(UICreativePowersMenu.WeatherSubcategory.None);

		// Token: 0x04005535 RID: 21813
		private UICreativePowersMenu.MenuTree<UICreativePowersMenu.TimeSubcategory> _timeCategory = new UICreativePowersMenu.MenuTree<UICreativePowersMenu.TimeSubcategory>(UICreativePowersMenu.TimeSubcategory.None);

		// Token: 0x04005536 RID: 21814
		private UICreativePowersMenu.MenuTree<UICreativePowersMenu.PersonalSubcategory> _personalCategory = new UICreativePowersMenu.MenuTree<UICreativePowersMenu.PersonalSubcategory>(UICreativePowersMenu.PersonalSubcategory.None);

		// Token: 0x04005537 RID: 21815
		private const int INITIAL_LEFT_PIXELS = 20;

		// Token: 0x04005538 RID: 21816
		private const int LEFT_PIXELS_PER_STRIP_DEPTH = 60;

		// Token: 0x04005539 RID: 21817
		private const string STRIP_MAIN = "strip 0";

		// Token: 0x0400553A RID: 21818
		private const string STRIP_DEPTH_1 = "strip 1";

		// Token: 0x0400553B RID: 21819
		private const string STRIP_DEPTH_2 = "strip 2";

		// Token: 0x0400553C RID: 21820
		private UIGamepadHelper _helper;

		// Token: 0x02000BE3 RID: 3043
		private class MenuTree<TEnum> where TEnum : struct, IConvertible
		{
			// Token: 0x06005E2F RID: 24111 RVA: 0x006CB857 File Offset: 0x006C9A57
			public MenuTree(TEnum defaultValue)
			{
				this.CurrentOption = defaultValue.ToInt32(null);
			}

			// Token: 0x04007793 RID: 30611
			public int CurrentOption;

			// Token: 0x04007794 RID: 30612
			public Dictionary<int, GroupOptionButton<int>> Buttons = new Dictionary<int, GroupOptionButton<int>>();

			// Token: 0x04007795 RID: 30613
			public Dictionary<int, UIElement> Sliders = new Dictionary<int, UIElement>();
		}

		// Token: 0x02000BE4 RID: 3044
		private enum OpenMainSubCategory
		{
			// Token: 0x04007797 RID: 30615
			None,
			// Token: 0x04007798 RID: 30616
			InfiniteItems,
			// Token: 0x04007799 RID: 30617
			ResearchWindow,
			// Token: 0x0400779A RID: 30618
			Time,
			// Token: 0x0400779B RID: 30619
			Weather,
			// Token: 0x0400779C RID: 30620
			EnemyStrengthSlider,
			// Token: 0x0400779D RID: 30621
			PersonalPowers
		}

		// Token: 0x02000BE5 RID: 3045
		private enum WeatherSubcategory
		{
			// Token: 0x0400779F RID: 30623
			None,
			// Token: 0x040077A0 RID: 30624
			WindSlider,
			// Token: 0x040077A1 RID: 30625
			RainSlider
		}

		// Token: 0x02000BE6 RID: 3046
		private enum TimeSubcategory
		{
			// Token: 0x040077A3 RID: 30627
			None,
			// Token: 0x040077A4 RID: 30628
			TimeRate
		}

		// Token: 0x02000BE7 RID: 3047
		private enum PersonalSubcategory
		{
			// Token: 0x040077A6 RID: 30630
			None,
			// Token: 0x040077A7 RID: 30631
			EnemySpawnRateSlider
		}
	}
}
