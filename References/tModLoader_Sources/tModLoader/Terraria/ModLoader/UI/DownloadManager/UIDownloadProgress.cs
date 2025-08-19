using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Terraria.Localization;

namespace Terraria.ModLoader.UI.DownloadManager
{
	// Token: 0x02000279 RID: 633
	internal class UIDownloadProgress : UIProgress
	{
		// Token: 0x14000052 RID: 82
		// (add) Token: 0x06002B7A RID: 11130 RVA: 0x00522964 File Offset: 0x00520B64
		// (remove) Token: 0x06002B7B RID: 11131 RVA: 0x0052299C File Offset: 0x00520B9C
		public event Action OnDownloadsComplete;

		// Token: 0x06002B7C RID: 11132 RVA: 0x005229D4 File Offset: 0x00520BD4
		public override void OnActivate()
		{
			base.OnActivate();
			this.downloadTimer = new Stopwatch();
			if (this._downloads.Count <= 0)
			{
				Logging.tML.Warn("UIDownloadProgress was activated but no downloads were present.");
				Main.menuMode = this.gotoMenu;
				return;
			}
			this._cts = new CancellationTokenSource();
			base.OnCancel += delegate()
			{
				this._cts.Cancel();
			};
			this.downloadTimer.Restart();
			this.DownloadMods();
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x00522A4C File Offset: 0x00520C4C
		public override void OnDeactivate()
		{
			base.OnDeactivate();
			foreach (DownloadFile download in this._downloads)
			{
				Logging.tML.Warn("UIDownloadProgress was deactivated but download [" + download.FilePath + "] was still present.");
			}
			this._downloadFile = null;
			this.OnDownloadsComplete = null;
			CancellationTokenSource cts = this._cts;
			if (cts != null)
			{
				cts.Dispose();
			}
			this._downloads.Clear();
			this._progressBar.UpdateProgress(0f);
		}

		// Token: 0x06002B7E RID: 11134 RVA: 0x00522AF8 File Offset: 0x00520CF8
		public void HandleDownloads(params DownloadFile[] downloads)
		{
			foreach (DownloadFile download in downloads)
			{
				if (download.Verify())
				{
					this._downloads.Add(download);
				}
			}
			this.Show();
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x00522B33 File Offset: 0x00520D33
		public void Show()
		{
			Main.menuMode = 10020;
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x00522B40 File Offset: 0x00520D40
		private void DownloadMods()
		{
			this.downloadTimer.Start();
			this._downloadFile = this._downloads.First<DownloadFile>();
			if (this._downloadFile == null)
			{
				return;
			}
			this._progressBar.UpdateProgress(0f);
			this._progressBar.DisplayText = Language.GetTextValue("tModLoader.MBDownloadingMod", this._downloadFile.DisplayText);
			this._downloadFile.Download(this._cts.Token, new DownloadFile.ProgressUpdated(this.UpdateDownloadProgress)).ContinueWith(new Action<Task<DownloadFile>>(this.HandleNextDownload), this._cts.Token);
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x00522BE4 File Offset: 0x00520DE4
		private void UpdateDownloadProgress(float progress, long bytesReceived, long totalBytesNeeded)
		{
			this._progressBar.UpdateProgress(progress);
			double elapsedSeconds = this.downloadTimer.Elapsed.TotalSeconds;
			double speed = (elapsedSeconds > 0.0) ? ((double)bytesReceived / elapsedSeconds) : 0.0;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 3);
			defaultInterpolatedStringHandler.AppendFormatted(UIMemoryBar.SizeSuffix(bytesReceived, 2));
			defaultInterpolatedStringHandler.AppendLiteral(" / ");
			defaultInterpolatedStringHandler.AppendFormatted(UIMemoryBar.SizeSuffix(totalBytesNeeded, 2));
			defaultInterpolatedStringHandler.AppendLiteral(" (");
			defaultInterpolatedStringHandler.AppendFormatted(UIMemoryBar.SizeSuffix((long)speed, 2));
			defaultInterpolatedStringHandler.AppendLiteral("/s)");
			base.SubProgressText = defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x00522C94 File Offset: 0x00520E94
		private void HandleNextDownload(Task<DownloadFile> task)
		{
			bool hasError = task.Exception != null;
			this._downloads.Remove(this._downloadFile);
			if (this._downloads.Count > 0 && !hasError)
			{
				this.downloadTimer.Restart();
				this.DownloadMods();
				return;
			}
			if (hasError)
			{
				Logging.tML.Error("There was a problem downloading the mod " + this._downloadFile.DisplayText, task.Exception);
			}
			Main.menuMode = this.gotoMenu;
			Action onDownloadsComplete = this.OnDownloadsComplete;
			if (onDownloadsComplete == null)
			{
				return;
			}
			onDownloadsComplete();
		}

		// Token: 0x04001BE3 RID: 7139
		private DownloadFile _downloadFile;

		// Token: 0x04001BE4 RID: 7140
		private readonly List<DownloadFile> _downloads = new List<DownloadFile>();

		// Token: 0x04001BE5 RID: 7141
		internal CancellationTokenSource _cts;

		// Token: 0x04001BE6 RID: 7142
		private Stopwatch downloadTimer;
	}
}
