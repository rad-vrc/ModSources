using System;
using Terraria.ModLoader.IO;

namespace Terraria.ModLoader.Default
{
	// Token: 0x020002DB RID: 731
	public abstract class UnloadedTile : ModTile
	{
		// Token: 0x06002E10 RID: 11792 RVA: 0x00530E28 File Offset: 0x0052F028
		public unsafe override void MouseOver(int i, int j)
		{
			if (Main.netMode != 0)
			{
				return;
			}
			Tile tile = Main.tile[i, j];
			if (tile == null || *tile.type != base.Type)
			{
				return;
			}
			Player localPlayer = Main.LocalPlayer;
			ushort type = TileIO.Tiles.unloadedEntryLookup.Lookup(i, j);
			TileEntry info = TileIO.Tiles.entries[(int)type];
			localPlayer.cursorItemIconEnabled = true;
			localPlayer.cursorItemIconID = -1;
			localPlayer.cursorItemIconText = info.modName + ": " + info.name;
		}
	}
}
