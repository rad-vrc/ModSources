using System;

namespace Terraria.Social.Base
{
	// Token: 0x020000FF RID: 255
	public abstract class OverlaySocialModule : ISocialModule
	{
		// Token: 0x060018DF RID: 6367
		public abstract void Initialize();

		// Token: 0x060018E0 RID: 6368
		public abstract void Shutdown();

		// Token: 0x060018E1 RID: 6369
		public abstract bool IsGamepadTextInputActive();

		// Token: 0x060018E2 RID: 6370
		public abstract bool ShowGamepadTextInput(string description, uint maxLength, bool multiLine = false, string existingText = "", bool password = false);

		// Token: 0x060018E3 RID: 6371
		public abstract string GetGamepadText();
	}
}
