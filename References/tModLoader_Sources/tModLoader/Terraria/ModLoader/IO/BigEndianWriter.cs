using System;
using System.Buffers.Binary;
using System.IO;

namespace Terraria.ModLoader.IO
{
	// Token: 0x0200027C RID: 636
	public class BigEndianWriter : BinaryWriter
	{
		// Token: 0x06002B8C RID: 11148 RVA: 0x00522F98 File Offset: 0x00521198
		public BigEndianWriter(Stream output) : base(output)
		{
		}

		// Token: 0x06002B8D RID: 11149 RVA: 0x00522FA4 File Offset: 0x005211A4
		public unsafe override void Write(short value)
		{
			Span<byte> buf = new Span<byte>(stackalloc byte[(UIntPtr)2], 2);
			BinaryPrimitives.WriteInt16BigEndian(buf, value);
			this.OutStream.Write(buf);
		}

		// Token: 0x06002B8E RID: 11150 RVA: 0x00522FD4 File Offset: 0x005211D4
		public unsafe override void Write(ushort value)
		{
			Span<byte> buf = new Span<byte>(stackalloc byte[(UIntPtr)2], 2);
			BinaryPrimitives.WriteUInt16BigEndian(buf, value);
			this.OutStream.Write(buf);
		}

		// Token: 0x06002B8F RID: 11151 RVA: 0x00523004 File Offset: 0x00521204
		public unsafe override void Write(int value)
		{
			Span<byte> buf = new Span<byte>(stackalloc byte[(UIntPtr)4], 4);
			BinaryPrimitives.WriteInt32BigEndian(buf, value);
			this.OutStream.Write(buf);
		}

		// Token: 0x06002B90 RID: 11152 RVA: 0x00523034 File Offset: 0x00521234
		public unsafe override void Write(uint value)
		{
			Span<byte> buf = new Span<byte>(stackalloc byte[(UIntPtr)4], 4);
			BinaryPrimitives.WriteUInt32BigEndian(buf, value);
			this.OutStream.Write(buf);
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x00523064 File Offset: 0x00521264
		public unsafe override void Write(long value)
		{
			Span<byte> buf = new Span<byte>(stackalloc byte[(UIntPtr)8], 8);
			BinaryPrimitives.WriteInt64BigEndian(buf, value);
			this.OutStream.Write(buf);
		}

		// Token: 0x06002B92 RID: 11154 RVA: 0x00523094 File Offset: 0x00521294
		public unsafe override void Write(ulong value)
		{
			Span<byte> buf = new Span<byte>(stackalloc byte[(UIntPtr)8], 8);
			BinaryPrimitives.WriteUInt64BigEndian(buf, value);
			this.OutStream.Write(buf);
		}

		// Token: 0x06002B93 RID: 11155 RVA: 0x005230C4 File Offset: 0x005212C4
		public unsafe override void Write(float value)
		{
			Span<byte> buf = new Span<byte>(stackalloc byte[(UIntPtr)4], 4);
			BinaryPrimitives.WriteSingleBigEndian(buf, value);
			this.OutStream.Write(buf);
		}

		// Token: 0x06002B94 RID: 11156 RVA: 0x005230F4 File Offset: 0x005212F4
		public unsafe override void Write(double value)
		{
			Span<byte> buf = new Span<byte>(stackalloc byte[(UIntPtr)8], 8);
			BinaryPrimitives.WriteDoubleBigEndian(buf, value);
			this.OutStream.Write(buf);
		}
	}
}
