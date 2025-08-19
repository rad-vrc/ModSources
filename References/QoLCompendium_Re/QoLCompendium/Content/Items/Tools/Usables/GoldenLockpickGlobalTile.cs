using System;
using QoLCompendium.Core;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Usables
{
	// Token: 0x0200017D RID: 381
	public class GoldenLockpickGlobalTile : GlobalTile
	{
		// Token: 0x060007B8 RID: 1976 RVA: 0x00015350 File Offset: 0x00013550
		public unsafe override void RightClick(int i, int j, int type)
		{
			if (type != 21 || *Main.tile[i, j].TileFrameX < 72 || *Main.tile[i, j].TileFrameX > 108 || !Main.LocalPlayer.GetModPlayer<QoLCPlayer>().HasGoldenLockpick)
			{
				return;
			}
			i -= (int)(*Main.tile[i, j].TileFrameX % 36 / 18);
			j -= (int)(*Main.tile[i, j].TileFrameY % 36 / 18);
			Chest.Unlock(i, j);
		}
	}
}
