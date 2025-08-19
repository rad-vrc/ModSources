using System;

namespace Terraria.GameContent.LootSimulation.LootSimulatorConditionSetterTypes
{
	// Token: 0x020005E5 RID: 1509
	public class FastConditionSetter : ISimulationConditionSetter
	{
		// Token: 0x06004341 RID: 17217 RVA: 0x005FD0F0 File Offset: 0x005FB2F0
		public FastConditionSetter(Action<SimulatorInfo> setup, Action<SimulatorInfo> tearDown)
		{
			this._setup = setup;
			this._tearDown = tearDown;
		}

		// Token: 0x06004342 RID: 17218 RVA: 0x005FD106 File Offset: 0x005FB306
		public void Setup(SimulatorInfo info)
		{
			if (this._setup != null)
			{
				this._setup(info);
			}
		}

		// Token: 0x06004343 RID: 17219 RVA: 0x005FD11C File Offset: 0x005FB31C
		public void TearDown(SimulatorInfo info)
		{
			if (this._tearDown != null)
			{
				this._tearDown(info);
			}
		}

		// Token: 0x06004344 RID: 17220 RVA: 0x005FD132 File Offset: 0x005FB332
		public int GetTimesToRunMultiplier(SimulatorInfo info)
		{
			return 1;
		}

		// Token: 0x04005A09 RID: 23049
		private Action<SimulatorInfo> _setup;

		// Token: 0x04005A0A RID: 23050
		private Action<SimulatorInfo> _tearDown;
	}
}
