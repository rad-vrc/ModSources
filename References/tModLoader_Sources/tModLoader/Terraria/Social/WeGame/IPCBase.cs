using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;

namespace Terraria.Social.WeGame
{
	// Token: 0x020000CF RID: 207
	public abstract class IPCBase
	{
		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060016E1 RID: 5857 RVA: 0x004B603D File Offset: 0x004B423D
		// (set) Token: 0x060016E2 RID: 5858 RVA: 0x004B6045 File Offset: 0x004B4245
		public int BufferSize { get; set; }

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x060016E3 RID: 5859 RVA: 0x004B604E File Offset: 0x004B424E
		// (remove) Token: 0x060016E4 RID: 5860 RVA: 0x004B6067 File Offset: 0x004B4267
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

		// Token: 0x060016E5 RID: 5861 RVA: 0x004B6080 File Offset: 0x004B4280
		public IPCBase()
		{
			this.BufferSize = 256;
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x004B60C0 File Offset: 0x004B42C0
		protected void AddPackToList(List<byte> pack)
		{
			object listLock = this._listLock;
			lock (listLock)
			{
				this._producer.Add(pack);
				this._haveDataToReadFlag = true;
			}
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x004B6110 File Offset: 0x004B4310
		protected List<List<byte>> GetPackList()
		{
			object listLock = this._listLock;
			List<List<byte>> result;
			lock (listLock)
			{
				List<List<byte>> producer = this._producer;
				this._producer = this._consumer;
				this._consumer = producer;
				this._producer.Clear();
				List<List<byte>> consumer = this._consumer;
				this._haveDataToReadFlag = false;
				result = consumer;
			}
			return result;
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x004B6180 File Offset: 0x004B4380
		protected bool HaveDataToRead()
		{
			return this._haveDataToReadFlag;
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x004B618A File Offset: 0x004B438A
		public virtual void Reset()
		{
			this._cancelTokenSrc.Cancel();
			this._pipeStream.Dispose();
			this._pipeStream = null;
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x004B61AC File Offset: 0x004B43AC
		public virtual void ProcessDataArriveEvent()
		{
			if (!this.HaveDataToRead())
			{
				return;
			}
			List<List<byte>> packList = this.GetPackList();
			if (packList == null || this._onDataArrive == null)
			{
				return;
			}
			foreach (List<byte> item in packList)
			{
				this._onDataArrive(item.ToArray());
			}
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x004B6220 File Offset: 0x004B4420
		protected virtual bool BeginReadData()
		{
			bool result = false;
			IPCContent iPCContent = new IPCContent
			{
				data = new byte[this.BufferSize],
				CancelToken = this._cancelTokenSrc.Token
			};
			WeGameHelper.WriteDebugString("BeginReadData", Array.Empty<object>());
			bool result2;
			try
			{
				if (this._pipeStream != null)
				{
					this._pipeStream.BeginRead(iPCContent.data, 0, this.BufferSize, new AsyncCallback(this.ReadCallback), iPCContent);
					result = true;
					result2 = result;
				}
				else
				{
					result2 = result;
				}
			}
			catch (IOException ex)
			{
				this._pipeBrokenFlag = true;
				WeGameHelper.WriteDebugString("BeginReadData Exception, {0}", new object[]
				{
					ex.Message
				});
				result2 = result;
			}
			return result2;
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x004B62D8 File Offset: 0x004B44D8
		public virtual void ReadCallback(IAsyncResult result)
		{
			WeGameHelper.WriteDebugString("ReadCallback: " + Thread.CurrentThread.ManagedThreadId.ToString(), Array.Empty<object>());
			IPCContent iPCContent = (IPCContent)result.AsyncState;
			try
			{
				int num = this._pipeStream.EndRead(result);
				if (!iPCContent.CancelToken.IsCancellationRequested)
				{
					if (num > 0)
					{
						this._totalData.AddRange(iPCContent.data.Take(num));
						if (this._pipeStream.IsMessageComplete)
						{
							this.AddPackToList(this._totalData);
							this._totalData = new List<byte>();
						}
					}
				}
				else
				{
					WeGameHelper.WriteDebugString("IPCBase.ReadCallback.cancel", Array.Empty<object>());
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

		// Token: 0x060016ED RID: 5869 RVA: 0x004B63F0 File Offset: 0x004B45F0
		public virtual bool Send(string value)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			return this.Send(bytes);
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x004B6410 File Offset: 0x004B4610
		public virtual bool Send(byte[] data)
		{
			bool result = false;
			if (this._pipeStream != null && this._pipeStream.IsConnected)
			{
				try
				{
					this._pipeStream.BeginWrite(data, 0, data.Length, new AsyncCallback(this.SendCallback), null);
					result = true;
					return result;
				}
				catch (IOException ex)
				{
					this._pipeBrokenFlag = true;
					WeGameHelper.WriteDebugString("Send Exception, {0}", new object[]
					{
						ex.Message
					});
					return result;
				}
				return result;
			}
			return result;
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x004B6494 File Offset: 0x004B4694
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

		// Token: 0x040012D2 RID: 4818
		private List<List<byte>> _producer = new List<List<byte>>();

		// Token: 0x040012D3 RID: 4819
		private List<List<byte>> _consumer = new List<List<byte>>();

		// Token: 0x040012D4 RID: 4820
		private List<byte> _totalData = new List<byte>();

		// Token: 0x040012D5 RID: 4821
		private object _listLock = new object();

		// Token: 0x040012D6 RID: 4822
		private volatile bool _haveDataToReadFlag;

		// Token: 0x040012D7 RID: 4823
		protected volatile bool _pipeBrokenFlag;

		// Token: 0x040012D8 RID: 4824
		protected PipeStream _pipeStream;

		// Token: 0x040012D9 RID: 4825
		protected CancellationTokenSource _cancelTokenSrc;

		// Token: 0x040012DA RID: 4826
		protected Action<byte[]> _onDataArrive;
	}
}
