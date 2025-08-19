using System;

namespace Terraria.ModLoader.UI.DownloadManager
{
	// Token: 0x0200027A RID: 634
	public interface IDownloadProgress
	{
		// Token: 0x06002B85 RID: 11141
		void DownloadStarted(string displayName);

		// Token: 0x06002B86 RID: 11142
		void UpdateDownloadProgress(float progress, long bytesReceived, long totalBytesNeeded);
	}
}
