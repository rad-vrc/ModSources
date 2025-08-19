using System;

namespace Terraria.ModLoader
{
	// Token: 0x02000162 RID: 354
	public static class FrameworkVersion
	{
		// Token: 0x06001C4D RID: 7245 RVA: 0x004D310B File Offset: 0x004D130B
		private static Version GetVersion()
		{
			return Environment.Version;
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x004D3114 File Offset: 0x004D1314
		private static Version CheckFor45PlusVersion(int releaseKey)
		{
			if (releaseKey >= 528040)
			{
				return new Version("4.8");
			}
			if (releaseKey >= 461808)
			{
				return new Version("4.7.2");
			}
			if (releaseKey >= 461308)
			{
				return new Version("4.7.1");
			}
			if (releaseKey >= 460798)
			{
				return new Version("4.7");
			}
			if (releaseKey >= 394802)
			{
				return new Version("4.6.2");
			}
			if (releaseKey >= 394254)
			{
				return new Version("4.6.1");
			}
			if (releaseKey >= 393295)
			{
				return new Version("4.6");
			}
			if (releaseKey >= 379893)
			{
				return new Version("4.5.2");
			}
			if (releaseKey >= 378675)
			{
				return new Version("4.5.1");
			}
			if (releaseKey >= 378389)
			{
				return new Version("4.5");
			}
			throw new Exception("No 4.5 or later version detected");
		}

		// Token: 0x04001523 RID: 5411
		public static readonly Framework Framework = Framework.NetCore;

		// Token: 0x04001524 RID: 5412
		public static readonly Version Version = FrameworkVersion.GetVersion();
	}
}
