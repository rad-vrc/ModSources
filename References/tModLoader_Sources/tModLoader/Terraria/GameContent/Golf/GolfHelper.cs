using System;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Physics;

namespace Terraria.GameContent.Golf
{
	// Token: 0x0200061C RID: 1564
	public static class GolfHelper
	{
		// Token: 0x060044BD RID: 17597 RVA: 0x0060B761 File Offset: 0x00609961
		public static BallStepResult StepGolfBall(Entity entity, ref float angularVelocity)
		{
			return BallCollision.Step(GolfHelper.PhysicsProperties, entity, ref angularVelocity, GolfHelper.Listener);
		}

		// Token: 0x060044BE RID: 17598 RVA: 0x0060B774 File Offset: 0x00609974
		public static Vector2 FindVectorOnOval(Vector2 vector, Vector2 radius)
		{
			if (Math.Abs(radius.X) < 0.0001f || Math.Abs(radius.Y) < 0.0001f)
			{
				return Vector2.Zero;
			}
			return Vector2.Normalize(vector / radius) * radius;
		}

		// Token: 0x060044BF RID: 17599 RVA: 0x0060B7B4 File Offset: 0x006099B4
		public static GolfHelper.ShotStrength CalculateShotStrength(Vector2 shotVector, GolfHelper.ClubProperties clubProperties)
		{
			Vector2.Normalize(shotVector);
			float num3 = shotVector.Length();
			float num = GolfHelper.FindVectorOnOval(shotVector, clubProperties.MaximumStrength).Length();
			float num2 = GolfHelper.FindVectorOnOval(shotVector, clubProperties.MinimumStrength).Length();
			float num4 = MathHelper.Clamp(num3, num2, num);
			float relativeStrength = Math.Max((num4 - num2) / (num - num2), 0.001f);
			return new GolfHelper.ShotStrength(num4 * 32f, relativeStrength, clubProperties.RoughLandResistance);
		}

		// Token: 0x060044C0 RID: 17600 RVA: 0x0060B824 File Offset: 0x00609A24
		public static bool IsPlayerHoldingClub(Player player)
		{
			if (player == null || player.HeldItem == null)
			{
				return false;
			}
			int type = player.HeldItem.type;
			return type == 4039 || type - 4092 <= 2 || type - 4587 <= 11;
		}

		// Token: 0x060044C1 RID: 17601 RVA: 0x0060B86C File Offset: 0x00609A6C
		public static GolfHelper.ShotStrength CalculateShotStrength(Projectile golfHelper, Entity golfBall)
		{
			int num = Main.screenWidth;
			if (num > Main.screenHeight)
			{
				num = Main.screenHeight;
			}
			int num2 = 150;
			num -= num2;
			num /= 2;
			if (num < 200)
			{
				num = 200;
			}
			float num3 = (float)num;
			num3 = 300f;
			if (golfHelper.ai[0] != 0f)
			{
				return default(GolfHelper.ShotStrength);
			}
			Vector2 shotVector = (golfHelper.Center - golfBall.Center) / num3;
			GolfHelper.ClubProperties clubPropertiesFromGolfHelper = GolfHelper.GetClubPropertiesFromGolfHelper(golfHelper);
			return GolfHelper.CalculateShotStrength(shotVector, clubPropertiesFromGolfHelper);
		}

		// Token: 0x060044C2 RID: 17602 RVA: 0x0060B8F0 File Offset: 0x00609AF0
		public static GolfHelper.ClubProperties GetClubPropertiesFromGolfHelper(Projectile golfHelper)
		{
			return GolfHelper.GetClubProperties((short)Main.player[golfHelper.owner].HeldItem.type);
		}

		// Token: 0x060044C3 RID: 17603 RVA: 0x0060B910 File Offset: 0x00609B10
		public static GolfHelper.ClubProperties GetClubProperties(short itemId)
		{
			Vector2 vector;
			vector..ctor(0.25f, 0.25f);
			if (itemId == 4039)
			{
				return new GolfHelper.ClubProperties(vector, Vector2.One, 0f);
			}
			switch (itemId)
			{
			case 4092:
				return new GolfHelper.ClubProperties(Vector2.Zero, vector, 0f);
			case 4093:
				return new GolfHelper.ClubProperties(vector, new Vector2(0.65f, 1.5f), 1f);
			case 4094:
				return new GolfHelper.ClubProperties(vector, new Vector2(1.5f, 0.65f), 0f);
			default:
				switch (itemId)
				{
				case 4587:
					return new GolfHelper.ClubProperties(vector, Vector2.One, 0f);
				case 4588:
					return new GolfHelper.ClubProperties(Vector2.Zero, vector, 0f);
				case 4589:
					return new GolfHelper.ClubProperties(vector, new Vector2(0.65f, 1.5f), 1f);
				case 4590:
					return new GolfHelper.ClubProperties(vector, new Vector2(1.5f, 0.65f), 0f);
				case 4591:
					return new GolfHelper.ClubProperties(vector, Vector2.One, 0f);
				case 4592:
					return new GolfHelper.ClubProperties(Vector2.Zero, vector, 0f);
				case 4593:
					return new GolfHelper.ClubProperties(vector, new Vector2(0.65f, 1.5f), 1f);
				case 4594:
					return new GolfHelper.ClubProperties(vector, new Vector2(1.5f, 0.65f), 0f);
				case 4595:
					return new GolfHelper.ClubProperties(vector, Vector2.One, 0f);
				case 4596:
					return new GolfHelper.ClubProperties(Vector2.Zero, vector, 0f);
				case 4597:
					return new GolfHelper.ClubProperties(vector, new Vector2(0.65f, 1.5f), 1f);
				case 4598:
					return new GolfHelper.ClubProperties(vector, new Vector2(1.5f, 0.65f), 0f);
				default:
					return default(GolfHelper.ClubProperties);
				}
				break;
			}
		}

		// Token: 0x060044C4 RID: 17604 RVA: 0x0060BAF8 File Offset: 0x00609CF8
		public static Projectile FindHelperFromGolfBall(Projectile golfBall)
		{
			for (int i = 0; i < 1000; i++)
			{
				Projectile projectile = Main.projectile[i];
				if (projectile.active && projectile.type == 722 && projectile.owner == golfBall.owner)
				{
					return Main.projectile[i];
				}
			}
			return null;
		}

		// Token: 0x060044C5 RID: 17605 RVA: 0x0060BB4C File Offset: 0x00609D4C
		public static Projectile FindGolfBallForHelper(Projectile golfHelper)
		{
			for (int i = 0; i < 1000; i++)
			{
				Projectile projectile = Main.projectile[i];
				Vector2 shotVector = golfHelper.Center - projectile.Center;
				if (projectile.active && ProjectileID.Sets.IsAGolfBall[projectile.type] && projectile.owner == golfHelper.owner && GolfHelper.ValidateShot(projectile, Main.player[golfHelper.owner], ref shotVector))
				{
					return Main.projectile[i];
				}
			}
			return null;
		}

		// Token: 0x060044C6 RID: 17606 RVA: 0x0060BBC6 File Offset: 0x00609DC6
		public static bool IsGolfBallResting(Projectile golfBall)
		{
			return (int)golfBall.localAI[1] == 0 || Vector2.Distance(golfBall.position, golfBall.oldPos[golfBall.oldPos.Length - 1]) < 1f;
		}

		// Token: 0x060044C7 RID: 17607 RVA: 0x0060BBFC File Offset: 0x00609DFC
		public static bool IsGolfShotValid(Entity golfBall, Player player)
		{
			Vector2 vector = golfBall.Center - player.Bottom;
			if (player.direction == -1)
			{
				vector.X *= -1f;
			}
			return vector.X >= -16f && vector.X <= 32f && vector.Y <= 16f && vector.Y >= -16f;
		}

		// Token: 0x060044C8 RID: 17608 RVA: 0x0060BC70 File Offset: 0x00609E70
		public static bool ValidateShot(Entity golfBall, Player player, ref Vector2 shotVector)
		{
			Vector2 vector = golfBall.Center - player.Bottom;
			if (player.direction == -1)
			{
				vector.X *= -1f;
				shotVector.X *= -1f;
			}
			float num = shotVector.ToRotation();
			if (num > 0f)
			{
				shotVector = shotVector.Length() * new Vector2((float)Math.Cos(0.0), (float)Math.Sin(0.0));
			}
			else if (num < -1.5207964f)
			{
				shotVector = shotVector.Length() * new Vector2((float)Math.Cos(-1.5207964181900024), (float)Math.Sin(-1.5207964181900024));
			}
			if (player.direction == -1)
			{
				shotVector.X *= -1f;
			}
			return vector.X >= -16f && vector.X <= 32f && vector.Y <= 16f && vector.Y >= -16f;
		}

		// Token: 0x060044C9 RID: 17609 RVA: 0x0060BD90 File Offset: 0x00609F90
		public unsafe static void HitGolfBall(Entity entity, Vector2 velocity, float roughLandResistance)
		{
			Vector2 bottom = entity.Bottom;
			bottom.Y += 1f;
			Point point = bottom.ToTileCoordinates();
			Tile tile = Main.tile[point.X, point.Y];
			if (tile != null && tile.active())
			{
				TileMaterial byTileId = TileMaterials.GetByTileId(*tile.type);
				velocity = Vector2.Lerp(velocity * byTileId.GolfPhysics.ClubImpactDampening, velocity, byTileId.GolfPhysics.ImpactDampeningResistanceEfficiency * roughLandResistance);
			}
			entity.velocity = velocity;
			Projectile projectile = entity as Projectile;
			if (projectile != null)
			{
				projectile.timeLeft = 18000;
				if (projectile.ai[1] < 0f)
				{
					projectile.ai[1] = 0f;
				}
				projectile.ai[1] += 1f;
				projectile.localAI[1] = 1f;
				Main.LocalGolfState.RecordSwing(projectile);
			}
		}

		// Token: 0x060044CA RID: 17610 RVA: 0x0060BE80 File Offset: 0x0060A080
		public static void DrawPredictionLine(Entity golfBall, Vector2 impactVelocity, float chargeProgress, float roughLandResistance)
		{
			if (GolfHelper.PredictionLine == null)
			{
				GolfHelper.PredictionLine = new FancyGolfPredictionLine(20);
			}
			GolfHelper.PredictionLine.Update(golfBall, impactVelocity, roughLandResistance);
			GolfHelper.PredictionLine.Draw(Main.Camera, Main.spriteBatch, chargeProgress);
		}

		// Token: 0x04005A9E RID: 23198
		public const int PointsNeededForLevel1 = 500;

		// Token: 0x04005A9F RID: 23199
		public const int PointsNeededForLevel2 = 1000;

		// Token: 0x04005AA0 RID: 23200
		public const int PointsNeededForLevel3 = 2000;

		// Token: 0x04005AA1 RID: 23201
		public static readonly PhysicsProperties PhysicsProperties = new PhysicsProperties(0.3f, 0.99f);

		// Token: 0x04005AA2 RID: 23202
		public static readonly GolfHelper.ContactListener Listener = new GolfHelper.ContactListener();

		// Token: 0x04005AA3 RID: 23203
		public static FancyGolfPredictionLine PredictionLine;

		// Token: 0x02000CCA RID: 3274
		public struct ClubProperties
		{
			// Token: 0x06006174 RID: 24948 RVA: 0x006D3316 File Offset: 0x006D1516
			public ClubProperties(Vector2 minimumStrength, Vector2 maximumStrength, float roughLandResistance)
			{
				this.MinimumStrength = minimumStrength;
				this.MaximumStrength = maximumStrength;
				this.RoughLandResistance = roughLandResistance;
			}

			// Token: 0x040079FA RID: 31226
			public readonly Vector2 MinimumStrength;

			// Token: 0x040079FB RID: 31227
			public readonly Vector2 MaximumStrength;

			// Token: 0x040079FC RID: 31228
			public readonly float RoughLandResistance;
		}

		// Token: 0x02000CCB RID: 3275
		public struct ShotStrength
		{
			// Token: 0x06006175 RID: 24949 RVA: 0x006D332D File Offset: 0x006D152D
			public ShotStrength(float absoluteStrength, float relativeStrength, float roughLandResistance)
			{
				this.AbsoluteStrength = absoluteStrength;
				this.RelativeStrength = relativeStrength;
				this.RoughLandResistance = roughLandResistance;
			}

			// Token: 0x040079FD RID: 31229
			public readonly float AbsoluteStrength;

			// Token: 0x040079FE RID: 31230
			public readonly float RelativeStrength;

			// Token: 0x040079FF RID: 31231
			public readonly float RoughLandResistance;
		}

		// Token: 0x02000CCC RID: 3276
		public class ContactListener : IBallContactListener
		{
			// Token: 0x06006176 RID: 24950 RVA: 0x006D3344 File Offset: 0x006D1544
			public unsafe void OnCollision(PhysicsProperties properties, ref Vector2 position, ref Vector2 velocity, ref BallCollisionEvent collision)
			{
				TileMaterial byTileId = TileMaterials.GetByTileId(*collision.Tile.type);
				Vector2 vector = velocity * byTileId.GolfPhysics.SideImpactDampening;
				Vector2 vector2 = collision.Normal * Vector2.Dot(velocity, collision.Normal) * (byTileId.GolfPhysics.DirectImpactDampening - byTileId.GolfPhysics.SideImpactDampening);
				velocity = vector + vector2;
				Projectile projectile = collision.Entity as Projectile;
				ushort num4 = *collision.Tile.type;
				if (num4 - 421 > 1)
				{
					if (num4 == 476)
					{
						float num = velocity.Length() / collision.TimeScale;
						if (collision.Normal.Y <= -0.01f && num <= 100f)
						{
							velocity *= 0f;
							if (projectile != null && projectile.active)
							{
								this.PutBallInCup(projectile, collision);
							}
						}
					}
				}
				else
				{
					float num2 = 2.5f * collision.TimeScale;
					Vector2 vector3;
					vector3..ctor(0f - collision.Normal.Y, collision.Normal.X);
					if (*collision.Tile.type == 422)
					{
						vector3 = -vector3;
					}
					float num3 = Vector2.Dot(velocity, vector3);
					if (num3 < num2)
					{
						velocity += vector3 * MathHelper.Clamp(num2 - num3, 0f, num2 * 0.5f);
					}
				}
				if (projectile != null && velocity.Y < -0.3f && velocity.Y > -2f && velocity.Length() > 1f)
				{
					Dust dust = Dust.NewDustPerfect(collision.Entity.Center, 31, new Vector2?(collision.Normal), 127, default(Color), 1f);
					dust.scale = 0.7f;
					dust.fadeIn = 1f;
					dust.velocity = dust.velocity * 0.5f + Main.rand.NextVector2CircularEdge(0.5f, 0.4f);
				}
			}

			// Token: 0x06006177 RID: 24951 RVA: 0x006D3598 File Offset: 0x006D1798
			public void PutBallInCup(Projectile proj, BallCollisionEvent collision)
			{
				if (proj.owner == Main.myPlayer && Main.LocalGolfState.ShouldScoreHole)
				{
					Point hitLocation = (collision.ImpactPoint - collision.Normal * 0.5f).ToTileCoordinates();
					int owner = proj.owner;
					int num = (int)proj.ai[1];
					int type = proj.type;
					if (num > 1)
					{
						Main.LocalGolfState.SetScoreTime();
					}
					Main.LocalGolfState.RecordBallInfo(proj);
					Main.LocalGolfState.LandBall(proj);
					int golfBallScore = Main.LocalGolfState.GetGolfBallScore(proj);
					if (num > 0)
					{
						Main.player[owner].AccumulateGolfingScore(golfBallScore);
					}
					GolfHelper.ContactListener.PutBallInCup_TextAndEffects(hitLocation, owner, num, type);
					Main.LocalGolfState.ResetScoreTime();
					Wiring.HitSwitch(hitLocation.X, hitLocation.Y);
					NetMessage.SendData(59, -1, -1, null, hitLocation.X, (float)hitLocation.Y, 0f, 0f, 0, 0, 0);
					if (Main.netMode == 1)
					{
						NetMessage.SendData(128, -1, -1, null, owner, (float)num, (float)type, 0f, hitLocation.X, hitLocation.Y, 0);
					}
				}
				proj.Kill();
			}

			// Token: 0x06006178 RID: 24952 RVA: 0x006D36BC File Offset: 0x006D18BC
			public static void PutBallInCup_TextAndEffects(Point hitLocation, int plr, int numberOfHits, int projid)
			{
				if (numberOfHits != 0)
				{
					GolfHelper.ContactListener.EmitGolfballExplosion(hitLocation.ToWorldCoordinates(8f, 0f));
					string key = "Game.BallBounceResultGolf_Single";
					NetworkText networkText;
					if (numberOfHits != 1)
					{
						key = "Game.BallBounceResultGolf_Plural";
						networkText = NetworkText.FromKey(key, new object[]
						{
							Main.player[plr].name,
							NetworkText.FromKey(Lang.GetProjectileName(projid).Key, Array.Empty<object>()),
							numberOfHits
						});
					}
					else
					{
						networkText = NetworkText.FromKey(key, new object[]
						{
							Main.player[plr].name,
							NetworkText.FromKey(Lang.GetProjectileName(projid).Key, Array.Empty<object>())
						});
					}
					if (Main.netMode == 0 || Main.netMode == 1)
					{
						Main.NewText(networkText.ToString(), byte.MaxValue, 240, 20);
						return;
					}
					if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(networkText, new Color(255, 240, 20), -1);
					}
				}
			}

			// Token: 0x06006179 RID: 24953 RVA: 0x006D37B0 File Offset: 0x006D19B0
			public unsafe void OnPassThrough(PhysicsProperties properties, ref Vector2 position, ref Vector2 velocity, ref float angularVelocity, ref BallPassThroughEvent collision)
			{
				switch (collision.Type)
				{
				case BallPassThroughType.Water:
					velocity *= 0.91f;
					angularVelocity *= 0.91f;
					return;
				case BallPassThroughType.Honey:
					velocity *= 0.8f;
					angularVelocity *= 0.8f;
					return;
				case BallPassThroughType.Lava:
					break;
				case BallPassThroughType.Tile:
				{
					TileMaterial byTileId = TileMaterials.GetByTileId(*collision.Tile.type);
					velocity *= byTileId.GolfPhysics.PassThroughDampening;
					angularVelocity *= byTileId.GolfPhysics.PassThroughDampening;
					break;
				}
				default:
					return;
				}
			}

			// Token: 0x0600617A RID: 24954 RVA: 0x006D3865 File Offset: 0x006D1A65
			public static void EmitGolfballExplosion_Old(Vector2 Center)
			{
				GolfHelper.ContactListener.EmitGolfballExplosion(Center);
			}

			// Token: 0x0600617B RID: 24955 RVA: 0x006D3870 File Offset: 0x006D1A70
			public static void EmitGolfballExplosion(Vector2 Center)
			{
				SoundEngine.PlaySound(SoundID.Item129, new Vector2?(Center), null);
				for (float num = 0f; num < 1f; num += 0.085f)
				{
					Dust dust5 = Dust.NewDustPerfect(Center, 278, new Vector2?((num * 6.2831855f).ToRotationVector2() * new Vector2(2f, 0.5f)), 0, default(Color), 1f);
					dust5.fadeIn = 1.2f;
					dust5.noGravity = true;
					dust5.velocity.X = dust5.velocity.X * 0.7f;
					dust5.velocity.Y = dust5.velocity.Y - 1.5f;
					dust5.position.Y = dust5.position.Y + 8f;
					dust5.velocity.X = dust5.velocity.X * 2f;
					dust5.color = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.5f, byte.MaxValue);
				}
				float num2 = Main.rand.NextFloat();
				float num3 = (float)Main.rand.Next(5, 10);
				int i = 0;
				while ((float)i < num3)
				{
					int num4 = Main.rand.Next(5, 22);
					Vector2 value = (((float)i - num3 / 2f) * 6.2831855f / 256f - 1.5707964f).ToRotationVector2() * new Vector2(5f, 1f) * (0.25f + Main.rand.NextFloat() * 0.05f);
					Color color = Main.hslToRgb((num2 + (float)i / num3) % 1f, 0.7f, 0.7f, byte.MaxValue);
					color.A = 127;
					for (int j = 0; j < num4; j++)
					{
						Dust dust6 = Dust.NewDustPerfect(Center + new Vector2((float)i - num3 / 2f, 0f) * 2f, 278, new Vector2?(value), 0, default(Color), 1f);
						dust6.fadeIn = 0.7f;
						dust6.scale = 0.7f;
						dust6.noGravity = true;
						dust6.position.Y = dust6.position.Y + -1f;
						dust6.velocity *= (float)j;
						dust6.scale += 0.2f - (float)j * 0.03f;
						dust6.velocity += Main.rand.NextVector2Circular(0.05f, 0.05f);
						dust6.color = color;
					}
					i++;
				}
				for (float num5 = 0f; num5 < 1f; num5 += 0.2f)
				{
					Dust dust7 = Dust.NewDustPerfect(Center, 278, new Vector2?((num5 * 6.2831855f).ToRotationVector2() * new Vector2(1f, 0.5f)), 0, default(Color), 1f);
					dust7.fadeIn = 1.2f;
					dust7.noGravity = true;
					dust7.velocity.X = dust7.velocity.X * 0.7f;
					dust7.velocity.Y = dust7.velocity.Y - 0.5f;
					dust7.position.Y = dust7.position.Y + 8f;
					dust7.velocity.X = dust7.velocity.X * 2f;
					dust7.color = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.3f, byte.MaxValue);
				}
				float num6 = Main.rand.NextFloatDirection();
				for (float num7 = 0f; num7 < 1f; num7 += 0.15f)
				{
					Dust dust4 = Dust.NewDustPerfect(Center, 278, new Vector2?((num6 + num7 * 6.2831855f).ToRotationVector2() * 4f), 0, default(Color), 1f);
					dust4.fadeIn = 1.5f;
					dust4.velocity *= 0.5f + num7 * 0.8f;
					dust4.noGravity = true;
					Dust dust8 = dust4;
					dust8.velocity.X = dust8.velocity.X * 0.35f;
					Dust dust9 = dust4;
					dust9.velocity.Y = dust9.velocity.Y * 2f;
					Dust dust10 = dust4;
					dust10.velocity.Y = dust10.velocity.Y - 1f;
					dust4.velocity.Y = 0f - Math.Abs(dust4.velocity.Y);
					dust4.position += dust4.velocity * 3f;
					dust4.color = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.6f + Main.rand.NextFloat() * 0.2f, byte.MaxValue);
				}
			}

			// Token: 0x0600617C RID: 24956 RVA: 0x006D3D5C File Offset: 0x006D1F5C
			public static void EmitGolfballExplosion_v1(Vector2 Center)
			{
				for (float num = 0f; num < 1f; num += 0.085f)
				{
					Dust dust4 = Dust.NewDustPerfect(Center, 278, new Vector2?((num * 6.2831855f).ToRotationVector2() * new Vector2(2f, 0.5f)), 0, default(Color), 1f);
					dust4.fadeIn = 1.2f;
					dust4.noGravity = true;
					dust4.velocity.X = dust4.velocity.X * 0.7f;
					dust4.velocity.Y = dust4.velocity.Y - 1.5f;
					dust4.position.Y = dust4.position.Y + 8f;
					dust4.color = Color.Lerp(Color.Silver, Color.White, 0.5f);
				}
				for (float num2 = 0f; num2 < 1f; num2 += 0.2f)
				{
					Dust dust5 = Dust.NewDustPerfect(Center, 278, new Vector2?((num2 * 6.2831855f).ToRotationVector2() * new Vector2(1f, 0.5f)), 0, default(Color), 1f);
					dust5.fadeIn = 1.2f;
					dust5.noGravity = true;
					dust5.velocity.X = dust5.velocity.X * 0.7f;
					dust5.velocity.Y = dust5.velocity.Y - 0.5f;
					dust5.position.Y = dust5.position.Y + 8f;
					dust5.color = Color.Lerp(Color.Silver, Color.White, 0.5f);
				}
				float num3 = Main.rand.NextFloatDirection();
				for (float num4 = 0f; num4 < 1f; num4 += 0.15f)
				{
					Dust dust3 = Dust.NewDustPerfect(Center, 278, new Vector2?((num3 + num4 * 6.2831855f).ToRotationVector2() * 4f), 0, default(Color), 1f);
					dust3.fadeIn = 1.5f;
					dust3.velocity *= 0.5f + num4 * 0.8f;
					dust3.noGravity = true;
					Dust dust6 = dust3;
					dust6.velocity.X = dust6.velocity.X * 0.35f;
					Dust dust7 = dust3;
					dust7.velocity.Y = dust7.velocity.Y * 2f;
					Dust dust8 = dust3;
					dust8.velocity.Y = dust8.velocity.Y - 1f;
					dust3.velocity.Y = 0f - Math.Abs(dust3.velocity.Y);
					dust3.position += dust3.velocity * 3f;
					dust3.color = Color.Lerp(Color.Silver, Color.White, 0.5f);
				}
			}
		}
	}
}
