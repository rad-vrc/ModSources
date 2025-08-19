using System;
using Steamworks;
using Terraria.Social.Base;

namespace Terraria.Social.Steam
{
	// Token: 0x020000EC RID: 236
	public class OverlaySocialModule : OverlaySocialModule
	{
		// Token: 0x06001823 RID: 6179 RVA: 0x004BAA0E File Offset: 0x004B8C0E
		public override void Initialize()
		{
			this._gamepadTextInputDismissed = Callback<GamepadTextInputDismissed_t>.Create(new Callback<GamepadTextInputDismissed_t>.DispatchDelegate(this.OnGamepadTextInputDismissed));
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x004BAA27 File Offset: 0x004B8C27
		public override void Shutdown()
		{
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x004BAA29 File Offset: 0x004B8C29
		public override bool IsGamepadTextInputActive()
		{
			return this._gamepadTextInputActive;
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x004BAA31 File Offset: 0x004B8C31
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

		// Token: 0x06001827 RID: 6183 RVA: 0x004BAA60 File Offset: 0x004B8C60
		public override string GetGamepadText()
		{
			uint enteredGamepadTextLength = SteamUtils.GetEnteredGamepadTextLength();
			string pchText;
			SteamUtils.GetEnteredGamepadTextInput(ref pchText, enteredGamepadTextLength);
			return pchText;
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x004BAA7D File Offset: 0x004B8C7D
		private void OnGamepadTextInputDismissed(GamepadTextInputDismissed_t result)
		{
			this._gamepadTextInputActive = false;
		}

		// Token: 0x04001356 RID: 4950
		private Callback<GamepadTextInputDismissed_t> _gamepadTextInputDismissed;

		// Token: 0x04001357 RID: 4951
		private bool _gamepadTextInputActive;
	}
}
