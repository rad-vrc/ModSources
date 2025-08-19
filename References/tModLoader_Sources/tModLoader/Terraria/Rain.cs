using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace Terraria
{
	// Token: 0x0200004A RID: 74
	public class Rain
	{
		// Token: 0x06000DF6 RID: 3574 RVA: 0x003B2E90 File Offset: 0x003B1090
		public static void ClearRain()
		{
			for (int i = 0; i < Main.maxRain; i++)
			{
				Main.rain[i].active = false;
			}
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x003B2EBC File Offset: 0x003B10BC
		public unsafe static void MakeRain()
		{
			if (Main.shimmerAlpha > 0f || Main.netMode == 2 || Main.gamePaused)
			{
				return;
			}
			if (Main.remixWorld)
			{
				if ((double)(Main.player[Main.myPlayer].position.Y / 16f) <= Main.rockLayer || Main.player[Main.myPlayer].position.Y / 16f >= (float)(Main.maxTilesY - 350) || Main.player[Main.myPlayer].ZoneDungeon)
				{
					return;
				}
			}
			else if ((double)Main.screenPosition.Y > Main.worldSurface * 16.0)
			{
				return;
			}
			if (Main.gameMenu)
			{
				return;
			}
			float num = (float)Main.screenWidth / 1920f;
			num *= 25f;
			num *= 0.25f + 1f * Main.cloudAlpha;
			if (Filters.Scene["Sandstorm"].IsActive())
			{
				return;
			}
			Vector2 vector = default(Vector2);
			int i = 0;
			while ((float)i < num)
			{
				int num2 = 600;
				if (Main.player[Main.myPlayer].velocity.Y < 0f)
				{
					num2 += (int)(Math.Abs(Main.player[Main.myPlayer].velocity.Y) * 30f);
				}
				vector.X = (float)Main.rand.Next((int)Main.screenPosition.X - num2, (int)Main.screenPosition.X + Main.screenWidth + num2);
				vector.Y = Main.screenPosition.Y - (float)Main.rand.Next(20, 100);
				vector.X -= Main.windSpeedCurrent * 15f * 40f;
				vector.X += Main.player[Main.myPlayer].velocity.X * 40f;
				if (vector.X < 0f)
				{
					vector.X = 0f;
				}
				if (vector.X > (float)((Main.maxTilesX - 1) * 16))
				{
					vector.X = (float)((Main.maxTilesX - 1) * 16);
				}
				int num3 = (int)vector.X / 16;
				int num4 = (int)vector.Y / 16;
				if (num3 < 0)
				{
					num3 = 0;
				}
				if (num3 > Main.maxTilesX - 1)
				{
					num3 = Main.maxTilesX - 1;
				}
				if (num4 < 0)
				{
					num4 = 0;
				}
				if (num4 > Main.maxTilesY - 1)
				{
					num4 = Main.maxTilesY - 1;
				}
				if (Main.remixWorld || Main.gameMenu || (!WorldGen.SolidTile(num3, num4, false) && *Main.tile[num3, num4].wall <= 0))
				{
					Vector2 rainFallVelocity = Rain.GetRainFallVelocity();
					Rain.NewRain(vector, rainFallVelocity);
				}
				i++;
			}
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x003B3179 File Offset: 0x003B1379
		public static Vector2 GetRainFallVelocity()
		{
			return new Vector2(Main.windSpeedCurrent * 18f, 14f);
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x003B3190 File Offset: 0x003B1390
		public void Update()
		{
			if (Main.gamePaused)
			{
				return;
			}
			this.position += this.velocity;
			if (Main.gameMenu)
			{
				if (this.position.Y > Main.screenPosition.Y + (float)Main.screenHeight + 2000f)
				{
					this.active = false;
					return;
				}
			}
			else if (Main.remixWorld)
			{
				if (this.position.Y > Main.screenPosition.Y + (float)Main.screenHeight + 100f)
				{
					this.active = false;
					return;
				}
			}
			else if (Collision.SolidCollision(this.position, 2, 2) || this.position.Y > Main.screenPosition.Y + (float)Main.screenHeight + 100f || Collision.WetCollision(this.position, 2, 2))
			{
				this.active = false;
				if ((float)Main.rand.Next(100) < Main.gfxQuality * 100f)
				{
					int num = Dust.NewDust(this.position - this.velocity, 2, 2, Dust.dustWater(), 0f, 0f, 0, default(Color), 1f);
					Dust dust = Main.dust[num];
					dust.position.X = dust.position.X - 2f;
					Dust dust2 = Main.dust[num];
					dust2.position.Y = dust2.position.Y + 2f;
					Main.dust[num].alpha = 38;
					Main.dust[num].velocity *= 0.1f;
					Main.dust[num].velocity += -this.velocity * 0.025f;
					Dust dust3 = Main.dust[num];
					dust3.velocity.Y = dust3.velocity.Y - 2f;
					Main.dust[num].scale = 0.6f;
					Main.dust[num].noGravity = true;
				}
			}
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x003B338C File Offset: 0x003B158C
		public static int NewRainForced(Vector2 Position, Vector2 Velocity)
		{
			int num = -1;
			int num2 = Main.maxRain;
			float num3 = (1f + Main.gfxQuality) / 2f;
			if (num3 < 0.9f)
			{
				num2 = (int)((float)num2 * num3);
			}
			for (int i = 0; i < num2; i++)
			{
				if (!Main.rain[i].active)
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				return Main.maxRain;
			}
			Rain rain = Main.rain[num];
			rain.active = true;
			rain.position = Position;
			rain.scale = 1f + (float)Main.rand.Next(-20, 21) * 0.01f;
			rain.velocity = Velocity * rain.scale;
			rain.rotation = (float)Math.Atan2((double)rain.velocity.X, (double)(0f - rain.velocity.Y));
			rain.waterStyle = (byte)Main.waterStyle;
			if (Main.waterStyle >= 15)
			{
				rain.type = LoaderManager.Get<WaterStylesLoader>().Get(Main.waterStyle).GetRainVariant();
				return num;
			}
			rain.type = (byte)(Main.waterStyle * 3 + Main.rand.Next(3));
			return num;
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x003B34B0 File Offset: 0x003B16B0
		private static int NewRain(Vector2 Position, Vector2 Velocity)
		{
			int num = -1;
			int num2 = (int)((float)Main.maxRain * Main.cloudAlpha);
			if (num2 > Main.maxRain)
			{
				num2 = Main.maxRain;
			}
			float num3 = (float)Main.maxTilesX / 6400f;
			Math.Max(0f, Math.Min(1f, (Main.player[Main.myPlayer].position.Y / 16f - 85f * num3) / (60f * num3)));
			float num4 = (1f + Main.gfxQuality) / 2f;
			if ((double)num4 < 0.9)
			{
				num2 = (int)((float)num2 * num4);
			}
			float num5 = (float)(800 - Main.SceneMetrics.SnowTileCount);
			if (num5 < 0f)
			{
				num5 = 0f;
			}
			num5 /= 800f;
			num2 = (int)((float)num2 * num5);
			num2 = (int)((double)num2 * Math.Pow((double)Main.atmo, 9.0));
			if ((double)Main.atmo < 0.4)
			{
				num2 = 0;
			}
			for (int i = 0; i < num2; i++)
			{
				if (!Main.rain[i].active)
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				return Main.maxRain;
			}
			Rain rain = Main.rain[num];
			rain.active = true;
			rain.position = Position;
			rain.scale = 1f + (float)Main.rand.Next(-20, 21) * 0.01f;
			rain.velocity = Velocity * rain.scale;
			rain.rotation = (float)Math.Atan2((double)rain.velocity.X, (double)(0f - rain.velocity.Y));
			rain.waterStyle = (byte)Main.waterStyle;
			if (Main.waterStyle >= 15)
			{
				rain.type = LoaderManager.Get<WaterStylesLoader>().Get(Main.waterStyle).GetRainVariant();
				return num;
			}
			rain.type = (byte)(Main.waterStyle * 3 + Main.rand.Next(3));
			return num;
		}

		// Token: 0x04000E1C RID: 3612
		public Vector2 position;

		// Token: 0x04000E1D RID: 3613
		public Vector2 velocity;

		// Token: 0x04000E1E RID: 3614
		public float scale;

		// Token: 0x04000E1F RID: 3615
		public float rotation;

		// Token: 0x04000E20 RID: 3616
		public int alpha;

		// Token: 0x04000E21 RID: 3617
		public bool active;

		// Token: 0x04000E22 RID: 3618
		public byte type;

		// Token: 0x04000E23 RID: 3619
		public byte waterStyle;
	}
}
