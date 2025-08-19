using System;

namespace Terraria.Social.Base
{
	// Token: 0x02000189 RID: 393
	public abstract class AWorkshopProgressReporter
	{
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06001B14 RID: 6932
		public abstract bool HasOngoingTasks { get; }

		// Token: 0x06001B15 RID: 6933
		public abstract bool TryGetProgress(out float progress);
	}
}
