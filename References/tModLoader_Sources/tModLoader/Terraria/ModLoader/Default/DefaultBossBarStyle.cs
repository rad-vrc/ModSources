using System;

namespace Terraria.ModLoader.Default
{
	/// <summary>
	/// This is the default boss bar style - the way vanilla draws boss bars.
	/// </summary>
	// Token: 0x020002BF RID: 703
	[Autoload(false)]
	internal class DefaultBossBarStyle : ModBossBarStyle
	{
		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06002D6B RID: 11627 RVA: 0x0052E2AC File Offset: 0x0052C4AC
		public override string DisplayName
		{
			get
			{
				return "Vanilla";
			}
		}
	}
}
