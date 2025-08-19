using System;

namespace Terraria.GameContent
{
	// Token: 0x020001E6 RID: 486
	public struct PlayerEyeHelper
	{
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06001C74 RID: 7284 RVA: 0x004F4254 File Offset: 0x004F2454
		// (set) Token: 0x06001C75 RID: 7285 RVA: 0x004F425C File Offset: 0x004F245C
		public int EyeFrameToShow { get; private set; }

		// Token: 0x06001C76 RID: 7286 RVA: 0x004F4265 File Offset: 0x004F2465
		public void Update(Player player)
		{
			this.SetStateByPlayerInfo(player);
			this.UpdateEyeFrameToShow(player);
			this._timeInState++;
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x004F4284 File Offset: 0x004F2484
		private void UpdateEyeFrameToShow(Player player)
		{
			PlayerEyeHelper.EyeFrame eyeFrameToShow = PlayerEyeHelper.EyeFrame.EyeOpen;
			switch (this._state)
			{
			case PlayerEyeHelper.EyeState.NormalBlinking:
			{
				int num = this._timeInState % 240 - 234;
				if (num >= 4)
				{
					eyeFrameToShow = PlayerEyeHelper.EyeFrame.EyeHalfClosed;
				}
				else if (num >= 2)
				{
					eyeFrameToShow = PlayerEyeHelper.EyeFrame.EyeClosed;
				}
				else if (num >= 0)
				{
					eyeFrameToShow = PlayerEyeHelper.EyeFrame.EyeHalfClosed;
				}
				else
				{
					eyeFrameToShow = PlayerEyeHelper.EyeFrame.EyeOpen;
				}
				break;
			}
			case PlayerEyeHelper.EyeState.InStorm:
				if (this._timeInState % 120 - 114 >= 0)
				{
					eyeFrameToShow = PlayerEyeHelper.EyeFrame.EyeClosed;
				}
				else
				{
					eyeFrameToShow = PlayerEyeHelper.EyeFrame.EyeHalfClosed;
				}
				break;
			case PlayerEyeHelper.EyeState.InBed:
			{
				PlayerEyeHelper.EyeFrame eyeFrame = this.DoesPlayerCountAsModeratelyDamaged(player) ? PlayerEyeHelper.EyeFrame.EyeHalfClosed : PlayerEyeHelper.EyeFrame.EyeOpen;
				this._timeInState = player.sleeping.timeSleeping;
				if (this._timeInState < 60)
				{
					eyeFrameToShow = eyeFrame;
				}
				else if (this._timeInState < 120)
				{
					eyeFrameToShow = PlayerEyeHelper.EyeFrame.EyeHalfClosed;
				}
				else
				{
					eyeFrameToShow = PlayerEyeHelper.EyeFrame.EyeClosed;
				}
				break;
			}
			case PlayerEyeHelper.EyeState.JustTookDamage:
				eyeFrameToShow = PlayerEyeHelper.EyeFrame.EyeClosed;
				break;
			case PlayerEyeHelper.EyeState.IsModeratelyDamaged:
			case PlayerEyeHelper.EyeState.IsTipsy:
			case PlayerEyeHelper.EyeState.IsPoisoned:
				if (this._timeInState % 120 - 100 >= 0)
				{
					eyeFrameToShow = PlayerEyeHelper.EyeFrame.EyeClosed;
				}
				else
				{
					eyeFrameToShow = PlayerEyeHelper.EyeFrame.EyeHalfClosed;
				}
				break;
			case PlayerEyeHelper.EyeState.IsBlind:
				eyeFrameToShow = PlayerEyeHelper.EyeFrame.EyeClosed;
				break;
			}
			this.EyeFrameToShow = (int)eyeFrameToShow;
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x004F4374 File Offset: 0x004F2574
		private void SetStateByPlayerInfo(Player player)
		{
			if (player.blackout || player.blind)
			{
				this.SwitchToState(PlayerEyeHelper.EyeState.IsBlind, false);
				return;
			}
			if (this._state == PlayerEyeHelper.EyeState.JustTookDamage && this._timeInState < 20)
			{
				return;
			}
			if (player.sleeping.isSleeping)
			{
				bool resetStateTimerEvenIfAlreadyInState = player.itemAnimation > 0;
				this.SwitchToState(PlayerEyeHelper.EyeState.InBed, resetStateTimerEvenIfAlreadyInState);
				return;
			}
			if (this.DoesPlayerCountAsModeratelyDamaged(player))
			{
				this.SwitchToState(PlayerEyeHelper.EyeState.IsModeratelyDamaged, false);
				return;
			}
			if (player.tipsy)
			{
				this.SwitchToState(PlayerEyeHelper.EyeState.IsTipsy, false);
				return;
			}
			if (player.poisoned || player.venom || player.starving)
			{
				this.SwitchToState(PlayerEyeHelper.EyeState.IsPoisoned, false);
				return;
			}
			bool flag = player.ZoneSandstorm || (player.ZoneSnow && Main.IsItRaining);
			if (player.behindBackWall)
			{
				flag = false;
			}
			if (flag)
			{
				this.SwitchToState(PlayerEyeHelper.EyeState.InStorm, false);
				return;
			}
			this.SwitchToState(PlayerEyeHelper.EyeState.NormalBlinking, false);
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x004F444C File Offset: 0x004F264C
		private void SwitchToState(PlayerEyeHelper.EyeState newState, bool resetStateTimerEvenIfAlreadyInState = false)
		{
			if (this._state == newState && !resetStateTimerEvenIfAlreadyInState)
			{
				return;
			}
			this._state = newState;
			this._timeInState = 0;
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x004F4469 File Offset: 0x004F2669
		private bool DoesPlayerCountAsModeratelyDamaged(Player player)
		{
			return (float)player.statLife <= (float)player.statLifeMax2 * 0.25f;
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x004F4484 File Offset: 0x004F2684
		public void BlinkBecausePlayerGotHurt()
		{
			this.SwitchToState(PlayerEyeHelper.EyeState.JustTookDamage, true);
		}

		// Token: 0x040043A8 RID: 17320
		private PlayerEyeHelper.EyeState _state;

		// Token: 0x040043A9 RID: 17321
		private int _timeInState;

		// Token: 0x040043AB RID: 17323
		private const int TimeToActDamaged = 20;

		// Token: 0x02000605 RID: 1541
		private enum EyeFrame
		{
			// Token: 0x04006031 RID: 24625
			EyeOpen,
			// Token: 0x04006032 RID: 24626
			EyeHalfClosed,
			// Token: 0x04006033 RID: 24627
			EyeClosed
		}

		// Token: 0x02000606 RID: 1542
		private enum EyeState
		{
			// Token: 0x04006035 RID: 24629
			NormalBlinking,
			// Token: 0x04006036 RID: 24630
			InStorm,
			// Token: 0x04006037 RID: 24631
			InBed,
			// Token: 0x04006038 RID: 24632
			JustTookDamage,
			// Token: 0x04006039 RID: 24633
			IsModeratelyDamaged,
			// Token: 0x0400603A RID: 24634
			IsBlind,
			// Token: 0x0400603B RID: 24635
			IsTipsy,
			// Token: 0x0400603C RID: 24636
			IsPoisoned
		}
	}
}
