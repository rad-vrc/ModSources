using System;
using System.Net;

namespace Terraria.Net
{
	// Token: 0x02000125 RID: 293
	public class TcpAddress : RemoteAddress
	{
		// Token: 0x06001A3C RID: 6716 RVA: 0x004CB1A7 File Offset: 0x004C93A7
		public TcpAddress(IPAddress address, int port)
		{
			this.Type = AddressType.Tcp;
			this.Address = address;
			this.Port = port;
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x004CB1C4 File Offset: 0x004C93C4
		public override string GetIdentifier()
		{
			return this.Address.ToString();
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x004CB1D1 File Offset: 0x004C93D1
		public override bool IsLocalHost()
		{
			return this.Address.Equals(IPAddress.Loopback);
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x004CB1E3 File Offset: 0x004C93E3
		public override string ToString()
		{
			return new IPEndPoint(this.Address, this.Port).ToString();
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x004CB1FB File Offset: 0x004C93FB
		public override string GetFriendlyName()
		{
			return this.ToString();
		}

		// Token: 0x04001426 RID: 5158
		public IPAddress Address;

		// Token: 0x04001427 RID: 5159
		public int Port;
	}
}
