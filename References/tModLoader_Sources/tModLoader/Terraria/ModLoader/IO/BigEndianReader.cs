using System;
using System.Buffers.Binary;
using System.IO;

namespace Terraria.ModLoader.IO
{
	// Token: 0x0200027D RID: 637
	public class BigEndianReader : BinaryReader
	{
		// Token: 0x06002B95 RID: 11157 RVA: 0x00523124 File Offset: 0x00521324
		public BigEndianReader(Stream input) : base(input)
		{
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x0052312D File Offset: 0x0052132D
		public override short ReadInt16()
		{
			return BinaryPrimitives.ReadInt16BigEndian(this.BaseStream.ReadByteSpan(2));
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x00523140 File Offset: 0x00521340
		public override ushort ReadUInt16()
		{
			return BinaryPrimitives.ReadUInt16BigEndian(this.BaseStream.ReadByteSpan(2));
		}

		// Token: 0x06002B98 RID: 11160 RVA: 0x00523153 File Offset: 0x00521353
		public override int ReadInt32()
		{
			return BinaryPrimitives.ReadInt32BigEndian(this.BaseStream.ReadByteSpan(4));
		}

		// Token: 0x06002B99 RID: 11161 RVA: 0x00523166 File Offset: 0x00521366
		public override uint ReadUInt32()
		{
			return BinaryPrimitives.ReadUInt32BigEndian(this.BaseStream.ReadByteSpan(4));
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x00523179 File Offset: 0x00521379
		public override long ReadInt64()
		{
			return BinaryPrimitives.ReadInt64BigEndian(this.BaseStream.ReadByteSpan(8));
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x0052318C File Offset: 0x0052138C
		public override ulong ReadUInt64()
		{
			return BinaryPrimitives.ReadUInt64BigEndian(this.BaseStream.ReadByteSpan(8));
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x0052319F File Offset: 0x0052139F
		public override float ReadSingle()
		{
			return BinaryPrimitives.ReadSingleBigEndian(this.BaseStream.ReadByteSpan(4));
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x005231B2 File Offset: 0x005213B2
		public override double ReadDouble()
		{
			return BinaryPrimitives.ReadDoubleBigEndian(this.BaseStream.ReadByteSpan(8));
		}
	}
}
