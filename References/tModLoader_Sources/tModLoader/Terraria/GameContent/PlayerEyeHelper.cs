using System;

namespace Terraria.GameContent
{
	// Token: 0x020004A7 RID: 1191
	public struct PlayerEyeHelper
	{
		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06003978 RID: 14712 RVA: 0x00598377 File Offset: 0x00596577
		// (set) Token: 0x06003979 RID: 14713 RVA: 0x0059837F File Offset: 0x0059657F
		[Obsolete("Use CurrentEyeFrame instead")]
		public int EyeFrameToShow { readonly get; private set; }

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x0600397A RID: 14714 RVA: 0x00598388 File Offset: 0x00596588
		// (set) Token: 0x0600397B RID: 14715 RVA: 0x00598390 File Offset: 0x00596590
		public PlayerEyeHelper.EyeState CurrentEyeState
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x0600397C RID: 14716 RVA: 0x00598399 File Offset: 0x00596599
		// (set) Token: 0x0600397D RID: 14717 RVA: 0x005983A1 File Offset: 0x005965A1
		public PlayerEyeHelper.EyeFrame CurrentEyeFrame
		{
			get
			{
				return (PlayerEyeHelper.EyeFrame)this.EyeFrameToShow;
			}
			set
			{
				this.EyeFrameToShow = (int)value;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x0600397E RID: 14718 RVA: 0x005983AA File Offset: 0x005965AA
		// (set) Token: 0x0600397F RID: 14719 RVA: 0x005983B2 File Offset: 0x005965B2
		public int TimeInState
		{
			get
			{
				return this._timeInState;
			}
			set
			{
				this._timeInState = value;
			}
		}

		// Token: 0x06003980 RID: 14720 RVA: 0x005983BB File Offset: 0x005965BB
		public void Update(Player player)
		{
			this.SetStateByPlayerInfo(player);
			this.UpdateEyeFrameToShow(player);
			this._timeInState++;
		}

		// Token: 0x06003981 RID: 14721 RVA: 0x005983DC File Offset: 0x005965DC
		private void UpdateEyeFrameToShow(Player player)
		{
			PlayerEyeHelper.EyeFrame eyeFrameToShow = PlayerEyeHelper.EyeFrame.EyeOpen;
			switch (this._state)
			{
			case PlayerEyeHelper.EyeState.NormalBlinking:
			{
				int num = this._timeInState % 240 - 234;
				eyeFrameToShow = ((num >= 4) ? PlayerEyeHelper.EyeFrame.EyeHalfClosed : ((num < 2) ? ((num >= 0) ? PlayerEyeHelper.EyeFrame.EyeHalfClosed : PlayerEyeHelper.EyeFrame.EyeOpen) : PlayerEyeHelper.EyeFrame.EyeClosed));
				break;
			}
			case PlayerEyeHelper.EyeState.InStorm:
				eyeFrameToShow = ((this._timeInState % 120 - 114 < 0) ? PlayerEyeHelper.EyeFrame.EyeHalfClosed : PlayerEyeHelper.EyeFrame.EyeClosed);
				break;
			case PlayerEyeHelper.EyeState.InBed:
			{
				PlayerEyeHelper.EyeFrame eyeFrame = this.DoesPlayerCountAsModeratelyDamaged(player) ? PlayerEyeHelper.EyeFrame.EyeHalfClosed : PlayerEyeHelper.EyeFrame.EyeOpen;
				this._timeInState = player.sleeping.timeSleeping;
				eyeFrameToShow = ((this._timeInState >= 60) ? ((this._timeInState < 120) ? PlayerEyeHelper.EyeFrame.EyeHalfClosed : PlayerEyeHelper.EyeFrame.EyeClosed) : eyeFrame);
				break;
			}
			case PlayerEyeHelper.EyeState.JustTookDamage:
				eyeFrameToShow = PlayerEyeHelper.EyeFrame.EyeClosed;
				break;
			case PlayerEyeHelper.EyeState.IsModeratelyDamaged:
			case PlayerEyeHelper.EyeState.IsTipsy:
			case PlayerEyeHelper.EyeState.IsPoisoned:
				eyeFrameToShow = ((this._timeInState % 120 - 100 < 0) ? PlayerEyeHelper.EyeFrame.EyeHalfClosed : PlayerEyeHelper.EyeFrame.EyeClosed);
				break;
			case PlayerEyeHelper.EyeState.IsBlind:
				eyeFrameToShow = PlayerEyeHelper.EyeFrame.EyeClosed;
				break;
			}
			this.EyeFrameToShow = (int)eyeFrameToShow;
		}

		// Token: 0x06003982 RID: 14722 RVA: 0x005984C0 File Offset: 0x005966C0
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

		// Token: 0x06003983 RID: 14723 RVA: 0x00598598 File Offset: 0x00596798
		public void SwitchToState(PlayerEyeHelper.EyeState newState, bool resetStateTimerEvenIfAlreadyInState = false)
		{
			if (this._state != newState || resetStateTimerEvenIfAlreadyInState)
			{
				this._state = newState;
				this._timeInState = 0;
			}
		}

		// Token: 0x06003984 RID: 14724 RVA: 0x005985B8 File Offset: 0x005967B8
		private bool DoesPlayerCountAsModeratelyDamaged(Player player)
		{
			return (float)player.statLife <= (float)player.statLifeMax2 * 0.25f;
		}

		// Token: 0x06003985 RID: 14725 RVA: 0x005985D3 File Offset: 0x005967D3
		public void BlinkBecausePlayerGotHurt()
		{
			this.SwitchToState(PlayerEyeHelper.EyeState.JustTookDamage, true);
		}

		// Token: 0x0400525D RID: 21085
		private PlayerEyeHelper.EyeState _state;

		// Token: 0x0400525E RID: 21086
		private int _timeInState;

		// Token: 0x0400525F RID: 21087
		private const int TimeToActDamaged = 20;

		// Token: 0x02000BB0 RID: 2992
		public enum EyeFrame
		{
			// Token: 0x040076CF RID: 30415
			EyeOpen,
			// Token: 0x040076D0 RID: 30416
			EyeHalfClosed,
			// Token: 0x040076D1 RID: 30417
			EyeClosed
		}

		// Token: 0x02000BB1 RID: 2993
		public enum EyeState
		{
			// Token: 0x040076D3 RID: 30419
			NormalBlinking,
			/// <summary>
			/// <br /> Out of 120 ticks, 5 of them will have eyes closed; otherwise eyes are half closed.
			/// </summary>
			// Token: 0x040076D4 RID: 30420
			InStorm,
			/// <summary>
			/// <br /> Slowly closes eyes.
			/// <para /> If player is moderately damaged, then eyes will be half closed for first second in bed instead of open.
			/// <br /> For the next second, eyes are half closed.
			/// <br /> Afterwards, eyes are closed.
			/// </summary>
			// Token: 0x040076D5 RID: 30421
			InBed,
			/// <summary>
			/// <br /> Eyes always closed.
			/// </summary>
			// Token: 0x040076D6 RID: 30422
			JustTookDamage,
			/// <summary>
			/// <br /> Out of 120 ticks, 19 of them will have eyes closed; otherwise eyes are half closed.
			/// </summary>
			// Token: 0x040076D7 RID: 30423
			IsModeratelyDamaged,
			/// <summary>
			/// <br /> Eyes always closed.
			/// </summary>
			// Token: 0x040076D8 RID: 30424
			IsBlind,
			/// <summary>
			/// <br /> Out of 120 ticks, 19 of them will have eyes closed; otherwise eyes are half closed.
			/// </summary>
			// Token: 0x040076D9 RID: 30425
			IsTipsy,
			/// <summary>
			/// <br /> Out of 120 ticks, 19 of them will have eyes closed; otherwise eyes are half closed.
			/// </summary>
			// Token: 0x040076DA RID: 30426
			IsPoisoned
		}
	}
}
