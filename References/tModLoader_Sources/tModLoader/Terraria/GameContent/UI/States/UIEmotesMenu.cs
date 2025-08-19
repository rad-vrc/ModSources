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
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020004DA RID: 1242
	public class UIEmotesMenu : UIState
	{
		// Token: 0x06003BC0 RID: 15296 RVA: 0x005B798C File Offset: 0x005B5B8C
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

		// Token: 0x06003BC1 RID: 15297 RVA: 0x005B7A10 File Offset: 0x005B5C10
		public void InitializePage()
		{
			base.RemoveAllChildren();
			UIElement uIElement = new UIElement();
			uIElement.Width.Set(590f, 0f);
			uIElement.Top.Set(220f, 0f);
			uIElement.Height.Set(-220f, 1f);
			uIElement.HAlign = 0.5f;
			this._outerContainer = uIElement;
			base.Append(uIElement);
			UIPanel uIPanel = new UIPanel();
			uIPanel.Width.Set(0f, 1f);
			uIPanel.Height.Set(-110f, 1f);
			uIPanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
			uIPanel.PaddingTop = 0f;
			uIElement.Append(uIPanel);
			this._container = uIPanel;
			UIList uIList = new UIList();
			uIList.Width.Set(-25f, 1f);
			uIList.Height.Set(-50f, 1f);
			uIList.Top.Set(50f, 0f);
			uIList.HAlign = 0.5f;
			uIList.ListPadding = 14f;
			uIPanel.Append(uIList);
			this._list = uIList;
			UIScrollbar uIScrollbar = new UIScrollbar();
			uIScrollbar.SetView(100f, 1000f);
			uIScrollbar.Height.Set(-20f, 1f);
			uIScrollbar.HAlign = 1f;
			uIScrollbar.VAlign = 1f;
			uIScrollbar.Top = StyleDimension.FromPixels(-5f);
			uIList.SetScrollbar(uIScrollbar);
			this._scrollBar = uIScrollbar;
			UITextPanel<LocalizedText> uITextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
			uITextPanel.Width.Set(-10f, 0.5f);
			uITextPanel.Height.Set(50f, 0f);
			uITextPanel.VAlign = 1f;
			uITextPanel.HAlign = 0.5f;
			uITextPanel.Top.Set(-45f, 0f);
			uITextPanel.OnMouseOver += this.FadedMouseOver;
			uITextPanel.OnMouseOut += this.FadedMouseOut;
			uITextPanel.OnLeftClick += this.GoBackClick;
			uITextPanel.SetSnapPoint("Back", 0, null, null);
			uIElement.Append(uITextPanel);
			this._backPanel = uITextPanel;
			int currentGroupIndex = 0;
			this.TryAddingList(Language.GetText("UI.EmoteCategoryGeneral"), ref currentGroupIndex, 10, UIEmotesMenu.GetEmotesGeneral());
			this.TryAddingList(Language.GetText("UI.EmoteCategoryRPS"), ref currentGroupIndex, 10, UIEmotesMenu.GetEmotesRPS());
			this.TryAddingList(Language.GetText("UI.EmoteCategoryItems"), ref currentGroupIndex, 11, UIEmotesMenu.GetEmotesItems());
			this.TryAddingList(Language.GetText("UI.EmoteCategoryBiomesAndEvents"), ref currentGroupIndex, 8, UIEmotesMenu.GetEmotesBiomesAndEvents());
			this.TryAddingList(Language.GetText("UI.EmoteCategoryTownNPCs"), ref currentGroupIndex, 9, UIEmotesMenu.GetEmotesTownNPCs());
			this.TryAddingList(Language.GetText("UI.EmoteCategoryCritters"), ref currentGroupIndex, 7, UIEmotesMenu.GetEmotesCritters());
			this.TryAddingList(Language.GetText("UI.EmoteCategoryBosses"), ref currentGroupIndex, 8, UIEmotesMenu.GetEmotesBosses());
			foreach (KeyValuePair<Mod, List<int>> keyValuePair in EmoteBubbleLoader.GetAllUnlockedModEmotes())
			{
				Mod mod2;
				List<int> list;
				keyValuePair.Deconstruct(out mod2, out list);
				Mod mod = mod2;
				List<int> emotes = list;
				this.TryAddingList(new LocalizedText(mod.Name, mod.DisplayName), ref currentGroupIndex, -1, emotes);
			}
			this._totalGroups = currentGroupIndex;
		}

		// Token: 0x06003BC2 RID: 15298 RVA: 0x005B7DB0 File Offset: 0x005B5FB0
		private void TryAddingList(LocalizedText title, ref int currentGroupIndex, int maxEmotesPerRow, List<int> emoteIds)
		{
			if (emoteIds != null && emoteIds.Count != 0)
			{
				UIList list = this._list;
				int num = currentGroupIndex;
				currentGroupIndex = num + 1;
				list.Add(new EmotesGroupListItem(title, num, maxEmotesPerRow, emoteIds.ToArray()));
			}
		}

		// Token: 0x06003BC3 RID: 15299 RVA: 0x005B7DEC File Offset: 0x005B5FEC
		public static List<int> GetEmotesGeneral()
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
			}.AddEmotesToCategory(0);
		}

		// Token: 0x06003BC4 RID: 15300 RVA: 0x005B7F02 File Offset: 0x005B6102
		public static List<int> GetEmotesRPS()
		{
			return new List<int>
			{
				36,
				37,
				38,
				33,
				34,
				35
			}.AddEmotesToCategory(1);
		}

		// Token: 0x06003BC5 RID: 15301 RVA: 0x005B7F40 File Offset: 0x005B6140
		public static List<int> GetEmotesItems()
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
			}.AddEmotesToCategory(2);
		}

		// Token: 0x06003BC6 RID: 15302 RVA: 0x005B8020 File Offset: 0x005B6220
		public static List<int> GetEmotesBiomesAndEvents()
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
			}.AddEmotesToCategory(3);
		}

		// Token: 0x06003BC7 RID: 15303 RVA: 0x005B80F0 File Offset: 0x005B62F0
		public static List<int> GetEmotesTownNPCs()
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
			}.AddEmotesToCategory(4);
		}

		// Token: 0x06003BC8 RID: 15304 RVA: 0x005B8208 File Offset: 0x005B6408
		public static List<int> GetEmotesCritters()
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
			list.AddEmotesToCategory(5);
			return list;
		}

		// Token: 0x06003BC9 RID: 15305 RVA: 0x005B8298 File Offset: 0x005B6498
		public static List<int> GetEmotesBosses()
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
			list.AddEmotesToCategory(6);
			return list;
		}

		// Token: 0x06003BCA RID: 15306 RVA: 0x005B8440 File Offset: 0x005B6640
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

		// Token: 0x06003BCB RID: 15307 RVA: 0x005B84EE File Offset: 0x005B66EE
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.menuMode = 0;
			IngameFancyUI.Close();
		}

		// Token: 0x06003BCC RID: 15308 RVA: 0x005B84FC File Offset: 0x005B66FC
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06003BCD RID: 15309 RVA: 0x005B8551 File Offset: 0x005B6751
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06003BCE RID: 15310 RVA: 0x005B8590 File Offset: 0x005B6790
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.SetupGamepadPoints2(spriteBatch);
		}

		// Token: 0x06003BCF RID: 15311 RVA: 0x005B85A0 File Offset: 0x005B67A0
		private void SetupGamepadPoints2(SpriteBatch spriteBatch)
		{
			int num = this._totalGroups;
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 1;
			int num2;
			int id = num2 = 3001;
			List<SnapPoint> snapPoints = this.GetSnapPoints();
			this.RemoveSnapPointsOutOfScreen(spriteBatch, snapPoints);
			UILinkPointNavigator.SetPosition(id, this._backPanel.GetInnerDimensions().ToRectangle().Center.ToVector2());
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[num2];
			uilinkPoint.Unlink();
			uilinkPoint.Up = num2 + 1;
			UILinkPoint uILinkPoint2 = uilinkPoint;
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
			uILinkPoint2.Up = up;
			for (int n = 0; n < array.GetLength(1); n++)
			{
				for (int num7 = 0; num7 < array.GetLength(0); num7++)
				{
					int num8 = array2[num7, n];
					if (num8 != 0)
					{
						UILinkPoint uILinkPoint3 = UILinkPointNavigator.Points[num8];
						if (this.TryGetPointOnGrid(array2, num7, n, -1, 0))
						{
							uILinkPoint3.Left = array2[num7 - 1, n];
						}
						else
						{
							uILinkPoint3.Left = uILinkPoint3.ID;
							for (int num9 = num7; num9 < array.GetLength(0); num9++)
							{
								if (this.TryGetPointOnGrid(array2, num9, n, 0, 0))
								{
									uILinkPoint3.Left = array2[num9, n];
								}
							}
						}
						if (this.TryGetPointOnGrid(array2, num7, n, 1, 0))
						{
							uILinkPoint3.Right = array2[num7 + 1, n];
						}
						else
						{
							uILinkPoint3.Right = uILinkPoint3.ID;
							for (int num10 = num7; num10 >= 0; num10--)
							{
								if (this.TryGetPointOnGrid(array2, num10, n, 0, 0))
								{
									uILinkPoint3.Right = array2[num10, n];
								}
							}
						}
						if (this.TryGetPointOnGrid(array2, num7, n, 0, -1))
						{
							uILinkPoint3.Up = array2[num7, n - 1];
						}
						else
						{
							uILinkPoint3.Up = uILinkPoint3.ID;
							for (int num11 = n - 1; num11 >= 0; num11--)
							{
								if (this.TryGetPointOnGrid(array2, num7, num11, 0, 0))
								{
									uILinkPoint3.Up = array2[num7, num11];
									break;
								}
							}
						}
						if (this.TryGetPointOnGrid(array2, num7, n, 0, 1))
						{
							uILinkPoint3.Down = array2[num7, n + 1];
						}
						else
						{
							uILinkPoint3.Down = uILinkPoint3.ID;
							for (int num12 = n + 1; num12 < array.GetLength(1); num12++)
							{
								if (this.TryGetPointOnGrid(array2, num7, num12, 0, 0))
								{
									uILinkPoint3.Down = array2[num7, num12];
									break;
								}
							}
							if (uILinkPoint3.Down == uILinkPoint3.ID)
							{
								uILinkPoint3.Down = uILinkPoint2.ID;
							}
						}
					}
				}
			}
		}

		// Token: 0x06003BD0 RID: 15312 RVA: 0x005B89A0 File Offset: 0x005B6BA0
		private bool TryGetPointOnGrid(int[,] grid, int x, int y, int offsetX, int offsetY)
		{
			return x + offsetX >= 0 && x + offsetX < grid.GetLength(0) && y + offsetY >= 0 && y + offsetY < grid.GetLength(1) && grid[x + offsetX, y + offsetY] != 0;
		}

		// Token: 0x06003BD1 RID: 15313 RVA: 0x005B89F0 File Offset: 0x005B6BF0
		private void RemoveSnapPointsOutOfScreen(SpriteBatch spriteBatch, List<SnapPoint> pts)
		{
			float num = 1f / Main.UIScale;
			Rectangle clippingRectangle = this._container.GetClippingRectangle(spriteBatch);
			Vector2 minimum = clippingRectangle.TopLeft() * num;
			Vector2 maximum = clippingRectangle.BottomRight() * num;
			for (int i = 0; i < pts.Count; i++)
			{
				if (!pts[i].Position.Between(minimum, maximum))
				{
					pts.Remove(pts[i]);
					i--;
				}
			}
		}

		// Token: 0x06003BD2 RID: 15314 RVA: 0x005B8A68 File Offset: 0x005B6C68
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 1;
			int num = 3001;
			int num2 = num;
			List<SnapPoint> snapPoints = this.GetSnapPoints();
			UILinkPointNavigator.SetPosition(num, this._backPanel.GetInnerDimensions().ToRectangle().Center.ToVector2());
			UILinkPoint uILinkPoint = UILinkPointNavigator.Points[num2];
			uILinkPoint.Unlink();
			uILinkPoint.Up = num2 + 1;
			UILinkPoint uILinkPoint2 = uILinkPoint;
			num2++;
			float num3 = 1f / Main.UIScale;
			Rectangle clippingRectangle = this._container.GetClippingRectangle(spriteBatch);
			Vector2 minimum = clippingRectangle.TopLeft() * num3;
			Vector2 maximum = clippingRectangle.BottomRight() * num3;
			for (int i = 0; i < snapPoints.Count; i++)
			{
				if (!snapPoints[i].Position.Between(minimum, maximum))
				{
					snapPoints.Remove(snapPoints[i]);
					i--;
				}
			}
			int num4 = 0;
			int num5 = 7;
			List<List<SnapPoint>> list = new List<List<SnapPoint>>();
			for (int j = 0; j < num5; j++)
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
				int num6 = list2.Count / 14;
				if (list2.Count % 14 > 0)
				{
					num6++;
				}
				int num7 = 14;
				if (list2.Count % 14 != 0)
				{
					num7 = list2.Count % 14;
				}
				for (int l = 0; l < list2.Count; l++)
				{
					uILinkPoint = UILinkPointNavigator.Points[num2];
					uILinkPoint.Unlink();
					UILinkPointNavigator.SetPosition(num2, list2[l].Position);
					int num8 = 14;
					if (l / 14 == num6 - 1 && list2.Count % 14 != 0)
					{
						num8 = list2.Count % 14;
					}
					int num9 = l % 14;
					uILinkPoint.Left = num2 - 1;
					uILinkPoint.Right = num2 + 1;
					uILinkPoint.Up = num2 - 14;
					uILinkPoint.Down = num2 + 14;
					if (num9 == num8 - 1)
					{
						uILinkPoint.Right = num2 - num8 + 1;
					}
					if (num9 == 0)
					{
						uILinkPoint.Left = num2 + num8 - 1;
					}
					if (num9 == 0)
					{
						uILinkPoint2.Up = num2;
					}
					if (l < 14)
					{
						if (num4 == 0)
						{
							uILinkPoint.Up = -1;
						}
						else
						{
							uILinkPoint.Up = num2 - 14;
							if (num9 >= num4)
							{
								uILinkPoint.Up -= 14;
							}
							int num10 = k - 1;
							while (num10 > 0 && array[num10].Count <= num9)
							{
								uILinkPoint.Up -= 14;
								num10--;
							}
						}
					}
					int down = num;
					if (k == array.Length - 1)
					{
						if (l / 14 < num6 - 1 && num9 >= list2.Count % 14)
						{
							uILinkPoint.Down = down;
						}
						if (l / 14 == num6 - 1)
						{
							uILinkPoint.Down = down;
						}
					}
					else if (l / 14 == num6 - 1)
					{
						uILinkPoint.Down = num2 + 14;
						int m = k + 1;
						while (m < array.Length && array[m].Count <= num9)
						{
							uILinkPoint.Down += 14;
							m++;
						}
						if (k == array.Length - 1)
						{
							uILinkPoint.Down = down;
						}
					}
					else if (num9 >= num7)
					{
						uILinkPoint.Down = num2 + 14 + 14;
						int n = k + 1;
						while (n < array.Length && array[n].Count <= num9)
						{
							uILinkPoint.Down += 14;
							n++;
						}
					}
					num2++;
				}
				num4 = num7;
				int num11 = 14 - num4;
				num2 += num11;
			}
		}

		// Token: 0x06003BD3 RID: 15315 RVA: 0x005B8E18 File Offset: 0x005B7018
		private List<SnapPoint> GetEmoteGroup(List<SnapPoint> ptsOnPage, int groupIndex)
		{
			string groupName = "Group " + groupIndex.ToString();
			List<SnapPoint> list = (from a in ptsOnPage
			where a.Name == groupName
			select a).ToList<SnapPoint>();
			list.Sort(new Comparison<SnapPoint>(this.SortPoints));
			return list;
		}

		// Token: 0x06003BD4 RID: 15316 RVA: 0x005B8E6C File Offset: 0x005B706C
		private int SortPoints(SnapPoint a, SnapPoint b)
		{
			return a.Id.CompareTo(b.Id);
		}

		// Token: 0x0400553D RID: 21821
		private UIElement _outerContainer;

		// Token: 0x0400553E RID: 21822
		private UIElement _backPanel;

		// Token: 0x0400553F RID: 21823
		private UIElement _container;

		// Token: 0x04005540 RID: 21824
		private UIList _list;

		// Token: 0x04005541 RID: 21825
		private UIScrollbar _scrollBar;

		// Token: 0x04005542 RID: 21826
		private bool _isScrollbarAttached;

		// Token: 0x04005543 RID: 21827
		private int _totalGroups = 7;
	}
}
