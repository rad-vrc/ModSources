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
	// Token: 0x020004E5 RID: 1253
	public class UIWorkshopWorldImport : UIState, IHaveBackButtonCommand
	{
		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06003CB6 RID: 15542 RVA: 0x005C32A0 File Offset: 0x005C14A0
		// (set) Token: 0x06003CB7 RID: 15543 RVA: 0x005C32A8 File Offset: 0x005C14A8
		public UIState PreviousUIState
		{
			get
			{
				return this._uiStateToGoBackTo;
			}
			set
			{
				this._uiStateToGoBackTo = value;
			}
		}

		// Token: 0x06003CB8 RID: 15544 RVA: 0x005C32B1 File Offset: 0x005C14B1
		public UIWorkshopWorldImport(UIState uiStateToGoBackTo)
		{
			this._uiStateToGoBackTo = uiStateToGoBackTo;
		}

		// Token: 0x06003CB9 RID: 15545 RVA: 0x005C32CC File Offset: 0x005C14CC
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
			this._worldList = new UIList();
			this._worldList.Width.Set(0f, 1f);
			this._worldList.Height.Set(0f, 1f);
			this._worldList.ListPadding = 5f;
			uIPanel.Append(this._worldList);
			this._scrollbar = new UIScrollbar();
			this._scrollbar.SetView(100f, 1000f);
			this._scrollbar.Height.Set(0f, 1f);
			this._scrollbar.HAlign = 1f;
			this._worldList.SetScrollbar(this._scrollbar);
			UITextPanel<LocalizedText> uITextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.WorkshopImportWorld"), 0.8f, true);
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

		// Token: 0x06003CBA RID: 15546 RVA: 0x005C3560 File Offset: 0x005C1760
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

		// Token: 0x06003CBB RID: 15547 RVA: 0x005C360E File Offset: 0x005C180E
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			this.HandleBackButtonUsage();
		}

		// Token: 0x06003CBC RID: 15548 RVA: 0x005C3616 File Offset: 0x005C1816
		public void HandleBackButtonUsage()
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.MenuUI.SetState(this._uiStateToGoBackTo);
		}

		// Token: 0x06003CBD RID: 15549 RVA: 0x005C3640 File Offset: 0x005C1840
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06003CBE RID: 15550 RVA: 0x005C3695 File Offset: 0x005C1895
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.7f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06003CBF RID: 15551 RVA: 0x005C36D4 File Offset: 0x005C18D4
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

		// Token: 0x06003CC0 RID: 15552 RVA: 0x005C370C File Offset: 0x005C190C
		public void UpdateWorkshopWorldList()
		{
			UIWorkshopWorldImport.WorkshopWorldList.Clear();
			if (SocialAPI.Workshop == null)
			{
				return;
			}
			foreach (string listOfSubscribedWorldPath in SocialAPI.Workshop.GetListOfSubscribedWorldPaths())
			{
				WorldFileData allMetadata = WorldFile.GetAllMetadata(listOfSubscribedWorldPath, false);
				if (allMetadata != null)
				{
					UIWorkshopWorldImport.WorkshopWorldList.Add(allMetadata);
				}
				else
				{
					UIWorkshopWorldImport.WorkshopWorldList.Add(WorldFileData.FromInvalidWorld(listOfSubscribedWorldPath, false));
				}
			}
		}

		// Token: 0x06003CC1 RID: 15553 RVA: 0x005C3798 File Offset: 0x005C1998
		private void UpdateWorldsList()
		{
			this._worldList.Clear();
			IEnumerable<WorldFileData> enumerable = from x in new List<WorldFileData>(UIWorkshopWorldImport.WorkshopWorldList)
			orderby x.IsFavorite descending, x.Name, x.GetFileName(true)
			select x;
			int num = 0;
			foreach (WorldFileData item in enumerable)
			{
				this._worldList.Add(new UIWorkshopImportWorldListItem(this, item, num++));
			}
		}

		// Token: 0x06003CC2 RID: 15554 RVA: 0x005C3874 File Offset: 0x005C1A74
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

		// Token: 0x06003CC3 RID: 15555 RVA: 0x005C3894 File Offset: 0x005C1A94
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
			int num = 3000;
			UILinkPointNavigator.SetPosition(num, this._backPanel.GetInnerDimensions().ToRectangle().Center.ToVector2());
			int key = num;
			UILinkPoint uILinkPoint = UILinkPointNavigator.Points[key];
			uILinkPoint.Unlink();
			float num2 = 1f / Main.UIScale;
			Rectangle clippingRectangle = this._containerPanel.GetClippingRectangle(spriteBatch);
			Vector2 minimum = clippingRectangle.TopLeft() * num2;
			Vector2 maximum = clippingRectangle.BottomRight() * num2;
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
			foreach (SnapPoint item in from a in snapPoints
			where a.Name == "Import"
			select a)
			{
				array[item.Id, 0] = item;
			}
			key = num + 2;
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
						uILinkPoint = UILinkPointNavigator.Points[key];
						uILinkPoint.Unlink();
						UILinkPointNavigator.SetPosition(key, array[l, k].Position);
						if (num3 != -1)
						{
							uILinkPoint.Up = num3;
							UILinkPointNavigator.Points[num3].Down = key;
						}
						if (array2[l] != -1)
						{
							uILinkPoint.Left = array2[l];
							UILinkPointNavigator.Points[array2[l]].Right = key;
						}
						uILinkPoint.Down = num;
						if (k == 0)
						{
							UILinkPointNavigator.Points[num].Up = (UILinkPointNavigator.Points[num + 1].Up = key);
						}
						num3 = key;
						array2[l] = key;
						UILinkPointNavigator.Shortcuts.FANCYUI_HIGHEST_INDEX = key;
						key++;
					}
				}
			}
			if (PlayerInput.UsingGamepadUI && this._worldList.Count == 0 && UILinkPointNavigator.CurrentPoint > 3001)
			{
				UILinkPointNavigator.ChangePoint(3001);
			}
		}

		// Token: 0x040055D0 RID: 21968
		private UIList _worldList;

		// Token: 0x040055D1 RID: 21969
		private UITextPanel<LocalizedText> _backPanel;

		// Token: 0x040055D2 RID: 21970
		private UIPanel _containerPanel;

		// Token: 0x040055D3 RID: 21971
		private UIScrollbar _scrollbar;

		// Token: 0x040055D4 RID: 21972
		private bool _isScrollbarAttached;

		// Token: 0x040055D5 RID: 21973
		private List<Tuple<string, bool>> favoritesCache = new List<Tuple<string, bool>>();

		// Token: 0x040055D6 RID: 21974
		private UIState _uiStateToGoBackTo;

		// Token: 0x040055D7 RID: 21975
		public static List<WorldFileData> WorkshopWorldList = new List<WorldFileData>();

		// Token: 0x040055D8 RID: 21976
		private bool skipDraw;
	}
}
