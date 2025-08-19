using System;
using Microsoft.Xna.Framework;

namespace Terraria.UI
{
	// Token: 0x02000098 RID: 152
	public class UIScrollWheelEvent : UIMouseEvent
	{
		// Token: 0x060012F7 RID: 4855 RVA: 0x0049CCFE File Offset: 0x0049AEFE
		public UIScrollWheelEvent(UIElement target, Vector2 mousePosition, int scrollWheelValue) : base(target, mousePosition)
		{
			this.ScrollWheelValue = scrollWheelValue;
		}

		// Token: 0x04001059 RID: 4185
		public readonly int ScrollWheelValue;
	}
}
