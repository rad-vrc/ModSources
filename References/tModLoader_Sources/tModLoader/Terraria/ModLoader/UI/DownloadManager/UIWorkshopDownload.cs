using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.ModLoader.UI.DownloadManager
{
	// Token: 0x0200027B RID: 635
	internal class UIWorkshopDownload : UIProgress, IDownloadProgress
	{
		// Token: 0x06002B87 RID: 11143 RVA: 0x00522D47 File Offset: 0x00520F47
		public UIWorkshopDownload()
		{
			this.downloadTimer = new Stopwatch();
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x00522D5A File Offset: 0x00520F5A
		public override void OnInitialize()
		{
			base.OnInitialize();
			this._cancelButton.Remove();
		}

		// Token: 0x06002B89 RID: 11145 RVA: 0x00522D70 File Offset: 0x00520F70
		public override void Update(GameTime gameTime)
		{
			if (this.needToUpdateProgressData)
			{
				UIWorkshopDownload.ProgressData localProgressData;
				lock (this)
				{
					localProgressData = this.progressData;
					this.progressData.reset = false;
					this.needToUpdateProgressData = false;
				}
				if (localProgressData.reset)
				{
					this._progressBar.DisplayText = Language.GetTextValue("tModLoader.MBDownloadingMod", localProgressData.displayName);
					this.downloadTimer.Restart();
				}
				this._progressBar.UpdateProgress(localProgressData.progress);
				double elapsedSeconds = this.downloadTimer.Elapsed.TotalSeconds;
				double speed = (elapsedSeconds > 0.0) ? ((double)localProgressData.bytesReceived / elapsedSeconds) : 0.0;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 3);
				defaultInterpolatedStringHandler.AppendFormatted(UIMemoryBar.SizeSuffix(localProgressData.bytesReceived, 2));
				defaultInterpolatedStringHandler.AppendLiteral(" / ");
				defaultInterpolatedStringHandler.AppendFormatted(UIMemoryBar.SizeSuffix(localProgressData.totalBytesNeeded, 2));
				defaultInterpolatedStringHandler.AppendLiteral(" (");
				defaultInterpolatedStringHandler.AppendFormatted(UIMemoryBar.SizeSuffix((long)speed, 2));
				defaultInterpolatedStringHandler.AppendLiteral("/s)");
				base.SubProgressText = defaultInterpolatedStringHandler.ToStringAndClear();
			}
			base.Update(gameTime);
		}

		/// <remarks>This will be called from a thread!</remarks>
		// Token: 0x06002B8A RID: 11146 RVA: 0x00522EB8 File Offset: 0x005210B8
		public void DownloadStarted(string displayName)
		{
			lock (this)
			{
				this.progressData.displayName = displayName;
				this.progressData.progress = 0f;
				this.progressData.bytesReceived = 0L;
				this.progressData.totalBytesNeeded = 0L;
				this.progressData.reset = true;
				this.needToUpdateProgressData = true;
			}
		}

		/// <remarks>This will be called from a thread!</remarks>
		// Token: 0x06002B8B RID: 11147 RVA: 0x00522F38 File Offset: 0x00521138
		public void UpdateDownloadProgress(float progress, long bytesReceived, long totalBytesNeeded)
		{
			lock (this)
			{
				this.progressData.progress = progress;
				this.progressData.bytesReceived = bytesReceived;
				this.progressData.totalBytesNeeded = totalBytesNeeded;
				this.needToUpdateProgressData = true;
			}
		}

		// Token: 0x04001BE7 RID: 7143
		private UIWorkshopDownload.ProgressData progressData;

		// Token: 0x04001BE8 RID: 7144
		private bool needToUpdateProgressData;

		// Token: 0x04001BE9 RID: 7145
		private Stopwatch downloadTimer;

		// Token: 0x02000A32 RID: 2610
		internal struct ProgressData
		{
			// Token: 0x04006CA0 RID: 27808
			public string displayName;

			// Token: 0x04006CA1 RID: 27809
			public float progress;

			// Token: 0x04006CA2 RID: 27810
			public long bytesReceived;

			// Token: 0x04006CA3 RID: 27811
			public long totalBytesNeeded;

			// Token: 0x04006CA4 RID: 27812
			public bool reset;
		}
	}
}
