using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.UI;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020004D6 RID: 1238
	public class UIBestiaryTest : UIState
	{
		// Token: 0x06003B0A RID: 15114 RVA: 0x005AFB9C File Offset: 0x005ADD9C
		public UIBestiaryTest(BestiaryDatabase database)
		{
			this._filterer.SetSearchFilterObject<Filters.BySearch>(new Filters.BySearch());
			this._originalEntriesList = new List<BestiaryEntry>(database.Entries);
			this._workingSetEntries = new List<BestiaryEntry>(this._originalEntriesList);
			this._filterer.AddFilters(database.Filters);
			this._sorter.AddSortSteps(database.SortSteps);
			this.BuildPage();
		}

		// Token: 0x06003B0B RID: 15115 RVA: 0x005AFC1F File Offset: 0x005ADE1F
		public void OnOpenPage()
		{
			this.UpdateBestiaryContents();
		}

		// Token: 0x06003B0C RID: 15116 RVA: 0x005AFC28 File Offset: 0x005ADE28
		private void BuildPage()
		{
			base.RemoveAllChildren();
			int num = true.ToInt() * 100;
			UIElement uIElement = new UIElement();
			uIElement.Width.Set(0f, 0.875f);
			uIElement.MaxWidth.Set(800f + (float)num, 0f);
			uIElement.MinWidth.Set(600f + (float)num, 0f);
			uIElement.Top.Set(220f, 0f);
			uIElement.Height.Set(-220f, 1f);
			uIElement.HAlign = 0.5f;
			base.Append(uIElement);
			this.MakeExitButton(uIElement);
			UIPanel uIPanel = new UIPanel();
			uIPanel.Width.Set(0f, 1f);
			uIPanel.Height.Set(-90f, 1f);
			uIPanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
			uIElement.Append(uIPanel);
			uIPanel.PaddingTop -= 4f;
			uIPanel.PaddingBottom -= 4f;
			int num2 = 24;
			UIElement uIElement2 = new UIElement
			{
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension((float)num2, 0f),
				VAlign = 0f
			};
			uIElement2.SetPadding(0f);
			uIPanel.Append(uIElement2);
			UIBestiaryEntryInfoPage uIBestiaryEntryInfoPage = new UIBestiaryEntryInfoPage
			{
				Height = new StyleDimension(12f, 1f),
				HAlign = 1f
			};
			this.AddSortAndFilterButtons(uIElement2, uIBestiaryEntryInfoPage);
			this.AddSearchBar(uIElement2, uIBestiaryEntryInfoPage);
			int num3 = 20;
			UIElement uIElement3 = new UIElement
			{
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension((float)(-(float)num2 - 6 - num3), 1f),
				VAlign = 1f,
				Top = new StyleDimension((float)(-(float)num3), 0f)
			};
			uIElement3.SetPadding(0f);
			uIPanel.Append(uIElement3);
			UIElement uIElement4 = new UIElement
			{
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(20f, 0f),
				VAlign = 1f
			};
			uIPanel.Append(uIElement4);
			uIElement4.SetPadding(0f);
			this.FillProgressBottomBar(uIElement4);
			UIElement uIElement5 = new UIElement
			{
				Width = new StyleDimension(-12f - uIBestiaryEntryInfoPage.Width.Pixels, 1f),
				Height = new StyleDimension(-4f, 1f),
				VAlign = 1f
			};
			uIElement3.Append(uIElement5);
			uIElement5.SetPadding(0f);
			this._bestiarySpace = uIElement5;
			UIBestiaryEntryGrid uIBestiaryEntryGrid = new UIBestiaryEntryGrid(this._workingSetEntries, new UIElement.MouseEvent(this.Click_SelectEntryButton));
			uIElement5.Append(uIBestiaryEntryGrid);
			this._entryGrid = uIBestiaryEntryGrid;
			this._entryGrid.OnGridContentsChanged += this.UpdateBestiaryGridRange;
			uIElement3.Append(uIBestiaryEntryInfoPage);
			this._infoSpace = uIBestiaryEntryInfoPage;
			this.AddBackAndForwardButtons(uIElement2);
			this._sortingGrid = new UIBestiarySortingOptionsGrid(this._sorter);
			this._sortingGrid.OnLeftClick += this.Click_CloseSortingGrid;
			this._sortingGrid.OnClickingOption += this.UpdateBestiaryContents;
			this._filteringGrid = new UIBestiaryFilteringOptionsGrid(this._filterer);
			this._filteringGrid.OnLeftClick += this.Click_CloseFilteringGrid;
			this._filteringGrid.OnClickingOption += this.UpdateBestiaryContents;
			this._filteringGrid.SetupAvailabilityTest(this._originalEntriesList);
			this._searchBar.SetContents(null, true);
			this.UpdateBestiaryContents();
		}

		// Token: 0x06003B0D RID: 15117 RVA: 0x005AFFFC File Offset: 0x005AE1FC
		private void FillProgressBottomBar(UIElement container)
		{
			UIText progressPercentText = new UIText("", 0.8f, false)
			{
				HAlign = 0f,
				VAlign = 1f,
				TextOriginX = 0f,
				TextOriginY = 0f
			};
			this._progressPercentText = progressPercentText;
			UIBestiaryBar uIColoredSliderSimple = new UIBestiaryBar(Main.BestiaryDB)
			{
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(15f, 0f),
				VAlign = 1f
			};
			this._unlocksProgressBar = uIColoredSliderSimple;
			container.Append(uIColoredSliderSimple);
		}

		// Token: 0x06003B0E RID: 15118 RVA: 0x005B009C File Offset: 0x005AE29C
		private void ShowStats_Completion(UIElement element)
		{
			if (element.IsMouseHovering)
			{
				string completionPercentText = this.GetCompletionPercentText();
				Main.instance.MouseText(completionPercentText, 0, 0, -1, -1, -1, -1, 0);
			}
		}

		// Token: 0x06003B0F RID: 15119 RVA: 0x005B00CC File Offset: 0x005AE2CC
		private string GetCompletionPercentText()
		{
			string percent = Utils.PrettifyPercentDisplay(this.GetProgressPercent(), "P2");
			return Language.GetTextValueWith("BestiaryInfo.PercentCollected", new
			{
				Percent = percent
			});
		}

		// Token: 0x06003B10 RID: 15120 RVA: 0x005B00FA File Offset: 0x005AE2FA
		private float GetProgressPercent()
		{
			return this._progressReport.CompletionPercent;
		}

		// Token: 0x06003B11 RID: 15121 RVA: 0x005B0107 File Offset: 0x005AE307
		private void EmptyInteraction(float input)
		{
		}

		// Token: 0x06003B12 RID: 15122 RVA: 0x005B0109 File Offset: 0x005AE309
		private void EmptyInteraction2()
		{
		}

		// Token: 0x06003B13 RID: 15123 RVA: 0x005B010B File Offset: 0x005AE30B
		private Color GetColorAtBlip(float percentile)
		{
			if (percentile < this.GetProgressPercent())
			{
				return new Color(51, 137, 255);
			}
			return new Color(35, 40, 83);
		}

		// Token: 0x06003B14 RID: 15124 RVA: 0x005B0134 File Offset: 0x005AE334
		private void AddBackAndForwardButtons(UIElement innerTopContainer)
		{
			UIImageButton uIImageButton = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Back"));
			uIImageButton.SetHoverImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Border"));
			uIImageButton.SetVisibility(1f, 1f);
			uIImageButton.SetSnapPoint("BackPage", 0, null, null);
			this._entryGrid.MakeButtonGoByOffset(uIImageButton, -1);
			innerTopContainer.Append(uIImageButton);
			UIImageButton uIImageButton2 = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Forward"))
			{
				Left = new StyleDimension(uIImageButton.Width.Pixels + 1f, 0f)
			};
			uIImageButton2.SetHoverImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Border"));
			uIImageButton2.SetVisibility(1f, 1f);
			uIImageButton2.SetSnapPoint("NextPage", 0, null, null);
			this._entryGrid.MakeButtonGoByOffset(uIImageButton2, 1);
			innerTopContainer.Append(uIImageButton2);
			UIPanel uIPanel = new UIPanel
			{
				Left = new StyleDimension(uIImageButton.Width.Pixels + 1f + uIImageButton2.Width.Pixels + 3f, 0f),
				Width = new StyleDimension(135f, 0f),
				Height = new StyleDimension(0f, 1f),
				VAlign = 0.5f
			};
			uIPanel.BackgroundColor = new Color(35, 40, 83);
			uIPanel.BorderColor = new Color(35, 40, 83);
			uIPanel.SetPadding(0f);
			innerTopContainer.Append(uIPanel);
			UIText uIText = new UIText("9000-9999 (9001)", 0.8f, false)
			{
				HAlign = 0.5f,
				VAlign = 0.5f
			};
			uIPanel.Append(uIText);
			this._indexesRangeText = uIText;
		}

		// Token: 0x06003B15 RID: 15125 RVA: 0x005B0318 File Offset: 0x005AE518
		private void AddSortAndFilterButtons(UIElement innerTopContainer, UIBestiaryEntryInfoPage infoSpace)
		{
			int num = 17;
			UIImageButton uIImageButton = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Filtering"))
			{
				Left = new StyleDimension(0f - infoSpace.Width.Pixels - (float)num, 0f),
				HAlign = 1f
			};
			uIImageButton.SetHoverImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Wide_Border"));
			uIImageButton.SetVisibility(1f, 1f);
			uIImageButton.SetSnapPoint("FilterButton", 0, null, null);
			uIImageButton.OnLeftClick += this.OpenOrCloseFilteringGrid;
			innerTopContainer.Append(uIImageButton);
			UIText uIText = new UIText("", 0.8f, false)
			{
				Left = new StyleDimension(34f, 0f),
				Top = new StyleDimension(2f, 0f),
				VAlign = 0.5f,
				TextOriginX = 0f,
				TextOriginY = 0f
			};
			uIImageButton.Append(uIText);
			this._filteringText = uIText;
			UIImageButton uIImageButton2 = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Sorting"))
			{
				Left = new StyleDimension(0f - infoSpace.Width.Pixels - uIImageButton.Width.Pixels - 3f - (float)num, 0f),
				HAlign = 1f
			};
			uIImageButton2.SetHoverImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Wide_Border"));
			uIImageButton2.SetVisibility(1f, 1f);
			uIImageButton2.SetSnapPoint("SortButton", 0, null, null);
			uIImageButton2.OnLeftClick += this.OpenOrCloseSortingOptions;
			innerTopContainer.Append(uIImageButton2);
			UIText uIText2 = new UIText("", 0.8f, false)
			{
				Left = new StyleDimension(34f, 0f),
				Top = new StyleDimension(2f, 0f),
				VAlign = 0.5f,
				TextOriginX = 0f,
				TextOriginY = 0f
			};
			uIImageButton2.Append(uIText2);
			this._sortingText = uIText2;
		}

		// Token: 0x06003B16 RID: 15126 RVA: 0x005B0558 File Offset: 0x005AE758
		private void AddSearchBar(UIElement innerTopContainer, UIBestiaryEntryInfoPage infoSpace)
		{
			UIImageButton uIImageButton = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Search"))
			{
				Left = new StyleDimension(0f - infoSpace.Width.Pixels, 1f),
				VAlign = 0.5f
			};
			uIImageButton.OnLeftClick += this.Click_SearchArea;
			uIImageButton.SetHoverImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Search_Border"));
			uIImageButton.SetVisibility(1f, 1f);
			uIImageButton.SetSnapPoint("SearchButton", 0, null, null);
			innerTopContainer.Append(uIImageButton);
			UIPanel uipanel = new UIPanel();
			uipanel.Left = new StyleDimension(0f - infoSpace.Width.Pixels + uIImageButton.Width.Pixels + 3f, 1f);
			uipanel.Width = new StyleDimension(infoSpace.Width.Pixels - uIImageButton.Width.Pixels - 3f, 0f);
			uipanel.Height = new StyleDimension(0f, 1f);
			uipanel.VAlign = 0.5f;
			UIPanel uipanel2 = uipanel;
			this._searchBoxPanel = uipanel;
			UIPanel uIPanel = uipanel2;
			uIPanel.BackgroundColor = new Color(35, 40, 83);
			uIPanel.BorderColor = new Color(35, 40, 83);
			uIPanel.SetPadding(0f);
			innerTopContainer.Append(uIPanel);
			UISearchBar uisearchBar = new UISearchBar(Language.GetText("UI.PlayerNameSlot"), 0.8f);
			uisearchBar.Width = new StyleDimension(0f, 1f);
			uisearchBar.Height = new StyleDimension(0f, 1f);
			uisearchBar.HAlign = 0f;
			uisearchBar.VAlign = 0.5f;
			uisearchBar.Left = new StyleDimension(0f, 0f);
			uisearchBar.IgnoresMouseInteraction = true;
			UISearchBar uisearchBar2 = uisearchBar;
			this._searchBar = uisearchBar;
			UISearchBar uISearchBar = uisearchBar2;
			uIPanel.OnLeftClick += this.Click_SearchArea;
			uISearchBar.OnContentsChanged += this.OnSearchContentsChanged;
			uIPanel.Append(uISearchBar);
			uISearchBar.OnStartTakingInput += this.OnStartTakingInput;
			uISearchBar.OnEndTakingInput += this.OnEndTakingInput;
			uISearchBar.OnNeedingVirtualKeyboard += this.OpenVirtualKeyboardWhenNeeded;
			UIImageButton uIImageButton2 = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/SearchCancel"))
			{
				HAlign = 1f,
				VAlign = 0.5f,
				Left = new StyleDimension(-2f, 0f)
			};
			uIImageButton2.OnMouseOver += this.searchCancelButton_OnMouseOver;
			uIImageButton2.OnLeftClick += this.searchCancelButton_OnClick;
			uIPanel.Append(uIImageButton2);
		}

		// Token: 0x06003B17 RID: 15127 RVA: 0x005B0814 File Offset: 0x005AEA14
		private void searchCancelButton_OnClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this._searchBar.HasContents)
			{
				this._searchBar.SetContents(null, true);
				SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				return;
			}
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
		}

		// Token: 0x06003B18 RID: 15128 RVA: 0x005B0866 File Offset: 0x005AEA66
		private void searchCancelButton_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
		}

		// Token: 0x06003B19 RID: 15129 RVA: 0x005B0880 File Offset: 0x005AEA80
		private void OpenVirtualKeyboardWhenNeeded()
		{
			int maxInputLength = 40;
			UIVirtualKeyboard uIVirtualKeyboard = new UIVirtualKeyboard(Language.GetText("UI.PlayerNameSlot").Value, this._searchString, new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedSettingName), new Action(this.GoBackHere), 0, true);
			uIVirtualKeyboard.SetMaxInputLength(maxInputLength);
			UserInterface.ActiveInstance.SetState(uIVirtualKeyboard);
		}

		// Token: 0x06003B1A RID: 15130 RVA: 0x005B08D8 File Offset: 0x005AEAD8
		private void OnFinishedSettingName(string name)
		{
			string contents = name.Trim();
			this._searchBar.SetContents(contents, false);
			this.GoBackHere();
		}

		// Token: 0x06003B1B RID: 15131 RVA: 0x005B08FF File Offset: 0x005AEAFF
		private void GoBackHere()
		{
			UserInterface.ActiveInstance.SetState(this);
			this._searchBar.ToggleTakingText();
		}

		// Token: 0x06003B1C RID: 15132 RVA: 0x005B0917 File Offset: 0x005AEB17
		private void OnStartTakingInput()
		{
			this._searchBoxPanel.BorderColor = Main.OurFavoriteColor;
		}

		// Token: 0x06003B1D RID: 15133 RVA: 0x005B0929 File Offset: 0x005AEB29
		private void OnEndTakingInput()
		{
			this._searchBoxPanel.BorderColor = new Color(35, 40, 83);
		}

		// Token: 0x06003B1E RID: 15134 RVA: 0x005B0941 File Offset: 0x005AEB41
		private void OnSearchContentsChanged(string contents)
		{
			this._searchString = contents;
			this._filterer.SetSearchFilter(contents);
			this.UpdateBestiaryContents();
		}

		// Token: 0x06003B1F RID: 15135 RVA: 0x005B095C File Offset: 0x005AEB5C
		private void Click_SearchArea(UIMouseEvent evt, UIElement listeningElement)
		{
			if (evt.Target.Parent != this._searchBoxPanel)
			{
				this._searchBar.ToggleTakingText();
				this._didClickSearchBar = true;
			}
		}

		// Token: 0x06003B20 RID: 15136 RVA: 0x005B0983 File Offset: 0x005AEB83
		public override void LeftClick(UIMouseEvent evt)
		{
			base.LeftClick(evt);
			this.AttemptStoppingUsingSearchbar(evt);
		}

		// Token: 0x06003B21 RID: 15137 RVA: 0x005B0993 File Offset: 0x005AEB93
		public override void RightClick(UIMouseEvent evt)
		{
			base.RightClick(evt);
			this.AttemptStoppingUsingSearchbar(evt);
		}

		// Token: 0x06003B22 RID: 15138 RVA: 0x005B09A3 File Offset: 0x005AEBA3
		private void AttemptStoppingUsingSearchbar(UIMouseEvent evt)
		{
			this._didClickSomething = true;
		}

		// Token: 0x06003B23 RID: 15139 RVA: 0x005B09AC File Offset: 0x005AEBAC
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (this._didClickSomething && !this._didClickSearchBar && this._searchBar.IsWritingText)
			{
				this._searchBar.ToggleTakingText();
			}
			this._didClickSomething = false;
			this._didClickSearchBar = false;
		}

		// Token: 0x06003B24 RID: 15140 RVA: 0x005B09EB File Offset: 0x005AEBEB
		private void FilterEntries()
		{
			this._workingSetEntries.Clear();
			this._workingSetEntries.AddRange(this._originalEntriesList.Where(new Func<BestiaryEntry, bool>(this._filterer.FitsFilter)));
		}

		// Token: 0x06003B25 RID: 15141 RVA: 0x005B0A20 File Offset: 0x005AEC20
		private void SortEntries()
		{
			foreach (BestiaryEntry bestiaryEntry in this._workingSetEntries)
			{
				foreach (IBestiaryInfoElement bestiaryInfoElement in bestiaryEntry.Info)
				{
					IUpdateBeforeSorting updateBeforeSorting = bestiaryInfoElement as IUpdateBeforeSorting;
					if (updateBeforeSorting != null)
					{
						updateBeforeSorting.UpdateBeforeSorting();
					}
				}
			}
			this._workingSetEntries.Sort(this._sorter);
		}

		// Token: 0x06003B26 RID: 15142 RVA: 0x005B0AC4 File Offset: 0x005AECC4
		private void FillBestiarySpaceWithEntries()
		{
			if (this._entryGrid != null && this._entryGrid.Parent != null)
			{
				this.DeselectEntryButton();
				this._progressReport = this.GetUnlockProgress();
				this._entryGrid.FillBestiarySpaceWithEntries();
			}
		}

		// Token: 0x06003B27 RID: 15143 RVA: 0x005B0AF8 File Offset: 0x005AECF8
		public void UpdateBestiaryGridRange()
		{
			this._indexesRangeText.SetText(this._entryGrid.GetRangeText());
		}

		// Token: 0x06003B28 RID: 15144 RVA: 0x005B0B10 File Offset: 0x005AED10
		public override void Recalculate()
		{
			base.Recalculate();
			this.FillBestiarySpaceWithEntries();
		}

		// Token: 0x06003B29 RID: 15145 RVA: 0x005B0B20 File Offset: 0x005AED20
		private void GetEntriesToShow(out int maxEntriesWidth, out int maxEntriesHeight, out int maxEntriesToHave)
		{
			Rectangle rectangle = this._bestiarySpace.GetDimensions().ToRectangle();
			maxEntriesWidth = rectangle.Width / 72;
			maxEntriesHeight = rectangle.Height / 72;
			int num = 0;
			maxEntriesToHave = maxEntriesWidth * maxEntriesHeight - num;
		}

		// Token: 0x06003B2A RID: 15146 RVA: 0x005B0B64 File Offset: 0x005AED64
		private void MakeExitButton(UIElement outerContainer)
		{
			UITextPanel<LocalizedText> uITextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true)
			{
				Width = StyleDimension.FromPixelsAndPercent(-10f, 0.5f),
				Height = StyleDimension.FromPixels(50f),
				VAlign = 1f,
				HAlign = 0.5f,
				Top = StyleDimension.FromPixels(-25f)
			};
			uITextPanel.OnMouseOver += this.FadedMouseOver;
			uITextPanel.OnMouseOut += this.FadedMouseOut;
			uITextPanel.OnLeftMouseDown += this.Click_GoBack;
			uITextPanel.SetSnapPoint("ExitButton", 0, null, null);
			outerContainer.Append(uITextPanel);
		}

		// Token: 0x06003B2B RID: 15147 RVA: 0x005B0C2D File Offset: 0x005AEE2D
		private void Click_GoBack(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			if (Main.gameMenu)
			{
				Main.menuMode = 0;
				return;
			}
			IngameFancyUI.Close();
		}

		// Token: 0x06003B2C RID: 15148 RVA: 0x005B0C58 File Offset: 0x005AEE58
		private void OpenOrCloseSortingOptions(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this._sortingGrid.Parent != null)
			{
				this.CloseSortingGrid();
				return;
			}
			this._bestiarySpace.RemoveChild(this._sortingGrid);
			this._bestiarySpace.RemoveChild(this._filteringGrid);
			this._bestiarySpace.Append(this._sortingGrid);
		}

		// Token: 0x06003B2D RID: 15149 RVA: 0x005B0CAC File Offset: 0x005AEEAC
		private void OpenOrCloseFilteringGrid(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this._filteringGrid.Parent != null)
			{
				this.CloseFilteringGrid();
				return;
			}
			this._bestiarySpace.RemoveChild(this._sortingGrid);
			this._bestiarySpace.RemoveChild(this._filteringGrid);
			this._bestiarySpace.Append(this._filteringGrid);
		}

		// Token: 0x06003B2E RID: 15150 RVA: 0x005B0D00 File Offset: 0x005AEF00
		private void Click_CloseFilteringGrid(UIMouseEvent evt, UIElement listeningElement)
		{
			if (evt.Target == this._filteringGrid)
			{
				this.CloseFilteringGrid();
			}
		}

		// Token: 0x06003B2F RID: 15151 RVA: 0x005B0D16 File Offset: 0x005AEF16
		private void CloseFilteringGrid()
		{
			this.UpdateBestiaryContents();
			this._bestiarySpace.RemoveChild(this._filteringGrid);
		}

		// Token: 0x06003B30 RID: 15152 RVA: 0x005B0D30 File Offset: 0x005AEF30
		private void UpdateBestiaryContents()
		{
			this._filteringGrid.UpdateAvailability();
			this._sortingText.SetText(this._sorter.GetDisplayName());
			this._filteringText.SetText(this._filterer.GetDisplayName());
			this.FilterEntries();
			this.SortEntries();
			this.FillBestiarySpaceWithEntries();
			this._progressReport = this.GetUnlockProgress();
			string completionPercentText = this.GetCompletionPercentText();
			this._progressPercentText.SetText(completionPercentText);
			this._unlocksProgressBar.RecalculateBars();
		}

		// Token: 0x06003B31 RID: 15153 RVA: 0x005B0DB0 File Offset: 0x005AEFB0
		private void Click_CloseSortingGrid(UIMouseEvent evt, UIElement listeningElement)
		{
			if (evt.Target == this._sortingGrid)
			{
				this.CloseSortingGrid();
			}
		}

		// Token: 0x06003B32 RID: 15154 RVA: 0x005B0DC6 File Offset: 0x005AEFC6
		private void CloseSortingGrid()
		{
			this.UpdateBestiaryContents();
			this._bestiarySpace.RemoveChild(this._sortingGrid);
		}

		// Token: 0x06003B33 RID: 15155 RVA: 0x005B0DE0 File Offset: 0x005AEFE0
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06003B34 RID: 15156 RVA: 0x005B0E35 File Offset: 0x005AF035
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06003B35 RID: 15157 RVA: 0x005B0E74 File Offset: 0x005AF074
		private void Click_SelectEntryButton(UIMouseEvent evt, UIElement listeningElement)
		{
			UIBestiaryEntryButton uIBestiaryEntryButton = (UIBestiaryEntryButton)listeningElement;
			if (uIBestiaryEntryButton != null)
			{
				this.SelectEntryButton(uIBestiaryEntryButton);
			}
		}

		// Token: 0x06003B36 RID: 15158 RVA: 0x005B0E94 File Offset: 0x005AF094
		private void SelectEntryButton(UIBestiaryEntryButton button)
		{
			this.DeselectEntryButton();
			this._selectedEntryButton = button;
			this._infoSpace.FillInfoForEntry(button.Entry, new ExtraBestiaryInfoPageInformation
			{
				BestiaryProgressReport = this._progressReport
			});
		}

		// Token: 0x06003B37 RID: 15159 RVA: 0x005B0ED8 File Offset: 0x005AF0D8
		private void DeselectEntryButton()
		{
			this._infoSpace.FillInfoForEntry(null, default(ExtraBestiaryInfoPageInformation));
		}

		// Token: 0x06003B38 RID: 15160 RVA: 0x005B0EFC File Offset: 0x005AF0FC
		public BestiaryUnlockProgressReport GetUnlockProgress()
		{
			float num = 0f;
			int num2 = 0;
			List<BestiaryEntry> originalEntriesList = this._originalEntriesList;
			for (int i = 0; i < originalEntriesList.Count; i++)
			{
				int num3 = (originalEntriesList[i].UIInfoProvider.GetEntryUICollectionInfo().UnlockState > BestiaryEntryUnlockState.NotKnownAtAll_0) ? 1 : 0;
				num2++;
				num += (float)num3;
			}
			return new BestiaryUnlockProgressReport
			{
				EntriesTotal = num2,
				CompletionAmountTotal = num
			};
		}

		// Token: 0x06003B39 RID: 15161 RVA: 0x005B0F6C File Offset: 0x005AF16C
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.SetupGamepadPoints(spriteBatch);
		}

		// Token: 0x06003B3A RID: 15162 RVA: 0x005B0F7C File Offset: 0x005AF17C
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 1;
			int num = 3000;
			int currentID = num;
			List<SnapPoint> snapPoints = this.GetSnapPoints();
			SnapPoint snap = null;
			SnapPoint snap2 = null;
			SnapPoint snap3 = null;
			SnapPoint snap4 = null;
			SnapPoint snap5 = null;
			SnapPoint snap6 = null;
			for (int i = 0; i < snapPoints.Count; i++)
			{
				SnapPoint snapPoint = snapPoints[i];
				string name = snapPoint.Name;
				if (!(name == "BackPage"))
				{
					if (!(name == "NextPage"))
					{
						if (!(name == "ExitButton"))
						{
							if (!(name == "FilterButton"))
							{
								if (!(name == "SortButton"))
								{
									if (name == "SearchButton")
									{
										snap5 = snapPoint;
									}
								}
								else
								{
									snap3 = snapPoint;
								}
							}
							else
							{
								snap4 = snapPoint;
							}
						}
						else
						{
							snap6 = snapPoint;
						}
					}
					else
					{
						snap2 = snapPoint;
					}
				}
				else
				{
					snap = snapPoint;
				}
			}
			UILinkPoint uILinkPoint = this.MakeLinkPointFromSnapPoint(currentID++, snap);
			UILinkPoint uILinkPoint2 = this.MakeLinkPointFromSnapPoint(currentID++, snap2);
			UILinkPoint uILinkPoint3 = this.MakeLinkPointFromSnapPoint(currentID++, snap6);
			UILinkPoint uILinkPoint4 = this.MakeLinkPointFromSnapPoint(currentID++, snap4);
			UILinkPoint uILinkPoint5 = this.MakeLinkPointFromSnapPoint(currentID++, snap3);
			UILinkPoint rightSide = this.MakeLinkPointFromSnapPoint(currentID++, snap5);
			this.PairLeftRight(uILinkPoint, uILinkPoint2);
			this.PairLeftRight(uILinkPoint2, uILinkPoint5);
			this.PairLeftRight(uILinkPoint5, uILinkPoint4);
			this.PairLeftRight(uILinkPoint4, rightSide);
			uILinkPoint3.Up = uILinkPoint2.ID;
			UILinkPoint[,] gridPoints = new UILinkPoint[1, 1];
			if (this._filteringGrid.Parent != null)
			{
				int gridWidth;
				int gridHeight;
				this.SetupPointsForFilterGrid(ref currentID, snapPoints, out gridWidth, out gridHeight, out gridPoints);
				this.PairUpDown(uILinkPoint2, uILinkPoint3);
				this.PairUpDown(uILinkPoint, uILinkPoint3);
				for (int num2 = gridWidth - 1; num2 >= 0; num2--)
				{
					UILinkPoint uILinkPoint6 = gridPoints[num2, gridHeight - 1];
					if (uILinkPoint6 != null)
					{
						this.PairUpDown(uILinkPoint6, uILinkPoint3);
					}
					UILinkPoint uILinkPoint7 = gridPoints[num2, gridHeight - 2];
					if (uILinkPoint7 != null && uILinkPoint6 == null)
					{
						this.PairUpDown(uILinkPoint7, uILinkPoint3);
					}
					UILinkPoint uILinkPoint8 = gridPoints[num2, 0];
					if (uILinkPoint8 != null)
					{
						if (num2 < gridWidth - 3)
						{
							this.PairUpDown(uILinkPoint5, uILinkPoint8);
						}
						else
						{
							this.PairUpDown(uILinkPoint4, uILinkPoint8);
						}
					}
				}
			}
			else if (this._sortingGrid.Parent != null)
			{
				int gridWidth;
				int gridHeight;
				this.SetupPointsForSortingGrid(ref currentID, snapPoints, out gridWidth, out gridHeight, out gridPoints);
				this.PairUpDown(uILinkPoint2, uILinkPoint3);
				this.PairUpDown(uILinkPoint, uILinkPoint3);
				for (int num3 = gridWidth - 1; num3 >= 0; num3--)
				{
					UILinkPoint uILinkPoint9 = gridPoints[num3, gridHeight - 1];
					if (uILinkPoint9 != null)
					{
						this.PairUpDown(uILinkPoint9, uILinkPoint3);
					}
					UILinkPoint uILinkPoint10 = gridPoints[num3, 0];
					if (uILinkPoint10 != null)
					{
						this.PairUpDown(uILinkPoint4, uILinkPoint10);
						this.PairUpDown(uILinkPoint5, uILinkPoint10);
					}
				}
			}
			else if (this._entryGrid.Parent != null)
			{
				int gridWidth;
				int gridHeight;
				this.SetupPointsForEntryGrid(ref currentID, snapPoints, out gridWidth, out gridHeight, out gridPoints);
				for (int j = 0; j < gridWidth; j++)
				{
					if (gridHeight - 1 >= 0)
					{
						UILinkPoint uILinkPoint11 = gridPoints[j, gridHeight - 1];
						if (uILinkPoint11 != null)
						{
							this.PairUpDown(uILinkPoint11, uILinkPoint3);
						}
						if (gridHeight - 2 >= 0)
						{
							UILinkPoint uILinkPoint12 = gridPoints[j, gridHeight - 2];
							if (uILinkPoint12 != null && uILinkPoint11 == null)
							{
								this.PairUpDown(uILinkPoint12, uILinkPoint3);
							}
						}
					}
					UILinkPoint uILinkPoint13 = gridPoints[j, 0];
					if (uILinkPoint13 != null)
					{
						if (j < gridWidth / 2)
						{
							this.PairUpDown(uILinkPoint2, uILinkPoint13);
						}
						else if (j == gridWidth - 1)
						{
							this.PairUpDown(uILinkPoint4, uILinkPoint13);
						}
						else
						{
							this.PairUpDown(uILinkPoint5, uILinkPoint13);
						}
					}
				}
				UILinkPoint uILinkPoint14 = gridPoints[0, 0];
				if (uILinkPoint14 != null)
				{
					this.PairUpDown(uILinkPoint2, uILinkPoint14);
					this.PairUpDown(uILinkPoint, uILinkPoint14);
				}
				else
				{
					this.PairUpDown(uILinkPoint2, uILinkPoint3);
					this.PairUpDown(uILinkPoint, uILinkPoint3);
					this.PairUpDown(uILinkPoint4, uILinkPoint3);
					this.PairUpDown(uILinkPoint5, uILinkPoint3);
				}
			}
			List<UILinkPoint> list = new List<UILinkPoint>();
			for (int k = num; k < currentID; k++)
			{
				list.Add(UILinkPointNavigator.Points[k]);
			}
			if (PlayerInput.UsingGamepadUI && UILinkPointNavigator.CurrentPoint >= currentID)
			{
				this.MoveToVisuallyClosestPoint(list);
			}
		}

		// Token: 0x06003B3B RID: 15163 RVA: 0x005B1380 File Offset: 0x005AF580
		private void MoveToVisuallyClosestPoint(List<UILinkPoint> lostrefpoints)
		{
			Dictionary<int, UILinkPoint> points = UILinkPointNavigator.Points;
			Vector2 mouseScreen = Main.MouseScreen;
			UILinkPoint uILinkPoint = null;
			foreach (UILinkPoint lostrefpoint in lostrefpoints)
			{
				if (uILinkPoint == null || Vector2.Distance(mouseScreen, uILinkPoint.Position) > Vector2.Distance(mouseScreen, lostrefpoint.Position))
				{
					uILinkPoint = lostrefpoint;
				}
			}
			if (uILinkPoint != null)
			{
				UILinkPointNavigator.ChangePoint(uILinkPoint.ID);
			}
		}

		// Token: 0x06003B3C RID: 15164 RVA: 0x005B1404 File Offset: 0x005AF604
		private void SetupPointsForEntryGrid(ref int currentID, List<SnapPoint> pts, out int gridWidth, out int gridHeight, out UILinkPoint[,] gridPoints)
		{
			List<SnapPoint> orderedPointsByCategoryName = UIBestiaryTest.GetOrderedPointsByCategoryName(pts, "Entries");
			int num3;
			this._entryGrid.GetEntriesToShow(out gridWidth, out gridHeight, out num3);
			gridPoints = new UILinkPoint[gridWidth, gridHeight];
			for (int i = 0; i < orderedPointsByCategoryName.Count; i++)
			{
				int num = i % gridWidth;
				int num2 = i / gridWidth;
				UILinkPoint[,] array = gridPoints;
				int num4 = num;
				int num5 = num2;
				num3 = currentID;
				currentID = num3 + 1;
				array[num4, num5] = this.MakeLinkPointFromSnapPoint(num3, orderedPointsByCategoryName[i]);
			}
			for (int j = 0; j < gridWidth; j++)
			{
				for (int k = 0; k < gridHeight; k++)
				{
					UILinkPoint uILinkPoint = gridPoints[j, k];
					if (j < gridWidth - 1)
					{
						UILinkPoint uILinkPoint2 = gridPoints[j + 1, k];
						if (uILinkPoint != null && uILinkPoint2 != null)
						{
							this.PairLeftRight(uILinkPoint, uILinkPoint2);
						}
					}
					if (k < gridHeight - 1)
					{
						UILinkPoint uILinkPoint3 = gridPoints[j, k + 1];
						if (uILinkPoint != null && uILinkPoint3 != null)
						{
							this.PairUpDown(uILinkPoint, uILinkPoint3);
						}
					}
				}
			}
		}

		// Token: 0x06003B3D RID: 15165 RVA: 0x005B1500 File Offset: 0x005AF700
		private void SetupPointsForFilterGrid(ref int currentID, List<SnapPoint> pts, out int gridWidth, out int gridHeight, out UILinkPoint[,] gridPoints)
		{
			List<SnapPoint> orderedPointsByCategoryName = UIBestiaryTest.GetOrderedPointsByCategoryName(pts, "Filters");
			int num3;
			this._filteringGrid.GetEntriesToShow(out gridWidth, out gridHeight, out num3);
			gridPoints = new UILinkPoint[gridWidth, gridHeight];
			for (int i = 0; i < orderedPointsByCategoryName.Count; i++)
			{
				int num = i % gridWidth;
				int num2 = i / gridWidth;
				UILinkPoint[,] array = gridPoints;
				int num4 = num;
				int num5 = num2;
				num3 = currentID;
				currentID = num3 + 1;
				array[num4, num5] = this.MakeLinkPointFromSnapPoint(num3, orderedPointsByCategoryName[i]);
			}
			for (int j = 0; j < gridWidth; j++)
			{
				for (int k = 0; k < gridHeight; k++)
				{
					UILinkPoint uILinkPoint = gridPoints[j, k];
					if (j < gridWidth - 1)
					{
						UILinkPoint uILinkPoint2 = gridPoints[j + 1, k];
						if (uILinkPoint != null && uILinkPoint2 != null)
						{
							this.PairLeftRight(uILinkPoint, uILinkPoint2);
						}
					}
					if (k < gridHeight - 1)
					{
						UILinkPoint uILinkPoint3 = gridPoints[j, k + 1];
						if (uILinkPoint != null && uILinkPoint3 != null)
						{
							this.PairUpDown(uILinkPoint, uILinkPoint3);
						}
					}
				}
			}
		}

		// Token: 0x06003B3E RID: 15166 RVA: 0x005B15FC File Offset: 0x005AF7FC
		private void SetupPointsForSortingGrid(ref int currentID, List<SnapPoint> pts, out int gridWidth, out int gridHeight, out UILinkPoint[,] gridPoints)
		{
			List<SnapPoint> orderedPointsByCategoryName = UIBestiaryTest.GetOrderedPointsByCategoryName(pts, "SortSteps");
			int num3;
			this._sortingGrid.GetEntriesToShow(out gridWidth, out gridHeight, out num3);
			gridPoints = new UILinkPoint[gridWidth, gridHeight];
			for (int i = 0; i < orderedPointsByCategoryName.Count; i++)
			{
				int num = i % gridWidth;
				int num2 = i / gridWidth;
				UILinkPoint[,] array = gridPoints;
				int num4 = num;
				int num5 = num2;
				num3 = currentID;
				currentID = num3 + 1;
				array[num4, num5] = this.MakeLinkPointFromSnapPoint(num3, orderedPointsByCategoryName[i]);
			}
			for (int j = 0; j < gridWidth; j++)
			{
				for (int k = 0; k < gridHeight; k++)
				{
					UILinkPoint uILinkPoint = gridPoints[j, k];
					if (j < gridWidth - 1)
					{
						UILinkPoint uILinkPoint2 = gridPoints[j + 1, k];
						if (uILinkPoint != null && uILinkPoint2 != null)
						{
							this.PairLeftRight(uILinkPoint, uILinkPoint2);
						}
					}
					if (k < gridHeight - 1)
					{
						UILinkPoint uILinkPoint3 = gridPoints[j, k + 1];
						if (uILinkPoint != null && uILinkPoint3 != null)
						{
							this.PairUpDown(uILinkPoint, uILinkPoint3);
						}
					}
				}
			}
		}

		// Token: 0x06003B3F RID: 15167 RVA: 0x005B16F8 File Offset: 0x005AF8F8
		private static List<SnapPoint> GetOrderedPointsByCategoryName(List<SnapPoint> pts, string name)
		{
			return (from x in pts
			where x.Name == name
			orderby x.Id
			select x).ToList<SnapPoint>();
		}

		// Token: 0x06003B40 RID: 15168 RVA: 0x005B174D File Offset: 0x005AF94D
		private void PairLeftRight(UILinkPoint leftSide, UILinkPoint rightSide)
		{
			leftSide.Right = rightSide.ID;
			rightSide.Left = leftSide.ID;
		}

		// Token: 0x06003B41 RID: 15169 RVA: 0x005B1767 File Offset: 0x005AF967
		private void PairUpDown(UILinkPoint upSide, UILinkPoint downSide)
		{
			upSide.Down = downSide.ID;
			downSide.Up = upSide.ID;
		}

		// Token: 0x06003B42 RID: 15170 RVA: 0x005B1781 File Offset: 0x005AF981
		private UILinkPoint MakeLinkPointFromSnapPoint(int id, SnapPoint snap)
		{
			UILinkPointNavigator.SetPosition(id, snap.Position);
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[id];
			uilinkPoint.Unlink();
			return uilinkPoint;
		}

		// Token: 0x06003B43 RID: 15171 RVA: 0x005B17A0 File Offset: 0x005AF9A0
		public override void ScrollWheel(UIScrollWheelEvent evt)
		{
			base.ScrollWheel(evt);
			this._infoSpace.UpdateScrollbar(evt.ScrollWheelValue);
		}

		// Token: 0x06003B44 RID: 15172 RVA: 0x005B17BA File Offset: 0x005AF9BA
		public void TryMovingPages(int direction)
		{
			this._entryGrid.OffsetLibraryByPages(direction);
		}

		// Token: 0x040054ED RID: 21741
		private UIElement _bestiarySpace;

		// Token: 0x040054EE RID: 21742
		private UIBestiaryEntryInfoPage _infoSpace;

		// Token: 0x040054EF RID: 21743
		private UIBestiaryEntryButton _selectedEntryButton;

		// Token: 0x040054F0 RID: 21744
		private List<BestiaryEntry> _originalEntriesList;

		// Token: 0x040054F1 RID: 21745
		private List<BestiaryEntry> _workingSetEntries;

		// Token: 0x040054F2 RID: 21746
		private UIText _indexesRangeText;

		// Token: 0x040054F3 RID: 21747
		private EntryFilterer<BestiaryEntry, IBestiaryEntryFilter> _filterer = new EntryFilterer<BestiaryEntry, IBestiaryEntryFilter>();

		// Token: 0x040054F4 RID: 21748
		private EntrySorter<BestiaryEntry, IBestiarySortStep> _sorter = new EntrySorter<BestiaryEntry, IBestiarySortStep>();

		// Token: 0x040054F5 RID: 21749
		private UIBestiaryEntryGrid _entryGrid;

		// Token: 0x040054F6 RID: 21750
		private UIBestiarySortingOptionsGrid _sortingGrid;

		// Token: 0x040054F7 RID: 21751
		private UIBestiaryFilteringOptionsGrid _filteringGrid;

		// Token: 0x040054F8 RID: 21752
		private UISearchBar _searchBar;

		// Token: 0x040054F9 RID: 21753
		private UIPanel _searchBoxPanel;

		// Token: 0x040054FA RID: 21754
		private UIText _sortingText;

		// Token: 0x040054FB RID: 21755
		private UIText _filteringText;

		// Token: 0x040054FC RID: 21756
		private string _searchString;

		// Token: 0x040054FD RID: 21757
		private BestiaryUnlockProgressReport _progressReport;

		// Token: 0x040054FE RID: 21758
		private UIText _progressPercentText;

		// Token: 0x040054FF RID: 21759
		private UIBestiaryBar _unlocksProgressBar;

		// Token: 0x04005500 RID: 21760
		private bool _didClickSomething;

		// Token: 0x04005501 RID: 21761
		private bool _didClickSearchBar;
	}
}
