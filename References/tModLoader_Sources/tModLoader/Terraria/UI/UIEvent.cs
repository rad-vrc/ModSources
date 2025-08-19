using System;

namespace Terraria.UI
{
	// Token: 0x020000B5 RID: 181
	public class UIEvent
	{
		// Token: 0x06001616 RID: 5654 RVA: 0x004B2247 File Offset: 0x004B0447
		public UIEvent(UIElement target)
		{
			this.Target = target;
		}

		/// <summary>
		/// The element targeted by the mouse event. This is usually the element lowest in the UI hierarchy overlapping with mouse position unless <see cref="F:Terraria.UI.UIElement.IgnoresMouseInteraction" /> is true.
		/// </summary>
		// Token: 0x0400115A RID: 4442
		public readonly UIElement Target;
	}
}
