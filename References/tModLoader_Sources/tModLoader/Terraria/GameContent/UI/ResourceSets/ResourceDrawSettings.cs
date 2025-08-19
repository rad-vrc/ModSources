using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace Terraria.GameContent.UI.ResourceSets
{
	// Token: 0x020004F4 RID: 1268
	public struct ResourceDrawSettings
	{
		// Token: 0x06003D74 RID: 15732 RVA: 0x005CA9E0 File Offset: 0x005C8BE0
		public void Draw(SpriteBatch spriteBatch, ref bool isHovered)
		{
			int elementCount = this.ElementCount;
			Vector2 topLeftAnchor = this.TopLeftAnchor;
			Point value = Main.MouseScreen.ToPoint();
			for (int i = 0; i < elementCount; i++)
			{
				int elementIndex = i + this.ElementIndexOffset;
				Asset<Texture2D> texture;
				Vector2 drawOffset;
				float drawScale;
				Rectangle? sourceRect;
				this.GetTextureMethod(elementIndex, this.ElementIndexOffset, this.ElementIndexOffset + elementCount - 1, out texture, out drawOffset, out drawScale, out sourceRect);
				Rectangle rectangle = texture.Frame(1, 1, 0, 0, 0, 0);
				if (sourceRect != null)
				{
					rectangle = sourceRect.Value;
				}
				Vector2 position = topLeftAnchor + drawOffset;
				Vector2 origin = this.OffsetSpriteAnchor + rectangle.Size() * this.OffsetSpriteAnchorByTexturePercentile;
				Rectangle rectangle2 = rectangle;
				rectangle2.X += (int)(position.X - origin.X);
				rectangle2.Y += (int)(position.Y - origin.Y);
				if (rectangle2.Contains(value))
				{
					isHovered = true;
				}
				ResourceOverlayLoader.DrawResource(new ResourceOverlayDrawContext(this.StatsSnapshot, this.DisplaySet, elementIndex + this.ResourceIndexOffset, texture)
				{
					position = position,
					source = new Rectangle?(rectangle),
					origin = origin,
					scale = new Vector2(drawScale),
					SpriteBatch = spriteBatch
				});
				topLeftAnchor += this.OffsetPerDraw + rectangle.Size() * this.OffsetPerDrawByTexturePercentile;
			}
		}

		// Token: 0x04005643 RID: 22083
		public Vector2 TopLeftAnchor;

		// Token: 0x04005644 RID: 22084
		public int ElementCount;

		// Token: 0x04005645 RID: 22085
		public int ElementIndexOffset;

		// Token: 0x04005646 RID: 22086
		public ResourceDrawSettings.TextureGetter GetTextureMethod;

		// Token: 0x04005647 RID: 22087
		public Vector2 OffsetPerDraw;

		// Token: 0x04005648 RID: 22088
		public Vector2 OffsetPerDrawByTexturePercentile;

		// Token: 0x04005649 RID: 22089
		public Vector2 OffsetSpriteAnchor;

		// Token: 0x0400564A RID: 22090
		public Vector2 OffsetSpriteAnchorByTexturePercentile;

		// Token: 0x0400564B RID: 22091
		public PlayerStatsSnapshot StatsSnapshot;

		// Token: 0x0400564C RID: 22092
		public IPlayerResourcesDisplaySet DisplaySet;

		// Token: 0x0400564D RID: 22093
		public int ResourceIndexOffset;

		// Token: 0x02000C0B RID: 3083
		// (Invoke) Token: 0x06005ED8 RID: 24280
		public delegate void TextureGetter(int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> texture, out Vector2 drawOffset, out float drawScale, out Rectangle? sourceRect);
	}
}
