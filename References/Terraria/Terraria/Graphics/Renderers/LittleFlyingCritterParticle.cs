using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000122 RID: 290
	public class LittleFlyingCritterParticle : IPooledParticle, IParticle
	{
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06001744 RID: 5956 RVA: 0x004D1F6E File Offset: 0x004D016E
		// (set) Token: 0x06001745 RID: 5957 RVA: 0x004D1F76 File Offset: 0x004D0176
		public bool IsRestingInPool { get; private set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06001746 RID: 5958 RVA: 0x004D1F7F File Offset: 0x004D017F
		// (set) Token: 0x06001747 RID: 5959 RVA: 0x004D1F87 File Offset: 0x004D0187
		public bool ShouldBeRemovedFromRenderer { get; private set; }

		// Token: 0x06001749 RID: 5961 RVA: 0x004D1F90 File Offset: 0x004D0190
		public void Prepare(Vector2 position, int duration)
		{
			this._spawnPosition = position;
			this._localPosition = position + Main.rand.NextVector2Circular(4f, 8f);
			this._neverGoBelowThis = position.Y + 8f;
			this.RandomizeVelocity();
			this._lifeTimeCounted = 0;
			this._lifeTimeTotal = 300 + Main.rand.Next(6) * 60;
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x004D1FFD File Offset: 0x004D01FD
		private void RandomizeVelocity()
		{
			this._velocity = Main.rand.NextVector2Circular(1f, 1f);
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x004D2019 File Offset: 0x004D0219
		public void RestInPool()
		{
			this.IsRestingInPool = true;
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x004D2022 File Offset: 0x004D0222
		public virtual void FetchFromPool()
		{
			this.IsRestingInPool = false;
			this.ShouldBeRemovedFromRenderer = false;
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x004D2034 File Offset: 0x004D0234
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

		// Token: 0x0600174E RID: 5966 RVA: 0x004D2160 File Offset: 0x004D0360
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
			Vector2 origin = new Vector2((float)((this._velocity.X > 0f) ? 3 : 1), 3f);
			float scale = Utils.Remap((float)this._lifeTimeCounted, 0f, 90f, 0f, 1f, true) * Utils.Remap((float)this._lifeTimeCounted, (float)(this._lifeTimeTotal - 90), (float)this._lifeTimeTotal, 1f, 0f, true);
			spritebatch.Draw(value, settings.AnchorPosition + this._localPosition, new Rectangle?(value2), Lighting.GetColor(this._localPosition.ToTileCoordinates()) * scale, 0f, origin, 1f, (this._velocity.X > 0f) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
		}

		// Token: 0x040013F5 RID: 5109
		private int _lifeTimeCounted;

		// Token: 0x040013F6 RID: 5110
		private int _lifeTimeTotal;

		// Token: 0x040013F9 RID: 5113
		private Vector2 _spawnPosition;

		// Token: 0x040013FA RID: 5114
		private Vector2 _localPosition;

		// Token: 0x040013FB RID: 5115
		private Vector2 _velocity;

		// Token: 0x040013FC RID: 5116
		private float _neverGoBelowThis;
	}
}
