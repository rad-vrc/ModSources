using System;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// Contains potential results for <see cref="M:Terraria.ModLoader.Config.ModConfig.SaveChanges(Terraria.ModLoader.Config.ModConfig,System.Action{System.String,Microsoft.Xna.Framework.Color},System.Boolean,System.Boolean)" />.
	/// </summary>
	// Token: 0x02000388 RID: 904
	public enum ConfigSaveResult
	{
		/// <summary> The provided config values have been successfully saved. </summary>
		// Token: 0x04001D33 RID: 7475
		Success,
		/// <summary> The provided config values have not been saved because they would require a reload. </summary>
		// Token: 0x04001D34 RID: 7476
		NeedsReload,
		/// <summary> The provided config values have been been sent to the server where <see cref="M:Terraria.ModLoader.Config.ModConfig.AcceptClientChanges(Terraria.ModLoader.Config.ModConfig,System.Int32,Terraria.Localization.NetworkText@)" /> will decide if they should be applied or not. This will be returned when attempting to save a <see cref="F:Terraria.ModLoader.Config.ConfigScope.ServerSide" /> config on a multiplayer client. </summary>
		// Token: 0x04001D35 RID: 7477
		RequestSentToServer
	}
}
