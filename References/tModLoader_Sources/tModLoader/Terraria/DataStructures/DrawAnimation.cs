using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.DataStructures
{
	// Token: 0x020006DB RID: 1755
	public class DrawAnimation
	{
		// Token: 0x0600492B RID: 18731 RVA: 0x0064CFF6 File Offset: 0x0064B1F6
		public virtual void Update()
		{
		}

		// Token: 0x0600492C RID: 18732 RVA: 0x0064CFF8 File Offset: 0x0064B1F8
		public virtual Rectangle GetFrame(Texture2D texture, int frameCounterOverride = -1)
		{
			return texture.Frame(1, 1, 0, 0, 0, 0);
		}

		// Token: 0x04005E8F RID: 24207
		public int Frame;

		// Token: 0x04005E90 RID: 24208
		public int FrameCount;

		// Token: 0x04005E91 RID: 24209
		public int TicksPerFrame;

		// Token: 0x04005E92 RID: 24210
		public int FrameCounter;
	}
}
