using System;
using Terraria.GameContent.UI.States;

namespace Terraria.Social.Base
{
	// Token: 0x0200017F RID: 383
	public class RichPresenceState : IEquatable<RichPresenceState>
	{
		// Token: 0x06001ADC RID: 6876 RVA: 0x004E65EC File Offset: 0x004E47EC
		public bool Equals(RichPresenceState other)
		{
			return this.GameMode == other.GameMode;
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x004E6600 File Offset: 0x004E4800
		public static RichPresenceState GetCurrentState()
		{
			RichPresenceState richPresenceState = new RichPresenceState();
			if (Main.gameMenu)
			{
				bool flag = Main.MenuUI.CurrentState is UICharacterCreation;
				bool flag2 = Main.MenuUI.CurrentState is UIWorldCreation;
				if (flag)
				{
					richPresenceState.GameMode = RichPresenceState.GameModeState.CreatingPlayer;
				}
				else if (flag2)
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

		// Token: 0x040015DA RID: 5594
		public RichPresenceState.GameModeState GameMode;

		// Token: 0x020005C2 RID: 1474
		public enum GameModeState
		{
			// Token: 0x04005A96 RID: 23190
			InMainMenu,
			// Token: 0x04005A97 RID: 23191
			CreatingPlayer,
			// Token: 0x04005A98 RID: 23192
			CreatingWorld,
			// Token: 0x04005A99 RID: 23193
			PlayingSingle,
			// Token: 0x04005A9A RID: 23194
			PlayingMulti
		}
	}
}
