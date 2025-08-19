using System;

namespace Terraria.Map
{
	// Token: 0x020000D0 RID: 208
	public struct MapTile
	{
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06001484 RID: 5252 RVA: 0x004AD654 File Offset: 0x004AB854
		// (set) Token: 0x06001485 RID: 5253 RVA: 0x004AD669 File Offset: 0x004AB869
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

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x004AD692 File Offset: 0x004AB892
		// (set) Token: 0x06001487 RID: 5255 RVA: 0x004AD69E File Offset: 0x004AB89E
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

		// Token: 0x06001488 RID: 5256 RVA: 0x004AD6B8 File Offset: 0x004AB8B8
		private MapTile(ushort type, byte light, byte extraData)
		{
			this.Type = type;
			this.Light = light;
			this._extraData = extraData;
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x004AD6CF File Offset: 0x004AB8CF
		public bool Equals(ref MapTile other)
		{
			return this.Light == other.Light && this.Type == other.Type && this.Color == other.Color;
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x004AD6FD File Offset: 0x004AB8FD
		public bool EqualsWithoutLight(ref MapTile other)
		{
			return this.Type == other.Type && this.Color == other.Color;
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x004AD71D File Offset: 0x004AB91D
		public void Clear()
		{
			this.Type = 0;
			this.Light = 0;
			this._extraData = 0;
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x004AD734 File Offset: 0x004AB934
		public MapTile WithLight(byte light)
		{
			return new MapTile(this.Type, light, this._extraData | 128);
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x004AD74F File Offset: 0x004AB94F
		public static MapTile Create(ushort type, byte light, byte color)
		{
			return new MapTile(type, light, color | 128);
		}

		// Token: 0x04001246 RID: 4678
		public ushort Type;

		// Token: 0x04001247 RID: 4679
		public byte Light;

		// Token: 0x04001248 RID: 4680
		private byte _extraData;
	}
}
