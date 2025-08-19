using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x0200056A RID: 1386
	public class NebulaSky : CustomSky
	{
		// Token: 0x0600416E RID: 16750 RVA: 0x005E6B70 File Offset: 0x005E4D70
		public override void OnLoad()
		{
			this._planetTexture = Main.Assets.Request<Texture2D>("Images/Misc/NebulaSky/Planet");
			this._bgTexture = Main.Assets.Request<Texture2D>("Images/Misc/NebulaSky/Background");
			this._beamTexture = Main.Assets.Request<Texture2D>("Images/Misc/NebulaSky/Beam");
			this._rockTextures = new Asset<Texture2D>[3];
			for (int i = 0; i < this._rockTextures.Length; i++)
			{
				this._rockTextures[i] = Main.Assets.Request<Texture2D>("Images/Misc/NebulaSky/Rock_" + i.ToString());
			}
		}

		// Token: 0x0600416F RID: 16751 RVA: 0x005E6C00 File Offset: 0x005E4E00
		public override void Update(GameTime gameTime)
		{
			if (this._isActive)
			{
				this._fadeOpacity = Math.Min(1f, 0.01f + this._fadeOpacity);
				return;
			}
			this._fadeOpacity = Math.Max(0f, this._fadeOpacity - 0.01f);
		}

		// Token: 0x06004170 RID: 16752 RVA: 0x005E6C4E File Offset: 0x005E4E4E
		public override Color OnTileColor(Color inColor)
		{
			return new Color(Vector4.Lerp(inColor.ToVector4(), Vector4.One, this._fadeOpacity * 0.5f));
		}

		// Token: 0x06004171 RID: 16753 RVA: 0x005E6C74 File Offset: 0x005E4E74
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
			for (int i = 0; i < this._pillars.Length; i++)
			{
				float depth = this._pillars[i].Depth;
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
			Vector2 vector3 = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
			Rectangle rectangle;
			rectangle..ctor(-1000, -1000, 4000, 4000);
			float num3 = Math.Min(1f, (Main.screenPosition.Y - 1000f) / 1000f);
			for (int j = num; j < num2; j++)
			{
				Vector2 vector4;
				vector4..ctor(1f / this._pillars[j].Depth, 0.9f / this._pillars[j].Depth);
				Vector2 position = this._pillars[j].Position;
				position = (position - vector3) * vector4 + vector3 - Main.screenPosition;
				if (rectangle.Contains((int)position.X, (int)position.Y))
				{
					float num4 = vector4.X * 450f;
					spriteBatch.Draw(this._beamTexture.Value, position, null, Color.White * 0.2f * num3 * this._fadeOpacity, 0f, Vector2.Zero, new Vector2(num4 / 70f, num4 / 45f), 0, 0f);
					int num5 = 0;
					for (float num6 = 0f; num6 <= 1f; num6 += 0.03f)
					{
						float num7 = 1f - (num6 + Main.GlobalTimeWrappedHourly * 0.02f + (float)Math.Sin((double)j)) % 1f;
						spriteBatch.Draw(this._rockTextures[num5].Value, position + new Vector2((float)Math.Sin((double)(num6 * 1582f)) * (num4 * 0.5f) + num4 * 0.5f, num7 * 2000f), null, Color.White * num7 * num3 * this._fadeOpacity, num7 * 20f, new Vector2((float)(this._rockTextures[num5].Width() >> 1), (float)(this._rockTextures[num5].Height() >> 1)), 0.9f, 0, 0f);
						num5 = (num5 + 1) % this._rockTextures.Length;
					}
				}
			}
		}

		// Token: 0x06004172 RID: 16754 RVA: 0x005E70D7 File Offset: 0x005E52D7
		public override float GetCloudAlpha()
		{
			return (1f - this._fadeOpacity) * 0.3f + 0.7f;
		}

		// Token: 0x06004173 RID: 16755 RVA: 0x005E70F4 File Offset: 0x005E52F4
		public override void Activate(Vector2 position, params object[] args)
		{
			this._fadeOpacity = 0.002f;
			this._isActive = true;
			this._pillars = new NebulaSky.LightPillar[40];
			for (int i = 0; i < this._pillars.Length; i++)
			{
				this._pillars[i].Position.X = (float)i / (float)this._pillars.Length * ((float)Main.maxTilesX * 16f + 20000f) + this._random.NextFloat() * 40f - 20f - 20000f;
				this._pillars[i].Position.Y = this._random.NextFloat() * 200f - 2000f;
				this._pillars[i].Depth = this._random.NextFloat() * 8f + 7f;
			}
			Array.Sort<NebulaSky.LightPillar>(this._pillars, new Comparison<NebulaSky.LightPillar>(this.SortMethod));
		}

		// Token: 0x06004174 RID: 16756 RVA: 0x005E71F8 File Offset: 0x005E53F8
		private int SortMethod(NebulaSky.LightPillar pillar1, NebulaSky.LightPillar pillar2)
		{
			return pillar2.Depth.CompareTo(pillar1.Depth);
		}

		// Token: 0x06004175 RID: 16757 RVA: 0x005E720C File Offset: 0x005E540C
		public override void Deactivate(params object[] args)
		{
			this._isActive = false;
		}

		// Token: 0x06004176 RID: 16758 RVA: 0x005E7215 File Offset: 0x005E5415
		public override void Reset()
		{
			this._isActive = false;
		}

		// Token: 0x06004177 RID: 16759 RVA: 0x005E721E File Offset: 0x005E541E
		public override bool IsActive()
		{
			return this._isActive || this._fadeOpacity > 0.001f;
		}

		// Token: 0x040058C9 RID: 22729
		private NebulaSky.LightPillar[] _pillars;

		// Token: 0x040058CA RID: 22730
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x040058CB RID: 22731
		private Asset<Texture2D> _planetTexture;

		// Token: 0x040058CC RID: 22732
		private Asset<Texture2D> _bgTexture;

		// Token: 0x040058CD RID: 22733
		private Asset<Texture2D> _beamTexture;

		// Token: 0x040058CE RID: 22734
		private Asset<Texture2D>[] _rockTextures;

		// Token: 0x040058CF RID: 22735
		private bool _isActive;

		// Token: 0x040058D0 RID: 22736
		private float _fadeOpacity;

		// Token: 0x02000C4A RID: 3146
		private struct LightPillar
		{
			// Token: 0x040078ED RID: 30957
			public Vector2 Position;

			// Token: 0x040078EE RID: 30958
			public float Depth;
		}
	}
}
