using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000456 RID: 1110
	public class LittleFlyingCritterParticle : IPooledParticle, IParticle
	{
		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06003692 RID: 13970 RVA: 0x0057D86F File Offset: 0x0057BA6F
		// (set) Token: 0x06003693 RID: 13971 RVA: 0x0057D877 File Offset: 0x0057BA77
		public bool IsRestingInPool { get; private set; }

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06003694 RID: 13972 RVA: 0x0057D880 File Offset: 0x0057BA80
		// (set) Token: 0x06003695 RID: 13973 RVA: 0x0057D888 File Offset: 0x0057BA88
		public bool ShouldBeRemovedFromRenderer { get; private set; }

		// Token: 0x06003696 RID: 13974 RVA: 0x0057D894 File Offset: 0x0057BA94
		public void Prepare(Vector2 position, int duration)
		{
			this._spawnPosition = position;
			this._localPosition = position + Main.rand.NextVector2Circular(4f, 8f);
			this._neverGoBelowThis = position.Y + 8f;
			this.RandomizeVelocity();
			this._lifeTimeCounted = 0;
			this._lifeTimeTotal = 300 + Main.rand.Next(6) * 60;
		}

		// Token: 0x06003697 RID: 13975 RVA: 0x0057D901 File Offset: 0x0057BB01
		private void RandomizeVelocity()
		{
			this._velocity = Main.rand.NextVector2Circular(1f, 1f);
		}

		// Token: 0x06003698 RID: 13976 RVA: 0x0057D91D File Offset: 0x0057BB1D
		public void RestInPool()
		{
			this.IsRestingInPool = true;
		}

		// Token: 0x06003699 RID: 13977 RVA: 0x0057D926 File Offset: 0x0057BB26
		public virtual void FetchFromPool()
		{
			this.IsRestingInPool = false;
			this.ShouldBeRemovedFromRenderer = false;
		}

		// Token: 0x0600369A RID: 13978 RVA: 0x0057D938 File Offset: 0x0057BB38
		public void Update(ref ParticleRendererSettings settings)
		{
			int num = this._lifeTimeCounted + 1;
			this._lifeTimeCounted = num;
			if (num >= this._lifeTimeTotal)
			{
				this.ShouldBeRemovedFromRenderer = true;
			}
			this._velocity += new Vector2((float)Math.Sign(this._spawnPosition.X - this._localPosition.X) * 0.02f, (float)Math.Sign(this._spawnPosition.Y - this._localPosition.Y) * 0.02f);
			if (this._lifeTimeCounted % 30 == 0 && Main.rand.Next(2) == 0)
			{
				this.RandomizeVelocity();
				if (Main.rand.Next(2) == 0)
				{
					this._velocity /= 2f;
				}
			}
			this._localPosition += this._velocity;
			if (this._localPosition.Y > this._neverGoBelowThis)
			{
				this._localPosition.Y = this._neverGoBelowThis;
				if (this._velocity.Y > 0f)
				{
					this._velocity.Y = this._velocity.Y * -1f;
				}
			}
		}

		// Token: 0x0600369B RID: 13979 RVA: 0x0057DA64 File Offset: 0x0057BC64
		public void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			Vector2 vector = settings.AnchorPosition + this._localPosition;
			if (vector.X < -10f || vector.X > (float)(Main.screenWidth + 10) || vector.Y < -10f || vector.Y > (float)(Main.screenHeight + 10))
			{
				this.ShouldBeRemovedFromRenderer = true;
				return;
			}
			Texture2D value = TextureAssets.Extra[262].Value;
			int frameY = this._lifeTimeCounted % 6 / 3;
			Rectangle value2 = value.Frame(1, 2, 0, frameY, 0, 0);
			Vector2 origin;
			origin..ctor((float)((this._velocity.X <= 0f) ? 1 : 3), 3f);
			float num = Utils.Remap((float)this._lifeTimeCounted, 0f, 90f, 0f, 1f, true) * Utils.Remap((float)this._lifeTimeCounted, (float)(this._lifeTimeTotal - 90), (float)this._lifeTimeTotal, 1f, 0f, true);
			spritebatch.Draw(value, settings.AnchorPosition + this._localPosition, new Rectangle?(value2), Lighting.GetColor(this._localPosition.ToTileCoordinates()) * num, 0f, origin, 1f, (this._velocity.X > 0f) ? 1 : 0, 0f);
		}

		// Token: 0x04005067 RID: 20583
		private int _lifeTimeCounted;

		// Token: 0x04005068 RID: 20584
		private int _lifeTimeTotal;

		// Token: 0x04005069 RID: 20585
		private Vector2 _spawnPosition;

		// Token: 0x0400506A RID: 20586
		private Vector2 _localPosition;

		// Token: 0x0400506B RID: 20587
		private Vector2 _velocity;

		// Token: 0x0400506C RID: 20588
		private float _neverGoBelowThis;
	}
}
