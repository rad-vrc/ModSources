using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x02000329 RID: 809
	public class StardustSky : CustomSky
	{
		// Token: 0x060024A8 RID: 9384 RVA: 0x0055D534 File Offset: 0x0055B734
		public override void OnLoad()
		{
			this._planetTexture = Main.Assets.Request<Texture2D>("Images/Misc/StarDustSky/Planet", 1);
			this._bgTexture = Main.Assets.Request<Texture2D>("Images/Misc/StarDustSky/Background", 1);
			this._starTextures = new Asset<Texture2D>[2];
			for (int i = 0; i < this._starTextures.Length; i++)
			{
				this._starTextures[i] = Main.Assets.Request<Texture2D>("Images/Misc/StarDustSky/Star " + i, 1);
			}
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x0055D5B0 File Offset: 0x0055B7B0
		public override void Update(GameTime gameTime)
		{
			if (this._isActive)
			{
				this._fadeOpacity = Math.Min(1f, 0.01f + this._fadeOpacity);
				return;
			}
			this._fadeOpacity = Math.Max(0f, this._fadeOpacity - 0.01f);
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x0055D5FE File Offset: 0x0055B7FE
		public override Color OnTileColor(Color inColor)
		{
			return new Color(Vector4.Lerp(inColor.ToVector4(), Vector4.One, this._fadeOpacity * 0.5f));
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x0055D624 File Offset: 0x0055B824
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (maxDepth >= 3.4028235E+38f && minDepth < 3.4028235E+38f)
			{
				spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * this._fadeOpacity);
				spriteBatch.Draw(this._bgTexture.Value, new Rectangle(0, Math.Max(0, (int)((Main.worldSurface * 16.0 - (double)Main.screenPosition.Y - 2400.0) * 0.10000000149011612)), Main.screenWidth, Main.screenHeight), Color.White * Math.Min(1f, (Main.screenPosition.Y - 800f) / 1000f * this._fadeOpacity));
				Vector2 value = new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
				Vector2 value2 = 0.01f * (new Vector2((float)Main.maxTilesX * 8f, (float)Main.worldSurface / 2f) - Main.screenPosition);
				spriteBatch.Draw(this._planetTexture.Value, value + new Vector2(-200f, -200f) + value2, null, Color.White * 0.9f * this._fadeOpacity, 0f, new Vector2((float)(this._planetTexture.Width() >> 1), (float)(this._planetTexture.Height() >> 1)), 1f, SpriteEffects.None, 1f);
			}
			int num = -1;
			int num2 = 0;
			for (int i = 0; i < this._stars.Length; i++)
			{
				float depth = this._stars[i].Depth;
				if (num == -1 && depth < maxDepth)
				{
					num = i;
				}
				if (depth <= minDepth)
				{
					break;
				}
				num2 = i;
			}
			if (num == -1)
			{
				return;
			}
			float scale = Math.Min(1f, (Main.screenPosition.Y - 1000f) / 1000f);
			Vector2 value3 = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
			Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
			for (int j = num; j < num2; j++)
			{
				Vector2 vector = new Vector2(1f / this._stars[j].Depth, 1.1f / this._stars[j].Depth);
				Vector2 vector2 = (this._stars[j].Position - value3) * vector + value3 - Main.screenPosition;
				if (rectangle.Contains((int)vector2.X, (int)vector2.Y))
				{
					float num3 = (float)Math.Sin((double)(this._stars[j].AlphaFrequency * Main.GlobalTimeWrappedHourly + this._stars[j].SinOffset)) * this._stars[j].AlphaAmplitude + this._stars[j].AlphaAmplitude;
					float num4 = (float)Math.Sin((double)(this._stars[j].AlphaFrequency * Main.GlobalTimeWrappedHourly * 5f + this._stars[j].SinOffset)) * 0.1f - 0.1f;
					num3 = MathHelper.Clamp(num3, 0f, 1f);
					Texture2D value4 = this._starTextures[this._stars[j].TextureIndex].Value;
					spriteBatch.Draw(value4, vector2, null, Color.White * scale * num3 * 0.8f * (1f - num4) * this._fadeOpacity, 0f, new Vector2((float)(value4.Width >> 1), (float)(value4.Height >> 1)), (vector.X * 0.5f + 0.5f) * (num3 * 0.3f + 0.7f), SpriteEffects.None, 0f);
				}
			}
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x0055DA6C File Offset: 0x0055BC6C
		public override float GetCloudAlpha()
		{
			return (1f - this._fadeOpacity) * 0.3f + 0.7f;
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x0055DA88 File Offset: 0x0055BC88
		public override void Activate(Vector2 position, params object[] args)
		{
			this._fadeOpacity = 0.002f;
			this._isActive = true;
			int num = 200;
			int num2 = 10;
			this._stars = new StardustSky.Star[num * num2];
			int num3 = 0;
			for (int i = 0; i < num; i++)
			{
				float num4 = (float)i / (float)num;
				for (int j = 0; j < num2; j++)
				{
					float num5 = (float)j / (float)num2;
					this._stars[num3].Position.X = num4 * (float)Main.maxTilesX * 16f;
					this._stars[num3].Position.Y = num5 * ((float)Main.worldSurface * 16f + 2000f) - 1000f;
					this._stars[num3].Depth = this._random.NextFloat() * 8f + 1.5f;
					this._stars[num3].TextureIndex = this._random.Next(this._starTextures.Length);
					this._stars[num3].SinOffset = this._random.NextFloat() * 6.28f;
					this._stars[num3].AlphaAmplitude = this._random.NextFloat() * 5f;
					this._stars[num3].AlphaFrequency = this._random.NextFloat() + 1f;
					num3++;
				}
			}
			Array.Sort<StardustSky.Star>(this._stars, new Comparison<StardustSky.Star>(this.SortMethod));
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x0055DC1A File Offset: 0x0055BE1A
		private int SortMethod(StardustSky.Star meteor1, StardustSky.Star meteor2)
		{
			return meteor2.Depth.CompareTo(meteor1.Depth);
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x0055DC2E File Offset: 0x0055BE2E
		public override void Deactivate(params object[] args)
		{
			this._isActive = false;
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x0055DC2E File Offset: 0x0055BE2E
		public override void Reset()
		{
			this._isActive = false;
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x0055DC37 File Offset: 0x0055BE37
		public override bool IsActive()
		{
			return this._isActive || this._fadeOpacity > 0.001f;
		}

		// Token: 0x040048C0 RID: 18624
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x040048C1 RID: 18625
		private Asset<Texture2D> _planetTexture;

		// Token: 0x040048C2 RID: 18626
		private Asset<Texture2D> _bgTexture;

		// Token: 0x040048C3 RID: 18627
		private Asset<Texture2D>[] _starTextures;

		// Token: 0x040048C4 RID: 18628
		private bool _isActive;

		// Token: 0x040048C5 RID: 18629
		private StardustSky.Star[] _stars;

		// Token: 0x040048C6 RID: 18630
		private float _fadeOpacity;

		// Token: 0x0200070C RID: 1804
		private struct Star
		{
			// Token: 0x040062E6 RID: 25318
			public Vector2 Position;

			// Token: 0x040062E7 RID: 25319
			public float Depth;

			// Token: 0x040062E8 RID: 25320
			public int TextureIndex;

			// Token: 0x040062E9 RID: 25321
			public float SinOffset;

			// Token: 0x040062EA RID: 25322
			public float AlphaFrequency;

			// Token: 0x040062EB RID: 25323
			public float AlphaAmplitude;
		}
	}
}
