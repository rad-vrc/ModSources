using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000610 RID: 1552
	public class MechBossSpawnersDropRule : IItemDropRule
	{
		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06004469 RID: 17513 RVA: 0x0060A43E File Offset: 0x0060863E
		// (set) Token: 0x0600446A RID: 17514 RVA: 0x0060A446 File Offset: 0x00608646
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x0600446B RID: 17515 RVA: 0x0060A44F File Offset: 0x0060864F
		public MechBossSpawnersDropRule()
		{
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x0600446C RID: 17516 RVA: 0x0060A46D File Offset: 0x0060866D
		public bool CanDrop(DropAttemptInfo info)
		{
			return info.npc.value > 0f && Main.hardMode && (!NPC.downedMechBoss1 || !NPC.downedMechBoss2 || !NPC.downedMechBoss3) && !info.IsInSimulation;
		}

		// Token: 0x0600446D RID: 17517 RVA: 0x0060A4A8 File Offset: 0x006086A8
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			if (!NPC.downedMechBoss1 && info.player.RollLuck(2500) == 0)
			{
				CommonCode.DropItem(info, 556, 1, false);
				return new ItemDropAttemptResult
				{
					State = ItemDropAttemptResultState.Success
				};
			}
			if (!NPC.downedMechBoss2 && info.player.RollLuck(2500) == 0)
			{
				CommonCode.DropItem(info, 544, 1, false);
				return new ItemDropAttemptResult
				{
					State = ItemDropAttemptResultState.Success
				};
			}
			if (!NPC.downedMechBoss3 && info.player.RollLuck(2500) == 0)
			{
				CommonCode.DropItem(info, 557, 1, false);
				return new ItemDropAttemptResult
				{
					State = ItemDropAttemptResultState.Success
				};
			}
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.FailedRandomRoll
			};
		}

		// Token: 0x0600446E RID: 17518 RVA: 0x0060A570 File Offset: 0x00608770
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			ratesInfo.AddCondition(this.dummyCondition);
			float num = 0.0004f;
			float dropRate = num * ratesInfo.parentDroprateChance;
			drops.Add(new DropRateInfo(556, 1, 1, dropRate, ratesInfo.conditions));
			drops.Add(new DropRateInfo(544, 1, 1, dropRate, ratesInfo.conditions));
			drops.Add(new DropRateInfo(557, 1, 1, dropRate, ratesInfo.conditions));
			Chains.ReportDroprates(this.ChainedRules, num, drops, ratesInfo);
		}

		// Token: 0x04005A79 RID: 23161
		public Conditions.MechanicalBossesDummyCondition dummyCondition = new Conditions.MechanicalBossesDummyCondition();
	}
}
