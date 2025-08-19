using System;
using Microsoft.Xna.Framework;

namespace Terraria.UI
{
	// Token: 0x020000B6 RID: 182
	public class UIMouseEvent : UIEvent
	{
		// Token: 0x06001617 RID: 5655 RVA: 0x004B2256 File Offset: 0x004B0456
		public UIMouseEvent(UIElement target, Vector2 mousePosition) : base(target)
		{
			this.MousePosition = mousePosition;
		}

		// Token: 0x0400115B RID: 4443
		public readonly Vector2 MousePosition;
	}
}
