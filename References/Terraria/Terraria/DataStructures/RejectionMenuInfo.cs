using System;
using Terraria.Audio;

namespace Terraria.DataStructures
{
	// Token: 0x0200043D RID: 1085
	public class RejectionMenuInfo
	{
		// Token: 0x06002B9F RID: 11167 RVA: 0x0059E87B File Offset: 0x0059CA7B
		public void DefaultExitAction()
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.menuMode = 0;
			Main.netMode = 0;
		}

		// Token: 0x04004FC7 RID: 20423
		public ReturnFromRejectionMenuAction ExitAction;

		// Token: 0x04004FC8 RID: 20424
		public string TextToShow;
	}
}
