using System;
using Terraria.GameContent.NetModules;
using Terraria.Net;

namespace Terraria.Initializers
{
	// Token: 0x020003EF RID: 1007
	public static class NetworkInitializer
	{
		// Token: 0x060034E1 RID: 13537 RVA: 0x00566A88 File Offset: 0x00564C88
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
