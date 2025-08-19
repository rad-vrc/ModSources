using System;

namespace Terraria.Social.Base
{
	// Token: 0x020000F6 RID: 246
	public abstract class AchievementsSocialModule : ISocialModule
	{
		// Token: 0x060018A0 RID: 6304
		public abstract void Initialize();

		// Token: 0x060018A1 RID: 6305
		public abstract void Shutdown();

		// Token: 0x060018A2 RID: 6306
		public abstract byte[] GetEncryptionKey();

		// Token: 0x060018A3 RID: 6307
		public abstract string GetSavePath();

		// Token: 0x060018A4 RID: 6308
		public abstract void UpdateIntStat(string name, int value);

		// Token: 0x060018A5 RID: 6309
		public abstract void UpdateFloatStat(string name, float value);

		// Token: 0x060018A6 RID: 6310
		public abstract void CompleteAchievement(string name);

		// Token: 0x060018A7 RID: 6311
		public abstract bool IsAchievementCompleted(string name);

		// Token: 0x060018A8 RID: 6312
		public abstract void StoreStats();
	}
}
