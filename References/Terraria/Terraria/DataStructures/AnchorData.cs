using System;
using Terraria.Enums;

namespace Terraria.DataStructures
{
	// Token: 0x020003FB RID: 1019
	public struct AnchorData
	{
		// Token: 0x06002AE4 RID: 10980 RVA: 0x0059D1F8 File Offset: 0x0059B3F8
		public AnchorData(AnchorType type, int count, int start)
		{
			this.type = type;
			this.tileCount = count;
			this.checkStart = start;
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x0059D20F File Offset: 0x0059B40F
		public static bool operator ==(AnchorData data1, AnchorData data2)
		{
			return data1.type == data2.type && data1.tileCount == data2.tileCount && data1.checkStart == data2.checkStart;
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x0059D23D File Offset: 0x0059B43D
		public static bool operator !=(AnchorData data1, AnchorData data2)
		{
			return data1.type != data2.type || data1.tileCount != data2.tileCount || data1.checkStart != data2.checkStart;
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x0059D270 File Offset: 0x0059B470
		public override bool Equals(object obj)
		{
			return obj is AnchorData && (this.type == ((AnchorData)obj).type && this.tileCount == ((AnchorData)obj).tileCount) && this.checkStart == ((AnchorData)obj).checkStart;
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x0059D2C4 File Offset: 0x0059B4C4
		public override int GetHashCode()
		{
			byte b = (byte)this.checkStart;
			byte b2 = (byte)this.tileCount;
			return (int)((ushort)this.type) << 16 | (int)b2 << 8 | (int)b;
		}

		// Token: 0x04004F2E RID: 20270
		public AnchorType type;

		// Token: 0x04004F2F RID: 20271
		public int tileCount;

		// Token: 0x04004F30 RID: 20272
		public int checkStart;

		// Token: 0x04004F31 RID: 20273
		public static AnchorData Empty;
	}
}
