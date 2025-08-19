using System;
using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
	// Token: 0x020006B8 RID: 1720
	public class CustomFlagCondition : AchievementCondition
	{
		// Token: 0x060048BE RID: 18622 RVA: 0x0064B7E8 File Offset: 0x006499E8
		private CustomFlagCondition(string name) : base(name)
		{
		}

		// Token: 0x060048BF RID: 18623 RVA: 0x0064B7F1 File Offset: 0x006499F1
		public static AchievementCondition Create(string name)
		{
			return new CustomFlagCondition(name);
		}
	}
}
