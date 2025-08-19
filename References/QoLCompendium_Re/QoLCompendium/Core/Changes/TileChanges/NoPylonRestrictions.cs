using System;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.TileChanges
{
	// Token: 0x0200021E RID: 542
	public class NoPylonRestrictions : GlobalPylon
	{
		// Token: 0x06000D2B RID: 3371 RVA: 0x00066F24 File Offset: 0x00065124
		public override bool? ValidTeleportCheck_PreAnyDanger(TeleportPylonInfo pylonInfo)
		{
			if (QoLCompendium.mainConfig.NoPylonTeleportRestrictions)
			{
				return new bool?(true);
			}
			return null;
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00066F50 File Offset: 0x00065150
		public override bool? ValidTeleportCheck_PreNPCCount(TeleportPylonInfo pylonInfo, ref int defaultNecessaryNPCCount)
		{
			if (QoLCompendium.mainConfig.NoPylonTeleportRestrictions)
			{
				return new bool?(true);
			}
			return null;
		}
	}
}
