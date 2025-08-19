using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.UI;

namespace Terraria.Map
{
	// Token: 0x020000CB RID: 203
	public struct MapOverlayDrawContext
	{
		// Token: 0x0600146B RID: 5227 RVA: 0x004A2F63 File Offset: 0x004A1163
		public MapOverlayDrawContext(Vector2 mapPosition, Vector2 mapOffset, Rectangle? clippingRect, float mapScale, float drawScale)
		{
			this._mapPosition = mapPosition;
			this._mapOffset = mapOffset;
			this._clippingRect = clippingRect;
			this._mapScale = mapScale;
			this._drawScale = drawScale;
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x004A2F8A File Offset: 0x004A118A
		public MapOverlayDrawContext.DrawResult Draw(Texture2D texture, Vector2 position, Alignment alignment)
		{
			return this.Draw(texture, position, new SpriteFrame(1, 1), alignment);
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x004A2F9C File Offset: 0x004A119C
		public MapOverlayDrawContext.DrawResult Draw(Texture2D texture, Vector2 position, SpriteFrame frame, Alignment alignment)
		{
			position = (position - this._mapPosition) * this._mapScale + this._mapOffset;
			if (this._clippingRect != null && !this._clippingRect.Value.Contains(position.ToPoint()))
			{
				return MapOverlayDrawContext.DrawResult.Culled;
			}
			Rectangle sourceRectangle = frame.GetSourceRectangle(texture);
			Vector2 vector = sourceRectangle.Size() * alignment.OffsetMultiplier;
			Main.spriteBatch.Draw(texture, position, new Rectangle?(sourceRectangle), Color.White, 0f, vector, this._drawScale, SpriteEffects.None, 0f);
			position -= vector * this._drawScale;
			Rectangle rectangle = new Rectangle((int)position.X, (int)position.Y, (int)((float)texture.Width * this._drawScale), (int)((float)texture.Height * this._drawScale));
			return new MapOverlayDrawContext.DrawResult(rectangle.Contains(Main.MouseScreen.ToPoint()));
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x004A30A4 File Offset: 0x004A12A4
		public MapOverlayDrawContext.DrawResult Draw(Texture2D texture, Vector2 position, Color color, SpriteFrame frame, float scaleIfNotSelected, float scaleIfSelected, Alignment alignment)
		{
			position = (position - this._mapPosition) * this._mapScale + this._mapOffset;
			if (this._clippingRect != null && !this._clippingRect.Value.Contains(position.ToPoint()))
			{
				return MapOverlayDrawContext.DrawResult.Culled;
			}
			Rectangle sourceRectangle = frame.GetSourceRectangle(texture);
			Vector2 vector = sourceRectangle.Size() * alignment.OffsetMultiplier;
			Vector2 position2 = position;
			float num = this._drawScale * scaleIfNotSelected;
			Vector2 vector2 = position - vector * num;
			Rectangle rectangle = new Rectangle((int)vector2.X, (int)vector2.Y, (int)((float)sourceRectangle.Width * num), (int)((float)sourceRectangle.Height * num));
			bool flag = rectangle.Contains(Main.MouseScreen.ToPoint());
			float scale = num;
			if (flag)
			{
				scale = this._drawScale * scaleIfSelected;
			}
			Main.spriteBatch.Draw(texture, position2, new Rectangle?(sourceRectangle), color, 0f, vector, scale, SpriteEffects.None, 0f);
			return new MapOverlayDrawContext.DrawResult(flag);
		}

		// Token: 0x0400120A RID: 4618
		private readonly Vector2 _mapPosition;

		// Token: 0x0400120B RID: 4619
		private readonly Vector2 _mapOffset;

		// Token: 0x0400120C RID: 4620
		private readonly Rectangle? _clippingRect;

		// Token: 0x0400120D RID: 4621
		private readonly float _mapScale;

		// Token: 0x0400120E RID: 4622
		private readonly float _drawScale;

		// Token: 0x02000560 RID: 1376
		public struct DrawResult
		{
			// Token: 0x06003129 RID: 12585 RVA: 0x005E4D6C File Offset: 0x005E2F6C
			public DrawResult(bool isMouseOver)
			{
				this.IsMouseOver = isMouseOver;
			}

			// Token: 0x040058F0 RID: 22768
			public static readonly MapOverlayDrawContext.DrawResult Culled = new MapOverlayDrawContext.DrawResult(false);

			// Token: 0x040058F1 RID: 22769
			public readonly bool IsMouseOver;
		}
	}
}
