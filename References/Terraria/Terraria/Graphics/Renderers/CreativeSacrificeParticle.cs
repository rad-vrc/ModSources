using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000124 RID: 292
	public class CreativeSacrificeParticle : IParticle
	{
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06001759 RID: 5977 RVA: 0x004D25A3 File Offset: 0x004D07A3
		// (set) Token: 0x0600175A RID: 5978 RVA: 0x004D25AB File Offset: 0x004D07AB
		public bool ShouldBeRemovedFromRenderer { get; private set; }

		// Token: 0x0600175B RID: 5979 RVA: 0x004D25B4 File Offset: 0x004D07B4
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

		// Token: 0x0600175C RID: 5980 RVA: 0x004D2640 File Offset: 0x004D0840
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

		// Token: 0x0600175D RID: 5981 RVA: 0x004D26A4 File Offset: 0x004D08A4
		public void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			Color color = Color.Lerp(Color.White, new Color(255, 255, 255, 0), Utils.GetLerpValue(0.1f, 0.5f, this._scale, false));
			spritebatch.Draw(this._texture.Value, settings.AnchorPosition + this.LocalPosition, new Rectangle?(this._frame), color, 0f, this._origin, this._scale, SpriteEffects.None, 0f);
		}

		// Token: 0x04001407 RID: 5127
		public Vector2 AccelerationPerFrame;

		// Token: 0x04001408 RID: 5128
		public Vector2 Velocity;

		// Token: 0x04001409 RID: 5129
		public Vector2 LocalPosition;

		// Token: 0x0400140A RID: 5130
		public float ScaleOffsetPerFrame;

		// Token: 0x0400140B RID: 5131
		public float StopWhenBelowXScale;

		// Token: 0x0400140C RID: 5132
		private Asset<Texture2D> _texture;

		// Token: 0x0400140D RID: 5133
		private Rectangle _frame;

		// Token: 0x0400140E RID: 5134
		private Vector2 _origin;

		// Token: 0x0400140F RID: 5135
		private float _scale;
	}
}
