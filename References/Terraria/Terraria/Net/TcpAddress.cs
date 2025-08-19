using System;
using System.Net;

namespace Terraria.Net
{
	// Token: 0x020000BB RID: 187
	public class TcpAddress : RemoteAddress
	{
		// Token: 0x06001404 RID: 5124 RVA: 0x004A2015 File Offset: 0x004A0215
		public TcpAddress(IPAddress address, int port)
		{
			this.Type = AddressType.Tcp;
			this.Address = address;
			this.Port = port;
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x004A2032 File Offset: 0x004A0232
		public override string GetIdentifier()
		{
			return this.Address.ToString();
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x004A203F File Offset: 0x004A023F
		public override bool IsLocalHost()
		{
			return this.Address.Equals(IPAddress.Loopback);
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x004A2051 File Offset: 0x004A0251
		public override string ToString()
		{
			return new IPEndPoint(this.Address, this.Port).ToString();
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x004A2069 File Offset: 0x004A0269
		public override string GetFriendlyName()
		{
			return this.ToString();
		}

		// Token: 0x040011E3 RID: 4579
		public IPAddress Address;

		// Token: 0x040011E4 RID: 4580
		public int Port;
	}
}
