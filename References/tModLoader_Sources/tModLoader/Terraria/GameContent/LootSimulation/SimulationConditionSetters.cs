using System;
using Terraria.GameContent.LootSimulation.LootSimulatorConditionSetterTypes;

namespace Terraria.GameContent.LootSimulation
{
	// Token: 0x020005E3 RID: 1507
	public class SimulationConditionSetters
	{
		// Token: 0x040059F3 RID: 23027
		public static FastConditionSetter HardMode = new FastConditionSetter(delegate(SimulatorInfo <p0>)
		{
			Main.hardMode = true;
		}, delegate(SimulatorInfo <p0>)
		{
			Main.hardMode = false;
		});

		// Token: 0x040059F4 RID: 23028
		public static FastConditionSetter ExpertMode = new FastConditionSetter(delegate(SimulatorInfo info)
		{
			Main.GameMode = 1;
			info.runningExpertMode = true;
		}, delegate(SimulatorInfo info)
		{
			Main.GameMode = 0;
			info.runningExpertMode = false;
		});

		// Token: 0x040059F5 RID: 23029
		public static FastConditionSetter Eclipse = new FastConditionSetter(delegate(SimulatorInfo <p0>)
		{
			Main.eclipse = true;
		}, delegate(SimulatorInfo <p0>)
		{
			Main.eclipse = false;
		});

		// Token: 0x040059F6 RID: 23030
		public static FastConditionSetter BloodMoon = new FastConditionSetter(delegate(SimulatorInfo <p0>)
		{
			Main.bloodMoon = true;
		}, delegate(SimulatorInfo <p0>)
		{
			Main.bloodMoon = false;
		});

		// Token: 0x040059F7 RID: 23031
		public static FastConditionSetter SlainMechBosses = new FastConditionSetter(delegate(SimulatorInfo <p0>)
		{
			NPC.downedMechBoss1 = (NPC.downedMechBoss2 = (NPC.downedMechBoss3 = (NPC.downedMechBossAny = true)));
		}, delegate(SimulatorInfo <p0>)
		{
			NPC.downedMechBoss1 = (NPC.downedMechBoss2 = (NPC.downedMechBoss3 = (NPC.downedMechBossAny = false)));
		});

		// Token: 0x040059F8 RID: 23032
		public static FastConditionSetter SlainPlantera = new FastConditionSetter(delegate(SimulatorInfo <p0>)
		{
			NPC.downedPlantBoss = true;
		}, delegate(SimulatorInfo <p0>)
		{
			NPC.downedPlantBoss = false;
		});

		// Token: 0x040059F9 RID: 23033
		public static StackedConditionSetter ExpertAndHardMode = new StackedConditionSetter(new ISimulationConditionSetter[]
		{
			SimulationConditionSetters.ExpertMode,
			SimulationConditionSetters.HardMode
		});

		// Token: 0x040059FA RID: 23034
		public static FastConditionSetter WindyWeather = new FastConditionSetter(delegate(SimulatorInfo <p0>)
		{
			Main._shouldUseWindyDayMusic = true;
		}, delegate(SimulatorInfo <p0>)
		{
			Main._shouldUseWindyDayMusic = false;
		});

		// Token: 0x040059FB RID: 23035
		public static FastConditionSetter MidDay = new FastConditionSetter(delegate(SimulatorInfo <p0>)
		{
			Main.dayTime = true;
			Main.time = 27000.0;
		}, delegate(SimulatorInfo info)
		{
			info.ReturnToOriginalDaytime();
		});

		// Token: 0x040059FC RID: 23036
		public static FastConditionSetter MidNight = new FastConditionSetter(delegate(SimulatorInfo <p0>)
		{
			Main.dayTime = false;
			Main.time = 16200.0;
		}, delegate(SimulatorInfo info)
		{
			info.ReturnToOriginalDaytime();
		});

		// Token: 0x040059FD RID: 23037
		public static FastConditionSetter SlimeRain = new FastConditionSetter(delegate(SimulatorInfo <p0>)
		{
			Main.slimeRain = true;
		}, delegate(SimulatorInfo <p0>)
		{
			Main.slimeRain = false;
		});

		// Token: 0x040059FE RID: 23038
		public static StackedConditionSetter WindyExpertHardmodeEndgameEclipseMorning = new StackedConditionSetter(new ISimulationConditionSetter[]
		{
			SimulationConditionSetters.WindyWeather,
			SimulationConditionSetters.ExpertMode,
			SimulationConditionSetters.HardMode,
			SimulationConditionSetters.SlainMechBosses,
			SimulationConditionSetters.SlainPlantera,
			SimulationConditionSetters.Eclipse,
			SimulationConditionSetters.MidDay
		});

		// Token: 0x040059FF RID: 23039
		public static StackedConditionSetter WindyExpertHardmodeEndgameBloodMoonNight = new StackedConditionSetter(new ISimulationConditionSetter[]
		{
			SimulationConditionSetters.WindyWeather,
			SimulationConditionSetters.ExpertMode,
			SimulationConditionSetters.HardMode,
			SimulationConditionSetters.SlainMechBosses,
			SimulationConditionSetters.SlainPlantera,
			SimulationConditionSetters.BloodMoon,
			SimulationConditionSetters.MidNight
		});

		// Token: 0x04005A00 RID: 23040
		public static SlimeStaffConditionSetter SlimeStaffTest = new SlimeStaffConditionSetter(100);

		// Token: 0x04005A01 RID: 23041
		public static LuckyCoinConditionSetter LuckyCoinTest = new LuckyCoinConditionSetter(100);
	}
}
