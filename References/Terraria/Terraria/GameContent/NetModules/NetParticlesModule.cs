using System;
using System.IO;
using Terraria.GameContent.Drawing;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x02000270 RID: 624
	public class NetParticlesModule : NetModule
	{
		// Token: 0x06001FB7 RID: 8119 RVA: 0x005167C0 File Offset: 0x005149C0
		public static NetPacket Serialize(ParticleOrchestraType particleType, ParticleOrchestraSettings settings)
		{
			NetPacket result = NetModule.CreatePacket<NetParticlesModule>(22);
			result.Writer.Write((byte)particleType);
			settings.Serialize(result.Writer);
			return result;
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x005167F4 File Offset: 0x005149F4
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
