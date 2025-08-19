using System;
using System.IO;
using Terraria.Localization;
using Terraria.Net.Sockets;

namespace Terraria
{
	// Token: 0x0200002E RID: 46
	public class RemoteServer
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0003C076 File Offset: 0x0003A276
		public bool HideStatusTextPercent
		{
			get
			{
				return this.ServerSpecialFlags[0];
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0003C084 File Offset: 0x0003A284
		public bool StatusTextHasShadows
		{
			get
			{
				return this.ServerSpecialFlags[1];
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0003C092 File Offset: 0x0003A292
		public bool ServerWantsToRunCheckBytesInClientLoopThread
		{
			get
			{
				return this.ServerSpecialFlags[2];
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0003C0A0 File Offset: 0x0003A2A0
		public void ResetSpecialFlags()
		{
			this.ServerSpecialFlags = 0;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0003C0AE File Offset: 0x0003A2AE
		public void ClientWriteCallBack(object state)
		{
			NetMessage.buffer[256].spamCount--;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0003C0C8 File Offset: 0x0003A2C8
		public void ClientReadCallBack(object state, int length)
		{
			try
			{
				if (!Netplay.Disconnect)
				{
					if (length == 0)
					{
						Netplay.Disconnect = true;
						Main.statusText = Language.GetTextValue("Net.LostConnection");
					}
					else
					{
						if (Main.ignoreErrors)
						{
							try
							{
								NetMessage.ReceiveBytes(this.ReadBuffer, length, 256);
								goto IL_51;
							}
							catch
							{
								goto IL_51;
							}
						}
						NetMessage.ReceiveBytes(this.ReadBuffer, length, 256);
					}
				}
				IL_51:
				this.IsReading = false;
			}
			catch (Exception value)
			{
				try
				{
					using (StreamWriter streamWriter = new StreamWriter("client-crashlog.txt", true))
					{
						streamWriter.WriteLine(DateTime.Now);
						streamWriter.WriteLine(value);
						streamWriter.WriteLine("");
					}
				}
				catch
				{
				}
				Netplay.Disconnect = true;
			}
		}

		// Token: 0x040001E3 RID: 483
		public ISocket Socket = new TcpSocket();

		// Token: 0x040001E4 RID: 484
		public bool IsActive;

		// Token: 0x040001E5 RID: 485
		public int State;

		// Token: 0x040001E6 RID: 486
		public int TimeOutTimer;

		// Token: 0x040001E7 RID: 487
		public bool IsReading;

		// Token: 0x040001E8 RID: 488
		public byte[] ReadBuffer;

		// Token: 0x040001E9 RID: 489
		public string StatusText;

		// Token: 0x040001EA RID: 490
		public int StatusCount;

		// Token: 0x040001EB RID: 491
		public int StatusMax;

		// Token: 0x040001EC RID: 492
		public BitsByte ServerSpecialFlags;
	}
}
