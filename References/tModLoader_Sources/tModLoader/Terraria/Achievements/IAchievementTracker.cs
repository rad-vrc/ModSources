using System;

namespace Terraria.Achievements
{
	// Token: 0x02000779 RID: 1913
	public interface IAchievementTracker
	{
		// Token: 0x06004D54 RID: 19796
		void ReportAs(string name);

		// Token: 0x06004D55 RID: 19797
		TrackerType GetTrackerType();

		// Token: 0x06004D56 RID: 19798
		void Load();

		// Token: 0x06004D57 RID: 19799
		void Clear();
	}
}
