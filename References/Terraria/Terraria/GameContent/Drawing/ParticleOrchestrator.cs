using System;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.GameContent.NetModules;
using Terraria.Graphics.Renderers;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Net;

namespace Terraria.GameContent.Drawing
{
	// Token: 0x020002AF RID: 687
	public class ParticleOrchestrator
	{
		// Token: 0x06002180 RID: 8576 RVA: 0x00525C34 File Offset: 0x00523E34
		public static void RequestParticleSpawn(bool clientOnly, ParticleOrchestraType type, ParticleOrchestraSettings settings, int? overrideInvokingPlayerIndex = null)
		{
			settings.IndexOfPlayerWhoInvokedThis = (byte)Main.myPlayer;
			if (overrideInvokingPlayerIndex != null)
			{
				settings.IndexOfPlayerWhoInvokedThis = (byte)overrideInvokingPlayerIndex.Value;
			}
			if (clientOnly)
			{
				ParticleOrchestrator.SpawnParticlesDirect(type, settings);
				return;
			}
			NetManager.Instance.SendToServerAndSelf(NetParticlesModule.Serialize(type, settings));
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x00525C82 File Offset: 0x00523E82
		public static void BroadcastParticleSpawn(ParticleOrchestraType type, ParticleOrchestraSettings settings)
		{
			settings.IndexOfPlayerWhoInvokedThis = (byte)Main.myPlayer;
			NetManager.Instance.BroadcastOrLoopback(NetParticlesModule.Serialize(type, settings));
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x00525CA2 File Offset: 0x00523EA2
		public static void BroadcastOrRequestParticleSpawn(ParticleOrchestraType type, ParticleOrchestraSettings settings)
		{
			settings.IndexOfPlayerWhoInvokedThis = (byte)Main.myPlayer;
			if (Main.netMode == 1)
			{
				NetManager.Instance.SendToServerAndSelf(NetParticlesModule.Serialize(type, settings));
				return;
			}
			NetManager.Instance.BroadcastOrLoopback(NetParticlesModule.Serialize(type, settings));
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x00525CDC File Offset: 0x00523EDC
		private static FadingParticle GetNewFadingParticle()
		{
			return new FadingParticle();
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x00525CE3 File Offset: 0x00523EE3
		private static LittleFlyingCritterParticle GetNewPooFlyParticle()
		{
			return new LittleFlyingCritterParticle();
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x00525CEA File Offset: 0x00523EEA
		private static ItemTransferParticle GetNewItemTransferParticle()
		{
			return new ItemTransferParticle();
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x00525CF1 File Offset: 0x00523EF1
		private static FlameParticle GetNewFlameParticle()
		{
			return new FlameParticle();
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x00525CF8 File Offset: 0x00523EF8
		private static RandomizedFrameParticle GetNewRandomizedFrameParticle()
		{
			return new RandomizedFrameParticle();
		}

		// Token: 0x06002188 RID: 8584 RVA: 0x00525CFF File Offset: 0x00523EFF
		private static PrettySparkleParticle GetNewPrettySparkleParticle()
		{
			return new PrettySparkleParticle();
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x00525D06 File Offset: 0x00523F06
		private static GasParticle GetNewGasParticle()
		{
			return new GasParticle();
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x00525D10 File Offset: 0x00523F10
		public static void SpawnParticlesDirect(ParticleOrchestraType type, ParticleOrchestraSettings settings)
		{
			if (Main.netMode == 2)
			{
				return;
			}
			switch (type)
			{
			case ParticleOrchestraType.Keybrand:
				ParticleOrchestrator.Spawn_Keybrand(settings);
				return;
			case ParticleOrchestraType.FlameWaders:
				ParticleOrchestrator.Spawn_FlameWaders(settings);
				return;
			case ParticleOrchestraType.StellarTune:
				ParticleOrchestrator.Spawn_StellarTune(settings);
				return;
			case ParticleOrchestraType.WallOfFleshGoatMountFlames:
				ParticleOrchestrator.Spawn_WallOfFleshGoatMountFlames(settings);
				return;
			case ParticleOrchestraType.BlackLightningHit:
				ParticleOrchestrator.Spawn_BlackLightningHit(settings);
				return;
			case ParticleOrchestraType.RainbowRodHit:
				ParticleOrchestrator.Spawn_RainbowRodHit(settings);
				return;
			case ParticleOrchestraType.BlackLightningSmall:
				ParticleOrchestrator.Spawn_BlackLightningSmall(settings);
				return;
			case ParticleOrchestraType.StardustPunch:
				ParticleOrchestrator.Spawn_StardustPunch(settings);
				return;
			case ParticleOrchestraType.PrincessWeapon:
				ParticleOrchestrator.Spawn_PrincessWeapon(settings);
				return;
			case ParticleOrchestraType.PaladinsHammer:
				ParticleOrchestrator.Spawn_PaladinsHammer(settings);
				return;
			case ParticleOrchestraType.NightsEdge:
				ParticleOrchestrator.Spawn_NightsEdge(settings);
				return;
			case ParticleOrchestraType.SilverBulletSparkle:
				ParticleOrchestrator.Spawn_SilverBulletSparkle(settings);
				return;
			case ParticleOrchestraType.TrueNightsEdge:
				ParticleOrchestrator.Spawn_TrueNightsEdge(settings);
				return;
			case ParticleOrchestraType.Excalibur:
				ParticleOrchestrator.Spawn_Excalibur(settings);
				return;
			case ParticleOrchestraType.TrueExcalibur:
				ParticleOrchestrator.Spawn_TrueExcalibur(settings);
				return;
			case ParticleOrchestraType.TerraBlade:
				ParticleOrchestrator.Spawn_TerraBlade(settings);
				return;
			case ParticleOrchestraType.ChlorophyteLeafCrystalPassive:
				ParticleOrchestrator.Spawn_LeafCrystalPassive(settings);
				return;
			case ParticleOrchestraType.ChlorophyteLeafCrystalShot:
				ParticleOrchestrator.Spawn_LeafCrystalShot(settings);
				return;
			case ParticleOrchestraType.AshTreeShake:
				ParticleOrchestrator.Spawn_AshTreeShake(settings);
				return;
			case ParticleOrchestraType.PetExchange:
				ParticleOrchestrator.Spawn_PetExchange(settings);
				return;
			case ParticleOrchestraType.SlapHand:
				ParticleOrchestrator.Spawn_SlapHand(settings);
				return;
			case ParticleOrchestraType.FlyMeal:
				ParticleOrchestrator.Spawn_FlyMeal(settings);
				return;
			case ParticleOrchestraType.GasTrap:
				ParticleOrchestrator.Spawn_GasTrap(settings);
				return;
			case ParticleOrchestraType.ItemTransfer:
				ParticleOrchestrator.Spawn_ItemTransfer(settings);
				return;
			case ParticleOrchestraType.ShimmerArrow:
				ParticleOrchestrator.Spawn_ShimmerArrow(settings);
				return;
			case ParticleOrchestraType.TownSlimeTransform:
				ParticleOrchestrator.Spawn_TownSlimeTransform(settings);
				return;
			case ParticleOrchestraType.LoadoutChange:
				ParticleOrchestrator.Spawn_LoadOutChange(settings);
				return;
			case ParticleOrchestraType.ShimmerBlock:
				ParticleOrchestrator.Spawn_ShimmerBlock(settings);
				return;
			case ParticleOrchestraType.Digestion:
				ParticleOrchestrator.Spawn_Digestion(settings);
				return;
			case ParticleOrchestraType.WaffleIron:
				ParticleOrchestrator.Spawn_WaffleIron(settings);
				return;
			case ParticleOrchestraType.PooFly:
				ParticleOrchestrator.Spawn_PooFly(settings);
				return;
			case ParticleOrchestraType.ShimmerTownNPC:
				ParticleOrchestrator.Spawn_ShimmerTownNPC(settings);
				return;
			case ParticleOrchestraType.ShimmerTownNPCSend:
				ParticleOrchestrator.Spawn_ShimmerTownNPCSend(settings);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x00525E98 File Offset: 0x00524098
		private static void Spawn_ShimmerTownNPCSend(ParticleOrchestraSettings settings)
		{
			Rectangle rect = Utils.CenteredRectangle(settings.PositionInWorld, new Vector2(30f, 60f));
			for (float num = 0f; num < 20f; num += 1f)
			{
				PrettySparkleParticle prettySparkleParticle = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				int num2 = Main.rand.Next(20, 40);
				prettySparkleParticle.ColorTint = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.5f, 0);
				prettySparkleParticle.LocalPosition = Main.rand.NextVector2FromRectangle(rect);
				prettySparkleParticle.Rotation = 1.5707964f;
				prettySparkleParticle.Scale = new Vector2(1f + Main.rand.NextFloat() * 2f, 0.7f + Main.rand.NextFloat() * 0.7f);
				prettySparkleParticle.Velocity = new Vector2(0f, -1f);
				prettySparkleParticle.FadeInNormalizedTime = 5E-06f;
				prettySparkleParticle.FadeOutNormalizedTime = 0.95f;
				prettySparkleParticle.TimeToLive = (float)num2;
				prettySparkleParticle.FadeOutEnd = (float)num2;
				prettySparkleParticle.FadeInEnd = (float)(num2 / 2);
				prettySparkleParticle.FadeOutStart = (float)(num2 / 2);
				prettySparkleParticle.AdditiveAmount = 0.35f;
				prettySparkleParticle.DrawVerticalAxis = false;
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle);
				PrettySparkleParticle prettySparkleParticle2 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				prettySparkleParticle2.ColorTint = new Color(255, 255, 255, 0);
				prettySparkleParticle2.LocalPosition = Main.rand.NextVector2FromRectangle(rect);
				prettySparkleParticle2.Rotation = 1.5707964f;
				prettySparkleParticle2.Scale = prettySparkleParticle.Scale * 0.5f;
				prettySparkleParticle2.Velocity = new Vector2(0f, -1f);
				prettySparkleParticle2.FadeInNormalizedTime = 5E-06f;
				prettySparkleParticle2.FadeOutNormalizedTime = 0.95f;
				prettySparkleParticle2.TimeToLive = (float)num2;
				prettySparkleParticle2.FadeOutEnd = (float)num2;
				prettySparkleParticle2.FadeInEnd = (float)(num2 / 2);
				prettySparkleParticle2.FadeOutStart = (float)(num2 / 2);
				prettySparkleParticle2.AdditiveAmount = 1f;
				prettySparkleParticle2.DrawVerticalAxis = false;
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle2);
			}
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x005260AC File Offset: 0x005242AC
		private static void Spawn_ShimmerTownNPC(ParticleOrchestraSettings settings)
		{
			Rectangle rectangle = Utils.CenteredRectangle(settings.PositionInWorld, new Vector2(30f, 60f));
			for (float num = 0f; num < 20f; num += 1f)
			{
				PrettySparkleParticle prettySparkleParticle = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				int num2 = Main.rand.Next(20, 40);
				prettySparkleParticle.ColorTint = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.5f, 0);
				prettySparkleParticle.LocalPosition = Main.rand.NextVector2FromRectangle(rectangle);
				prettySparkleParticle.Rotation = 1.5707964f;
				prettySparkleParticle.Scale = new Vector2(1f + Main.rand.NextFloat() * 2f, 0.7f + Main.rand.NextFloat() * 0.7f);
				prettySparkleParticle.Velocity = new Vector2(0f, -1f);
				prettySparkleParticle.FadeInNormalizedTime = 5E-06f;
				prettySparkleParticle.FadeOutNormalizedTime = 0.95f;
				prettySparkleParticle.TimeToLive = (float)num2;
				prettySparkleParticle.FadeOutEnd = (float)num2;
				prettySparkleParticle.FadeInEnd = (float)(num2 / 2);
				prettySparkleParticle.FadeOutStart = (float)(num2 / 2);
				prettySparkleParticle.AdditiveAmount = 0.35f;
				prettySparkleParticle.DrawVerticalAxis = false;
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle);
				PrettySparkleParticle prettySparkleParticle2 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				prettySparkleParticle2.ColorTint = new Color(255, 255, 255, 0);
				prettySparkleParticle2.LocalPosition = Main.rand.NextVector2FromRectangle(rectangle);
				prettySparkleParticle2.Rotation = 1.5707964f;
				prettySparkleParticle2.Scale = prettySparkleParticle.Scale * 0.5f;
				prettySparkleParticle2.Velocity = new Vector2(0f, -1f);
				prettySparkleParticle2.FadeInNormalizedTime = 5E-06f;
				prettySparkleParticle2.FadeOutNormalizedTime = 0.95f;
				prettySparkleParticle2.TimeToLive = (float)num2;
				prettySparkleParticle2.FadeOutEnd = (float)num2;
				prettySparkleParticle2.FadeInEnd = (float)(num2 / 2);
				prettySparkleParticle2.FadeOutStart = (float)(num2 / 2);
				prettySparkleParticle2.AdditiveAmount = 1f;
				prettySparkleParticle2.DrawVerticalAxis = false;
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle2);
			}
			for (int i = 0; i < 20; i++)
			{
				int num3 = Dust.NewDust(rectangle.TopLeft(), rectangle.Width, rectangle.Height, 308, 0f, 0f, 0, default(Color), 1f);
				Dust dust = Main.dust[num3];
				dust.velocity.Y = dust.velocity.Y - 8f;
				Dust dust2 = Main.dust[num3];
				dust2.velocity.X = dust2.velocity.X * 0.5f;
				Main.dust[num3].scale = 0.8f;
				Main.dust[num3].noGravity = true;
				int num4 = Main.rand.Next(6);
				if (num4 == 0)
				{
					Main.dust[num3].color = new Color(255, 255, 210);
				}
				else if (num4 == 1)
				{
					Main.dust[num3].color = new Color(190, 245, 255);
				}
				else if (num4 == 2)
				{
					Main.dust[num3].color = new Color(255, 150, 255);
				}
				else
				{
					Main.dust[num3].color = new Color(190, 175, 255);
				}
			}
			SoundEngine.PlaySound(SoundID.Item29, settings.PositionInWorld);
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x0052641C File Offset: 0x0052461C
		private static void Spawn_PooFly(ParticleOrchestraSettings settings)
		{
			int num = ParticleOrchestrator._poolFlies.CountParticlesInUse();
			if (num > 50 && Main.rand.NextFloat() >= Utils.Remap((float)num, 50f, 400f, 0.5f, 0f, true))
			{
				return;
			}
			LittleFlyingCritterParticle littleFlyingCritterParticle = ParticleOrchestrator._poolFlies.RequestParticle();
			littleFlyingCritterParticle.Prepare(settings.PositionInWorld, 300);
			Main.ParticleSystem_World_OverPlayers.Add(littleFlyingCritterParticle);
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x0052648C File Offset: 0x0052468C
		private static void Spawn_Digestion(ParticleOrchestraSettings settings)
		{
			Vector2 positionInWorld = settings.PositionInWorld;
			int num = (settings.MovementVector.X < 0f) ? 1 : -1;
			int num2 = Main.rand.Next(4);
			for (int i = 0; i < 3 + num2; i++)
			{
				int num3 = Dust.NewDust(positionInWorld + Vector2.UnitX * (float)(-(float)num) * 8f - Vector2.One * 5f + Vector2.UnitY * 8f, 3, 6, 216, (float)(-(float)num), 1f, 0, default(Color), 1f);
				Main.dust[num3].velocity /= 2f;
				Main.dust[num3].scale = 0.8f;
			}
			if (Main.rand.Next(30) == 0)
			{
				int num4 = Gore.NewGore(positionInWorld + Vector2.UnitX * (float)(-(float)num) * 8f, Vector2.Zero, Main.rand.Next(580, 583), 1f);
				Main.gore[num4].velocity /= 2f;
				Main.gore[num4].velocity.Y = Math.Abs(Main.gore[num4].velocity.Y);
				Main.gore[num4].velocity.X = -Math.Abs(Main.gore[num4].velocity.X) * (float)num;
			}
			SoundEngine.PlaySound(SoundID.Item16, settings.PositionInWorld);
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x00526648 File Offset: 0x00524848
		private static void Spawn_ShimmerBlock(ParticleOrchestraSettings settings)
		{
			FadingParticle fadingParticle = ParticleOrchestrator._poolFading.RequestParticle();
			fadingParticle.SetBasicInfo(TextureAssets.Star[0], null, settings.MovementVector, settings.PositionInWorld);
			float num = 45f;
			fadingParticle.SetTypeInfo(num);
			fadingParticle.AccelerationPerFrame = settings.MovementVector / num;
			fadingParticle.ColorTint = Main.hslToRgb(Main.rand.NextFloat(), 0.75f, 0.8f, byte.MaxValue);
			fadingParticle.ColorTint.A = 30;
			fadingParticle.FadeInNormalizedTime = 0.5f;
			fadingParticle.FadeOutNormalizedTime = 0.5f;
			fadingParticle.Rotation = Main.rand.NextFloat() * 6.2831855f;
			fadingParticle.Scale = Vector2.One * (0.5f + 0.5f * Main.rand.NextFloat());
			Main.ParticleSystem_World_OverPlayers.Add(fadingParticle);
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x00526730 File Offset: 0x00524930
		private static void Spawn_LoadOutChange(ParticleOrchestraSettings settings)
		{
			Player player = Main.player[(int)settings.IndexOfPlayerWhoInvokedThis];
			if (!player.active)
			{
				return;
			}
			Rectangle hitbox = player.Hitbox;
			int num = 6;
			hitbox.Height -= num;
			if (player.gravDir == 1f)
			{
				hitbox.Y += num;
			}
			for (int i = 0; i < 40; i++)
			{
				Dust dust = Dust.NewDustPerfect(Main.rand.NextVector2FromRectangle(hitbox), 16, null, 120, default(Color), Main.rand.NextFloat() * 0.8f + 0.8f);
				dust.velocity = new Vector2(0f, (float)(-(float)hitbox.Height) * Main.rand.NextFloat() * 0.04f).RotatedBy((double)(Main.rand.NextFloatDirection() * 6.2831855f * 0.1f), default(Vector2));
				dust.velocity += player.velocity * 2f * Main.rand.NextFloat();
				dust.noGravity = true;
				dust.noLight = (dust.noLightEmittence = true);
			}
			for (int j = 0; j < 5; j++)
			{
				Dust dust2 = Dust.NewDustPerfect(Main.rand.NextVector2FromRectangle(hitbox), 43, null, 254, Main.hslToRgb(Main.rand.NextFloat(), 0.3f, 0.8f, byte.MaxValue), Main.rand.NextFloat() * 0.8f + 0.8f);
				dust2.velocity = new Vector2(0f, (float)(-(float)hitbox.Height) * Main.rand.NextFloat() * 0.04f).RotatedBy((double)(Main.rand.NextFloatDirection() * 6.2831855f * 0.1f), default(Vector2));
				dust2.velocity += player.velocity * 2f * Main.rand.NextFloat();
				dust2.noGravity = true;
				dust2.noLight = (dust2.noLightEmittence = true);
			}
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x0052696C File Offset: 0x00524B6C
		private static void Spawn_TownSlimeTransform(ParticleOrchestraSettings settings)
		{
			switch (settings.UniqueInfoPiece)
			{
			case 0:
				ParticleOrchestrator.NerdySlimeEffect(settings);
				return;
			case 1:
				ParticleOrchestrator.CopperSlimeEffect(settings);
				return;
			case 2:
				ParticleOrchestrator.ElderSlimeEffect(settings);
				return;
			default:
				return;
			}
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x005269A8 File Offset: 0x00524BA8
		private static void ElderSlimeEffect(ParticleOrchestraSettings settings)
		{
			for (int i = 0; i < 30; i++)
			{
				Dust dust = Dust.NewDustPerfect(settings.PositionInWorld + Main.rand.NextVector2Circular(20f, 20f), 43, new Vector2?((settings.MovementVector * 0.75f + Main.rand.NextVector2Circular(6f, 6f)) * Main.rand.NextFloat()), 26, Color.Lerp(Main.OurFavoriteColor, Color.White, Main.rand.NextFloat()), 1f + Main.rand.NextFloat() * 1.4f);
				dust.fadeIn = 1.5f;
				if (dust.velocity.Y > 0f && Main.rand.Next(2) == 0)
				{
					Dust dust2 = dust;
					dust2.velocity.Y = dust2.velocity.Y * -1f;
				}
				dust.noGravity = true;
			}
			for (int j = 0; j < 8; j++)
			{
				Gore.NewGoreDirect(settings.PositionInWorld + Utils.RandomVector2(Main.rand, -30f, 30f) * new Vector2(0.5f, 1f), Vector2.Zero, 61 + Main.rand.Next(3), 1f).velocity *= 0.5f;
			}
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x00526B14 File Offset: 0x00524D14
		private static void NerdySlimeEffect(ParticleOrchestraSettings settings)
		{
			Color newColor = new Color(0, 80, 255, 100);
			for (int i = 0; i < 60; i++)
			{
				Dust.NewDustPerfect(settings.PositionInWorld, 4, new Vector2?((settings.MovementVector * 0.75f + Main.rand.NextVector2Circular(6f, 6f)) * Main.rand.NextFloat()), 175, newColor, 0.6f + Main.rand.NextFloat() * 1.4f);
			}
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x00526BA8 File Offset: 0x00524DA8
		private static void CopperSlimeEffect(ParticleOrchestraSettings settings)
		{
			for (int i = 0; i < 40; i++)
			{
				Dust dust = Dust.NewDustPerfect(settings.PositionInWorld + Main.rand.NextVector2Circular(20f, 20f), 43, new Vector2?((settings.MovementVector * 0.75f + Main.rand.NextVector2Circular(6f, 6f)) * Main.rand.NextFloat()), 26, Color.Lerp(new Color(183, 88, 25), Color.White, Main.rand.NextFloat() * 0.5f), 1f + Main.rand.NextFloat() * 1.4f);
				dust.fadeIn = 1.5f;
				if (dust.velocity.Y > 0f && Main.rand.Next(2) == 0)
				{
					Dust dust2 = dust;
					dust2.velocity.Y = dust2.velocity.Y * -1f;
				}
				dust.noGravity = true;
			}
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x00526CB4 File Offset: 0x00524EB4
		private static void Spawn_ShimmerArrow(ParticleOrchestraSettings settings)
		{
			float num = 20f;
			for (int i = 0; i < 2; i++)
			{
				float num2 = 6.2831855f * Main.rand.NextFloatDirection() * 0.05f;
				Color color = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.5f, byte.MaxValue);
				color.A /= 2;
				Color color2 = color;
				color2.A = byte.MaxValue;
				color2 = Color.Lerp(color2, Color.White, 0.5f);
				for (float num3 = 0f; num3 < 4f; num3 += 1f)
				{
					PrettySparkleParticle prettySparkleParticle = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
					Vector2 vector = (1.5707964f * num3 + num2).ToRotationVector2() * 4f;
					prettySparkleParticle.ColorTint = color;
					prettySparkleParticle.LocalPosition = settings.PositionInWorld;
					prettySparkleParticle.Rotation = vector.ToRotation();
					prettySparkleParticle.Scale = new Vector2((num3 % 2f == 0f) ? 2f : 4f, 0.5f) * 1.1f;
					prettySparkleParticle.FadeInNormalizedTime = 5E-06f;
					prettySparkleParticle.FadeOutNormalizedTime = 0.95f;
					prettySparkleParticle.TimeToLive = num;
					prettySparkleParticle.FadeOutEnd = num;
					prettySparkleParticle.FadeInEnd = num / 2f;
					prettySparkleParticle.FadeOutStart = num / 2f;
					prettySparkleParticle.AdditiveAmount = 0.35f;
					prettySparkleParticle.Velocity = -vector * 0.2f;
					prettySparkleParticle.DrawVerticalAxis = false;
					if (num3 % 2f == 1f)
					{
						prettySparkleParticle.Scale *= 0.9f;
						prettySparkleParticle.Velocity *= 0.9f;
					}
					Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle);
				}
				for (float num4 = 0f; num4 < 4f; num4 += 1f)
				{
					PrettySparkleParticle prettySparkleParticle2 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
					Vector2 vector2 = (1.5707964f * num4 + num2).ToRotationVector2() * 4f;
					prettySparkleParticle2.ColorTint = color2;
					prettySparkleParticle2.LocalPosition = settings.PositionInWorld;
					prettySparkleParticle2.Rotation = vector2.ToRotation();
					prettySparkleParticle2.Scale = new Vector2((num4 % 2f == 0f) ? 2f : 4f, 0.5f) * 0.7f;
					prettySparkleParticle2.FadeInNormalizedTime = 5E-06f;
					prettySparkleParticle2.FadeOutNormalizedTime = 0.95f;
					prettySparkleParticle2.TimeToLive = num;
					prettySparkleParticle2.FadeOutEnd = num;
					prettySparkleParticle2.FadeInEnd = num / 2f;
					prettySparkleParticle2.FadeOutStart = num / 2f;
					prettySparkleParticle2.Velocity = vector2 * 0.2f;
					prettySparkleParticle2.DrawVerticalAxis = false;
					if (num4 % 2f == 1f)
					{
						prettySparkleParticle2.Scale *= 1.2f;
						prettySparkleParticle2.Velocity *= 1.2f;
					}
					Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle2);
					if (i == 0)
					{
						for (int j = 0; j < 1; j++)
						{
							Dust dust = Dust.NewDustPerfect(settings.PositionInWorld, 306, new Vector2?(vector2.RotatedBy((double)(Main.rand.NextFloatDirection() * 6.2831855f * 0.025f), default(Vector2)) * Main.rand.NextFloat()), 0, default(Color), 1f);
							dust.noGravity = true;
							dust.scale = 1.4f;
							dust.fadeIn = 1.2f;
							dust.color = color;
							Dust dust2 = Dust.NewDustPerfect(settings.PositionInWorld, 306, new Vector2?(-vector2.RotatedBy((double)(Main.rand.NextFloatDirection() * 6.2831855f * 0.025f), default(Vector2)) * Main.rand.NextFloat()), 0, default(Color), 1f);
							dust2.noGravity = true;
							dust2.scale = 1.4f;
							dust2.fadeIn = 1.2f;
							dust2.color = color;
						}
					}
				}
			}
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x00527108 File Offset: 0x00525308
		private static void Spawn_ItemTransfer(ParticleOrchestraSettings settings)
		{
			Vector2 value = settings.PositionInWorld + settings.MovementVector;
			Vector2 value2 = Main.rand.NextVector2Circular(32f, 32f);
			Vector2 vector = settings.PositionInWorld + value2;
			Vector2 value3 = value - vector;
			int num = settings.UniqueInfoPiece;
			Item item;
			if (!ContentSamples.ItemsByType.TryGetValue(num, out item))
			{
				return;
			}
			if (item.IsAir)
			{
				return;
			}
			num = item.type;
			int num2 = Main.rand.Next(60, 80);
			Chest.AskForChestToEatItem(vector + value3 + new Vector2(-8f, -8f), num2 + 10);
			ItemTransferParticle itemTransferParticle = ParticleOrchestrator._poolItemTransfer.RequestParticle();
			itemTransferParticle.Prepare(num, num2, vector, vector + value3);
			Main.ParticleSystem_World_OverPlayers.Add(itemTransferParticle);
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x005271D8 File Offset: 0x005253D8
		private static void Spawn_PetExchange(ParticleOrchestraSettings settings)
		{
			Vector2 positionInWorld = settings.PositionInWorld;
			for (int i = 0; i < 13; i++)
			{
				Gore gore = Gore.NewGoreDirect(positionInWorld + new Vector2(-20f, -20f) + Main.rand.NextVector2Circular(20f, 20f), Vector2.Zero, Main.rand.Next(61, 64), 1f + Main.rand.NextFloat() * 0.3f);
				gore.alpha = 100;
				gore.velocity = (6.2831855f * (float)Main.rand.Next()).ToRotationVector2() * Main.rand.NextFloat() + settings.MovementVector * 0.5f;
			}
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x005272A4 File Offset: 0x005254A4
		private static void Spawn_TerraBlade(ParticleOrchestraSettings settings)
		{
			float num = 30f;
			float num2 = settings.MovementVector.ToRotation() + 1.5707964f;
			float x = 3f;
			for (float num3 = 0f; num3 < 4f; num3 += 1f)
			{
				PrettySparkleParticle prettySparkleParticle = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				Vector2 vector = (1.5707964f * num3 + num2).ToRotationVector2() * 4f;
				prettySparkleParticle.ColorTint = new Color(0.2f, 0.85f, 0.4f, 0.5f);
				prettySparkleParticle.LocalPosition = settings.PositionInWorld;
				prettySparkleParticle.Rotation = vector.ToRotation();
				prettySparkleParticle.Scale = new Vector2(x, 0.5f) * 1.1f;
				prettySparkleParticle.FadeInNormalizedTime = 5E-06f;
				prettySparkleParticle.FadeOutNormalizedTime = 0.95f;
				prettySparkleParticle.TimeToLive = num;
				prettySparkleParticle.FadeOutEnd = num;
				prettySparkleParticle.FadeInEnd = num / 2f;
				prettySparkleParticle.FadeOutStart = num / 2f;
				prettySparkleParticle.AdditiveAmount = 0.35f;
				prettySparkleParticle.Velocity = -vector * 0.2f;
				prettySparkleParticle.DrawVerticalAxis = false;
				if (num3 % 2f == 1f)
				{
					prettySparkleParticle.Scale *= 1.5f;
					prettySparkleParticle.Velocity *= 2f;
				}
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle);
			}
			for (float num4 = -1f; num4 <= 1f; num4 += 2f)
			{
				PrettySparkleParticle prettySparkleParticle2 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				num2.ToRotationVector2() * 4f;
				Vector2 vector2 = (1.5707964f * num4 + num2).ToRotationVector2() * 2f;
				prettySparkleParticle2.ColorTint = new Color(0.4f, 1f, 0.4f, 0.5f);
				prettySparkleParticle2.LocalPosition = settings.PositionInWorld;
				prettySparkleParticle2.Rotation = vector2.ToRotation();
				prettySparkleParticle2.Scale = new Vector2(x, 0.5f) * 1.1f;
				prettySparkleParticle2.FadeInNormalizedTime = 5E-06f;
				prettySparkleParticle2.FadeOutNormalizedTime = 0.95f;
				prettySparkleParticle2.TimeToLive = num;
				prettySparkleParticle2.FadeOutEnd = num;
				prettySparkleParticle2.FadeInEnd = num / 2f;
				prettySparkleParticle2.FadeOutStart = num / 2f;
				prettySparkleParticle2.AdditiveAmount = 0.35f;
				prettySparkleParticle2.Velocity = vector2.RotatedBy(1.5707963705062866, default(Vector2)) * 0.5f;
				prettySparkleParticle2.DrawVerticalAxis = false;
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle2);
			}
			for (float num5 = 0f; num5 < 4f; num5 += 1f)
			{
				PrettySparkleParticle prettySparkleParticle3 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				Vector2 vector3 = (1.5707964f * num5 + num2).ToRotationVector2() * 4f;
				prettySparkleParticle3.ColorTint = new Color(0.2f, 1f, 0.2f, 1f);
				prettySparkleParticle3.LocalPosition = settings.PositionInWorld;
				prettySparkleParticle3.Rotation = vector3.ToRotation();
				prettySparkleParticle3.Scale = new Vector2(x, 0.5f) * 0.7f;
				prettySparkleParticle3.FadeInNormalizedTime = 5E-06f;
				prettySparkleParticle3.FadeOutNormalizedTime = 0.95f;
				prettySparkleParticle3.TimeToLive = num;
				prettySparkleParticle3.FadeOutEnd = num;
				prettySparkleParticle3.FadeInEnd = num / 2f;
				prettySparkleParticle3.FadeOutStart = num / 2f;
				prettySparkleParticle3.Velocity = vector3 * 0.2f;
				prettySparkleParticle3.DrawVerticalAxis = false;
				if (num5 % 2f == 1f)
				{
					prettySparkleParticle3.Scale *= 1.5f;
					prettySparkleParticle3.Velocity *= 2f;
				}
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle3);
				for (int i = 0; i < 1; i++)
				{
					Dust dust = Dust.NewDustPerfect(settings.PositionInWorld, 107, new Vector2?(vector3.RotatedBy((double)(Main.rand.NextFloatDirection() * 6.2831855f * 0.025f), default(Vector2)) * Main.rand.NextFloat()), 0, default(Color), 1f);
					dust.noGravity = true;
					dust.scale = 0.8f;
					Dust dust2 = Dust.NewDustPerfect(settings.PositionInWorld, 107, new Vector2?(-vector3.RotatedBy((double)(Main.rand.NextFloatDirection() * 6.2831855f * 0.025f), default(Vector2)) * Main.rand.NextFloat()), 0, default(Color), 1f);
					dust2.noGravity = true;
					dust2.scale = 1.4f;
				}
			}
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x0052779C File Offset: 0x0052599C
		private static void Spawn_Excalibur(ParticleOrchestraSettings settings)
		{
			float num = 30f;
			float num2 = 0f;
			for (float num3 = 0f; num3 < 4f; num3 += 1f)
			{
				PrettySparkleParticle prettySparkleParticle = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				Vector2 vector = (1.5707964f * num3 + num2).ToRotationVector2() * 4f;
				prettySparkleParticle.ColorTint = new Color(0.9f, 0.85f, 0.4f, 0.5f);
				prettySparkleParticle.LocalPosition = settings.PositionInWorld;
				prettySparkleParticle.Rotation = vector.ToRotation();
				prettySparkleParticle.Scale = new Vector2((num3 % 2f == 0f) ? 2f : 4f, 0.5f) * 1.1f;
				prettySparkleParticle.FadeInNormalizedTime = 5E-06f;
				prettySparkleParticle.FadeOutNormalizedTime = 0.95f;
				prettySparkleParticle.TimeToLive = num;
				prettySparkleParticle.FadeOutEnd = num;
				prettySparkleParticle.FadeInEnd = num / 2f;
				prettySparkleParticle.FadeOutStart = num / 2f;
				prettySparkleParticle.AdditiveAmount = 0.35f;
				prettySparkleParticle.Velocity = -vector * 0.2f;
				prettySparkleParticle.DrawVerticalAxis = false;
				if (num3 % 2f == 1f)
				{
					prettySparkleParticle.Scale *= 1.5f;
					prettySparkleParticle.Velocity *= 1.5f;
				}
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle);
			}
			for (float num4 = 0f; num4 < 4f; num4 += 1f)
			{
				PrettySparkleParticle prettySparkleParticle2 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				Vector2 vector2 = (1.5707964f * num4 + num2).ToRotationVector2() * 4f;
				prettySparkleParticle2.ColorTint = new Color(1f, 1f, 0.2f, 1f);
				prettySparkleParticle2.LocalPosition = settings.PositionInWorld;
				prettySparkleParticle2.Rotation = vector2.ToRotation();
				prettySparkleParticle2.Scale = new Vector2((num4 % 2f == 0f) ? 2f : 4f, 0.5f) * 0.7f;
				prettySparkleParticle2.FadeInNormalizedTime = 5E-06f;
				prettySparkleParticle2.FadeOutNormalizedTime = 0.95f;
				prettySparkleParticle2.TimeToLive = num;
				prettySparkleParticle2.FadeOutEnd = num;
				prettySparkleParticle2.FadeInEnd = num / 2f;
				prettySparkleParticle2.FadeOutStart = num / 2f;
				prettySparkleParticle2.Velocity = vector2 * 0.2f;
				prettySparkleParticle2.DrawVerticalAxis = false;
				if (num4 % 2f == 1f)
				{
					prettySparkleParticle2.Scale *= 1.5f;
					prettySparkleParticle2.Velocity *= 1.5f;
				}
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle2);
				for (int i = 0; i < 1; i++)
				{
					Dust dust = Dust.NewDustPerfect(settings.PositionInWorld, 169, new Vector2?(vector2.RotatedBy((double)(Main.rand.NextFloatDirection() * 6.2831855f * 0.025f), default(Vector2)) * Main.rand.NextFloat()), 0, default(Color), 1f);
					dust.noGravity = true;
					dust.scale = 1.4f;
					Dust dust2 = Dust.NewDustPerfect(settings.PositionInWorld, 169, new Vector2?(-vector2.RotatedBy((double)(Main.rand.NextFloatDirection() * 6.2831855f * 0.025f), default(Vector2)) * Main.rand.NextFloat()), 0, default(Color), 1f);
					dust2.noGravity = true;
					dust2.scale = 1.4f;
				}
			}
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x00527B67 File Offset: 0x00525D67
		private static void Spawn_SlapHand(ParticleOrchestraSettings settings)
		{
			SoundEngine.PlaySound(SoundID.Item175, settings.PositionInWorld);
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x00527B7A File Offset: 0x00525D7A
		private static void Spawn_WaffleIron(ParticleOrchestraSettings settings)
		{
			SoundEngine.PlaySound(SoundID.Item178, settings.PositionInWorld);
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x00527B8D File Offset: 0x00525D8D
		private static void Spawn_FlyMeal(ParticleOrchestraSettings settings)
		{
			SoundEngine.PlaySound(SoundID.Item16, settings.PositionInWorld);
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x00527BA0 File Offset: 0x00525DA0
		private static void Spawn_GasTrap(ParticleOrchestraSettings settings)
		{
			SoundEngine.PlaySound(SoundID.Item16, settings.PositionInWorld);
			Vector2 movementVector = settings.MovementVector;
			int num = 12;
			int num2 = 10;
			float num3 = 5f;
			float num4 = 2.5f;
			Color lightColorTint = new Color(0.2f, 0.4f, 0.15f);
			Vector2 positionInWorld = settings.PositionInWorld;
			float num5 = 0.15707964f;
			float num6 = 0.20943952f;
			for (int i = 0; i < num; i++)
			{
				Vector2 vector = movementVector + new Vector2(num3 + Main.rand.NextFloat() * 1f, 0f).RotatedBy((double)((float)i / (float)num * 6.2831855f), Vector2.Zero);
				vector = vector.RotatedByRandom((double)num5);
				GasParticle gasParticle = ParticleOrchestrator._poolGas.RequestParticle();
				gasParticle.AccelerationPerFrame = Vector2.Zero;
				gasParticle.Velocity = vector;
				gasParticle.ColorTint = Color.White;
				gasParticle.LightColorTint = lightColorTint;
				gasParticle.LocalPosition = positionInWorld + vector;
				gasParticle.TimeToLive = (float)(50 + Main.rand.Next(20));
				gasParticle.InitialScale = 1f + Main.rand.NextFloat() * 0.35f;
				Main.ParticleSystem_World_BehindPlayers.Add(gasParticle);
			}
			for (int j = 0; j < num2; j++)
			{
				Vector2 vector2 = new Vector2(num4 + Main.rand.NextFloat() * 1.45f, 0f).RotatedBy((double)((float)j / (float)num2 * 6.2831855f), Vector2.Zero);
				vector2 = vector2.RotatedByRandom((double)num6);
				if (j % 2 == 0)
				{
					vector2 *= 0.5f;
				}
				GasParticle gasParticle2 = ParticleOrchestrator._poolGas.RequestParticle();
				gasParticle2.AccelerationPerFrame = Vector2.Zero;
				gasParticle2.Velocity = vector2;
				gasParticle2.ColorTint = Color.White;
				gasParticle2.LightColorTint = lightColorTint;
				gasParticle2.LocalPosition = positionInWorld;
				gasParticle2.TimeToLive = (float)(80 + Main.rand.Next(30));
				gasParticle2.InitialScale = 1f + Main.rand.NextFloat() * 0.5f;
				Main.ParticleSystem_World_BehindPlayers.Add(gasParticle2);
			}
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x00527DD4 File Offset: 0x00525FD4
		private static void Spawn_TrueExcalibur(ParticleOrchestraSettings settings)
		{
			float num = 36f;
			float num2 = 0.7853982f;
			for (float num3 = 0f; num3 < 2f; num3 += 1f)
			{
				PrettySparkleParticle prettySparkleParticle = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				Vector2 v = (1.5707964f * num3 + num2).ToRotationVector2() * 4f;
				prettySparkleParticle.ColorTint = new Color(1f, 0f, 0.3f, 1f);
				prettySparkleParticle.LocalPosition = settings.PositionInWorld;
				prettySparkleParticle.Rotation = v.ToRotation();
				prettySparkleParticle.Scale = new Vector2(5f, 0.5f) * 1.1f;
				prettySparkleParticle.FadeInNormalizedTime = 5E-06f;
				prettySparkleParticle.FadeOutNormalizedTime = 0.95f;
				prettySparkleParticle.TimeToLive = num;
				prettySparkleParticle.FadeOutEnd = num;
				prettySparkleParticle.FadeInEnd = num / 2f;
				prettySparkleParticle.FadeOutStart = num / 2f;
				prettySparkleParticle.AdditiveAmount = 0.35f;
				prettySparkleParticle.DrawVerticalAxis = false;
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle);
			}
			for (float num4 = 0f; num4 < 2f; num4 += 1f)
			{
				PrettySparkleParticle prettySparkleParticle2 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				Vector2 vector = (1.5707964f * num4 + num2).ToRotationVector2() * 4f;
				prettySparkleParticle2.ColorTint = new Color(1f, 0.5f, 0.8f, 1f);
				prettySparkleParticle2.LocalPosition = settings.PositionInWorld;
				prettySparkleParticle2.Rotation = vector.ToRotation();
				prettySparkleParticle2.Scale = new Vector2(3f, 0.5f) * 0.7f;
				prettySparkleParticle2.FadeInNormalizedTime = 5E-06f;
				prettySparkleParticle2.FadeOutNormalizedTime = 0.95f;
				prettySparkleParticle2.TimeToLive = num;
				prettySparkleParticle2.FadeOutEnd = num;
				prettySparkleParticle2.FadeInEnd = num / 2f;
				prettySparkleParticle2.FadeOutStart = num / 2f;
				prettySparkleParticle2.DrawVerticalAxis = false;
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle2);
				for (int i = 0; i < 1; i++)
				{
					if (Main.rand.Next(2) != 0)
					{
						Dust dust = Dust.NewDustPerfect(settings.PositionInWorld, 242, new Vector2?(vector.RotatedBy((double)(Main.rand.NextFloatDirection() * 6.2831855f * 0.025f), default(Vector2)) * Main.rand.NextFloat()), 0, default(Color), 1f);
						dust.noGravity = true;
						dust.scale = 1.4f;
						Dust dust2 = Dust.NewDustPerfect(settings.PositionInWorld, 242, new Vector2?(-vector.RotatedBy((double)(Main.rand.NextFloatDirection() * 6.2831855f * 0.025f), default(Vector2)) * Main.rand.NextFloat()), 0, default(Color), 1f);
						dust2.noGravity = true;
						dust2.scale = 1.4f;
					}
				}
			}
			num = 30f;
			num2 = 0f;
			for (float num5 = 0f; num5 < 4f; num5 += 1f)
			{
				PrettySparkleParticle prettySparkleParticle3 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				Vector2 vector2 = (1.5707964f * num5 + num2).ToRotationVector2() * 4f;
				prettySparkleParticle3.ColorTint = new Color(0.9f, 0.85f, 0.4f, 0.5f);
				prettySparkleParticle3.LocalPosition = settings.PositionInWorld;
				prettySparkleParticle3.Rotation = vector2.ToRotation();
				prettySparkleParticle3.Scale = new Vector2((num5 % 2f == 0f) ? 2f : 4f, 0.5f) * 1.1f;
				prettySparkleParticle3.FadeInNormalizedTime = 5E-06f;
				prettySparkleParticle3.FadeOutNormalizedTime = 0.95f;
				prettySparkleParticle3.TimeToLive = num;
				prettySparkleParticle3.FadeOutEnd = num;
				prettySparkleParticle3.FadeInEnd = num / 2f;
				prettySparkleParticle3.FadeOutStart = num / 2f;
				prettySparkleParticle3.AdditiveAmount = 0.35f;
				prettySparkleParticle3.Velocity = -vector2 * 0.2f;
				prettySparkleParticle3.DrawVerticalAxis = false;
				if (num5 % 2f == 1f)
				{
					prettySparkleParticle3.Scale *= 1.5f;
					prettySparkleParticle3.Velocity *= 1.5f;
				}
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle3);
			}
			for (float num6 = 0f; num6 < 4f; num6 += 1f)
			{
				PrettySparkleParticle prettySparkleParticle4 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				Vector2 vector3 = (1.5707964f * num6 + num2).ToRotationVector2() * 4f;
				prettySparkleParticle4.ColorTint = new Color(1f, 1f, 0.2f, 1f);
				prettySparkleParticle4.LocalPosition = settings.PositionInWorld;
				prettySparkleParticle4.Rotation = vector3.ToRotation();
				prettySparkleParticle4.Scale = new Vector2((num6 % 2f == 0f) ? 2f : 4f, 0.5f) * 0.7f;
				prettySparkleParticle4.FadeInNormalizedTime = 5E-06f;
				prettySparkleParticle4.FadeOutNormalizedTime = 0.95f;
				prettySparkleParticle4.TimeToLive = num;
				prettySparkleParticle4.FadeOutEnd = num;
				prettySparkleParticle4.FadeInEnd = num / 2f;
				prettySparkleParticle4.FadeOutStart = num / 2f;
				prettySparkleParticle4.Velocity = vector3 * 0.2f;
				prettySparkleParticle4.DrawVerticalAxis = false;
				if (num6 % 2f == 1f)
				{
					prettySparkleParticle4.Scale *= 1.5f;
					prettySparkleParticle4.Velocity *= 1.5f;
				}
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle4);
				for (int j = 0; j < 1; j++)
				{
					if (Main.rand.Next(2) != 0)
					{
						Dust dust3 = Dust.NewDustPerfect(settings.PositionInWorld, 169, new Vector2?(vector3.RotatedBy((double)(Main.rand.NextFloatDirection() * 6.2831855f * 0.025f), default(Vector2)) * Main.rand.NextFloat()), 0, default(Color), 1f);
						dust3.noGravity = true;
						dust3.scale = 1.4f;
						Dust dust4 = Dust.NewDustPerfect(settings.PositionInWorld, 169, new Vector2?(-vector3.RotatedBy((double)(Main.rand.NextFloatDirection() * 6.2831855f * 0.025f), default(Vector2)) * Main.rand.NextFloat()), 0, default(Color), 1f);
						dust4.noGravity = true;
						dust4.scale = 1.4f;
					}
				}
			}
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x005284CC File Offset: 0x005266CC
		private static void Spawn_AshTreeShake(ParticleOrchestraSettings settings)
		{
			float num = 10f + 20f * Main.rand.NextFloat();
			float num2 = -0.7853982f;
			float scaleFactor = 0.2f + 0.4f * Main.rand.NextFloat();
			Color color = Main.hslToRgb(Main.rand.NextFloat() * 0.1f + 0.06f, 1f, 0.5f, byte.MaxValue);
			color.A /= 2;
			color *= Main.rand.NextFloat() * 0.3f + 0.7f;
			for (float num3 = 0f; num3 < 2f; num3 += 1f)
			{
				PrettySparkleParticle prettySparkleParticle = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				Vector2 vector = (0.7853982f + 3.1415927f * num3 + num2).ToRotationVector2() * 4f;
				prettySparkleParticle.ColorTint = color;
				prettySparkleParticle.LocalPosition = settings.PositionInWorld;
				prettySparkleParticle.Rotation = vector.ToRotation();
				prettySparkleParticle.Scale = new Vector2(4f, 1f) * 1.1f * scaleFactor;
				prettySparkleParticle.FadeInNormalizedTime = 5E-06f;
				prettySparkleParticle.FadeOutNormalizedTime = 0.95f;
				prettySparkleParticle.TimeToLive = num;
				prettySparkleParticle.FadeOutEnd = num;
				prettySparkleParticle.FadeInEnd = num / 2f;
				prettySparkleParticle.FadeOutStart = num / 2f;
				prettySparkleParticle.AdditiveAmount = 0.35f;
				prettySparkleParticle.LocalPosition -= vector * num * 0.25f;
				prettySparkleParticle.Velocity = vector * 0.05f;
				prettySparkleParticle.DrawVerticalAxis = false;
				if (num3 == 1f)
				{
					prettySparkleParticle.Scale *= 1.5f;
					prettySparkleParticle.Velocity *= 1.5f;
					prettySparkleParticle.LocalPosition -= prettySparkleParticle.Velocity * 4f;
				}
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle);
			}
			for (float num4 = 0f; num4 < 2f; num4 += 1f)
			{
				PrettySparkleParticle prettySparkleParticle2 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				Vector2 vector2 = (0.7853982f + 3.1415927f * num4 + num2).ToRotationVector2() * 4f;
				prettySparkleParticle2.ColorTint = new Color(1f, 0.4f, 0.2f, 1f);
				prettySparkleParticle2.LocalPosition = settings.PositionInWorld;
				prettySparkleParticle2.Rotation = vector2.ToRotation();
				prettySparkleParticle2.Scale = new Vector2(4f, 1f) * 0.7f * scaleFactor;
				prettySparkleParticle2.FadeInNormalizedTime = 5E-06f;
				prettySparkleParticle2.FadeOutNormalizedTime = 0.95f;
				prettySparkleParticle2.TimeToLive = num;
				prettySparkleParticle2.FadeOutEnd = num;
				prettySparkleParticle2.FadeInEnd = num / 2f;
				prettySparkleParticle2.FadeOutStart = num / 2f;
				prettySparkleParticle2.LocalPosition -= vector2 * num * 0.25f;
				prettySparkleParticle2.Velocity = vector2 * 0.05f;
				prettySparkleParticle2.DrawVerticalAxis = false;
				if (num4 == 1f)
				{
					prettySparkleParticle2.Scale *= 1.5f;
					prettySparkleParticle2.Velocity *= 1.5f;
					prettySparkleParticle2.LocalPosition -= prettySparkleParticle2.Velocity * 4f;
				}
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle2);
				for (int i = 0; i < 1; i++)
				{
					Dust dust = Dust.NewDustPerfect(settings.PositionInWorld, 6, new Vector2?(vector2.RotatedBy((double)(Main.rand.NextFloatDirection() * 6.2831855f * 0.025f), default(Vector2)) * Main.rand.NextFloat()), 0, default(Color), 1f);
					dust.noGravity = true;
					dust.scale = 1.4f;
					Dust dust2 = Dust.NewDustPerfect(settings.PositionInWorld, 6, new Vector2?(-vector2.RotatedBy((double)(Main.rand.NextFloatDirection() * 6.2831855f * 0.025f), default(Vector2)) * Main.rand.NextFloat()), 0, default(Color), 1f);
					dust2.noGravity = true;
					dust2.scale = 1.4f;
				}
			}
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x00528978 File Offset: 0x00526B78
		private static void Spawn_LeafCrystalPassive(ParticleOrchestraSettings settings)
		{
			float num = 90f;
			float num2 = 6.2831855f * Main.rand.NextFloat();
			float num3 = 3f;
			for (float num4 = 0f; num4 < num3; num4 += 1f)
			{
				PrettySparkleParticle prettySparkleParticle = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				Vector2 v = (6.2831855f / num3 * num4 + num2).ToRotationVector2() * 4f;
				prettySparkleParticle.ColorTint = new Color(0.3f, 0.6f, 0.3f, 0.5f);
				prettySparkleParticle.LocalPosition = settings.PositionInWorld;
				prettySparkleParticle.Rotation = v.ToRotation();
				prettySparkleParticle.Scale = new Vector2(4f, 1f) * 0.4f;
				prettySparkleParticle.FadeInNormalizedTime = 5E-06f;
				prettySparkleParticle.FadeOutNormalizedTime = 0.95f;
				prettySparkleParticle.TimeToLive = num;
				prettySparkleParticle.FadeOutEnd = num;
				prettySparkleParticle.FadeInEnd = 10f;
				prettySparkleParticle.FadeOutStart = 10f;
				prettySparkleParticle.AdditiveAmount = 0.5f;
				prettySparkleParticle.Velocity = Vector2.Zero;
				prettySparkleParticle.DrawVerticalAxis = false;
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle);
			}
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x00528AAC File Offset: 0x00526CAC
		private static void Spawn_LeafCrystalShot(ParticleOrchestraSettings settings)
		{
			int num = 30;
			PrettySparkleParticle prettySparkleParticle = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
			Vector2 movementVector = settings.MovementVector;
			Color color = Main.hslToRgb((float)settings.UniqueInfoPiece / 255f, 1f, 0.5f, byte.MaxValue);
			color = Color.Lerp(color, Color.Gold, (float)color.R / 255f * 0.5f);
			prettySparkleParticle.ColorTint = color;
			prettySparkleParticle.LocalPosition = settings.PositionInWorld;
			prettySparkleParticle.Rotation = movementVector.ToRotation();
			prettySparkleParticle.Scale = new Vector2(4f, 1f) * 1f;
			prettySparkleParticle.FadeInNormalizedTime = 5E-06f;
			prettySparkleParticle.FadeOutNormalizedTime = 1f;
			prettySparkleParticle.TimeToLive = (float)num;
			prettySparkleParticle.FadeOutEnd = (float)num;
			prettySparkleParticle.FadeInEnd = (float)(num / 2);
			prettySparkleParticle.FadeOutStart = (float)(num / 2);
			prettySparkleParticle.AdditiveAmount = 0.5f;
			prettySparkleParticle.Velocity = settings.MovementVector;
			prettySparkleParticle.LocalPosition -= prettySparkleParticle.Velocity * 4f;
			prettySparkleParticle.DrawVerticalAxis = false;
			Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle);
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x00528BD4 File Offset: 0x00526DD4
		private static void Spawn_TrueNightsEdge(ParticleOrchestraSettings settings)
		{
			float num = 30f;
			float num2 = 0f;
			for (float num3 = 0f; num3 < 3f; num3 += 2f)
			{
				PrettySparkleParticle prettySparkleParticle = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				Vector2 vector = (0.7853982f + 0.7853982f * num3 + num2).ToRotationVector2() * 4f;
				prettySparkleParticle.ColorTint = new Color(0.3f, 0.6f, 0.3f, 0.5f);
				prettySparkleParticle.LocalPosition = settings.PositionInWorld;
				prettySparkleParticle.Rotation = vector.ToRotation();
				prettySparkleParticle.Scale = new Vector2(4f, 1f) * 1.1f;
				prettySparkleParticle.FadeInNormalizedTime = 5E-06f;
				prettySparkleParticle.FadeOutNormalizedTime = 0.95f;
				prettySparkleParticle.TimeToLive = num;
				prettySparkleParticle.FadeOutEnd = num;
				prettySparkleParticle.FadeInEnd = num / 2f;
				prettySparkleParticle.FadeOutStart = num / 2f;
				prettySparkleParticle.AdditiveAmount = 0.35f;
				prettySparkleParticle.LocalPosition -= vector * num * 0.25f;
				prettySparkleParticle.Velocity = vector;
				prettySparkleParticle.DrawVerticalAxis = false;
				if (num3 == 1f)
				{
					prettySparkleParticle.Scale *= 1.5f;
					prettySparkleParticle.Velocity *= 1.5f;
					prettySparkleParticle.LocalPosition -= prettySparkleParticle.Velocity * 4f;
				}
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle);
			}
			for (float num4 = 0f; num4 < 3f; num4 += 2f)
			{
				PrettySparkleParticle prettySparkleParticle2 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				Vector2 vector2 = (0.7853982f + 0.7853982f * num4 + num2).ToRotationVector2() * 4f;
				prettySparkleParticle2.ColorTint = new Color(0.6f, 1f, 0.2f, 1f);
				prettySparkleParticle2.LocalPosition = settings.PositionInWorld;
				prettySparkleParticle2.Rotation = vector2.ToRotation();
				prettySparkleParticle2.Scale = new Vector2(4f, 1f) * 0.7f;
				prettySparkleParticle2.FadeInNormalizedTime = 5E-06f;
				prettySparkleParticle2.FadeOutNormalizedTime = 0.95f;
				prettySparkleParticle2.TimeToLive = num;
				prettySparkleParticle2.FadeOutEnd = num;
				prettySparkleParticle2.FadeInEnd = num / 2f;
				prettySparkleParticle2.FadeOutStart = num / 2f;
				prettySparkleParticle2.LocalPosition -= vector2 * num * 0.25f;
				prettySparkleParticle2.Velocity = vector2;
				prettySparkleParticle2.DrawVerticalAxis = false;
				if (num4 == 1f)
				{
					prettySparkleParticle2.Scale *= 1.5f;
					prettySparkleParticle2.Velocity *= 1.5f;
					prettySparkleParticle2.LocalPosition -= prettySparkleParticle2.Velocity * 4f;
				}
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle2);
				for (int i = 0; i < 2; i++)
				{
					Dust dust = Dust.NewDustPerfect(settings.PositionInWorld, 75, new Vector2?(vector2.RotatedBy((double)(Main.rand.NextFloatDirection() * 6.2831855f * 0.025f), default(Vector2)) * Main.rand.NextFloat()), 0, default(Color), 1f);
					dust.noGravity = true;
					dust.scale = 1.4f;
					Dust dust2 = Dust.NewDustPerfect(settings.PositionInWorld, 75, new Vector2?(-vector2.RotatedBy((double)(Main.rand.NextFloatDirection() * 6.2831855f * 0.025f), default(Vector2)) * Main.rand.NextFloat()), 0, default(Color), 1f);
					dust2.noGravity = true;
					dust2.scale = 1.4f;
				}
			}
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x00528FE0 File Offset: 0x005271E0
		private static void Spawn_NightsEdge(ParticleOrchestraSettings settings)
		{
			float num = 30f;
			float num2 = 0f;
			for (float num3 = 0f; num3 < 3f; num3 += 1f)
			{
				PrettySparkleParticle prettySparkleParticle = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				Vector2 vector = (0.7853982f + 0.7853982f * num3 + num2).ToRotationVector2() * 4f;
				prettySparkleParticle.ColorTint = new Color(0.25f, 0.1f, 0.5f, 0.5f);
				prettySparkleParticle.LocalPosition = settings.PositionInWorld;
				prettySparkleParticle.Rotation = vector.ToRotation();
				prettySparkleParticle.Scale = new Vector2(2f, 1f) * 1.1f;
				prettySparkleParticle.FadeInNormalizedTime = 5E-06f;
				prettySparkleParticle.FadeOutNormalizedTime = 0.95f;
				prettySparkleParticle.TimeToLive = num;
				prettySparkleParticle.FadeOutEnd = num;
				prettySparkleParticle.FadeInEnd = num / 2f;
				prettySparkleParticle.FadeOutStart = num / 2f;
				prettySparkleParticle.AdditiveAmount = 0.35f;
				prettySparkleParticle.LocalPosition -= vector * num * 0.25f;
				prettySparkleParticle.Velocity = vector;
				prettySparkleParticle.DrawVerticalAxis = false;
				if (num3 == 1f)
				{
					prettySparkleParticle.Scale *= 1.5f;
					prettySparkleParticle.Velocity *= 1.5f;
					prettySparkleParticle.LocalPosition -= prettySparkleParticle.Velocity * 4f;
				}
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle);
			}
			for (float num4 = 0f; num4 < 3f; num4 += 1f)
			{
				PrettySparkleParticle prettySparkleParticle2 = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				Vector2 vector2 = (0.7853982f + 0.7853982f * num4 + num2).ToRotationVector2() * 4f;
				prettySparkleParticle2.ColorTint = new Color(0.5f, 0.25f, 1f, 1f);
				prettySparkleParticle2.LocalPosition = settings.PositionInWorld;
				prettySparkleParticle2.Rotation = vector2.ToRotation();
				prettySparkleParticle2.Scale = new Vector2(2f, 1f) * 0.7f;
				prettySparkleParticle2.FadeInNormalizedTime = 5E-06f;
				prettySparkleParticle2.FadeOutNormalizedTime = 0.95f;
				prettySparkleParticle2.TimeToLive = num;
				prettySparkleParticle2.FadeOutEnd = num;
				prettySparkleParticle2.FadeInEnd = num / 2f;
				prettySparkleParticle2.FadeOutStart = num / 2f;
				prettySparkleParticle2.LocalPosition -= vector2 * num * 0.25f;
				prettySparkleParticle2.Velocity = vector2;
				prettySparkleParticle2.DrawVerticalAxis = false;
				if (num4 == 1f)
				{
					prettySparkleParticle2.Scale *= 1.5f;
					prettySparkleParticle2.Velocity *= 1.5f;
					prettySparkleParticle2.LocalPosition -= prettySparkleParticle2.Velocity * 4f;
				}
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle2);
			}
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x00529300 File Offset: 0x00527500
		private static void Spawn_SilverBulletSparkle(ParticleOrchestraSettings settings)
		{
			float num = Main.rand.NextFloat() * 6.2831855f;
			Vector2 movementVector = settings.MovementVector;
			Vector2 vector = new Vector2(Main.rand.NextFloat() * 0.2f + 0.4f);
			Main.rand.NextFloat();
			float rotation = 1.5707964f;
			Vector2 value = Main.rand.NextVector2Circular(4f, 4f) * vector;
			PrettySparkleParticle prettySparkleParticle = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
			prettySparkleParticle.AccelerationPerFrame = -movementVector * 1f / 30f;
			prettySparkleParticle.Velocity = movementVector;
			prettySparkleParticle.ColorTint = Color.White;
			prettySparkleParticle.LocalPosition = settings.PositionInWorld + value;
			prettySparkleParticle.Rotation = rotation;
			prettySparkleParticle.Scale = vector;
			prettySparkleParticle.FadeInNormalizedTime = 5E-06f;
			prettySparkleParticle.FadeOutNormalizedTime = 0.95f;
			prettySparkleParticle.FadeInEnd = 10f;
			prettySparkleParticle.FadeOutStart = 20f;
			prettySparkleParticle.FadeOutEnd = 30f;
			prettySparkleParticle.TimeToLive = 30f;
			Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle);
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x00529428 File Offset: 0x00527628
		private static void Spawn_PaladinsHammer(ParticleOrchestraSettings settings)
		{
			float num = Main.rand.NextFloat() * 6.2831855f;
			float num2 = 1f;
			for (float num3 = 0f; num3 < 1f; num3 += 1f / num2)
			{
				float scaleFactor = 0.6f + Main.rand.NextFloat() * 0.35f;
				Vector2 vector = settings.MovementVector * scaleFactor;
				Vector2 vector2 = new Vector2(Main.rand.NextFloat() * 0.4f + 0.2f);
				float f = num + Main.rand.NextFloat() * 6.2831855f;
				float rotation = 1.5707964f;
				0.1f * vector2;
				Vector2 value = Main.rand.NextVector2Circular(12f, 12f) * vector2;
				PrettySparkleParticle prettySparkleParticle = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				prettySparkleParticle.AccelerationPerFrame = -vector * 1f / 30f;
				prettySparkleParticle.Velocity = vector + f.ToRotationVector2() * 2f * scaleFactor;
				prettySparkleParticle.ColorTint = new Color(1f, 0.8f, 0.4f, 0f);
				prettySparkleParticle.LocalPosition = settings.PositionInWorld + value;
				prettySparkleParticle.Rotation = rotation;
				prettySparkleParticle.Scale = vector2;
				prettySparkleParticle.FadeInNormalizedTime = 5E-06f;
				prettySparkleParticle.FadeOutNormalizedTime = 0.95f;
				prettySparkleParticle.TimeToLive = 40f;
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle);
				prettySparkleParticle = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				prettySparkleParticle.AccelerationPerFrame = -vector * 1f / 30f;
				prettySparkleParticle.Velocity = vector * 0.8f + f.ToRotationVector2() * 2f;
				prettySparkleParticle.ColorTint = new Color(255, 255, 255, 0);
				prettySparkleParticle.LocalPosition = settings.PositionInWorld + value;
				prettySparkleParticle.Rotation = rotation;
				prettySparkleParticle.Scale = vector2 * 0.6f;
				prettySparkleParticle.FadeInNormalizedTime = 0.1f;
				prettySparkleParticle.FadeOutNormalizedTime = 0.9f;
				prettySparkleParticle.TimeToLive = 60f;
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle);
			}
			for (int i = 0; i < 2; i++)
			{
				Color newColor = new Color(1f, 0.7f, 0.3f, 0f);
				int num4 = Dust.NewDust(settings.PositionInWorld, 0, 0, 267, 0f, 0f, 0, newColor, 1f);
				Main.dust[num4].velocity = Main.rand.NextVector2Circular(2f, 2f);
				Main.dust[num4].velocity += settings.MovementVector * (0.5f + 0.5f * Main.rand.NextFloat()) * 1.4f;
				Main.dust[num4].noGravity = true;
				Main.dust[num4].scale = 0.1f;
				Main.dust[num4].position += Main.rand.NextVector2Circular(16f, 16f);
				Main.dust[num4].velocity = settings.MovementVector;
				if (num4 != 6000)
				{
					Dust dust = Dust.CloneDust(num4);
					dust.scale /= 2f;
					dust.fadeIn *= 0.75f;
					dust.color = new Color(255, 255, 255, 255);
				}
			}
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x005297FC File Offset: 0x005279FC
		private static void Spawn_PrincessWeapon(ParticleOrchestraSettings settings)
		{
			float num = Main.rand.NextFloat() * 6.2831855f;
			float num2 = 1f;
			for (float num3 = 0f; num3 < 1f; num3 += 1f / num2)
			{
				Vector2 vector = settings.MovementVector * (0.6f + Main.rand.NextFloat() * 0.35f);
				Vector2 vector2 = new Vector2(Main.rand.NextFloat() * 0.4f + 0.2f);
				float f = num + Main.rand.NextFloat() * 6.2831855f;
				float rotation = 1.5707964f;
				Vector2 vector3 = 0.1f * vector2;
				float divider = 60f;
				Vector2 value = Main.rand.NextVector2Circular(8f, 8f) * vector2;
				PrettySparkleParticle prettySparkleParticle = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				prettySparkleParticle.Velocity = f.ToRotationVector2() * vector3 + vector;
				prettySparkleParticle.AccelerationPerFrame = f.ToRotationVector2() * -(vector3 / divider) - vector * 1f / 30f;
				prettySparkleParticle.AccelerationPerFrame = -vector * 1f / 60f;
				prettySparkleParticle.Velocity = vector * 0.66f;
				prettySparkleParticle.ColorTint = Main.hslToRgb((0.92f + Main.rand.NextFloat() * 0.02f) % 1f, 1f, 0.4f + Main.rand.NextFloat() * 0.25f, byte.MaxValue);
				prettySparkleParticle.ColorTint.A = 0;
				prettySparkleParticle.LocalPosition = settings.PositionInWorld + value;
				prettySparkleParticle.Rotation = rotation;
				prettySparkleParticle.Scale = vector2;
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle);
				prettySparkleParticle = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				prettySparkleParticle.Velocity = f.ToRotationVector2() * vector3 + vector;
				prettySparkleParticle.AccelerationPerFrame = f.ToRotationVector2() * -(vector3 / divider) - vector * 1f / 15f;
				prettySparkleParticle.AccelerationPerFrame = -vector * 1f / 60f;
				prettySparkleParticle.Velocity = vector * 0.66f;
				prettySparkleParticle.ColorTint = new Color(255, 255, 255, 0);
				prettySparkleParticle.LocalPosition = settings.PositionInWorld + value;
				prettySparkleParticle.Rotation = rotation;
				prettySparkleParticle.Scale = vector2 * 0.6f;
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle);
			}
			for (int i = 0; i < 2; i++)
			{
				Color newColor = Main.hslToRgb((0.92f + Main.rand.NextFloat() * 0.02f) % 1f, 1f, 0.4f + Main.rand.NextFloat() * 0.25f, byte.MaxValue);
				int num4 = Dust.NewDust(settings.PositionInWorld, 0, 0, 267, 0f, 0f, 0, newColor, 1f);
				Main.dust[num4].velocity = Main.rand.NextVector2Circular(2f, 2f);
				Main.dust[num4].velocity += settings.MovementVector * (0.5f + 0.5f * Main.rand.NextFloat()) * 1.4f;
				Main.dust[num4].noGravity = true;
				Main.dust[num4].scale = 0.1f;
				Main.dust[num4].position += Main.rand.NextVector2Circular(16f, 16f);
				Main.dust[num4].velocity = settings.MovementVector;
				if (num4 != 6000)
				{
					Dust dust = Dust.CloneDust(num4);
					dust.scale /= 2f;
					dust.fadeIn *= 0.75f;
					dust.color = new Color(255, 255, 255, 255);
				}
			}
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x00529C6C File Offset: 0x00527E6C
		private static void Spawn_StardustPunch(ParticleOrchestraSettings settings)
		{
			float num = Main.rand.NextFloat() * 6.2831855f;
			float num2 = 1f;
			for (float num3 = 0f; num3 < 1f; num3 += 1f / num2)
			{
				Vector2 vector = settings.MovementVector * (0.3f + Main.rand.NextFloat() * 0.35f);
				Vector2 vector2 = new Vector2(Main.rand.NextFloat() * 0.4f + 0.4f);
				float f = num + Main.rand.NextFloat() * 6.2831855f;
				float rotation = 1.5707964f;
				Vector2 vector3 = 0.1f * vector2;
				float divider = 60f;
				Vector2 value = Main.rand.NextVector2Circular(8f, 8f) * vector2;
				PrettySparkleParticle prettySparkleParticle = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				prettySparkleParticle.Velocity = f.ToRotationVector2() * vector3 + vector;
				prettySparkleParticle.AccelerationPerFrame = f.ToRotationVector2() * -(vector3 / divider) - vector * 1f / 60f;
				prettySparkleParticle.ColorTint = Main.hslToRgb((0.6f + Main.rand.NextFloat() * 0.05f) % 1f, 1f, 0.4f + Main.rand.NextFloat() * 0.25f, byte.MaxValue);
				prettySparkleParticle.ColorTint.A = 0;
				prettySparkleParticle.LocalPosition = settings.PositionInWorld + value;
				prettySparkleParticle.Rotation = rotation;
				prettySparkleParticle.Scale = vector2;
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle);
				prettySparkleParticle = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				prettySparkleParticle.Velocity = f.ToRotationVector2() * vector3 + vector;
				prettySparkleParticle.AccelerationPerFrame = f.ToRotationVector2() * -(vector3 / divider) - vector * 1f / 30f;
				prettySparkleParticle.ColorTint = new Color(255, 255, 255, 0);
				prettySparkleParticle.LocalPosition = settings.PositionInWorld + value;
				prettySparkleParticle.Rotation = rotation;
				prettySparkleParticle.Scale = vector2 * 0.6f;
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle);
			}
			for (int i = 0; i < 2; i++)
			{
				Color newColor = Main.hslToRgb((0.59f + Main.rand.NextFloat() * 0.05f) % 1f, 1f, 0.4f + Main.rand.NextFloat() * 0.25f, byte.MaxValue);
				int num4 = Dust.NewDust(settings.PositionInWorld, 0, 0, 267, 0f, 0f, 0, newColor, 1f);
				Main.dust[num4].velocity = Main.rand.NextVector2Circular(2f, 2f);
				Main.dust[num4].velocity += settings.MovementVector * (0.5f + 0.5f * Main.rand.NextFloat()) * 1.4f;
				Main.dust[num4].noGravity = true;
				Main.dust[num4].scale = 0.6f + Main.rand.NextFloat() * 2f;
				Main.dust[num4].position += Main.rand.NextVector2Circular(16f, 16f);
				if (num4 != 6000)
				{
					Dust dust = Dust.CloneDust(num4);
					dust.scale /= 2f;
					dust.fadeIn *= 0.75f;
					dust.color = new Color(255, 255, 255, 255);
				}
			}
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x0052A074 File Offset: 0x00528274
		private static void Spawn_RainbowRodHit(ParticleOrchestraSettings settings)
		{
			float num = Main.rand.NextFloat() * 6.2831855f;
			float num2 = 6f;
			float num3 = Main.rand.NextFloat();
			for (float num4 = 0f; num4 < 1f; num4 += 1f / num2)
			{
				Vector2 vector = settings.MovementVector * Main.rand.NextFloatDirection() * 0.15f;
				Vector2 vector2 = new Vector2(Main.rand.NextFloat() * 0.4f + 0.4f);
				float f = num + Main.rand.NextFloat() * 6.2831855f;
				float rotation = 1.5707964f;
				Vector2 vector3 = 1.5f * vector2;
				float divider = 60f;
				Vector2 value = Main.rand.NextVector2Circular(8f, 8f) * vector2;
				PrettySparkleParticle prettySparkleParticle = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				prettySparkleParticle.Velocity = f.ToRotationVector2() * vector3 + vector;
				prettySparkleParticle.AccelerationPerFrame = f.ToRotationVector2() * -(vector3 / divider) - vector * 1f / 60f;
				prettySparkleParticle.ColorTint = Main.hslToRgb((num3 + Main.rand.NextFloat() * 0.33f) % 1f, 1f, 0.4f + Main.rand.NextFloat() * 0.25f, byte.MaxValue);
				prettySparkleParticle.ColorTint.A = 0;
				prettySparkleParticle.LocalPosition = settings.PositionInWorld + value;
				prettySparkleParticle.Rotation = rotation;
				prettySparkleParticle.Scale = vector2;
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle);
				prettySparkleParticle = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				prettySparkleParticle.Velocity = f.ToRotationVector2() * vector3 + vector;
				prettySparkleParticle.AccelerationPerFrame = f.ToRotationVector2() * -(vector3 / divider) - vector * 1f / 60f;
				prettySparkleParticle.ColorTint = new Color(255, 255, 255, 0);
				prettySparkleParticle.LocalPosition = settings.PositionInWorld + value;
				prettySparkleParticle.Rotation = rotation;
				prettySparkleParticle.Scale = vector2 * 0.6f;
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle);
			}
			for (int i = 0; i < 12; i++)
			{
				Color newColor = Main.hslToRgb((num3 + Main.rand.NextFloat() * 0.12f) % 1f, 1f, 0.4f + Main.rand.NextFloat() * 0.25f, byte.MaxValue);
				int num5 = Dust.NewDust(settings.PositionInWorld, 0, 0, 267, 0f, 0f, 0, newColor, 1f);
				Main.dust[num5].velocity = Main.rand.NextVector2Circular(1f, 1f);
				Main.dust[num5].velocity += settings.MovementVector * Main.rand.NextFloatDirection() * 0.5f;
				Main.dust[num5].noGravity = true;
				Main.dust[num5].scale = 0.6f + Main.rand.NextFloat() * 0.9f;
				Main.dust[num5].fadeIn = 0.7f + Main.rand.NextFloat() * 0.8f;
				if (num5 != 6000)
				{
					Dust dust = Dust.CloneDust(num5);
					dust.scale /= 2f;
					dust.fadeIn *= 0.75f;
					dust.color = new Color(255, 255, 255, 255);
				}
			}
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x0052A46C File Offset: 0x0052866C
		private static void Spawn_BlackLightningSmall(ParticleOrchestraSettings settings)
		{
			float num = Main.rand.NextFloat() * 6.2831855f;
			float num2 = (float)Main.rand.Next(1, 3);
			float scaleFactor = 0.7f;
			int num3 = 916;
			Main.instance.LoadProjectile(num3);
			Color value = new Color(255, 255, 255, 255);
			Color indigo = Color.Indigo;
			indigo.A = 0;
			for (float num4 = 0f; num4 < 1f; num4 += 1f / num2)
			{
				float f = 6.2831855f * num4 + num + Main.rand.NextFloatDirection() * 0.25f;
				float scaleFactor2 = Main.rand.NextFloat() * 4f + 0.1f;
				Vector2 vector = Main.rand.NextVector2Circular(12f, 12f) * scaleFactor;
				Color.Lerp(Color.Lerp(Color.Black, indigo, Main.rand.NextFloat() * 0.5f), value, Main.rand.NextFloat() * 0.6f);
				Color colorTint = new Color(0, 0, 0, 255);
				int num5 = Main.rand.Next(4);
				if (num5 == 1)
				{
					colorTint = Color.Lerp(new Color(106, 90, 205, 127), Color.Black, 0.1f + 0.7f * Main.rand.NextFloat());
				}
				if (num5 == 2)
				{
					colorTint = Color.Lerp(new Color(106, 90, 205, 60), Color.Black, 0.1f + 0.8f * Main.rand.NextFloat());
				}
				RandomizedFrameParticle randomizedFrameParticle = ParticleOrchestrator._poolRandomizedFrame.RequestParticle();
				randomizedFrameParticle.SetBasicInfo(TextureAssets.Projectile[num3], null, Vector2.Zero, vector);
				randomizedFrameParticle.SetTypeInfo(Main.projFrames[num3], 2, 24f);
				randomizedFrameParticle.Velocity = f.ToRotationVector2() * scaleFactor2 * new Vector2(1f, 0.5f) * 0.2f + settings.MovementVector;
				randomizedFrameParticle.ColorTint = colorTint;
				randomizedFrameParticle.LocalPosition = settings.PositionInWorld + vector;
				randomizedFrameParticle.Rotation = randomizedFrameParticle.Velocity.ToRotation();
				randomizedFrameParticle.Scale = Vector2.One * 0.5f;
				randomizedFrameParticle.FadeInNormalizedTime = 0.01f;
				randomizedFrameParticle.FadeOutNormalizedTime = 0.5f;
				randomizedFrameParticle.ScaleVelocity = new Vector2(0.025f);
				Main.ParticleSystem_World_OverPlayers.Add(randomizedFrameParticle);
			}
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x0052A700 File Offset: 0x00528900
		private static void Spawn_BlackLightningHit(ParticleOrchestraSettings settings)
		{
			float num = Main.rand.NextFloat() * 6.2831855f;
			float num2 = 7f;
			float scaleFactor = 0.7f;
			int num3 = 916;
			Main.instance.LoadProjectile(num3);
			Color value = new Color(255, 255, 255, 255);
			Color indigo = Color.Indigo;
			indigo.A = 0;
			for (float num4 = 0f; num4 < 1f; num4 += 1f / num2)
			{
				float num5 = 6.2831855f * num4 + num + Main.rand.NextFloatDirection() * 0.25f;
				float scaleFactor2 = Main.rand.NextFloat() * 4f + 0.1f;
				Vector2 vector = Main.rand.NextVector2Circular(12f, 12f) * scaleFactor;
				Color.Lerp(Color.Lerp(Color.Black, indigo, Main.rand.NextFloat() * 0.5f), value, Main.rand.NextFloat() * 0.6f);
				Color colorTint = new Color(0, 0, 0, 255);
				int num6 = Main.rand.Next(4);
				if (num6 == 1)
				{
					colorTint = Color.Lerp(new Color(106, 90, 205, 127), Color.Black, 0.1f + 0.7f * Main.rand.NextFloat());
				}
				if (num6 == 2)
				{
					colorTint = Color.Lerp(new Color(106, 90, 205, 60), Color.Black, 0.1f + 0.8f * Main.rand.NextFloat());
				}
				RandomizedFrameParticle randomizedFrameParticle = ParticleOrchestrator._poolRandomizedFrame.RequestParticle();
				randomizedFrameParticle.SetBasicInfo(TextureAssets.Projectile[num3], null, Vector2.Zero, vector);
				randomizedFrameParticle.SetTypeInfo(Main.projFrames[num3], 2, 24f);
				randomizedFrameParticle.Velocity = num5.ToRotationVector2() * scaleFactor2 * new Vector2(1f, 0.5f);
				randomizedFrameParticle.ColorTint = colorTint;
				randomizedFrameParticle.LocalPosition = settings.PositionInWorld + vector;
				randomizedFrameParticle.Rotation = num5;
				randomizedFrameParticle.Scale = Vector2.One;
				randomizedFrameParticle.FadeInNormalizedTime = 0.01f;
				randomizedFrameParticle.FadeOutNormalizedTime = 0.5f;
				randomizedFrameParticle.ScaleVelocity = new Vector2(0.05f);
				Main.ParticleSystem_World_OverPlayers.Add(randomizedFrameParticle);
			}
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x0052A964 File Offset: 0x00528B64
		private static void Spawn_StellarTune(ParticleOrchestraSettings settings)
		{
			float num = Main.rand.NextFloat() * 6.2831855f;
			float num2 = 5f;
			Vector2 vector = new Vector2(0.7f);
			for (float num3 = 0f; num3 < 1f; num3 += 1f / num2)
			{
				float num4 = 6.2831855f * num3 + num + Main.rand.NextFloatDirection() * 0.25f;
				Vector2 vector2 = 1.5f * vector;
				float divider = 60f;
				Vector2 value = Main.rand.NextVector2Circular(12f, 12f) * vector;
				Color colorTint = Color.Lerp(Color.Gold, Color.HotPink, Main.rand.NextFloat());
				if (Main.rand.Next(2) == 0)
				{
					colorTint = Color.Lerp(Color.Violet, Color.HotPink, Main.rand.NextFloat());
				}
				PrettySparkleParticle prettySparkleParticle = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				prettySparkleParticle.Velocity = num4.ToRotationVector2() * vector2;
				prettySparkleParticle.AccelerationPerFrame = num4.ToRotationVector2() * -(vector2 / divider);
				prettySparkleParticle.ColorTint = colorTint;
				prettySparkleParticle.LocalPosition = settings.PositionInWorld + value;
				prettySparkleParticle.Rotation = num4;
				prettySparkleParticle.Scale = vector * (Main.rand.NextFloat() * 0.8f + 0.2f);
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle);
			}
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x0052AAE0 File Offset: 0x00528CE0
		private static void Spawn_Keybrand(ParticleOrchestraSettings settings)
		{
			float num = Main.rand.NextFloat() * 6.2831855f;
			float num2 = 3f;
			Vector2 vector = new Vector2(0.7f);
			for (float num3 = 0f; num3 < 1f; num3 += 1f / num2)
			{
				float num4 = 6.2831855f * num3 + num + Main.rand.NextFloatDirection() * 0.1f;
				Vector2 vector2 = 1.5f * vector;
				float divider = 60f;
				Vector2 value = Main.rand.NextVector2Circular(4f, 4f) * vector;
				PrettySparkleParticle prettySparkleParticle = ParticleOrchestrator._poolPrettySparkle.RequestParticle();
				prettySparkleParticle.Velocity = num4.ToRotationVector2() * vector2;
				prettySparkleParticle.AccelerationPerFrame = num4.ToRotationVector2() * -(vector2 / divider);
				prettySparkleParticle.ColorTint = Color.Lerp(Color.Gold, Color.OrangeRed, Main.rand.NextFloat());
				prettySparkleParticle.LocalPosition = settings.PositionInWorld + value;
				prettySparkleParticle.Rotation = num4;
				prettySparkleParticle.Scale = vector * 0.8f;
				Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle);
			}
			num += 1f / num2 / 2f * 6.2831855f;
			num = Main.rand.NextFloat() * 6.2831855f;
			for (float num5 = 0f; num5 < 1f; num5 += 1f / num2)
			{
				float num6 = 6.2831855f * num5 + num + Main.rand.NextFloatDirection() * 0.1f;
				Vector2 vector3 = 1f * vector;
				float num7 = 30f;
				Color color = Color.Lerp(Color.Gold, Color.OrangeRed, Main.rand.NextFloat());
				color = Color.Lerp(Color.White, color, 0.5f);
				color.A = 0;
				Vector2 value2 = Main.rand.NextVector2Circular(4f, 4f) * vector;
				FadingParticle fadingParticle = ParticleOrchestrator._poolFading.RequestParticle();
				fadingParticle.SetBasicInfo(TextureAssets.Extra[98], null, Vector2.Zero, Vector2.Zero);
				fadingParticle.SetTypeInfo(num7);
				fadingParticle.Velocity = num6.ToRotationVector2() * vector3;
				fadingParticle.AccelerationPerFrame = num6.ToRotationVector2() * -(vector3 / num7);
				fadingParticle.ColorTint = color;
				fadingParticle.LocalPosition = settings.PositionInWorld + num6.ToRotationVector2() * vector3 * vector * num7 * 0.2f + value2;
				fadingParticle.Rotation = num6 + 1.5707964f;
				fadingParticle.FadeInNormalizedTime = 0.3f;
				fadingParticle.FadeOutNormalizedTime = 0.4f;
				fadingParticle.Scale = new Vector2(0.5f, 1.2f) * 0.8f * vector;
				Main.ParticleSystem_World_OverPlayers.Add(fadingParticle);
			}
			num2 = 1f;
			num = Main.rand.NextFloat() * 6.2831855f;
			for (float num8 = 0f; num8 < 1f; num8 += 1f / num2)
			{
				float num9 = 6.2831855f * num8 + num;
				float typeInfo = 30f;
				Color colorTint = Color.Lerp(Color.CornflowerBlue, Color.White, Main.rand.NextFloat());
				colorTint.A = 127;
				Vector2 value3 = Main.rand.NextVector2Circular(4f, 4f) * vector;
				Vector2 value4 = Main.rand.NextVector2Square(0.7f, 1.3f);
				FadingParticle fadingParticle2 = ParticleOrchestrator._poolFading.RequestParticle();
				fadingParticle2.SetBasicInfo(TextureAssets.Extra[174], null, Vector2.Zero, Vector2.Zero);
				fadingParticle2.SetTypeInfo(typeInfo);
				fadingParticle2.ColorTint = colorTint;
				fadingParticle2.LocalPosition = settings.PositionInWorld + value3;
				fadingParticle2.Rotation = num9 + 1.5707964f;
				fadingParticle2.FadeInNormalizedTime = 0.1f;
				fadingParticle2.FadeOutNormalizedTime = 0.4f;
				fadingParticle2.Scale = new Vector2(0.1f, 0.1f) * vector;
				fadingParticle2.ScaleVelocity = value4 * 1f / 60f;
				fadingParticle2.ScaleAcceleration = value4 * -0.016666668f / 60f;
				Main.ParticleSystem_World_OverPlayers.Add(fadingParticle2);
			}
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x0052AF7C File Offset: 0x0052917C
		private static void Spawn_FlameWaders(ParticleOrchestraSettings settings)
		{
			float num = 60f;
			for (int i = -1; i <= 1; i++)
			{
				int num2 = (int)Main.rand.NextFromList(new short[]
				{
					326,
					327,
					328
				});
				Main.instance.LoadProjectile(num2);
				Player player = Main.player[(int)settings.IndexOfPlayerWhoInvokedThis];
				float scaleFactor = Main.rand.NextFloat() * 0.9f + 0.1f;
				Vector2 vector = settings.PositionInWorld + new Vector2((float)i * 5.3333335f, 0f);
				FlameParticle flameParticle = ParticleOrchestrator._poolFlame.RequestParticle();
				flameParticle.SetBasicInfo(TextureAssets.Projectile[num2], null, Vector2.Zero, vector);
				flameParticle.SetTypeInfo(num, (int)settings.IndexOfPlayerWhoInvokedThis, player.cFlameWaker);
				flameParticle.FadeOutNormalizedTime = 0.4f;
				flameParticle.ScaleAcceleration = Vector2.One * scaleFactor * -0.016666668f / num;
				flameParticle.Scale = Vector2.One * scaleFactor;
				Main.ParticleSystem_World_BehindPlayers.Add(flameParticle);
				if (Main.rand.Next(16) == 0)
				{
					Dust dust = Dust.NewDustDirect(vector, 4, 4, 6, 0f, 0f, 100, default(Color), 1f);
					if (Main.rand.Next(2) == 0)
					{
						dust.noGravity = true;
						dust.fadeIn = 1.15f;
					}
					else
					{
						dust.scale = 0.6f;
					}
					dust.velocity *= 0.6f;
					Dust dust2 = dust;
					dust2.velocity.Y = dust2.velocity.Y - 1.2f;
					dust.noLight = true;
					Dust dust3 = dust;
					dust3.position.Y = dust3.position.Y - 4f;
					dust.shader = GameShaders.Armor.GetSecondaryShader(player.cFlameWaker, player);
				}
			}
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x0052B164 File Offset: 0x00529364
		private static void Spawn_WallOfFleshGoatMountFlames(ParticleOrchestraSettings settings)
		{
			float num = 50f;
			for (int i = -1; i <= 1; i++)
			{
				int num2 = (int)Main.rand.NextFromList(new short[]
				{
					326,
					327,
					328
				});
				Main.instance.LoadProjectile(num2);
				Player player = Main.player[(int)settings.IndexOfPlayerWhoInvokedThis];
				float scaleFactor = Main.rand.NextFloat() * 0.9f + 0.1f;
				Vector2 vector = settings.PositionInWorld + new Vector2((float)i * 5.3333335f, 0f);
				FlameParticle flameParticle = ParticleOrchestrator._poolFlame.RequestParticle();
				flameParticle.SetBasicInfo(TextureAssets.Projectile[num2], null, Vector2.Zero, vector);
				flameParticle.SetTypeInfo(num, (int)settings.IndexOfPlayerWhoInvokedThis, player.cMount);
				flameParticle.FadeOutNormalizedTime = 0.3f;
				flameParticle.ScaleAcceleration = Vector2.One * scaleFactor * -0.016666668f / num;
				flameParticle.Scale = Vector2.One * scaleFactor;
				Main.ParticleSystem_World_BehindPlayers.Add(flameParticle);
				if (Main.rand.Next(8) == 0)
				{
					Dust dust = Dust.NewDustDirect(vector, 4, 4, 6, 0f, 0f, 100, default(Color), 1f);
					if (Main.rand.Next(2) == 0)
					{
						dust.noGravity = true;
						dust.fadeIn = 1.15f;
					}
					else
					{
						dust.scale = 0.6f;
					}
					dust.velocity *= 0.6f;
					Dust dust2 = dust;
					dust2.velocity.Y = dust2.velocity.Y - 1.2f;
					dust.noLight = true;
					Dust dust3 = dust;
					dust3.position.Y = dust3.position.Y - 4f;
					dust.shader = GameShaders.Armor.GetSecondaryShader(player.cMount, player);
				}
			}
		}

		// Token: 0x0400474E RID: 18254
		private static ParticlePool<FadingParticle> _poolFading = new ParticlePool<FadingParticle>(200, new ParticlePool<FadingParticle>.ParticleInstantiator(ParticleOrchestrator.GetNewFadingParticle));

		// Token: 0x0400474F RID: 18255
		private static ParticlePool<LittleFlyingCritterParticle> _poolFlies = new ParticlePool<LittleFlyingCritterParticle>(200, new ParticlePool<LittleFlyingCritterParticle>.ParticleInstantiator(ParticleOrchestrator.GetNewPooFlyParticle));

		// Token: 0x04004750 RID: 18256
		private static ParticlePool<ItemTransferParticle> _poolItemTransfer = new ParticlePool<ItemTransferParticle>(100, new ParticlePool<ItemTransferParticle>.ParticleInstantiator(ParticleOrchestrator.GetNewItemTransferParticle));

		// Token: 0x04004751 RID: 18257
		private static ParticlePool<FlameParticle> _poolFlame = new ParticlePool<FlameParticle>(200, new ParticlePool<FlameParticle>.ParticleInstantiator(ParticleOrchestrator.GetNewFlameParticle));

		// Token: 0x04004752 RID: 18258
		private static ParticlePool<RandomizedFrameParticle> _poolRandomizedFrame = new ParticlePool<RandomizedFrameParticle>(200, new ParticlePool<RandomizedFrameParticle>.ParticleInstantiator(ParticleOrchestrator.GetNewRandomizedFrameParticle));

		// Token: 0x04004753 RID: 18259
		private static ParticlePool<PrettySparkleParticle> _poolPrettySparkle = new ParticlePool<PrettySparkleParticle>(200, new ParticlePool<PrettySparkleParticle>.ParticleInstantiator(ParticleOrchestrator.GetNewPrettySparkleParticle));

		// Token: 0x04004754 RID: 18260
		private static ParticlePool<GasParticle> _poolGas = new ParticlePool<GasParticle>(200, new ParticlePool<GasParticle>.ParticleInstantiator(ParticleOrchestrator.GetNewGasParticle));
	}
}
