using System;

namespace Terraria.ModLoader.Exceptions
{
	// Token: 0x020002A9 RID: 681
	internal class ResourceLoadException : Exception
	{
		// Token: 0x06002CF9 RID: 11513 RVA: 0x0052B386 File Offset: 0x00529586
		public ResourceLoadException(string message, Exception inner = null) : base(message, inner)
		{
		}
	}
}
