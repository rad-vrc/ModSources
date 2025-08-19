using System;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200037C RID: 892
	public class UISliderBase : UIElement
	{
		// Token: 0x0600289D RID: 10397 RVA: 0x0058BDD8 File Offset: 0x00589FD8
		internal int GetUsageLevel()
		{
			int result = 0;
			if (UISliderBase.CurrentLockedSlider == this)
			{
				result = 1;
			}
			else if (UISliderBase.CurrentLockedSlider != null)
			{
				result = 2;
			}
			return result;
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x0058BDFD File Offset: 0x00589FFD
		public static void EscapeElements()
		{
			UISliderBase.CurrentLockedSlider = null;
			UISliderBase.CurrentAimedSlider = null;
		}

		// Token: 0x04004BD6 RID: 19414
		internal const int UsageLevel_NotSelected = 0;

		// Token: 0x04004BD7 RID: 19415
		internal const int UsageLevel_SelectedAndLocked = 1;

		// Token: 0x04004BD8 RID: 19416
		internal const int UsageLevel_OtherElementIsLocked = 2;

		// Token: 0x04004BD9 RID: 19417
		internal static UIElement CurrentLockedSlider;

		// Token: 0x04004BDA RID: 19418
		internal static UIElement CurrentAimedSlider;
	}
}
