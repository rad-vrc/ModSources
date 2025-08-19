using System;

namespace Terraria.Achievements
{
	// Token: 0x02000495 RID: 1173
	public interface IAchievementTracker
	{
		// Token: 0x06002EA2 RID: 11938
		void ReportAs(string name);

		// Token: 0x06002EA3 RID: 11939
		TrackerType GetTrackerType();

		// Token: 0x06002EA4 RID: 11940
		void Load();

		// Token: 0x06002EA5 RID: 11941
		void Clear();
	}
}
