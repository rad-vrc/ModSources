using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x0200032B RID: 811
	public class NebulaSky : CustomSky
	{
		// Token: 0x060024BB RID: 9403 RVA: 0x0055E1A8 File Offset: 0x0055C3A8
		public override void OnLoad()
		{
			this._planetTexture = Main.Assets.Request<Texture2D>("Images/Misc/NebulaSky/Planet", 1);
			this._bgTexture = Main.Assets.Request<Texture2D>("Images/Misc/NebulaSky/Background", 1);
			this._beamTexture = Main.Assets.Request<Texture2D>("Images/Misc/NebulaSky/Beam", 1);
			this._rockTextures = new Asset<Texture2D>[3];
			for (int i = 0; i < this._rockTextures.Length; i++)
			{
				this._rockTextures[i] = Main.Assets.Request<Texture2D>("Images/Misc/NebulaSky/Rock_" + i, 1);
			}
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x0055E23C File Offset: 0x0055C43C
		public override void Update(GameTime gameTime)
		{
			if (this._isActive)
			{
				this._fadeOpacity = Math.Min(1f, 0.01f + this._fadeOpacity);
				return;
			}
			this._fadeOpacity = Math.Max(0f, this._fadeOpacity - 0.01f);
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x0055E28A File Offset: 0x0055C48A
		public override Color OnTileColor(Color inColor)
		{
			return new Color(Vector4.Lerp(inColor.ToVector4(), Vector4.One, this._fadeOpacity * 0.5f));
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x0055E2B0 File Offset: 0x0055C4B0
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (maxDepth >= 3.4028235E+38f && minDepth < 3.4028235E+38f)
			{
				spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * this._fadeOpacity);
				spriteBatch.Draw(this._bgTexture.Value, new Rectangle(0, Math.Max(0, (int)((Main.worldSurface * 16.0 - (double)Main.screenPosition.Y - 2400.0) * 0.10000000149011612)), Main.screenWidth, Main.screenHeight), Color.White * Math.Min(1f, (Main.screenPosition.Y - 800f) / 1000f * this._fadeOpacity));
				Vector2 value = new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
				Vector2 value2 = 0.01f * (new Vector2((float)Main.maxTilesX * 8f, (float)Main.worldSurface / 2f) - Main.screenPosition);
				spriteBatch.Draw(this._planetTexture.Value, value + new Vector2(-200f, -200f) + value2, null, Color.White * 0.9f * this._fadeOpacity, 0f, new Vector2((float)(this._planetTexture.Width() >> 1), (float)(this._planetTexture.Height() >> 1)), 1f, SpriteEffects.None, 1f);
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
			Vector2 value3 = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
			Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
			float scale = Math.Min(1f, (Main.screenPosition.Y - 1000f) / 1000f);
			for (int j = num; j < num2; j++)
			{
				Vector2 vector = new Vector2(1f / this._pillars[j].Depth, 0.9f / this._pillars[j].Depth);
				Vector2 vector2 = this._pillars[j].Position;
				vector2 = (vector2 - value3) * vector + value3 - Main.screenPosition;
				if (rectangle.Contains((int)vector2.X, (int)vector2.Y))
				{
					float num3 = vector.X * 450f;
					spriteBatch.Draw(this._beamTexture.Value, vector2, null, Color.White * 0.2f * scale * this._fadeOpacity, 0f, Vector2.Zero, new Vector2(num3 / 70f, num3 / 45f), SpriteEffects.None, 0f);
					int num4 = 0;
					for (float num5 = 0f; num5 <= 1f; num5 += 0.03f)
					{
						float num6 = 1f - (num5 + Main.GlobalTimeWrappedHourly * 0.02f + (float)Math.Sin((double)j)) % 1f;
						spriteBatch.Draw(this._rockTextures[num4].Value, vector2 + new Vector2((float)Math.Sin((double)(num5 * 1582f)) * (num3 * 0.5f) + num3 * 0.5f, num6 * 2000f), null, Color.White * num6 * scale * this._fadeOpacity, num6 * 20f, new Vector2((float)(this._rockTextures[num4].Width() >> 1), (float)(this._rockTextures[num4].Height() >> 1)), 0.9f, SpriteEffects.None, 0f);
						num4 = (num4 + 1) % this._rockTextures.Length;
					}
				}
			}
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x0055E713 File Offset: 0x0055C913
		public override float GetCloudAlpha()
		{
			return (1f - this._fadeOpacity) * 0.3f + 0.7f;
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x0055E730 File Offset: 0x0055C930
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

		// Token: 0x060024C1 RID: 9409 RVA: 0x0055E834 File Offset: 0x0055CA34
		private int SortMethod(NebulaSky.LightPillar pillar1, NebulaSky.LightPillar pillar2)
		{
			return pillar2.Depth.CompareTo(pillar1.Depth);
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x0055E848 File Offset: 0x0055CA48
		public override void Deactivate(params object[] args)
		{
			this._isActive = false;
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x0055E848 File Offset: 0x0055CA48
		public override void Reset()
		{
			this._isActive = false;
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x0055E851 File Offset: 0x0055CA51
		public override bool IsActive()
		{
			return this._isActive || this._fadeOpacity > 0.001f;
		}

		// Token: 0x040048CD RID: 18637
		private NebulaSky.LightPillar[] _pillars;

		// Token: 0x040048CE RID: 18638
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x040048CF RID: 18639
		private Asset<Texture2D> _planetTexture;

		// Token: 0x040048D0 RID: 18640
		private Asset<Texture2D> _bgTexture;

		// Token: 0x040048D1 RID: 18641
		private Asset<Texture2D> _beamTexture;

		// Token: 0x040048D2 RID: 18642
		private Asset<Texture2D>[] _rockTextures;

		// Token: 0x040048D3 RID: 18643
		private bool _isActive;

		// Token: 0x040048D4 RID: 18644
		private float _fadeOpacity;

		// Token: 0x02000712 RID: 1810
		private struct LightPillar
		{
			// Token: 0x04006302 RID: 25346
			public Vector2 Position;

			// Token: 0x04006303 RID: 25347
			public float Depth;
		}
	}
}
