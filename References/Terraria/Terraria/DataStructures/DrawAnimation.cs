using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.DataStructures
{
	// Token: 0x02000447 RID: 1095
	public class DrawAnimation
	{
		// Token: 0x06002BE1 RID: 11233 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public virtual void Update()
		{
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x0059FC8E File Offset: 0x0059DE8E
		public virtual Rectangle GetFrame(Texture2D texture, int frameCounterOverride = -1)
		{
			return texture.Frame(1, 1, 0, 0, 0, 0);
		}

		// Token: 0x04005000 RID: 20480
		public int Frame;

		// Token: 0x04005001 RID: 20481
		public int FrameCount;

		// Token: 0x04005002 RID: 20482
		public int TicksPerFrame;

		// Token: 0x04005003 RID: 20483
		public int FrameCounter;
	}
}
