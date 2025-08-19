using System;

namespace Terraria.Graphics
{
	// Token: 0x02000443 RID: 1091
	public class WindowStateController
	{
		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060035EF RID: 13807 RVA: 0x00579F4A File Offset: 0x0057814A
		public bool CanMoveWindowAcrossScreens
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060035F0 RID: 13808 RVA: 0x00579F4D File Offset: 0x0057814D
		public string ScreenDeviceName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x060035F1 RID: 13809 RVA: 0x00579F54 File Offset: 0x00578154
		public void TryMovingToScreen(string screenDeviceName)
		{
		}
	}
}
