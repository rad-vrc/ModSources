using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria.DataStructures;
using Terraria.GameContent.Ambience;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x02000564 RID: 1380
	public class AmbientSky : CustomSky
	{
		// Token: 0x06004135 RID: 16693 RVA: 0x005E5141 File Offset: 0x005E3341
		public override void Activate(Vector2 position, params object[] args)
		{
			this._isActive = true;
		}

		// Token: 0x06004136 RID: 16694 RVA: 0x005E514A File Offset: 0x005E334A
		public override void Deactivate(params object[] args)
		{
			this._isActive = false;
		}

		// Token: 0x06004137 RID: 16695 RVA: 0x005E5153 File Offset: 0x005E3353
		private bool AnActiveSkyConflictsWithAmbience()
		{
			return SkyManager.Instance["MonolithMoonLord"].IsActive() || SkyManager.Instance["MoonLord"].IsActive();
		}

		// Token: 0x06004138 RID: 16696 RVA: 0x005E5184 File Offset: 0x005E3384
		public override void Update(GameTime gameTime)
		{
			if (Main.gamePaused)
			{
				return;
			}
			this._frameCounter++;
			if (Main.netMode != 2 && this.AnActiveSkyConflictsWithAmbience() && SkyManager.Instance["Ambience"].IsActive())
			{
				SkyManager.Instance.Deactivate("Ambience", Array.Empty<object>());
			}
			foreach (SlotVector<AmbientSky.SkyEntity>.ItemPair item in this._entities)
			{
				AmbientSky.SkyEntity value = item.Value;
				value.Update(this._frameCounter);
				if (!value.IsActive)
				{
					this._entities.Remove(item.Id);
					if (Main.netMode != 2 && this._entities.Count == 0 && SkyManager.Instance["Ambience"].IsActive())
					{
						SkyManager.Instance.Deactivate("Ambience", Array.Empty<object>());
					}
				}
			}
		}

		// Token: 0x06004139 RID: 16697 RVA: 0x005E5284 File Offset: 0x005E3484
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (Main.gameMenu && Main.netMode == 0 && SkyManager.Instance["Ambience"].IsActive())
			{
				this._entities.Clear();
				SkyManager.Instance.Deactivate("Ambience", Array.Empty<object>());
			}
			foreach (SlotVector<AmbientSky.SkyEntity>.ItemPair itemPair in this._entities)
			{
				itemPair.Value.Draw(spriteBatch, 3f, minDepth, maxDepth);
			}
		}

		// Token: 0x0600413A RID: 16698 RVA: 0x005E5320 File Offset: 0x005E3520
		public override bool IsActive()
		{
			return this._isActive;
		}

		// Token: 0x0600413B RID: 16699 RVA: 0x005E5328 File Offset: 0x005E3528
		public override void Reset()
		{
		}

		// Token: 0x0600413C RID: 16700 RVA: 0x005E532C File Offset: 0x005E352C
		public void Spawn(Player player, SkyEntityType type, int seed)
		{
			FastRandom random = new FastRandom(seed);
			switch (type)
			{
			case SkyEntityType.BirdsV:
				this._entities.Add(new AmbientSky.BirdsPackSkyEntity(player, random));
				break;
			case SkyEntityType.Wyvern:
				this._entities.Add(new AmbientSky.WyvernSkyEntity(player, random));
				break;
			case SkyEntityType.Airship:
				this._entities.Add(new AmbientSky.AirshipSkyEntity(player, random));
				break;
			case SkyEntityType.AirBalloon:
				this._entities.Add(new AmbientSky.AirBalloonSkyEntity(player, random));
				break;
			case SkyEntityType.Eyeball:
				this._entities.Add(new AmbientSky.EOCSkyEntity(player, random));
				break;
			case SkyEntityType.Meteor:
				this._entities.Add(new AmbientSky.MeteorSkyEntity(player, random));
				break;
			case SkyEntityType.Bats:
			{
				List<AmbientSky.BatsGroupSkyEntity> list5 = AmbientSky.BatsGroupSkyEntity.CreateGroup(player, random);
				for (int i = 0; i < list5.Count; i++)
				{
					this._entities.Add(list5[i]);
				}
				break;
			}
			case SkyEntityType.Butterflies:
				this._entities.Add(new AmbientSky.ButterfliesSkyEntity(player, random));
				break;
			case SkyEntityType.LostKite:
				this._entities.Add(new AmbientSky.LostKiteSkyEntity(player, random));
				break;
			case SkyEntityType.Vulture:
				this._entities.Add(new AmbientSky.VultureSkyEntity(player, random));
				break;
			case SkyEntityType.PixiePosse:
				this._entities.Add(new AmbientSky.PixiePosseSkyEntity(player, random));
				break;
			case SkyEntityType.Seagulls:
			{
				List<AmbientSky.SeagullsGroupSkyEntity> list6 = AmbientSky.SeagullsGroupSkyEntity.CreateGroup(player, random);
				for (int j = 0; j < list6.Count; j++)
				{
					this._entities.Add(list6[j]);
				}
				break;
			}
			case SkyEntityType.SlimeBalloons:
			{
				List<AmbientSky.SlimeBalloonGroupSkyEntity> list7 = AmbientSky.SlimeBalloonGroupSkyEntity.CreateGroup(player, random);
				for (int k = 0; k < list7.Count; k++)
				{
					this._entities.Add(list7[k]);
				}
				break;
			}
			case SkyEntityType.Gastropods:
			{
				List<AmbientSky.GastropodGroupSkyEntity> list8 = AmbientSky.GastropodGroupSkyEntity.CreateGroup(player, random);
				for (int l = 0; l < list8.Count; l++)
				{
					this._entities.Add(list8[l]);
				}
				break;
			}
			case SkyEntityType.Pegasus:
				this._entities.Add(new AmbientSky.PegasusSkyEntity(player, random));
				break;
			case SkyEntityType.EaterOfSouls:
				this._entities.Add(new AmbientSky.EOSSkyEntity(player, random));
				break;
			case SkyEntityType.Crimera:
				this._entities.Add(new AmbientSky.CrimeraSkyEntity(player, random));
				break;
			case SkyEntityType.Hellbats:
			{
				List<AmbientSky.HellBatsGoupSkyEntity> list9 = AmbientSky.HellBatsGoupSkyEntity.CreateGroup(player, random);
				for (int m = 0; m < list9.Count; m++)
				{
					this._entities.Add(list9[m]);
				}
				break;
			}
			}
			if (Main.netMode != 2 && !this.AnActiveSkyConflictsWithAmbience() && !SkyManager.Instance["Ambience"].IsActive())
			{
				SkyManager.Instance.Activate("Ambience", default(Vector2), Array.Empty<object>());
			}
		}

		// Token: 0x040058A7 RID: 22695
		private bool _isActive;

		// Token: 0x040058A8 RID: 22696
		private readonly SlotVector<AmbientSky.SkyEntity> _entities = new SlotVector<AmbientSky.SkyEntity>(500);

		// Token: 0x040058A9 RID: 22697
		private int _frameCounter;

		// Token: 0x02000C2D RID: 3117
		private abstract class SkyEntity
		{
			// Token: 0x17000959 RID: 2393
			// (get) Token: 0x06005F36 RID: 24374 RVA: 0x006CD68D File Offset: 0x006CB88D
			public Rectangle SourceRectangle
			{
				get
				{
					return this.Frame.GetSourceRectangle(this.Texture.Value);
				}
			}

			// Token: 0x06005F37 RID: 24375 RVA: 0x006CD6A5 File Offset: 0x006CB8A5
			protected void NextFrame()
			{
				this.Frame.CurrentRow = (this.Frame.CurrentRow + 1) % this.Frame.RowCount;
			}

			// Token: 0x06005F38 RID: 24376
			public abstract Color GetColor(Color backgroundColor);

			// Token: 0x06005F39 RID: 24377
			public abstract void Update(int frameCount);

			// Token: 0x06005F3A RID: 24378 RVA: 0x006CD6CC File Offset: 0x006CB8CC
			protected void SetPositionInWorldBasedOnScreenSpace(Vector2 actualWorldSpace)
			{
				Vector2 vector = actualWorldSpace - Main.Camera.Center;
				Vector2 position = Main.Camera.Center + vector * (this.Depth / 3f);
				this.Position = position;
			}

			// Token: 0x06005F3B RID: 24379
			public abstract Vector2 GetDrawPosition();

			// Token: 0x06005F3C RID: 24380 RVA: 0x006CD713 File Offset: 0x006CB913
			public virtual void Draw(SpriteBatch spriteBatch, float depthScale, float minDepth, float maxDepth)
			{
				this.CommonDraw(spriteBatch, depthScale, minDepth, maxDepth);
			}

			// Token: 0x06005F3D RID: 24381 RVA: 0x006CD720 File Offset: 0x006CB920
			public void CommonDraw(SpriteBatch spriteBatch, float depthScale, float minDepth, float maxDepth)
			{
				if (this.Depth > minDepth && this.Depth <= maxDepth)
				{
					Vector2 drawPositionByDepth = this.GetDrawPositionByDepth();
					Color color = this.GetColor(Main.ColorOfTheSkies) * Main.atmo;
					Vector2 origin = this.SourceRectangle.Size() / 2f;
					float scale = depthScale / this.Depth;
					spriteBatch.Draw(this.Texture.Value, drawPositionByDepth - Main.Camera.UnscaledPosition, new Rectangle?(this.SourceRectangle), color, this.Rotation, origin, scale, this.Effects, 0f);
				}
			}

			// Token: 0x06005F3E RID: 24382 RVA: 0x006CD7C0 File Offset: 0x006CB9C0
			internal Vector2 GetDrawPositionByDepth()
			{
				return (this.GetDrawPosition() - Main.Camera.Center) * new Vector2(1f / this.Depth, 0.9f / this.Depth) + Main.Camera.Center;
			}

			// Token: 0x06005F3F RID: 24383 RVA: 0x006CD814 File Offset: 0x006CBA14
			internal float Helper_GetOpacityWithAccountingForOceanWaterLine()
			{
				Vector2 vector = this.GetDrawPositionByDepth() - Main.Camera.UnscaledPosition;
				int num = this.SourceRectangle.Height / 2;
				float t = vector.Y + (float)num;
				float yScreenPosition = AmbientSkyDrawCache.Instance.OceanLineInfo.YScreenPosition;
				float lerpValue = Utils.GetLerpValue(yScreenPosition - 10f, yScreenPosition - 2f, t, true);
				lerpValue *= AmbientSkyDrawCache.Instance.OceanLineInfo.OceanOpacity;
				return 1f - lerpValue;
			}

			// Token: 0x0400789F RID: 30879
			public Vector2 Position;

			// Token: 0x040078A0 RID: 30880
			public Asset<Texture2D> Texture;

			// Token: 0x040078A1 RID: 30881
			public SpriteFrame Frame;

			// Token: 0x040078A2 RID: 30882
			public float Depth;

			// Token: 0x040078A3 RID: 30883
			public SpriteEffects Effects;

			// Token: 0x040078A4 RID: 30884
			public bool IsActive = true;

			// Token: 0x040078A5 RID: 30885
			public float Rotation;
		}

		// Token: 0x02000C2E RID: 3118
		private class FadingSkyEntity : AmbientSky.SkyEntity
		{
			// Token: 0x06005F41 RID: 24385 RVA: 0x006CD89C File Offset: 0x006CBA9C
			public FadingSkyEntity()
			{
				this.Opacity = 0f;
				this.TimeEntitySpawnedIn = -1;
				this.BrightnessLerper = 0f;
				this.FinalOpacityMultiplier = 1f;
				this.OpacityNormalizedTimeToFadeIn = 0.1f;
				this.OpacityNormalizedTimeToFadeOut = 0.9f;
			}

			// Token: 0x06005F42 RID: 24386 RVA: 0x006CD8F0 File Offset: 0x006CBAF0
			public override void Update(int frameCount)
			{
				if (!this.IsMovementDone(frameCount))
				{
					this.UpdateOpacity(frameCount);
					if ((frameCount + this.FrameOffset) % this.FramingSpeed == 0)
					{
						base.NextFrame();
					}
					this.UpdateVelocity(frameCount);
					this.Position += this.Velocity;
				}
			}

			// Token: 0x06005F43 RID: 24387 RVA: 0x006CD942 File Offset: 0x006CBB42
			public virtual void UpdateVelocity(int frameCount)
			{
			}

			// Token: 0x06005F44 RID: 24388 RVA: 0x006CD944 File Offset: 0x006CBB44
			private void UpdateOpacity(int frameCount)
			{
				int num = frameCount - this.TimeEntitySpawnedIn;
				if ((float)num >= (float)this.LifeTime * this.OpacityNormalizedTimeToFadeOut)
				{
					this.Opacity = Utils.GetLerpValue((float)this.LifeTime, (float)this.LifeTime * this.OpacityNormalizedTimeToFadeOut, (float)num, true);
					return;
				}
				this.Opacity = Utils.GetLerpValue(0f, (float)this.LifeTime * this.OpacityNormalizedTimeToFadeIn, (float)num, true);
			}

			// Token: 0x06005F45 RID: 24389 RVA: 0x006CD9B1 File Offset: 0x006CBBB1
			private bool IsMovementDone(int frameCount)
			{
				if (this.TimeEntitySpawnedIn == -1)
				{
					this.TimeEntitySpawnedIn = frameCount;
				}
				if (frameCount - this.TimeEntitySpawnedIn >= this.LifeTime)
				{
					this.IsActive = false;
					return true;
				}
				return false;
			}

			// Token: 0x06005F46 RID: 24390 RVA: 0x006CD9DD File Offset: 0x006CBBDD
			public override Color GetColor(Color backgroundColor)
			{
				return Color.Lerp(backgroundColor, Color.White, this.BrightnessLerper) * this.Opacity * this.FinalOpacityMultiplier * base.Helper_GetOpacityWithAccountingForOceanWaterLine();
			}

			// Token: 0x06005F47 RID: 24391 RVA: 0x006CDA14 File Offset: 0x006CBC14
			public void StartFadingOut(int currentFrameCount)
			{
				int num = (int)((float)this.LifeTime * this.OpacityNormalizedTimeToFadeOut);
				int num2 = currentFrameCount - num;
				if (num2 < this.TimeEntitySpawnedIn)
				{
					this.TimeEntitySpawnedIn = num2;
				}
			}

			// Token: 0x06005F48 RID: 24392 RVA: 0x006CDA45 File Offset: 0x006CBC45
			public override Vector2 GetDrawPosition()
			{
				return this.Position;
			}

			// Token: 0x040078A6 RID: 30886
			protected int LifeTime;

			// Token: 0x040078A7 RID: 30887
			protected Vector2 Velocity;

			// Token: 0x040078A8 RID: 30888
			protected int FramingSpeed;

			// Token: 0x040078A9 RID: 30889
			protected int TimeEntitySpawnedIn;

			// Token: 0x040078AA RID: 30890
			protected float Opacity;

			// Token: 0x040078AB RID: 30891
			protected float BrightnessLerper;

			// Token: 0x040078AC RID: 30892
			protected float FinalOpacityMultiplier;

			// Token: 0x040078AD RID: 30893
			protected float OpacityNormalizedTimeToFadeIn;

			// Token: 0x040078AE RID: 30894
			protected float OpacityNormalizedTimeToFadeOut;

			// Token: 0x040078AF RID: 30895
			protected int FrameOffset;
		}

		// Token: 0x02000C2F RID: 3119
		private class ButterfliesSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06005F49 RID: 24393 RVA: 0x006CDA50 File Offset: 0x006CBC50
			public ButterfliesSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals <= 0f) ? 1 : 0);
				int num = 100;
				if (this.Effects == 1)
				{
					this.Position.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					this.Position.X = virtualCamera.Position.X - (float)num;
				}
				this.Position.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 4000f) + 4000f;
				this.Depth = random.NextFloat() * 3f + 3f;
				base.SetPositionInWorldBasedOnScreenSpace(this.Position);
				int num2 = random.Next(2) + 1;
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/ButterflySwarm" + num2.ToString());
				this.Frame = new SpriteFrame(1, (num2 == 2) ? 19 : 17);
				this.LifeTime = random.Next(60, 121) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.15f;
				this.OpacityNormalizedTimeToFadeOut = 0.85f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 5;
			}

			// Token: 0x06005F4A RID: 24394 RVA: 0x006CDBAC File Offset: 0x006CBDAC
			public override void UpdateVelocity(int frameCount)
			{
				float num = 0.1f + Math.Abs(Main.WindForVisuals) * 0.05f;
				this.Velocity = new Vector2(num * (float)((this.Effects != 1) ? 1 : -1), 0f);
			}

			// Token: 0x06005F4B RID: 24395 RVA: 0x006CDBF0 File Offset: 0x006CBDF0
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if (Main.IsItRaining || !Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}
		}

		// Token: 0x02000C30 RID: 3120
		private class LostKiteSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06005F4C RID: 24396 RVA: 0x006CDC18 File Offset: 0x006CBE18
			public LostKiteSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals <= 0f) ? 1 : 0);
				int num = 100;
				if (this.Effects == 1)
				{
					this.Position.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					this.Position.X = virtualCamera.Position.X - (float)num;
				}
				this.Position.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 2400f) + 2400f;
				this.Depth = random.NextFloat() * 3f + 3f;
				base.SetPositionInWorldBasedOnScreenSpace(this.Position);
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/LostKite");
				this.Frame = new SpriteFrame(1, 42);
				this.LifeTime = random.Next(60, 121) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.15f;
				this.OpacityNormalizedTimeToFadeOut = 0.85f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 6;
				int num2 = random.Next((int)this.Frame.RowCount);
				for (int i = 0; i < num2; i++)
				{
					base.NextFrame();
				}
			}

			// Token: 0x06005F4D RID: 24397 RVA: 0x006CDD80 File Offset: 0x006CBF80
			public override void UpdateVelocity(int frameCount)
			{
				float num = 1.2f + Math.Abs(Main.WindForVisuals) * 3f;
				if (Main.IsItStorming)
				{
					num *= 1.5f;
				}
				this.Velocity = new Vector2(num * (float)((this.Effects != 1) ? 1 : -1), 0f);
			}

			// Token: 0x06005F4E RID: 24398 RVA: 0x006CDDD4 File Offset: 0x006CBFD4
			public override void Update(int frameCount)
			{
				if (Main.IsItStorming)
				{
					this.FramingSpeed = 4;
				}
				this.Effects = ((Main.WindForVisuals <= 0f) ? 1 : 0);
				base.Update(frameCount);
				if (!Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}
		}

		// Token: 0x02000C31 RID: 3121
		private class PegasusSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06005F4F RID: 24399 RVA: 0x006CDE24 File Offset: 0x006CC024
			public PegasusSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals <= 0f) ? 1 : 0);
				int num = 100;
				if (this.Effects == 1)
				{
					this.Position.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					this.Position.X = virtualCamera.Position.X - (float)num;
				}
				this.Position.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 2400f) + 2400f;
				this.Depth = random.NextFloat() * 3f + 3f;
				base.SetPositionInWorldBasedOnScreenSpace(this.Position);
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/Pegasus");
				this.Frame = new SpriteFrame(1, 11);
				this.LifeTime = random.Next(60, 121) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.15f;
				this.OpacityNormalizedTimeToFadeOut = 0.85f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 5;
			}

			// Token: 0x06005F50 RID: 24400 RVA: 0x006CDF64 File Offset: 0x006CC164
			public override void UpdateVelocity(int frameCount)
			{
				float num = 1.5f + Math.Abs(Main.WindForVisuals) * 0.6f;
				this.Velocity = new Vector2(num * (float)((this.Effects != 1) ? 1 : -1), 0f);
			}

			// Token: 0x06005F51 RID: 24401 RVA: 0x006CDFA8 File Offset: 0x006CC1A8
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if (Main.IsItRaining || !Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}

			// Token: 0x06005F52 RID: 24402 RVA: 0x006CDFCD File Offset: 0x006CC1CD
			public override Color GetColor(Color backgroundColor)
			{
				return base.GetColor(backgroundColor) * Main.bgAlphaFrontLayer[6];
			}
		}

		// Token: 0x02000C32 RID: 3122
		private class VultureSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06005F53 RID: 24403 RVA: 0x006CDFE4 File Offset: 0x006CC1E4
			public VultureSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals <= 0f) ? 1 : 0);
				int num = 100;
				if (this.Effects == 1)
				{
					this.Position.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					this.Position.X = virtualCamera.Position.X - (float)num;
				}
				this.Position.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 2400f) + 2400f;
				this.Depth = random.NextFloat() * 3f + 3f;
				base.SetPositionInWorldBasedOnScreenSpace(this.Position);
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/Vulture");
				this.Frame = new SpriteFrame(1, 10);
				this.LifeTime = random.Next(60, 121) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.15f;
				this.OpacityNormalizedTimeToFadeOut = 0.85f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 5;
			}

			// Token: 0x06005F54 RID: 24404 RVA: 0x006CE124 File Offset: 0x006CC324
			public override void UpdateVelocity(int frameCount)
			{
				float num = 3f + Math.Abs(Main.WindForVisuals) * 0.8f;
				this.Velocity = new Vector2(num * (float)((this.Effects != 1) ? 1 : -1), 0f);
			}

			// Token: 0x06005F55 RID: 24405 RVA: 0x006CE168 File Offset: 0x006CC368
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if (Main.IsItRaining || !Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}

			// Token: 0x06005F56 RID: 24406 RVA: 0x006CE18D File Offset: 0x006CC38D
			public override Color GetColor(Color backgroundColor)
			{
				return base.GetColor(backgroundColor) * Math.Max(Main.bgAlphaFrontLayer[2], Main.bgAlphaFrontLayer[5]);
			}
		}

		// Token: 0x02000C33 RID: 3123
		private class PixiePosseSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06005F57 RID: 24407 RVA: 0x006CE1B0 File Offset: 0x006CC3B0
			public PixiePosseSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals <= 0f) ? 1 : 0);
				int num = 100;
				if (this.Effects == 1)
				{
					this.Position.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					this.Position.X = virtualCamera.Position.X - (float)num;
				}
				this.Position.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 4000f) + 4000f;
				this.Depth = random.NextFloat() * 3f + 2f;
				base.SetPositionInWorldBasedOnScreenSpace(this.Position);
				if (!Main.dayTime)
				{
					this.pixieType = 2;
				}
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/PixiePosse" + this.pixieType.ToString());
				this.Frame = new SpriteFrame(1, 25);
				this.LifeTime = random.Next(60, 121) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.15f;
				this.OpacityNormalizedTimeToFadeOut = 0.85f;
				this.BrightnessLerper = 0.6f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 5;
			}

			// Token: 0x06005F58 RID: 24408 RVA: 0x006CE310 File Offset: 0x006CC510
			public override void UpdateVelocity(int frameCount)
			{
				float num = 0.12f + Math.Abs(Main.WindForVisuals) * 0.08f;
				this.Velocity = new Vector2(num * (float)((this.Effects != 1) ? 1 : -1), 0f);
			}

			// Token: 0x06005F59 RID: 24409 RVA: 0x006CE354 File Offset: 0x006CC554
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if ((this.pixieType == 1 && !Main.dayTime) || (this.pixieType == 2 && Main.dayTime) || Main.IsItRaining || Main.eclipse || Main.bloodMoon || Main.pumpkinMoon || Main.snowMoon)
				{
					base.StartFadingOut(frameCount);
				}
			}

			// Token: 0x06005F5A RID: 24410 RVA: 0x006CE3B2 File Offset: 0x006CC5B2
			public override void Draw(SpriteBatch spriteBatch, float depthScale, float minDepth, float maxDepth)
			{
				base.CommonDraw(spriteBatch, depthScale - 0.1f, minDepth, maxDepth);
			}

			// Token: 0x040078B0 RID: 30896
			private int pixieType = 1;
		}

		// Token: 0x02000C34 RID: 3124
		private class BirdsPackSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06005F5B RID: 24411 RVA: 0x006CE3C8 File Offset: 0x006CC5C8
			public BirdsPackSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals <= 0f) ? 1 : 0);
				int num = 100;
				if (this.Effects == 1)
				{
					this.Position.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					this.Position.X = virtualCamera.Position.X - (float)num;
				}
				this.Position.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 2400f) + 2400f;
				this.Depth = random.NextFloat() * 3f + 3f;
				base.SetPositionInWorldBasedOnScreenSpace(this.Position);
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/BirdsVShape");
				this.Frame = new SpriteFrame(1, 4);
				this.LifeTime = random.Next(60, 121) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.15f;
				this.OpacityNormalizedTimeToFadeOut = 0.85f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 5;
			}

			// Token: 0x06005F5C RID: 24412 RVA: 0x006CE508 File Offset: 0x006CC708
			public override void UpdateVelocity(int frameCount)
			{
				float num = 3f + Math.Abs(Main.WindForVisuals) * 0.8f;
				this.Velocity = new Vector2(num * (float)((this.Effects != 1) ? 1 : -1), 0f);
			}

			// Token: 0x06005F5D RID: 24413 RVA: 0x006CE54C File Offset: 0x006CC74C
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if (Main.IsItRaining || !Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}
		}

		// Token: 0x02000C35 RID: 3125
		private class SeagullsGroupSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06005F5E RID: 24414 RVA: 0x006CE574 File Offset: 0x006CC774
			public SeagullsGroupSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals <= 0f) ? 1 : 0);
				int num = 100;
				if (this.Effects == 1)
				{
					this.Position.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					this.Position.X = virtualCamera.Position.X - (float)num;
				}
				this.Position.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 2400f) + 2400f;
				this.Depth = random.NextFloat() * 3f + 3f;
				base.SetPositionInWorldBasedOnScreenSpace(this.Position);
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/Seagull");
				this.Frame = new SpriteFrame(1, 9);
				this.LifeTime = random.Next(60, 121) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.1f;
				this.OpacityNormalizedTimeToFadeOut = 0.9f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 4;
				this.FrameOffset = random.Next(0, (int)this.Frame.RowCount);
				int num2 = random.Next((int)this.Frame.RowCount);
				for (int i = 0; i < num2; i++)
				{
					base.NextFrame();
				}
			}

			// Token: 0x06005F5F RID: 24415 RVA: 0x006CE6F4 File Offset: 0x006CC8F4
			public override void UpdateVelocity(int frameCount)
			{
				Vector2 vector = this._magnetAccelerations * new Vector2((float)Math.Sign(this._magnetPointTarget.X - this._positionVsMagnet.X), (float)Math.Sign(this._magnetPointTarget.Y - this._positionVsMagnet.Y));
				this._velocityVsMagnet += vector;
				this._positionVsMagnet += this._velocityVsMagnet;
				float x = 4f * (float)((this.Effects != 1) ? 1 : -1);
				this.Velocity = new Vector2(x, 0f) + this._velocityVsMagnet;
			}

			// Token: 0x06005F60 RID: 24416 RVA: 0x006CE7A6 File Offset: 0x006CC9A6
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if (Main.IsItRaining || !Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}

			// Token: 0x06005F61 RID: 24417 RVA: 0x006CE7CB File Offset: 0x006CC9CB
			public void SetMagnetization(Vector2 accelerations, Vector2 targetOffset)
			{
				this._magnetAccelerations = accelerations;
				this._magnetPointTarget = targetOffset;
			}

			// Token: 0x06005F62 RID: 24418 RVA: 0x006CE7DB File Offset: 0x006CC9DB
			public override Color GetColor(Color backgroundColor)
			{
				return base.GetColor(backgroundColor) * Main.bgAlphaFrontLayer[4];
			}

			// Token: 0x06005F63 RID: 24419 RVA: 0x006CE7F0 File Offset: 0x006CC9F0
			public override void Draw(SpriteBatch spriteBatch, float depthScale, float minDepth, float maxDepth)
			{
				base.CommonDraw(spriteBatch, depthScale - 1.5f, minDepth, maxDepth);
			}

			// Token: 0x06005F64 RID: 24420 RVA: 0x006CE804 File Offset: 0x006CCA04
			public static List<AmbientSky.SeagullsGroupSkyEntity> CreateGroup(Player player, FastRandom random)
			{
				List<AmbientSky.SeagullsGroupSkyEntity> list = new List<AmbientSky.SeagullsGroupSkyEntity>();
				int num = 100;
				int num2 = random.Next(5, 9);
				float num3 = 100f;
				VirtualCamera virtualCamera = new VirtualCamera(player);
				SpriteEffects spriteEffects = (Main.WindForVisuals <= 0f) ? 1 : 0;
				Vector2 vector = default(Vector2);
				if (spriteEffects == 1)
				{
					vector.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					vector.X = virtualCamera.Position.X - (float)num;
				}
				vector.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 2400f) + 2400f;
				float num4 = random.NextFloat() * 2f + 1f;
				int num5 = random.Next(30, 61) * 60;
				Vector2 vector2;
				vector2..ctor(random.NextFloat() * 0.5f + 0.5f, random.NextFloat() * 0.5f + 0.5f);
				Vector2 targetOffset = new Vector2(random.NextFloat() * 2f - 1f, random.NextFloat() * 2f - 1f) * num3;
				for (int i = 0; i < num2; i++)
				{
					AmbientSky.SeagullsGroupSkyEntity seagullsGroupSkyEntity = new AmbientSky.SeagullsGroupSkyEntity(player, random);
					seagullsGroupSkyEntity.Depth = num4 + random.NextFloat() * 0.5f;
					seagullsGroupSkyEntity.Position = vector + new Vector2(random.NextFloat() * 20f - 10f, random.NextFloat() * 3f) * 50f;
					seagullsGroupSkyEntity.Effects = spriteEffects;
					seagullsGroupSkyEntity.SetPositionInWorldBasedOnScreenSpace(seagullsGroupSkyEntity.Position);
					seagullsGroupSkyEntity.LifeTime = num5 + random.Next(301);
					seagullsGroupSkyEntity.SetMagnetization(vector2 * (random.NextFloat() * 0.3f + 0.85f) * 0.05f, targetOffset);
					list.Add(seagullsGroupSkyEntity);
				}
				return list;
			}

			// Token: 0x040078B1 RID: 30897
			private Vector2 _magnetAccelerations;

			// Token: 0x040078B2 RID: 30898
			private Vector2 _magnetPointTarget;

			// Token: 0x040078B3 RID: 30899
			private Vector2 _positionVsMagnet;

			// Token: 0x040078B4 RID: 30900
			private Vector2 _velocityVsMagnet;
		}

		// Token: 0x02000C36 RID: 3126
		private class GastropodGroupSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06005F65 RID: 24421 RVA: 0x006CEA18 File Offset: 0x006CCC18
			public GastropodGroupSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals <= 0f) ? 1 : 0);
				int num = 100;
				if (this.Effects == 1)
				{
					this.Position.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					this.Position.X = virtualCamera.Position.X - (float)num;
				}
				this.Position.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 3200f) + 3200f;
				this.Depth = random.NextFloat() * 3f + 2f;
				base.SetPositionInWorldBasedOnScreenSpace(this.Position);
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/Gastropod");
				this.Frame = new SpriteFrame(1, 1);
				this.LifeTime = random.Next(60, 121) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.1f;
				this.OpacityNormalizedTimeToFadeOut = 0.9f;
				this.BrightnessLerper = 0.75f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = int.MaxValue;
			}

			// Token: 0x06005F66 RID: 24422 RVA: 0x006CEB5C File Offset: 0x006CCD5C
			public override void UpdateVelocity(int frameCount)
			{
				Vector2 vector = this._magnetAccelerations * new Vector2((float)Math.Sign(this._magnetPointTarget.X - this._positionVsMagnet.X), (float)Math.Sign(this._magnetPointTarget.Y - this._positionVsMagnet.Y));
				this._velocityVsMagnet += vector;
				this._positionVsMagnet += this._velocityVsMagnet;
				float x = (1.5f + Math.Abs(Main.WindForVisuals) * 0.2f) * (float)((this.Effects != 1) ? 1 : -1);
				this.Velocity = new Vector2(x, 0f) + this._velocityVsMagnet;
				this.Rotation = this.Velocity.X * 0.1f;
			}

			// Token: 0x06005F67 RID: 24423 RVA: 0x006CEC36 File Offset: 0x006CCE36
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if (Main.IsItRaining || Main.dayTime || Main.bloodMoon || Main.pumpkinMoon || Main.snowMoon)
				{
					base.StartFadingOut(frameCount);
				}
			}

			// Token: 0x06005F68 RID: 24424 RVA: 0x006CEC69 File Offset: 0x006CCE69
			public override Color GetColor(Color backgroundColor)
			{
				return Color.Lerp(backgroundColor, Colors.AmbientNPCGastropodLight, this.BrightnessLerper) * this.Opacity * this.FinalOpacityMultiplier;
			}

			// Token: 0x06005F69 RID: 24425 RVA: 0x006CEC92 File Offset: 0x006CCE92
			public override void Draw(SpriteBatch spriteBatch, float depthScale, float minDepth, float maxDepth)
			{
				base.CommonDraw(spriteBatch, depthScale - 0.1f, minDepth, maxDepth);
			}

			// Token: 0x06005F6A RID: 24426 RVA: 0x006CECA5 File Offset: 0x006CCEA5
			public void SetMagnetization(Vector2 accelerations, Vector2 targetOffset)
			{
				this._magnetAccelerations = accelerations;
				this._magnetPointTarget = targetOffset;
			}

			// Token: 0x06005F6B RID: 24427 RVA: 0x006CECB8 File Offset: 0x006CCEB8
			public static List<AmbientSky.GastropodGroupSkyEntity> CreateGroup(Player player, FastRandom random)
			{
				List<AmbientSky.GastropodGroupSkyEntity> list = new List<AmbientSky.GastropodGroupSkyEntity>();
				int num = 100;
				int num2 = random.Next(3, 8);
				VirtualCamera virtualCamera = new VirtualCamera(player);
				SpriteEffects spriteEffects = (Main.WindForVisuals <= 0f) ? 1 : 0;
				Vector2 vector = default(Vector2);
				if (spriteEffects == 1)
				{
					vector.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					vector.X = virtualCamera.Position.X - (float)num;
				}
				vector.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 3200f) + 3200f;
				float num3 = random.NextFloat() * 3f + 2f;
				int num4 = random.Next(30, 61) * 60;
				Vector2 vector2;
				vector2..ctor(random.NextFloat() * 0.1f + 0.1f, random.NextFloat() * 0.3f + 0.3f);
				Vector2 targetOffset = new Vector2(random.NextFloat() * 2f - 1f, random.NextFloat() * 2f - 1f) * 120f;
				for (int i = 0; i < num2; i++)
				{
					AmbientSky.GastropodGroupSkyEntity gastropodGroupSkyEntity = new AmbientSky.GastropodGroupSkyEntity(player, random);
					gastropodGroupSkyEntity.Depth = num3 + random.NextFloat() * 0.5f;
					gastropodGroupSkyEntity.Position = vector + new Vector2(random.NextFloat() * 20f - 10f, random.NextFloat() * 3f) * 60f;
					gastropodGroupSkyEntity.Effects = spriteEffects;
					gastropodGroupSkyEntity.SetPositionInWorldBasedOnScreenSpace(gastropodGroupSkyEntity.Position);
					gastropodGroupSkyEntity.LifeTime = num4 + random.Next(301);
					gastropodGroupSkyEntity.SetMagnetization(vector2 * (random.NextFloat() * 0.5f) * 0.05f, targetOffset);
					list.Add(gastropodGroupSkyEntity);
				}
				return list;
			}

			// Token: 0x040078B5 RID: 30901
			private Vector2 _magnetAccelerations;

			// Token: 0x040078B6 RID: 30902
			private Vector2 _magnetPointTarget;

			// Token: 0x040078B7 RID: 30903
			private Vector2 _positionVsMagnet;

			// Token: 0x040078B8 RID: 30904
			private Vector2 _velocityVsMagnet;
		}

		// Token: 0x02000C37 RID: 3127
		private class SlimeBalloonGroupSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06005F6C RID: 24428 RVA: 0x006CEEC0 File Offset: 0x006CD0C0
			public SlimeBalloonGroupSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals <= 0f) ? 1 : 0);
				int num = 100;
				if (this.Effects == 1)
				{
					this.Position.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					this.Position.X = virtualCamera.Position.X - (float)num;
				}
				this.Position.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 4000f) + 4000f;
				this.Depth = random.NextFloat() * 3f + 3f;
				base.SetPositionInWorldBasedOnScreenSpace(this.Position);
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/SlimeBalloons");
				this.Frame = new SpriteFrame(1, 7);
				this.Frame.CurrentRow = (byte)random.Next(7);
				this.LifeTime = random.Next(60, 121) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.025f;
				this.OpacityNormalizedTimeToFadeOut = 0.975f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = int.MaxValue;
			}

			// Token: 0x06005F6D RID: 24429 RVA: 0x006CF018 File Offset: 0x006CD218
			public override void UpdateVelocity(int frameCount)
			{
				Vector2 vector = this._magnetAccelerations * new Vector2((float)Math.Sign(this._magnetPointTarget.X - this._positionVsMagnet.X), (float)Math.Sign(this._magnetPointTarget.Y - this._positionVsMagnet.Y));
				this._velocityVsMagnet += vector;
				this._positionVsMagnet += this._velocityVsMagnet;
				float x = (1f + Math.Abs(Main.WindForVisuals) * 1f) * (float)((this.Effects != 1) ? 1 : -1);
				this.Velocity = new Vector2(x, -0.01f) + this._velocityVsMagnet;
				this.Rotation = this.Velocity.X * 0.1f;
			}

			// Token: 0x06005F6E RID: 24430 RVA: 0x006CF0F4 File Offset: 0x006CD2F4
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				this.Effects = ((Main.WindForVisuals <= 0f) ? 1 : 0);
				if (!Main.IsItAHappyWindyDay || Main.IsItRaining || !Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}

			// Token: 0x06005F6F RID: 24431 RVA: 0x006CF141 File Offset: 0x006CD341
			public void SetMagnetization(Vector2 accelerations, Vector2 targetOffset)
			{
				this._magnetAccelerations = accelerations;
				this._magnetPointTarget = targetOffset;
			}

			// Token: 0x06005F70 RID: 24432 RVA: 0x006CF154 File Offset: 0x006CD354
			public static List<AmbientSky.SlimeBalloonGroupSkyEntity> CreateGroup(Player player, FastRandom random)
			{
				List<AmbientSky.SlimeBalloonGroupSkyEntity> list = new List<AmbientSky.SlimeBalloonGroupSkyEntity>();
				int num = 100;
				int num2 = random.Next(5, 10);
				VirtualCamera virtualCamera = new VirtualCamera(player);
				SpriteEffects spriteEffects = (Main.WindForVisuals <= 0f) ? 1 : 0;
				Vector2 vector = default(Vector2);
				if (spriteEffects == 1)
				{
					vector.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					vector.X = virtualCamera.Position.X - (float)num;
				}
				vector.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 2400f) + 2400f;
				float num3 = random.NextFloat() * 3f + 3f;
				int num4 = random.Next(80, 121) * 60;
				Vector2 vector2;
				vector2..ctor(random.NextFloat() * 0.1f + 0.1f, random.NextFloat() * 0.1f + 0.1f);
				Vector2 targetOffset = new Vector2(random.NextFloat() * 2f - 1f, random.NextFloat() * 2f - 1f) * 150f;
				for (int i = 0; i < num2; i++)
				{
					AmbientSky.SlimeBalloonGroupSkyEntity slimeBalloonGroupSkyEntity = new AmbientSky.SlimeBalloonGroupSkyEntity(player, random);
					slimeBalloonGroupSkyEntity.Depth = num3 + random.NextFloat() * 0.5f;
					slimeBalloonGroupSkyEntity.Position = vector + new Vector2(random.NextFloat() * 20f - 10f, random.NextFloat() * 3f) * 80f;
					slimeBalloonGroupSkyEntity.Effects = spriteEffects;
					slimeBalloonGroupSkyEntity.SetPositionInWorldBasedOnScreenSpace(slimeBalloonGroupSkyEntity.Position);
					slimeBalloonGroupSkyEntity.LifeTime = num4 + random.Next(301);
					slimeBalloonGroupSkyEntity.SetMagnetization(vector2 * (random.NextFloat() * 0.2f) * 0.05f, targetOffset);
					list.Add(slimeBalloonGroupSkyEntity);
				}
				return list;
			}

			// Token: 0x040078B9 RID: 30905
			private Vector2 _magnetAccelerations;

			// Token: 0x040078BA RID: 30906
			private Vector2 _magnetPointTarget;

			// Token: 0x040078BB RID: 30907
			private Vector2 _positionVsMagnet;

			// Token: 0x040078BC RID: 30908
			private Vector2 _velocityVsMagnet;
		}

		// Token: 0x02000C38 RID: 3128
		private class HellBatsGoupSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06005F71 RID: 24433 RVA: 0x006CF360 File Offset: 0x006CD560
			public HellBatsGoupSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals <= 0f) ? 1 : 0);
				int num = 100;
				if (this.Effects == 1)
				{
					this.Position.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					this.Position.X = virtualCamera.Position.X - (float)num;
				}
				this.Position.Y = random.NextFloat() * 400f + (float)(Main.UnderworldLayer * 16);
				this.Depth = random.NextFloat() * 5f + 3f;
				base.SetPositionInWorldBasedOnScreenSpace(this.Position);
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/HellBat" + random.Next(1, 3).ToString());
				this.Frame = new SpriteFrame(1, 10);
				this.LifeTime = random.Next(60, 121) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.1f;
				this.OpacityNormalizedTimeToFadeOut = 0.9f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 4;
				this.FrameOffset = random.Next(0, (int)this.Frame.RowCount);
				int num2 = random.Next((int)this.Frame.RowCount);
				for (int i = 0; i < num2; i++)
				{
					base.NextFrame();
				}
			}

			// Token: 0x06005F72 RID: 24434 RVA: 0x006CF4EC File Offset: 0x006CD6EC
			public override void UpdateVelocity(int frameCount)
			{
				Vector2 vector = this._magnetAccelerations * new Vector2((float)Math.Sign(this._magnetPointTarget.X - this._positionVsMagnet.X), (float)Math.Sign(this._magnetPointTarget.Y - this._positionVsMagnet.Y));
				this._velocityVsMagnet += vector;
				this._positionVsMagnet += this._velocityVsMagnet;
				float x = (3f + Math.Abs(Main.WindForVisuals) * 0.8f) * (float)((this.Effects != 1) ? 1 : -1);
				this.Velocity = new Vector2(x, 0f) + this._velocityVsMagnet;
			}

			// Token: 0x06005F73 RID: 24435 RVA: 0x006CF5AF File Offset: 0x006CD7AF
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
			}

			// Token: 0x06005F74 RID: 24436 RVA: 0x006CF5B8 File Offset: 0x006CD7B8
			public void SetMagnetization(Vector2 accelerations, Vector2 targetOffset)
			{
				this._magnetAccelerations = accelerations;
				this._magnetPointTarget = targetOffset;
			}

			// Token: 0x06005F75 RID: 24437 RVA: 0x006CF5C8 File Offset: 0x006CD7C8
			public override Color GetColor(Color backgroundColor)
			{
				return Color.Lerp(Color.White, Color.Gray, this.Depth / 15f) * this.Opacity * this.FinalOpacityMultiplier * this.Helper_GetOpacityWithAccountingForBackgroundsOff();
			}

			// Token: 0x06005F76 RID: 24438 RVA: 0x006CF608 File Offset: 0x006CD808
			public static List<AmbientSky.HellBatsGoupSkyEntity> CreateGroup(Player player, FastRandom random)
			{
				List<AmbientSky.HellBatsGoupSkyEntity> list = new List<AmbientSky.HellBatsGoupSkyEntity>();
				int num = 100;
				int num2 = random.Next(20, 40);
				VirtualCamera virtualCamera = new VirtualCamera(player);
				SpriteEffects spriteEffects = (Main.WindForVisuals <= 0f) ? 1 : 0;
				Vector2 vector = default(Vector2);
				if (spriteEffects == 1)
				{
					vector.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					vector.X = virtualCamera.Position.X - (float)num;
				}
				vector.Y = random.NextFloat() * 800f + (float)(Main.UnderworldLayer * 16);
				float num3 = random.NextFloat() * 5f + 3f;
				int num4 = random.Next(30, 61) * 60;
				Vector2 vector2;
				vector2..ctor(random.NextFloat() * 0.5f + 0.5f, random.NextFloat() * 0.5f + 0.5f);
				Vector2 targetOffset = new Vector2(random.NextFloat() * 2f - 1f, random.NextFloat() * 2f - 1f) * 100f;
				for (int i = 0; i < num2; i++)
				{
					AmbientSky.HellBatsGoupSkyEntity hellBatsGoupSkyEntity = new AmbientSky.HellBatsGoupSkyEntity(player, random);
					hellBatsGoupSkyEntity.Depth = num3 + random.NextFloat() * 0.5f;
					hellBatsGoupSkyEntity.Position = vector + new Vector2(random.NextFloat() * 20f - 10f, random.NextFloat() * 3f) * 50f;
					hellBatsGoupSkyEntity.Effects = spriteEffects;
					hellBatsGoupSkyEntity.SetPositionInWorldBasedOnScreenSpace(hellBatsGoupSkyEntity.Position);
					hellBatsGoupSkyEntity.LifeTime = num4 + random.Next(301);
					hellBatsGoupSkyEntity.SetMagnetization(vector2 * (random.NextFloat() * 0.3f + 0.85f) * 0.05f, targetOffset);
					list.Add(hellBatsGoupSkyEntity);
				}
				return list;
			}

			// Token: 0x06005F77 RID: 24439 RVA: 0x006CF809 File Offset: 0x006CDA09
			internal float Helper_GetOpacityWithAccountingForBackgroundsOff()
			{
				if (Main.netMode == 2 || Main.BackgroundEnabled)
				{
					return 1f;
				}
				return 0f;
			}

			// Token: 0x040078BD RID: 30909
			private Vector2 _magnetAccelerations;

			// Token: 0x040078BE RID: 30910
			private Vector2 _magnetPointTarget;

			// Token: 0x040078BF RID: 30911
			private Vector2 _positionVsMagnet;

			// Token: 0x040078C0 RID: 30912
			private Vector2 _velocityVsMagnet;
		}

		// Token: 0x02000C39 RID: 3129
		private class BatsGroupSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06005F78 RID: 24440 RVA: 0x006CF828 File Offset: 0x006CDA28
			public BatsGroupSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals <= 0f) ? 1 : 0);
				int num = 100;
				if (this.Effects == 1)
				{
					this.Position.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					this.Position.X = virtualCamera.Position.X - (float)num;
				}
				this.Position.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 2400f) + 2400f;
				this.Depth = random.NextFloat() * 3f + 3f;
				base.SetPositionInWorldBasedOnScreenSpace(this.Position);
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/Bat" + random.Next(1, 4).ToString());
				this.Frame = new SpriteFrame(1, 10);
				this.LifeTime = random.Next(60, 121) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.1f;
				this.OpacityNormalizedTimeToFadeOut = 0.9f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 4;
				this.FrameOffset = random.Next(0, (int)this.Frame.RowCount);
				int num2 = random.Next((int)this.Frame.RowCount);
				for (int i = 0; i < num2; i++)
				{
					base.NextFrame();
				}
			}

			// Token: 0x06005F79 RID: 24441 RVA: 0x006CF9C0 File Offset: 0x006CDBC0
			public override void UpdateVelocity(int frameCount)
			{
				Vector2 vector = this._magnetAccelerations * new Vector2((float)Math.Sign(this._magnetPointTarget.X - this._positionVsMagnet.X), (float)Math.Sign(this._magnetPointTarget.Y - this._positionVsMagnet.Y));
				this._velocityVsMagnet += vector;
				this._positionVsMagnet += this._velocityVsMagnet;
				float x = (3f + Math.Abs(Main.WindForVisuals) * 0.8f) * (float)((this.Effects != 1) ? 1 : -1);
				this.Velocity = new Vector2(x, 0f) + this._velocityVsMagnet;
			}

			// Token: 0x06005F7A RID: 24442 RVA: 0x006CFA83 File Offset: 0x006CDC83
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if (Main.IsItRaining || !Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}

			// Token: 0x06005F7B RID: 24443 RVA: 0x006CFAA8 File Offset: 0x006CDCA8
			public void SetMagnetization(Vector2 accelerations, Vector2 targetOffset)
			{
				this._magnetAccelerations = accelerations;
				this._magnetPointTarget = targetOffset;
			}

			// Token: 0x06005F7C RID: 24444 RVA: 0x006CFAB8 File Offset: 0x006CDCB8
			public override Color GetColor(Color backgroundColor)
			{
				return base.GetColor(backgroundColor) * Utils.Max<float>(new float[]
				{
					Main.bgAlphaFrontLayer[3],
					Main.bgAlphaFrontLayer[0],
					Main.bgAlphaFrontLayer[10],
					Main.bgAlphaFrontLayer[11],
					Main.bgAlphaFrontLayer[12]
				});
			}

			// Token: 0x06005F7D RID: 24445 RVA: 0x006CFB14 File Offset: 0x006CDD14
			public static List<AmbientSky.BatsGroupSkyEntity> CreateGroup(Player player, FastRandom random)
			{
				List<AmbientSky.BatsGroupSkyEntity> list = new List<AmbientSky.BatsGroupSkyEntity>();
				int num = 100;
				int num2 = random.Next(20, 40);
				VirtualCamera virtualCamera = new VirtualCamera(player);
				SpriteEffects spriteEffects = (Main.WindForVisuals <= 0f) ? 1 : 0;
				Vector2 vector = default(Vector2);
				if (spriteEffects == 1)
				{
					vector.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					vector.X = virtualCamera.Position.X - (float)num;
				}
				vector.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 2400f) + 2400f;
				float num3 = random.NextFloat() * 3f + 3f;
				int num4 = random.Next(30, 61) * 60;
				Vector2 vector2;
				vector2..ctor(random.NextFloat() * 0.5f + 0.5f, random.NextFloat() * 0.5f + 0.5f);
				Vector2 targetOffset = new Vector2(random.NextFloat() * 2f - 1f, random.NextFloat() * 2f - 1f) * 100f;
				for (int i = 0; i < num2; i++)
				{
					AmbientSky.BatsGroupSkyEntity batsGroupSkyEntity = new AmbientSky.BatsGroupSkyEntity(player, random);
					batsGroupSkyEntity.Depth = num3 + random.NextFloat() * 0.5f;
					batsGroupSkyEntity.Position = vector + new Vector2(random.NextFloat() * 20f - 10f, random.NextFloat() * 3f) * 50f;
					batsGroupSkyEntity.Effects = spriteEffects;
					batsGroupSkyEntity.SetPositionInWorldBasedOnScreenSpace(batsGroupSkyEntity.Position);
					batsGroupSkyEntity.LifeTime = num4 + random.Next(301);
					batsGroupSkyEntity.SetMagnetization(vector2 * (random.NextFloat() * 0.3f + 0.85f) * 0.05f, targetOffset);
					list.Add(batsGroupSkyEntity);
				}
				return list;
			}

			// Token: 0x040078C1 RID: 30913
			private Vector2 _magnetAccelerations;

			// Token: 0x040078C2 RID: 30914
			private Vector2 _magnetPointTarget;

			// Token: 0x040078C3 RID: 30915
			private Vector2 _positionVsMagnet;

			// Token: 0x040078C4 RID: 30916
			private Vector2 _velocityVsMagnet;
		}

		// Token: 0x02000C3A RID: 3130
		private class WyvernSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06005F7E RID: 24446 RVA: 0x006CFD24 File Offset: 0x006CDF24
			public WyvernSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals <= 0f) ? 1 : 0);
				int num = 100;
				if (this.Effects == 1)
				{
					this.Position.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					this.Position.X = virtualCamera.Position.X - (float)num;
				}
				this.Position.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 2400f) + 2400f;
				this.Depth = random.NextFloat() * 3f + 3f;
				base.SetPositionInWorldBasedOnScreenSpace(this.Position);
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/Wyvern");
				this.Frame = new SpriteFrame(1, 5);
				this.LifeTime = random.Next(40, 71) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.15f;
				this.OpacityNormalizedTimeToFadeOut = 0.85f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 4;
			}

			// Token: 0x06005F7F RID: 24447 RVA: 0x006CFE64 File Offset: 0x006CE064
			public override void UpdateVelocity(int frameCount)
			{
				float num = 3f + Math.Abs(Main.WindForVisuals) * 0.8f;
				this.Velocity = new Vector2(num * (float)((this.Effects != 1) ? 1 : -1), 0f);
			}
		}

		// Token: 0x02000C3B RID: 3131
		private class NormalizedBackgroundLayerSpaceSkyEntity : AmbientSky.SkyEntity
		{
			// Token: 0x06005F80 RID: 24448 RVA: 0x006CFEA8 File Offset: 0x006CE0A8
			public override Color GetColor(Color backgroundColor)
			{
				return Color.Lerp(backgroundColor, Color.White, 0.3f);
			}

			// Token: 0x06005F81 RID: 24449 RVA: 0x006CFEBA File Offset: 0x006CE0BA
			public override Vector2 GetDrawPosition()
			{
				return this.Position;
			}

			// Token: 0x06005F82 RID: 24450 RVA: 0x006CFEC2 File Offset: 0x006CE0C2
			public override void Update(int frameCount)
			{
			}
		}

		// Token: 0x02000C3C RID: 3132
		private class BoneSerpentSkyEntity : AmbientSky.NormalizedBackgroundLayerSpaceSkyEntity
		{
		}

		// Token: 0x02000C3D RID: 3133
		private class AirshipSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06005F85 RID: 24453 RVA: 0x006CFED4 File Offset: 0x006CE0D4
			public AirshipSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((random.Next(2) != 0) ? 1 : 0);
				int num = 100;
				if (this.Effects == 1)
				{
					this.Position.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					this.Position.X = virtualCamera.Position.X - (float)num;
				}
				this.Position.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 2400f) + 2400f;
				this.Depth = random.NextFloat() * 3f + 3f;
				base.SetPositionInWorldBasedOnScreenSpace(this.Position);
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/FlyingShip");
				this.Frame = new SpriteFrame(1, 4);
				this.LifeTime = random.Next(40, 71) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.05f;
				this.OpacityNormalizedTimeToFadeOut = 0.95f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 4;
			}

			// Token: 0x06005F86 RID: 24454 RVA: 0x006D0014 File Offset: 0x006CE214
			public override void UpdateVelocity(int frameCount)
			{
				float num = 6f + Math.Abs(Main.WindForVisuals) * 1.6f;
				this.Velocity = new Vector2(num * (float)((this.Effects != 1) ? 1 : -1), 0f);
			}

			// Token: 0x06005F87 RID: 24455 RVA: 0x006D0058 File Offset: 0x006CE258
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if (Main.IsItRaining || !Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}
		}

		// Token: 0x02000C3E RID: 3134
		private class AirBalloonSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06005F88 RID: 24456 RVA: 0x006D0080 File Offset: 0x006CE280
			public AirBalloonSkyEntity(Player player, FastRandom random)
			{
				new VirtualCamera(player);
				int x = player.Center.ToTileCoordinates().X;
				this.Effects = ((random.Next(2) != 0) ? 1 : 0);
				this.Position.X = ((float)x + 100f * (random.NextFloat() * 2f - 1f)) * 16f;
				this.Position.Y = (float)Main.worldSurface * 16f - (float)random.Next(50, 81) * 16f;
				this.Depth = random.NextFloat() * 3f + 3f;
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/AirBalloons_" + ((random.Next(2) == 0) ? "Large" : "Small"));
				this.Frame = new SpriteFrame(1, 5);
				this.Frame.CurrentRow = (byte)random.Next(5);
				this.LifeTime = random.Next(20, 51) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.05f;
				this.OpacityNormalizedTimeToFadeOut = 0.95f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = int.MaxValue;
			}

			// Token: 0x06005F89 RID: 24457 RVA: 0x006D01CC File Offset: 0x006CE3CC
			public override void UpdateVelocity(int frameCount)
			{
				float x = Main.WindForVisuals * 4f;
				float num = 3f + Math.Abs(Main.WindForVisuals) * 1f;
				if ((double)this.Position.Y < Main.worldSurface * 12.0)
				{
					num *= 0.5f;
				}
				if ((double)this.Position.Y < Main.worldSurface * 8.0)
				{
					num *= 0.5f;
				}
				if ((double)this.Position.Y < Main.worldSurface * 4.0)
				{
					num *= 0.5f;
				}
				this.Velocity = new Vector2(x, 0f - num);
			}

			// Token: 0x06005F8A RID: 24458 RVA: 0x006D027E File Offset: 0x006CE47E
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if (Main.IsItRaining || !Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}

			// Token: 0x040078C5 RID: 30917
			private const int RANDOM_TILE_SPAWN_RANGE = 100;
		}

		// Token: 0x02000C3F RID: 3135
		private class CrimeraSkyEntity : AmbientSky.EOCSkyEntity
		{
			// Token: 0x06005F8B RID: 24459 RVA: 0x006D02A4 File Offset: 0x006CE4A4
			public CrimeraSkyEntity(Player player, FastRandom random) : base(player, random)
			{
				int num = 3;
				if (this.Depth <= 6f)
				{
					num = 2;
				}
				if (this.Depth <= 5f)
				{
					num = 1;
				}
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/Crimera" + num.ToString());
				this.Frame = new SpriteFrame(1, 3);
			}

			// Token: 0x06005F8C RID: 24460 RVA: 0x006D0307 File Offset: 0x006CE507
			public override Color GetColor(Color backgroundColor)
			{
				return base.GetColor(backgroundColor) * Main.bgAlphaFrontLayer[8];
			}
		}

		// Token: 0x02000C40 RID: 3136
		private class EOSSkyEntity : AmbientSky.EOCSkyEntity
		{
			// Token: 0x06005F8D RID: 24461 RVA: 0x006D031C File Offset: 0x006CE51C
			public EOSSkyEntity(Player player, FastRandom random) : base(player, random)
			{
				int num = 3;
				if (this.Depth <= 6f)
				{
					num = 2;
				}
				if (this.Depth <= 5f)
				{
					num = 1;
				}
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/EOS" + num.ToString());
				this.Frame = new SpriteFrame(1, 4);
			}

			// Token: 0x06005F8E RID: 24462 RVA: 0x006D037F File Offset: 0x006CE57F
			public override Color GetColor(Color backgroundColor)
			{
				return base.GetColor(backgroundColor) * Main.bgAlphaFrontLayer[1];
			}
		}

		// Token: 0x02000C41 RID: 3137
		private class EOCSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06005F8F RID: 24463 RVA: 0x006D0394 File Offset: 0x006CE594
			public EOCSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera camera = new VirtualCamera(player);
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/EOC");
				this.Frame = new SpriteFrame(1, 3);
				this.Depth = random.NextFloat() * 3f + 4.5f;
				if (random.Next(4) != 0)
				{
					this.BeginZigZag(ref random, camera, (random.Next(2) == 1) ? 1 : -1);
				}
				else
				{
					this.BeginChasingPlayer(ref random, camera);
				}
				base.SetPositionInWorldBasedOnScreenSpace(this.Position);
				this.OpacityNormalizedTimeToFadeIn = 0.1f;
				this.OpacityNormalizedTimeToFadeOut = 0.9f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 5;
			}

			// Token: 0x06005F90 RID: 24464 RVA: 0x006D0458 File Offset: 0x006CE658
			private void BeginZigZag(ref FastRandom random, VirtualCamera camera, int direction)
			{
				this._state = 1;
				this.LifeTime = random.Next(18, 31) * 60;
				this._direction = direction;
				this._waviness = random.NextFloat() * 1f + 1f;
				this.Position.Y = camera.Position.Y;
				int num = 100;
				if (this._direction == 1)
				{
					this.Position.X = camera.Position.X - (float)num;
					return;
				}
				this.Position.X = camera.Position.X + camera.Size.X + (float)num;
			}

			// Token: 0x06005F91 RID: 24465 RVA: 0x006D0504 File Offset: 0x006CE704
			private void BeginChasingPlayer(ref FastRandom random, VirtualCamera camera)
			{
				this._state = 2;
				this.LifeTime = random.Next(18, 31) * 60;
				this.Position = camera.Position + camera.Size * new Vector2(random.NextFloat(), random.NextFloat());
			}

			// Token: 0x06005F92 RID: 24466 RVA: 0x006D055C File Offset: 0x006CE75C
			public override void UpdateVelocity(int frameCount)
			{
				int state = this._state;
				if (state != 1)
				{
					if (state == 2)
					{
						this.ChasePlayerTop(frameCount);
					}
				}
				else
				{
					this.ZigzagMove(frameCount);
				}
				this.Rotation = this.Velocity.ToRotation();
			}

			// Token: 0x06005F93 RID: 24467 RVA: 0x006D059B File Offset: 0x006CE79B
			private void ZigzagMove(int frameCount)
			{
				this.Velocity = new Vector2((float)(this._direction * 3), (float)Math.Cos((double)((float)frameCount / 1200f * 6.2831855f)) * this._waviness);
			}

			// Token: 0x06005F94 RID: 24468 RVA: 0x006D05D0 File Offset: 0x006CE7D0
			private void ChasePlayerTop(int frameCount)
			{
				Vector2 vector = Main.LocalPlayer.Center + new Vector2(0f, -500f) - this.Position;
				if (vector.Length() >= 100f)
				{
					this.Velocity.X = this.Velocity.X + 0.1f * (float)Math.Sign(vector.X);
					this.Velocity.Y = this.Velocity.Y + 0.1f * (float)Math.Sign(vector.Y);
					this.Velocity = Vector2.Clamp(this.Velocity, new Vector2(-18f), new Vector2(18f));
				}
			}

			// Token: 0x040078C6 RID: 30918
			private const int STATE_ZIGZAG = 1;

			// Token: 0x040078C7 RID: 30919
			private const int STATE_GOOVERPLAYER = 2;

			// Token: 0x040078C8 RID: 30920
			private int _state;

			// Token: 0x040078C9 RID: 30921
			private int _direction;

			// Token: 0x040078CA RID: 30922
			private float _waviness;
		}

		// Token: 0x02000C42 RID: 3138
		private class MeteorSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06005F95 RID: 24469 RVA: 0x006D067C File Offset: 0x006CE87C
			public MeteorSkyEntity(Player player, FastRandom random)
			{
				new VirtualCamera(player);
				this.Effects = ((random.Next(2) != 0) ? 1 : 0);
				this.Depth = random.NextFloat() * 3f + 3f;
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/Meteor");
				this.Frame = new SpriteFrame(1, 4);
				Vector2 vector = (0.7853982f + random.NextFloat() * 1.5707964f).ToRotationVector2();
				float num5 = (float)(Main.worldSurface * 16.0 - 0.0) / vector.Y;
				float num2 = 1200f;
				float num3 = num5 / num2;
				Vector2 velocity = vector * num3;
				this.Velocity = velocity;
				int num4 = 100;
				Vector2 position = player.Center + new Vector2((float)random.Next(-num4, num4 + 1), (float)random.Next(-num4, num4 + 1)) - this.Velocity * num2 * 0.5f;
				this.Position = position;
				this.LifeTime = (int)num2;
				this.OpacityNormalizedTimeToFadeIn = 0.05f;
				this.OpacityNormalizedTimeToFadeOut = 0.95f;
				this.BrightnessLerper = 0.5f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 5;
				this.Rotation = this.Velocity.ToRotation() + 1.5707964f;
			}
		}

		// Token: 0x02000C43 RID: 3139
		// (Invoke) Token: 0x06005F97 RID: 24471
		private delegate AmbientSky.SkyEntity EntityFactoryMethod(Player player, int seed);
	}
}
