using System;
using Terraria.Audio;

namespace Terraria.DataStructures
{
	// Token: 0x0200072D RID: 1837
	public class RejectionMenuInfo
	{
		// Token: 0x06004AAE RID: 19118 RVA: 0x00668170 File Offset: 0x00666370
		public void DefaultExitAction()
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.menuMode = 0;
			Main.netMode = 0;
		}

		// Token: 0x04005FF9 RID: 24569
		public ReturnFromRejectionMenuAction ExitAction;

		// Token: 0x04005FFA RID: 24570
		public string TextToShow;
	}
}
