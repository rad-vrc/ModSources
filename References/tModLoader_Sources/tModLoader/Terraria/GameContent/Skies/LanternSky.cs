using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.Events;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x02000567 RID: 1383
	public class LanternSky : CustomSky
	{
		// Token: 0x0600414F RID: 16719 RVA: 0x005E5A10 File Offset: 0x005E3C10
		public override void OnLoad()
		{
			this._texture = TextureAssets.Extra[134];
			this.GenerateLanterns(false);
		}

		// Token: 0x06004150 RID: 16720 RVA: 0x005E5A2C File Offset: 0x005E3C2C
		private void GenerateLanterns(bool onlyMissing)
		{
			if (!onlyMissing)
			{
				this._lanterns = new LanternSky.Lantern[Main.maxTilesY / 4];
			}
			for (int i = 0; i < this._lanterns.Length; i++)
			{
				if (!onlyMissing || !this._lanterns[i].Active)
				{
					int num = (int)((double)Main.screenPosition.Y * 0.7 - (double)Main.screenHeight);
					int minValue = (int)((double)num - Main.worldSurface * 16.0);
					this._lanterns[i].Position = new Vector2((float)(this._random.Next(0, Main.maxTilesX) * 16), (float)this._random.Next(minValue, num));
					this.ResetLantern(i);
					this._lanterns[i].Active = true;
				}
			}
			this._lanternsDrawing = this._lanterns.Length;
		}

		// Token: 0x06004151 RID: 16721 RVA: 0x005E5B14 File Offset: 0x005E3D14
		public void ResetLantern(int i)
		{
			this._lanterns[i].Depth = (1f - (float)i / (float)this._lanterns.Length) * 4.4f + 1.6f;
			this._lanterns[i].Speed = -1.5f - 2.5f * (float)this._random.NextDouble();
			this._lanterns[i].Texture = this._texture.Value;
			this._lanterns[i].Variant = this._random.Next(3);
			this._lanterns[i].TimeUntilFloat = (int)((float)(2000 + this._random.Next(1200)) * 2f);
			this._lanterns[i].TimeUntilFloatMax = this._lanterns[i].TimeUntilFloat;
		}

		// Token: 0x06004152 RID: 16722 RVA: 0x005E5C04 File Offset: 0x005E3E04
		public override void Update(GameTime gameTime)
		{
			if (Main.gamePaused || !Main.hasFocus)
			{
				return;
			}
			this._opacity = Utils.Clamp<float>(this._opacity + (float)LanternNight.LanternsUp.ToDirectionInt() * 0.01f, 0f, 1f);
			for (int i = 0; i < this._lanterns.Length; i++)
			{
				if (this._lanterns[i].Active)
				{
					float num = Main.windSpeedCurrent;
					if (num == 0f)
					{
						num = 0.1f;
					}
					float num2 = (float)Math.Sin((double)(this._lanterns[i].Position.X / 120f)) * 0.5f;
					LanternSky.Lantern[] lanterns = this._lanterns;
					int num3 = i;
					lanterns[num3].Position.Y = lanterns[num3].Position.Y + num2 * 0.5f;
					LanternSky.Lantern[] lanterns2 = this._lanterns;
					int num4 = i;
					lanterns2[num4].Position.Y = lanterns2[num4].Position.Y + this._lanterns[i].FloatAdjustedSpeed * 0.5f;
					LanternSky.Lantern[] lanterns3 = this._lanterns;
					int num5 = i;
					lanterns3[num5].Position.X = lanterns3[num5].Position.X + (0.1f + num) * (3f - this._lanterns[i].Speed) * 0.5f * ((float)i / (float)this._lanterns.Length + 1.5f) / 2.5f;
					this._lanterns[i].Rotation = num2 * (float)((num >= 0f) ? 1 : -1) * 0.5f;
					this._lanterns[i].TimeUntilFloat = Math.Max(0, this._lanterns[i].TimeUntilFloat - 1);
					if (this._lanterns[i].Position.Y < 300f)
					{
						if (!this._leaving)
						{
							this.ResetLantern(i);
							this._lanterns[i].Position = new Vector2((float)(this._random.Next(0, Main.maxTilesX) * 16), (float)Main.worldSurface * 16f + 1600f);
						}
						else
						{
							this._lanterns[i].Active = false;
							this._lanternsDrawing--;
						}
					}
				}
			}
			this._active = true;
		}

		// Token: 0x06004153 RID: 16723 RVA: 0x005E5E48 File Offset: 0x005E4048
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (Main.gameMenu && this._active)
			{
				this._active = false;
				this._leaving = false;
				for (int i = 0; i < this._lanterns.Length; i++)
				{
					this._lanterns[i].Active = false;
				}
			}
			if ((double)Main.screenPosition.Y > Main.worldSurface * 16.0 || Main.gameMenu || this._opacity <= 0f)
			{
				return;
			}
			int num = -1;
			int num2 = 0;
			for (int j = 0; j < this._lanterns.Length; j++)
			{
				float depth = this._lanterns[j].Depth;
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
				if (this._lanterns[k].Active)
				{
					Color color;
					color..ctor(250, 120, 60, 120);
					float num3 = 1f;
					if (this._lanterns[k].Depth > 5f)
					{
						num3 = 0.3f;
					}
					else if ((double)this._lanterns[k].Depth > 4.5)
					{
						num3 = 0.4f;
					}
					else if (this._lanterns[k].Depth > 4f)
					{
						num3 = 0.5f;
					}
					else if ((double)this._lanterns[k].Depth > 3.5)
					{
						num3 = 0.6f;
					}
					else if (this._lanterns[k].Depth > 3f)
					{
						num3 = 0.7f;
					}
					else if ((double)this._lanterns[k].Depth > 2.5)
					{
						num3 = 0.8f;
					}
					else if (this._lanterns[k].Depth > 2f)
					{
						num3 = 0.9f;
					}
					color..ctor((int)((float)color.R * num3), (int)((float)color.G * num3), (int)((float)color.B * num3), (int)((float)color.A * num3));
					Vector2 vector2;
					vector2..ctor(1f / this._lanterns[k].Depth, 0.9f / this._lanterns[k].Depth);
					vector2 *= 1.2f;
					Vector2 position = this._lanterns[k].Position;
					position = (position - vector) * vector2 + vector - Main.screenPosition;
					position.X = (position.X + 500f) % 4000f;
					if (position.X < 0f)
					{
						position.X += 4000f;
					}
					position.X -= 500f;
					if (rectangle.Contains((int)position.X, (int)position.Y))
					{
						this.DrawLantern(spriteBatch, this._lanterns[k], color, vector2, position, num3);
					}
				}
			}
		}

		// Token: 0x06004154 RID: 16724 RVA: 0x005E61C8 File Offset: 0x005E43C8
		private void DrawLantern(SpriteBatch spriteBatch, LanternSky.Lantern lantern, Color opacity, Vector2 depthScale, Vector2 position, float alpha)
		{
			float y = (Main.GlobalTimeWrappedHourly % 6f / 6f * 6.2831855f).ToRotationVector2().Y;
			float num = y * 0.2f + 0.8f;
			Color color = new Color(255, 255, 255, 0) * this._opacity * alpha * num * 0.4f;
			for (float num2 = 0f; num2 < 1f; num2 += 0.33333334f)
			{
				Vector2 vector = new Vector2(0f, 2f).RotatedBy((double)(6.2831855f * num2 + lantern.Rotation), default(Vector2)) * y;
				spriteBatch.Draw(lantern.Texture, position + vector, new Rectangle?(lantern.GetSourceRectangle()), color, lantern.Rotation, lantern.GetSourceRectangle().Size() / 2f, depthScale.X * 2f, 0, 0f);
			}
			spriteBatch.Draw(lantern.Texture, position, new Rectangle?(lantern.GetSourceRectangle()), opacity * this._opacity, lantern.Rotation, lantern.GetSourceRectangle().Size() / 2f, depthScale.X * 2f, 0, 0f);
		}

		// Token: 0x06004155 RID: 16725 RVA: 0x005E6338 File Offset: 0x005E4538
		public override void Activate(Vector2 position, params object[] args)
		{
			if (this._active)
			{
				this._leaving = false;
				this.GenerateLanterns(true);
				return;
			}
			this.GenerateLanterns(false);
			this._active = true;
			this._leaving = false;
		}

		// Token: 0x06004156 RID: 16726 RVA: 0x005E6366 File Offset: 0x005E4566
		public override void Deactivate(params object[] args)
		{
			this._leaving = true;
		}

		// Token: 0x06004157 RID: 16727 RVA: 0x005E636F File Offset: 0x005E456F
		public override bool IsActive()
		{
			return this._active;
		}

		// Token: 0x06004158 RID: 16728 RVA: 0x005E6377 File Offset: 0x005E4577
		public override void Reset()
		{
			this._active = false;
		}

		// Token: 0x040058B6 RID: 22710
		private bool _active;

		// Token: 0x040058B7 RID: 22711
		private bool _leaving;

		// Token: 0x040058B8 RID: 22712
		private float _opacity;

		// Token: 0x040058B9 RID: 22713
		private Asset<Texture2D> _texture;

		// Token: 0x040058BA RID: 22714
		private LanternSky.Lantern[] _lanterns;

		// Token: 0x040058BB RID: 22715
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x040058BC RID: 22716
		private int _lanternsDrawing;

		// Token: 0x040058BD RID: 22717
		private const float slowDown = 0.5f;

		// Token: 0x02000C44 RID: 3140
		private struct Lantern
		{
			// Token: 0x1700095A RID: 2394
			// (get) Token: 0x06005F9A RID: 24474 RVA: 0x006D07E0 File Offset: 0x006CE9E0
			// (set) Token: 0x06005F9B RID: 24475 RVA: 0x006D07E8 File Offset: 0x006CE9E8
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
					this.FrameHeight = value.Height;
				}
			}

			// Token: 0x1700095B RID: 2395
			// (get) Token: 0x06005F9C RID: 24476 RVA: 0x006D080B File Offset: 0x006CEA0B
			public float FloatAdjustedSpeed
			{
				get
				{
					return this.Speed * ((float)this.TimeUntilFloat / (float)this.TimeUntilFloatMax);
				}
			}

			// Token: 0x06005F9D RID: 24477 RVA: 0x006D0823 File Offset: 0x006CEA23
			public Rectangle GetSourceRectangle()
			{
				return new Rectangle(this.FrameWidth * this.Variant, 0, this.FrameWidth, this.FrameHeight);
			}

			// Token: 0x040078CB RID: 30923
			private const int MAX_FRAMES_X = 3;

			// Token: 0x040078CC RID: 30924
			public int Variant;

			// Token: 0x040078CD RID: 30925
			public int TimeUntilFloat;

			// Token: 0x040078CE RID: 30926
			public int TimeUntilFloatMax;

			// Token: 0x040078CF RID: 30927
			private Texture2D _texture;

			// Token: 0x040078D0 RID: 30928
			public Vector2 Position;

			// Token: 0x040078D1 RID: 30929
			public float Depth;

			// Token: 0x040078D2 RID: 30930
			public float Rotation;

			// Token: 0x040078D3 RID: 30931
			public int FrameHeight;

			// Token: 0x040078D4 RID: 30932
			public int FrameWidth;

			// Token: 0x040078D5 RID: 30933
			public float Speed;

			// Token: 0x040078D6 RID: 30934
			public bool Active;
		}
	}
}
