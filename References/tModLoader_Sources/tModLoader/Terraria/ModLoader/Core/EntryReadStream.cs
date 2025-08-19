using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Terraria.ModLoader.Core
{
	// Token: 0x02000357 RID: 855
	internal sealed class EntryReadStream : Stream
	{
		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06002F9D RID: 12189 RVA: 0x005365D2 File Offset: 0x005347D2
		private int Start
		{
			get
			{
				return this.entry.Offset;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06002F9E RID: 12190 RVA: 0x005365DF File Offset: 0x005347DF
		public string Name
		{
			get
			{
				return this.entry.Name;
			}
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x005365EC File Offset: 0x005347EC
		public EntryReadStream(TmodFile file, TmodFile.FileEntry entry, Stream stream, bool leaveOpen)
		{
			this.file = file;
			this.entry = entry;
			this.stream = stream;
			this.leaveOpen = leaveOpen;
			if (this.stream.Position != (long)this.Start)
			{
				this.stream.Position = (long)this.Start;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06002FA0 RID: 12192 RVA: 0x00536642 File Offset: 0x00534842
		public override bool CanRead
		{
			get
			{
				return this.stream.CanRead;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06002FA1 RID: 12193 RVA: 0x0053664F File Offset: 0x0053484F
		public override bool CanSeek
		{
			get
			{
				return this.stream.CanSeek;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06002FA2 RID: 12194 RVA: 0x0053665C File Offset: 0x0053485C
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06002FA3 RID: 12195 RVA: 0x0053665F File Offset: 0x0053485F
		public override long Length
		{
			get
			{
				return (long)this.entry.CompressedLength;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06002FA4 RID: 12196 RVA: 0x0053666D File Offset: 0x0053486D
		// (set) Token: 0x06002FA5 RID: 12197 RVA: 0x00536684 File Offset: 0x00534884
		public override long Position
		{
			get
			{
				return this.stream.Position - (long)this.Start;
			}
			set
			{
				if (value < 0L || value > this.Length)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(28, 2);
					defaultInterpolatedStringHandler.AppendLiteral("Position ");
					defaultInterpolatedStringHandler.AppendFormatted<long>(value);
					defaultInterpolatedStringHandler.AppendLiteral(" outside range (0-");
					defaultInterpolatedStringHandler.AppendFormatted<long>(this.Length);
					defaultInterpolatedStringHandler.AppendLiteral(")");
					throw new ArgumentOutOfRangeException(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				this.stream.Position = value + (long)this.Start;
			}
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x00536703 File Offset: 0x00534903
		public override void Flush()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x0053670A File Offset: 0x0053490A
		public override int Read(byte[] buffer, int offset, int count)
		{
			count = Math.Min(count, (int)(this.Length - this.Position));
			return this.stream.Read(buffer, offset, count);
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x00536730 File Offset: 0x00534930
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (origin != SeekOrigin.Current)
			{
				this.Position = ((origin == SeekOrigin.Begin) ? offset : (this.Length - offset));
				return this.Position;
			}
			long target = this.Position + offset;
			if (target < 0L || target > this.Length)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(28, 2);
				defaultInterpolatedStringHandler.AppendLiteral("Position ");
				defaultInterpolatedStringHandler.AppendFormatted<long>(target);
				defaultInterpolatedStringHandler.AppendLiteral(" outside range (0-");
				defaultInterpolatedStringHandler.AppendFormatted<long>(this.Length);
				defaultInterpolatedStringHandler.AppendLiteral(")");
				throw new ArgumentOutOfRangeException(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			return this.stream.Seek(offset, origin) - (long)this.Start;
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x005367D8 File Offset: 0x005349D8
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x005367DF File Offset: 0x005349DF
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002FAB RID: 12203 RVA: 0x005367E6 File Offset: 0x005349E6
		public override void Close()
		{
			if (this.stream == null)
			{
				return;
			}
			if (!this.leaveOpen)
			{
				this.stream.Close();
			}
			this.stream = null;
			this.file.OnStreamClosed(this);
		}

		// Token: 0x04001CB7 RID: 7351
		private TmodFile file;

		// Token: 0x04001CB8 RID: 7352
		private TmodFile.FileEntry entry;

		// Token: 0x04001CB9 RID: 7353
		private Stream stream;

		// Token: 0x04001CBA RID: 7354
		private bool leaveOpen;
	}
}
