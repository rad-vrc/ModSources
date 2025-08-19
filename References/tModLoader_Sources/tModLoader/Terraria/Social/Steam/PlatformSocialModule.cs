using System;
using Steamworks;
using Terraria.GameInput;
using Terraria.Social.Base;
using Terraria.UI.Gamepad;

namespace Terraria.Social.Steam
{
	// Token: 0x020000ED RID: 237
	public class PlatformSocialModule : PlatformSocialModule
	{
		// Token: 0x0600182A RID: 6186 RVA: 0x004BAA90 File Offset: 0x004B8C90
		public override void Initialize()
		{
			if (!Main.dedServ)
			{
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
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x004BAB2B File Offset: 0x004B8D2B
		public override void Shutdown()
		{
		}
	}
}
