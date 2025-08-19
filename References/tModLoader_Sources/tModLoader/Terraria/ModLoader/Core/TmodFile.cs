using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using Ionic.Zlib;
using Terraria.Localization;
using Terraria.ModLoader.IO;
using Terraria.ModLoader.UI;

namespace Terraria.ModLoader.Core
{
	// Token: 0x02000366 RID: 870
	public class TmodFile : IEnumerable<TmodFile.FileEntry>, IEnumerable
	{
		// Token: 0x0600305A RID: 12378 RVA: 0x0053BEB8 File Offset: 0x0053A0B8
		private static string Sanitize(string path)
		{
			return path.Replace('\\', '/');
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x0600305B RID: 12379 RVA: 0x0053BEC4 File Offset: 0x0053A0C4
		// (set) Token: 0x0600305C RID: 12380 RVA: 0x0053BECC File Offset: 0x0053A0CC
		public Version TModLoaderVersion { get; private set; }

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x0600305D RID: 12381 RVA: 0x0053BED5 File Offset: 0x0053A0D5
		// (set) Token: 0x0600305E RID: 12382 RVA: 0x0053BEDD File Offset: 0x0053A0DD
		public string Name { get; private set; }

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x0600305F RID: 12383 RVA: 0x0053BEE6 File Offset: 0x0053A0E6
		// (set) Token: 0x06003060 RID: 12384 RVA: 0x0053BEEE File Offset: 0x0053A0EE
		public Version Version { get; private set; }

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06003061 RID: 12385 RVA: 0x0053BEF7 File Offset: 0x0053A0F7
		// (set) Token: 0x06003062 RID: 12386 RVA: 0x0053BEFF File Offset: 0x0053A0FF
		public byte[] Hash { get; private set; }

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06003063 RID: 12387 RVA: 0x0053BF08 File Offset: 0x0053A108
		// (set) Token: 0x06003064 RID: 12388 RVA: 0x0053BF10 File Offset: 0x0053A110
		internal byte[] Signature { get; private set; } = new byte[256];

		// Token: 0x06003065 RID: 12389 RVA: 0x0053BF1C File Offset: 0x0053A11C
		internal TmodFile(string path, string name = null, Version version = null)
		{
			this.path = path;
			this.Name = name;
			this.Version = version;
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x0053BF6A File Offset: 0x0053A16A
		public bool HasFile(string fileName)
		{
			return this.files.ContainsKey(TmodFile.Sanitize(fileName));
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x0053BF80 File Offset: 0x0053A180
		public byte[] GetBytes(TmodFile.FileEntry entry)
		{
			if (entry.cachedBytes != null && !entry.IsCompressed)
			{
				return entry.cachedBytes;
			}
			byte[] result;
			using (Stream stream = this.GetStream(entry, false))
			{
				result = stream.ReadBytes(entry.Length);
			}
			return result;
		}

		// Token: 0x06003068 RID: 12392 RVA: 0x0053BFD8 File Offset: 0x0053A1D8
		public List<string> GetFileNames()
		{
			return this.files.Keys.ToList<string>();
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x0053BFEC File Offset: 0x0053A1EC
		public byte[] GetBytes(string fileName)
		{
			TmodFile.FileEntry entry;
			if (!this.files.TryGetValue(TmodFile.Sanitize(fileName), out entry))
			{
				return null;
			}
			return this.GetBytes(entry);
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x0053C018 File Offset: 0x0053A218
		public Stream GetStream(TmodFile.FileEntry entry, bool newFileStream = false)
		{
			Stream stream;
			if (entry.cachedBytes != null)
			{
				stream = entry.cachedBytes.ToMemoryStream(false);
			}
			else
			{
				if (this.fileStream == null)
				{
					throw new IOException("File not open: " + this.path);
				}
				if (newFileStream)
				{
					EntryReadStream ers = new EntryReadStream(this, entry, File.OpenRead(this.path), false);
					List<EntryReadStream> obj = this.independentEntryReadStreams;
					lock (obj)
					{
						this.independentEntryReadStreams.Add(ers);
					}
					stream = ers;
				}
				else
				{
					if (this.sharedEntryReadStream != null)
					{
						throw new IOException("Previous entry read stream not closed: " + this.sharedEntryReadStream.Name);
					}
					stream = (this.sharedEntryReadStream = new EntryReadStream(this, entry, this.fileStream, true));
				}
			}
			if (entry.IsCompressed)
			{
				stream = new DeflateStream(stream, 1);
			}
			return stream;
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x0053C100 File Offset: 0x0053A300
		internal void OnStreamClosed(EntryReadStream stream)
		{
			if (stream == this.sharedEntryReadStream)
			{
				this.sharedEntryReadStream = null;
				return;
			}
			List<EntryReadStream> obj = this.independentEntryReadStreams;
			lock (obj)
			{
				if (!this.independentEntryReadStreams.Remove(stream))
				{
					throw new IOException("Closed EntryReadStream not associated with this file. " + stream.Name + " @ " + this.path);
				}
			}
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x0053C17C File Offset: 0x0053A37C
		public Stream GetStream(string fileName, bool newFileStream = false)
		{
			TmodFile.FileEntry entry;
			if (!this.files.TryGetValue(TmodFile.Sanitize(fileName), out entry))
			{
				throw new KeyNotFoundException(fileName);
			}
			return this.GetStream(entry, newFileStream);
		}

		/// <summary>
		/// Adds a (fileName -&gt; content) entry to the compressed payload
		/// This method is not threadsafe with reads, but is threadsafe with multiple concurrent AddFile calls
		/// </summary>
		/// <param name="fileName">The internal filepath, will be slash sanitised automatically</param>
		/// <param name="data">The file content to add. WARNING, data is kept as a shallow copy, so modifications to the passed byte array will affect file content</param>
		// Token: 0x0600306D RID: 12397 RVA: 0x0053C1B0 File Offset: 0x0053A3B0
		internal void AddFile(string fileName, byte[] data)
		{
			fileName = TmodFile.Sanitize(fileName);
			int size = data.Length;
			if ((long)size > 1024L && TmodFile.ShouldCompress(fileName))
			{
				using (MemoryStream ms = new MemoryStream(data.Length))
				{
					using (DeflateStream ds = new DeflateStream(ms, 0))
					{
						ds.Write(data, 0, data.Length);
					}
					byte[] compressed = ms.ToArray();
					if ((float)compressed.Length < (float)size * 0.9f)
					{
						data = compressed;
					}
				}
			}
			IDictionary<string, TmodFile.FileEntry> obj = this.files;
			lock (obj)
			{
				this.files[fileName] = new TmodFile.FileEntry(fileName, -1, size, data.Length, data);
			}
			this.fileTable = null;
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x0053C290 File Offset: 0x0053A490
		internal void RemoveFile(string fileName)
		{
			this.files.Remove(TmodFile.Sanitize(fileName));
			this.fileTable = null;
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x0600306F RID: 12399 RVA: 0x0053C2AB File Offset: 0x0053A4AB
		public int Count
		{
			get
			{
				return this.fileTable.Length;
			}
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x0053C2B5 File Offset: 0x0053A4B5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x0053C2BD File Offset: 0x0053A4BD
		public IEnumerator<TmodFile.FileEntry> GetEnumerator()
		{
			foreach (TmodFile.FileEntry entry in this.fileTable)
			{
				yield return entry;
			}
			TmodFile.FileEntry[] array = null;
			yield break;
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x0053C2CC File Offset: 0x0053A4CC
		internal void Save()
		{
			if (this.fileStream != null)
			{
				throw new IOException("File already open: " + this.path);
			}
			Directory.CreateDirectory(Path.GetDirectoryName(this.path));
			using (this.fileStream = File.Create(this.path))
			{
				using (BinaryWriter writer = new BinaryWriter(this.fileStream))
				{
					writer.Write(Encoding.ASCII.GetBytes("TMOD"));
					writer.Write((this.TModLoaderVersion = BuildInfo.tMLVersion).ToString());
					int hashPos = (int)this.fileStream.Position;
					writer.Write(new byte[280]);
					int dataPos = (int)this.fileStream.Position;
					writer.Write(this.Name);
					writer.Write(this.Version.ToString());
					this.fileTable = this.files.Values.ToArray<TmodFile.FileEntry>();
					writer.Write(this.fileTable.Length);
					foreach (TmodFile.FileEntry f in this.fileTable)
					{
						if (f.CompressedLength != f.cachedBytes.Length)
						{
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(46, 3);
							defaultInterpolatedStringHandler.AppendLiteral("CompressedLength (");
							defaultInterpolatedStringHandler.AppendFormatted<int>(f.CompressedLength);
							defaultInterpolatedStringHandler.AppendLiteral(") != cachedBytes.Length (");
							defaultInterpolatedStringHandler.AppendFormatted<int>(f.cachedBytes.Length);
							defaultInterpolatedStringHandler.AppendLiteral("): ");
							defaultInterpolatedStringHandler.AppendFormatted(f.Name);
							throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
						}
						writer.Write(f.Name);
						writer.Write(f.Length);
						writer.Write(f.CompressedLength);
					}
					int offset = (int)this.fileStream.Position;
					foreach (TmodFile.FileEntry f2 in this.fileTable)
					{
						writer.Write(f2.cachedBytes);
						f2.Offset = offset;
						offset += f2.CompressedLength;
					}
					this.fileStream.Position = (long)dataPos;
					this.Hash = SHA1.Create().ComputeHash(this.fileStream);
					this.fileStream.Position = (long)hashPos;
					writer.Write(this.Hash);
					this.fileStream.Seek(256L, SeekOrigin.Current);
					writer.Write((int)(this.fileStream.Length - (long)dataPos));
				}
			}
			this.fileStream = null;
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x0053C598 File Offset: 0x0053A798
		public IDisposable Open()
		{
			int num = this.openCounter;
			this.openCounter = num + 1;
			if (num == 0)
			{
				if (this.fileStream != null)
				{
					throw new Exception("File already opened? " + this.path);
				}
				try
				{
					if (this.Name == null)
					{
						this.Read();
					}
					else
					{
						this.Reopen();
					}
				}
				catch
				{
					try
					{
						this.Close();
					}
					catch
					{
					}
					throw;
				}
			}
			return new TmodFile.DisposeWrapper(new Action(this.Close));
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x0053C62C File Offset: 0x0053A82C
		private void Close()
		{
			if (this.openCounter == 0)
			{
				return;
			}
			int num = this.openCounter - 1;
			this.openCounter = num;
			if (num == 0)
			{
				if (this.sharedEntryReadStream != null)
				{
					throw new IOException("Previous entry read stream not closed: " + this.sharedEntryReadStream.Name);
				}
				if (this.independentEntryReadStreams.Count != 0)
				{
					throw new IOException("Shared entry read streams not closed: " + string.Join(", ", from e in this.independentEntryReadStreams
					select e.Name));
				}
				FileStream fileStream = this.fileStream;
				if (fileStream != null)
				{
					fileStream.Close();
				}
				this.fileStream = null;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06003075 RID: 12405 RVA: 0x0053C6E4 File Offset: 0x0053A8E4
		public bool IsOpen
		{
			get
			{
				return this.fileStream != null;
			}
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x0053C6EF File Offset: 0x0053A8EF
		private static bool ShouldCompress(string fileName)
		{
			return !fileName.EndsWith(".png") && !fileName.EndsWith(".mp3") && !fileName.EndsWith(".ogg");
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x0053C71C File Offset: 0x0053A91C
		private void Read()
		{
			this.fileStream = File.OpenRead(this.path);
			BinaryReader reader = new BinaryReader(this.fileStream);
			if (Encoding.ASCII.GetString(reader.ReadBytes(4)) != "TMOD")
			{
				throw new Exception("Magic Header != \"TMOD\"");
			}
			this.TModLoaderVersion = new Version(reader.ReadString());
			this.Hash = reader.ReadBytes(20);
			this.Signature = reader.ReadBytes(256);
			long num = (long)reader.ReadInt32();
			this.hashStartPos = this.fileStream.Position;
			if (num != this.fileStream.Length - this.hashStartPos)
			{
				throw new Exception(Language.GetTextValue("tModLoader.LoadErrorHashMismatchCorrupted"));
			}
			try
			{
				if (this.TModLoaderVersion < new Version(0, 11))
				{
					this.Upgrade();
				}
				else
				{
					this.Name = reader.ReadString();
					this.Version = new Version(reader.ReadString());
					int offset = 0;
					this.fileTable = new TmodFile.FileEntry[reader.ReadInt32()];
					for (int i = 0; i < this.fileTable.Length; i++)
					{
						TmodFile.FileEntry f = new TmodFile.FileEntry(reader.ReadString(), offset, reader.ReadInt32(), reader.ReadInt32(), null);
						this.fileTable[i] = f;
						this.files[f.Name] = f;
						offset += f.CompressedLength;
					}
					int fileStartPos = (int)this.fileStream.Position;
					TmodFile.FileEntry[] array = this.fileTable;
					for (int j = 0; j < array.Length; j++)
					{
						array[j].Offset += fileStartPos;
					}
				}
			}
			catch (Exception e)
			{
				if (!this.VerifyHash())
				{
					throw new Exception(Language.GetTextValue("tModLoader.LoadErrorHashMismatchCorrupted"), e);
				}
				throw;
			}
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x0053C8EC File Offset: 0x0053AAEC
		private void Reopen()
		{
			this.fileStream = File.OpenRead(this.path);
			BinaryReader reader = new BinaryReader(this.fileStream);
			if (Encoding.ASCII.GetString(reader.ReadBytes(4)) != "TMOD")
			{
				throw new Exception("Magic Header != \"TMOD\"");
			}
			reader.ReadString();
			if (!reader.ReadBytes(20).SequenceEqual(this.Hash))
			{
				throw new Exception("File has been modified, hash. " + this.path);
			}
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x0053C970 File Offset: 0x0053AB70
		public void CacheFiles(ISet<string> skip = null)
		{
			this.fileStream.Seek((long)this.fileTable[0].Offset, SeekOrigin.Begin);
			foreach (TmodFile.FileEntry f in this.fileTable)
			{
				if ((long)f.CompressedLength > 131072L || (skip != null && skip.Contains(f.Name)))
				{
					this.fileStream.Seek((long)f.CompressedLength, SeekOrigin.Current);
				}
				else
				{
					f.cachedBytes = this.fileStream.ReadBytes(f.CompressedLength);
				}
			}
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x0053CA00 File Offset: 0x0053AC00
		public void RemoveFromCache(IEnumerable<string> fileNames)
		{
			foreach (string fileName in fileNames)
			{
				this.files[fileName].cachedBytes = null;
			}
		}

		// Token: 0x0600307B RID: 12411 RVA: 0x0053CA54 File Offset: 0x0053AC54
		public void ResetCache()
		{
			TmodFile.FileEntry[] array = this.fileTable;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].cachedBytes = null;
			}
		}

		// Token: 0x0600307C RID: 12412 RVA: 0x0053CA80 File Offset: 0x0053AC80
		private void Upgrade()
		{
			Interface.loadMods.SubProgressText = "Upgrading: " + Path.GetFileName(this.path);
			Logging.tML.InfoFormat("Upgrading: {0}", Path.GetFileName(this.path));
			using (DeflateStream deflateStream = new DeflateStream(this.fileStream, 1, true))
			{
				using (BinaryReader reader = new BinaryReader(deflateStream))
				{
					this.Name = reader.ReadString();
					this.Version = new Version(reader.ReadString());
					int count = reader.ReadInt32();
					for (int i = 0; i < count; i++)
					{
						this.AddFile(reader.ReadString(), reader.ReadBytes(reader.ReadInt32()));
					}
				}
			}
			BuildProperties info = BuildProperties.ReadModFile(this);
			info.buildVersion = this.TModLoaderVersion;
			this.AddFile("Info", info.ToBytes());
			this.fileStream.Seek(0L, SeekOrigin.Begin);
			string path = Path.Combine(Path.GetDirectoryName(this.path), "UpgradeBackup");
			Directory.CreateDirectory(path);
			using (FileStream backupStream = File.OpenWrite(Path.Combine(path, Path.GetFileName(this.path))))
			{
				this.fileStream.CopyTo(backupStream);
			}
			this.Close();
			this.Save();
			this.ResetCache();
			this.Open();
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x0053CC00 File Offset: 0x0053AE00
		internal bool VerifyHash()
		{
			bool flag = this.hashVerified.GetValueOrDefault();
			if (this.hashVerified == null)
			{
				flag = this._VerifyHash();
				this.hashVerified = new bool?(flag);
				return flag;
			}
			return flag;
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x0053CC3C File Offset: 0x0053AE3C
		private bool _VerifyHash()
		{
			if (this.hashStartPos == 0L)
			{
				return false;
			}
			bool result;
			using (FileStream fs = File.OpenRead(this.path))
			{
				fs.Position = this.hashStartPos;
				result = this.Hash.SequenceEqual(SHA1.Create().ComputeHash(fs));
			}
			return result;
		}

		// Token: 0x04001D01 RID: 7425
		public const uint MIN_COMPRESS_SIZE = 1024U;

		// Token: 0x04001D02 RID: 7426
		public const uint MAX_CACHE_SIZE = 131072U;

		// Token: 0x04001D03 RID: 7427
		public const float COMPRESSION_TRADEOFF = 0.9f;

		// Token: 0x04001D04 RID: 7428
		public readonly string path;

		// Token: 0x04001D05 RID: 7429
		private FileStream fileStream;

		// Token: 0x04001D06 RID: 7430
		private IDictionary<string, TmodFile.FileEntry> files = new Dictionary<string, TmodFile.FileEntry>();

		// Token: 0x04001D07 RID: 7431
		private TmodFile.FileEntry[] fileTable;

		// Token: 0x04001D08 RID: 7432
		private int openCounter;

		// Token: 0x04001D09 RID: 7433
		private EntryReadStream sharedEntryReadStream;

		// Token: 0x04001D0A RID: 7434
		private List<EntryReadStream> independentEntryReadStreams = new List<EntryReadStream>();

		// Token: 0x04001D10 RID: 7440
		private long hashStartPos;

		// Token: 0x04001D11 RID: 7441
		private bool? hashVerified;

		// Token: 0x02000AE1 RID: 2785
		public class FileEntry
		{
			// Token: 0x1700092E RID: 2350
			// (get) Token: 0x06005AAD RID: 23213 RVA: 0x006A43F4 File Offset: 0x006A25F4
			public string Name { get; }

			// Token: 0x1700092F RID: 2351
			// (get) Token: 0x06005AAE RID: 23214 RVA: 0x006A43FC File Offset: 0x006A25FC
			// (set) Token: 0x06005AAF RID: 23215 RVA: 0x006A4404 File Offset: 0x006A2604
			public int Offset { get; internal set; }

			// Token: 0x17000930 RID: 2352
			// (get) Token: 0x06005AB0 RID: 23216 RVA: 0x006A440D File Offset: 0x006A260D
			public int Length { get; }

			// Token: 0x17000931 RID: 2353
			// (get) Token: 0x06005AB1 RID: 23217 RVA: 0x006A4415 File Offset: 0x006A2615
			public int CompressedLength { get; }

			// Token: 0x06005AB2 RID: 23218 RVA: 0x006A441D File Offset: 0x006A261D
			internal FileEntry(string name, int offset, int length, int compressedLength, byte[] cachedBytes = null)
			{
				this.Name = name;
				this.Offset = offset;
				this.Length = length;
				this.CompressedLength = compressedLength;
				this.cachedBytes = cachedBytes;
			}

			// Token: 0x17000932 RID: 2354
			// (get) Token: 0x06005AB3 RID: 23219 RVA: 0x006A444A File Offset: 0x006A264A
			public bool IsCompressed
			{
				get
				{
					return this.Length != this.CompressedLength;
				}
			}

			// Token: 0x04006E72 RID: 28274
			internal byte[] cachedBytes;
		}

		// Token: 0x02000AE2 RID: 2786
		private class DisposeWrapper : IDisposable
		{
			// Token: 0x06005AB4 RID: 23220 RVA: 0x006A445D File Offset: 0x006A265D
			public DisposeWrapper(Action dispose)
			{
				this.dispose = dispose;
			}

			// Token: 0x06005AB5 RID: 23221 RVA: 0x006A446C File Offset: 0x006A266C
			public void Dispose()
			{
				Action action = this.dispose;
				if (action == null)
				{
					return;
				}
				action();
			}

			// Token: 0x04006E73 RID: 28275
			private readonly Action dispose;
		}
	}
}
