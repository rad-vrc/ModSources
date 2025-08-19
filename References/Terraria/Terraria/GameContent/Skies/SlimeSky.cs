using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x02000328 RID: 808
	public class SlimeSky : CustomSky
	{
		// Token: 0x0600249F RID: 9375 RVA: 0x0055CD2C File Offset: 0x0055AF2C
		public override void OnLoad()
		{
			this._textures = new Asset<Texture2D>[4];
			for (int i = 0; i < 4; i++)
			{
				this._textures[i] = Main.Assets.Request<Texture2D>("Images/Misc/Sky_Slime_" + (i + 1), 1);
			}
			this.GenerateSlimes();
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x0055CD7C File Offset: 0x0055AF7C
		private void GenerateSlimes()
		{
			this._slimes = new SlimeSky.Slime[Main.maxTilesY / 6];
			for (int i = 0; i < this._slimes.Length; i++)
			{
				int num = (int)((double)Main.screenPosition.Y * 0.7 - (double)Main.screenHeight);
				int minValue = (int)((double)num - Main.worldSurface * 16.0);
				this._slimes[i].Position = new Vector2((float)(this._random.Next(0, Main.maxTilesX) * 16), (float)this._random.Next(minValue, num));
				this._slimes[i].Speed = 5f + 3f * (float)this._random.NextDouble();
				this._slimes[i].Depth = (float)i / (float)this._slimes.Length * 1.75f + 1.6f;
				this._slimes[i].Texture = this._textures[this._random.Next(2)].Value;
				if (this._random.Next(60) == 0)
				{
					this._slimes[i].Texture = this._textures[3].Value;
					this._slimes[i].Speed = 6f + 3f * (float)this._random.NextDouble();
					SlimeSky.Slime[] slimes = this._slimes;
					int num2 = i;
					slimes[num2].Depth = slimes[num2].Depth + 0.5f;
				}
				else if (this._random.Next(30) == 0)
				{
					this._slimes[i].Texture = this._textures[2].Value;
					this._slimes[i].Speed = 6f + 2f * (float)this._random.NextDouble();
				}
				this._slimes[i].Active = true;
			}
			this._slimesRemaining = this._slimes.Length;
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x0055CF84 File Offset: 0x0055B184
		public override void Update(GameTime gameTime)
		{
			if (Main.gamePaused || !Main.hasFocus)
			{
				return;
			}
			for (int i = 0; i < this._slimes.Length; i++)
			{
				if (this._slimes[i].Active)
				{
					SlimeSky.Slime[] slimes = this._slimes;
					int num = i;
					int frame = slimes[num].Frame;
					slimes[num].Frame = frame + 1;
					SlimeSky.Slime[] slimes2 = this._slimes;
					int num2 = i;
					slimes2[num2].Position.Y = slimes2[num2].Position.Y + this._slimes[i].Speed;
					if ((double)this._slimes[i].Position.Y > Main.worldSurface * 16.0)
					{
						if (!this._isLeaving)
						{
							this._slimes[i].Depth = (float)i / (float)this._slimes.Length * 1.75f + 1.6f;
							this._slimes[i].Position = new Vector2((float)(this._random.Next(0, Main.maxTilesX) * 16), -100f);
							this._slimes[i].Texture = this._textures[this._random.Next(2)].Value;
							this._slimes[i].Speed = 5f + 3f * (float)this._random.NextDouble();
							if (this._random.Next(60) == 0)
							{
								this._slimes[i].Texture = this._textures[3].Value;
								this._slimes[i].Speed = 6f + 3f * (float)this._random.NextDouble();
								SlimeSky.Slime[] slimes3 = this._slimes;
								int num3 = i;
								slimes3[num3].Depth = slimes3[num3].Depth + 0.5f;
							}
							else if (this._random.Next(30) == 0)
							{
								this._slimes[i].Texture = this._textures[2].Value;
								this._slimes[i].Speed = 6f + 2f * (float)this._random.NextDouble();
							}
						}
						else
						{
							this._slimes[i].Active = false;
							this._slimesRemaining--;
						}
					}
				}
			}
			if (this._slimesRemaining == 0)
			{
				this._isActive = false;
			}
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x0055D1F0 File Offset: 0x0055B3F0
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (Main.screenPosition.Y > 10000f || Main.gameMenu)
			{
				return;
			}
			int num = -1;
			int num2 = 0;
			for (int i = 0; i < this._slimes.Length; i++)
			{
				float depth = this._slimes[i].Depth;
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
			Vector2 value = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
			Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
			for (int j = num; j < num2; j++)
			{
				if (this._slimes[j].Active)
				{
					Color color = new Color(Main.ColorOfTheSkies.ToVector4() * 0.9f + new Vector4(0.1f)) * 0.8f;
					float num3 = 1f;
					if (this._slimes[j].Depth > 3f)
					{
						num3 = 0.6f;
					}
					else if ((double)this._slimes[j].Depth > 2.5)
					{
						num3 = 0.7f;
					}
					else if (this._slimes[j].Depth > 2f)
					{
						num3 = 0.8f;
					}
					else if ((double)this._slimes[j].Depth > 1.5)
					{
						num3 = 0.9f;
					}
					num3 *= 0.8f;
					color = new Color((int)((float)color.R * num3), (int)((float)color.G * num3), (int)((float)color.B * num3), (int)((float)color.A * num3));
					Vector2 vector = new Vector2(1f / this._slimes[j].Depth, 0.9f / this._slimes[j].Depth);
					Vector2 vector2 = this._slimes[j].Position;
					vector2 = (vector2 - value) * vector + value - Main.screenPosition;
					vector2.X = (vector2.X + 500f) % 4000f;
					if (vector2.X < 0f)
					{
						vector2.X += 4000f;
					}
					vector2.X -= 500f;
					if (rectangle.Contains((int)vector2.X, (int)vector2.Y))
					{
						spriteBatch.Draw(this._slimes[j].Texture, vector2, new Rectangle?(this._slimes[j].GetSourceRectangle()), color, 0f, Vector2.Zero, vector.X * 2f, SpriteEffects.None, 0f);
					}
				}
			}
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x0055D4EF File Offset: 0x0055B6EF
		public override void Activate(Vector2 position, params object[] args)
		{
			this.GenerateSlimes();
			this._isActive = true;
			this._isLeaving = false;
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x0055D505 File Offset: 0x0055B705
		public override void Deactivate(params object[] args)
		{
			this._isLeaving = true;
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x0055D50E File Offset: 0x0055B70E
		public override void Reset()
		{
			this._isActive = false;
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x0055D517 File Offset: 0x0055B717
		public override bool IsActive()
		{
			return this._isActive;
		}

		// Token: 0x040048BA RID: 18618
		private Asset<Texture2D>[] _textures;

		// Token: 0x040048BB RID: 18619
		private SlimeSky.Slime[] _slimes;

		// Token: 0x040048BC RID: 18620
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x040048BD RID: 18621
		private int _slimesRemaining;

		// Token: 0x040048BE RID: 18622
		private bool _isActive;

		// Token: 0x040048BF RID: 18623
		private bool _isLeaving;

		// Token: 0x0200070B RID: 1803
		private struct Slime
		{
			// Token: 0x17000405 RID: 1029
			// (get) Token: 0x06003779 RID: 14201 RVA: 0x00610BAA File Offset: 0x0060EDAA
			// (set) Token: 0x0600377A RID: 14202 RVA: 0x00610BB2 File Offset: 0x0060EDB2
			public Texture2D Texture
			{
				get
				{
					return this._texture;
				}
				set
				{
					this._texture = value;
					this.FrameWidth = value.Width;
					this.FrameHeight = value.Height / 4;
				}
			}

			// Token: 0x17000406 RID: 1030
			// (get) Token: 0x0600377B RID: 14203 RVA: 0x00610BD5 File Offset: 0x0060EDD5
			// (set) Token: 0x0600377C RID: 14204 RVA: 0x00610BDD File Offset: 0x0060EDDD
			public int Frame
			{
				get
				{
					return this._frame;
				}
				set
				{
					this._frame = value % 24;
				}
			}

			// Token: 0x0600377D RID: 14205 RVA: 0x00610BE9 File Offset: 0x0060EDE9
			public Rectangle GetSourceRectangle()
			{
				return new Rectangle(0, this._frame / 6 * this.FrameHeight, this.FrameWidth, this.FrameHeight);
			}

			// Token: 0x040062DC RID: 25308
			private const int MAX_FRAMES = 4;

			// Token: 0x040062DD RID: 25309
			private const int FRAME_RATE = 6;

			// Token: 0x040062DE RID: 25310
			private Texture2D _texture;

			// Token: 0x040062DF RID: 25311
			public Vector2 Position;

			// Token: 0x040062E0 RID: 25312
			public float Depth;

			// Token: 0x040062E1 RID: 25313
			public int FrameHeight;

			// Token: 0x040062E2 RID: 25314
			public int FrameWidth;

			// Token: 0x040062E3 RID: 25315
			public float Speed;

			// Token: 0x040062E4 RID: 25316
			public bool Active;

			// Token: 0x040062E5 RID: 25317
			private int _frame;
		}
	}
}
