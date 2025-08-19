using System;

namespace Terraria
{
	// Token: 0x02000061 RID: 97
	public struct LiquidData : ITileData
	{
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x003FD3BA File Offset: 0x003FB5BA
		// (set) Token: 0x06000FC4 RID: 4036 RVA: 0x003FD3C9 File Offset: 0x003FB5C9
		public int LiquidType
		{
			get
			{
				return TileDataPacking.Unpack((int)this.typeAndFlags, 0, 6);
			}
			set
			{
				this.typeAndFlags = (byte)TileDataPacking.Pack(value, (int)this.typeAndFlags, 0, 6);
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x003FD3E0 File Offset: 0x003FB5E0
		// (set) Token: 0x06000FC6 RID: 4038 RVA: 0x003FD3EE File Offset: 0x003FB5EE
		public bool SkipLiquid
		{
			get
			{
				return TileDataPacking.GetBit((int)this.typeAndFlags, 6);
			}
			set
			{
				this.typeAndFlags = (byte)TileDataPacking.SetBit(value, (int)this.typeAndFlags, 6);
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x003FD404 File Offset: 0x003FB604
		// (set) Token: 0x06000FC8 RID: 4040 RVA: 0x003FD412 File Offset: 0x003FB612
		public bool CheckingLiquid
		{
			get
			{
				return TileDataPacking.GetBit((int)this.typeAndFlags, 7);
			}
			set
			{
				this.typeAndFlags = (byte)TileDataPacking.SetBit(value, (int)this.typeAndFlags, 7);
			}
		}

		// Token: 0x04000ED7 RID: 3799
		public byte Amount;

		// Token: 0x04000ED8 RID: 3800
		private byte typeAndFlags;
	}
}
