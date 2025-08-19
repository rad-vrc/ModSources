using System;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.TileChanges
{
	// Token: 0x02000221 RID: 545
	public class FailBreak : GlobalTile
	{
		// Token: 0x06000D3B RID: 3387 RVA: 0x0006748C File Offset: 0x0006568C
		public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
		{
			if (QoLCompendium.mainConfig.RegrowthAutoReplant && (Main.LocalPlayer.HeldItem.type == 213 || Main.LocalPlayer.HeldItem.type == 5295 || (Main.mouseItem != null && (Main.mouseItem.type == 213 || Main.mouseItem.type == 5295))) && (type == 82 || type == 83 || type == 84))
			{
				fail = true;
			}
		}
	}
}
