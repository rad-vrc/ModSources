using System;

namespace Terraria.Utilities
{
	// Token: 0x02000089 RID: 137
	public struct Bits64
	{
		// Token: 0x17000249 RID: 585
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

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x004A14EC File Offset: 0x0049F6EC
		public bool IsEmpty
		{
			get
			{
				return this.v == 0UL;
			}
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x004A14F8 File Offset: 0x0049F6F8
		public static implicit operator ulong(Bits64 b)
		{
			return b.v;
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x004A1500 File Offset: 0x0049F700
		public static implicit operator Bits64(ulong v)
		{
			return new Bits64
			{
				v = v
			};
		}

		// Token: 0x040010A2 RID: 4258
		private ulong v;
	}
}
