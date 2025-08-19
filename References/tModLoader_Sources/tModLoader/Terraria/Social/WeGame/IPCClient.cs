using System;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;

namespace Terraria.Social.WeGame
{
	// Token: 0x020000D0 RID: 208
	public class IPCClient : IPCBase
	{
		// Token: 0x14000032 RID: 50
		// (add) Token: 0x060016F0 RID: 5872 RVA: 0x004B64EC File Offset: 0x004B46EC
		// (remove) Token: 0x060016F1 RID: 5873 RVA: 0x004B6524 File Offset: 0x004B4724
		public event Action OnConnected;

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x060016F2 RID: 5874 RVA: 0x004B6559 File Offset: 0x004B4759
		// (remove) Token: 0x060016F3 RID: 5875 RVA: 0x004B6572 File Offset: 0x004B4772
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

		// Token: 0x060016F4 RID: 5876 RVA: 0x004B658B File Offset: 0x004B478B
		private NamedPipeClientStream GetPipeStream()
		{
			return (NamedPipeClientStream)this._pipeStream;
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x004B6598 File Offset: 0x004B4798
		private void ProcessConnectedEvent()
		{
			if (this._connectedFlag)
			{
				if (this.OnConnected != null)
				{
					this.OnConnected();
				}
				this._connectedFlag = false;
			}
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x004B65BC File Offset: 0x004B47BC
		private void ProcessPipeBrokenEvent()
		{
			if (this._pipeBrokenFlag)
			{
				this.Reset();
				this._pipeBrokenFlag = false;
			}
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x004B65D7 File Offset: 0x004B47D7
		private void CheckFlagAndFireEvent()
		{
			this.ProcessConnectedEvent();
			this.ProcessDataArriveEvent();
			this.ProcessPipeBrokenEvent();
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x004B65EB File Offset: 0x004B47EB
		public void Init(string clientName)
		{
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x004B65F0 File Offset: 0x004B47F0
		public void ConnectTo(string serverName)
		{
			if (this.GetPipeStream() != null)
			{
				return;
			}
			this._pipeStream = new NamedPipeClientStream(".", serverName, PipeDirection.InOut, PipeOptions.Asynchronous);
			this._cancelTokenSrc = new CancellationTokenSource();
			Task.Factory.StartNew(delegate(object content)
			{
				this.GetPipeStream().Connect();
				if (!((CancellationToken)content).IsCancellationRequested)
				{
					this.GetPipeStream().ReadMode = PipeTransmissionMode.Message;
					this.BeginReadData();
					this._connectedFlag = true;
				}
			}, this._cancelTokenSrc.Token);
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x004B664F File Offset: 0x004B484F
		public void Tick()
		{
			this.CheckFlagAndFireEvent();
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x004B6658 File Offset: 0x004B4858
		public override void ReadCallback(IAsyncResult result)
		{
			IPCContent ipccontent = (IPCContent)result.AsyncState;
			base.ReadCallback(result);
			if (!ipccontent.CancelToken.IsCancellationRequested)
			{
				if (this.GetPipeStream().IsConnected)
				{
					this.BeginReadData();
					return;
				}
			}
			else
			{
				WeGameHelper.WriteDebugString("ReadCallback cancel", Array.Empty<object>());
			}
		}

		// Token: 0x040012DC RID: 4828
		private bool _connectedFlag;
	}
}
