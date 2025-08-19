using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x0200012A RID: 298
	public class PrettySparkleParticle : ABasicParticle
	{
		// Token: 0x0600177D RID: 6013 RVA: 0x004D31E4 File Offset: 0x004D13E4
		public override void FetchFromPool()
		{
			base.FetchFromPool();
			this.ColorTint = Color.Transparent;
			this._timeSinceSpawn = 0f;
			this.Opacity = 0f;
			this.FadeInNormalizedTime = 0.05f;
			this.FadeOutNormalizedTime = 0.9f;
			this.TimeToLive = 60f;
			this.AdditiveAmount = 1f;
			this.FadeInEnd = 20f;
			this.FadeOutStart = 30f;
			this.FadeOutEnd = 45f;
			this.DrawVerticalAxis = (this.DrawHorizontalAxis = true);
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x004D3278 File Offset: 0x004D1478
		public override void Update(ref ParticleRendererSettings settings)
		{
			base.Update(ref settings);
			this._timeSinceSpawn += 1f;
			this.Opacity = Utils.GetLerpValue(0f, this.FadeInNormalizedTime, this._timeSinceSpawn / this.TimeToLive, true) * Utils.GetLerpValue(1f, this.FadeOutNormalizedTime, this._timeSinceSpawn / this.TimeToLive, true);
			if (this._timeSinceSpawn >= this.TimeToLive)
			{
				base.ShouldBeRemovedFromRenderer = true;
			}
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x004D32F8 File Offset: 0x004D14F8
		public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			Color value = Color.White * this.Opacity * 0.9f;
			value.A /= 2;
			Texture2D value2 = TextureAssets.Extra[98].Value;
			Color color = this.ColorTint * this.Opacity * 0.5f;
			color.A = (byte)((float)color.A * (1f - this.AdditiveAmount));
			Vector2 origin = value2.Size() / 2f;
			Color color2 = value * 0.5f;
			float t = this._timeSinceSpawn / this.TimeToLive * 60f;
			float num = Utils.GetLerpValue(0f, this.FadeInEnd, t, true) * Utils.GetLerpValue(this.FadeOutEnd, this.FadeOutStart, t, true);
			Vector2 vector = new Vector2(0.3f, 2f) * num * this.Scale;
			Vector2 vector2 = new Vector2(0.3f, 1f) * num * this.Scale;
			color *= num;
			color2 *= num;
			Vector2 position = settings.AnchorPosition + this.LocalPosition;
			SpriteEffects effects = SpriteEffects.None;
			if (this.DrawHorizontalAxis)
			{
				spritebatch.Draw(value2, position, null, color, 1.5707964f + this.Rotation, origin, vector, effects, 0f);
			}
			if (this.DrawVerticalAxis)
			{
				spritebatch.Draw(value2, position, null, color, 0f + this.Rotation, origin, vector2, effects, 0f);
			}
			if (this.DrawHorizontalAxis)
			{
				spritebatch.Draw(value2, position, null, color2, 1.5707964f + this.Rotation, origin, vector * 0.6f, effects, 0f);
			}
			if (this.DrawVerticalAxis)
			{
				spritebatch.Draw(value2, position, null, color2, 0f + this.Rotation, origin, vector2 * 0.6f, effects, 0f);
			}
		}

		// Token: 0x0400143E RID: 5182
		public float FadeInNormalizedTime = 0.05f;

		// Token: 0x0400143F RID: 5183
		public float FadeOutNormalizedTime = 0.9f;

		// Token: 0x04001440 RID: 5184
		public float TimeToLive = 60f;

		// Token: 0x04001441 RID: 5185
		public Color ColorTint;

		// Token: 0x04001442 RID: 5186
		public float Opacity;

		// Token: 0x04001443 RID: 5187
		public float AdditiveAmount = 1f;

		// Token: 0x04001444 RID: 5188
		public float FadeInEnd = 20f;

		// Token: 0x04001445 RID: 5189
		public float FadeOutStart = 30f;

		// Token: 0x04001446 RID: 5190
		public float FadeOutEnd = 45f;

		// Token: 0x04001447 RID: 5191
		public bool DrawHorizontalAxis = true;

		// Token: 0x04001448 RID: 5192
		public bool DrawVerticalAxis = true;

		// Token: 0x04001449 RID: 5193
		private float _timeSinceSpawn;
	}
}
