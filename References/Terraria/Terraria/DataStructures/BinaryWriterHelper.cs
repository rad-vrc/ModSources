using System;
using System.IO;

namespace Terraria.DataStructures
{
	// Token: 0x02000407 RID: 1031
	public struct BinaryWriterHelper
	{
		// Token: 0x06002B27 RID: 11047 RVA: 0x0059DA9D File Offset: 0x0059BC9D
		public void ReservePointToFillLengthLaterByFilling6Bytes(BinaryWriter writer)
		{
			this._placeInWriter = writer.BaseStream.Position;
			writer.Write(0U);
			writer.Write(0);
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x0059DAC0 File Offset: 0x0059BCC0
		public void FillReservedPoint(BinaryWriter writer, ushort dataId)
		{
			long position = writer.BaseStream.Position;
			writer.BaseStream.Position = this._placeInWriter;
			long num = position - this._placeInWriter - 4L;
			writer.Write((int)num);
			writer.Write(dataId);
			writer.BaseStream.Position = position;
		}

		// Token: 0x06002B29 RID: 11049 RVA: 0x0059DB14 File Offset: 0x0059BD14
		public void FillOnlyIfThereIsLengthOrRevertToSavedPosition(BinaryWriter writer, ushort dataId, out bool wroteSomething)
		{
			wroteSomething = false;
			long position = writer.BaseStream.Position;
			writer.BaseStream.Position = this._placeInWriter;
			long num = position - this._placeInWriter - 4L;
			if (num == 0L)
			{
				return;
			}
			writer.Write((int)num);
			writer.Write(dataId);
			writer.BaseStream.Position = position;
			wroteSomething = true;
		}

		// Token: 0x04004F5A RID: 20314
		private long _placeInWriter;
	}
}
