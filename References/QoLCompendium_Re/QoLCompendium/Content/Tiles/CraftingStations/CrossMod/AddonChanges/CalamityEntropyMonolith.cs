using System;
using System.Collections.Generic;
using System.Linq;
using CalamityEntropy.Content.Tiles;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Tiles.CraftingStations.CrossMod.AddonChanges
{
	// Token: 0x0200001F RID: 31
	[JITWhenModsEnabled(new string[]
	{
		"CalamityEntropy"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityEntropy"
	})]
	public class CalamityEntropyMonolith : GlobalTile
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00003EF4 File Offset: 0x000020F4
		public override int[] AdjTiles(int type)
		{
			HashSet<int> newAdjTiles = base.AdjTiles(type).ToHashSet<int>();
			if (type == ModContent.TileType<CalamityMonolithTile>())
			{
				newAdjTiles.Add(ModContent.TileType<AbyssalAltarTile>());
			}
			return newAdjTiles.ToArray<int>();
		}
	}
}
