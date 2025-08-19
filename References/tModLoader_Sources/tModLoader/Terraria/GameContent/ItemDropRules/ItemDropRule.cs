using System;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x0200060C RID: 1548
	public class ItemDropRule
	{
		// Token: 0x0600443C RID: 17468 RVA: 0x0060A147 File Offset: 0x00608347
		public static IItemDropRule Common(int itemId, int chanceDenominator = 1, int minimumDropped = 1, int maximumDropped = 1)
		{
			return new CommonDrop(itemId, chanceDenominator, minimumDropped, maximumDropped, 1);
		}

		// Token: 0x0600443D RID: 17469 RVA: 0x0060A153 File Offset: 0x00608353
		public static IItemDropRule BossBag(int itemId)
		{
			return new DropBasedOnExpertMode(ItemDropRule.DropNothing(), new DropLocalPerClientAndResetsNPCMoneyTo0(itemId, 1, 1, 1, null));
		}

		// Token: 0x0600443E RID: 17470 RVA: 0x0060A169 File Offset: 0x00608369
		public static IItemDropRule BossBagByCondition(IItemDropRuleCondition condition, int itemId)
		{
			return new DropBasedOnExpertMode(ItemDropRule.DropNothing(), new DropLocalPerClientAndResetsNPCMoneyTo0(itemId, 1, 1, 1, condition));
		}

		// Token: 0x0600443F RID: 17471 RVA: 0x0060A17F File Offset: 0x0060837F
		public static IItemDropRule ExpertGetsRerolls(int itemId, int chanceDenominator, int expertRerolls)
		{
			return new DropBasedOnExpertMode(ItemDropRule.WithRerolls(itemId, 0, chanceDenominator, 1, 1), ItemDropRule.WithRerolls(itemId, expertRerolls, chanceDenominator, 1, 1));
		}

		// Token: 0x06004440 RID: 17472 RVA: 0x0060A19A File Offset: 0x0060839A
		public static IItemDropRule MasterModeCommonDrop(int itemId)
		{
			return ItemDropRule.ByCondition(new Conditions.IsMasterMode(), itemId, 1, 1, 1, 1);
		}

		// Token: 0x06004441 RID: 17473 RVA: 0x0060A1AB File Offset: 0x006083AB
		public static IItemDropRule MasterModeDropOnAllPlayers(int itemId, int chanceDenominator = 1)
		{
			return new DropBasedOnMasterMode(ItemDropRule.DropNothing(), new DropPerPlayerOnThePlayer(itemId, chanceDenominator, 1, 1, new Conditions.IsMasterMode()));
		}

		// Token: 0x06004442 RID: 17474 RVA: 0x0060A1C5 File Offset: 0x006083C5
		public static IItemDropRule WithRerolls(int itemId, int rerolls, int chanceDenominator = 1, int minimumDropped = 1, int maximumDropped = 1)
		{
			return new CommonDropWithRerolls(itemId, chanceDenominator, minimumDropped, maximumDropped, rerolls);
		}

		// Token: 0x06004443 RID: 17475 RVA: 0x0060A1D2 File Offset: 0x006083D2
		public static IItemDropRule ByCondition(IItemDropRuleCondition condition, int itemId, int chanceDenominator = 1, int minimumDropped = 1, int maximumDropped = 1, int chanceNumerator = 1)
		{
			return new ItemDropWithConditionRule(itemId, chanceDenominator, minimumDropped, maximumDropped, condition, chanceNumerator);
		}

		// Token: 0x06004444 RID: 17476 RVA: 0x0060A1E1 File Offset: 0x006083E1
		public static IItemDropRule NotScalingWithLuck(int itemId, int chanceDenominator = 1, int minimumDropped = 1, int maximumDropped = 1)
		{
			return new CommonDropNotScalingWithLuck(itemId, chanceDenominator, minimumDropped, maximumDropped);
		}

		// Token: 0x06004445 RID: 17477 RVA: 0x0060A1EC File Offset: 0x006083EC
		public static IItemDropRule OneFromOptionsNotScalingWithLuck(int chanceDenominator, params int[] options)
		{
			return new OneFromOptionsNotScaledWithLuckDropRule(chanceDenominator, 1, options);
		}

		// Token: 0x06004446 RID: 17478 RVA: 0x0060A1F6 File Offset: 0x006083F6
		public static IItemDropRule OneFromOptionsNotScalingWithLuckWithX(int chanceDenominator, int chanceNumerator, params int[] options)
		{
			return new OneFromOptionsNotScaledWithLuckDropRule(chanceDenominator, chanceNumerator, options);
		}

		// Token: 0x06004447 RID: 17479 RVA: 0x0060A200 File Offset: 0x00608400
		public static IItemDropRule OneFromOptions(int chanceDenominator, params int[] options)
		{
			return new OneFromOptionsDropRule(chanceDenominator, 1, options);
		}

		// Token: 0x06004448 RID: 17480 RVA: 0x0060A20A File Offset: 0x0060840A
		public static IItemDropRule OneFromOptionsWithNumerator(int chanceDenominator, int chanceNumerator, params int[] options)
		{
			return new OneFromOptionsDropRule(chanceDenominator, chanceNumerator, options);
		}

		// Token: 0x06004449 RID: 17481 RVA: 0x0060A214 File Offset: 0x00608414
		public static IItemDropRule DropNothing()
		{
			return new DropNothing();
		}

		// Token: 0x0600444A RID: 17482 RVA: 0x0060A21B File Offset: 0x0060841B
		public static IItemDropRule NormalvsExpert(int itemId, int chanceDenominatorInNormal, int chanceDenominatorInExpert)
		{
			return new DropBasedOnExpertMode(ItemDropRule.Common(itemId, chanceDenominatorInNormal, 1, 1), ItemDropRule.Common(itemId, chanceDenominatorInExpert, 1, 1));
		}

		// Token: 0x0600444B RID: 17483 RVA: 0x0060A234 File Offset: 0x00608434
		public static IItemDropRule NormalvsExpertNotScalingWithLuck(int itemId, int chanceDenominatorInNormal, int chanceDenominatorInExpert)
		{
			return new DropBasedOnExpertMode(ItemDropRule.NotScalingWithLuck(itemId, chanceDenominatorInNormal, 1, 1), ItemDropRule.NotScalingWithLuck(itemId, chanceDenominatorInExpert, 1, 1));
		}

		// Token: 0x0600444C RID: 17484 RVA: 0x0060A24D File Offset: 0x0060844D
		public static IItemDropRule NormalvsExpertOneFromOptionsNotScalingWithLuck(int chanceDenominatorInNormal, int chanceDenominatorInExpert, params int[] options)
		{
			return new DropBasedOnExpertMode(ItemDropRule.OneFromOptionsNotScalingWithLuck(chanceDenominatorInNormal, options), ItemDropRule.OneFromOptionsNotScalingWithLuck(chanceDenominatorInExpert, options));
		}

		// Token: 0x0600444D RID: 17485 RVA: 0x0060A262 File Offset: 0x00608462
		public static IItemDropRule NormalvsExpertOneFromOptions(int chanceDenominatorInNormal, int chanceDenominatorInExpert, params int[] options)
		{
			return new DropBasedOnExpertMode(ItemDropRule.OneFromOptions(chanceDenominatorInNormal, options), ItemDropRule.OneFromOptions(chanceDenominatorInExpert, options));
		}

		// Token: 0x0600444E RID: 17486 RVA: 0x0060A277 File Offset: 0x00608477
		public static IItemDropRule Food(int itemId, int chanceDenominator, int minimumDropped = 1, int maximumDropped = 1)
		{
			return new ItemDropWithConditionRule(itemId, chanceDenominator, minimumDropped, maximumDropped, new Conditions.NotFromStatue(), 1);
		}

		// Token: 0x0600444F RID: 17487 RVA: 0x0060A288 File Offset: 0x00608488
		public static IItemDropRule StatusImmunityItem(int itemId, int dropsOutOfX)
		{
			return ItemDropRule.ExpertGetsRerolls(itemId, dropsOutOfX, 1);
		}

		// Token: 0x06004450 RID: 17488 RVA: 0x0060A292 File Offset: 0x00608492
		public static IItemDropRule FewFromOptionsNotScalingWithLuck(int amount, int chanceDenominator, params int[] options)
		{
			return new FewFromOptionsNotScaledWithLuckDropRule(amount, chanceDenominator, 1, options);
		}

		// Token: 0x06004451 RID: 17489 RVA: 0x0060A29D File Offset: 0x0060849D
		public static IItemDropRule FewFromOptionsNotScalingWithLuckWithX(int amount, int chanceDenominator, int chanceNumerator, params int[] options)
		{
			return new FewFromOptionsNotScaledWithLuckDropRule(amount, chanceDenominator, chanceNumerator, options);
		}

		// Token: 0x06004452 RID: 17490 RVA: 0x0060A2A8 File Offset: 0x006084A8
		public static IItemDropRule FewFromOptions(int amount, int chanceDenominator, params int[] options)
		{
			return new FewFromOptionsDropRule(amount, chanceDenominator, 1, options);
		}

		// Token: 0x06004453 RID: 17491 RVA: 0x0060A2B3 File Offset: 0x006084B3
		public static IItemDropRule FewFromOptionsWithNumerator(int amount, int chanceDenominator, int chanceNumerator, params int[] options)
		{
			return new FewFromOptionsDropRule(amount, chanceDenominator, chanceNumerator, options);
		}

		// Token: 0x06004454 RID: 17492 RVA: 0x0060A2BE File Offset: 0x006084BE
		public static IItemDropRule SequentialRules(int chanceDenominator, params IItemDropRule[] rules)
		{
			return new SequentialRulesRule(chanceDenominator, rules);
		}

		// Token: 0x06004455 RID: 17493 RVA: 0x0060A2C7 File Offset: 0x006084C7
		public static IItemDropRule SequentialRulesNotScalingWithLuck(int chanceDenominator, params IItemDropRule[] rules)
		{
			return new SequentialRulesNotScalingWithLuckRule(chanceDenominator, rules);
		}

		// Token: 0x06004456 RID: 17494 RVA: 0x0060A2D0 File Offset: 0x006084D0
		public static IItemDropRule SequentialRulesNotScalingWithLuckWithNumerator(int chanceDenominator, int chanceNumerator, params IItemDropRule[] rules)
		{
			return new SequentialRulesNotScalingWithLuckRule(chanceDenominator, chanceNumerator, rules);
		}

		// Token: 0x06004457 RID: 17495 RVA: 0x0060A2DA File Offset: 0x006084DA
		public static IItemDropRule Coins(long value, bool withRandomBonus)
		{
			return new CoinsRule(value, withRandomBonus);
		}

		// Token: 0x06004458 RID: 17496 RVA: 0x0060A2E4 File Offset: 0x006084E4
		public static IItemDropRule CoinsBasedOnNPCValue(int npcId)
		{
			NPC npc = new NPC();
			npc.SetDefaults(npcId, default(NPCSpawnParams));
			return ItemDropRule.Coins((long)npc.value, true);
		}

		// Token: 0x06004459 RID: 17497 RVA: 0x0060A312 File Offset: 0x00608512
		public static IItemDropRule AlwaysAtleastOneSuccess(params IItemDropRule[] rules)
		{
			return new AlwaysAtleastOneSuccessDropRule(rules);
		}

		// Token: 0x0600445A RID: 17498 RVA: 0x0060A31A File Offset: 0x0060851A
		public static IItemDropRule NotScalingWithLuckWithNumerator(int itemId, int chanceDenominator = 1, int chanceNumerator = 1, int minimumDropped = 1, int maximumDropped = 1)
		{
			return new CommonDropNotScalingWithLuck(itemId, chanceDenominator, chanceNumerator, minimumDropped, maximumDropped);
		}
	}
}
