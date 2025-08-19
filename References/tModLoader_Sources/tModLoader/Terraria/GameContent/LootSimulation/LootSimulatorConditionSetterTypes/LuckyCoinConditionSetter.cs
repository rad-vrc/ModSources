using System;

namespace Terraria.GameContent.LootSimulation.LootSimulatorConditionSetterTypes
{
	// Token: 0x020005E6 RID: 1510
	public class LuckyCoinConditionSetter : ISimulationConditionSetter
	{
		// Token: 0x06004345 RID: 17221 RVA: 0x005FD135 File Offset: 0x005FB335
		public LuckyCoinConditionSetter(int timesToRunMultiplier)
		{
			this._timesToRun = timesToRunMultiplier;
		}

		// Token: 0x06004346 RID: 17222 RVA: 0x005FD144 File Offset: 0x005FB344
		public int GetTimesToRunMultiplier(SimulatorInfo info)
		{
			int netID = info.npcVictim.netID;
			if (netID != 216 && netID != 491)
			{
				return 0;
			}
			return this._timesToRun;
		}

		// Token: 0x06004347 RID: 17223 RVA: 0x005FD175 File Offset: 0x005FB375
		public void Setup(SimulatorInfo info)
		{
		}

		// Token: 0x06004348 RID: 17224 RVA: 0x005FD177 File Offset: 0x005FB377
		public void TearDown(SimulatorInfo info)
		{
		}

		// Token: 0x04005A0B RID: 23051
		private int _timesToRun;
	}
}
