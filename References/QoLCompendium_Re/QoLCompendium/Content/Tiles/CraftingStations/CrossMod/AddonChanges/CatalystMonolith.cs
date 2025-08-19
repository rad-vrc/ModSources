using System;
using System.Collections.Generic;
using System.Linq;
using CatalystMod.Tiles.Furniture.CraftingStations;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Tiles.CraftingStations.CrossMod.AddonChanges
{
	// Token: 0x02000020 RID: 32
	[JITWhenModsEnabled(new string[]
	{
		"CatalystMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CatalystMod"
	})]
	public class CatalystMonolith : GlobalTile
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00003F30 File Offset: 0x00002130
		public override int[] AdjTiles(int type)
		{
			HashSet<int> newAdjTiles = base.AdjTiles(type).ToHashSet<int>();
			if (type == ModContent.TileType<CalamityMonolithTile>())
			{
				newAdjTiles.Add(ModContent.TileType<AstralTransmogrifier>());
			}
			return newAdjTiles.ToArray<int>();
		}
	}
}
