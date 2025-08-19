using System;

namespace Terraria.GameContent.LootSimulation
{
	// Token: 0x02000278 RID: 632
	public interface ISimulationConditionSetter
	{
		// Token: 0x06001FE2 RID: 8162
		int GetTimesToRunMultiplier(SimulatorInfo info);

		// Token: 0x06001FE3 RID: 8163
		void Setup(SimulatorInfo info);

		// Token: 0x06001FE4 RID: 8164
		void TearDown(SimulatorInfo info);
	}
}
