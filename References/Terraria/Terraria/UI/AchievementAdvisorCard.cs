using System;
using Microsoft.Xna.Framework;
using Terraria.Achievements;

namespace Terraria.UI
{
	// Token: 0x0200007B RID: 123
	public class AchievementAdvisorCard
	{
		// Token: 0x060011BE RID: 4542 RVA: 0x0048E7BC File Offset: 0x0048C9BC
		public AchievementAdvisorCard(Achievement achievement, float order)
		{
			this.achievement = achievement;
			this.order = order;
			this.achievementIndex = Main.Achievements.GetIconIndex(achievement.Name);
			this.frame = new Rectangle(this.achievementIndex % 8 * 66, this.achievementIndex / 8 * 66, 64, 64);
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0048E818 File Offset: 0x0048CA18
		public bool IsAchievableInWorld()
		{
			string name = this.achievement.Name;
			if (name == "MASTERMIND")
			{
				return WorldGen.crimson;
			}
			if (!(name == "WORM_FODDER"))
			{
				return !(name == "PLAY_ON_A_SPECIAL_SEED") || Main.specialSeedWorld;
			}
			return !WorldGen.crimson;
		}

		// Token: 0x04000FC9 RID: 4041
		private const int _iconSize = 64;

		// Token: 0x04000FCA RID: 4042
		private const int _iconSizeWithSpace = 66;

		// Token: 0x04000FCB RID: 4043
		private const int _iconsPerRow = 8;

		// Token: 0x04000FCC RID: 4044
		public Achievement achievement;

		// Token: 0x04000FCD RID: 4045
		public float order;

		// Token: 0x04000FCE RID: 4046
		public Rectangle frame;

		// Token: 0x04000FCF RID: 4047
		public int achievementIndex;
	}
}
