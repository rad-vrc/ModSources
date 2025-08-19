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
using Terraria.Social;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x02000345 RID: 837
	public class UIWorkshopWorldImport : UIState, IHaveBackButtonCommand
	{
		// Token: 0x060025BD RID: 9661 RVA: 0x0056F100 File Offset: 0x0056D300
		public UIWorkshopWorldImport(UIState uiStateToGoBackTo)
		{
			this._uiStateToGoBackTo = uiStateToGoBackTo;
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x0056F11C File Offset: 0x0056D31C
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
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.WorkshopImportWorld"), 0.8f, true);
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

		// Token: 0x060025BF RID: 9663 RVA: 0x0056F3B0 File Offset: 0x0056D5B0
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

		// Token: 0x060025C0 RID: 9664 RVA: 0x0056F45E File Offset: 0x0056D65E
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			this.HandleBackButtonUsage();
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x0056F466 File Offset: 0x0056D666
		public void HandleBackButtonUsage()
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.MenuUI.SetState(this._uiStateToGoBackTo);
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x0056F490 File Offset: 0x0056D690
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x0056E4FD File Offset: 0x0056C6FD
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.7f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x0056F4E5 File Offset: 0x0056D6E5
		public override void OnActivate()
		{
			Main.LoadWorlds();
			this.UpdateWorkshopWorldList();
			this.UpdateWorldsList();
			if (PlayerInput.UsingGamepadUI)
			{
				UILinkPointNavigator.ChangePoint(3000 + ((this._worldList.Count == 0) ? 1 : 2));
			}
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x0056F51C File Offset: 0x0056D71C
		public void UpdateWorkshopWorldList()
		{
			UIWorkshopWorldImport.WorkshopWorldList.Clear();
			if (SocialAPI.Workshop != null)
			{
				foreach (string text in SocialAPI.Workshop.GetListOfSubscribedWorldPaths())
				{
					WorldFileData allMetadata = WorldFile.GetAllMetadata(text, false);
					if (allMetadata != null)
					{
						UIWorkshopWorldImport.WorkshopWorldList.Add(allMetadata);
					}
					else
					{
						UIWorkshopWorldImport.WorkshopWorldList.Add(WorldFileData.FromInvalidWorld(text, false));
					}
				}
			}
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x0056F5A8 File Offset: 0x0056D7A8
		private void UpdateWorldsList()
		{
			this._worldList.Clear();
			IEnumerable<WorldFileData> enumerable = from x in new List<WorldFileData>(UIWorkshopWorldImport.WorkshopWorldList)
			orderby x.IsFavorite descending, x.Name, x.GetFileName(true)
			select x;
			int num = 0;
			foreach (WorldFileData data in enumerable)
			{
				this._worldList.Add(new UIWorkshopImportWorldListItem(this, data, num++));
			}
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x0056F684 File Offset: 0x0056D884
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

		// Token: 0x060025C8 RID: 9672 RVA: 0x0056F6A4 File Offset: 0x0056D8A4
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
			int num = 3000;
			UILinkPointNavigator.SetPosition(num, this._backPanel.GetInnerDimensions().ToRectangle().Center.ToVector2());
			int num2 = num;
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[num2];
			uilinkPoint.Unlink();
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
			SnapPoint[,] array = new SnapPoint[this._worldList.Count, 1];
			foreach (SnapPoint snapPoint in from a in snapPoints
			where a.Name == "Import"
			select a)
			{
				array[snapPoint.Id, 0] = snapPoint;
			}
			num2 = num + 2;
			int[] array2 = new int[this._worldList.Count];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = -1;
			}
			for (int k = 0; k < 1; k++)
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

		// Token: 0x040049F8 RID: 18936
		private UIList _worldList;

		// Token: 0x040049F9 RID: 18937
		private UITextPanel<LocalizedText> _backPanel;

		// Token: 0x040049FA RID: 18938
		private UIPanel _containerPanel;

		// Token: 0x040049FB RID: 18939
		private UIScrollbar _scrollbar;

		// Token: 0x040049FC RID: 18940
		private bool _isScrollbarAttached;

		// Token: 0x040049FD RID: 18941
		private List<Tuple<string, bool>> favoritesCache = new List<Tuple<string, bool>>();

		// Token: 0x040049FE RID: 18942
		private UIState _uiStateToGoBackTo;

		// Token: 0x040049FF RID: 18943
		public static List<WorldFileData> WorkshopWorldList = new List<WorldFileData>();

		// Token: 0x04004A00 RID: 18944
		private bool skipDraw;
	}
}
