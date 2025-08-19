using System;
using System.Linq;

namespace Terraria.Testing.ChatCommands
{
	// Token: 0x020000C6 RID: 198
	public static class ArgumentHelper
	{
		// Token: 0x060016A0 RID: 5792 RVA: 0x004B53F8 File Offset: 0x004B35F8
		public static ArgumentListResult ParseList(string arguments)
		{
			return new ArgumentListResult(from arg in arguments.Split(' ', StringSplitOptions.None)
			select arg.Trim() into arg
			where arg.Length != 0
			select arg);
		}
	}
}
