using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;

namespace Terraria.Social.WeGame
{
	// Token: 0x02000162 RID: 354
	public class IPCServer : IPCBase
	{
		// Token: 0x14000033 RID: 51
		// (add) Token: 0x060019F4 RID: 6644 RVA: 0x004E2FD0 File Offset: 0x004E11D0
		// (remove) Token: 0x060019F5 RID: 6645 RVA: 0x004E3008 File Offset: 0x004E1208
		public event Action OnClientAccess;

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x060019F6 RID: 6646 RVA: 0x004E2B37 File Offset: 0x004E0D37
		// (remove) Token: 0x060019F7 RID: 6647 RVA: 0x004E2B50 File Offset: 0x004E0D50
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

		// Token: 0x060019F8 RID: 6648 RVA: 0x004E303D File Offset: 0x004E123D
		private NamedPipeServerStream GetPipeStream()
		{
			return (NamedPipeServerStream)this._pipeStream;
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x004E304A File Offset: 0x004E124A
		public void Init(string serverName)
		{
			this._serverName = serverName;
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x004E3053 File Offset: 0x004E1253
		private void LazyCreatePipe()
		{
			if (this.GetPipeStream() == null)
			{
				this._pipeStream = new NamedPipeServerStream(this._serverName, PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
				this._cancelTokenSrc = new CancellationTokenSource();
			}
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x004E3084 File Offset: 0x004E1284
		public override void ReadCallback(IAsyncResult result)
		{
			IPCContent ipccontent = (IPCContent)result.AsyncState;
			base.ReadCallback(result);
			if (!ipccontent.CancelToken.IsCancellationRequested)
			{
				this.ContinueReadOrWait();
				return;
			}
			WeGameHelper.WriteDebugString("servcer.ReadCallback cancel", new object[0]);
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x004E30C9 File Offset: 0x004E12C9
		public void StartListen()
		{
			this.LazyCreatePipe();
			WeGameHelper.WriteDebugString("begin listen", new object[0]);
			this.GetPipeStream().BeginWaitForConnection(new AsyncCallback(this.ConnectionCallback), this._cancelTokenSrc.Token);
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x004E3109 File Offset: 0x004E1309
		private void RestartListen()
		{
			this.StartListen();
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x004E3114 File Offset: 0x004E1314
		private void ConnectionCallback(IAsyncResult result)
		{
			try
			{
				this._haveClientAccessFlag = true;
				WeGameHelper.WriteDebugString("Connected in", new object[0]);
				this.GetPipeStream().EndWaitForConnection(result);
				if (!((CancellationToken)result.AsyncState).IsCancellationRequested)
				{
					this.BeginReadData();
				}
				else
				{
					WeGameHelper.WriteDebugString("ConnectionCallback but user cancel", new object[0]);
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

		// Token: 0x060019FF RID: 6655 RVA: 0x004E31AC File Offset: 0x004E13AC
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

		// Token: 0x06001A00 RID: 6656 RVA: 0x004E3220 File Offset: 0x004E1420
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

		// Token: 0x06001A01 RID: 6657 RVA: 0x004E3244 File Offset: 0x004E1444
		private void CheckFlagAndFireEvent()
		{
			this.ProcessClientAccessEvent();
			this.ProcessDataArriveEvent();
			this.ProcessPipeBrokenEvent();
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x004E3258 File Offset: 0x004E1458
		private void ProcessPipeBrokenEvent()
		{
			if (this._pipeBrokenFlag)
			{
				this.Reset();
				this._pipeBrokenFlag = false;
				this.RestartListen();
			}
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x004E3279 File Offset: 0x004E1479
		public void Tick()
		{
			this.CheckFlagAndFireEvent();
		}

		// Token: 0x0400156F RID: 5487
		private string _serverName;

		// Token: 0x04001570 RID: 5488
		private bool _haveClientAccessFlag;
	}
}
