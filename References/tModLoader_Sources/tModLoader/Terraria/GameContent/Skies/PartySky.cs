using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x0200056B RID: 1387
	public class PartySky : CustomSky
	{
		// Token: 0x06004179 RID: 16761 RVA: 0x005E724C File Offset: 0x005E544C
		public override void OnLoad()
		{
			this._textures = new Asset<Texture2D>[3];
			for (int i = 0; i < this._textures.Length; i++)
			{
				this._textures[i] = TextureAssets.Extra[69 + i];
			}
			this.GenerateBalloons(false);
		}

		// Token: 0x0600417A RID: 16762 RVA: 0x005E7294 File Offset: 0x005E5494
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

		// Token: 0x0600417B RID: 16763 RVA: 0x005E737C File Offset: 0x005E557C
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

		// Token: 0x0600417C RID: 16764 RVA: 0x005E744E File Offset: 0x005E564E
		private bool IsNearParty()
		{
			return Main.player[Main.myPlayer].townNPCs > 0f || Main.SceneMetrics.PartyMonolithCount > 0;
		}

		// Token: 0x0600417D RID: 16765 RVA: 0x005E7478 File Offset: 0x005E5678
		public override void Update(GameTime gameTime)
		{
			if (!PartySky.MultipleSkyWorkaroundFix && Main.dayRate == 0.0)
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

		// Token: 0x0600417E RID: 16766 RVA: 0x005E7660 File Offset: 0x005E5860
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
			if ((double)Main.screenPosition.Y > Main.worldSurface * 16.0 || Main.gameMenu || this._opacity <= 0f)
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
			Vector2 vector = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
			Rectangle rectangle;
			rectangle..ctor(-1000, -1000, 4000, 4000);
			for (int k = num; k < num2; k++)
			{
				if (this._balloons[k].Active)
				{
					Color color = new Color(Main.ColorOfTheSkies.ToVector4() * 0.9f + new Vector4(0.1f)) * 0.8f;
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
					color..ctor((int)((float)color.R * num3), (int)((float)color.G * num3), (int)((float)color.B * num3), (int)((float)color.A * num3));
					Vector2 vector2;
					vector2..ctor(1f / this._balloons[k].Depth, 0.9f / this._balloons[k].Depth);
					Vector2 position = this._balloons[k].Position;
					position = (position - vector) * vector2 + vector - Main.screenPosition;
					position.X = (position.X + 500f) % 4000f;
					if (position.X < 0f)
					{
						position.X += 4000f;
					}
					position.X -= 500f;
					if (rectangle.Contains((int)position.X, (int)position.Y))
					{
						spriteBatch.Draw(this._balloons[k].Texture, position, new Rectangle?(this._balloons[k].GetSourceRectangle()), color * this._opacity, 0f, Vector2.Zero, vector2.X * 2f, 0, 0f);
					}
				}
			}
		}

		// Token: 0x0600417F RID: 16767 RVA: 0x005E79C9 File Offset: 0x005E5BC9
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

		// Token: 0x06004180 RID: 16768 RVA: 0x005E79F7 File Offset: 0x005E5BF7
		public override void Deactivate(params object[] args)
		{
			this._leaving = true;
		}

		// Token: 0x06004181 RID: 16769 RVA: 0x005E7A00 File Offset: 0x005E5C00
		public override bool IsActive()
		{
			return this._active;
		}

		// Token: 0x06004182 RID: 16770 RVA: 0x005E7A08 File Offset: 0x005E5C08
		public override void Reset()
		{
			this._active = false;
		}

		// Token: 0x040058D1 RID: 22737
		public static bool MultipleSkyWorkaroundFix;

		// Token: 0x040058D2 RID: 22738
		private bool _active;

		// Token: 0x040058D3 RID: 22739
		private bool _leaving;

		// Token: 0x040058D4 RID: 22740
		private float _opacity;

		// Token: 0x040058D5 RID: 22741
		private Asset<Texture2D>[] _textures;

		// Token: 0x040058D6 RID: 22742
		private PartySky.Balloon[] _balloons;

		// Token: 0x040058D7 RID: 22743
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x040058D8 RID: 22744
		private int _balloonsDrawing;

		// Token: 0x02000C4B RID: 3147
		private struct Balloon
		{
			// Token: 0x1700095F RID: 2399
			// (get) Token: 0x06005FB5 RID: 24501 RVA: 0x006D0C2C File Offset: 0x006CEE2C
			// (set) Token: 0x06005FB6 RID: 24502 RVA: 0x006D0C34 File Offset: 0x006CEE34
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

			// Token: 0x17000960 RID: 2400
			// (get) Token: 0x06005FB7 RID: 24503 RVA: 0x006D0C59 File Offset: 0x006CEE59
			// (set) Token: 0x06005FB8 RID: 24504 RVA: 0x006D0C61 File Offset: 0x006CEE61
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

			// Token: 0x06005FB9 RID: 24505 RVA: 0x006D0C6D File Offset: 0x006CEE6D
			public Rectangle GetSourceRectangle()
			{
				return new Rectangle(this.FrameWidth * this.Variant, this._frameCounter / 14 * this.FrameHeight, this.FrameWidth, this.FrameHeight);
			}

			// Token: 0x040078EF RID: 30959
			private const int MAX_FRAMES_X = 3;

			// Token: 0x040078F0 RID: 30960
			private const int MAX_FRAMES_Y = 3;

			// Token: 0x040078F1 RID: 30961
			private const int FRAME_RATE = 14;

			// Token: 0x040078F2 RID: 30962
			public int Variant;

			// Token: 0x040078F3 RID: 30963
			private Texture2D _texture;

			// Token: 0x040078F4 RID: 30964
			public Vector2 Position;

			// Token: 0x040078F5 RID: 30965
			public float Depth;

			// Token: 0x040078F6 RID: 30966
			public int FrameHeight;

			// Token: 0x040078F7 RID: 30967
			public int FrameWidth;

			// Token: 0x040078F8 RID: 30968
			public float Speed;

			// Token: 0x040078F9 RID: 30969
			public bool Active;

			// Token: 0x040078FA RID: 30970
			private int _frameCounter;
		}
	}
}
