using System;
using Terraria.Localization;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x0200029D RID: 669
	public class Conditions
	{
		// Token: 0x06002085 RID: 8325 RVA: 0x00519378 File Offset: 0x00517578
		public static bool SoulOfWhateverConditionCanDrop(DropAttemptInfo info)
		{
			if (info.npc.boss)
			{
				return false;
			}
			int type = info.npc.type;
			if (type <= 15)
			{
				if (type != 1 && type - 13 > 2)
				{
					goto IL_3C;
				}
			}
			else if (type != 121 && type != 535)
			{
				goto IL_3C;
			}
			return false;
			IL_3C:
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

		// Token: 0x0200064E RID: 1614
		public class NeverTrue : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600341A RID: 13338 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
			public bool CanDrop(DropAttemptInfo info)
			{
				return false;
			}

			// Token: 0x0600341B RID: 13339 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600341C RID: 13340 RVA: 0x0006A9EF File Offset: 0x00068BEF
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x0200064F RID: 1615
		public class IsUsingSpecificAIValues : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600341E RID: 13342 RVA: 0x006078CD File Offset: 0x00605ACD
			public IsUsingSpecificAIValues(int aislot, float valueToMatch)
			{
				this.aiSlotToCheck = aislot;
				this.valueToMatch = valueToMatch;
			}

			// Token: 0x0600341F RID: 13343 RVA: 0x006078E3 File Offset: 0x00605AE3
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.ai[this.aiSlotToCheck] == this.valueToMatch;
			}

			// Token: 0x06003420 RID: 13344 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06003421 RID: 13345 RVA: 0x0006A9EF File Offset: 0x00068BEF
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x0400617D RID: 24957
			public int aiSlotToCheck;

			// Token: 0x0400617E RID: 24958
			public float valueToMatch;
		}

		// Token: 0x02000650 RID: 1616
		public class FrostMoonDropGatingChance : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06003422 RID: 13346 RVA: 0x00607900 File Offset: 0x00605B00
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

			// Token: 0x06003423 RID: 13347 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06003424 RID: 13348 RVA: 0x00607958 File Offset: 0x00605B58
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.WaveBasedDrop");
			}
		}

		// Token: 0x02000651 RID: 1617
		public class PumpkinMoonDropGatingChance : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06003426 RID: 13350 RVA: 0x00607964 File Offset: 0x00605B64
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

			// Token: 0x06003427 RID: 13351 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06003428 RID: 13352 RVA: 0x00607958 File Offset: 0x00605B58
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.WaveBasedDrop");
			}
		}

		// Token: 0x02000652 RID: 1618
		public class FrostMoonDropGateForTrophies : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600342A RID: 13354 RVA: 0x006079BC File Offset: 0x00605BBC
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

			// Token: 0x0600342B RID: 13355 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600342C RID: 13356 RVA: 0x0006A9EF File Offset: 0x00068BEF
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000653 RID: 1619
		public class PumpkinMoonDropGateForTrophies : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600342E RID: 13358 RVA: 0x00607A30 File Offset: 0x00605C30
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

			// Token: 0x0600342F RID: 13359 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06003430 RID: 13360 RVA: 0x0006A9EF File Offset: 0x00068BEF
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000654 RID: 1620
		public class IsPumpkinMoon : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06003432 RID: 13362 RVA: 0x00607AA3 File Offset: 0x00605CA3
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.pumpkinMoon;
			}

			// Token: 0x06003433 RID: 13363 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06003434 RID: 13364 RVA: 0x0006A9EF File Offset: 0x00068BEF
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000655 RID: 1621
		public class FromCertainWaveAndAbove : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06003436 RID: 13366 RVA: 0x00607AAA File Offset: 0x00605CAA
			public FromCertainWaveAndAbove(int neededWave)
			{
				this.neededWave = neededWave;
			}

			// Token: 0x06003437 RID: 13367 RVA: 0x00607AB9 File Offset: 0x00605CB9
			public bool CanDrop(DropAttemptInfo info)
			{
				return NPC.waveNumber >= this.neededWave;
			}

			// Token: 0x06003438 RID: 13368 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06003439 RID: 13369 RVA: 0x00607ACB File Offset: 0x00605CCB
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.PastWaveBasedDrop", this.neededWave);
			}

			// Token: 0x0400617F RID: 24959
			public int neededWave;
		}

		// Token: 0x02000656 RID: 1622
		public class IsBloodMoonAndNotFromStatue : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600343A RID: 13370 RVA: 0x00607AE2 File Offset: 0x00605CE2
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.dayTime && Main.bloodMoon && !info.npc.SpawnedFromStatue && !info.IsInSimulation;
			}

			// Token: 0x0600343B RID: 13371 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600343C RID: 13372 RVA: 0x0006A9EF File Offset: 0x00068BEF
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000657 RID: 1623
		public class DownedAllMechBosses : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600343E RID: 13374 RVA: 0x00607B0A File Offset: 0x00605D0A
			public bool CanDrop(DropAttemptInfo info)
			{
				return NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3;
			}

			// Token: 0x0600343F RID: 13375 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06003440 RID: 13376 RVA: 0x0006A9EF File Offset: 0x00068BEF
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000658 RID: 1624
		public class DownedPlantera : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06003442 RID: 13378 RVA: 0x00607B21 File Offset: 0x00605D21
			public bool CanDrop(DropAttemptInfo info)
			{
				return NPC.downedPlantBoss;
			}

			// Token: 0x06003443 RID: 13379 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06003444 RID: 13380 RVA: 0x0006A9EF File Offset: 0x00068BEF
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000659 RID: 1625
		public class IsHardmode : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06003446 RID: 13382 RVA: 0x00607B28 File Offset: 0x00605D28
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.hardMode;
			}

			// Token: 0x06003447 RID: 13383 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06003448 RID: 13384 RVA: 0x0006A9EF File Offset: 0x00068BEF
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x0200065A RID: 1626
		public class FirstTimeKillingPlantera : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600344A RID: 13386 RVA: 0x00607B2F File Offset: 0x00605D2F
			public bool CanDrop(DropAttemptInfo info)
			{
				return !NPC.downedPlantBoss;
			}

			// Token: 0x0600344B RID: 13387 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600344C RID: 13388 RVA: 0x0006A9EF File Offset: 0x00068BEF
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x0200065B RID: 1627
		public class MechanicalBossesDummyCondition : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600344E RID: 13390 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanDrop(DropAttemptInfo info)
			{
				return true;
			}

			// Token: 0x0600344F RID: 13391 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06003450 RID: 13392 RVA: 0x0006A9EF File Offset: 0x00068BEF
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x0200065C RID: 1628
		public class PirateMap : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06003452 RID: 13394 RVA: 0x00607B3C File Offset: 0x00605D3C
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.value > 0f && Main.hardMode && (double)(info.npc.position.Y / 16f) < Main.worldSurface + 10.0 && (info.npc.Center.X / 16f < 380f || info.npc.Center.X / 16f > (float)(Main.maxTilesX - 380)) && !info.IsInSimulation;
			}

			// Token: 0x06003453 RID: 13395 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06003454 RID: 13396 RVA: 0x00607BD6 File Offset: 0x00605DD6
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.PirateMap");
			}
		}

		// Token: 0x0200065D RID: 1629
		public class IsChristmas : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06003456 RID: 13398 RVA: 0x00607BE2 File Offset: 0x00605DE2
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.xMas;
			}

			// Token: 0x06003457 RID: 13399 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06003458 RID: 13400 RVA: 0x00607BE9 File Offset: 0x00605DE9
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.IsChristmas");
			}
		}

		// Token: 0x0200065E RID: 1630
		public class NotExpert : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600345A RID: 13402 RVA: 0x00607BF5 File Offset: 0x00605DF5
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.expertMode;
			}

			// Token: 0x0600345B RID: 13403 RVA: 0x00607BF5 File Offset: 0x00605DF5
			public bool CanShowItemDropInUI()
			{
				return !Main.expertMode;
			}

			// Token: 0x0600345C RID: 13404 RVA: 0x00607BFF File Offset: 0x00605DFF
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.NotExpert");
			}
		}

		// Token: 0x0200065F RID: 1631
		public class NotMasterMode : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600345E RID: 13406 RVA: 0x00607C0B File Offset: 0x00605E0B
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.masterMode;
			}

			// Token: 0x0600345F RID: 13407 RVA: 0x00607C0B File Offset: 0x00605E0B
			public bool CanShowItemDropInUI()
			{
				return !Main.masterMode;
			}

			// Token: 0x06003460 RID: 13408 RVA: 0x00607C15 File Offset: 0x00605E15
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.NotMasterMode");
			}
		}

		// Token: 0x02000660 RID: 1632
		public class MissingTwin : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06003462 RID: 13410 RVA: 0x00607C24 File Offset: 0x00605E24
			public bool CanDrop(DropAttemptInfo info)
			{
				int type = 125;
				if (info.npc.type == 125)
				{
					type = 126;
				}
				return !NPC.AnyNPCs(type);
			}

			// Token: 0x06003463 RID: 13411 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06003464 RID: 13412 RVA: 0x0006A9EF File Offset: 0x00068BEF
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000661 RID: 1633
		public class EmpressOfLightIsGenuinelyEnraged : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06003466 RID: 13414 RVA: 0x00607C4F File Offset: 0x00605E4F
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.AI_120_HallowBoss_IsGenuinelyEnraged();
			}

			// Token: 0x06003467 RID: 13415 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06003468 RID: 13416 RVA: 0x00607C5C File Offset: 0x00605E5C
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.EmpressOfLightOnlyTookDamageWhileEnraged");
			}
		}

		// Token: 0x02000662 RID: 1634
		public class PlayerNeedsHealing : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600346A RID: 13418 RVA: 0x00607C68 File Offset: 0x00605E68
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.player.statLife < info.player.statLifeMax2;
			}

			// Token: 0x0600346B RID: 13419 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600346C RID: 13420 RVA: 0x00607C82 File Offset: 0x00605E82
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.PlayerNeedsHealing");
			}
		}

		// Token: 0x02000663 RID: 1635
		public class MechdusaKill : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600346E RID: 13422 RVA: 0x00607C90 File Offset: 0x00605E90
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

			// Token: 0x0600346F RID: 13423 RVA: 0x00607CE7 File Offset: 0x00605EE7
			public bool CanShowItemDropInUI()
			{
				return Main.remixWorld && Main.getGoodWorld;
			}

			// Token: 0x06003470 RID: 13424 RVA: 0x0006A9EF File Offset: 0x00068BEF
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x04006180 RID: 24960
			private static int[] _targetList = new int[]
			{
				127,
				126,
				125,
				134
			};
		}

		// Token: 0x02000664 RID: 1636
		public class LegacyHack_IsBossAndExpert : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06003473 RID: 13427 RVA: 0x00607D0F File Offset: 0x00605F0F
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.boss && Main.expertMode;
			}

			// Token: 0x06003474 RID: 13428 RVA: 0x00607D25 File Offset: 0x00605F25
			public bool CanShowItemDropInUI()
			{
				return Main.expertMode;
			}

			// Token: 0x06003475 RID: 13429 RVA: 0x00607D2C File Offset: 0x00605F2C
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.LegacyHack_IsBossAndExpert");
			}
		}

		// Token: 0x02000665 RID: 1637
		public class LegacyHack_IsBossAndNotExpert : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06003477 RID: 13431 RVA: 0x00607D38 File Offset: 0x00605F38
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.boss && !Main.expertMode;
			}

			// Token: 0x06003478 RID: 13432 RVA: 0x00607BF5 File Offset: 0x00605DF5
			public bool CanShowItemDropInUI()
			{
				return !Main.expertMode;
			}

			// Token: 0x06003479 RID: 13433 RVA: 0x00607D51 File Offset: 0x00605F51
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.LegacyHack_IsBossAndNotExpert");
			}
		}

		// Token: 0x02000666 RID: 1638
		public class LegacyHack_IsABoss : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600347B RID: 13435 RVA: 0x00607D5D File Offset: 0x00605F5D
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.boss;
			}

			// Token: 0x0600347C RID: 13436 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600347D RID: 13437 RVA: 0x0006A9EF File Offset: 0x00068BEF
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000667 RID: 1639
		public class IsExpert : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600347F RID: 13439 RVA: 0x00607D25 File Offset: 0x00605F25
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.expertMode;
			}

			// Token: 0x06003480 RID: 13440 RVA: 0x00607D25 File Offset: 0x00605F25
			public bool CanShowItemDropInUI()
			{
				return Main.expertMode;
			}

			// Token: 0x06003481 RID: 13441 RVA: 0x00607D6A File Offset: 0x00605F6A
			public string GetConditionDescription()
			{
				if (Main.masterMode)
				{
					return Language.GetTextValue("Bestiary_ItemDropConditions.IsMasterMode");
				}
				return Language.GetTextValue("Bestiary_ItemDropConditions.IsExpert");
			}
		}

		// Token: 0x02000668 RID: 1640
		public class IsMasterMode : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06003483 RID: 13443 RVA: 0x00607D88 File Offset: 0x00605F88
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.masterMode;
			}

			// Token: 0x06003484 RID: 13444 RVA: 0x00607D88 File Offset: 0x00605F88
			public bool CanShowItemDropInUI()
			{
				return Main.masterMode;
			}

			// Token: 0x06003485 RID: 13445 RVA: 0x00607D8F File Offset: 0x00605F8F
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.IsMasterMode");
			}
		}

		// Token: 0x02000669 RID: 1641
		public class IsCrimson : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06003487 RID: 13447 RVA: 0x00607D9B File Offset: 0x00605F9B
			public bool CanDrop(DropAttemptInfo info)
			{
				return WorldGen.crimson;
			}

			// Token: 0x06003488 RID: 13448 RVA: 0x00607D9B File Offset: 0x00605F9B
			public bool CanShowItemDropInUI()
			{
				return WorldGen.crimson;
			}

			// Token: 0x06003489 RID: 13449 RVA: 0x00607DA2 File Offset: 0x00605FA2
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.IsCrimson");
			}
		}

		// Token: 0x0200066A RID: 1642
		public class IsCorruption : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600348B RID: 13451 RVA: 0x00607DAE File Offset: 0x00605FAE
			public bool CanDrop(DropAttemptInfo info)
			{
				return !WorldGen.crimson;
			}

			// Token: 0x0600348C RID: 13452 RVA: 0x00607DAE File Offset: 0x00605FAE
			public bool CanShowItemDropInUI()
			{
				return !WorldGen.crimson;
			}

			// Token: 0x0600348D RID: 13453 RVA: 0x00607DB8 File Offset: 0x00605FB8
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.IsCorruption");
			}
		}

		// Token: 0x0200066B RID: 1643
		public class IsCrimsonAndNotExpert : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600348F RID: 13455 RVA: 0x00607DC4 File Offset: 0x00605FC4
			public bool CanDrop(DropAttemptInfo info)
			{
				return WorldGen.crimson && !Main.expertMode;
			}

			// Token: 0x06003490 RID: 13456 RVA: 0x00607DC4 File Offset: 0x00605FC4
			public bool CanShowItemDropInUI()
			{
				return WorldGen.crimson && !Main.expertMode;
			}

			// Token: 0x06003491 RID: 13457 RVA: 0x00607DD7 File Offset: 0x00605FD7
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.IsCrimsonAndNotExpert");
			}
		}

		// Token: 0x0200066C RID: 1644
		public class IsCorruptionAndNotExpert : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06003493 RID: 13459 RVA: 0x00607DE3 File Offset: 0x00605FE3
			public bool CanDrop(DropAttemptInfo info)
			{
				return !WorldGen.crimson && !Main.expertMode;
			}

			// Token: 0x06003494 RID: 13460 RVA: 0x00607DE3 File Offset: 0x00605FE3
			public bool CanShowItemDropInUI()
			{
				return !WorldGen.crimson && !Main.expertMode;
			}

			// Token: 0x06003495 RID: 13461 RVA: 0x00607DF6 File Offset: 0x00605FF6
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.IsCorruptionAndNotExpert");
			}
		}

		// Token: 0x0200066D RID: 1645
		public class HalloweenWeapons : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06003497 RID: 13463 RVA: 0x00607E04 File Offset: 0x00606004
			public bool CanDrop(DropAttemptInfo info)
			{
				float num = 500f * Main.GameModeInfo.EnemyMoneyDropMultiplier;
				float num2 = 40f * Main.GameModeInfo.EnemyDamageMultiplier;
				float num3 = 20f * Main.GameModeInfo.EnemyDefenseMultiplier;
				return Main.halloween && info.npc.value > 0f && info.npc.value < num && (float)info.npc.damage < num2 && (float)info.npc.defense < num3 && !info.IsInSimulation;
			}

			// Token: 0x06003498 RID: 13464 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06003499 RID: 13465 RVA: 0x00607E94 File Offset: 0x00606094
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.HalloweenWeapons");
			}
		}

		// Token: 0x0200066E RID: 1646
		public class SoulOfNight : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600349B RID: 13467 RVA: 0x00607EA0 File Offset: 0x006060A0
			public bool CanDrop(DropAttemptInfo info)
			{
				return Conditions.SoulOfWhateverConditionCanDrop(info) && (info.player.ZoneCorrupt || info.player.ZoneCrimson);
			}

			// Token: 0x0600349C RID: 13468 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600349D RID: 13469 RVA: 0x00607EC6 File Offset: 0x006060C6
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.SoulOfNight");
			}
		}

		// Token: 0x0200066F RID: 1647
		public class SoulOfLight : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600349F RID: 13471 RVA: 0x00607ED2 File Offset: 0x006060D2
			public bool CanDrop(DropAttemptInfo info)
			{
				return Conditions.SoulOfWhateverConditionCanDrop(info) && info.player.ZoneHallow;
			}

			// Token: 0x060034A0 RID: 13472 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060034A1 RID: 13473 RVA: 0x00607EE9 File Offset: 0x006060E9
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.SoulOfLight");
			}
		}

		// Token: 0x02000670 RID: 1648
		public class NotFromStatue : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060034A3 RID: 13475 RVA: 0x00607EF5 File Offset: 0x006060F5
			public bool CanDrop(DropAttemptInfo info)
			{
				return !info.npc.SpawnedFromStatue;
			}

			// Token: 0x060034A4 RID: 13476 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060034A5 RID: 13477 RVA: 0x00607F05 File Offset: 0x00606105
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.NotFromStatue");
			}
		}

		// Token: 0x02000671 RID: 1649
		public class HalloweenGoodieBagDrop : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060034A7 RID: 13479 RVA: 0x00607F14 File Offset: 0x00606114
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.halloween && info.npc.lifeMax > 1 && info.npc.damage > 0 && !info.npc.friendly && info.npc.type != 121 && info.npc.type != 23 && info.npc.value > 0f && !info.IsInSimulation;
			}

			// Token: 0x060034A8 RID: 13480 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060034A9 RID: 13481 RVA: 0x00607F8C File Offset: 0x0060618C
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.HalloweenGoodieBagDrop");
			}
		}

		// Token: 0x02000672 RID: 1650
		public class XmasPresentDrop : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060034AB RID: 13483 RVA: 0x00607F98 File Offset: 0x00606198
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.xMas && info.npc.lifeMax > 1 && info.npc.damage > 0 && !info.npc.friendly && info.npc.type != 121 && info.npc.type != 23 && info.npc.value > 0f && !info.IsInSimulation;
			}

			// Token: 0x060034AC RID: 13484 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060034AD RID: 13485 RVA: 0x00608010 File Offset: 0x00606210
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.XmasPresentDrop");
			}
		}

		// Token: 0x02000673 RID: 1651
		public class LivingFlames : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060034AF RID: 13487 RVA: 0x0060801C File Offset: 0x0060621C
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.lifeMax > 5 && info.npc.value > 0f && !info.npc.friendly && Main.hardMode && info.npc.position.Y / 16f > (float)Main.UnderworldLayer && !info.IsInSimulation;
			}

			// Token: 0x060034B0 RID: 13488 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060034B1 RID: 13489 RVA: 0x00608086 File Offset: 0x00606286
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.LivingFlames");
			}
		}

		// Token: 0x02000674 RID: 1652
		public class NamedNPC : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060034B3 RID: 13491 RVA: 0x00608092 File Offset: 0x00606292
			public NamedNPC(string neededName)
			{
				this.neededName = neededName;
			}

			// Token: 0x060034B4 RID: 13492 RVA: 0x006080A1 File Offset: 0x006062A1
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.GivenOrTypeName == this.neededName;
			}

			// Token: 0x060034B5 RID: 13493 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060034B6 RID: 13494 RVA: 0x006080B9 File Offset: 0x006062B9
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.NamedNPC");
			}

			// Token: 0x04006181 RID: 24961
			public string neededName;
		}

		// Token: 0x02000675 RID: 1653
		public class HallowKeyCondition : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060034B7 RID: 13495 RVA: 0x006080C5 File Offset: 0x006062C5
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.value > 0f && Main.hardMode && !info.IsInSimulation && info.player.ZoneHallow;
			}

			// Token: 0x060034B8 RID: 13496 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060034B9 RID: 13497 RVA: 0x006080F5 File Offset: 0x006062F5
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.HallowKeyCondition");
			}
		}

		// Token: 0x02000676 RID: 1654
		public class JungleKeyCondition : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060034BB RID: 13499 RVA: 0x00608101 File Offset: 0x00606301
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.value > 0f && Main.hardMode && !info.IsInSimulation && info.player.ZoneJungle;
			}

			// Token: 0x060034BC RID: 13500 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060034BD RID: 13501 RVA: 0x00608131 File Offset: 0x00606331
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.JungleKeyCondition");
			}
		}

		// Token: 0x02000677 RID: 1655
		public class CorruptKeyCondition : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060034BF RID: 13503 RVA: 0x0060813D File Offset: 0x0060633D
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.value > 0f && Main.hardMode && !info.IsInSimulation && info.player.ZoneCorrupt;
			}

			// Token: 0x060034C0 RID: 13504 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060034C1 RID: 13505 RVA: 0x0060816D File Offset: 0x0060636D
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.CorruptKeyCondition");
			}
		}

		// Token: 0x02000678 RID: 1656
		public class CrimsonKeyCondition : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060034C3 RID: 13507 RVA: 0x00608179 File Offset: 0x00606379
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.value > 0f && Main.hardMode && !info.IsInSimulation && info.player.ZoneCrimson;
			}

			// Token: 0x060034C4 RID: 13508 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060034C5 RID: 13509 RVA: 0x006081A9 File Offset: 0x006063A9
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.CrimsonKeyCondition");
			}
		}

		// Token: 0x02000679 RID: 1657
		public class FrozenKeyCondition : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060034C7 RID: 13511 RVA: 0x006081B5 File Offset: 0x006063B5
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.value > 0f && Main.hardMode && !info.IsInSimulation && info.player.ZoneSnow;
			}

			// Token: 0x060034C8 RID: 13512 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060034C9 RID: 13513 RVA: 0x006081E5 File Offset: 0x006063E5
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.FrozenKeyCondition");
			}
		}

		// Token: 0x0200067A RID: 1658
		public class DesertKeyCondition : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060034CB RID: 13515 RVA: 0x006081F1 File Offset: 0x006063F1
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.value > 0f && Main.hardMode && !info.IsInSimulation && info.player.ZoneDesert && !info.player.ZoneBeach;
			}

			// Token: 0x060034CC RID: 13516 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060034CD RID: 13517 RVA: 0x00608231 File Offset: 0x00606431
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.DesertKeyCondition");
			}
		}

		// Token: 0x0200067B RID: 1659
		public class BeatAnyMechBoss : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060034CF RID: 13519 RVA: 0x0060823D File Offset: 0x0060643D
			public bool CanDrop(DropAttemptInfo info)
			{
				return NPC.downedMechBossAny;
			}

			// Token: 0x060034D0 RID: 13520 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060034D1 RID: 13521 RVA: 0x00608244 File Offset: 0x00606444
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.BeatAnyMechBoss");
			}
		}

		// Token: 0x0200067C RID: 1660
		public class YoyoCascade : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060034D3 RID: 13523 RVA: 0x00608250 File Offset: 0x00606450
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.hardMode && info.npc.HasPlayerTarget && info.npc.lifeMax > 5 && !info.npc.friendly && info.npc.value > 0f && info.npc.position.Y / 16f > (float)(Main.maxTilesY - 350) && NPC.downedBoss3 && !info.IsInSimulation;
			}

			// Token: 0x060034D4 RID: 13524 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060034D5 RID: 13525 RVA: 0x006082D4 File Offset: 0x006064D4
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.YoyoCascade");
			}
		}

		// Token: 0x0200067D RID: 1661
		public class YoyosAmarok : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060034D7 RID: 13527 RVA: 0x006082E0 File Offset: 0x006064E0
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.hardMode && info.npc.HasPlayerTarget && info.player.ZoneSnow && info.npc.lifeMax > 5 && !info.npc.friendly && info.npc.value > 0f && !info.IsInSimulation;
			}

			// Token: 0x060034D8 RID: 13528 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060034D9 RID: 13529 RVA: 0x00608346 File Offset: 0x00606546
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.YoyosAmarok");
			}
		}

		// Token: 0x0200067E RID: 1662
		public class YoyosYelets : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060034DB RID: 13531 RVA: 0x00608354 File Offset: 0x00606554
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.hardMode && info.player.ZoneJungle && NPC.downedMechBossAny && info.npc.lifeMax > 5 && info.npc.HasPlayerTarget && !info.npc.friendly && info.npc.value > 0f && !info.IsInSimulation;
			}

			// Token: 0x060034DC RID: 13532 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060034DD RID: 13533 RVA: 0x006083C1 File Offset: 0x006065C1
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.YoyosYelets");
			}
		}

		// Token: 0x0200067F RID: 1663
		public class YoyosKraken : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060034DF RID: 13535 RVA: 0x006083D0 File Offset: 0x006065D0
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.hardMode && info.player.ZoneDungeon && NPC.downedPlantBoss && info.npc.lifeMax > 5 && info.npc.HasPlayerTarget && !info.npc.friendly && info.npc.value > 0f && !info.IsInSimulation;
			}

			// Token: 0x060034E0 RID: 13536 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060034E1 RID: 13537 RVA: 0x0060843D File Offset: 0x0060663D
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.YoyosKraken");
			}
		}

		// Token: 0x02000680 RID: 1664
		public class YoyosHelFire : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060034E3 RID: 13539 RVA: 0x0060844C File Offset: 0x0060664C
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.hardMode && !info.player.ZoneDungeon && (double)(info.npc.position.Y / 16f) > (Main.rockLayer + (double)(Main.maxTilesY * 2)) / 3.0 && info.npc.lifeMax > 5 && info.npc.HasPlayerTarget && !info.npc.friendly && info.npc.value > 0f && !info.IsInSimulation;
			}

			// Token: 0x060034E4 RID: 13540 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060034E5 RID: 13541 RVA: 0x006084E6 File Offset: 0x006066E6
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.YoyosHelFire");
			}
		}

		// Token: 0x02000681 RID: 1665
		public class WindyEnoughForKiteDrops : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060034E7 RID: 13543 RVA: 0x006084F2 File Offset: 0x006066F2
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.WindyEnoughForKiteDrops;
			}

			// Token: 0x060034E8 RID: 13544 RVA: 0x0003266D File Offset: 0x0003086D
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060034E9 RID: 13545 RVA: 0x006084F9 File Offset: 0x006066F9
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.IsItAHappyWindyDay");
			}
		}

		// Token: 0x02000682 RID: 1666
		public class RemixSeedEasymode : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060034EB RID: 13547 RVA: 0x00608505 File Offset: 0x00606705
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.remixWorld && !Main.hardMode;
			}

			// Token: 0x060034EC RID: 13548 RVA: 0x00608505 File Offset: 0x00606705
			public bool CanShowItemDropInUI()
			{
				return Main.remixWorld && !Main.hardMode;
			}

			// Token: 0x060034ED RID: 13549 RVA: 0x0006A9EF File Offset: 0x00068BEF
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000683 RID: 1667
		public class RemixSeedHardmode : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060034EF RID: 13551 RVA: 0x00608518 File Offset: 0x00606718
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.remixWorld && Main.hardMode;
			}

			// Token: 0x060034F0 RID: 13552 RVA: 0x00608518 File Offset: 0x00606718
			public bool CanShowItemDropInUI()
			{
				return Main.remixWorld && Main.hardMode;
			}

			// Token: 0x060034F1 RID: 13553 RVA: 0x0006A9EF File Offset: 0x00068BEF
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000684 RID: 1668
		public class RemixSeed : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060034F3 RID: 13555 RVA: 0x00608528 File Offset: 0x00606728
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.remixWorld;
			}

			// Token: 0x060034F4 RID: 13556 RVA: 0x00608528 File Offset: 0x00606728
			public bool CanShowItemDropInUI()
			{
				return Main.remixWorld;
			}

			// Token: 0x060034F5 RID: 13557 RVA: 0x0006A9EF File Offset: 0x00068BEF
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000685 RID: 1669
		public class NotRemixSeed : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060034F7 RID: 13559 RVA: 0x0060852F File Offset: 0x0060672F
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.remixWorld;
			}

			// Token: 0x060034F8 RID: 13560 RVA: 0x0060852F File Offset: 0x0060672F
			public bool CanShowItemDropInUI()
			{
				return !Main.remixWorld;
			}

			// Token: 0x060034F9 RID: 13561 RVA: 0x0006A9EF File Offset: 0x00068BEF
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000686 RID: 1670
		public class TenthAnniversaryIsUp : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060034FB RID: 13563 RVA: 0x00608539 File Offset: 0x00606739
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.tenthAnniversaryWorld;
			}

			// Token: 0x060034FC RID: 13564 RVA: 0x00608539 File Offset: 0x00606739
			public bool CanShowItemDropInUI()
			{
				return Main.tenthAnniversaryWorld;
			}

			// Token: 0x060034FD RID: 13565 RVA: 0x0006A9EF File Offset: 0x00068BEF
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000687 RID: 1671
		public class TenthAnniversaryIsNotUp : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060034FF RID: 13567 RVA: 0x00608540 File Offset: 0x00606740
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.tenthAnniversaryWorld;
			}

			// Token: 0x06003500 RID: 13568 RVA: 0x00608540 File Offset: 0x00606740
			public bool CanShowItemDropInUI()
			{
				return !Main.tenthAnniversaryWorld;
			}

			// Token: 0x06003501 RID: 13569 RVA: 0x0006A9EF File Offset: 0x00068BEF
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000688 RID: 1672
		public class DontStarveIsUp : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06003503 RID: 13571 RVA: 0x0060854A File Offset: 0x0060674A
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.dontStarveWorld;
			}

			// Token: 0x06003504 RID: 13572 RVA: 0x0060854A File Offset: 0x0060674A
			public bool CanShowItemDropInUI()
			{
				return Main.dontStarveWorld;
			}

			// Token: 0x06003505 RID: 13573 RVA: 0x0006A9EF File Offset: 0x00068BEF
			public string GetConditionDescription()
			{
				return null;
			}
		}

		// Token: 0x02000689 RID: 1673
		public class DontStarveIsNotUp : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06003507 RID: 13575 RVA: 0x00608551 File Offset: 0x00606751
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.dontStarveWorld;
			}

			// Token: 0x06003508 RID: 13576 RVA: 0x00608551 File Offset: 0x00606751
			public bool CanShowItemDropInUI()
			{
				return !Main.dontStarveWorld;
			}

			// Token: 0x06003509 RID: 13577 RVA: 0x0006A9EF File Offset: 0x00068BEF
			public string GetConditionDescription()
			{
				return null;
			}
		}
	}
}
