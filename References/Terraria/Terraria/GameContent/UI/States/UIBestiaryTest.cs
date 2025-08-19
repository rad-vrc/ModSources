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
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x02000347 RID: 839
	public class UIBestiaryTest : UIState
	{
		// Token: 0x060025D2 RID: 9682 RVA: 0x00570044 File Offset: 0x0056E244
		public UIBestiaryTest(BestiaryDatabase database)
		{
			this._filterer.SetSearchFilterObject<Filters.BySearch>(new Filters.BySearch());
			this._originalEntriesList = new List<BestiaryEntry>(database.Entries);
			this._workingSetEntries = new List<BestiaryEntry>(this._originalEntriesList);
			this._filterer.AddFilters(database.Filters);
			this._sorter.AddSortSteps(database.SortSteps);
			this.BuildPage();
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x005700C7 File Offset: 0x0056E2C7
		public void OnOpenPage()
		{
			this.UpdateBestiaryContents();
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x005700D0 File Offset: 0x0056E2D0
		private void BuildPage()
		{
			base.RemoveAllChildren();
			int num = true.ToInt() * 100;
			UIElement uielement = new UIElement();
			uielement.Width.Set(0f, 0.875f);
			uielement.MaxWidth.Set(800f + (float)num, 0f);
			uielement.MinWidth.Set(600f + (float)num, 0f);
			uielement.Top.Set(220f, 0f);
			uielement.Height.Set(-220f, 1f);
			uielement.HAlign = 0.5f;
			base.Append(uielement);
			this.MakeExitButton(uielement);
			UIPanel uipanel = new UIPanel();
			uipanel.Width.Set(0f, 1f);
			uipanel.Height.Set(-90f, 1f);
			uipanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
			uielement.Append(uipanel);
			uipanel.PaddingTop -= 4f;
			uipanel.PaddingBottom -= 4f;
			int num2 = 24;
			UIElement uielement2 = new UIElement
			{
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension((float)num2, 0f),
				VAlign = 0f
			};
			uielement2.SetPadding(0f);
			uipanel.Append(uielement2);
			UIBestiaryEntryInfoPage uibestiaryEntryInfoPage = new UIBestiaryEntryInfoPage
			{
				Height = new StyleDimension(12f, 1f),
				HAlign = 1f
			};
			this.AddSortAndFilterButtons(uielement2, uibestiaryEntryInfoPage);
			this.AddSearchBar(uielement2, uibestiaryEntryInfoPage);
			int num3 = 20;
			UIElement uielement3 = new UIElement
			{
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension((float)(-(float)num2 - 6 - num3), 1f),
				VAlign = 1f,
				Top = new StyleDimension((float)(-(float)num3), 0f)
			};
			uielement3.SetPadding(0f);
			uipanel.Append(uielement3);
			UIElement uielement4 = new UIElement
			{
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(20f, 0f),
				VAlign = 1f
			};
			uipanel.Append(uielement4);
			uielement4.SetPadding(0f);
			this.FillProgressBottomBar(uielement4);
			UIElement uielement5 = new UIElement
			{
				Width = new StyleDimension(-12f - uibestiaryEntryInfoPage.Width.Pixels, 1f),
				Height = new StyleDimension(-4f, 1f),
				VAlign = 1f
			};
			uielement3.Append(uielement5);
			uielement5.SetPadding(0f);
			this._bestiarySpace = uielement5;
			UIBestiaryEntryGrid uibestiaryEntryGrid = new UIBestiaryEntryGrid(this._workingSetEntries, new UIElement.MouseEvent(this.Click_SelectEntryButton));
			uielement5.Append(uibestiaryEntryGrid);
			this._entryGrid = uibestiaryEntryGrid;
			this._entryGrid.OnGridContentsChanged += this.UpdateBestiaryGridRange;
			uielement3.Append(uibestiaryEntryInfoPage);
			this._infoSpace = uibestiaryEntryInfoPage;
			this.AddBackAndForwardButtons(uielement2);
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

		// Token: 0x060025D5 RID: 9685 RVA: 0x005704A4 File Offset: 0x0056E6A4
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
			UIColoredSliderSimple uicoloredSliderSimple = new UIColoredSliderSimple
			{
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(15f, 0f),
				VAlign = 1f,
				FilledColor = new Color(51, 137, 255),
				EmptyColor = new Color(35, 43, 81),
				FillPercent = 0f
			};
			uicoloredSliderSimple.OnUpdate += this.ShowStats_Completion;
			this._unlocksProgressBar = uicoloredSliderSimple;
			container.Append(uicoloredSliderSimple);
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x00570584 File Offset: 0x0056E784
		private void ShowStats_Completion(UIElement element)
		{
			if (!element.IsMouseHovering)
			{
				return;
			}
			string completionPercentText = this.GetCompletionPercentText();
			Main.instance.MouseText(completionPercentText, 0, 0, -1, -1, -1, -1, 0);
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x005705B4 File Offset: 0x0056E7B4
		private string GetCompletionPercentText()
		{
			string percent = Utils.PrettifyPercentDisplay(this.GetProgressPercent(), "P2");
			return Language.GetTextValueWith("BestiaryInfo.PercentCollected", new
			{
				Percent = percent
			});
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x005705E2 File Offset: 0x0056E7E2
		private float GetProgressPercent()
		{
			return this._progressReport.CompletionPercent;
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		private void EmptyInteraction(float input)
		{
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		private void EmptyInteraction2()
		{
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x005705EF File Offset: 0x0056E7EF
		private Color GetColorAtBlip(float percentile)
		{
			if (percentile < this.GetProgressPercent())
			{
				return new Color(51, 137, 255);
			}
			return new Color(35, 40, 83);
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x00570618 File Offset: 0x0056E818
		private void AddBackAndForwardButtons(UIElement innerTopContainer)
		{
			UIImageButton uiimageButton = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Back", 1));
			uiimageButton.SetHoverImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Border", 1));
			uiimageButton.SetVisibility(1f, 1f);
			uiimageButton.SetSnapPoint("BackPage", 0, null, null);
			this._entryGrid.MakeButtonGoByOffset(uiimageButton, -1);
			innerTopContainer.Append(uiimageButton);
			UIImageButton uiimageButton2 = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Forward", 1))
			{
				Left = new StyleDimension(uiimageButton.Width.Pixels + 1f, 0f)
			};
			uiimageButton2.SetHoverImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Border", 1));
			uiimageButton2.SetVisibility(1f, 1f);
			uiimageButton2.SetSnapPoint("NextPage", 0, null, null);
			this._entryGrid.MakeButtonGoByOffset(uiimageButton2, 1);
			innerTopContainer.Append(uiimageButton2);
			UIPanel uipanel = new UIPanel
			{
				Left = new StyleDimension(uiimageButton.Width.Pixels + 1f + uiimageButton2.Width.Pixels + 3f, 0f),
				Width = new StyleDimension(135f, 0f),
				Height = new StyleDimension(0f, 1f),
				VAlign = 0.5f
			};
			uipanel.BackgroundColor = new Color(35, 40, 83);
			uipanel.BorderColor = new Color(35, 40, 83);
			uipanel.SetPadding(0f);
			innerTopContainer.Append(uipanel);
			UIText uitext = new UIText("9000-9999 (9001)", 0.8f, false)
			{
				HAlign = 0.5f,
				VAlign = 0.5f
			};
			uipanel.Append(uitext);
			this._indexesRangeText = uitext;
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x00570800 File Offset: 0x0056EA00
		private void AddSortAndFilterButtons(UIElement innerTopContainer, UIBestiaryEntryInfoPage infoSpace)
		{
			int num = 17;
			UIImageButton uiimageButton = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Filtering", 1))
			{
				Left = new StyleDimension(-infoSpace.Width.Pixels - (float)num, 0f),
				HAlign = 1f
			};
			uiimageButton.SetHoverImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Wide_Border", 1));
			uiimageButton.SetVisibility(1f, 1f);
			uiimageButton.SetSnapPoint("FilterButton", 0, null, null);
			uiimageButton.OnLeftClick += this.OpenOrCloseFilteringGrid;
			innerTopContainer.Append(uiimageButton);
			UIText uitext = new UIText("", 0.8f, false)
			{
				Left = new StyleDimension(34f, 0f),
				Top = new StyleDimension(2f, 0f),
				VAlign = 0.5f,
				TextOriginX = 0f,
				TextOriginY = 0f
			};
			uiimageButton.Append(uitext);
			this._filteringText = uitext;
			UIImageButton uiimageButton2 = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Sorting", 1))
			{
				Left = new StyleDimension(-infoSpace.Width.Pixels - uiimageButton.Width.Pixels - 3f - (float)num, 0f),
				HAlign = 1f
			};
			uiimageButton2.SetHoverImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Wide_Border", 1));
			uiimageButton2.SetVisibility(1f, 1f);
			uiimageButton2.SetSnapPoint("SortButton", 0, null, null);
			uiimageButton2.OnLeftClick += this.OpenOrCloseSortingOptions;
			innerTopContainer.Append(uiimageButton2);
			UIText uitext2 = new UIText("", 0.8f, false)
			{
				Left = new StyleDimension(34f, 0f),
				Top = new StyleDimension(2f, 0f),
				VAlign = 0.5f,
				TextOriginX = 0f,
				TextOriginY = 0f
			};
			uiimageButton2.Append(uitext2);
			this._sortingText = uitext2;
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x00570A38 File Offset: 0x0056EC38
		private void AddSearchBar(UIElement innerTopContainer, UIBestiaryEntryInfoPage infoSpace)
		{
			UIImageButton uiimageButton = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Search", 1))
			{
				Left = new StyleDimension(-infoSpace.Width.Pixels, 1f),
				VAlign = 0.5f
			};
			uiimageButton.OnLeftClick += this.Click_SearchArea;
			uiimageButton.SetHoverImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Search_Border", 1));
			uiimageButton.SetVisibility(1f, 1f);
			uiimageButton.SetSnapPoint("SearchButton", 0, null, null);
			innerTopContainer.Append(uiimageButton);
			UIPanel uipanel = new UIPanel
			{
				Left = new StyleDimension(-infoSpace.Width.Pixels + uiimageButton.Width.Pixels + 3f, 1f),
				Width = new StyleDimension(infoSpace.Width.Pixels - uiimageButton.Width.Pixels - 3f, 0f),
				Height = new StyleDimension(0f, 1f),
				VAlign = 0.5f
			};
			this._searchBoxPanel = uipanel;
			uipanel.BackgroundColor = new Color(35, 40, 83);
			uipanel.BorderColor = new Color(35, 40, 83);
			uipanel.SetPadding(0f);
			innerTopContainer.Append(uipanel);
			UISearchBar uisearchBar = new UISearchBar(Language.GetText("UI.PlayerNameSlot"), 0.8f)
			{
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(0f, 1f),
				HAlign = 0f,
				VAlign = 0.5f,
				Left = new StyleDimension(0f, 0f),
				IgnoresMouseInteraction = true
			};
			this._searchBar = uisearchBar;
			uipanel.OnLeftClick += this.Click_SearchArea;
			uisearchBar.OnContentsChanged += this.OnSearchContentsChanged;
			uipanel.Append(uisearchBar);
			uisearchBar.OnStartTakingInput += this.OnStartTakingInput;
			uisearchBar.OnEndTakingInput += this.OnEndTakingInput;
			uisearchBar.OnNeedingVirtualKeyboard += this.OpenVirtualKeyboardWhenNeeded;
			UIImageButton uiimageButton2 = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/SearchCancel", 1))
			{
				HAlign = 1f,
				VAlign = 0.5f,
				Left = new StyleDimension(-2f, 0f)
			};
			uiimageButton2.OnMouseOver += this.searchCancelButton_OnMouseOver;
			uiimageButton2.OnLeftClick += this.searchCancelButton_OnClick;
			uipanel.Append(uiimageButton2);
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x00570CE4 File Offset: 0x0056EEE4
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

		// Token: 0x060025E0 RID: 9696 RVA: 0x00570D36 File Offset: 0x0056EF36
		private void searchCancelButton_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x00570D50 File Offset: 0x0056EF50
		private void OpenVirtualKeyboardWhenNeeded()
		{
			int maxInputLength = 40;
			UIVirtualKeyboard uivirtualKeyboard = new UIVirtualKeyboard(Language.GetText("UI.PlayerNameSlot").Value, this._searchString, new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedSettingName), new Action(this.GoBackHere), 0, true);
			uivirtualKeyboard.SetMaxInputLength(maxInputLength);
			UserInterface.ActiveInstance.SetState(uivirtualKeyboard);
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x00570DA8 File Offset: 0x0056EFA8
		private void OnFinishedSettingName(string name)
		{
			string contents = name.Trim();
			this._searchBar.SetContents(contents, false);
			this.GoBackHere();
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x00570DCF File Offset: 0x0056EFCF
		private void GoBackHere()
		{
			UserInterface.ActiveInstance.SetState(this);
			this._searchBar.ToggleTakingText();
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x00570DE7 File Offset: 0x0056EFE7
		private void OnStartTakingInput()
		{
			this._searchBoxPanel.BorderColor = Main.OurFavoriteColor;
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x00570DF9 File Offset: 0x0056EFF9
		private void OnEndTakingInput()
		{
			this._searchBoxPanel.BorderColor = new Color(35, 40, 83);
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x00570E11 File Offset: 0x0056F011
		private void OnSearchContentsChanged(string contents)
		{
			this._searchString = contents;
			this._filterer.SetSearchFilter(contents);
			this.UpdateBestiaryContents();
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x00570E2C File Offset: 0x0056F02C
		private void Click_SearchArea(UIMouseEvent evt, UIElement listeningElement)
		{
			if (evt.Target.Parent == this._searchBoxPanel)
			{
				return;
			}
			this._searchBar.ToggleTakingText();
			this._didClickSearchBar = true;
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x00570E54 File Offset: 0x0056F054
		public override void LeftClick(UIMouseEvent evt)
		{
			base.LeftClick(evt);
			this.AttemptStoppingUsingSearchbar(evt);
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x00570E64 File Offset: 0x0056F064
		public override void RightClick(UIMouseEvent evt)
		{
			base.RightClick(evt);
			this.AttemptStoppingUsingSearchbar(evt);
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x00570E74 File Offset: 0x0056F074
		private void AttemptStoppingUsingSearchbar(UIMouseEvent evt)
		{
			this._didClickSomething = true;
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x00570E7D File Offset: 0x0056F07D
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

		// Token: 0x060025EC RID: 9708 RVA: 0x00570EBC File Offset: 0x0056F0BC
		private void FilterEntries()
		{
			this._workingSetEntries.Clear();
			this._workingSetEntries.AddRange(this._originalEntriesList.Where(new Func<BestiaryEntry, bool>(this._filterer.FitsFilter)));
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x00570EF0 File Offset: 0x0056F0F0
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

		// Token: 0x060025EE RID: 9710 RVA: 0x00570F94 File Offset: 0x0056F194
		private void FillBestiarySpaceWithEntries()
		{
			if (this._entryGrid == null || this._entryGrid.Parent == null)
			{
				return;
			}
			this.DeselectEntryButton();
			this._progressReport = this.GetUnlockProgress();
			this._entryGrid.FillBestiarySpaceWithEntries();
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x00570FC9 File Offset: 0x0056F1C9
		public void UpdateBestiaryGridRange()
		{
			this._indexesRangeText.SetText(this._entryGrid.GetRangeText());
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x00570FE1 File Offset: 0x0056F1E1
		public override void Recalculate()
		{
			base.Recalculate();
			this.FillBestiarySpaceWithEntries();
		}

		// Token: 0x060025F1 RID: 9713 RVA: 0x00570FF0 File Offset: 0x0056F1F0
		private void GetEntriesToShow(out int maxEntriesWidth, out int maxEntriesHeight, out int maxEntriesToHave)
		{
			Rectangle rectangle = this._bestiarySpace.GetDimensions().ToRectangle();
			maxEntriesWidth = rectangle.Width / 72;
			maxEntriesHeight = rectangle.Height / 72;
			int num = 0;
			maxEntriesToHave = maxEntriesWidth * maxEntriesHeight - num;
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x00571034 File Offset: 0x0056F234
		private void MakeExitButton(UIElement outerContainer)
		{
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true)
			{
				Width = StyleDimension.FromPixelsAndPercent(-10f, 0.5f),
				Height = StyleDimension.FromPixels(50f),
				VAlign = 1f,
				HAlign = 0.5f,
				Top = StyleDimension.FromPixels(-25f)
			};
			uitextPanel.OnMouseOver += this.FadedMouseOver;
			uitextPanel.OnMouseOut += this.FadedMouseOut;
			uitextPanel.OnLeftMouseDown += this.Click_GoBack;
			uitextPanel.SetSnapPoint("ExitButton", 0, null, null);
			outerContainer.Append(uitextPanel);
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x005710FD File Offset: 0x0056F2FD
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

		// Token: 0x060025F4 RID: 9716 RVA: 0x00571128 File Offset: 0x0056F328
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

		// Token: 0x060025F5 RID: 9717 RVA: 0x00571180 File Offset: 0x0056F380
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

		// Token: 0x060025F6 RID: 9718 RVA: 0x005711D7 File Offset: 0x0056F3D7
		private void Click_CloseFilteringGrid(UIMouseEvent evt, UIElement listeningElement)
		{
			if (evt.Target != this._filteringGrid)
			{
				return;
			}
			this.CloseFilteringGrid();
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x005711EE File Offset: 0x0056F3EE
		private void CloseFilteringGrid()
		{
			this.UpdateBestiaryContents();
			this._bestiarySpace.RemoveChild(this._filteringGrid);
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x00571208 File Offset: 0x0056F408
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
			this._unlocksProgressBar.FillPercent = this.GetProgressPercent();
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x0057128E File Offset: 0x0056F48E
		private void Click_CloseSortingGrid(UIMouseEvent evt, UIElement listeningElement)
		{
			if (evt.Target != this._sortingGrid)
			{
				return;
			}
			this.CloseSortingGrid();
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x005712A5 File Offset: 0x0056F4A5
		private void CloseSortingGrid()
		{
			this.UpdateBestiaryContents();
			this._bestiarySpace.RemoveChild(this._sortingGrid);
		}

		// Token: 0x060025FB RID: 9723 RVA: 0x005712C0 File Offset: 0x0056F4C0
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x060025FC RID: 9724 RVA: 0x0056C33D File Offset: 0x0056A53D
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x060025FD RID: 9725 RVA: 0x00571318 File Offset: 0x0056F518
		private void Click_SelectEntryButton(UIMouseEvent evt, UIElement listeningElement)
		{
			UIBestiaryEntryButton uibestiaryEntryButton = (UIBestiaryEntryButton)listeningElement;
			if (uibestiaryEntryButton != null)
			{
				this.SelectEntryButton(uibestiaryEntryButton);
			}
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x00571338 File Offset: 0x0056F538
		private void SelectEntryButton(UIBestiaryEntryButton button)
		{
			this.DeselectEntryButton();
			this._selectedEntryButton = button;
			this._infoSpace.FillInfoForEntry(button.Entry, new ExtraBestiaryInfoPageInformation
			{
				BestiaryProgressReport = this._progressReport
			});
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x0057137C File Offset: 0x0056F57C
		private void DeselectEntryButton()
		{
			this._infoSpace.FillInfoForEntry(null, default(ExtraBestiaryInfoPageInformation));
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x005713A0 File Offset: 0x0056F5A0
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

		// Token: 0x06002601 RID: 9729 RVA: 0x00571410 File Offset: 0x0056F610
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.SetupGamepadPoints(spriteBatch);
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x00571420 File Offset: 0x0056F620
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 1;
			int num = 3000;
			int num2 = num;
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
			UILinkPoint uilinkPoint = this.MakeLinkPointFromSnapPoint(num2++, snap);
			UILinkPoint uilinkPoint2 = this.MakeLinkPointFromSnapPoint(num2++, snap2);
			UILinkPoint uilinkPoint3 = this.MakeLinkPointFromSnapPoint(num2++, snap6);
			UILinkPoint uilinkPoint4 = this.MakeLinkPointFromSnapPoint(num2++, snap4);
			UILinkPoint uilinkPoint5 = this.MakeLinkPointFromSnapPoint(num2++, snap3);
			UILinkPoint rightSide = this.MakeLinkPointFromSnapPoint(num2++, snap5);
			this.PairLeftRight(uilinkPoint, uilinkPoint2);
			this.PairLeftRight(uilinkPoint2, uilinkPoint5);
			this.PairLeftRight(uilinkPoint5, uilinkPoint4);
			this.PairLeftRight(uilinkPoint4, rightSide);
			uilinkPoint3.Up = uilinkPoint2.ID;
			UILinkPoint[,] array = new UILinkPoint[1, 1];
			if (this._filteringGrid.Parent != null)
			{
				int num3;
				int num4;
				this.SetupPointsForFilterGrid(ref num2, snapPoints, out num3, out num4, out array);
				this.PairUpDown(uilinkPoint2, uilinkPoint3);
				this.PairUpDown(uilinkPoint, uilinkPoint3);
				for (int j = num3 - 1; j >= 0; j--)
				{
					UILinkPoint uilinkPoint6 = array[j, num4 - 1];
					if (uilinkPoint6 != null)
					{
						this.PairUpDown(uilinkPoint6, uilinkPoint3);
					}
					UILinkPoint uilinkPoint7 = array[j, num4 - 2];
					if (uilinkPoint7 != null && uilinkPoint6 == null)
					{
						this.PairUpDown(uilinkPoint7, uilinkPoint3);
					}
					UILinkPoint uilinkPoint8 = array[j, 0];
					if (uilinkPoint8 != null)
					{
						if (j < num3 - 3)
						{
							this.PairUpDown(uilinkPoint5, uilinkPoint8);
						}
						else
						{
							this.PairUpDown(uilinkPoint4, uilinkPoint8);
						}
					}
				}
			}
			else if (this._sortingGrid.Parent != null)
			{
				int num3;
				int num4;
				this.SetupPointsForSortingGrid(ref num2, snapPoints, out num3, out num4, out array);
				this.PairUpDown(uilinkPoint2, uilinkPoint3);
				this.PairUpDown(uilinkPoint, uilinkPoint3);
				for (int k = num3 - 1; k >= 0; k--)
				{
					UILinkPoint uilinkPoint9 = array[k, num4 - 1];
					if (uilinkPoint9 != null)
					{
						this.PairUpDown(uilinkPoint9, uilinkPoint3);
					}
					UILinkPoint uilinkPoint10 = array[k, 0];
					if (uilinkPoint10 != null)
					{
						this.PairUpDown(uilinkPoint4, uilinkPoint10);
						this.PairUpDown(uilinkPoint5, uilinkPoint10);
					}
				}
			}
			else if (this._entryGrid.Parent != null)
			{
				int num3;
				int num4;
				this.SetupPointsForEntryGrid(ref num2, snapPoints, out num3, out num4, out array);
				for (int l = 0; l < num3; l++)
				{
					if (num4 - 1 >= 0)
					{
						UILinkPoint uilinkPoint11 = array[l, num4 - 1];
						if (uilinkPoint11 != null)
						{
							this.PairUpDown(uilinkPoint11, uilinkPoint3);
						}
						if (num4 - 2 >= 0)
						{
							UILinkPoint uilinkPoint12 = array[l, num4 - 2];
							if (uilinkPoint12 != null && uilinkPoint11 == null)
							{
								this.PairUpDown(uilinkPoint12, uilinkPoint3);
							}
						}
					}
					UILinkPoint uilinkPoint13 = array[l, 0];
					if (uilinkPoint13 != null)
					{
						if (l < num3 / 2)
						{
							this.PairUpDown(uilinkPoint2, uilinkPoint13);
						}
						else if (l == num3 - 1)
						{
							this.PairUpDown(uilinkPoint4, uilinkPoint13);
						}
						else
						{
							this.PairUpDown(uilinkPoint5, uilinkPoint13);
						}
					}
				}
				UILinkPoint uilinkPoint14 = array[0, 0];
				if (uilinkPoint14 != null)
				{
					this.PairUpDown(uilinkPoint2, uilinkPoint14);
					this.PairUpDown(uilinkPoint, uilinkPoint14);
				}
				else
				{
					this.PairUpDown(uilinkPoint2, uilinkPoint3);
					this.PairUpDown(uilinkPoint, uilinkPoint3);
					this.PairUpDown(uilinkPoint4, uilinkPoint3);
					this.PairUpDown(uilinkPoint5, uilinkPoint3);
				}
			}
			List<UILinkPoint> list = new List<UILinkPoint>();
			for (int m = num; m < num2; m++)
			{
				list.Add(UILinkPointNavigator.Points[m]);
			}
			if (PlayerInput.UsingGamepadUI && UILinkPointNavigator.CurrentPoint >= num2)
			{
				this.MoveToVisuallyClosestPoint(list);
			}
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x00571824 File Offset: 0x0056FA24
		private void MoveToVisuallyClosestPoint(List<UILinkPoint> lostrefpoints)
		{
			Dictionary<int, UILinkPoint> points = UILinkPointNavigator.Points;
			Vector2 mouseScreen = Main.MouseScreen;
			UILinkPoint uilinkPoint = null;
			foreach (UILinkPoint uilinkPoint2 in lostrefpoints)
			{
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

		// Token: 0x06002604 RID: 9732 RVA: 0x005718A8 File Offset: 0x0056FAA8
		private void SetupPointsForEntryGrid(ref int currentID, List<SnapPoint> pts, out int gridWidth, out int gridHeight, out UILinkPoint[,] gridPoints)
		{
			List<SnapPoint> orderedPointsByCategoryName = UIBestiaryTest.GetOrderedPointsByCategoryName(pts, "Entries");
			int num;
			this._entryGrid.GetEntriesToShow(out gridWidth, out gridHeight, out num);
			gridPoints = new UILinkPoint[gridWidth, gridHeight];
			for (int i = 0; i < orderedPointsByCategoryName.Count; i++)
			{
				int num2 = i % gridWidth;
				int num3 = i / gridWidth;
				UILinkPoint[,] array = gridPoints;
				int num4 = num2;
				int num5 = num3;
				int num6 = currentID;
				currentID = num6 + 1;
				array[num4, num5] = this.MakeLinkPointFromSnapPoint(num6, orderedPointsByCategoryName[i]);
			}
			for (int j = 0; j < gridWidth; j++)
			{
				for (int k = 0; k < gridHeight; k++)
				{
					UILinkPoint uilinkPoint = gridPoints[j, k];
					if (j < gridWidth - 1)
					{
						UILinkPoint uilinkPoint2 = gridPoints[j + 1, k];
						if (uilinkPoint != null && uilinkPoint2 != null)
						{
							this.PairLeftRight(uilinkPoint, uilinkPoint2);
						}
					}
					if (k < gridHeight - 1)
					{
						UILinkPoint uilinkPoint3 = gridPoints[j, k + 1];
						if (uilinkPoint != null && uilinkPoint3 != null)
						{
							this.PairUpDown(uilinkPoint, uilinkPoint3);
						}
					}
				}
			}
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x005719A8 File Offset: 0x0056FBA8
		private void SetupPointsForFilterGrid(ref int currentID, List<SnapPoint> pts, out int gridWidth, out int gridHeight, out UILinkPoint[,] gridPoints)
		{
			List<SnapPoint> orderedPointsByCategoryName = UIBestiaryTest.GetOrderedPointsByCategoryName(pts, "Filters");
			int num;
			this._filteringGrid.GetEntriesToShow(out gridWidth, out gridHeight, out num);
			gridPoints = new UILinkPoint[gridWidth, gridHeight];
			for (int i = 0; i < orderedPointsByCategoryName.Count; i++)
			{
				int num2 = i % gridWidth;
				int num3 = i / gridWidth;
				UILinkPoint[,] array = gridPoints;
				int num4 = num2;
				int num5 = num3;
				int num6 = currentID;
				currentID = num6 + 1;
				array[num4, num5] = this.MakeLinkPointFromSnapPoint(num6, orderedPointsByCategoryName[i]);
			}
			for (int j = 0; j < gridWidth; j++)
			{
				for (int k = 0; k < gridHeight; k++)
				{
					UILinkPoint uilinkPoint = gridPoints[j, k];
					if (j < gridWidth - 1)
					{
						UILinkPoint uilinkPoint2 = gridPoints[j + 1, k];
						if (uilinkPoint != null && uilinkPoint2 != null)
						{
							this.PairLeftRight(uilinkPoint, uilinkPoint2);
						}
					}
					if (k < gridHeight - 1)
					{
						UILinkPoint uilinkPoint3 = gridPoints[j, k + 1];
						if (uilinkPoint != null && uilinkPoint3 != null)
						{
							this.PairUpDown(uilinkPoint, uilinkPoint3);
						}
					}
				}
			}
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x00571AA8 File Offset: 0x0056FCA8
		private void SetupPointsForSortingGrid(ref int currentID, List<SnapPoint> pts, out int gridWidth, out int gridHeight, out UILinkPoint[,] gridPoints)
		{
			List<SnapPoint> orderedPointsByCategoryName = UIBestiaryTest.GetOrderedPointsByCategoryName(pts, "SortSteps");
			int num;
			this._sortingGrid.GetEntriesToShow(out gridWidth, out gridHeight, out num);
			gridPoints = new UILinkPoint[gridWidth, gridHeight];
			for (int i = 0; i < orderedPointsByCategoryName.Count; i++)
			{
				int num2 = i % gridWidth;
				int num3 = i / gridWidth;
				UILinkPoint[,] array = gridPoints;
				int num4 = num2;
				int num5 = num3;
				int num6 = currentID;
				currentID = num6 + 1;
				array[num4, num5] = this.MakeLinkPointFromSnapPoint(num6, orderedPointsByCategoryName[i]);
			}
			for (int j = 0; j < gridWidth; j++)
			{
				for (int k = 0; k < gridHeight; k++)
				{
					UILinkPoint uilinkPoint = gridPoints[j, k];
					if (j < gridWidth - 1)
					{
						UILinkPoint uilinkPoint2 = gridPoints[j + 1, k];
						if (uilinkPoint != null && uilinkPoint2 != null)
						{
							this.PairLeftRight(uilinkPoint, uilinkPoint2);
						}
					}
					if (k < gridHeight - 1)
					{
						UILinkPoint uilinkPoint3 = gridPoints[j, k + 1];
						if (uilinkPoint != null && uilinkPoint3 != null)
						{
							this.PairUpDown(uilinkPoint, uilinkPoint3);
						}
					}
				}
			}
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x00571BA8 File Offset: 0x0056FDA8
		private static List<SnapPoint> GetOrderedPointsByCategoryName(List<SnapPoint> pts, string name)
		{
			return (from x in pts
			where x.Name == name
			orderby x.Id
			select x).ToList<SnapPoint>();
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x00571BFD File Offset: 0x0056FDFD
		private void PairLeftRight(UILinkPoint leftSide, UILinkPoint rightSide)
		{
			leftSide.Right = rightSide.ID;
			rightSide.Left = leftSide.ID;
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x00571C17 File Offset: 0x0056FE17
		private void PairUpDown(UILinkPoint upSide, UILinkPoint downSide)
		{
			upSide.Down = downSide.ID;
			downSide.Up = upSide.ID;
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x00571C31 File Offset: 0x0056FE31
		private UILinkPoint MakeLinkPointFromSnapPoint(int id, SnapPoint snap)
		{
			UILinkPointNavigator.SetPosition(id, snap.Position);
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[id];
			uilinkPoint.Unlink();
			return uilinkPoint;
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x00571C50 File Offset: 0x0056FE50
		public override void ScrollWheel(UIScrollWheelEvent evt)
		{
			base.ScrollWheel(evt);
			this._infoSpace.UpdateScrollbar(evt.ScrollWheelValue);
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x00571C6A File Offset: 0x0056FE6A
		public void TryMovingPages(int direction)
		{
			this._entryGrid.OffsetLibraryByPages(direction);
		}

		// Token: 0x04004A09 RID: 18953
		private UIElement _bestiarySpace;

		// Token: 0x04004A0A RID: 18954
		private UIBestiaryEntryInfoPage _infoSpace;

		// Token: 0x04004A0B RID: 18955
		private UIBestiaryEntryButton _selectedEntryButton;

		// Token: 0x04004A0C RID: 18956
		private List<BestiaryEntry> _originalEntriesList;

		// Token: 0x04004A0D RID: 18957
		private List<BestiaryEntry> _workingSetEntries;

		// Token: 0x04004A0E RID: 18958
		private UIText _indexesRangeText;

		// Token: 0x04004A0F RID: 18959
		private EntryFilterer<BestiaryEntry, IBestiaryEntryFilter> _filterer = new EntryFilterer<BestiaryEntry, IBestiaryEntryFilter>();

		// Token: 0x04004A10 RID: 18960
		private EntrySorter<BestiaryEntry, IBestiarySortStep> _sorter = new EntrySorter<BestiaryEntry, IBestiarySortStep>();

		// Token: 0x04004A11 RID: 18961
		private UIBestiaryEntryGrid _entryGrid;

		// Token: 0x04004A12 RID: 18962
		private UIBestiarySortingOptionsGrid _sortingGrid;

		// Token: 0x04004A13 RID: 18963
		private UIBestiaryFilteringOptionsGrid _filteringGrid;

		// Token: 0x04004A14 RID: 18964
		private UISearchBar _searchBar;

		// Token: 0x04004A15 RID: 18965
		private UIPanel _searchBoxPanel;

		// Token: 0x04004A16 RID: 18966
		private UIText _sortingText;

		// Token: 0x04004A17 RID: 18967
		private UIText _filteringText;

		// Token: 0x04004A18 RID: 18968
		private string _searchString;

		// Token: 0x04004A19 RID: 18969
		private BestiaryUnlockProgressReport _progressReport;

		// Token: 0x04004A1A RID: 18970
		private UIText _progressPercentText;

		// Token: 0x04004A1B RID: 18971
		private UIColoredSliderSimple _unlocksProgressBar;

		// Token: 0x04004A1C RID: 18972
		private bool _didClickSomething;

		// Token: 0x04004A1D RID: 18973
		private bool _didClickSearchBar;
	}
}
