using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000128 RID: 296
	public class RandomizedFrameParticle : ABasicParticle
	{
		// Token: 0x06001773 RID: 6003 RVA: 0x004D2C7C File Offset: 0x004D0E7C
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

		// Token: 0x06001774 RID: 6004 RVA: 0x004D2CDB File Offset: 0x004D0EDB
		public void SetTypeInfo(int animationFramesAmount, int gameFramesPerAnimationFrame, float timeToLive)
		{
			this._timeTolive = timeToLive;
			this.GameFramesPerAnimationFrame = gameFramesPerAnimationFrame;
			this.AnimationFramesAmount = animationFramesAmount;
			this.RandomizeFrame();
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x004D2CF8 File Offset: 0x004D0EF8
		private void RandomizeFrame()
		{
			this._frame = this._texture.Frame(1, this.AnimationFramesAmount, 0, Main.rand.Next(this.AnimationFramesAmount), 0, 0);
			this._origin = this._frame.Size() / 2f;
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x004D2D4C File Offset: 0x004D0F4C
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

		// Token: 0x06001777 RID: 6007 RVA: 0x004D2DB0 File Offset: 0x004D0FB0
		public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			Color color = this.ColorTint * Utils.GetLerpValue(0f, this.FadeInNormalizedTime, this._timeSinceSpawn / this._timeTolive, true) * Utils.GetLerpValue(1f, this.FadeOutNormalizedTime, this._timeSinceSpawn / this._timeTolive, true);
			spritebatch.Draw(this._texture.Value, settings.AnchorPosition + this.LocalPosition, new Rectangle?(this._frame), color, this.Rotation, this._origin, this.Scale, SpriteEffects.None, 0f);
		}

		// Token: 0x04001428 RID: 5160
		public float FadeInNormalizedTime;

		// Token: 0x04001429 RID: 5161
		public float FadeOutNormalizedTime = 1f;

		// Token: 0x0400142A RID: 5162
		public Color ColorTint = Color.White;

		// Token: 0x0400142B RID: 5163
		public int AnimationFramesAmount;

		// Token: 0x0400142C RID: 5164
		public int GameFramesPerAnimationFrame;

		// Token: 0x0400142D RID: 5165
		private float _timeTolive;

		// Token: 0x0400142E RID: 5166
		private float _timeSinceSpawn;

		// Token: 0x0400142F RID: 5167
		private int _gameFramesCounted;
	}
}
