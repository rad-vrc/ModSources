using System;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x0200027E RID: 638
	public class ItemDropRule
	{
		// Token: 0x06001FF7 RID: 8183 RVA: 0x00517A31 File Offset: 0x00515C31
		public static IItemDropRule Common(int itemId, int chanceDenominator = 1, int minimumDropped = 1, int maximumDropped = 1)
		{
			return new CommonDrop(itemId, chanceDenominator, minimumDropped, maximumDropped, 1);
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x00517A3D File Offset: 0x00515C3D
		public static IItemDropRule BossBag(int itemId)
		{
			return new DropBasedOnExpertMode(ItemDropRule.DropNothing(), new DropLocalPerClientAndResetsNPCMoneyTo0(itemId, 1, 1, 1, null));
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x00517A53 File Offset: 0x00515C53
		public static IItemDropRule BossBagByCondition(IItemDropRuleCondition condition, int itemId)
		{
			return new DropBasedOnExpertMode(ItemDropRule.DropNothing(), new DropLocalPerClientAndResetsNPCMoneyTo0(itemId, 1, 1, 1, condition));
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x00517A69 File Offset: 0x00515C69
		public static IItemDropRule ExpertGetsRerolls(int itemId, int chanceDenominator, int expertRerolls)
		{
			return new DropBasedOnExpertMode(ItemDropRule.WithRerolls(itemId, 0, chanceDenominator, 1, 1), ItemDropRule.WithRerolls(itemId, expertRerolls, chanceDenominator, 1, 1));
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x00517A84 File Offset: 0x00515C84
		public static IItemDropRule MasterModeCommonDrop(int itemId)
		{
			return ItemDropRule.ByCondition(new Conditions.IsMasterMode(), itemId, 1, 1, 1, 1);
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x00517A95 File Offset: 0x00515C95
		public static IItemDropRule MasterModeDropOnAllPlayers(int itemId, int chanceDenominator = 1)
		{
			return new DropBasedOnMasterMode(ItemDropRule.DropNothing(), new DropPerPlayerOnThePlayer(itemId, chanceDenominator, 1, 1, new Conditions.IsMasterMode()));
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x00517AAF File Offset: 0x00515CAF
		public static IItemDropRule WithRerolls(int itemId, int rerolls, int chanceDenominator = 1, int minimumDropped = 1, int maximumDropped = 1)
		{
			return new CommonDropWithRerolls(itemId, chanceDenominator, minimumDropped, maximumDropped, rerolls);
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x00517ABC File Offset: 0x00515CBC
		public static IItemDropRule ByCondition(IItemDropRuleCondition condition, int itemId, int chanceDenominator = 1, int minimumDropped = 1, int maximumDropped = 1, int chanceNumerator = 1)
		{
			return new ItemDropWithConditionRule(itemId, chanceDenominator, minimumDropped, maximumDropped, condition, chanceNumerator);
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x00517ACB File Offset: 0x00515CCB
		public static IItemDropRule NotScalingWithLuck(int itemId, int chanceDenominator = 1, int minimumDropped = 1, int maximumDropped = 1)
		{
			return new CommonDropNotScalingWithLuck(itemId, chanceDenominator, minimumDropped, maximumDropped);
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x00517AD6 File Offset: 0x00515CD6
		public static IItemDropRule OneFromOptionsNotScalingWithLuck(int chanceDenominator, params int[] options)
		{
			return new OneFromOptionsNotScaledWithLuckDropRule(chanceDenominator, 1, options);
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x00517AE0 File Offset: 0x00515CE0
		public static IItemDropRule OneFromOptionsNotScalingWithLuckWithX(int chanceDenominator, int chanceNumerator, params int[] options)
		{
			return new OneFromOptionsNotScaledWithLuckDropRule(chanceDenominator, chanceNumerator, options);
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x00517AEA File Offset: 0x00515CEA
		public static IItemDropRule OneFromOptions(int chanceDenominator, params int[] options)
		{
			return new OneFromOptionsDropRule(chanceDenominator, 1, options);
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x00517AF4 File Offset: 0x00515CF4
		public static IItemDropRule OneFromOptionsWithNumerator(int chanceDenominator, int chanceNumerator, params int[] options)
		{
			return new OneFromOptionsDropRule(chanceDenominator, chanceNumerator, options);
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x00517AFE File Offset: 0x00515CFE
		public static IItemDropRule DropNothing()
		{
			return new DropNothing();
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x00517B05 File Offset: 0x00515D05
		public static IItemDropRule NormalvsExpert(int itemId, int chanceDenominatorInNormal, int chanceDenominatorInExpert)
		{
			return new DropBasedOnExpertMode(ItemDropRule.Common(itemId, chanceDenominatorInNormal, 1, 1), ItemDropRule.Common(itemId, chanceDenominatorInExpert, 1, 1));
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x00517B1E File Offset: 0x00515D1E
		public static IItemDropRule NormalvsExpertNotScalingWithLuck(int itemId, int chanceDenominatorInNormal, int chanceDenominatorInExpert)
		{
			return new DropBasedOnExpertMode(ItemDropRule.NotScalingWithLuck(itemId, chanceDenominatorInNormal, 1, 1), ItemDropRule.NotScalingWithLuck(itemId, chanceDenominatorInExpert, 1, 1));
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x00517B37 File Offset: 0x00515D37
		public static IItemDropRule NormalvsExpertOneFromOptionsNotScalingWithLuck(int chanceDenominatorInNormal, int chanceDenominatorInExpert, params int[] options)
		{
			return new DropBasedOnExpertMode(ItemDropRule.OneFromOptionsNotScalingWithLuck(chanceDenominatorInNormal, options), ItemDropRule.OneFromOptionsNotScalingWithLuck(chanceDenominatorInExpert, options));
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x00517B4C File Offset: 0x00515D4C
		public static IItemDropRule NormalvsExpertOneFromOptions(int chanceDenominatorInNormal, int chanceDenominatorInExpert, params int[] options)
		{
			return new DropBasedOnExpertMode(ItemDropRule.OneFromOptions(chanceDenominatorInNormal, options), ItemDropRule.OneFromOptions(chanceDenominatorInExpert, options));
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x00517B61 File Offset: 0x00515D61
		public static IItemDropRule Food(int itemId, int chanceDenominator, int minimumDropped = 1, int maximumDropped = 1)
		{
			return new ItemDropWithConditionRule(itemId, chanceDenominator, minimumDropped, maximumDropped, new Conditions.NotFromStatue(), 1);
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x00517B72 File Offset: 0x00515D72
		public static IItemDropRule StatusImmunityItem(int itemId, int dropsOutOfX)
		{
			return ItemDropRule.ExpertGetsRerolls(itemId, dropsOutOfX, 1);
		}
	}
}
