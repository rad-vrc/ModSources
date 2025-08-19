using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameInput;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x02000349 RID: 841
	public struct UIGamepadHelper
	{
		// Token: 0x0600262D RID: 9773 RVA: 0x00572B7C File Offset: 0x00570D7C
		public UILinkPoint[,] CreateUILinkPointGrid(ref int currentID, List<SnapPoint> pointsForGrid, int pointsPerLine, UILinkPoint topLinkPoint, UILinkPoint leftLinkPoint, UILinkPoint rightLinkPoint, UILinkPoint bottomLinkPoint)
		{
			int num = (int)Math.Ceiling((double)((float)pointsForGrid.Count / (float)pointsPerLine));
			UILinkPoint[,] array = new UILinkPoint[pointsPerLine, num];
			for (int i = 0; i < pointsForGrid.Count; i++)
			{
				int num2 = i % pointsPerLine;
				int num3 = i / pointsPerLine;
				UILinkPoint[,] array2 = array;
				int num4 = num2;
				int num5 = num3;
				int num6 = currentID;
				currentID = num6 + 1;
				array2[num4, num5] = this.MakeLinkPointFromSnapPoint(num6, pointsForGrid[i]);
			}
			for (int j = 0; j < array.GetLength(0); j++)
			{
				for (int k = 0; k < array.GetLength(1); k++)
				{
					UILinkPoint uilinkPoint = array[j, k];
					if (uilinkPoint != null)
					{
						if (j < array.GetLength(0) - 1)
						{
							UILinkPoint uilinkPoint2 = array[j + 1, k];
							if (uilinkPoint2 != null)
							{
								this.PairLeftRight(uilinkPoint, uilinkPoint2);
							}
						}
						if (k < array.GetLength(1) - 1)
						{
							UILinkPoint uilinkPoint3 = array[j, k + 1];
							if (uilinkPoint3 != null)
							{
								this.PairUpDown(uilinkPoint, uilinkPoint3);
							}
						}
						if (leftLinkPoint != null && j == 0)
						{
							uilinkPoint.Left = leftLinkPoint.ID;
						}
						if (topLinkPoint != null && k == 0)
						{
							uilinkPoint.Up = topLinkPoint.ID;
						}
						if (rightLinkPoint != null && j == pointsPerLine - 1)
						{
							uilinkPoint.Right = rightLinkPoint.ID;
						}
						if (bottomLinkPoint != null && k == num - 1)
						{
							uilinkPoint.Down = bottomLinkPoint.ID;
						}
					}
				}
			}
			return array;
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x00572CDC File Offset: 0x00570EDC
		public void LinkVerticalStrips(UILinkPoint[] stripOnLeft, UILinkPoint[] stripOnRight, int leftStripStartOffset)
		{
			if (stripOnLeft == null || stripOnRight == null)
			{
				return;
			}
			int num = Math.Max(stripOnLeft.Length, stripOnRight.Length);
			int num2 = Math.Min(stripOnLeft.Length, stripOnRight.Length);
			for (int i = 0; i < leftStripStartOffset; i++)
			{
				this.PairLeftRight(stripOnLeft[i], stripOnRight[0]);
			}
			for (int j = 0; j < num2; j++)
			{
				this.PairLeftRight(stripOnLeft[j + leftStripStartOffset], stripOnRight[j]);
			}
			for (int k = num2; k < num; k++)
			{
				if (stripOnLeft.Length > k)
				{
					stripOnLeft[k].Right = stripOnRight[stripOnRight.Length - 1].ID;
				}
				if (stripOnRight.Length > k)
				{
					stripOnRight[k].Left = stripOnLeft[stripOnLeft.Length - 1].ID;
				}
			}
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x00572D84 File Offset: 0x00570F84
		public void LinkVerticalStripRightSideToSingle(UILinkPoint[] strip, UILinkPoint theSingle)
		{
			if (strip == null || theSingle == null)
			{
				return;
			}
			int num = Math.Max(strip.Length, 1);
			int num2 = Math.Min(strip.Length, 1);
			for (int i = 0; i < num2; i++)
			{
				this.PairLeftRight(strip[i], theSingle);
			}
			for (int j = num2; j < num; j++)
			{
				if (strip.Length > j)
				{
					strip[j].Right = theSingle.ID;
				}
			}
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x00572DE4 File Offset: 0x00570FE4
		public void RemovePointsOutOfView(List<SnapPoint> pts, UIElement containerPanel, SpriteBatch spriteBatch)
		{
			float scaleFactor = 1f / Main.UIScale;
			Rectangle clippingRectangle = containerPanel.GetClippingRectangle(spriteBatch);
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

		// Token: 0x06002631 RID: 9777 RVA: 0x00572E58 File Offset: 0x00571058
		public void LinkHorizontalStripBottomSideToSingle(UILinkPoint[] strip, UILinkPoint theSingle)
		{
			if (strip == null || theSingle == null)
			{
				return;
			}
			for (int i = strip.Length - 1; i >= 0; i--)
			{
				this.PairUpDown(strip[i], theSingle);
			}
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x00572E88 File Offset: 0x00571088
		public void LinkHorizontalStripUpSideToSingle(UILinkPoint[] strip, UILinkPoint theSingle)
		{
			if (strip == null || theSingle == null)
			{
				return;
			}
			for (int i = strip.Length - 1; i >= 0; i--)
			{
				this.PairUpDown(theSingle, strip[i]);
			}
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x00572EB6 File Offset: 0x005710B6
		public void LinkVerticalStripBottomSideToSingle(UILinkPoint[] strip, UILinkPoint theSingle)
		{
			if (strip == null || theSingle == null)
			{
				return;
			}
			this.PairUpDown(strip[strip.Length - 1], theSingle);
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x00572ED0 File Offset: 0x005710D0
		public UILinkPoint[] CreateUILinkStripVertical(ref int currentID, List<SnapPoint> currentStrip)
		{
			UILinkPoint[] array = new UILinkPoint[currentStrip.Count];
			for (int i = 0; i < currentStrip.Count; i++)
			{
				UILinkPoint[] array2 = array;
				int num = i;
				int num2 = currentID;
				currentID = num2 + 1;
				array2[num] = this.MakeLinkPointFromSnapPoint(num2, currentStrip[i]);
			}
			for (int j = 0; j < currentStrip.Count - 1; j++)
			{
				this.PairUpDown(array[j], array[j + 1]);
			}
			return array;
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x00572F38 File Offset: 0x00571138
		public UILinkPoint[] CreateUILinkStripHorizontal(ref int currentID, List<SnapPoint> currentStrip)
		{
			UILinkPoint[] array = new UILinkPoint[currentStrip.Count];
			for (int i = 0; i < currentStrip.Count; i++)
			{
				UILinkPoint[] array2 = array;
				int num = i;
				int num2 = currentID;
				currentID = num2 + 1;
				array2[num] = this.MakeLinkPointFromSnapPoint(num2, currentStrip[i]);
			}
			for (int j = 0; j < currentStrip.Count - 1; j++)
			{
				this.PairLeftRight(array[j], array[j + 1]);
			}
			return array;
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x00572FA0 File Offset: 0x005711A0
		public void TryMovingBackIntoCreativeGridIfOutOfIt(int start, int currentID)
		{
			List<UILinkPoint> list = new List<UILinkPoint>();
			for (int i = start; i < currentID; i++)
			{
				list.Add(UILinkPointNavigator.Points[i]);
			}
			if (PlayerInput.UsingGamepadUI && UILinkPointNavigator.CurrentPoint >= currentID)
			{
				this.MoveToVisuallyClosestPoint(list);
			}
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x00572FE8 File Offset: 0x005711E8
		public void MoveToVisuallyClosestPoint(List<UILinkPoint> lostrefpoints)
		{
			Dictionary<int, UILinkPoint> points = UILinkPointNavigator.Points;
			Vector2 mouseScreen = Main.MouseScreen;
			UILinkPoint uilinkPoint = null;
			foreach (UILinkPoint uilinkPoint2 in lostrefpoints)
			{
				if (uilinkPoint == null || Vector2.Distance(mouseScreen, uilinkPoint.Position) > Vector2.Distance(mouseScreen, uilinkPoint2.Position))
				{
					uilinkPoint = uilinkPoint2;
				}
			}
			if (uilinkPoint != null)
			{
				UILinkPointNavigator.ChangePoint(uilinkPoint.ID);
			}
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x0057306C File Offset: 0x0057126C
		public List<SnapPoint> GetOrderedPointsByCategoryName(List<SnapPoint> pts, string name)
		{
			return (from x in pts
			where x.Name == name
			orderby x.Id
			select x).ToList<SnapPoint>();
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x005730C1 File Offset: 0x005712C1
		public void PairLeftRight(UILinkPoint leftSide, UILinkPoint rightSide)
		{
			if (leftSide == null || rightSide == null)
			{
				return;
			}
			leftSide.Right = rightSide.ID;
			rightSide.Left = leftSide.ID;
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x005730E2 File Offset: 0x005712E2
		public void PairUpDown(UILinkPoint upSide, UILinkPoint downSide)
		{
			if (upSide == null || downSide == null)
			{
				return;
			}
			upSide.Down = downSide.ID;
			downSide.Up = upSide.ID;
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x00571C31 File Offset: 0x0056FE31
		public UILinkPoint MakeLinkPointFromSnapPoint(int id, SnapPoint snap)
		{
			UILinkPointNavigator.SetPosition(id, snap.Position);
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[id];
			uilinkPoint.Unlink();
			return uilinkPoint;
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x00573104 File Offset: 0x00571304
		public UILinkPoint GetLinkPoint(int id, UIElement element)
		{
			SnapPoint snap;
			if (element.GetSnapPoint(out snap))
			{
				return this.MakeLinkPointFromSnapPoint(id, snap);
			}
			return null;
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x00573128 File Offset: 0x00571328
		public UILinkPoint TryMakeLinkPoint(ref int id, SnapPoint snap)
		{
			if (snap == null)
			{
				return null;
			}
			int num = id;
			id = num + 1;
			return this.MakeLinkPointFromSnapPoint(num, snap);
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x0057314C File Offset: 0x0057134C
		public UILinkPoint[] GetVerticalStripFromCategoryName(ref int currentID, List<SnapPoint> pts, string categoryName)
		{
			List<SnapPoint> orderedPointsByCategoryName = this.GetOrderedPointsByCategoryName(pts, categoryName);
			UILinkPoint[] result = null;
			if (orderedPointsByCategoryName.Count > 0)
			{
				result = this.CreateUILinkStripVertical(ref currentID, orderedPointsByCategoryName);
			}
			return result;
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x00573178 File Offset: 0x00571378
		public void MoveToVisuallyClosestPoint(int idRangeStartInclusive, int idRangeEndExclusive)
		{
			if (UILinkPointNavigator.CurrentPoint >= idRangeStartInclusive && UILinkPointNavigator.CurrentPoint < idRangeEndExclusive)
			{
				return;
			}
			Dictionary<int, UILinkPoint> points = UILinkPointNavigator.Points;
			Vector2 mouseScreen = Main.MouseScreen;
			UILinkPoint uilinkPoint = null;
			for (int i = idRangeStartInclusive; i < idRangeEndExclusive; i++)
			{
				UILinkPoint uilinkPoint2;
				if (!points.TryGetValue(i, out uilinkPoint2))
				{
					return;
				}
				if (uilinkPoint == null || Vector2.Distance(mouseScreen, uilinkPoint.Position) > Vector2.Distance(mouseScreen, uilinkPoint2.Position))
				{
					uilinkPoint = uilinkPoint2;
				}
			}
			if (uilinkPoint != null)
			{
				UILinkPointNavigator.ChangePoint(uilinkPoint.ID);
			}
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x005731EC File Offset: 0x005713EC
		public void CullPointsOutOfElementArea(SpriteBatch spriteBatch, List<SnapPoint> pointsAtMiddle, UIElement container)
		{
			float scaleFactor = 1f / Main.UIScale;
			Rectangle clippingRectangle = container.GetClippingRectangle(spriteBatch);
			Vector2 minimum = clippingRectangle.TopLeft() * scaleFactor;
			Vector2 maximum = clippingRectangle.BottomRight() * scaleFactor;
			for (int i = 0; i < pointsAtMiddle.Count; i++)
			{
				if (!pointsAtMiddle[i].Position.Between(minimum, maximum))
				{
					pointsAtMiddle.Remove(pointsAtMiddle[i]);
					i--;
				}
			}
		}
	}
}
