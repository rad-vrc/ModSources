using System;
using Terraria.GameContent.LootSimulation.LootSimulatorConditionSetterTypes;

namespace Terraria.GameContent.LootSimulation
{
	// Token: 0x02000279 RID: 633
	public class SimulationConditionSetters
	{
		// Token: 0x040046A3 RID: 18083
		public static FastConditionSetter HardMode = new FastConditionSetter(delegate(SimulatorInfo info)
		{
			Main.hardMode = true;
		}, delegate(SimulatorInfo info)
		{
			Main.hardMode = false;
		});

		// Token: 0x040046A4 RID: 18084
		public static FastConditionSetter ExpertMode = new FastConditionSetter(delegate(SimulatorInfo info)
		{
			Main.GameMode = 1;
			info.runningExpertMode = true;
		}, delegate(SimulatorInfo info)
		{
			Main.GameMode = 0;
			info.runningExpertMode = false;
		});

		// Token: 0x040046A5 RID: 18085
		public static FastConditionSetter Eclipse = new FastConditionSetter(delegate(SimulatorInfo info)
		{
			Main.eclipse = true;
		}, delegate(SimulatorInfo info)
		{
			Main.eclipse = false;
		});

		// Token: 0x040046A6 RID: 18086
		public static FastConditionSetter BloodMoon = new FastConditionSetter(delegate(SimulatorInfo info)
		{
			Main.bloodMoon = true;
		}, delegate(SimulatorInfo info)
		{
			Main.bloodMoon = false;
		});

		// Token: 0x040046A7 RID: 18087
		public static FastConditionSetter SlainMechBosses = new FastConditionSetter(delegate(SimulatorInfo info)
		{
			NPC.downedMechBoss1 = (NPC.downedMechBoss2 = (NPC.downedMechBoss3 = (NPC.downedMechBossAny = true)));
		}, delegate(SimulatorInfo info)
		{
			NPC.downedMechBoss1 = (NPC.downedMechBoss2 = (NPC.downedMechBoss3 = (NPC.downedMechBossAny = false)));
		});

		// Token: 0x040046A8 RID: 18088
		public static FastConditionSetter SlainPlantera = new FastConditionSetter(delegate(SimulatorInfo info)
		{
			NPC.downedPlantBoss = true;
		}, delegate(SimulatorInfo info)
		{
			NPC.downedPlantBoss = false;
		});

		// Token: 0x040046A9 RID: 18089
		public static StackedConditionSetter ExpertAndHardMode = new StackedConditionSetter(new ISimulationConditionSetter[]
		{
			SimulationConditionSetters.ExpertMode,
			SimulationConditionSetters.HardMode
		});

		// Token: 0x040046AA RID: 18090
		public static FastConditionSetter WindyWeather = new FastConditionSetter(delegate(SimulatorInfo info)
		{
			Main._shouldUseWindyDayMusic = true;
		}, delegate(SimulatorInfo info)
		{
			Main._shouldUseWindyDayMusic = false;
		});

		// Token: 0x040046AB RID: 18091
		public static FastConditionSetter MidDay = new FastConditionSetter(delegate(SimulatorInfo info)
		{
			Main.dayTime = true;
			Main.time = 27000.0;
		}, delegate(SimulatorInfo info)
		{
			info.ReturnToOriginalDaytime();
		});

		// Token: 0x040046AC RID: 18092
		public static FastConditionSetter MidNight = new FastConditionSetter(delegate(SimulatorInfo info)
		{
			Main.dayTime = false;
			Main.time = 16200.0;
		}, delegate(SimulatorInfo info)
		{
			info.ReturnToOriginalDaytime();
		});

		// Token: 0x040046AD RID: 18093
		public static FastConditionSetter SlimeRain = new FastConditionSetter(delegate(SimulatorInfo info)
		{
			Main.slimeRain = true;
		}, delegate(SimulatorInfo info)
		{
			Main.slimeRain = false;
		});

		// Token: 0x040046AE RID: 18094
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

		// Token: 0x040046AF RID: 18095
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

		// Token: 0x040046B0 RID: 18096
		public static SlimeStaffConditionSetter SlimeStaffTest = new SlimeStaffConditionSetter(100);

		// Token: 0x040046B1 RID: 18097
		public static LuckyCoinConditionSetter LuckyCoinTest = new LuckyCoinConditionSetter(100);
	}
}
