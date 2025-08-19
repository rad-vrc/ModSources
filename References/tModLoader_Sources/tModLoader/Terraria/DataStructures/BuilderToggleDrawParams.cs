using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Holds data required for builder toggle drawing.
	/// </summary>
	// Token: 0x020006D5 RID: 1749
	public struct BuilderToggleDrawParams
	{
		// Token: 0x06004911 RID: 18705 RVA: 0x0064C8E2 File Offset: 0x0064AAE2
		public BuilderToggleDrawParams()
		{
			this.Texture = null;
			this.Position = default(Vector2);
			this.Frame = default(Rectangle);
			this.Color = Color.White;
			this.Scale = 1f;
			this.SpriteEffects = 0;
		}

		/// <summary> The icon or icon hover texture </summary>
		// Token: 0x04005E6E RID: 24174
		public Texture2D Texture;

		/// <summary> The position </summary>
		// Token: 0x04005E6F RID: 24175
		public Vector2 Position;

		/// <summary> The frame rectangle (aka source rectangle) </summary>
		// Token: 0x04005E70 RID: 24176
		public Rectangle Frame;

		/// <summary> The color the icon or icon hover is drawn in. Defaults to White for icon, <see cref="F:Terraria.Main.OurFavoriteColor" /> (yellow) for icon hover. </summary>
		// Token: 0x04005E71 RID: 24177
		public Color Color;

		/// <summary> The scale of the icon or icon hover </summary>
		// Token: 0x04005E72 RID: 24178
		public float Scale;

		/// <summary> The spriteEffects </summary>
		// Token: 0x04005E73 RID: 24179
		public SpriteEffects SpriteEffects;
	}
}
