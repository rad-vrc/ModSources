using System;

namespace Terraria.DataStructures
{
	/// <summary>
	/// A structure containing fields used to manage extra jumps<br /><br />
	///
	/// Valid states for an extra jump are as follows:
	/// <list type="bullet">
	/// <item>Enabled = <see langword="false" /> | The extra jump cannot be used.  Available and Active will be <see langword="false" /></item>
	/// <item>Enabled = <see langword="true" />, Available = <see langword="true" />,  Active = <see langword="false" /> | The extra jump is ready to be consumed, but hasn't been consumed yet</item>
	/// <item>Enabled = <see langword="true" />, Available = <see langword="false" />, Active = <see langword="true" />  | The extra jump has been consumed and is currently in progress</item>
	/// <item>Enabled = <see langword="true" />, Available = <see langword="true" />,  Active = <see langword="true" />  | The extra jump is currently in progress, but can be re-used again after it ends</item>
	/// <item>Enabled = <see langword="true" />, Available = <see langword="false" />, Active = <see langword="false" /> | The extra jump has been consumed and cannot be used again until extra jumps are refreshed</item>
	/// </list>
	/// </summary>
	// Token: 0x0200070A RID: 1802
	public struct ExtraJumpState
	{
		/// <summary>
		/// Whether the extra jump can be used. This property is set by <see cref="M:Terraria.DataStructures.ExtraJumpState.Enable" /> and <see cref="M:Terraria.DataStructures.ExtraJumpState.Disable" />.<br />
		/// This property is automatically set to <see langword="false" /> in ResetEffects.<br />
		/// When <see langword="false" />, <see cref="P:Terraria.DataStructures.ExtraJumpState.Available" /> and <see cref="P:Terraria.DataStructures.ExtraJumpState.Active" /> will also be <see langword="false" />.<br />
		/// </summary>
		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x060049A0 RID: 18848 RVA: 0x0064DD28 File Offset: 0x0064BF28
		public bool Enabled
		{
			get
			{
				return this._enabled && !this._disabled;
			}
		}

		/// <summary>
		/// <see langword="true" /> if the extra jump has not been consumed. Will be set to <see langword="false" /> when the extra jump starts.<br />
		/// Setting this field to <see langword="false" /> will effectively make the game think that the player has already used this extra jump.<br />
		/// This property also checks <see cref="P:Terraria.DataStructures.ExtraJumpState.Enabled" /> when read.<br />
		/// For a reusable jump (e.g. MultipleUseExtraJump from ExampleMod), this property should only be set to <see langword="true" /> in <see cref="M:Terraria.ModLoader.ExtraJump.OnStarted(Terraria.Player,System.Boolean@)" />.
		/// </summary>
		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x060049A1 RID: 18849 RVA: 0x0064DD3D File Offset: 0x0064BF3D
		// (set) Token: 0x060049A2 RID: 18850 RVA: 0x0064DD4F File Offset: 0x0064BF4F
		public bool Available
		{
			get
			{
				return this.Enabled && this._available;
			}
			set
			{
				this._available = value;
			}
		}

		/// <summary>
		/// Whether any effects (e.g. spawning dusts) should be performed after consuming the extra jump, but before its duration runs out.<br />
		/// This property returns <see langword="true" /> while the extra jump is in progress, and returns <see langword="false" /> otherwise.<br />
		/// While an extra jump is in progress, <see cref="M:Terraria.ModLoader.ExtraJump.UpdateHorizontalSpeeds(Terraria.Player)" /> and <see cref="M:Terraria.ModLoader.ExtraJump.ShowVisuals(Terraria.Player)" /> will be executed.<br />
		/// This property also checks <see cref="P:Terraria.DataStructures.ExtraJumpState.Enabled" /> when read.
		/// </summary>
		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x060049A3 RID: 18851 RVA: 0x0064DD58 File Offset: 0x0064BF58
		public bool Active
		{
			get
			{
				return this.Enabled && this._active;
			}
		}

		/// <summary>
		/// Sets this extra jump to usable for this game tick.<br />
		/// If you want to disable this extra jump, use <see cref="M:Terraria.DataStructures.ExtraJumpState.Disable" />
		/// </summary>
		// Token: 0x060049A4 RID: 18852 RVA: 0x0064DD6A File Offset: 0x0064BF6A
		public void Enable()
		{
			this._enabled = true;
		}

		/// <summary>
		/// Forces this extra jump to be disabled, consuming it and preventing the usage of it during the current game tick in the process.<br />
		/// If you want to disable all extra jumps, using <see cref="F:Terraria.Player.blockExtraJumps" /> is preferred.
		/// </summary>
		// Token: 0x060049A5 RID: 18853 RVA: 0x0064DD73 File Offset: 0x0064BF73
		public void Disable()
		{
			this._disabled = true;
		}

		// Token: 0x060049A6 RID: 18854 RVA: 0x0064DD7C File Offset: 0x0064BF7C
		internal void Start()
		{
			this._available = false;
			this._active = true;
		}

		// Token: 0x060049A7 RID: 18855 RVA: 0x0064DD8C File Offset: 0x0064BF8C
		internal void Stop()
		{
			this._active = false;
		}

		// Token: 0x060049A8 RID: 18856 RVA: 0x0064DD95 File Offset: 0x0064BF95
		internal void ResetEnabled()
		{
			this._enabled = false;
			this._disabled = false;
		}

		// Token: 0x060049A9 RID: 18857 RVA: 0x0064DDA5 File Offset: 0x0064BFA5
		internal void CommitEnabledState(out bool jumpEnded)
		{
			jumpEnded = false;
			if (this.Enabled)
			{
				return;
			}
			jumpEnded = this._active;
			this._active = false;
			this._available = false;
		}

		// Token: 0x04005ED5 RID: 24277
		private bool _enabled;

		// Token: 0x04005ED6 RID: 24278
		private bool _available;

		// Token: 0x04005ED7 RID: 24279
		private bool _active;

		// Token: 0x04005ED8 RID: 24280
		private bool _disabled;
	}
}
