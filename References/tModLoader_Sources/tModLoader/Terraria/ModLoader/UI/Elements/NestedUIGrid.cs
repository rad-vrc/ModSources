using System;
using Terraria.UI;

namespace Terraria.ModLoader.UI.Elements
{
	// Token: 0x02000277 RID: 631
	internal class NestedUIGrid : UIGrid
	{
		// Token: 0x06002B6B RID: 11115 RVA: 0x005224A0 File Offset: 0x005206A0
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
