using System;

namespace Terraria.GameContent.LootSimulation.LootSimulatorConditionSetterTypes
{
	// Token: 0x0200027B RID: 635
	public class StackedConditionSetter : ISimulationConditionSetter
	{
		// Token: 0x06001FEB RID: 8171 RVA: 0x00517897 File Offset: 0x00515A97
		public StackedConditionSetter(params ISimulationConditionSetter[] setters)
		{
			this._setters = setters;
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x005178A8 File Offset: 0x00515AA8
		public void Setup(SimulatorInfo info)
		{
			for (int i = 0; i < this._setters.Length; i++)
			{
				this._setters[i].Setup(info);
			}
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x005178D8 File Offset: 0x00515AD8
		public void TearDown(SimulatorInfo info)
		{
			for (int i = 0; i < this._setters.Length; i++)
			{
				this._setters[i].TearDown(info);
			}
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x0003266D File Offset: 0x0003086D
		public int GetTimesToRunMultiplier(SimulatorInfo info)
		{
			return 1;
		}

		// Token: 0x040046B4 RID: 18100
		private ISimulationConditionSetter[] _setters;
	}
}
