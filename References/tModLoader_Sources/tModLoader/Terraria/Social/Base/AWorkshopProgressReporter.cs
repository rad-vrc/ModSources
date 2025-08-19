using System;

namespace Terraria.Social.Base
{
	// Token: 0x020000F8 RID: 248
	public abstract class AWorkshopProgressReporter
	{
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060018B0 RID: 6320
		public abstract bool HasOngoingTasks { get; }

		// Token: 0x060018B1 RID: 6321
		public abstract bool TryGetProgress(out float progress);
	}
}
