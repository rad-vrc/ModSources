using System;

namespace Terraria.ModLoader.Exceptions
{
	// Token: 0x020002A2 RID: 674
	public class JITException : Exception
	{
		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06002CE3 RID: 11491 RVA: 0x0052AC84 File Offset: 0x00528E84
		public override string HelpLink
		{
			get
			{
				return "https://github.com/tModLoader/tModLoader/wiki/JIT-Exception";
			}
		}

		// Token: 0x06002CE4 RID: 11492 RVA: 0x0052AC8B File Offset: 0x00528E8B
		public JITException(string message) : base(message)
		{
		}
	}
}
