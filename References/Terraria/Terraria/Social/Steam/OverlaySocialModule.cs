using System;
using Steamworks;
using Terraria.Social.Base;

namespace Terraria.Social.Steam
{
	// Token: 0x02000172 RID: 370
	public class OverlaySocialModule : OverlaySocialModule
	{
		// Token: 0x06001A5F RID: 6751 RVA: 0x004E47D0 File Offset: 0x004E29D0
		public override void Initialize()
		{
			this._gamepadTextInputDismissed = Callback<GamepadTextInputDismissed_t>.Create(new Callback<GamepadTextInputDismissed_t>.DispatchDelegate(this.OnGamepadTextInputDismissed));
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void Shutdown()
		{
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x004E47E9 File Offset: 0x004E29E9
		public override bool IsGamepadTextInputActive()
		{
			return this._gamepadTextInputActive;
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x004E47F1 File Offset: 0x004E29F1
		public override bool ShowGamepadTextInput(string description, uint maxLength, bool multiLine = false, string existingText = "", bool password = false)
		{
			if (this._gamepadTextInputActive)
			{
				return false;
			}
			bool flag = SteamUtils.ShowGamepadTextInput(password ? 1 : 0, multiLine ? 1 : 0, description, maxLength, existingText);
			if (flag)
			{
				this._gamepadTextInputActive = true;
			}
			return flag;
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x004E4820 File Offset: 0x004E2A20
		public override string GetGamepadText()
		{
			uint enteredGamepadTextLength = SteamUtils.GetEnteredGamepadTextLength();
			string result;
			SteamUtils.GetEnteredGamepadTextInput(ref result, enteredGamepadTextLength);
			return result;
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x004E483D File Offset: 0x004E2A3D
		private void OnGamepadTextInputDismissed(GamepadTextInputDismissed_t result)
		{
			this._gamepadTextInputActive = false;
		}

		// Token: 0x04001595 RID: 5525
		private Callback<GamepadTextInputDismissed_t> _gamepadTextInputDismissed;

		// Token: 0x04001596 RID: 5526
		private bool _gamepadTextInputActive;
	}
}
