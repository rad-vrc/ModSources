using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020004E4 RID: 1252
	public class UIWorkshopSelectWorldToPublish : UIState, IHaveBackButtonCommand
	{
		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06003CA9 RID: 15529 RVA: 0x005C2A88 File Offset: 0x005C0C88
		// (set) Token: 0x06003CAA RID: 15530 RVA: 0x005C2A90 File Offset: 0x005C0C90
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

		// Token: 0x06003CAB RID: 15531 RVA: 0x005C2A99 File Offset: 0x005C0C99
		public UIWorkshopSelectWorldToPublish(UIState menuToGoBackTo)
		{
			this._menuToGoBackTo = menuToGoBackTo;
		}

		// Token: 0x06003CAC RID: 15532 RVA: 0x005C2AA8 File Offset: 0x005C0CA8
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
			UITextPanel<LocalizedText> uITextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.WorkshopSelectWorldToPublishMenuTitle"), 0.8f, true);
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

		// Token: 0x06003CAD RID: 15533 RVA: 0x005C2D3C File Offset: 0x005C0F3C
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

		// Token: 0x06003CAE RID: 15534 RVA: 0x005C2DEA File Offset: 0x005C0FEA
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			this.HandleBackButtonUsage();
		}

		// Token: 0x06003CAF RID: 15535 RVA: 0x005C2DF2 File Offset: 0x005C0FF2
		public void HandleBackButtonUsage()
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.MenuUI.SetState(this._menuToGoBackTo);
		}

		// Token: 0x06003CB0 RID: 15536 RVA: 0x005C2E1C File Offset: 0x005C101C
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06003CB1 RID: 15537 RVA: 0x005C2E71 File Offset: 0x005C1071
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.7f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06003CB2 RID: 15538 RVA: 0x005C2EB0 File Offset: 0x005C10B0
		public override void OnActivate()
		{
			this.PopulateEntries();
			if (PlayerInput.UsingGamepadUI)
			{
				UILinkPointNavigator.ChangePoint(3000 + ((this._entryList.Count != 0) ? 1 : 0));
			}
		}

		// Token: 0x06003CB3 RID: 15539 RVA: 0x005C2ED8 File Offset: 0x005C10D8
		private void PopulateEntries()
		{
			Main.LoadWorlds();
			this._entryList.Clear();
			IEnumerable<WorldFileData> enumerable = from x in new List<WorldFileData>(Main.WorldList)
			orderby x.IsFavorite descending, x.Name, x.GetFileName(true)
			select x;
			this._entryList.Clear();
			int num = 0;
			foreach (WorldFileData item in enumerable)
			{
				this._entryList.Add(new UIWorkshopPublishWorldListItem(this, item, num++));
			}
		}

		// Token: 0x06003CB4 RID: 15540 RVA: 0x005C2FC4 File Offset: 0x005C11C4
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

		// Token: 0x06003CB5 RID: 15541 RVA: 0x005C2FE4 File Offset: 0x005C11E4
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

		// Token: 0x040055C9 RID: 21961
		private UIList _entryList;

		// Token: 0x040055CA RID: 21962
		private UITextPanel<LocalizedText> _backPanel;

		// Token: 0x040055CB RID: 21963
		private UIPanel _containerPanel;

		// Token: 0x040055CC RID: 21964
		private UIScrollbar _scrollbar;

		// Token: 0x040055CD RID: 21965
		private bool _isScrollbarAttached;

		// Token: 0x040055CE RID: 21966
		private UIState _menuToGoBackTo;

		// Token: 0x040055CF RID: 21967
		private bool skipDraw;
	}
}
