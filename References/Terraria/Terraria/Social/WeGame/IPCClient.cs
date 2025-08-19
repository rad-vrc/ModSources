using System;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;

namespace Terraria.Social.WeGame
{
	// Token: 0x02000163 RID: 355
	public class IPCClient : IPCBase
	{
		// Token: 0x14000035 RID: 53
		// (add) Token: 0x06001A05 RID: 6661 RVA: 0x004E328C File Offset: 0x004E148C
		// (remove) Token: 0x06001A06 RID: 6662 RVA: 0x004E32C4 File Offset: 0x004E14C4
		public event Action OnConnected;

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x06001A07 RID: 6663 RVA: 0x004E2B37 File Offset: 0x004E0D37
		// (remove) Token: 0x06001A08 RID: 6664 RVA: 0x004E2B50 File Offset: 0x004E0D50
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

		// Token: 0x06001A09 RID: 6665 RVA: 0x004E32F9 File Offset: 0x004E14F9
		private NamedPipeClientStream GetPipeStream()
		{
			return (NamedPipeClientStream)this._pipeStream;
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x004E3306 File Offset: 0x004E1506
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

		// Token: 0x06001A0B RID: 6667 RVA: 0x004E332A File Offset: 0x004E152A
		private void ProcessPipeBrokenEvent()
		{
			if (this._pipeBrokenFlag)
			{
				this.Reset();
				this._pipeBrokenFlag = false;
			}
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x004E3345 File Offset: 0x004E1545
		private void CheckFlagAndFireEvent()
		{
			this.ProcessConnectedEvent();
			this.ProcessDataArriveEvent();
			this.ProcessPipeBrokenEvent();
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void Init(string clientName)
		{
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x004E335C File Offset: 0x004E155C
		public void ConnectTo(string serverName)
		{
			if (this.GetPipeStream() == null)
			{
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
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x004E33BA File Offset: 0x004E15BA
		public void Tick()
		{
			this.CheckFlagAndFireEvent();
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x004E33C4 File Offset: 0x004E15C4
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
				WeGameHelper.WriteDebugString("ReadCallback cancel", new object[0]);
			}
		}

		// Token: 0x04001572 RID: 5490
		private bool _connectedFlag;
	}
}
