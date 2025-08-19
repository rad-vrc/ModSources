using System;

namespace NATUPNPLib
{
	// Token: 0x02000018 RID: 24
	public interface IStaticPortMapping
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000D2 RID: 210
		int InternalPort { get; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000D3 RID: 211
		string Protocol { get; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000D4 RID: 212
		string InternalClient { get; }
	}
}
