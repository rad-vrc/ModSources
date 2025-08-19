using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Terraria.ModLoader.IO
{
	// Token: 0x0200027E RID: 638
	public static class BinaryIO
	{
		// Token: 0x06002B9E RID: 11166 RVA: 0x005231C5 File Offset: 0x005213C5
		[Obsolete("Use Write7BitEncodedInt", true)]
		public static void WriteVarInt(this BinaryWriter writer, int value)
		{
			writer.Write7BitEncodedInt(value);
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x005231CE File Offset: 0x005213CE
		[Obsolete("Use Read7BitEncodedInt", true)]
		public static int ReadVarInt(this BinaryReader reader)
		{
			return reader.Read7BitEncodedInt();
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x005231D6 File Offset: 0x005213D6
		public static BitsByte ReadBitsByte(this BinaryReader reader)
		{
			return reader.ReadByte();
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.IO.BinaryIO.ReadFlags(System.IO.BinaryReader,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@)" />
		// Token: 0x06002BA1 RID: 11169 RVA: 0x005231E4 File Offset: 0x005213E4
		public static void ReadFlags(this BinaryReader reader, out bool b0)
		{
			b0 = false;
			reader.ReadByte().Retrieve(ref b0);
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.IO.BinaryIO.ReadFlags(System.IO.BinaryReader,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@)" />
		// Token: 0x06002BA2 RID: 11170 RVA: 0x00523208 File Offset: 0x00521408
		public static void ReadFlags(this BinaryReader reader, out bool b0, out bool b1)
		{
			b0 = (b1 = false);
			reader.ReadByte().Retrieve(ref b0, ref b1);
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.IO.BinaryIO.ReadFlags(System.IO.BinaryReader,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@)" />
		// Token: 0x06002BA3 RID: 11171 RVA: 0x00523234 File Offset: 0x00521434
		public static void ReadFlags(this BinaryReader reader, out bool b0, out bool b1, out bool b2)
		{
			b0 = (b1 = (b2 = false));
			reader.ReadByte().Retrieve(ref b0, ref b1, ref b2);
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.IO.BinaryIO.ReadFlags(System.IO.BinaryReader,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@)" />
		// Token: 0x06002BA4 RID: 11172 RVA: 0x00523264 File Offset: 0x00521464
		public static void ReadFlags(this BinaryReader reader, out bool b0, out bool b1, out bool b2, out bool b3)
		{
			b0 = (b1 = (b2 = (b3 = false)));
			reader.ReadByte().Retrieve(ref b0, ref b1, ref b2, ref b3);
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.IO.BinaryIO.ReadFlags(System.IO.BinaryReader,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@)" />
		// Token: 0x06002BA5 RID: 11173 RVA: 0x0052329C File Offset: 0x0052149C
		public static void ReadFlags(this BinaryReader reader, out bool b0, out bool b1, out bool b2, out bool b3, out bool b4)
		{
			b0 = (b1 = (b2 = (b3 = (b4 = false))));
			reader.ReadByte().Retrieve(ref b0, ref b1, ref b2, ref b3, ref b4);
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.IO.BinaryIO.ReadFlags(System.IO.BinaryReader,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@)" />
		// Token: 0x06002BA6 RID: 11174 RVA: 0x005232DC File Offset: 0x005214DC
		public static void ReadFlags(this BinaryReader reader, out bool b0, out bool b1, out bool b2, out bool b3, out bool b4, out bool b5)
		{
			b0 = (b1 = (b2 = (b3 = (b4 = (b5 = false)))));
			reader.ReadByte().Retrieve(ref b0, ref b1, ref b2, ref b3, ref b4, ref b5);
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.IO.BinaryIO.ReadFlags(System.IO.BinaryReader,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@,System.Boolean@)" />
		// Token: 0x06002BA7 RID: 11175 RVA: 0x00523324 File Offset: 0x00521524
		public static void ReadFlags(this BinaryReader reader, out bool b0, out bool b1, out bool b2, out bool b3, out bool b4, out bool b5, out bool b6)
		{
			b0 = (b1 = (b2 = (b3 = (b4 = (b5 = (b6 = false))))));
			reader.ReadByte().Retrieve(ref b0, ref b1, ref b2, ref b3, ref b4, ref b5, ref b6);
		}

		/// <summary>
		/// Reads up to 8 <see langword="bool" />s sent as a single <see langword="byte" /> using <c>BinaryReader.WriteFlags</c>. This is more efficient than using <see cref="M:System.IO.BinaryReader.ReadBoolean" />.
		/// </summary>
		// Token: 0x06002BA8 RID: 11176 RVA: 0x00523374 File Offset: 0x00521574
		public static void ReadFlags(this BinaryReader reader, out bool b0, out bool b1, out bool b2, out bool b3, out bool b4, out bool b5, out bool b6, out bool b7)
		{
			b0 = (b1 = (b2 = (b3 = (b4 = (b5 = (b6 = (b7 = false)))))));
			reader.ReadByte().Retrieve(ref b0, ref b1, ref b2, ref b3, ref b4, ref b5, ref b6, ref b7);
		}

		/// <summary>
		/// Efficiently writes up to 8 <see langword="bool" />s as a single <see langword="byte" />. To read, use <c>BinaryReader.ReadFlags</c>. This is more efficient than using <see cref="M:System.IO.BinaryWriter.Write(System.Boolean)" />.
		/// </summary>
		// Token: 0x06002BA9 RID: 11177 RVA: 0x005233CC File Offset: 0x005215CC
		public static void WriteFlags(this BinaryWriter writer, bool b1 = false, bool b2 = false, bool b3 = false, bool b4 = false, bool b5 = false, bool b6 = false, bool b7 = false, bool b8 = false)
		{
			writer.Write(new BitsByte(b1, b2, b3, b4, b5, b6, b7, b8));
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.IO.BinaryIO.WriteFlags(System.IO.BinaryWriter,System.Boolean,System.Boolean,System.Boolean,System.Boolean,System.Boolean,System.Boolean,System.Boolean,System.Boolean)" />
		// Token: 0x06002BAA RID: 11178 RVA: 0x005233F8 File Offset: 0x005215F8
		public static void Write(this BinaryWriter writer, bool b1 = false, bool b2 = false, bool b3 = false, bool b4 = false, bool b5 = false, bool b6 = false, bool b7 = false, bool b8 = false)
		{
			writer.Write(new BitsByte(b1, b2, b3, b4, b5, b6, b7, b8));
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x00523424 File Offset: 0x00521624
		public static void SafeWrite(this BinaryWriter writer, Action<BinaryWriter> write)
		{
			MemoryStream ms = new MemoryStream();
			write(new BinaryWriter(ms));
			writer.Write7BitEncodedInt((int)ms.Length);
			ms.Position = 0L;
			ms.CopyTo(writer.BaseStream);
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x00523464 File Offset: 0x00521664
		public static void SafeRead(this BinaryReader reader, Action<BinaryReader> read)
		{
			int length = reader.Read7BitEncodedInt();
			MemoryStream ms = reader.ReadBytes(length).ToMemoryStream(false);
			read(new BinaryReader(ms));
			if (ms.Position != (long)length)
			{
				throw new IOException(string.Concat(new string[]
				{
					"Read underflow ",
					ms.Position.ToString(),
					" of ",
					length.ToString(),
					" bytes"
				}));
			}
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x005234E0 File Offset: 0x005216E0
		public static void ReadBytes(this Stream stream, byte[] buf)
		{
			int pos = 0;
			int r;
			while ((r = stream.Read(buf, pos, buf.Length - pos)) > 0)
			{
				pos += r;
			}
			if (pos != buf.Length)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 2);
				defaultInterpolatedStringHandler.AppendLiteral("Stream did not contain enough bytes (");
				defaultInterpolatedStringHandler.AppendFormatted<int>(pos);
				defaultInterpolatedStringHandler.AppendLiteral(") < (");
				defaultInterpolatedStringHandler.AppendFormatted<int>(buf.Length);
				defaultInterpolatedStringHandler.AppendLiteral(")");
				throw new IOException(defaultInterpolatedStringHandler.ToStringAndClear());
			}
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x0052355A File Offset: 0x0052175A
		public static byte[] ReadBytes(this Stream stream, int len)
		{
			return stream.ReadBytes((long)len);
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x00523564 File Offset: 0x00521764
		public static byte[] ReadBytes(this Stream stream, long len)
		{
			byte[] buf = new byte[len];
			stream.ReadBytes(buf);
			return buf;
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x00523581 File Offset: 0x00521781
		public static MemoryStream ToMemoryStream(this byte[] bytes, bool writeable = false)
		{
			return new MemoryStream(bytes, 0, bytes.Length, writeable, true);
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x00523590 File Offset: 0x00521790
		public static ReadOnlySpan<byte> ReadByteSpan(this Stream stream, int len)
		{
			MemoryStream ms = stream as MemoryStream;
			ArraySegment<byte> buf;
			if (ms != null && ms.TryGetBuffer(out buf))
			{
				Span<byte> span = buf.AsSpan<byte>().Slice((int)ms.Position, len);
				ms.Seek((long)len, SeekOrigin.Current);
				return span;
			}
			return stream.ReadBytes(len);
		}
	}
}
