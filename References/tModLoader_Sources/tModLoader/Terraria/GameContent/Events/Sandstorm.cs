using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Events
{
	// Token: 0x02000631 RID: 1585
	public class Sandstorm
	{
		// Token: 0x06004587 RID: 17799 RVA: 0x006136B8 File Offset: 0x006118B8
		private static bool HasSufficientWind()
		{
			return Math.Abs(Main.windSpeedCurrent) >= 0.6f;
		}

		// Token: 0x06004588 RID: 17800 RVA: 0x006136CE File Offset: 0x006118CE
		public static void WorldClear()
		{
			Sandstorm.Happening = false;
		}

		// Token: 0x06004589 RID: 17801 RVA: 0x006136D8 File Offset: 0x006118D8
		public static void UpdateTime()
		{
			if (Main.netMode != 1)
			{
				if (Sandstorm.Happening)
				{
					if (Sandstorm.TimeLeft > 86400.0)
					{
						Sandstorm.TimeLeft = 0.0;
					}
					Sandstorm.TimeLeft -= Main.desiredWorldEventsUpdateRate;
					if (!Sandstorm.HasSufficientWind())
					{
						Sandstorm.TimeLeft -= 15.0 * Main.desiredWorldEventsUpdateRate;
					}
					if (Main.windSpeedCurrent == 0f)
					{
						Sandstorm.TimeLeft = 0.0;
					}
					if (Sandstorm.TimeLeft <= 0.0)
					{
						Sandstorm.StopSandstorm();
					}
				}
				else
				{
					int num = 21600;
					num = ((!Main.hardMode) ? (num * 3) : (num * 2));
					if (Sandstorm.HasSufficientWind())
					{
						for (int i = 0; i < Main.worldEventUpdates; i++)
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

		// Token: 0x0600458A RID: 17802 RVA: 0x006137D4 File Offset: 0x006119D4
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

		// Token: 0x0600458B RID: 17803 RVA: 0x00613854 File Offset: 0x00611A54
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

		/// <summary>
		/// Starts sandstorm for a random amount of time. Should be called on the server (netMode != client) - the method syncs it automatically.
		/// </summary>
		// Token: 0x0600458C RID: 17804 RVA: 0x006138DF File Offset: 0x00611ADF
		public static void StartSandstorm()
		{
			Sandstorm.Happening = true;
			Sandstorm.TimeLeft = (double)Main.rand.Next(28800, 86401);
			Sandstorm.ChangeSeverityIntentions();
		}

		/// <summary>
		/// Stops sandstorm. Should be called on the server (netMode != client) - the method syncs it automatically.
		/// </summary>
		// Token: 0x0600458D RID: 17805 RVA: 0x00613906 File Offset: 0x00611B06
		public static void StopSandstorm()
		{
			Sandstorm.Happening = false;
			Sandstorm.TimeLeft = 0.0;
			Sandstorm.ChangeSeverityIntentions();
		}

		// Token: 0x0600458E RID: 17806 RVA: 0x00613924 File Offset: 0x00611B24
		public static void HandleEffectAndSky(bool toState)
		{
			if (toState != Sandstorm._effectsUp)
			{
				Sandstorm._effectsUp = toState;
				Vector2 center = Main.player[Main.myPlayer].Center;
				if (Sandstorm._effectsUp)
				{
					SkyManager.Instance.Activate("Sandstorm", center, Array.Empty<object>());
					Filters.Scene.Activate("Sandstorm", center, Array.Empty<object>());
					Overlays.Scene.Activate("Sandstorm", center, Array.Empty<object>());
					return;
				}
				SkyManager.Instance.Deactivate("Sandstorm", Array.Empty<object>());
				Filters.Scene.Deactivate("Sandstorm", Array.Empty<object>());
				Overlays.Scene.Deactivate("Sandstorm", Array.Empty<object>());
			}
		}

		// Token: 0x0600458F RID: 17807 RVA: 0x006139D9 File Offset: 0x00611BD9
		public static bool ShouldSandstormDustPersist()
		{
			return Sandstorm.Happening && Main.player[Main.myPlayer].ZoneSandstorm && (Main.bgStyle == 2 || Main.bgStyle == 5) && Main.bgDelay < 50;
		}

		// Token: 0x06004590 RID: 17808 RVA: 0x00613A10 File Offset: 0x00611C10
		public unsafe static void EmitDust()
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
			if (!flag || Main.rand.Next(maxValue) != 0)
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
			float value = 3f / num4;
			value = MathHelper.Clamp(value, 0.77f, 1f);
			int num5 = (int)num4;
			float num6 = (float)Main.screenWidth / (float)Main.maxScreenW;
			int num7 = (int)(1000f * num6);
			float num8 = 20f * Sandstorm.Severity;
			float num9 = (float)num7 * (Main.gfxQuality * 0.5f + 0.5f) + (float)num7 * 0.1f - (float)Dust.SandStormCount;
			if (num9 <= 0f)
			{
				return;
			}
			float num10 = (float)Main.screenWidth + 1000f;
			float num11 = (float)Main.screenHeight;
			Vector2 vector = Main.screenPosition + player.velocity;
			WeightedRandom<Color> weightedRandom = new WeightedRandom<Color>();
			weightedRandom.Add(new Color(200, 160, 20, 180), (double)(Main.SceneMetrics.GetTileCount(53) + Main.SceneMetrics.GetTileCount(396) + Main.SceneMetrics.GetTileCount(397)));
			weightedRandom.Add(new Color(103, 98, 122, 180), (double)(Main.SceneMetrics.GetTileCount(112) + Main.SceneMetrics.GetTileCount(400) + Main.SceneMetrics.GetTileCount(398)));
			weightedRandom.Add(new Color(135, 43, 34, 180), (double)(Main.SceneMetrics.GetTileCount(234) + Main.SceneMetrics.GetTileCount(401) + Main.SceneMetrics.GetTileCount(399)));
			weightedRandom.Add(new Color(213, 196, 197, 180), (double)(Main.SceneMetrics.GetTileCount(116) + Main.SceneMetrics.GetTileCount(403) + Main.SceneMetrics.GetTileCount(402)));
			float num12 = MathHelper.Lerp(0.2f, 0.35f, Sandstorm.Severity);
			float num13 = MathHelper.Lerp(0.5f, 0.7f, Sandstorm.Severity);
			float amount = (value - 0.77f) / 0.23000002f;
			int maxValue2 = (int)MathHelper.Lerp(1f, 10f, amount);
			int i = 0;
			while ((float)i < num8)
			{
				if (Main.rand.Next(num5 / 4) == 0)
				{
					Vector2 position;
					position..ctor(Main.rand.NextFloat() * num10 - 500f, Main.rand.NextFloat() * -50f);
					if (Main.rand.Next(3) == 0 && num == 1)
					{
						position.X = (float)(Main.rand.Next(500) - 500);
					}
					else if (Main.rand.Next(3) == 0 && num == -1)
					{
						position.X = (float)(Main.rand.Next(500) + Main.screenWidth);
					}
					if (position.X < 0f || position.X > (float)Main.screenWidth)
					{
						position.Y += Main.rand.NextFloat() * num11 * 0.9f;
					}
					position += vector;
					int num14 = (int)position.X / 16;
					int num15 = (int)position.Y / 16;
					if (WorldGen.InWorld(num14, num15, 10) && !(Main.tile[num14, num15] == null) && *Main.tile[num14, num15].wall == 0)
					{
						for (int j = 0; j < 1; j++)
						{
							Dust dust = Main.dust[Dust.NewDust(position, 10, 10, 268, 0f, 0f, 0, default(Color), 1f)];
							dust.velocity.Y = 2f + Main.rand.NextFloat() * 0.2f;
							Dust dust2 = dust;
							dust2.velocity.Y = dust2.velocity.Y * dust.scale;
							Dust dust3 = dust;
							dust3.velocity.Y = dust3.velocity.Y * 0.35f;
							dust.velocity.X = num3 * 5f + Main.rand.NextFloat() * 1f;
							Dust dust4 = dust;
							dust4.velocity.X = dust4.velocity.X + num3 * num13 * 20f;
							dust.fadeIn += num13 * 0.2f;
							dust.velocity *= 1f + num12 * 0.5f;
							dust.color = weightedRandom;
							dust.velocity *= 1f + num12;
							dust.velocity *= value;
							dust.scale = 0.9f;
							num9 -= 1f;
							if (num9 <= 0f)
							{
								break;
							}
							if (Main.rand.Next(maxValue2) != 0)
							{
								j--;
								position += Utils.RandomVector2(Main.rand, -10f, 10f) + dust.velocity * -1.1f;
								num14 = (int)position.X / 16;
								num15 = (int)position.Y / 16;
								if (WorldGen.InWorld(num14, num15, 10) && Main.tile[num14, num15] != null)
								{
									ref ushort wall = ref Main.tile[num14, num15].wall;
								}
							}
						}
						if (num9 <= 0f)
						{
							break;
						}
					}
				}
				i++;
			}
		}

		// Token: 0x04005AFA RID: 23290
		private const int SANDSTORM_DURATION_MINIMUM = 28800;

		// Token: 0x04005AFB RID: 23291
		private const int SANDSTORM_DURATION_MAXIMUM = 86400;

		// Token: 0x04005AFC RID: 23292
		public static bool Happening;

		// Token: 0x04005AFD RID: 23293
		public static double TimeLeft;

		// Token: 0x04005AFE RID: 23294
		public static float Severity;

		// Token: 0x04005AFF RID: 23295
		public static float IntendedSeverity;

		// Token: 0x04005B00 RID: 23296
		private static bool _effectsUp;
	}
}
