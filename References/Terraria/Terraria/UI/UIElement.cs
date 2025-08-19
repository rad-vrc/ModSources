using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.GameContent.UI.Elements;

namespace Terraria.UI
{
	// Token: 0x02000094 RID: 148
	public class UIElement : IComparable
	{
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060012A8 RID: 4776 RVA: 0x0049B8AA File Offset: 0x00499AAA
		// (set) Token: 0x060012A9 RID: 4777 RVA: 0x0049B8B2 File Offset: 0x00499AB2
		public UIElement Parent { get; private set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x0049B8BB File Offset: 0x00499ABB
		// (set) Token: 0x060012AB RID: 4779 RVA: 0x0049B8C3 File Offset: 0x00499AC3
		public int UniqueId { get; private set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060012AC RID: 4780 RVA: 0x0049B8CC File Offset: 0x00499ACC
		public IEnumerable<UIElement> Children
		{
			get
			{
				return this.Elements;
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060012AD RID: 4781 RVA: 0x0049B8D4 File Offset: 0x00499AD4
		// (remove) Token: 0x060012AE RID: 4782 RVA: 0x0049B90C File Offset: 0x00499B0C
		public event UIElement.MouseEvent OnLeftMouseDown;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060012AF RID: 4783 RVA: 0x0049B944 File Offset: 0x00499B44
		// (remove) Token: 0x060012B0 RID: 4784 RVA: 0x0049B97C File Offset: 0x00499B7C
		public event UIElement.MouseEvent OnLeftMouseUp;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060012B1 RID: 4785 RVA: 0x0049B9B4 File Offset: 0x00499BB4
		// (remove) Token: 0x060012B2 RID: 4786 RVA: 0x0049B9EC File Offset: 0x00499BEC
		public event UIElement.MouseEvent OnLeftClick;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060012B3 RID: 4787 RVA: 0x0049BA24 File Offset: 0x00499C24
		// (remove) Token: 0x060012B4 RID: 4788 RVA: 0x0049BA5C File Offset: 0x00499C5C
		public event UIElement.MouseEvent OnLeftDoubleClick;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060012B5 RID: 4789 RVA: 0x0049BA94 File Offset: 0x00499C94
		// (remove) Token: 0x060012B6 RID: 4790 RVA: 0x0049BACC File Offset: 0x00499CCC
		public event UIElement.MouseEvent OnRightMouseDown;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060012B7 RID: 4791 RVA: 0x0049BB04 File Offset: 0x00499D04
		// (remove) Token: 0x060012B8 RID: 4792 RVA: 0x0049BB3C File Offset: 0x00499D3C
		public event UIElement.MouseEvent OnRightMouseUp;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060012B9 RID: 4793 RVA: 0x0049BB74 File Offset: 0x00499D74
		// (remove) Token: 0x060012BA RID: 4794 RVA: 0x0049BBAC File Offset: 0x00499DAC
		public event UIElement.MouseEvent OnRightClick;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060012BB RID: 4795 RVA: 0x0049BBE4 File Offset: 0x00499DE4
		// (remove) Token: 0x060012BC RID: 4796 RVA: 0x0049BC1C File Offset: 0x00499E1C
		public event UIElement.MouseEvent OnRightDoubleClick;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060012BD RID: 4797 RVA: 0x0049BC54 File Offset: 0x00499E54
		// (remove) Token: 0x060012BE RID: 4798 RVA: 0x0049BC8C File Offset: 0x00499E8C
		public event UIElement.MouseEvent OnMouseOver;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060012BF RID: 4799 RVA: 0x0049BCC4 File Offset: 0x00499EC4
		// (remove) Token: 0x060012C0 RID: 4800 RVA: 0x0049BCFC File Offset: 0x00499EFC
		public event UIElement.MouseEvent OnMouseOut;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060012C1 RID: 4801 RVA: 0x0049BD34 File Offset: 0x00499F34
		// (remove) Token: 0x060012C2 RID: 4802 RVA: 0x0049BD6C File Offset: 0x00499F6C
		public event UIElement.ScrollWheelEvent OnScrollWheel;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x060012C3 RID: 4803 RVA: 0x0049BDA4 File Offset: 0x00499FA4
		// (remove) Token: 0x060012C4 RID: 4804 RVA: 0x0049BDDC File Offset: 0x00499FDC
		public event UIElement.ElementEvent OnUpdate;

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060012C5 RID: 4805 RVA: 0x0049BE11 File Offset: 0x0049A011
		// (set) Token: 0x060012C6 RID: 4806 RVA: 0x0049BE19 File Offset: 0x0049A019
		public bool IsMouseHovering { get; private set; }

		// Token: 0x060012C7 RID: 4807 RVA: 0x0049BE24 File Offset: 0x0049A024
		public UIElement()
		{
			this.UniqueId = UIElement._idCounter++;
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x0049BE84 File Offset: 0x0049A084
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

		// Token: 0x060012C9 RID: 4809 RVA: 0x0049BEDB File Offset: 0x0049A0DB
		public bool GetSnapPoint(out SnapPoint point)
		{
			point = this._snapPoint;
			if (this._snapPoint != null)
			{
				this._snapPoint.Calculate(this);
			}
			return this._snapPoint != null;
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x0049BF04 File Offset: 0x0049A104
		public virtual void ExecuteRecursively(UIElement.UIElementAction action)
		{
			action(this);
			foreach (UIElement uielement in this.Elements)
			{
				uielement.ExecuteRecursively(action);
			}
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		protected virtual void DrawSelf(SpriteBatch spriteBatch)
		{
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x0049BF5C File Offset: 0x0049A15C
		protected virtual void DrawChildren(SpriteBatch spriteBatch)
		{
			foreach (UIElement uielement in this.Elements)
			{
				uielement.Draw(spriteBatch);
			}
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x0049BFB0 File Offset: 0x0049A1B0
		public void Append(UIElement element)
		{
			element.Remove();
			element.Parent = this;
			this.Elements.Add(element);
			element.Recalculate();
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x0049BFD1 File Offset: 0x0049A1D1
		public void Remove()
		{
			if (this.Parent != null)
			{
				this.Parent.RemoveChild(this);
			}
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x0049BFE7 File Offset: 0x0049A1E7
		public void RemoveChild(UIElement child)
		{
			this.Elements.Remove(child);
			child.Parent = null;
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x0049C000 File Offset: 0x0049A200
		public void RemoveAllChildren()
		{
			foreach (UIElement uielement in this.Elements)
			{
				uielement.Parent = null;
			}
			this.Elements.Clear();
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x0049C05C File Offset: 0x0049A25C
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
				spriteBatch.Begin(useImmediateMode ? SpriteSortMode.Immediate : SpriteSortMode.Deferred, BlendState.AlphaBlend, (this.OverrideSamplerState != null) ? this.OverrideSamplerState : anisotropicClamp, DepthStencilState.None, UIElement.OverflowHiddenRasterizerState, null, Main.UIScaleMatrix);
				this.DrawSelf(spriteBatch);
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, anisotropicClamp, DepthStencilState.None, UIElement.OverflowHiddenRasterizerState, null, Main.UIScaleMatrix);
			}
			else
			{
				this.DrawSelf(spriteBatch);
			}
			if (overflowHidden)
			{
				spriteBatch.End();
				Rectangle clippingRectangle = this.GetClippingRectangle(spriteBatch);
				spriteBatch.GraphicsDevice.ScissorRectangle = clippingRectangle;
				spriteBatch.GraphicsDevice.RasterizerState = UIElement.OverflowHiddenRasterizerState;
				spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, anisotropicClamp, DepthStencilState.None, UIElement.OverflowHiddenRasterizerState, null, Main.UIScaleMatrix);
			}
			this.DrawChildren(spriteBatch);
			if (overflowHidden)
			{
				spriteBatch.End();
				spriteBatch.GraphicsDevice.ScissorRectangle = scissorRectangle;
				spriteBatch.GraphicsDevice.RasterizerState = rasterizerState;
				spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, anisotropicClamp, DepthStencilState.None, rasterizerState, null, Main.UIScaleMatrix);
			}
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x0049C198 File Offset: 0x0049A398
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

		// Token: 0x060012D3 RID: 4819 RVA: 0x0049C200 File Offset: 0x0049A400
		public Rectangle GetClippingRectangle(SpriteBatch spriteBatch)
		{
			Vector2 vector = new Vector2(this._innerDimensions.X, this._innerDimensions.Y);
			Vector2 vector2 = new Vector2(this._innerDimensions.Width, this._innerDimensions.Height) + vector;
			vector = Vector2.Transform(vector, Main.UIScaleMatrix);
			vector2 = Vector2.Transform(vector2, Main.UIScaleMatrix);
			Rectangle rectangle = new Rectangle((int)vector.X, (int)vector.Y, (int)(vector2.X - vector.X), (int)(vector2.Y - vector.Y));
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

		// Token: 0x060012D4 RID: 4820 RVA: 0x0049C3A4 File Offset: 0x0049A5A4
		public virtual List<SnapPoint> GetSnapPoints()
		{
			List<SnapPoint> list = new List<SnapPoint>();
			SnapPoint item;
			if (this.GetSnapPoint(out item))
			{
				list.Add(item);
			}
			foreach (UIElement uielement in this.Elements)
			{
				list.AddRange(uielement.GetSnapPoints());
			}
			return list;
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x0049C414 File Offset: 0x0049A614
		public virtual void Recalculate()
		{
			CalculatedStyle parentDimensions;
			if (this.Parent != null)
			{
				parentDimensions = this.Parent.GetInnerDimensions();
			}
			else
			{
				parentDimensions = UserInterface.ActiveInstance.GetDimensions();
			}
			if (this.Parent != null && this.Parent is UIList)
			{
				parentDimensions.Height = float.MaxValue;
			}
			CalculatedStyle dimensionsBasedOnParentDimensions = this.GetDimensionsBasedOnParentDimensions(parentDimensions);
			this._outerDimensions = dimensionsBasedOnParentDimensions;
			dimensionsBasedOnParentDimensions.X += this.MarginLeft;
			dimensionsBasedOnParentDimensions.Y += this.MarginTop;
			dimensionsBasedOnParentDimensions.Width -= this.MarginLeft + this.MarginRight;
			dimensionsBasedOnParentDimensions.Height -= this.MarginTop + this.MarginBottom;
			this._dimensions = dimensionsBasedOnParentDimensions;
			dimensionsBasedOnParentDimensions.X += this.PaddingLeft;
			dimensionsBasedOnParentDimensions.Y += this.PaddingTop;
			dimensionsBasedOnParentDimensions.Width -= this.PaddingLeft + this.PaddingRight;
			dimensionsBasedOnParentDimensions.Height -= this.PaddingTop + this.PaddingBottom;
			this._innerDimensions = dimensionsBasedOnParentDimensions;
			this.RecalculateChildren();
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x0049C52C File Offset: 0x0049A72C
		private CalculatedStyle GetDimensionsBasedOnParentDimensions(CalculatedStyle parentDimensions)
		{
			CalculatedStyle calculatedStyle;
			calculatedStyle.X = this.Left.GetValue(parentDimensions.Width) + parentDimensions.X;
			calculatedStyle.Y = this.Top.GetValue(parentDimensions.Height) + parentDimensions.Y;
			float value = this.MinWidth.GetValue(parentDimensions.Width);
			float value2 = this.MaxWidth.GetValue(parentDimensions.Width);
			float value3 = this.MinHeight.GetValue(parentDimensions.Height);
			float value4 = this.MaxHeight.GetValue(parentDimensions.Height);
			calculatedStyle.Width = MathHelper.Clamp(this.Width.GetValue(parentDimensions.Width), value, value2);
			calculatedStyle.Height = MathHelper.Clamp(this.Height.GetValue(parentDimensions.Height), value3, value4);
			calculatedStyle.Width += this.MarginLeft + this.MarginRight;
			calculatedStyle.Height += this.MarginTop + this.MarginBottom;
			calculatedStyle.X += parentDimensions.Width * this.HAlign - calculatedStyle.Width * this.HAlign;
			calculatedStyle.Y += parentDimensions.Height * this.VAlign - calculatedStyle.Height * this.VAlign;
			return calculatedStyle;
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x0049C67C File Offset: 0x0049A87C
		public UIElement GetElementAt(Vector2 point)
		{
			UIElement uielement = null;
			for (int i = this.Elements.Count - 1; i >= 0; i--)
			{
				UIElement uielement2 = this.Elements[i];
				if (!uielement2.IgnoresMouseInteraction && uielement2.ContainsPoint(point))
				{
					uielement = uielement2;
					break;
				}
			}
			if (uielement != null)
			{
				return uielement.GetElementAt(point);
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

		// Token: 0x060012D8 RID: 4824 RVA: 0x0049C6E8 File Offset: 0x0049A8E8
		public virtual bool ContainsPoint(Vector2 point)
		{
			return point.X > this._dimensions.X && point.Y > this._dimensions.Y && point.X < this._dimensions.X + this._dimensions.Width && point.Y < this._dimensions.Y + this._dimensions.Height;
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x0049C75B File Offset: 0x0049A95B
		public virtual Rectangle GetViewCullingArea()
		{
			return this._dimensions.ToRectangle();
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x0049C768 File Offset: 0x0049A968
		public void SetPadding(float pixels)
		{
			this.PaddingBottom = pixels;
			this.PaddingLeft = pixels;
			this.PaddingRight = pixels;
			this.PaddingTop = pixels;
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x0049C788 File Offset: 0x0049A988
		public virtual void RecalculateChildren()
		{
			foreach (UIElement uielement in this.Elements)
			{
				uielement.Recalculate();
			}
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x0049C7D8 File Offset: 0x0049A9D8
		public CalculatedStyle GetInnerDimensions()
		{
			return this._innerDimensions;
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x0049C7E0 File Offset: 0x0049A9E0
		public CalculatedStyle GetDimensions()
		{
			return this._dimensions;
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x0049C7E8 File Offset: 0x0049A9E8
		public CalculatedStyle GetOuterDimensions()
		{
			return this._outerDimensions;
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x0049C7F0 File Offset: 0x0049A9F0
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

		// Token: 0x060012E0 RID: 4832 RVA: 0x0049C8AB File Offset: 0x0049AAAB
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

		// Token: 0x060012E1 RID: 4833 RVA: 0x0049C8D6 File Offset: 0x0049AAD6
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

		// Token: 0x060012E2 RID: 4834 RVA: 0x0049C901 File Offset: 0x0049AB01
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

		// Token: 0x060012E3 RID: 4835 RVA: 0x0049C92C File Offset: 0x0049AB2C
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

		// Token: 0x060012E4 RID: 4836 RVA: 0x0049C957 File Offset: 0x0049AB57
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

		// Token: 0x060012E5 RID: 4837 RVA: 0x0049C982 File Offset: 0x0049AB82
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

		// Token: 0x060012E6 RID: 4838 RVA: 0x0049C9AD File Offset: 0x0049ABAD
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

		// Token: 0x060012E7 RID: 4839 RVA: 0x0049C9D8 File Offset: 0x0049ABD8
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

		// Token: 0x060012E8 RID: 4840 RVA: 0x0049CA03 File Offset: 0x0049AC03
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

		// Token: 0x060012E9 RID: 4841 RVA: 0x0049CA35 File Offset: 0x0049AC35
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

		// Token: 0x060012EA RID: 4842 RVA: 0x0049CA67 File Offset: 0x0049AC67
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

		// Token: 0x060012EB RID: 4843 RVA: 0x0049CA94 File Offset: 0x0049AC94
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

		// Token: 0x060012EC RID: 4844 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public virtual void OnActivate()
		{
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x0049CAF8 File Offset: 0x0049ACF8
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

		// Token: 0x060012EE RID: 4846 RVA: 0x0049CC58 File Offset: 0x0049AE58
		public void Deactivate()
		{
			this.OnDeactivate();
			foreach (UIElement uielement in this.Elements)
			{
				uielement.Deactivate();
			}
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public virtual void OnDeactivate()
		{
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0049CCB0 File Offset: 0x0049AEB0
		public void Initialize()
		{
			this.OnInitialize();
			this._isInitialized = true;
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public virtual void OnInitialize()
		{
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
		public virtual int CompareTo(object obj)
		{
			return 0;
		}

		// Token: 0x0400102B RID: 4139
		protected readonly List<UIElement> Elements = new List<UIElement>();

		// Token: 0x0400102D RID: 4141
		public StyleDimension Top;

		// Token: 0x0400102E RID: 4142
		public StyleDimension Left;

		// Token: 0x0400102F RID: 4143
		public StyleDimension Width;

		// Token: 0x04001030 RID: 4144
		public StyleDimension Height;

		// Token: 0x04001031 RID: 4145
		public StyleDimension MaxWidth = StyleDimension.Fill;

		// Token: 0x04001032 RID: 4146
		public StyleDimension MaxHeight = StyleDimension.Fill;

		// Token: 0x04001033 RID: 4147
		public StyleDimension MinWidth = StyleDimension.Empty;

		// Token: 0x04001034 RID: 4148
		public StyleDimension MinHeight = StyleDimension.Empty;

		// Token: 0x04001041 RID: 4161
		private bool _isInitialized;

		// Token: 0x04001042 RID: 4162
		public bool IgnoresMouseInteraction;

		// Token: 0x04001043 RID: 4163
		public bool OverflowHidden;

		// Token: 0x04001044 RID: 4164
		public SamplerState OverrideSamplerState;

		// Token: 0x04001045 RID: 4165
		public float PaddingTop;

		// Token: 0x04001046 RID: 4166
		public float PaddingLeft;

		// Token: 0x04001047 RID: 4167
		public float PaddingRight;

		// Token: 0x04001048 RID: 4168
		public float PaddingBottom;

		// Token: 0x04001049 RID: 4169
		public float MarginTop;

		// Token: 0x0400104A RID: 4170
		public float MarginLeft;

		// Token: 0x0400104B RID: 4171
		public float MarginRight;

		// Token: 0x0400104C RID: 4172
		public float MarginBottom;

		// Token: 0x0400104D RID: 4173
		public float HAlign;

		// Token: 0x0400104E RID: 4174
		public float VAlign;

		// Token: 0x0400104F RID: 4175
		private CalculatedStyle _innerDimensions;

		// Token: 0x04001050 RID: 4176
		private CalculatedStyle _dimensions;

		// Token: 0x04001051 RID: 4177
		private CalculatedStyle _outerDimensions;

		// Token: 0x04001052 RID: 4178
		private static readonly RasterizerState OverflowHiddenRasterizerState = new RasterizerState
		{
			CullMode = CullMode.None,
			ScissorTestEnable = true
		};

		// Token: 0x04001053 RID: 4179
		public bool UseImmediateMode;

		// Token: 0x04001054 RID: 4180
		private SnapPoint _snapPoint;

		// Token: 0x04001056 RID: 4182
		private static int _idCounter = 0;

		// Token: 0x02000548 RID: 1352
		// (Invoke) Token: 0x060030E8 RID: 12520
		public delegate void MouseEvent(UIMouseEvent evt, UIElement listeningElement);

		// Token: 0x02000549 RID: 1353
		// (Invoke) Token: 0x060030EC RID: 12524
		public delegate void ScrollWheelEvent(UIScrollWheelEvent evt, UIElement listeningElement);

		// Token: 0x0200054A RID: 1354
		// (Invoke) Token: 0x060030F0 RID: 12528
		public delegate void ElementEvent(UIElement affectedElement);

		// Token: 0x0200054B RID: 1355
		// (Invoke) Token: 0x060030F4 RID: 12532
		public delegate void UIElementAction(UIElement element);
	}
}
