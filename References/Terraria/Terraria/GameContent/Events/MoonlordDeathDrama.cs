using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Utilities;

namespace Terraria.GameContent.Events
{
	// Token: 0x020002AB RID: 683
	public class MoonlordDeathDrama
	{
		// Token: 0x06002168 RID: 8552 RVA: 0x00524F84 File Offset: 0x00523184
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

		// Token: 0x06002169 RID: 8553 RVA: 0x005250C8 File Offset: 0x005232C8
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

		// Token: 0x0600216A RID: 8554 RVA: 0x00525158 File Offset: 0x00523358
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

		// Token: 0x0600216B RID: 8555 RVA: 0x005251E8 File Offset: 0x005233E8
		public static void DrawWhite(SpriteBatch spriteBatch)
		{
			if (MoonlordDeathDrama.whitening == 0f)
			{
				return;
			}
			Color color = Color.White * MoonlordDeathDrama.whitening;
			spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(-2, -2, Main.screenWidth + 4, Main.screenHeight + 4), new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x00525248 File Offset: 0x00523448
		public static void ThrowPieces(Vector2 MoonlordCoreCenter, int DramaSeed)
		{
			UnifiedRandom r = new UnifiedRandom(DramaSeed);
			Vector2 value = Vector2.UnitY.RotatedBy((double)(r.NextFloat() * 1.5707964f - 0.7853982f + 3.1415927f), default(Vector2));
			MoonlordDeathDrama._pieces.Add(new MoonlordDeathDrama.MoonlordPiece(Main.Assets.Request<Texture2D>("Images/Misc/MoonExplosion/Spine", 1).Value, new Vector2(64f, 150f), MoonlordCoreCenter + new Vector2(0f, 50f), value * 6f, 0f, r.NextFloat() * 0.1f - 0.05f));
			value = Vector2.UnitY.RotatedBy((double)(r.NextFloat() * 1.5707964f - 0.7853982f + 3.1415927f), default(Vector2));
			MoonlordDeathDrama._pieces.Add(new MoonlordDeathDrama.MoonlordPiece(Main.Assets.Request<Texture2D>("Images/Misc/MoonExplosion/Shoulder", 1).Value, new Vector2(40f, 120f), MoonlordCoreCenter + new Vector2(50f, -120f), value * 10f, 0f, r.NextFloat() * 0.1f - 0.05f));
			value = Vector2.UnitY.RotatedBy((double)(r.NextFloat() * 1.5707964f - 0.7853982f + 3.1415927f), default(Vector2));
			MoonlordDeathDrama._pieces.Add(new MoonlordDeathDrama.MoonlordPiece(Main.Assets.Request<Texture2D>("Images/Misc/MoonExplosion/Torso", 1).Value, new Vector2(192f, 252f), MoonlordCoreCenter, value * 8f, 0f, r.NextFloat() * 0.1f - 0.05f));
			value = Vector2.UnitY.RotatedBy((double)(r.NextFloat() * 1.5707964f - 0.7853982f + 3.1415927f), default(Vector2));
			MoonlordDeathDrama._pieces.Add(new MoonlordDeathDrama.MoonlordPiece(Main.Assets.Request<Texture2D>("Images/Misc/MoonExplosion/Head", 1).Value, new Vector2(138f, 185f), MoonlordCoreCenter - new Vector2(0f, 200f), value * 12f, 0f, r.NextFloat() * 0.1f - 0.05f));
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x005254A4 File Offset: 0x005236A4
		public static void AddExplosion(Vector2 spot)
		{
			MoonlordDeathDrama._explosions.Add(new MoonlordDeathDrama.MoonlordExplosion(Main.Assets.Request<Texture2D>("Images/Misc/MoonExplosion/Explosion", 1).Value, spot, Main.rand.Next(2, 4)));
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x005254D7 File Offset: 0x005236D7
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

		// Token: 0x04004740 RID: 18240
		private static List<MoonlordDeathDrama.MoonlordPiece> _pieces = new List<MoonlordDeathDrama.MoonlordPiece>();

		// Token: 0x04004741 RID: 18241
		private static List<MoonlordDeathDrama.MoonlordExplosion> _explosions = new List<MoonlordDeathDrama.MoonlordExplosion>();

		// Token: 0x04004742 RID: 18242
		private static List<Vector2> _lightSources = new List<Vector2>();

		// Token: 0x04004743 RID: 18243
		private static float whitening;

		// Token: 0x04004744 RID: 18244
		private static float requestedLight;

		// Token: 0x02000690 RID: 1680
		public class MoonlordPiece
		{
			// Token: 0x0600351C RID: 13596 RVA: 0x0060929B File Offset: 0x0060749B
			public MoonlordPiece(Texture2D pieceTexture, Vector2 textureOrigin, Vector2 centerPos, Vector2 velocity, float rot, float angularVelocity)
			{
				this._texture = pieceTexture;
				this._origin = textureOrigin;
				this._position = centerPos;
				this._velocity = velocity;
				this._rotation = rot;
				this._rotationVelocity = angularVelocity;
			}

			// Token: 0x0600351D RID: 13597 RVA: 0x006092D0 File Offset: 0x006074D0
			public void Update()
			{
				this._velocity.Y = this._velocity.Y + 0.3f;
				this._rotation += this._rotationVelocity;
				this._rotationVelocity *= 0.99f;
				this._position += this._velocity;
			}

			// Token: 0x0600351E RID: 13598 RVA: 0x00609330 File Offset: 0x00607530
			public void Draw(SpriteBatch sp)
			{
				Color light = this.GetLight();
				sp.Draw(this._texture, this._position - Main.screenPosition, null, light, this._rotation, this._origin, 1f, SpriteEffects.None, 0f);
			}

			// Token: 0x170003D4 RID: 980
			// (get) Token: 0x0600351F RID: 13599 RVA: 0x00609384 File Offset: 0x00607584
			public bool Dead
			{
				get
				{
					return this._position.Y > (float)(Main.maxTilesY * 16) - 480f || this._position.X < 480f || this._position.X >= (float)(Main.maxTilesX * 16) - 480f;
				}
			}

			// Token: 0x06003520 RID: 13600 RVA: 0x006093E0 File Offset: 0x006075E0
			public bool InDrawRange(Rectangle playerScreen)
			{
				return playerScreen.Contains(this._position.ToPoint());
			}

			// Token: 0x06003521 RID: 13601 RVA: 0x006093F4 File Offset: 0x006075F4
			public Color GetLight()
			{
				Vector3 vector = Vector3.Zero;
				float num = 0f;
				int num2 = 5;
				Point point = this._position.ToTileCoordinates();
				for (int i = point.X - num2; i <= point.X + num2; i++)
				{
					for (int j = point.Y - num2; j <= point.Y + num2; j++)
					{
						vector += Lighting.GetColor(i, j).ToVector3();
						num += 1f;
					}
				}
				if (num == 0f)
				{
					return Color.White;
				}
				return new Color(vector / num);
			}

			// Token: 0x0400618C RID: 24972
			private Texture2D _texture;

			// Token: 0x0400618D RID: 24973
			private Vector2 _position;

			// Token: 0x0400618E RID: 24974
			private Vector2 _velocity;

			// Token: 0x0400618F RID: 24975
			private Vector2 _origin;

			// Token: 0x04006190 RID: 24976
			private float _rotation;

			// Token: 0x04006191 RID: 24977
			private float _rotationVelocity;
		}

		// Token: 0x02000691 RID: 1681
		public class MoonlordExplosion
		{
			// Token: 0x06003522 RID: 13602 RVA: 0x00609494 File Offset: 0x00607694
			public MoonlordExplosion(Texture2D pieceTexture, Vector2 centerPos, int frameSpeed)
			{
				this._texture = pieceTexture;
				this._position = centerPos;
				this._frameSpeed = frameSpeed;
				this._frameCounter = 0;
				this._frame = this._texture.Frame(1, 7, 0, 0, 0, 0);
				this._origin = this._frame.Size() / 2f;
			}

			// Token: 0x06003523 RID: 13603 RVA: 0x006094F5 File Offset: 0x006076F5
			public void Update()
			{
				this._frameCounter++;
				this._frame = this._texture.Frame(1, 7, 0, this._frameCounter / this._frameSpeed, 0, 0);
			}

			// Token: 0x06003524 RID: 13604 RVA: 0x00609528 File Offset: 0x00607728
			public void Draw(SpriteBatch sp)
			{
				Color light = this.GetLight();
				sp.Draw(this._texture, this._position - Main.screenPosition, new Rectangle?(this._frame), light, 0f, this._origin, 1f, SpriteEffects.None, 0f);
			}

			// Token: 0x170003D5 RID: 981
			// (get) Token: 0x06003525 RID: 13605 RVA: 0x0060957C File Offset: 0x0060777C
			public bool Dead
			{
				get
				{
					return this._position.Y > (float)(Main.maxTilesY * 16) - 480f || this._position.X < 480f || this._position.X >= (float)(Main.maxTilesX * 16) - 480f || this._frameCounter >= this._frameSpeed * 7;
				}
			}

			// Token: 0x06003526 RID: 13606 RVA: 0x006095E8 File Offset: 0x006077E8
			public bool InDrawRange(Rectangle playerScreen)
			{
				return playerScreen.Contains(this._position.ToPoint());
			}

			// Token: 0x06003527 RID: 13607 RVA: 0x006095FC File Offset: 0x006077FC
			public Color GetLight()
			{
				return new Color(255, 255, 255, 127);
			}

			// Token: 0x04006192 RID: 24978
			private Texture2D _texture;

			// Token: 0x04006193 RID: 24979
			private Vector2 _position;

			// Token: 0x04006194 RID: 24980
			private Vector2 _origin;

			// Token: 0x04006195 RID: 24981
			private Rectangle _frame;

			// Token: 0x04006196 RID: 24982
			private int _frameCounter;

			// Token: 0x04006197 RID: 24983
			private int _frameSpeed;
		}
	}
}
