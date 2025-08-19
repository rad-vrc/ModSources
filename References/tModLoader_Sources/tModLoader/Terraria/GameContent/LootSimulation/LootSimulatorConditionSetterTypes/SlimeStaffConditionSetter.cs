using System;

namespace Terraria.GameContent.LootSimulation.LootSimulatorConditionSetterTypes
{
	// Token: 0x020005E7 RID: 1511
	public class SlimeStaffConditionSetter : ISimulationConditionSetter
	{
		// Token: 0x06004349 RID: 17225 RVA: 0x005FD179 File Offset: 0x005FB379
		public SlimeStaffConditionSetter(int timesToRunMultiplier)
		{
			this._timesToRun = timesToRunMultiplier;
		}

		// Token: 0x0600434A RID: 17226 RVA: 0x005FD188 File Offset: 0x005FB388
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

		// Token: 0x0600434B RID: 17227 RVA: 0x005FD25E File Offset: 0x005FB45E
		public void Setup(SimulatorInfo info)
		{
		}

		// Token: 0x0600434C RID: 17228 RVA: 0x005FD260 File Offset: 0x005FB460
		public void TearDown(SimulatorInfo info)
		{
		}

		// Token: 0x04005A0C RID: 23052
		private int _timesToRun;
	}
}
