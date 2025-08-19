using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.DataStructures
{
	// Token: 0x02000459 RID: 1113
	public class TileDrawInfo
	{
		// Token: 0x040050DD RID: 20701
		public Tile tileCache;

		// Token: 0x040050DE RID: 20702
		public ushort typeCache;

		// Token: 0x040050DF RID: 20703
		public short tileFrameX;

		// Token: 0x040050E0 RID: 20704
		public short tileFrameY;

		// Token: 0x040050E1 RID: 20705
		public Texture2D drawTexture;

		// Token: 0x040050E2 RID: 20706
		public Color tileLight;

		// Token: 0x040050E3 RID: 20707
		public int tileTop;

		// Token: 0x040050E4 RID: 20708
		public int tileWidth;

		// Token: 0x040050E5 RID: 20709
		public int tileHeight;

		// Token: 0x040050E6 RID: 20710
		public int halfBrickHeight;

		// Token: 0x040050E7 RID: 20711
		public int addFrY;

		// Token: 0x040050E8 RID: 20712
		public int addFrX;

		// Token: 0x040050E9 RID: 20713
		public SpriteEffects tileSpriteEffect;

		// Token: 0x040050EA RID: 20714
		public Texture2D glowTexture;

		// Token: 0x040050EB RID: 20715
		public Rectangle glowSourceRect;

		// Token: 0x040050EC RID: 20716
		public Color glowColor;

		// Token: 0x040050ED RID: 20717
		public Vector3[] colorSlices = new Vector3[9];

		// Token: 0x040050EE RID: 20718
		public Color finalColor;

		// Token: 0x040050EF RID: 20719
		public Color colorTint;
	}
}
