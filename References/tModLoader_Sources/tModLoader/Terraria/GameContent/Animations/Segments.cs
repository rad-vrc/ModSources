using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.Graphics.Shaders;
using Terraria.Localization;
using Terraria.UI.Chat;

namespace Terraria.GameContent.Animations
{
	// Token: 0x020006B2 RID: 1714
	public class Segments
	{
		// Token: 0x04005C47 RID: 23623
		private const float PixelsToRollUpPerFrame = 0.5f;

		// Token: 0x02000D40 RID: 3392
		public class LocalizedTextSegment : IAnimationSegment
		{
			// Token: 0x1700099D RID: 2461
			// (get) Token: 0x0600638D RID: 25485 RVA: 0x006D8D58 File Offset: 0x006D6F58
			public float DedicatedTimeNeeded
			{
				get
				{
					return 240f;
				}
			}

			// Token: 0x0600638E RID: 25486 RVA: 0x006D8D5F File Offset: 0x006D6F5F
			public LocalizedTextSegment(float timeInAnimation, string textKey)
			{
				this._text = Language.GetText(textKey);
				this._timeToShowPeak = timeInAnimation;
			}

			// Token: 0x0600638F RID: 25487 RVA: 0x006D8D7A File Offset: 0x006D6F7A
			public LocalizedTextSegment(float timeInAnimation, LocalizedText textObject, Vector2 anchorOffset)
			{
				this._text = textObject;
				this._timeToShowPeak = timeInAnimation;
				this._anchorOffset = anchorOffset;
			}

			// Token: 0x06006390 RID: 25488 RVA: 0x006D8D98 File Offset: 0x006D6F98
			public void Draw(ref GameAnimationSegment info)
			{
				float num = 250f;
				float num2 = 250f;
				int timeInAnimation = info.TimeInAnimation;
				float num3 = Utils.GetLerpValue(this._timeToShowPeak - num, this._timeToShowPeak, (float)timeInAnimation, true) * Utils.GetLerpValue(this._timeToShowPeak + num2, this._timeToShowPeak, (float)timeInAnimation, true);
				if (num3 > 0f)
				{
					float num4 = this._timeToShowPeak - (float)timeInAnimation;
					Vector2 position = info.AnchorPositionOnScreen + new Vector2(0f, num4 * 0.5f);
					position += this._anchorOffset;
					Vector2 baseScale;
					baseScale..ctor(0.7f);
					float num5 = Main.GlobalTimeWrappedHourly * 0.02f % 1f;
					if (num5 < 0f)
					{
						num5 += 1f;
					}
					Color color = Main.hslToRgb(num5, 1f, 0.5f, byte.MaxValue);
					string value = this._text.Value;
					Vector2 origin = FontAssets.DeathText.Value.MeasureString(value);
					origin *= 0.5f;
					float num6 = 1f - (1f - num3) * (1f - num3);
					ChatManager.DrawColorCodedStringShadow(info.SpriteBatch, FontAssets.DeathText.Value, value, position, color * num6 * num6 * 0.25f * info.DisplayOpacity, 0f, origin, baseScale, -1f, 2f);
					ChatManager.DrawColorCodedString(info.SpriteBatch, FontAssets.DeathText.Value, value, position, Color.White * num6 * info.DisplayOpacity, 0f, origin, baseScale, -1f, false);
				}
			}

			// Token: 0x04007B33 RID: 31539
			private const int PixelsForALine = 120;

			// Token: 0x04007B34 RID: 31540
			private LocalizedText _text;

			// Token: 0x04007B35 RID: 31541
			private float _timeToShowPeak;

			// Token: 0x04007B36 RID: 31542
			private Vector2 _anchorOffset;
		}

		// Token: 0x02000D41 RID: 3393
		public abstract class AnimationSegmentWithActions<T> : IAnimationSegment
		{
			// Token: 0x1700099E RID: 2462
			// (get) Token: 0x06006391 RID: 25489 RVA: 0x006D8F4A File Offset: 0x006D714A
			public float DedicatedTimeNeeded
			{
				get
				{
					return (float)this._dedicatedTimeNeeded;
				}
			}

			// Token: 0x06006392 RID: 25490 RVA: 0x006D8F53 File Offset: 0x006D7153
			public AnimationSegmentWithActions(int targetTime)
			{
				this._targetTime = targetTime;
				this._dedicatedTimeNeeded = 0;
			}

			// Token: 0x06006393 RID: 25491 RVA: 0x006D8F74 File Offset: 0x006D7174
			protected void ProcessActions(T obj, float localTimeForObject)
			{
				for (int i = 0; i < this._actions.Count; i++)
				{
					this._actions[i].ApplyTo(obj, localTimeForObject);
				}
			}

			// Token: 0x06006394 RID: 25492 RVA: 0x006D8FAC File Offset: 0x006D71AC
			public Segments.AnimationSegmentWithActions<T> Then(IAnimationSegmentAction<T> act)
			{
				this.Bind(act);
				act.SetDelay((float)this._dedicatedTimeNeeded);
				this._actions.Add(act);
				this._lastDedicatedTimeNeeded = this._dedicatedTimeNeeded;
				this._dedicatedTimeNeeded += act.ExpectedLengthOfActionInFrames;
				return this;
			}

			// Token: 0x06006395 RID: 25493 RVA: 0x006D8FF9 File Offset: 0x006D71F9
			public Segments.AnimationSegmentWithActions<T> With(IAnimationSegmentAction<T> act)
			{
				this.Bind(act);
				act.SetDelay((float)this._lastDedicatedTimeNeeded);
				this._actions.Add(act);
				return this;
			}

			// Token: 0x06006396 RID: 25494
			protected abstract void Bind(IAnimationSegmentAction<T> act);

			// Token: 0x06006397 RID: 25495
			public abstract void Draw(ref GameAnimationSegment info);

			// Token: 0x04007B37 RID: 31543
			private int _dedicatedTimeNeeded;

			// Token: 0x04007B38 RID: 31544
			private int _lastDedicatedTimeNeeded;

			// Token: 0x04007B39 RID: 31545
			protected int _targetTime;

			// Token: 0x04007B3A RID: 31546
			private List<IAnimationSegmentAction<T>> _actions = new List<IAnimationSegmentAction<T>>();
		}

		// Token: 0x02000D42 RID: 3394
		public class PlayerSegment : Segments.AnimationSegmentWithActions<Player>
		{
			// Token: 0x06006398 RID: 25496 RVA: 0x006D901C File Offset: 0x006D721C
			public PlayerSegment(int targetTime, Vector2 anchorOffset, Vector2 normalizedHitboxOrigin) : base(targetTime)
			{
				this._player = new Player();
				this._anchorOffset = anchorOffset;
				this._normalizedOriginForHitbox = normalizedHitboxOrigin;
			}

			// Token: 0x06006399 RID: 25497 RVA: 0x006D903E File Offset: 0x006D723E
			public Segments.PlayerSegment UseShaderEffect(Segments.PlayerSegment.IShaderEffect shaderEffect)
			{
				this._shaderEffect = shaderEffect;
				return this;
			}

			// Token: 0x0600639A RID: 25498 RVA: 0x006D9048 File Offset: 0x006D7248
			protected override void Bind(IAnimationSegmentAction<Player> act)
			{
				act.BindTo(this._player);
			}

			// Token: 0x0600639B RID: 25499 RVA: 0x006D9058 File Offset: 0x006D7258
			public override void Draw(ref GameAnimationSegment info)
			{
				if ((float)info.TimeInAnimation > (float)this._targetTime + base.DedicatedTimeNeeded || info.TimeInAnimation < this._targetTime)
				{
					return;
				}
				this.ResetPlayerAnimation(ref info);
				float localTimeForObject = (float)(info.TimeInAnimation - this._targetTime);
				base.ProcessActions(this._player, localTimeForObject);
				if (info.DisplayOpacity != 0f)
				{
					this._player.ResetEffects();
					this._player.ResetVisibleAccessories();
					this._player.UpdateMiscCounter();
					this._player.UpdateDyes();
					this._player.PlayerFrame();
					this._player.socialIgnoreLight = true;
					this._player.position += Main.screenPosition;
					this._player.position -= new Vector2((float)(this._player.width / 2), (float)this._player.height);
					this._player.opacityForAnimation *= info.DisplayOpacity;
					Item item = this._player.inventory[this._player.selectedItem];
					this._player.inventory[this._player.selectedItem] = Segments.PlayerSegment._blankItem;
					float num = 1f - this._player.opacityForAnimation;
					num = 0f;
					if (this._shaderEffect != null)
					{
						this._shaderEffect.BeforeDrawing(ref info);
					}
					Main.PlayerRenderer.DrawPlayer(Main.Camera, this._player, this._player.position, 0f, this._player.fullRotationOrigin, num, 1f);
					if (this._shaderEffect != null)
					{
						this._shaderEffect.AfterDrawing(ref info);
					}
					this._player.inventory[this._player.selectedItem] = item;
				}
			}

			// Token: 0x0600639C RID: 25500 RVA: 0x006D922B File Offset: 0x006D742B
			private void ResetPlayerAnimation(ref GameAnimationSegment info)
			{
				this._player.CopyVisuals(Main.LocalPlayer);
				this._player.position = info.AnchorPositionOnScreen + this._anchorOffset;
				this._player.opacityForAnimation = 1f;
			}

			// Token: 0x04007B3B RID: 31547
			private Player _player;

			// Token: 0x04007B3C RID: 31548
			private Vector2 _anchorOffset;

			// Token: 0x04007B3D RID: 31549
			private Vector2 _normalizedOriginForHitbox;

			// Token: 0x04007B3E RID: 31550
			private Segments.PlayerSegment.IShaderEffect _shaderEffect;

			// Token: 0x04007B3F RID: 31551
			private static Item _blankItem = new Item();

			// Token: 0x02000E86 RID: 3718
			public interface IShaderEffect
			{
				// Token: 0x060066C4 RID: 26308
				void BeforeDrawing(ref GameAnimationSegment info);

				// Token: 0x060066C5 RID: 26309
				void AfterDrawing(ref GameAnimationSegment info);
			}

			// Token: 0x02000E87 RID: 3719
			public class ImmediateSpritebatchForPlayerDyesEffect : Segments.PlayerSegment.IShaderEffect
			{
				// Token: 0x060066C6 RID: 26310 RVA: 0x006E19FD File Offset: 0x006DFBFD
				public void BeforeDrawing(ref GameAnimationSegment info)
				{
					info.SpriteBatch.End();
					info.SpriteBatch.Begin(1, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.CurrentFrameFlags.Hacks.CurrentBackgroundMatrixForCreditsRoll);
				}

				// Token: 0x060066C7 RID: 26311 RVA: 0x006E1A30 File Offset: 0x006DFC30
				public void AfterDrawing(ref GameAnimationSegment info)
				{
					info.SpriteBatch.End();
					info.SpriteBatch.Begin(0, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.CurrentFrameFlags.Hacks.CurrentBackgroundMatrixForCreditsRoll);
				}
			}
		}

		// Token: 0x02000D43 RID: 3395
		public class NPCSegment : Segments.AnimationSegmentWithActions<NPC>
		{
			// Token: 0x0600639E RID: 25502 RVA: 0x006D9278 File Offset: 0x006D7478
			public NPCSegment(int targetTime, int npcId, Vector2 anchorOffset, Vector2 normalizedNPCHitboxOrigin) : base(targetTime)
			{
				this._npc = new NPC();
				this._npc.SetDefaults(npcId, new NPCSpawnParams
				{
					gameModeData = new GameModeData?(Main.RegisteredGameModes[0]),
					playerCountForMultiplayerDifficultyOverride = new int?(1),
					sizeScaleOverride = null,
					strengthMultiplierOverride = new float?(1f)
				});
				this._npc.IsABestiaryIconDummy = true;
				this._anchorOffset = anchorOffset;
				this._normalizedOriginForHitbox = normalizedNPCHitboxOrigin;
			}

			// Token: 0x0600639F RID: 25503 RVA: 0x006D9309 File Offset: 0x006D7509
			protected override void Bind(IAnimationSegmentAction<NPC> act)
			{
				act.BindTo(this._npc);
			}

			// Token: 0x060063A0 RID: 25504 RVA: 0x006D9318 File Offset: 0x006D7518
			public override void Draw(ref GameAnimationSegment info)
			{
				if ((float)info.TimeInAnimation > (float)this._targetTime + base.DedicatedTimeNeeded || info.TimeInAnimation < this._targetTime)
				{
					return;
				}
				this.ResetNPCAnimation(ref info);
				float localTimeForObject = (float)(info.TimeInAnimation - this._targetTime);
				base.ProcessActions(this._npc, localTimeForObject);
				if (this._npc.alpha < 255)
				{
					this._npc.FindFrame();
					ITownNPCProfile profile;
					if (TownNPCProfiles.Instance.GetProfile(this._npc, out profile))
					{
						TextureAssets.Npc[this._npc.type] = profile.GetTextureNPCShouldUse(this._npc);
					}
					this._npc.Opacity *= info.DisplayOpacity;
					Main.instance.DrawNPCDirect(info.SpriteBatch, this._npc, this._npc.behindTiles, Vector2.Zero);
				}
			}

			// Token: 0x060063A1 RID: 25505 RVA: 0x006D93FC File Offset: 0x006D75FC
			private void ResetNPCAnimation(ref GameAnimationSegment info)
			{
				this._npc.position = info.AnchorPositionOnScreen + this._anchorOffset - this._npc.Size * this._normalizedOriginForHitbox;
				this._npc.alpha = 0;
				this._npc.velocity = Vector2.Zero;
			}

			// Token: 0x04007B40 RID: 31552
			private NPC _npc;

			// Token: 0x04007B41 RID: 31553
			private Vector2 _anchorOffset;

			// Token: 0x04007B42 RID: 31554
			private Vector2 _normalizedOriginForHitbox;
		}

		// Token: 0x02000D44 RID: 3396
		public class LooseSprite
		{
			// Token: 0x060063A2 RID: 25506 RVA: 0x006D945C File Offset: 0x006D765C
			public LooseSprite(DrawData data, Asset<Texture2D> asset)
			{
				this._originalDrawData = data;
				this._asset = asset;
				this.Reset();
			}

			// Token: 0x060063A3 RID: 25507 RVA: 0x006D9478 File Offset: 0x006D7678
			public void Reset()
			{
				this._originalDrawData.texture = this._asset.Value;
				this.CurrentDrawData = this._originalDrawData;
				this.CurrentOpacity = 1f;
			}

			// Token: 0x04007B43 RID: 31555
			private DrawData _originalDrawData;

			// Token: 0x04007B44 RID: 31556
			private Asset<Texture2D> _asset;

			// Token: 0x04007B45 RID: 31557
			public DrawData CurrentDrawData;

			// Token: 0x04007B46 RID: 31558
			public float CurrentOpacity;
		}

		// Token: 0x02000D45 RID: 3397
		public class SpriteSegment : Segments.AnimationSegmentWithActions<Segments.LooseSprite>
		{
			// Token: 0x060063A4 RID: 25508 RVA: 0x006D94A7 File Offset: 0x006D76A7
			public SpriteSegment(Asset<Texture2D> asset, int targetTime, DrawData data, Vector2 anchorOffset) : base(targetTime)
			{
				this._sprite = new Segments.LooseSprite(data, asset);
				this._anchorOffset = anchorOffset;
			}

			// Token: 0x060063A5 RID: 25509 RVA: 0x006D94C5 File Offset: 0x006D76C5
			protected override void Bind(IAnimationSegmentAction<Segments.LooseSprite> act)
			{
				act.BindTo(this._sprite);
			}

			// Token: 0x060063A6 RID: 25510 RVA: 0x006D94D3 File Offset: 0x006D76D3
			public Segments.SpriteSegment UseShaderEffect(Segments.SpriteSegment.IShaderEffect shaderEffect)
			{
				this._shaderEffect = shaderEffect;
				return this;
			}

			// Token: 0x060063A7 RID: 25511 RVA: 0x006D94E0 File Offset: 0x006D76E0
			public override void Draw(ref GameAnimationSegment info)
			{
				if ((float)info.TimeInAnimation <= (float)this._targetTime + base.DedicatedTimeNeeded && info.TimeInAnimation >= this._targetTime)
				{
					this.ResetSpriteAnimation(ref info);
					float localTimeForObject = (float)(info.TimeInAnimation - this._targetTime);
					base.ProcessActions(this._sprite, localTimeForObject);
					DrawData drawData = this._sprite.CurrentDrawData;
					drawData.position += info.AnchorPositionOnScreen + this._anchorOffset;
					drawData.color *= this._sprite.CurrentOpacity * info.DisplayOpacity;
					if (this._shaderEffect != null)
					{
						this._shaderEffect.BeforeDrawing(ref info, ref drawData);
					}
					drawData.Draw(info.SpriteBatch);
					if (this._shaderEffect != null)
					{
						this._shaderEffect.AfterDrawing(ref info, ref drawData);
					}
				}
			}

			// Token: 0x060063A8 RID: 25512 RVA: 0x006D95D1 File Offset: 0x006D77D1
			private void ResetSpriteAnimation(ref GameAnimationSegment info)
			{
				this._sprite.Reset();
			}

			// Token: 0x04007B47 RID: 31559
			private Segments.LooseSprite _sprite;

			// Token: 0x04007B48 RID: 31560
			private Vector2 _anchorOffset;

			// Token: 0x04007B49 RID: 31561
			private Segments.SpriteSegment.IShaderEffect _shaderEffect;

			// Token: 0x02000E88 RID: 3720
			public interface IShaderEffect
			{
				// Token: 0x060066C9 RID: 26313
				void BeforeDrawing(ref GameAnimationSegment info, ref DrawData drawData);

				// Token: 0x060066CA RID: 26314
				void AfterDrawing(ref GameAnimationSegment info, ref DrawData drawData);
			}

			// Token: 0x02000E89 RID: 3721
			public class MaskedFadeEffect : Segments.SpriteSegment.IShaderEffect
			{
				// Token: 0x060066CB RID: 26315 RVA: 0x006E1A6C File Offset: 0x006DFC6C
				public MaskedFadeEffect(Segments.SpriteSegment.MaskedFadeEffect.GetMatrixAction fetchMatrixMethod = null, string shaderKey = "MaskedFade", int verticalFrameCount = 1, int verticalFrameWait = 1)
				{
					this._fetchMatrix = fetchMatrixMethod;
					this._shaderKey = shaderKey;
					this._verticalFrameCount = verticalFrameCount;
					if (verticalFrameWait < 1)
					{
						verticalFrameWait = 1;
					}
					this._verticalFrameWait = verticalFrameWait;
					if (this._fetchMatrix == null)
					{
						this._fetchMatrix = new Segments.SpriteSegment.MaskedFadeEffect.GetMatrixAction(this.DefaultFetchMatrix);
					}
				}

				// Token: 0x060066CC RID: 26316 RVA: 0x006E1ABE File Offset: 0x006DFCBE
				private Matrix DefaultFetchMatrix()
				{
					return Main.CurrentFrameFlags.Hacks.CurrentBackgroundMatrixForCreditsRoll;
				}

				// Token: 0x060066CD RID: 26317 RVA: 0x006E1AC8 File Offset: 0x006DFCC8
				public void BeforeDrawing(ref GameAnimationSegment info, ref DrawData drawData)
				{
					info.SpriteBatch.End();
					info.SpriteBatch.Begin(1, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, this._fetchMatrix());
					MiscShaderData miscShaderData = GameShaders.Misc[this._shaderKey];
					float num = (float)(info.TimeInAnimation / this._verticalFrameWait % this._verticalFrameCount) / (float)this._verticalFrameCount;
					miscShaderData.UseShaderSpecificData(new Vector4(1f / (float)this._verticalFrameCount, num, this._panX.GetPanAmount((float)info.TimeInAnimation), this._panY.GetPanAmount((float)info.TimeInAnimation)));
					miscShaderData.Apply(new DrawData?(drawData));
				}

				// Token: 0x060066CE RID: 26318 RVA: 0x006E1B88 File Offset: 0x006DFD88
				public Segments.SpriteSegment.MaskedFadeEffect WithPanX(Segments.Panning panning)
				{
					this._panX = panning;
					return this;
				}

				// Token: 0x060066CF RID: 26319 RVA: 0x006E1B92 File Offset: 0x006DFD92
				public Segments.SpriteSegment.MaskedFadeEffect WithPanY(Segments.Panning panning)
				{
					this._panY = panning;
					return this;
				}

				// Token: 0x060066D0 RID: 26320 RVA: 0x006E1B9C File Offset: 0x006DFD9C
				public void AfterDrawing(ref GameAnimationSegment info, ref DrawData drawData)
				{
					Main.pixelShader.CurrentTechnique.Passes[0].Apply();
					info.SpriteBatch.End();
					info.SpriteBatch.Begin(0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, this._fetchMatrix());
				}

				// Token: 0x04007DB8 RID: 32184
				private readonly string _shaderKey;

				// Token: 0x04007DB9 RID: 32185
				private readonly int _verticalFrameCount;

				// Token: 0x04007DBA RID: 32186
				private readonly int _verticalFrameWait;

				// Token: 0x04007DBB RID: 32187
				private Segments.Panning _panX;

				// Token: 0x04007DBC RID: 32188
				private Segments.Panning _panY;

				// Token: 0x04007DBD RID: 32189
				private Segments.SpriteSegment.MaskedFadeEffect.GetMatrixAction _fetchMatrix;

				// Token: 0x02000E99 RID: 3737
				// (Invoke) Token: 0x060066FF RID: 26367
				public delegate Matrix GetMatrixAction();
			}
		}

		// Token: 0x02000D46 RID: 3398
		public struct Panning
		{
			// Token: 0x060063A9 RID: 25513 RVA: 0x006D95E0 File Offset: 0x006D77E0
			public float GetPanAmount(float time)
			{
				float num = MathHelper.Clamp((time - this.Delay) / this.Duration, 0f, 1f);
				return this.StartAmount + num * this.AmountOverTime;
			}

			// Token: 0x04007B4A RID: 31562
			public float AmountOverTime;

			// Token: 0x04007B4B RID: 31563
			public float StartAmount;

			// Token: 0x04007B4C RID: 31564
			public float Delay;

			// Token: 0x04007B4D RID: 31565
			public float Duration;
		}

		// Token: 0x02000D47 RID: 3399
		public class EmoteSegment : IAnimationSegment
		{
			// Token: 0x1700099F RID: 2463
			// (get) Token: 0x060063AA RID: 25514 RVA: 0x006D961B File Offset: 0x006D781B
			// (set) Token: 0x060063AB RID: 25515 RVA: 0x006D9623 File Offset: 0x006D7823
			public float DedicatedTimeNeeded { get; private set; }

			// Token: 0x060063AC RID: 25516 RVA: 0x006D962C File Offset: 0x006D782C
			public EmoteSegment(int emoteId, int targetTime, int timeToPlay, Vector2 position, SpriteEffects drawEffect, Vector2 velocity = default(Vector2))
			{
				this._emoteId = emoteId;
				this._targetTime = targetTime;
				this._effect = drawEffect;
				this._offset = position;
				this._velocity = velocity;
				this.DedicatedTimeNeeded = (float)timeToPlay;
			}

			// Token: 0x060063AD RID: 25517 RVA: 0x006D9664 File Offset: 0x006D7864
			public void Draw(ref GameAnimationSegment info)
			{
				int num = info.TimeInAnimation - this._targetTime;
				if (num < 0 || (float)num >= this.DedicatedTimeNeeded)
				{
					return;
				}
				Vector2 vec = info.AnchorPositionOnScreen + this._offset + this._velocity * (float)num;
				vec = vec.Floor();
				bool flag = num < 6 || (float)num >= this.DedicatedTimeNeeded - 6f;
				Texture2D value = TextureAssets.Extra[48].Value;
				Rectangle value2 = value.Frame(8, EmoteBubble.EMOTE_SHEET_VERTICAL_FRAMES, (!flag) ? 1 : 0, 0, 0, 0);
				Vector2 origin;
				origin..ctor((float)(value2.Width / 2), (float)value2.Height);
				SpriteEffects spriteEffects = this._effect;
				info.SpriteBatch.Draw(value, vec, new Rectangle?(value2), Color.White * info.DisplayOpacity, 0f, origin, 1f, spriteEffects, 0f);
				if (!flag)
				{
					int emoteId = this._emoteId;
					if ((emoteId == 87 || emoteId == 89) && spriteEffects.HasFlag(1))
					{
						spriteEffects &= -2;
						vec.X += 4f;
					}
					info.SpriteBatch.Draw(value, vec, new Rectangle?(this.GetFrame(num % 20)), Color.White, 0f, origin, 1f, spriteEffects, 0f);
				}
			}

			// Token: 0x060063AE RID: 25518 RVA: 0x006D97C8 File Offset: 0x006D79C8
			private Rectangle GetFrame(int wrappedTime)
			{
				int num = (wrappedTime >= 10) ? 1 : 0;
				return TextureAssets.Extra[48].Value.Frame(8, EmoteBubble.EMOTE_SHEET_VERTICAL_FRAMES, this._emoteId % 4 * 2 + num, this._emoteId / 4 + 1, 0, 0);
			}

			// Token: 0x04007B4E RID: 31566
			private int _targetTime;

			// Token: 0x04007B4F RID: 31567
			private Vector2 _offset;

			// Token: 0x04007B50 RID: 31568
			private SpriteEffects _effect;

			// Token: 0x04007B51 RID: 31569
			private int _emoteId;

			// Token: 0x04007B52 RID: 31570
			private Vector2 _velocity;
		}
	}
}
