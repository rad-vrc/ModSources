using System;
using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
	// Token: 0x02000207 RID: 519
	public class CustomFlagCondition : AchievementCondition
	{
		// Token: 0x06001DAE RID: 7598 RVA: 0x00506A84 File Offset: 0x00504C84
		private CustomFlagCondition(string name) : base(name)
		{
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x00506A8D File Offset: 0x00504C8D
		public static AchievementCondition Create(string name)
		{
			return new CustomFlagCondition(name);
		}
	}
}
