using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Holds data required for buff drawing.
	/// </summary>
	// Token: 0x020006D3 RID: 1747
	public struct BuffDrawParams
	{
		// Token: 0x0600490A RID: 18698 RVA: 0x0064C5FA File Offset: 0x0064A7FA
		public BuffDrawParams(Texture2D texture, Vector2 position, Vector2 textPosition, Rectangle sourceRectangle, Rectangle mouseRectangle, Color drawColor)
		{
			this.Texture = texture;
			this.Position = position;
			this.TextPosition = textPosition;
			this.SourceRectangle = sourceRectangle;
			this.MouseRectangle = mouseRectangle;
			this.DrawColor = drawColor;
		}

		// Token: 0x0600490B RID: 18699 RVA: 0x0064C62C File Offset: 0x0064A82C
		public void Deconstruct(out Texture2D texture, out Vector2 position, out Vector2 textPosition, out Rectangle sourceRectangle, out Rectangle mouseRectangle, out Color drawColor)
		{
			texture = this.Texture;
			position = this.Position;
			textPosition = this.TextPosition;
			sourceRectangle = this.SourceRectangle;
			mouseRectangle = this.MouseRectangle;
			drawColor = this.DrawColor;
		}

		/// <summary>
		/// The texture used for drawing the buff.
		/// </summary>
		// Token: 0x04005E61 RID: 24161
		public Texture2D Texture;

		/// <summary>
		/// Top-left position of the buff on the screen.
		/// </summary>
		// Token: 0x04005E62 RID: 24162
		public Vector2 Position;

		/// <summary>
		/// Top left position of the text below the buff (remaining time).
		/// </summary>
		// Token: 0x04005E63 RID: 24163
		public Vector2 TextPosition;

		/// <summary>
		/// The frame displayed from the texture. Defaults to the entire texture size.
		/// </summary>
		// Token: 0x04005E64 RID: 24164
		public Rectangle SourceRectangle;

		/// <summary>
		/// Defaults to the size of the autoloaded buffs' sprite, it handles mouseovering and clicking on the buff icon.
		/// If you offset the position, or have a non-standard size, change it accordingly.
		/// </summary>
		// Token: 0x04005E65 RID: 24165
		public Rectangle MouseRectangle;

		/// <summary>
		/// Color used to draw the buff. Use Main.buffAlpha[buffIndex] accordingly if you change it.
		/// </summary>
		// Token: 0x04005E66 RID: 24166
		public Color DrawColor;
	}
}
