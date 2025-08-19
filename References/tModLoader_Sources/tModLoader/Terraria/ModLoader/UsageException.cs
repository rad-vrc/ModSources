using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader
{
	// Token: 0x020001A9 RID: 425
	public class UsageException : Exception
	{
		// Token: 0x0600205F RID: 8287 RVA: 0x004E2E37 File Offset: 0x004E1037
		public UsageException()
		{
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x004E2E4A File Offset: 0x004E104A
		public UsageException(string msg)
		{
			this.msg = msg;
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x004E2E64 File Offset: 0x004E1064
		public UsageException(string msg, Color color)
		{
			this.msg = msg;
			this.color = color;
		}

		// Token: 0x040016B8 RID: 5816
		internal string msg;

		// Token: 0x040016B9 RID: 5817
		internal Color color = Color.Red;
	}
}
