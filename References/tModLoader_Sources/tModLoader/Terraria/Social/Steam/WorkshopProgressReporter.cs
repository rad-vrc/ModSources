using System;
using System.Collections.Generic;
using Terraria.Social.Base;

namespace Terraria.Social.Steam
{
	// Token: 0x020000F4 RID: 244
	public class WorkshopProgressReporter : AWorkshopProgressReporter
	{
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x004BCAF0 File Offset: 0x004BACF0
		public override bool HasOngoingTasks
		{
			get
			{
				return this._publisherInstances.Count > 0;
			}
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x004BCB00 File Offset: 0x004BAD00
		public WorkshopProgressReporter(List<WorkshopHelper.UGCBased.APublisherInstance> publisherInstances)
		{
			this._publisherInstances = publisherInstances;
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x004BCB10 File Offset: 0x004BAD10
		public override bool TryGetProgress(out float progress)
		{
			float num = 0f;
			float num2 = 0f;
			for (int i = 0; i < this._publisherInstances.Count; i++)
			{
				float progress2;
				if (this._publisherInstances[i].TryGetProgress(out progress2))
				{
					num += progress2;
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

		// Token: 0x04001372 RID: 4978
		private List<WorkshopHelper.UGCBased.APublisherInstance> _publisherInstances;
	}
}
