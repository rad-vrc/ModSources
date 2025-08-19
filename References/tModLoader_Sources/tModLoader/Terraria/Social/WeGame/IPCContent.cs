using System;
using System.Threading;

namespace Terraria.Social.WeGame
{
	// Token: 0x020000D1 RID: 209
	public class IPCContent
	{
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060016FE RID: 5886 RVA: 0x004B66F6 File Offset: 0x004B48F6
		// (set) Token: 0x060016FF RID: 5887 RVA: 0x004B66FE File Offset: 0x004B48FE
		public CancellationToken CancelToken { get; set; }

		// Token: 0x040012DE RID: 4830
		public byte[] data;
	}
}
