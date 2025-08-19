using System;
using System.IO;
using Terraria.GameContent.Drawing;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x020005D9 RID: 1497
	public class NetParticlesModule : NetModule
	{
		// Token: 0x0600430A RID: 17162 RVA: 0x005FC2D4 File Offset: 0x005FA4D4
		public static NetPacket Serialize(ParticleOrchestraType particleType, ParticleOrchestraSettings settings)
		{
			NetPacket result = NetModule.CreatePacket<NetParticlesModule>(22);
			result.Writer.Write((byte)particleType);
			settings.Serialize(result.Writer);
			return result;
		}

		// Token: 0x0600430B RID: 17163 RVA: 0x005FC308 File Offset: 0x005FA508
		public override bool Deserialize(BinaryReader reader, int userId)
		{
			ParticleOrchestraType particleOrchestraType = (ParticleOrchestraType)reader.ReadByte();
			ParticleOrchestraSettings settings = default(ParticleOrchestraSettings);
			settings.DeserializeFrom(reader);
			if (Main.netMode == 2)
			{
				NetManager.Instance.Broadcast(NetParticlesModule.Serialize(particleOrchestraType, settings), userId);
			}
			else
			{
				ParticleOrchestrator.SpawnParticlesDirect(particleOrchestraType, settings);
			}
			return true;
		}
	}
}
