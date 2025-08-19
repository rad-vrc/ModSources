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
	// Token: 0x02000352 RID: 850
	public class UIWorldSelect : UIState
	{
		// Token: 0x06002742 RID: 10050 RVA: 0x005814CC File Offset: 0x0057F6CC
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
			this._worldList = new UIList();
			this._worldList.Width.Set(0f, 1f);
			this._worldList.Height.Set(0f, 1f);
			this._worldList.ListPadding = 5f;
			uipanel.Append(this._worldList);
			this._scrollbar = new UIScrollbar();
			this._scrollbar.SetView(100f, 1000f);
			this._scrollbar.Height.Set(0f, 1f);
			this._scrollbar.HAlign = 1f;
			this._worldList.SetScrollbar(this._scrollbar);
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.SelectWorld"), 0.8f, true);
			uitextPanel.HAlign = 0.5f;
			uitextPanel.Top.Set(-40f, 0f);
			uitextPanel.SetPadding(15f);
			uitextPanel.BackgroundColor = new Color(73, 94, 171);
			uielement.Append(uitextPanel);
			UITextPanel<LocalizedText> uitextPanel2 = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
			uitextPanel2.Width.Set(-10f, 0.5f);
			uitextPanel2.Height.Set(50f, 0f);
			uitextPanel2.VAlign = 1f;
			uitextPanel2.HAlign = 0f;
			uitextPanel2.Top.Set(-45f, 0f);
			uitextPanel2.OnMouseOver += this.FadedMouseOver;
			uitextPanel2.OnMouseOut += this.FadedMouseOut;
			uitextPanel2.OnLeftClick += this.GoBackClick;
			uielement.Append(uitextPanel2);
			this._backPanel = uitextPanel2;
			UITextPanel<LocalizedText> uitextPanel3 = new UITextPanel<LocalizedText>(Language.GetText("UI.New"), 0.7f, true);
			uitextPanel3.CopyStyle(uitextPanel2);
			uitextPanel3.HAlign = 1f;
			uitextPanel3.OnMouseOver += this.FadedMouseOver;
			uitextPanel3.OnMouseOut += this.FadedMouseOut;
			uitextPanel3.OnLeftClick += this.NewWorldClick;
			uielement.Append(uitextPanel3);
			this._newPanel = uitextPanel3;
			base.Append(uielement);
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x005817D4 File Offset: 0x0057F9D4
		public override void Recalculate()
		{
			if (this._scrollbar != null)
			{
				if (this._isScrollbarAttached && !this._scrollbar.CanScroll)
				{
					this._containerPanel.RemoveChild(this._scrollbar);
					this._isScrollbarAttached = false;
					this._worldList.Width.Set(0f, 1f);
				}
				else if (!this._isScrollbarAttached && this._scrollbar.CanScroll)
				{
					this._containerPanel.Append(this._scrollbar);
					this._isScrollbarAttached = true;
					this._worldList.Width.Set(-25f, 1f);
				}
			}
			base.Recalculate();
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x00581884 File Offset: 0x0057FA84
		private void NewWorldClick(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.newWorldName = Lang.gen[57].Value + " " + (Main.WorldList.Count + 1);
			Main.menuMode = 888;
			Main.MenuUI.SetState(new UIWorldCreation());
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x005818EC File Offset: 0x0057FAEC
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.menuMode = (Main.menuMultiplayer ? 12 : 1);
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x00581914 File Offset: 0x0057FB14
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x0056E4FD File Offset: 0x0056C6FD
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.7f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x00581969 File Offset: 0x0057FB69
		public override void OnActivate()
		{
			Main.LoadWorlds();
			this.UpdateWorldsList();
			if (PlayerInput.UsingGamepadUI)
			{
				UILinkPointNavigator.ChangePoint(3000 + ((this._worldList.Count == 0) ? 1 : 2));
			}
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x0058199C File Offset: 0x0057FB9C
		private void UpdateWorldsList()
		{
			this._worldList.Clear();
			IEnumerable<WorldFileData> enumerable = new List<WorldFileData>(Main.WorldList).OrderByDescending(new Func<WorldFileData, bool>(this.CanWorldBePlayed)).ThenByDescending((WorldFileData x) => x.IsFavorite).ThenBy((WorldFileData x) => x.Name).ThenBy((WorldFileData x) => x.GetFileName(true));
			int num = 0;
			foreach (WorldFileData worldFileData in enumerable)
			{
				this._worldList.Add(new UIWorldListItem(worldFileData, num++, this.CanWorldBePlayed(worldFileData)));
			}
		}

		// Token: 0x0600274A RID: 10058 RVA: 0x00581A90 File Offset: 0x0057FC90
		private bool CanWorldBePlayed(WorldFileData file)
		{
			bool flag = Main.ActivePlayerFileData.Player.difficulty == 3;
			bool flag2 = file.GameMode == 3;
			return flag == flag2;
		}

		// Token: 0x0600274B RID: 10059 RVA: 0x00581ABC File Offset: 0x0057FCBC
		public override void Draw(SpriteBatch spriteBatch)
		{
			if (this.skipDraw)
			{
				this.skipDraw = false;
				return;
			}
			if (this.UpdateFavoritesCache())
			{
				this.skipDraw = true;
				Main.MenuUI.Draw(spriteBatch, new GameTime());
			}
			base.Draw(spriteBatch);
			this.SetupGamepadPoints(spriteBatch);
		}

		// Token: 0x0600274C RID: 10060 RVA: 0x00581AFC File Offset: 0x0057FCFC
		private bool UpdateFavoritesCache()
		{
			List<WorldFileData> list = new List<WorldFileData>(Main.WorldList);
			list.Sort(delegate(WorldFileData x, WorldFileData y)
			{
				if (x.IsFavorite && !y.IsFavorite)
				{
					return -1;
				}
				if (!x.IsFavorite && y.IsFavorite)
				{
					return 1;
				}
				if (x.Name == null)
				{
					return 1;
				}
				if (x.Name.CompareTo(y.Name) != 0)
				{
					return x.Name.CompareTo(y.Name);
				}
				return x.GetFileName(true).CompareTo(y.GetFileName(true));
			});
			bool flag = false;
			if (!flag && list.Count != this.favoritesCache.Count)
			{
				flag = true;
			}
			if (!flag)
			{
				for (int i = 0; i < this.favoritesCache.Count; i++)
				{
					Tuple<string, bool> tuple = this.favoritesCache[i];
					if (!(list[i].Name == tuple.Item1) || list[i].IsFavorite != tuple.Item2)
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				this.favoritesCache.Clear();
				foreach (WorldFileData worldFileData in list)
				{
					this.favoritesCache.Add(Tuple.Create<string, bool>(worldFileData.Name, worldFileData.IsFavorite));
				}
				this.UpdateWorldsList();
			}
			return flag;
		}

		// Token: 0x0600274D RID: 10061 RVA: 0x00581C1C File Offset: 0x0057FE1C
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 2;
			int num = 3000;
			UILinkPointNavigator.SetPosition(num, this._backPanel.GetInnerDimensions().ToRectangle().Center.ToVector2());
			UILinkPointNavigator.SetPosition(num + 1, this._newPanel.GetInnerDimensions().ToRectangle().Center.ToVector2());
			int num2 = num;
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[num2];
			uilinkPoint.Unlink();
			uilinkPoint.Right = num2 + 1;
			num2 = num + 1;
			uilinkPoint = UILinkPointNavigator.Points[num2];
			uilinkPoint.Unlink();
			uilinkPoint.Left = num2 - 1;
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
			SnapPoint[,] array = new SnapPoint[this._worldList.Count, 6];
			foreach (SnapPoint snapPoint in from a in snapPoints
			where a.Name == "Play"
			select a)
			{
				array[snapPoint.Id, 0] = snapPoint;
			}
			foreach (SnapPoint snapPoint2 in from a in snapPoints
			where a.Name == "Favorite"
			select a)
			{
				array[snapPoint2.Id, 1] = snapPoint2;
			}
			foreach (SnapPoint snapPoint3 in from a in snapPoints
			where a.Name == "Cloud"
			select a)
			{
				array[snapPoint3.Id, 2] = snapPoint3;
			}
			foreach (SnapPoint snapPoint4 in from a in snapPoints
			where a.Name == "Seed"
			select a)
			{
				array[snapPoint4.Id, 3] = snapPoint4;
			}
			foreach (SnapPoint snapPoint5 in from a in snapPoints
			where a.Name == "Rename"
			select a)
			{
				array[snapPoint5.Id, 4] = snapPoint5;
			}
			foreach (SnapPoint snapPoint6 in from a in snapPoints
			where a.Name == "Delete"
			select a)
			{
				array[snapPoint6.Id, 5] = snapPoint6;
			}
			num2 = num + 2;
			int[] array2 = new int[this._worldList.Count];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = -1;
			}
			for (int k = 0; k < array.GetLength(1); k++)
			{
				int num3 = -1;
				for (int l = 0; l < array.GetLength(0); l++)
				{
					if (array[l, k] != null)
					{
						uilinkPoint = UILinkPointNavigator.Points[num2];
						uilinkPoint.Unlink();
						UILinkPointNavigator.SetPosition(num2, array[l, k].Position);
						if (num3 != -1)
						{
							uilinkPoint.Up = num3;
							UILinkPointNavigator.Points[num3].Down = num2;
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
						num3 = num2;
						array2[l] = num2;
						UILinkPointNavigator.Shortcuts.FANCYUI_HIGHEST_INDEX = num2;
						num2++;
					}
				}
			}
			if (PlayerInput.UsingGamepadUI && this._worldList.Count == 0 && UILinkPointNavigator.CurrentPoint > 3001)
			{
				UILinkPointNavigator.ChangePoint(3001);
			}
		}

		// Token: 0x04004ABD RID: 19133
		private UIList _worldList;

		// Token: 0x04004ABE RID: 19134
		private UITextPanel<LocalizedText> _backPanel;

		// Token: 0x04004ABF RID: 19135
		private UITextPanel<LocalizedText> _newPanel;

		// Token: 0x04004AC0 RID: 19136
		private UITextPanel<LocalizedText> _workshopPanel;

		// Token: 0x04004AC1 RID: 19137
		private UIPanel _containerPanel;

		// Token: 0x04004AC2 RID: 19138
		private UIScrollbar _scrollbar;

		// Token: 0x04004AC3 RID: 19139
		private bool _isScrollbarAttached;

		// Token: 0x04004AC4 RID: 19140
		private List<Tuple<string, bool>> favoritesCache = new List<Tuple<string, bool>>();

		// Token: 0x04004AC5 RID: 19141
		private bool skipDraw;
	}
}
