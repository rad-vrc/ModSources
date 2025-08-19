using System;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// Each ModConfig class has a different scope. Failure to use the correct mode will lead to bugs.
	/// </summary>
	// Token: 0x02000389 RID: 905
	public enum ConfigScope
	{
		/// <summary>
		/// This config is shared between all clients and maintained by the server. Use this for game-play changes that should affect all players the same. ServerSide also covers single player as well.
		/// </summary>
		// Token: 0x04001D37 RID: 7479
		ServerSide,
		/// <summary>
		/// This config is specific to the client. Use this for personalization options.
		/// </summary>
		// Token: 0x04001D38 RID: 7480
		ClientSide
	}
}
