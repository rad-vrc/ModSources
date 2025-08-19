using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000293 RID: 659
	public class MechBossSpawnersDropRule : IItemDropRule
	{
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x0600204C RID: 8268 RVA: 0x0051881D File Offset: 0x00516A1D
		// (set) Token: 0x0600204D RID: 8269 RVA: 0x00518825 File Offset: 0x00516A25
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x0600204E RID: 8270 RVA: 0x0051882E File Offset: 0x00516A2E
		public MechBossSpawnersDropRule()
		{
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x0051884C File Offset: 0x00516A4C
		public bool CanDrop(DropAttemptInfo info)
		{
			return info.npc.value > 0f && Main.hardMode && (!NPC.downedMechBoss1 || !NPC.downedMechBoss2 || !NPC.downedMechBoss3) && !info.IsInSimulation;
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x00518888 File Offset: 0x00516A88
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			if (!NPC.downedMechBoss1 && info.player.RollLuck(2500) == 0)
			{
				CommonCode.DropItemFromNPC(info.npc, 556, 1, false);
				return new ItemDropAttemptResult
				{
					State = ItemDropAttemptResultState.Success
				};
			}
			if (!NPC.downedMechBoss2 && info.player.RollLuck(2500) == 0)
			{
				CommonCode.DropItemFromNPC(info.npc, 544, 1, false);
				return new ItemDropAttemptResult
				{
					State = ItemDropAttemptResultState.Success
				};
			}
			if (!NPC.downedMechBoss3 && info.player.RollLuck(2500) == 0)
			{
				CommonCode.DropItemFromNPC(info.npc, 557, 1, false);
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

		// Token: 0x06002051 RID: 8273 RVA: 0x00518960 File Offset: 0x00516B60
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

		// Token: 0x040046DF RID: 18143
		public Conditions.MechanicalBossesDummyCondition dummyCondition = new Conditions.MechanicalBossesDummyCondition();
	}
}
