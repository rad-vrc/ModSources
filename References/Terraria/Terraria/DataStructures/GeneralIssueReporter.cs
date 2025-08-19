using System;
using System.Collections.Generic;

namespace Terraria.DataStructures
{
	// Token: 0x0200040A RID: 1034
	public class GeneralIssueReporter : IProvideReports
	{
		// Token: 0x06002B2C RID: 11052 RVA: 0x0059DB89 File Offset: 0x0059BD89
		public void AddReport(string textToShow)
		{
			this._reports.Add(new IssueReport(textToShow));
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x0059DB9C File Offset: 0x0059BD9C
		public List<IssueReport> GetReports()
		{
			return this._reports;
		}

		// Token: 0x04004F5D RID: 20317
		private List<IssueReport> _reports = new List<IssueReport>();
	}
}
