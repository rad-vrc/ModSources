using System;

namespace Terraria.GameContent.LootSimulation.LootSimulatorConditionSetterTypes
{
	// Token: 0x0200027C RID: 636
	public class SlimeStaffConditionSetter : ISimulationConditionSetter
	{
		// Token: 0x06001FEF RID: 8175 RVA: 0x00517906 File Offset: 0x00515B06
		public SlimeStaffConditionSetter(int timesToRunMultiplier)
		{
			this._timesToRun = timesToRunMultiplier;
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x00517918 File Offset: 0x00515B18
		public int GetTimesToRunMultiplier(SimulatorInfo info)
		{
			int netID = info.npcVictim.netID;
			if (netID <= 147)
			{
				if (netID <= 1)
				{
					if (netID - -33 <= 1 || netID - -10 <= 7 || netID == 1)
					{
						goto IL_C3;
					}
				}
				else if (netID <= 138)
				{
					if (netID == 16 || netID == 138)
					{
						goto IL_C3;
					}
				}
				else if (netID == 141 || netID == 147)
				{
					goto IL_C3;
				}
			}
			else if (netID <= 302)
			{
				if (netID <= 187)
				{
					if (netID == 184 || netID == 187)
					{
						goto IL_C3;
					}
				}
				else if (netID == 204 || netID == 302)
				{
					goto IL_C3;
				}
			}
			else if (netID <= 433)
			{
				if (netID - 333 <= 3 || netID == 433)
				{
					goto IL_C3;
				}
			}
			else if (netID == 535 || netID == 537)
			{
				goto IL_C3;
			}
			return 0;
			IL_C3:
			return this._timesToRun;
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void Setup(SimulatorInfo info)
		{
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void TearDown(SimulatorInfo info)
		{
		}

		// Token: 0x040046B5 RID: 18101
		private int _timesToRun;
	}
}
