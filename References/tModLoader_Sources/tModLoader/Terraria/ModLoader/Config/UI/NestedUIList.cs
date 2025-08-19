using System;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003AC RID: 940
	internal class NestedUIList : UIList
	{
		// Token: 0x06003266 RID: 12902 RVA: 0x005437A0 File Offset: 0x005419A0
		public override void ScrollWheel(UIScrollWheelEvent evt)
		{
			if (this._scrollbar != null)
			{
				float viewPosition = this._scrollbar.ViewPosition;
				this._scrollbar.ViewPosition -= (float)evt.ScrollWheelValue;
				if (viewPosition == this._scrollbar.ViewPosition)
				{
					base.ScrollWheel(evt);
					return;
				}
			}
			else
			{
				base.ScrollWheel(evt);
			}
		}
	}
}
