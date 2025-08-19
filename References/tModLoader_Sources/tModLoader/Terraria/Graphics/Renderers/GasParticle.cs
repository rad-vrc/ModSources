using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000450 RID: 1104
	public class GasParticle : ABasicParticle
	{
		// Token: 0x0600366F RID: 13935 RVA: 0x0057BF9C File Offset: 0x0057A19C
		public override void FetchFromPool()
		{
			base.FetchFromPool();
			this.ColorTint = Color.Transparent;
			this._timeSinceSpawn = 0f;
			this.Opacity = 0f;
			this.FadeInNormalizedTime = 0.25f;
			this.FadeOutNormalizedTime = 0.75f;
			this.TimeToLive = 80f;
			this._internalIndentifier = Main.rand.Next(255);
			this.SlowdownScalar = 0.95f;
			this.LightColorTint = Color.Transparent;
			this.InitialScale = 1f;
		}

		// Token: 0x06003670 RID: 13936 RVA: 0x0057C028 File Offset: 0x0057A228
		public override void Update(ref ParticleRendererSettings settings)
		{
			base.Update(ref settings);
			this._timeSinceSpawn += 1f;
			float fromValue = this._timeSinceSpawn / this.TimeToLive;
			this.Scale = Vector2.One * this.InitialScale * Utils.Remap(fromValue, 0f, 0.95f, 1f, 1.3f, true);
			this.Opacity = MathHelper.Clamp(Utils.Remap(fromValue, 0f, this.FadeInNormalizedTime, 0f, 1f, true) * Utils.Remap(fromValue, this.FadeOutNormalizedTime, 1f, 1f, 0f, true), 0f, 1f) * 0.85f;
			this.Rotation = (float)this._internalIndentifier * 0.4002029f + this._timeSinceSpawn * 6.2831855f / 480f * 0.5f;
			this.Velocity *= this.SlowdownScalar;
			if (this.LightColorTint != Color.Transparent)
			{
				Color color = this.LightColorTint * this.Opacity;
				Lighting.AddLight(this.LocalPosition, (float)color.R / 255f, (float)color.G / 255f, (float)color.B / 255f);
			}
			if (this._timeSinceSpawn >= this.TimeToLive)
			{
				base.ShouldBeRemovedFromRenderer = true;
			}
		}

		// Token: 0x06003671 RID: 13937 RVA: 0x0057C198 File Offset: 0x0057A398
		public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			Main.instance.LoadProjectile(1007);
			Texture2D value = TextureAssets.Projectile[1007].Value;
			Vector2 origin;
			origin..ctor((float)(value.Width / 2), (float)(value.Height / 2));
			Vector2 position = settings.AnchorPosition + this.LocalPosition;
			Color color = Color.Lerp(Lighting.GetColor(this.LocalPosition.ToTileCoordinates()), this.ColorTint, 0.2f) * this.Opacity;
			Vector2 scale = this.Scale;
			spritebatch.Draw(value, position, new Rectangle?(value.Frame(1, 1, 0, 0, 0, 0)), color, this.Rotation, origin, scale, 0, 0f);
			spritebatch.Draw(value, position, new Rectangle?(value.Frame(1, 1, 0, 0, 0, 0)), color * 0.25f, this.Rotation, origin, scale * (1f + this.Opacity * 1.5f), 0, 0f);
		}

		// Token: 0x0400504D RID: 20557
		public float FadeInNormalizedTime = 0.25f;

		// Token: 0x0400504E RID: 20558
		public float FadeOutNormalizedTime = 0.75f;

		// Token: 0x0400504F RID: 20559
		public float TimeToLive = 80f;

		// Token: 0x04005050 RID: 20560
		public Color ColorTint;

		// Token: 0x04005051 RID: 20561
		public float Opacity;

		// Token: 0x04005052 RID: 20562
		public float AdditiveAmount = 1f;

		// Token: 0x04005053 RID: 20563
		public float FadeInEnd = 20f;

		// Token: 0x04005054 RID: 20564
		public float FadeOutStart = 30f;

		// Token: 0x04005055 RID: 20565
		public float FadeOutEnd = 45f;

		// Token: 0x04005056 RID: 20566
		public float SlowdownScalar = 0.95f;

		// Token: 0x04005057 RID: 20567
		private float _timeSinceSpawn;

		// Token: 0x04005058 RID: 20568
		public Color LightColorTint;

		// Token: 0x04005059 RID: 20569
		private int _internalIndentifier;

		// Token: 0x0400505A RID: 20570
		public float InitialScale = 1f;
	}
}
