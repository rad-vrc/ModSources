using System;

namespace Terraria.Map
{
	// Token: 0x020003CF RID: 975
	public struct MapTile
	{
		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06003351 RID: 13137 RVA: 0x00553A17 File Offset: 0x00551C17
		// (set) Token: 0x06003352 RID: 13138 RVA: 0x00553A2C File Offset: 0x00551C2C
		public bool IsChanged
		{
			get
			{
				return (this._extraData & 128) == 128;
			}
			set
			{
				if (value)
				{
					this._extraData |= 128;
					return;
				}
				this._extraData &= 127;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06003353 RID: 13139 RVA: 0x00553A55 File Offset: 0x00551C55
		// (set) Token: 0x06003354 RID: 13140 RVA: 0x00553A61 File Offset: 0x00551C61
		public byte Color
		{
			get
			{
				return this._extraData & 127;
			}
			set
			{
				this._extraData = ((this._extraData & 128) | (value & 127));
			}
		}

		// Token: 0x06003355 RID: 13141 RVA: 0x00553A7B File Offset: 0x00551C7B
		private MapTile(ushort type, byte light, byte extraData)
		{
			this.Type = type;
			this.Light = light;
			this._extraData = extraData;
		}

		// Token: 0x06003356 RID: 13142 RVA: 0x00553A92 File Offset: 0x00551C92
		public bool Equals(ref MapTile other)
		{
			return this.Light == other.Light && this.Type == other.Type && this.Color == other.Color;
		}

		// Token: 0x06003357 RID: 13143 RVA: 0x00553AC0 File Offset: 0x00551CC0
		public bool EqualsWithoutLight(ref MapTile other)
		{
			return this.Type == other.Type && this.Color == other.Color;
		}

		// Token: 0x06003358 RID: 13144 RVA: 0x00553AE0 File Offset: 0x00551CE0
		public void Clear()
		{
			this.Type = 0;
			this.Light = 0;
			this._extraData = 0;
		}

		// Token: 0x06003359 RID: 13145 RVA: 0x00553AF7 File Offset: 0x00551CF7
		public MapTile WithLight(byte light)
		{
			return new MapTile(this.Type, light, this._extraData | 128);
		}

		// Token: 0x0600335A RID: 13146 RVA: 0x00553B12 File Offset: 0x00551D12
		public static MapTile Create(ushort type, byte light, byte color)
		{
			return new MapTile(type, light, color | 128);
		}

		// Token: 0x04001E36 RID: 7734
		public ushort Type;

		// Token: 0x04001E37 RID: 7735
		public byte Light;

		// Token: 0x04001E38 RID: 7736
		private byte _extraData;
	}
}
