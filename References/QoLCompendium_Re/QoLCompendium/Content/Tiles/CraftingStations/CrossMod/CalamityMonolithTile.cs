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
	// Token: 0x0200001C RID: 28
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class CalamityMonolithTile : ModTile
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00003C44 File Offset: 0x00001E44
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
				ModContent.TileType<WulfrumLabstation>(),
				ModContent.TileType<EutrophicShelf>(),
				ModContent.TileType<StaticRefiner>(),
				ModContent.TileType<AncientAltar>(),
				ModContent.TileType<AshenAltar>(),
				ModContent.TileType<MonolithAmalgam>(),
				ModContent.TileType<PlagueInfuser>(),
				ModContent.TileType<VoidCondenser>(),
				ModContent.TileType<ProfanedCrucible>(),
				ModContent.TileType<BotanicPlanter>(),
				ModContent.TileType<SilvaBasin>(),
				ModContent.TileType<SCalAltar>(),
				ModContent.TileType<SCalAltarLarge>(),
				ModContent.TileType<CosmicAnvil>(),
				ModContent.TileType<DraedonsForge>()
			};
			base.DustType = -1;
		}
	}
}
