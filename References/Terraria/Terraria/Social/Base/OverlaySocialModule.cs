using System;

namespace Terraria.Social.Base
{
	// Token: 0x0200018F RID: 399
	public abstract class OverlaySocialModule : ISocialModule
	{
		// Token: 0x06001B31 RID: 6961
		public abstract void Initialize();

		// Token: 0x06001B32 RID: 6962
		public abstract void Shutdown();

		// Token: 0x06001B33 RID: 6963
		public abstract bool IsGamepadTextInputActive();

		// Token: 0x06001B34 RID: 6964
		public abstract bool ShowGamepadTextInput(string description, uint maxLength, bool multiLine = false, string existingText = "", bool password = false);

		// Token: 0x06001B35 RID: 6965
		public abstract string GetGamepadText();
	}
}
