using System;

namespace Terraria.GameContent.LootSimulation.LootSimulatorConditionSetterTypes
{
	// Token: 0x020005E8 RID: 1512
	public class StackedConditionSetter : ISimulationConditionSetter
	{
		// Token: 0x0600434D RID: 17229 RVA: 0x005FD262 File Offset: 0x005FB462
		public StackedConditionSetter(params ISimulationConditionSetter[] setters)
		{
			this._setters = setters;
		}

		// Token: 0x0600434E RID: 17230 RVA: 0x005FD274 File Offset: 0x005FB474
		public void Setup(SimulatorInfo info)
		{
			for (int i = 0; i < this._setters.Length; i++)
			{
				this._setters[i].Setup(info);
			}
		}

		// Token: 0x0600434F RID: 17231 RVA: 0x005FD2A4 File Offset: 0x005FB4A4
		public void TearDown(SimulatorInfo info)
		{
			for (int i = 0; i < this._setters.Length; i++)
			{
				this._setters[i].TearDown(info);
			}
		}

		// Token: 0x06004350 RID: 17232 RVA: 0x005FD2D2 File Offset: 0x005FB4D2
		public int GetTimesToRunMultiplier(SimulatorInfo info)
		{
			return 1;
		}

		// Token: 0x04005A0D RID: 23053
		private ISimulationConditionSetter[] _setters;
	}
}
