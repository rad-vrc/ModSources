using System;

namespace Terraria.Net
{
	// Token: 0x02000122 RID: 290
	public abstract class RemoteAddress
	{
		// Token: 0x06001A33 RID: 6707
		public abstract string GetIdentifier();

		// Token: 0x06001A34 RID: 6708
		public abstract string GetFriendlyName();

		// Token: 0x06001A35 RID: 6709
		public abstract bool IsLocalHost();

		// Token: 0x0400141E RID: 5150
		public AddressType Type;
	}
}
