using System;

namespace Terraria.Net
{
	// Token: 0x020000BA RID: 186
	public abstract class RemoteAddress
	{
		// Token: 0x06001400 RID: 5120
		public abstract string GetIdentifier();

		// Token: 0x06001401 RID: 5121
		public abstract string GetFriendlyName();

		// Token: 0x06001402 RID: 5122
		public abstract bool IsLocalHost();

		// Token: 0x040011E2 RID: 4578
		public AddressType Type;
	}
}
