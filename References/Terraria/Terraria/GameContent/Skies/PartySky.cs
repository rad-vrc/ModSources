using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x02000323 RID: 803
	public class PartySky : CustomSky
	{
		// Token: 0x0600246B RID: 9323 RVA: 0x0055B370 File Offset: 0x00559570
		public override void OnLoad()
		{
			this._textures = new Asset<Texture2D>[3];
			for (int i = 0; i < this._textures.Length; i++)
			{
				this._textures[i] = TextureAssets.Extra[69 + i];
			}
			this.GenerateBalloons(false);
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x0055B3B8 File Offset: 0x005595B8
		private void GenerateBalloons(bool onlyMissing)
		{
			if (!onlyMissing)
			{
				this._balloons = new PartySky.Balloon[Main.maxTilesY / 4];
			}
			for (int i = 0; i < this._balloons.Length; i++)
			{
				if (!onlyMissing || !this._balloons[i].Active)
				{
					int num = (int)((double)Main.screenPosition.Y * 0.7 - (double)Main.screenHeight);
					int minValue = (int)((double)num - Main.worldSurface * 16.0);
					this._balloons[i].Position = new Vector2((float)(this._random.Next(0, Main.maxTilesX) * 16), (float)this._random.Next(minValue, num));
					this.ResetBalloon(i);
					this._balloons[i].Active = true;
				}
			}
			this._balloonsDrawing = this._balloons.Length;
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x0055B4A0 File Offset: 0x005596A0
		public void ResetBalloon(int i)
		{
			this._balloons[i].Depth = (float)i / (float)this._balloons.Length * 1.75f + 1.6f;
			this._balloons[i].Speed = -1.5f - 2.5f * (float)this._random.NextDouble();
			this._balloons[i].Texture = this._textures[this._random.Next(2)].Value;
			this._balloons[i].Variant = this._random.Next(3);
			if (this._random.Next(30) == 0)
			{
				this._balloons[i].Texture = this._textures[2].Value;
			}
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x0055B572 File Offset: 0x00559772
		private bool IsNearParty()
		{
			return Main.player[Main.myPlayer].townNPCs > 0f || Main.SceneMetrics.PartyMonolithCount > 0;
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x0055B59C File Offset: 0x0055979C
		public override void Update(GameTime gameTime)
		{
			if (!PartySky.MultipleSkyWorkaroundFix && Main.dayRate == 0)
			{
				return;
			}
			PartySky.MultipleSkyWorkaroundFix = false;
			if (Main.gamePaused || !Main.hasFocus)
			{
				return;
			}
			this._opacity = Utils.Clamp<float>(this._opacity + (float)this.IsNearParty().ToDirectionInt() * 0.01f, 0f, 1f);
			for (int i = 0; i < this._balloons.Length; i++)
			{
				if (this._balloons[i].Active)
				{
					PartySky.Balloon[] balloons = this._balloons;
					int num = i;
					int frame = balloons[num].Frame;
					balloons[num].Frame = frame + 1;
					PartySky.Balloon[] balloons2 = this._balloons;
					int num2 = i;
					balloons2[num2].Position.Y = balloons2[num2].Position.Y + this._balloons[i].Speed;
					PartySky.Balloon[] balloons3 = this._balloons;
					int num3 = i;
					balloons3[num3].Position.X = balloons3[num3].Position.X + Main.windSpeedCurrent * (3f - this._balloons[i].Speed);
					if (this._balloons[i].Position.Y < 300f)
					{
						if (!this._leaving)
						{
							this.ResetBalloon(i);
							this._balloons[i].Position = new Vector2((float)(this._random.Next(0, Main.maxTilesX) * 16), (float)Main.worldSurface * 16f + 1600f);
							if (this._random.Next(30) == 0)
							{
								this._balloons[i].Texture = this._textures[2].Value;
							}
						}
						else
						{
							this._balloons[i].Active = false;
							this._balloonsDrawing--;
						}
					}
				}
			}
			if (this._balloonsDrawing == 0)
			{
				this._active = false;
			}
			this._active = true;
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x0055B778 File Offset: 0x00559978
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (Main.gameMenu && this._active)
			{
				this._active = false;
				this._leaving = false;
				for (int i = 0; i < this._balloons.Length; i++)
				{
					this._balloons[i].Active = false;
				}
			}
			if ((double)Main.screenPosition.Y > Main.worldSurface * 16.0 || Main.gameMenu)
			{
				return;
			}
			if (this._opacity <= 0f)
			{
				return;
			}
			int num = -1;
			int num2 = 0;
			for (int j = 0; j < this._balloons.Length; j++)
			{
				float depth = this._balloons[j].Depth;
				if (num == -1 && depth < maxDepth)
				{
					num = j;
				}
				if (depth <= minDepth)
				{
					break;
				}
				num2 = j;
			}
			if (num == -1)
			{
				return;
			}
			Vector2 value = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
			Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
			for (int k = num; k < num2; k++)
			{
				if (this._balloons[k].Active)
				{
					Color value2 = new Color(Main.ColorOfTheSkies.ToVector4() * 0.9f + new Vector4(0.1f)) * 0.8f;
					float num3 = 1f;
					if (this._balloons[k].Depth > 3f)
					{
						num3 = 0.6f;
					}
					else if ((double)this._balloons[k].Depth > 2.5)
					{
						num3 = 0.7f;
					}
					else if (this._balloons[k].Depth > 2f)
					{
						num3 = 0.8f;
					}
					else if ((double)this._balloons[k].Depth > 1.5)
					{
						num3 = 0.9f;
					}
					num3 *= 0.9f;
					value2 = new Color((int)((float)value2.R * num3), (int)((float)value2.G * num3), (int)((float)value2.B * num3), (int)((float)value2.A * num3));
					Vector2 vector = new Vector2(1f / this._balloons[k].Depth, 0.9f / this._balloons[k].Depth);
					Vector2 vector2 = this._balloons[k].Position;
					vector2 = (vector2 - value) * vector + value - Main.screenPosition;
					vector2.X = (vector2.X + 500f) % 4000f;
					if (vector2.X < 0f)
					{
						vector2.X += 4000f;
					}
					vector2.X -= 500f;
					if (rectangle.Contains((int)vector2.X, (int)vector2.Y))
					{
						spriteBatch.Draw(this._balloons[k].Texture, vector2, new Rectangle?(this._balloons[k].GetSourceRectangle()), value2 * this._opacity, 0f, Vector2.Zero, vector.X * 2f, SpriteEffects.None, 0f);
					}
				}
			}
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x0055BAE2 File Offset: 0x00559CE2
		public override void Activate(Vector2 position, params object[] args)
		{
			if (this._active)
			{
				this._leaving = false;
				this.GenerateBalloons(true);
				return;
			}
			this.GenerateBalloons(false);
			this._active = true;
			this._leaving = false;
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x0055BB10 File Offset: 0x00559D10
		public override void Deactivate(params object[] args)
		{
			this._leaving = true;
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x0055BB19 File Offset: 0x00559D19
		public override bool IsActive()
		{
			return this._active;
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x0055BB21 File Offset: 0x00559D21
		public override void Reset()
		{
			this._active = false;
		}

		// Token: 0x04004899 RID: 18585
		public static bool MultipleSkyWorkaroundFix;

		// Token: 0x0400489A RID: 18586
		private bool _active;

		// Token: 0x0400489B RID: 18587
		private bool _leaving;

		// Token: 0x0400489C RID: 18588
		private float _opacity;

		// Token: 0x0400489D RID: 18589
		private Asset<Texture2D>[] _textures;

		// Token: 0x0400489E RID: 18590
		private PartySky.Balloon[] _balloons;

		// Token: 0x0400489F RID: 18591
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x040048A0 RID: 18592
		private int _balloonsDrawing;

		// Token: 0x02000708 RID: 1800
		private struct Balloon
		{
			// Token: 0x17000403 RID: 1027
			// (get) Token: 0x06003774 RID: 14196 RVA: 0x00610B39 File Offset: 0x0060ED39
			// (set) Token: 0x06003775 RID: 14197 RVA: 0x00610B41 File Offset: 0x0060ED41
			public Texture2D Texture
			{
				get
				{
					return this._texture;
				}
				set
				{
					this._texture = value;
					this.FrameWidth = value.Width / 3;
					this.FrameHeight = value.Height / 3;
				}
			}

			// Token: 0x17000404 RID: 1028
			// (get) Token: 0x06003776 RID: 14198 RVA: 0x00610B66 File Offset: 0x0060ED66
			// (set) Token: 0x06003777 RID: 14199 RVA: 0x00610B6E File Offset: 0x0060ED6E
			public int Frame
			{
				get
				{
					return this._frameCounter;
				}
				set
				{
					this._frameCounter = value % 42;
				}
			}

			// Token: 0x06003778 RID: 14200 RVA: 0x00610B7A File Offset: 0x0060ED7A
			public Rectangle GetSourceRectangle()
			{
				return new Rectangle(this.FrameWidth * this.Variant, this._frameCounter / 14 * this.FrameHeight, this.FrameWidth, this.FrameHeight);
			}

			// Token: 0x040062C7 RID: 25287
			private const int MAX_FRAMES_X = 3;

			// Token: 0x040062C8 RID: 25288
			private const int MAX_FRAMES_Y = 3;

			// Token: 0x040062C9 RID: 25289
			private const int FRAME_RATE = 14;

			// Token: 0x040062CA RID: 25290
			public int Variant;

			// Token: 0x040062CB RID: 25291
			private Texture2D _texture;

			// Token: 0x040062CC RID: 25292
			public Vector2 Position;

			// Token: 0x040062CD RID: 25293
			public float Depth;

			// Token: 0x040062CE RID: 25294
			public int FrameHeight;

			// Token: 0x040062CF RID: 25295
			public int FrameWidth;

			// Token: 0x040062D0 RID: 25296
			public float Speed;

			// Token: 0x040062D1 RID: 25297
			public bool Active;

			// Token: 0x040062D2 RID: 25298
			private int _frameCounter;
		}
	}
}
