using System;
using Steamworks;
using Terraria.GameInput;
using Terraria.Social.Base;
using Terraria.UI.Gamepad;

namespace Terraria.Social.Steam
{
	// Token: 0x0200016D RID: 365
	public class PlatformSocialModule : PlatformSocialModule
	{
		// Token: 0x06001A43 RID: 6723 RVA: 0x004E3FE0 File Offset: 0x004E21E0
		public override void Initialize()
		{
			if (Main.dedServ)
			{
				return;
			}
			bool flag = PlayerInput.UseSteamDeckIfPossible = SteamUtils.IsSteamRunningOnSteamDeck();
			if (flag)
			{
				PlayerInput.SettingsForUI.SetCursorMode(CursorMode.Gamepad);
				PlayerInput.CurrentInputMode = InputMode.XBoxGamepadUI;
				GamepadMainMenuHandler.MoveCursorOnNextRun = true;
				PlayerInput.PreventFirstMousePositionGrab = true;
			}
			if (flag)
			{
				Main.graphics.PreferredBackBufferWidth = (Main.screenWidth = 1280);
				Main.graphics.PreferredBackBufferHeight = (Main.screenHeight = 800);
				Main.startFullscreen = true;
				Main.toggleFullscreen = true;
				Main.screenBorderless = false;
				Main.screenMaximized = false;
				Main.InitialMapScale = (Main.MapScale = 0.73f);
				Main.UIScale = 1.07f;
			}
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void Shutdown()
		{
		}
	}
}
