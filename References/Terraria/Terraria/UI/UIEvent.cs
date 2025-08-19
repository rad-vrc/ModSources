using System;

namespace Terraria.UI
{
	// Token: 0x02000096 RID: 150
	public class UIEvent
	{
		// Token: 0x060012F5 RID: 4853 RVA: 0x0049CCDF File Offset: 0x0049AEDF
		public UIEvent(UIElement target)
		{
			this.Target = target;
		}

		// Token: 0x04001057 RID: 4183
		public readonly UIElement Target;
	}
}
