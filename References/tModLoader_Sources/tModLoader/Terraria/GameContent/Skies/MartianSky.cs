using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x02000568 RID: 1384
	public class MartianSky : CustomSky
	{
		// Token: 0x0600415A RID: 16730 RVA: 0x005E6394 File Offset: 0x005E4594
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

		// Token: 0x0600415B RID: 16731 RVA: 0x005E6478 File Offset: 0x005E4678
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
			Color color;
			color..ctor(Main.ColorOfTheSkies.ToVector4() * 0.9f + new Vector4(0.1f));
			Vector2 vector = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
			Rectangle rectangle;
			rectangle..ctor(-1000, -1000, 4000, 4000);
			for (int j = num; j < num2; j++)
			{
				Vector2 vector2;
				vector2..ctor(1f / this._ufos[j].Depth, 0.9f / this._ufos[j].Depth);
				Vector2 position = this._ufos[j].Position;
				position = (position - vector) * vector2 + vector - Main.screenPosition;
				if (this._ufos[j].IsActive && rectangle.Contains((int)position.X, (int)position.Y))
				{
					spriteBatch.Draw(this._ufos[j].Texture, position, new Rectangle?(this._ufos[j].GetSourceRectangle()), color * this._ufos[j].Opacity, this._ufos[j].Rotation, Vector2.Zero, vector2.X * 5f * this._ufos[j].Scale, 0, 0f);
					if (this._ufos[j].GlowTexture != null)
					{
						spriteBatch.Draw(this._ufos[j].GlowTexture, position, new Rectangle?(this._ufos[j].GetSourceRectangle()), Color.White * this._ufos[j].Opacity, this._ufos[j].Rotation, Vector2.Zero, vector2.X * 5f * this._ufos[j].Scale, 0, 0f);
					}
				}
			}
		}

		// Token: 0x0600415C RID: 16732 RVA: 0x005E6720 File Offset: 0x005E4920
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

		// Token: 0x0600415D RID: 16733 RVA: 0x005E6858 File Offset: 0x005E4A58
		public override void Activate(Vector2 position, params object[] args)
		{
			this._activeUfos = 0;
			this.GenerateUfos();
			Array.Sort<MartianSky.Ufo>(this._ufos, (MartianSky.Ufo ufo1, MartianSky.Ufo ufo2) => ufo2.Depth.CompareTo(ufo1.Depth));
			this._active = true;
			this._leaving = false;
		}

		// Token: 0x0600415E RID: 16734 RVA: 0x005E68AA File Offset: 0x005E4AAA
		public override void Deactivate(params object[] args)
		{
			this._leaving = true;
		}

		// Token: 0x0600415F RID: 16735 RVA: 0x005E68B3 File Offset: 0x005E4AB3
		public override bool IsActive()
		{
			return this._active;
		}

		// Token: 0x06004160 RID: 16736 RVA: 0x005E68BB File Offset: 0x005E4ABB
		public override void Reset()
		{
			this._active = false;
		}

		// Token: 0x040058BE RID: 22718
		private MartianSky.Ufo[] _ufos;

		// Token: 0x040058BF RID: 22719
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x040058C0 RID: 22720
		private int _maxUfos;

		// Token: 0x040058C1 RID: 22721
		private bool _active;

		// Token: 0x040058C2 RID: 22722
		private bool _leaving;

		// Token: 0x040058C3 RID: 22723
		private int _activeUfos;

		// Token: 0x02000C45 RID: 3141
		private abstract class IUfoController
		{
			// Token: 0x06005F9E RID: 24478
			public abstract void InitializeUfo(ref MartianSky.Ufo ufo);

			// Token: 0x06005F9F RID: 24479
			public abstract bool Update(ref MartianSky.Ufo ufo);
		}

		// Token: 0x02000C46 RID: 3142
		private class ZipBehavior : MartianSky.IUfoController
		{
			// Token: 0x06005FA1 RID: 24481 RVA: 0x006D084C File Offset: 0x006CEA4C
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

			// Token: 0x06005FA2 RID: 24482 RVA: 0x006D093C File Offset: 0x006CEB3C
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

			// Token: 0x040078D7 RID: 30935
			private Vector2 _speed;

			// Token: 0x040078D8 RID: 30936
			private int _ticks;

			// Token: 0x040078D9 RID: 30937
			private int _maxTicks;
		}

		// Token: 0x02000C47 RID: 3143
		private class HoverBehavior : MartianSky.IUfoController
		{
			// Token: 0x06005FA4 RID: 24484 RVA: 0x006D09C8 File Offset: 0x006CEBC8
			public override void InitializeUfo(ref MartianSky.Ufo ufo)
			{
				ufo.Position.X = (float)(MartianSky.Ufo.Random.NextDouble() * (double)(Main.maxTilesX << 4));
				ufo.Position.Y = (float)(MartianSky.Ufo.Random.NextDouble() * 5000.0);
				ufo.Opacity = 0f;
				ufo.Rotation = 0f;
				this._ticks = 0;
				this._maxTicks = MartianSky.Ufo.Random.Next(120, 240);
			}

			// Token: 0x06005FA5 RID: 24485 RVA: 0x006D0A48 File Offset: 0x006CEC48
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

			// Token: 0x040078DA RID: 30938
			private int _ticks;

			// Token: 0x040078DB RID: 30939
			private int _maxTicks;
		}

		// Token: 0x02000C48 RID: 3144
		private struct Ufo
		{
			// Token: 0x1700095C RID: 2396
			// (get) Token: 0x06005FA7 RID: 24487 RVA: 0x006D0AB7 File Offset: 0x006CECB7
			// (set) Token: 0x06005FA8 RID: 24488 RVA: 0x006D0ABF File Offset: 0x006CECBF
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

			// Token: 0x1700095D RID: 2397
			// (get) Token: 0x06005FA9 RID: 24489 RVA: 0x006D0ACB File Offset: 0x006CECCB
			// (set) Token: 0x06005FAA RID: 24490 RVA: 0x006D0AD3 File Offset: 0x006CECD3
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

			// Token: 0x1700095E RID: 2398
			// (get) Token: 0x06005FAB RID: 24491 RVA: 0x006D0AF6 File Offset: 0x006CECF6
			// (set) Token: 0x06005FAC RID: 24492 RVA: 0x006D0AFE File Offset: 0x006CECFE
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

			// Token: 0x06005FAD RID: 24493 RVA: 0x006D0B10 File Offset: 0x006CED10
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

			// Token: 0x06005FAE RID: 24494 RVA: 0x006D0B8D File Offset: 0x006CED8D
			public Rectangle GetSourceRectangle()
			{
				return new Rectangle(0, this._frame / 4 * this.FrameHeight, this.FrameWidth, this.FrameHeight);
			}

			// Token: 0x06005FAF RID: 24495 RVA: 0x006D0BB0 File Offset: 0x006CEDB0
			public bool Update()
			{
				return this.Controller.Update(ref this);
			}

			// Token: 0x06005FB0 RID: 24496 RVA: 0x006D0BC0 File Offset: 0x006CEDC0
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

			// Token: 0x040078DC RID: 30940
			private const int MAX_FRAMES = 3;

			// Token: 0x040078DD RID: 30941
			private const int FRAME_RATE = 4;

			// Token: 0x040078DE RID: 30942
			public static UnifiedRandom Random = new UnifiedRandom();

			// Token: 0x040078DF RID: 30943
			private int _frame;

			// Token: 0x040078E0 RID: 30944
			private Texture2D _texture;

			// Token: 0x040078E1 RID: 30945
			private MartianSky.IUfoController _controller;

			// Token: 0x040078E2 RID: 30946
			public Texture2D GlowTexture;

			// Token: 0x040078E3 RID: 30947
			public Vector2 Position;

			// Token: 0x040078E4 RID: 30948
			public int FrameHeight;

			// Token: 0x040078E5 RID: 30949
			public int FrameWidth;

			// Token: 0x040078E6 RID: 30950
			public float Depth;

			// Token: 0x040078E7 RID: 30951
			public float Scale;

			// Token: 0x040078E8 RID: 30952
			public float Opacity;

			// Token: 0x040078E9 RID: 30953
			public bool IsActive;

			// Token: 0x040078EA RID: 30954
			public float Rotation;
		}
	}
}
