using System;
using System.IO;

namespace Terraria.IO
{
	// Token: 0x020003DD RID: 989
	public class FileMetadata
	{
		// Token: 0x060033F7 RID: 13303 RVA: 0x00556229 File Offset: 0x00554429
		private FileMetadata()
		{
		}

		// Token: 0x060033F8 RID: 13304 RVA: 0x00556231 File Offset: 0x00554431
		public void Write(BinaryWriter writer)
		{
			writer.Write(27981915666277746UL | (ulong)this.Type << 56);
			writer.Write(this.Revision);
			writer.Write((ulong)((long)((this.IsFavorite.ToInt() & 1) | 0)));
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x0055626F File Offset: 0x0055446F
		public void IncrementAndWrite(BinaryWriter writer)
		{
			this.Revision += 1U;
			this.Write(writer);
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x00556286 File Offset: 0x00554486
		public static FileMetadata FromCurrentSettings(FileType type)
		{
			return new FileMetadata
			{
				Type = type,
				Revision = 0U,
				IsFavorite = false
			};
		}

		// Token: 0x060033FB RID: 13307 RVA: 0x005562A4 File Offset: 0x005544A4
		public static FileMetadata Read(BinaryReader reader, FileType expectedType)
		{
			FileMetadata fileMetadata = new FileMetadata();
			fileMetadata.Read(reader);
			if (fileMetadata.Type != expectedType)
			{
				throw new FormatException(string.Concat(new string[]
				{
					"Expected type \"",
					Enum.GetName(typeof(FileType), expectedType),
					"\" but found \"",
					Enum.GetName(typeof(FileType), fileMetadata.Type),
					"\"."
				}));
			}
			return fileMetadata;
		}

		// Token: 0x060033FC RID: 13308 RVA: 0x00556328 File Offset: 0x00554528
		private void Read(BinaryReader reader)
		{
			ulong num3 = reader.ReadUInt64();
			if ((num3 & 72057594037927935UL) != 27981915666277746UL)
			{
				throw new FormatException("Expected Re-Logic file format.");
			}
			byte b = (byte)(num3 >> 56 & 255UL);
			FileType fileType = FileType.None;
			FileType[] array = (FileType[])Enum.GetValues(typeof(FileType));
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == (FileType)b)
				{
					fileType = array[i];
					break;
				}
			}
			if (fileType == FileType.None)
			{
				throw new FormatException("Found invalid file type.");
			}
			this.Type = fileType;
			this.Revision = reader.ReadUInt32();
			ulong num2 = reader.ReadUInt64();
			this.IsFavorite = ((num2 & 1UL) == 1UL);
		}

		// Token: 0x04001E68 RID: 7784
		public const ulong MAGIC_NUMBER = 27981915666277746UL;

		// Token: 0x04001E69 RID: 7785
		public const int SIZE = 20;

		// Token: 0x04001E6A RID: 7786
		public FileType Type;

		// Token: 0x04001E6B RID: 7787
		public uint Revision;

		// Token: 0x04001E6C RID: 7788
		public bool IsFavorite;
	}
}
