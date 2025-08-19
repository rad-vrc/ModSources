using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x02000326 RID: 806
	public class VortexSky : CustomSky
	{
		// Token: 0x0600248A RID: 9354 RVA: 0x0055BF54 File Offset: 0x0055A154
		public override void OnLoad()
		{
			this._planetTexture = Main.Assets.Request<Texture2D>("Images/Misc/VortexSky/Planet", 1);
			this._bgTexture = Main.Assets.Request<Texture2D>("Images/Misc/VortexSky/Background", 1);
			this._boltTexture = Main.Assets.Request<Texture2D>("Images/Misc/VortexSky/Bolt", 1);
			this._flashTexture = Main.Assets.Request<Texture2D>("Images/Misc/VortexSky/Flash", 1);
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x0055BFBC File Offset: 0x0055A1BC
		public override void Update(GameTime gameTime)
		{
			if (this._isActive)
			{
				this._fadeOpacity = Math.Min(1f, 0.01f + this._fadeOpacity);
			}
			else
			{
				this._fadeOpacity = Math.Max(0f, this._fadeOpacity - 0.01f);
			}
			if (this._ticksUntilNextBolt <= 0)
			{
				this._ticksUntilNextBolt = this._random.Next(1, 5);
				int num = 0;
				while (this._bolts[num].IsAlive && num != this._bolts.Length - 1)
				{
					num++;
				}
				this._bolts[num].IsAlive = true;
				this._bolts[num].Position.X = this._random.NextFloat() * ((float)Main.maxTilesX * 16f + 4000f) - 2000f;
				this._bolts[num].Position.Y = this._random.NextFloat() * 500f;
				this._bolts[num].Depth = this._random.NextFloat() * 8f + 2f;
				this._bolts[num].Life = 30;
			}
			this._ticksUntilNextBolt--;
			for (int i = 0; i < this._bolts.Length; i++)
			{
				if (this._bolts[i].IsAlive)
				{
					VortexSky.Bolt[] bolts = this._bolts;
					int num2 = i;
					bolts[num2].Life = bolts[num2].Life - 1;
					if (this._bolts[i].Life <= 0)
					{
						this._bolts[i].IsAlive = false;
					}
				}
			}
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x0055C170 File Offset: 0x0055A370
		public override Color OnTileColor(Color inColor)
		{
			return new Color(Vector4.Lerp(inColor.ToVector4(), Vector4.One, this._fadeOpacity * 0.5f));
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x0055C194 File Offset: 0x0055A394
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (maxDepth >= 3.4028235E+38f && minDepth < 3.4028235E+38f)
			{
				spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * this._fadeOpacity);
				spriteBatch.Draw(this._bgTexture.Value, new Rectangle(0, Math.Max(0, (int)((Main.worldSurface * 16.0 - (double)Main.screenPosition.Y - 2400.0) * 0.10000000149011612)), Main.screenWidth, Main.screenHeight), Color.White * Math.Min(1f, (Main.screenPosition.Y - 800f) / 1000f) * this._fadeOpacity);
				Vector2 value = new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
				Vector2 value2 = 0.01f * (new Vector2((float)Main.maxTilesX * 8f, (float)Main.worldSurface / 2f) - Main.screenPosition);
				spriteBatch.Draw(this._planetTexture.Value, value + new Vector2(-200f, -200f) + value2, null, Color.White * 0.9f * this._fadeOpacity, 0f, new Vector2((float)(this._planetTexture.Width() >> 1), (float)(this._planetTexture.Height() >> 1)), 1f, SpriteEffects.None, 1f);
			}
			float scale = Math.Min(1f, (Main.screenPosition.Y - 1000f) / 1000f);
			Vector2 value3 = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
			Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
			for (int i = 0; i < this._bolts.Length; i++)
			{
				if (this._bolts[i].IsAlive && this._bolts[i].Depth > minDepth && this._bolts[i].Depth < maxDepth)
				{
					Vector2 vector = new Vector2(1f / this._bolts[i].Depth, 0.9f / this._bolts[i].Depth);
					Vector2 vector2 = (this._bolts[i].Position - value3) * vector + value3 - Main.screenPosition;
					if (rectangle.Contains((int)vector2.X, (int)vector2.Y))
					{
						Texture2D value4 = this._boltTexture.Value;
						int life = this._bolts[i].Life;
						if (life > 26 && life % 2 == 0)
						{
							value4 = this._flashTexture.Value;
						}
						float scale2 = (float)life / 30f;
						spriteBatch.Draw(value4, vector2, null, Color.White * scale * scale2 * this._fadeOpacity, 0f, Vector2.Zero, vector.X * 5f, SpriteEffects.None, 0f);
					}
				}
			}
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x0055C517 File Offset: 0x0055A717
		public override float GetCloudAlpha()
		{
			return (1f - this._fadeOpacity) * 0.3f + 0.7f;
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x0055C534 File Offset: 0x0055A734
		public override void Activate(Vector2 position, params object[] args)
		{
			this._fadeOpacity = 0.002f;
			this._isActive = true;
			this._bolts = new VortexSky.Bolt[500];
			for (int i = 0; i < this._bolts.Length; i++)
			{
				this._bolts[i].IsAlive = false;
			}
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x0055C588 File Offset: 0x0055A788
		public override void Deactivate(params object[] args)
		{
			this._isActive = false;
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x0055C588 File Offset: 0x0055A788
		public override void Reset()
		{
			this._isActive = false;
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x0055C591 File Offset: 0x0055A791
		public override bool IsActive()
		{
			return this._isActive || this._fadeOpacity > 0.001f;
		}

		// Token: 0x040048AA RID: 18602
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x040048AB RID: 18603
		private Asset<Texture2D> _planetTexture;

		// Token: 0x040048AC RID: 18604
		private Asset<Texture2D> _bgTexture;

		// Token: 0x040048AD RID: 18605
		private Asset<Texture2D> _boltTexture;

		// Token: 0x040048AE RID: 18606
		private Asset<Texture2D> _flashTexture;

		// Token: 0x040048AF RID: 18607
		private bool _isActive;

		// Token: 0x040048B0 RID: 18608
		private int _ticksUntilNextBolt;

		// Token: 0x040048B1 RID: 18609
		private float _fadeOpacity;

		// Token: 0x040048B2 RID: 18610
		private VortexSky.Bolt[] _bolts;

		// Token: 0x02000709 RID: 1801
		private struct Bolt
		{
			// Token: 0x040062D3 RID: 25299
			public Vector2 Position;

			// Token: 0x040062D4 RID: 25300
			public float Depth;

			// Token: 0x040062D5 RID: 25301
			public int Life;

			// Token: 0x040062D6 RID: 25302
			public bool IsAlive;
		}
	}
}
