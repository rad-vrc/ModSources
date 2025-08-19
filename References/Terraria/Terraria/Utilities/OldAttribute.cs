using System;

namespace Terraria.Utilities
{
	// Token: 0x02000141 RID: 321
	public sealed class OldAttribute : Attribute
	{
		// Token: 0x060018C0 RID: 6336 RVA: 0x004DEFA8 File Offset: 0x004DD1A8
		public OldAttribute()
		{
			this.message = "";
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x004DEFBB File Offset: 0x004DD1BB
		public OldAttribute(string message)
		{
			this.message = message;
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060018C2 RID: 6338 RVA: 0x004DEFCA File Offset: 0x004DD1CA
		public string Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x0400150B RID: 5387
		private string message;
	}
}
