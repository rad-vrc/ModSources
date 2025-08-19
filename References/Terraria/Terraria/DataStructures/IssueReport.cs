using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000409 RID: 1033
	public class IssueReport
	{
		// Token: 0x06002B2B RID: 11051 RVA: 0x0059DB6F File Offset: 0x0059BD6F
		public IssueReport(string reportText)
		{
			this.timeReported = DateTime.Now;
			this.reportText = reportText;
		}

		// Token: 0x04004F5B RID: 20315
		public DateTime timeReported;

		// Token: 0x04004F5C RID: 20316
		public string reportText;
	}
}
