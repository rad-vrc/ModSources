using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.GameContent.Liquid;
using Terraria.Graphics;
using Terraria.Graphics.Light;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace Terraria.GameContent.Shaders
{
	// Token: 0x02000578 RID: 1400
	public class WaterShaderData : ScreenShaderData
	{
		// Token: 0x1400006B RID: 107
		// (add) Token: 0x060041DB RID: 16859 RVA: 0x005F147C File Offset: 0x005EF67C
		// (remove) Token: 0x060041DC RID: 16860 RVA: 0x005F14B4 File Offset: 0x005EF6B4
		public event Action<TileBatch> OnWaveDraw;

		// Token: 0x060041DD RID: 16861 RVA: 0x005F14EC File Offset: 0x005EF6EC
		public WaterShaderData(string passName) : base(passName)
		{
			Main.OnRenderTargetsInitialized += this.InitRenderTargets;
			Main.OnRenderTargetsReleased += this.ReleaseRenderTargets;
			this._rippleShapeTexture = Main.Assets.Request<Texture2D>("Images/Misc/Ripples");
			Main.OnPreDraw += this.PreDraw;
		}

		// Token: 0x060041DE RID: 16862 RVA: 0x005F15A8 File Offset: 0x005EF7A8
		public override void Update(GameTime gameTime)
		{
			this._useViscosityFilter = (Main.WaveQuality >= 3);
			this._useProjectileWaves = (Main.WaveQuality >= 3);
			this._usePlayerWaves = (Main.WaveQuality >= 2);
			this._useRippleWaves = (Main.WaveQuality >= 2);
			this._useCustomWaves = (Main.WaveQuality >= 2);
			if (!Main.gamePaused && Main.hasFocus)
			{
				this._progress += (float)gameTime.ElapsedGameTime.TotalSeconds * base.Intensity * 0.75f;
				this._progress %= 86400f;
				if (this._useProjectileWaves || this._useRippleWaves || this._useCustomWaves || this._usePlayerWaves)
				{
					this._queuedSteps++;
				}
				base.Update(gameTime);
			}
		}

		// Token: 0x060041DF RID: 16863 RVA: 0x005F1688 File Offset: 0x005EF888
		private void StepLiquids()
		{
			this._isWaveBufferDirty = true;
			Vector2 vector = Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange);
			Vector2 vector2 = vector - Main.screenPosition;
			TileBatch tileBatch = Main.tileBatch;
			GraphicsDevice graphicsDevice = Main.instance.GraphicsDevice;
			graphicsDevice.SetRenderTarget(this._distortionTarget);
			if (this._clearNextFrame)
			{
				graphicsDevice.Clear(new Color(0.5f, 0.5f, 0f, 1f));
				this._clearNextFrame = false;
			}
			this.DrawWaves();
			graphicsDevice.SetRenderTarget(this._distortionTargetSwap);
			graphicsDevice.Clear(new Color(0.5f, 0.5f, 0.5f, 1f));
			Main.tileBatch.Begin();
			vector2 *= 0.25f;
			vector2.X = (float)Math.Floor((double)vector2.X);
			vector2.Y = (float)Math.Floor((double)vector2.Y);
			Vector2 vector3 = vector2 - this._lastDistortionDrawOffset;
			this._lastDistortionDrawOffset = vector2;
			tileBatch.Draw(this._distortionTarget, new Vector4(vector3.X, vector3.Y, (float)this._distortionTarget.Width, (float)this._distortionTarget.Height), new VertexColors(Color.White));
			GameShaders.Misc["WaterProcessor"].Apply(new DrawData?(new DrawData(this._distortionTarget, Vector2.Zero, Color.White)));
			tileBatch.End();
			RenderTarget2D distortionTarget = this._distortionTarget;
			this._distortionTarget = this._distortionTargetSwap;
			this._distortionTargetSwap = distortionTarget;
			if (this._useViscosityFilter)
			{
				LiquidRenderer.Instance.SetWaveMaskData(ref this._viscosityMaskChain[this._activeViscosityMask]);
				tileBatch.Begin();
				Rectangle cachedDrawArea = LiquidRenderer.Instance.GetCachedDrawArea();
				Rectangle rectangle;
				rectangle..ctor(0, 0, cachedDrawArea.Height, cachedDrawArea.Width);
				Vector4 destination;
				destination..ctor((float)(cachedDrawArea.X + cachedDrawArea.Width), (float)cachedDrawArea.Y, (float)cachedDrawArea.Height, (float)cachedDrawArea.Width);
				destination *= 16f;
				destination.X -= vector.X;
				destination.Y -= vector.Y;
				destination *= 0.25f;
				destination.X += vector2.X;
				destination.Y += vector2.Y;
				graphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
				tileBatch.Draw(this._viscosityMaskChain[this._activeViscosityMask], destination, new Rectangle?(rectangle), new VertexColors(Color.White), rectangle.Size(), 1, -1.5707964f);
				tileBatch.End();
				this._activeViscosityMask++;
				this._activeViscosityMask %= this._viscosityMaskChain.Length;
			}
			graphicsDevice.SetRenderTarget(null);
		}

		// Token: 0x060041E0 RID: 16864 RVA: 0x005F197C File Offset: 0x005EFB7C
		private void DrawWaves()
		{
			Vector2 screenPosition = Main.screenPosition;
			Vector2 vector = Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange);
			Vector2 vector2 = -this._lastDistortionDrawOffset / 0.25f + vector;
			TileBatch tileBatch = Main.tileBatch;
			GraphicsDevice graphicsDevice = Main.instance.GraphicsDevice;
			Vector2 dimensions;
			dimensions..ctor((float)Main.screenWidth, (float)Main.screenHeight);
			Vector2 vector3;
			vector3..ctor(16f, 16f);
			tileBatch.Begin();
			GameShaders.Misc["WaterDistortionObject"].Apply(null);
			if (this._useNPCWaves)
			{
				for (int i = 0; i < 200; i++)
				{
					if (Main.npc[i] != null && Main.npc[i].active && (Main.npc[i].wet || Main.npc[i].wetCount != 0) && Collision.CheckAABBvAABBCollision(screenPosition, dimensions, Main.npc[i].position - vector3, Main.npc[i].Size + vector3))
					{
						NPC nPC = Main.npc[i];
						Vector2 vector4 = nPC.Center - vector2;
						Vector2 vector5 = nPC.velocity.RotatedBy((double)(0f - nPC.rotation), default(Vector2)) / new Vector2((float)nPC.height, (float)nPC.width);
						float num = vector5.LengthSquared();
						num = num * 0.3f + 0.7f * num * (1024f / (float)(nPC.height * nPC.width));
						num = Math.Min(num, 0.08f);
						num += (nPC.velocity - nPC.oldVelocity).Length() * 0.5f;
						vector5.Normalize();
						Vector2 velocity = nPC.velocity;
						velocity.Normalize();
						vector4 -= velocity * 10f;
						if (!this._useViscosityFilter && (nPC.honeyWet || nPC.lavaWet))
						{
							num *= 0.3f;
						}
						if (nPC.wet)
						{
							tileBatch.Draw(TextureAssets.MagicPixel.Value, new Vector4(vector4.X, vector4.Y, (float)nPC.width * 2f, (float)nPC.height * 2f) * 0.25f, null, new VertexColors(new Color(vector5.X * 0.5f + 0.5f, vector5.Y * 0.5f + 0.5f, 0.5f * num)), new Vector2((float)TextureAssets.MagicPixel.Width() / 2f, (float)TextureAssets.MagicPixel.Height() / 2f), 0, nPC.rotation);
						}
						if (nPC.wetCount != 0)
						{
							num = nPC.velocity.Length();
							num = 0.195f * (float)Math.Sqrt((double)num);
							float num2 = 5f;
							if (!nPC.wet)
							{
								num2 = -20f;
							}
							this.QueueRipple(nPC.Center + velocity * num2, new Color(0.5f, (nPC.wet ? num : (0f - num)) * 0.5f + 0.5f, 0f, 1f) * 0.5f, new Vector2((float)nPC.width, (float)nPC.height * ((float)nPC.wetCount / 9f)) * MathHelper.Clamp(num * 10f, 0f, 1f), RippleShape.Circle, 0f);
						}
					}
				}
			}
			if (this._usePlayerWaves)
			{
				for (int j = 0; j < 255; j++)
				{
					if (Main.player[j] != null && Main.player[j].active && (Main.player[j].wet || Main.player[j].wetCount != 0) && Collision.CheckAABBvAABBCollision(screenPosition, dimensions, Main.player[j].position - vector3, Main.player[j].Size + vector3))
					{
						Player player = Main.player[j];
						Vector2 vector6 = player.Center - vector2;
						float num3 = player.velocity.Length();
						num3 = 0.05f * (float)Math.Sqrt((double)num3);
						Vector2 velocity2 = player.velocity;
						velocity2.Normalize();
						vector6 -= velocity2 * 10f;
						if (!this._useViscosityFilter && (player.honeyWet || player.lavaWet))
						{
							num3 *= 0.3f;
						}
						if (player.wet)
						{
							tileBatch.Draw(TextureAssets.MagicPixel.Value, new Vector4(vector6.X - (float)player.width * 2f * 0.5f, vector6.Y - (float)player.height * 2f * 0.5f, (float)player.width * 2f, (float)player.height * 2f) * 0.25f, new VertexColors(new Color(velocity2.X * 0.5f + 0.5f, velocity2.Y * 0.5f + 0.5f, 0.5f * num3)));
						}
						if (player.wetCount != 0)
						{
							float num4 = 5f;
							if (!player.wet)
							{
								num4 = -20f;
							}
							num3 *= 3f;
							this.QueueRipple(player.Center + velocity2 * num4, player.wet ? num3 : (0f - num3), new Vector2((float)player.width, (float)player.height * ((float)player.wetCount / 9f)) * MathHelper.Clamp(num3 * 10f, 0f, 1f), RippleShape.Circle, 0f);
						}
					}
				}
			}
			if (this._useProjectileWaves)
			{
				for (int k = 0; k < 1000; k++)
				{
					Projectile projectile = Main.projectile[k];
					if (projectile.wet && !projectile.lavaWet)
					{
						bool honeyWet = projectile.honeyWet;
					}
					bool flag = projectile.lavaWet;
					bool flag2 = projectile.honeyWet;
					bool flag3 = projectile.wet;
					if (projectile.ignoreWater)
					{
						flag3 = true;
					}
					if (projectile != null && projectile.active && ProjectileID.Sets.CanDistortWater[projectile.type] && flag3 && !ProjectileID.Sets.NoLiquidDistortion[projectile.type] && Collision.CheckAABBvAABBCollision(screenPosition, dimensions, projectile.position - vector3, projectile.Size + vector3))
					{
						if (projectile.ignoreWater)
						{
							bool flag4 = Collision.LavaCollision(projectile.position, projectile.width, projectile.height);
							flag = Collision.WetCollision(projectile.position, projectile.width, projectile.height);
							flag2 = Collision.honey;
							if (!flag4 && !flag && !flag2)
							{
								goto IL_87E;
							}
						}
						Vector2 vector7 = projectile.Center - vector2;
						float num5 = projectile.velocity.Length();
						num5 = 2f * (float)Math.Sqrt((double)(0.05f * num5));
						Vector2 velocity3 = projectile.velocity;
						velocity3.Normalize();
						if (!this._useViscosityFilter && (flag2 || flag))
						{
							num5 *= 0.3f;
						}
						float num6 = Math.Max(12f, (float)projectile.width * 0.75f);
						float num7 = Math.Max(12f, (float)projectile.height * 0.75f);
						tileBatch.Draw(TextureAssets.MagicPixel.Value, new Vector4(vector7.X - num6 * 0.5f, vector7.Y - num7 * 0.5f, num6, num7) * 0.25f, new VertexColors(new Color(velocity3.X * 0.5f + 0.5f, velocity3.Y * 0.5f + 0.5f, num5 * 0.5f)));
					}
					IL_87E:;
				}
			}
			tileBatch.End();
			if (this._useRippleWaves)
			{
				tileBatch.Begin();
				for (int l = 0; l < this._rippleQueueCount; l++)
				{
					Vector2 vector8 = this._rippleQueue[l].Position - vector2;
					Vector2 size = this._rippleQueue[l].Size;
					Rectangle sourceRectangle = this._rippleQueue[l].SourceRectangle;
					Texture2D value = this._rippleShapeTexture.Value;
					tileBatch.Draw(value, new Vector4(vector8.X, vector8.Y, size.X, size.Y) * 0.25f, new Rectangle?(sourceRectangle), new VertexColors(this._rippleQueue[l].WaveData), new Vector2((float)(sourceRectangle.Width / 2), (float)(sourceRectangle.Height / 2)), 0, this._rippleQueue[l].Rotation);
				}
				tileBatch.End();
			}
			this._rippleQueueCount = 0;
			if (this._useCustomWaves && this.OnWaveDraw != null)
			{
				tileBatch.Begin();
				this.OnWaveDraw(tileBatch);
				tileBatch.End();
			}
		}

		// Token: 0x060041E1 RID: 16865 RVA: 0x005F234C File Offset: 0x005F054C
		private void PreDraw(GameTime gameTime)
		{
			this.ValidateRenderTargets();
			if (!this._usingRenderTargets || !Main.IsGraphicsDeviceAvailable)
			{
				return;
			}
			if (this._useProjectileWaves || this._useRippleWaves || this._useCustomWaves || this._usePlayerWaves)
			{
				for (int i = 0; i < Math.Min(this._queuedSteps, 2); i++)
				{
					this.StepLiquids();
				}
			}
			else if (this._isWaveBufferDirty || this._clearNextFrame)
			{
				GraphicsDevice graphicsDevice = Main.instance.GraphicsDevice;
				graphicsDevice.SetRenderTarget(this._distortionTarget);
				graphicsDevice.Clear(new Color(0.5f, 0.5f, 0f, 1f));
				this._clearNextFrame = false;
				this._isWaveBufferDirty = false;
				graphicsDevice.SetRenderTarget(null);
			}
			this._queuedSteps = 0;
		}

		// Token: 0x060041E2 RID: 16866 RVA: 0x005F2410 File Offset: 0x005F0610
		public override void Apply()
		{
			if (this._usingRenderTargets && Main.IsGraphicsDeviceAvailable)
			{
				base.UseProgress(this._progress);
				Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
				Vector2 vector = new Vector2((float)Main.screenWidth, (float)Main.screenHeight) * 0.5f * (Vector2.One - Vector2.One / Main.GameViewMatrix.Zoom);
				Vector2 vector2 = (Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange)) - Main.screenPosition - vector;
				base.UseImage(this._distortionTarget, 1, null);
				base.UseImage(Main.waterTarget, 2, SamplerState.PointClamp);
				base.UseTargetPosition(Main.screenPosition - Main.sceneWaterPos + new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange) + vector);
				base.UseImageOffset(-(vector2 * 0.25f - this._lastDistortionDrawOffset) / new Vector2((float)this._distortionTarget.Width, (float)this._distortionTarget.Height));
				base.Apply();
			}
		}

		// Token: 0x060041E3 RID: 16867 RVA: 0x005F2568 File Offset: 0x005F0768
		private void ValidateRenderTargets()
		{
			int backBufferWidth = Main.instance.GraphicsDevice.PresentationParameters.BackBufferWidth;
			int backBufferHeight = Main.instance.GraphicsDevice.PresentationParameters.BackBufferHeight;
			bool flag = !Main.drawToScreen;
			if (this._usingRenderTargets && !flag)
			{
				this.ReleaseRenderTargets();
				return;
			}
			if (!this._usingRenderTargets && flag)
			{
				this.InitRenderTargets(backBufferWidth, backBufferHeight);
				return;
			}
			if (this._usingRenderTargets && flag && (this._distortionTarget.IsContentLost || this._distortionTargetSwap.IsContentLost))
			{
				this._clearNextFrame = true;
			}
		}

		// Token: 0x060041E4 RID: 16868 RVA: 0x005F25FC File Offset: 0x005F07FC
		private void InitRenderTargets(int width, int height)
		{
			this._lastScreenWidth = width;
			this._lastScreenHeight = height;
			width = (int)((float)width * 0.25f);
			height = (int)((float)height * 0.25f);
			try
			{
				this._distortionTarget = new RenderTarget2D(Main.instance.GraphicsDevice, width, height, false, 0, 0, 0, 1);
				this._distortionTargetSwap = new RenderTarget2D(Main.instance.GraphicsDevice, width, height, false, 0, 0, 0, 1);
				this._usingRenderTargets = true;
				this._clearNextFrame = true;
			}
			catch (Exception ex)
			{
				Lighting.Mode = LightMode.Retro;
				this._usingRenderTargets = false;
				string str = "Failed to create water distortion render targets. ";
				Exception ex2 = ex;
				Console.WriteLine(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x060041E5 RID: 16869 RVA: 0x005F26B4 File Offset: 0x005F08B4
		private void ReleaseRenderTargets()
		{
			try
			{
				if (this._distortionTarget != null)
				{
					this._distortionTarget.Dispose();
				}
				if (this._distortionTargetSwap != null)
				{
					this._distortionTargetSwap.Dispose();
				}
			}
			catch (Exception ex)
			{
				string str = "Error disposing of water distortion render targets. ";
				Exception ex2 = ex;
				Console.WriteLine(str + ((ex2 != null) ? ex2.ToString() : null));
			}
			this._distortionTarget = null;
			this._distortionTargetSwap = null;
			this._usingRenderTargets = false;
		}

		// Token: 0x060041E6 RID: 16870 RVA: 0x005F2730 File Offset: 0x005F0930
		public void QueueRipple(Vector2 position, float strength = 1f, RippleShape shape = RippleShape.Square, float rotation = 0f)
		{
			float g = strength * 0.5f + 0.5f;
			float num = Math.Min(Math.Abs(strength), 1f);
			this.QueueRipple(position, new Color(0.5f, g, 0f, 1f) * num, new Vector2(4f * Math.Max(Math.Abs(strength), 1f)), shape, rotation);
		}

		// Token: 0x060041E7 RID: 16871 RVA: 0x005F279C File Offset: 0x005F099C
		public void QueueRipple(Vector2 position, float strength, Vector2 size, RippleShape shape = RippleShape.Square, float rotation = 0f)
		{
			float g = strength * 0.5f + 0.5f;
			float num = Math.Min(Math.Abs(strength), 1f);
			this.QueueRipple(position, new Color(0.5f, g, 0f, 1f) * num, size, shape, rotation);
		}

		// Token: 0x060041E8 RID: 16872 RVA: 0x005F27F0 File Offset: 0x005F09F0
		public void QueueRipple(Vector2 position, Color waveData, Vector2 size, RippleShape shape = RippleShape.Square, float rotation = 0f)
		{
			if (!this._useRippleWaves || Main.drawToScreen)
			{
				this._rippleQueueCount = 0;
				return;
			}
			if (this._rippleQueueCount < this._rippleQueue.Length)
			{
				WaterShaderData.Ripple[] rippleQueue = this._rippleQueue;
				int rippleQueueCount = this._rippleQueueCount;
				this._rippleQueueCount = rippleQueueCount + 1;
				rippleQueue[rippleQueueCount] = new WaterShaderData.Ripple(position, waveData, size, shape, rotation);
			}
		}

		// Token: 0x04005909 RID: 22793
		private const float DISTORTION_BUFFER_SCALE = 0.25f;

		// Token: 0x0400590A RID: 22794
		private const float WAVE_FRAMERATE = 0.016666668f;

		// Token: 0x0400590B RID: 22795
		private const int MAX_RIPPLES_QUEUED = 200;

		// Token: 0x0400590C RID: 22796
		public bool _useViscosityFilter = true;

		// Token: 0x0400590D RID: 22797
		private RenderTarget2D _distortionTarget;

		// Token: 0x0400590E RID: 22798
		private RenderTarget2D _distortionTargetSwap;

		// Token: 0x0400590F RID: 22799
		private bool _usingRenderTargets;

		// Token: 0x04005910 RID: 22800
		private Vector2 _lastDistortionDrawOffset = Vector2.Zero;

		// Token: 0x04005911 RID: 22801
		private float _progress;

		// Token: 0x04005912 RID: 22802
		private WaterShaderData.Ripple[] _rippleQueue = new WaterShaderData.Ripple[200];

		// Token: 0x04005913 RID: 22803
		private int _rippleQueueCount;

		// Token: 0x04005914 RID: 22804
		private int _lastScreenWidth;

		// Token: 0x04005915 RID: 22805
		private int _lastScreenHeight;

		// Token: 0x04005916 RID: 22806
		public bool _useProjectileWaves = true;

		// Token: 0x04005917 RID: 22807
		private bool _useNPCWaves = true;

		// Token: 0x04005918 RID: 22808
		private bool _usePlayerWaves = true;

		// Token: 0x04005919 RID: 22809
		private bool _useRippleWaves = true;

		// Token: 0x0400591A RID: 22810
		private bool _useCustomWaves = true;

		// Token: 0x0400591B RID: 22811
		private bool _clearNextFrame = true;

		// Token: 0x0400591C RID: 22812
		private Texture2D[] _viscosityMaskChain = new Texture2D[3];

		// Token: 0x0400591D RID: 22813
		private int _activeViscosityMask;

		// Token: 0x0400591E RID: 22814
		private Asset<Texture2D> _rippleShapeTexture;

		// Token: 0x0400591F RID: 22815
		private bool _isWaveBufferDirty = true;

		// Token: 0x04005920 RID: 22816
		private int _queuedSteps;

		// Token: 0x04005921 RID: 22817
		private const int MAX_QUEUED_STEPS = 2;

		// Token: 0x02000C52 RID: 3154
		private struct Ripple
		{
			// Token: 0x17000963 RID: 2403
			// (get) Token: 0x06005FC6 RID: 24518 RVA: 0x006D0D93 File Offset: 0x006CEF93
			public Rectangle SourceRectangle
			{
				get
				{
					return WaterShaderData.Ripple.RIPPLE_SHAPE_SOURCE_RECTS[(int)this.Shape];
				}
			}

			// Token: 0x06005FC7 RID: 24519 RVA: 0x006D0DA5 File Offset: 0x006CEFA5
			public Ripple(Vector2 position, Color waveData, Vector2 size, RippleShape shape, float rotation)
			{
				this.Position = position;
				this.WaveData = waveData;
				this.Size = size;
				this.Shape = shape;
				this.Rotation = rotation;
			}

			// Token: 0x04007916 RID: 30998
			private static readonly Rectangle[] RIPPLE_SHAPE_SOURCE_RECTS = new Rectangle[]
			{
				new Rectangle(0, 0, 0, 0),
				new Rectangle(1, 1, 62, 62),
				new Rectangle(1, 65, 62, 62)
			};

			// Token: 0x04007917 RID: 30999
			public readonly Vector2 Position;

			// Token: 0x04007918 RID: 31000
			public readonly Color WaveData;

			// Token: 0x04007919 RID: 31001
			public readonly Vector2 Size;

			// Token: 0x0400791A RID: 31002
			public readonly RippleShape Shape;

			// Token: 0x0400791B RID: 31003
			public readonly float Rotation;
		}
	}
}
