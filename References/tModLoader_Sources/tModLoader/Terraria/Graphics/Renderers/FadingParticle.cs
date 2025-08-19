using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x0200044E RID: 1102
	public class FadingParticle : ABasicParticle
	{
		// Token: 0x06003664 RID: 13924 RVA: 0x0057BC68 File Offset: 0x00579E68
		public override void FetchFromPool()
		{
			base.FetchFromPool();
			this.FadeInNormalizedTime = 0f;
			this.FadeOutNormalizedTime = 1f;
			this.ColorTint = Color.White;
			this._timeTolive = 0f;
			this._timeSinceSpawn = 0f;
		}

		// Token: 0x06003665 RID: 13925 RVA: 0x0057BCA7 File Offset: 0x00579EA7
		public void SetTypeInfo(float timeToLive)
		{
			this._timeTolive = timeToLive;
		}

		// Token: 0x06003666 RID: 13926 RVA: 0x0057BCB0 File Offset: 0x00579EB0
		public override void Update(ref ParticleRendererSettings settings)
		{
			base.Update(ref settings);
			this._timeSinceSpawn += 1f;
			if (this._timeSinceSpawn >= this._timeTolive)
			{
				base.ShouldBeRemovedFromRenderer = true;
			}
		}

		// Token: 0x06003667 RID: 13927 RVA: 0x0057BCE0 File Offset: 0x00579EE0
		public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			Color color = this.ColorTint * Utils.GetLerpValue(0f, this.FadeInNormalizedTime, this._timeSinceSpawn / this._timeTolive, true) * Utils.GetLerpValue(1f, this.FadeOutNormalizedTime, this._timeSinceSpawn / this._timeTolive, true);
			spritebatch.Draw(this._texture.Value, settings.AnchorPosition + this.LocalPosition, new Rectangle?(this._frame), color, this.Rotation, this._origin, this.Scale, 0, 0f);
		}

		// Token: 0x04005043 RID: 20547
		public float FadeInNormalizedTime;

		// Token: 0x04005044 RID: 20548
		public float FadeOutNormalizedTime = 1f;

		// Token: 0x04005045 RID: 20549
		public Color ColorTint = Color.White;

		// Token: 0x04005046 RID: 20550
		private float _timeTolive;

		// Token: 0x04005047 RID: 20551
		private float _timeSinceSpawn;
	}
}
