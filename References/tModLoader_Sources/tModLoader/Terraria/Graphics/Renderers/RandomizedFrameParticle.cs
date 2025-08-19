using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x0200045D RID: 1117
	public class RandomizedFrameParticle : ABasicParticle
	{
		// Token: 0x060036B5 RID: 14005 RVA: 0x0057E6A4 File Offset: 0x0057C8A4
		public override void FetchFromPool()
		{
			base.FetchFromPool();
			this.FadeInNormalizedTime = 0f;
			this.FadeOutNormalizedTime = 1f;
			this.ColorTint = Color.White;
			this.AnimationFramesAmount = 0;
			this.GameFramesPerAnimationFrame = 0;
			this._timeTolive = 0f;
			this._timeSinceSpawn = 0f;
			this._gameFramesCounted = 0;
		}

		// Token: 0x060036B6 RID: 14006 RVA: 0x0057E703 File Offset: 0x0057C903
		public void SetTypeInfo(int animationFramesAmount, int gameFramesPerAnimationFrame, float timeToLive)
		{
			this._timeTolive = timeToLive;
			this.GameFramesPerAnimationFrame = gameFramesPerAnimationFrame;
			this.AnimationFramesAmount = animationFramesAmount;
			this.RandomizeFrame();
		}

		// Token: 0x060036B7 RID: 14007 RVA: 0x0057E720 File Offset: 0x0057C920
		private void RandomizeFrame()
		{
			this._frame = this._texture.Frame(1, this.AnimationFramesAmount, 0, Main.rand.Next(this.AnimationFramesAmount), 0, 0);
			this._origin = this._frame.Size() / 2f;
		}

		// Token: 0x060036B8 RID: 14008 RVA: 0x0057E774 File Offset: 0x0057C974
		public override void Update(ref ParticleRendererSettings settings)
		{
			base.Update(ref settings);
			this._timeSinceSpawn += 1f;
			if (this._timeSinceSpawn >= this._timeTolive)
			{
				base.ShouldBeRemovedFromRenderer = true;
			}
			int num = this._gameFramesCounted + 1;
			this._gameFramesCounted = num;
			if (num >= this.GameFramesPerAnimationFrame)
			{
				this._gameFramesCounted = 0;
				this.RandomizeFrame();
			}
		}

		// Token: 0x060036B9 RID: 14009 RVA: 0x0057E7D8 File Offset: 0x0057C9D8
		public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			Color color = this.ColorTint * Utils.GetLerpValue(0f, this.FadeInNormalizedTime, this._timeSinceSpawn / this._timeTolive, true) * Utils.GetLerpValue(1f, this.FadeOutNormalizedTime, this._timeSinceSpawn / this._timeTolive, true);
			spritebatch.Draw(this._texture.Value, settings.AnchorPosition + this.LocalPosition, new Rectangle?(this._frame), color, this.Rotation, this._origin, this.Scale, 0, 0f);
		}

		// Token: 0x04005085 RID: 20613
		public float FadeInNormalizedTime;

		// Token: 0x04005086 RID: 20614
		public float FadeOutNormalizedTime = 1f;

		// Token: 0x04005087 RID: 20615
		public Color ColorTint = Color.White;

		// Token: 0x04005088 RID: 20616
		public int AnimationFramesAmount;

		// Token: 0x04005089 RID: 20617
		public int GameFramesPerAnimationFrame;

		// Token: 0x0400508A RID: 20618
		private float _timeTolive;

		// Token: 0x0400508B RID: 20619
		private float _timeSinceSpawn;

		// Token: 0x0400508C RID: 20620
		private int _gameFramesCounted;
	}
}
