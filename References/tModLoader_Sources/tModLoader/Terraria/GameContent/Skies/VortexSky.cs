using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x02000570 RID: 1392
	public class VortexSky : CustomSky
	{
		// Token: 0x060041AB RID: 16811 RVA: 0x005E9260 File Offset: 0x005E7460
		public override void OnLoad()
		{
			this._planetTexture = Main.Assets.Request<Texture2D>("Images/Misc/VortexSky/Planet");
			this._bgTexture = Main.Assets.Request<Texture2D>("Images/Misc/VortexSky/Background");
			this._boltTexture = Main.Assets.Request<Texture2D>("Images/Misc/VortexSky/Bolt");
			this._flashTexture = Main.Assets.Request<Texture2D>("Images/Misc/VortexSky/Flash");
		}

		// Token: 0x060041AC RID: 16812 RVA: 0x005E92C4 File Offset: 0x005E74C4
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
				int i = 0;
				while (this._bolts[i].IsAlive && i != this._bolts.Length - 1)
				{
					i++;
				}
				this._bolts[i].IsAlive = true;
				this._bolts[i].Position.X = this._random.NextFloat() * ((float)Main.maxTilesX * 16f + 4000f) - 2000f;
				this._bolts[i].Position.Y = this._random.NextFloat() * 500f;
				this._bolts[i].Depth = this._random.NextFloat() * 8f + 2f;
				this._bolts[i].Life = 30;
			}
			this._ticksUntilNextBolt--;
			for (int j = 0; j < this._bolts.Length; j++)
			{
				if (this._bolts[j].IsAlive)
				{
					VortexSky.Bolt[] bolts = this._bolts;
					int num = j;
					bolts[num].Life = bolts[num].Life - 1;
					if (this._bolts[j].Life <= 0)
					{
						this._bolts[j].IsAlive = false;
					}
				}
			}
		}

		// Token: 0x060041AD RID: 16813 RVA: 0x005E9478 File Offset: 0x005E7678
		public override Color OnTileColor(Color inColor)
		{
			return new Color(Vector4.Lerp(inColor.ToVector4(), Vector4.One, this._fadeOpacity * 0.5f));
		}

		// Token: 0x060041AE RID: 16814 RVA: 0x005E949C File Offset: 0x005E769C
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (maxDepth >= 3.4028235E+38f && minDepth < 3.4028235E+38f)
			{
				spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * this._fadeOpacity);
				spriteBatch.Draw(this._bgTexture.Value, new Rectangle(0, Math.Max(0, (int)((Main.worldSurface * 16.0 - (double)Main.screenPosition.Y - 2400.0) * 0.10000000149011612)), Main.screenWidth, Main.screenHeight), Color.White * Math.Min(1f, (Main.screenPosition.Y - 800f) / 1000f) * this._fadeOpacity);
				Vector2 vector;
				vector..ctor((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
				Vector2 vector2 = 0.01f * (new Vector2((float)Main.maxTilesX * 8f, (float)Main.worldSurface / 2f) - Main.screenPosition);
				spriteBatch.Draw(this._planetTexture.Value, vector + new Vector2(-200f, -200f) + vector2, null, Color.White * 0.9f * this._fadeOpacity, 0f, new Vector2((float)(this._planetTexture.Width() >> 1), (float)(this._planetTexture.Height() >> 1)), 1f, 0, 1f);
			}
			float num = Math.Min(1f, (Main.screenPosition.Y - 1000f) / 1000f);
			Vector2 vector3 = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
			Rectangle rectangle;
			rectangle..ctor(-1000, -1000, 4000, 4000);
			for (int i = 0; i < this._bolts.Length; i++)
			{
				if (this._bolts[i].IsAlive && this._bolts[i].Depth > minDepth && this._bolts[i].Depth < maxDepth)
				{
					Vector2 vector4;
					vector4..ctor(1f / this._bolts[i].Depth, 0.9f / this._bolts[i].Depth);
					Vector2 position = (this._bolts[i].Position - vector3) * vector4 + vector3 - Main.screenPosition;
					if (rectangle.Contains((int)position.X, (int)position.Y))
					{
						Texture2D value = this._boltTexture.Value;
						int life = this._bolts[i].Life;
						if (life > 26 && life % 2 == 0)
						{
							value = this._flashTexture.Value;
						}
						float num2 = (float)life / 30f;
						spriteBatch.Draw(value, position, null, Color.White * num * num2 * this._fadeOpacity, 0f, Vector2.Zero, vector4.X * 5f, 0, 0f);
					}
				}
			}
		}

		// Token: 0x060041AF RID: 16815 RVA: 0x005E981F File Offset: 0x005E7A1F
		public override float GetCloudAlpha()
		{
			return (1f - this._fadeOpacity) * 0.3f + 0.7f;
		}

		// Token: 0x060041B0 RID: 16816 RVA: 0x005E983C File Offset: 0x005E7A3C
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

		// Token: 0x060041B1 RID: 16817 RVA: 0x005E9890 File Offset: 0x005E7A90
		public override void Deactivate(params object[] args)
		{
			this._isActive = false;
		}

		// Token: 0x060041B2 RID: 16818 RVA: 0x005E9899 File Offset: 0x005E7A99
		public override void Reset()
		{
			this._isActive = false;
		}

		// Token: 0x060041B3 RID: 16819 RVA: 0x005E98A2 File Offset: 0x005E7AA2
		public override bool IsActive()
		{
			return this._isActive || this._fadeOpacity > 0.001f;
		}

		// Token: 0x040058F1 RID: 22769
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x040058F2 RID: 22770
		private Asset<Texture2D> _planetTexture;

		// Token: 0x040058F3 RID: 22771
		private Asset<Texture2D> _bgTexture;

		// Token: 0x040058F4 RID: 22772
		private Asset<Texture2D> _boltTexture;

		// Token: 0x040058F5 RID: 22773
		private Asset<Texture2D> _flashTexture;

		// Token: 0x040058F6 RID: 22774
		private bool _isActive;

		// Token: 0x040058F7 RID: 22775
		private int _ticksUntilNextBolt;

		// Token: 0x040058F8 RID: 22776
		private float _fadeOpacity;

		// Token: 0x040058F9 RID: 22777
		private VortexSky.Bolt[] _bolts;

		// Token: 0x02000C4F RID: 3151
		private struct Bolt
		{
			// Token: 0x04007910 RID: 30992
			public Vector2 Position;

			// Token: 0x04007911 RID: 30993
			public float Depth;

			// Token: 0x04007912 RID: 30994
			public int Life;

			// Token: 0x04007913 RID: 30995
			public bool IsAlive;
		}
	}
}
