using System;
using System.IO;

namespace Terraria.IO
{
	// Token: 0x020000DB RID: 219
	public class FileMetadata
	{
		// Token: 0x06001508 RID: 5384 RVA: 0x0000B904 File Offset: 0x00009B04
		private FileMetadata()
		{
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x004AF2EA File Offset: 0x004AD4EA
		public void Write(BinaryWriter writer)
		{
			writer.Write(27981915666277746UL | (ulong)this.Type << 56);
			writer.Write(this.Revision);
			writer.Write((ulong)((long)((this.IsFavorite.ToInt() & 1) | 0)));
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x004AF328 File Offset: 0x004AD528
		public void IncrementAndWrite(BinaryWriter writer)
		{
			this.Revision += 1U;
			this.Write(writer);
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x004AF33F File Offset: 0x004AD53F
		public static FileMetadata FromCurrentSettings(FileType type)
		{
			return new FileMetadata
			{
				Type = type,
				Revision = 0U,
				IsFavorite = false
			};
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x004AF35C File Offset: 0x004AD55C
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

		// Token: 0x0600150D RID: 5389 RVA: 0x004AF3E0 File Offset: 0x004AD5E0
		private void Read(BinaryReader reader)
		{
			ulong num = reader.ReadUInt64();
			if ((num & 72057594037927935UL) != 27981915666277746UL)
			{
				throw new FormatException("Expected Re-Logic file format.");
			}
			byte b = (byte)(num >> 56 & 255UL);
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

		// Token: 0x0400128B RID: 4747
		public const ulong MAGIC_NUMBER = 27981915666277746UL;

		// Token: 0x0400128C RID: 4748
		public const int SIZE = 20;

		// Token: 0x0400128D RID: 4749
		public FileType Type;

		// Token: 0x0400128E RID: 4750
		public uint Revision;

		// Token: 0x0400128F RID: 4751
		public bool IsFavorite;
	}
}
