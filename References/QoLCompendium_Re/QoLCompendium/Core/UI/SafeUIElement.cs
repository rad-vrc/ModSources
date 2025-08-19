using System;
using Microsoft.Xna.Framework;
using Terraria.UI;

namespace QoLCompendium.Core.UI
{
	// Token: 0x0200026B RID: 619
	public class SafeUIElement : UIElement
	{
		// Token: 0x06000E61 RID: 3681 RVA: 0x0004CCBF File Offset: 0x0004AEBF
		public virtual void SafeXButton1MouseUp(UIMouseEvent evt)
		{
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x000739E7 File Offset: 0x00071BE7
		public sealed override void XButton1MouseUp(UIMouseEvent evt)
		{
			base.XButton1MouseUp(evt);
			this.SafeXButton1MouseUp(evt);
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0004CCBF File Offset: 0x0004AEBF
		public virtual void SafeXButton1MouseDown(UIMouseEvent evt)
		{
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x000739F7 File Offset: 0x00071BF7
		public sealed override void XButton1MouseDown(UIMouseEvent evt)
		{
			base.XButton1MouseDown(evt);
			this.SafeXButton1MouseDown(evt);
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x0004CCBF File Offset: 0x0004AEBF
		public virtual void SafeXButton1Click(UIMouseEvent evt)
		{
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00073A07 File Offset: 0x00071C07
		public sealed override void XButton1Click(UIMouseEvent evt)
		{
			base.XButton1Click(evt);
			this.SafeXButton1Click(evt);
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x0004CCBF File Offset: 0x0004AEBF
		public virtual void SafeXButton1DoubleClick(UIMouseEvent evt)
		{
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x00073A17 File Offset: 0x00071C17
		public sealed override void XButton1DoubleClick(UIMouseEvent evt)
		{
			base.XButton1DoubleClick(evt);
			this.SafeXButton1DoubleClick(evt);
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0004CCBF File Offset: 0x0004AEBF
		public virtual void SafeXButton2MouseUp(UIMouseEvent evt)
		{
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x00073A27 File Offset: 0x00071C27
		public sealed override void XButton2MouseUp(UIMouseEvent evt)
		{
			base.XButton2MouseUp(evt);
			this.SafeXButton2MouseUp(evt);
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x0004CCBF File Offset: 0x0004AEBF
		public virtual void SafeXButton2MouseDown(UIMouseEvent evt)
		{
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x00073A37 File Offset: 0x00071C37
		public sealed override void XButton2MouseDown(UIMouseEvent evt)
		{
			base.XButton2MouseDown(evt);
			this.SafeXButton2MouseDown(evt);
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x0004CCBF File Offset: 0x0004AEBF
		public virtual void SafeXButton2Click(UIMouseEvent evt)
		{
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x00073A47 File Offset: 0x00071C47
		public sealed override void XButton2Click(UIMouseEvent evt)
		{
			base.XButton2Click(evt);
			this.SafeXButton2Click(evt);
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0004CCBF File Offset: 0x0004AEBF
		public virtual void SafeXButton2DoubleClick(UIMouseEvent evt)
		{
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00073A57 File Offset: 0x00071C57
		public sealed override void XButton2DoubleClick(UIMouseEvent evt)
		{
			base.XButton2DoubleClick(evt);
			this.SafeXButton2DoubleClick(evt);
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x0004CCBF File Offset: 0x0004AEBF
		public virtual void SafeLeftMouseUp(UIMouseEvent evt)
		{
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x00073A67 File Offset: 0x00071C67
		public override void LeftMouseUp(UIMouseEvent evt)
		{
			base.LeftMouseUp(evt);
			this.SafeLeftMouseUp(evt);
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x0004CCBF File Offset: 0x0004AEBF
		public virtual void SafeLeftMouseDown(UIMouseEvent evt)
		{
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x00073A77 File Offset: 0x00071C77
		public sealed override void LeftMouseDown(UIMouseEvent evt)
		{
			base.LeftMouseDown(evt);
			this.SafeLeftMouseDown(evt);
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0004CCBF File Offset: 0x0004AEBF
		public virtual void SafeLeftClick(UIMouseEvent evt)
		{
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x00073A87 File Offset: 0x00071C87
		public sealed override void LeftClick(UIMouseEvent evt)
		{
			base.LeftClick(evt);
			this.SafeLeftClick(evt);
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0004CCBF File Offset: 0x0004AEBF
		public virtual void SafeLeftDoubleClick(UIMouseEvent evt)
		{
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x00073A97 File Offset: 0x00071C97
		public sealed override void LeftDoubleClick(UIMouseEvent evt)
		{
			base.LeftDoubleClick(evt);
			this.SafeLeftDoubleClick(evt);
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x0004CCBF File Offset: 0x0004AEBF
		public virtual void SafeRightMouseUp(UIMouseEvent evt)
		{
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00073AA7 File Offset: 0x00071CA7
		public sealed override void RightMouseUp(UIMouseEvent evt)
		{
			base.RightMouseUp(evt);
			this.SafeRightMouseUp(evt);
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0004CCBF File Offset: 0x0004AEBF
		public virtual void SafeRightMouseDown(UIMouseEvent evt)
		{
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x00073AB7 File Offset: 0x00071CB7
		public sealed override void RightMouseDown(UIMouseEvent evt)
		{
			base.RightMouseDown(evt);
			this.SafeRightMouseDown(evt);
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0004CCBF File Offset: 0x0004AEBF
		public virtual void SafeRightClick(UIMouseEvent evt)
		{
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00073AC7 File Offset: 0x00071CC7
		public sealed override void RightClick(UIMouseEvent evt)
		{
			base.RightClick(evt);
			this.SafeRightClick(evt);
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0004CCBF File Offset: 0x0004AEBF
		public virtual void SafeRightDoubleClick(UIMouseEvent evt)
		{
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00073AD7 File Offset: 0x00071CD7
		public sealed override void RightDoubleClick(UIMouseEvent evt)
		{
			base.RightDoubleClick(evt);
			this.SafeRightDoubleClick(evt);
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0004CCBF File Offset: 0x0004AEBF
		public virtual void SafeMiddleMouseUp(UIMouseEvent evt)
		{
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x00073AE7 File Offset: 0x00071CE7
		public sealed override void MiddleMouseUp(UIMouseEvent evt)
		{
			base.MiddleMouseUp(evt);
			this.SafeMiddleMouseUp(evt);
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x0004CCBF File Offset: 0x0004AEBF
		public virtual void SafeMiddleMouseDown(UIMouseEvent evt)
		{
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x00073AF7 File Offset: 0x00071CF7
		public sealed override void MiddleMouseDown(UIMouseEvent evt)
		{
			base.MiddleMouseDown(evt);
			this.SafeMiddleMouseDown(evt);
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0004CCBF File Offset: 0x0004AEBF
		public virtual void SafeMiddleClick(UIMouseEvent evt)
		{
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x00073B07 File Offset: 0x00071D07
		public sealed override void MiddleClick(UIMouseEvent evt)
		{
			base.MiddleClick(evt);
			this.SafeMiddleClick(evt);
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0004CCBF File Offset: 0x0004AEBF
		public virtual void SafeMiddleDoubleClick(UIMouseEvent evt)
		{
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x00073B17 File Offset: 0x00071D17
		public sealed override void MiddleDoubleClick(UIMouseEvent evt)
		{
			base.MiddleDoubleClick(evt);
			this.SafeMiddleDoubleClick(evt);
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0004CCBF File Offset: 0x0004AEBF
		public virtual void SafeMouseOver(UIMouseEvent evt)
		{
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x00073B27 File Offset: 0x00071D27
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this.SafeMouseOver(evt);
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0004CCBF File Offset: 0x0004AEBF
		public virtual void SafeUpdate(GameTime gameTime)
		{
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00073B37 File Offset: 0x00071D37
		public sealed override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			this.SafeUpdate(gameTime);
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0004CCBF File Offset: 0x0004AEBF
		public virtual void SafeScrollWheel(UIScrollWheelEvent evt)
		{
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x00073B47 File Offset: 0x00071D47
		public sealed override void ScrollWheel(UIScrollWheelEvent evt)
		{
			base.ScrollWheel(evt);
			this.SafeScrollWheel(evt);
		}
	}
}
