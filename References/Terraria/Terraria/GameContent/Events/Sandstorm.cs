using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Events
{
	// Token: 0x020002AA RID: 682
	public class Sandstorm
	{
		// Token: 0x0600215D RID: 8541 RVA: 0x0052461B File Offset: 0x0052281B
		private static bool HasSufficientWind()
		{
			return Math.Abs(Main.windSpeedCurrent) >= 0.6f;
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x00524631 File Offset: 0x00522831
		public static void WorldClear()
		{
			Sandstorm.Happening = false;
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x0052463C File Offset: 0x0052283C
		public static void UpdateTime()
		{
			if (Main.netMode != 1)
			{
				if (Sandstorm.Happening)
				{
					if (Sandstorm.TimeLeft > 86400)
					{
						Sandstorm.TimeLeft = 0;
					}
					Sandstorm.TimeLeft -= Main.dayRate;
					if (!Sandstorm.HasSufficientWind())
					{
						Sandstorm.TimeLeft -= 15 * Main.dayRate;
					}
					if (Main.windSpeedCurrent == 0f)
					{
						Sandstorm.TimeLeft = 0;
					}
					if (Sandstorm.TimeLeft <= 0)
					{
						Sandstorm.StopSandstorm();
					}
				}
				else
				{
					int num = 21600;
					if (Main.hardMode)
					{
						num *= 2;
					}
					else
					{
						num *= 3;
					}
					if (Sandstorm.HasSufficientWind())
					{
						for (int i = 0; i < Main.dayRate; i++)
						{
							if (Main.rand.Next(num) == 0)
							{
								Sandstorm.StartSandstorm();
							}
						}
					}
				}
				if (Main.rand.Next(18000) == 0)
				{
					Sandstorm.ChangeSeverityIntentions();
				}
			}
			Sandstorm.UpdateSeverity();
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x00524714 File Offset: 0x00522914
		private static void ChangeSeverityIntentions()
		{
			if (Sandstorm.Happening)
			{
				Sandstorm.IntendedSeverity = 0.4f + Main.rand.NextFloat();
			}
			else if (Main.rand.Next(3) == 0)
			{
				Sandstorm.IntendedSeverity = 0f;
			}
			else
			{
				Sandstorm.IntendedSeverity = Main.rand.NextFloat() * 0.3f;
			}
			if (Main.netMode != 1)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x00524794 File Offset: 0x00522994
		private static void UpdateSeverity()
		{
			if (float.IsNaN(Sandstorm.Severity))
			{
				Sandstorm.Severity = 0f;
			}
			if (float.IsNaN(Sandstorm.IntendedSeverity))
			{
				Sandstorm.IntendedSeverity = 0f;
			}
			int num = Math.Sign(Sandstorm.IntendedSeverity - Sandstorm.Severity);
			Sandstorm.Severity = MathHelper.Clamp(Sandstorm.Severity + 0.003f * (float)num, 0f, 1f);
			int num2 = Math.Sign(Sandstorm.IntendedSeverity - Sandstorm.Severity);
			if (num != num2)
			{
				Sandstorm.Severity = Sandstorm.IntendedSeverity;
			}
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x0052481F File Offset: 0x00522A1F
		private static void StartSandstorm()
		{
			Sandstorm.Happening = true;
			Sandstorm.TimeLeft = Main.rand.Next(28800, 86401);
			Sandstorm.ChangeSeverityIntentions();
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x00524845 File Offset: 0x00522A45
		private static void StopSandstorm()
		{
			Sandstorm.Happening = false;
			Sandstorm.TimeLeft = 0;
			Sandstorm.ChangeSeverityIntentions();
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x00524858 File Offset: 0x00522A58
		public static void HandleEffectAndSky(bool toState)
		{
			if (toState == Sandstorm._effectsUp)
			{
				return;
			}
			Sandstorm._effectsUp = toState;
			Vector2 center = Main.player[Main.myPlayer].Center;
			if (Sandstorm._effectsUp)
			{
				SkyManager.Instance.Activate("Sandstorm", center, new object[0]);
				Filters.Scene.Activate("Sandstorm", center, new object[0]);
				Overlays.Scene.Activate("Sandstorm", center, new object[0]);
				return;
			}
			SkyManager.Instance.Deactivate("Sandstorm", new object[0]);
			Filters.Scene.Deactivate("Sandstorm", new object[0]);
			Overlays.Scene.Deactivate("Sandstorm", new object[0]);
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x00524911 File Offset: 0x00522B11
		public static bool ShouldSandstormDustPersist()
		{
			return Sandstorm.Happening && Main.player[Main.myPlayer].ZoneSandstorm && (Main.bgStyle == 2 || Main.bgStyle == 5) && Main.bgDelay < 50;
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x00524948 File Offset: 0x00522B48
		public static void EmitDust()
		{
			if (Main.gamePaused)
			{
				return;
			}
			int sandTileCount = Main.SceneMetrics.SandTileCount;
			Player player = Main.player[Main.myPlayer];
			bool flag = Sandstorm.ShouldSandstormDustPersist();
			Sandstorm.HandleEffectAndSky(flag && Main.UseStormEffects);
			if (sandTileCount < 100 || (double)player.position.Y > Main.worldSurface * 16.0 || player.ZoneBeach)
			{
				return;
			}
			int maxValue = 1;
			if (!flag)
			{
				return;
			}
			if (Main.rand.Next(maxValue) != 0)
			{
				return;
			}
			int num = Math.Sign(Main.windSpeedCurrent);
			float num2 = Math.Abs(Main.windSpeedCurrent);
			if (num2 < 0.01f)
			{
				return;
			}
			float num3 = (float)num * MathHelper.Lerp(0.9f, 1f, num2);
			float num4 = 2000f / (float)sandTileCount;
			float num5 = 3f / num4;
			num5 = MathHelper.Clamp(num5, 0.77f, 1f);
			int num6 = (int)num4;
			float num7 = (float)Main.screenWidth / (float)Main.maxScreenW;
			int num8 = (int)(1000f * num7);
			float num9 = 20f * Sandstorm.Severity;
			float num10 = (float)num8 * (Main.gfxQuality * 0.5f + 0.5f) + (float)num8 * 0.1f - (float)Dust.SandStormCount;
			if (num10 <= 0f)
			{
				return;
			}
			float num11 = (float)Main.screenWidth + 1000f;
			float num12 = (float)Main.screenHeight;
			Vector2 value = Main.screenPosition + player.velocity;
			WeightedRandom<Color> weightedRandom = new WeightedRandom<Color>();
			weightedRandom.Add(new Color(200, 160, 20, 180), (double)(Main.SceneMetrics.GetTileCount(53) + Main.SceneMetrics.GetTileCount(396) + Main.SceneMetrics.GetTileCount(397)));
			weightedRandom.Add(new Color(103, 98, 122, 180), (double)(Main.SceneMetrics.GetTileCount(112) + Main.SceneMetrics.GetTileCount(400) + Main.SceneMetrics.GetTileCount(398)));
			weightedRandom.Add(new Color(135, 43, 34, 180), (double)(Main.SceneMetrics.GetTileCount(234) + Main.SceneMetrics.GetTileCount(401) + Main.SceneMetrics.GetTileCount(399)));
			weightedRandom.Add(new Color(213, 196, 197, 180), (double)(Main.SceneMetrics.GetTileCount(116) + Main.SceneMetrics.GetTileCount(403) + Main.SceneMetrics.GetTileCount(402)));
			float num13 = MathHelper.Lerp(0.2f, 0.35f, Sandstorm.Severity);
			float num14 = MathHelper.Lerp(0.5f, 0.7f, Sandstorm.Severity);
			float amount = (num5 - 0.77f) / 0.23000002f;
			int maxValue2 = (int)MathHelper.Lerp(1f, 10f, amount);
			int num15 = 0;
			while ((float)num15 < num9)
			{
				if (Main.rand.Next(num6 / 4) == 0)
				{
					Vector2 vector = new Vector2(Main.rand.NextFloat() * num11 - 500f, Main.rand.NextFloat() * -50f);
					if (Main.rand.Next(3) == 0 && num == 1)
					{
						vector.X = (float)(Main.rand.Next(500) - 500);
					}
					else if (Main.rand.Next(3) == 0 && num == -1)
					{
						vector.X = (float)(Main.rand.Next(500) + Main.screenWidth);
					}
					if (vector.X < 0f || vector.X > (float)Main.screenWidth)
					{
						vector.Y += Main.rand.NextFloat() * num12 * 0.9f;
					}
					vector += value;
					int num16 = (int)vector.X / 16;
					int num17 = (int)vector.Y / 16;
					if (WorldGen.InWorld(num16, num17, 10) && Main.tile[num16, num17] != null && Main.tile[num16, num17].wall == 0)
					{
						for (int i = 0; i < 1; i++)
						{
							Dust dust = Main.dust[Dust.NewDust(vector, 10, 10, 268, 0f, 0f, 0, default(Color), 1f)];
							dust.velocity.Y = 2f + Main.rand.NextFloat() * 0.2f;
							Dust dust2 = dust;
							dust2.velocity.Y = dust2.velocity.Y * dust.scale;
							Dust dust3 = dust;
							dust3.velocity.Y = dust3.velocity.Y * 0.35f;
							dust.velocity.X = num3 * 5f + Main.rand.NextFloat() * 1f;
							Dust dust4 = dust;
							dust4.velocity.X = dust4.velocity.X + num3 * num14 * 20f;
							dust.fadeIn += num14 * 0.2f;
							dust.velocity *= 1f + num13 * 0.5f;
							dust.color = weightedRandom;
							dust.velocity *= 1f + num13;
							dust.velocity *= num5;
							dust.scale = 0.9f;
							num10 -= 1f;
							if (num10 <= 0f)
							{
								break;
							}
							if (Main.rand.Next(maxValue2) != 0)
							{
								i--;
								vector += Utils.RandomVector2(Main.rand, -10f, 10f) + dust.velocity * -1.1f;
								num16 = (int)vector.X / 16;
								num17 = (int)vector.Y / 16;
								if (WorldGen.InWorld(num16, num17, 10) && Main.tile[num16, num17] != null)
								{
									ushort wall = Main.tile[num16, num17].wall;
								}
							}
						}
						if (num10 <= 0f)
						{
							break;
						}
					}
				}
				num15++;
			}
		}

		// Token: 0x04004739 RID: 18233
		private const int SANDSTORM_DURATION_MINIMUM = 28800;

		// Token: 0x0400473A RID: 18234
		private const int SANDSTORM_DURATION_MAXIMUM = 86400;

		// Token: 0x0400473B RID: 18235
		public static bool Happening;

		// Token: 0x0400473C RID: 18236
		public static int TimeLeft;

		// Token: 0x0400473D RID: 18237
		public static float Severity;

		// Token: 0x0400473E RID: 18238
		public static float IntendedSeverity;

		// Token: 0x0400473F RID: 18239
		private static bool _effectsUp;
	}
}
