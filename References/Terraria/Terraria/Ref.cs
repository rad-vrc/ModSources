using System;

namespace Terraria
{
	// Token: 0x02000021 RID: 33
	public class Ref<T>
	{
		// Token: 0x06000162 RID: 354 RVA: 0x0000B904 File Offset: 0x00009B04
		public Ref()
		{
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000EFF0 File Offset: 0x0000D1F0
		public Ref(T value)
		{
			this.Value = value;
		}

		// Token: 0x04000100 RID: 256
		public T Value;
	}
}
