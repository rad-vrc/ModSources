using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200041A RID: 1050
	public struct PlayerMovementAccsCache
	{
		// Token: 0x06002B78 RID: 11128 RVA: 0x0059E56C File Offset: 0x0059C76C
		public void CopyFrom(Player player)
		{
			if (this._readyToPaste)
			{
				return;
			}
			this._readyToPaste = true;
			this._mountPreventedFlight = true;
			this._mountPreventedExtraJumps = player.mount.BlockExtraJumps;
			this.rocketTime = player.rocketTime;
			this.rocketDelay = player.rocketDelay;
			this.rocketDelay2 = player.rocketDelay2;
			this.wingTime = player.wingTime;
			this.jumpAgainCloud = player.canJumpAgain_Cloud;
			this.jumpAgainSandstorm = player.canJumpAgain_Sandstorm;
			this.jumpAgainBlizzard = player.canJumpAgain_Blizzard;
			this.jumpAgainFart = player.canJumpAgain_Fart;
			this.jumpAgainSail = player.canJumpAgain_Sail;
			this.jumpAgainUnicorn = player.canJumpAgain_Unicorn;
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x0059E61C File Offset: 0x0059C81C
		public void PasteInto(Player player)
		{
			if (!this._readyToPaste)
			{
				return;
			}
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
				player.canJumpAgain_Cloud = this.jumpAgainCloud;
				player.canJumpAgain_Sandstorm = this.jumpAgainSandstorm;
				player.canJumpAgain_Blizzard = this.jumpAgainBlizzard;
				player.canJumpAgain_Fart = this.jumpAgainFart;
				player.canJumpAgain_Sail = this.jumpAgainSail;
				player.canJumpAgain_Unicorn = this.jumpAgainUnicorn;
			}
		}

		// Token: 0x04004F9A RID: 20378
		private bool _readyToPaste;

		// Token: 0x04004F9B RID: 20379
		private bool _mountPreventedFlight;

		// Token: 0x04004F9C RID: 20380
		private bool _mountPreventedExtraJumps;

		// Token: 0x04004F9D RID: 20381
		private int rocketTime;

		// Token: 0x04004F9E RID: 20382
		private float wingTime;

		// Token: 0x04004F9F RID: 20383
		private int rocketDelay;

		// Token: 0x04004FA0 RID: 20384
		private int rocketDelay2;

		// Token: 0x04004FA1 RID: 20385
		private bool jumpAgainCloud;

		// Token: 0x04004FA2 RID: 20386
		private bool jumpAgainSandstorm;

		// Token: 0x04004FA3 RID: 20387
		private bool jumpAgainBlizzard;

		// Token: 0x04004FA4 RID: 20388
		private bool jumpAgainFart;

		// Token: 0x04004FA5 RID: 20389
		private bool jumpAgainSail;

		// Token: 0x04004FA6 RID: 20390
		private bool jumpAgainUnicorn;
	}
}
