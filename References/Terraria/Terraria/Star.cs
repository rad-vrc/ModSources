﻿using System;
using Microsoft.Xna.Framework;
using Terraria.Utilities;

namespace Terraria
{
	// Token: 0x0200001D RID: 29
	public class Star
	{
		// Token: 0x06000132 RID: 306 RVA: 0x0000E068 File Offset: 0x0000C268
		public static void NightSetup()
		{
			Star.starfallBoost = 1f;
			int maxValue = 10;
			int maxValue2 = 3;
			if (Main.tenthAnniversaryWorld)
			{
				maxValue = 5;
				maxValue2 = 2;
			}
			if (Main.rand.Next(maxValue) == 0)
			{
				Star.starfallBoost = (float)Main.rand.Next(300, 501) * 0.01f;
			}
			else if (Main.rand.Next(maxValue2) == 0)
			{
				Star.starfallBoost = (float)Main.rand.Next(100, 151) * 0.01f;
			}
			Star.starFallCount = 0;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000E0F0 File Offset: 0x0000C2F0
		public static void StarFall(float positionX)
		{
			Star.starFallCount++;
			int num = -1;
			float num2 = -1f;
			float num3 = positionX / Main.rightWorld * 1920f;
			for (int i = 0; i < Main.numStars; i++)
			{
				if (!Main.star[i].hidden && !Main.star[i].falling)
				{
					float num4 = Math.Abs(Main.star[i].position.X - num3);
					if (num2 == -1f || num4 < num2)
					{
						num = i;
						num2 = num4;
					}
				}
			}
			if (num >= 0)
			{
				Main.star[num].Fall();
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000E188 File Offset: 0x0000C388
		public static void SpawnStars(int s = -1)
		{
			FastRandom fastRandom = FastRandom.CreateWithRandomSeed();
			int num = fastRandom.Next(200, 400);
			int num2 = 0;
			int num3 = num;
			if (s >= 0)
			{
				num2 = s;
				num3 = s + 1;
			}
			for (int i = num2; i < num3; i++)
			{
				Main.star[i] = new Star();
				if (s >= 0)
				{
					Main.star[i].fadeIn = 1f;
					int num4 = 10;
					int num5 = -2000;
					for (int j = 0; j < num4; j++)
					{
						float num6 = (float)fastRandom.Next(1921);
						int num7 = 2000;
						for (int k = 0; k < Main.numStars; k++)
						{
							if (k != s && !Main.star[k].hidden && !Main.star[k].falling)
							{
								int num8 = (int)Math.Abs(num6 - Main.star[k].position.X);
								if (num8 < num7)
								{
									num7 = num8;
								}
							}
						}
						if (s == 0 || num7 > num5)
						{
							num5 = num7;
							Main.star[i].position.X = num6;
						}
					}
				}
				else
				{
					Main.star[i].position.X = (float)fastRandom.Next(1921);
				}
				Main.star[i].position.Y = (float)fastRandom.Next(1201);
				Main.star[i].rotation = (float)fastRandom.Next(628) * 0.01f;
				Main.star[i].scale = (float)fastRandom.Next(70, 130) * 0.006f;
				Main.star[i].type = fastRandom.Next(0, 4);
				Main.star[i].twinkle = (float)fastRandom.Next(60, 101) * 0.01f;
				Main.star[i].twinkleSpeed = (float)fastRandom.Next(30, 110) * 0.0001f;
				if (fastRandom.Next(2) == 0)
				{
					Main.star[i].twinkleSpeed *= -1f;
				}
				Main.star[i].rotationSpeed = (float)fastRandom.Next(5, 50) * 0.0001f;
				if (fastRandom.Next(2) == 0)
				{
					Main.star[i].rotationSpeed *= -1f;
				}
				if (fastRandom.Next(40) == 0)
				{
					Main.star[i].scale *= 2f;
					Main.star[i].twinkleSpeed /= 2f;
					Main.star[i].rotationSpeed /= 2f;
				}
			}
			if (s == -1)
			{
				Main.numStars = num;
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000E448 File Offset: 0x0000C648
		public void Fall()
		{
			this.fallTime = 0;
			this.falling = true;
			this.fallSpeed.Y = (float)Main.rand.Next(700, 1001) * 0.01f;
			this.fallSpeed.X = (float)Main.rand.Next(-400, 401) * 0.01f;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000E4B0 File Offset: 0x0000C6B0
		public void Update()
		{
			if (this.falling && !this.hidden)
			{
				this.fallTime += Main.dayRate;
				this.position += this.fallSpeed * (float)(Main.dayRate + 99) / 100f;
				if (this.position.Y > 1500f)
				{
					this.hidden = true;
				}
				if (Main.starGame && this.position.Length() > 99999f)
				{
					this.hidden = true;
				}
				this.twinkle += this.twinkleSpeed * 3f;
				if (this.twinkle > 1f)
				{
					this.twinkle = 1f;
					this.twinkleSpeed *= -1f;
				}
				else if ((double)this.twinkle < 0.6)
				{
					this.twinkle = 0.6f;
					this.twinkleSpeed *= -1f;
				}
				this.rotation += 0.5f;
				if ((double)this.rotation > 6.28)
				{
					this.rotation -= 6.28f;
				}
				if (this.rotation < 0f)
				{
					this.rotation += 6.28f;
					return;
				}
			}
			else
			{
				if (this.fadeIn > 0f)
				{
					float num = 6.1728395E-05f * (float)Main.dayRate;
					num *= 10f;
					this.fadeIn -= num;
					if (this.fadeIn < 0f)
					{
						this.fadeIn = 0f;
					}
				}
				this.twinkle += this.twinkleSpeed;
				if (this.twinkle > 1f)
				{
					this.twinkle = 1f;
					this.twinkleSpeed *= -1f;
				}
				else if ((double)this.twinkle < 0.6)
				{
					this.twinkle = 0.6f;
					this.twinkleSpeed *= -1f;
				}
				this.rotation += this.rotationSpeed;
				if ((double)this.rotation > 6.28)
				{
					this.rotation -= 6.28f;
				}
				if (this.rotation < 0f)
				{
					this.rotation += 6.28f;
				}
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000E728 File Offset: 0x0000C928
		public static void UpdateStars()
		{
			if (!Main.dayTime)
			{
				Star.dayCheck = false;
			}
			else if (!Star.dayCheck && Main.time >= 27000.0)
			{
				for (int i = 0; i < Main.numStars; i++)
				{
					if (Main.star[i].hidden)
					{
						Star.SpawnStars(i);
					}
				}
			}
			for (int j = 0; j < Main.numStars; j++)
			{
				Main.star[j].Update();
			}
		}

		// Token: 0x040000E0 RID: 224
		public Vector2 position;

		// Token: 0x040000E1 RID: 225
		public float scale;

		// Token: 0x040000E2 RID: 226
		public float rotation;

		// Token: 0x040000E3 RID: 227
		public int type;

		// Token: 0x040000E4 RID: 228
		public float twinkle;

		// Token: 0x040000E5 RID: 229
		public float twinkleSpeed;

		// Token: 0x040000E6 RID: 230
		public float rotationSpeed;

		// Token: 0x040000E7 RID: 231
		public bool falling;

		// Token: 0x040000E8 RID: 232
		public bool hidden;

		// Token: 0x040000E9 RID: 233
		public Vector2 fallSpeed;

		// Token: 0x040000EA RID: 234
		public int fallTime;

		// Token: 0x040000EB RID: 235
		public static bool dayCheck = false;

		// Token: 0x040000EC RID: 236
		public static float starfallBoost = 1f;

		// Token: 0x040000ED RID: 237
		public static int starFallCount = 0;

		// Token: 0x040000EE RID: 238
		public float fadeIn;
	}
}
