using System;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Terraria.GameContent
{
	// Token: 0x020004B9 RID: 1209
	public struct TeleportPylonInfo : IEquatable<TeleportPylonInfo>
	{
		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06003A0F RID: 14863 RVA: 0x005A4CB4 File Offset: 0x005A2EB4
		public ModPylon ModPylon
		{
			get
			{
				return PylonLoader.GetModPylon(this.TypeOfPylon);
			}
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x005A4CC1 File Offset: 0x005A2EC1
		public bool Equals(TeleportPylonInfo other)
		{
			return this.PositionInTiles == other.PositionInTiles && this.TypeOfPylon == other.TypeOfPylon;
		}

		// Token: 0x0400529B RID: 21147
		public Point16 PositionInTiles;

		// Token: 0x0400529C RID: 21148
		public TeleportPylonType TypeOfPylon;
	}
}
