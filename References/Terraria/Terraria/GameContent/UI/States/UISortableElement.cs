using System;
using Terraria.UI;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x0200034E RID: 846
	public class UISortableElement : UIElement
	{
		// Token: 0x060026E2 RID: 9954 RVA: 0x0057B7B0 File Offset: 0x005799B0
		public UISortableElement(int index)
		{
			this.OrderIndex = index;
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x0057B7C0 File Offset: 0x005799C0
		public override int CompareTo(object obj)
		{
			UISortableElement uisortableElement = obj as UISortableElement;
			if (uisortableElement != null)
			{
				return this.OrderIndex.CompareTo(uisortableElement.OrderIndex);
			}
			return base.CompareTo(obj);
		}

		// Token: 0x04004A71 RID: 19057
		public int OrderIndex;
	}
}
