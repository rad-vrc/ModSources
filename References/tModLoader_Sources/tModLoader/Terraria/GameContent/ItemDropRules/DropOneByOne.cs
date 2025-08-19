using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x020005FA RID: 1530
	public class DropOneByOne : IItemDropRule
	{
		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x060043B7 RID: 17335 RVA: 0x00600B02 File Offset: 0x005FED02
		// (set) Token: 0x060043B8 RID: 17336 RVA: 0x00600B0A File Offset: 0x005FED0A
		public DropOneByOne.Parameters parameters { get; private set; }

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x060043B9 RID: 17337 RVA: 0x00600B13 File Offset: 0x005FED13
		// (set) Token: 0x060043BA RID: 17338 RVA: 0x00600B1B File Offset: 0x005FED1B
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x060043BB RID: 17339 RVA: 0x00600B24 File Offset: 0x005FED24
		public DropOneByOne(int itemId, DropOneByOne.Parameters parameters)
		{
			if (parameters.MinimumItemDropsCount > parameters.MaximumItemDropsCount)
			{
				throw new ArgumentException("MinimumItemDropsCount must be lesser or equal to MaximumItemDropsCount.", "parameters");
			}
			if (parameters.MinimumStackPerChunkBase > parameters.MaximumStackPerChunkBase)
			{
				throw new ArgumentException("MinimumStackPerChunkBase must be lesser or equal to MaximumStackPerChunkBase.", "parameters");
			}
			if (parameters.BonusMinDropsPerChunkPerPlayer > parameters.BonusMaxDropsPerChunkPerPlayer)
			{
				throw new ArgumentException("BonusMinDropsPerChunkPerPlayer must be lesser or equal to BonusMaxDropsPerChunkPerPlayer.", "parameters");
			}
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
			this.parameters = parameters;
			this.itemId = itemId;
		}

		// Token: 0x060043BC RID: 17340 RVA: 0x00600BAC File Offset: 0x005FEDAC
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
					CommonCode.DropItem(info, this.itemId, info.rng.Next(minValue, num2 + 1), true);
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

		// Token: 0x060043BD RID: 17341 RVA: 0x00600C90 File Offset: 0x005FEE90
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			float personalDropRate = this.parameters.GetPersonalDropRate();
			float dropRate = personalDropRate * ratesInfo.parentDroprateChance;
			drops.Add(new DropRateInfo(this.itemId, this.parameters.MinimumItemDropsCount * (this.parameters.MinimumStackPerChunkBase + this.parameters.BonusMinDropsPerChunkPerPlayer), this.parameters.MaximumItemDropsCount * (this.parameters.MaximumStackPerChunkBase + this.parameters.BonusMaxDropsPerChunkPerPlayer), dropRate, ratesInfo.conditions));
			Chains.ReportDroprates(this.ChainedRules, personalDropRate, drops, ratesInfo);
		}

		// Token: 0x060043BE RID: 17342 RVA: 0x00600D21 File Offset: 0x005FEF21
		public bool CanDrop(DropAttemptInfo info)
		{
			return true;
		}

		// Token: 0x04005A4A RID: 23114
		public int itemId;

		// Token: 0x02000CC7 RID: 3271
		public struct Parameters
		{
			// Token: 0x0600616F RID: 24943 RVA: 0x006D32DD File Offset: 0x006D14DD
			public float GetPersonalDropRate()
			{
				return (float)this.ChanceNumerator / (float)this.ChanceDenominator;
			}

			// Token: 0x040079F0 RID: 31216
			public int ChanceNumerator;

			// Token: 0x040079F1 RID: 31217
			public int ChanceDenominator;

			// Token: 0x040079F2 RID: 31218
			public int MinimumItemDropsCount;

			// Token: 0x040079F3 RID: 31219
			public int MaximumItemDropsCount;

			// Token: 0x040079F4 RID: 31220
			public int MinimumStackPerChunkBase;

			// Token: 0x040079F5 RID: 31221
			public int MaximumStackPerChunkBase;

			// Token: 0x040079F6 RID: 31222
			public int BonusMinDropsPerChunkPerPlayer;

			// Token: 0x040079F7 RID: 31223
			public int BonusMaxDropsPerChunkPerPlayer;
		}
	}
}
