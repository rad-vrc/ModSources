using System;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x020005F3 RID: 1523
	public class Conditions
	{
		// Token: 0x06004397 RID: 17303 RVA: 0x006005A8 File Offset: 0x005FE7A8
		public static bool SoulOfWhateverConditionCanDrop(DropAttemptInfo info)
		{
			if (info.npc.boss)
			{
				return false;
			}
			int type = info.npc.type;
			if (NPCID.Sets.CannotDropSouls[info.npc.type])
			{
				return false;
			}
			if (Main.remixWorld)
			{
				if (!Main.hardMode || info.npc.lifeMax <= 1 || info.npc.friendly || info.npc.value < 1f)
				{
					return false;
				}
			}
			else if (!Main.hardMode || info.npc.lifeMax <= 1 || info.npc.friendly || (double)info.npc.position.Y <= Main.rockLayer * 16.0 || info.npc.value < 1f)
			{
				return false;
			}
			return true;
		}

		// Token: 0x02000C7E RID: 3198
		public class NeverTrue : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600604A RID: 24650 RVA: 0x006D21BB File Offset: 0x006D03BB
			public bool CanDrop(DropAttemptInfo info)
			{
				return false;
			}

			// Token: 0x0600604B RID: 24651 RVA: 0x006D21BE File Offset: 0x006D03BE
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600604C RID: 24652 RVA: 0x006D21C1 File Offset: 0x006D03C1
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000C7F RID: 3199
		public class IsUsingSpecificAIValues : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600604E RID: 24654 RVA: 0x006D21CC File Offset: 0x006D03CC
			public IsUsingSpecificAIValues(int aislot, float valueToMatch)
			{
				this.aiSlotToCheck = aislot;
				this.valueToMatch = valueToMatch;
			}

			// Token: 0x0600604F RID: 24655 RVA: 0x006D21E2 File Offset: 0x006D03E2
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.ai[this.aiSlotToCheck] == this.valueToMatch;
			}

			// Token: 0x06006050 RID: 24656 RVA: 0x006D21FE File Offset: 0x006D03FE
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06006051 RID: 24657 RVA: 0x006D2201 File Offset: 0x006D0401
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x040079EB RID: 31211
			public int aiSlotToCheck;

			// Token: 0x040079EC RID: 31212
			public float valueToMatch;
		}

		// Token: 0x02000C80 RID: 3200
		public class FrostMoonDropGatingChance : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06006052 RID: 24658 RVA: 0x006D2204 File Offset: 0x006D0404
			public bool CanDrop(DropAttemptInfo info)
			{
				if (!Main.snowMoon)
				{
					return false;
				}
				int num = NPC.waveNumber;
				if (Main.expertMode)
				{
					num += 5;
				}
				int num2 = (int)((double)(28 - num) / 2.5);
				if (Main.expertMode)
				{
					num2 -= 2;
				}
				if (num2 < 1)
				{
					num2 = 1;
				}
				return info.player.RollLuck(num2) == 0;
			}

			// Token: 0x06006053 RID: 24659 RVA: 0x006D225C File Offset: 0x006D045C
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06006054 RID: 24660 RVA: 0x006D225F File Offset: 0x006D045F
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.WaveBasedDrop");
			}
		}

		// Token: 0x02000C81 RID: 3201
		public class PumpkinMoonDropGatingChance : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06006056 RID: 24662 RVA: 0x006D2274 File Offset: 0x006D0474
			public bool CanDrop(DropAttemptInfo info)
			{
				if (!Main.pumpkinMoon)
				{
					return false;
				}
				int num = NPC.waveNumber;
				if (Main.expertMode)
				{
					num += 5;
				}
				int num2 = (int)((double)(24 - num) / 2.5);
				if (Main.expertMode)
				{
					num2--;
				}
				if (num2 < 1)
				{
					num2 = 1;
				}
				return info.player.RollLuck(num2) == 0;
			}

			// Token: 0x06006057 RID: 24663 RVA: 0x006D22CC File Offset: 0x006D04CC
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06006058 RID: 24664 RVA: 0x006D22CF File Offset: 0x006D04CF
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.WaveBasedDrop");
			}
		}

		// Token: 0x02000C82 RID: 3202
		public class FrostMoonDropGateForTrophies : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600605A RID: 24666 RVA: 0x006D22E4 File Offset: 0x006D04E4
			public bool CanDrop(DropAttemptInfo info)
			{
				if (!Main.snowMoon)
				{
					return false;
				}
				int waveNumber = NPC.waveNumber;
				if (NPC.waveNumber < 15)
				{
					return false;
				}
				int num = 4;
				if (waveNumber == 16)
				{
					num = 4;
				}
				if (waveNumber == 17)
				{
					num = 3;
				}
				if (waveNumber == 18)
				{
					num = 3;
				}
				if (waveNumber == 19)
				{
					num = 2;
				}
				if (waveNumber >= 20)
				{
					num = 2;
				}
				if (Main.expertMode && Main.rand.Next(3) == 0)
				{
					num--;
				}
				return info.rng.Next(num) == 0;
			}

			// Token: 0x0600605B RID: 24667 RVA: 0x006D2357 File Offset: 0x006D0557
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600605C RID: 24668 RVA: 0x006D235A File Offset: 0x006D055A
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000C83 RID: 3203
		public class PumpkinMoonDropGateForTrophies : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600605E RID: 24670 RVA: 0x006D2368 File Offset: 0x006D0568
			public bool CanDrop(DropAttemptInfo info)
			{
				if (!Main.pumpkinMoon)
				{
					return false;
				}
				int waveNumber = NPC.waveNumber;
				if (NPC.waveNumber < 15)
				{
					return false;
				}
				int num = 4;
				if (waveNumber == 16)
				{
					num = 4;
				}
				if (waveNumber == 17)
				{
					num = 3;
				}
				if (waveNumber == 18)
				{
					num = 3;
				}
				if (waveNumber == 19)
				{
					num = 2;
				}
				if (waveNumber >= 20)
				{
					num = 2;
				}
				if (Main.expertMode && Main.rand.Next(3) == 0)
				{
					num--;
				}
				return info.rng.Next(num) == 0;
			}

			// Token: 0x0600605F RID: 24671 RVA: 0x006D23DB File Offset: 0x006D05DB
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06006060 RID: 24672 RVA: 0x006D23DE File Offset: 0x006D05DE
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000C84 RID: 3204
		public class IsPumpkinMoon : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06006062 RID: 24674 RVA: 0x006D23E9 File Offset: 0x006D05E9
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.pumpkinMoon;
			}

			// Token: 0x06006063 RID: 24675 RVA: 0x006D23F0 File Offset: 0x006D05F0
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06006064 RID: 24676 RVA: 0x006D23F3 File Offset: 0x006D05F3
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000C85 RID: 3205
		public class FromCertainWaveAndAbove : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06006066 RID: 24678 RVA: 0x006D23FE File Offset: 0x006D05FE
			public FromCertainWaveAndAbove(int neededWave)
			{
				this.neededWave = neededWave;
			}

			// Token: 0x06006067 RID: 24679 RVA: 0x006D240D File Offset: 0x006D060D
			public bool CanDrop(DropAttemptInfo info)
			{
				return NPC.waveNumber >= this.neededWave;
			}

			// Token: 0x06006068 RID: 24680 RVA: 0x006D241F File Offset: 0x006D061F
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06006069 RID: 24681 RVA: 0x006D2422 File Offset: 0x006D0622
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.PastWaveBasedDrop", this.neededWave);
			}

			// Token: 0x040079ED RID: 31213
			public int neededWave;
		}

		// Token: 0x02000C86 RID: 3206
		public class IsBloodMoonAndNotFromStatue : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600606A RID: 24682 RVA: 0x006D2439 File Offset: 0x006D0639
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.dayTime && Main.bloodMoon && !info.npc.SpawnedFromStatue && !info.IsInSimulation;
			}

			// Token: 0x0600606B RID: 24683 RVA: 0x006D2461 File Offset: 0x006D0661
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600606C RID: 24684 RVA: 0x006D2464 File Offset: 0x006D0664
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000C87 RID: 3207
		public class DownedAllMechBosses : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600606E RID: 24686 RVA: 0x006D246F File Offset: 0x006D066F
			public bool CanDrop(DropAttemptInfo info)
			{
				return NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3;
			}

			// Token: 0x0600606F RID: 24687 RVA: 0x006D2486 File Offset: 0x006D0686
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06006070 RID: 24688 RVA: 0x006D2489 File Offset: 0x006D0689
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000C88 RID: 3208
		public class DownedPlantera : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06006072 RID: 24690 RVA: 0x006D2494 File Offset: 0x006D0694
			public bool CanDrop(DropAttemptInfo info)
			{
				return NPC.downedPlantBoss;
			}

			// Token: 0x06006073 RID: 24691 RVA: 0x006D249B File Offset: 0x006D069B
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06006074 RID: 24692 RVA: 0x006D249E File Offset: 0x006D069E
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000C89 RID: 3209
		public class IsHardmode : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06006076 RID: 24694 RVA: 0x006D24A9 File Offset: 0x006D06A9
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.hardMode;
			}

			// Token: 0x06006077 RID: 24695 RVA: 0x006D24B0 File Offset: 0x006D06B0
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06006078 RID: 24696 RVA: 0x006D24B3 File Offset: 0x006D06B3
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000C8A RID: 3210
		public class FirstTimeKillingPlantera : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600607A RID: 24698 RVA: 0x006D24BE File Offset: 0x006D06BE
			public bool CanDrop(DropAttemptInfo info)
			{
				return !NPC.downedPlantBoss;
			}

			// Token: 0x0600607B RID: 24699 RVA: 0x006D24C8 File Offset: 0x006D06C8
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600607C RID: 24700 RVA: 0x006D24CB File Offset: 0x006D06CB
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000C8B RID: 3211
		public class MechanicalBossesDummyCondition : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600607E RID: 24702 RVA: 0x006D24D6 File Offset: 0x006D06D6
			public bool CanDrop(DropAttemptInfo info)
			{
				return true;
			}

			// Token: 0x0600607F RID: 24703 RVA: 0x006D24D9 File Offset: 0x006D06D9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06006080 RID: 24704 RVA: 0x006D24DC File Offset: 0x006D06DC
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000C8C RID: 3212
		public class PirateMap : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06006082 RID: 24706 RVA: 0x006D24E8 File Offset: 0x006D06E8
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.value > 0f && Main.hardMode && (double)(info.npc.position.Y / 16f) < Main.worldSurface + 10.0 && (info.npc.Center.X / 16f < 380f || info.npc.Center.X / 16f > (float)(Main.maxTilesX - 380)) && !info.IsInSimulation;
			}

			// Token: 0x06006083 RID: 24707 RVA: 0x006D2582 File Offset: 0x006D0782
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06006084 RID: 24708 RVA: 0x006D2585 File Offset: 0x006D0785
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.PirateMap");
			}
		}

		// Token: 0x02000C8D RID: 3213
		public class IsChristmas : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06006086 RID: 24710 RVA: 0x006D2599 File Offset: 0x006D0799
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.xMas;
			}

			// Token: 0x06006087 RID: 24711 RVA: 0x006D25A0 File Offset: 0x006D07A0
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06006088 RID: 24712 RVA: 0x006D25A3 File Offset: 0x006D07A3
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.IsChristmas");
			}
		}

		// Token: 0x02000C8E RID: 3214
		public class NotExpert : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600608A RID: 24714 RVA: 0x006D25B7 File Offset: 0x006D07B7
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.expertMode;
			}

			// Token: 0x0600608B RID: 24715 RVA: 0x006D25C1 File Offset: 0x006D07C1
			public bool CanShowItemDropInUI()
			{
				return !Main.expertMode;
			}

			// Token: 0x0600608C RID: 24716 RVA: 0x006D25CB File Offset: 0x006D07CB
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.NotExpert");
			}
		}

		// Token: 0x02000C8F RID: 3215
		public class NotMasterMode : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600608E RID: 24718 RVA: 0x006D25DF File Offset: 0x006D07DF
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.masterMode;
			}

			// Token: 0x0600608F RID: 24719 RVA: 0x006D25E9 File Offset: 0x006D07E9
			public bool CanShowItemDropInUI()
			{
				return !Main.masterMode;
			}

			// Token: 0x06006090 RID: 24720 RVA: 0x006D25F3 File Offset: 0x006D07F3
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.NotMasterMode");
			}
		}

		// Token: 0x02000C90 RID: 3216
		public class MissingTwin : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06006092 RID: 24722 RVA: 0x006D2608 File Offset: 0x006D0808
			public bool CanDrop(DropAttemptInfo info)
			{
				int type = 125;
				if (info.npc.type == 125)
				{
					type = 126;
				}
				return !NPC.AnyNPCs(type);
			}

			// Token: 0x06006093 RID: 24723 RVA: 0x006D2633 File Offset: 0x006D0833
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06006094 RID: 24724 RVA: 0x006D2636 File Offset: 0x006D0836
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000C91 RID: 3217
		public class EmpressOfLightIsGenuinelyEnraged : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06006096 RID: 24726 RVA: 0x006D2641 File Offset: 0x006D0841
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.AI_120_HallowBoss_IsGenuinelyEnraged();
			}

			// Token: 0x06006097 RID: 24727 RVA: 0x006D264E File Offset: 0x006D084E
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06006098 RID: 24728 RVA: 0x006D2651 File Offset: 0x006D0851
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.EmpressOfLightOnlyTookDamageWhileEnraged");
			}
		}

		// Token: 0x02000C92 RID: 3218
		public class PlayerNeedsHealing : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600609A RID: 24730 RVA: 0x006D2665 File Offset: 0x006D0865
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.player.statLife < info.player.statLifeMax2;
			}

			// Token: 0x0600609B RID: 24731 RVA: 0x006D267F File Offset: 0x006D087F
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600609C RID: 24732 RVA: 0x006D2682 File Offset: 0x006D0882
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.PlayerNeedsHealing");
			}
		}

		// Token: 0x02000C93 RID: 3219
		public class MechdusaKill : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600609E RID: 24734 RVA: 0x006D2698 File Offset: 0x006D0898
			public bool CanDrop(DropAttemptInfo info)
			{
				if (!Main.remixWorld || !Main.getGoodWorld)
				{
					return false;
				}
				for (int i = 0; i < Conditions.MechdusaKill._targetList.Length; i++)
				{
					if (Conditions.MechdusaKill._targetList[i] != info.npc.type && NPC.AnyNPCs(Conditions.MechdusaKill._targetList[i]))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600609F RID: 24735 RVA: 0x006D26EC File Offset: 0x006D08EC
			public bool CanShowItemDropInUI()
			{
				return Main.remixWorld && Main.getGoodWorld;
			}

			// Token: 0x060060A0 RID: 24736 RVA: 0x006D26FC File Offset: 0x006D08FC
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x040079EE RID: 31214
			private static int[] _targetList = new int[]
			{
				127,
				126,
				125,
				134
			};
		}

		// Token: 0x02000C94 RID: 3220
		public class LegacyHack_IsBossAndExpert : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060060A3 RID: 24739 RVA: 0x006D271F File Offset: 0x006D091F
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.boss && Main.expertMode;
			}

			// Token: 0x060060A4 RID: 24740 RVA: 0x006D2735 File Offset: 0x006D0935
			public bool CanShowItemDropInUI()
			{
				return Main.expertMode;
			}

			// Token: 0x060060A5 RID: 24741 RVA: 0x006D273C File Offset: 0x006D093C
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.LegacyHack_IsBossAndExpert");
			}
		}

		// Token: 0x02000C95 RID: 3221
		public class LegacyHack_IsBossAndNotExpert : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060060A7 RID: 24743 RVA: 0x006D2750 File Offset: 0x006D0950
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.boss && !Main.expertMode;
			}

			// Token: 0x060060A8 RID: 24744 RVA: 0x006D2769 File Offset: 0x006D0969
			public bool CanShowItemDropInUI()
			{
				return !Main.expertMode;
			}

			// Token: 0x060060A9 RID: 24745 RVA: 0x006D2773 File Offset: 0x006D0973
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.LegacyHack_IsBossAndNotExpert");
			}
		}

		// Token: 0x02000C96 RID: 3222
		public class LegacyHack_IsABoss : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060060AB RID: 24747 RVA: 0x006D2787 File Offset: 0x006D0987
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.boss;
			}

			// Token: 0x060060AC RID: 24748 RVA: 0x006D2794 File Offset: 0x006D0994
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060060AD RID: 24749 RVA: 0x006D2797 File Offset: 0x006D0997
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000C97 RID: 3223
		public class IsExpert : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060060AF RID: 24751 RVA: 0x006D27A2 File Offset: 0x006D09A2
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.expertMode;
			}

			// Token: 0x060060B0 RID: 24752 RVA: 0x006D27A9 File Offset: 0x006D09A9
			public bool CanShowItemDropInUI()
			{
				return Main.expertMode;
			}

			// Token: 0x060060B1 RID: 24753 RVA: 0x006D27B0 File Offset: 0x006D09B0
			public string GetConditionDescription()
			{
				if (Main.masterMode)
				{
					return Language.GetTextValue("Bestiary_ItemDropConditions.IsMasterMode");
				}
				return Language.GetTextValue("Bestiary_ItemDropConditions.IsExpert");
			}
		}

		// Token: 0x02000C98 RID: 3224
		public class IsMasterMode : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060060B3 RID: 24755 RVA: 0x006D27D6 File Offset: 0x006D09D6
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.masterMode;
			}

			// Token: 0x060060B4 RID: 24756 RVA: 0x006D27DD File Offset: 0x006D09DD
			public bool CanShowItemDropInUI()
			{
				return Main.masterMode;
			}

			// Token: 0x060060B5 RID: 24757 RVA: 0x006D27E4 File Offset: 0x006D09E4
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.IsMasterMode");
			}
		}

		// Token: 0x02000C99 RID: 3225
		public class IsCrimson : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060060B7 RID: 24759 RVA: 0x006D27F8 File Offset: 0x006D09F8
			public bool CanDrop(DropAttemptInfo info)
			{
				return WorldGen.crimson;
			}

			// Token: 0x060060B8 RID: 24760 RVA: 0x006D27FF File Offset: 0x006D09FF
			public bool CanShowItemDropInUI()
			{
				return WorldGen.crimson;
			}

			// Token: 0x060060B9 RID: 24761 RVA: 0x006D2806 File Offset: 0x006D0A06
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.IsCrimson");
			}
		}

		// Token: 0x02000C9A RID: 3226
		public class IsCorruption : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060060BB RID: 24763 RVA: 0x006D281A File Offset: 0x006D0A1A
			public bool CanDrop(DropAttemptInfo info)
			{
				return !WorldGen.crimson;
			}

			// Token: 0x060060BC RID: 24764 RVA: 0x006D2824 File Offset: 0x006D0A24
			public bool CanShowItemDropInUI()
			{
				return !WorldGen.crimson;
			}

			// Token: 0x060060BD RID: 24765 RVA: 0x006D282E File Offset: 0x006D0A2E
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.IsCorruption");
			}
		}

		// Token: 0x02000C9B RID: 3227
		public class IsCrimsonAndNotExpert : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060060BF RID: 24767 RVA: 0x006D2842 File Offset: 0x006D0A42
			public bool CanDrop(DropAttemptInfo info)
			{
				return WorldGen.crimson && !Main.expertMode;
			}

			// Token: 0x060060C0 RID: 24768 RVA: 0x006D2855 File Offset: 0x006D0A55
			public bool CanShowItemDropInUI()
			{
				return WorldGen.crimson && !Main.expertMode;
			}

			// Token: 0x060060C1 RID: 24769 RVA: 0x006D2868 File Offset: 0x006D0A68
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.IsCrimsonAndNotExpert");
			}
		}

		// Token: 0x02000C9C RID: 3228
		public class IsCorruptionAndNotExpert : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060060C3 RID: 24771 RVA: 0x006D287C File Offset: 0x006D0A7C
			public bool CanDrop(DropAttemptInfo info)
			{
				return !WorldGen.crimson && !Main.expertMode;
			}

			// Token: 0x060060C4 RID: 24772 RVA: 0x006D288F File Offset: 0x006D0A8F
			public bool CanShowItemDropInUI()
			{
				return !WorldGen.crimson && !Main.expertMode;
			}

			// Token: 0x060060C5 RID: 24773 RVA: 0x006D28A2 File Offset: 0x006D0AA2
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.IsCorruptionAndNotExpert");
			}
		}

		// Token: 0x02000C9D RID: 3229
		public class HalloweenWeapons : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060060C7 RID: 24775 RVA: 0x006D28B8 File Offset: 0x006D0AB8
			public bool CanDrop(DropAttemptInfo info)
			{
				float num = 500f * Main.GameModeInfo.EnemyMoneyDropMultiplier;
				float num2 = 40f * Main.GameModeInfo.EnemyDamageMultiplier;
				float num3 = 20f * Main.GameModeInfo.EnemyDefenseMultiplier;
				return Main.halloween && info.npc.value > 0f && info.npc.value < num && (float)info.npc.damage < num2 && (float)info.npc.defense < num3 && !info.IsInSimulation;
			}

			// Token: 0x060060C8 RID: 24776 RVA: 0x006D2951 File Offset: 0x006D0B51
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060060C9 RID: 24777 RVA: 0x006D2954 File Offset: 0x006D0B54
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.HalloweenWeapons");
			}
		}

		// Token: 0x02000C9E RID: 3230
		public class SoulOfNight : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060060CB RID: 24779 RVA: 0x006D2968 File Offset: 0x006D0B68
			public bool CanDrop(DropAttemptInfo info)
			{
				return Conditions.SoulOfWhateverConditionCanDrop(info) && (info.player.ZoneCorrupt || info.player.ZoneCrimson);
			}

			// Token: 0x060060CC RID: 24780 RVA: 0x006D298E File Offset: 0x006D0B8E
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060060CD RID: 24781 RVA: 0x006D2991 File Offset: 0x006D0B91
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.SoulOfNight");
			}
		}

		// Token: 0x02000C9F RID: 3231
		public class SoulOfLight : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060060CF RID: 24783 RVA: 0x006D29A5 File Offset: 0x006D0BA5
			public bool CanDrop(DropAttemptInfo info)
			{
				return Conditions.SoulOfWhateverConditionCanDrop(info) && info.player.ZoneHallow;
			}

			// Token: 0x060060D0 RID: 24784 RVA: 0x006D29BC File Offset: 0x006D0BBC
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060060D1 RID: 24785 RVA: 0x006D29BF File Offset: 0x006D0BBF
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.SoulOfLight");
			}
		}

		// Token: 0x02000CA0 RID: 3232
		public class NotFromStatue : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060060D3 RID: 24787 RVA: 0x006D29D3 File Offset: 0x006D0BD3
			public bool CanDrop(DropAttemptInfo info)
			{
				return !info.npc.SpawnedFromStatue;
			}

			// Token: 0x060060D4 RID: 24788 RVA: 0x006D29E3 File Offset: 0x006D0BE3
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060060D5 RID: 24789 RVA: 0x006D29E6 File Offset: 0x006D0BE6
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.NotFromStatue");
			}
		}

		// Token: 0x02000CA1 RID: 3233
		public class HalloweenGoodieBagDrop : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060060D7 RID: 24791 RVA: 0x006D29FC File Offset: 0x006D0BFC
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.halloween && info.npc.lifeMax > 1 && info.npc.damage > 0 && !info.npc.friendly && info.npc.type != 121 && info.npc.type != 23 && info.npc.value > 0f && !info.IsInSimulation;
			}

			// Token: 0x060060D8 RID: 24792 RVA: 0x006D2A74 File Offset: 0x006D0C74
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060060D9 RID: 24793 RVA: 0x006D2A77 File Offset: 0x006D0C77
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.HalloweenGoodieBagDrop");
			}
		}

		// Token: 0x02000CA2 RID: 3234
		public class XmasPresentDrop : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060060DB RID: 24795 RVA: 0x006D2A8C File Offset: 0x006D0C8C
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.xMas && info.npc.lifeMax > 1 && info.npc.damage > 0 && !info.npc.friendly && info.npc.type != 121 && info.npc.type != 23 && info.npc.value > 0f && !info.IsInSimulation;
			}

			// Token: 0x060060DC RID: 24796 RVA: 0x006D2B04 File Offset: 0x006D0D04
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060060DD RID: 24797 RVA: 0x006D2B07 File Offset: 0x006D0D07
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.XmasPresentDrop");
			}
		}

		// Token: 0x02000CA3 RID: 3235
		public class LivingFlames : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060060DF RID: 24799 RVA: 0x006D2B1C File Offset: 0x006D0D1C
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.lifeMax > 5 && info.npc.value > 0f && !info.npc.friendly && Main.hardMode && info.npc.position.Y / 16f > (float)Main.UnderworldLayer && !info.IsInSimulation;
			}

			// Token: 0x060060E0 RID: 24800 RVA: 0x006D2B86 File Offset: 0x006D0D86
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060060E1 RID: 24801 RVA: 0x006D2B89 File Offset: 0x006D0D89
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.LivingFlames");
			}
		}

		// Token: 0x02000CA4 RID: 3236
		public class NamedNPC : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060060E3 RID: 24803 RVA: 0x006D2B9D File Offset: 0x006D0D9D
			public NamedNPC(string neededName)
			{
				this.neededName = neededName;
			}

			// Token: 0x060060E4 RID: 24804 RVA: 0x006D2BAC File Offset: 0x006D0DAC
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.GivenOrTypeName == this.neededName;
			}

			// Token: 0x060060E5 RID: 24805 RVA: 0x006D2BC4 File Offset: 0x006D0DC4
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060060E6 RID: 24806 RVA: 0x006D2BC7 File Offset: 0x006D0DC7
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.NamedNPC");
			}

			// Token: 0x040079EF RID: 31215
			public string neededName;
		}

		// Token: 0x02000CA5 RID: 3237
		public class HallowKeyCondition : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060060E7 RID: 24807 RVA: 0x006D2BD3 File Offset: 0x006D0DD3
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.value > 0f && Main.hardMode && !info.IsInSimulation && info.player.ZoneHallow;
			}

			// Token: 0x060060E8 RID: 24808 RVA: 0x006D2C03 File Offset: 0x006D0E03
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060060E9 RID: 24809 RVA: 0x006D2C06 File Offset: 0x006D0E06
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.HallowKeyCondition");
			}
		}

		// Token: 0x02000CA6 RID: 3238
		public class JungleKeyCondition : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060060EB RID: 24811 RVA: 0x006D2C1A File Offset: 0x006D0E1A
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.value > 0f && Main.hardMode && !info.IsInSimulation && info.player.ZoneJungle;
			}

			// Token: 0x060060EC RID: 24812 RVA: 0x006D2C4A File Offset: 0x006D0E4A
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060060ED RID: 24813 RVA: 0x006D2C4D File Offset: 0x006D0E4D
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.JungleKeyCondition");
			}
		}

		// Token: 0x02000CA7 RID: 3239
		public class CorruptKeyCondition : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060060EF RID: 24815 RVA: 0x006D2C61 File Offset: 0x006D0E61
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.value > 0f && Main.hardMode && !info.IsInSimulation && info.player.ZoneCorrupt;
			}

			// Token: 0x060060F0 RID: 24816 RVA: 0x006D2C91 File Offset: 0x006D0E91
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060060F1 RID: 24817 RVA: 0x006D2C94 File Offset: 0x006D0E94
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.CorruptKeyCondition");
			}
		}

		// Token: 0x02000CA8 RID: 3240
		public class CrimsonKeyCondition : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060060F3 RID: 24819 RVA: 0x006D2CA8 File Offset: 0x006D0EA8
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.value > 0f && Main.hardMode && !info.IsInSimulation && info.player.ZoneCrimson;
			}

			// Token: 0x060060F4 RID: 24820 RVA: 0x006D2CD8 File Offset: 0x006D0ED8
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060060F5 RID: 24821 RVA: 0x006D2CDB File Offset: 0x006D0EDB
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.CrimsonKeyCondition");
			}
		}

		// Token: 0x02000CA9 RID: 3241
		public class FrozenKeyCondition : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060060F7 RID: 24823 RVA: 0x006D2CEF File Offset: 0x006D0EEF
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.value > 0f && Main.hardMode && !info.IsInSimulation && info.player.ZoneSnow;
			}

			// Token: 0x060060F8 RID: 24824 RVA: 0x006D2D1F File Offset: 0x006D0F1F
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060060F9 RID: 24825 RVA: 0x006D2D22 File Offset: 0x006D0F22
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.FrozenKeyCondition");
			}
		}

		// Token: 0x02000CAA RID: 3242
		public class DesertKeyCondition : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060060FB RID: 24827 RVA: 0x006D2D36 File Offset: 0x006D0F36
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.value > 0f && Main.hardMode && !info.IsInSimulation && info.player.ZoneDesert && !info.player.ZoneBeach;
			}

			// Token: 0x060060FC RID: 24828 RVA: 0x006D2D76 File Offset: 0x006D0F76
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060060FD RID: 24829 RVA: 0x006D2D79 File Offset: 0x006D0F79
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.DesertKeyCondition");
			}
		}

		// Token: 0x02000CAB RID: 3243
		public class BeatAnyMechBoss : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060060FF RID: 24831 RVA: 0x006D2D8D File Offset: 0x006D0F8D
			public bool CanDrop(DropAttemptInfo info)
			{
				return NPC.downedMechBossAny;
			}

			// Token: 0x06006100 RID: 24832 RVA: 0x006D2D94 File Offset: 0x006D0F94
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06006101 RID: 24833 RVA: 0x006D2D97 File Offset: 0x006D0F97
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.BeatAnyMechBoss");
			}
		}

		// Token: 0x02000CAC RID: 3244
		public class YoyoCascade : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06006103 RID: 24835 RVA: 0x006D2DAC File Offset: 0x006D0FAC
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.hardMode && info.npc.HasPlayerTarget && info.npc.lifeMax > 5 && !info.npc.friendly && info.npc.value > 0f && info.npc.position.Y / 16f > (float)(Main.maxTilesY - 350) && NPC.downedBoss3 && !info.IsInSimulation;
			}

			// Token: 0x06006104 RID: 24836 RVA: 0x006D2E30 File Offset: 0x006D1030
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06006105 RID: 24837 RVA: 0x006D2E33 File Offset: 0x006D1033
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.YoyoCascade");
			}
		}

		// Token: 0x02000CAD RID: 3245
		public class YoyosAmarok : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06006107 RID: 24839 RVA: 0x006D2E48 File Offset: 0x006D1048
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.hardMode && info.npc.HasPlayerTarget && info.player.ZoneSnow && info.npc.lifeMax > 5 && !info.npc.friendly && info.npc.value > 0f && !info.IsInSimulation;
			}

			// Token: 0x06006108 RID: 24840 RVA: 0x006D2EAE File Offset: 0x006D10AE
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06006109 RID: 24841 RVA: 0x006D2EB1 File Offset: 0x006D10B1
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.YoyosAmarok");
			}
		}

		// Token: 0x02000CAE RID: 3246
		public class YoyosYelets : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600610B RID: 24843 RVA: 0x006D2EC8 File Offset: 0x006D10C8
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.hardMode && info.player.ZoneJungle && NPC.downedMechBossAny && info.npc.lifeMax > 5 && info.npc.HasPlayerTarget && !info.npc.friendly && info.npc.value > 0f && !info.IsInSimulation;
			}

			// Token: 0x0600610C RID: 24844 RVA: 0x006D2F35 File Offset: 0x006D1135
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600610D RID: 24845 RVA: 0x006D2F38 File Offset: 0x006D1138
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.YoyosYelets");
			}
		}

		// Token: 0x02000CAF RID: 3247
		public class YoyosKraken : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600610F RID: 24847 RVA: 0x006D2F4C File Offset: 0x006D114C
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.hardMode && info.player.ZoneDungeon && NPC.downedPlantBoss && info.npc.lifeMax > 5 && info.npc.HasPlayerTarget && !info.npc.friendly && info.npc.value > 0f && !info.IsInSimulation;
			}

			// Token: 0x06006110 RID: 24848 RVA: 0x006D2FB9 File Offset: 0x006D11B9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06006111 RID: 24849 RVA: 0x006D2FBC File Offset: 0x006D11BC
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.YoyosKraken");
			}
		}

		// Token: 0x02000CB0 RID: 3248
		public class YoyosHelFire : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06006113 RID: 24851 RVA: 0x006D2FD0 File Offset: 0x006D11D0
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.hardMode && !info.player.ZoneDungeon && (double)(info.npc.position.Y / 16f) > (Main.rockLayer + (double)(Main.maxTilesY * 2)) / 3.0 && info.npc.lifeMax > 5 && info.npc.HasPlayerTarget && !info.npc.friendly && info.npc.value > 0f && !info.IsInSimulation;
			}

			// Token: 0x06006114 RID: 24852 RVA: 0x006D306A File Offset: 0x006D126A
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06006115 RID: 24853 RVA: 0x006D306D File Offset: 0x006D126D
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.YoyosHelFire");
			}
		}

		// Token: 0x02000CB1 RID: 3249
		public class WindyEnoughForKiteDrops : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06006117 RID: 24855 RVA: 0x006D3081 File Offset: 0x006D1281
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.WindyEnoughForKiteDrops;
			}

			// Token: 0x06006118 RID: 24856 RVA: 0x006D3088 File Offset: 0x006D1288
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06006119 RID: 24857 RVA: 0x006D308B File Offset: 0x006D128B
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.IsItAHappyWindyDay");
			}
		}

		// Token: 0x02000CB2 RID: 3250
		public class RemixSeedEasymode : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600611B RID: 24859 RVA: 0x006D309F File Offset: 0x006D129F
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.remixWorld && !Main.hardMode;
			}

			// Token: 0x0600611C RID: 24860 RVA: 0x006D30B2 File Offset: 0x006D12B2
			public bool CanShowItemDropInUI()
			{
				return Main.remixWorld && !Main.hardMode;
			}

			// Token: 0x0600611D RID: 24861 RVA: 0x006D30C5 File Offset: 0x006D12C5
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000CB3 RID: 3251
		public class RemixSeedHardmode : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600611F RID: 24863 RVA: 0x006D30D0 File Offset: 0x006D12D0
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.remixWorld && Main.hardMode;
			}

			// Token: 0x06006120 RID: 24864 RVA: 0x006D30E0 File Offset: 0x006D12E0
			public bool CanShowItemDropInUI()
			{
				return Main.remixWorld && Main.hardMode;
			}

			// Token: 0x06006121 RID: 24865 RVA: 0x006D30F0 File Offset: 0x006D12F0
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000CB4 RID: 3252
		public class RemixSeed : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06006123 RID: 24867 RVA: 0x006D30FB File Offset: 0x006D12FB
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.remixWorld;
			}

			// Token: 0x06006124 RID: 24868 RVA: 0x006D3102 File Offset: 0x006D1302
			public bool CanShowItemDropInUI()
			{
				return Main.remixWorld;
			}

			// Token: 0x06006125 RID: 24869 RVA: 0x006D3109 File Offset: 0x006D1309
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000CB5 RID: 3253
		public class NotRemixSeed : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06006127 RID: 24871 RVA: 0x006D3114 File Offset: 0x006D1314
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.remixWorld;
			}

			// Token: 0x06006128 RID: 24872 RVA: 0x006D311E File Offset: 0x006D131E
			public bool CanShowItemDropInUI()
			{
				return !Main.remixWorld;
			}

			// Token: 0x06006129 RID: 24873 RVA: 0x006D3128 File Offset: 0x006D1328
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000CB6 RID: 3254
		public class TenthAnniversaryIsUp : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600612B RID: 24875 RVA: 0x006D3133 File Offset: 0x006D1333
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.tenthAnniversaryWorld;
			}

			// Token: 0x0600612C RID: 24876 RVA: 0x006D313A File Offset: 0x006D133A
			public bool CanShowItemDropInUI()
			{
				return Main.tenthAnniversaryWorld;
			}

			// Token: 0x0600612D RID: 24877 RVA: 0x006D3141 File Offset: 0x006D1341
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000CB7 RID: 3255
		public class TenthAnniversaryIsNotUp : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600612F RID: 24879 RVA: 0x006D314C File Offset: 0x006D134C
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.tenthAnniversaryWorld;
			}

			// Token: 0x06006130 RID: 24880 RVA: 0x006D3156 File Offset: 0x006D1356
			public bool CanShowItemDropInUI()
			{
				return !Main.tenthAnniversaryWorld;
			}

			// Token: 0x06006131 RID: 24881 RVA: 0x006D3160 File Offset: 0x006D1360
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000CB8 RID: 3256
		public class DontStarveIsUp : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06006133 RID: 24883 RVA: 0x006D316B File Offset: 0x006D136B
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.dontStarveWorld;
			}

			// Token: 0x06006134 RID: 24884 RVA: 0x006D3172 File Offset: 0x006D1372
			public bool CanShowItemDropInUI()
			{
				return Main.dontStarveWorld;
			}

			// Token: 0x06006135 RID: 24885 RVA: 0x006D3179 File Offset: 0x006D1379
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000CB9 RID: 3257
		public class DontStarveIsNotUp : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06006137 RID: 24887 RVA: 0x006D3184 File Offset: 0x006D1384
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.dontStarveWorld;
			}

			// Token: 0x06006138 RID: 24888 RVA: 0x006D318E File Offset: 0x006D138E
			public bool CanShowItemDropInUI()
			{
				return !Main.dontStarveWorld;
			}

			// Token: 0x06006139 RID: 24889 RVA: 0x006D3198 File Offset: 0x006D1398
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000CBA RID: 3258
		public class NotUsedDemonHeart : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600613B RID: 24891 RVA: 0x006D31A3 File Offset: 0x006D13A3
			public bool CanDrop(DropAttemptInfo info)
			{
				return !info.player.extraAccessory;
			}

			// Token: 0x0600613C RID: 24892 RVA: 0x006D31B3 File Offset: 0x006D13B3
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600613D RID: 24893 RVA: 0x006D31B6 File Offset: 0x006D13B6
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000CBB RID: 3259
		public class NoPortalGun : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600613F RID: 24895 RVA: 0x006D31C1 File Offset: 0x006D13C1
			public bool CanDrop(DropAttemptInfo info)
			{
				return !info.player.HasItem(3384);
			}

			// Token: 0x06006140 RID: 24896 RVA: 0x006D31D6 File Offset: 0x006D13D6
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06006141 RID: 24897 RVA: 0x006D31D9 File Offset: 0x006D13D9
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000CBC RID: 3260
		public class IsPreHardmode : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06006143 RID: 24899 RVA: 0x006D31E4 File Offset: 0x006D13E4
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.hardMode;
			}

			// Token: 0x06006144 RID: 24900 RVA: 0x006D31EE File Offset: 0x006D13EE
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06006145 RID: 24901 RVA: 0x006D31F1 File Offset: 0x006D13F1
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000CBD RID: 3261
		public class DrunkWorldIsUp : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06006147 RID: 24903 RVA: 0x006D31FC File Offset: 0x006D13FC
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.drunkWorld;
			}

			// Token: 0x06006148 RID: 24904 RVA: 0x006D3203 File Offset: 0x006D1403
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06006149 RID: 24905 RVA: 0x006D3206 File Offset: 0x006D1406
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000CBE RID: 3262
		public class ForTheWorthyIsUp : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600614B RID: 24907 RVA: 0x006D3211 File Offset: 0x006D1411
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.getGoodWorld;
			}

			// Token: 0x0600614C RID: 24908 RVA: 0x006D3218 File Offset: 0x006D1418
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600614D RID: 24909 RVA: 0x006D321B File Offset: 0x006D141B
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000CBF RID: 3263
		public class BeesSeed : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600614F RID: 24911 RVA: 0x006D3226 File Offset: 0x006D1426
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.notTheBeesWorld;
			}

			// Token: 0x06006150 RID: 24912 RVA: 0x006D322D File Offset: 0x006D142D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06006151 RID: 24913 RVA: 0x006D3230 File Offset: 0x006D1430
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000CC0 RID: 3264
		public class NoTrapsSeed : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06006153 RID: 24915 RVA: 0x006D323B File Offset: 0x006D143B
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.noTrapsWorld;
			}

			// Token: 0x06006154 RID: 24916 RVA: 0x006D3242 File Offset: 0x006D1442
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06006155 RID: 24917 RVA: 0x006D3245 File Offset: 0x006D1445
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000CC1 RID: 3265
		public class ZenithSeedIsUp : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06006157 RID: 24919 RVA: 0x006D3250 File Offset: 0x006D1450
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.zenithWorld;
			}

			// Token: 0x06006158 RID: 24920 RVA: 0x006D3257 File Offset: 0x006D1457
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06006159 RID: 24921 RVA: 0x006D325A File Offset: 0x006D145A
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000CC2 RID: 3266
		public class DrunkWorldIsNotUp : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600615B RID: 24923 RVA: 0x006D3265 File Offset: 0x006D1465
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.drunkWorld;
			}

			// Token: 0x0600615C RID: 24924 RVA: 0x006D326F File Offset: 0x006D146F
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600615D RID: 24925 RVA: 0x006D3272 File Offset: 0x006D1472
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000CC3 RID: 3267
		public class ForTheWorthyIsNotUp : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600615F RID: 24927 RVA: 0x006D327D File Offset: 0x006D147D
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.getGoodWorld;
			}

			// Token: 0x06006160 RID: 24928 RVA: 0x006D3287 File Offset: 0x006D1487
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06006161 RID: 24929 RVA: 0x006D328A File Offset: 0x006D148A
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000CC4 RID: 3268
		public class NotBeesSeed : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06006163 RID: 24931 RVA: 0x006D3295 File Offset: 0x006D1495
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.notTheBeesWorld;
			}

			// Token: 0x06006164 RID: 24932 RVA: 0x006D329F File Offset: 0x006D149F
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06006165 RID: 24933 RVA: 0x006D32A2 File Offset: 0x006D14A2
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000CC5 RID: 3269
		public class NotNoTrapsSeed : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06006167 RID: 24935 RVA: 0x006D32AD File Offset: 0x006D14AD
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.noTrapsWorld;
			}

			// Token: 0x06006168 RID: 24936 RVA: 0x006D32B7 File Offset: 0x006D14B7
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06006169 RID: 24937 RVA: 0x006D32BA File Offset: 0x006D14BA
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000CC6 RID: 3270
		public class ZenithSeedIsNotUp : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600616B RID: 24939 RVA: 0x006D32C5 File Offset: 0x006D14C5
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.zenithWorld;
			}

			// Token: 0x0600616C RID: 24940 RVA: 0x006D32CF File Offset: 0x006D14CF
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600616D RID: 24941 RVA: 0x006D32D2 File Offset: 0x006D14D2
			public string GetConditionDescription()
			{
				return null;
			}
		}
	}
}
