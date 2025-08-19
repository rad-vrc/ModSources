using System;
using Terraria.Social.Base;

namespace Terraria.Social.WeGame
{
	// Token: 0x020000DD RID: 221
	public class OverlaySocialModule : OverlaySocialModule
	{
		// Token: 0x0600179B RID: 6043 RVA: 0x004B89C2 File Offset: 0x004B6BC2
		public override void Initialize()
		{
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x004B89C4 File Offset: 0x004B6BC4
		public override void Shutdown()
		{
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x004B89C6 File Offset: 0x004B6BC6
		public override bool IsGamepadTextInputActive()
		{
			return this._gamepadTextInputActive;
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x004B89CE File Offset: 0x004B6BCE
		public override bool ShowGamepadTextInput(string description, uint maxLength, bool multiLine = false, string existingText = "", bool password = false)
		{
			return false;
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x004B89D1 File Offset: 0x004B6BD1
		public override string GetGamepadText()
		{
			return "";
		}

		// Token: 0x04001311 RID: 4881
		private bool _gamepadTextInputActive;
	}
}
