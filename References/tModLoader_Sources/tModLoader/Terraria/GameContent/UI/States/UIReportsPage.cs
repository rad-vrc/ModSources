using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020004DD RID: 1245
	public class UIReportsPage : UIState
	{
		// Token: 0x06003C0B RID: 15371 RVA: 0x005BD05D File Offset: 0x005BB25D
		public UIReportsPage(UIState stateToGoBackTo, int menuIdToGoBackTo, List<IProvideReports> reporters)
		{
			this._previousUIState = stateToGoBackTo;
			this._menuIdToGoBackTo = menuIdToGoBackTo;
			this._reporters = reporters;
		}

		// Token: 0x06003C0C RID: 15372 RVA: 0x005BD07A File Offset: 0x005BB27A
		public override void OnInitialize()
		{
			this.BuildPage();
		}

		// Token: 0x06003C0D RID: 15373 RVA: 0x005BD084 File Offset: 0x005BB284
		private void BuildPage()
		{
			base.RemoveAllChildren();
			UIElement uIElement = new UIElement();
			uIElement.Width.Set(0f, 0.8f);
			uIElement.MaxWidth.Set(500f, 0f);
			uIElement.MinWidth.Set(300f, 0f);
			uIElement.Top.Set(230f, 0f);
			uIElement.Height.Set(0f - uIElement.Top.Pixels, 1f);
			uIElement.HAlign = 0.5f;
			base.Append(uIElement);
			UIPanel uIPanel = new UIPanel();
			uIPanel.Width.Set(0f, 1f);
			uIPanel.Height.Set(-110f, 1f);
			uIPanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
			uIElement.Append(uIPanel);
			UIElement uIElement2 = new UIElement
			{
				Width = StyleDimension.Fill,
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f)
			};
			uIPanel.Append(uIElement2);
			UIElement uIElement3 = new UIElement
			{
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(28f, 0f)
			};
			uIElement3.SetPadding(0f);
			uIElement2.Append(uIElement3);
			uIElement3.Append(new UIText(Language.GetTextValue("UI.ReportsPage"), 0.7f, true)
			{
				HAlign = 0.5f,
				VAlign = 0f
			});
			UIElement uIElement4 = new UIElement
			{
				HAlign = 0.5f,
				VAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(-40f, 1f),
				Top = new StyleDimension(-2f, 0f)
			};
			uIElement2.Append(uIElement4);
			this._container = uIElement4;
			float num = 0f;
			UISlicedImage uISlicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight"))
			{
				HAlign = 0.5f,
				VAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent((0f - num) * 2f, 1f),
				Left = StyleDimension.FromPixels(0f - num),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Top = StyleDimension.FromPixels(2f)
			};
			uISlicedImage.SetSliceDepths(10);
			uISlicedImage.Color = Color.LightGray * 0.5f;
			uIElement4.Append(uISlicedImage);
			UIList uIList = new UIList
			{
				HAlign = 0.5f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(-10f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				PaddingRight = 20f
			};
			uIList.ListPadding = 40f;
			uIList.ManualSortMethod = new Action<List<UIElement>>(this.ManualIfnoSortingMethod);
			UIElement item = new UIElement();
			uIList.Add(item);
			this.PopulateLogs(uIList);
			uIElement4.Append(uIList);
			this._list = uIList;
			UIScrollbar uIScrollbar = new UIScrollbar();
			uIScrollbar.SetView(100f, 1000f);
			uIScrollbar.Height.Set(0f, 1f);
			uIScrollbar.HAlign = 1f;
			this._scrollbar = uIScrollbar;
			uIList.SetScrollbar(uIScrollbar);
			uIScrollbar.GoToBottom();
			UITextPanel<LocalizedText> uITextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
			uITextPanel.Width.Set(-10f, 0.5f);
			uITextPanel.Height.Set(50f, 0f);
			uITextPanel.VAlign = 1f;
			uITextPanel.HAlign = 0.5f;
			uITextPanel.Top.Set(-45f, 0f);
			UIElement uielement = uITextPanel;
			UIElement.MouseEvent value;
			if ((value = UIReportsPage.<>O.<0>__FadedMouseOver) == null)
			{
				value = (UIReportsPage.<>O.<0>__FadedMouseOver = new UIElement.MouseEvent(UIReportsPage.FadedMouseOver));
			}
			uielement.OnMouseOver += value;
			UIElement uielement2 = uITextPanel;
			UIElement.MouseEvent value2;
			if ((value2 = UIReportsPage.<>O.<1>__FadedMouseOut) == null)
			{
				value2 = (UIReportsPage.<>O.<1>__FadedMouseOut = new UIElement.MouseEvent(UIReportsPage.FadedMouseOut));
			}
			uielement2.OnMouseOut += value2;
			uITextPanel.OnLeftClick += this.GoBackClick;
			uITextPanel.SetSnapPoint("GoBack", 0, null, null);
			uIElement.Append(uITextPanel);
		}

		// Token: 0x06003C0E RID: 15374 RVA: 0x005BD516 File Offset: 0x005BB716
		private void ManualIfnoSortingMethod(List<UIElement> list)
		{
		}

		// Token: 0x06003C0F RID: 15375 RVA: 0x005BD518 File Offset: 0x005BB718
		private void PopulateLogs(UIList listContents)
		{
			List<IssueReport> list = (from report in this._reporters.SelectMany((IProvideReports reporter) => reporter.GetReports())
			orderby report.timeReported
			select report).ToList<IssueReport>();
			if (list.Count == 0)
			{
				UIText item = new UIText(Language.GetTextValue("Workshop.ReportLogsInitialMessage"), 1f, false)
				{
					HAlign = 0f,
					VAlign = 0f,
					Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
					Height = StyleDimension.FromPixelsAndPercent(0f, 0f),
					IsWrapped = true,
					WrappedTextBottomPadding = 0f,
					TextOriginX = 0.5f,
					TextColor = Color.Gray
				};
				listContents.Add(item);
			}
			for (int i = 0; i < list.Count; i++)
			{
				UIText uIText = new UIText(list[i].reportText, 1f, false)
				{
					HAlign = 0f,
					VAlign = 0f,
					Width = StyleDimension.FromPixelsAndPercent(-10f, 1f),
					Height = StyleDimension.FromPixelsAndPercent(0f, 0f),
					IsWrapped = true,
					WrappedTextBottomPadding = 0f,
					TextOriginX = 0f
				};
				listContents.Add(uIText);
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Divider");
				UIImage element = new UIImage(asset)
				{
					Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
					Height = StyleDimension.FromPixels((float)asset.Height()),
					ScaleToFit = true,
					VAlign = 1f
				};
				uIText.Append(element);
			}
			UIElement item2 = new UIElement();
			listContents.Add(item2);
		}

		// Token: 0x06003C10 RID: 15376 RVA: 0x005BD708 File Offset: 0x005BB908
		public override void Recalculate()
		{
			if (this._scrollbar != null)
			{
				if (this._isScrollbarAttached && !this._scrollbar.CanScroll)
				{
					this._container.RemoveChild(this._scrollbar);
					this._isScrollbarAttached = false;
					this._list.Width.Set(0f, 1f);
				}
				else if (!this._isScrollbarAttached && this._scrollbar.CanScroll)
				{
					this._container.Append(this._scrollbar);
					this._isScrollbarAttached = true;
					this._list.Width.Set(-25f, 1f);
				}
			}
			base.Recalculate();
		}

		// Token: 0x06003C11 RID: 15377 RVA: 0x005BD7B6 File Offset: 0x005BB9B6
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.MenuUI.SetState(this._previousUIState);
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.menuMode = this._menuIdToGoBackTo;
		}

		// Token: 0x06003C12 RID: 15378 RVA: 0x005BD7E8 File Offset: 0x005BB9E8
		private static void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06003C13 RID: 15379 RVA: 0x005BD83D File Offset: 0x005BBA3D
		private static void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06003C14 RID: 15380 RVA: 0x005BD87C File Offset: 0x005BBA7C
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.SetupGamepadPoints(spriteBatch);
		}

		// Token: 0x06003C15 RID: 15381 RVA: 0x005BD88C File Offset: 0x005BBA8C
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 1;
			int num = 3000;
			int idRangeEndExclusive = num;
			List<SnapPoint> snapPoints = this.GetSnapPoints();
			for (int i = 0; i < snapPoints.Count; i++)
			{
				SnapPoint snapPoint = snapPoints[i];
				if (snapPoint.Name == "GoBack")
				{
					this._helper.MakeLinkPointFromSnapPoint(idRangeEndExclusive++, snapPoint);
				}
			}
			this._helper.MoveToVisuallyClosestPoint(num, idRangeEndExclusive);
		}

		// Token: 0x04005565 RID: 21861
		private UIState _previousUIState;

		// Token: 0x04005566 RID: 21862
		private int _menuIdToGoBackTo;

		// Token: 0x04005567 RID: 21863
		private UIElement _container;

		// Token: 0x04005568 RID: 21864
		private UIList _list;

		// Token: 0x04005569 RID: 21865
		private UIScrollbar _scrollbar;

		// Token: 0x0400556A RID: 21866
		private bool _isScrollbarAttached;

		// Token: 0x0400556B RID: 21867
		private const string _backPointName = "GoBack";

		// Token: 0x0400556C RID: 21868
		private List<IProvideReports> _reporters;

		// Token: 0x0400556D RID: 21869
		private UIGamepadHelper _helper;

		// Token: 0x02000BEF RID: 3055
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040077EB RID: 30699
			public static UIElement.MouseEvent <0>__FadedMouseOver;

			// Token: 0x040077EC RID: 30700
			public static UIElement.MouseEvent <1>__FadedMouseOut;
		}
	}
}
