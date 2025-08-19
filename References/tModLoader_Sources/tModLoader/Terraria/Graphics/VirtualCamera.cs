using System;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics
{
	// Token: 0x02000442 RID: 1090
	public struct VirtualCamera
	{
		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x060035EB RID: 13803 RVA: 0x00579F04 File Offset: 0x00578104
		public Vector2 Position
		{
			get
			{
				return this.Center - this.Size * 0.5f;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x060035EC RID: 13804 RVA: 0x00579F21 File Offset: 0x00578121
		public Vector2 Size
		{
			get
			{
				return new Vector2((float)Main.maxScreenW, (float)Main.maxScreenH);
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060035ED RID: 13805 RVA: 0x00579F34 File Offset: 0x00578134
		public Vector2 Center
		{
			get
			{
				return this.Player.Center;
			}
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x00579F41 File Offset: 0x00578141
		public VirtualCamera(Player player)
		{
			this.Player = player;
		}

		// Token: 0x04004FF8 RID: 20472
		public readonly Player Player;
	}
}
