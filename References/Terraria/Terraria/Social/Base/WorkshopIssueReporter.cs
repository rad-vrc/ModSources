using System;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.Localization;

namespace Terraria.Social.Base
{
	// Token: 0x02000188 RID: 392
	public class WorkshopIssueReporter : IProvideReports
	{
		// Token: 0x14000040 RID: 64
		// (add) Token: 0x06001B05 RID: 6917 RVA: 0x004E6C48 File Offset: 0x004E4E48
		// (remove) Token: 0x06001B06 RID: 6918 RVA: 0x004E6C80 File Offset: 0x004E4E80
		public event Action OnNeedToOpenUI;

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06001B07 RID: 6919 RVA: 0x004E6CB8 File Offset: 0x004E4EB8
		// (remove) Token: 0x06001B08 RID: 6920 RVA: 0x004E6CF0 File Offset: 0x004E4EF0
		public event Action OnNeedToNotifyUI;

		// Token: 0x06001B09 RID: 6921 RVA: 0x004E6D28 File Offset: 0x004E4F28
		private void AddReport(string reportText)
		{
			IssueReport item = new IssueReport(reportText);
			this._reports.Add(item);
			while (this._reports.Count > this._maxReports)
			{
				this._reports.RemoveAt(0);
			}
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x004E6D69 File Offset: 0x004E4F69
		public List<IssueReport> GetReports()
		{
			return this._reports;
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x004E6D71 File Offset: 0x004E4F71
		private void OpenReportsScreen()
		{
			if (this.OnNeedToOpenUI != null)
			{
				this.OnNeedToOpenUI();
			}
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x004E6D86 File Offset: 0x004E4F86
		private void NotifyReportsScreen()
		{
			if (this.OnNeedToNotifyUI != null)
			{
				this.OnNeedToNotifyUI();
			}
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x004E6D9C File Offset: 0x004E4F9C
		public void ReportInstantUploadProblem(string textKey)
		{
			string textValue = Language.GetTextValue(textKey);
			this.AddReport(textValue);
			this.OpenReportsScreen();
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x004E6DBD File Offset: 0x004E4FBD
		public void ReportInstantUploadProblemFromValue(string text)
		{
			this.AddReport(text);
			this.OpenReportsScreen();
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x004E6DCC File Offset: 0x004E4FCC
		public void ReportDelayedUploadProblem(string textKey)
		{
			string textValue = Language.GetTextValue(textKey);
			this.AddReport(textValue);
			this.NotifyReportsScreen();
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x004E6DF0 File Offset: 0x004E4FF0
		public void ReportDelayedUploadProblemWithoutKnownReason(string textKey, string reasonValue)
		{
			object obj = new
			{
				Reason = reasonValue
			};
			string textValueWith = Language.GetTextValueWith(textKey, obj);
			this.AddReport(textValueWith);
			this.NotifyReportsScreen();
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x004E6E1C File Offset: 0x004E501C
		public void ReportDownloadProblem(string textKey, string path, Exception exception)
		{
			object obj = new
			{
				FilePath = path,
				Reason = exception.ToString()
			};
			string textValueWith = Language.GetTextValueWith(textKey, obj);
			this.AddReport(textValueWith);
			this.NotifyReportsScreen();
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x004E6E4C File Offset: 0x004E504C
		public void ReportManifestCreationProblem(string textKey, Exception exception)
		{
			object obj = new
			{
				Reason = exception.ToString()
			};
			string textValueWith = Language.GetTextValueWith(textKey, obj);
			this.AddReport(textValueWith);
			this.NotifyReportsScreen();
		}

		// Token: 0x040015F6 RID: 5622
		private int _maxReports = 1000;

		// Token: 0x040015F7 RID: 5623
		private List<IssueReport> _reports = new List<IssueReport>();
	}
}
