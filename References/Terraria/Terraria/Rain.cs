using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;

namespace Terraria
{
	// Token: 0x02000036 RID: 54
	public class Rain
	{
		// Token: 0x060003E8 RID: 1000 RVA: 0x0010CCD0 File Offset: 0x0010AED0
		public static void ClearRain()
		{
			for (int i = 0; i < Main.maxRain; i++)
			{
				Main.rain[i].active = false;
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0010CCFC File Offset: 0x0010AEFC
		public static void MakeRain()
		{
			if (Main.shimmerAlpha > 0f)
			{
				return;
			}
			if (Main.netMode == 2)
			{
				return;
			}
			if (Main.gamePaused)
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
			int num2 = 0;
			while ((float)num2 < num)
			{
				int num3 = 600;
				if (Main.player[Main.myPlayer].velocity.Y < 0f)
				{
					num3 += (int)(Math.Abs(Main.player[Main.myPlayer].velocity.Y) * 30f);
				}
				Vector2 vector;
				vector.X = (float)Main.rand.Next((int)Main.screenPosition.X - num3, (int)Main.screenPosition.X + Main.screenWidth + num3);
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
				int num4 = (int)vector.X / 16;
				int num5 = (int)vector.Y / 16;
				if (num4 < 0)
				{
					num4 = 0;
				}
				if (num4 > Main.maxTilesX - 1)
				{
					num4 = Main.maxTilesX - 1;
				}
				if (num5 < 0)
				{
					num5 = 0;
				}
				if (num5 > Main.maxTilesY - 1)
				{
					num5 = Main.maxTilesY - 1;
				}
				if (Main.remixWorld || Main.gameMenu || (!WorldGen.SolidTile(num4, num5, false) && Main.tile[num4, num5].wall <= 0))
				{
					Vector2 rainFallVelocity = Rain.GetRainFallVelocity();
					Rain.NewRain(vector, rainFallVelocity);
				}
				num2++;
			}
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0010CFAE File Offset: 0x0010B1AE
		public static Vector2 GetRainFallVelocity()
		{
			return new Vector2(Main.windSpeedCurrent * 18f, 14f);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0010CFC8 File Offset: 0x0010B1C8
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

		// Token: 0x060003EC RID: 1004 RVA: 0x0010D1C4 File Offset: 0x0010B3C4
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
			rain.rotation = (float)Math.Atan2((double)rain.velocity.X, (double)(-(double)rain.velocity.Y));
			rain.type = (byte)(Main.waterStyle * 3 + Main.rand.Next(3));
			return num;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0010D2B0 File Offset: 0x0010B4B0
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
			rain.rotation = (float)Math.Atan2((double)rain.velocity.X, (double)(-(double)rain.velocity.Y));
			rain.type = (byte)(Main.waterStyle * 3 + Main.rand.Next(3));
			return num;
		}

		// Token: 0x04000294 RID: 660
		public Vector2 position;

		// Token: 0x04000295 RID: 661
		public Vector2 velocity;

		// Token: 0x04000296 RID: 662
		public float scale;

		// Token: 0x04000297 RID: 663
		public float rotation;

		// Token: 0x04000298 RID: 664
		public int alpha;

		// Token: 0x04000299 RID: 665
		public bool active;

		// Token: 0x0400029A RID: 666
		public byte type;
	}
}
