using System;
using System.IO;

namespace Terraria.ModLoader.Exceptions
{
	// Token: 0x0200029F RID: 671
	public class CustomModDataException : IOException
	{
		// Token: 0x06002CDE RID: 11486 RVA: 0x0052AC43 File Offset: 0x00528E43
		public CustomModDataException(Mod mod, string message, Exception inner) : base(message, inner)
		{
			this.modName = (mod.Name ?? "Terraria");
		}

		// Token: 0x04001C12 RID: 7186
		public readonly string modName;
	}
}
