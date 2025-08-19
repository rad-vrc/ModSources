using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200038E RID: 910
	public class UIList : UIElement, IEnumerable<UIElement>, IEnumerable
	{
		// Token: 0x17000318 RID: 792
		// (get) Token: 0x0600290C RID: 10508 RVA: 0x00590A81 File Offset: 0x0058EC81
		public int Count
		{
			get
			{
				return this._items.Count;
			}
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x00590A90 File Offset: 0x0058EC90
		public UIList()
		{
			this._innerList.OverflowHidden = false;
			this._innerList.Width.Set(0f, 1f);
			this._innerList.Height.Set(0f, 1f);
			this.OverflowHidden = true;
			base.Append(this._innerList);
		}

		// Token: 0x0600290E RID: 10510 RVA: 0x00590B17 File Offset: 0x0058ED17
		public float GetTotalHeight()
		{
			return this._innerListHeight;
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x00590B20 File Offset: 0x0058ED20
		public void Goto(UIList.ElementSearchMethod searchMethod)
		{
			for (int i = 0; i < this._items.Count; i++)
			{
				if (searchMethod(this._items[i]))
				{
					this._scrollbar.ViewPosition = this._items[i].Top.Pixels;
					return;
				}
			}
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x00590B79 File Offset: 0x0058ED79
		public virtual void Add(UIElement item)
		{
			this._items.Add(item);
			this._innerList.Append(item);
			this.UpdateOrder();
			this._innerList.Recalculate();
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x00590BA4 File Offset: 0x0058EDA4
		public virtual bool Remove(UIElement item)
		{
			this._innerList.RemoveChild(item);
			this.UpdateOrder();
			return this._items.Remove(item);
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x00590BC4 File Offset: 0x0058EDC4
		public virtual void Clear()
		{
			this._innerList.RemoveAllChildren();
			this._items.Clear();
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x00590BDC File Offset: 0x0058EDDC
		public override void Recalculate()
		{
			base.Recalculate();
			this.UpdateScrollbar();
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x00590BEA File Offset: 0x0058EDEA
		public override void ScrollWheel(UIScrollWheelEvent evt)
		{
			base.ScrollWheel(evt);
			if (this._scrollbar != null)
			{
				this._scrollbar.ViewPosition -= (float)evt.ScrollWheelValue;
			}
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x00590C14 File Offset: 0x0058EE14
		public override void RecalculateChildren()
		{
			base.RecalculateChildren();
			float num = 0f;
			for (int i = 0; i < this._items.Count; i++)
			{
				float num2 = (this._items.Count == 1) ? 0f : this.ListPadding;
				this._items[i].Top.Set(num, 0f);
				this._items[i].Recalculate();
				CalculatedStyle outerDimensions = this._items[i].GetOuterDimensions();
				num += outerDimensions.Height + num2;
			}
			this._innerListHeight = num;
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x00590CB0 File Offset: 0x0058EEB0
		private void UpdateScrollbar()
		{
			if (this._scrollbar == null)
			{
				return;
			}
			float height = base.GetInnerDimensions().Height;
			this._scrollbar.SetView(height, this._innerListHeight);
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x00590CE4 File Offset: 0x0058EEE4
		public void SetScrollbar(UIScrollbar scrollbar)
		{
			this._scrollbar = scrollbar;
			this.UpdateScrollbar();
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x00590CF3 File Offset: 0x0058EEF3
		public void UpdateOrder()
		{
			if (this.ManualSortMethod != null)
			{
				this.ManualSortMethod(this._items);
			}
			else
			{
				this._items.Sort(new Comparison<UIElement>(this.SortMethod));
			}
			this.UpdateScrollbar();
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x00590D2D File Offset: 0x0058EF2D
		public int SortMethod(UIElement item1, UIElement item2)
		{
			return item1.CompareTo(item2);
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x00590D38 File Offset: 0x0058EF38
		public override List<SnapPoint> GetSnapPoints()
		{
			List<SnapPoint> list = new List<SnapPoint>();
			SnapPoint item;
			if (base.GetSnapPoint(out item))
			{
				list.Add(item);
			}
			foreach (UIElement uielement in this._items)
			{
				list.AddRange(uielement.GetSnapPoints());
			}
			return list;
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x00590DA8 File Offset: 0x0058EFA8
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (this._scrollbar != null)
			{
				this._innerList.Top.Set(-this._scrollbar.GetValue(), 0f);
			}
			this.Recalculate();
		}

		// Token: 0x0600291C RID: 10524 RVA: 0x00590DD9 File Offset: 0x0058EFD9
		public IEnumerator<UIElement> GetEnumerator()
		{
			return ((IEnumerable<UIElement>)this._items).GetEnumerator();
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x00590DD9 File Offset: 0x0058EFD9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<UIElement>)this._items).GetEnumerator();
		}

		// Token: 0x04004C47 RID: 19527
		protected List<UIElement> _items = new List<UIElement>();

		// Token: 0x04004C48 RID: 19528
		protected UIScrollbar _scrollbar;

		// Token: 0x04004C49 RID: 19529
		private UIElement _innerList = new UIList.UIInnerList();

		// Token: 0x04004C4A RID: 19530
		private float _innerListHeight;

		// Token: 0x04004C4B RID: 19531
		public float ListPadding = 5f;

		// Token: 0x04004C4C RID: 19532
		public Action<List<UIElement>> ManualSortMethod;

		// Token: 0x02000754 RID: 1876
		// (Invoke) Token: 0x060038B0 RID: 14512
		public delegate bool ElementSearchMethod(UIElement element);

		// Token: 0x02000755 RID: 1877
		private class UIInnerList : UIElement
		{
			// Token: 0x060038B3 RID: 14515 RVA: 0x0003266D File Offset: 0x0003086D
			public override bool ContainsPoint(Vector2 point)
			{
				return true;
			}

			// Token: 0x060038B4 RID: 14516 RVA: 0x00614010 File Offset: 0x00612210
			protected override void DrawChildren(SpriteBatch spriteBatch)
			{
				Vector2 position = base.Parent.GetDimensions().Position();
				Vector2 dimensions = new Vector2(base.Parent.GetDimensions().Width, base.Parent.GetDimensions().Height);
				foreach (UIElement uielement in this.Elements)
				{
					Vector2 position2 = uielement.GetDimensions().Position();
					Vector2 dimensions2 = new Vector2(uielement.GetDimensions().Width, uielement.GetDimensions().Height);
					if (Collision.CheckAABBvAABBCollision(position, dimensions, position2, dimensions2))
					{
						uielement.Draw(spriteBatch);
					}
				}
			}

			// Token: 0x060038B5 RID: 14517 RVA: 0x006140E0 File Offset: 0x006122E0
			public override Rectangle GetViewCullingArea()
			{
				return base.Parent.GetDimensions().ToRectangle();
			}
		}
	}
}
