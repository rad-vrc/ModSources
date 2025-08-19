using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.GameContent.UI.Elements;

namespace Terraria.UI
{
	/// <summary>
	/// A UI element, the building blocks of a user interface. Commonly used elements include <see cref="T:Terraria.GameContent.UI.Elements.UIPanel" />, <see cref="T:Terraria.GameContent.UI.Elements.UIImage" />, <see cref="T:Terraria.GameContent.UI.Elements.UIList" />, <see cref="T:Terraria.GameContent.UI.Elements.UIScrollbar" />, <see cref="T:Terraria.GameContent.UI.Elements.UITextPanel`1" />, and <see cref="T:Terraria.GameContent.UI.Elements.UIText" />. 
	/// <para /> UIElements are nested within each other using <see cref="M:Terraria.UI.UIElement.Append(Terraria.UI.UIElement)" /> to build a layout. 
	/// </summary>
	// Token: 0x020000B4 RID: 180
	public class UIElement : IComparable
	{
		/// <summary>
		/// The element this element is appended to.
		/// </summary>
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060015A5 RID: 5541 RVA: 0x004B06C7 File Offset: 0x004AE8C7
		// (set) Token: 0x060015A6 RID: 5542 RVA: 0x004B06CF File Offset: 0x004AE8CF
		public UIElement Parent { get; private set; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x004B06D8 File Offset: 0x004AE8D8
		// (set) Token: 0x060015A8 RID: 5544 RVA: 0x004B06E0 File Offset: 0x004AE8E0
		public int UniqueId { get; private set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x004B06E9 File Offset: 0x004AE8E9
		public IEnumerable<UIElement> Children
		{
			get
			{
				return this.Elements;
			}
		}

		/// <summary>
		/// True when the mouse is hovering over this UIElement. Useful for setting hover tooltips in DrawSelf or drawing hover visual indicators.
		/// <para /> Note that this is true for all elements in the hierarchy that are hovered, not just the topmost element. For example, hovering over a button that is appended to a panel will mean that IsMouseHovering is true for the button and the panel. Use this or <c>ContainsPoint(Main.MouseScreen)</c> for behaviors that use this logic, such as setting <see cref="F:Terraria.Player.mouseInterface" /> to true.
		/// </summary>
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x004B06F1 File Offset: 0x004AE8F1
		// (set) Token: 0x060015AB RID: 5547 RVA: 0x004B06F9 File Offset: 0x004AE8F9
		public bool IsMouseHovering { get; private set; }

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060015AC RID: 5548 RVA: 0x004B0704 File Offset: 0x004AE904
		// (remove) Token: 0x060015AD RID: 5549 RVA: 0x004B073C File Offset: 0x004AE93C
		public event UIElement.MouseEvent OnLeftMouseDown;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060015AE RID: 5550 RVA: 0x004B0774 File Offset: 0x004AE974
		// (remove) Token: 0x060015AF RID: 5551 RVA: 0x004B07AC File Offset: 0x004AE9AC
		public event UIElement.MouseEvent OnLeftMouseUp;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060015B0 RID: 5552 RVA: 0x004B07E4 File Offset: 0x004AE9E4
		// (remove) Token: 0x060015B1 RID: 5553 RVA: 0x004B081C File Offset: 0x004AEA1C
		public event UIElement.MouseEvent OnLeftClick;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060015B2 RID: 5554 RVA: 0x004B0854 File Offset: 0x004AEA54
		// (remove) Token: 0x060015B3 RID: 5555 RVA: 0x004B088C File Offset: 0x004AEA8C
		public event UIElement.MouseEvent OnLeftDoubleClick;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060015B4 RID: 5556 RVA: 0x004B08C4 File Offset: 0x004AEAC4
		// (remove) Token: 0x060015B5 RID: 5557 RVA: 0x004B08FC File Offset: 0x004AEAFC
		public event UIElement.MouseEvent OnRightMouseDown;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060015B6 RID: 5558 RVA: 0x004B0934 File Offset: 0x004AEB34
		// (remove) Token: 0x060015B7 RID: 5559 RVA: 0x004B096C File Offset: 0x004AEB6C
		public event UIElement.MouseEvent OnRightMouseUp;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060015B8 RID: 5560 RVA: 0x004B09A4 File Offset: 0x004AEBA4
		// (remove) Token: 0x060015B9 RID: 5561 RVA: 0x004B09DC File Offset: 0x004AEBDC
		public event UIElement.MouseEvent OnRightClick;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060015BA RID: 5562 RVA: 0x004B0A14 File Offset: 0x004AEC14
		// (remove) Token: 0x060015BB RID: 5563 RVA: 0x004B0A4C File Offset: 0x004AEC4C
		public event UIElement.MouseEvent OnRightDoubleClick;

		/// <summary> Called by <see cref="M:Terraria.UI.UIElement.MouseOver(Terraria.UI.UIMouseEvent)" />. Use this event instead of inheritance if suitable. </summary>
		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060015BC RID: 5564 RVA: 0x004B0A84 File Offset: 0x004AEC84
		// (remove) Token: 0x060015BD RID: 5565 RVA: 0x004B0ABC File Offset: 0x004AECBC
		public event UIElement.MouseEvent OnMouseOver;

		/// <summary> Called by <see cref="M:Terraria.UI.UIElement.MouseOut(Terraria.UI.UIMouseEvent)" />. Use this event instead of inheritance if suitable. </summary>
		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060015BE RID: 5566 RVA: 0x004B0AF4 File Offset: 0x004AECF4
		// (remove) Token: 0x060015BF RID: 5567 RVA: 0x004B0B2C File Offset: 0x004AED2C
		public event UIElement.MouseEvent OnMouseOut;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060015C0 RID: 5568 RVA: 0x004B0B64 File Offset: 0x004AED64
		// (remove) Token: 0x060015C1 RID: 5569 RVA: 0x004B0B9C File Offset: 0x004AED9C
		public event UIElement.ScrollWheelEvent OnScrollWheel;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060015C2 RID: 5570 RVA: 0x004B0BD4 File Offset: 0x004AEDD4
		// (remove) Token: 0x060015C3 RID: 5571 RVA: 0x004B0C0C File Offset: 0x004AEE0C
		public event UIElement.ElementEvent OnUpdate;

		// Token: 0x060015C4 RID: 5572 RVA: 0x004B0C44 File Offset: 0x004AEE44
		public UIElement()
		{
			this.UniqueId = UIElement._idCounter++;
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x004B0CA4 File Offset: 0x004AEEA4
		public void SetSnapPoint(string name, int id, Vector2? anchor = null, Vector2? offset = null)
		{
			if (anchor == null)
			{
				anchor = new Vector2?(new Vector2(0.5f));
			}
			if (offset == null)
			{
				offset = new Vector2?(Vector2.Zero);
			}
			this._snapPoint = new SnapPoint(name, id, anchor.Value, offset.Value);
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x004B0CFB File Offset: 0x004AEEFB
		public bool GetSnapPoint(out SnapPoint point)
		{
			point = this._snapPoint;
			if (this._snapPoint != null)
			{
				this._snapPoint.Calculate(this);
			}
			return this._snapPoint != null;
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x004B0D24 File Offset: 0x004AEF24
		public virtual void ExecuteRecursively(UIElement.UIElementAction action)
		{
			action(this);
			foreach (UIElement uielement in this.Elements)
			{
				uielement.ExecuteRecursively(action);
			}
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x004B0D7C File Offset: 0x004AEF7C
		protected virtual void DrawSelf(SpriteBatch spriteBatch)
		{
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x004B0D80 File Offset: 0x004AEF80
		protected virtual void DrawChildren(SpriteBatch spriteBatch)
		{
			foreach (UIElement uielement in this.Elements)
			{
				uielement.Draw(spriteBatch);
			}
		}

		/// <summary>
		/// Adds the element as a child of this element. The UI system expects children elements to be positioned within the bounds of the parent element and will not work correctly if this restriction is not followed.
		/// </summary>
		// Token: 0x060015CA RID: 5578 RVA: 0x004B0DD4 File Offset: 0x004AEFD4
		public void Append(UIElement element)
		{
			element.Remove();
			element.Parent = this;
			this.Elements.Add(element);
			element.Recalculate();
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x004B0DF5 File Offset: 0x004AEFF5
		public void Remove()
		{
			if (this.Parent != null)
			{
				this.Parent.RemoveChild(this);
			}
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x004B0E0B File Offset: 0x004AF00B
		public void RemoveChild(UIElement child)
		{
			this.Elements.Remove(child);
			child.Parent = null;
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x004B0E24 File Offset: 0x004AF024
		public void RemoveAllChildren()
		{
			foreach (UIElement uielement in this.Elements)
			{
				uielement.Parent = null;
			}
			this.Elements.Clear();
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x004B0E80 File Offset: 0x004AF080
		public virtual void Draw(SpriteBatch spriteBatch)
		{
			bool overflowHidden = this.OverflowHidden;
			bool useImmediateMode = this.UseImmediateMode;
			RasterizerState rasterizerState = spriteBatch.GraphicsDevice.RasterizerState;
			Rectangle scissorRectangle = spriteBatch.GraphicsDevice.ScissorRectangle;
			SamplerState anisotropicClamp = SamplerState.AnisotropicClamp;
			if (useImmediateMode || this.OverrideSamplerState != null)
			{
				spriteBatch.End();
				spriteBatch.Begin(useImmediateMode ? 1 : 0, BlendState.AlphaBlend, (this.OverrideSamplerState != null) ? this.OverrideSamplerState : anisotropicClamp, DepthStencilState.None, UIElement.OverflowHiddenRasterizerState, null, Main.UIScaleMatrix);
				this.DrawSelf(spriteBatch);
				spriteBatch.End();
				spriteBatch.Begin(0, BlendState.AlphaBlend, anisotropicClamp, DepthStencilState.None, UIElement.OverflowHiddenRasterizerState, null, Main.UIScaleMatrix);
			}
			else
			{
				this.DrawSelf(spriteBatch);
			}
			if (overflowHidden)
			{
				spriteBatch.End();
				Rectangle adjustedClippingRectangle = Rectangle.Intersect(this.GetClippingRectangle(spriteBatch), spriteBatch.GraphicsDevice.ScissorRectangle);
				spriteBatch.GraphicsDevice.ScissorRectangle = adjustedClippingRectangle;
				spriteBatch.GraphicsDevice.RasterizerState = UIElement.OverflowHiddenRasterizerState;
				spriteBatch.Begin(0, BlendState.AlphaBlend, anisotropicClamp, DepthStencilState.None, UIElement.OverflowHiddenRasterizerState, null, Main.UIScaleMatrix);
			}
			this.DrawChildren(spriteBatch);
			if (overflowHidden)
			{
				rasterizerState = spriteBatch.GraphicsDevice.RasterizerState;
				spriteBatch.End();
				spriteBatch.GraphicsDevice.ScissorRectangle = scissorRectangle;
				spriteBatch.GraphicsDevice.RasterizerState = rasterizerState;
				spriteBatch.Begin(0, BlendState.AlphaBlend, anisotropicClamp, DepthStencilState.None, rasterizerState, null, Main.UIScaleMatrix);
			}
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x004B0FD8 File Offset: 0x004AF1D8
		public virtual void Update(GameTime gameTime)
		{
			if (this.OnUpdate != null)
			{
				this.OnUpdate(this);
			}
			foreach (UIElement uielement in this.Elements)
			{
				uielement.Update(gameTime);
			}
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x004B1040 File Offset: 0x004AF240
		public Rectangle GetClippingRectangle(SpriteBatch spriteBatch)
		{
			Vector2 vector;
			vector..ctor(this._innerDimensions.X, this._innerDimensions.Y);
			Vector2 position = new Vector2(this._innerDimensions.Width, this._innerDimensions.Height) + vector;
			vector = Vector2.Transform(vector, Main.UIScaleMatrix);
			position = Vector2.Transform(position, Main.UIScaleMatrix);
			Rectangle rectangle;
			rectangle..ctor((int)vector.X, (int)vector.Y, (int)(position.X - vector.X), (int)(position.Y - vector.Y));
			int num = (int)((float)Main.screenWidth * Main.UIScale);
			int num2 = (int)((float)Main.screenHeight * Main.UIScale);
			rectangle.X = Utils.Clamp<int>(rectangle.X, 0, num);
			rectangle.Y = Utils.Clamp<int>(rectangle.Y, 0, num2);
			rectangle.Width = Utils.Clamp<int>(rectangle.Width, 0, num - rectangle.X);
			rectangle.Height = Utils.Clamp<int>(rectangle.Height, 0, num2 - rectangle.Y);
			Rectangle scissorRectangle = spriteBatch.GraphicsDevice.ScissorRectangle;
			int num3 = Utils.Clamp<int>(rectangle.Left, scissorRectangle.Left, scissorRectangle.Right);
			int num4 = Utils.Clamp<int>(rectangle.Top, scissorRectangle.Top, scissorRectangle.Bottom);
			int num5 = Utils.Clamp<int>(rectangle.Right, scissorRectangle.Left, scissorRectangle.Right);
			int num6 = Utils.Clamp<int>(rectangle.Bottom, scissorRectangle.Top, scissorRectangle.Bottom);
			return new Rectangle(num3, num4, num5 - num3, num6 - num4);
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x004B11E4 File Offset: 0x004AF3E4
		public virtual List<SnapPoint> GetSnapPoints()
		{
			List<SnapPoint> list = new List<SnapPoint>();
			SnapPoint point;
			if (this.GetSnapPoint(out point))
			{
				list.Add(point);
			}
			foreach (UIElement element in this.Elements)
			{
				list.AddRange(element.GetSnapPoints());
			}
			return list;
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x004B1254 File Offset: 0x004AF454
		public virtual void Recalculate()
		{
			CalculatedStyle parentDimensions = (this.Parent == null) ? UserInterface.ActiveInstance.GetDimensions() : this.Parent.GetInnerDimensions();
			if (this.Parent != null && this.Parent is UIList)
			{
				parentDimensions.Height = float.MaxValue;
			}
			CalculatedStyle calculatedStyle = this._outerDimensions = this.GetDimensionsBasedOnParentDimensions(parentDimensions);
			calculatedStyle.X += this.MarginLeft;
			calculatedStyle.Y += this.MarginTop;
			calculatedStyle.Width -= this.MarginLeft + this.MarginRight;
			calculatedStyle.Height -= this.MarginTop + this.MarginBottom;
			this._dimensions = calculatedStyle;
			calculatedStyle.X += this.PaddingLeft;
			calculatedStyle.Y += this.PaddingTop;
			calculatedStyle.Width -= this.PaddingLeft + this.PaddingRight;
			calculatedStyle.Height -= this.PaddingTop + this.PaddingBottom;
			this._innerDimensions = calculatedStyle;
			this.RecalculateChildren();
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x004B136C File Offset: 0x004AF56C
		private CalculatedStyle GetDimensionsBasedOnParentDimensions(CalculatedStyle parentDimensions)
		{
			CalculatedStyle result = default(CalculatedStyle);
			result.X = this.Left.GetValue(parentDimensions.Width) + parentDimensions.X;
			result.Y = this.Top.GetValue(parentDimensions.Height) + parentDimensions.Y;
			float value = this.MinWidth.GetValue(parentDimensions.Width);
			float value2 = this.MaxWidth.GetValue(parentDimensions.Width);
			float value3 = this.MinHeight.GetValue(parentDimensions.Height);
			float value4 = this.MaxHeight.GetValue(parentDimensions.Height);
			result.Width = MathHelper.Clamp(this.Width.GetValue(parentDimensions.Width), value, value2);
			result.Height = MathHelper.Clamp(this.Height.GetValue(parentDimensions.Height), value3, value4);
			result.Width += this.MarginLeft + this.MarginRight;
			result.Height += this.MarginTop + this.MarginBottom;
			result.X += parentDimensions.Width * this.HAlign - result.Width * this.HAlign;
			result.Y += parentDimensions.Height * this.VAlign - result.Height * this.VAlign;
			return result;
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x004B14C4 File Offset: 0x004AF6C4
		public UIElement GetElementAt(Vector2 point)
		{
			UIElement uIElement = null;
			for (int num = this.Elements.Count - 1; num >= 0; num--)
			{
				UIElement uIElement2 = this.Elements[num];
				if (!uIElement2.IgnoresMouseInteraction && uIElement2.ContainsPoint(point))
				{
					uIElement = uIElement2;
					break;
				}
			}
			if (uIElement != null)
			{
				return uIElement.GetElementAt(point);
			}
			if (this.IgnoresMouseInteraction)
			{
				return null;
			}
			if (this.ContainsPoint(point))
			{
				return this;
			}
			return null;
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x004B1530 File Offset: 0x004AF730
		public virtual bool ContainsPoint(Vector2 point)
		{
			return point.X > this._dimensions.X && point.Y > this._dimensions.Y && point.X < this._dimensions.X + this._dimensions.Width && point.Y < this._dimensions.Y + this._dimensions.Height;
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x004B15A3 File Offset: 0x004AF7A3
		public virtual Rectangle GetViewCullingArea()
		{
			return this._dimensions.ToRectangle();
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x004B15B0 File Offset: 0x004AF7B0
		public void SetPadding(float pixels)
		{
			this.PaddingBottom = pixels;
			this.PaddingLeft = pixels;
			this.PaddingRight = pixels;
			this.PaddingTop = pixels;
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x004B15D0 File Offset: 0x004AF7D0
		public virtual void RecalculateChildren()
		{
			foreach (UIElement uielement in this.Elements)
			{
				uielement.Recalculate();
			}
		}

		/// <summary> The dimensions of the area within this element suitable for placing children elements. Takes into account <see cref="F:Terraria.UI.UIElement.PaddingLeft" />/Right/Top/Bottom. </summary>
		// Token: 0x060015D9 RID: 5593 RVA: 0x004B1620 File Offset: 0x004AF820
		public CalculatedStyle GetInnerDimensions()
		{
			return this._innerDimensions;
		}

		/// <summary>
		/// The dimensions of the area covered by this element. This is the area of this element interactible by the mouse.
		/// <para /> The width and height are derived from the <see cref="F:Terraria.UI.UIElement.Width" /> and <see cref="F:Terraria.UI.UIElement.Height" /> values of this element and will be limited by <see cref="F:Terraria.UI.UIElement.MinWidth" />/MaxWidth/MinHeight/MaxHeight as well as the <see cref="M:Terraria.UI.UIElement.GetInnerDimensions" /> of the parent element.
		/// <para /> The position is derived from the <see cref="F:Terraria.UI.UIElement.Top" />, <see cref="F:Terraria.UI.UIElement.Left" />, <see cref="F:Terraria.UI.UIElement.HAlign" />, <see cref="F:Terraria.UI.UIElement.VAlign" />, and <see cref="F:Terraria.UI.UIElement.MarginLeft" />/Right/Top/Bottom values of this element as well as the <see cref="M:Terraria.UI.UIElement.GetInnerDimensions" /> of the parent element.
		/// </summary>
		// Token: 0x060015DA RID: 5594 RVA: 0x004B1628 File Offset: 0x004AF828
		public CalculatedStyle GetDimensions()
		{
			return this._dimensions;
		}

		/// <summary> The dimensions of the area covered by this element plus the additional <see cref="F:Terraria.UI.UIElement.MarginLeft" />/Right/Top/Bottom. </summary>
		// Token: 0x060015DB RID: 5595 RVA: 0x004B1630 File Offset: 0x004AF830
		public CalculatedStyle GetOuterDimensions()
		{
			return this._outerDimensions;
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x004B1638 File Offset: 0x004AF838
		public void CopyStyle(UIElement element)
		{
			this.Top = element.Top;
			this.Left = element.Left;
			this.Width = element.Width;
			this.Height = element.Height;
			this.PaddingBottom = element.PaddingBottom;
			this.PaddingLeft = element.PaddingLeft;
			this.PaddingRight = element.PaddingRight;
			this.PaddingTop = element.PaddingTop;
			this.HAlign = element.HAlign;
			this.VAlign = element.VAlign;
			this.MinWidth = element.MinWidth;
			this.MaxWidth = element.MaxWidth;
			this.MinHeight = element.MinHeight;
			this.MaxHeight = element.MaxHeight;
			this.Recalculate();
		}

		/// <summary>
		/// Called when the UIElement under the mouse is left clicked. The default code calls the <see cref="E:Terraria.UI.UIElement.OnLeftMouseDown" /> event and then calls <see cref="M:Terraria.UI.UIElement.LeftMouseDown(Terraria.UI.UIMouseEvent)" /> on the <see cref="P:Terraria.UI.UIElement.Parent" /> element.
		/// <para /> Since the method is called on all parent elements in the hierarchy, check <c>if (evt.Target == this)</c> for code only interested in direct clicks to this element. Children elements overlaying this element can be ignored by setting <see cref="F:Terraria.UI.UIElement.IgnoresMouseInteraction" /> to true on them.
		/// </summary>
		/// <param name="evt"></param>
		// Token: 0x060015DD RID: 5597 RVA: 0x004B16F3 File Offset: 0x004AF8F3
		public virtual void LeftMouseDown(UIMouseEvent evt)
		{
			if (this.OnLeftMouseDown != null)
			{
				this.OnLeftMouseDown(evt, this);
			}
			if (this.Parent != null)
			{
				this.Parent.LeftMouseDown(evt);
			}
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x004B171E File Offset: 0x004AF91E
		public virtual void LeftMouseUp(UIMouseEvent evt)
		{
			if (this.OnLeftMouseUp != null)
			{
				this.OnLeftMouseUp(evt, this);
			}
			if (this.Parent != null)
			{
				this.Parent.LeftMouseUp(evt);
			}
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x004B1749 File Offset: 0x004AF949
		public virtual void LeftClick(UIMouseEvent evt)
		{
			if (this.OnLeftClick != null)
			{
				this.OnLeftClick(evt, this);
			}
			if (this.Parent != null)
			{
				this.Parent.LeftClick(evt);
			}
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x004B1774 File Offset: 0x004AF974
		public virtual void LeftDoubleClick(UIMouseEvent evt)
		{
			if (this.OnLeftDoubleClick != null)
			{
				this.OnLeftDoubleClick(evt, this);
			}
			if (this.Parent != null)
			{
				this.Parent.LeftDoubleClick(evt);
			}
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x004B179F File Offset: 0x004AF99F
		public virtual void RightMouseDown(UIMouseEvent evt)
		{
			if (this.OnRightMouseDown != null)
			{
				this.OnRightMouseDown(evt, this);
			}
			if (this.Parent != null)
			{
				this.Parent.RightMouseDown(evt);
			}
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x004B17CA File Offset: 0x004AF9CA
		public virtual void RightMouseUp(UIMouseEvent evt)
		{
			if (this.OnRightMouseUp != null)
			{
				this.OnRightMouseUp(evt, this);
			}
			if (this.Parent != null)
			{
				this.Parent.RightMouseUp(evt);
			}
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x004B17F5 File Offset: 0x004AF9F5
		public virtual void RightClick(UIMouseEvent evt)
		{
			if (this.OnRightClick != null)
			{
				this.OnRightClick(evt, this);
			}
			if (this.Parent != null)
			{
				this.Parent.RightClick(evt);
			}
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x004B1820 File Offset: 0x004AFA20
		public virtual void RightDoubleClick(UIMouseEvent evt)
		{
			if (this.OnRightDoubleClick != null)
			{
				this.OnRightDoubleClick(evt, this);
			}
			if (this.Parent != null)
			{
				this.Parent.RightDoubleClick(evt);
			}
		}

		/// <summary>
		/// Called once when this UIElement becomes moused over (hovered). Default implementation sets <see cref="P:Terraria.UI.UIElement.IsMouseHovering" /> to true, calls <see cref="E:Terraria.UI.UIElement.OnMouseOver" /> event, then calls this same method on the <see cref="P:Terraria.UI.UIElement.Parent" /> element.
		/// <para /> Useful for changing visuals to indicate the element is interactable, as is the <see cref="E:Terraria.UI.UIElement.OnMouseOver" /> event.
		/// <para /> Any code that needs to run as long as the element is hovered should use check <see cref="P:Terraria.UI.UIElement.IsMouseHovering" /> in <see cref="M:Terraria.UI.UIElement.Update(Microsoft.Xna.Framework.GameTime)" />.
		/// <para /> <see cref="M:Terraria.UI.UIElement.MouseOut(Terraria.UI.UIMouseEvent)" /> will be called when it no longer hovered.
		/// </summary>
		// Token: 0x060015E5 RID: 5605 RVA: 0x004B184B File Offset: 0x004AFA4B
		public virtual void MouseOver(UIMouseEvent evt)
		{
			this.IsMouseHovering = true;
			if (this.OnMouseOver != null)
			{
				this.OnMouseOver(evt, this);
			}
			if (this.Parent != null)
			{
				this.Parent.MouseOver(evt);
			}
		}

		/// <summary>
		/// Called once when this UIElement is no longer moused over. Default implementation sets <see cref="P:Terraria.UI.UIElement.IsMouseHovering" /> to false, calls <see cref="E:Terraria.UI.UIElement.OnMouseOut" /> event, then calls this same method on the <see cref="P:Terraria.UI.UIElement.Parent" /> element.
		/// <para /> Useful for changing visuals to indicate the element is no longer interactable, as is the <see cref="E:Terraria.UI.UIElement.OnMouseOut" /> event.
		/// <para /> <see cref="M:Terraria.UI.UIElement.MouseOver(Terraria.UI.UIMouseEvent)" /> will be called when it is hovered once again.
		/// </summary>
		// Token: 0x060015E6 RID: 5606 RVA: 0x004B187D File Offset: 0x004AFA7D
		public virtual void MouseOut(UIMouseEvent evt)
		{
			this.IsMouseHovering = false;
			if (this.OnMouseOut != null)
			{
				this.OnMouseOut(evt, this);
			}
			if (this.Parent != null)
			{
				this.Parent.MouseOut(evt);
			}
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x004B18AF File Offset: 0x004AFAAF
		public virtual void ScrollWheel(UIScrollWheelEvent evt)
		{
			if (this.OnScrollWheel != null)
			{
				this.OnScrollWheel(evt, this);
			}
			if (this.Parent != null)
			{
				this.Parent.ScrollWheel(evt);
			}
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x004B18DC File Offset: 0x004AFADC
		public void Activate()
		{
			if (!this._isInitialized)
			{
				this.Initialize();
			}
			this.OnActivate();
			foreach (UIElement uielement in this.Elements)
			{
				uielement.Activate();
			}
		}

		/// <summary>
		/// Called each time this element is activated, which is usually when a <see cref="T:Terraria.UI.UIState" /> is activated via <see cref="M:Terraria.UI.UserInterface.SetState(Terraria.UI.UIState)" />. Use this to run code to update elements whenever the UI is toggled on.
		/// </summary>
		// Token: 0x060015E9 RID: 5609 RVA: 0x004B1940 File Offset: 0x004AFB40
		public virtual void OnActivate()
		{
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x004B1944 File Offset: 0x004AFB44
		[Conditional("DEBUG")]
		public void DrawDebugHitbox(BasicDebugDrawer drawer, float colorIntensity = 0f)
		{
			if (this.IsMouseHovering)
			{
				colorIntensity += 0.1f;
			}
			Color color = Main.hslToRgb(colorIntensity, colorIntensity, 0.5f, byte.MaxValue);
			CalculatedStyle innerDimensions = this.GetInnerDimensions();
			drawer.DrawLine(innerDimensions.Position(), innerDimensions.Position() + new Vector2(innerDimensions.Width, 0f), 2f, color);
			drawer.DrawLine(innerDimensions.Position() + new Vector2(innerDimensions.Width, 0f), innerDimensions.Position() + new Vector2(innerDimensions.Width, innerDimensions.Height), 2f, color);
			drawer.DrawLine(innerDimensions.Position() + new Vector2(innerDimensions.Width, innerDimensions.Height), innerDimensions.Position() + new Vector2(0f, innerDimensions.Height), 2f, color);
			drawer.DrawLine(innerDimensions.Position() + new Vector2(0f, innerDimensions.Height), innerDimensions.Position(), 2f, color);
			foreach (UIElement uielement in this.Elements)
			{
			}
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x004B1AA4 File Offset: 0x004AFCA4
		public void Deactivate()
		{
			this.OnDeactivate();
			foreach (UIElement uielement in this.Elements)
			{
				uielement.Deactivate();
			}
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x004B1AFC File Offset: 0x004AFCFC
		public virtual void OnDeactivate()
		{
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x004B1AFE File Offset: 0x004AFCFE
		public void Initialize()
		{
			this.OnInitialize();
			this._isInitialized = true;
		}

		/// <summary>
		/// Called before the first time this element is activated (see <see cref="M:Terraria.UI.UIElement.OnActivate" />). Use this method to create and append other UIElement to this to build a UI. 
		/// </summary>
		// Token: 0x060015EE RID: 5614 RVA: 0x004B1B0D File Offset: 0x004AFD0D
		public virtual void OnInitialize()
		{
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x004B1B0F File Offset: 0x004AFD0F
		public virtual int CompareTo(object obj)
		{
			return 0;
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060015F0 RID: 5616 RVA: 0x004B1B14 File Offset: 0x004AFD14
		// (remove) Token: 0x060015F1 RID: 5617 RVA: 0x004B1B4C File Offset: 0x004AFD4C
		public event UIElement.MouseEvent OnMiddleMouseDown;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060015F2 RID: 5618 RVA: 0x004B1B84 File Offset: 0x004AFD84
		// (remove) Token: 0x060015F3 RID: 5619 RVA: 0x004B1BBC File Offset: 0x004AFDBC
		public event UIElement.MouseEvent OnMiddleMouseUp;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060015F4 RID: 5620 RVA: 0x004B1BF4 File Offset: 0x004AFDF4
		// (remove) Token: 0x060015F5 RID: 5621 RVA: 0x004B1C2C File Offset: 0x004AFE2C
		public event UIElement.MouseEvent OnMiddleClick;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x060015F6 RID: 5622 RVA: 0x004B1C64 File Offset: 0x004AFE64
		// (remove) Token: 0x060015F7 RID: 5623 RVA: 0x004B1C9C File Offset: 0x004AFE9C
		public event UIElement.MouseEvent OnMiddleDoubleClick;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x060015F8 RID: 5624 RVA: 0x004B1CD4 File Offset: 0x004AFED4
		// (remove) Token: 0x060015F9 RID: 5625 RVA: 0x004B1D0C File Offset: 0x004AFF0C
		public event UIElement.MouseEvent OnXButton1MouseDown;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x060015FA RID: 5626 RVA: 0x004B1D44 File Offset: 0x004AFF44
		// (remove) Token: 0x060015FB RID: 5627 RVA: 0x004B1D7C File Offset: 0x004AFF7C
		public event UIElement.MouseEvent OnXButton1MouseUp;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x060015FC RID: 5628 RVA: 0x004B1DB4 File Offset: 0x004AFFB4
		// (remove) Token: 0x060015FD RID: 5629 RVA: 0x004B1DEC File Offset: 0x004AFFEC
		public event UIElement.MouseEvent OnXButton1Click;

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x060015FE RID: 5630 RVA: 0x004B1E24 File Offset: 0x004B0024
		// (remove) Token: 0x060015FF RID: 5631 RVA: 0x004B1E5C File Offset: 0x004B005C
		public event UIElement.MouseEvent OnXButton1DoubleClick;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06001600 RID: 5632 RVA: 0x004B1E94 File Offset: 0x004B0094
		// (remove) Token: 0x06001601 RID: 5633 RVA: 0x004B1ECC File Offset: 0x004B00CC
		public event UIElement.MouseEvent OnXButton2MouseDown;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06001602 RID: 5634 RVA: 0x004B1F04 File Offset: 0x004B0104
		// (remove) Token: 0x06001603 RID: 5635 RVA: 0x004B1F3C File Offset: 0x004B013C
		public event UIElement.MouseEvent OnXButton2MouseUp;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06001604 RID: 5636 RVA: 0x004B1F74 File Offset: 0x004B0174
		// (remove) Token: 0x06001605 RID: 5637 RVA: 0x004B1FAC File Offset: 0x004B01AC
		public event UIElement.MouseEvent OnXButton2Click;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06001606 RID: 5638 RVA: 0x004B1FE4 File Offset: 0x004B01E4
		// (remove) Token: 0x06001607 RID: 5639 RVA: 0x004B201C File Offset: 0x004B021C
		public event UIElement.MouseEvent OnXButton2DoubleClick;

		// Token: 0x06001608 RID: 5640 RVA: 0x004B2051 File Offset: 0x004B0251
		public bool HasChild(UIElement child)
		{
			return this.Elements.Contains(child);
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x004B205F File Offset: 0x004B025F
		public virtual void MiddleMouseDown(UIMouseEvent evt)
		{
			UIElement.MouseEvent onMiddleMouseDown = this.OnMiddleMouseDown;
			if (onMiddleMouseDown != null)
			{
				onMiddleMouseDown(evt, this);
			}
			UIElement parent = this.Parent;
			if (parent == null)
			{
				return;
			}
			parent.MiddleMouseDown(evt);
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x004B2085 File Offset: 0x004B0285
		public virtual void MiddleMouseUp(UIMouseEvent evt)
		{
			UIElement.MouseEvent onMiddleMouseUp = this.OnMiddleMouseUp;
			if (onMiddleMouseUp != null)
			{
				onMiddleMouseUp(evt, this);
			}
			UIElement parent = this.Parent;
			if (parent == null)
			{
				return;
			}
			parent.MiddleMouseUp(evt);
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x004B20AB File Offset: 0x004B02AB
		public virtual void MiddleClick(UIMouseEvent evt)
		{
			UIElement.MouseEvent onMiddleClick = this.OnMiddleClick;
			if (onMiddleClick != null)
			{
				onMiddleClick(evt, this);
			}
			UIElement parent = this.Parent;
			if (parent == null)
			{
				return;
			}
			parent.MiddleClick(evt);
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x004B20D1 File Offset: 0x004B02D1
		public virtual void MiddleDoubleClick(UIMouseEvent evt)
		{
			UIElement.MouseEvent onMiddleDoubleClick = this.OnMiddleDoubleClick;
			if (onMiddleDoubleClick != null)
			{
				onMiddleDoubleClick(evt, this);
			}
			UIElement parent = this.Parent;
			if (parent == null)
			{
				return;
			}
			parent.MiddleDoubleClick(evt);
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x004B20F7 File Offset: 0x004B02F7
		public virtual void XButton1MouseDown(UIMouseEvent evt)
		{
			UIElement.MouseEvent onXButton1MouseDown = this.OnXButton1MouseDown;
			if (onXButton1MouseDown != null)
			{
				onXButton1MouseDown(evt, this);
			}
			UIElement parent = this.Parent;
			if (parent == null)
			{
				return;
			}
			parent.XButton1MouseDown(evt);
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x004B211D File Offset: 0x004B031D
		public virtual void XButton1MouseUp(UIMouseEvent evt)
		{
			UIElement.MouseEvent onXButton1MouseUp = this.OnXButton1MouseUp;
			if (onXButton1MouseUp != null)
			{
				onXButton1MouseUp(evt, this);
			}
			UIElement parent = this.Parent;
			if (parent == null)
			{
				return;
			}
			parent.XButton1MouseUp(evt);
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x004B2143 File Offset: 0x004B0343
		public virtual void XButton1Click(UIMouseEvent evt)
		{
			UIElement.MouseEvent onXButton1Click = this.OnXButton1Click;
			if (onXButton1Click != null)
			{
				onXButton1Click(evt, this);
			}
			UIElement parent = this.Parent;
			if (parent == null)
			{
				return;
			}
			parent.XButton1Click(evt);
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x004B2169 File Offset: 0x004B0369
		public virtual void XButton1DoubleClick(UIMouseEvent evt)
		{
			UIElement.MouseEvent onXButton1DoubleClick = this.OnXButton1DoubleClick;
			if (onXButton1DoubleClick != null)
			{
				onXButton1DoubleClick(evt, this);
			}
			UIElement parent = this.Parent;
			if (parent == null)
			{
				return;
			}
			parent.XButton1DoubleClick(evt);
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x004B218F File Offset: 0x004B038F
		public virtual void XButton2MouseDown(UIMouseEvent evt)
		{
			UIElement.MouseEvent onXButton2MouseDown = this.OnXButton2MouseDown;
			if (onXButton2MouseDown != null)
			{
				onXButton2MouseDown(evt, this);
			}
			UIElement parent = this.Parent;
			if (parent == null)
			{
				return;
			}
			parent.XButton2MouseDown(evt);
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x004B21B5 File Offset: 0x004B03B5
		public virtual void XButton2MouseUp(UIMouseEvent evt)
		{
			UIElement.MouseEvent onXButton2MouseUp = this.OnXButton2MouseUp;
			if (onXButton2MouseUp != null)
			{
				onXButton2MouseUp(evt, this);
			}
			UIElement parent = this.Parent;
			if (parent == null)
			{
				return;
			}
			parent.XButton2MouseUp(evt);
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x004B21DB File Offset: 0x004B03DB
		public virtual void XButton2Click(UIMouseEvent evt)
		{
			UIElement.MouseEvent onXButton2Click = this.OnXButton2Click;
			if (onXButton2Click != null)
			{
				onXButton2Click(evt, this);
			}
			UIElement parent = this.Parent;
			if (parent == null)
			{
				return;
			}
			parent.XButton2Click(evt);
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x004B2201 File Offset: 0x004B0401
		public virtual void XButton2DoubleClick(UIMouseEvent evt)
		{
			UIElement.MouseEvent onXButton2DoubleClick = this.OnXButton2DoubleClick;
			if (onXButton2DoubleClick != null)
			{
				onXButton2DoubleClick(evt, this);
			}
			UIElement parent = this.Parent;
			if (parent == null)
			{
				return;
			}
			parent.XButton2DoubleClick(evt);
		}

		// Token: 0x04001121 RID: 4385
		protected readonly List<UIElement> Elements = new List<UIElement>();

		/// <summary> How far down from the top edge of the <see cref="P:Terraria.UI.UIElement.Parent" /> element's <see cref="M:Terraria.UI.UIElement.GetInnerDimensions" /> that this element will be positioned. See also <see cref="F:Terraria.UI.UIElement.HAlign" /> for another positioning option. </summary>
		// Token: 0x04001122 RID: 4386
		public StyleDimension Top;

		/// <summary> How far right from the left edge of the <see cref="P:Terraria.UI.UIElement.Parent" /> element's <see cref="M:Terraria.UI.UIElement.GetInnerDimensions" /> that this element will be positioned. See also <see cref="F:Terraria.UI.UIElement.VAlign" /> for another positioning option. </summary>
		// Token: 0x04001123 RID: 4387
		public StyleDimension Left;

		/// <summary> How wide this element intends to be. The calculated width will be clamped between <see cref="F:Terraria.UI.UIElement.MinWidth" /> and <see cref="F:Terraria.UI.UIElement.MaxWidth" /> according to the <see cref="M:Terraria.UI.UIElement.GetInnerDimensions" /> of the parent element. </summary>
		// Token: 0x04001124 RID: 4388
		public StyleDimension Width;

		/// <summary> How tall this element intends to be. The calculated height will be clamped between <see cref="F:Terraria.UI.UIElement.MinHeight" /> and <see cref="F:Terraria.UI.UIElement.MaxHeight" /> according to the <see cref="M:Terraria.UI.UIElement.GetInnerDimensions" /> of the parent element. </summary>
		// Token: 0x04001125 RID: 4389
		public StyleDimension Height;

		/// <summary> The maximum width of this element. Defaults to the full width. </summary>
		// Token: 0x04001126 RID: 4390
		public StyleDimension MaxWidth = StyleDimension.Fill;

		/// <summary> The maximum height of this element. Defaults to the full height. </summary>
		// Token: 0x04001127 RID: 4391
		public StyleDimension MaxHeight = StyleDimension.Fill;

		/// <summary> The minimum width of this element. Defaults to no width. </summary>
		// Token: 0x04001128 RID: 4392
		public StyleDimension MinWidth = StyleDimension.Empty;

		/// <summary> The maximum width of this element. Defaults to no height. </summary>
		// Token: 0x04001129 RID: 4393
		public StyleDimension MinHeight = StyleDimension.Empty;

		// Token: 0x0400112A RID: 4394
		private bool _isInitialized;

		/// <summary>
		/// If true, this element will be ignored for mouse interactions. This can be used to allow UIElement placed over other  UIElement to not be the target of mouse clicks. Defaults to false.
		/// </summary>
		// Token: 0x0400112B RID: 4395
		public bool IgnoresMouseInteraction;

		// Token: 0x0400112C RID: 4396
		public bool OverflowHidden;

		// Token: 0x0400112D RID: 4397
		public SamplerState OverrideSamplerState;

		/// <summary>
		/// Additional spacing between this element's <see cref="M:Terraria.UI.UIElement.GetDimensions" /> and the position of its children placed within. 
		/// </summary>
		// Token: 0x0400112E RID: 4398
		public float PaddingTop;

		/// <inheritdoc cref="F:Terraria.UI.UIElement.PaddingTop" />
		// Token: 0x0400112F RID: 4399
		public float PaddingLeft;

		/// <inheritdoc cref="F:Terraria.UI.UIElement.PaddingTop" />
		// Token: 0x04001130 RID: 4400
		public float PaddingRight;

		/// <inheritdoc cref="F:Terraria.UI.UIElement.PaddingTop" />
		// Token: 0x04001131 RID: 4401
		public float PaddingBottom;

		/// <summary>
		/// Additional spacing between this element and the <see cref="M:Terraria.UI.UIElement.GetInnerDimensions" /> of its parent element. 
		/// </summary>
		// Token: 0x04001132 RID: 4402
		public float MarginTop;

		/// <inheritdoc cref="F:Terraria.UI.UIElement.MarginTop" />
		// Token: 0x04001133 RID: 4403
		public float MarginLeft;

		/// <inheritdoc cref="F:Terraria.UI.UIElement.MarginTop" />
		// Token: 0x04001134 RID: 4404
		public float MarginRight;

		/// <inheritdoc cref="F:Terraria.UI.UIElement.MarginTop" />
		// Token: 0x04001135 RID: 4405
		public float MarginBottom;

		/// <summary>
		/// Aligns the element horizontally within the available space. A value of 1 will place the element at the right side. A value of 0.5 will place the element centered horizontally.
		/// </summary>
		// Token: 0x04001136 RID: 4406
		public float HAlign;

		/// <summary>
		/// Aligns the element vertically within the available space. A value of 1 will place the element at the bottom. A value of 0.5 will place the element centered vertically.
		/// </summary>
		// Token: 0x04001137 RID: 4407
		public float VAlign;

		// Token: 0x04001138 RID: 4408
		private CalculatedStyle _innerDimensions;

		// Token: 0x04001139 RID: 4409
		private CalculatedStyle _dimensions;

		// Token: 0x0400113A RID: 4410
		private CalculatedStyle _outerDimensions;

		// Token: 0x0400113B RID: 4411
		private static readonly RasterizerState OverflowHiddenRasterizerState = new RasterizerState
		{
			CullMode = 0,
			ScissorTestEnable = true
		};

		// Token: 0x0400113C RID: 4412
		public bool UseImmediateMode;

		// Token: 0x0400113D RID: 4413
		private SnapPoint _snapPoint;

		// Token: 0x0400113E RID: 4414
		private static int _idCounter = 0;

		// Token: 0x02000871 RID: 2161
		// (Invoke) Token: 0x06005171 RID: 20849
		public delegate void MouseEvent(UIMouseEvent evt, UIElement listeningElement);

		// Token: 0x02000872 RID: 2162
		// (Invoke) Token: 0x06005175 RID: 20853
		public delegate void ScrollWheelEvent(UIScrollWheelEvent evt, UIElement listeningElement);

		// Token: 0x02000873 RID: 2163
		// (Invoke) Token: 0x06005179 RID: 20857
		public delegate void ElementEvent(UIElement affectedElement);

		// Token: 0x02000874 RID: 2164
		// (Invoke) Token: 0x0600517D RID: 20861
		public delegate void UIElementAction(UIElement element);
	}
}
