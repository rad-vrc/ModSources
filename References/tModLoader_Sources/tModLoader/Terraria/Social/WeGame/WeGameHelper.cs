using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Terraria.Social.WeGame
{
	// Token: 0x020000E0 RID: 224
	public class WeGameHelper
	{
		// Token: 0x060017A3 RID: 6051
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern void OutputDebugString(string message);

		// Token: 0x060017A4 RID: 6052 RVA: 0x004B89F0 File Offset: 0x004B6BF0
		public static void WriteDebugString(string format, params object[] args)
		{
			"[WeGame] - " + format;
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x004B8A00 File Offset: 0x004B6C00
		public static string Serialize<T>(T data)
		{
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T));
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				dataContractJsonSerializer.WriteObject(memoryStream, data);
				memoryStream.Position = 0L;
				using (StreamReader streamReader = new StreamReader(memoryStream, Encoding.UTF8))
				{
					result = streamReader.ReadToEnd();
				}
			}
			return result;
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x004B8A80 File Offset: 0x004B6C80
		public static void UnSerialize<T>(string str, out T data)
		{
			using (MemoryStream stream = new MemoryStream(Encoding.Unicode.GetBytes(str)))
			{
				DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T));
				data = (T)((object)dataContractJsonSerializer.ReadObject(stream));
			}
		}
	}
}
