using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
	// Token: 0x02000512 RID: 1298
	public class UICreativeInfiniteItemsDisplay : UIElement
	{
		// Token: 0x06003E64 RID: 15972 RVA: 0x005D245C File Offset: 0x005D065C
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

		// Token: 0x06003E65 RID: 15973 RVA: 0x005D25C0 File Offset: 0x005D07C0
		private void BuildPage()
		{
			this._lastCheckedVersionForEdits = -1;
			base.RemoveAllChildren();
			base.SetPadding(0f);
			UIElement uIElement = new UIElement
			{
				Width = StyleDimension.Fill,
				Height = StyleDimension.Fill
			};
			uIElement.SetPadding(0f);
			this._containerInfinites = uIElement;
			UIElement uIElement2 = new UIElement
			{
				Width = StyleDimension.Fill,
				Height = StyleDimension.Fill
			};
			uIElement2.SetPadding(0f);
			this._containerSacrifice = uIElement2;
			this.BuildInfinitesMenuContents(uIElement);
			this.BuildSacrificeMenuContents(uIElement2);
			this.UpdateContents();
			base.OnUpdate += this.UICreativeInfiniteItemsDisplay_OnUpdate;
		}

		// Token: 0x06003E66 RID: 15974 RVA: 0x005D2667 File Offset: 0x005D0867
		private void Hover_OnUpdate(UIElement affectedElement)
		{
			if (this._hovered)
			{
				Main.LocalPlayer.mouseInterface = true;
			}
		}

		// Token: 0x06003E67 RID: 15975 RVA: 0x005D267C File Offset: 0x005D087C
		private void Hover_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			this._hovered = false;
		}

		// Token: 0x06003E68 RID: 15976 RVA: 0x005D2685 File Offset: 0x005D0885
		private void Hover_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this._hovered = true;
		}

		// Token: 0x06003E69 RID: 15977 RVA: 0x005D268E File Offset: 0x005D088E
		private static UIPanel CreateBasicPanel()
		{
			UIPanel uipanel = new UIPanel();
			UICreativeInfiniteItemsDisplay.SetBasicSizesForCreativeSacrificeOrInfinitesPanel(uipanel);
			uipanel.BackgroundColor *= 0.8f;
			uipanel.BorderColor *= 0.8f;
			return uipanel;
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x005D26C8 File Offset: 0x005D08C8
		private static void SetBasicSizesForCreativeSacrificeOrInfinitesPanel(UIElement element)
		{
			element.Width = new StyleDimension(0f, 1f);
			element.Height = new StyleDimension(-38f, 1f);
			element.Top = new StyleDimension(38f, 0f);
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x005D2714 File Offset: 0x005D0914
		private void BuildInfinitesMenuContents(UIElement totalContainer)
		{
			UIPanel uIPanel = UICreativeInfiniteItemsDisplay.CreateBasicPanel();
			totalContainer.Append(uIPanel);
			uIPanel.OnUpdate += this.Hover_OnUpdate;
			uIPanel.OnMouseOver += this.Hover_OnMouseOver;
			uIPanel.OnMouseOut += this.Hover_OnMouseOut;
			UIDynamicItemCollection item = this._itemGrid = new UIDynamicItemCollection();
			UIElement uIElement = new UIElement
			{
				Height = new StyleDimension(24f, 0f),
				Width = new StyleDimension(0f, 1f)
			};
			uIElement.SetPadding(0f);
			uIPanel.Append(uIElement);
			this.AddSearchBar(uIElement);
			this._searchBar.SetContents(null, true);
			UIList uIList = new UIList
			{
				Width = new StyleDimension(-25f, 1f),
				Height = new StyleDimension(-28f, 1f),
				VAlign = 1f,
				HAlign = 0f
			};
			uIPanel.Append(uIList);
			float num = 4f;
			UIScrollbar uIScrollbar = new UIScrollbar
			{
				Height = new StyleDimension(-28f - num * 2f, 1f),
				Top = new StyleDimension(0f - num, 0f),
				VAlign = 1f,
				HAlign = 1f
			};
			uIPanel.Append(uIScrollbar);
			uIList.SetScrollbar(uIScrollbar);
			uIList.Add(item);
			UICreativeItemsInfiniteFilteringOptions uICreativeItemsInfiniteFilteringOptions = new UICreativeItemsInfiniteFilteringOptions(this._filterer, "CreativeInfinitesFilter");
			uICreativeItemsInfiniteFilteringOptions.OnClickingOption += this.filtersHelper_OnClickingOption;
			uICreativeItemsInfiniteFilteringOptions.Left = new StyleDimension(20f, 0f);
			totalContainer.Append(uICreativeItemsInfiniteFilteringOptions);
			uICreativeItemsInfiniteFilteringOptions.OnUpdate += this.Hover_OnUpdate;
			uICreativeItemsInfiniteFilteringOptions.OnMouseOver += this.Hover_OnMouseOver;
			uICreativeItemsInfiniteFilteringOptions.OnMouseOut += this.Hover_OnMouseOut;
		}

		// Token: 0x06003E6C RID: 15980 RVA: 0x005D2908 File Offset: 0x005D0B08
		private void BuildSacrificeMenuContents(UIElement totalContainer)
		{
			UIPanel uIPanel = UICreativeInfiniteItemsDisplay.CreateBasicPanel();
			uIPanel.VAlign = 0.5f;
			uIPanel.Height = new StyleDimension(170f, 0f);
			uIPanel.Width = new StyleDimension(170f, 0f);
			uIPanel.Top = default(StyleDimension);
			totalContainer.Append(uIPanel);
			uIPanel.OnUpdate += this.Hover_OnUpdate;
			uIPanel.OnMouseOver += this.Hover_OnMouseOver;
			uIPanel.OnMouseOut += this.Hover_OnMouseOut;
			this.AddCogsForSacrificeMenu(uIPanel);
			this._pistonParticleAsset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Research_Spark");
			float pixels = 0f;
			UIImage uIImage = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Creative/Research_Slots"))
			{
				HAlign = 0.5f,
				VAlign = 0.5f,
				Top = new StyleDimension(-20f, 0f),
				Left = new StyleDimension(pixels, 0f)
			};
			uIPanel.Append(uIImage);
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Research_FramedPistons");
			UIImageFramed uIImageFramed = new UIImageFramed(asset, asset.Frame(1, 9, 0, 0, 0, 0))
			{
				HAlign = 0.5f,
				VAlign = 0.5f,
				Top = new StyleDimension(-20f, 0f),
				Left = new StyleDimension(pixels, 0f),
				IgnoresMouseInteraction = true
			};
			uIPanel.Append(uIImageFramed);
			this._sacrificePistons = uIImageFramed;
			UIParticleLayer pistonParticleSystem = new UIParticleLayer
			{
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(0f, 1f),
				AnchorPositionOffsetByPercents = Vector2.One / 2f,
				AnchorPositionOffsetByPixels = Vector2.Zero
			};
			this._pistonParticleSystem = pistonParticleSystem;
			uIImageFramed.Append(this._pistonParticleSystem);
			UIElement uIElement = Main.CreativeMenu.ProvideItemSlotElement(0);
			uIElement.HAlign = 0.5f;
			uIElement.VAlign = 0.5f;
			uIElement.Top = new StyleDimension(-15f, 0f);
			uIElement.Left = new StyleDimension(pixels, 0f);
			uIElement.SetSnapPoint("CreativeSacrificeSlot", 0, null, null);
			uIImage.Append(uIElement);
			UIText uIText = new UIText("(0/50)", 0.8f, false)
			{
				Top = new StyleDimension(10f, 0f),
				Left = new StyleDimension(pixels, 0f),
				HAlign = 0.5f,
				VAlign = 0.5f,
				IgnoresMouseInteraction = true
			};
			uIText.OnUpdate += this.descriptionText_OnUpdate;
			uIPanel.Append(uIText);
			UIPanel uIPanel2 = new UIPanel
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
			uIPanel2.Append(element);
			uIPanel2.SetSnapPoint("CreativeSacrificeConfirm", 0, null, null);
			uIPanel2.OnLeftClick += this.sacrificeButton_OnClick;
			uIPanel2.OnMouseOver += this.FadedMouseOver;
			uIPanel2.OnMouseOut += this.FadedMouseOut;
			uIPanel2.OnUpdate += this.research_OnUpdate;
			uIPanel.Append(uIPanel2);
			uIPanel.OnUpdate += this.sacrificeWindow_OnUpdate;
		}

		// Token: 0x06003E6D RID: 15981 RVA: 0x005D2D04 File Offset: 0x005D0F04
		private void research_OnUpdate(UIElement affectedElement)
		{
			if (affectedElement.IsMouseHovering)
			{
				Main.instance.MouseText(Language.GetTextValue("CreativePowers.ResearchButtonTooltip"), 0, 0, -1, -1, -1, -1, 0);
			}
		}

		// Token: 0x06003E6E RID: 15982 RVA: 0x005D2D34 File Offset: 0x005D0F34
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06003E6F RID: 15983 RVA: 0x005D2D89 File Offset: 0x005D0F89
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06003E70 RID: 15984 RVA: 0x005D2DC8 File Offset: 0x005D0FC8
		private void AddCogsForSacrificeMenu(UIElement sacrificesContainer)
		{
			UIElement uIElement = new UIElement();
			uIElement.IgnoresMouseInteraction = true;
			UICreativeInfiniteItemsDisplay.SetBasicSizesForCreativeSacrificeOrInfinitesPanel(uIElement);
			uIElement.VAlign = 0.5f;
			uIElement.Height = new StyleDimension(170f, 0f);
			uIElement.Width = new StyleDimension(280f, 0f);
			uIElement.Top = default(StyleDimension);
			uIElement.SetPadding(0f);
			sacrificesContainer.Append(uIElement);
			Vector2 vector;
			vector..ctor(-10f, -10f);
			this.AddSymetricalCogsPair(uIElement, new Vector2(22f, 1f) + vector, "Images/UI/Creative/Research_GearC", this._sacrificeCogsSmall);
			this.AddSymetricalCogsPair(uIElement, new Vector2(1f, 28f) + vector, "Images/UI/Creative/Research_GearB", this._sacrificeCogsMedium);
			this.AddSymetricalCogsPair(uIElement, new Vector2(5f, 5f) + vector, "Images/UI/Creative/Research_GearA", this._sacrificeCogsBig);
		}

		// Token: 0x06003E71 RID: 15985 RVA: 0x005D2EC1 File Offset: 0x005D10C1
		private void sacrificeWindow_OnUpdate(UIElement affectedElement)
		{
			this.UpdateVisualFrame();
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x005D2ECC File Offset: 0x005D10CC
		private void UpdateVisualFrame()
		{
			float num = 0.05f;
			float sacrificeAnimationProgress = this.GetSacrificeAnimationProgress();
			float lerpValue = Utils.GetLerpValue(1f, 0.7f, sacrificeAnimationProgress, true);
			float num2 = lerpValue * lerpValue;
			num2 *= 2f;
			float num3 = 1f + num2;
			num *= num3;
			float num7 = 1.1428572f;
			float num4 = 1f;
			UICreativeInfiniteItemsDisplay.OffsetRotationsForCogs(2f * num, this._sacrificeCogsSmall);
			UICreativeInfiniteItemsDisplay.OffsetRotationsForCogs(num7 * num, this._sacrificeCogsMedium);
			UICreativeInfiniteItemsDisplay.OffsetRotationsForCogs((0f - num4) * num, this._sacrificeCogsBig);
			int frameY = 0;
			if (this._sacrificeAnimationTimeLeft != 0)
			{
				float num5 = 0.1f;
				float num6 = 0.06666667f;
				frameY = ((sacrificeAnimationProgress >= 1f - num5) ? 8 : ((sacrificeAnimationProgress >= 1f - num5 * 2f) ? 7 : ((sacrificeAnimationProgress >= 1f - num5 * 3f) ? 6 : ((sacrificeAnimationProgress >= num6 * 4f) ? 5 : ((sacrificeAnimationProgress >= num6 * 3f) ? 4 : ((sacrificeAnimationProgress >= num6 * 2f) ? 3 : ((sacrificeAnimationProgress < num6) ? 1 : 2)))))));
				if (this._sacrificeAnimationTimeLeft == 56)
				{
					SoundEngine.PlaySound(63, -1, -1, 1, 1f, 0f);
					Vector2 accelerationPerFrame;
					accelerationPerFrame..ctor(0f, 0.16350001f);
					for (int i = 0; i < 15; i++)
					{
						Vector2 initialVelocity = Main.rand.NextVector2Circular(4f, 3f);
						if (initialVelocity.Y > 0f)
						{
							initialVelocity.Y = 0f - initialVelocity.Y;
						}
						initialVelocity.Y -= 2f;
						this._pistonParticleSystem.AddParticle(new CreativeSacrificeParticle(this._pistonParticleAsset, null, initialVelocity, Vector2.Zero)
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

		// Token: 0x06003E73 RID: 15987 RVA: 0x005D30E2 File Offset: 0x005D12E2
		private static void OffsetRotationsForCogs(float rotationOffset, List<UIImage> cogsList)
		{
			cogsList[0].Rotation += rotationOffset;
			cogsList[1].Rotation -= rotationOffset;
		}

		// Token: 0x06003E74 RID: 15988 RVA: 0x005D310C File Offset: 0x005D130C
		private void AddSymetricalCogsPair(UIElement sacrificesContainer, Vector2 cogOFfsetsInPixels, string assetPath, List<UIImage> imagesList)
		{
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>(assetPath);
			cogOFfsetsInPixels += -asset.Size() / 2f;
			UIImage uIImage = new UIImage(asset)
			{
				NormalizedOrigin = Vector2.One / 2f,
				Left = new StyleDimension(cogOFfsetsInPixels.X, 0f),
				Top = new StyleDimension(cogOFfsetsInPixels.Y, 0f)
			};
			imagesList.Add(uIImage);
			sacrificesContainer.Append(uIImage);
			uIImage = new UIImage(asset)
			{
				NormalizedOrigin = Vector2.One / 2f,
				HAlign = 1f,
				Left = new StyleDimension(0f - cogOFfsetsInPixels.X, 0f),
				Top = new StyleDimension(cogOFfsetsInPixels.Y, 0f)
			};
			imagesList.Add(uIImage);
			sacrificesContainer.Append(uIImage);
		}

		// Token: 0x06003E75 RID: 15989 RVA: 0x005D3204 File Offset: 0x005D1404
		private void descriptionText_OnUpdate(UIElement affectedElement)
		{
			UIText uIText = affectedElement as UIText;
			int itemIdChecked;
			int amountWeHave;
			int amountNeededTotal;
			bool sacrificeNumbers = Main.CreativeMenu.GetSacrificeNumbers(out itemIdChecked, out amountWeHave, out amountNeededTotal);
			Main.CreativeMenu.ShouldDrawSacrificeArea();
			if (!Main.mouseItem.IsAir)
			{
				this.ForgetItemSacrifice();
			}
			if (itemIdChecked == 0)
			{
				if (this._lastItemIdSacrificed != 0 && this._lastItemAmountWeNeededTotal != this._lastItemAmountWeHad)
				{
					UIText uitext = uIText;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
					defaultInterpolatedStringHandler.AppendLiteral("(");
					defaultInterpolatedStringHandler.AppendFormatted<int>(this._lastItemAmountWeHad);
					defaultInterpolatedStringHandler.AppendLiteral("/");
					defaultInterpolatedStringHandler.AppendFormatted<int>(this._lastItemAmountWeNeededTotal);
					defaultInterpolatedStringHandler.AppendLiteral(")");
					uitext.SetText(defaultInterpolatedStringHandler.ToStringAndClear());
					return;
				}
				uIText.SetText("???");
				return;
			}
			else
			{
				this.ForgetItemSacrifice();
				if (!sacrificeNumbers)
				{
					uIText.SetText("X");
					return;
				}
				UIText uitext2 = uIText;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
				defaultInterpolatedStringHandler.AppendLiteral("(");
				defaultInterpolatedStringHandler.AppendFormatted<int>(amountWeHave);
				defaultInterpolatedStringHandler.AppendLiteral("/");
				defaultInterpolatedStringHandler.AppendFormatted<int>(amountNeededTotal);
				defaultInterpolatedStringHandler.AppendLiteral(")");
				uitext2.SetText(defaultInterpolatedStringHandler.ToStringAndClear());
				return;
			}
		}

		// Token: 0x06003E76 RID: 15990 RVA: 0x005D3321 File Offset: 0x005D1521
		private void sacrificeButton_OnClick(UIMouseEvent evt, UIElement listeningElement)
		{
			this.SacrificeWhatYouCan();
		}

		// Token: 0x06003E77 RID: 15991 RVA: 0x005D332C File Offset: 0x005D152C
		public void SacrificeWhatYouCan()
		{
			int itemIdChecked;
			int amountWeHave;
			int amountNeededTotal;
			Main.CreativeMenu.GetSacrificeNumbers(out itemIdChecked, out amountWeHave, out amountNeededTotal);
			int amountWeSacrificed;
			CreativeUI.ItemSacrificeResult itemSacrificeResult = Main.CreativeMenu.SacrificeItem(out amountWeSacrificed);
			if (itemSacrificeResult != CreativeUI.ItemSacrificeResult.SacrificedButNotDone)
			{
				if (itemSacrificeResult == CreativeUI.ItemSacrificeResult.SacrificedAndDone)
				{
					this._researchComplete = true;
					this.BeginSacrificeAnimation();
					this.RememberItemSacrifice(itemIdChecked, amountWeHave + amountWeSacrificed, amountNeededTotal);
					return;
				}
			}
			else
			{
				this._researchComplete = false;
				this.BeginSacrificeAnimation();
				this.RememberItemSacrifice(itemIdChecked, amountWeHave + amountWeSacrificed, amountNeededTotal);
			}
		}

		// Token: 0x06003E78 RID: 15992 RVA: 0x005D3393 File Offset: 0x005D1593
		public void StopPlayingAnimation()
		{
			this.ForgetItemSacrifice();
			this._sacrificeAnimationTimeLeft = 0;
			this._pistonParticleSystem.ClearParticles();
			this.UpdateVisualFrame();
		}

		// Token: 0x06003E79 RID: 15993 RVA: 0x005D33B3 File Offset: 0x005D15B3
		private void RememberItemSacrifice(int itemId, int amountWeHave, int amountWeNeedTotal)
		{
			this._lastItemIdSacrificed = itemId;
			this._lastItemAmountWeHad = amountWeHave;
			this._lastItemAmountWeNeededTotal = amountWeNeedTotal;
		}

		// Token: 0x06003E7A RID: 15994 RVA: 0x005D33CA File Offset: 0x005D15CA
		private void ForgetItemSacrifice()
		{
			this._lastItemIdSacrificed = 0;
			this._lastItemAmountWeHad = 0;
			this._lastItemAmountWeNeededTotal = 0;
		}

		// Token: 0x06003E7B RID: 15995 RVA: 0x005D33E1 File Offset: 0x005D15E1
		private void BeginSacrificeAnimation()
		{
			this._sacrificeAnimationTimeLeft = 60;
		}

		// Token: 0x06003E7C RID: 15996 RVA: 0x005D33EB File Offset: 0x005D15EB
		private void UpdateSacrificeAnimation()
		{
			if (this._sacrificeAnimationTimeLeft > 0)
			{
				this._sacrificeAnimationTimeLeft--;
			}
		}

		// Token: 0x06003E7D RID: 15997 RVA: 0x005D3404 File Offset: 0x005D1604
		private float GetSacrificeAnimationProgress()
		{
			return Utils.GetLerpValue(60f, 0f, (float)this._sacrificeAnimationTimeLeft, true);
		}

		// Token: 0x06003E7E RID: 15998 RVA: 0x005D341D File Offset: 0x005D161D
		public void SetPageTypeToShow(UICreativeInfiniteItemsDisplay.InfiniteItemsDisplayPage page)
		{
			this._showSacrificesInsteadOfInfinites = (page == UICreativeInfiniteItemsDisplay.InfiniteItemsDisplayPage.InfiniteItemsResearch);
		}

		// Token: 0x06003E7F RID: 15999 RVA: 0x005D342C File Offset: 0x005D162C
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

		// Token: 0x06003E80 RID: 16000 RVA: 0x005D34A6 File Offset: 0x005D16A6
		private void filtersHelper_OnClickingOption()
		{
			this.UpdateContents();
		}

		// Token: 0x06003E81 RID: 16001 RVA: 0x005D34B0 File Offset: 0x005D16B0
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

		// Token: 0x06003E82 RID: 16002 RVA: 0x005D352C File Offset: 0x005D172C
		private void AddSearchBar(UIElement searchArea)
		{
			UIImageButton uIImageButton = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Search"))
			{
				VAlign = 0.5f,
				HAlign = 0f
			};
			uIImageButton.OnLeftClick += this.Click_SearchArea;
			uIImageButton.SetHoverImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Search_Border"));
			uIImageButton.SetVisibility(1f, 1f);
			uIImageButton.SetSnapPoint("CreativeInfinitesSearch", 0, null, null);
			searchArea.Append(uIImageButton);
			UIPanel uipanel = new UIPanel();
			uipanel.Width = new StyleDimension(0f - uIImageButton.Width.Pixels - 3f, 1f);
			uipanel.Height = new StyleDimension(0f, 1f);
			uipanel.VAlign = 0.5f;
			uipanel.HAlign = 1f;
			UIPanel uipanel2 = uipanel;
			this._searchBoxPanel = uipanel;
			UIPanel uIPanel = uipanel2;
			uIPanel.BackgroundColor = new Color(35, 40, 83);
			uIPanel.BorderColor = new Color(35, 40, 83);
			uIPanel.SetPadding(0f);
			searchArea.Append(uIPanel);
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
			uISearchBar.OnCanceledTakingInput += this.OnCanceledInput;
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

		// Token: 0x06003E83 RID: 16003 RVA: 0x005D37B4 File Offset: 0x005D19B4
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

		// Token: 0x06003E84 RID: 16004 RVA: 0x005D3806 File Offset: 0x005D1A06
		private void searchCancelButton_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
		}

		// Token: 0x06003E85 RID: 16005 RVA: 0x005D381D File Offset: 0x005D1A1D
		private void OnCanceledInput()
		{
			Main.LocalPlayer.ToggleInv();
		}

		// Token: 0x06003E86 RID: 16006 RVA: 0x005D3829 File Offset: 0x005D1A29
		private void Click_SearchArea(UIMouseEvent evt, UIElement listeningElement)
		{
			if (evt.Target.Parent != this._searchBoxPanel)
			{
				this._searchBar.ToggleTakingText();
				this._didClickSearchBar = true;
			}
		}

		// Token: 0x06003E87 RID: 16007 RVA: 0x005D3850 File Offset: 0x005D1A50
		public override void LeftClick(UIMouseEvent evt)
		{
			base.LeftClick(evt);
			this.AttemptStoppingUsingSearchbar(evt);
		}

		// Token: 0x06003E88 RID: 16008 RVA: 0x005D3860 File Offset: 0x005D1A60
		public override void RightClick(UIMouseEvent evt)
		{
			base.RightClick(evt);
			this.AttemptStoppingUsingSearchbar(evt);
		}

		// Token: 0x06003E89 RID: 16009 RVA: 0x005D3870 File Offset: 0x005D1A70
		private void AttemptStoppingUsingSearchbar(UIMouseEvent evt)
		{
			this._didClickSomething = true;
		}

		// Token: 0x06003E8A RID: 16010 RVA: 0x005D3879 File Offset: 0x005D1A79
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

		// Token: 0x06003E8B RID: 16011 RVA: 0x005D38B8 File Offset: 0x005D1AB8
		private void OnSearchContentsChanged(string contents)
		{
			this._searchString = contents;
			this._filterer.SetSearchFilter(contents);
			this.UpdateContents();
		}

		// Token: 0x06003E8C RID: 16012 RVA: 0x005D38D3 File Offset: 0x005D1AD3
		private void OnStartTakingInput()
		{
			this._searchBoxPanel.BorderColor = Main.OurFavoriteColor;
		}

		// Token: 0x06003E8D RID: 16013 RVA: 0x005D38E5 File Offset: 0x005D1AE5
		private void OnEndTakingInput()
		{
			this._searchBoxPanel.BorderColor = new Color(35, 40, 83);
		}

		// Token: 0x06003E8E RID: 16014 RVA: 0x005D3900 File Offset: 0x005D1B00
		private void OpenVirtualKeyboardWhenNeeded()
		{
			int maxInputLength = 40;
			UIVirtualKeyboard uivirtualKeyboard = new UIVirtualKeyboard(Language.GetText("UI.PlayerNameSlot").Value, this._searchString, new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedSettingName), new Action(this.GoBackHere), 3, true);
			uivirtualKeyboard.SetMaxInputLength(maxInputLength);
			uivirtualKeyboard.CustomEscapeAttempt = new Func<bool>(this.EscapeVirtualKeyboard);
			IngameFancyUI.OpenUIState(uivirtualKeyboard);
		}

		// Token: 0x06003E8F RID: 16015 RVA: 0x005D3962 File Offset: 0x005D1B62
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

		// Token: 0x06003E90 RID: 16016 RVA: 0x005D3992 File Offset: 0x005D1B92
		private static UserInterface GetCurrentInterface()
		{
			UserInterface activeInstance = UserInterface.ActiveInstance;
			if (Main.gameMenu)
			{
				return Main.MenuUI;
			}
			return Main.InGameUI;
		}

		// Token: 0x06003E91 RID: 16017 RVA: 0x005D39AC File Offset: 0x005D1BAC
		private void OnFinishedSettingName(string name)
		{
			string contents = name.Trim();
			this._searchBar.SetContents(contents, false);
			this.GoBackHere();
		}

		// Token: 0x06003E92 RID: 16018 RVA: 0x005D39D3 File Offset: 0x005D1BD3
		private void GoBackHere()
		{
			IngameFancyUI.Close();
			Main.CreativeMenu.ToggleMenu();
			this._searchBar.ToggleTakingText();
			Main.CreativeMenu.GamepadMoveToSearchButtonHack = true;
		}

		// Token: 0x06003E93 RID: 16019 RVA: 0x005D39FA File Offset: 0x005D1BFA
		public int GetItemsPerLine()
		{
			return this._itemGrid.GetItemsPerLine();
		}

		// Token: 0x04005701 RID: 22273
		private List<int> _itemIdsAvailableTotal;

		// Token: 0x04005702 RID: 22274
		private List<int> _itemIdsAvailableToShow;

		// Token: 0x04005703 RID: 22275
		private CreativeUnlocksTracker _lastTrackerCheckedForEdits;

		// Token: 0x04005704 RID: 22276
		private int _lastCheckedVersionForEdits = -1;

		// Token: 0x04005705 RID: 22277
		private UISearchBar _searchBar;

		// Token: 0x04005706 RID: 22278
		private UIPanel _searchBoxPanel;

		// Token: 0x04005707 RID: 22279
		private UIState _parentUIState;

		// Token: 0x04005708 RID: 22280
		private string _searchString;

		// Token: 0x04005709 RID: 22281
		private UIDynamicItemCollection _itemGrid;

		// Token: 0x0400570A RID: 22282
		private EntryFilterer<Item, IItemEntryFilter> _filterer;

		// Token: 0x0400570B RID: 22283
		private EntrySorter<int, ICreativeItemSortStep> _sorter;

		// Token: 0x0400570C RID: 22284
		private UIElement _containerInfinites;

		// Token: 0x0400570D RID: 22285
		private UIElement _containerSacrifice;

		// Token: 0x0400570E RID: 22286
		private bool _showSacrificesInsteadOfInfinites;

		// Token: 0x0400570F RID: 22287
		public const string SnapPointName_SacrificeSlot = "CreativeSacrificeSlot";

		// Token: 0x04005710 RID: 22288
		public const string SnapPointName_SacrificeConfirmButton = "CreativeSacrificeConfirm";

		// Token: 0x04005711 RID: 22289
		public const string SnapPointName_InfinitesFilter = "CreativeInfinitesFilter";

		// Token: 0x04005712 RID: 22290
		public const string SnapPointName_InfinitesSearch = "CreativeInfinitesSearch";

		// Token: 0x04005713 RID: 22291
		public const string SnapPointName_InfinitesItemSlot = "CreativeInfinitesSlot";

		// Token: 0x04005714 RID: 22292
		private List<UIImage> _sacrificeCogsSmall = new List<UIImage>();

		// Token: 0x04005715 RID: 22293
		private List<UIImage> _sacrificeCogsMedium = new List<UIImage>();

		// Token: 0x04005716 RID: 22294
		private List<UIImage> _sacrificeCogsBig = new List<UIImage>();

		// Token: 0x04005717 RID: 22295
		private UIImageFramed _sacrificePistons;

		// Token: 0x04005718 RID: 22296
		private UIParticleLayer _pistonParticleSystem;

		// Token: 0x04005719 RID: 22297
		private Asset<Texture2D> _pistonParticleAsset;

		// Token: 0x0400571A RID: 22298
		private int _sacrificeAnimationTimeLeft;

		// Token: 0x0400571B RID: 22299
		private bool _researchComplete;

		// Token: 0x0400571C RID: 22300
		private bool _hovered;

		// Token: 0x0400571D RID: 22301
		private int _lastItemIdSacrificed;

		// Token: 0x0400571E RID: 22302
		private int _lastItemAmountWeHad;

		// Token: 0x0400571F RID: 22303
		private int _lastItemAmountWeNeededTotal;

		// Token: 0x04005720 RID: 22304
		private bool _didClickSomething;

		// Token: 0x04005721 RID: 22305
		private bool _didClickSearchBar;

		// Token: 0x02000C18 RID: 3096
		public enum InfiniteItemsDisplayPage
		{
			// Token: 0x0400786F RID: 30831
			InfiniteItemsPickup,
			// Token: 0x04007870 RID: 30832
			InfiniteItemsResearch
		}
	}
}
