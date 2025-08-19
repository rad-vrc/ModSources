using System;
using Microsoft.Xna.Framework;
using Terraria.Utilities;

namespace Terraria
{
	// Token: 0x02000055 RID: 85
	public class Star
	{
		// Token: 0x06000F04 RID: 3844 RVA: 0x003FB668 File Offset: 0x003F9868
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

		// Token: 0x06000F05 RID: 3845 RVA: 0x003FB6F0 File Offset: 0x003F98F0
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

		// Token: 0x06000F06 RID: 3846 RVA: 0x003FB788 File Offset: 0x003F9988
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

		// Token: 0x06000F07 RID: 3847 RVA: 0x003FBA48 File Offset: 0x003F9C48
		public void Fall()
		{
			this.fallTime = 0.0;
			this.falling = true;
			this.fallSpeed.Y = (float)Main.rand.Next(700, 1001) * 0.01f;
			this.fallSpeed.X = (float)Main.rand.Next(-400, 401) * 0.01f;
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x003FBAB8 File Offset: 0x003F9CB8
		public void Update()
		{
			if (this.falling && !this.hidden)
			{
				this.fallTime += Main.desiredWorldEventsUpdateRate;
				this.position += this.fallSpeed * (float)(Main.desiredWorldEventsUpdateRate + 99.0) / 100f;
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
				}
				return;
			}
			if (this.fadeIn > 0f)
			{
				float num = 6.1728395E-05f * (float)Main.desiredWorldEventsUpdateRate;
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

		// Token: 0x06000F09 RID: 3849 RVA: 0x003FBD34 File Offset: 0x003F9F34
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

		// Token: 0x04000EB2 RID: 3762
		public Vector2 position;

		// Token: 0x04000EB3 RID: 3763
		public float scale;

		// Token: 0x04000EB4 RID: 3764
		public float rotation;

		// Token: 0x04000EB5 RID: 3765
		public int type;

		// Token: 0x04000EB6 RID: 3766
		public float twinkle;

		// Token: 0x04000EB7 RID: 3767
		public float twinkleSpeed;

		// Token: 0x04000EB8 RID: 3768
		public float rotationSpeed;

		// Token: 0x04000EB9 RID: 3769
		public bool falling;

		// Token: 0x04000EBA RID: 3770
		public bool hidden;

		// Token: 0x04000EBB RID: 3771
		public Vector2 fallSpeed;

		// Token: 0x04000EBC RID: 3772
		public double fallTime;

		// Token: 0x04000EBD RID: 3773
		public static bool dayCheck = false;

		// Token: 0x04000EBE RID: 3774
		public static float starfallBoost = 1f;

		// Token: 0x04000EBF RID: 3775
		public static int starFallCount = 0;

		// Token: 0x04000EC0 RID: 3776
		public float fadeIn;
	}
}
