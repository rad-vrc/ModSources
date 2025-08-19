using System;

namespace Terraria.Enums
{
	// Token: 0x020006C0 RID: 1728
	[Flags]
	public enum AnchorType
	{
		// Token: 0x04005C89 RID: 23689
		None = 0,
		/// <summary> Solid tiles: <see cref="F:Terraria.Main.tileSolid" /> but not <see cref="F:Terraria.Main.tileSolidTop" />, <see cref="F:Terraria.Main.tileNoAttach" />, or sloped (unless <see cref="P:Terraria.ObjectData.TileObjectData.FlattenAnchors" /> is true).
		/// <br /><br /> Sloped solid tiles that are not sloped on the side being anchored to are covered by <see cref="F:Terraria.Enums.AnchorType.SolidSide" />. </summary>
		// Token: 0x04005C8A RID: 23690
		SolidTile = 1,
		/// <summary> Platforms with a top surface and tiles that are <see cref="F:Terraria.Main.tileSolid" /> and <see cref="F:Terraria.Main.tileSolidTop" /> (such as <see cref="F:Terraria.ID.TileID.PlanterBox" /> and <see cref="F:Terraria.ID.TileID.MetalBars" />) </summary>
		// Token: 0x04005C8B RID: 23691
		SolidWithTop = 2,
		/// <summary> Table tiles: <see cref="F:Terraria.Main.tileTable" /> but not <see cref="F:Terraria.ID.TileID.Sets.Platforms" />. Also includes <see cref="F:Terraria.Enums.AnchorType.SolidWithTop" /> tiles. </summary>
		// Token: 0x04005C8C RID: 23692
		Table = 4,
		/// <summary> Solid tiles with full side facing the tile: <see cref="F:Terraria.Main.tileSolid" /> but not <see cref="F:Terraria.Main.tileSolidTop" />. The tile can be sloped as long as the side being anchored to isn't sloped. </summary>
		// Token: 0x04005C8D RID: 23693
		SolidSide = 8,
		// Token: 0x04005C8E RID: 23694
		Tree = 16,
		/// <summary> The tile is contained in <see cref="P:Terraria.ObjectData.TileObjectData.AnchorAlternateTiles" /> </summary>
		// Token: 0x04005C8F RID: 23695
		AlternateTile = 32,
		/// <summary> Either no tile exists or the tile is actuated </summary>
		// Token: 0x04005C90 RID: 23696
		EmptyTile = 64,
		// Token: 0x04005C91 RID: 23697
		SolidBottom = 128,
		// Token: 0x04005C92 RID: 23698
		Platform = 256,
		// Token: 0x04005C93 RID: 23699
		PlanterBox = 512,
		// Token: 0x04005C94 RID: 23700
		PlatformNonHammered = 1024
	}
}
