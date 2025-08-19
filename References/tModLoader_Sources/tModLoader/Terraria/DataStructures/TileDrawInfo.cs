using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Contains info about a tile to be drawn. Tiles aren't necessarily drawn using the <see cref="P:Terraria.Tile.TileFrameX" /> and <see cref="P:Terraria.Tile.TileFrameY" /> values directly.
	/// <para /> The values contained are a result of changes made by various ModTile methods (<see cref="M:Terraria.ModLoader.ModTile.SetDrawPositions(System.Int32,System.Int32,System.Int32@,System.Int32@,System.Int32@,System.Int16@,System.Int16@)" />, <see cref="M:Terraria.ModLoader.ModTile.AnimateTile(System.Int32@,System.Int32@)" />, <see cref="M:Terraria.ModLoader.ModTile.AnimateIndividualTile(System.Int32,System.Int32,System.Int32,System.Int32@,System.Int32@)" />, <see cref="M:Terraria.ModLoader.ModTile.SetSpriteEffects(System.Int32,System.Int32,Microsoft.Xna.Framework.Graphics.SpriteEffects@)" />, and finally <see cref="M:Terraria.ModLoader.ModTile.DrawEffects(System.Int32,System.Int32,Microsoft.Xna.Framework.Graphics.SpriteBatch,Terraria.DataStructures.TileDrawInfo@)" />).
	/// </summary>
	// Token: 0x02000735 RID: 1845
	public class TileDrawInfo
	{
		/// <summary> The Tile to be drawn </summary>
		// Token: 0x04006020 RID: 24608
		public Tile tileCache;

		/// <summary> The <see cref="P:Terraria.Tile.TileType" /> of this tile </summary>
		// Token: 0x04006021 RID: 24609
		public ushort typeCache;

		/// <summary>
		/// The X value for the frame of this tile to be drawn. Derived from <see cref="P:Terraria.Tile.TileFrameX" /> and further changed by the tileFrameX parameter of <see cref="M:Terraria.ModLoader.ModTile.SetDrawPositions(System.Int32,System.Int32,System.Int32@,System.Int32@,System.Int32@,System.Int16@,System.Int16@)" />
		/// </summary>
		// Token: 0x04006022 RID: 24610
		public short tileFrameX;

		/// <summary>
		/// The Y value for the frame of this tile to be drawn. Derived from <see cref="P:Terraria.Tile.TileFrameY" /> and further changed by the tileFrameY parameter of <see cref="M:Terraria.ModLoader.ModTile.SetDrawPositions(System.Int32,System.Int32,System.Int32@,System.Int32@,System.Int32@,System.Int16@,System.Int16@)" />
		/// </summary>
		// Token: 0x04006023 RID: 24611
		public short tileFrameY;

		// Token: 0x04006024 RID: 24612
		public Texture2D drawTexture;

		// Token: 0x04006025 RID: 24613
		public Color tileLight;

		/// <summary>
		/// Offsets the drawing of the tile vertically. Derived from <see cref="P:Terraria.ObjectData.TileObjectData.DrawYOffset" /> and further changed by the offsetY parameter of <see cref="M:Terraria.ModLoader.ModTile.SetDrawPositions(System.Int32,System.Int32,System.Int32@,System.Int32@,System.Int32@,System.Int16@,System.Int16@)" />.
		/// </summary>
		// Token: 0x04006026 RID: 24614
		public int tileTop;

		/// <summary>
		/// The width for the frame of this tile to be drawn. Derived from <see cref="P:Terraria.ObjectData.TileObjectData.CoordinateWidth" /> and further changed by the width parameter of <see cref="M:Terraria.ModLoader.ModTile.SetDrawPositions(System.Int32,System.Int32,System.Int32@,System.Int32@,System.Int32@,System.Int16@,System.Int16@)" />.
		/// </summary>
		// Token: 0x04006027 RID: 24615
		public int tileWidth;

		/// <summary>
		/// The height for the frame of this tile to be drawn. Derived from <see cref="P:Terraria.ObjectData.TileObjectData.CoordinateHeights" /> and further changed by the height parameter of <see cref="M:Terraria.ModLoader.ModTile.SetDrawPositions(System.Int32,System.Int32,System.Int32@,System.Int32@,System.Int32@,System.Int16@,System.Int16@)" />.
		/// </summary>
		// Token: 0x04006028 RID: 24616
		public int tileHeight;

		// Token: 0x04006029 RID: 24617
		public int halfBrickHeight;

		/// <summary>
		/// An additional offset to <see cref="F:Terraria.DataStructures.TileDrawInfo.tileFrameX" /> corresponding to the animation of the tile. Defaults to 0 and further changed by the frameXOffset parameter of <see cref="M:Terraria.ModLoader.ModTile.AnimateIndividualTile(System.Int32,System.Int32,System.Int32,System.Int32@,System.Int32@)" />.
		/// </summary>
		// Token: 0x0400602A RID: 24618
		public int addFrY;

		/// <summary>
		/// An additional offset to <see cref="F:Terraria.DataStructures.TileDrawInfo.tileFrameY" /> corresponding to the animation of the tile. Defaults to <c>modTile.AnimationFrameHeight * Main.tileFrame[type]</c> (which itself is set in <see cref="M:Terraria.ModLoader.ModTile.AnimateTile(System.Int32@,System.Int32@)" />) and further changed by the frameXOffset parameter of <see cref="M:Terraria.ModLoader.ModTile.AnimateIndividualTile(System.Int32,System.Int32,System.Int32,System.Int32@,System.Int32@)" />.
		/// </summary>
		// Token: 0x0400602B RID: 24619
		public int addFrX;

		/// <summary>
		/// If the tile should be drawn flipped or not. Defaults to <see cref="F:Microsoft.Xna.Framework.Graphics.SpriteEffects.None" /> and changed by the spriteEffects parameter of <see cref="M:Terraria.ModLoader.ModTile.SetSpriteEffects(System.Int32,System.Int32,Microsoft.Xna.Framework.Graphics.SpriteEffects@)" />.
		/// </summary>
		// Token: 0x0400602C RID: 24620
		public SpriteEffects tileSpriteEffect;

		// Token: 0x0400602D RID: 24621
		public Texture2D glowTexture;

		// Token: 0x0400602E RID: 24622
		public Rectangle glowSourceRect;

		// Token: 0x0400602F RID: 24623
		public Color glowColor;

		// Token: 0x04006030 RID: 24624
		public Vector3[] colorSlices = new Vector3[9];

		// Token: 0x04006031 RID: 24625
		public Color finalColor;

		// Token: 0x04006032 RID: 24626
		public Color colorTint;
	}
}
