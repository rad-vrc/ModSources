using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Initializers;
using Terraria.IO;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020004E3 RID: 1251
	public class UIWorkshopSelectResourcePackToPublish : UIState, IHaveBackButtonCommand
	{
		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06003C9C RID: 15516 RVA: 0x005C2261 File Offset: 0x005C0461
		// (set) Token: 0x06003C9D RID: 15517 RVA: 0x005C2269 File Offset: 0x005C0469
		public UIState PreviousUIState
		{
			get
			{
				return this._menuToGoBackTo;
			}
			set
			{
				this._menuToGoBackTo = value;
			}
		}

		// Token: 0x06003C9E RID: 15518 RVA: 0x005C2272 File Offset: 0x005C0472
		public UIWorkshopSelectResourcePackToPublish(UIState menuToGoBackTo)
		{
			this._menuToGoBackTo = menuToGoBackTo;
		}

		// Token: 0x06003C9F RID: 15519 RVA: 0x005C228C File Offset: 0x005C048C
		public override void OnInitialize()
		{
			UIElement uIElement = new UIElement();
			uIElement.Width.Set(0f, 0.8f);
			uIElement.MaxWidth.Set(650f, 0f);
			uIElement.Top.Set(220f, 0f);
			uIElement.Height.Set(-220f, 1f);
			uIElement.HAlign = 0.5f;
			UIPanel uIPanel = new UIPanel();
			uIPanel.Width.Set(0f, 1f);
			uIPanel.Height.Set(-110f, 1f);
			uIPanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
			uIElement.Append(uIPanel);
			this._containerPanel = uIPanel;
			this._entryList = new UIList();
			this._entryList.Width.Set(0f, 1f);
			this._entryList.Height.Set(0f, 1f);
			this._entryList.ListPadding = 5f;
			uIPanel.Append(this._entryList);
			this._scrollbar = new UIScrollbar();
			this._scrollbar.SetView(100f, 1000f);
			this._scrollbar.Height.Set(0f, 1f);
			this._scrollbar.HAlign = 1f;
			this._entryList.SetScrollbar(this._scrollbar);
			UITextPanel<LocalizedText> uITextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.WorkshopSelectResourcePackToPublishMenuTitle"), 0.8f, true);
			uITextPanel.HAlign = 0.5f;
			uITextPanel.Top.Set(-40f, 0f);
			uITextPanel.SetPadding(15f);
			uITextPanel.BackgroundColor = new Color(73, 94, 171);
			uIElement.Append(uITextPanel);
			UITextPanel<LocalizedText> uITextPanel2 = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
			uITextPanel2.Width.Set(-10f, 0.5f);
			uITextPanel2.Height.Set(50f, 0f);
			uITextPanel2.VAlign = 1f;
			uITextPanel2.HAlign = 0.5f;
			uITextPanel2.Top.Set(-45f, 0f);
			uITextPanel2.OnMouseOver += this.FadedMouseOver;
			uITextPanel2.OnMouseOut += this.FadedMouseOut;
			uITextPanel2.OnLeftClick += this.GoBackClick;
			uIElement.Append(uITextPanel2);
			this._backPanel = uITextPanel2;
			base.Append(uIElement);
		}

		// Token: 0x06003CA0 RID: 15520 RVA: 0x005C2520 File Offset: 0x005C0720
		public override void Recalculate()
		{
			if (this._scrollbar != null)
			{
				if (this._isScrollbarAttached && !this._scrollbar.CanScroll)
				{
					this._containerPanel.RemoveChild(this._scrollbar);
					this._isScrollbarAttached = false;
					this._entryList.Width.Set(0f, 1f);
				}
				else if (!this._isScrollbarAttached && this._scrollbar.CanScroll)
				{
					this._containerPanel.Append(this._scrollbar);
					this._isScrollbarAttached = true;
					this._entryList.Width.Set(-25f, 1f);
				}
			}
			base.Recalculate();
		}

		// Token: 0x06003CA1 RID: 15521 RVA: 0x005C25CE File Offset: 0x005C07CE
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			this.HandleBackButtonUsage();
		}

		// Token: 0x06003CA2 RID: 15522 RVA: 0x005C25D6 File Offset: 0x005C07D6
		public void HandleBackButtonUsage()
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.MenuUI.SetState(this._menuToGoBackTo);
		}

		// Token: 0x06003CA3 RID: 15523 RVA: 0x005C2600 File Offset: 0x005C0800
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06003CA4 RID: 15524 RVA: 0x005C2655 File Offset: 0x005C0855
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.7f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06003CA5 RID: 15525 RVA: 0x005C2694 File Offset: 0x005C0894
		public override void OnActivate()
		{
			this.PopulateEntries();
			if (PlayerInput.UsingGamepadUI)
			{
				UILinkPointNavigator.ChangePoint(3000 + ((this._entryList.Count != 0) ? 1 : 0));
			}
		}

		// Token: 0x06003CA6 RID: 15526 RVA: 0x005C26BC File Offset: 0x005C08BC
		public void PopulateEntries()
		{
			this._entries.Clear();
			IOrderedEnumerable<ResourcePack> collection = from x in AssetInitializer.CreatePublishableResourcePacksList(Main.instance.Services).AllPacks
			where x.Branding == ResourcePack.BrandingType.None
			orderby x.IsCompressed
			select x;
			this._entries.AddRange(collection);
			this._entryList.Clear();
			int num = 0;
			foreach (ResourcePack entry in this._entries)
			{
				this._entryList.Add(new UIWorkshopPublishResourcePackListItem(this, entry, num++, !entry.IsCompressed));
			}
		}

		// Token: 0x06003CA7 RID: 15527 RVA: 0x005C27AC File Offset: 0x005C09AC
		public override void Draw(SpriteBatch spriteBatch)
		{
			if (this.skipDraw)
			{
				this.skipDraw = false;
				return;
			}
			base.Draw(spriteBatch);
			this.SetupGamepadPoints(spriteBatch);
		}

		// Token: 0x06003CA8 RID: 15528 RVA: 0x005C27CC File Offset: 0x005C09CC
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
			int num = 3000;
			UILinkPointNavigator.SetPosition(num, this._backPanel.GetInnerDimensions().ToRectangle().Center.ToVector2());
			int num2 = num;
			UILinkPoint uILinkPoint = UILinkPointNavigator.Points[num2];
			uILinkPoint.Unlink();
			uILinkPoint.Right = num2;
			float num3 = 1f / Main.UIScale;
			Rectangle clippingRectangle = this._containerPanel.GetClippingRectangle(spriteBatch);
			Vector2 minimum = clippingRectangle.TopLeft() * num3;
			Vector2 maximum = clippingRectangle.BottomRight() * num3;
			List<SnapPoint> snapPoints = this.GetSnapPoints();
			for (int i = 0; i < snapPoints.Count; i++)
			{
				if (!snapPoints[i].Position.Between(minimum, maximum))
				{
					snapPoints.Remove(snapPoints[i]);
					i--;
				}
			}
			int num4 = 1;
			SnapPoint[,] array = new SnapPoint[this._entryList.Count, num4];
			foreach (SnapPoint item in from a in snapPoints
			where a.Name == "Publish"
			select a)
			{
				array[item.Id, 0] = item;
			}
			num2 = num + 1;
			int[] array2 = new int[this._entryList.Count];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = -1;
			}
			for (int k = 0; k < num4; k++)
			{
				int num5 = -1;
				for (int l = 0; l < array.GetLength(0); l++)
				{
					if (array[l, k] != null)
					{
						uILinkPoint = UILinkPointNavigator.Points[num2];
						uILinkPoint.Unlink();
						UILinkPointNavigator.SetPosition(num2, array[l, k].Position);
						if (num5 != -1)
						{
							uILinkPoint.Up = num5;
							UILinkPointNavigator.Points[num5].Down = num2;
						}
						if (array2[l] != -1)
						{
							uILinkPoint.Left = array2[l];
							UILinkPointNavigator.Points[array2[l]].Right = num2;
						}
						uILinkPoint.Down = num;
						if (k == 0)
						{
							UILinkPointNavigator.Points[num].Up = (UILinkPointNavigator.Points[num + 1].Up = num2);
						}
						num5 = num2;
						array2[l] = num2;
						UILinkPointNavigator.Shortcuts.FANCYUI_HIGHEST_INDEX = num2;
						num2++;
					}
				}
			}
			if (PlayerInput.UsingGamepadUI && this._entryList.Count == 0 && UILinkPointNavigator.CurrentPoint > 3000)
			{
				UILinkPointNavigator.ChangePoint(3000);
			}
		}

		// Token: 0x040055C1 RID: 21953
		private UIList _entryList;

		// Token: 0x040055C2 RID: 21954
		private UITextPanel<LocalizedText> _backPanel;

		// Token: 0x040055C3 RID: 21955
		private UIPanel _containerPanel;

		// Token: 0x040055C4 RID: 21956
		private UIScrollbar _scrollbar;

		// Token: 0x040055C5 RID: 21957
		private bool _isScrollbarAttached;

		// Token: 0x040055C6 RID: 21958
		private UIState _menuToGoBackTo;

		// Token: 0x040055C7 RID: 21959
		private List<ResourcePack> _entries = new List<ResourcePack>();

		// Token: 0x040055C8 RID: 21960
		private bool skipDraw;
	}
}
