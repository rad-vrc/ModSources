using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
	// Token: 0x020004DE RID: 1246
	public class UIResourcePackInfoMenu : UIState
	{
		// Token: 0x06003C16 RID: 15382 RVA: 0x005BD8FB File Offset: 0x005BBAFB
		public UIResourcePackInfoMenu(UIResourcePackSelectionMenu parent, ResourcePack pack)
		{
			this._resourceMenu = parent;
			this._pack = pack;
			this.BuildPage();
		}

		// Token: 0x06003C17 RID: 15383 RVA: 0x005BD918 File Offset: 0x005BBB18
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
				Height = new StyleDimension(52f, 0f)
			};
			uIElement3.SetPadding(0f);
			uIElement2.Append(uIElement3);
			UIText uIText = new UIText(this._pack.Name, 0.7f, true)
			{
				TextColor = Color.Gold
			};
			uIText.HAlign = 0.5f;
			uIText.VAlign = 0f;
			uIElement3.Append(uIText);
			UIText uIText2 = new UIText(Language.GetTextValue("UI.Author", this._pack.Author), 0.9f, false)
			{
				HAlign = 0f,
				VAlign = 1f
			};
			uIText2.Top.Set(-6f, 0f);
			uIElement3.Append(uIText2);
			UIText uIText3 = new UIText(Language.GetTextValue("UI.Version", this._pack.Version.GetFormattedVersion()), 0.9f, false)
			{
				HAlign = 1f,
				VAlign = 1f,
				TextColor = Color.Silver
			};
			uIText3.Top.Set(-6f, 0f);
			uIElement3.Append(uIText3);
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Divider");
			UIImage uIImage = new UIImage(asset)
			{
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixels((float)asset.Height()),
				ScaleToFit = true
			};
			uIImage.Top.Set(52f, 0f);
			uIImage.SetPadding(6f);
			uIElement2.Append(uIImage);
			UIElement uIElement4 = new UIElement
			{
				HAlign = 0.5f,
				VAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(-74f, 1f)
			};
			uIElement2.Append(uIElement4);
			this._container = uIElement4;
			UIText item = new UIText(this._pack.Description, 1f, false)
			{
				HAlign = 0.5f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 0f),
				IsWrapped = true,
				WrappedTextBottomPadding = 0f
			};
			UIList uIList = new UIList
			{
				HAlign = 0.5f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				PaddingRight = 20f
			};
			uIList.ListPadding = 5f;
			uIList.Add(item);
			uIElement4.Append(uIList);
			this._list = uIList;
			UIScrollbar uIScrollbar = new UIScrollbar();
			uIScrollbar.SetView(100f, 1000f);
			uIScrollbar.Height.Set(0f, 1f);
			uIScrollbar.HAlign = 1f;
			this._scrollbar = uIScrollbar;
			uIList.SetScrollbar(uIScrollbar);
			UITextPanel<LocalizedText> uITextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
			uITextPanel.Width.Set(-10f, 0.5f);
			uITextPanel.Height.Set(50f, 0f);
			uITextPanel.VAlign = 1f;
			uITextPanel.HAlign = 0.5f;
			uITextPanel.Top.Set(-45f, 0f);
			UIElement uielement = uITextPanel;
			UIElement.MouseEvent value;
			if ((value = UIResourcePackInfoMenu.<>O.<0>__FadedMouseOver) == null)
			{
				value = (UIResourcePackInfoMenu.<>O.<0>__FadedMouseOver = new UIElement.MouseEvent(UIResourcePackInfoMenu.FadedMouseOver));
			}
			uielement.OnMouseOver += value;
			UIElement uielement2 = uITextPanel;
			UIElement.MouseEvent value2;
			if ((value2 = UIResourcePackInfoMenu.<>O.<1>__FadedMouseOut) == null)
			{
				value2 = (UIResourcePackInfoMenu.<>O.<1>__FadedMouseOut = new UIElement.MouseEvent(UIResourcePackInfoMenu.FadedMouseOut));
			}
			uielement2.OnMouseOut += value2;
			uITextPanel.OnLeftClick += this.GoBackClick;
			uITextPanel.SetSnapPoint("GoBack", 0, null, null);
			uIElement.Append(uITextPanel);
		}

		// Token: 0x06003C18 RID: 15384 RVA: 0x005BDE64 File Offset: 0x005BC064
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

		// Token: 0x06003C19 RID: 15385 RVA: 0x005BDF12 File Offset: 0x005BC112
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.MenuUI.SetState(this._resourceMenu);
		}

		// Token: 0x06003C1A RID: 15386 RVA: 0x005BDF24 File Offset: 0x005BC124
		private static void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06003C1B RID: 15387 RVA: 0x005BDF79 File Offset: 0x005BC179
		private static void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06003C1C RID: 15388 RVA: 0x005BDFB8 File Offset: 0x005BC1B8
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.SetupGamepadPoints(spriteBatch);
		}

		// Token: 0x06003C1D RID: 15389 RVA: 0x005BDFC8 File Offset: 0x005BC1C8
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

		// Token: 0x0400556E RID: 21870
		private UIResourcePackSelectionMenu _resourceMenu;

		// Token: 0x0400556F RID: 21871
		private ResourcePack _pack;

		// Token: 0x04005570 RID: 21872
		private UIElement _container;

		// Token: 0x04005571 RID: 21873
		private UIList _list;

		// Token: 0x04005572 RID: 21874
		private UIScrollbar _scrollbar;

		// Token: 0x04005573 RID: 21875
		private bool _isScrollbarAttached;

		// Token: 0x04005574 RID: 21876
		private const string _backPointName = "GoBack";

		// Token: 0x04005575 RID: 21877
		private UIGamepadHelper _helper;

		// Token: 0x02000BF1 RID: 3057
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040077F0 RID: 30704
			public static UIElement.MouseEvent <0>__FadedMouseOver;

			// Token: 0x040077F1 RID: 30705
			public static UIElement.MouseEvent <1>__FadedMouseOut;
		}
	}
}
