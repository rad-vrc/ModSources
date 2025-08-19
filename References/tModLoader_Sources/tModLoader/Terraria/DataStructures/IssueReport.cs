using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000716 RID: 1814
	public class IssueReport
	{
		// Token: 0x060049D8 RID: 18904 RVA: 0x0064E52A File Offset: 0x0064C72A
		public IssueReport(string reportText)
		{
			this.timeReported = DateTime.Now;
			this.reportText = reportText;
		}

		// Token: 0x04005F05 RID: 24325
		public DateTime timeReported;

		// Token: 0x04005F06 RID: 24326
		public string reportText;
	}
}
