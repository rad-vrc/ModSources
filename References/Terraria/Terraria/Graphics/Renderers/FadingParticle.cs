using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000126 RID: 294
	public class FadingParticle : ABasicParticle
	{
		// Token: 0x06001768 RID: 5992 RVA: 0x004D2947 File Offset: 0x004D0B47
		public override void FetchFromPool()
		{
			base.FetchFromPool();
			this.FadeInNormalizedTime = 0f;
			this.FadeOutNormalizedTime = 1f;
			this.ColorTint = Color.White;
			this._timeTolive = 0f;
			this._timeSinceSpawn = 0f;
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x004D2986 File Offset: 0x004D0B86
		public void SetTypeInfo(float timeToLive)
		{
			this._timeTolive = timeToLive;
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x004D298F File Offset: 0x004D0B8F
		public override void Update(ref ParticleRendererSettings settings)
		{
			base.Update(ref settings);
			this._timeSinceSpawn += 1f;
			if (this._timeSinceSpawn >= this._timeTolive)
			{
				base.ShouldBeRemovedFromRenderer = true;
			}
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x004D29C0 File Offset: 0x004D0BC0
		public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			Color color = this.ColorTint * Utils.GetLerpValue(0f, this.FadeInNormalizedTime, this._timeSinceSpawn / this._timeTolive, true) * Utils.GetLerpValue(1f, this.FadeOutNormalizedTime, this._timeSinceSpawn / this._timeTolive, true);
			spritebatch.Draw(this._texture.Value, settings.AnchorPosition + this.LocalPosition, new Rectangle?(this._frame), color, this.Rotation, this._origin, this.Scale, SpriteEffects.None, 0f);
		}

		// Token: 0x0400141E RID: 5150
		public float FadeInNormalizedTime;

		// Token: 0x0400141F RID: 5151
		public float FadeOutNormalizedTime = 1f;

		// Token: 0x04001420 RID: 5152
		public Color ColorTint = Color.White;

		// Token: 0x04001421 RID: 5153
		private float _timeTolive;

		// Token: 0x04001422 RID: 5154
		private float _timeSinceSpawn;
	}
}
