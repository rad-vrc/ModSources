using System;

namespace Terraria
{
	// Token: 0x02000062 RID: 98
	public struct TileWallBrightnessInvisibilityData : ITileData
	{
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x003FD428 File Offset: 0x003FB628
		public byte Data
		{
			get
			{
				return this.bitpack;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000FCA RID: 4042 RVA: 0x003FD435 File Offset: 0x003FB635
		// (set) Token: 0x06000FCB RID: 4043 RVA: 0x003FD443 File Offset: 0x003FB643
		public bool IsTileInvisible
		{
			get
			{
				return this.bitpack[0];
			}
			set
			{
				this.bitpack[0] = value;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x003FD452 File Offset: 0x003FB652
		// (set) Token: 0x06000FCD RID: 4045 RVA: 0x003FD460 File Offset: 0x003FB660
		public bool IsWallInvisible
		{
			get
			{
				return this.bitpack[1];
			}
			set
			{
				this.bitpack[1] = value;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x003FD46F File Offset: 0x003FB66F
		// (set) Token: 0x06000FCF RID: 4047 RVA: 0x003FD47D File Offset: 0x003FB67D
		public bool IsTileFullbright
		{
			get
			{
				return this.bitpack[2];
			}
			set
			{
				this.bitpack[2] = value;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x003FD48C File Offset: 0x003FB68C
		// (set) Token: 0x06000FD1 RID: 4049 RVA: 0x003FD49A File Offset: 0x003FB69A
		public bool IsWallFullbright
		{
			get
			{
				return this.bitpack[3];
			}
			set
			{
				this.bitpack[3] = value;
			}
		}

		// Token: 0x04000ED9 RID: 3801
		private BitsByte bitpack;
	}
}
