using System;
using System.IO;
using Terraria.GameContent.Creative;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x0200026F RID: 623
	public class NetCreativePowerPermissionsModule : NetModule
	{
		// Token: 0x06001FB4 RID: 8116 RVA: 0x00516738 File Offset: 0x00514938
		public static NetPacket SerializeCurrentPowerPermissionLevel(ushort powerId, int level)
		{
			NetPacket result = NetModule.CreatePacket<NetCreativePowerPermissionsModule>(4);
			result.Writer.Write(0);
			result.Writer.Write(powerId);
			result.Writer.Write((byte)level);
			return result;
		}

		// Token: 0x06001FB5 RID: 8117 RVA: 0x00516778 File Offset: 0x00514978
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
				ICreativePower creativePower;
				if (!CreativePowerManager.Instance.TryGetPower(id, out creativePower))
				{
					return false;
				}
				creativePower.CurrentPermissionLevel = (PowerPermissionLevel)currentPermissionLevel;
			}
			return true;
		}

		// Token: 0x04004693 RID: 18067
		private const byte _setPermissionLevelId = 0;
	}
}
