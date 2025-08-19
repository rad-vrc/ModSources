using System;
using Terraria.Audio;
using Terraria.ID;

namespace Terraria.UI
{
	// Token: 0x020000A3 RID: 163
	public interface IHaveBackButtonCommand
	{
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060014ED RID: 5357
		// (set) Token: 0x060014EE RID: 5358
		UIState PreviousUIState { get; set; }

		// Token: 0x060014EF RID: 5359 RVA: 0x004A6B7B File Offset: 0x004A4D7B
		void HandleBackButtonUsage()
		{
			IHaveBackButtonCommand.GoBackTo(this.PreviousUIState);
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x004A6B88 File Offset: 0x004A4D88
		public static void GoBackTo(UIState state)
		{
			if (state == null)
			{
				Main.menuMode = 0;
			}
			else
			{
				Main.menuMode = 888;
				Main.MenuUI.SetState(state);
			}
			SoundEngine.PlaySound(SoundID.MenuClose, null, null);
		}
	}
}
