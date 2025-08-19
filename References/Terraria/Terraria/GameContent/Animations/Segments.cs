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
	// Token: 0x020003EA RID: 1002
	public class Segments
	{
		// Token: 0x04004D70 RID: 19824
		private const float PixelsToRollUpPerFrame = 0.5f;

		// Token: 0x0200076B RID: 1899
		public class LocalizedTextSegment : IAnimationSegment
		{
			// Token: 0x17000411 RID: 1041
			// (get) Token: 0x060038E9 RID: 14569 RVA: 0x006147E5 File Offset: 0x006129E5
			public float DedicatedTimeNeeded
			{
				get
				{
					return 240f;
				}
			}

			// Token: 0x060038EA RID: 14570 RVA: 0x006147EC File Offset: 0x006129EC
			public LocalizedTextSegment(float timeInAnimation, string textKey)
			{
				this._text = Language.GetText(textKey);
				this._timeToShowPeak = timeInAnimation;
			}

			// Token: 0x060038EB RID: 14571 RVA: 0x00614807 File Offset: 0x00612A07
			public LocalizedTextSegment(float timeInAnimation, LocalizedText textObject, Vector2 anchorOffset)
			{
				this._text = textObject;
				this._timeToShowPeak = timeInAnimation;
				this._anchorOffset = anchorOffset;
			}

			// Token: 0x060038EC RID: 14572 RVA: 0x00614824 File Offset: 0x00612A24
			public void Draw(ref GameAnimationSegment info)
			{
				float num = 250f;
				float num2 = 250f;
				int timeInAnimation = info.TimeInAnimation;
				float num3 = Utils.GetLerpValue(this._timeToShowPeak - num, this._timeToShowPeak, (float)timeInAnimation, true) * Utils.GetLerpValue(this._timeToShowPeak + num2, this._timeToShowPeak, (float)timeInAnimation, true);
				if (num3 <= 0f)
				{
					return;
				}
				float num4 = this._timeToShowPeak - (float)timeInAnimation;
				Vector2 vector = info.AnchorPositionOnScreen + new Vector2(0f, num4 * 0.5f);
				vector += this._anchorOffset;
				Vector2 baseScale = new Vector2(0.7f);
				float num5 = Main.GlobalTimeWrappedHourly * 0.02f % 1f;
				if (num5 < 0f)
				{
					num5 += 1f;
				}
				Color value = Main.hslToRgb(num5, 1f, 0.5f, byte.MaxValue);
				string value2 = this._text.Value;
				Vector2 vector2 = FontAssets.DeathText.Value.MeasureString(value2);
				vector2 *= 0.5f;
				float scale = 1f - (1f - num3) * (1f - num3);
				ChatManager.DrawColorCodedStringShadow(info.SpriteBatch, FontAssets.DeathText.Value, value2, vector, value * scale * scale * 0.25f * info.DisplayOpacity, 0f, vector2, baseScale, -1f, 2f);
				ChatManager.DrawColorCodedString(info.SpriteBatch, FontAssets.DeathText.Value, value2, vector, Color.White * scale * info.DisplayOpacity, 0f, vector2, baseScale, -1f, false);
			}

			// Token: 0x0400646F RID: 25711
			private const int PixelsForALine = 120;

			// Token: 0x04006470 RID: 25712
			private LocalizedText _text;

			// Token: 0x04006471 RID: 25713
			private float _timeToShowPeak;

			// Token: 0x04006472 RID: 25714
			private Vector2 _anchorOffset;
		}

		// Token: 0x0200076C RID: 1900
		public abstract class AnimationSegmentWithActions<T> : IAnimationSegment
		{
			// Token: 0x17000412 RID: 1042
			// (get) Token: 0x060038ED RID: 14573 RVA: 0x006149D4 File Offset: 0x00612BD4
			public float DedicatedTimeNeeded
			{
				get
				{
					return (float)this._dedicatedTimeNeeded;
				}
			}

			// Token: 0x060038EE RID: 14574 RVA: 0x006149DD File Offset: 0x00612BDD
			public AnimationSegmentWithActions(int targetTime)
			{
				this._targetTime = targetTime;
				this._dedicatedTimeNeeded = 0;
			}

			// Token: 0x060038EF RID: 14575 RVA: 0x00614A00 File Offset: 0x00612C00
			protected void ProcessActions(T obj, float localTimeForObject)
			{
				for (int i = 0; i < this._actions.Count; i++)
				{
					this._actions[i].ApplyTo(obj, localTimeForObject);
				}
			}

			// Token: 0x060038F0 RID: 14576 RVA: 0x00614A38 File Offset: 0x00612C38
			public Segments.AnimationSegmentWithActions<T> Then(IAnimationSegmentAction<T> act)
			{
				this.Bind(act);
				act.SetDelay((float)this._dedicatedTimeNeeded);
				this._actions.Add(act);
				this._lastDedicatedTimeNeeded = this._dedicatedTimeNeeded;
				this._dedicatedTimeNeeded += act.ExpectedLengthOfActionInFrames;
				return this;
			}

			// Token: 0x060038F1 RID: 14577 RVA: 0x00614A85 File Offset: 0x00612C85
			public Segments.AnimationSegmentWithActions<T> With(IAnimationSegmentAction<T> act)
			{
				this.Bind(act);
				act.SetDelay((float)this._lastDedicatedTimeNeeded);
				this._actions.Add(act);
				return this;
			}

			// Token: 0x060038F2 RID: 14578
			protected abstract void Bind(IAnimationSegmentAction<T> act);

			// Token: 0x060038F3 RID: 14579
			public abstract void Draw(ref GameAnimationSegment info);

			// Token: 0x04006473 RID: 25715
			private int _dedicatedTimeNeeded;

			// Token: 0x04006474 RID: 25716
			private int _lastDedicatedTimeNeeded;

			// Token: 0x04006475 RID: 25717
			protected int _targetTime;

			// Token: 0x04006476 RID: 25718
			private List<IAnimationSegmentAction<T>> _actions = new List<IAnimationSegmentAction<T>>();
		}

		// Token: 0x0200076D RID: 1901
		public class PlayerSegment : Segments.AnimationSegmentWithActions<Player>
		{
			// Token: 0x060038F4 RID: 14580 RVA: 0x00614AA8 File Offset: 0x00612CA8
			public PlayerSegment(int targetTime, Vector2 anchorOffset, Vector2 normalizedHitboxOrigin) : base(targetTime)
			{
				this._player = new Player();
				this._anchorOffset = anchorOffset;
				this._normalizedOriginForHitbox = normalizedHitboxOrigin;
			}

			// Token: 0x060038F5 RID: 14581 RVA: 0x00614ACA File Offset: 0x00612CCA
			public Segments.PlayerSegment UseShaderEffect(Segments.PlayerSegment.IShaderEffect shaderEffect)
			{
				this._shaderEffect = shaderEffect;
				return this;
			}

			// Token: 0x060038F6 RID: 14582 RVA: 0x00614AD4 File Offset: 0x00612CD4
			protected override void Bind(IAnimationSegmentAction<Player> act)
			{
				act.BindTo(this._player);
			}

			// Token: 0x060038F7 RID: 14583 RVA: 0x00614AE4 File Offset: 0x00612CE4
			public override void Draw(ref GameAnimationSegment info)
			{
				if ((float)info.TimeInAnimation > (float)this._targetTime + base.DedicatedTimeNeeded)
				{
					return;
				}
				if (info.TimeInAnimation < this._targetTime)
				{
					return;
				}
				this.ResetPlayerAnimation(ref info);
				float localTimeForObject = (float)(info.TimeInAnimation - this._targetTime);
				base.ProcessActions(this._player, localTimeForObject);
				if (info.DisplayOpacity == 0f)
				{
					return;
				}
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
				float shadow = 1f - this._player.opacityForAnimation;
				shadow = 0f;
				if (this._shaderEffect != null)
				{
					this._shaderEffect.BeforeDrawing(ref info);
				}
				Main.PlayerRenderer.DrawPlayer(Main.Camera, this._player, this._player.position, 0f, this._player.fullRotationOrigin, shadow, 1f);
				if (this._shaderEffect != null)
				{
					this._shaderEffect.AfterDrawing(ref info);
				}
				this._player.inventory[this._player.selectedItem] = item;
			}

			// Token: 0x060038F8 RID: 14584 RVA: 0x00614CB6 File Offset: 0x00612EB6
			private void ResetPlayerAnimation(ref GameAnimationSegment info)
			{
				this._player.CopyVisuals(Main.LocalPlayer);
				this._player.position = info.AnchorPositionOnScreen + this._anchorOffset;
				this._player.opacityForAnimation = 1f;
			}

			// Token: 0x04006477 RID: 25719
			private Player _player;

			// Token: 0x04006478 RID: 25720
			private Vector2 _anchorOffset;

			// Token: 0x04006479 RID: 25721
			private Vector2 _normalizedOriginForHitbox;

			// Token: 0x0400647A RID: 25722
			private Segments.PlayerSegment.IShaderEffect _shaderEffect;

			// Token: 0x0400647B RID: 25723
			private static Item _blankItem = new Item();

			// Token: 0x02000848 RID: 2120
			public interface IShaderEffect
			{
				// Token: 0x06003ACC RID: 15052
				void BeforeDrawing(ref GameAnimationSegment info);

				// Token: 0x06003ACD RID: 15053
				void AfterDrawing(ref GameAnimationSegment info);
			}

			// Token: 0x02000849 RID: 2121
			public class ImmediateSpritebatchForPlayerDyesEffect : Segments.PlayerSegment.IShaderEffect
			{
				// Token: 0x06003ACE RID: 15054 RVA: 0x0061A263 File Offset: 0x00618463
				public void BeforeDrawing(ref GameAnimationSegment info)
				{
					info.SpriteBatch.End();
					info.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.CurrentFrameFlags.Hacks.CurrentBackgroundMatrixForCreditsRoll);
				}

				// Token: 0x06003ACF RID: 15055 RVA: 0x0061A296 File Offset: 0x00618496
				public void AfterDrawing(ref GameAnimationSegment info)
				{
					info.SpriteBatch.End();
					info.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.CurrentFrameFlags.Hacks.CurrentBackgroundMatrixForCreditsRoll);
				}
			}
		}

		// Token: 0x0200076E RID: 1902
		public class NPCSegment : Segments.AnimationSegmentWithActions<NPC>
		{
			// Token: 0x060038FA RID: 14586 RVA: 0x00614D00 File Offset: 0x00612F00
			public NPCSegment(int targetTime, int npcId, Vector2 anchorOffset, Vector2 normalizedNPCHitboxOrigin) : base(targetTime)
			{
				this._npc = new NPC();
				this._npc.SetDefaults(npcId, new NPCSpawnParams
				{
					gameModeData = Main.RegisteredGameModes[0],
					playerCountForMultiplayerDifficultyOverride = new int?(1),
					sizeScaleOverride = null,
					strengthMultiplierOverride = new float?((float)1)
				});
				this._npc.IsABestiaryIconDummy = true;
				this._anchorOffset = anchorOffset;
				this._normalizedOriginForHitbox = normalizedNPCHitboxOrigin;
			}

			// Token: 0x060038FB RID: 14587 RVA: 0x00614D89 File Offset: 0x00612F89
			protected override void Bind(IAnimationSegmentAction<NPC> act)
			{
				act.BindTo(this._npc);
			}

			// Token: 0x060038FC RID: 14588 RVA: 0x00614D98 File Offset: 0x00612F98
			public override void Draw(ref GameAnimationSegment info)
			{
				if ((float)info.TimeInAnimation > (float)this._targetTime + base.DedicatedTimeNeeded)
				{
					return;
				}
				if (info.TimeInAnimation < this._targetTime)
				{
					return;
				}
				this.ResetNPCAnimation(ref info);
				float localTimeForObject = (float)(info.TimeInAnimation - this._targetTime);
				base.ProcessActions(this._npc, localTimeForObject);
				if (this._npc.alpha >= 255)
				{
					return;
				}
				this._npc.FindFrame();
				ITownNPCProfile townNPCProfile;
				if (TownNPCProfiles.Instance.GetProfile(this._npc.type, out townNPCProfile))
				{
					TextureAssets.Npc[this._npc.type] = townNPCProfile.GetTextureNPCShouldUse(this._npc);
				}
				this._npc.Opacity *= info.DisplayOpacity;
				Main.instance.DrawNPCDirect(info.SpriteBatch, this._npc, this._npc.behindTiles, Vector2.Zero);
			}

			// Token: 0x060038FD RID: 14589 RVA: 0x00614E84 File Offset: 0x00613084
			private void ResetNPCAnimation(ref GameAnimationSegment info)
			{
				this._npc.position = info.AnchorPositionOnScreen + this._anchorOffset - this._npc.Size * this._normalizedOriginForHitbox;
				this._npc.alpha = 0;
				this._npc.velocity = Vector2.Zero;
			}

			// Token: 0x0400647C RID: 25724
			private NPC _npc;

			// Token: 0x0400647D RID: 25725
			private Vector2 _anchorOffset;

			// Token: 0x0400647E RID: 25726
			private Vector2 _normalizedOriginForHitbox;
		}

		// Token: 0x0200076F RID: 1903
		public class LooseSprite
		{
			// Token: 0x060038FE RID: 14590 RVA: 0x00614EE4 File Offset: 0x006130E4
			public LooseSprite(DrawData data, Asset<Texture2D> asset)
			{
				this._originalDrawData = data;
				this._asset = asset;
				this.Reset();
			}

			// Token: 0x060038FF RID: 14591 RVA: 0x00614F00 File Offset: 0x00613100
			public void Reset()
			{
				this._originalDrawData.texture = this._asset.Value;
				this.CurrentDrawData = this._originalDrawData;
				this.CurrentOpacity = 1f;
			}

			// Token: 0x0400647F RID: 25727
			private DrawData _originalDrawData;

			// Token: 0x04006480 RID: 25728
			private Asset<Texture2D> _asset;

			// Token: 0x04006481 RID: 25729
			public DrawData CurrentDrawData;

			// Token: 0x04006482 RID: 25730
			public float CurrentOpacity;
		}

		// Token: 0x02000770 RID: 1904
		public class SpriteSegment : Segments.AnimationSegmentWithActions<Segments.LooseSprite>
		{
			// Token: 0x06003900 RID: 14592 RVA: 0x00614F2F File Offset: 0x0061312F
			public SpriteSegment(Asset<Texture2D> asset, int targetTime, DrawData data, Vector2 anchorOffset) : base(targetTime)
			{
				this._sprite = new Segments.LooseSprite(data, asset);
				this._anchorOffset = anchorOffset;
			}

			// Token: 0x06003901 RID: 14593 RVA: 0x00614F4D File Offset: 0x0061314D
			protected override void Bind(IAnimationSegmentAction<Segments.LooseSprite> act)
			{
				act.BindTo(this._sprite);
			}

			// Token: 0x06003902 RID: 14594 RVA: 0x00614F5B File Offset: 0x0061315B
			public Segments.SpriteSegment UseShaderEffect(Segments.SpriteSegment.IShaderEffect shaderEffect)
			{
				this._shaderEffect = shaderEffect;
				return this;
			}

			// Token: 0x06003903 RID: 14595 RVA: 0x00614F68 File Offset: 0x00613168
			public override void Draw(ref GameAnimationSegment info)
			{
				if ((float)info.TimeInAnimation > (float)this._targetTime + base.DedicatedTimeNeeded)
				{
					return;
				}
				if (info.TimeInAnimation < this._targetTime)
				{
					return;
				}
				this.ResetSpriteAnimation(ref info);
				float localTimeForObject = (float)(info.TimeInAnimation - this._targetTime);
				base.ProcessActions(this._sprite, localTimeForObject);
				DrawData currentDrawData = this._sprite.CurrentDrawData;
				currentDrawData.position += info.AnchorPositionOnScreen + this._anchorOffset;
				currentDrawData.color *= this._sprite.CurrentOpacity * info.DisplayOpacity;
				if (this._shaderEffect != null)
				{
					this._shaderEffect.BeforeDrawing(ref info, ref currentDrawData);
				}
				currentDrawData.Draw(info.SpriteBatch);
				if (this._shaderEffect != null)
				{
					this._shaderEffect.AfterDrawing(ref info, ref currentDrawData);
				}
			}

			// Token: 0x06003904 RID: 14596 RVA: 0x00615055 File Offset: 0x00613255
			private void ResetSpriteAnimation(ref GameAnimationSegment info)
			{
				this._sprite.Reset();
			}

			// Token: 0x04006483 RID: 25731
			private Segments.LooseSprite _sprite;

			// Token: 0x04006484 RID: 25732
			private Vector2 _anchorOffset;

			// Token: 0x04006485 RID: 25733
			private Segments.SpriteSegment.IShaderEffect _shaderEffect;

			// Token: 0x0200084A RID: 2122
			public interface IShaderEffect
			{
				// Token: 0x06003AD1 RID: 15057
				void BeforeDrawing(ref GameAnimationSegment info, ref DrawData drawData);

				// Token: 0x06003AD2 RID: 15058
				void AfterDrawing(ref GameAnimationSegment info, ref DrawData drawData);
			}

			// Token: 0x0200084B RID: 2123
			public class MaskedFadeEffect : Segments.SpriteSegment.IShaderEffect
			{
				// Token: 0x06003AD3 RID: 15059 RVA: 0x0061A2CC File Offset: 0x006184CC
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

				// Token: 0x06003AD4 RID: 15060 RVA: 0x0061A31E File Offset: 0x0061851E
				private Matrix DefaultFetchMatrix()
				{
					return Main.CurrentFrameFlags.Hacks.CurrentBackgroundMatrixForCreditsRoll;
				}

				// Token: 0x06003AD5 RID: 15061 RVA: 0x0061A328 File Offset: 0x00618528
				public void BeforeDrawing(ref GameAnimationSegment info, ref DrawData drawData)
				{
					info.SpriteBatch.End();
					info.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, this._fetchMatrix());
					MiscShaderData miscShaderData = GameShaders.Misc[this._shaderKey];
					int num = info.TimeInAnimation / this._verticalFrameWait % this._verticalFrameCount;
					miscShaderData.UseShaderSpecificData(new Vector4(1f / (float)this._verticalFrameCount, (float)num / (float)this._verticalFrameCount, this._panX.GetPanAmount((float)info.TimeInAnimation), this._panY.GetPanAmount((float)info.TimeInAnimation)));
					miscShaderData.Apply(new DrawData?(drawData));
				}

				// Token: 0x06003AD6 RID: 15062 RVA: 0x0061A3E8 File Offset: 0x006185E8
				public Segments.SpriteSegment.MaskedFadeEffect WithPanX(Segments.Panning panning)
				{
					this._panX = panning;
					return this;
				}

				// Token: 0x06003AD7 RID: 15063 RVA: 0x0061A3F2 File Offset: 0x006185F2
				public Segments.SpriteSegment.MaskedFadeEffect WithPanY(Segments.Panning panning)
				{
					this._panY = panning;
					return this;
				}

				// Token: 0x06003AD8 RID: 15064 RVA: 0x0061A3FC File Offset: 0x006185FC
				public void AfterDrawing(ref GameAnimationSegment info, ref DrawData drawData)
				{
					Main.pixelShader.CurrentTechnique.Passes[0].Apply();
					info.SpriteBatch.End();
					info.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, this._fetchMatrix());
				}

				// Token: 0x040065F1 RID: 26097
				private readonly string _shaderKey;

				// Token: 0x040065F2 RID: 26098
				private readonly int _verticalFrameCount;

				// Token: 0x040065F3 RID: 26099
				private readonly int _verticalFrameWait;

				// Token: 0x040065F4 RID: 26100
				private Segments.Panning _panX;

				// Token: 0x040065F5 RID: 26101
				private Segments.Panning _panY;

				// Token: 0x040065F6 RID: 26102
				private Segments.SpriteSegment.MaskedFadeEffect.GetMatrixAction _fetchMatrix;

				// Token: 0x0200086F RID: 2159
				// (Invoke) Token: 0x06003B64 RID: 15204
				public delegate Matrix GetMatrixAction();
			}
		}

		// Token: 0x02000771 RID: 1905
		public struct Panning
		{
			// Token: 0x06003905 RID: 14597 RVA: 0x00615064 File Offset: 0x00613264
			public float GetPanAmount(float time)
			{
				float num = MathHelper.Clamp((time - this.Delay) / this.Duration, 0f, 1f);
				return this.StartAmount + num * this.AmountOverTime;
			}

			// Token: 0x04006486 RID: 25734
			public float AmountOverTime;

			// Token: 0x04006487 RID: 25735
			public float StartAmount;

			// Token: 0x04006488 RID: 25736
			public float Delay;

			// Token: 0x04006489 RID: 25737
			public float Duration;
		}

		// Token: 0x02000772 RID: 1906
		public class EmoteSegment : IAnimationSegment
		{
			// Token: 0x17000413 RID: 1043
			// (get) Token: 0x06003906 RID: 14598 RVA: 0x0061509F File Offset: 0x0061329F
			// (set) Token: 0x06003907 RID: 14599 RVA: 0x006150A7 File Offset: 0x006132A7
			public float DedicatedTimeNeeded { get; private set; }

			// Token: 0x06003908 RID: 14600 RVA: 0x006150B0 File Offset: 0x006132B0
			public EmoteSegment(int emoteId, int targetTime, int timeToPlay, Vector2 position, SpriteEffects drawEffect, Vector2 velocity = default(Vector2))
			{
				this._emoteId = emoteId;
				this._targetTime = targetTime;
				this._effect = drawEffect;
				this._offset = position;
				this._velocity = velocity;
				this.DedicatedTimeNeeded = (float)timeToPlay;
			}

			// Token: 0x06003909 RID: 14601 RVA: 0x006150E8 File Offset: 0x006132E8
			public void Draw(ref GameAnimationSegment info)
			{
				int num = info.TimeInAnimation - this._targetTime;
				if (num < 0)
				{
					return;
				}
				if ((float)num >= this.DedicatedTimeNeeded)
				{
					return;
				}
				Vector2 vector = info.AnchorPositionOnScreen + this._offset + this._velocity * (float)num;
				vector = vector.Floor();
				bool flag = num < 6 || (float)num >= this.DedicatedTimeNeeded - 6f;
				Texture2D value = TextureAssets.Extra[48].Value;
				Rectangle rectangle = value.Frame(8, EmoteBubble.EMOTE_SHEET_VERTICAL_FRAMES, flag ? 0 : 1, 0, 0, 0);
				Vector2 origin = new Vector2((float)(rectangle.Width / 2), (float)rectangle.Height);
				SpriteEffects spriteEffects = this._effect;
				info.SpriteBatch.Draw(value, vector, new Rectangle?(rectangle), Color.White * info.DisplayOpacity, 0f, origin, 1f, spriteEffects, 0f);
				if (!flag)
				{
					int emoteId = this._emoteId;
					if ((emoteId == 87 || emoteId == 89) && spriteEffects.HasFlag(SpriteEffects.FlipHorizontally))
					{
						spriteEffects &= ~SpriteEffects.FlipHorizontally;
						vector.X += 4f;
					}
					info.SpriteBatch.Draw(value, vector, new Rectangle?(this.GetFrame(num % 20)), Color.White, 0f, origin, 1f, spriteEffects, 0f);
				}
			}

			// Token: 0x0600390A RID: 14602 RVA: 0x00615250 File Offset: 0x00613450
			private Rectangle GetFrame(int wrappedTime)
			{
				int num = (wrappedTime >= 10) ? 1 : 0;
				return TextureAssets.Extra[48].Value.Frame(8, EmoteBubble.EMOTE_SHEET_VERTICAL_FRAMES, this._emoteId % 4 * 2 + num, this._emoteId / 4 + 1, 0, 0);
			}

			// Token: 0x0400648B RID: 25739
			private int _targetTime;

			// Token: 0x0400648C RID: 25740
			private Vector2 _offset;

			// Token: 0x0400648D RID: 25741
			private SpriteEffects _effect;

			// Token: 0x0400648E RID: 25742
			private int _emoteId;

			// Token: 0x0400648F RID: 25743
			private Vector2 _velocity;
		}
	}
}
