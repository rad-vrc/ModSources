using System;
using Terraria.GameContent.NetModules;
using Terraria.Net;

namespace Terraria.Initializers
{
	// Token: 0x020000E6 RID: 230
	public static class NetworkInitializer
	{
		// Token: 0x06001586 RID: 5510 RVA: 0x004BC8D4 File Offset: 0x004BAAD4
		public static void Load()
		{
			NetManager.Instance.Register<NetLiquidModule>();
			NetManager.Instance.Register<NetTextModule>();
			NetManager.Instance.Register<NetPingModule>();
			NetManager.Instance.Register<NetAmbienceModule>();
			NetManager.Instance.Register<NetBestiaryModule>();
			NetManager.Instance.Register<NetCreativeUnlocksModule>();
			NetManager.Instance.Register<NetCreativePowersModule>();
			NetManager.Instance.Register<NetCreativeUnlocksPlayerReportModule>();
			NetManager.Instance.Register<NetTeleportPylonModule>();
			NetManager.Instance.Register<NetParticlesModule>();
			NetManager.Instance.Register<NetCreativePowerPermissionsModule>();
		}
	}
}
