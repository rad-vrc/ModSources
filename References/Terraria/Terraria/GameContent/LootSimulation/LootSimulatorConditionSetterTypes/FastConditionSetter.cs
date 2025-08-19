using System;

namespace Terraria.GameContent.LootSimulation.LootSimulatorConditionSetterTypes
{
	// Token: 0x0200027A RID: 634
	public class FastConditionSetter : ISimulationConditionSetter
	{
		// Token: 0x06001FE7 RID: 8167 RVA: 0x00517855 File Offset: 0x00515A55
		public FastConditionSetter(Action<SimulatorInfo> setup, Action<SimulatorInfo> tearDown)
		{
			this._setup = setup;
			this._tearDown = tearDown;
		}

		// Token: 0x06001FE8 RID: 8168 RVA: 0x0051786B File Offset: 0x00515A6B
		public void Setup(SimulatorInfo info)
		{
			if (this._setup != null)
			{
				this._setup(info);
			}
		}

		// Token: 0x06001FE9 RID: 8169 RVA: 0x00517881 File Offset: 0x00515A81
		public void TearDown(SimulatorInfo info)
		{
			if (this._tearDown != null)
			{
				this._tearDown(info);
			}
		}

		// Token: 0x06001FEA RID: 8170 RVA: 0x0003266D File Offset: 0x0003086D
		public int GetTimesToRunMultiplier(SimulatorInfo info)
		{
			return 1;
		}

		// Token: 0x040046B2 RID: 18098
		private Action<SimulatorInfo> _setup;

		// Token: 0x040046B3 RID: 18099
		private Action<SimulatorInfo> _tearDown;
	}
}
