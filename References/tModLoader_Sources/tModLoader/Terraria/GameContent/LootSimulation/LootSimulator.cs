using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using ReLogic.OS;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terraria.GameContent.LootSimulation
{
	// Token: 0x020005E2 RID: 1506
	public class LootSimulator
	{
		// Token: 0x06004335 RID: 17205 RVA: 0x005FC9A5 File Offset: 0x005FABA5
		public LootSimulator()
		{
			this.FillDesiredTestConditions();
			this.FillItemExclusions();
		}

		// Token: 0x06004336 RID: 17206 RVA: 0x005FC9D0 File Offset: 0x005FABD0
		private void FillItemExclusions()
		{
			List<int> list = new List<int>();
			list.AddRange(from tuple in ItemID.Sets.IsAPickup.Select((bool state, int index) => new
			{
				index,
				state
			})
			where tuple.state
			select tuple.index);
			list.AddRange(from tuple in ItemID.Sets.CommonCoin.Select((bool state, int index) => new
			{
				index,
				state
			})
			where tuple.state
			select tuple.index);
			this._excludedItemIds = list.ToArray();
		}

		// Token: 0x06004337 RID: 17207 RVA: 0x005FCAE0 File Offset: 0x005FACE0
		private void FillDesiredTestConditions()
		{
			this._neededTestConditions.AddRange(new List<ISimulationConditionSetter>
			{
				SimulationConditionSetters.MidDay,
				SimulationConditionSetters.MidNight,
				SimulationConditionSetters.HardMode,
				SimulationConditionSetters.ExpertMode,
				SimulationConditionSetters.ExpertAndHardMode,
				SimulationConditionSetters.WindyExpertHardmodeEndgameBloodMoonNight,
				SimulationConditionSetters.WindyExpertHardmodeEndgameEclipseMorning,
				SimulationConditionSetters.SlimeStaffTest,
				SimulationConditionSetters.LuckyCoinTest
			});
		}

		// Token: 0x06004338 RID: 17208 RVA: 0x005FCB60 File Offset: 0x005FAD60
		public void Run()
		{
			int timesMultiplier = 10000;
			this.SetCleanSlateWorldConditions();
			string text = "";
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			for (int i = -65; i < NPCLoader.NPCCount; i++)
			{
				string outputText;
				if (this.TryGettingLootFor(i, timesMultiplier, out outputText))
				{
					text = text + outputText + "\n\n";
				}
			}
			stopwatch.Stop();
			string str = text;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(39, 1);
			defaultInterpolatedStringHandler.AppendLiteral("\nSimulation Took ");
			defaultInterpolatedStringHandler.AppendFormatted<float>((float)stopwatch.ElapsedMilliseconds / 1000f);
			defaultInterpolatedStringHandler.AppendLiteral(" seconds to complete.\n");
			text = str + defaultInterpolatedStringHandler.ToStringAndClear();
			Platform.Get<IClipboard>().Value = text;
		}

		// Token: 0x06004339 RID: 17209 RVA: 0x005FCC0C File Offset: 0x005FAE0C
		private void SetCleanSlateWorldConditions()
		{
			Main.dayTime = true;
			Main.time = 27000.0;
			Main.hardMode = false;
			Main.GameMode = 0;
			NPC.downedMechBoss1 = false;
			NPC.downedMechBoss2 = false;
			NPC.downedMechBoss3 = false;
			NPC.downedMechBossAny = false;
			NPC.downedPlantBoss = false;
			Main._shouldUseWindyDayMusic = false;
			Main._shouldUseStormMusic = false;
			Main.eclipse = false;
			Main.bloodMoon = false;
		}

		// Token: 0x0600433A RID: 17210 RVA: 0x005FCC70 File Offset: 0x005FAE70
		private bool TryGettingLootFor(int npcNetId, int timesMultiplier, out string outputText)
		{
			SimulatorInfo simulatorInfo = new SimulatorInfo();
			NPC nPC = new NPC();
			nPC.SetDefaults(npcNetId, default(NPCSpawnParams));
			simulatorInfo.npcVictim = nPC;
			LootSimulationItemCounter lootSimulationItemCounter = simulatorInfo.itemCounter = new LootSimulationItemCounter();
			foreach (ISimulationConditionSetter neededTestCondition in this._neededTestConditions)
			{
				neededTestCondition.Setup(simulatorInfo);
				int num = neededTestCondition.GetTimesToRunMultiplier(simulatorInfo) * timesMultiplier;
				for (int i = 0; i < num; i++)
				{
					nPC.NPCLoot();
				}
				lootSimulationItemCounter.IncreaseTimesAttempted(num, simulatorInfo.runningExpertMode);
				neededTestCondition.TearDown(simulatorInfo);
				this.SetCleanSlateWorldConditions();
			}
			lootSimulationItemCounter.Exclude(this._excludedItemIds.ToArray<int>());
			string text = lootSimulationItemCounter.PrintCollectedItems(false);
			string text2 = lootSimulationItemCounter.PrintCollectedItems(true);
			string name = NPCID.Search.GetName(npcNetId);
			string text3 = "FindEntryByNPCID(NPCID." + name + ")";
			if (text.Length > 0)
			{
				text3 = text3 + "\n.AddDropsNormalMode(" + text + ")";
			}
			if (text2.Length > 0)
			{
				text3 = text3 + "\n.AddDropsExpertMode(" + text2 + ")";
			}
			text3 += ";";
			outputText = text3;
			return text.Length > 0 || text2.Length > 0;
		}

		// Token: 0x040059F1 RID: 23025
		private List<ISimulationConditionSetter> _neededTestConditions = new List<ISimulationConditionSetter>();

		// Token: 0x040059F2 RID: 23026
		private int[] _excludedItemIds = new int[0];
	}
}
