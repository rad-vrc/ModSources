using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;

namespace Terraria.Social.WeGame
{
	// Token: 0x020000D4 RID: 212
	public class IPCServer : IPCBase
	{
		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06001707 RID: 5895 RVA: 0x004B67C8 File Offset: 0x004B49C8
		// (remove) Token: 0x06001708 RID: 5896 RVA: 0x004B6800 File Offset: 0x004B4A00
		public event Action OnClientAccess;

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x06001709 RID: 5897 RVA: 0x004B6835 File Offset: 0x004B4A35
		// (remove) Token: 0x0600170A RID: 5898 RVA: 0x004B684E File Offset: 0x004B4A4E
		public override event Action<byte[]> OnDataArrive
		{
			add
			{
				this._onDataArrive = (Action<byte[]>)Delegate.Combine(this._onDataArrive, value);
			}
			remove
			{
				this._onDataArrive = (Action<byte[]>)Delegate.Remove(this._onDataArrive, value);
			}
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x004B6867 File Offset: 0x004B4A67
		private NamedPipeServerStream GetPipeStream()
		{
			return (NamedPipeServerStream)this._pipeStream;
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x004B6874 File Offset: 0x004B4A74
		public void Init(string serverName)
		{
			this._serverName = serverName;
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x004B687D File Offset: 0x004B4A7D
		private void LazyCreatePipe()
		{
			if (this.GetPipeStream() == null)
			{
				this._pipeStream = new NamedPipeServerStream(this._serverName, PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
				this._cancelTokenSrc = new CancellationTokenSource();
			}
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x004B68AC File Offset: 0x004B4AAC
		public override void ReadCallback(IAsyncResult result)
		{
			IPCContent ipccontent = (IPCContent)result.AsyncState;
			base.ReadCallback(result);
			if (!ipccontent.CancelToken.IsCancellationRequested)
			{
				this.ContinueReadOrWait();
				return;
			}
			WeGameHelper.WriteDebugString("servcer.ReadCallback cancel", Array.Empty<object>());
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x004B68F0 File Offset: 0x004B4AF0
		public void StartListen()
		{
			this.LazyCreatePipe();
			WeGameHelper.WriteDebugString("begin listen", Array.Empty<object>());
			this.GetPipeStream().BeginWaitForConnection(new AsyncCallback(this.ConnectionCallback), this._cancelTokenSrc.Token);
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x004B692F File Offset: 0x004B4B2F
		private void RestartListen()
		{
			this.StartListen();
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x004B6938 File Offset: 0x004B4B38
		private void ConnectionCallback(IAsyncResult result)
		{
			try
			{
				this._haveClientAccessFlag = true;
				WeGameHelper.WriteDebugString("Connected in", Array.Empty<object>());
				this.GetPipeStream().EndWaitForConnection(result);
				if (!((CancellationToken)result.AsyncState).IsCancellationRequested)
				{
					this.BeginReadData();
				}
				else
				{
					WeGameHelper.WriteDebugString("ConnectionCallback but user cancel", Array.Empty<object>());
				}
			}
			catch (IOException ex)
			{
				this._pipeBrokenFlag = true;
				WeGameHelper.WriteDebugString("ConnectionCallback Exception, {0}", new object[]
				{
					ex.Message
				});
			}
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x004B69CC File Offset: 0x004B4BCC
		public void ContinueReadOrWait()
		{
			if (this.GetPipeStream().IsConnected)
			{
				this.BeginReadData();
				return;
			}
			try
			{
				this.GetPipeStream().BeginWaitForConnection(new AsyncCallback(this.ConnectionCallback), null);
			}
			catch (IOException ex)
			{
				this._pipeBrokenFlag = true;
				WeGameHelper.WriteDebugString("ContinueReadOrWait Exception, {0}", new object[]
				{
					ex.Message
				});
			}
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x004B6A40 File Offset: 0x004B4C40
		private void ProcessClientAccessEvent()
		{
			if (this._haveClientAccessFlag)
			{
				if (this.OnClientAccess != null)
				{
					this.OnClientAccess();
				}
				this._haveClientAccessFlag = false;
			}
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x004B6A64 File Offset: 0x004B4C64
		private void CheckFlagAndFireEvent()
		{
			this.ProcessClientAccessEvent();
			this.ProcessDataArriveEvent();
			this.ProcessPipeBrokenEvent();
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x004B6A78 File Offset: 0x004B4C78
		private void ProcessPipeBrokenEvent()
		{
			if (this._pipeBrokenFlag)
			{
				this.Reset();
				this._pipeBrokenFlag = false;
				this.RestartListen();
			}
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x004B6A99 File Offset: 0x004B4C99
		public void Tick()
		{
			this.CheckFlagAndFireEvent();
		}

		// Token: 0x040012E5 RID: 4837
		private string _serverName;

		// Token: 0x040012E6 RID: 4838
		private bool _haveClientAccessFlag;
	}
}
