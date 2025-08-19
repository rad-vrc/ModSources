using System;

namespace NATUPNPLib
{
	// Token: 0x0200001A RID: 26
	public interface IUPnPNAT
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000D7 RID: 215
		IStaticPortMappingCollection StaticPortMappingCollection { get; }
	}
}
