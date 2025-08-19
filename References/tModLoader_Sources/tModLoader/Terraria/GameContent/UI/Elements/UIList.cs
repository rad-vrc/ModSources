using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameInput;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	/// <summary>
	/// A scrollable list element. Typically paired with a <see cref="T:Terraria.GameContent.UI.Elements.UIScrollbar" />.
	/// <para /> To add elements to the list, use <see cref="M:Terraria.GameContent.UI.Elements.UIList.Add(Terraria.UI.UIElement)" /> rather than <see cref="M:Terraria.UI.UIElement.Append(Terraria.UI.UIElement)" />.
	/// <para /> If the ordering of list elements is inconsistent, either override <see cref="M:Terraria.UI.UIElement.CompareTo(System.Object)" /> on the elements of the list or assign a custom sort delegate to <see cref="F:Terraria.GameContent.UI.Elements.UIList.ManualSortMethod" />. If elements are added in order, you can use an empty sort method to not do any sorting to preserve the original order: <c>myList.ManualSortMethod = (e) =&gt; { };</c>
	/// </summary>
	// Token: 0x02000526 RID: 1318
	public class UIList : UIElement, IEnumerable<UIElement>, IEnumerable
	{
		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06003EF1 RID: 16113 RVA: 0x005D709F File Offset: 0x005D529F
		public int Count
		{
			get
			{
				return this._items.Count;
			}
		}

		// Token: 0x06003EF2 RID: 16114 RVA: 0x005D70AC File Offset: 0x005D52AC
		public UIList()
		{
			this._innerList.OverflowHidden = false;
			this._innerList.Width.Set(0f, 1f);
			this._innerList.Height.Set(0f, 1f);
			this.OverflowHidden = true;
			base.Append(this._innerList);
		}

		// Token: 0x06003EF3 RID: 16115 RVA: 0x005D7133 File Offset: 0x005D5333
		public float GetTotalHeight()
		{
			return this._innerListHeight;
		}

		// Token: 0x06003EF4 RID: 16116 RVA: 0x005D713C File Offset: 0x005D533C
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

		// Token: 0x06003EF5 RID: 16117 RVA: 0x005D7198 File Offset: 0x005D5398
		public void Goto(UIList.ElementSearchMethod searchMethod, bool center = false)
		{
			float innerDimensionHeight = base.GetInnerDimensions().Height;
			for (int i = 0; i < this._items.Count; i++)
			{
				UIElement item = this._items[i];
				if (searchMethod(item))
				{
					this._scrollbar.ViewPosition = item.Top.Pixels;
					if (center)
					{
						this._scrollbar.ViewPosition = item.Top.Pixels - innerDimensionHeight / 2f + item.GetOuterDimensions().Height / 2f;
					}
					return;
				}
			}
		}

		// Token: 0x06003EF6 RID: 16118 RVA: 0x005D7227 File Offset: 0x005D5427
		public virtual void Add(UIElement item)
		{
			this._items.Add(item);
			this._innerList.Append(item);
			this.UpdateOrder();
			this._innerList.Recalculate();
		}

		// Token: 0x06003EF7 RID: 16119 RVA: 0x005D7252 File Offset: 0x005D5452
		public virtual bool Remove(UIElement item)
		{
			this._innerList.RemoveChild(item);
			this.UpdateOrder();
			return this._items.Remove(item);
		}

		// Token: 0x06003EF8 RID: 16120 RVA: 0x005D7272 File Offset: 0x005D5472
		public virtual void Clear()
		{
			this._innerList.RemoveAllChildren();
			this._items.Clear();
		}

		// Token: 0x06003EF9 RID: 16121 RVA: 0x005D728A File Offset: 0x005D548A
		public override void Recalculate()
		{
			base.Recalculate();
			this.UpdateScrollbar();
		}

		// Token: 0x06003EFA RID: 16122 RVA: 0x005D7298 File Offset: 0x005D5498
		public override void ScrollWheel(UIScrollWheelEvent evt)
		{
			base.ScrollWheel(evt);
			if (this._scrollbar != null)
			{
				this._scrollbar.ViewPosition -= (float)evt.ScrollWheelValue;
			}
		}

		// Token: 0x06003EFB RID: 16123 RVA: 0x005D72C4 File Offset: 0x005D54C4
		public override void RecalculateChildren()
		{
			base.RecalculateChildren();
			float num = 0f;
			for (int i = 0; i < this._items.Count; i++)
			{
				float num2 = (this._items.Count == 1) ? 0f : this.ListPadding;
				this._items[i].Top.Set(num, 0f);
				this._items[i].Recalculate();
				num += this._items[i].GetOuterDimensions().Height + num2;
			}
			this._innerListHeight = num;
		}

		// Token: 0x06003EFC RID: 16124 RVA: 0x005D7360 File Offset: 0x005D5560
		private void UpdateScrollbar()
		{
			if (this._scrollbar != null)
			{
				float height = base.GetInnerDimensions().Height;
				this._scrollbar.SetView(height, this._innerListHeight);
			}
		}

		// Token: 0x06003EFD RID: 16125 RVA: 0x005D7393 File Offset: 0x005D5593
		public void SetScrollbar(UIScrollbar scrollbar)
		{
			this._scrollbar = scrollbar;
			this.UpdateScrollbar();
		}

		// Token: 0x06003EFE RID: 16126 RVA: 0x005D73A2 File Offset: 0x005D55A2
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

		// Token: 0x06003EFF RID: 16127 RVA: 0x005D73DC File Offset: 0x005D55DC
		public int SortMethod(UIElement item1, UIElement item2)
		{
			return item1.CompareTo(item2);
		}

		// Token: 0x06003F00 RID: 16128 RVA: 0x005D73E8 File Offset: 0x005D55E8
		public override List<SnapPoint> GetSnapPoints()
		{
			List<SnapPoint> list = new List<SnapPoint>();
			SnapPoint point;
			if (base.GetSnapPoint(out point))
			{
				list.Add(point);
			}
			foreach (UIElement item in this._items)
			{
				list.AddRange(item.GetSnapPoints());
			}
			return list;
		}

		// Token: 0x06003F01 RID: 16129 RVA: 0x005D7458 File Offset: 0x005D5658
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (this._scrollbar != null)
			{
				this._innerList.Top.Set(0f - this._scrollbar.GetValue(), 0f);
			}
			this.Recalculate();
		}

		// Token: 0x06003F02 RID: 16130 RVA: 0x005D748E File Offset: 0x005D568E
		public IEnumerator<UIElement> GetEnumerator()
		{
			return ((IEnumerable<UIElement>)this._items).GetEnumerator();
		}

		// Token: 0x06003F03 RID: 16131 RVA: 0x005D749B File Offset: 0x005D569B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<UIElement>)this._items).GetEnumerator();
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06003F04 RID: 16132 RVA: 0x005D74A8 File Offset: 0x005D56A8
		// (set) Token: 0x06003F05 RID: 16133 RVA: 0x005D74B5 File Offset: 0x005D56B5
		public float ViewPosition
		{
			get
			{
				return this._scrollbar.ViewPosition;
			}
			set
			{
				this._scrollbar.ViewPosition = value;
			}
		}

		// Token: 0x06003F06 RID: 16134 RVA: 0x005D74C4 File Offset: 0x005D56C4
		public virtual void AddRange(IEnumerable<UIElement> items)
		{
			foreach (UIElement item in items)
			{
				this._items.Add(item);
				this._innerList.Append(item);
			}
			this.UpdateOrder();
			this._innerList.Recalculate();
		}

		// Token: 0x06003F07 RID: 16135 RVA: 0x005D7530 File Offset: 0x005D5730
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (base.IsMouseHovering)
			{
				PlayerInput.LockVanillaMouseScroll("ModLoader/UIList");
			}
		}

		// Token: 0x0400577D RID: 22397
		public List<UIElement> _items = new List<UIElement>();

		// Token: 0x0400577E RID: 22398
		protected UIScrollbar _scrollbar;

		// Token: 0x0400577F RID: 22399
		internal UIElement _innerList = new UIList.UIInnerList();

		// Token: 0x04005780 RID: 22400
		private float _innerListHeight;

		// Token: 0x04005781 RID: 22401
		public float ListPadding = 5f;

		// Token: 0x04005782 RID: 22402
		public Action<List<UIElement>> ManualSortMethod;

		// Token: 0x02000C1D RID: 3101
		// (Invoke) Token: 0x06005F0F RID: 24335
		public delegate bool ElementSearchMethod(UIElement element);

		// Token: 0x02000C1E RID: 3102
		private class UIInnerList : UIElement
		{
			// Token: 0x06005F12 RID: 24338 RVA: 0x006CD15F File Offset: 0x006CB35F
			public override bool ContainsPoint(Vector2 point)
			{
				return true;
			}

			// Token: 0x06005F13 RID: 24339 RVA: 0x006CD164 File Offset: 0x006CB364
			protected override void DrawChildren(SpriteBatch spriteBatch)
			{
				Vector2 position = base.Parent.GetDimensions().Position();
				Vector2 dimensions;
				dimensions..ctor(base.Parent.GetDimensions().Width, base.Parent.GetDimensions().Height);
				foreach (UIElement element in this.Elements)
				{
					Vector2 position2 = element.GetDimensions().Position();
					Vector2 dimensions2;
					dimensions2..ctor(element.GetDimensions().Width, element.GetDimensions().Height);
					if (Collision.CheckAABBvAABBCollision(position, dimensions, position2, dimensions2))
					{
						element.Draw(spriteBatch);
					}
				}
			}

			// Token: 0x06005F14 RID: 24340 RVA: 0x006CD234 File Offset: 0x006CB434
			public override Rectangle GetViewCullingArea()
			{
				return base.Parent.GetDimensions().ToRectangle();
			}
		}
	}
}
