using System;

namespace Terraria.GameContent.LootSimulation.LootSimulatorConditionSetterTypes
{
	// Token: 0x0200027D RID: 637
	public class LuckyCoinConditionSetter : ISimulationConditionSetter
	{
		// Token: 0x06001FF3 RID: 8179 RVA: 0x005179EE File Offset: 0x00515BEE
		public LuckyCoinConditionSetter(int timesToRunMultiplier)
		{
			this._timesToRun = timesToRunMultiplier;
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x00517A00 File Offset: 0x00515C00
		public int GetTimesToRunMultiplier(SimulatorInfo info)
		{
			int netID = info.npcVictim.netID;
			if (netID != 216 && netID != 491)
			{
				return 0;
			}
			return this._timesToRun;
		}

		// Token: 0x06001FF5 RID: 8181 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void Setup(SimulatorInfo info)
		{
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void TearDown(SimulatorInfo info)
		{
		}

		// Token: 0x040046B6 RID: 18102
		private int _timesToRun;
	}
}
