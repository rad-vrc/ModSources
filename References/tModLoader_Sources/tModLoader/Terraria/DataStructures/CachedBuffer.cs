using System;
using System.IO;

namespace Terraria.DataStructures
{
	// Token: 0x020006D6 RID: 1750
	public class CachedBuffer
	{
		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06004912 RID: 18706 RVA: 0x0064C920 File Offset: 0x0064AB20
		public int Length
		{
			get
			{
				return this.Data.Length;
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06004913 RID: 18707 RVA: 0x0064C92A File Offset: 0x0064AB2A
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
		}

		// Token: 0x06004914 RID: 18708 RVA: 0x0064C934 File Offset: 0x0064AB34
		public CachedBuffer(byte[] data)
		{
			this.Data = data;
			this._memoryStream = new MemoryStream(data);
			this.Writer = new BinaryWriter(this._memoryStream);
			this.Reader = new BinaryReader(this._memoryStream);
		}

		// Token: 0x06004915 RID: 18709 RVA: 0x0064C983 File Offset: 0x0064AB83
		internal CachedBuffer Activate()
		{
			this._isActive = true;
			this._memoryStream.Position = 0L;
			return this;
		}

		// Token: 0x06004916 RID: 18710 RVA: 0x0064C99A File Offset: 0x0064AB9A
		public void Recycle()
		{
			if (this._isActive)
			{
				this._isActive = false;
				BufferPool.Recycle(this);
			}
		}

		// Token: 0x04005E74 RID: 24180
		public readonly byte[] Data;

		// Token: 0x04005E75 RID: 24181
		public readonly BinaryWriter Writer;

		// Token: 0x04005E76 RID: 24182
		public readonly BinaryReader Reader;

		// Token: 0x04005E77 RID: 24183
		private readonly MemoryStream _memoryStream;

		// Token: 0x04005E78 RID: 24184
		private bool _isActive = true;
	}
}
