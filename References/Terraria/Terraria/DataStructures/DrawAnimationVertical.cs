using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.DataStructures
{
	// Token: 0x02000448 RID: 1096
	public class DrawAnimationVertical : DrawAnimation
	{
		// Token: 0x06002BE4 RID: 11236 RVA: 0x0059FC9C File Offset: 0x0059DE9C
		public DrawAnimationVertical(int ticksperframe, int frameCount, bool pingPong = false)
		{
			this.Frame = 0;
			this.FrameCounter = 0;
			this.FrameCount = frameCount;
			this.TicksPerFrame = ticksperframe;
			this.PingPong = pingPong;
		}

		// Token: 0x06002BE5 RID: 11237 RVA: 0x0059FCC8 File Offset: 0x0059DEC8
		public override void Update()
		{
			if (this.NotActuallyAnimating)
			{
				return;
			}
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
			}
		}

		// Token: 0x06002BE6 RID: 11238 RVA: 0x0059FD4C File Offset: 0x0059DF4C
		public override Rectangle GetFrame(Texture2D texture, int frameCounterOverride = -1)
		{
			if (frameCounterOverride != -1)
			{
				int num = frameCounterOverride / this.TicksPerFrame;
				int num2 = this.FrameCount;
				if (this.PingPong)
				{
					num2 = num2 * 2 - 1;
				}
				int num3 = num % num2;
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

		// Token: 0x04005004 RID: 20484
		public bool PingPong;

		// Token: 0x04005005 RID: 20485
		public bool NotActuallyAnimating;
	}
}
