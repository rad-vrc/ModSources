using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x0200032A RID: 810
	public class MartianSky : CustomSky
	{
		// Token: 0x060024B3 RID: 9395 RVA: 0x0055DC64 File Offset: 0x0055BE64
		public override void Update(GameTime gameTime)
		{
			if (Main.gamePaused || !Main.hasFocus)
			{
				return;
			}
			int num = this._activeUfos;
			for (int i = 0; i < this._ufos.Length; i++)
			{
				MartianSky.Ufo ufo = this._ufos[i];
				if (ufo.IsActive)
				{
					int frame = ufo.Frame;
					ufo.Frame = frame + 1;
					if (!ufo.Update())
					{
						if (!this._leaving)
						{
							ufo.AssignNewBehavior();
						}
						else
						{
							ufo.IsActive = false;
							num--;
						}
					}
				}
				this._ufos[i] = ufo;
			}
			if (!this._leaving && num != this._maxUfos)
			{
				this._ufos[num].IsActive = true;
				this._ufos[num++].AssignNewBehavior();
			}
			this._active = (!this._leaving || num != 0);
			this._activeUfos = num;
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x0055DD48 File Offset: 0x0055BF48
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (Main.screenPosition.Y > 10000f)
			{
				return;
			}
			int num = -1;
			int num2 = 0;
			for (int i = 0; i < this._ufos.Length; i++)
			{
				float depth = this._ufos[i].Depth;
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
			Color value = new Color(Main.ColorOfTheSkies.ToVector4() * 0.9f + new Vector4(0.1f));
			Vector2 value2 = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
			Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
			for (int j = num; j < num2; j++)
			{
				Vector2 vector = new Vector2(1f / this._ufos[j].Depth, 0.9f / this._ufos[j].Depth);
				Vector2 vector2 = this._ufos[j].Position;
				vector2 = (vector2 - value2) * vector + value2 - Main.screenPosition;
				if (this._ufos[j].IsActive && rectangle.Contains((int)vector2.X, (int)vector2.Y))
				{
					spriteBatch.Draw(this._ufos[j].Texture, vector2, new Rectangle?(this._ufos[j].GetSourceRectangle()), value * this._ufos[j].Opacity, this._ufos[j].Rotation, Vector2.Zero, vector.X * 5f * this._ufos[j].Scale, SpriteEffects.None, 0f);
					if (this._ufos[j].GlowTexture != null)
					{
						spriteBatch.Draw(this._ufos[j].GlowTexture, vector2, new Rectangle?(this._ufos[j].GetSourceRectangle()), Color.White * this._ufos[j].Opacity, this._ufos[j].Rotation, Vector2.Zero, vector.X * 5f * this._ufos[j].Scale, SpriteEffects.None, 0f);
					}
				}
			}
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x0055DFF0 File Offset: 0x0055C1F0
		private void GenerateUfos()
		{
			float num = (float)Main.maxTilesX / 4200f;
			this._maxUfos = (int)(256f * num);
			this._ufos = new MartianSky.Ufo[this._maxUfos];
			int num2 = this._maxUfos >> 4;
			for (int i = 0; i < num2; i++)
			{
				float num3 = (float)i / (float)num2;
				this._ufos[i] = new MartianSky.Ufo(TextureAssets.Extra[5].Value, (float)Main.rand.NextDouble() * 4f + 6.6f);
				this._ufos[i].GlowTexture = TextureAssets.GlowMask[90].Value;
			}
			for (int j = num2; j < this._ufos.Length; j++)
			{
				float num4 = (float)(j - num2) / (float)(this._ufos.Length - num2);
				this._ufos[j] = new MartianSky.Ufo(TextureAssets.Extra[6].Value, (float)Main.rand.NextDouble() * 5f + 1.6f);
				this._ufos[j].Scale = 0.5f;
				this._ufos[j].GlowTexture = TextureAssets.GlowMask[91].Value;
			}
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x0055E128 File Offset: 0x0055C328
		public override void Activate(Vector2 position, params object[] args)
		{
			this._activeUfos = 0;
			this.GenerateUfos();
			Array.Sort<MartianSky.Ufo>(this._ufos, (MartianSky.Ufo ufo1, MartianSky.Ufo ufo2) => ufo2.Depth.CompareTo(ufo1.Depth));
			this._active = true;
			this._leaving = false;
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x0055E17A File Offset: 0x0055C37A
		public override void Deactivate(params object[] args)
		{
			this._leaving = true;
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x0055E183 File Offset: 0x0055C383
		public override bool IsActive()
		{
			return this._active;
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x0055E18B File Offset: 0x0055C38B
		public override void Reset()
		{
			this._active = false;
		}

		// Token: 0x040048C7 RID: 18631
		private MartianSky.Ufo[] _ufos;

		// Token: 0x040048C8 RID: 18632
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x040048C9 RID: 18633
		private int _maxUfos;

		// Token: 0x040048CA RID: 18634
		private bool _active;

		// Token: 0x040048CB RID: 18635
		private bool _leaving;

		// Token: 0x040048CC RID: 18636
		private int _activeUfos;

		// Token: 0x0200070D RID: 1805
		private abstract class IUfoController
		{
			// Token: 0x0600377E RID: 14206
			public abstract void InitializeUfo(ref MartianSky.Ufo ufo);

			// Token: 0x0600377F RID: 14207
			public abstract bool Update(ref MartianSky.Ufo ufo);
		}

		// Token: 0x0200070E RID: 1806
		private class ZipBehavior : MartianSky.IUfoController
		{
			// Token: 0x06003781 RID: 14209 RVA: 0x00610C0C File Offset: 0x0060EE0C
			public override void InitializeUfo(ref MartianSky.Ufo ufo)
			{
				ufo.Position.X = (float)(MartianSky.Ufo.Random.NextDouble() * (double)(Main.maxTilesX << 4));
				ufo.Position.Y = (float)(MartianSky.Ufo.Random.NextDouble() * 5000.0);
				ufo.Opacity = 0f;
				float num = (float)MartianSky.Ufo.Random.NextDouble() * 5f + 10f;
				double num2 = MartianSky.Ufo.Random.NextDouble() * 0.6000000238418579 - 0.30000001192092896;
				ufo.Rotation = (float)num2;
				if (MartianSky.Ufo.Random.Next(2) == 0)
				{
					num2 += 3.1415927410125732;
				}
				this._speed = new Vector2((float)Math.Cos(num2) * num, (float)Math.Sin(num2) * num);
				this._ticks = 0;
				this._maxTicks = MartianSky.Ufo.Random.Next(400, 500);
			}

			// Token: 0x06003782 RID: 14210 RVA: 0x00610CFC File Offset: 0x0060EEFC
			public override bool Update(ref MartianSky.Ufo ufo)
			{
				if (this._ticks < 10)
				{
					ufo.Opacity += 0.1f;
				}
				else if (this._ticks > this._maxTicks - 10)
				{
					ufo.Opacity -= 0.1f;
				}
				ufo.Position += this._speed;
				if (this._ticks == this._maxTicks)
				{
					return false;
				}
				this._ticks++;
				return true;
			}

			// Token: 0x040062EC RID: 25324
			private Vector2 _speed;

			// Token: 0x040062ED RID: 25325
			private int _ticks;

			// Token: 0x040062EE RID: 25326
			private int _maxTicks;
		}

		// Token: 0x0200070F RID: 1807
		private class HoverBehavior : MartianSky.IUfoController
		{
			// Token: 0x06003784 RID: 14212 RVA: 0x00610D88 File Offset: 0x0060EF88
			public override void InitializeUfo(ref MartianSky.Ufo ufo)
			{
				ufo.Position.X = (float)(MartianSky.Ufo.Random.NextDouble() * (double)(Main.maxTilesX << 4));
				ufo.Position.Y = (float)(MartianSky.Ufo.Random.NextDouble() * 5000.0);
				ufo.Opacity = 0f;
				ufo.Rotation = 0f;
				this._ticks = 0;
				this._maxTicks = MartianSky.Ufo.Random.Next(120, 240);
			}

			// Token: 0x06003785 RID: 14213 RVA: 0x00610E08 File Offset: 0x0060F008
			public override bool Update(ref MartianSky.Ufo ufo)
			{
				if (this._ticks < 10)
				{
					ufo.Opacity += 0.1f;
				}
				else if (this._ticks > this._maxTicks - 10)
				{
					ufo.Opacity -= 0.1f;
				}
				if (this._ticks == this._maxTicks)
				{
					return false;
				}
				this._ticks++;
				return true;
			}

			// Token: 0x040062EF RID: 25327
			private int _ticks;

			// Token: 0x040062F0 RID: 25328
			private int _maxTicks;
		}

		// Token: 0x02000710 RID: 1808
		private struct Ufo
		{
			// Token: 0x17000407 RID: 1031
			// (get) Token: 0x06003787 RID: 14215 RVA: 0x00610E6F File Offset: 0x0060F06F
			// (set) Token: 0x06003788 RID: 14216 RVA: 0x00610E77 File Offset: 0x0060F077
			public int Frame
			{
				get
				{
					return this._frame;
				}
				set
				{
					this._frame = value % 12;
				}
			}

			// Token: 0x17000408 RID: 1032
			// (get) Token: 0x06003789 RID: 14217 RVA: 0x00610E83 File Offset: 0x0060F083
			// (set) Token: 0x0600378A RID: 14218 RVA: 0x00610E8B File Offset: 0x0060F08B
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
					this.FrameHeight = value.Height / 3;
				}
			}

			// Token: 0x17000409 RID: 1033
			// (get) Token: 0x0600378B RID: 14219 RVA: 0x00610EAE File Offset: 0x0060F0AE
			// (set) Token: 0x0600378C RID: 14220 RVA: 0x00610EB6 File Offset: 0x0060F0B6
			public MartianSky.IUfoController Controller
			{
				get
				{
					return this._controller;
				}
				set
				{
					this._controller = value;
					value.InitializeUfo(ref this);
				}
			}

			// Token: 0x0600378D RID: 14221 RVA: 0x00610EC8 File Offset: 0x0060F0C8
			public Ufo(Texture2D texture, float depth = 1f)
			{
				this._frame = 0;
				this.Position = Vector2.Zero;
				this._texture = texture;
				this.Depth = depth;
				this.Scale = 1f;
				this.FrameWidth = texture.Width;
				this.FrameHeight = texture.Height / 3;
				this.GlowTexture = null;
				this.Opacity = 0f;
				this.Rotation = 0f;
				this.IsActive = false;
				this._controller = null;
			}

			// Token: 0x0600378E RID: 14222 RVA: 0x00610F45 File Offset: 0x0060F145
			public Rectangle GetSourceRectangle()
			{
				return new Rectangle(0, this._frame / 4 * this.FrameHeight, this.FrameWidth, this.FrameHeight);
			}

			// Token: 0x0600378F RID: 14223 RVA: 0x00610F68 File Offset: 0x0060F168
			public bool Update()
			{
				return this.Controller.Update(ref this);
			}

			// Token: 0x06003790 RID: 14224 RVA: 0x00610F78 File Offset: 0x0060F178
			public void AssignNewBehavior()
			{
				int num = MartianSky.Ufo.Random.Next(2);
				if (num == 0)
				{
					this.Controller = new MartianSky.ZipBehavior();
					return;
				}
				if (num != 1)
				{
					return;
				}
				this.Controller = new MartianSky.HoverBehavior();
			}

			// Token: 0x040062F1 RID: 25329
			private const int MAX_FRAMES = 3;

			// Token: 0x040062F2 RID: 25330
			private const int FRAME_RATE = 4;

			// Token: 0x040062F3 RID: 25331
			public static UnifiedRandom Random = new UnifiedRandom();

			// Token: 0x040062F4 RID: 25332
			private int _frame;

			// Token: 0x040062F5 RID: 25333
			private Texture2D _texture;

			// Token: 0x040062F6 RID: 25334
			private MartianSky.IUfoController _controller;

			// Token: 0x040062F7 RID: 25335
			public Texture2D GlowTexture;

			// Token: 0x040062F8 RID: 25336
			public Vector2 Position;

			// Token: 0x040062F9 RID: 25337
			public int FrameHeight;

			// Token: 0x040062FA RID: 25338
			public int FrameWidth;

			// Token: 0x040062FB RID: 25339
			public float Depth;

			// Token: 0x040062FC RID: 25340
			public float Scale;

			// Token: 0x040062FD RID: 25341
			public float Opacity;

			// Token: 0x040062FE RID: 25342
			public bool IsActive;

			// Token: 0x040062FF RID: 25343
			public float Rotation;
		}
	}
}
