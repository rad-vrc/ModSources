using System;
using System.IO;

namespace Terraria.DataStructures
{
	// Token: 0x02000444 RID: 1092
	public class CachedBuffer
	{
		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06002BCD RID: 11213 RVA: 0x0059F61A File Offset: 0x0059D81A
		public int Length
		{
			get
			{
				return this.Data.Length;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06002BCE RID: 11214 RVA: 0x0059F624 File Offset: 0x0059D824
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x0059F62C File Offset: 0x0059D82C
		public CachedBuffer(byte[] data)
		{
			this.Data = data;
			this._memoryStream = new MemoryStream(data);
			this.Writer = new BinaryWriter(this._memoryStream);
			this.Reader = new BinaryReader(this._memoryStream);
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x0059F67B File Offset: 0x0059D87B
		internal CachedBuffer Activate()
		{
			this._isActive = true;
			this._memoryStream.Position = 0L;
			return this;
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x0059F692 File Offset: 0x0059D892
		public void Recycle()
		{
			if (this._isActive)
			{
				this._isActive = false;
				BufferPool.Recycle(this);
			}
		}

		// Token: 0x04004FEF RID: 20463
		public readonly byte[] Data;

		// Token: 0x04004FF0 RID: 20464
		public readonly BinaryWriter Writer;

		// Token: 0x04004FF1 RID: 20465
		public readonly BinaryReader Reader;

		// Token: 0x04004FF2 RID: 20466
		private readonly MemoryStream _memoryStream;

		// Token: 0x04004FF3 RID: 20467
		private bool _isActive = true;
	}
}
