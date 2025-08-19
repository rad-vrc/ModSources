using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent.Events;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x0200034A RID: 842
	public class UIEmotesMenu : UIState
	{
		// Token: 0x06002641 RID: 9793 RVA: 0x00573260 File Offset: 0x00571460
		public override void OnActivate()
		{
			this.InitializePage();
			if (Main.gameMenu)
			{
				this._outerContainer.Top.Set(220f, 0f);
				this._outerContainer.Height.Set(-220f, 1f);
				return;
			}
			this._outerContainer.Top.Set(120f, 0f);
			this._outerContainer.Height.Set(-120f, 1f);
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x005732E4 File Offset: 0x005714E4
		public void InitializePage()
		{
			base.RemoveAllChildren();
			UIElement uielement = new UIElement();
			uielement.Width.Set(590f, 0f);
			uielement.Top.Set(220f, 0f);
			uielement.Height.Set(-220f, 1f);
			uielement.HAlign = 0.5f;
			this._outerContainer = uielement;
			base.Append(uielement);
			UIPanel uipanel = new UIPanel();
			uipanel.Width.Set(0f, 1f);
			uipanel.Height.Set(-110f, 1f);
			uipanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
			uipanel.PaddingTop = 0f;
			uielement.Append(uipanel);
			this._container = uipanel;
			UIList uilist = new UIList();
			uilist.Width.Set(-25f, 1f);
			uilist.Height.Set(-50f, 1f);
			uilist.Top.Set(50f, 0f);
			uilist.HAlign = 0.5f;
			uilist.ListPadding = 14f;
			uipanel.Append(uilist);
			this._list = uilist;
			UIScrollbar uiscrollbar = new UIScrollbar();
			uiscrollbar.SetView(100f, 1000f);
			uiscrollbar.Height.Set(-20f, 1f);
			uiscrollbar.HAlign = 1f;
			uiscrollbar.VAlign = 1f;
			uiscrollbar.Top = StyleDimension.FromPixels(-5f);
			uilist.SetScrollbar(uiscrollbar);
			this._scrollBar = uiscrollbar;
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
			uitextPanel.Width.Set(-10f, 0.5f);
			uitextPanel.Height.Set(50f, 0f);
			uitextPanel.VAlign = 1f;
			uitextPanel.HAlign = 0.5f;
			uitextPanel.Top.Set(-45f, 0f);
			uitextPanel.OnMouseOver += this.FadedMouseOver;
			uitextPanel.OnMouseOut += this.FadedMouseOut;
			uitextPanel.OnLeftClick += this.GoBackClick;
			uitextPanel.SetSnapPoint("Back", 0, null, null);
			uielement.Append(uitextPanel);
			this._backPanel = uitextPanel;
			int num = 0;
			this.TryAddingList(Language.GetText("UI.EmoteCategoryGeneral"), ref num, 10, this.GetEmotesGeneral());
			this.TryAddingList(Language.GetText("UI.EmoteCategoryRPS"), ref num, 10, this.GetEmotesRPS());
			this.TryAddingList(Language.GetText("UI.EmoteCategoryItems"), ref num, 11, this.GetEmotesItems());
			this.TryAddingList(Language.GetText("UI.EmoteCategoryBiomesAndEvents"), ref num, 8, this.GetEmotesBiomesAndEvents());
			this.TryAddingList(Language.GetText("UI.EmoteCategoryTownNPCs"), ref num, 9, this.GetEmotesTownNPCs());
			this.TryAddingList(Language.GetText("UI.EmoteCategoryCritters"), ref num, 7, this.GetEmotesCritters());
			this.TryAddingList(Language.GetText("UI.EmoteCategoryBosses"), ref num, 8, this.GetEmotesBosses());
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x00573614 File Offset: 0x00571814
		private void TryAddingList(LocalizedText title, ref int currentGroupIndex, int maxEmotesPerRow, List<int> emoteIds)
		{
			if (emoteIds == null)
			{
				return;
			}
			if (emoteIds.Count == 0)
			{
				return;
			}
			UIList list = this._list;
			int num = currentGroupIndex;
			currentGroupIndex = num + 1;
			list.Add(new EmotesGroupListItem(title, num, maxEmotesPerRow, emoteIds.ToArray()));
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x00573654 File Offset: 0x00571854
		private List<int> GetEmotesGeneral()
		{
			return new List<int>
			{
				0,
				1,
				2,
				3,
				15,
				136,
				94,
				16,
				135,
				134,
				137,
				138,
				139,
				17,
				87,
				88,
				89,
				91,
				92,
				93,
				8,
				9,
				10,
				11,
				14,
				100,
				146,
				147,
				148
			};
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x00573764 File Offset: 0x00571964
		private List<int> GetEmotesRPS()
		{
			return new List<int>
			{
				36,
				37,
				38,
				33,
				34,
				35
			};
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x0057379C File Offset: 0x0057199C
		private List<int> GetEmotesItems()
		{
			return new List<int>
			{
				7,
				73,
				74,
				75,
				76,
				131,
				77,
				78,
				79,
				80,
				81,
				82,
				83,
				84,
				85,
				86,
				90,
				132,
				126,
				127,
				128,
				129,
				149
			};
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x00573874 File Offset: 0x00571A74
		private List<int> GetEmotesBiomesAndEvents()
		{
			return new List<int>
			{
				22,
				23,
				24,
				25,
				26,
				27,
				28,
				29,
				30,
				31,
				32,
				18,
				19,
				20,
				21,
				99,
				4,
				5,
				6,
				95,
				96,
				97,
				98
			};
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x0057393C File Offset: 0x00571B3C
		private List<int> GetEmotesTownNPCs()
		{
			return new List<int>
			{
				101,
				102,
				103,
				104,
				105,
				106,
				107,
				108,
				109,
				110,
				111,
				112,
				113,
				114,
				115,
				116,
				117,
				118,
				119,
				120,
				121,
				122,
				123,
				124,
				125,
				130,
				140,
				141,
				142,
				145
			};
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x00573A50 File Offset: 0x00571C50
		private List<int> GetEmotesCritters()
		{
			List<int> list = new List<int>();
			list.AddRange(new int[]
			{
				12,
				13,
				61,
				62,
				63
			});
			list.AddRange(new int[]
			{
				67,
				68,
				69,
				70
			});
			list.Add(72);
			if (NPC.downedGoblins)
			{
				list.Add(64);
			}
			if (NPC.downedFrost)
			{
				list.Add(66);
			}
			if (NPC.downedPirates)
			{
				list.Add(65);
			}
			if (NPC.downedMartians)
			{
				list.Add(71);
			}
			return list;
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x00573AD8 File Offset: 0x00571CD8
		private List<int> GetEmotesBosses()
		{
			List<int> list = new List<int>();
			if (NPC.downedBoss1)
			{
				list.Add(39);
			}
			if (NPC.downedBoss2)
			{
				list.Add(40);
				list.Add(41);
			}
			if (NPC.downedSlimeKing)
			{
				list.Add(51);
			}
			if (NPC.downedDeerclops)
			{
				list.Add(150);
			}
			if (NPC.downedQueenBee)
			{
				list.Add(42);
			}
			if (NPC.downedBoss3)
			{
				list.Add(43);
			}
			if (Main.hardMode)
			{
				list.Add(44);
			}
			if (NPC.downedQueenSlime)
			{
				list.Add(144);
			}
			if (NPC.downedMechBoss1)
			{
				list.Add(45);
			}
			if (NPC.downedMechBoss3)
			{
				list.Add(46);
			}
			if (NPC.downedMechBoss2)
			{
				list.Add(47);
			}
			if (NPC.downedPlantBoss)
			{
				list.Add(48);
			}
			if (NPC.downedGolemBoss)
			{
				list.Add(49);
			}
			if (NPC.downedFishron)
			{
				list.Add(50);
			}
			if (NPC.downedEmpressOfLight)
			{
				list.Add(143);
			}
			if (NPC.downedAncientCultist)
			{
				list.Add(52);
			}
			if (NPC.downedMoonlord)
			{
				list.Add(53);
			}
			if (NPC.downedHalloweenTree)
			{
				list.Add(54);
			}
			if (NPC.downedHalloweenKing)
			{
				list.Add(55);
			}
			if (NPC.downedChristmasTree)
			{
				list.Add(56);
			}
			if (NPC.downedChristmasIceQueen)
			{
				list.Add(57);
			}
			if (NPC.downedChristmasSantank)
			{
				list.Add(58);
			}
			if (NPC.downedPirates)
			{
				list.Add(59);
			}
			if (NPC.downedMartians)
			{
				list.Add(60);
			}
			if (DD2Event.DownedInvasionAnyDifficulty)
			{
				list.Add(133);
			}
			return list;
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x00573C78 File Offset: 0x00571E78
		public override void Recalculate()
		{
			if (this._scrollBar != null)
			{
				if (this._isScrollbarAttached && !this._scrollBar.CanScroll)
				{
					this._container.RemoveChild(this._scrollBar);
					this._isScrollbarAttached = false;
					this._list.Width.Set(0f, 1f);
				}
				else if (!this._isScrollbarAttached && this._scrollBar.CanScroll)
				{
					this._container.Append(this._scrollBar);
					this._isScrollbarAttached = true;
					this._list.Width.Set(-25f, 1f);
				}
			}
			base.Recalculate();
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x00573D26 File Offset: 0x00571F26
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.menuMode = 0;
			IngameFancyUI.Close();
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x00573D34 File Offset: 0x00571F34
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x0056C33D File Offset: 0x0056A53D
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x00573D89 File Offset: 0x00571F89
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.SetupGamepadPoints2(spriteBatch);
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x00573D9C File Offset: 0x00571F9C
		private void SetupGamepadPoints2(SpriteBatch spriteBatch)
		{
			int num = 7;
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 1;
			int num2;
			int id = num2 = 3001;
			List<SnapPoint> snapPoints = this.GetSnapPoints();
			this.RemoveSnapPointsOutOfScreen(spriteBatch, snapPoints);
			UILinkPointNavigator.SetPosition(id, this._backPanel.GetInnerDimensions().ToRectangle().Center.ToVector2());
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[num2];
			uilinkPoint.Unlink();
			uilinkPoint.Up = num2 + 1;
			UILinkPoint uilinkPoint2 = uilinkPoint;
			num2++;
			int num3 = 0;
			List<List<SnapPoint>> list = new List<List<SnapPoint>>();
			for (int i = 0; i < num; i++)
			{
				List<SnapPoint> emoteGroup = this.GetEmoteGroup(snapPoints, i);
				if (emoteGroup.Count > 0)
				{
					list.Add(emoteGroup);
				}
				num3 += (int)Math.Ceiling((double)((float)emoteGroup.Count / 14f));
			}
			SnapPoint[,] array = new SnapPoint[14, num3];
			int num4 = 0;
			for (int j = 0; j < list.Count; j++)
			{
				List<SnapPoint> list2 = list[j];
				for (int k = 0; k < list2.Count; k++)
				{
					int num5 = num4 + k / 14;
					int num6 = k % 14;
					array[num6, num5] = list2[k];
				}
				num4 += (int)Math.Ceiling((double)((float)list2.Count / 14f));
			}
			int[,] array2 = new int[14, num3];
			int up = 0;
			for (int l = 0; l < array.GetLength(1); l++)
			{
				for (int m = 0; m < array.GetLength(0); m++)
				{
					SnapPoint snapPoint = array[m, l];
					if (snapPoint != null)
					{
						UILinkPointNavigator.Points[num2].Unlink();
						UILinkPointNavigator.SetPosition(num2, snapPoint.Position);
						array2[m, l] = num2;
						if (m == 0)
						{
							up = num2;
						}
						num2++;
					}
				}
			}
			uilinkPoint2.Up = up;
			for (int n = 0; n < array.GetLength(1); n++)
			{
				for (int num7 = 0; num7 < array.GetLength(0); num7++)
				{
					int num8 = array2[num7, n];
					if (num8 != 0)
					{
						UILinkPoint uilinkPoint3 = UILinkPointNavigator.Points[num8];
						if (this.TryGetPointOnGrid(array2, num7, n, -1, 0))
						{
							uilinkPoint3.Left = array2[num7 - 1, n];
						}
						else
						{
							uilinkPoint3.Left = uilinkPoint3.ID;
							for (int num9 = num7; num9 < array.GetLength(0); num9++)
							{
								if (this.TryGetPointOnGrid(array2, num9, n, 0, 0))
								{
									uilinkPoint3.Left = array2[num9, n];
								}
							}
						}
						if (this.TryGetPointOnGrid(array2, num7, n, 1, 0))
						{
							uilinkPoint3.Right = array2[num7 + 1, n];
						}
						else
						{
							uilinkPoint3.Right = uilinkPoint3.ID;
							for (int num10 = num7; num10 >= 0; num10--)
							{
								if (this.TryGetPointOnGrid(array2, num10, n, 0, 0))
								{
									uilinkPoint3.Right = array2[num10, n];
								}
							}
						}
						if (this.TryGetPointOnGrid(array2, num7, n, 0, -1))
						{
							uilinkPoint3.Up = array2[num7, n - 1];
						}
						else
						{
							uilinkPoint3.Up = uilinkPoint3.ID;
							for (int num11 = n - 1; num11 >= 0; num11--)
							{
								if (this.TryGetPointOnGrid(array2, num7, num11, 0, 0))
								{
									uilinkPoint3.Up = array2[num7, num11];
									break;
								}
							}
						}
						if (this.TryGetPointOnGrid(array2, num7, n, 0, 1))
						{
							uilinkPoint3.Down = array2[num7, n + 1];
						}
						else
						{
							uilinkPoint3.Down = uilinkPoint3.ID;
							for (int num12 = n + 1; num12 < array.GetLength(1); num12++)
							{
								if (this.TryGetPointOnGrid(array2, num7, num12, 0, 0))
								{
									uilinkPoint3.Down = array2[num7, num12];
									break;
								}
							}
							if (uilinkPoint3.Down == uilinkPoint3.ID)
							{
								uilinkPoint3.Down = uilinkPoint2.ID;
							}
						}
					}
				}
			}
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x00574198 File Offset: 0x00572398
		private bool TryGetPointOnGrid(int[,] grid, int x, int y, int offsetX, int offsetY)
		{
			return x + offsetX >= 0 && x + offsetX < grid.GetLength(0) && y + offsetY >= 0 && y + offsetY < grid.GetLength(1) && grid[x + offsetX, y + offsetY] != 0;
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x005741E8 File Offset: 0x005723E8
		private void RemoveSnapPointsOutOfScreen(SpriteBatch spriteBatch, List<SnapPoint> pts)
		{
			float scaleFactor = 1f / Main.UIScale;
			Rectangle clippingRectangle = this._container.GetClippingRectangle(spriteBatch);
			Vector2 minimum = clippingRectangle.TopLeft() * scaleFactor;
			Vector2 maximum = clippingRectangle.BottomRight() * scaleFactor;
			for (int i = 0; i < pts.Count; i++)
			{
				if (!pts[i].Position.Between(minimum, maximum))
				{
					pts.Remove(pts[i]);
					i--;
				}
			}
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x00574260 File Offset: 0x00572460
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 1;
			int num = 3001;
			int num2 = num;
			List<SnapPoint> snapPoints = this.GetSnapPoints();
			UILinkPointNavigator.SetPosition(num, this._backPanel.GetInnerDimensions().ToRectangle().Center.ToVector2());
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[num2];
			uilinkPoint.Unlink();
			uilinkPoint.Up = num2 + 1;
			UILinkPoint uilinkPoint2 = uilinkPoint;
			num2++;
			float scaleFactor = 1f / Main.UIScale;
			Rectangle clippingRectangle = this._container.GetClippingRectangle(spriteBatch);
			Vector2 minimum = clippingRectangle.TopLeft() * scaleFactor;
			Vector2 maximum = clippingRectangle.BottomRight() * scaleFactor;
			for (int i = 0; i < snapPoints.Count; i++)
			{
				if (!snapPoints[i].Position.Between(minimum, maximum))
				{
					snapPoints.Remove(snapPoints[i]);
					i--;
				}
			}
			int num3 = 0;
			int num4 = 7;
			List<List<SnapPoint>> list = new List<List<SnapPoint>>();
			for (int j = 0; j < num4; j++)
			{
				List<SnapPoint> emoteGroup = this.GetEmoteGroup(snapPoints, j);
				if (emoteGroup.Count > 0)
				{
					list.Add(emoteGroup);
				}
			}
			List<SnapPoint>[] array = list.ToArray();
			for (int k = 0; k < array.Length; k++)
			{
				List<SnapPoint> list2 = array[k];
				int num5 = list2.Count / 14;
				if (list2.Count % 14 > 0)
				{
					num5++;
				}
				int num6 = 14;
				if (list2.Count % 14 != 0)
				{
					num6 = list2.Count % 14;
				}
				for (int l = 0; l < list2.Count; l++)
				{
					uilinkPoint = UILinkPointNavigator.Points[num2];
					uilinkPoint.Unlink();
					UILinkPointNavigator.SetPosition(num2, list2[l].Position);
					int num7 = 14;
					if (l / 14 == num5 - 1 && list2.Count % 14 != 0)
					{
						num7 = list2.Count % 14;
					}
					int num8 = l % 14;
					uilinkPoint.Left = num2 - 1;
					uilinkPoint.Right = num2 + 1;
					uilinkPoint.Up = num2 - 14;
					uilinkPoint.Down = num2 + 14;
					if (num8 == num7 - 1)
					{
						uilinkPoint.Right = num2 - num7 + 1;
					}
					if (num8 == 0)
					{
						uilinkPoint.Left = num2 + num7 - 1;
					}
					if (num8 == 0)
					{
						uilinkPoint2.Up = num2;
					}
					if (l < 14)
					{
						if (num3 == 0)
						{
							uilinkPoint.Up = -1;
						}
						else
						{
							uilinkPoint.Up = num2 - 14;
							if (num8 >= num3)
							{
								uilinkPoint.Up -= 14;
							}
							int num9 = k - 1;
							while (num9 > 0 && array[num9].Count <= num8)
							{
								uilinkPoint.Up -= 14;
								num9--;
							}
						}
					}
					int down = num;
					if (k == array.Length - 1)
					{
						if (l / 14 < num5 - 1 && num8 >= list2.Count % 14)
						{
							uilinkPoint.Down = down;
						}
						if (l / 14 == num5 - 1)
						{
							uilinkPoint.Down = down;
						}
					}
					else if (l / 14 == num5 - 1)
					{
						uilinkPoint.Down = num2 + 14;
						int num10 = k + 1;
						while (num10 < array.Length && array[num10].Count <= num8)
						{
							uilinkPoint.Down += 14;
							num10++;
						}
						if (k == array.Length - 1)
						{
							uilinkPoint.Down = down;
						}
					}
					else if (num8 >= num6)
					{
						uilinkPoint.Down = num2 + 14 + 14;
						int num11 = k + 1;
						while (num11 < array.Length && array[num11].Count <= num8)
						{
							uilinkPoint.Down += 14;
							num11++;
						}
					}
					num2++;
				}
				num3 = num6;
				int num12 = 14 - num3;
				num2 += num12;
			}
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x00574610 File Offset: 0x00572810
		private List<SnapPoint> GetEmoteGroup(List<SnapPoint> ptsOnPage, int groupIndex)
		{
			string groupName = "Group " + groupIndex;
			List<SnapPoint> list = (from a in ptsOnPage
			where a.Name == groupName
			select a).ToList<SnapPoint>();
			list.Sort(new Comparison<SnapPoint>(this.SortPoints));
			return list;
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x00574664 File Offset: 0x00572864
		private int SortPoints(SnapPoint a, SnapPoint b)
		{
			return a.Id.CompareTo(b.Id);
		}

		// Token: 0x04004A2F RID: 18991
		private UIElement _outerContainer;

		// Token: 0x04004A30 RID: 18992
		private UIElement _backPanel;

		// Token: 0x04004A31 RID: 18993
		private UIElement _container;

		// Token: 0x04004A32 RID: 18994
		private UIList _list;

		// Token: 0x04004A33 RID: 18995
		private UIScrollbar _scrollBar;

		// Token: 0x04004A34 RID: 18996
		private bool _isScrollbarAttached;
	}
}
