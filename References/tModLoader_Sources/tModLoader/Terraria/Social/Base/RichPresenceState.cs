using System;
using Terraria.GameContent.UI.States;

namespace Terraria.Social.Base
{
	// Token: 0x02000101 RID: 257
	public class RichPresenceState : IEquatable<RichPresenceState>
	{
		// Token: 0x060018E8 RID: 6376 RVA: 0x004BE3D0 File Offset: 0x004BC5D0
		public bool Equals(RichPresenceState other)
		{
			return this.GameMode == other.GameMode;
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x004BE3E4 File Offset: 0x004BC5E4
		public static RichPresenceState GetCurrentState()
		{
			RichPresenceState richPresenceState = new RichPresenceState();
			if (Main.gameMenu)
			{
				bool flag2 = Main.MenuUI.CurrentState is UICharacterCreation;
				bool flag = Main.MenuUI.CurrentState is UIWorldCreation;
				if (flag2)
				{
					richPresenceState.GameMode = RichPresenceState.GameModeState.CreatingPlayer;
				}
				else if (flag)
				{
					richPresenceState.GameMode = RichPresenceState.GameModeState.CreatingWorld;
				}
				else
				{
					richPresenceState.GameMode = RichPresenceState.GameModeState.InMainMenu;
				}
			}
			else if (Main.netMode == 0)
			{
				richPresenceState.GameMode = RichPresenceState.GameModeState.PlayingSingle;
			}
			else
			{
				richPresenceState.GameMode = RichPresenceState.GameModeState.PlayingMulti;
			}
			return richPresenceState;
		}

		// Token: 0x0400138C RID: 5004
		public RichPresenceState.GameModeState GameMode;

		// Token: 0x02000897 RID: 2199
		public enum GameModeState
		{
			// Token: 0x04006A14 RID: 27156
			InMainMenu,
			// Token: 0x04006A15 RID: 27157
			CreatingPlayer,
			// Token: 0x04006A16 RID: 27158
			CreatingWorld,
			// Token: 0x04006A17 RID: 27159
			PlayingSingle,
			// Token: 0x04006A18 RID: 27160
			PlayingMulti
		}
	}
}
