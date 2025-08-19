using System;

namespace Terraria.GameContent.UI.ResourceSets
{
	// Token: 0x020003BC RID: 956
	public struct PlayerStatsSnapshot
	{
		// Token: 0x06002A49 RID: 10825 RVA: 0x00598BC0 File Offset: 0x00596DC0
		public PlayerStatsSnapshot(Player player)
		{
			this.Life = player.statLife;
			this.Mana = player.statMana;
			this.LifeMax = player.statLifeMax2;
			this.ManaMax = player.statManaMax2;
			float num = 20f;
			int num2 = player.statLifeMax / 20;
			int num3 = (player.statLifeMax - 400) / 5;
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
			this.LifePerSegment = num;
			this.ManaPerSegment = 20f;
		}

		// Token: 0x04004D15 RID: 19733
		public int Life;

		// Token: 0x04004D16 RID: 19734
		public int LifeMax;

		// Token: 0x04004D17 RID: 19735
		public int LifeFruitCount;

		// Token: 0x04004D18 RID: 19736
		public float LifePerSegment;

		// Token: 0x04004D19 RID: 19737
		public int Mana;

		// Token: 0x04004D1A RID: 19738
		public int ManaMax;

		// Token: 0x04004D1B RID: 19739
		public float ManaPerSegment;
	}
}
