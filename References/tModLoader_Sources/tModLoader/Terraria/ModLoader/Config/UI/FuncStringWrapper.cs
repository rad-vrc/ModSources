using System;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003B2 RID: 946
	internal class FuncStringWrapper
	{
		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06003285 RID: 12933 RVA: 0x005446E8 File Offset: 0x005428E8
		public Func<string> Func { get; }

		// Token: 0x06003286 RID: 12934 RVA: 0x005446F0 File Offset: 0x005428F0
		public FuncStringWrapper(Func<string> func)
		{
			this.Func = func;
		}

		// Token: 0x06003287 RID: 12935 RVA: 0x005446FF File Offset: 0x005428FF
		public override string ToString()
		{
			return this.Func();
		}
	}
}
