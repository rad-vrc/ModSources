using System;
using System.IO;
using Terraria.GameContent.Ambience;
using Terraria.GameContent.Skies;
using Terraria.Graphics.Effects;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x020005D2 RID: 1490
	public class NetAmbienceModule : NetModule
	{
		// Token: 0x060042EB RID: 17131 RVA: 0x005FBA34 File Offset: 0x005F9C34
		public static NetPacket SerializeSkyEntitySpawn(Player player, SkyEntityType type)
		{
			int value = Main.rand.Next();
			NetPacket result = NetModule.CreatePacket<NetAmbienceModule>(6);
			result.Writer.Write((byte)player.whoAmI);
			result.Writer.Write(value);
			result.Writer.Write((byte)type);
			return result;
		}

		// Token: 0x060042EC RID: 17132 RVA: 0x005FBA84 File Offset: 0x005F9C84
		public override bool Deserialize(BinaryReader reader, int userId)
		{
			if (Main.dedServ)
			{
				return false;
			}
			byte playerId = reader.ReadByte();
			int seed = reader.ReadInt32();
			SkyEntityType type = (SkyEntityType)reader.ReadByte();
			if (Main.remixWorld)
			{
				return true;
			}
			Main.QueueMainThreadAction(delegate
			{
				((AmbientSky)SkyManager.Instance["Ambience"]).Spawn(Main.player[(int)playerId], type, seed);
			});
			return true;
		}
	}
}
