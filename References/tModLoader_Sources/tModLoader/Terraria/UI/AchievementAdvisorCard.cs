using System;
using Microsoft.Xna.Framework;
using Terraria.Achievements;

namespace Terraria.UI
{
	// Token: 0x0200009B RID: 155
	public class AchievementAdvisorCard
	{
		// Token: 0x060014BC RID: 5308 RVA: 0x004A3AA8 File Offset: 0x004A1CA8
		public AchievementAdvisorCard(Achievement achievement, float order)
		{
			this.achievement = achievement;
			this.order = order;
			this.achievementIndex = Main.Achievements.GetIconIndex(achievement.Name);
			this.frame = new Rectangle(this.achievementIndex % 8 * 66, this.achievementIndex / 8 * 66, 64, 64);
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x004A3B04 File Offset: 0x004A1D04
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

		// Token: 0x040010C5 RID: 4293
		private const int _iconSize = 64;

		// Token: 0x040010C6 RID: 4294
		private const int _iconSizeWithSpace = 66;

		// Token: 0x040010C7 RID: 4295
		private const int _iconsPerRow = 8;

		// Token: 0x040010C8 RID: 4296
		public Achievement achievement;

		// Token: 0x040010C9 RID: 4297
		public float order;

		// Token: 0x040010CA RID: 4298
		public Rectangle frame;

		// Token: 0x040010CB RID: 4299
		public int achievementIndex;
	}
}
