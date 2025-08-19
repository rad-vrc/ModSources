using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000297 RID: 663
	public class DropOneByOne : IItemDropRule
	{
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06002062 RID: 8290 RVA: 0x00518C62 File Offset: 0x00516E62
		// (set) Token: 0x06002063 RID: 8291 RVA: 0x00518C6A File Offset: 0x00516E6A
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x06002064 RID: 8292 RVA: 0x00518C73 File Offset: 0x00516E73
		public DropOneByOne(int itemId, DropOneByOne.Parameters parameters)
		{
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
			this.parameters = parameters;
			this.itemId = itemId;
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x00518C94 File Offset: 0x00516E94
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			if (info.player.RollLuck(this.parameters.ChanceDenominator) < this.parameters.ChanceNumerator)
			{
				int num = info.rng.Next(this.parameters.MinimumItemDropsCount, this.parameters.MaximumItemDropsCount + 1);
				int activePlayersCount = Main.CurrentFrameFlags.ActivePlayersCount;
				int minValue = this.parameters.MinimumStackPerChunkBase + activePlayersCount * this.parameters.BonusMinDropsPerChunkPerPlayer;
				int num2 = this.parameters.MaximumStackPerChunkBase + activePlayersCount * this.parameters.BonusMaxDropsPerChunkPerPlayer;
				for (int i = 0; i < num; i++)
				{
					CommonCode.DropItemFromNPC(info.npc, this.itemId, info.rng.Next(minValue, num2 + 1), true);
				}
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

		// Token: 0x06002066 RID: 8294 RVA: 0x00518D7C File Offset: 0x00516F7C
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			float personalDropRate = this.parameters.GetPersonalDropRate();
			float dropRate = personalDropRate * ratesInfo.parentDroprateChance;
			drops.Add(new DropRateInfo(this.itemId, this.parameters.MinimumItemDropsCount * (this.parameters.MinimumStackPerChunkBase + this.parameters.BonusMinDropsPerChunkPerPlayer), this.parameters.MaximumItemDropsCount * (this.parameters.MaximumStackPerChunkBase + this.parameters.BonusMaxDropsPerChunkPerPlayer), dropRate, ratesInfo.conditions));
			Chains.ReportDroprates(this.ChainedRules, personalDropRate, drops, ratesInfo);
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x0003266D File Offset: 0x0003086D
		public bool CanDrop(DropAttemptInfo info)
		{
			return true;
		}

		// Token: 0x040046E4 RID: 18148
		public int itemId;

		// Token: 0x040046E5 RID: 18149
		public DropOneByOne.Parameters parameters;

		// Token: 0x0200064A RID: 1610
		public struct Parameters
		{
			// Token: 0x0600340A RID: 13322 RVA: 0x006077C3 File Offset: 0x006059C3
			public float GetPersonalDropRate()
			{
				return (float)this.ChanceNumerator / (float)this.ChanceDenominator;
			}

			// Token: 0x0400616F RID: 24943
			public int ChanceNumerator;

			// Token: 0x04006170 RID: 24944
			public int ChanceDenominator;

			// Token: 0x04006171 RID: 24945
			public int MinimumItemDropsCount;

			// Token: 0x04006172 RID: 24946
			public int MaximumItemDropsCount;

			// Token: 0x04006173 RID: 24947
			public int MinimumStackPerChunkBase;

			// Token: 0x04006174 RID: 24948
			public int MaximumStackPerChunkBase;

			// Token: 0x04006175 RID: 24949
			public int BonusMinDropsPerChunkPerPlayer;

			// Token: 0x04006176 RID: 24950
			public int BonusMaxDropsPerChunkPerPlayer;
		}
	}
}
