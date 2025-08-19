using System;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.Localization;

namespace Terraria.Social.Base
{
	// Token: 0x0200010C RID: 268
	public class WorkshopIssueReporter : IProvideReports
	{
		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06001919 RID: 6425 RVA: 0x004BEA44 File Offset: 0x004BCC44
		// (remove) Token: 0x0600191A RID: 6426 RVA: 0x004BEA7C File Offset: 0x004BCC7C
		public event Action OnNeedToOpenUI;

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x0600191B RID: 6427 RVA: 0x004BEAB4 File Offset: 0x004BCCB4
		// (remove) Token: 0x0600191C RID: 6428 RVA: 0x004BEAEC File Offset: 0x004BCCEC
		public event Action OnNeedToNotifyUI;

		// Token: 0x0600191D RID: 6429 RVA: 0x004BEB24 File Offset: 0x004BCD24
		private void AddReport(string reportText)
		{
			IssueReport item = new IssueReport(reportText);
			this._reports.Add(item);
			while (this._reports.Count > this._maxReports)
			{
				this._reports.RemoveAt(0);
			}
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x004BEB65 File Offset: 0x004BCD65
		public List<IssueReport> GetReports()
		{
			return this._reports;
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x004BEB6D File Offset: 0x004BCD6D
		private void OpenReportsScreen()
		{
			if (this.OnNeedToOpenUI != null)
			{
				this.OnNeedToOpenUI();
			}
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x004BEB82 File Offset: 0x004BCD82
		private void NotifyReportsScreen()
		{
			if (this.OnNeedToNotifyUI != null)
			{
				this.OnNeedToNotifyUI();
			}
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x004BEB98 File Offset: 0x004BCD98
		public void ReportInstantUploadProblem(string textKey)
		{
			string textValue = Language.GetTextValue(textKey);
			Utils.LogAndConsoleErrorMessage(textValue);
			if (Main.dedServ)
			{
				return;
			}
			this.AddReport(textValue);
			this.OpenReportsScreen();
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x004BEBC7 File Offset: 0x004BCDC7
		public void ReportInstantUploadProblemFromValue(string text)
		{
			Utils.LogAndConsoleErrorMessage(text);
			if (Main.dedServ)
			{
				return;
			}
			this.AddReport(text);
			this.OpenReportsScreen();
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x004BEBE4 File Offset: 0x004BCDE4
		public void ReportDelayedUploadProblem(string textKey)
		{
			string textValue = Language.GetTextValue(textKey);
			Utils.LogAndConsoleErrorMessage(textValue);
			if (Main.dedServ)
			{
				return;
			}
			this.AddReport(textValue);
			this.NotifyReportsScreen();
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x004BEC14 File Offset: 0x004BCE14
		public void ReportDelayedUploadProblemWithoutKnownReason(string textKey, string reasonValue)
		{
			object obj = new
			{
				Reason = reasonValue
			};
			string textValueWith = Language.GetTextValueWith(textKey, obj);
			Utils.LogAndConsoleErrorMessage(textValueWith);
			if (Main.dedServ)
			{
				return;
			}
			this.AddReport(textValueWith);
			this.NotifyReportsScreen();
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x004BEC4C File Offset: 0x004BCE4C
		public void ReportDownloadProblem(string textKey, string path, Exception exception)
		{
			object obj = new
			{
				FilePath = path,
				Reason = exception.ToString()
			};
			string textValueWith = Language.GetTextValueWith(textKey, obj);
			Utils.LogAndConsoleErrorMessage(textValueWith);
			if (Main.dedServ)
			{
				return;
			}
			this.AddReport(textValueWith);
			this.NotifyReportsScreen();
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x004BEC8C File Offset: 0x004BCE8C
		public void ReportManifestCreationProblem(string textKey, Exception exception)
		{
			object obj = new
			{
				Reason = exception.ToString()
			};
			string textValueWith = Language.GetTextValueWith(textKey, obj);
			Utils.LogAndConsoleErrorMessage(textValueWith);
			if (Main.dedServ)
			{
				return;
			}
			this.AddReport(textValueWith);
			this.NotifyReportsScreen();
		}

		// Token: 0x040013A5 RID: 5029
		private int _maxReports = 1000;

		// Token: 0x040013A6 RID: 5030
		private List<IssueReport> _reports = new List<IssueReport>();
	}
}
