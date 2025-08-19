using System;
using Terraria.UI;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020004E0 RID: 1248
	public class UISortableElement : UIElement
	{
		// Token: 0x06003C3A RID: 15418 RVA: 0x005BF5BE File Offset: 0x005BD7BE
		public UISortableElement(int index)
		{
			this.OrderIndex = index;
		}

		// Token: 0x06003C3B RID: 15419 RVA: 0x005BF5D0 File Offset: 0x005BD7D0
		public override int CompareTo(object obj)
		{
			UISortableElement uISortableElement = obj as UISortableElement;
			if (uISortableElement != null)
			{
				return this.OrderIndex.CompareTo(uISortableElement.OrderIndex);
			}
			return base.CompareTo(obj);
		}

		// Token: 0x04005586 RID: 21894
		public int OrderIndex;
	}
}
