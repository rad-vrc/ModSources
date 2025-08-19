using System;

namespace Terraria.Utilities
{
	// Token: 0x02000090 RID: 144
	public sealed class OldAttribute : Attribute
	{
		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06001484 RID: 5252 RVA: 0x004A2751 File Offset: 0x004A0951
		public string Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x004A2759 File Offset: 0x004A0959
		public OldAttribute()
		{
			this.message = "";
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x004A276C File Offset: 0x004A096C
		public OldAttribute(string message)
		{
			this.message = message;
		}

		// Token: 0x040010AE RID: 4270
		private string message;
	}
}
