using System;
using Terraria.DataStructures;

namespace Terraria.GameContent
{
	// Token: 0x020001EC RID: 492
	public struct TeleportPylonInfo : IEquatable<TeleportPylonInfo>
	{
		// Token: 0x06001CD2 RID: 7378 RVA: 0x004FDB59 File Offset: 0x004FBD59
		public bool Equals(TeleportPylonInfo other)
		{
			return this.PositionInTiles == other.PositionInTiles && this.TypeOfPylon == other.TypeOfPylon;
		}

		// Token: 0x040043C8 RID: 17352
		public Point16 PositionInTiles;

		// Token: 0x040043C9 RID: 17353
		public TeleportPylonType TypeOfPylon;
	}
}
