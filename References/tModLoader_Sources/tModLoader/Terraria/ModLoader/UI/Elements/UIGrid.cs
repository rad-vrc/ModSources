using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.UI;

namespace Terraria.ModLoader.UI.Elements
{
	/// <summary>
	/// Similar to <see cref="T:Terraria.GameContent.UI.Elements.UIList" /> except the elements are arranged in a grid in normal reading order.
	/// <para /> <b>UIList docs:</b>
	/// <inheritdoc cref="T:Terraria.GameContent.UI.Elements.UIList" />
	/// </summary>
	// Token: 0x02000276 RID: 630
	public class UIGrid : UIElement
	{
		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06002B58 RID: 11096 RVA: 0x0052204E File Offset: 0x0052024E
		public int Count
		{
			get
			{
				return this._items.Count;
			}
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x0052205C File Offset: 0x0052025C
		public UIGrid()
		{
			this._innerList.OverflowHidden = false;
			this._innerList.Width.Set(0f, 1f);
			this._innerList.Height.Set(0f, 1f);
			this.OverflowHidden = true;
			base.Append(this._innerList);
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x005220E3 File Offset: 0x005202E3
		public float GetTotalHeight()
		{
			return this._innerListHeight;
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x005220EC File Offset: 0x005202EC
		public void Goto(UIGrid.ElementSearchMethod searchMethod, bool center = false)
		{
			for (int i = 0; i < this._items.Count; i++)
			{
				if (searchMethod(this._items[i]))
				{
					this._scrollbar.ViewPosition = this._items[i].Top.Pixels;
					if (center)
					{
						this._scrollbar.ViewPosition = this._items[i].Top.Pixels - base.GetInnerDimensions().Height / 2f + this._items[i].GetOuterDimensions().Height / 2f;
					}
					return;
				}
			}
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x0052219E File Offset: 0x0052039E
		public virtual void Add(UIElement item)
		{
			this._items.Add(item);
			this._innerList.Append(item);
			this.UpdateOrder();
			this._innerList.Recalculate();
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x005221CC File Offset: 0x005203CC
		public virtual void AddRange(IEnumerable<UIElement> items)
		{
			this._items.AddRange(items);
			foreach (UIElement item in items)
			{
				this._innerList.Append(item);
			}
			this.UpdateOrder();
			this._innerList.Recalculate();
		}

		// Token: 0x06002B5E RID: 11102 RVA: 0x00522238 File Offset: 0x00520438
		public virtual bool Remove(UIElement item)
		{
			this._innerList.RemoveChild(item);
			this.UpdateOrder();
			return this._items.Remove(item);
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x00522258 File Offset: 0x00520458
		public virtual void Clear()
		{
			this._innerList.RemoveAllChildren();
			this._items.Clear();
		}

		// Token: 0x06002B60 RID: 11104 RVA: 0x00522270 File Offset: 0x00520470
		public override void Recalculate()
		{
			base.Recalculate();
			this.UpdateScrollbar();
		}

		// Token: 0x06002B61 RID: 11105 RVA: 0x0052227E File Offset: 0x0052047E
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (base.IsMouseHovering)
			{
				PlayerInput.LockVanillaMouseScroll("ModLoader/UIList");
			}
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x00522299 File Offset: 0x00520499
		public override void ScrollWheel(UIScrollWheelEvent evt)
		{
			base.ScrollWheel(evt);
			if (this._scrollbar != null)
			{
				this._scrollbar.ViewPosition -= (float)evt.ScrollWheelValue;
			}
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x005222C4 File Offset: 0x005204C4
		public override void RecalculateChildren()
		{
			float availableWidth = base.GetInnerDimensions().Width;
			base.RecalculateChildren();
			float top = 0f;
			float left = 0f;
			float maxRowHeight = 0f;
			for (int i = 0; i < this._items.Count; i++)
			{
				UIElement uielement = this._items[i];
				CalculatedStyle outerDimensions = uielement.GetOuterDimensions();
				if (left + outerDimensions.Width > availableWidth && left > 0f)
				{
					top += maxRowHeight + this.ListPadding;
					left = 0f;
					maxRowHeight = 0f;
				}
				maxRowHeight = Math.Max(maxRowHeight, outerDimensions.Height);
				uielement.Left.Set(left, 0f);
				left += outerDimensions.Width + this.ListPadding;
				uielement.Top.Set(top, 0f);
			}
			this._innerListHeight = top + maxRowHeight;
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x0052239E File Offset: 0x0052059E
		private void UpdateScrollbar()
		{
			if (this._scrollbar == null)
			{
				return;
			}
			this._scrollbar.SetView(base.GetInnerDimensions().Height, this._innerListHeight);
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x005223C5 File Offset: 0x005205C5
		public void SetScrollbar(UIScrollbar scrollbar)
		{
			this._scrollbar = scrollbar;
			this.UpdateScrollbar();
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x005223D4 File Offset: 0x005205D4
		public void UpdateOrder()
		{
			this._items.Sort(new Comparison<UIElement>(this.SortMethod));
			this.UpdateScrollbar();
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x005223F3 File Offset: 0x005205F3
		public int SortMethod(UIElement item1, UIElement item2)
		{
			return item1.CompareTo(item2);
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x005223FC File Offset: 0x005205FC
		public override List<SnapPoint> GetSnapPoints()
		{
			List<SnapPoint> list = new List<SnapPoint>();
			SnapPoint item;
			if (base.GetSnapPoint(out item))
			{
				list.Add(item);
			}
			foreach (UIElement current in this._items)
			{
				list.AddRange(current.GetSnapPoints());
			}
			return list;
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x0052246C File Offset: 0x0052066C
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (this._scrollbar != null)
			{
				this._innerList.Top.Set(-this._scrollbar.GetValue(), 0f);
			}
		}

		// Token: 0x04001BD0 RID: 7120
		public List<UIElement> _items = new List<UIElement>();

		// Token: 0x04001BD1 RID: 7121
		protected UIScrollbar _scrollbar;

		// Token: 0x04001BD2 RID: 7122
		internal UIElement _innerList = new UIGrid.UIInnerList();

		// Token: 0x04001BD3 RID: 7123
		private float _innerListHeight;

		// Token: 0x04001BD4 RID: 7124
		public float ListPadding = 5f;

		// Token: 0x02000A2E RID: 2606
		// (Invoke) Token: 0x060057E6 RID: 22502
		public delegate bool ElementSearchMethod(UIElement element);

		// Token: 0x02000A2F RID: 2607
		private class UIInnerList : UIElement
		{
			// Token: 0x060057E9 RID: 22505 RVA: 0x0069EE2B File Offset: 0x0069D02B
			public override bool ContainsPoint(Vector2 point)
			{
				return true;
			}

			// Token: 0x060057EA RID: 22506 RVA: 0x0069EE30 File Offset: 0x0069D030
			protected override void DrawChildren(SpriteBatch spriteBatch)
			{
				Vector2 position = base.Parent.GetDimensions().Position();
				Vector2 dimensions;
				dimensions..ctor(base.Parent.GetDimensions().Width, base.Parent.GetDimensions().Height);
				foreach (UIElement current in this.Elements)
				{
					Vector2 position2 = current.GetDimensions().Position();
					Vector2 dimensions2;
					dimensions2..ctor(current.GetDimensions().Width, current.GetDimensions().Height);
					if (Collision.CheckAABBvAABBCollision(position, dimensions, position2, dimensions2))
					{
						current.Draw(spriteBatch);
					}
				}
			}
		}
	}
}
