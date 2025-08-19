using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.GameContent.UI.States;
using Terraria.Graphics.Renderers;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000358 RID: 856
	public class UICreativeInfiniteItemsDisplay : UIElement
	{
		// Token: 0x06002770 RID: 10096 RVA: 0x0058378C File Offset: 0x0058198C
		public UICreativeInfiniteItemsDisplay(UIState uiStateThatHoldsThis)
		{
			this._parentUIState = uiStateThatHoldsThis;
			this._itemIdsAvailableTotal = new List<int>();
			this._itemIdsAvailableToShow = new List<int>();
			this._filterer = new EntryFilterer<Item, IItemEntryFilter>();
			List<IItemEntryFilter> list = new List<IItemEntryFilter>
			{
				new ItemFilters.Weapon(),
				new ItemFilters.Armor(),
				new ItemFilters.Vanity(),
				new ItemFilters.BuildingBlock(),
				new ItemFilters.Furniture(),
				new ItemFilters.Accessories(),
				new ItemFilters.MiscAccessories(),
				new ItemFilters.Consumables(),
				new ItemFilters.Tools(),
				new ItemFilters.Materials()
			};
			List<IItemEntryFilter> list2 = new List<IItemEntryFilter>();
			list2.AddRange(list);
			list2.Add(new ItemFilters.MiscFallback(list));
			this._filterer.AddFilters(list2);
			this._filterer.SetSearchFilterObject<ItemFilters.BySearch>(new ItemFilters.BySearch());
			this._sorter = new EntrySorter<int, ICreativeItemSortStep>();
			this._sorter.AddSortSteps(new List<ICreativeItemSortStep>
			{
				new SortingSteps.ByCreativeSortingId(),
				new SortingSteps.Alphabetical()
			});
			this._itemIdsAvailableTotal.AddRange(CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId.Keys.ToList<int>());
			this.BuildPage();
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x005838F0 File Offset: 0x00581AF0
		private void BuildPage()
		{
			this._lastCheckedVersionForEdits = -1;
			base.RemoveAllChildren();
			base.SetPadding(0f);
			UIElement uielement = new UIElement
			{
				Width = StyleDimension.Fill,
				Height = StyleDimension.Fill
			};
			uielement.SetPadding(0f);
			this._containerInfinites = uielement;
			UIElement uielement2 = new UIElement
			{
				Width = StyleDimension.Fill,
				Height = StyleDimension.Fill
			};
			uielement2.SetPadding(0f);
			this._containerSacrifice = uielement2;
			this.BuildInfinitesMenuContents(uielement);
			this.BuildSacrificeMenuContents(uielement2);
			this.UpdateContents();
			base.OnUpdate += this.UICreativeInfiniteItemsDisplay_OnUpdate;
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x00583997 File Offset: 0x00581B97
		private void Hover_OnUpdate(UIElement affectedElement)
		{
			if (this._hovered)
			{
				Main.LocalPlayer.mouseInterface = true;
			}
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x005839AC File Offset: 0x00581BAC
		private void Hover_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			this._hovered = false;
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x005839B5 File Offset: 0x00581BB5
		private void Hover_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this._hovered = true;
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x005839BE File Offset: 0x00581BBE
		private static UIPanel CreateBasicPanel()
		{
			UIPanel uipanel = new UIPanel();
			UICreativeInfiniteItemsDisplay.SetBasicSizesForCreativeSacrificeOrInfinitesPanel(uipanel);
			uipanel.BackgroundColor *= 0.8f;
			uipanel.BorderColor *= 0.8f;
			return uipanel;
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x005839F8 File Offset: 0x00581BF8
		private static void SetBasicSizesForCreativeSacrificeOrInfinitesPanel(UIElement element)
		{
			element.Width = new StyleDimension(0f, 1f);
			element.Height = new StyleDimension(-38f, 1f);
			element.Top = new StyleDimension(38f, 0f);
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x00583A44 File Offset: 0x00581C44
		private void BuildInfinitesMenuContents(UIElement totalContainer)
		{
			UIPanel uipanel = UICreativeInfiniteItemsDisplay.CreateBasicPanel();
			totalContainer.Append(uipanel);
			uipanel.OnUpdate += this.Hover_OnUpdate;
			uipanel.OnMouseOver += this.Hover_OnMouseOver;
			uipanel.OnMouseOut += this.Hover_OnMouseOut;
			UIDynamicItemCollection uidynamicItemCollection = new UIDynamicItemCollection();
			this._itemGrid = uidynamicItemCollection;
			UIElement uielement = new UIElement
			{
				Height = new StyleDimension(24f, 0f),
				Width = new StyleDimension(0f, 1f)
			};
			uielement.SetPadding(0f);
			uipanel.Append(uielement);
			this.AddSearchBar(uielement);
			this._searchBar.SetContents(null, true);
			UIList uilist = new UIList
			{
				Width = new StyleDimension(-25f, 1f),
				Height = new StyleDimension(-28f, 1f),
				VAlign = 1f,
				HAlign = 0f
			};
			uipanel.Append(uilist);
			float num = 4f;
			UIScrollbar uiscrollbar = new UIScrollbar
			{
				Height = new StyleDimension(-28f - num * 2f, 1f),
				Top = new StyleDimension(-num, 0f),
				VAlign = 1f,
				HAlign = 1f
			};
			uipanel.Append(uiscrollbar);
			uilist.SetScrollbar(uiscrollbar);
			uilist.Add(uidynamicItemCollection);
			UICreativeItemsInfiniteFilteringOptions uicreativeItemsInfiniteFilteringOptions = new UICreativeItemsInfiniteFilteringOptions(this._filterer, "CreativeInfinitesFilter");
			uicreativeItemsInfiniteFilteringOptions.OnClickingOption += this.filtersHelper_OnClickingOption;
			uicreativeItemsInfiniteFilteringOptions.Left = new StyleDimension(20f, 0f);
			totalContainer.Append(uicreativeItemsInfiniteFilteringOptions);
			uicreativeItemsInfiniteFilteringOptions.OnUpdate += this.Hover_OnUpdate;
			uicreativeItemsInfiniteFilteringOptions.OnMouseOver += this.Hover_OnMouseOver;
			uicreativeItemsInfiniteFilteringOptions.OnMouseOut += this.Hover_OnMouseOut;
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x00583C2C File Offset: 0x00581E2C
		private void BuildSacrificeMenuContents(UIElement totalContainer)
		{
			UIPanel uipanel = UICreativeInfiniteItemsDisplay.CreateBasicPanel();
			uipanel.VAlign = 0.5f;
			uipanel.Height = new StyleDimension(170f, 0f);
			uipanel.Width = new StyleDimension(170f, 0f);
			uipanel.Top = default(StyleDimension);
			totalContainer.Append(uipanel);
			uipanel.OnUpdate += this.Hover_OnUpdate;
			uipanel.OnMouseOver += this.Hover_OnMouseOver;
			uipanel.OnMouseOut += this.Hover_OnMouseOut;
			this.AddCogsForSacrificeMenu(uipanel);
			this._pistonParticleAsset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Research_Spark", 1);
			float pixels = 0f;
			UIImage uiimage = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Creative/Research_Slots", 1))
			{
				HAlign = 0.5f,
				VAlign = 0.5f,
				Top = new StyleDimension(-20f, 0f),
				Left = new StyleDimension(pixels, 0f)
			};
			uipanel.Append(uiimage);
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Research_FramedPistons", 1);
			UIImageFramed uiimageFramed = new UIImageFramed(asset, asset.Frame(1, 9, 0, 0, 0, 0))
			{
				HAlign = 0.5f,
				VAlign = 0.5f,
				Top = new StyleDimension(-20f, 0f),
				Left = new StyleDimension(pixels, 0f),
				IgnoresMouseInteraction = true
			};
			uipanel.Append(uiimageFramed);
			this._sacrificePistons = uiimageFramed;
			UIParticleLayer pistonParticleSystem = new UIParticleLayer
			{
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(0f, 1f),
				AnchorPositionOffsetByPercents = Vector2.One / 2f,
				AnchorPositionOffsetByPixels = Vector2.Zero
			};
			this._pistonParticleSystem = pistonParticleSystem;
			uiimageFramed.Append(this._pistonParticleSystem);
			UIElement uielement = Main.CreativeMenu.ProvideItemSlotElement(0);
			uielement.HAlign = 0.5f;
			uielement.VAlign = 0.5f;
			uielement.Top = new StyleDimension(-15f, 0f);
			uielement.Left = new StyleDimension(pixels, 0f);
			uielement.SetSnapPoint("CreativeSacrificeSlot", 0, null, null);
			uiimage.Append(uielement);
			UIText uitext = new UIText("(0/50)", 0.8f, false)
			{
				Top = new StyleDimension(10f, 0f),
				Left = new StyleDimension(pixels, 0f),
				HAlign = 0.5f,
				VAlign = 0.5f,
				IgnoresMouseInteraction = true
			};
			uitext.OnUpdate += this.descriptionText_OnUpdate;
			uipanel.Append(uitext);
			UIPanel uipanel2 = new UIPanel
			{
				Top = new StyleDimension(0f, 0f),
				Left = new StyleDimension(pixels, 0f),
				HAlign = 0.5f,
				VAlign = 1f,
				Width = new StyleDimension(124f, 0f),
				Height = new StyleDimension(30f, 0f)
			};
			UIText element = new UIText(Language.GetText("CreativePowers.ConfirmInfiniteItemSacrifice"), 0.8f, false)
			{
				IgnoresMouseInteraction = true,
				HAlign = 0.5f,
				VAlign = 0.5f
			};
			uipanel2.Append(element);
			uipanel2.SetSnapPoint("CreativeSacrificeConfirm", 0, null, null);
			uipanel2.OnLeftClick += this.sacrificeButton_OnClick;
			uipanel2.OnMouseOver += this.FadedMouseOver;
			uipanel2.OnMouseOut += this.FadedMouseOut;
			uipanel2.OnUpdate += this.research_OnUpdate;
			uipanel.Append(uipanel2);
			uipanel.OnUpdate += this.sacrificeWindow_OnUpdate;
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x0058402C File Offset: 0x0058222C
		private void research_OnUpdate(UIElement affectedElement)
		{
			if (affectedElement.IsMouseHovering)
			{
				Main.instance.MouseText(Language.GetTextValue("CreativePowers.ResearchButtonTooltip"), 0, 0, -1, -1, -1, -1, 0);
			}
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x0058405C File Offset: 0x0058225C
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x0056C33D File Offset: 0x0056A53D
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x005840B4 File Offset: 0x005822B4
		private void AddCogsForSacrificeMenu(UIElement sacrificesContainer)
		{
			UIElement uielement = new UIElement();
			uielement.IgnoresMouseInteraction = true;
			UICreativeInfiniteItemsDisplay.SetBasicSizesForCreativeSacrificeOrInfinitesPanel(uielement);
			uielement.VAlign = 0.5f;
			uielement.Height = new StyleDimension(170f, 0f);
			uielement.Width = new StyleDimension(280f, 0f);
			uielement.Top = default(StyleDimension);
			uielement.SetPadding(0f);
			sacrificesContainer.Append(uielement);
			Vector2 value = new Vector2(-10f, -10f);
			this.AddSymetricalCogsPair(uielement, new Vector2(22f, 1f) + value, "Images/UI/Creative/Research_GearC", this._sacrificeCogsSmall);
			this.AddSymetricalCogsPair(uielement, new Vector2(1f, 28f) + value, "Images/UI/Creative/Research_GearB", this._sacrificeCogsMedium);
			this.AddSymetricalCogsPair(uielement, new Vector2(5f, 5f) + value, "Images/UI/Creative/Research_GearA", this._sacrificeCogsBig);
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x005841AD File Offset: 0x005823AD
		private void sacrificeWindow_OnUpdate(UIElement affectedElement)
		{
			this.UpdateVisualFrame();
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x005841B8 File Offset: 0x005823B8
		private void UpdateVisualFrame()
		{
			float num = 0.05f;
			float sacrificeAnimationProgress = this.GetSacrificeAnimationProgress();
			float lerpValue = Utils.GetLerpValue(1f, 0.7f, sacrificeAnimationProgress, true);
			float num2 = lerpValue * lerpValue;
			num2 *= 2f;
			float num3 = 1f + num2;
			num *= num3;
			float num4 = 2f;
			float num5 = 1.1428572f;
			float num6 = 1f;
			UICreativeInfiniteItemsDisplay.OffsetRotationsForCogs(num4 * num, this._sacrificeCogsSmall);
			UICreativeInfiniteItemsDisplay.OffsetRotationsForCogs(num5 * num, this._sacrificeCogsMedium);
			UICreativeInfiniteItemsDisplay.OffsetRotationsForCogs(-num6 * num, this._sacrificeCogsBig);
			int frameY = 0;
			if (this._sacrificeAnimationTimeLeft != 0)
			{
				float num7 = 0.1f;
				float num8 = 0.06666667f;
				if (sacrificeAnimationProgress >= 1f - num7)
				{
					frameY = 8;
				}
				else if (sacrificeAnimationProgress >= 1f - num7 * 2f)
				{
					frameY = 7;
				}
				else if (sacrificeAnimationProgress >= 1f - num7 * 3f)
				{
					frameY = 6;
				}
				else if (sacrificeAnimationProgress >= num8 * 4f)
				{
					frameY = 5;
				}
				else if (sacrificeAnimationProgress >= num8 * 3f)
				{
					frameY = 4;
				}
				else if (sacrificeAnimationProgress >= num8 * 2f)
				{
					frameY = 3;
				}
				else if (sacrificeAnimationProgress >= num8)
				{
					frameY = 2;
				}
				else
				{
					frameY = 1;
				}
				if (this._sacrificeAnimationTimeLeft == 56)
				{
					SoundEngine.PlaySound(63, -1, -1, 1, 1f, 0f);
					Vector2 accelerationPerFrame = new Vector2(0f, 0.16350001f);
					for (int i = 0; i < 15; i++)
					{
						Vector2 vector = Main.rand.NextVector2Circular(4f, 3f);
						if (vector.Y > 0f)
						{
							vector.Y = -vector.Y;
						}
						vector.Y -= 2f;
						this._pistonParticleSystem.AddParticle(new CreativeSacrificeParticle(this._pistonParticleAsset, null, vector, Vector2.Zero)
						{
							AccelerationPerFrame = accelerationPerFrame,
							ScaleOffsetPerFrame = -0.016666668f
						});
					}
				}
				if (this._sacrificeAnimationTimeLeft == 40 && this._researchComplete)
				{
					this._researchComplete = false;
					SoundEngine.PlaySound(64, -1, -1, 1, 1f, 0f);
				}
			}
			this._sacrificePistons.SetFrame(1, 9, 0, frameY, 0, 0);
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x005843D8 File Offset: 0x005825D8
		private static void OffsetRotationsForCogs(float rotationOffset, List<UIImage> cogsList)
		{
			cogsList[0].Rotation += rotationOffset;
			cogsList[1].Rotation -= rotationOffset;
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x00584404 File Offset: 0x00582604
		private void AddSymetricalCogsPair(UIElement sacrificesContainer, Vector2 cogOFfsetsInPixels, string assetPath, List<UIImage> imagesList)
		{
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>(assetPath, 1);
			cogOFfsetsInPixels += -asset.Size() / 2f;
			UIImage uiimage = new UIImage(asset)
			{
				NormalizedOrigin = Vector2.One / 2f,
				Left = new StyleDimension(cogOFfsetsInPixels.X, 0f),
				Top = new StyleDimension(cogOFfsetsInPixels.Y, 0f)
			};
			imagesList.Add(uiimage);
			sacrificesContainer.Append(uiimage);
			uiimage = new UIImage(asset)
			{
				NormalizedOrigin = Vector2.One / 2f,
				HAlign = 1f,
				Left = new StyleDimension(-cogOFfsetsInPixels.X, 0f),
				Top = new StyleDimension(cogOFfsetsInPixels.Y, 0f)
			};
			imagesList.Add(uiimage);
			sacrificesContainer.Append(uiimage);
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x005844F8 File Offset: 0x005826F8
		private void descriptionText_OnUpdate(UIElement affectedElement)
		{
			UIText uitext = affectedElement as UIText;
			int num;
			int num2;
			int num3;
			bool sacrificeNumbers = Main.CreativeMenu.GetSacrificeNumbers(out num, out num2, out num3);
			Main.CreativeMenu.ShouldDrawSacrificeArea();
			if (!Main.mouseItem.IsAir)
			{
				this.ForgetItemSacrifice();
			}
			if (num == 0)
			{
				if (this._lastItemIdSacrificed != 0 && this._lastItemAmountWeNeededTotal != this._lastItemAmountWeHad)
				{
					uitext.SetText(string.Format("({0}/{1})", this._lastItemAmountWeHad, this._lastItemAmountWeNeededTotal));
					return;
				}
				uitext.SetText("???");
				return;
			}
			else
			{
				this.ForgetItemSacrifice();
				if (!sacrificeNumbers)
				{
					uitext.SetText("X");
					return;
				}
				uitext.SetText(string.Format("({0}/{1})", num2, num3));
				return;
			}
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x005845B9 File Offset: 0x005827B9
		private void sacrificeButton_OnClick(UIMouseEvent evt, UIElement listeningElement)
		{
			this.SacrificeWhatYouCan();
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x005845C4 File Offset: 0x005827C4
		public void SacrificeWhatYouCan()
		{
			int itemId;
			int num;
			int amountWeNeedTotal;
			Main.CreativeMenu.GetSacrificeNumbers(out itemId, out num, out amountWeNeedTotal);
			int num2;
			CreativeUI.ItemSacrificeResult itemSacrificeResult = Main.CreativeMenu.SacrificeItem(out num2);
			if (itemSacrificeResult != CreativeUI.ItemSacrificeResult.SacrificedButNotDone)
			{
				if (itemSacrificeResult == CreativeUI.ItemSacrificeResult.SacrificedAndDone)
				{
					this._researchComplete = true;
					this.BeginSacrificeAnimation();
					this.RememberItemSacrifice(itemId, num + num2, amountWeNeedTotal);
					return;
				}
			}
			else
			{
				this._researchComplete = false;
				this.BeginSacrificeAnimation();
				this.RememberItemSacrifice(itemId, num + num2, amountWeNeedTotal);
			}
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x0058462B File Offset: 0x0058282B
		public void StopPlayingAnimation()
		{
			this.ForgetItemSacrifice();
			this._sacrificeAnimationTimeLeft = 0;
			this._pistonParticleSystem.ClearParticles();
			this.UpdateVisualFrame();
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x0058464B File Offset: 0x0058284B
		private void RememberItemSacrifice(int itemId, int amountWeHave, int amountWeNeedTotal)
		{
			this._lastItemIdSacrificed = itemId;
			this._lastItemAmountWeHad = amountWeHave;
			this._lastItemAmountWeNeededTotal = amountWeNeedTotal;
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x00584662 File Offset: 0x00582862
		private void ForgetItemSacrifice()
		{
			this._lastItemIdSacrificed = 0;
			this._lastItemAmountWeHad = 0;
			this._lastItemAmountWeNeededTotal = 0;
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x00584679 File Offset: 0x00582879
		private void BeginSacrificeAnimation()
		{
			this._sacrificeAnimationTimeLeft = 60;
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x00584683 File Offset: 0x00582883
		private void UpdateSacrificeAnimation()
		{
			if (this._sacrificeAnimationTimeLeft > 0)
			{
				this._sacrificeAnimationTimeLeft--;
			}
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x0058469C File Offset: 0x0058289C
		private float GetSacrificeAnimationProgress()
		{
			return Utils.GetLerpValue(60f, 0f, (float)this._sacrificeAnimationTimeLeft, true);
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x005846B5 File Offset: 0x005828B5
		public void SetPageTypeToShow(UICreativeInfiniteItemsDisplay.InfiniteItemsDisplayPage page)
		{
			this._showSacrificesInsteadOfInfinites = (page == UICreativeInfiniteItemsDisplay.InfiniteItemsDisplayPage.InfiniteItemsResearch);
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x005846C4 File Offset: 0x005828C4
		private void UICreativeInfiniteItemsDisplay_OnUpdate(UIElement affectedElement)
		{
			base.RemoveAllChildren();
			CreativeUnlocksTracker localPlayerCreativeTracker = Main.LocalPlayerCreativeTracker;
			if (this._lastTrackerCheckedForEdits != localPlayerCreativeTracker)
			{
				this._lastTrackerCheckedForEdits = localPlayerCreativeTracker;
				this._lastCheckedVersionForEdits = -1;
			}
			int lastEditId = localPlayerCreativeTracker.ItemSacrifices.LastEditId;
			if (this._lastCheckedVersionForEdits != lastEditId)
			{
				this._lastCheckedVersionForEdits = lastEditId;
				this.UpdateContents();
			}
			if (this._showSacrificesInsteadOfInfinites)
			{
				base.Append(this._containerSacrifice);
			}
			else
			{
				base.Append(this._containerInfinites);
			}
			this.UpdateSacrificeAnimation();
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x0058473E File Offset: 0x0058293E
		private void filtersHelper_OnClickingOption()
		{
			this.UpdateContents();
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x00584748 File Offset: 0x00582948
		private void UpdateContents()
		{
			this._itemIdsAvailableTotal.Clear();
			Main.LocalPlayerCreativeTracker.ItemSacrifices.FillListOfItemsThatCanBeObtainedInfinitely(this._itemIdsAvailableTotal);
			this._itemIdsAvailableToShow.Clear();
			this._itemIdsAvailableToShow.AddRange(from x in this._itemIdsAvailableTotal
			where this._filterer.FitsFilter(ContentSamples.ItemsByType[x])
			select x);
			this._itemIdsAvailableToShow.Sort(this._sorter);
			this._itemGrid.SetContentsToShow(this._itemIdsAvailableToShow);
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x005847C4 File Offset: 0x005829C4
		private void AddSearchBar(UIElement searchArea)
		{
			UIImageButton uiimageButton = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Search", 1))
			{
				VAlign = 0.5f,
				HAlign = 0f
			};
			uiimageButton.OnLeftClick += this.Click_SearchArea;
			uiimageButton.SetHoverImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Search_Border", 1));
			uiimageButton.SetVisibility(1f, 1f);
			uiimageButton.SetSnapPoint("CreativeInfinitesSearch", 0, null, null);
			searchArea.Append(uiimageButton);
			UIPanel uipanel = new UIPanel
			{
				Width = new StyleDimension(-uiimageButton.Width.Pixels - 3f, 1f),
				Height = new StyleDimension(0f, 1f),
				VAlign = 0.5f,
				HAlign = 1f
			};
			this._searchBoxPanel = uipanel;
			uipanel.BackgroundColor = new Color(35, 40, 83);
			uipanel.BorderColor = new Color(35, 40, 83);
			uipanel.SetPadding(0f);
			searchArea.Append(uipanel);
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
			uisearchBar.OnCanceledTakingInput += this.OnCanceledInput;
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

		// Token: 0x0600278F RID: 10127 RVA: 0x00584A44 File Offset: 0x00582C44
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

		// Token: 0x06002790 RID: 10128 RVA: 0x00570D36 File Offset: 0x0056EF36
		private void searchCancelButton_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x00584A96 File Offset: 0x00582C96
		private void OnCanceledInput()
		{
			Main.LocalPlayer.ToggleInv();
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x00584AA2 File Offset: 0x00582CA2
		private void Click_SearchArea(UIMouseEvent evt, UIElement listeningElement)
		{
			if (evt.Target.Parent == this._searchBoxPanel)
			{
				return;
			}
			this._searchBar.ToggleTakingText();
			this._didClickSearchBar = true;
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x00584ACA File Offset: 0x00582CCA
		public override void LeftClick(UIMouseEvent evt)
		{
			base.LeftClick(evt);
			this.AttemptStoppingUsingSearchbar(evt);
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x00584ADA File Offset: 0x00582CDA
		public override void RightClick(UIMouseEvent evt)
		{
			base.RightClick(evt);
			this.AttemptStoppingUsingSearchbar(evt);
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x00584AEA File Offset: 0x00582CEA
		private void AttemptStoppingUsingSearchbar(UIMouseEvent evt)
		{
			this._didClickSomething = true;
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x00584AF3 File Offset: 0x00582CF3
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

		// Token: 0x06002797 RID: 10135 RVA: 0x00584B32 File Offset: 0x00582D32
		private void OnSearchContentsChanged(string contents)
		{
			this._searchString = contents;
			this._filterer.SetSearchFilter(contents);
			this.UpdateContents();
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x00584B4D File Offset: 0x00582D4D
		private void OnStartTakingInput()
		{
			this._searchBoxPanel.BorderColor = Main.OurFavoriteColor;
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x00584B5F File Offset: 0x00582D5F
		private void OnEndTakingInput()
		{
			this._searchBoxPanel.BorderColor = new Color(35, 40, 83);
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x00584B78 File Offset: 0x00582D78
		private void OpenVirtualKeyboardWhenNeeded()
		{
			int maxInputLength = 40;
			UIVirtualKeyboard uivirtualKeyboard = new UIVirtualKeyboard(Language.GetText("UI.PlayerNameSlot").Value, this._searchString, new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedSettingName), new Action(this.GoBackHere), 3, true);
			uivirtualKeyboard.SetMaxInputLength(maxInputLength);
			uivirtualKeyboard.CustomEscapeAttempt = new Func<bool>(this.EscapeVirtualKeyboard);
			IngameFancyUI.OpenUIState(uivirtualKeyboard);
		}

		// Token: 0x0600279B RID: 10139 RVA: 0x00584BDA File Offset: 0x00582DDA
		private bool EscapeVirtualKeyboard()
		{
			IngameFancyUI.Close();
			Main.playerInventory = true;
			if (this._searchBar.IsWritingText)
			{
				this._searchBar.ToggleTakingText();
			}
			Main.CreativeMenu.ToggleMenu();
			return true;
		}

		// Token: 0x0600279C RID: 10140 RVA: 0x00584C0C File Offset: 0x00582E0C
		private static UserInterface GetCurrentInterface()
		{
			UserInterface result = UserInterface.ActiveInstance;
			if (Main.gameMenu)
			{
				result = Main.MenuUI;
			}
			else
			{
				result = Main.InGameUI;
			}
			return result;
		}

		// Token: 0x0600279D RID: 10141 RVA: 0x00584C38 File Offset: 0x00582E38
		private void OnFinishedSettingName(string name)
		{
			string contents = name.Trim();
			this._searchBar.SetContents(contents, false);
			this.GoBackHere();
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x00584C5F File Offset: 0x00582E5F
		private void GoBackHere()
		{
			IngameFancyUI.Close();
			Main.CreativeMenu.ToggleMenu();
			this._searchBar.ToggleTakingText();
			Main.CreativeMenu.GamepadMoveToSearchButtonHack = true;
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x00584C86 File Offset: 0x00582E86
		public int GetItemsPerLine()
		{
			return this._itemGrid.GetItemsPerLine();
		}

		// Token: 0x04004AE0 RID: 19168
		private List<int> _itemIdsAvailableTotal;

		// Token: 0x04004AE1 RID: 19169
		private List<int> _itemIdsAvailableToShow;

		// Token: 0x04004AE2 RID: 19170
		private CreativeUnlocksTracker _lastTrackerCheckedForEdits;

		// Token: 0x04004AE3 RID: 19171
		private int _lastCheckedVersionForEdits = -1;

		// Token: 0x04004AE4 RID: 19172
		private UISearchBar _searchBar;

		// Token: 0x04004AE5 RID: 19173
		private UIPanel _searchBoxPanel;

		// Token: 0x04004AE6 RID: 19174
		private UIState _parentUIState;

		// Token: 0x04004AE7 RID: 19175
		private string _searchString;

		// Token: 0x04004AE8 RID: 19176
		private UIDynamicItemCollection _itemGrid;

		// Token: 0x04004AE9 RID: 19177
		private EntryFilterer<Item, IItemEntryFilter> _filterer;

		// Token: 0x04004AEA RID: 19178
		private EntrySorter<int, ICreativeItemSortStep> _sorter;

		// Token: 0x04004AEB RID: 19179
		private UIElement _containerInfinites;

		// Token: 0x04004AEC RID: 19180
		private UIElement _containerSacrifice;

		// Token: 0x04004AED RID: 19181
		private bool _showSacrificesInsteadOfInfinites;

		// Token: 0x04004AEE RID: 19182
		public const string SnapPointName_SacrificeSlot = "CreativeSacrificeSlot";

		// Token: 0x04004AEF RID: 19183
		public const string SnapPointName_SacrificeConfirmButton = "CreativeSacrificeConfirm";

		// Token: 0x04004AF0 RID: 19184
		public const string SnapPointName_InfinitesFilter = "CreativeInfinitesFilter";

		// Token: 0x04004AF1 RID: 19185
		public const string SnapPointName_InfinitesSearch = "CreativeInfinitesSearch";

		// Token: 0x04004AF2 RID: 19186
		public const string SnapPointName_InfinitesItemSlot = "CreativeInfinitesSlot";

		// Token: 0x04004AF3 RID: 19187
		private List<UIImage> _sacrificeCogsSmall = new List<UIImage>();

		// Token: 0x04004AF4 RID: 19188
		private List<UIImage> _sacrificeCogsMedium = new List<UIImage>();

		// Token: 0x04004AF5 RID: 19189
		private List<UIImage> _sacrificeCogsBig = new List<UIImage>();

		// Token: 0x04004AF6 RID: 19190
		private UIImageFramed _sacrificePistons;

		// Token: 0x04004AF7 RID: 19191
		private UIParticleLayer _pistonParticleSystem;

		// Token: 0x04004AF8 RID: 19192
		private Asset<Texture2D> _pistonParticleAsset;

		// Token: 0x04004AF9 RID: 19193
		private int _sacrificeAnimationTimeLeft;

		// Token: 0x04004AFA RID: 19194
		private bool _researchComplete;

		// Token: 0x04004AFB RID: 19195
		private bool _hovered;

		// Token: 0x04004AFC RID: 19196
		private int _lastItemIdSacrificed;

		// Token: 0x04004AFD RID: 19197
		private int _lastItemAmountWeHad;

		// Token: 0x04004AFE RID: 19198
		private int _lastItemAmountWeNeededTotal;

		// Token: 0x04004AFF RID: 19199
		private bool _didClickSomething;

		// Token: 0x04004B00 RID: 19200
		private bool _didClickSearchBar;

		// Token: 0x02000746 RID: 1862
		public enum InfiniteItemsDisplayPage
		{
			// Token: 0x04006413 RID: 25619
			InfiniteItemsPickup,
			// Token: 0x04006414 RID: 25620
			InfiniteItemsResearch
		}
	}
}
