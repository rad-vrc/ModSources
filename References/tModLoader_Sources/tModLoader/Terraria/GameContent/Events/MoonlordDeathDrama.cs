using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Utilities;

namespace Terraria.GameContent.Events
{
	// Token: 0x0200062F RID: 1583
	public class MoonlordDeathDrama
	{
		// Token: 0x06004573 RID: 17779 RVA: 0x00612C90 File Offset: 0x00610E90
		public static void Update()
		{
			for (int i = 0; i < MoonlordDeathDrama._pieces.Count; i++)
			{
				MoonlordDeathDrama.MoonlordPiece moonlordPiece = MoonlordDeathDrama._pieces[i];
				moonlordPiece.Update();
				if (moonlordPiece.Dead)
				{
					MoonlordDeathDrama._pieces.Remove(moonlordPiece);
					i--;
				}
			}
			for (int j = 0; j < MoonlordDeathDrama._explosions.Count; j++)
			{
				MoonlordDeathDrama.MoonlordExplosion moonlordExplosion = MoonlordDeathDrama._explosions[j];
				moonlordExplosion.Update();
				if (moonlordExplosion.Dead)
				{
					MoonlordDeathDrama._explosions.Remove(moonlordExplosion);
					j--;
				}
			}
			bool flag = false;
			for (int k = 0; k < MoonlordDeathDrama._lightSources.Count; k++)
			{
				if (Main.player[Main.myPlayer].Distance(MoonlordDeathDrama._lightSources[k]) < 2000f)
				{
					flag = true;
					break;
				}
			}
			MoonlordDeathDrama._lightSources.Clear();
			if (!flag)
			{
				MoonlordDeathDrama.requestedLight = 0f;
			}
			if (MoonlordDeathDrama.requestedLight != MoonlordDeathDrama.whitening)
			{
				if (Math.Abs(MoonlordDeathDrama.requestedLight - MoonlordDeathDrama.whitening) < 0.02f)
				{
					MoonlordDeathDrama.whitening = MoonlordDeathDrama.requestedLight;
				}
				else
				{
					MoonlordDeathDrama.whitening += (float)Math.Sign(MoonlordDeathDrama.requestedLight - MoonlordDeathDrama.whitening) * 0.02f;
				}
			}
			MoonlordDeathDrama.requestedLight = 0f;
		}

		// Token: 0x06004574 RID: 17780 RVA: 0x00612DD4 File Offset: 0x00610FD4
		public static void DrawPieces(SpriteBatch spriteBatch)
		{
			Rectangle playerScreen = Utils.CenteredRectangle(Main.screenPosition + new Vector2((float)Main.screenWidth, (float)Main.screenHeight) * 0.5f, new Vector2((float)(Main.screenWidth + 1000), (float)(Main.screenHeight + 1000)));
			for (int i = 0; i < MoonlordDeathDrama._pieces.Count; i++)
			{
				if (MoonlordDeathDrama._pieces[i].InDrawRange(playerScreen))
				{
					MoonlordDeathDrama._pieces[i].Draw(spriteBatch);
				}
			}
		}

		// Token: 0x06004575 RID: 17781 RVA: 0x00612E64 File Offset: 0x00611064
		public static void DrawExplosions(SpriteBatch spriteBatch)
		{
			Rectangle playerScreen = Utils.CenteredRectangle(Main.screenPosition + new Vector2((float)Main.screenWidth, (float)Main.screenHeight) * 0.5f, new Vector2((float)(Main.screenWidth + 1000), (float)(Main.screenHeight + 1000)));
			for (int i = 0; i < MoonlordDeathDrama._explosions.Count; i++)
			{
				if (MoonlordDeathDrama._explosions[i].InDrawRange(playerScreen))
				{
					MoonlordDeathDrama._explosions[i].Draw(spriteBatch);
				}
			}
		}

		// Token: 0x06004576 RID: 17782 RVA: 0x00612EF4 File Offset: 0x006110F4
		public static void DrawWhite(SpriteBatch spriteBatch)
		{
			if (MoonlordDeathDrama.whitening != 0f)
			{
				Color color = Color.White * MoonlordDeathDrama.whitening;
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(-2, -2, Main.screenWidth + 4, Main.screenHeight + 4), new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
			}
		}

		// Token: 0x06004577 RID: 17783 RVA: 0x00612F54 File Offset: 0x00611154
		public static void ThrowPieces(Vector2 MoonlordCoreCenter, int DramaSeed)
		{
			UnifiedRandom r = new UnifiedRandom(DramaSeed);
			Vector2 vector = Vector2.UnitY.RotatedBy((double)(r.NextFloat() * 1.5707964f - 0.7853982f + 3.1415927f), default(Vector2));
			MoonlordDeathDrama._pieces.Add(new MoonlordDeathDrama.MoonlordPiece(Main.Assets.Request<Texture2D>("Images/Misc/MoonExplosion/Spine").Value, new Vector2(64f, 150f), MoonlordCoreCenter + new Vector2(0f, 50f), vector * 6f, 0f, r.NextFloat() * 0.1f - 0.05f));
			vector = Vector2.UnitY.RotatedBy((double)(r.NextFloat() * 1.5707964f - 0.7853982f + 3.1415927f), default(Vector2));
			MoonlordDeathDrama._pieces.Add(new MoonlordDeathDrama.MoonlordPiece(Main.Assets.Request<Texture2D>("Images/Misc/MoonExplosion/Shoulder").Value, new Vector2(40f, 120f), MoonlordCoreCenter + new Vector2(50f, -120f), vector * 10f, 0f, r.NextFloat() * 0.1f - 0.05f));
			vector = Vector2.UnitY.RotatedBy((double)(r.NextFloat() * 1.5707964f - 0.7853982f + 3.1415927f), default(Vector2));
			MoonlordDeathDrama._pieces.Add(new MoonlordDeathDrama.MoonlordPiece(Main.Assets.Request<Texture2D>("Images/Misc/MoonExplosion/Torso").Value, new Vector2(192f, 252f), MoonlordCoreCenter, vector * 8f, 0f, r.NextFloat() * 0.1f - 0.05f));
			vector = Vector2.UnitY.RotatedBy((double)(r.NextFloat() * 1.5707964f - 0.7853982f + 3.1415927f), default(Vector2));
			MoonlordDeathDrama._pieces.Add(new MoonlordDeathDrama.MoonlordPiece(Main.Assets.Request<Texture2D>("Images/Misc/MoonExplosion/Head").Value, new Vector2(138f, 185f), MoonlordCoreCenter - new Vector2(0f, 200f), vector * 12f, 0f, r.NextFloat() * 0.1f - 0.05f));
		}

		// Token: 0x06004578 RID: 17784 RVA: 0x006131AC File Offset: 0x006113AC
		public static void AddExplosion(Vector2 spot)
		{
			MoonlordDeathDrama._explosions.Add(new MoonlordDeathDrama.MoonlordExplosion(Main.Assets.Request<Texture2D>("Images/Misc/MoonExplosion/Explosion").Value, spot, Main.rand.Next(2, 4)));
		}

		// Token: 0x06004579 RID: 17785 RVA: 0x006131DE File Offset: 0x006113DE
		public static void RequestLight(float light, Vector2 spot)
		{
			MoonlordDeathDrama._lightSources.Add(spot);
			if (light > 1f)
			{
				light = 1f;
			}
			if (MoonlordDeathDrama.requestedLight < light)
			{
				MoonlordDeathDrama.requestedLight = light;
			}
		}

		// Token: 0x04005AF1 RID: 23281
		private static List<MoonlordDeathDrama.MoonlordPiece> _pieces = new List<MoonlordDeathDrama.MoonlordPiece>();

		// Token: 0x04005AF2 RID: 23282
		private static List<MoonlordDeathDrama.MoonlordExplosion> _explosions = new List<MoonlordDeathDrama.MoonlordExplosion>();

		// Token: 0x04005AF3 RID: 23283
		private static List<Vector2> _lightSources = new List<Vector2>();

		// Token: 0x04005AF4 RID: 23284
		private static float whitening;

		// Token: 0x04005AF5 RID: 23285
		private static float requestedLight;

		// Token: 0x02000CD3 RID: 3283
		public class MoonlordPiece
		{
			// Token: 0x17000971 RID: 2417
			// (get) Token: 0x06006184 RID: 24964 RVA: 0x006D40B0 File Offset: 0x006D22B0
			public bool Dead
			{
				get
				{
					return this._position.Y > (float)(Main.maxTilesY * 16) - 480f || this._position.X < 480f || this._position.X >= (float)(Main.maxTilesX * 16) - 480f;
				}
			}

			// Token: 0x06006185 RID: 24965 RVA: 0x006D410C File Offset: 0x006D230C
			public MoonlordPiece(Texture2D pieceTexture, Vector2 textureOrigin, Vector2 centerPos, Vector2 velocity, float rot, float angularVelocity)
			{
				this._texture = pieceTexture;
				this._origin = textureOrigin;
				this._position = centerPos;
				this._velocity = velocity;
				this._rotation = rot;
				this._rotationVelocity = angularVelocity;
			}

			// Token: 0x06006186 RID: 24966 RVA: 0x006D4144 File Offset: 0x006D2344
			public void Update()
			{
				this._velocity.Y = this._velocity.Y + 0.3f;
				this._rotation += this._rotationVelocity;
				this._rotationVelocity *= 0.99f;
				this._position += this._velocity;
			}

			// Token: 0x06006187 RID: 24967 RVA: 0x006D41A4 File Offset: 0x006D23A4
			public void Draw(SpriteBatch sp)
			{
				Color light = this.GetLight();
				sp.Draw(this._texture, this._position - Main.screenPosition, null, light, this._rotation, this._origin, 1f, 0, 0f);
			}

			// Token: 0x06006188 RID: 24968 RVA: 0x006D41F5 File Offset: 0x006D23F5
			public bool InDrawRange(Rectangle playerScreen)
			{
				return playerScreen.Contains(this._position.ToPoint());
			}

			// Token: 0x06006189 RID: 24969 RVA: 0x006D420C File Offset: 0x006D240C
			public Color GetLight()
			{
				Vector3 zero = Vector3.Zero;
				float num = 0f;
				int num2 = 5;
				Point point = this._position.ToTileCoordinates();
				for (int i = point.X - num2; i <= point.X + num2; i++)
				{
					for (int j = point.Y - num2; j <= point.Y + num2; j++)
					{
						zero += Lighting.GetColor(i, j).ToVector3();
						num += 1f;
					}
				}
				if (num == 0f)
				{
					return Color.White;
				}
				return new Color(zero / num);
			}

			// Token: 0x04007A15 RID: 31253
			private Texture2D _texture;

			// Token: 0x04007A16 RID: 31254
			private Vector2 _position;

			// Token: 0x04007A17 RID: 31255
			private Vector2 _velocity;

			// Token: 0x04007A18 RID: 31256
			private Vector2 _origin;

			// Token: 0x04007A19 RID: 31257
			private float _rotation;

			// Token: 0x04007A1A RID: 31258
			private float _rotationVelocity;
		}

		// Token: 0x02000CD4 RID: 3284
		public class MoonlordExplosion
		{
			// Token: 0x17000972 RID: 2418
			// (get) Token: 0x0600618A RID: 24970 RVA: 0x006D42AC File Offset: 0x006D24AC
			public bool Dead
			{
				get
				{
					return this._position.Y > (float)(Main.maxTilesY * 16) - 480f || this._position.X < 480f || this._position.X >= (float)(Main.maxTilesX * 16) - 480f || this._frameCounter >= this._frameSpeed * 7;
				}
			}

			// Token: 0x0600618B RID: 24971 RVA: 0x006D4318 File Offset: 0x006D2518
			public MoonlordExplosion(Texture2D pieceTexture, Vector2 centerPos, int frameSpeed)
			{
				this._texture = pieceTexture;
				this._position = centerPos;
				this._frameSpeed = frameSpeed;
				this._frameCounter = 0;
				this._frame = this._texture.Frame(1, 7, 0, 0, 0, 0);
				this._origin = this._frame.Size() / 2f;
			}

			// Token: 0x0600618C RID: 24972 RVA: 0x006D4379 File Offset: 0x006D2579
			public void Update()
			{
				this._frameCounter++;
				this._frame = this._texture.Frame(1, 7, 0, this._frameCounter / this._frameSpeed, 0, 0);
			}

			// Token: 0x0600618D RID: 24973 RVA: 0x006D43AC File Offset: 0x006D25AC
			public void Draw(SpriteBatch sp)
			{
				Color light = this.GetLight();
				sp.Draw(this._texture, this._position - Main.screenPosition, new Rectangle?(this._frame), light, 0f, this._origin, 1f, 0, 0f);
			}

			// Token: 0x0600618E RID: 24974 RVA: 0x006D43FE File Offset: 0x006D25FE
			public bool InDrawRange(Rectangle playerScreen)
			{
				return playerScreen.Contains(this._position.ToPoint());
			}

			// Token: 0x0600618F RID: 24975 RVA: 0x006D4412 File Offset: 0x006D2612
			public Color GetLight()
			{
				return new Color(255, 255, 255, 127);
			}

			// Token: 0x04007A1B RID: 31259
			private Texture2D _texture;

			// Token: 0x04007A1C RID: 31260
			private Vector2 _position;

			// Token: 0x04007A1D RID: 31261
			private Vector2 _origin;

			// Token: 0x04007A1E RID: 31262
			private Rectangle _frame;

			// Token: 0x04007A1F RID: 31263
			private int _frameCounter;

			// Token: 0x04007A20 RID: 31264
			private int _frameSpeed;
		}
	}
}
