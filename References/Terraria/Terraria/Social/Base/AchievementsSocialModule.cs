using System;

namespace Terraria.Social.Base
{
	// Token: 0x02000193 RID: 403
	public abstract class AchievementsSocialModule : ISocialModule
	{
		// Token: 0x06001B5C RID: 7004
		public abstract void Initialize();

		// Token: 0x06001B5D RID: 7005
		public abstract void Shutdown();

		// Token: 0x06001B5E RID: 7006
		public abstract byte[] GetEncryptionKey();

		// Token: 0x06001B5F RID: 7007
		public abstract string GetSavePath();

		// Token: 0x06001B60 RID: 7008
		public abstract void UpdateIntStat(string name, int value);

		// Token: 0x06001B61 RID: 7009
		public abstract void UpdateFloatStat(string name, float value);

		// Token: 0x06001B62 RID: 7010
		public abstract void CompleteAchievement(string name);

		// Token: 0x06001B63 RID: 7011
		public abstract bool IsAchievementCompleted(string name);

		// Token: 0x06001B64 RID: 7012
		public abstract void StoreStats();
	}
}
