using System;
using Terraria.ModLoader;

namespace Terraria.DataStructures
{
	// Token: 0x02000729 RID: 1833
	public struct PlayerMovementAccsCache
	{
		// Token: 0x06004A95 RID: 19093 RVA: 0x00667CF0 File Offset: 0x00665EF0
		public void CopyFrom(Player player)
		{
			if (!this._readyToPaste)
			{
				this._readyToPaste = true;
				this._mountPreventedFlight = true;
				this._mountPreventedExtraJumps = player.mount.BlockExtraJumps;
				this.rocketTime = player.rocketTime;
				this.rocketDelay = player.rocketDelay;
				this.rocketDelay2 = player.rocketDelay2;
				this.wingTime = player.wingTime;
				if (this.canJumpAgain == null)
				{
					this.canJumpAgain = new bool[ExtraJumpLoader.ExtraJumpCount];
				}
				foreach (ExtraJump jump in ExtraJumpLoader.ExtraJumps)
				{
					this.canJumpAgain[jump.Type] = player.GetJumpState<ExtraJump>(jump).Available;
				}
			}
		}

		// Token: 0x06004A96 RID: 19094 RVA: 0x00667DC8 File Offset: 0x00665FC8
		public void PasteInto(Player player)
		{
			if (this._readyToPaste)
			{
				this._readyToPaste = false;
				if (this._mountPreventedFlight)
				{
					player.rocketTime = this.rocketTime;
					player.rocketDelay = this.rocketDelay;
					player.rocketDelay2 = this.rocketDelay2;
					player.wingTime = this.wingTime;
				}
				if (this._mountPreventedExtraJumps)
				{
					foreach (ExtraJump jump in ExtraJumpLoader.ExtraJumps)
					{
						player.GetJumpState<ExtraJump>(jump).Available = this.canJumpAgain[jump.Type];
					}
				}
			}
		}

		// Token: 0x04005FE5 RID: 24549
		private bool _readyToPaste;

		// Token: 0x04005FE6 RID: 24550
		private bool _mountPreventedFlight;

		// Token: 0x04005FE7 RID: 24551
		private bool _mountPreventedExtraJumps;

		// Token: 0x04005FE8 RID: 24552
		private int rocketTime;

		// Token: 0x04005FE9 RID: 24553
		private float wingTime;

		// Token: 0x04005FEA RID: 24554
		private int rocketDelay;

		// Token: 0x04005FEB RID: 24555
		private int rocketDelay2;

		// Token: 0x04005FEC RID: 24556
		private bool[] canJumpAgain;
	}
}
