using System;

namespace Terraria.ModLoader
{
	/// <summary>A flag enum representing context where this command operates.</summary>
	// Token: 0x020001A6 RID: 422
	[Flags]
	public enum CommandType
	{
		/// <summary>Command can be used in Chat in SP and MP.</summary>
		// Token: 0x040016B4 RID: 5812
		Chat = 1,
		/// <summary>Command is executed by server in MP.</summary>
		// Token: 0x040016B5 RID: 5813
		Server = 2,
		/// <summary>Command can be used in server console during MP.</summary>
		// Token: 0x040016B6 RID: 5814
		Console = 4,
		/// <summary>Command can be used in Chat in SP and MP, but executes on the Server in MP. (SinglePlayer ? Chat : Server)</summary>
		// Token: 0x040016B7 RID: 5815
		World = 8
	}
}
