using System;

namespace Terraria.GameContent.UI.ResourceSets
{
	// Token: 0x020004F3 RID: 1267
	public struct PlayerStatsSnapshot
	{
		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06003D6D RID: 15725 RVA: 0x005CA8C3 File Offset: 0x005C8AC3
		public float LifePerSegment
		{
			get
			{
				return (float)this.LifeMax / (float)this.AmountOfLifeHearts;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06003D6E RID: 15726 RVA: 0x005CA8D4 File Offset: 0x005C8AD4
		public float ManaPerSegment
		{
			get
			{
				return (float)this.ManaMax / (float)this.AmountOfManaStars;
			}
		}

		/// <summary>
		/// How many life hearts should be drawn
		/// </summary>
		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06003D6F RID: 15727 RVA: 0x005CA8E5 File Offset: 0x005C8AE5
		// (set) Token: 0x06003D70 RID: 15728 RVA: 0x005CA8ED File Offset: 0x005C8AED
		public int AmountOfLifeHearts
		{
			get
			{
				return this.numLifeHearts;
			}
			set
			{
				this.numLifeHearts = Utils.Clamp<int>(value, int.MinValue, 20);
			}
		}

		/// <summary>
		/// How many mana stars should be drawn
		/// </summary>
		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06003D71 RID: 15729 RVA: 0x005CA902 File Offset: 0x005C8B02
		// (set) Token: 0x06003D72 RID: 15730 RVA: 0x005CA90A File Offset: 0x005C8B0A
		public int AmountOfManaStars
		{
			get
			{
				return this.numManaStars;
			}
			set
			{
				this.numManaStars = Utils.Clamp<int>(value, int.MinValue, 20);
			}
		}

		// Token: 0x06003D73 RID: 15731 RVA: 0x005CA920 File Offset: 0x005C8B20
		public PlayerStatsSnapshot(Player player)
		{
			this.Life = player.statLife;
			this.Mana = player.statMana;
			this.LifeMax = player.statLifeMax2;
			this.ManaMax = player.statManaMax2;
			float num = 20f;
			int num2 = player.statLifeMax / 20;
			int num3 = player.ConsumedLifeFruit;
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num3 > 0)
			{
				num2 = player.statLifeMax / (20 + num3 / 4);
				num = (float)player.statLifeMax / 20f;
			}
			int num4 = player.statLifeMax2 - player.statLifeMax;
			num += (float)(num4 / num2);
			this.LifeFruitCount = num3;
			this.numLifeHearts = (int)((float)this.LifeMax / num);
			this.numManaStars = (int)((double)this.ManaMax / 20.0);
		}

		// Token: 0x0400563C RID: 22076
		public int Life;

		// Token: 0x0400563D RID: 22077
		public int LifeMax;

		// Token: 0x0400563E RID: 22078
		public int LifeFruitCount;

		// Token: 0x0400563F RID: 22079
		public int Mana;

		// Token: 0x04005640 RID: 22080
		public int ManaMax;

		// Token: 0x04005641 RID: 22081
		private int numLifeHearts;

		// Token: 0x04005642 RID: 22082
		private int numManaStars;
	}
}
