using System;
using System.Threading;

namespace Terraria.Social.WeGame
{
	// Token: 0x02000160 RID: 352
	public class IPCContent
	{
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060019E2 RID: 6626 RVA: 0x004E2B15 File Offset: 0x004E0D15
		// (set) Token: 0x060019E3 RID: 6627 RVA: 0x004E2B1D File Offset: 0x004E0D1D
		public CancellationToken CancelToken { get; set; }

		// Token: 0x04001563 RID: 5475
		public byte[] data;
	}
}
