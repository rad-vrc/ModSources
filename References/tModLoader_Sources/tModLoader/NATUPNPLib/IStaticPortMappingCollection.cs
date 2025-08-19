using System;
using System.Collections;

namespace NATUPNPLib
{
	// Token: 0x02000019 RID: 25
	public interface IStaticPortMappingCollection : IEnumerable
	{
		// Token: 0x060000D5 RID: 213
		void Remove(int lExternalPort, string bstrProtocol);

		// Token: 0x060000D6 RID: 214
		IStaticPortMapping Add(int lExternalPort, string bstrProtocol, int lInternalPort, string bstrInternalClient, bool bEnabled, string bstrDescription);
	}
}
