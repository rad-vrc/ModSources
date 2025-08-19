using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Terraria.ModLoader.UI.DownloadManager
{
	// Token: 0x02000278 RID: 632
	internal class DownloadFile
	{
		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06002B6C RID: 11116 RVA: 0x005224F5 File Offset: 0x005206F5
		// (set) Token: 0x06002B6D RID: 11117 RVA: 0x005224FD File Offset: 0x005206FD
		public HttpWebRequest Request { get; private set; }

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x06002B6E RID: 11118 RVA: 0x00522508 File Offset: 0x00520708
		// (remove) Token: 0x06002B6F RID: 11119 RVA: 0x00522540 File Offset: 0x00520740
		public event DownloadFile.ProgressUpdated OnUpdateProgress;

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x06002B70 RID: 11120 RVA: 0x00522578 File Offset: 0x00520778
		// (remove) Token: 0x06002B71 RID: 11121 RVA: 0x005225B0 File Offset: 0x005207B0
		public event Action OnComplete;

		// Token: 0x06002B72 RID: 11122 RVA: 0x005225E5 File Offset: 0x005207E5
		public DownloadFile(string url, string filePath, string displayText)
		{
			this.Url = url;
			this.FilePath = filePath;
			this.DisplayText = displayText;
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x00522618 File Offset: 0x00520818
		public bool Verify()
		{
			return !string.IsNullOrWhiteSpace(this.Url) && !string.IsNullOrWhiteSpace(this.FilePath);
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x0052263C File Offset: 0x0052083C
		public Task<DownloadFile> Download(CancellationToken token, DownloadFile.ProgressUpdated updateProgressAction = null)
		{
			this.SetupDownloadRequest();
			if (updateProgressAction != null)
			{
				this.OnUpdateProgress = updateProgressAction;
			}
			return Task.Factory.FromAsync<WebResponse>(new Func<AsyncCallback, object, IAsyncResult>(this.Request.BeginGetResponse), ([Nullable(1)] IAsyncResult asyncResult) => this.Request.EndGetResponse(asyncResult), null).ContinueWith<DownloadFile>(([Nullable(new byte[]
			{
				1,
				0
			})] Task<WebResponse> t) => this.HandleResponse(t.Result, token), token);
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x005226B0 File Offset: 0x005208B0
		private void AbortDownload(string filePath)
		{
			this._aborted = true;
			HttpWebRequest request = this.Request;
			if (request != null)
			{
				request.Abort();
			}
			FileStream fileStream = this._fileStream;
			if (fileStream != null)
			{
				fileStream.Flush();
			}
			FileStream fileStream2 = this._fileStream;
			if (fileStream2 != null)
			{
				fileStream2.Close();
			}
			if (File.Exists(filePath))
			{
				File.Delete(filePath);
			}
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x00522708 File Offset: 0x00520908
		private DownloadFile HandleResponse(WebResponse response, CancellationToken token)
		{
			long contentLength = response.ContentLength;
			if (contentLength < 0L)
			{
				string txt = "Could not get a proper content length for DownloadFile[" + this.DisplayText + "]";
				Logging.tML.Error(txt);
				throw new Exception(txt);
			}
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 4);
			defaultInterpolatedStringHandler.AppendFormatted(new FileInfo(this.FilePath).Directory.FullName);
			defaultInterpolatedStringHandler.AppendFormatted<char>(Path.DirectorySeparatorChar);
			defaultInterpolatedStringHandler.AppendFormatted<long>(DateTime.Now.Ticks);
			defaultInterpolatedStringHandler.AppendFormatted(".tmp");
			string _downloadPath = defaultInterpolatedStringHandler.ToStringAndClear();
			this._fileStream = new FileStream(_downloadPath, FileMode.Create);
			Stream responseStream = response.GetResponseStream();
			int currentIndex = 0;
			byte[] buf = new byte[1048576];
			try
			{
				int r;
				while ((r = responseStream.Read(buf, 0, buf.Length)) > 0)
				{
					token.ThrowIfCancellationRequested();
					this._fileStream.Write(buf, 0, r);
					currentIndex += r;
					DownloadFile.ProgressUpdated onUpdateProgress = this.OnUpdateProgress;
					if (onUpdateProgress != null)
					{
						onUpdateProgress((float)((double)currentIndex / (double)contentLength), (long)currentIndex, response.ContentLength);
					}
				}
			}
			catch (OperationCanceledException e)
			{
				this.AbortDownload(_downloadPath);
				Logging.tML.Info("DownloadFile[" + this.DisplayText + "] operation was cancelled", e);
			}
			catch (Exception e2)
			{
				this.AbortDownload(_downloadPath);
				Logging.tML.Info("Unknown error", e2);
			}
			if (!this._aborted)
			{
				FileStream fileStream = this._fileStream;
				if (fileStream != null)
				{
					fileStream.Close();
				}
				this.PreCopy();
				File.Copy(_downloadPath, this.FilePath, true);
				File.Delete(_downloadPath);
				Action onComplete = this.OnComplete;
				if (onComplete != null)
				{
					onComplete();
				}
			}
			return this;
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x005228C4 File Offset: 0x00520AC4
		private void SetupDownloadRequest()
		{
			ServicePointManager.SecurityProtocol = this.SecurityProtocol;
			ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.ServerCertificateValidation);
			this.Request = WebRequest.CreateHttp(this.Url);
			this.Request.ServicePoint.ReceiveBufferSize = 1048576;
			this.Request.Method = "GET";
			this.Request.ProtocolVersion = this.ProtocolVersion;
			this.Request.UserAgent = "tModLoader/" + BuildInfo.versionTag;
			this.Request.KeepAlive = true;
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x0052295A File Offset: 0x00520B5A
		private bool ServerCertificateValidation(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
		{
			return errors == SslPolicyErrors.None;
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x00522961 File Offset: 0x00520B61
		internal virtual void PreCopy()
		{
		}

		// Token: 0x04001BD5 RID: 7125
		internal const string TEMP_EXTENSION = ".tmp";

		// Token: 0x04001BD6 RID: 7126
		public const int CHUNK_SIZE = 1048576;

		// Token: 0x04001BD7 RID: 7127
		public const SecurityProtocolType Tls12 = SecurityProtocolType.Tls12;

		// Token: 0x04001BDB RID: 7131
		public readonly string Url;

		// Token: 0x04001BDC RID: 7132
		public readonly string FilePath;

		// Token: 0x04001BDD RID: 7133
		public readonly string DisplayText;

		// Token: 0x04001BDE RID: 7134
		private FileStream _fileStream;

		// Token: 0x04001BDF RID: 7135
		public SecurityProtocolType SecurityProtocol = SecurityProtocolType.Tls12;

		// Token: 0x04001BE0 RID: 7136
		public Version ProtocolVersion = HttpVersion.Version11;

		// Token: 0x04001BE1 RID: 7137
		private bool _aborted;

		// Token: 0x02000A30 RID: 2608
		// (Invoke) Token: 0x060057ED RID: 22509
		public delegate void ProgressUpdated(float progress, long bytesReceived, long totalBytesNeeded);
	}
}
