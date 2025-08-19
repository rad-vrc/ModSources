using System;

namespace Terraria.Utilities
{
	// Token: 0x0200013F RID: 319
	public struct Bits64
	{
		// Token: 0x1700026C RID: 620
		public bool this[int i]
		{
			get
			{
				return (this.v & 1UL << i) > 0UL;
			}
			set
			{
				if (value)
				{
					this.v |= 1UL << i;
					return;
				}
				this.v &= ~(1UL << i);
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060018B7 RID: 6327 RVA: 0x004DEDD8 File Offset: 0x004DCFD8
		public bool IsEmpty
		{
			get
			{
				return this.v == 0UL;
			}
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x004DEDE4 File Offset: 0x004DCFE4
		public static implicit operator ulong(Bits64 b)
		{
			return b.v;
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x004DEDEC File Offset: 0x004DCFEC
		public static implicit operator Bits64(ulong v)
		{
			return new Bits64
			{
				v = v
			};
		}

		// Token: 0x04001509 RID: 5385
		private ulong v;
	}
}
