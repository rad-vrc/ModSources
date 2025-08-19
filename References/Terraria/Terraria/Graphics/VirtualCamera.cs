using System;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics
{
	// Token: 0x020000F3 RID: 243
	public struct VirtualCamera
	{
		// Token: 0x060015D3 RID: 5587 RVA: 0x004C4705 File Offset: 0x004C2905
		public VirtualCamera(Player player)
		{
			this.Player = player;
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x004C470E File Offset: 0x004C290E
		public Vector2 Position
		{
			get
			{
				return this.Center - this.Size * 0.5f;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060015D5 RID: 5589 RVA: 0x004C472B File Offset: 0x004C292B
		public Vector2 Size
		{
			get
			{
				return new Vector2((float)Main.maxScreenW, (float)Main.maxScreenH);
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060015D6 RID: 5590 RVA: 0x004C473E File Offset: 0x004C293E
		public Vector2 Center
		{
			get
			{
				return this.Player.Center;
			}
		}

		// Token: 0x040012EB RID: 4843
		public readonly Player Player;
	}
}
