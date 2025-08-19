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
	// Token: 0x0200031F RID: 799
	public class AmbientSky : CustomSky
	{
		// Token: 0x06002446 RID: 9286 RVA: 0x0055A11E File Offset: 0x0055831E
		public override void Activate(Vector2 position, params object[] args)
		{
			this._isActive = true;
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x0055A127 File Offset: 0x00558327
		public override void Deactivate(params object[] args)
		{
			this._isActive = false;
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x0055A130 File Offset: 0x00558330
		private bool AnActiveSkyConflictsWithAmbience()
		{
			return SkyManager.Instance["MonolithMoonLord"].IsActive() || SkyManager.Instance["MoonLord"].IsActive();
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x0055A160 File Offset: 0x00558360
		public override void Update(GameTime gameTime)
		{
			if (Main.gamePaused)
			{
				return;
			}
			this._frameCounter++;
			if (Main.netMode != 2 && this.AnActiveSkyConflictsWithAmbience() && SkyManager.Instance["Ambience"].IsActive())
			{
				SkyManager.Instance.Deactivate("Ambience", new object[0]);
			}
			foreach (SlotVector<AmbientSky.SkyEntity>.ItemPair itemPair in this._entities)
			{
				AmbientSky.SkyEntity value = itemPair.Value;
				value.Update(this._frameCounter);
				if (!value.IsActive)
				{
					this._entities.Remove(itemPair.Id);
					if (Main.netMode != 2 && this._entities.Count == 0 && SkyManager.Instance["Ambience"].IsActive())
					{
						SkyManager.Instance.Deactivate("Ambience", new object[0]);
					}
				}
			}
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x0055A264 File Offset: 0x00558464
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (Main.gameMenu && Main.netMode == 0 && SkyManager.Instance["Ambience"].IsActive())
			{
				this._entities.Clear();
				SkyManager.Instance.Deactivate("Ambience", new object[0]);
			}
			foreach (SlotVector<AmbientSky.SkyEntity>.ItemPair itemPair in this._entities)
			{
				itemPair.Value.Draw(spriteBatch, 3f, minDepth, maxDepth);
			}
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x0055A300 File Offset: 0x00558500
		public override bool IsActive()
		{
			return this._isActive;
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void Reset()
		{
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x0055A308 File Offset: 0x00558508
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
				List<AmbientSky.BatsGroupSkyEntity> list = AmbientSky.BatsGroupSkyEntity.CreateGroup(player, random);
				for (int i = 0; i < list.Count; i++)
				{
					this._entities.Add(list[i]);
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
				List<AmbientSky.SeagullsGroupSkyEntity> list2 = AmbientSky.SeagullsGroupSkyEntity.CreateGroup(player, random);
				for (int j = 0; j < list2.Count; j++)
				{
					this._entities.Add(list2[j]);
				}
				break;
			}
			case SkyEntityType.SlimeBalloons:
			{
				List<AmbientSky.SlimeBalloonGroupSkyEntity> list3 = AmbientSky.SlimeBalloonGroupSkyEntity.CreateGroup(player, random);
				for (int k = 0; k < list3.Count; k++)
				{
					this._entities.Add(list3[k]);
				}
				break;
			}
			case SkyEntityType.Gastropods:
			{
				List<AmbientSky.GastropodGroupSkyEntity> list4 = AmbientSky.GastropodGroupSkyEntity.CreateGroup(player, random);
				for (int l = 0; l < list4.Count; l++)
				{
					this._entities.Add(list4[l]);
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
				List<AmbientSky.HellBatsGoupSkyEntity> list5 = AmbientSky.HellBatsGoupSkyEntity.CreateGroup(player, random);
				for (int m = 0; m < list5.Count; m++)
				{
					this._entities.Add(list5[m]);
				}
				break;
			}
			}
			if (Main.netMode != 2 && !this.AnActiveSkyConflictsWithAmbience() && !SkyManager.Instance["Ambience"].IsActive())
			{
				SkyManager.Instance.Activate("Ambience", default(Vector2), new object[0]);
			}
		}

		// Token: 0x04004882 RID: 18562
		private bool _isActive;

		// Token: 0x04004883 RID: 18563
		private readonly SlotVector<AmbientSky.SkyEntity> _entities = new SlotVector<AmbientSky.SkyEntity>(500);

		// Token: 0x04004884 RID: 18564
		private int _frameCounter;

		// Token: 0x020006F0 RID: 1776
		private abstract class SkyEntity
		{
			// Token: 0x17000400 RID: 1024
			// (get) Token: 0x0600370C RID: 14092 RVA: 0x0060DAA7 File Offset: 0x0060BCA7
			public Rectangle SourceRectangle
			{
				get
				{
					return this.Frame.GetSourceRectangle(this.Texture.Value);
				}
			}

			// Token: 0x0600370D RID: 14093 RVA: 0x0060DABF File Offset: 0x0060BCBF
			protected void NextFrame()
			{
				this.Frame.CurrentRow = (this.Frame.CurrentRow + 1) % this.Frame.RowCount;
			}

			// Token: 0x0600370E RID: 14094
			public abstract Color GetColor(Color backgroundColor);

			// Token: 0x0600370F RID: 14095
			public abstract void Update(int frameCount);

			// Token: 0x06003710 RID: 14096 RVA: 0x0060DAE8 File Offset: 0x0060BCE8
			protected void SetPositionInWorldBasedOnScreenSpace(Vector2 actualWorldSpace)
			{
				Vector2 value = actualWorldSpace - Main.Camera.Center;
				Vector2 position = Main.Camera.Center + value * (this.Depth / 3f);
				this.Position = position;
			}

			// Token: 0x06003711 RID: 14097
			public abstract Vector2 GetDrawPosition();

			// Token: 0x06003712 RID: 14098 RVA: 0x0060DB2F File Offset: 0x0060BD2F
			public virtual void Draw(SpriteBatch spriteBatch, float depthScale, float minDepth, float maxDepth)
			{
				this.CommonDraw(spriteBatch, depthScale, minDepth, maxDepth);
			}

			// Token: 0x06003713 RID: 14099 RVA: 0x0060DB3C File Offset: 0x0060BD3C
			public void CommonDraw(SpriteBatch spriteBatch, float depthScale, float minDepth, float maxDepth)
			{
				if (this.Depth <= minDepth || this.Depth > maxDepth)
				{
					return;
				}
				Vector2 drawPositionByDepth = this.GetDrawPositionByDepth();
				Color color = this.GetColor(Main.ColorOfTheSkies) * Main.atmo;
				Vector2 origin = this.SourceRectangle.Size() / 2f;
				float scale = depthScale / this.Depth;
				spriteBatch.Draw(this.Texture.Value, drawPositionByDepth - Main.Camera.UnscaledPosition, new Rectangle?(this.SourceRectangle), color, this.Rotation, origin, scale, this.Effects, 0f);
			}

			// Token: 0x06003714 RID: 14100 RVA: 0x0060DBDC File Offset: 0x0060BDDC
			internal Vector2 GetDrawPositionByDepth()
			{
				return (this.GetDrawPosition() - Main.Camera.Center) * new Vector2(1f / this.Depth, 0.9f / this.Depth) + Main.Camera.Center;
			}

			// Token: 0x06003715 RID: 14101 RVA: 0x0060DC30 File Offset: 0x0060BE30
			internal float Helper_GetOpacityWithAccountingForOceanWaterLine()
			{
				ref Vector2 ptr = this.GetDrawPositionByDepth() - Main.Camera.UnscaledPosition;
				int num = this.SourceRectangle.Height / 2;
				float t = ptr.Y + (float)num;
				float yscreenPosition = AmbientSkyDrawCache.Instance.OceanLineInfo.YScreenPosition;
				float num2 = Utils.GetLerpValue(yscreenPosition - 10f, yscreenPosition - 2f, t, true);
				num2 *= AmbientSkyDrawCache.Instance.OceanLineInfo.OceanOpacity;
				return 1f - num2;
			}

			// Token: 0x0400628F RID: 25231
			public Vector2 Position;

			// Token: 0x04006290 RID: 25232
			public Asset<Texture2D> Texture;

			// Token: 0x04006291 RID: 25233
			public SpriteFrame Frame;

			// Token: 0x04006292 RID: 25234
			public float Depth;

			// Token: 0x04006293 RID: 25235
			public SpriteEffects Effects;

			// Token: 0x04006294 RID: 25236
			public bool IsActive = true;

			// Token: 0x04006295 RID: 25237
			public float Rotation;
		}

		// Token: 0x020006F1 RID: 1777
		private class FadingSkyEntity : AmbientSky.SkyEntity
		{
			// Token: 0x06003717 RID: 14103 RVA: 0x0060DCB8 File Offset: 0x0060BEB8
			public FadingSkyEntity()
			{
				this.Opacity = 0f;
				this.TimeEntitySpawnedIn = -1;
				this.BrightnessLerper = 0f;
				this.FinalOpacityMultiplier = 1f;
				this.OpacityNormalizedTimeToFadeIn = 0.1f;
				this.OpacityNormalizedTimeToFadeOut = 0.9f;
			}

			// Token: 0x06003718 RID: 14104 RVA: 0x0060DD0C File Offset: 0x0060BF0C
			public override void Update(int frameCount)
			{
				if (this.IsMovementDone(frameCount))
				{
					return;
				}
				this.UpdateOpacity(frameCount);
				if ((frameCount + this.FrameOffset) % this.FramingSpeed == 0)
				{
					base.NextFrame();
				}
				this.UpdateVelocity(frameCount);
				this.Position += this.Velocity;
			}

			// Token: 0x06003719 RID: 14105 RVA: 0x0003C3EC File Offset: 0x0003A5EC
			public virtual void UpdateVelocity(int frameCount)
			{
			}

			// Token: 0x0600371A RID: 14106 RVA: 0x0060DD60 File Offset: 0x0060BF60
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

			// Token: 0x0600371B RID: 14107 RVA: 0x0060DDCD File Offset: 0x0060BFCD
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

			// Token: 0x0600371C RID: 14108 RVA: 0x0060DDF9 File Offset: 0x0060BFF9
			public override Color GetColor(Color backgroundColor)
			{
				return Color.Lerp(backgroundColor, Color.White, this.BrightnessLerper) * this.Opacity * this.FinalOpacityMultiplier * base.Helper_GetOpacityWithAccountingForOceanWaterLine();
			}

			// Token: 0x0600371D RID: 14109 RVA: 0x0060DE30 File Offset: 0x0060C030
			public void StartFadingOut(int currentFrameCount)
			{
				int num = (int)((float)this.LifeTime * this.OpacityNormalizedTimeToFadeOut);
				int num2 = currentFrameCount - num;
				if (num2 < this.TimeEntitySpawnedIn)
				{
					this.TimeEntitySpawnedIn = num2;
				}
			}

			// Token: 0x0600371E RID: 14110 RVA: 0x0060DE61 File Offset: 0x0060C061
			public override Vector2 GetDrawPosition()
			{
				return this.Position;
			}

			// Token: 0x04006296 RID: 25238
			protected int LifeTime;

			// Token: 0x04006297 RID: 25239
			protected Vector2 Velocity;

			// Token: 0x04006298 RID: 25240
			protected int FramingSpeed;

			// Token: 0x04006299 RID: 25241
			protected int TimeEntitySpawnedIn;

			// Token: 0x0400629A RID: 25242
			protected float Opacity;

			// Token: 0x0400629B RID: 25243
			protected float BrightnessLerper;

			// Token: 0x0400629C RID: 25244
			protected float FinalOpacityMultiplier;

			// Token: 0x0400629D RID: 25245
			protected float OpacityNormalizedTimeToFadeIn;

			// Token: 0x0400629E RID: 25246
			protected float OpacityNormalizedTimeToFadeOut;

			// Token: 0x0400629F RID: 25247
			protected int FrameOffset;
		}

		// Token: 0x020006F2 RID: 1778
		private class ButterfliesSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x0600371F RID: 14111 RVA: 0x0060DE6C File Offset: 0x0060C06C
			public ButterfliesSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				int num = 100;
				if (this.Effects == SpriteEffects.FlipHorizontally)
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
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/ButterflySwarm" + num2, 1);
				this.Frame = new SpriteFrame(1, (num2 == 2) ? 19 : 17);
				this.LifeTime = random.Next(60, 121) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.15f;
				this.OpacityNormalizedTimeToFadeOut = 0.85f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 5;
			}

			// Token: 0x06003720 RID: 14112 RVA: 0x0060DFC8 File Offset: 0x0060C1C8
			public override void UpdateVelocity(int frameCount)
			{
				float num = 0.1f + Math.Abs(Main.WindForVisuals) * 0.05f;
				this.Velocity = new Vector2(num * (float)((this.Effects == SpriteEffects.FlipHorizontally) ? -1 : 1), 0f);
			}

			// Token: 0x06003721 RID: 14113 RVA: 0x0060E00C File Offset: 0x0060C20C
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if (Main.IsItRaining || !Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}
		}

		// Token: 0x020006F3 RID: 1779
		private class LostKiteSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06003722 RID: 14114 RVA: 0x0060E034 File Offset: 0x0060C234
			public LostKiteSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				int num = 100;
				if (this.Effects == SpriteEffects.FlipHorizontally)
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
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/LostKite", 1);
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

			// Token: 0x06003723 RID: 14115 RVA: 0x0060E19C File Offset: 0x0060C39C
			public override void UpdateVelocity(int frameCount)
			{
				float num = 1.2f + Math.Abs(Main.WindForVisuals) * 3f;
				if (Main.IsItStorming)
				{
					num *= 1.5f;
				}
				this.Velocity = new Vector2(num * (float)((this.Effects == SpriteEffects.FlipHorizontally) ? -1 : 1), 0f);
			}

			// Token: 0x06003724 RID: 14116 RVA: 0x0060E1F0 File Offset: 0x0060C3F0
			public override void Update(int frameCount)
			{
				if (Main.IsItStorming)
				{
					this.FramingSpeed = 4;
				}
				this.Effects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				base.Update(frameCount);
				if (!Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}
		}

		// Token: 0x020006F4 RID: 1780
		private class PegasusSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06003725 RID: 14117 RVA: 0x0060E240 File Offset: 0x0060C440
			public PegasusSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				int num = 100;
				if (this.Effects == SpriteEffects.FlipHorizontally)
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
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/Pegasus", 1);
				this.Frame = new SpriteFrame(1, 11);
				this.LifeTime = random.Next(60, 121) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.15f;
				this.OpacityNormalizedTimeToFadeOut = 0.85f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 5;
			}

			// Token: 0x06003726 RID: 14118 RVA: 0x0060E384 File Offset: 0x0060C584
			public override void UpdateVelocity(int frameCount)
			{
				float num = 1.5f + Math.Abs(Main.WindForVisuals) * 0.6f;
				this.Velocity = new Vector2(num * (float)((this.Effects == SpriteEffects.FlipHorizontally) ? -1 : 1), 0f);
			}

			// Token: 0x06003727 RID: 14119 RVA: 0x0060E00C File Offset: 0x0060C20C
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if (Main.IsItRaining || !Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}

			// Token: 0x06003728 RID: 14120 RVA: 0x0060E3C8 File Offset: 0x0060C5C8
			public override Color GetColor(Color backgroundColor)
			{
				return base.GetColor(backgroundColor) * Main.bgAlphaFrontLayer[6];
			}
		}

		// Token: 0x020006F5 RID: 1781
		private class VultureSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06003729 RID: 14121 RVA: 0x0060E3E0 File Offset: 0x0060C5E0
			public VultureSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				int num = 100;
				if (this.Effects == SpriteEffects.FlipHorizontally)
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
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/Vulture", 1);
				this.Frame = new SpriteFrame(1, 10);
				this.LifeTime = random.Next(60, 121) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.15f;
				this.OpacityNormalizedTimeToFadeOut = 0.85f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 5;
			}

			// Token: 0x0600372A RID: 14122 RVA: 0x0060E524 File Offset: 0x0060C724
			public override void UpdateVelocity(int frameCount)
			{
				float num = 3f + Math.Abs(Main.WindForVisuals) * 0.8f;
				this.Velocity = new Vector2(num * (float)((this.Effects == SpriteEffects.FlipHorizontally) ? -1 : 1), 0f);
			}

			// Token: 0x0600372B RID: 14123 RVA: 0x0060E00C File Offset: 0x0060C20C
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if (Main.IsItRaining || !Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}

			// Token: 0x0600372C RID: 14124 RVA: 0x0060E568 File Offset: 0x0060C768
			public override Color GetColor(Color backgroundColor)
			{
				return base.GetColor(backgroundColor) * Math.Max(Main.bgAlphaFrontLayer[2], Main.bgAlphaFrontLayer[5]);
			}
		}

		// Token: 0x020006F6 RID: 1782
		private class PixiePosseSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x0600372D RID: 14125 RVA: 0x0060E58C File Offset: 0x0060C78C
			public PixiePosseSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				int num = 100;
				if (this.Effects == SpriteEffects.FlipHorizontally)
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
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/PixiePosse" + this.pixieType, 1);
				this.Frame = new SpriteFrame(1, 25);
				this.LifeTime = random.Next(60, 121) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.15f;
				this.OpacityNormalizedTimeToFadeOut = 0.85f;
				this.BrightnessLerper = 0.6f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 5;
			}

			// Token: 0x0600372E RID: 14126 RVA: 0x0060E6EC File Offset: 0x0060C8EC
			public override void UpdateVelocity(int frameCount)
			{
				float num = 0.12f + Math.Abs(Main.WindForVisuals) * 0.08f;
				this.Velocity = new Vector2(num * (float)((this.Effects == SpriteEffects.FlipHorizontally) ? -1 : 1), 0f);
			}

			// Token: 0x0600372F RID: 14127 RVA: 0x0060E730 File Offset: 0x0060C930
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if ((this.pixieType == 1 && !Main.dayTime) || (this.pixieType == 2 && Main.dayTime) || Main.IsItRaining || Main.eclipse || Main.bloodMoon || Main.pumpkinMoon || Main.snowMoon)
				{
					base.StartFadingOut(frameCount);
				}
			}

			// Token: 0x06003730 RID: 14128 RVA: 0x0060E78E File Offset: 0x0060C98E
			public override void Draw(SpriteBatch spriteBatch, float depthScale, float minDepth, float maxDepth)
			{
				base.CommonDraw(spriteBatch, depthScale - 0.1f, minDepth, maxDepth);
			}

			// Token: 0x040062A0 RID: 25248
			private int pixieType = 1;
		}

		// Token: 0x020006F7 RID: 1783
		private class BirdsPackSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06003731 RID: 14129 RVA: 0x0060E7A4 File Offset: 0x0060C9A4
			public BirdsPackSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				int num = 100;
				if (this.Effects == SpriteEffects.FlipHorizontally)
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
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/BirdsVShape", 1);
				this.Frame = new SpriteFrame(1, 4);
				this.LifeTime = random.Next(60, 121) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.15f;
				this.OpacityNormalizedTimeToFadeOut = 0.85f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 5;
			}

			// Token: 0x06003732 RID: 14130 RVA: 0x0060E8E4 File Offset: 0x0060CAE4
			public override void UpdateVelocity(int frameCount)
			{
				float num = 3f + Math.Abs(Main.WindForVisuals) * 0.8f;
				this.Velocity = new Vector2(num * (float)((this.Effects == SpriteEffects.FlipHorizontally) ? -1 : 1), 0f);
			}

			// Token: 0x06003733 RID: 14131 RVA: 0x0060E00C File Offset: 0x0060C20C
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if (Main.IsItRaining || !Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}
		}

		// Token: 0x020006F8 RID: 1784
		private class SeagullsGroupSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06003734 RID: 14132 RVA: 0x0060E928 File Offset: 0x0060CB28
			public SeagullsGroupSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				int num = 100;
				if (this.Effects == SpriteEffects.FlipHorizontally)
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
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/Seagull", 1);
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

			// Token: 0x06003735 RID: 14133 RVA: 0x0060EAA8 File Offset: 0x0060CCA8
			public override void UpdateVelocity(int frameCount)
			{
				Vector2 value = this._magnetAccelerations * new Vector2((float)Math.Sign(this._magnetPointTarget.X - this._positionVsMagnet.X), (float)Math.Sign(this._magnetPointTarget.Y - this._positionVsMagnet.Y));
				this._velocityVsMagnet += value;
				this._positionVsMagnet += this._velocityVsMagnet;
				float x = 4f * (float)((this.Effects == SpriteEffects.FlipHorizontally) ? -1 : 1);
				this.Velocity = new Vector2(x, 0f) + this._velocityVsMagnet;
			}

			// Token: 0x06003736 RID: 14134 RVA: 0x0060E00C File Offset: 0x0060C20C
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if (Main.IsItRaining || !Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}

			// Token: 0x06003737 RID: 14135 RVA: 0x0060EB5A File Offset: 0x0060CD5A
			public void SetMagnetization(Vector2 accelerations, Vector2 targetOffset)
			{
				this._magnetAccelerations = accelerations;
				this._magnetPointTarget = targetOffset;
			}

			// Token: 0x06003738 RID: 14136 RVA: 0x0060EB6A File Offset: 0x0060CD6A
			public override Color GetColor(Color backgroundColor)
			{
				return base.GetColor(backgroundColor) * Main.bgAlphaFrontLayer[4];
			}

			// Token: 0x06003739 RID: 14137 RVA: 0x0060EB7F File Offset: 0x0060CD7F
			public override void Draw(SpriteBatch spriteBatch, float depthScale, float minDepth, float maxDepth)
			{
				base.CommonDraw(spriteBatch, depthScale - 1.5f, minDepth, maxDepth);
			}

			// Token: 0x0600373A RID: 14138 RVA: 0x0060EB94 File Offset: 0x0060CD94
			public static List<AmbientSky.SeagullsGroupSkyEntity> CreateGroup(Player player, FastRandom random)
			{
				List<AmbientSky.SeagullsGroupSkyEntity> list = new List<AmbientSky.SeagullsGroupSkyEntity>();
				int num = 100;
				int num2 = random.Next(5, 9);
				float scaleFactor = 100f;
				VirtualCamera virtualCamera = new VirtualCamera(player);
				SpriteEffects spriteEffects = (Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
				Vector2 value = default(Vector2);
				if (spriteEffects == SpriteEffects.FlipHorizontally)
				{
					value.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					value.X = virtualCamera.Position.X - (float)num;
				}
				value.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 2400f) + 2400f;
				float num3 = random.NextFloat() * 2f + 1f;
				int num4 = random.Next(30, 61) * 60;
				Vector2 value2 = new Vector2(random.NextFloat() * 0.5f + 0.5f, random.NextFloat() * 0.5f + 0.5f);
				Vector2 targetOffset = new Vector2(random.NextFloat() * 2f - 1f, random.NextFloat() * 2f - 1f) * scaleFactor;
				for (int i = 0; i < num2; i++)
				{
					AmbientSky.SeagullsGroupSkyEntity seagullsGroupSkyEntity = new AmbientSky.SeagullsGroupSkyEntity(player, random);
					seagullsGroupSkyEntity.Depth = num3 + random.NextFloat() * 0.5f;
					seagullsGroupSkyEntity.Position = value + new Vector2(random.NextFloat() * 20f - 10f, random.NextFloat() * 3f) * 50f;
					seagullsGroupSkyEntity.Effects = spriteEffects;
					seagullsGroupSkyEntity.SetPositionInWorldBasedOnScreenSpace(seagullsGroupSkyEntity.Position);
					seagullsGroupSkyEntity.LifeTime = num4 + random.Next(301);
					seagullsGroupSkyEntity.SetMagnetization(value2 * (random.NextFloat() * 0.3f + 0.85f) * 0.05f, targetOffset);
					list.Add(seagullsGroupSkyEntity);
				}
				return list;
			}

			// Token: 0x040062A1 RID: 25249
			private Vector2 _magnetAccelerations;

			// Token: 0x040062A2 RID: 25250
			private Vector2 _magnetPointTarget;

			// Token: 0x040062A3 RID: 25251
			private Vector2 _positionVsMagnet;

			// Token: 0x040062A4 RID: 25252
			private Vector2 _velocityVsMagnet;
		}

		// Token: 0x020006F9 RID: 1785
		private class GastropodGroupSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x0600373B RID: 14139 RVA: 0x0060EDA8 File Offset: 0x0060CFA8
			public GastropodGroupSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				int num = 100;
				if (this.Effects == SpriteEffects.FlipHorizontally)
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
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/Gastropod", 1);
				this.Frame = new SpriteFrame(1, 1);
				this.LifeTime = random.Next(60, 121) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.1f;
				this.OpacityNormalizedTimeToFadeOut = 0.9f;
				this.BrightnessLerper = 0.75f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = int.MaxValue;
			}

			// Token: 0x0600373C RID: 14140 RVA: 0x0060EEEC File Offset: 0x0060D0EC
			public override void UpdateVelocity(int frameCount)
			{
				Vector2 value = this._magnetAccelerations * new Vector2((float)Math.Sign(this._magnetPointTarget.X - this._positionVsMagnet.X), (float)Math.Sign(this._magnetPointTarget.Y - this._positionVsMagnet.Y));
				this._velocityVsMagnet += value;
				this._positionVsMagnet += this._velocityVsMagnet;
				float x = (1.5f + Math.Abs(Main.WindForVisuals) * 0.2f) * (float)((this.Effects == SpriteEffects.FlipHorizontally) ? -1 : 1);
				this.Velocity = new Vector2(x, 0f) + this._velocityVsMagnet;
				this.Rotation = this.Velocity.X * 0.1f;
			}

			// Token: 0x0600373D RID: 14141 RVA: 0x0060EFC6 File Offset: 0x0060D1C6
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if (Main.IsItRaining || Main.dayTime || Main.bloodMoon || Main.pumpkinMoon || Main.snowMoon)
				{
					base.StartFadingOut(frameCount);
				}
			}

			// Token: 0x0600373E RID: 14142 RVA: 0x0060EFF9 File Offset: 0x0060D1F9
			public override Color GetColor(Color backgroundColor)
			{
				return Color.Lerp(backgroundColor, Colors.AmbientNPCGastropodLight, this.BrightnessLerper) * this.Opacity * this.FinalOpacityMultiplier;
			}

			// Token: 0x0600373F RID: 14143 RVA: 0x0060E78E File Offset: 0x0060C98E
			public override void Draw(SpriteBatch spriteBatch, float depthScale, float minDepth, float maxDepth)
			{
				base.CommonDraw(spriteBatch, depthScale - 0.1f, minDepth, maxDepth);
			}

			// Token: 0x06003740 RID: 14144 RVA: 0x0060F022 File Offset: 0x0060D222
			public void SetMagnetization(Vector2 accelerations, Vector2 targetOffset)
			{
				this._magnetAccelerations = accelerations;
				this._magnetPointTarget = targetOffset;
			}

			// Token: 0x06003741 RID: 14145 RVA: 0x0060F034 File Offset: 0x0060D234
			public static List<AmbientSky.GastropodGroupSkyEntity> CreateGroup(Player player, FastRandom random)
			{
				List<AmbientSky.GastropodGroupSkyEntity> list = new List<AmbientSky.GastropodGroupSkyEntity>();
				int num = 100;
				int num2 = random.Next(3, 8);
				VirtualCamera virtualCamera = new VirtualCamera(player);
				SpriteEffects spriteEffects = (Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
				Vector2 value = default(Vector2);
				if (spriteEffects == SpriteEffects.FlipHorizontally)
				{
					value.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					value.X = virtualCamera.Position.X - (float)num;
				}
				value.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 3200f) + 3200f;
				float num3 = random.NextFloat() * 3f + 2f;
				int num4 = random.Next(30, 61) * 60;
				Vector2 value2 = new Vector2(random.NextFloat() * 0.1f + 0.1f, random.NextFloat() * 0.3f + 0.3f);
				Vector2 targetOffset = new Vector2(random.NextFloat() * 2f - 1f, random.NextFloat() * 2f - 1f) * 120f;
				for (int i = 0; i < num2; i++)
				{
					AmbientSky.GastropodGroupSkyEntity gastropodGroupSkyEntity = new AmbientSky.GastropodGroupSkyEntity(player, random);
					gastropodGroupSkyEntity.Depth = num3 + random.NextFloat() * 0.5f;
					gastropodGroupSkyEntity.Position = value + new Vector2(random.NextFloat() * 20f - 10f, random.NextFloat() * 3f) * 60f;
					gastropodGroupSkyEntity.Effects = spriteEffects;
					gastropodGroupSkyEntity.SetPositionInWorldBasedOnScreenSpace(gastropodGroupSkyEntity.Position);
					gastropodGroupSkyEntity.LifeTime = num4 + random.Next(301);
					gastropodGroupSkyEntity.SetMagnetization(value2 * (random.NextFloat() * 0.5f) * 0.05f, targetOffset);
					list.Add(gastropodGroupSkyEntity);
				}
				return list;
			}

			// Token: 0x040062A5 RID: 25253
			private Vector2 _magnetAccelerations;

			// Token: 0x040062A6 RID: 25254
			private Vector2 _magnetPointTarget;

			// Token: 0x040062A7 RID: 25255
			private Vector2 _positionVsMagnet;

			// Token: 0x040062A8 RID: 25256
			private Vector2 _velocityVsMagnet;
		}

		// Token: 0x020006FA RID: 1786
		private class SlimeBalloonGroupSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06003742 RID: 14146 RVA: 0x0060F23C File Offset: 0x0060D43C
			public SlimeBalloonGroupSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				int num = 100;
				if (this.Effects == SpriteEffects.FlipHorizontally)
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
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/SlimeBalloons", 1);
				this.Frame = new SpriteFrame(1, 7);
				this.Frame.CurrentRow = (byte)random.Next(7);
				this.LifeTime = random.Next(60, 121) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.025f;
				this.OpacityNormalizedTimeToFadeOut = 0.975f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = int.MaxValue;
			}

			// Token: 0x06003743 RID: 14147 RVA: 0x0060F394 File Offset: 0x0060D594
			public override void UpdateVelocity(int frameCount)
			{
				Vector2 value = this._magnetAccelerations * new Vector2((float)Math.Sign(this._magnetPointTarget.X - this._positionVsMagnet.X), (float)Math.Sign(this._magnetPointTarget.Y - this._positionVsMagnet.Y));
				this._velocityVsMagnet += value;
				this._positionVsMagnet += this._velocityVsMagnet;
				float x = (1f + Math.Abs(Main.WindForVisuals) * 1f) * (float)((this.Effects == SpriteEffects.FlipHorizontally) ? -1 : 1);
				this.Velocity = new Vector2(x, -0.01f) + this._velocityVsMagnet;
				this.Rotation = this.Velocity.X * 0.1f;
			}

			// Token: 0x06003744 RID: 14148 RVA: 0x0060F470 File Offset: 0x0060D670
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				this.Effects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				if (!Main.IsItAHappyWindyDay || Main.IsItRaining || !Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}

			// Token: 0x06003745 RID: 14149 RVA: 0x0060F4BD File Offset: 0x0060D6BD
			public void SetMagnetization(Vector2 accelerations, Vector2 targetOffset)
			{
				this._magnetAccelerations = accelerations;
				this._magnetPointTarget = targetOffset;
			}

			// Token: 0x06003746 RID: 14150 RVA: 0x0060F4D0 File Offset: 0x0060D6D0
			public static List<AmbientSky.SlimeBalloonGroupSkyEntity> CreateGroup(Player player, FastRandom random)
			{
				List<AmbientSky.SlimeBalloonGroupSkyEntity> list = new List<AmbientSky.SlimeBalloonGroupSkyEntity>();
				int num = 100;
				int num2 = random.Next(5, 10);
				VirtualCamera virtualCamera = new VirtualCamera(player);
				SpriteEffects spriteEffects = (Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
				Vector2 value = default(Vector2);
				if (spriteEffects == SpriteEffects.FlipHorizontally)
				{
					value.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					value.X = virtualCamera.Position.X - (float)num;
				}
				value.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 2400f) + 2400f;
				float num3 = random.NextFloat() * 3f + 3f;
				int num4 = random.Next(80, 121) * 60;
				Vector2 value2 = new Vector2(random.NextFloat() * 0.1f + 0.1f, random.NextFloat() * 0.1f + 0.1f);
				Vector2 targetOffset = new Vector2(random.NextFloat() * 2f - 1f, random.NextFloat() * 2f - 1f) * 150f;
				for (int i = 0; i < num2; i++)
				{
					AmbientSky.SlimeBalloonGroupSkyEntity slimeBalloonGroupSkyEntity = new AmbientSky.SlimeBalloonGroupSkyEntity(player, random);
					slimeBalloonGroupSkyEntity.Depth = num3 + random.NextFloat() * 0.5f;
					slimeBalloonGroupSkyEntity.Position = value + new Vector2(random.NextFloat() * 20f - 10f, random.NextFloat() * 3f) * 80f;
					slimeBalloonGroupSkyEntity.Effects = spriteEffects;
					slimeBalloonGroupSkyEntity.SetPositionInWorldBasedOnScreenSpace(slimeBalloonGroupSkyEntity.Position);
					slimeBalloonGroupSkyEntity.LifeTime = num4 + random.Next(301);
					slimeBalloonGroupSkyEntity.SetMagnetization(value2 * (random.NextFloat() * 0.2f) * 0.05f, targetOffset);
					list.Add(slimeBalloonGroupSkyEntity);
				}
				return list;
			}

			// Token: 0x040062A9 RID: 25257
			private Vector2 _magnetAccelerations;

			// Token: 0x040062AA RID: 25258
			private Vector2 _magnetPointTarget;

			// Token: 0x040062AB RID: 25259
			private Vector2 _positionVsMagnet;

			// Token: 0x040062AC RID: 25260
			private Vector2 _velocityVsMagnet;
		}

		// Token: 0x020006FB RID: 1787
		private class HellBatsGoupSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06003747 RID: 14151 RVA: 0x0060F6DC File Offset: 0x0060D8DC
			public HellBatsGoupSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				int num = 100;
				if (this.Effects == SpriteEffects.FlipHorizontally)
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
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/HellBat" + random.Next(1, 3), 1);
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

			// Token: 0x06003748 RID: 14152 RVA: 0x0060F860 File Offset: 0x0060DA60
			public override void UpdateVelocity(int frameCount)
			{
				Vector2 value = this._magnetAccelerations * new Vector2((float)Math.Sign(this._magnetPointTarget.X - this._positionVsMagnet.X), (float)Math.Sign(this._magnetPointTarget.Y - this._positionVsMagnet.Y));
				this._velocityVsMagnet += value;
				this._positionVsMagnet += this._velocityVsMagnet;
				float x = (3f + Math.Abs(Main.WindForVisuals) * 0.8f) * (float)((this.Effects == SpriteEffects.FlipHorizontally) ? -1 : 1);
				this.Velocity = new Vector2(x, 0f) + this._velocityVsMagnet;
			}

			// Token: 0x06003749 RID: 14153 RVA: 0x0060F923 File Offset: 0x0060DB23
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
			}

			// Token: 0x0600374A RID: 14154 RVA: 0x0060F92C File Offset: 0x0060DB2C
			public void SetMagnetization(Vector2 accelerations, Vector2 targetOffset)
			{
				this._magnetAccelerations = accelerations;
				this._magnetPointTarget = targetOffset;
			}

			// Token: 0x0600374B RID: 14155 RVA: 0x0060F93C File Offset: 0x0060DB3C
			public override Color GetColor(Color backgroundColor)
			{
				return Color.Lerp(Color.White, Color.Gray, this.Depth / 15f) * this.Opacity * this.FinalOpacityMultiplier * this.Helper_GetOpacityWithAccountingForBackgroundsOff();
			}

			// Token: 0x0600374C RID: 14156 RVA: 0x0060F97C File Offset: 0x0060DB7C
			public static List<AmbientSky.HellBatsGoupSkyEntity> CreateGroup(Player player, FastRandom random)
			{
				List<AmbientSky.HellBatsGoupSkyEntity> list = new List<AmbientSky.HellBatsGoupSkyEntity>();
				int num = 100;
				int num2 = random.Next(20, 40);
				VirtualCamera virtualCamera = new VirtualCamera(player);
				SpriteEffects spriteEffects = (Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
				Vector2 value = default(Vector2);
				if (spriteEffects == SpriteEffects.FlipHorizontally)
				{
					value.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					value.X = virtualCamera.Position.X - (float)num;
				}
				value.Y = random.NextFloat() * 800f + (float)(Main.UnderworldLayer * 16);
				float num3 = random.NextFloat() * 5f + 3f;
				int num4 = random.Next(30, 61) * 60;
				Vector2 value2 = new Vector2(random.NextFloat() * 0.5f + 0.5f, random.NextFloat() * 0.5f + 0.5f);
				Vector2 targetOffset = new Vector2(random.NextFloat() * 2f - 1f, random.NextFloat() * 2f - 1f) * 100f;
				for (int i = 0; i < num2; i++)
				{
					AmbientSky.HellBatsGoupSkyEntity hellBatsGoupSkyEntity = new AmbientSky.HellBatsGoupSkyEntity(player, random);
					hellBatsGoupSkyEntity.Depth = num3 + random.NextFloat() * 0.5f;
					hellBatsGoupSkyEntity.Position = value + new Vector2(random.NextFloat() * 20f - 10f, random.NextFloat() * 3f) * 50f;
					hellBatsGoupSkyEntity.Effects = spriteEffects;
					hellBatsGoupSkyEntity.SetPositionInWorldBasedOnScreenSpace(hellBatsGoupSkyEntity.Position);
					hellBatsGoupSkyEntity.LifeTime = num4 + random.Next(301);
					hellBatsGoupSkyEntity.SetMagnetization(value2 * (random.NextFloat() * 0.3f + 0.85f) * 0.05f, targetOffset);
					list.Add(hellBatsGoupSkyEntity);
				}
				return list;
			}

			// Token: 0x0600374D RID: 14157 RVA: 0x0060FB7D File Offset: 0x0060DD7D
			internal float Helper_GetOpacityWithAccountingForBackgroundsOff()
			{
				if (Main.netMode == 2 || Main.BackgroundEnabled)
				{
					return 1f;
				}
				return 0f;
			}

			// Token: 0x040062AD RID: 25261
			private Vector2 _magnetAccelerations;

			// Token: 0x040062AE RID: 25262
			private Vector2 _magnetPointTarget;

			// Token: 0x040062AF RID: 25263
			private Vector2 _positionVsMagnet;

			// Token: 0x040062B0 RID: 25264
			private Vector2 _velocityVsMagnet;
		}

		// Token: 0x020006FC RID: 1788
		private class BatsGroupSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x0600374E RID: 14158 RVA: 0x0060FB9C File Offset: 0x0060DD9C
			public BatsGroupSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				int num = 100;
				if (this.Effects == SpriteEffects.FlipHorizontally)
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
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/Bat" + random.Next(1, 4), 1);
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

			// Token: 0x0600374F RID: 14159 RVA: 0x0060FD30 File Offset: 0x0060DF30
			public override void UpdateVelocity(int frameCount)
			{
				Vector2 value = this._magnetAccelerations * new Vector2((float)Math.Sign(this._magnetPointTarget.X - this._positionVsMagnet.X), (float)Math.Sign(this._magnetPointTarget.Y - this._positionVsMagnet.Y));
				this._velocityVsMagnet += value;
				this._positionVsMagnet += this._velocityVsMagnet;
				float x = (3f + Math.Abs(Main.WindForVisuals) * 0.8f) * (float)((this.Effects == SpriteEffects.FlipHorizontally) ? -1 : 1);
				this.Velocity = new Vector2(x, 0f) + this._velocityVsMagnet;
			}

			// Token: 0x06003750 RID: 14160 RVA: 0x0060E00C File Offset: 0x0060C20C
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if (Main.IsItRaining || !Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}

			// Token: 0x06003751 RID: 14161 RVA: 0x0060FDF3 File Offset: 0x0060DFF3
			public void SetMagnetization(Vector2 accelerations, Vector2 targetOffset)
			{
				this._magnetAccelerations = accelerations;
				this._magnetPointTarget = targetOffset;
			}

			// Token: 0x06003752 RID: 14162 RVA: 0x0060FE04 File Offset: 0x0060E004
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

			// Token: 0x06003753 RID: 14163 RVA: 0x0060FE60 File Offset: 0x0060E060
			public static List<AmbientSky.BatsGroupSkyEntity> CreateGroup(Player player, FastRandom random)
			{
				List<AmbientSky.BatsGroupSkyEntity> list = new List<AmbientSky.BatsGroupSkyEntity>();
				int num = 100;
				int num2 = random.Next(20, 40);
				VirtualCamera virtualCamera = new VirtualCamera(player);
				SpriteEffects spriteEffects = (Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
				Vector2 value = default(Vector2);
				if (spriteEffects == SpriteEffects.FlipHorizontally)
				{
					value.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					value.X = virtualCamera.Position.X - (float)num;
				}
				value.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 2400f) + 2400f;
				float num3 = random.NextFloat() * 3f + 3f;
				int num4 = random.Next(30, 61) * 60;
				Vector2 value2 = new Vector2(random.NextFloat() * 0.5f + 0.5f, random.NextFloat() * 0.5f + 0.5f);
				Vector2 targetOffset = new Vector2(random.NextFloat() * 2f - 1f, random.NextFloat() * 2f - 1f) * 100f;
				for (int i = 0; i < num2; i++)
				{
					AmbientSky.BatsGroupSkyEntity batsGroupSkyEntity = new AmbientSky.BatsGroupSkyEntity(player, random);
					batsGroupSkyEntity.Depth = num3 + random.NextFloat() * 0.5f;
					batsGroupSkyEntity.Position = value + new Vector2(random.NextFloat() * 20f - 10f, random.NextFloat() * 3f) * 50f;
					batsGroupSkyEntity.Effects = spriteEffects;
					batsGroupSkyEntity.SetPositionInWorldBasedOnScreenSpace(batsGroupSkyEntity.Position);
					batsGroupSkyEntity.LifeTime = num4 + random.Next(301);
					batsGroupSkyEntity.SetMagnetization(value2 * (random.NextFloat() * 0.3f + 0.85f) * 0.05f, targetOffset);
					list.Add(batsGroupSkyEntity);
				}
				return list;
			}

			// Token: 0x040062B1 RID: 25265
			private Vector2 _magnetAccelerations;

			// Token: 0x040062B2 RID: 25266
			private Vector2 _magnetPointTarget;

			// Token: 0x040062B3 RID: 25267
			private Vector2 _positionVsMagnet;

			// Token: 0x040062B4 RID: 25268
			private Vector2 _velocityVsMagnet;
		}

		// Token: 0x020006FD RID: 1789
		private class WyvernSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06003754 RID: 14164 RVA: 0x00610070 File Offset: 0x0060E270
			public WyvernSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				int num = 100;
				if (this.Effects == SpriteEffects.FlipHorizontally)
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
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/Wyvern", 1);
				this.Frame = new SpriteFrame(1, 5);
				this.LifeTime = random.Next(40, 71) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.15f;
				this.OpacityNormalizedTimeToFadeOut = 0.85f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 4;
			}

			// Token: 0x06003755 RID: 14165 RVA: 0x006101B0 File Offset: 0x0060E3B0
			public override void UpdateVelocity(int frameCount)
			{
				float num = 3f + Math.Abs(Main.WindForVisuals) * 0.8f;
				this.Velocity = new Vector2(num * (float)((this.Effects == SpriteEffects.FlipHorizontally) ? -1 : 1), 0f);
			}
		}

		// Token: 0x020006FE RID: 1790
		private class NormalizedBackgroundLayerSpaceSkyEntity : AmbientSky.SkyEntity
		{
			// Token: 0x06003756 RID: 14166 RVA: 0x006101F4 File Offset: 0x0060E3F4
			public override Color GetColor(Color backgroundColor)
			{
				return Color.Lerp(backgroundColor, Color.White, 0.3f);
			}

			// Token: 0x06003757 RID: 14167 RVA: 0x0060DE61 File Offset: 0x0060C061
			public override Vector2 GetDrawPosition()
			{
				return this.Position;
			}

			// Token: 0x06003758 RID: 14168 RVA: 0x0003C3EC File Offset: 0x0003A5EC
			public override void Update(int frameCount)
			{
			}
		}

		// Token: 0x020006FF RID: 1791
		private class BoneSerpentSkyEntity : AmbientSky.NormalizedBackgroundLayerSpaceSkyEntity
		{
		}

		// Token: 0x02000700 RID: 1792
		private class AirshipSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x0600375B RID: 14171 RVA: 0x00610218 File Offset: 0x0060E418
			public AirshipSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((random.Next(2) == 0) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				int num = 100;
				if (this.Effects == SpriteEffects.FlipHorizontally)
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
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/FlyingShip", 1);
				this.Frame = new SpriteFrame(1, 4);
				this.LifeTime = random.Next(40, 71) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.05f;
				this.OpacityNormalizedTimeToFadeOut = 0.95f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 4;
			}

			// Token: 0x0600375C RID: 14172 RVA: 0x00610358 File Offset: 0x0060E558
			public override void UpdateVelocity(int frameCount)
			{
				float num = 6f + Math.Abs(Main.WindForVisuals) * 1.6f;
				this.Velocity = new Vector2(num * (float)((this.Effects == SpriteEffects.FlipHorizontally) ? -1 : 1), 0f);
			}

			// Token: 0x0600375D RID: 14173 RVA: 0x0060E00C File Offset: 0x0060C20C
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if (Main.IsItRaining || !Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}
		}

		// Token: 0x02000701 RID: 1793
		private class AirBalloonSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x0600375E RID: 14174 RVA: 0x0061039C File Offset: 0x0060E59C
			public AirBalloonSkyEntity(Player player, FastRandom random)
			{
				new VirtualCamera(player);
				int x = player.Center.ToTileCoordinates().X;
				this.Effects = ((random.Next(2) == 0) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				this.Position.X = ((float)x + 100f * (random.NextFloat() * 2f - 1f)) * 16f;
				this.Position.Y = (float)Main.worldSurface * 16f - (float)random.Next(50, 81) * 16f;
				this.Depth = random.NextFloat() * 3f + 3f;
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/AirBalloons_" + ((random.Next(2) == 0) ? "Large" : "Small"), 1);
				this.Frame = new SpriteFrame(1, 5);
				this.Frame.CurrentRow = (byte)random.Next(5);
				this.LifeTime = random.Next(20, 51) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.05f;
				this.OpacityNormalizedTimeToFadeOut = 0.95f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = int.MaxValue;
			}

			// Token: 0x0600375F RID: 14175 RVA: 0x006104E8 File Offset: 0x0060E6E8
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
				this.Velocity = new Vector2(x, -num);
			}

			// Token: 0x06003760 RID: 14176 RVA: 0x0060E00C File Offset: 0x0060C20C
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if (Main.IsItRaining || !Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}

			// Token: 0x040062B5 RID: 25269
			private const int RANDOM_TILE_SPAWN_RANGE = 100;
		}

		// Token: 0x02000702 RID: 1794
		private class CrimeraSkyEntity : AmbientSky.EOCSkyEntity
		{
			// Token: 0x06003761 RID: 14177 RVA: 0x00610598 File Offset: 0x0060E798
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
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/Crimera" + num, 1);
				this.Frame = new SpriteFrame(1, 3);
			}

			// Token: 0x06003762 RID: 14178 RVA: 0x006105FB File Offset: 0x0060E7FB
			public override Color GetColor(Color backgroundColor)
			{
				return base.GetColor(backgroundColor) * Main.bgAlphaFrontLayer[8];
			}
		}

		// Token: 0x02000703 RID: 1795
		private class EOSSkyEntity : AmbientSky.EOCSkyEntity
		{
			// Token: 0x06003763 RID: 14179 RVA: 0x00610610 File Offset: 0x0060E810
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
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/EOS" + num, 1);
				this.Frame = new SpriteFrame(1, 4);
			}

			// Token: 0x06003764 RID: 14180 RVA: 0x00610673 File Offset: 0x0060E873
			public override Color GetColor(Color backgroundColor)
			{
				return base.GetColor(backgroundColor) * Main.bgAlphaFrontLayer[1];
			}
		}

		// Token: 0x02000704 RID: 1796
		private class EOCSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06003765 RID: 14181 RVA: 0x00610688 File Offset: 0x0060E888
			public EOCSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera camera = new VirtualCamera(player);
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/EOC", 1);
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

			// Token: 0x06003766 RID: 14182 RVA: 0x0061074C File Offset: 0x0060E94C
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

			// Token: 0x06003767 RID: 14183 RVA: 0x006107F8 File Offset: 0x0060E9F8
			private void BeginChasingPlayer(ref FastRandom random, VirtualCamera camera)
			{
				this._state = 2;
				this.LifeTime = random.Next(18, 31) * 60;
				this.Position = camera.Position + camera.Size * new Vector2(random.NextFloat(), random.NextFloat());
			}

			// Token: 0x06003768 RID: 14184 RVA: 0x00610850 File Offset: 0x0060EA50
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

			// Token: 0x06003769 RID: 14185 RVA: 0x0061088F File Offset: 0x0060EA8F
			private void ZigzagMove(int frameCount)
			{
				this.Velocity = new Vector2((float)(this._direction * 3), (float)Math.Cos((double)((float)frameCount / 1200f * 6.2831855f)) * this._waviness);
			}

			// Token: 0x0600376A RID: 14186 RVA: 0x006108C4 File Offset: 0x0060EAC4
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

			// Token: 0x040062B6 RID: 25270
			private const int STATE_ZIGZAG = 1;

			// Token: 0x040062B7 RID: 25271
			private const int STATE_GOOVERPLAYER = 2;

			// Token: 0x040062B8 RID: 25272
			private int _state;

			// Token: 0x040062B9 RID: 25273
			private int _direction;

			// Token: 0x040062BA RID: 25274
			private float _waviness;
		}

		// Token: 0x02000705 RID: 1797
		private class MeteorSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x0600376B RID: 14187 RVA: 0x00610970 File Offset: 0x0060EB70
			public MeteorSkyEntity(Player player, FastRandom random)
			{
				new VirtualCamera(player);
				this.Effects = ((random.Next(2) == 0) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				this.Depth = random.NextFloat() * 3f + 3f;
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/Meteor", 1);
				this.Frame = new SpriteFrame(1, 4);
				Vector2 vector = (0.7853982f + random.NextFloat() * 1.5707964f).ToRotationVector2();
				float num = (float)(Main.worldSurface * 16.0 - 0.0) / vector.Y;
				float num2 = 1200f;
				float scaleFactor = num / num2;
				Vector2 velocity = vector * scaleFactor;
				this.Velocity = velocity;
				int num3 = 100;
				Vector2 position = player.Center + new Vector2((float)random.Next(-num3, num3 + 1), (float)random.Next(-num3, num3 + 1)) - this.Velocity * num2 * 0.5f;
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

		// Token: 0x02000706 RID: 1798
		// (Invoke) Token: 0x0600376D RID: 14189
		private delegate AmbientSky.SkyEntity EntityFactoryMethod(Player player, int seed);
	}
}
