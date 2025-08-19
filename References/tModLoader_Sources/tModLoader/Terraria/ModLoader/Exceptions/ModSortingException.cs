using System;
using System.Collections.Generic;
using Terraria.ModLoader.Core;

namespace Terraria.ModLoader.Exceptions
{
	// Token: 0x020002A5 RID: 677
	internal class ModSortingException : Exception
	{
		// Token: 0x06002CEF RID: 11503 RVA: 0x0052B30A File Offset: 0x0052950A
		public ModSortingException(ICollection<LocalMod> errored, string message) : base(message)
		{
			this.errored = errored;
		}

		// Token: 0x04001C13 RID: 7187
		public ICollection<LocalMod> errored;
	}
}
