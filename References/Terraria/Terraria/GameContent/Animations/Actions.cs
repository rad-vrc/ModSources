using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.Animations
{
	// Token: 0x020003ED RID: 1005
	public class Actions
	{
		// Token: 0x02000773 RID: 1907
		public class Players
		{
			// Token: 0x0200084C RID: 2124
			public interface IPlayerAction : IAnimationSegmentAction<Player>
			{
			}

			// Token: 0x0200084D RID: 2125
			public class Fade : Actions.Players.IPlayerAction, IAnimationSegmentAction<Player>
			{
				// Token: 0x06003AD9 RID: 15065 RVA: 0x0061A45A File Offset: 0x0061865A
				public Fade(float opacityTarget)
				{
					this._duration = 0;
					this._opacityTarget = opacityTarget;
				}

				// Token: 0x06003ADA RID: 15066 RVA: 0x0061A470 File Offset: 0x00618670
				public Fade(float opacityTarget, int duration)
				{
					this._duration = duration;
					this._opacityTarget = opacityTarget;
				}

				// Token: 0x06003ADB RID: 15067 RVA: 0x0003C3EC File Offset: 0x0003A5EC
				public void BindTo(Player obj)
				{
				}

				// Token: 0x1700041D RID: 1053
				// (get) Token: 0x06003ADC RID: 15068 RVA: 0x0061A486 File Offset: 0x00618686
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06003ADD RID: 15069 RVA: 0x0061A48E File Offset: 0x0061868E
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06003ADE RID: 15070 RVA: 0x0061A498 File Offset: 0x00618698
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

				// Token: 0x040065F7 RID: 26103
				private int _duration;

				// Token: 0x040065F8 RID: 26104
				private float _opacityTarget;

				// Token: 0x040065F9 RID: 26105
				private float _delay;
			}

			// Token: 0x0200084E RID: 2126
			public class Wait : Actions.Players.IPlayerAction, IAnimationSegmentAction<Player>
			{
				// Token: 0x06003ADF RID: 15071 RVA: 0x0061A509 File Offset: 0x00618709
				public Wait(int durationInFrames)
				{
					this._duration = durationInFrames;
				}

				// Token: 0x06003AE0 RID: 15072 RVA: 0x0003C3EC File Offset: 0x0003A5EC
				public void BindTo(Player obj)
				{
				}

				// Token: 0x1700041E RID: 1054
				// (get) Token: 0x06003AE1 RID: 15073 RVA: 0x0061A518 File Offset: 0x00618718
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06003AE2 RID: 15074 RVA: 0x0061A520 File Offset: 0x00618720
				public void ApplyTo(Player obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					obj.velocity = Vector2.Zero;
				}

				// Token: 0x06003AE3 RID: 15075 RVA: 0x0061A537 File Offset: 0x00618737
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x040065FA RID: 26106
				private int _duration;

				// Token: 0x040065FB RID: 26107
				private float _delay;
			}

			// Token: 0x0200084F RID: 2127
			public class LookAt : Actions.Players.IPlayerAction, IAnimationSegmentAction<Player>
			{
				// Token: 0x06003AE4 RID: 15076 RVA: 0x0061A540 File Offset: 0x00618740
				public LookAt(int direction)
				{
					this._direction = direction;
				}

				// Token: 0x06003AE5 RID: 15077 RVA: 0x0003C3EC File Offset: 0x0003A5EC
				public void BindTo(Player obj)
				{
				}

				// Token: 0x1700041F RID: 1055
				// (get) Token: 0x06003AE6 RID: 15078 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x06003AE7 RID: 15079 RVA: 0x0061A54F File Offset: 0x0061874F
				public void ApplyTo(Player obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					obj.direction = this._direction;
				}

				// Token: 0x06003AE8 RID: 15080 RVA: 0x0061A567 File Offset: 0x00618767
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x040065FC RID: 26108
				private int _direction;

				// Token: 0x040065FD RID: 26109
				private float _delay;
			}

			// Token: 0x02000850 RID: 2128
			public class MoveWithAcceleration : Actions.Players.IPlayerAction, IAnimationSegmentAction<Player>
			{
				// Token: 0x06003AE9 RID: 15081 RVA: 0x0061A570 File Offset: 0x00618770
				public MoveWithAcceleration(Vector2 offsetPerFrame, Vector2 accelerationPerFrame, int durationInFrames)
				{
					this._accelerationPerFrame = accelerationPerFrame;
					this._offsetPerFrame = offsetPerFrame;
					this._duration = durationInFrames;
				}

				// Token: 0x06003AEA RID: 15082 RVA: 0x0003C3EC File Offset: 0x0003A5EC
				public void BindTo(Player obj)
				{
				}

				// Token: 0x17000420 RID: 1056
				// (get) Token: 0x06003AEB RID: 15083 RVA: 0x0061A58D File Offset: 0x0061878D
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06003AEC RID: 15084 RVA: 0x0061A595 File Offset: 0x00618795
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06003AED RID: 15085 RVA: 0x0061A5A0 File Offset: 0x006187A0
				public void ApplyTo(Player obj, float localTimeForObj)
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
					Vector2 value = this._offsetPerFrame * num + this._accelerationPerFrame * (num * num * 0.5f);
					obj.position += value;
					obj.velocity = this._offsetPerFrame + this._accelerationPerFrame * num;
					if (this._offsetPerFrame.X != 0f)
					{
						obj.direction = ((this._offsetPerFrame.X > 0f) ? 1 : -1);
					}
				}

				// Token: 0x040065FE RID: 26110
				private Vector2 _offsetPerFrame;

				// Token: 0x040065FF RID: 26111
				private Vector2 _accelerationPerFrame;

				// Token: 0x04006600 RID: 26112
				private int _duration;

				// Token: 0x04006601 RID: 26113
				private float _delay;
			}
		}

		// Token: 0x02000774 RID: 1908
		public class NPCs
		{
			// Token: 0x02000851 RID: 2129
			public interface INPCAction : IAnimationSegmentAction<NPC>
			{
			}

			// Token: 0x02000852 RID: 2130
			public class Fade : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x06003AEE RID: 15086 RVA: 0x0061A655 File Offset: 0x00618855
				public Fade(int alphaPerFrame)
				{
					this._duration = 0;
					this._alphaPerFrame = alphaPerFrame;
				}

				// Token: 0x06003AEF RID: 15087 RVA: 0x0061A66B File Offset: 0x0061886B
				public Fade(int alphaPerFrame, int duration)
				{
					this._duration = duration;
					this._alphaPerFrame = alphaPerFrame;
				}

				// Token: 0x06003AF0 RID: 15088 RVA: 0x0003C3EC File Offset: 0x0003A5EC
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x17000421 RID: 1057
				// (get) Token: 0x06003AF1 RID: 15089 RVA: 0x0061A681 File Offset: 0x00618881
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06003AF2 RID: 15090 RVA: 0x0061A689 File Offset: 0x00618889
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06003AF3 RID: 15091 RVA: 0x0061A694 File Offset: 0x00618894
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

				// Token: 0x04006602 RID: 26114
				private int _duration;

				// Token: 0x04006603 RID: 26115
				private int _alphaPerFrame;

				// Token: 0x04006604 RID: 26116
				private float _delay;
			}

			// Token: 0x02000853 RID: 2131
			public class ShowItem : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x06003AF4 RID: 15092 RVA: 0x0061A70E File Offset: 0x0061890E
				public ShowItem(int durationInFrames, int itemIdToShow)
				{
					this._duration = durationInFrames;
					this._itemIdToShow = itemIdToShow;
				}

				// Token: 0x06003AF5 RID: 15093 RVA: 0x0003C3EC File Offset: 0x0003A5EC
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x17000422 RID: 1058
				// (get) Token: 0x06003AF6 RID: 15094 RVA: 0x0061A724 File Offset: 0x00618924
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06003AF7 RID: 15095 RVA: 0x0061A72C File Offset: 0x0061892C
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06003AF8 RID: 15096 RVA: 0x0061A738 File Offset: 0x00618938
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
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

				// Token: 0x06003AF9 RID: 15097 RVA: 0x0061A7AC File Offset: 0x006189AC
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

				// Token: 0x04006605 RID: 26117
				private int _itemIdToShow;

				// Token: 0x04006606 RID: 26118
				private int _duration;

				// Token: 0x04006607 RID: 26119
				private float _delay;
			}

			// Token: 0x02000854 RID: 2132
			public class Move : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x06003AFA RID: 15098 RVA: 0x0061A7FE File Offset: 0x006189FE
				public Move(Vector2 offsetPerFrame, int durationInFrames)
				{
					this._offsetPerFrame = offsetPerFrame;
					this._duration = durationInFrames;
				}

				// Token: 0x06003AFB RID: 15099 RVA: 0x0003C3EC File Offset: 0x0003A5EC
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x17000423 RID: 1059
				// (get) Token: 0x06003AFC RID: 15100 RVA: 0x0061A814 File Offset: 0x00618A14
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06003AFD RID: 15101 RVA: 0x0061A81C File Offset: 0x00618A1C
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06003AFE RID: 15102 RVA: 0x0061A828 File Offset: 0x00618A28
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
					obj.position += this._offsetPerFrame * num;
					obj.velocity = this._offsetPerFrame;
					if (this._offsetPerFrame.X != 0f)
					{
						obj.direction = (obj.spriteDirection = ((this._offsetPerFrame.X > 0f) ? 1 : -1));
					}
				}

				// Token: 0x04006608 RID: 26120
				private Vector2 _offsetPerFrame;

				// Token: 0x04006609 RID: 26121
				private int _duration;

				// Token: 0x0400660A RID: 26122
				private float _delay;
			}

			// Token: 0x02000855 RID: 2133
			public class MoveWithAcceleration : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x06003AFF RID: 15103 RVA: 0x0061A8BA File Offset: 0x00618ABA
				public MoveWithAcceleration(Vector2 offsetPerFrame, Vector2 accelerationPerFrame, int durationInFrames)
				{
					this._accelerationPerFrame = accelerationPerFrame;
					this._offsetPerFrame = offsetPerFrame;
					this._duration = durationInFrames;
				}

				// Token: 0x06003B00 RID: 15104 RVA: 0x0003C3EC File Offset: 0x0003A5EC
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x17000424 RID: 1060
				// (get) Token: 0x06003B01 RID: 15105 RVA: 0x0061A8D7 File Offset: 0x00618AD7
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06003B02 RID: 15106 RVA: 0x0061A8DF File Offset: 0x00618ADF
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06003B03 RID: 15107 RVA: 0x0061A8E8 File Offset: 0x00618AE8
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
					Vector2 value = this._offsetPerFrame * num + this._accelerationPerFrame * (num * num * 0.5f);
					obj.position += value;
					obj.velocity = this._offsetPerFrame + this._accelerationPerFrame * num;
					if (this._offsetPerFrame.X != 0f)
					{
						obj.direction = (obj.spriteDirection = ((this._offsetPerFrame.X > 0f) ? 1 : -1));
					}
				}

				// Token: 0x0400660B RID: 26123
				private Vector2 _offsetPerFrame;

				// Token: 0x0400660C RID: 26124
				private Vector2 _accelerationPerFrame;

				// Token: 0x0400660D RID: 26125
				private int _duration;

				// Token: 0x0400660E RID: 26126
				private float _delay;
			}

			// Token: 0x02000856 RID: 2134
			public class MoveWithRotor : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x06003B04 RID: 15108 RVA: 0x0061A9A6 File Offset: 0x00618BA6
				public MoveWithRotor(Vector2 radialOffset, float rotationPerFrame, Vector2 resultMultiplier, int durationInFrames)
				{
					this._radialOffset = rotationPerFrame;
					this._offsetPerFrame = radialOffset;
					this._resultMultiplier = resultMultiplier;
					this._duration = durationInFrames;
				}

				// Token: 0x06003B05 RID: 15109 RVA: 0x0003C3EC File Offset: 0x0003A5EC
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x17000425 RID: 1061
				// (get) Token: 0x06003B06 RID: 15110 RVA: 0x0061A9CB File Offset: 0x00618BCB
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06003B07 RID: 15111 RVA: 0x0061A9D3 File Offset: 0x00618BD3
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06003B08 RID: 15112 RVA: 0x0061A9DC File Offset: 0x00618BDC
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
					Vector2 value = this._offsetPerFrame.RotatedBy((double)(this._radialOffset * num), default(Vector2)) * this._resultMultiplier;
					obj.position += value;
				}

				// Token: 0x0400660F RID: 26127
				private Vector2 _offsetPerFrame;

				// Token: 0x04006610 RID: 26128
				private Vector2 _resultMultiplier;

				// Token: 0x04006611 RID: 26129
				private float _radialOffset;

				// Token: 0x04006612 RID: 26130
				private int _duration;

				// Token: 0x04006613 RID: 26131
				private float _delay;
			}

			// Token: 0x02000857 RID: 2135
			public class DoBunnyRestAnimation : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x06003B09 RID: 15113 RVA: 0x0061AA49 File Offset: 0x00618C49
				public DoBunnyRestAnimation(int durationInFrames)
				{
					this._duration = durationInFrames;
				}

				// Token: 0x06003B0A RID: 15114 RVA: 0x0003C3EC File Offset: 0x0003A5EC
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x17000426 RID: 1062
				// (get) Token: 0x06003B0B RID: 15115 RVA: 0x0061AA58 File Offset: 0x00618C58
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06003B0C RID: 15116 RVA: 0x0061AA60 File Offset: 0x00618C60
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06003B0D RID: 15117 RVA: 0x0061AA6C File Offset: 0x00618C6C
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
					int i = (int)num;
					while (i > 4)
					{
						i -= 4;
						num2++;
						if (num2 > 13)
						{
							num2 = 13;
						}
					}
					obj.ai[0] = 21f;
					obj.ai[1] = 31f;
					obj.frameCounter = (double)i;
					obj.frame.Y = num2 * frame.Height;
				}

				// Token: 0x04006614 RID: 26132
				private int _duration;

				// Token: 0x04006615 RID: 26133
				private float _delay;
			}

			// Token: 0x02000858 RID: 2136
			public class Wait : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x06003B0E RID: 15118 RVA: 0x0061AAF6 File Offset: 0x00618CF6
				public Wait(int durationInFrames)
				{
					this._duration = durationInFrames;
				}

				// Token: 0x06003B0F RID: 15119 RVA: 0x0003C3EC File Offset: 0x0003A5EC
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x17000427 RID: 1063
				// (get) Token: 0x06003B10 RID: 15120 RVA: 0x0061AB05 File Offset: 0x00618D05
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06003B11 RID: 15121 RVA: 0x0061AB0D File Offset: 0x00618D0D
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					obj.velocity = Vector2.Zero;
				}

				// Token: 0x06003B12 RID: 15122 RVA: 0x0061AB24 File Offset: 0x00618D24
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x04006616 RID: 26134
				private int _duration;

				// Token: 0x04006617 RID: 26135
				private float _delay;
			}

			// Token: 0x02000859 RID: 2137
			public class Blink : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x06003B13 RID: 15123 RVA: 0x0061AB2D File Offset: 0x00618D2D
				public Blink(int durationInFrames)
				{
					this._duration = durationInFrames;
				}

				// Token: 0x06003B14 RID: 15124 RVA: 0x0003C3EC File Offset: 0x0003A5EC
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x17000428 RID: 1064
				// (get) Token: 0x06003B15 RID: 15125 RVA: 0x0061AB3C File Offset: 0x00618D3C
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06003B16 RID: 15126 RVA: 0x0061AB44 File Offset: 0x00618D44
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					obj.velocity = Vector2.Zero;
					obj.ai[0] = 0f;
					if (localTimeForObj > this._delay + (float)this._duration)
					{
						return;
					}
					obj.ai[0] = 1001f;
				}

				// Token: 0x06003B17 RID: 15127 RVA: 0x0061AB92 File Offset: 0x00618D92
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x04006618 RID: 26136
				private int _duration;

				// Token: 0x04006619 RID: 26137
				private float _delay;
			}

			// Token: 0x0200085A RID: 2138
			public class LookAt : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x06003B18 RID: 15128 RVA: 0x0061AB9B File Offset: 0x00618D9B
				public LookAt(int direction)
				{
					this._direction = direction;
				}

				// Token: 0x06003B19 RID: 15129 RVA: 0x0003C3EC File Offset: 0x0003A5EC
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x17000429 RID: 1065
				// (get) Token: 0x06003B1A RID: 15130 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x06003B1B RID: 15131 RVA: 0x0061ABAC File Offset: 0x00618DAC
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					obj.direction = (obj.spriteDirection = this._direction);
				}

				// Token: 0x06003B1C RID: 15132 RVA: 0x0061ABD8 File Offset: 0x00618DD8
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x0400661A RID: 26138
				private int _direction;

				// Token: 0x0400661B RID: 26139
				private float _delay;
			}

			// Token: 0x0200085B RID: 2139
			public class PartyHard : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x06003B1E RID: 15134 RVA: 0x0061ABE1 File Offset: 0x00618DE1
				public void BindTo(NPC obj)
				{
					obj.ForcePartyHatOn = true;
					obj.UpdateAltTexture();
				}

				// Token: 0x1700042A RID: 1066
				// (get) Token: 0x06003B1F RID: 15135 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x06003B20 RID: 15136 RVA: 0x0003C3EC File Offset: 0x0003A5EC
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
				}

				// Token: 0x06003B21 RID: 15137 RVA: 0x0003C3EC File Offset: 0x0003A5EC
				public void SetDelay(float delay)
				{
				}
			}

			// Token: 0x0200085C RID: 2140
			public class ForceAltTexture : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x06003B22 RID: 15138 RVA: 0x0061ABF0 File Offset: 0x00618DF0
				public ForceAltTexture(int altTexture)
				{
					this._altTexture = altTexture;
				}

				// Token: 0x06003B23 RID: 15139 RVA: 0x0061ABFF File Offset: 0x00618DFF
				public void BindTo(NPC obj)
				{
					obj.altTexture = this._altTexture;
				}

				// Token: 0x1700042B RID: 1067
				// (get) Token: 0x06003B24 RID: 15140 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x06003B25 RID: 15141 RVA: 0x0003C3EC File Offset: 0x0003A5EC
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
				}

				// Token: 0x06003B26 RID: 15142 RVA: 0x0003C3EC File Offset: 0x0003A5EC
				public void SetDelay(float delay)
				{
				}

				// Token: 0x0400661C RID: 26140
				private int _altTexture;
			}

			// Token: 0x0200085D RID: 2141
			public class Variant : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x06003B27 RID: 15143 RVA: 0x0061AC0D File Offset: 0x00618E0D
				public Variant(int variant)
				{
					this._variant = variant;
				}

				// Token: 0x06003B28 RID: 15144 RVA: 0x0061AC1C File Offset: 0x00618E1C
				public void BindTo(NPC obj)
				{
					obj.townNpcVariationIndex = this._variant;
				}

				// Token: 0x1700042C RID: 1068
				// (get) Token: 0x06003B29 RID: 15145 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x06003B2A RID: 15146 RVA: 0x0003C3EC File Offset: 0x0003A5EC
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
				}

				// Token: 0x06003B2B RID: 15147 RVA: 0x0003C3EC File Offset: 0x0003A5EC
				public void SetDelay(float delay)
				{
				}

				// Token: 0x0400661D RID: 26141
				private int _variant;
			}

			// Token: 0x0200085E RID: 2142
			public class ZombieKnockOnDoor : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x06003B2C RID: 15148 RVA: 0x0061AC2A File Offset: 0x00618E2A
				public ZombieKnockOnDoor(int durationInFrames)
				{
					this._duration = durationInFrames;
				}

				// Token: 0x06003B2D RID: 15149 RVA: 0x0003C3EC File Offset: 0x0003A5EC
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x1700042D RID: 1069
				// (get) Token: 0x06003B2E RID: 15150 RVA: 0x0061AC63 File Offset: 0x00618E63
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06003B2F RID: 15151 RVA: 0x0061AC6B File Offset: 0x00618E6B
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06003B30 RID: 15152 RVA: 0x0061AC74 File Offset: 0x00618E74
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
					if ((int)num % 60 / 4 <= 3)
					{
						obj.position += this.bumpOffset;
						obj.velocity = this.bumpVelocity;
						return;
					}
					obj.position -= this.bumpOffset;
					obj.velocity = Vector2.Zero;
				}

				// Token: 0x0400661E RID: 26142
				private int _duration;

				// Token: 0x0400661F RID: 26143
				private float _delay;

				// Token: 0x04006620 RID: 26144
				private Vector2 bumpOffset = new Vector2(-1f, 0f);

				// Token: 0x04006621 RID: 26145
				private Vector2 bumpVelocity = new Vector2(0.75f, 0f);
			}
		}

		// Token: 0x02000775 RID: 1909
		public class Sprites
		{
			// Token: 0x0200085F RID: 2143
			public interface ISpriteAction : IAnimationSegmentAction<Segments.LooseSprite>
			{
			}

			// Token: 0x02000860 RID: 2144
			public class Fade : Actions.Sprites.ISpriteAction, IAnimationSegmentAction<Segments.LooseSprite>
			{
				// Token: 0x06003B31 RID: 15153 RVA: 0x0061ACF6 File Offset: 0x00618EF6
				public Fade(float opacityTarget)
				{
					this._duration = 0;
					this._opacityTarget = opacityTarget;
				}

				// Token: 0x06003B32 RID: 15154 RVA: 0x0061AD0C File Offset: 0x00618F0C
				public Fade(float opacityTarget, int duration)
				{
					this._duration = duration;
					this._opacityTarget = opacityTarget;
				}

				// Token: 0x06003B33 RID: 15155 RVA: 0x0003C3EC File Offset: 0x0003A5EC
				public void BindTo(Segments.LooseSprite obj)
				{
				}

				// Token: 0x1700042E RID: 1070
				// (get) Token: 0x06003B34 RID: 15156 RVA: 0x0061AD22 File Offset: 0x00618F22
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06003B35 RID: 15157 RVA: 0x0061AD2A File Offset: 0x00618F2A
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06003B36 RID: 15158 RVA: 0x0061AD34 File Offset: 0x00618F34
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

				// Token: 0x04006622 RID: 26146
				private int _duration;

				// Token: 0x04006623 RID: 26147
				private float _opacityTarget;

				// Token: 0x04006624 RID: 26148
				private float _delay;
			}

			// Token: 0x02000861 RID: 2145
			public abstract class AScale : Actions.Sprites.ISpriteAction, IAnimationSegmentAction<Segments.LooseSprite>
			{
				// Token: 0x06003B37 RID: 15159 RVA: 0x0061ADA5 File Offset: 0x00618FA5
				public AScale(Vector2 scaleTarget)
				{
					this.Duration = 0;
					this._scaleTarget = scaleTarget;
				}

				// Token: 0x06003B38 RID: 15160 RVA: 0x0061ADBB File Offset: 0x00618FBB
				public AScale(Vector2 scaleTarget, int duration)
				{
					this.Duration = duration;
					this._scaleTarget = scaleTarget;
				}

				// Token: 0x06003B39 RID: 15161 RVA: 0x0003C3EC File Offset: 0x0003A5EC
				public void BindTo(Segments.LooseSprite obj)
				{
				}

				// Token: 0x1700042F RID: 1071
				// (get) Token: 0x06003B3A RID: 15162 RVA: 0x0061ADD1 File Offset: 0x00618FD1
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this.Duration;
					}
				}

				// Token: 0x06003B3B RID: 15163 RVA: 0x0061ADD9 File Offset: 0x00618FD9
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06003B3C RID: 15164 RVA: 0x0061ADE4 File Offset: 0x00618FE4
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

				// Token: 0x06003B3D RID: 15165
				protected abstract float GetProgress(float durationInFramesToApply);

				// Token: 0x04006625 RID: 26149
				protected int Duration;

				// Token: 0x04006626 RID: 26150
				private Vector2 _scaleTarget;

				// Token: 0x04006627 RID: 26151
				private float _delay;
			}

			// Token: 0x02000862 RID: 2146
			public class LinearScale : Actions.Sprites.AScale
			{
				// Token: 0x06003B3E RID: 15166 RVA: 0x0061AE5A File Offset: 0x0061905A
				public LinearScale(Vector2 scaleTarget) : base(scaleTarget)
				{
				}

				// Token: 0x06003B3F RID: 15167 RVA: 0x0061AE63 File Offset: 0x00619063
				public LinearScale(Vector2 scaleTarget, int duration) : base(scaleTarget, duration)
				{
				}

				// Token: 0x06003B40 RID: 15168 RVA: 0x0061AE6D File Offset: 0x0061906D
				protected override float GetProgress(float durationInFramesToApply)
				{
					return Utils.GetLerpValue(0f, (float)this.Duration, durationInFramesToApply, true);
				}
			}

			// Token: 0x02000863 RID: 2147
			public class OutCircleScale : Actions.Sprites.AScale
			{
				// Token: 0x06003B41 RID: 15169 RVA: 0x0061AE5A File Offset: 0x0061905A
				public OutCircleScale(Vector2 scaleTarget) : base(scaleTarget)
				{
				}

				// Token: 0x06003B42 RID: 15170 RVA: 0x0061AE63 File Offset: 0x00619063
				public OutCircleScale(Vector2 scaleTarget, int duration) : base(scaleTarget, duration)
				{
				}

				// Token: 0x06003B43 RID: 15171 RVA: 0x0061AE84 File Offset: 0x00619084
				protected override float GetProgress(float durationInFramesToApply)
				{
					float num = Utils.GetLerpValue(0f, (float)this.Duration, durationInFramesToApply, true);
					num -= 1f;
					return (float)Math.Sqrt((double)(1f - num * num));
				}
			}

			// Token: 0x02000864 RID: 2148
			public class Wait : Actions.Sprites.ISpriteAction, IAnimationSegmentAction<Segments.LooseSprite>
			{
				// Token: 0x06003B44 RID: 15172 RVA: 0x0061AEBD File Offset: 0x006190BD
				public Wait(int durationInFrames)
				{
					this._duration = durationInFrames;
				}

				// Token: 0x06003B45 RID: 15173 RVA: 0x0003C3EC File Offset: 0x0003A5EC
				public void BindTo(Segments.LooseSprite obj)
				{
				}

				// Token: 0x17000430 RID: 1072
				// (get) Token: 0x06003B46 RID: 15174 RVA: 0x0061AECC File Offset: 0x006190CC
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06003B47 RID: 15175 RVA: 0x0061AED4 File Offset: 0x006190D4
				public void ApplyTo(Segments.LooseSprite obj, float localTimeForObj)
				{
					float delay = this._delay;
				}

				// Token: 0x06003B48 RID: 15176 RVA: 0x0061AEDF File Offset: 0x006190DF
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x04006628 RID: 26152
				private int _duration;

				// Token: 0x04006629 RID: 26153
				private float _delay;
			}

			// Token: 0x02000865 RID: 2149
			public class SimulateGravity : Actions.Sprites.ISpriteAction, IAnimationSegmentAction<Segments.LooseSprite>
			{
				// Token: 0x06003B49 RID: 15177 RVA: 0x0061AEE8 File Offset: 0x006190E8
				public SimulateGravity(Vector2 initialVelocity, Vector2 gravityPerFrame, float rotationPerFrame, int duration)
				{
					this._duration = duration;
					this._initialVelocity = initialVelocity;
					this._gravityPerFrame = gravityPerFrame;
					this._rotationPerFrame = rotationPerFrame;
				}

				// Token: 0x06003B4A RID: 15178 RVA: 0x0003C3EC File Offset: 0x0003A5EC
				public void BindTo(Segments.LooseSprite obj)
				{
				}

				// Token: 0x17000431 RID: 1073
				// (get) Token: 0x06003B4B RID: 15179 RVA: 0x0061AF0D File Offset: 0x0061910D
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06003B4C RID: 15180 RVA: 0x0061AF15 File Offset: 0x00619115
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06003B4D RID: 15181 RVA: 0x0061AF20 File Offset: 0x00619120
				public void ApplyTo(Segments.LooseSprite obj, float localTimeForObj)
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
					Vector2 value = this._initialVelocity * num + this._gravityPerFrame * (num * num);
					obj.CurrentDrawData.position = obj.CurrentDrawData.position + value;
					obj.CurrentDrawData.rotation = obj.CurrentDrawData.rotation + this._rotationPerFrame * num;
				}

				// Token: 0x0400662A RID: 26154
				private int _duration;

				// Token: 0x0400662B RID: 26155
				private float _delay;

				// Token: 0x0400662C RID: 26156
				private Vector2 _initialVelocity;

				// Token: 0x0400662D RID: 26157
				private Vector2 _gravityPerFrame;

				// Token: 0x0400662E RID: 26158
				private float _rotationPerFrame;
			}

			// Token: 0x02000866 RID: 2150
			public class SetFrame : Actions.Sprites.ISpriteAction, IAnimationSegmentAction<Segments.LooseSprite>
			{
				// Token: 0x06003B4E RID: 15182 RVA: 0x0061AFA5 File Offset: 0x006191A5
				public SetFrame(int frameX, int frameY, int paddingX = 2, int paddingY = 2)
				{
					this._targetFrameX = frameX;
					this._targetFrameY = frameY;
					this._paddingX = paddingX;
					this._paddingY = paddingY;
				}

				// Token: 0x06003B4F RID: 15183 RVA: 0x0003C3EC File Offset: 0x0003A5EC
				public void BindTo(Segments.LooseSprite obj)
				{
				}

				// Token: 0x17000432 RID: 1074
				// (get) Token: 0x06003B50 RID: 15184 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x06003B51 RID: 15185 RVA: 0x0061AFCA File Offset: 0x006191CA
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06003B52 RID: 15186 RVA: 0x0061AFD4 File Offset: 0x006191D4
				public void ApplyTo(Segments.LooseSprite obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					Rectangle value = obj.CurrentDrawData.sourceRect.Value;
					value.X = (value.Width + this._paddingX) * this._targetFrameX;
					value.Y = (value.Height + this._paddingY) * this._targetFrameY;
					obj.CurrentDrawData.sourceRect = new Rectangle?(value);
				}

				// Token: 0x0400662F RID: 26159
				private int _targetFrameX;

				// Token: 0x04006630 RID: 26160
				private int _targetFrameY;

				// Token: 0x04006631 RID: 26161
				private int _paddingX;

				// Token: 0x04006632 RID: 26162
				private int _paddingY;

				// Token: 0x04006633 RID: 26163
				private float _delay;
			}

			// Token: 0x02000867 RID: 2151
			public class SetFrameSequence : Actions.Sprites.ISpriteAction, IAnimationSegmentAction<Segments.LooseSprite>
			{
				// Token: 0x06003B53 RID: 15187 RVA: 0x0061B043 File Offset: 0x00619243
				public SetFrameSequence(int duration, Point[] frameIndices, int timePerFrame, int paddingX = 2, int paddingY = 2) : this(frameIndices, timePerFrame, paddingX, paddingY)
				{
					this._duration = duration;
					this._loop = true;
				}

				// Token: 0x06003B54 RID: 15188 RVA: 0x0061B05F File Offset: 0x0061925F
				public SetFrameSequence(Point[] frameIndices, int timePerFrame, int paddingX = 2, int paddingY = 2)
				{
					this._frameIndices = frameIndices;
					this._timePerFrame = timePerFrame;
					this._paddingX = paddingX;
					this._paddingY = paddingY;
					this._duration = this._timePerFrame * this._frameIndices.Length;
				}

				// Token: 0x06003B55 RID: 15189 RVA: 0x0003C3EC File Offset: 0x0003A5EC
				public void BindTo(Segments.LooseSprite obj)
				{
				}

				// Token: 0x17000433 RID: 1075
				// (get) Token: 0x06003B56 RID: 15190 RVA: 0x0061B099 File Offset: 0x00619299
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06003B57 RID: 15191 RVA: 0x0061B0A1 File Offset: 0x006192A1
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06003B58 RID: 15192 RVA: 0x0061B0AC File Offset: 0x006192AC
				public void ApplyTo(Segments.LooseSprite obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					Rectangle value = obj.CurrentDrawData.sourceRect.Value;
					int num2;
					if (this._loop)
					{
						int num = this._frameIndices.Length;
						num2 = (int)(localTimeForObj % (float)(this._timePerFrame * num)) / this._timePerFrame;
						if (num2 >= num)
						{
							num2 = num - 1;
						}
					}
					else
					{
						float num3 = localTimeForObj - this._delay;
						if (num3 > (float)this._duration)
						{
							num3 = (float)this._duration;
						}
						num2 = (int)(num3 / (float)this._timePerFrame);
						if (num2 >= this._frameIndices.Length)
						{
							num2 = this._frameIndices.Length - 1;
						}
					}
					Point point = this._frameIndices[num2];
					value.X = (value.Width + this._paddingX) * point.X;
					value.Y = (value.Height + this._paddingY) * point.Y;
					obj.CurrentDrawData.sourceRect = new Rectangle?(value);
				}

				// Token: 0x04006634 RID: 26164
				private Point[] _frameIndices;

				// Token: 0x04006635 RID: 26165
				private int _timePerFrame;

				// Token: 0x04006636 RID: 26166
				private int _paddingX;

				// Token: 0x04006637 RID: 26167
				private int _paddingY;

				// Token: 0x04006638 RID: 26168
				private float _delay;

				// Token: 0x04006639 RID: 26169
				private int _duration;

				// Token: 0x0400663A RID: 26170
				private bool _loop;
			}
		}
	}
}
