using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.LootSimulation
{
	// Token: 0x02000276 RID: 630
	public class SimulatorInfo
	{
		// Token: 0x06001FD9 RID: 8153 RVA: 0x005173C8 File Offset: 0x005155C8
		public SimulatorInfo()
		{
			this.player = new Player();
			this._originalDayTimeCounter = Main.time;
			this._originalDayTimeFlag = Main.dayTime;
			this._originalPlayerPosition = this.player.position;
			this.runningExpertMode = false;
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x00517414 File Offset: 0x00515614
		public void ReturnToOriginalDaytime()
		{
			Main.dayTime = this._originalDayTimeFlag;
			Main.time = this._originalDayTimeCounter;
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x0051742C File Offset: 0x0051562C
		public void AddItem(int itemId, int amount)
		{
			this.itemCounter.AddItem(itemId, amount, this.runningExpertMode);
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x00517441 File Offset: 0x00515641
		public void ReturnToOriginalPlayerPosition()
		{
			this.player.position = this._originalPlayerPosition;
		}

		// Token: 0x04004698 RID: 18072
		public Player player;

		// Token: 0x04004699 RID: 18073
		private double _originalDayTimeCounter;

		// Token: 0x0400469A RID: 18074
		private bool _originalDayTimeFlag;

		// Token: 0x0400469B RID: 18075
		private Vector2 _originalPlayerPosition;

		// Token: 0x0400469C RID: 18076
		public bool runningExpertMode;

		// Token: 0x0400469D RID: 18077
		public LootSimulationItemCounter itemCounter;

		// Token: 0x0400469E RID: 18078
		public NPC npcVictim;
	}
}
