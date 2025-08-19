using System;

namespace Terraria.ModLoader
{
	// Token: 0x020001CB RID: 459
	public static class ModSideExtensions
	{
		// Token: 0x060023E2 RID: 9186 RVA: 0x004E9196 File Offset: 0x004E7396
		public static string ToFriendlyString(this ModSide sortmode)
		{
			switch (sortmode)
			{
			case ModSide.Both:
				return "Both";
			case ModSide.Client:
				return "Client";
			case ModSide.Server:
				return "Server";
			case ModSide.NoSync:
				return "NoSync";
			default:
				return "Unknown";
			}
		}
	}
}
