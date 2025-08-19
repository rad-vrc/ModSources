using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000727 RID: 1831
	public struct PlayerFishingConditions
	{
		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06004A8B RID: 19083 RVA: 0x00667C19 File Offset: 0x00665E19
		public int PolePower
		{
			get
			{
				Item pole = this.Pole;
				if (pole == null)
				{
					return 0;
				}
				return pole.fishingPole;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06004A8C RID: 19084 RVA: 0x00667C2C File Offset: 0x00665E2C
		public int PoleItemType
		{
			get
			{
				Item pole = this.Pole;
				if (pole == null)
				{
					return 0;
				}
				return pole.type;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06004A8D RID: 19085 RVA: 0x00667C3F File Offset: 0x00665E3F
		public int BaitPower
		{
			get
			{
				Item bait = this.Bait;
				if (bait == null)
				{
					return 0;
				}
				return bait.bait;
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06004A8E RID: 19086 RVA: 0x00667C52 File Offset: 0x00665E52
		public int BaitItemType
		{
			get
			{
				Item bait = this.Bait;
				if (bait == null)
				{
					return 0;
				}
				return bait.type;
			}
		}

		// Token: 0x04005FDE RID: 24542
		public float LevelMultipliers;

		// Token: 0x04005FDF RID: 24543
		public int FinalFishingLevel;

		// Token: 0x04005FE0 RID: 24544
		public Item Pole;

		// Token: 0x04005FE1 RID: 24545
		public Item Bait;
	}
}
