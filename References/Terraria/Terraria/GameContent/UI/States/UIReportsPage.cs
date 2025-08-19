using System;
using System.Collections.Generic;
using System.Linq;
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
	// Token: 0x02000341 RID: 833
	public class UIReportsPage : UIState
	{
		// Token: 0x06002580 RID: 9600 RVA: 0x0056CBAC File Offset: 0x0056ADAC
		public UIReportsPage(UIState stateToGoBackTo, int menuIdToGoBackTo, List<IProvideReports> reporters)
		{
			this._previousUIState = stateToGoBackTo;
			this._menuIdToGoBackTo = menuIdToGoBackTo;
			this._reporters = reporters;
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x0056CBC9 File Offset: 0x0056ADC9
		public override void OnInitialize()
		{
			this.BuildPage();
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x0056CBD4 File Offset: 0x0056ADD4
		private void BuildPage()
		{
			base.RemoveAllChildren();
			UIElement uielement = new UIElement();
			uielement.Width.Set(0f, 0.8f);
			uielement.MaxWidth.Set(500f, 0f);
			uielement.MinWidth.Set(300f, 0f);
			uielement.Top.Set(230f, 0f);
			uielement.Height.Set(-uielement.Top.Pixels, 1f);
			uielement.HAlign = 0.5f;
			base.Append(uielement);
			UIPanel uipanel = new UIPanel();
			uipanel.Width.Set(0f, 1f);
			uipanel.Height.Set(-110f, 1f);
			uipanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
			uielement.Append(uipanel);
			UIElement uielement2 = new UIElement
			{
				Width = StyleDimension.Fill,
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f)
			};
			uipanel.Append(uielement2);
			UIElement uielement3 = new UIElement
			{
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(28f, 0f)
			};
			uielement3.SetPadding(0f);
			uielement2.Append(uielement3);
			uielement3.Append(new UIText(Language.GetTextValue("UI.ReportsPage"), 0.7f, true)
			{
				HAlign = 0.5f,
				VAlign = 0f
			});
			UIElement uielement4 = new UIElement
			{
				HAlign = 0.5f,
				VAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(-40f, 1f),
				Top = new StyleDimension(-2f, 0f)
			};
			uielement2.Append(uielement4);
			this._container = uielement4;
			float num = 0f;
			UISlicedImage uislicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", 1))
			{
				HAlign = 0.5f,
				VAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent(-num * 2f, 1f),
				Left = StyleDimension.FromPixels(-num),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Top = StyleDimension.FromPixels(2f)
			};
			uislicedImage.SetSliceDepths(10);
			uislicedImage.Color = Color.LightGray * 0.5f;
			uielement4.Append(uislicedImage);
			UIList uilist = new UIList
			{
				HAlign = 0.5f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(-10f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				PaddingRight = 20f
			};
			uilist.ListPadding = 40f;
			uilist.ManualSortMethod = new Action<List<UIElement>>(this.ManualIfnoSortingMethod);
			UIElement item = new UIElement();
			uilist.Add(item);
			this.PopulateLogs(uilist);
			uielement4.Append(uilist);
			this._list = uilist;
			UIScrollbar uiscrollbar = new UIScrollbar();
			uiscrollbar.SetView(100f, 1000f);
			uiscrollbar.Height.Set(0f, 1f);
			uiscrollbar.HAlign = 1f;
			this._scrollbar = uiscrollbar;
			uilist.SetScrollbar(uiscrollbar);
			uiscrollbar.GoToBottom();
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
			uitextPanel.Width.Set(-10f, 0.5f);
			uitextPanel.Height.Set(50f, 0f);
			uitextPanel.VAlign = 1f;
			uitextPanel.HAlign = 0.5f;
			uitextPanel.Top.Set(-45f, 0f);
			uitextPanel.OnMouseOver += UIReportsPage.FadedMouseOver;
			uitextPanel.OnMouseOut += UIReportsPage.FadedMouseOut;
			uitextPanel.OnLeftClick += this.GoBackClick;
			uitextPanel.SetSnapPoint("GoBack", 0, null, null);
			uielement.Append(uitextPanel);
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		private void ManualIfnoSortingMethod(List<UIElement> list)
		{
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x0056D03C File Offset: 0x0056B23C
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
				UIText uitext = new UIText(list[i].reportText, 1f, false)
				{
					HAlign = 0f,
					VAlign = 0f,
					Width = StyleDimension.FromPixelsAndPercent(-10f, 1f),
					Height = StyleDimension.FromPixelsAndPercent(0f, 0f),
					IsWrapped = true,
					WrappedTextBottomPadding = 0f,
					TextOriginX = 0f
				};
				listContents.Add(uitext);
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Divider", 1);
				UIImage element = new UIImage(asset)
				{
					Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
					Height = StyleDimension.FromPixels((float)asset.Height()),
					ScaleToFit = true,
					VAlign = 1f
				};
				uitext.Append(element);
			}
			UIElement item2 = new UIElement();
			listContents.Add(item2);
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x0056D230 File Offset: 0x0056B430
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

		// Token: 0x06002586 RID: 9606 RVA: 0x0056D2DE File Offset: 0x0056B4DE
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.MenuUI.SetState(this._previousUIState);
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.menuMode = this._menuIdToGoBackTo;
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x0056D310 File Offset: 0x0056B510
		private static void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x0056D365 File Offset: 0x0056B565
		private static void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x0056D3A4 File Offset: 0x0056B5A4
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.SetupGamepadPoints(spriteBatch);
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x0056D3B4 File Offset: 0x0056B5B4
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 1;
			int num = 3000;
			int idRangeEndExclusive = num;
			List<SnapPoint> snapPoints = this.GetSnapPoints();
			for (int i = 0; i < snapPoints.Count; i++)
			{
				SnapPoint snapPoint = snapPoints[i];
				string name = snapPoint.Name;
				if (name == "GoBack")
				{
					this._helper.MakeLinkPointFromSnapPoint(idRangeEndExclusive++, snapPoint);
				}
			}
			this._helper.MoveToVisuallyClosestPoint(num, idRangeEndExclusive);
		}

		// Token: 0x040049D6 RID: 18902
		private UIState _previousUIState;

		// Token: 0x040049D7 RID: 18903
		private int _menuIdToGoBackTo;

		// Token: 0x040049D8 RID: 18904
		private UIElement _container;

		// Token: 0x040049D9 RID: 18905
		private UIList _list;

		// Token: 0x040049DA RID: 18906
		private UIScrollbar _scrollbar;

		// Token: 0x040049DB RID: 18907
		private bool _isScrollbarAttached;

		// Token: 0x040049DC RID: 18908
		private const string _backPointName = "GoBack";

		// Token: 0x040049DD RID: 18909
		private List<IProvideReports> _reporters;

		// Token: 0x040049DE RID: 18910
		private UIGamepadHelper _helper;
	}
}
