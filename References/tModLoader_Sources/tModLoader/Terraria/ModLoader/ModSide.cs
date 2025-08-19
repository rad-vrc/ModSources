using System;

namespace Terraria.ModLoader
{
	/// <summary>A ModSide enum defines how mods are synced between clients and servers. You can set your mod's ModSide from its build.txt file.</summary>
	// Token: 0x020001CA RID: 458
	public enum ModSide
	{
		/// <summary>The default value for ModSide. This means that the mod has effects both client-side and server-side. When a client connects to a server, this mod will be disabled if the server does not have it. If a client without this mod connects to a server with this mod, the server will send this mod to the client and enable it. In general all mods that add game content should use this.</summary>
		// Token: 0x0400172B RID: 5931
		Both,
		/// <summary>This means that the mod only has effects client-side. This mod will not be disabled client-side if the server does not have it. This is useful for mods that only add controls (for example, keybinds), change textures/musics, etc.</summary>
		// Token: 0x0400172C RID: 5932
		Client,
		/// <summary>This means that the mod only has effects server-side. The server will not send this mod to every client that connects.</summary>
		// Token: 0x0400172D RID: 5933
		Server,
		/// <summary>This means that the mod could have effects client-side and could have effects server-side. The client will not download this mod if it connects to a server with this mod, and the client will not disable this mod if it connects to a server without this mod. If a client connects to a server and both have this mod, then IDs will still be synchronized. This is useful if you want optional extra features when both the client and server have this mod.</summary>
		// Token: 0x0400172E RID: 5934
		NoSync
	}
}
