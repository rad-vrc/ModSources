using System;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace QoLCompendium.Content.Tiles.CraftingStations.CrossMod
{
	// Token: 0x0200001D RID: 29
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class HardmodeCalamityMonolithTile : ModTile
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00003D64 File Offset: 0x00001F64
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
			Main.tileNoAttach[(int)base.Type] = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				16
			};
			TileObjectData.addTile((int)base.Type);
			LocalizedText name = base.CreateMapEntryName();
			base.AddMapEntry(new Color(200, 200, 200), name);
			TileID.Sets.DisableSmartCursor[(int)base.Type] = true;
			base.AdjTiles = new int[]
			{
				ModContent.TileType<AncientAltar>(),
				ModContent.TileType<AshenAltar>(),
				ModContent.TileType<MonolithAmalgam>(),
				ModContent.TileType<PlagueInfuser>(),
				ModContent.TileType<VoidCondenser>()
			};
			base.DustType = -1;
		}
	}
}
