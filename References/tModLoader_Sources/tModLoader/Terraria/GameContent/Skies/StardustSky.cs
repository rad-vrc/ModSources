using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x0200056F RID: 1391
	public class StardustSky : CustomSky
	{
		// Token: 0x060041A0 RID: 16800 RVA: 0x005E8B28 File Offset: 0x005E6D28
		public override void OnLoad()
		{
			this._planetTexture = Main.Assets.Request<Texture2D>("Images/Misc/StarDustSky/Planet");
			this._bgTexture = Main.Assets.Request<Texture2D>("Images/Misc/StarDustSky/Background");
			this._starTextures = new Asset<Texture2D>[2];
			for (int i = 0; i < this._starTextures.Length; i++)
			{
				this._starTextures[i] = Main.Assets.Request<Texture2D>("Images/Misc/StarDustSky/Star " + i.ToString());
			}
		}

		// Token: 0x060041A1 RID: 16801 RVA: 0x005E8BA4 File Offset: 0x005E6DA4
		public override void Update(GameTime gameTime)
		{
			if (this._isActive)
			{
				this._fadeOpacity = Math.Min(1f, 0.01f + this._fadeOpacity);
				return;
			}
			this._fadeOpacity = Math.Max(0f, this._fadeOpacity - 0.01f);
		}

		// Token: 0x060041A2 RID: 16802 RVA: 0x005E8BF2 File Offset: 0x005E6DF2
		public override Color OnTileColor(Color inColor)
		{
			return new Color(Vector4.Lerp(inColor.ToVector4(), Vector4.One, this._fadeOpacity * 0.5f));
		}

		// Token: 0x060041A3 RID: 16803 RVA: 0x005E8C18 File Offset: 0x005E6E18
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (maxDepth >= 3.4028235E+38f && minDepth < 3.4028235E+38f)
			{
				spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * this._fadeOpacity);
				spriteBatch.Draw(this._bgTexture.Value, new Rectangle(0, Math.Max(0, (int)((Main.worldSurface * 16.0 - (double)Main.screenPosition.Y - 2400.0) * 0.10000000149011612)), Main.screenWidth, Main.screenHeight), Color.White * Math.Min(1f, (Main.screenPosition.Y - 800f) / 1000f * this._fadeOpacity));
				Vector2 vector;
				vector..ctor((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
				Vector2 vector2 = 0.01f * (new Vector2((float)Main.maxTilesX * 8f, (float)Main.worldSurface / 2f) - Main.screenPosition);
				spriteBatch.Draw(this._planetTexture.Value, vector + new Vector2(-200f, -200f) + vector2, null, Color.White * 0.9f * this._fadeOpacity, 0f, new Vector2((float)(this._planetTexture.Width() >> 1), (float)(this._planetTexture.Height() >> 1)), 1f, 0, 1f);
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
			float num3 = Math.Min(1f, (Main.screenPosition.Y - 1000f) / 1000f);
			Vector2 vector3 = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
			Rectangle rectangle;
			rectangle..ctor(-1000, -1000, 4000, 4000);
			for (int j = num; j < num2; j++)
			{
				Vector2 vector4;
				vector4..ctor(1f / this._stars[j].Depth, 1.1f / this._stars[j].Depth);
				Vector2 position = (this._stars[j].Position - vector3) * vector4 + vector3 - Main.screenPosition;
				if (rectangle.Contains((int)position.X, (int)position.Y))
				{
					float value = (float)Math.Sin((double)(this._stars[j].AlphaFrequency * Main.GlobalTimeWrappedHourly + this._stars[j].SinOffset)) * this._stars[j].AlphaAmplitude + this._stars[j].AlphaAmplitude;
					float num4 = (float)Math.Sin((double)(this._stars[j].AlphaFrequency * Main.GlobalTimeWrappedHourly * 5f + this._stars[j].SinOffset)) * 0.1f - 0.1f;
					value = MathHelper.Clamp(value, 0f, 1f);
					Texture2D value2 = this._starTextures[this._stars[j].TextureIndex].Value;
					spriteBatch.Draw(value2, position, null, Color.White * num3 * value * 0.8f * (1f - num4) * this._fadeOpacity, 0f, new Vector2((float)(value2.Width >> 1), (float)(value2.Height >> 1)), (vector4.X * 0.5f + 0.5f) * (value * 0.3f + 0.7f), 0, 0f);
				}
			}
		}

		// Token: 0x060041A4 RID: 16804 RVA: 0x005E9060 File Offset: 0x005E7260
		public override float GetCloudAlpha()
		{
			return (1f - this._fadeOpacity) * 0.3f + 0.7f;
		}

		// Token: 0x060041A5 RID: 16805 RVA: 0x005E907C File Offset: 0x005E727C
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

		// Token: 0x060041A6 RID: 16806 RVA: 0x005E920E File Offset: 0x005E740E
		private int SortMethod(StardustSky.Star meteor1, StardustSky.Star meteor2)
		{
			return meteor2.Depth.CompareTo(meteor1.Depth);
		}

		// Token: 0x060041A7 RID: 16807 RVA: 0x005E9222 File Offset: 0x005E7422
		public override void Deactivate(params object[] args)
		{
			this._isActive = false;
		}

		// Token: 0x060041A8 RID: 16808 RVA: 0x005E922B File Offset: 0x005E742B
		public override void Reset()
		{
			this._isActive = false;
		}

		// Token: 0x060041A9 RID: 16809 RVA: 0x005E9234 File Offset: 0x005E7434
		public override bool IsActive()
		{
			return this._isActive || this._fadeOpacity > 0.001f;
		}

		// Token: 0x040058EA RID: 22762
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x040058EB RID: 22763
		private Asset<Texture2D> _planetTexture;

		// Token: 0x040058EC RID: 22764
		private Asset<Texture2D> _bgTexture;

		// Token: 0x040058ED RID: 22765
		private Asset<Texture2D>[] _starTextures;

		// Token: 0x040058EE RID: 22766
		private bool _isActive;

		// Token: 0x040058EF RID: 22767
		private StardustSky.Star[] _stars;

		// Token: 0x040058F0 RID: 22768
		private float _fadeOpacity;

		// Token: 0x02000C4E RID: 3150
		private struct Star
		{
			// Token: 0x0400790A RID: 30986
			public Vector2 Position;

			// Token: 0x0400790B RID: 30987
			public float Depth;

			// Token: 0x0400790C RID: 30988
			public int TextureIndex;

			// Token: 0x0400790D RID: 30989
			public float SinOffset;

			// Token: 0x0400790E RID: 30990
			public float AlphaFrequency;

			// Token: 0x0400790F RID: 30991
			public float AlphaAmplitude;
		}
	}
}
