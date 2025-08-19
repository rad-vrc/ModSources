using System;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.TileChanges
{
	// Token: 0x02000219 RID: 537
	public class DangersenseChanges : GlobalTile
	{
		// Token: 0x06000D1B RID: 3355 RVA: 0x00066A48 File Offset: 0x00064C48
		public override bool? IsTileDangerous(int i, int j, int type, Player player)
		{
			if (QoLCompendium.mainConfig.DangersenseHighlightsSiltAndSlush && (type == 123 || type == 224))
			{
				return new bool?(true);
			}
			if (QoLCompendium.mainConfig.DangersenseIgnoresThinIce && type == 162)
			{
				return new bool?(false);
			}
			return base.IsTileDangerous(i, j, type, player);
		}
	}
}
