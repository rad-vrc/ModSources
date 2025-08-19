using System;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.NPCChanges
{
	// Token: 0x02000238 RID: 568
	public class NPCDrops : GlobalNPC
	{
		// Token: 0x06000D9C RID: 3484 RVA: 0x0006C9E0 File Offset: 0x0006ABE0
		public override void SetDefaults(NPC npc)
		{
			npc.value *= (float)QoLCompendium.mainConfig.EnemiesDropMoreCoins;
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0006C9FC File Offset: 0x0006ABFC
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			NPCDrops.<>c__DisplayClass1_0 CS$<>8__locals1;
			CS$<>8__locals1.npc = npc;
			CS$<>8__locals1.npcLoot = npcLoot;
			if (CS$<>8__locals1.npc.type == 564 && QoLCompendium.mainConfig.ExtraDefenderMedalDrops)
			{
				CS$<>8__locals1.npcLoot.Add(ItemDropRule.Common(3817, 1, 10, 10));
			}
			if (CS$<>8__locals1.npc.type == 565 && QoLCompendium.mainConfig.ExtraDefenderMedalDrops)
			{
				CS$<>8__locals1.npcLoot.Add(ItemDropRule.Common(3817, 1, 25, 25));
			}
			if (CS$<>8__locals1.npc.type == 576 && QoLCompendium.mainConfig.ExtraDefenderMedalDrops)
			{
				CS$<>8__locals1.npcLoot.Add(ItemDropRule.Common(3817, 1, 25, 25));
			}
			if (CS$<>8__locals1.npc.type == 577 && QoLCompendium.mainConfig.ExtraDefenderMedalDrops)
			{
				CS$<>8__locals1.npcLoot.Add(ItemDropRule.Common(3817, 1, 30, 30));
			}
			if (CS$<>8__locals1.npc.type == 551 && QoLCompendium.mainConfig.ExtraDefenderMedalDrops)
			{
				CS$<>8__locals1.npcLoot.Add(ItemDropRule.Common(3817, 1, 50, 50));
			}
			if ((CS$<>8__locals1.npc.type == 421 || CS$<>8__locals1.npc.type == 423 || CS$<>8__locals1.npc.type == 420 || CS$<>8__locals1.npc.type == 424) && QoLCompendium.mainConfig.LunarEnemiesDropFragments)
			{
				CS$<>8__locals1.npcLoot.Add(ItemDropRule.Common(3457, 1, 1, 3));
			}
			if ((CS$<>8__locals1.npc.type == 418 || CS$<>8__locals1.npc.type == 412 || CS$<>8__locals1.npc.type == 518 || CS$<>8__locals1.npc.type == 415 || CS$<>8__locals1.npc.type == 416 || CS$<>8__locals1.npc.type == 419 || CS$<>8__locals1.npc.type == 417) && QoLCompendium.mainConfig.LunarEnemiesDropFragments)
			{
				CS$<>8__locals1.npcLoot.Add(ItemDropRule.Common(3458, 1, 1, 3));
			}
			if ((CS$<>8__locals1.npc.type == 407 || CS$<>8__locals1.npc.type == 402 || CS$<>8__locals1.npc.type == 405 || CS$<>8__locals1.npc.type == 411 || CS$<>8__locals1.npc.type == 409) && QoLCompendium.mainConfig.LunarEnemiesDropFragments)
			{
				CS$<>8__locals1.npcLoot.Add(ItemDropRule.Common(3459, 1, 1, 3));
			}
			if ((CS$<>8__locals1.npc.type == 427 || CS$<>8__locals1.npc.type == 426 || CS$<>8__locals1.npc.type == 425 || CS$<>8__locals1.npc.type == 429) && QoLCompendium.mainConfig.LunarEnemiesDropFragments)
			{
				CS$<>8__locals1.npcLoot.Add(ItemDropRule.Common(3456, 1, 1, 3));
			}
			CS$<>8__locals1.allowedRecursionDepth = 10;
			foreach (IItemDropRule dropRule in CS$<>8__locals1.npcLoot.Get(true))
			{
				NPCDrops.<ModifyNPCLoot>g__CheckMasterDropRule|1_1(dropRule, ref CS$<>8__locals1);
			}
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0006CD90 File Offset: 0x0006AF90
		[CompilerGenerated]
		internal static void <ModifyNPCLoot>g__AddDrop|1_0(IItemDropRule dropRule, ref NPCDrops.<>c__DisplayClass1_0 A_1)
		{
			if (A_1.npc.type == 125 || A_1.npc.type == 126)
			{
				LeadingConditionRule noTwin = new LeadingConditionRule(new Conditions.MissingTwin());
				((IItemDropRule)noTwin).OnSuccess(dropRule, false);
				A_1.npcLoot.Add((IItemDropRule)noTwin);
				return;
			}
			if (A_1.npc.type == 14 || A_1.npc.type == 13 || A_1.npc.type == 15)
			{
				LeadingConditionRule lastEater = new LeadingConditionRule(new Conditions.LegacyHack_IsABoss());
				((IItemDropRule)lastEater).OnSuccess(dropRule, false);
				A_1.npcLoot.Add((IItemDropRule)lastEater);
				return;
			}
			A_1.npcLoot.Add(dropRule);
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0006CE50 File Offset: 0x0006B050
		[CompilerGenerated]
		internal static void <ModifyNPCLoot>g__CheckMasterDropRule|1_1(IItemDropRule dropRule, ref NPCDrops.<>c__DisplayClass1_0 A_1)
		{
			int num = A_1.allowedRecursionDepth - 1;
			A_1.allowedRecursionDepth = num;
			if (num > 0)
			{
				if (dropRule != null && dropRule.ChainedRules != null)
				{
					foreach (IItemDropRuleChainAttempt chain in dropRule.ChainedRules)
					{
						if (chain != null && chain.RuleToChain != null)
						{
							NPCDrops.<ModifyNPCLoot>g__CheckMasterDropRule|1_1(chain.RuleToChain, ref A_1);
						}
					}
				}
				DropBasedOnMasterMode dropBasedOnMasterMode = (DropBasedOnMasterMode)((dropRule is DropBasedOnMasterMode) ? dropRule : null);
				if (dropBasedOnMasterMode != null && dropBasedOnMasterMode != null && dropBasedOnMasterMode.ruleForMasterMode != null)
				{
					NPCDrops.<ModifyNPCLoot>g__CheckMasterDropRule|1_1(dropBasedOnMasterMode.ruleForMasterMode, ref A_1);
				}
			}
			num = A_1.allowedRecursionDepth;
			A_1.allowedRecursionDepth = num + 1;
			ItemDropWithConditionRule itemDropWithCondition = (ItemDropWithConditionRule)((dropRule is ItemDropWithConditionRule) ? dropRule : null);
			if (itemDropWithCondition != null && itemDropWithCondition.condition is Conditions.IsMasterMode)
			{
				NPCDrops.<ModifyNPCLoot>g__AddDrop|1_0(ItemDropRule.ByCondition((IItemDropRuleCondition)new ExpertOnlyDropCondition(), itemDropWithCondition.itemId, itemDropWithCondition.chanceDenominator, itemDropWithCondition.amountDroppedMinimum, itemDropWithCondition.amountDroppedMaximum, itemDropWithCondition.chanceNumerator), ref A_1);
				return;
			}
			DropPerPlayerOnThePlayer dropPerPlayer = (DropPerPlayerOnThePlayer)((dropRule is DropPerPlayerOnThePlayer) ? dropRule : null);
			if (dropPerPlayer != null && dropPerPlayer.condition is Conditions.IsMasterMode)
			{
				NPCDrops.<ModifyNPCLoot>g__AddDrop|1_0(ItemDropRule.ByCondition((IItemDropRuleCondition)new ExpertOnlyDropCondition(), dropPerPlayer.itemId, dropPerPlayer.chanceDenominator, dropPerPlayer.amountDroppedMinimum, dropPerPlayer.amountDroppedMaximum, dropPerPlayer.chanceNumerator), ref A_1);
			}
		}
	}
}
