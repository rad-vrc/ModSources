using System;
using System.IO;

namespace Terraria.DataStructures
{
	// Token: 0x020006D1 RID: 1745
	public struct BinaryWriterHelper
	{
		// Token: 0x06004905 RID: 18693 RVA: 0x0064C436 File Offset: 0x0064A636
		public void ReservePointToFillLengthLaterByFilling6Bytes(BinaryWriter writer)
		{
			this._placeInWriter = writer.BaseStream.Position;
			writer.Write(0U);
			writer.Write(0);
		}

		// Token: 0x06004906 RID: 18694 RVA: 0x0064C458 File Offset: 0x0064A658
		public void FillReservedPoint(BinaryWriter writer, ushort dataId)
		{
			long position = writer.BaseStream.Position;
			writer.BaseStream.Position = this._placeInWriter;
			long num = position - this._placeInWriter - 4L;
			writer.Write((int)num);
			writer.Write(dataId);
			writer.BaseStream.Position = position;
		}

		// Token: 0x06004907 RID: 18695 RVA: 0x0064C4AC File Offset: 0x0064A6AC
		public void FillOnlyIfThereIsLengthOrRevertToSavedPosition(BinaryWriter writer, ushort dataId, out bool wroteSomething)
		{
			wroteSomething = false;
			long position = writer.BaseStream.Position;
			writer.BaseStream.Position = this._placeInWriter;
			long num = position - this._placeInWriter - 4L;
			if (num != 0L)
			{
				writer.Write((int)num);
				writer.Write(dataId);
				writer.BaseStream.Position = position;
				wroteSomething = true;
			}
		}

		// Token: 0x04005E54 RID: 24148
		private long _placeInWriter;
	}
}
