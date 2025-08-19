using System;
using System.IO;
using Terraria.GameContent.Ambience;
using Terraria.GameContent.Skies;
using Terraria.Graphics.Effects;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x0200026A RID: 618
	public class NetAmbienceModule : NetModule
	{
		// Token: 0x06001FA3 RID: 8099 RVA: 0x005163EC File Offset: 0x005145EC
		public static NetPacket SerializeSkyEntitySpawn(Player player, SkyEntityType type)
		{
			int value = Main.rand.Next();
			NetPacket result = NetModule.CreatePacket<NetAmbienceModule>(6);
			result.Writer.Write((byte)player.whoAmI);
			result.Writer.Write(value);
			result.Writer.Write((byte)type);
			return result;
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x0051643C File Offset: 0x0051463C
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
