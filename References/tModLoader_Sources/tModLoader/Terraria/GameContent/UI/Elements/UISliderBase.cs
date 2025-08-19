using System;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000531 RID: 1329
	public class UISliderBase : UIElement
	{
		// Token: 0x06003F58 RID: 16216 RVA: 0x005D8F80 File Offset: 0x005D7180
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

		// Token: 0x06003F59 RID: 16217 RVA: 0x005D8FA5 File Offset: 0x005D71A5
		public static void EscapeElements()
		{
			UISliderBase.CurrentLockedSlider = null;
			UISliderBase.CurrentAimedSlider = null;
		}

		// Token: 0x040057BD RID: 22461
		internal const int UsageLevel_NotSelected = 0;

		// Token: 0x040057BE RID: 22462
		internal const int UsageLevel_SelectedAndLocked = 1;

		// Token: 0x040057BF RID: 22463
		internal const int UsageLevel_OtherElementIsLocked = 2;

		// Token: 0x040057C0 RID: 22464
		internal static UIElement CurrentLockedSlider;

		// Token: 0x040057C1 RID: 22465
		internal static UIElement CurrentAimedSlider;
	}
}
