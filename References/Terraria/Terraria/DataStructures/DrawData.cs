using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.DataStructures
{
	// Token: 0x02000449 RID: 1097
	public struct DrawData
	{
		// Token: 0x06002BE7 RID: 11239 RVA: 0x0059FE0C File Offset: 0x0059E00C
		public DrawData(Texture2D texture, Vector2 position, Color color)
		{
			this.texture = texture;
			this.position = position;
			this.color = color;
			this.destinationRectangle = default(Rectangle);
			this.sourceRect = DrawData.nullRectangle;
			this.rotation = 0f;
			this.origin = Vector2.Zero;
			this.scale = Vector2.One;
			this.effect = SpriteEffects.None;
			this.shader = 0;
			this.ignorePlayerRotation = false;
			this.useDestinationRectangle = false;
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x0059FE84 File Offset: 0x0059E084
		public DrawData(Texture2D texture, Vector2 position, Rectangle? sourceRect, Color color)
		{
			this.texture = texture;
			this.position = position;
			this.color = color;
			this.destinationRectangle = default(Rectangle);
			this.sourceRect = sourceRect;
			this.rotation = 0f;
			this.origin = Vector2.Zero;
			this.scale = Vector2.One;
			this.effect = SpriteEffects.None;
			this.shader = 0;
			this.ignorePlayerRotation = false;
			this.useDestinationRectangle = false;
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x0059FEF8 File Offset: 0x0059E0F8
		public DrawData(Texture2D texture, Vector2 position, Rectangle? sourceRect, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effect, float inactiveLayerDepth = 0f)
		{
			this.texture = texture;
			this.position = position;
			this.sourceRect = sourceRect;
			this.color = color;
			this.rotation = rotation;
			this.origin = origin;
			this.scale = new Vector2(scale, scale);
			this.effect = effect;
			this.destinationRectangle = default(Rectangle);
			this.shader = 0;
			this.ignorePlayerRotation = false;
			this.useDestinationRectangle = false;
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x0059FF6C File Offset: 0x0059E16C
		public DrawData(Texture2D texture, Vector2 position, Rectangle? sourceRect, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effect, float inactiveLayerDepth = 0f)
		{
			this.texture = texture;
			this.position = position;
			this.sourceRect = sourceRect;
			this.color = color;
			this.rotation = rotation;
			this.origin = origin;
			this.scale = scale;
			this.effect = effect;
			this.destinationRectangle = default(Rectangle);
			this.shader = 0;
			this.ignorePlayerRotation = false;
			this.useDestinationRectangle = false;
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x0059FFD8 File Offset: 0x0059E1D8
		public DrawData(Texture2D texture, Rectangle destinationRectangle, Color color)
		{
			this.texture = texture;
			this.destinationRectangle = destinationRectangle;
			this.color = color;
			this.position = Vector2.Zero;
			this.sourceRect = DrawData.nullRectangle;
			this.rotation = 0f;
			this.origin = Vector2.Zero;
			this.scale = Vector2.One;
			this.effect = SpriteEffects.None;
			this.shader = 0;
			this.ignorePlayerRotation = false;
			this.useDestinationRectangle = false;
		}

		// Token: 0x06002BEC RID: 11244 RVA: 0x005A0050 File Offset: 0x0059E250
		public DrawData(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRect, Color color)
		{
			this.texture = texture;
			this.destinationRectangle = destinationRectangle;
			this.color = color;
			this.position = Vector2.Zero;
			this.sourceRect = sourceRect;
			this.rotation = 0f;
			this.origin = Vector2.Zero;
			this.scale = Vector2.One;
			this.effect = SpriteEffects.None;
			this.shader = 0;
			this.ignorePlayerRotation = false;
			this.useDestinationRectangle = false;
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x005A00C4 File Offset: 0x0059E2C4
		public DrawData(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRect, Color color, float rotation, Vector2 origin, SpriteEffects effect, float inactiveLayerDepth = 0f)
		{
			this.texture = texture;
			this.destinationRectangle = destinationRectangle;
			this.sourceRect = sourceRect;
			this.color = color;
			this.rotation = rotation;
			this.origin = origin;
			this.effect = effect;
			this.position = Vector2.Zero;
			this.scale = Vector2.One;
			this.shader = 0;
			this.ignorePlayerRotation = false;
			this.useDestinationRectangle = false;
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x005A0134 File Offset: 0x0059E334
		public void Draw(SpriteBatch sb)
		{
			if (this.useDestinationRectangle)
			{
				sb.Draw(this.texture, this.destinationRectangle, this.sourceRect, this.color, this.rotation, this.origin, this.effect, 0f);
				return;
			}
			sb.Draw(this.texture, this.position, this.sourceRect, this.color, this.rotation, this.origin, this.scale, this.effect, 0f);
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x005A01BC File Offset: 0x0059E3BC
		public void Draw(SpriteDrawBuffer sb)
		{
			if (this.useDestinationRectangle)
			{
				sb.Draw(this.texture, this.destinationRectangle, this.sourceRect, this.color, this.rotation, this.origin, this.effect);
				return;
			}
			sb.Draw(this.texture, this.position, this.sourceRect, this.color, this.rotation, this.origin, this.scale, this.effect);
		}

		// Token: 0x04005006 RID: 20486
		public Texture2D texture;

		// Token: 0x04005007 RID: 20487
		public Vector2 position;

		// Token: 0x04005008 RID: 20488
		public Rectangle destinationRectangle;

		// Token: 0x04005009 RID: 20489
		public Rectangle? sourceRect;

		// Token: 0x0400500A RID: 20490
		public Color color;

		// Token: 0x0400500B RID: 20491
		public float rotation;

		// Token: 0x0400500C RID: 20492
		public Vector2 origin;

		// Token: 0x0400500D RID: 20493
		public Vector2 scale;

		// Token: 0x0400500E RID: 20494
		public SpriteEffects effect;

		// Token: 0x0400500F RID: 20495
		public int shader;

		// Token: 0x04005010 RID: 20496
		public bool ignorePlayerRotation;

		// Token: 0x04005011 RID: 20497
		public readonly bool useDestinationRectangle;

		// Token: 0x04005012 RID: 20498
		public static Rectangle? nullRectangle;
	}
}
