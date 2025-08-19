using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;

namespace Terraria.ModLoader.IO
{
	// Token: 0x0200029B RID: 667
	public class UploadFile
	{
		// Token: 0x06002C9F RID: 11423 RVA: 0x00528B4E File Offset: 0x00526D4E
		public UploadFile()
		{
			this.ContentType = "application/octet-stream";
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06002CA0 RID: 11424 RVA: 0x00528B61 File Offset: 0x00526D61
		// (set) Token: 0x06002CA1 RID: 11425 RVA: 0x00528B69 File Offset: 0x00526D69
		public string Name { get; set; }

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06002CA2 RID: 11426 RVA: 0x00528B72 File Offset: 0x00526D72
		// (set) Token: 0x06002CA3 RID: 11427 RVA: 0x00528B7A File Offset: 0x00526D7A
		public string Filename { get; set; }

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06002CA4 RID: 11428 RVA: 0x00528B83 File Offset: 0x00526D83
		// (set) Token: 0x06002CA5 RID: 11429 RVA: 0x00528B8B File Offset: 0x00526D8B
		public string ContentType { get; set; }

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06002CA6 RID: 11430 RVA: 0x00528B94 File Offset: 0x00526D94
		// (set) Token: 0x06002CA7 RID: 11431 RVA: 0x00528B9C File Offset: 0x00526D9C
		public byte[] Content { get; set; }

		// Token: 0x06002CA8 RID: 11432 RVA: 0x00528BA8 File Offset: 0x00526DA8
		public static byte[] UploadFiles(string address, IEnumerable<UploadFile> files, NameValueCollection values)
		{
			WebRequest request = WebRequest.Create(address);
			request.Method = "POST";
			string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x", NumberFormatInfo.InvariantInfo);
			request.ContentType = "multipart/form-data; boundary=" + boundary;
			boundary = "--" + boundary;
			using (Stream requestStream = request.GetRequestStream())
			{
				UploadFile.WriteValues(requestStream, values, boundary);
				UploadFile.WriteFiles(requestStream, files, boundary);
				byte[] boundaryBuffer = Encoding.ASCII.GetBytes(boundary + "--");
				requestStream.Write(boundaryBuffer, 0, boundaryBuffer.Length);
			}
			byte[] result;
			using (WebResponse response = request.GetResponse())
			{
				using (Stream responseStream = response.GetResponseStream())
				{
					using (MemoryStream stream = new MemoryStream())
					{
						responseStream.CopyTo(stream);
						result = stream.ToArray();
					}
				}
			}
			return result;
		}

		// Token: 0x06002CA9 RID: 11433 RVA: 0x00528CE4 File Offset: 0x00526EE4
		public static byte[] GetUploadFilesRequestData(IEnumerable<UploadFile> files, NameValueCollection values, string boundary)
		{
			boundary = "--" + boundary;
			byte[] result;
			using (MemoryStream requestStream = new MemoryStream())
			{
				UploadFile.WriteValues(requestStream, values, boundary);
				UploadFile.WriteFiles(requestStream, files, boundary);
				byte[] boundaryBuffer = Encoding.ASCII.GetBytes(boundary + "--");
				requestStream.Write(boundaryBuffer, 0, boundaryBuffer.Length);
				result = requestStream.ToArray();
			}
			return result;
		}

		// Token: 0x06002CAA RID: 11434 RVA: 0x00528D5C File Offset: 0x00526F5C
		private static void WriteValues(Stream requestStream, NameValueCollection values, string boundary)
		{
			if (values == null)
			{
				return;
			}
			foreach (object obj in values.Keys)
			{
				string name = (string)obj;
				byte[] buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
				requestStream.Write(buffer, 0, buffer.Length);
				buffer = Encoding.ASCII.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"{1}{1}", name, Environment.NewLine));
				requestStream.Write(buffer, 0, buffer.Length);
				buffer = Encoding.UTF8.GetBytes(values[name] + Environment.NewLine);
				requestStream.Write(buffer, 0, buffer.Length);
			}
		}

		// Token: 0x06002CAB RID: 11435 RVA: 0x00528E24 File Offset: 0x00527024
		private static void WriteFiles(Stream requestStream, IEnumerable<UploadFile> files, string boundary)
		{
			if (files == null)
			{
				return;
			}
			foreach (UploadFile file in files)
			{
				byte[] buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
				requestStream.Write(buffer, 0, buffer.Length);
				buffer = Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"{2}", file.Name, file.Filename, Environment.NewLine));
				requestStream.Write(buffer, 0, buffer.Length);
				buffer = Encoding.ASCII.GetBytes(string.Format("Content-Type: {0}{1}{1}", file.ContentType, Environment.NewLine));
				requestStream.Write(buffer, 0, buffer.Length);
				requestStream.Write(file.Content, 0, file.Content.Length);
				buffer = Encoding.ASCII.GetBytes(Environment.NewLine);
				requestStream.Write(buffer, 0, buffer.Length);
			}
		}
	}
}
