using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x02000346 RID: 838
	public class UIResourcePackInfoMenu : UIState
	{
		// Token: 0x060025CA RID: 9674 RVA: 0x0056F960 File Offset: 0x0056DB60
		public UIResourcePackInfoMenu(UIResourcePackSelectionMenu parent, ResourcePack pack)
		{
			this._resourceMenu = parent;
			this._pack = pack;
			this.BuildPage();
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x0056F97C File Offset: 0x0056DB7C
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
				Height = new StyleDimension(52f, 0f)
			};
			uielement3.SetPadding(0f);
			uielement2.Append(uielement3);
			UIText uitext = new UIText(this._pack.Name, 0.7f, true)
			{
				TextColor = Color.Gold
			};
			uitext.HAlign = 0.5f;
			uitext.VAlign = 0f;
			uielement3.Append(uitext);
			UIText uitext2 = new UIText(Language.GetTextValue("UI.Author", this._pack.Author), 0.9f, false)
			{
				HAlign = 0f,
				VAlign = 1f
			};
			uitext2.Top.Set(-6f, 0f);
			uielement3.Append(uitext2);
			UIText uitext3 = new UIText(Language.GetTextValue("UI.Version", this._pack.Version.GetFormattedVersion()), 0.9f, false)
			{
				HAlign = 1f,
				VAlign = 1f,
				TextColor = Color.Silver
			};
			uitext3.Top.Set(-6f, 0f);
			uielement3.Append(uitext3);
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Divider", 1);
			UIImage uiimage = new UIImage(asset)
			{
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixels((float)asset.Height()),
				ScaleToFit = true
			};
			uiimage.Top.Set(52f, 0f);
			uiimage.SetPadding(6f);
			uielement2.Append(uiimage);
			UIElement uielement4 = new UIElement
			{
				HAlign = 0.5f,
				VAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(-74f, 1f)
			};
			uielement2.Append(uielement4);
			this._container = uielement4;
			UIText item = new UIText(this._pack.Description, 1f, false)
			{
				HAlign = 0.5f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 0f),
				IsWrapped = true,
				WrappedTextBottomPadding = 0f
			};
			UIList uilist = new UIList
			{
				HAlign = 0.5f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				PaddingRight = 20f
			};
			uilist.ListPadding = 5f;
			uilist.Add(item);
			uielement4.Append(uilist);
			this._list = uilist;
			UIScrollbar uiscrollbar = new UIScrollbar();
			uiscrollbar.SetView(100f, 1000f);
			uiscrollbar.Height.Set(0f, 1f);
			uiscrollbar.HAlign = 1f;
			this._scrollbar = uiscrollbar;
			uilist.SetScrollbar(uiscrollbar);
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
			uitextPanel.Width.Set(-10f, 0.5f);
			uitextPanel.Height.Set(50f, 0f);
			uitextPanel.VAlign = 1f;
			uitextPanel.HAlign = 0.5f;
			uitextPanel.Top.Set(-45f, 0f);
			uitextPanel.OnMouseOver += UIResourcePackInfoMenu.FadedMouseOver;
			uitextPanel.OnMouseOut += UIResourcePackInfoMenu.FadedMouseOut;
			uitextPanel.OnLeftClick += this.GoBackClick;
			uitextPanel.SetSnapPoint("GoBack", 0, null, null);
			uielement.Append(uitextPanel);
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x0056FEA8 File Offset: 0x0056E0A8
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

		// Token: 0x060025CD RID: 9677 RVA: 0x0056FF56 File Offset: 0x0056E156
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.MenuUI.SetState(this._resourceMenu);
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x0056FF68 File Offset: 0x0056E168
		private static void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x0056D365 File Offset: 0x0056B565
		private static void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x0056FFBD File Offset: 0x0056E1BD
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.SetupGamepadPoints(spriteBatch);
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x0056FFD0 File Offset: 0x0056E1D0
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

		// Token: 0x04004A01 RID: 18945
		private UIResourcePackSelectionMenu _resourceMenu;

		// Token: 0x04004A02 RID: 18946
		private ResourcePack _pack;

		// Token: 0x04004A03 RID: 18947
		private UIElement _container;

		// Token: 0x04004A04 RID: 18948
		private UIList _list;

		// Token: 0x04004A05 RID: 18949
		private UIScrollbar _scrollbar;

		// Token: 0x04004A06 RID: 18950
		private bool _isScrollbarAttached;

		// Token: 0x04004A07 RID: 18951
		private const string _backPointName = "GoBack";

		// Token: 0x04004A08 RID: 18952
		private UIGamepadHelper _helper;
	}
}
