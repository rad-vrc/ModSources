using System;
using Terraria.ID;

namespace Terraria
{
	// Token: 0x02000063 RID: 99
	public struct TileWallWireStateData : ITileData
	{
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x003FD4A9 File Offset: 0x003FB6A9
		// (set) Token: 0x06000FD3 RID: 4051 RVA: 0x003FD4B7 File Offset: 0x003FB6B7
		public bool HasTile
		{
			get
			{
				return TileDataPacking.GetBit(this.bitpack, 0);
			}
			set
			{
				this.bitpack = TileDataPacking.SetBit(value, this.bitpack, 0);
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x003FD4CC File Offset: 0x003FB6CC
		// (set) Token: 0x06000FD5 RID: 4053 RVA: 0x003FD4DA File Offset: 0x003FB6DA
		public bool IsActuated
		{
			get
			{
				return TileDataPacking.GetBit(this.bitpack, 1);
			}
			set
			{
				this.bitpack = TileDataPacking.SetBit(value, this.bitpack, 1);
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x003FD4EF File Offset: 0x003FB6EF
		// (set) Token: 0x06000FD7 RID: 4055 RVA: 0x003FD4FD File Offset: 0x003FB6FD
		public bool HasActuator
		{
			get
			{
				return TileDataPacking.GetBit(this.bitpack, 2);
			}
			set
			{
				this.bitpack = TileDataPacking.SetBit(value, this.bitpack, 2);
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x003FD512 File Offset: 0x003FB712
		// (set) Token: 0x06000FD9 RID: 4057 RVA: 0x003FD522 File Offset: 0x003FB722
		public byte TileColor
		{
			get
			{
				return (byte)TileDataPacking.Unpack(this.bitpack, 3, 5);
			}
			set
			{
				this.bitpack = TileDataPacking.Pack((int)value, this.bitpack, 3, 5);
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x003FD538 File Offset: 0x003FB738
		// (set) Token: 0x06000FDB RID: 4059 RVA: 0x003FD548 File Offset: 0x003FB748
		public byte WallColor
		{
			get
			{
				return (byte)TileDataPacking.Unpack(this.bitpack, 8, 5);
			}
			set
			{
				this.bitpack = TileDataPacking.Pack((int)value, this.bitpack, 8, 5);
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x003FD55E File Offset: 0x003FB75E
		// (set) Token: 0x06000FDD RID: 4061 RVA: 0x003FD56E File Offset: 0x003FB76E
		public int TileFrameNumber
		{
			get
			{
				return TileDataPacking.Unpack(this.bitpack, 13, 2);
			}
			set
			{
				this.bitpack = TileDataPacking.Pack(value, this.bitpack, 13, 2);
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x003FD585 File Offset: 0x003FB785
		// (set) Token: 0x06000FDF RID: 4063 RVA: 0x003FD595 File Offset: 0x003FB795
		public int WallFrameNumber
		{
			get
			{
				return TileDataPacking.Unpack(this.bitpack, 15, 2);
			}
			set
			{
				this.bitpack = TileDataPacking.Pack(value, this.bitpack, 15, 2);
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x003FD5AC File Offset: 0x003FB7AC
		// (set) Token: 0x06000FE1 RID: 4065 RVA: 0x003FD5BF File Offset: 0x003FB7BF
		public int WallFrameX
		{
			get
			{
				return TileDataPacking.Unpack(this.bitpack, 17, 4) * 36;
			}
			set
			{
				this.bitpack = TileDataPacking.Pack(value / 36, this.bitpack, 17, 4);
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x003FD5D9 File Offset: 0x003FB7D9
		// (set) Token: 0x06000FE3 RID: 4067 RVA: 0x003FD5EC File Offset: 0x003FB7EC
		public int WallFrameY
		{
			get
			{
				return TileDataPacking.Unpack(this.bitpack, 21, 3) * 36;
			}
			set
			{
				this.bitpack = TileDataPacking.Pack(value / 36, this.bitpack, 21, 3);
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x003FD606 File Offset: 0x003FB806
		// (set) Token: 0x06000FE5 RID: 4069 RVA: 0x003FD615 File Offset: 0x003FB815
		public bool IsHalfBlock
		{
			get
			{
				return TileDataPacking.GetBit(this.bitpack, 24);
			}
			set
			{
				this.bitpack = TileDataPacking.SetBit(value, this.bitpack, 24);
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x003FD62B File Offset: 0x003FB82B
		// (set) Token: 0x06000FE7 RID: 4071 RVA: 0x003FD63B File Offset: 0x003FB83B
		public SlopeType Slope
		{
			get
			{
				return (SlopeType)TileDataPacking.Unpack(this.bitpack, 25, 3);
			}
			set
			{
				this.bitpack = TileDataPacking.Pack((int)value, this.bitpack, 25, 3);
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x003FD652 File Offset: 0x003FB852
		// (set) Token: 0x06000FE9 RID: 4073 RVA: 0x003FD662 File Offset: 0x003FB862
		public int WireData
		{
			get
			{
				return TileDataPacking.Unpack(this.bitpack, 28, 4);
			}
			set
			{
				this.bitpack = TileDataPacking.Pack(value, this.bitpack, 28, 4);
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000FEA RID: 4074 RVA: 0x003FD679 File Offset: 0x003FB879
		// (set) Token: 0x06000FEB RID: 4075 RVA: 0x003FD688 File Offset: 0x003FB888
		public bool RedWire
		{
			get
			{
				return TileDataPacking.GetBit(this.bitpack, 28);
			}
			set
			{
				this.bitpack = TileDataPacking.SetBit(value, this.bitpack, 28);
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000FEC RID: 4076 RVA: 0x003FD69E File Offset: 0x003FB89E
		// (set) Token: 0x06000FED RID: 4077 RVA: 0x003FD6AD File Offset: 0x003FB8AD
		public bool BlueWire
		{
			get
			{
				return TileDataPacking.GetBit(this.bitpack, 29);
			}
			set
			{
				this.bitpack = TileDataPacking.SetBit(value, this.bitpack, 29);
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000FEE RID: 4078 RVA: 0x003FD6C3 File Offset: 0x003FB8C3
		// (set) Token: 0x06000FEF RID: 4079 RVA: 0x003FD6D2 File Offset: 0x003FB8D2
		public bool GreenWire
		{
			get
			{
				return TileDataPacking.GetBit(this.bitpack, 30);
			}
			set
			{
				this.bitpack = TileDataPacking.SetBit(value, this.bitpack, 30);
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000FF0 RID: 4080 RVA: 0x003FD6E8 File Offset: 0x003FB8E8
		// (set) Token: 0x06000FF1 RID: 4081 RVA: 0x003FD6F7 File Offset: 0x003FB8F7
		public bool YellowWire
		{
			get
			{
				return TileDataPacking.GetBit(this.bitpack, 31);
			}
			set
			{
				this.bitpack = TileDataPacking.SetBit(value, this.bitpack, 31);
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x003FD70D File Offset: 0x003FB90D
		public int NonFrameBits
		{
			get
			{
				return (int)((long)this.bitpack & (long)((ulong)-16769025));
			}
		}

		/// <summary>
		/// Intended to be used to set all the persistent data about a tile. For example, when loading a schematic from serialized NonFrameBits.
		/// </summary>
		// Token: 0x06000FF3 RID: 4083 RVA: 0x003FD71E File Offset: 0x003FB91E
		public void SetAllBitsClearFrame(int nonFrameBits)
		{
			this.bitpack = (int)((long)nonFrameBits & (long)((ulong)-16769025));
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x003FD730 File Offset: 0x003FB930
		// (set) Token: 0x06000FF5 RID: 4085 RVA: 0x003FD756 File Offset: 0x003FB956
		public BlockType BlockType
		{
			get
			{
				if (this.IsHalfBlock)
				{
					return BlockType.HalfBlock;
				}
				SlopeType slope = this.Slope;
				if (slope != SlopeType.Solid)
				{
					return (BlockType)(slope + 1);
				}
				return BlockType.Solid;
			}
			set
			{
				this.IsHalfBlock = (value == BlockType.HalfBlock);
				this.Slope = (SlopeType)((value > BlockType.HalfBlock) ? (value - 1) : BlockType.Solid);
			}
		}

		// Token: 0x04000EDA RID: 3802
		public short TileFrameX;

		// Token: 0x04000EDB RID: 3803
		public short TileFrameY;

		// Token: 0x04000EDC RID: 3804
		private int bitpack;
	}
}
