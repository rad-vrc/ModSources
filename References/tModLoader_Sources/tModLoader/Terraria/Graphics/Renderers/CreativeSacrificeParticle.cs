using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x0200044D RID: 1101
	public class CreativeSacrificeParticle : IParticle
	{
		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x0600365F RID: 13919 RVA: 0x0057BADF File Offset: 0x00579CDF
		// (set) Token: 0x06003660 RID: 13920 RVA: 0x0057BAE7 File Offset: 0x00579CE7
		public bool ShouldBeRemovedFromRenderer { get; private set; }

		// Token: 0x06003661 RID: 13921 RVA: 0x0057BAF0 File Offset: 0x00579CF0
		public CreativeSacrificeParticle(Asset<Texture2D> textureAsset, Rectangle? frame, Vector2 initialVelocity, Vector2 initialLocalPosition)
		{
			this._texture = textureAsset;
			this._frame = ((frame != null) ? frame.Value : this._texture.Frame(1, 1, 0, 0, 0, 0));
			this._origin = this._frame.Size() / 2f;
			this.Velocity = initialVelocity;
			this.LocalPosition = initialLocalPosition;
			this.StopWhenBelowXScale = 0f;
			this.ShouldBeRemovedFromRenderer = false;
			this._scale = 0.6f;
		}

		// Token: 0x06003662 RID: 13922 RVA: 0x0057BB7C File Offset: 0x00579D7C
		public void Update(ref ParticleRendererSettings settings)
		{
			this.Velocity += this.AccelerationPerFrame;
			this.LocalPosition += this.Velocity;
			this._scale += this.ScaleOffsetPerFrame;
			if (this._scale <= this.StopWhenBelowXScale)
			{
				this.ShouldBeRemovedFromRenderer = true;
			}
		}

		// Token: 0x06003663 RID: 13923 RVA: 0x0057BBE0 File Offset: 0x00579DE0
		public void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			Color color = Color.Lerp(Color.White, new Color(255, 255, 255, 0), Utils.GetLerpValue(0.1f, 0.5f, this._scale, false));
			spritebatch.Draw(this._texture.Value, settings.AnchorPosition + this.LocalPosition, new Rectangle?(this._frame), color, 0f, this._origin, this._scale, 0, 0f);
		}

		// Token: 0x04005039 RID: 20537
		public Vector2 AccelerationPerFrame;

		// Token: 0x0400503A RID: 20538
		public Vector2 Velocity;

		// Token: 0x0400503B RID: 20539
		public Vector2 LocalPosition;

		// Token: 0x0400503C RID: 20540
		public float ScaleOffsetPerFrame;

		// Token: 0x0400503D RID: 20541
		public float StopWhenBelowXScale;

		// Token: 0x0400503E RID: 20542
		private Asset<Texture2D> _texture;

		// Token: 0x0400503F RID: 20543
		private Rectangle _frame;

		// Token: 0x04005040 RID: 20544
		private Vector2 _origin;

		// Token: 0x04005041 RID: 20545
		private float _scale;
	}
}
