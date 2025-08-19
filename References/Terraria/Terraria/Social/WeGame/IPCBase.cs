using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;

namespace Terraria.Social.WeGame
{
	// Token: 0x02000161 RID: 353
	public abstract class IPCBase
	{
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060019E6 RID: 6630 RVA: 0x004E2B2F File Offset: 0x004E0D2F
		// (set) Token: 0x060019E5 RID: 6629 RVA: 0x004E2B26 File Offset: 0x004E0D26
		public int BufferSize { get; set; }

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x060019E7 RID: 6631 RVA: 0x004E2B37 File Offset: 0x004E0D37
		// (remove) Token: 0x060019E8 RID: 6632 RVA: 0x004E2B50 File Offset: 0x004E0D50
		public virtual event Action<byte[]> OnDataArrive
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

		// Token: 0x060019E9 RID: 6633 RVA: 0x004E2B69 File Offset: 0x004E0D69
		public IPCBase()
		{
			this.BufferSize = 256;
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x004E2BA8 File Offset: 0x004E0DA8
		protected void AddPackToList(List<byte> pack)
		{
			object listLock = this._listLock;
			lock (listLock)
			{
				this._producer.Add(pack);
				this._haveDataToReadFlag = true;
			}
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x004E2BF8 File Offset: 0x004E0DF8
		protected List<List<byte>> GetPackList()
		{
			List<List<byte>> result = null;
			object listLock = this._listLock;
			lock (listLock)
			{
				List<List<byte>> producer = this._producer;
				this._producer = this._consumer;
				this._consumer = producer;
				this._producer.Clear();
				result = this._consumer;
				this._haveDataToReadFlag = false;
			}
			return result;
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x004E2C6C File Offset: 0x004E0E6C
		protected bool HaveDataToRead()
		{
			return this._haveDataToReadFlag;
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x004E2C76 File Offset: 0x004E0E76
		public virtual void Reset()
		{
			this._cancelTokenSrc.Cancel();
			this._pipeStream.Dispose();
			this._pipeStream = null;
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x004E2C98 File Offset: 0x004E0E98
		public virtual void ProcessDataArriveEvent()
		{
			if (this.HaveDataToRead())
			{
				List<List<byte>> packList = this.GetPackList();
				if (packList != null && this._onDataArrive != null)
				{
					foreach (List<byte> list in packList)
					{
						this._onDataArrive(list.ToArray());
					}
				}
			}
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x004E2D0C File Offset: 0x004E0F0C
		protected virtual bool BeginReadData()
		{
			bool result = false;
			IPCContent ipccontent = new IPCContent
			{
				data = new byte[this.BufferSize],
				CancelToken = this._cancelTokenSrc.Token
			};
			WeGameHelper.WriteDebugString("BeginReadData", new object[0]);
			try
			{
				if (this._pipeStream != null)
				{
					this._pipeStream.BeginRead(ipccontent.data, 0, this.BufferSize, new AsyncCallback(this.ReadCallback), ipccontent);
					result = true;
				}
			}
			catch (IOException ex)
			{
				this._pipeBrokenFlag = true;
				WeGameHelper.WriteDebugString("BeginReadData Exception, {0}", new object[]
				{
					ex.Message
				});
			}
			return result;
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x004E2DBC File Offset: 0x004E0FBC
		public virtual void ReadCallback(IAsyncResult result)
		{
			WeGameHelper.WriteDebugString("ReadCallback: " + Thread.CurrentThread.ManagedThreadId.ToString(), new object[0]);
			IPCContent ipccontent = (IPCContent)result.AsyncState;
			try
			{
				int num = this._pipeStream.EndRead(result);
				if (!ipccontent.CancelToken.IsCancellationRequested)
				{
					if (num > 0)
					{
						this._totalData.AddRange(ipccontent.data.Take(num));
						if (this._pipeStream.IsMessageComplete)
						{
							this.AddPackToList(this._totalData);
							this._totalData = new List<byte>();
						}
					}
				}
				else
				{
					WeGameHelper.WriteDebugString("IPCBase.ReadCallback.cancel", new object[0]);
				}
			}
			catch (IOException ex)
			{
				this._pipeBrokenFlag = true;
				WeGameHelper.WriteDebugString("ReadCallback Exception, {0}", new object[]
				{
					ex.Message
				});
			}
			catch (InvalidOperationException ex2)
			{
				this._pipeBrokenFlag = true;
				WeGameHelper.WriteDebugString("ReadCallback Exception, {0}", new object[]
				{
					ex2.Message
				});
			}
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x004E2ED8 File Offset: 0x004E10D8
		public virtual bool Send(string value)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			return this.Send(bytes);
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x004E2EF8 File Offset: 0x004E10F8
		public virtual bool Send(byte[] data)
		{
			bool result = false;
			if (this._pipeStream != null && this._pipeStream.IsConnected)
			{
				try
				{
					this._pipeStream.BeginWrite(data, 0, data.Length, new AsyncCallback(this.SendCallback), null);
					result = true;
				}
				catch (IOException ex)
				{
					this._pipeBrokenFlag = true;
					WeGameHelper.WriteDebugString("Send Exception, {0}", new object[]
					{
						ex.Message
					});
				}
			}
			return result;
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x004E2F78 File Offset: 0x004E1178
		protected virtual void SendCallback(IAsyncResult result)
		{
			try
			{
				if (this._pipeStream != null)
				{
					this._pipeStream.EndWrite(result);
				}
			}
			catch (IOException ex)
			{
				this._pipeBrokenFlag = true;
				WeGameHelper.WriteDebugString("SendCallback Exception, {0}", new object[]
				{
					ex.Message
				});
			}
		}

		// Token: 0x04001565 RID: 5477
		private List<List<byte>> _producer = new List<List<byte>>();

		// Token: 0x04001566 RID: 5478
		private List<List<byte>> _consumer = new List<List<byte>>();

		// Token: 0x04001567 RID: 5479
		private List<byte> _totalData = new List<byte>();

		// Token: 0x04001568 RID: 5480
		private object _listLock = new object();

		// Token: 0x04001569 RID: 5481
		private volatile bool _haveDataToReadFlag;

		// Token: 0x0400156A RID: 5482
		protected volatile bool _pipeBrokenFlag;

		// Token: 0x0400156B RID: 5483
		protected PipeStream _pipeStream;

		// Token: 0x0400156C RID: 5484
		protected CancellationTokenSource _cancelTokenSrc;

		// Token: 0x0400156D RID: 5485
		protected Action<byte[]> _onDataArrive;
	}
}
