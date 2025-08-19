using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terraria.Social.WeGame
{
	// Token: 0x020000D2 RID: 210
	public class IPCMessage
	{
		// Token: 0x06001701 RID: 5889 RVA: 0x004B670F File Offset: 0x004B490F
		public void Build<T>(IPCMessageType cmd, T t)
		{
			this._jsonData = WeGameHelper.Serialize<T>(t);
			this._cmd = cmd;
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x004B6724 File Offset: 0x004B4924
		public void BuildFrom(byte[] data)
		{
			byte[] value = data.Take(4).ToArray<byte>();
			byte[] bytes = data.Skip(4).ToArray<byte>();
			this._cmd = (IPCMessageType)BitConverter.ToInt32(value, 0);
			this._jsonData = Encoding.UTF8.GetString(bytes);
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x004B6769 File Offset: 0x004B4969
		public void Parse<T>(out T value)
		{
			WeGameHelper.UnSerialize<T>(this._jsonData, out value);
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x004B6778 File Offset: 0x004B4978
		public byte[] GetBytes()
		{
			List<byte> list = new List<byte>();
			byte[] bytes = BitConverter.GetBytes((int)this._cmd);
			list.AddRange(bytes);
			list.AddRange(Encoding.UTF8.GetBytes(this._jsonData));
			return list.ToArray();
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x004B67B8 File Offset: 0x004B49B8
		public IPCMessageType GetCmd()
		{
			return this._cmd;
		}

		// Token: 0x040012E0 RID: 4832
		private IPCMessageType _cmd;

		// Token: 0x040012E1 RID: 4833
		private string _jsonData;
	}
}
