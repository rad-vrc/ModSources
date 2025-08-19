using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000129 RID: 297
	public class GasParticle : ABasicParticle
	{
		// Token: 0x06001779 RID: 6009 RVA: 0x004D2E70 File Offset: 0x004D1070
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

		// Token: 0x0600177A RID: 6010 RVA: 0x004D2EFC File Offset: 0x004D10FC
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

		// Token: 0x0600177B RID: 6011 RVA: 0x004D306C File Offset: 0x004D126C
		public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			Main.instance.LoadProjectile(1007);
			Texture2D value = TextureAssets.Projectile[1007].Value;
			Vector2 origin = new Vector2((float)(value.Width / 2), (float)(value.Height / 2));
			Vector2 position = settings.AnchorPosition + this.LocalPosition;
			Color color = Color.Lerp(Lighting.GetColor(this.LocalPosition.ToTileCoordinates()), this.ColorTint, 0.2f) * this.Opacity;
			Vector2 scale = this.Scale;
			spritebatch.Draw(value, position, new Rectangle?(value.Frame(1, 1, 0, 0, 0, 0)), color, this.Rotation, origin, scale, SpriteEffects.None, 0f);
			spritebatch.Draw(value, position, new Rectangle?(value.Frame(1, 1, 0, 0, 0, 0)), color * 0.25f, this.Rotation, origin, scale * (1f + this.Opacity * 1.5f), SpriteEffects.None, 0f);
		}

		// Token: 0x04001430 RID: 5168
		public float FadeInNormalizedTime = 0.25f;

		// Token: 0x04001431 RID: 5169
		public float FadeOutNormalizedTime = 0.75f;

		// Token: 0x04001432 RID: 5170
		public float TimeToLive = 80f;

		// Token: 0x04001433 RID: 5171
		public Color ColorTint;

		// Token: 0x04001434 RID: 5172
		public float Opacity;

		// Token: 0x04001435 RID: 5173
		public float AdditiveAmount = 1f;

		// Token: 0x04001436 RID: 5174
		public float FadeInEnd = 20f;

		// Token: 0x04001437 RID: 5175
		public float FadeOutStart = 30f;

		// Token: 0x04001438 RID: 5176
		public float FadeOutEnd = 45f;

		// Token: 0x04001439 RID: 5177
		public float SlowdownScalar = 0.95f;

		// Token: 0x0400143A RID: 5178
		private float _timeSinceSpawn;

		// Token: 0x0400143B RID: 5179
		public Color LightColorTint;

		// Token: 0x0400143C RID: 5180
		private int _internalIndentifier;

		// Token: 0x0400143D RID: 5181
		public float InitialScale = 1f;
	}
}
