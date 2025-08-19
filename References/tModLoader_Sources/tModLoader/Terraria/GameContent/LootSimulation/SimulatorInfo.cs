using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.LootSimulation
{
	// Token: 0x020005E4 RID: 1508
	public class SimulatorInfo
	{
		// Token: 0x0600433D RID: 17213 RVA: 0x005FD064 File Offset: 0x005FB264
		public SimulatorInfo()
		{
			this.player = new Player();
			this._originalDayTimeCounter = Main.time;
			this._originalDayTimeFlag = Main.dayTime;
			this._originalPlayerPosition = this.player.position;
			this.runningExpertMode = false;
		}

		// Token: 0x0600433E RID: 17214 RVA: 0x005FD0B0 File Offset: 0x005FB2B0
		public void ReturnToOriginalDaytime()
		{
			Main.dayTime = this._originalDayTimeFlag;
			Main.time = this._originalDayTimeCounter;
		}

		// Token: 0x0600433F RID: 17215 RVA: 0x005FD0C8 File Offset: 0x005FB2C8
		public void AddItem(int itemId, int amount)
		{
			this.itemCounter.AddItem(itemId, amount, this.runningExpertMode);
		}

		// Token: 0x06004340 RID: 17216 RVA: 0x005FD0DD File Offset: 0x005FB2DD
		public void ReturnToOriginalPlayerPosition()
		{
			this.player.position = this._originalPlayerPosition;
		}

		// Token: 0x04005A02 RID: 23042
		public Player player;

		// Token: 0x04005A03 RID: 23043
		private double _originalDayTimeCounter;

		// Token: 0x04005A04 RID: 23044
		private bool _originalDayTimeFlag;

		// Token: 0x04005A05 RID: 23045
		private Vector2 _originalPlayerPosition;

		// Token: 0x04005A06 RID: 23046
		public bool runningExpertMode;

		// Token: 0x04005A07 RID: 23047
		public LootSimulationItemCounter itemCounter;

		// Token: 0x04005A08 RID: 23048
		public NPC npcVictim;
	}
}
