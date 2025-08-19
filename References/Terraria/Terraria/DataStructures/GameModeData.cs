using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000405 RID: 1029
	public class GameModeData
	{
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06002B0B RID: 11019 RVA: 0x0059D764 File Offset: 0x0059B964
		// (set) Token: 0x06002B0C RID: 11020 RVA: 0x0059D76C File Offset: 0x0059B96C
		public int Id { get; private set; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06002B0D RID: 11021 RVA: 0x0059D775 File Offset: 0x0059B975
		// (set) Token: 0x06002B0E RID: 11022 RVA: 0x0059D77D File Offset: 0x0059B97D
		public bool IsExpertMode { get; private set; }

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06002B0F RID: 11023 RVA: 0x0059D786 File Offset: 0x0059B986
		// (set) Token: 0x06002B10 RID: 11024 RVA: 0x0059D78E File Offset: 0x0059B98E
		public bool IsMasterMode { get; private set; }

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06002B11 RID: 11025 RVA: 0x0059D797 File Offset: 0x0059B997
		// (set) Token: 0x06002B12 RID: 11026 RVA: 0x0059D79F File Offset: 0x0059B99F
		public bool IsJourneyMode { get; private set; }

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06002B13 RID: 11027 RVA: 0x0059D7A8 File Offset: 0x0059B9A8
		// (set) Token: 0x06002B14 RID: 11028 RVA: 0x0059D7B0 File Offset: 0x0059B9B0
		public float EnemyMaxLifeMultiplier { get; private set; }

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06002B15 RID: 11029 RVA: 0x0059D7B9 File Offset: 0x0059B9B9
		// (set) Token: 0x06002B16 RID: 11030 RVA: 0x0059D7C1 File Offset: 0x0059B9C1
		public float EnemyDamageMultiplier { get; private set; }

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06002B17 RID: 11031 RVA: 0x0059D7CA File Offset: 0x0059B9CA
		// (set) Token: 0x06002B18 RID: 11032 RVA: 0x0059D7D2 File Offset: 0x0059B9D2
		public float DebuffTimeMultiplier { get; private set; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06002B19 RID: 11033 RVA: 0x0059D7DB File Offset: 0x0059B9DB
		// (set) Token: 0x06002B1A RID: 11034 RVA: 0x0059D7E3 File Offset: 0x0059B9E3
		public float KnockbackToEnemiesMultiplier { get; private set; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06002B1B RID: 11035 RVA: 0x0059D7EC File Offset: 0x0059B9EC
		// (set) Token: 0x06002B1C RID: 11036 RVA: 0x0059D7F4 File Offset: 0x0059B9F4
		public float TownNPCDamageMultiplier { get; private set; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06002B1D RID: 11037 RVA: 0x0059D7FD File Offset: 0x0059B9FD
		// (set) Token: 0x06002B1E RID: 11038 RVA: 0x0059D805 File Offset: 0x0059BA05
		public float EnemyDefenseMultiplier { get; private set; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06002B1F RID: 11039 RVA: 0x0059D80E File Offset: 0x0059BA0E
		// (set) Token: 0x06002B20 RID: 11040 RVA: 0x0059D816 File Offset: 0x0059BA16
		public float EnemyMoneyDropMultiplier { get; private set; }

		// Token: 0x04004F48 RID: 20296
		public static readonly GameModeData NormalMode = new GameModeData
		{
			Id = 0,
			EnemyMaxLifeMultiplier = 1f,
			EnemyDamageMultiplier = 1f,
			DebuffTimeMultiplier = 1f,
			KnockbackToEnemiesMultiplier = 1f,
			TownNPCDamageMultiplier = 1f,
			EnemyDefenseMultiplier = 1f,
			EnemyMoneyDropMultiplier = 1f
		};

		// Token: 0x04004F49 RID: 20297
		public static readonly GameModeData ExpertMode = new GameModeData
		{
			Id = 1,
			IsExpertMode = true,
			EnemyMaxLifeMultiplier = 2f,
			EnemyDamageMultiplier = 2f,
			DebuffTimeMultiplier = 2f,
			KnockbackToEnemiesMultiplier = 0.9f,
			TownNPCDamageMultiplier = 1.5f,
			EnemyDefenseMultiplier = 1f,
			EnemyMoneyDropMultiplier = 2.5f
		};

		// Token: 0x04004F4A RID: 20298
		public static readonly GameModeData MasterMode = new GameModeData
		{
			Id = 2,
			IsExpertMode = true,
			IsMasterMode = true,
			EnemyMaxLifeMultiplier = 3f,
			EnemyDamageMultiplier = 3f,
			DebuffTimeMultiplier = 2.5f,
			KnockbackToEnemiesMultiplier = 0.8f,
			TownNPCDamageMultiplier = 1.75f,
			EnemyDefenseMultiplier = 1f,
			EnemyMoneyDropMultiplier = 2.5f
		};

		// Token: 0x04004F4B RID: 20299
		public static readonly GameModeData CreativeMode = new GameModeData
		{
			Id = 3,
			IsJourneyMode = true,
			EnemyMaxLifeMultiplier = 1f,
			EnemyDamageMultiplier = 1f,
			DebuffTimeMultiplier = 1f,
			KnockbackToEnemiesMultiplier = 1f,
			TownNPCDamageMultiplier = 2f,
			EnemyDefenseMultiplier = 1f,
			EnemyMoneyDropMultiplier = 1f
		};
	}
}
