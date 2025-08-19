using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.TileChanges
{
	// Token: 0x0200021C RID: 540
	public class HellstoneSpelunker : GlobalTile
	{
		// Token: 0x06000D24 RID: 3364 RVA: 0x00066D43 File Offset: 0x00064F43
		public override bool? IsTileSpelunkable(int i, int j, int type)
		{
			if (QoLCompendium.mainConfig.HellstoneSpelunker && type == 58)
			{
				return new bool?(true);
			}
			return base.IsTileSpelunkable(i, j, type);
		}
	}
}
