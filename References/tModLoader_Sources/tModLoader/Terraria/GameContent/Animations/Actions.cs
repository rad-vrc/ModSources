using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.Animations
{
	// Token: 0x020006AD RID: 1709
	public class Actions
	{
		// Token: 0x02000D3D RID: 3389
		public class Players
		{
			// Token: 0x02000E6A RID: 3690
			public interface IPlayerAction : IAnimationSegmentAction<Player>
			{
			}

			// Token: 0x02000E6B RID: 3691
			public class Fade : Actions.Players.IPlayerAction, IAnimationSegmentAction<Player>
			{
				// Token: 0x170009B9 RID: 2489
				// (get) Token: 0x06006644 RID: 26180 RVA: 0x006E0C68 File Offset: 0x006DEE68
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06006645 RID: 26181 RVA: 0x006E0C70 File Offset: 0x006DEE70
				public Fade(float opacityTarget)
				{
					this._duration = 0;
					this._opacityTarget = opacityTarget;
				}

				// Token: 0x06006646 RID: 26182 RVA: 0x006E0C86 File Offset: 0x006DEE86
				public Fade(float opacityTarget, int duration)
				{
					this._duration = duration;
					this._opacityTarget = opacityTarget;
				}

				// Token: 0x06006647 RID: 26183 RVA: 0x006E0C9C File Offset: 0x006DEE9C
				public void BindTo(Player obj)
				{
				}

				// Token: 0x06006648 RID: 26184 RVA: 0x006E0C9E File Offset: 0x006DEE9E
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06006649 RID: 26185 RVA: 0x006E0CA8 File Offset: 0x006DEEA8
				public void ApplyTo(Player obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					if (this._duration == 0)
					{
						obj.opacityForAnimation = this._opacityTarget;
						return;
					}
					float num = localTimeForObj - this._delay;
					if (num > (float)this._duration)
					{
						num = (float)this._duration;
					}
					obj.opacityForAnimation = MathHelper.Lerp(obj.opacityForAnimation, this._opacityTarget, Utils.GetLerpValue(0f, (float)this._duration, num, true));
				}

				// Token: 0x04007D74 RID: 32116
				private int _duration;

				// Token: 0x04007D75 RID: 32117
				private float _opacityTarget;

				// Token: 0x04007D76 RID: 32118
				private float _delay;
			}

			// Token: 0x02000E6C RID: 3692
			public class Wait : Actions.Players.IPlayerAction, IAnimationSegmentAction<Player>
			{
				// Token: 0x170009BA RID: 2490
				// (get) Token: 0x0600664A RID: 26186 RVA: 0x006E0D19 File Offset: 0x006DEF19
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x0600664B RID: 26187 RVA: 0x006E0D21 File Offset: 0x006DEF21
				public Wait(int durationInFrames)
				{
					this._duration = durationInFrames;
				}

				// Token: 0x0600664C RID: 26188 RVA: 0x006E0D30 File Offset: 0x006DEF30
				public void BindTo(Player obj)
				{
				}

				// Token: 0x0600664D RID: 26189 RVA: 0x006E0D32 File Offset: 0x006DEF32
				public void ApplyTo(Player obj, float localTimeForObj)
				{
					if (localTimeForObj >= this._delay)
					{
						obj.velocity = Vector2.Zero;
					}
				}

				// Token: 0x0600664E RID: 26190 RVA: 0x006E0D48 File Offset: 0x006DEF48
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x04007D77 RID: 32119
				private int _duration;

				// Token: 0x04007D78 RID: 32120
				private float _delay;
			}

			// Token: 0x02000E6D RID: 3693
			public class LookAt : Actions.Players.IPlayerAction, IAnimationSegmentAction<Player>
			{
				// Token: 0x170009BB RID: 2491
				// (get) Token: 0x0600664F RID: 26191 RVA: 0x006E0D51 File Offset: 0x006DEF51
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x06006650 RID: 26192 RVA: 0x006E0D54 File Offset: 0x006DEF54
				public LookAt(int direction)
				{
					this._direction = direction;
				}

				// Token: 0x06006651 RID: 26193 RVA: 0x006E0D63 File Offset: 0x006DEF63
				public void BindTo(Player obj)
				{
				}

				// Token: 0x06006652 RID: 26194 RVA: 0x006E0D65 File Offset: 0x006DEF65
				public void ApplyTo(Player obj, float localTimeForObj)
				{
					if (localTimeForObj >= this._delay)
					{
						obj.direction = this._direction;
					}
				}

				// Token: 0x06006653 RID: 26195 RVA: 0x006E0D7C File Offset: 0x006DEF7C
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x04007D79 RID: 32121
				private int _direction;

				// Token: 0x04007D7A RID: 32122
				private float _delay;
			}

			// Token: 0x02000E6E RID: 3694
			public class MoveWithAcceleration : Actions.Players.IPlayerAction, IAnimationSegmentAction<Player>
			{
				// Token: 0x170009BC RID: 2492
				// (get) Token: 0x06006654 RID: 26196 RVA: 0x006E0D85 File Offset: 0x006DEF85
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06006655 RID: 26197 RVA: 0x006E0D8D File Offset: 0x006DEF8D
				public MoveWithAcceleration(Vector2 offsetPerFrame, Vector2 accelerationPerFrame, int durationInFrames)
				{
					this._accelerationPerFrame = accelerationPerFrame;
					this._offsetPerFrame = offsetPerFrame;
					this._duration = durationInFrames;
				}

				// Token: 0x06006656 RID: 26198 RVA: 0x006E0DAA File Offset: 0x006DEFAA
				public void BindTo(Player obj)
				{
				}

				// Token: 0x06006657 RID: 26199 RVA: 0x006E0DAC File Offset: 0x006DEFAC
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06006658 RID: 26200 RVA: 0x006E0DB8 File Offset: 0x006DEFB8
				public void ApplyTo(Player obj, float localTimeForObj)
				{
					if (localTimeForObj >= this._delay)
					{
						float num = localTimeForObj - this._delay;
						if (num > (float)this._duration)
						{
							num = (float)this._duration;
						}
						Vector2 vector = this._offsetPerFrame * num + this._accelerationPerFrame * (num * num * 0.5f);
						obj.position += vector;
						obj.velocity = this._offsetPerFrame + this._accelerationPerFrame * num;
						if (this._offsetPerFrame.X != 0f)
						{
							obj.direction = ((this._offsetPerFrame.X > 0f) ? 1 : -1);
						}
					}
				}

				// Token: 0x04007D7B RID: 32123
				private Vector2 _offsetPerFrame;

				// Token: 0x04007D7C RID: 32124
				private Vector2 _accelerationPerFrame;

				// Token: 0x04007D7D RID: 32125
				private int _duration;

				// Token: 0x04007D7E RID: 32126
				private float _delay;
			}
		}

		// Token: 0x02000D3E RID: 3390
		public class NPCs
		{
			// Token: 0x02000E6F RID: 3695
			public interface INPCAction : IAnimationSegmentAction<NPC>
			{
			}

			// Token: 0x02000E70 RID: 3696
			public class Fade : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x170009BD RID: 2493
				// (get) Token: 0x06006659 RID: 26201 RVA: 0x006E0E6F File Offset: 0x006DF06F
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x0600665A RID: 26202 RVA: 0x006E0E77 File Offset: 0x006DF077
				public Fade(int alphaPerFrame)
				{
					this._duration = 0;
					this._alphaPerFrame = alphaPerFrame;
				}

				// Token: 0x0600665B RID: 26203 RVA: 0x006E0E8D File Offset: 0x006DF08D
				public Fade(int alphaPerFrame, int duration)
				{
					this._duration = duration;
					this._alphaPerFrame = alphaPerFrame;
				}

				// Token: 0x0600665C RID: 26204 RVA: 0x006E0EA3 File Offset: 0x006DF0A3
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x0600665D RID: 26205 RVA: 0x006E0EA5 File Offset: 0x006DF0A5
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x0600665E RID: 26206 RVA: 0x006E0EB0 File Offset: 0x006DF0B0
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					if (this._duration == 0)
					{
						obj.alpha = Utils.Clamp<int>(obj.alpha + this._alphaPerFrame, 0, 255);
						return;
					}
					float num = localTimeForObj - this._delay;
					if (num > (float)this._duration)
					{
						num = (float)this._duration;
					}
					obj.alpha = Utils.Clamp<int>(obj.alpha + (int)num * this._alphaPerFrame, 0, 255);
				}

				// Token: 0x04007D7F RID: 32127
				private int _duration;

				// Token: 0x04007D80 RID: 32128
				private int _alphaPerFrame;

				// Token: 0x04007D81 RID: 32129
				private float _delay;
			}

			// Token: 0x02000E71 RID: 3697
			public class ShowItem : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x170009BE RID: 2494
				// (get) Token: 0x0600665F RID: 26207 RVA: 0x006E0F2A File Offset: 0x006DF12A
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06006660 RID: 26208 RVA: 0x006E0F32 File Offset: 0x006DF132
				public ShowItem(int durationInFrames, int itemIdToShow)
				{
					this._duration = durationInFrames;
					this._itemIdToShow = itemIdToShow;
				}

				// Token: 0x06006661 RID: 26209 RVA: 0x006E0F48 File Offset: 0x006DF148
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x06006662 RID: 26210 RVA: 0x006E0F4A File Offset: 0x006DF14A
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06006663 RID: 26211 RVA: 0x006E0F54 File Offset: 0x006DF154
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
					if (localTimeForObj >= this._delay)
					{
						float num = localTimeForObj - this._delay;
						if (num > (float)this._duration)
						{
							this.FixNPCIfWasHoldingItem(obj);
							return;
						}
						obj.velocity = Vector2.Zero;
						obj.frameCounter = (double)num;
						obj.ai[0] = 23f;
						obj.ai[1] = (float)this._duration - num;
						obj.ai[2] = (float)this._itemIdToShow;
					}
				}

				// Token: 0x06006664 RID: 26212 RVA: 0x006E0FC8 File Offset: 0x006DF1C8
				private void FixNPCIfWasHoldingItem(NPC obj)
				{
					if (obj.ai[0] == 23f)
					{
						obj.frameCounter = 0.0;
						obj.ai[0] = 0f;
						obj.ai[1] = 0f;
						obj.ai[2] = 0f;
					}
				}

				// Token: 0x04007D82 RID: 32130
				private int _itemIdToShow;

				// Token: 0x04007D83 RID: 32131
				private int _duration;

				// Token: 0x04007D84 RID: 32132
				private float _delay;
			}

			// Token: 0x02000E72 RID: 3698
			public class Move : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x170009BF RID: 2495
				// (get) Token: 0x06006665 RID: 26213 RVA: 0x006E101A File Offset: 0x006DF21A
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06006666 RID: 26214 RVA: 0x006E1022 File Offset: 0x006DF222
				public Move(Vector2 offsetPerFrame, int durationInFrames)
				{
					this._offsetPerFrame = offsetPerFrame;
					this._duration = durationInFrames;
				}

				// Token: 0x06006667 RID: 26215 RVA: 0x006E1038 File Offset: 0x006DF238
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x06006668 RID: 26216 RVA: 0x006E103A File Offset: 0x006DF23A
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06006669 RID: 26217 RVA: 0x006E1044 File Offset: 0x006DF244
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
					if (localTimeForObj >= this._delay)
					{
						float num = localTimeForObj - this._delay;
						if (num > (float)this._duration)
						{
							num = (float)this._duration;
						}
						obj.position += this._offsetPerFrame * num;
						obj.velocity = this._offsetPerFrame;
						if (this._offsetPerFrame.X != 0f)
						{
							obj.direction = (obj.spriteDirection = ((this._offsetPerFrame.X > 0f) ? 1 : -1));
						}
					}
				}

				// Token: 0x04007D85 RID: 32133
				private Vector2 _offsetPerFrame;

				// Token: 0x04007D86 RID: 32134
				private int _duration;

				// Token: 0x04007D87 RID: 32135
				private float _delay;
			}

			// Token: 0x02000E73 RID: 3699
			public class MoveWithAcceleration : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x170009C0 RID: 2496
				// (get) Token: 0x0600666A RID: 26218 RVA: 0x006E10D5 File Offset: 0x006DF2D5
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x0600666B RID: 26219 RVA: 0x006E10DD File Offset: 0x006DF2DD
				public MoveWithAcceleration(Vector2 offsetPerFrame, Vector2 accelerationPerFrame, int durationInFrames)
				{
					this._accelerationPerFrame = accelerationPerFrame;
					this._offsetPerFrame = offsetPerFrame;
					this._duration = durationInFrames;
				}

				// Token: 0x0600666C RID: 26220 RVA: 0x006E10FA File Offset: 0x006DF2FA
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x0600666D RID: 26221 RVA: 0x006E10FC File Offset: 0x006DF2FC
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x0600666E RID: 26222 RVA: 0x006E1108 File Offset: 0x006DF308
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
					if (localTimeForObj >= this._delay)
					{
						float num = localTimeForObj - this._delay;
						if (num > (float)this._duration)
						{
							num = (float)this._duration;
						}
						Vector2 vector = this._offsetPerFrame * num + this._accelerationPerFrame * (num * num * 0.5f);
						obj.position += vector;
						obj.velocity = this._offsetPerFrame + this._accelerationPerFrame * num;
						if (this._offsetPerFrame.X != 0f)
						{
							obj.direction = (obj.spriteDirection = ((this._offsetPerFrame.X > 0f) ? 1 : -1));
						}
					}
				}

				// Token: 0x04007D88 RID: 32136
				private Vector2 _offsetPerFrame;

				// Token: 0x04007D89 RID: 32137
				private Vector2 _accelerationPerFrame;

				// Token: 0x04007D8A RID: 32138
				private int _duration;

				// Token: 0x04007D8B RID: 32139
				private float _delay;
			}

			// Token: 0x02000E74 RID: 3700
			public class MoveWithRotor : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x170009C1 RID: 2497
				// (get) Token: 0x0600666F RID: 26223 RVA: 0x006E11C8 File Offset: 0x006DF3C8
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06006670 RID: 26224 RVA: 0x006E11D0 File Offset: 0x006DF3D0
				public MoveWithRotor(Vector2 radialOffset, float rotationPerFrame, Vector2 resultMultiplier, int durationInFrames)
				{
					this._radialOffset = rotationPerFrame;
					this._offsetPerFrame = radialOffset;
					this._resultMultiplier = resultMultiplier;
					this._duration = durationInFrames;
				}

				// Token: 0x06006671 RID: 26225 RVA: 0x006E11F5 File Offset: 0x006DF3F5
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x06006672 RID: 26226 RVA: 0x006E11F7 File Offset: 0x006DF3F7
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06006673 RID: 26227 RVA: 0x006E1200 File Offset: 0x006DF400
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
					if (localTimeForObj >= this._delay)
					{
						float num = localTimeForObj - this._delay;
						if (num > (float)this._duration)
						{
							num = (float)this._duration;
						}
						Vector2 vector = this._offsetPerFrame.RotatedBy((double)(this._radialOffset * num), default(Vector2)) * this._resultMultiplier;
						obj.position += vector;
					}
				}

				// Token: 0x04007D8C RID: 32140
				private Vector2 _offsetPerFrame;

				// Token: 0x04007D8D RID: 32141
				private Vector2 _resultMultiplier;

				// Token: 0x04007D8E RID: 32142
				private float _radialOffset;

				// Token: 0x04007D8F RID: 32143
				private int _duration;

				// Token: 0x04007D90 RID: 32144
				private float _delay;
			}

			// Token: 0x02000E75 RID: 3701
			public class DoBunnyRestAnimation : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x170009C2 RID: 2498
				// (get) Token: 0x06006674 RID: 26228 RVA: 0x006E126C File Offset: 0x006DF46C
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06006675 RID: 26229 RVA: 0x006E1274 File Offset: 0x006DF474
				public DoBunnyRestAnimation(int durationInFrames)
				{
					this._duration = durationInFrames;
				}

				// Token: 0x06006676 RID: 26230 RVA: 0x006E1283 File Offset: 0x006DF483
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x06006677 RID: 26231 RVA: 0x006E1285 File Offset: 0x006DF485
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06006678 RID: 26232 RVA: 0x006E1290 File Offset: 0x006DF490
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					float num = localTimeForObj - this._delay;
					if (num > (float)this._duration)
					{
						num = (float)this._duration;
					}
					Rectangle frame = obj.frame;
					int num2 = 10;
					int num3 = (int)num;
					while (num3 > 4)
					{
						num3 -= 4;
						num2++;
						if (num2 > 13)
						{
							num2 = 13;
						}
					}
					obj.ai[0] = 21f;
					obj.ai[1] = 31f;
					obj.frameCounter = (double)num3;
					obj.frame.Y = num2 * frame.Height;
				}

				// Token: 0x04007D91 RID: 32145
				private int _duration;

				// Token: 0x04007D92 RID: 32146
				private float _delay;
			}

			// Token: 0x02000E76 RID: 3702
			public class Wait : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x170009C3 RID: 2499
				// (get) Token: 0x06006679 RID: 26233 RVA: 0x006E131A File Offset: 0x006DF51A
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x0600667A RID: 26234 RVA: 0x006E1322 File Offset: 0x006DF522
				public Wait(int durationInFrames)
				{
					this._duration = durationInFrames;
				}

				// Token: 0x0600667B RID: 26235 RVA: 0x006E1331 File Offset: 0x006DF531
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x0600667C RID: 26236 RVA: 0x006E1333 File Offset: 0x006DF533
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
					if (localTimeForObj >= this._delay)
					{
						obj.velocity = Vector2.Zero;
					}
				}

				// Token: 0x0600667D RID: 26237 RVA: 0x006E1349 File Offset: 0x006DF549
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x04007D93 RID: 32147
				private int _duration;

				// Token: 0x04007D94 RID: 32148
				private float _delay;
			}

			// Token: 0x02000E77 RID: 3703
			public class Blink : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x170009C4 RID: 2500
				// (get) Token: 0x0600667E RID: 26238 RVA: 0x006E1352 File Offset: 0x006DF552
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x0600667F RID: 26239 RVA: 0x006E135A File Offset: 0x006DF55A
				public Blink(int durationInFrames)
				{
					this._duration = durationInFrames;
				}

				// Token: 0x06006680 RID: 26240 RVA: 0x006E1369 File Offset: 0x006DF569
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x06006681 RID: 26241 RVA: 0x006E136C File Offset: 0x006DF56C
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
					if (localTimeForObj >= this._delay)
					{
						obj.velocity = Vector2.Zero;
						obj.ai[0] = 0f;
						if (localTimeForObj <= this._delay + (float)this._duration)
						{
							obj.ai[0] = 1001f;
						}
					}
				}

				// Token: 0x06006682 RID: 26242 RVA: 0x006E13B8 File Offset: 0x006DF5B8
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x04007D95 RID: 32149
				private int _duration;

				// Token: 0x04007D96 RID: 32150
				private float _delay;
			}

			// Token: 0x02000E78 RID: 3704
			public class LookAt : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x170009C5 RID: 2501
				// (get) Token: 0x06006683 RID: 26243 RVA: 0x006E13C1 File Offset: 0x006DF5C1
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x06006684 RID: 26244 RVA: 0x006E13C4 File Offset: 0x006DF5C4
				public LookAt(int direction)
				{
					this._direction = direction;
				}

				// Token: 0x06006685 RID: 26245 RVA: 0x006E13D3 File Offset: 0x006DF5D3
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x06006686 RID: 26246 RVA: 0x006E13D8 File Offset: 0x006DF5D8
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
					if (localTimeForObj >= this._delay)
					{
						obj.direction = (obj.spriteDirection = this._direction);
					}
				}

				// Token: 0x06006687 RID: 26247 RVA: 0x006E1403 File Offset: 0x006DF603
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x04007D97 RID: 32151
				private int _direction;

				// Token: 0x04007D98 RID: 32152
				private float _delay;
			}

			// Token: 0x02000E79 RID: 3705
			public class PartyHard : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x170009C6 RID: 2502
				// (get) Token: 0x06006688 RID: 26248 RVA: 0x006E140C File Offset: 0x006DF60C
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x06006689 RID: 26249 RVA: 0x006E140F File Offset: 0x006DF60F
				public void BindTo(NPC obj)
				{
					obj.ForcePartyHatOn = true;
					obj.UpdateAltTexture();
				}

				// Token: 0x0600668A RID: 26250 RVA: 0x006E141E File Offset: 0x006DF61E
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
				}

				// Token: 0x0600668B RID: 26251 RVA: 0x006E1420 File Offset: 0x006DF620
				public void SetDelay(float delay)
				{
				}
			}

			// Token: 0x02000E7A RID: 3706
			public class ForceAltTexture : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x170009C7 RID: 2503
				// (get) Token: 0x0600668D RID: 26253 RVA: 0x006E142A File Offset: 0x006DF62A
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x0600668E RID: 26254 RVA: 0x006E142D File Offset: 0x006DF62D
				public ForceAltTexture(int altTexture)
				{
					this._altTexture = altTexture;
				}

				// Token: 0x0600668F RID: 26255 RVA: 0x006E143C File Offset: 0x006DF63C
				public void BindTo(NPC obj)
				{
					obj.altTexture = this._altTexture;
				}

				// Token: 0x06006690 RID: 26256 RVA: 0x006E144A File Offset: 0x006DF64A
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
				}

				// Token: 0x06006691 RID: 26257 RVA: 0x006E144C File Offset: 0x006DF64C
				public void SetDelay(float delay)
				{
				}

				// Token: 0x04007D99 RID: 32153
				private int _altTexture;
			}

			// Token: 0x02000E7B RID: 3707
			public class Variant : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x170009C8 RID: 2504
				// (get) Token: 0x06006692 RID: 26258 RVA: 0x006E144E File Offset: 0x006DF64E
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x06006693 RID: 26259 RVA: 0x006E1451 File Offset: 0x006DF651
				public Variant(int variant)
				{
					this._variant = variant;
				}

				// Token: 0x06006694 RID: 26260 RVA: 0x006E1460 File Offset: 0x006DF660
				public void BindTo(NPC obj)
				{
					obj.townNpcVariationIndex = this._variant;
				}

				// Token: 0x06006695 RID: 26261 RVA: 0x006E146E File Offset: 0x006DF66E
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
				}

				// Token: 0x06006696 RID: 26262 RVA: 0x006E1470 File Offset: 0x006DF670
				public void SetDelay(float delay)
				{
				}

				// Token: 0x04007D9A RID: 32154
				private int _variant;
			}

			// Token: 0x02000E7C RID: 3708
			public class ZombieKnockOnDoor : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x170009C9 RID: 2505
				// (get) Token: 0x06006697 RID: 26263 RVA: 0x006E1472 File Offset: 0x006DF672
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06006698 RID: 26264 RVA: 0x006E147A File Offset: 0x006DF67A
				public ZombieKnockOnDoor(int durationInFrames)
				{
					this._duration = durationInFrames;
				}

				// Token: 0x06006699 RID: 26265 RVA: 0x006E14B3 File Offset: 0x006DF6B3
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x0600669A RID: 26266 RVA: 0x006E14B5 File Offset: 0x006DF6B5
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x0600669B RID: 26267 RVA: 0x006E14C0 File Offset: 0x006DF6C0
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
					if (localTimeForObj >= this._delay)
					{
						float num = localTimeForObj - this._delay;
						if (num > (float)this._duration)
						{
							num = (float)this._duration;
						}
						if ((int)num % 60 / 4 <= 3)
						{
							obj.position += this.bumpOffset;
							obj.velocity = this.bumpVelocity;
							return;
						}
						obj.position -= this.bumpOffset;
						obj.velocity = Vector2.Zero;
					}
				}

				// Token: 0x04007D9B RID: 32155
				private int _duration;

				// Token: 0x04007D9C RID: 32156
				private float _delay;

				// Token: 0x04007D9D RID: 32157
				private Vector2 bumpOffset = new Vector2(-1f, 0f);

				// Token: 0x04007D9E RID: 32158
				private Vector2 bumpVelocity = new Vector2(0.75f, 0f);
			}
		}

		// Token: 0x02000D3F RID: 3391
		public class Sprites
		{
			// Token: 0x02000E7D RID: 3709
			public interface ISpriteAction : IAnimationSegmentAction<Segments.LooseSprite>
			{
			}

			// Token: 0x02000E7E RID: 3710
			public class Fade : Actions.Sprites.ISpriteAction, IAnimationSegmentAction<Segments.LooseSprite>
			{
				// Token: 0x170009CA RID: 2506
				// (get) Token: 0x0600669C RID: 26268 RVA: 0x006E1541 File Offset: 0x006DF741
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x0600669D RID: 26269 RVA: 0x006E1549 File Offset: 0x006DF749
				public Fade(float opacityTarget)
				{
					this._duration = 0;
					this._opacityTarget = opacityTarget;
				}

				// Token: 0x0600669E RID: 26270 RVA: 0x006E155F File Offset: 0x006DF75F
				public Fade(float opacityTarget, int duration)
				{
					this._duration = duration;
					this._opacityTarget = opacityTarget;
				}

				// Token: 0x0600669F RID: 26271 RVA: 0x006E1575 File Offset: 0x006DF775
				public void BindTo(Segments.LooseSprite obj)
				{
				}

				// Token: 0x060066A0 RID: 26272 RVA: 0x006E1577 File Offset: 0x006DF777
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x060066A1 RID: 26273 RVA: 0x006E1580 File Offset: 0x006DF780
				public void ApplyTo(Segments.LooseSprite obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					if (this._duration == 0)
					{
						obj.CurrentOpacity = this._opacityTarget;
						return;
					}
					float num = localTimeForObj - this._delay;
					if (num > (float)this._duration)
					{
						num = (float)this._duration;
					}
					obj.CurrentOpacity = MathHelper.Lerp(obj.CurrentOpacity, this._opacityTarget, Utils.GetLerpValue(0f, (float)this._duration, num, true));
				}

				// Token: 0x04007D9F RID: 32159
				private int _duration;

				// Token: 0x04007DA0 RID: 32160
				private float _opacityTarget;

				// Token: 0x04007DA1 RID: 32161
				private float _delay;
			}

			// Token: 0x02000E7F RID: 3711
			public abstract class AScale : Actions.Sprites.ISpriteAction, IAnimationSegmentAction<Segments.LooseSprite>
			{
				// Token: 0x170009CB RID: 2507
				// (get) Token: 0x060066A2 RID: 26274 RVA: 0x006E15F1 File Offset: 0x006DF7F1
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this.Duration;
					}
				}

				// Token: 0x060066A3 RID: 26275 RVA: 0x006E15F9 File Offset: 0x006DF7F9
				public AScale(Vector2 scaleTarget)
				{
					this.Duration = 0;
					this._scaleTarget = scaleTarget;
				}

				// Token: 0x060066A4 RID: 26276 RVA: 0x006E160F File Offset: 0x006DF80F
				public AScale(Vector2 scaleTarget, int duration)
				{
					this.Duration = duration;
					this._scaleTarget = scaleTarget;
				}

				// Token: 0x060066A5 RID: 26277 RVA: 0x006E1625 File Offset: 0x006DF825
				public void BindTo(Segments.LooseSprite obj)
				{
				}

				// Token: 0x060066A6 RID: 26278 RVA: 0x006E1627 File Offset: 0x006DF827
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x060066A7 RID: 26279 RVA: 0x006E1630 File Offset: 0x006DF830
				public void ApplyTo(Segments.LooseSprite obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					if (this.Duration == 0)
					{
						obj.CurrentDrawData.scale = this._scaleTarget;
						return;
					}
					float num = localTimeForObj - this._delay;
					if (num > (float)this.Duration)
					{
						num = (float)this.Duration;
					}
					float progress = this.GetProgress(num);
					obj.CurrentDrawData.scale = Vector2.Lerp(obj.CurrentDrawData.scale, this._scaleTarget, progress);
				}

				// Token: 0x060066A8 RID: 26280
				protected abstract float GetProgress(float durationInFramesToApply);

				// Token: 0x04007DA2 RID: 32162
				protected int Duration;

				// Token: 0x04007DA3 RID: 32163
				private Vector2 _scaleTarget;

				// Token: 0x04007DA4 RID: 32164
				private float _delay;
			}

			// Token: 0x02000E80 RID: 3712
			public class LinearScale : Actions.Sprites.AScale
			{
				// Token: 0x060066A9 RID: 26281 RVA: 0x006E16A6 File Offset: 0x006DF8A6
				public LinearScale(Vector2 scaleTarget) : base(scaleTarget)
				{
				}

				// Token: 0x060066AA RID: 26282 RVA: 0x006E16AF File Offset: 0x006DF8AF
				public LinearScale(Vector2 scaleTarget, int duration) : base(scaleTarget, duration)
				{
				}

				// Token: 0x060066AB RID: 26283 RVA: 0x006E16B9 File Offset: 0x006DF8B9
				protected override float GetProgress(float durationInFramesToApply)
				{
					return Utils.GetLerpValue(0f, (float)this.Duration, durationInFramesToApply, true);
				}
			}

			// Token: 0x02000E81 RID: 3713
			public class OutCircleScale : Actions.Sprites.AScale
			{
				// Token: 0x060066AC RID: 26284 RVA: 0x006E16CE File Offset: 0x006DF8CE
				public OutCircleScale(Vector2 scaleTarget) : base(scaleTarget)
				{
				}

				// Token: 0x060066AD RID: 26285 RVA: 0x006E16D7 File Offset: 0x006DF8D7
				public OutCircleScale(Vector2 scaleTarget, int duration) : base(scaleTarget, duration)
				{
				}

				// Token: 0x060066AE RID: 26286 RVA: 0x006E16E4 File Offset: 0x006DF8E4
				protected override float GetProgress(float durationInFramesToApply)
				{
					float lerpValue = Utils.GetLerpValue(0f, (float)this.Duration, durationInFramesToApply, true);
					lerpValue -= 1f;
					return (float)Math.Sqrt((double)(1f - lerpValue * lerpValue));
				}
			}

			// Token: 0x02000E82 RID: 3714
			public class Wait : Actions.Sprites.ISpriteAction, IAnimationSegmentAction<Segments.LooseSprite>
			{
				// Token: 0x170009CC RID: 2508
				// (get) Token: 0x060066AF RID: 26287 RVA: 0x006E171D File Offset: 0x006DF91D
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x060066B0 RID: 26288 RVA: 0x006E1725 File Offset: 0x006DF925
				public Wait(int durationInFrames)
				{
					this._duration = durationInFrames;
				}

				// Token: 0x060066B1 RID: 26289 RVA: 0x006E1734 File Offset: 0x006DF934
				public void BindTo(Segments.LooseSprite obj)
				{
				}

				// Token: 0x060066B2 RID: 26290 RVA: 0x006E1736 File Offset: 0x006DF936
				public void ApplyTo(Segments.LooseSprite obj, float localTimeForObj)
				{
					float delay = this._delay;
				}

				// Token: 0x060066B3 RID: 26291 RVA: 0x006E173F File Offset: 0x006DF93F
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x04007DA5 RID: 32165
				private int _duration;

				// Token: 0x04007DA6 RID: 32166
				private float _delay;
			}

			// Token: 0x02000E83 RID: 3715
			public class SimulateGravity : Actions.Sprites.ISpriteAction, IAnimationSegmentAction<Segments.LooseSprite>
			{
				// Token: 0x170009CD RID: 2509
				// (get) Token: 0x060066B4 RID: 26292 RVA: 0x006E1748 File Offset: 0x006DF948
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x060066B5 RID: 26293 RVA: 0x006E1750 File Offset: 0x006DF950
				public SimulateGravity(Vector2 initialVelocity, Vector2 gravityPerFrame, float rotationPerFrame, int duration)
				{
					this._duration = duration;
					this._initialVelocity = initialVelocity;
					this._gravityPerFrame = gravityPerFrame;
					this._rotationPerFrame = rotationPerFrame;
				}

				// Token: 0x060066B6 RID: 26294 RVA: 0x006E1775 File Offset: 0x006DF975
				public void BindTo(Segments.LooseSprite obj)
				{
				}

				// Token: 0x060066B7 RID: 26295 RVA: 0x006E1777 File Offset: 0x006DF977
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x060066B8 RID: 26296 RVA: 0x006E1780 File Offset: 0x006DF980
				public void ApplyTo(Segments.LooseSprite obj, float localTimeForObj)
				{
					if (localTimeForObj >= this._delay)
					{
						float num = localTimeForObj - this._delay;
						if (num > (float)this._duration)
						{
							num = (float)this._duration;
						}
						Vector2 vector = this._initialVelocity * num + this._gravityPerFrame * (num * num);
						obj.CurrentDrawData.position = obj.CurrentDrawData.position + vector;
						obj.CurrentDrawData.rotation = obj.CurrentDrawData.rotation + this._rotationPerFrame * num;
					}
				}

				// Token: 0x04007DA7 RID: 32167
				private int _duration;

				// Token: 0x04007DA8 RID: 32168
				private float _delay;

				// Token: 0x04007DA9 RID: 32169
				private Vector2 _initialVelocity;

				// Token: 0x04007DAA RID: 32170
				private Vector2 _gravityPerFrame;

				// Token: 0x04007DAB RID: 32171
				private float _rotationPerFrame;
			}

			// Token: 0x02000E84 RID: 3716
			public class SetFrame : Actions.Sprites.ISpriteAction, IAnimationSegmentAction<Segments.LooseSprite>
			{
				// Token: 0x170009CE RID: 2510
				// (get) Token: 0x060066B9 RID: 26297 RVA: 0x006E1804 File Offset: 0x006DFA04
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x060066BA RID: 26298 RVA: 0x006E1807 File Offset: 0x006DFA07
				public SetFrame(int frameX, int frameY, int paddingX = 2, int paddingY = 2)
				{
					this._targetFrameX = frameX;
					this._targetFrameY = frameY;
					this._paddingX = paddingX;
					this._paddingY = paddingY;
				}

				// Token: 0x060066BB RID: 26299 RVA: 0x006E182C File Offset: 0x006DFA2C
				public void BindTo(Segments.LooseSprite obj)
				{
				}

				// Token: 0x060066BC RID: 26300 RVA: 0x006E182E File Offset: 0x006DFA2E
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x060066BD RID: 26301 RVA: 0x006E1838 File Offset: 0x006DFA38
				public void ApplyTo(Segments.LooseSprite obj, float localTimeForObj)
				{
					if (localTimeForObj >= this._delay)
					{
						Rectangle value = obj.CurrentDrawData.sourceRect.Value;
						value.X = (value.Width + this._paddingX) * this._targetFrameX;
						value.Y = (value.Height + this._paddingY) * this._targetFrameY;
						obj.CurrentDrawData.sourceRect = new Rectangle?(value);
					}
				}

				// Token: 0x04007DAC RID: 32172
				private int _targetFrameX;

				// Token: 0x04007DAD RID: 32173
				private int _targetFrameY;

				// Token: 0x04007DAE RID: 32174
				private int _paddingX;

				// Token: 0x04007DAF RID: 32175
				private int _paddingY;

				// Token: 0x04007DB0 RID: 32176
				private float _delay;
			}

			// Token: 0x02000E85 RID: 3717
			public class SetFrameSequence : Actions.Sprites.ISpriteAction, IAnimationSegmentAction<Segments.LooseSprite>
			{
				// Token: 0x170009CF RID: 2511
				// (get) Token: 0x060066BE RID: 26302 RVA: 0x006E18A6 File Offset: 0x006DFAA6
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x060066BF RID: 26303 RVA: 0x006E18AE File Offset: 0x006DFAAE
				public SetFrameSequence(int duration, Point[] frameIndices, int timePerFrame, int paddingX = 2, int paddingY = 2) : this(frameIndices, timePerFrame, paddingX, paddingY)
				{
					this._duration = duration;
					this._loop = true;
				}

				// Token: 0x060066C0 RID: 26304 RVA: 0x006E18CA File Offset: 0x006DFACA
				public SetFrameSequence(Point[] frameIndices, int timePerFrame, int paddingX = 2, int paddingY = 2)
				{
					this._frameIndices = frameIndices;
					this._timePerFrame = timePerFrame;
					this._paddingX = paddingX;
					this._paddingY = paddingY;
					this._duration = this._timePerFrame * this._frameIndices.Length;
				}

				// Token: 0x060066C1 RID: 26305 RVA: 0x006E1904 File Offset: 0x006DFB04
				public void BindTo(Segments.LooseSprite obj)
				{
				}

				// Token: 0x060066C2 RID: 26306 RVA: 0x006E1906 File Offset: 0x006DFB06
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x060066C3 RID: 26307 RVA: 0x006E1910 File Offset: 0x006DFB10
				public void ApplyTo(Segments.LooseSprite obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					Rectangle value = obj.CurrentDrawData.sourceRect.Value;
					int num3;
					if (this._loop)
					{
						int num2 = this._frameIndices.Length;
						num3 = (int)(localTimeForObj % (float)(this._timePerFrame * num2)) / this._timePerFrame;
						if (num3 >= num2)
						{
							num3 = num2 - 1;
						}
					}
					else
					{
						float num4 = localTimeForObj - this._delay;
						if (num4 > (float)this._duration)
						{
							num4 = (float)this._duration;
						}
						num3 = (int)(num4 / (float)this._timePerFrame);
						if (num3 >= this._frameIndices.Length)
						{
							num3 = this._frameIndices.Length - 1;
						}
					}
					Point point = this._frameIndices[num3];
					value.X = (value.Width + this._paddingX) * point.X;
					value.Y = (value.Height + this._paddingY) * point.Y;
					obj.CurrentDrawData.sourceRect = new Rectangle?(value);
				}

				// Token: 0x04007DB1 RID: 32177
				private Point[] _frameIndices;

				// Token: 0x04007DB2 RID: 32178
				private int _timePerFrame;

				// Token: 0x04007DB3 RID: 32179
				private int _paddingX;

				// Token: 0x04007DB4 RID: 32180
				private int _paddingY;

				// Token: 0x04007DB5 RID: 32181
				private float _delay;

				// Token: 0x04007DB6 RID: 32182
				private int _duration;

				// Token: 0x04007DB7 RID: 32183
				private bool _loop;
			}
		}
	}
}
