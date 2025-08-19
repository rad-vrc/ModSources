using System;
using System.Linq;

namespace Terraria.Testing.ChatCommands
{
	// Token: 0x020000A7 RID: 167
	public static class ArgumentHelper
	{
		// Token: 0x0600137B RID: 4987 RVA: 0x0049FB4C File Offset: 0x0049DD4C
		public static ArgumentListResult ParseList(string arguments)
		{
			return new ArgumentListResult(from arg in arguments.Split(new char[]
			{
				' '
			})
			select arg.Trim() into arg
			where arg.Length != 0
			select arg);
		}
	}
}
