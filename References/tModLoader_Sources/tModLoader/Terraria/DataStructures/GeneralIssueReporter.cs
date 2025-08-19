using System;
using System.Collections.Generic;

namespace Terraria.DataStructures
{
	// Token: 0x0200070E RID: 1806
	public class GeneralIssueReporter : IProvideReports
	{
		// Token: 0x060049C9 RID: 18889 RVA: 0x0064E4ED File Offset: 0x0064C6ED
		public void AddReport(string textToShow)
		{
			this._reports.Add(new IssueReport(textToShow));
		}

		// Token: 0x060049CA RID: 18890 RVA: 0x0064E500 File Offset: 0x0064C700
		public List<IssueReport> GetReports()
		{
			return this._reports;
		}

		// Token: 0x04005F03 RID: 24323
		private List<IssueReport> _reports = new List<IssueReport>();
	}
}
