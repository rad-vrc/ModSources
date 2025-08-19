using System;
using Microsoft.Xna.Framework;

namespace Terraria.UI
{
	// Token: 0x020000B7 RID: 183
	public class UIScrollWheelEvent : UIMouseEvent
	{
		// Token: 0x06001618 RID: 5656 RVA: 0x004B2266 File Offset: 0x004B0466
		public UIScrollWheelEvent(UIElement target, Vector2 mousePosition, int scrollWheelValue) : base(target, mousePosition)
		{
			this.ScrollWheelValue = scrollWheelValue;
		}

		// Token: 0x0400115C RID: 4444
		public readonly int ScrollWheelValue;
	}
}
