using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terraria.Social.WeGame
{
	// Token: 0x02000164 RID: 356
	public class IPCMessage
	{
		// Token: 0x06001A13 RID: 6675 RVA: 0x004E345A File Offset: 0x004E165A
		public void Build<T>(IPCMessageType cmd, T t)
		{
			this._jsonData = WeGameHelper.Serialize<T>(t);
			this._cmd = cmd;
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x004E3470 File Offset: 0x004E1670
		public void BuildFrom(byte[] data)
		{
			byte[] value = data.Take(4).ToArray<byte>();
			byte[] bytes = data.Skip(4).ToArray<byte>();
			this._cmd = (IPCMessageType)BitConverter.ToInt32(value, 0);
			this._jsonData = Encoding.UTF8.GetString(bytes);
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x004E34B5 File Offset: 0x004E16B5
		public void Parse<T>(out T value)
		{
			WeGameHelper.UnSerialize<T>(this._jsonData, out value);
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x004E34C4 File Offset: 0x004E16C4
		public byte[] GetBytes()
		{
			List<byte> list = new List<byte>();
			byte[] bytes = BitConverter.GetBytes((int)this._cmd);
			list.AddRange(bytes);
			list.AddRange(Encoding.UTF8.GetBytes(this._jsonData));
			return list.ToArray();
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x004E3504 File Offset: 0x004E1704
		public IPCMessageType GetCmd()
		{
			return this._cmd;
		}

		// Token: 0x04001574 RID: 5492
		private IPCMessageType _cmd;

		// Token: 0x04001575 RID: 5493
		private string _jsonData;
	}
}
