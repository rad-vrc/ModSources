using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Terraria.DataStructures
{
	// Token: 0x0200070D RID: 1805
	public struct GameModeData : IEquatable<GameModeData>
	{
		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x060049AB RID: 18859 RVA: 0x0064DDFD File Offset: 0x0064BFFD
		// (set) Token: 0x060049AC RID: 18860 RVA: 0x0064DE05 File Offset: 0x0064C005
		public int Id { readonly get; set; }

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x060049AD RID: 18861 RVA: 0x0064DE0E File Offset: 0x0064C00E
		// (set) Token: 0x060049AE RID: 18862 RVA: 0x0064DE16 File Offset: 0x0064C016
		public bool IsExpertMode { readonly get; set; }

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x060049AF RID: 18863 RVA: 0x0064DE1F File Offset: 0x0064C01F
		// (set) Token: 0x060049B0 RID: 18864 RVA: 0x0064DE27 File Offset: 0x0064C027
		public bool IsMasterMode { readonly get; set; }

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x060049B1 RID: 18865 RVA: 0x0064DE30 File Offset: 0x0064C030
		// (set) Token: 0x060049B2 RID: 18866 RVA: 0x0064DE38 File Offset: 0x0064C038
		public bool IsJourneyMode { readonly get; set; }

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x060049B3 RID: 18867 RVA: 0x0064DE41 File Offset: 0x0064C041
		// (set) Token: 0x060049B4 RID: 18868 RVA: 0x0064DE49 File Offset: 0x0064C049
		public float EnemyMaxLifeMultiplier { readonly get; set; }

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x060049B5 RID: 18869 RVA: 0x0064DE52 File Offset: 0x0064C052
		// (set) Token: 0x060049B6 RID: 18870 RVA: 0x0064DE5A File Offset: 0x0064C05A
		public float EnemyDamageMultiplier { readonly get; set; }

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x060049B7 RID: 18871 RVA: 0x0064DE63 File Offset: 0x0064C063
		// (set) Token: 0x060049B8 RID: 18872 RVA: 0x0064DE6B File Offset: 0x0064C06B
		public float DebuffTimeMultiplier { readonly get; set; }

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x060049B9 RID: 18873 RVA: 0x0064DE74 File Offset: 0x0064C074
		// (set) Token: 0x060049BA RID: 18874 RVA: 0x0064DE7C File Offset: 0x0064C07C
		public float KnockbackToEnemiesMultiplier { readonly get; set; }

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x060049BB RID: 18875 RVA: 0x0064DE85 File Offset: 0x0064C085
		// (set) Token: 0x060049BC RID: 18876 RVA: 0x0064DE8D File Offset: 0x0064C08D
		public float TownNPCDamageMultiplier { readonly get; set; }

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x060049BD RID: 18877 RVA: 0x0064DE96 File Offset: 0x0064C096
		// (set) Token: 0x060049BE RID: 18878 RVA: 0x0064DE9E File Offset: 0x0064C09E
		public float EnemyDefenseMultiplier { readonly get; set; }

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x060049BF RID: 18879 RVA: 0x0064DEA7 File Offset: 0x0064C0A7
		// (set) Token: 0x060049C0 RID: 18880 RVA: 0x0064DEAF File Offset: 0x0064C0AF
		public float EnemyMoneyDropMultiplier { readonly get; set; }

		// Token: 0x060049C1 RID: 18881 RVA: 0x0064DEB8 File Offset: 0x0064C0B8
		[CompilerGenerated]
		public override readonly string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GameModeData");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060049C2 RID: 18882 RVA: 0x0064DF04 File Offset: 0x0064C104
		[CompilerGenerated]
		private readonly bool PrintMembers(StringBuilder builder)
		{
			builder.Append("Id = ");
			builder.Append(this.Id.ToString());
			builder.Append(", IsExpertMode = ");
			builder.Append(this.IsExpertMode.ToString());
			builder.Append(", IsMasterMode = ");
			builder.Append(this.IsMasterMode.ToString());
			builder.Append(", IsJourneyMode = ");
			builder.Append(this.IsJourneyMode.ToString());
			builder.Append(", EnemyMaxLifeMultiplier = ");
			builder.Append(this.EnemyMaxLifeMultiplier.ToString());
			builder.Append(", EnemyDamageMultiplier = ");
			builder.Append(this.EnemyDamageMultiplier.ToString());
			builder.Append(", DebuffTimeMultiplier = ");
			builder.Append(this.DebuffTimeMultiplier.ToString());
			builder.Append(", KnockbackToEnemiesMultiplier = ");
			builder.Append(this.KnockbackToEnemiesMultiplier.ToString());
			builder.Append(", TownNPCDamageMultiplier = ");
			builder.Append(this.TownNPCDamageMultiplier.ToString());
			builder.Append(", EnemyDefenseMultiplier = ");
			builder.Append(this.EnemyDefenseMultiplier.ToString());
			builder.Append(", EnemyMoneyDropMultiplier = ");
			builder.Append(this.EnemyMoneyDropMultiplier.ToString());
			return true;
		}

		// Token: 0x060049C3 RID: 18883 RVA: 0x0064E0BF File Offset: 0x0064C2BF
		[CompilerGenerated]
		public static bool operator !=(GameModeData left, GameModeData right)
		{
			return !(left == right);
		}

		// Token: 0x060049C4 RID: 18884 RVA: 0x0064E0CB File Offset: 0x0064C2CB
		[CompilerGenerated]
		public static bool operator ==(GameModeData left, GameModeData right)
		{
			return left.Equals(right);
		}

		// Token: 0x060049C5 RID: 18885 RVA: 0x0064E0D8 File Offset: 0x0064C2D8
		[CompilerGenerated]
		public override readonly int GetHashCode()
		{
			return (((((((((EqualityComparer<int>.Default.GetHashCode(this.<Id>k__BackingField) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsExpertMode>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsMasterMode>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsJourneyMode>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<EnemyMaxLifeMultiplier>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<EnemyDamageMultiplier>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<DebuffTimeMultiplier>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<KnockbackToEnemiesMultiplier>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<TownNPCDamageMultiplier>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<EnemyDefenseMultiplier>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<EnemyMoneyDropMultiplier>k__BackingField);
		}

		// Token: 0x060049C6 RID: 18886 RVA: 0x0064E1DB File Offset: 0x0064C3DB
		[CompilerGenerated]
		public override readonly bool Equals(object obj)
		{
			return obj is GameModeData && this.Equals((GameModeData)obj);
		}

		// Token: 0x060049C7 RID: 18887 RVA: 0x0064E1F4 File Offset: 0x0064C3F4
		[CompilerGenerated]
		public readonly bool Equals(GameModeData other)
		{
			return EqualityComparer<int>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsExpertMode>k__BackingField, other.<IsExpertMode>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsMasterMode>k__BackingField, other.<IsMasterMode>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsJourneyMode>k__BackingField, other.<IsJourneyMode>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<EnemyMaxLifeMultiplier>k__BackingField, other.<EnemyMaxLifeMultiplier>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<EnemyDamageMultiplier>k__BackingField, other.<EnemyDamageMultiplier>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<DebuffTimeMultiplier>k__BackingField, other.<DebuffTimeMultiplier>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<KnockbackToEnemiesMultiplier>k__BackingField, other.<KnockbackToEnemiesMultiplier>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<TownNPCDamageMultiplier>k__BackingField, other.<TownNPCDamageMultiplier>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<EnemyDefenseMultiplier>k__BackingField, other.<EnemyDefenseMultiplier>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<EnemyMoneyDropMultiplier>k__BackingField, other.<EnemyMoneyDropMultiplier>k__BackingField);
		}

		// Token: 0x04005EF4 RID: 24308
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

		// Token: 0x04005EF5 RID: 24309
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

		// Token: 0x04005EF6 RID: 24310
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

		// Token: 0x04005EF7 RID: 24311
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
