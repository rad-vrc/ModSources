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
	// Token: 0x020004DB RID: 1243
	public struct UIGamepadHelper
	{
		// Token: 0x06003BD6 RID: 15318 RVA: 0x005B8E9C File Offset: 0x005B709C
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
					UILinkPoint uILinkPoint = array[j, k];
					if (uILinkPoint != null)
					{
						if (j < array.GetLength(0) - 1)
						{
							UILinkPoint uILinkPoint2 = array[j + 1, k];
							if (uILinkPoint2 != null)
							{
								this.PairLeftRight(uILinkPoint, uILinkPoint2);
							}
						}
						if (k < array.GetLength(1) - 1)
						{
							UILinkPoint uILinkPoint3 = array[j, k + 1];
							if (uILinkPoint3 != null)
							{
								this.PairUpDown(uILinkPoint, uILinkPoint3);
							}
						}
						if (leftLinkPoint != null && j == 0)
						{
							uILinkPoint.Left = leftLinkPoint.ID;
						}
						if (topLinkPoint != null && k == 0)
						{
							uILinkPoint.Up = topLinkPoint.ID;
						}
						if (rightLinkPoint != null && j == pointsPerLine - 1)
						{
							uILinkPoint.Right = rightLinkPoint.ID;
						}
						if (bottomLinkPoint != null && k == num - 1)
						{
							uILinkPoint.Down = bottomLinkPoint.ID;
						}
					}
				}
			}
			return array;
		}

		// Token: 0x06003BD7 RID: 15319 RVA: 0x005B8FFC File Offset: 0x005B71FC
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

		// Token: 0x06003BD8 RID: 15320 RVA: 0x005B90A4 File Offset: 0x005B72A4
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

		// Token: 0x06003BD9 RID: 15321 RVA: 0x005B9104 File Offset: 0x005B7304
		public void RemovePointsOutOfView(List<SnapPoint> pts, UIElement containerPanel, SpriteBatch spriteBatch)
		{
			float num = 1f / Main.UIScale;
			Rectangle clippingRectangle = containerPanel.GetClippingRectangle(spriteBatch);
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

		// Token: 0x06003BDA RID: 15322 RVA: 0x005B9178 File Offset: 0x005B7378
		public void LinkHorizontalStripBottomSideToSingle(UILinkPoint[] strip, UILinkPoint theSingle)
		{
			if (strip != null && theSingle != null)
			{
				for (int num = strip.Length - 1; num >= 0; num--)
				{
					this.PairUpDown(strip[num], theSingle);
				}
			}
		}

		// Token: 0x06003BDB RID: 15323 RVA: 0x005B91A8 File Offset: 0x005B73A8
		public void LinkHorizontalStripUpSideToSingle(UILinkPoint[] strip, UILinkPoint theSingle)
		{
			if (strip != null && theSingle != null)
			{
				for (int num = strip.Length - 1; num >= 0; num--)
				{
					this.PairUpDown(theSingle, strip[num]);
				}
			}
		}

		// Token: 0x06003BDC RID: 15324 RVA: 0x005B91D5 File Offset: 0x005B73D5
		public void LinkVerticalStripBottomSideToSingle(UILinkPoint[] strip, UILinkPoint theSingle)
		{
			if (strip != null && theSingle != null)
			{
				this.PairUpDown(strip[strip.Length - 1], theSingle);
			}
		}

		// Token: 0x06003BDD RID: 15325 RVA: 0x005B91EC File Offset: 0x005B73EC
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

		// Token: 0x06003BDE RID: 15326 RVA: 0x005B9254 File Offset: 0x005B7454
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

		// Token: 0x06003BDF RID: 15327 RVA: 0x005B92BC File Offset: 0x005B74BC
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

		// Token: 0x06003BE0 RID: 15328 RVA: 0x005B9304 File Offset: 0x005B7504
		public void MoveToVisuallyClosestPoint(List<UILinkPoint> lostrefpoints)
		{
			Dictionary<int, UILinkPoint> points = UILinkPointNavigator.Points;
			Vector2 mouseScreen = Main.MouseScreen;
			UILinkPoint uILinkPoint = null;
			foreach (UILinkPoint lostrefpoint in lostrefpoints)
			{
				if (uILinkPoint == null || Vector2.Distance(mouseScreen, uILinkPoint.Position) > Vector2.Distance(mouseScreen, lostrefpoint.Position))
				{
					uILinkPoint = lostrefpoint;
				}
			}
			if (uILinkPoint != null)
			{
				UILinkPointNavigator.ChangePoint(uILinkPoint.ID);
			}
		}

		// Token: 0x06003BE1 RID: 15329 RVA: 0x005B9388 File Offset: 0x005B7588
		public List<SnapPoint> GetOrderedPointsByCategoryName(List<SnapPoint> pts, string name)
		{
			return (from x in pts
			where x.Name == name
			orderby x.Id
			select x).ToList<SnapPoint>();
		}

		// Token: 0x06003BE2 RID: 15330 RVA: 0x005B93DD File Offset: 0x005B75DD
		public void PairLeftRight(UILinkPoint leftSide, UILinkPoint rightSide)
		{
			if (leftSide != null && rightSide != null)
			{
				leftSide.Right = rightSide.ID;
				rightSide.Left = leftSide.ID;
			}
		}

		// Token: 0x06003BE3 RID: 15331 RVA: 0x005B93FD File Offset: 0x005B75FD
		public void PairUpDown(UILinkPoint upSide, UILinkPoint downSide)
		{
			if (upSide != null && downSide != null)
			{
				upSide.Down = downSide.ID;
				downSide.Up = upSide.ID;
			}
		}

		// Token: 0x06003BE4 RID: 15332 RVA: 0x005B941D File Offset: 0x005B761D
		public UILinkPoint MakeLinkPointFromSnapPoint(int id, SnapPoint snap)
		{
			UILinkPointNavigator.SetPosition(id, snap.Position);
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[id];
			uilinkPoint.Unlink();
			return uilinkPoint;
		}

		// Token: 0x06003BE5 RID: 15333 RVA: 0x005B943C File Offset: 0x005B763C
		public UILinkPoint GetLinkPoint(int id, UIElement element)
		{
			SnapPoint point;
			if (element.GetSnapPoint(out point))
			{
				return this.MakeLinkPointFromSnapPoint(id, point);
			}
			return null;
		}

		// Token: 0x06003BE6 RID: 15334 RVA: 0x005B9460 File Offset: 0x005B7660
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

		// Token: 0x06003BE7 RID: 15335 RVA: 0x005B9484 File Offset: 0x005B7684
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

		// Token: 0x06003BE8 RID: 15336 RVA: 0x005B94B0 File Offset: 0x005B76B0
		public void MoveToVisuallyClosestPoint(int idRangeStartInclusive, int idRangeEndExclusive)
		{
			if (UILinkPointNavigator.CurrentPoint >= idRangeStartInclusive && UILinkPointNavigator.CurrentPoint < idRangeEndExclusive)
			{
				return;
			}
			Dictionary<int, UILinkPoint> points = UILinkPointNavigator.Points;
			Vector2 mouseScreen = Main.MouseScreen;
			UILinkPoint uILinkPoint = null;
			for (int i = idRangeStartInclusive; i < idRangeEndExclusive; i++)
			{
				UILinkPoint value;
				if (!points.TryGetValue(i, out value))
				{
					return;
				}
				if (uILinkPoint == null || Vector2.Distance(mouseScreen, uILinkPoint.Position) > Vector2.Distance(mouseScreen, value.Position))
				{
					uILinkPoint = value;
				}
			}
			if (uILinkPoint != null)
			{
				UILinkPointNavigator.ChangePoint(uILinkPoint.ID);
			}
		}

		// Token: 0x06003BE9 RID: 15337 RVA: 0x005B9524 File Offset: 0x005B7724
		public void CullPointsOutOfElementArea(SpriteBatch spriteBatch, List<SnapPoint> pointsAtMiddle, UIElement container)
		{
			float num = 1f / Main.UIScale;
			Rectangle clippingRectangle = container.GetClippingRectangle(spriteBatch);
			Vector2 minimum = clippingRectangle.TopLeft() * num;
			Vector2 maximum = clippingRectangle.BottomRight() * num;
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
