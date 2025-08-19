using System;
using Microsoft.Xna.Framework;

namespace Terraria.UI
{
	// Token: 0x02000097 RID: 151
	public class UIMouseEvent : UIEvent
	{
		// Token: 0x060012F6 RID: 4854 RVA: 0x0049CCEE File Offset: 0x0049AEEE
		public UIMouseEvent(UIElement target, Vector2 mousePosition) : base(target)
		{
			this.MousePosition = mousePosition;
		}

		// Token: 0x04001058 RID: 4184
		public readonly Vector2 MousePosition;
	}
}
