using System;

namespace Terraria.GameContent.LootSimulation
{
	// Token: 0x020005E0 RID: 1504
	public interface ISimulationConditionSetter
	{
		// Token: 0x0600432D RID: 17197
		int GetTimesToRunMultiplier(SimulatorInfo info);

		// Token: 0x0600432E RID: 17198
		void Setup(SimulatorInfo info);

		// Token: 0x0600432F RID: 17199
		void TearDown(SimulatorInfo info);
	}
}
