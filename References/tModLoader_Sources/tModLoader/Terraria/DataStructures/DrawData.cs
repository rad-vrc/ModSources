using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.DataStructures
{
	// Token: 0x020006DD RID: 1757
	public struct DrawData
	{
		// Token: 0x06004931 RID: 18737 RVA: 0x0064D180 File Offset: 0x0064B380
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
			this.effect = 0;
			this.shader = 0;
			this.ignorePlayerRotation = false;
			this.useDestinationRectangle = false;
		}

		// Token: 0x06004932 RID: 18738 RVA: 0x0064D1F8 File Offset: 0x0064B3F8
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
			this.effect = 0;
			this.shader = 0;
			this.ignorePlayerRotation = false;
			this.useDestinationRectangle = false;
		}

		// Token: 0x06004933 RID: 18739 RVA: 0x0064D26C File Offset: 0x0064B46C
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

		// Token: 0x06004934 RID: 18740 RVA: 0x0064D2E0 File Offset: 0x0064B4E0
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

		// Token: 0x06004935 RID: 18741 RVA: 0x0064D34C File Offset: 0x0064B54C
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
			this.effect = 0;
			this.shader = 0;
			this.ignorePlayerRotation = false;
			this.useDestinationRectangle = true;
		}

		// Token: 0x06004936 RID: 18742 RVA: 0x0064D3C4 File Offset: 0x0064B5C4
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
			this.effect = 0;
			this.shader = 0;
			this.ignorePlayerRotation = false;
			this.useDestinationRectangle = true;
		}

		// Token: 0x06004937 RID: 18743 RVA: 0x0064D438 File Offset: 0x0064B638
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
			this.useDestinationRectangle = true;
		}

		// Token: 0x06004938 RID: 18744 RVA: 0x0064D4A8 File Offset: 0x0064B6A8
		public void Draw(SpriteBatch sb)
		{
			if (this.useDestinationRectangle)
			{
				sb.Draw(this.texture, this.destinationRectangle, this.sourceRect, this.color, this.rotation, this.origin, this.effect, 0f);
				return;
			}
			sb.Draw(this.texture, this.position, this.sourceRect, this.color, this.rotation, this.origin, this.scale, this.effect, 0f);
		}

		// Token: 0x06004939 RID: 18745 RVA: 0x0064D530 File Offset: 0x0064B730
		public void Draw(SpriteDrawBuffer sb)
		{
			if (this.useDestinationRectangle)
			{
				sb.Draw(this.texture, this.destinationRectangle, this.sourceRect, this.color, this.rotation, this.origin, this.effect);
				return;
			}
			sb.Draw(this.texture, this.position, this.sourceRect, this.color, this.rotation, this.origin, this.scale, this.effect);
		}

		// Token: 0x04005E95 RID: 24213
		public Texture2D texture;

		// Token: 0x04005E96 RID: 24214
		public Vector2 position;

		// Token: 0x04005E97 RID: 24215
		public Rectangle destinationRectangle;

		// Token: 0x04005E98 RID: 24216
		public Rectangle? sourceRect;

		// Token: 0x04005E99 RID: 24217
		public Color color;

		// Token: 0x04005E9A RID: 24218
		public float rotation;

		// Token: 0x04005E9B RID: 24219
		public Vector2 origin;

		// Token: 0x04005E9C RID: 24220
		public Vector2 scale;

		// Token: 0x04005E9D RID: 24221
		public SpriteEffects effect;

		// Token: 0x04005E9E RID: 24222
		public int shader;

		// Token: 0x04005E9F RID: 24223
		public bool ignorePlayerRotation;

		// Token: 0x04005EA0 RID: 24224
		public readonly bool useDestinationRectangle;

		// Token: 0x04005EA1 RID: 24225
		public static Rectangle? nullRectangle;
	}
}
