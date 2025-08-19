﻿using System;
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
	// Token: 0x02000344 RID: 836
	public class UIWorkshopSelectWorldToPublish : UIState, IHaveBackButtonCommand
	{
		// Token: 0x060025B2 RID: 9650 RVA: 0x0056E934 File Offset: 0x0056CB34
		public UIWorkshopSelectWorldToPublish(UIState menuToGoBackTo)
		{
			this._menuToGoBackTo = menuToGoBackTo;
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x0056E944 File Offset: 0x0056CB44
		public override void OnInitialize()
		{
			UIElement uielement = new UIElement();
			uielement.Width.Set(0f, 0.8f);
			uielement.MaxWidth.Set(650f, 0f);
			uielement.Top.Set(220f, 0f);
			uielement.Height.Set(-220f, 1f);
			uielement.HAlign = 0.5f;
			UIPanel uipanel = new UIPanel();
			uipanel.Width.Set(0f, 1f);
			uipanel.Height.Set(-110f, 1f);
			uipanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
			uielement.Append(uipanel);
			this._containerPanel = uipanel;
			this._entryList = new UIList();
			this._entryList.Width.Set(0f, 1f);
			this._entryList.Height.Set(0f, 1f);
			this._entryList.ListPadding = 5f;
			uipanel.Append(this._entryList);
			this._scrollbar = new UIScrollbar();
			this._scrollbar.SetView(100f, 1000f);
			this._scrollbar.Height.Set(0f, 1f);
			this._scrollbar.HAlign = 1f;
			this._entryList.SetScrollbar(this._scrollbar);
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.WorkshopSelectWorldToPublishMenuTitle"), 0.8f, true);
			uitextPanel.HAlign = 0.5f;
			uitextPanel.Top.Set(-40f, 0f);
			uitextPanel.SetPadding(15f);
			uitextPanel.BackgroundColor = new Color(73, 94, 171);
			uielement.Append(uitextPanel);
			UITextPanel<LocalizedText> uitextPanel2 = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
			uitextPanel2.Width.Set(-10f, 0.5f);
			uitextPanel2.Height.Set(50f, 0f);
			uitextPanel2.VAlign = 1f;
			uitextPanel2.HAlign = 0.5f;
			uitextPanel2.Top.Set(-45f, 0f);
			uitextPanel2.OnMouseOver += this.FadedMouseOver;
			uitextPanel2.OnMouseOut += this.FadedMouseOut;
			uitextPanel2.OnLeftClick += this.GoBackClick;
			uielement.Append(uitextPanel2);
			this._backPanel = uitextPanel2;
			base.Append(uielement);
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x0056EBD8 File Offset: 0x0056CDD8
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

		// Token: 0x060025B5 RID: 9653 RVA: 0x0056EC86 File Offset: 0x0056CE86
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			this.HandleBackButtonUsage();
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x0056EC8E File Offset: 0x0056CE8E
		public void HandleBackButtonUsage()
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.MenuUI.SetState(this._menuToGoBackTo);
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x0056ECB8 File Offset: 0x0056CEB8
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x0056E4FD File Offset: 0x0056C6FD
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.7f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x0056ED0D File Offset: 0x0056CF0D
		public override void OnActivate()
		{
			this.PopulateEntries();
			if (PlayerInput.UsingGamepadUI)
			{
				UILinkPointNavigator.ChangePoint(3000 + ((this._entryList.Count == 0) ? 0 : 1));
			}
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x0056ED38 File Offset: 0x0056CF38
		private void PopulateEntries()
		{
			Main.LoadWorlds();
			this._entryList.Clear();
			IEnumerable<WorldFileData> enumerable = from x in new List<WorldFileData>(Main.WorldList)
			orderby x.IsFavorite descending, x.Name, x.GetFileName(true)
			select x;
			this._entryList.Clear();
			int num = 0;
			foreach (WorldFileData data in enumerable)
			{
				this._entryList.Add(new UIWorkshopPublishWorldListItem(this, data, num++));
			}
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x0056EE24 File Offset: 0x0056D024
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

		// Token: 0x060025BC RID: 9660 RVA: 0x0056EE44 File Offset: 0x0056D044
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
			int num = 3000;
			UILinkPointNavigator.SetPosition(num, this._backPanel.GetInnerDimensions().ToRectangle().Center.ToVector2());
			int num2 = num;
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[num2];
			uilinkPoint.Unlink();
			uilinkPoint.Right = num2;
			float scaleFactor = 1f / Main.UIScale;
			Rectangle clippingRectangle = this._containerPanel.GetClippingRectangle(spriteBatch);
			Vector2 minimum = clippingRectangle.TopLeft() * scaleFactor;
			Vector2 maximum = clippingRectangle.BottomRight() * scaleFactor;
			List<SnapPoint> snapPoints = this.GetSnapPoints();
			for (int i = 0; i < snapPoints.Count; i++)
			{
				if (!snapPoints[i].Position.Between(minimum, maximum))
				{
					snapPoints.Remove(snapPoints[i]);
					i--;
				}
			}
			int num3 = 1;
			SnapPoint[,] array = new SnapPoint[this._entryList.Count, num3];
			foreach (SnapPoint snapPoint in from a in snapPoints
			where a.Name == "Publish"
			select a)
			{
				array[snapPoint.Id, 0] = snapPoint;
			}
			num2 = num + 1;
			int[] array2 = new int[this._entryList.Count];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = -1;
			}
			for (int k = 0; k < num3; k++)
			{
				int num4 = -1;
				for (int l = 0; l < array.GetLength(0); l++)
				{
					if (array[l, k] != null)
					{
						uilinkPoint = UILinkPointNavigator.Points[num2];
						uilinkPoint.Unlink();
						UILinkPointNavigator.SetPosition(num2, array[l, k].Position);
						if (num4 != -1)
						{
							uilinkPoint.Up = num4;
							UILinkPointNavigator.Points[num4].Down = num2;
						}
						if (array2[l] != -1)
						{
							uilinkPoint.Left = array2[l];
							UILinkPointNavigator.Points[array2[l]].Right = num2;
						}
						uilinkPoint.Down = num;
						if (k == 0)
						{
							UILinkPointNavigator.Points[num].Up = (UILinkPointNavigator.Points[num + 1].Up = num2);
						}
						num4 = num2;
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

		// Token: 0x040049F1 RID: 18929
		private UIList _entryList;

		// Token: 0x040049F2 RID: 18930
		private UITextPanel<LocalizedText> _backPanel;

		// Token: 0x040049F3 RID: 18931
		private UIPanel _containerPanel;

		// Token: 0x040049F4 RID: 18932
		private UIScrollbar _scrollbar;

		// Token: 0x040049F5 RID: 18933
		private bool _isScrollbarAttached;

		// Token: 0x040049F6 RID: 18934
		private UIState _menuToGoBackTo;

		// Token: 0x040049F7 RID: 18935
		private bool skipDraw;
	}
}
