using System;

namespace Terraria.ModLoader.Exceptions
{
	// Token: 0x020002A7 RID: 679
	public class OldHookException : Exception
	{
		// Token: 0x06002CF4 RID: 11508 RVA: 0x0052B34C File Offset: 0x0052954C
		public OldHookException(string hook) : base("This mod uses an old " + hook + " hook")
		{
		}
	}
}
