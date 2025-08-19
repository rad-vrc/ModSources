using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Terraria.Social.WeGame
{
	// Token: 0x0200015D RID: 349
	public class WeGameHelper
	{
		// Token: 0x060019D8 RID: 6616
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern void OutputDebugString(string message);

		// Token: 0x060019D9 RID: 6617 RVA: 0x004E29A1 File Offset: 0x004E0BA1
		public static void WriteDebugString(string format, params object[] args)
		{
			"[WeGame] - " + format;
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x004E29B0 File Offset: 0x004E0BB0
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

		// Token: 0x060019DB RID: 6619 RVA: 0x004E2A30 File Offset: 0x004E0C30
		public static void UnSerialize<T>(string str, out T data)
		{
			using (MemoryStream memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(str)))
			{
				DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T));
				data = (T)((object)dataContractJsonSerializer.ReadObject(memoryStream));
			}
		}
	}
}
