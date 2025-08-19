using System;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.PlayerChanges
{
	// Token: 0x0200022E RID: 558
	public class DashPlayer : ModPlayer
	{
		// Token: 0x06000D6F RID: 3439 RVA: 0x00068744 File Offset: 0x00066944
		public override void ProcessTriggers(TriggersSet triggersSet)
		{
			if (Main.LocalPlayer.controlLeft && !this.LeftLastPressed)
			{
				this.latestXDirPressed = -1;
			}
			if (Main.LocalPlayer.controlRight && !this.RightLastPressed)
			{
				this.latestXDirPressed = 1;
			}
			if (!Main.LocalPlayer.controlLeft && !Main.LocalPlayer.releaseLeft)
			{
				this.latestXDirReleased = -1;
			}
			if (!Main.LocalPlayer.controlRight && !Main.LocalPlayer.releaseRight)
			{
				this.latestXDirReleased = 1;
			}
			this.LeftLastPressed = Main.LocalPlayer.controlLeft;
			this.RightLastPressed = Main.LocalPlayer.controlRight;
		}

		// Token: 0x04000598 RID: 1432
		public int latestXDirPressed;

		// Token: 0x04000599 RID: 1433
		public int latestXDirReleased;

		// Token: 0x0400059A RID: 1434
		public bool LeftLastPressed;

		// Token: 0x0400059B RID: 1435
		public bool RightLastPressed;
	}
}
