using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x0200045C RID: 1116
	public class PrettySparkleParticle : ABasicParticle
	{
		// Token: 0x060036B1 RID: 14001 RVA: 0x0057E2F4 File Offset: 0x0057C4F4
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

		// Token: 0x060036B2 RID: 14002 RVA: 0x0057E388 File Offset: 0x0057C588
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

		// Token: 0x060036B3 RID: 14003 RVA: 0x0057E408 File Offset: 0x0057C608
		public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			Color color = Color.White * this.Opacity * 0.9f;
			color.A /= 2;
			Texture2D value = TextureAssets.Extra[98].Value;
			Color color2 = this.ColorTint * this.Opacity * 0.5f;
			color2.A = (byte)((float)color2.A * (1f - this.AdditiveAmount));
			Vector2 origin = value.Size() / 2f;
			Color color3 = color * 0.5f;
			float t = this._timeSinceSpawn / this.TimeToLive * 60f;
			float num = Utils.GetLerpValue(0f, this.FadeInEnd, t, true) * Utils.GetLerpValue(this.FadeOutEnd, this.FadeOutStart, t, true);
			Vector2 vector = new Vector2(0.3f, 2f) * num * this.Scale;
			Vector2 vector2 = new Vector2(0.3f, 1f) * num * this.Scale;
			color2 *= num;
			color3 *= num;
			Vector2 position = settings.AnchorPosition + this.LocalPosition;
			SpriteEffects effects = 0;
			if (this.DrawHorizontalAxis)
			{
				spritebatch.Draw(value, position, null, color2, 1.5707964f + this.Rotation, origin, vector, effects, 0f);
			}
			if (this.DrawVerticalAxis)
			{
				spritebatch.Draw(value, position, null, color2, 0f + this.Rotation, origin, vector2, effects, 0f);
			}
			if (this.DrawHorizontalAxis)
			{
				spritebatch.Draw(value, position, null, color3, 1.5707964f + this.Rotation, origin, vector * 0.6f, effects, 0f);
			}
			if (this.DrawVerticalAxis)
			{
				spritebatch.Draw(value, position, null, color3, 0f + this.Rotation, origin, vector2 * 0.6f, effects, 0f);
			}
		}

		// Token: 0x04005079 RID: 20601
		public float FadeInNormalizedTime = 0.05f;

		// Token: 0x0400507A RID: 20602
		public float FadeOutNormalizedTime = 0.9f;

		// Token: 0x0400507B RID: 20603
		public float TimeToLive = 60f;

		// Token: 0x0400507C RID: 20604
		public Color ColorTint;

		// Token: 0x0400507D RID: 20605
		public float Opacity;

		// Token: 0x0400507E RID: 20606
		public float AdditiveAmount = 1f;

		// Token: 0x0400507F RID: 20607
		public float FadeInEnd = 20f;

		// Token: 0x04005080 RID: 20608
		public float FadeOutStart = 30f;

		// Token: 0x04005081 RID: 20609
		public float FadeOutEnd = 45f;

		// Token: 0x04005082 RID: 20610
		public bool DrawHorizontalAxis = true;

		// Token: 0x04005083 RID: 20611
		public bool DrawVerticalAxis = true;

		// Token: 0x04005084 RID: 20612
		private float _timeSinceSpawn;
	}
}
