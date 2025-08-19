using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x0200056D RID: 1389
	public class SlimeSky : CustomSky
	{
		// Token: 0x0600418C RID: 16780 RVA: 0x005E7BA8 File Offset: 0x005E5DA8
		public override void OnLoad()
		{
			this._textures = new Asset<Texture2D>[4];
			for (int i = 0; i < 4; i++)
			{
				this._textures[i] = Main.Assets.Request<Texture2D>("Images/Misc/Sky_Slime_" + (i + 1).ToString());
			}
			this.GenerateSlimes();
		}

		// Token: 0x0600418D RID: 16781 RVA: 0x005E7BFC File Offset: 0x005E5DFC
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

		// Token: 0x0600418E RID: 16782 RVA: 0x005E7E04 File Offset: 0x005E6004
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

		// Token: 0x0600418F RID: 16783 RVA: 0x005E8070 File Offset: 0x005E6270
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
			Vector2 vector = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
			Rectangle rectangle;
			rectangle..ctor(-1000, -1000, 4000, 4000);
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
					color..ctor((int)((float)color.R * num3), (int)((float)color.G * num3), (int)((float)color.B * num3), (int)((float)color.A * num3));
					Vector2 vector2;
					vector2..ctor(1f / this._slimes[j].Depth, 0.9f / this._slimes[j].Depth);
					Vector2 position = this._slimes[j].Position;
					position = (position - vector) * vector2 + vector - Main.screenPosition;
					position.X = (position.X + 500f) % 4000f;
					if (position.X < 0f)
					{
						position.X += 4000f;
					}
					position.X -= 500f;
					if (rectangle.Contains((int)position.X, (int)position.Y))
					{
						spriteBatch.Draw(this._slimes[j].Texture, position, new Rectangle?(this._slimes[j].GetSourceRectangle()), color, 0f, Vector2.Zero, vector2.X * 2f, 0, 0f);
					}
				}
			}
		}

		// Token: 0x06004190 RID: 16784 RVA: 0x005E836F File Offset: 0x005E656F
		public override void Activate(Vector2 position, params object[] args)
		{
			this.GenerateSlimes();
			this._isActive = true;
			this._isLeaving = false;
		}

		// Token: 0x06004191 RID: 16785 RVA: 0x005E8385 File Offset: 0x005E6585
		public override void Deactivate(params object[] args)
		{
			this._isLeaving = true;
		}

		// Token: 0x06004192 RID: 16786 RVA: 0x005E838E File Offset: 0x005E658E
		public override void Reset()
		{
			this._isActive = false;
		}

		// Token: 0x06004193 RID: 16787 RVA: 0x005E8397 File Offset: 0x005E6597
		public override bool IsActive()
		{
			return this._isActive;
		}

		// Token: 0x040058DD RID: 22749
		private Asset<Texture2D>[] _textures;

		// Token: 0x040058DE RID: 22750
		private SlimeSky.Slime[] _slimes;

		// Token: 0x040058DF RID: 22751
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x040058E0 RID: 22752
		private int _slimesRemaining;

		// Token: 0x040058E1 RID: 22753
		private bool _isActive;

		// Token: 0x040058E2 RID: 22754
		private bool _isLeaving;

		// Token: 0x02000C4C RID: 3148
		private struct Slime
		{
			// Token: 0x17000961 RID: 2401
			// (get) Token: 0x06005FBA RID: 24506 RVA: 0x006D0C9D File Offset: 0x006CEE9D
			// (set) Token: 0x06005FBB RID: 24507 RVA: 0x006D0CA5 File Offset: 0x006CEEA5
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

			// Token: 0x17000962 RID: 2402
			// (get) Token: 0x06005FBC RID: 24508 RVA: 0x006D0CC8 File Offset: 0x006CEEC8
			// (set) Token: 0x06005FBD RID: 24509 RVA: 0x006D0CD0 File Offset: 0x006CEED0
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

			// Token: 0x06005FBE RID: 24510 RVA: 0x006D0CDC File Offset: 0x006CEEDC
			public Rectangle GetSourceRectangle()
			{
				return new Rectangle(0, this._frame / 6 * this.FrameHeight, this.FrameWidth, this.FrameHeight);
			}

			// Token: 0x040078FB RID: 30971
			private const int MAX_FRAMES = 4;

			// Token: 0x040078FC RID: 30972
			private const int FRAME_RATE = 6;

			// Token: 0x040078FD RID: 30973
			private Texture2D _texture;

			// Token: 0x040078FE RID: 30974
			public Vector2 Position;

			// Token: 0x040078FF RID: 30975
			public float Depth;

			// Token: 0x04007900 RID: 30976
			public int FrameHeight;

			// Token: 0x04007901 RID: 30977
			public int FrameWidth;

			// Token: 0x04007902 RID: 30978
			public float Speed;

			// Token: 0x04007903 RID: 30979
			public bool Active;

			// Token: 0x04007904 RID: 30980
			private int _frame;
		}
	}
}
