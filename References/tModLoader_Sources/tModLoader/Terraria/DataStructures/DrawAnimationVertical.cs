using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.DataStructures
{
	// Token: 0x020006DC RID: 1756
	public class DrawAnimationVertical : DrawAnimation
	{
		// Token: 0x0600492E RID: 18734 RVA: 0x0064D00E File Offset: 0x0064B20E
		public DrawAnimationVertical(int ticksperframe, int frameCount, bool pingPong = false)
		{
			this.Frame = 0;
			this.FrameCounter = 0;
			this.FrameCount = frameCount;
			this.TicksPerFrame = ticksperframe;
			this.PingPong = pingPong;
		}

		// Token: 0x0600492F RID: 18735 RVA: 0x0064D03C File Offset: 0x0064B23C
		public override void Update()
		{
			if (!this.NotActuallyAnimating)
			{
				int num = this.FrameCounter + 1;
				this.FrameCounter = num;
				if (num >= this.TicksPerFrame)
				{
					this.FrameCounter = 0;
					if (this.PingPong)
					{
						num = this.Frame + 1;
						this.Frame = num;
						if (num >= this.FrameCount * 2 - 2)
						{
							this.Frame = 0;
							return;
						}
					}
					else
					{
						num = this.Frame + 1;
						this.Frame = num;
						if (num >= this.FrameCount)
						{
							this.Frame = 0;
						}
					}
					return;
				}
			}
		}

		// Token: 0x06004930 RID: 18736 RVA: 0x0064D0C0 File Offset: 0x0064B2C0
		public override Rectangle GetFrame(Texture2D texture, int frameCounterOverride = -1)
		{
			if (frameCounterOverride != -1)
			{
				int num4 = frameCounterOverride / this.TicksPerFrame;
				int num2 = this.FrameCount;
				if (this.PingPong)
				{
					num2 = num2 * 2 - 1;
				}
				int num3 = num4 % num2;
				if (this.PingPong && num3 >= this.FrameCount)
				{
					num3 = this.FrameCount * 2 - 2 - num3;
				}
				Rectangle result = texture.Frame(1, this.FrameCount, 0, num3, 0, 0);
				result.Height -= 2;
				return result;
			}
			int frameY = this.Frame;
			if (this.PingPong && this.Frame >= this.FrameCount)
			{
				frameY = this.FrameCount * 2 - 2 - this.Frame;
			}
			Rectangle result2 = texture.Frame(1, this.FrameCount, 0, frameY, 0, 0);
			result2.Height -= 2;
			return result2;
		}

		// Token: 0x04005E93 RID: 24211
		public bool PingPong;

		// Token: 0x04005E94 RID: 24212
		public bool NotActuallyAnimating;
	}
}
