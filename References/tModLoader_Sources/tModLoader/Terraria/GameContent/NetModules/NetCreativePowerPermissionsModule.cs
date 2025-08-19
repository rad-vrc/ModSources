using System;
using System.IO;
using Terraria.GameContent.Creative;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x020005D4 RID: 1492
	public class NetCreativePowerPermissionsModule : NetModule
	{
		// Token: 0x060042F3 RID: 17139 RVA: 0x005FBC54 File Offset: 0x005F9E54
		public static NetPacket SerializeCurrentPowerPermissionLevel(ushort powerId, int level)
		{
			NetPacket result = NetModule.CreatePacket<NetCreativePowerPermissionsModule>(4);
			result.Writer.Write(0);
			result.Writer.Write(powerId);
			result.Writer.Write((byte)level);
			return result;
		}

		// Token: 0x060042F4 RID: 17140 RVA: 0x005FBC94 File Offset: 0x005F9E94
		public override bool Deserialize(BinaryReader reader, int userId)
		{
			if (reader.ReadByte() == 0)
			{
				ushort id = reader.ReadUInt16();
				int currentPermissionLevel = (int)reader.ReadByte();
				if (Main.netMode == 2)
				{
					return false;
				}
				ICreativePower power;
				if (!CreativePowerManager.Instance.TryGetPower(id, out power))
				{
					return false;
				}
				power.CurrentPermissionLevel = (PowerPermissionLevel)currentPermissionLevel;
			}
			return true;
		}

		// Token: 0x040059E1 RID: 23009
		private const byte _setPermissionLevelId = 0;
	}
}
