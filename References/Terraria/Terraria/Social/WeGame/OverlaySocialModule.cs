using System;
using Terraria.Social.Base;

namespace Terraria.Social.WeGame
{
	// Token: 0x0200015C RID: 348
	public class OverlaySocialModule : OverlaySocialModule
	{
		// Token: 0x060019D2 RID: 6610 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void Initialize()
		{
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void Shutdown()
		{
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x004E298A File Offset: 0x004E0B8A
		public override bool IsGamepadTextInputActive()
		{
			return this._gamepadTextInputActive;
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
		public override bool ShowGamepadTextInput(string description, uint maxLength, bool multiLine = false, string existingText = "", bool password = false)
		{
			return false;
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x004E2992 File Offset: 0x004E0B92
		public override string GetGamepadText()
		{
			return "";
		}

		// Token: 0x04001560 RID: 5472
		private bool _gamepadTextInputActive;
	}
}
