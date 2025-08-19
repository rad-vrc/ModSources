using System;
using System.Collections.Generic;
using Terraria.Social.Base;

namespace Terraria.Social.Steam
{
	// Token: 0x02000170 RID: 368
	public class WorkshopProgressReporter : AWorkshopProgressReporter
	{
		// Token: 0x06001A48 RID: 6728 RVA: 0x004E4217 File Offset: 0x004E2417
		public WorkshopProgressReporter(List<WorkshopHelper.UGCBased.APublisherInstance> publisherInstances)
		{
			this._publisherInstances = publisherInstances;
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06001A49 RID: 6729 RVA: 0x004E4226 File Offset: 0x004E2426
		public override bool HasOngoingTasks
		{
			get
			{
				return this._publisherInstances.Count > 0;
			}
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x004E4238 File Offset: 0x004E2438
		public override bool TryGetProgress(out float progress)
		{
			float num = 0f;
			float num2 = 0f;
			for (int i = 0; i < this._publisherInstances.Count; i++)
			{
				float num3;
				if (this._publisherInstances[i].TryGetProgress(out num3))
				{
					num += num3;
					num2 += 1f;
				}
			}
			progress = 0f;
			if (num2 == 0f)
			{
				return false;
			}
			progress = num / num2;
			return true;
		}

		// Token: 0x04001590 RID: 5520
		private List<WorkshopHelper.UGCBased.APublisherInstance> _publisherInstances;
	}
}
