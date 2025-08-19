using System;
using System.Runtime.CompilerServices;

namespace Terraria.ModLoader.Core
{
	// Token: 0x02000359 RID: 857
	[CompilerFeatureRequired("RefStructs")]
	public ref struct FilteredSpanEnumerator<T>
	{
		// Token: 0x06002FB0 RID: 12208 RVA: 0x00536895 File Offset: 0x00534A95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public FilteredSpanEnumerator(ReadOnlySpan<T> arr, int[] inds)
		{
			this.arr = arr;
			this.inds = inds;
			this.current = default(T);
			this.i = 0;
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06002FB1 RID: 12209 RVA: 0x005368B8 File Offset: 0x00534AB8
		public T Current
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x06002FB2 RID: 12210 RVA: 0x005368C0 File Offset: 0x00534AC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe bool MoveNext()
		{
			if (this.i >= this.inds.Length)
			{
				return false;
			}
			int[] array = this.inds;
			int num = this.i;
			this.i = num + 1;
			this.current = *this.arr[array[num]];
			return true;
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x0053690E File Offset: 0x00534B0E
		public FilteredSpanEnumerator<T> GetEnumerator()
		{
			return this;
		}

		// Token: 0x04001CBF RID: 7359
		private readonly ReadOnlySpan<T> arr;

		// Token: 0x04001CC0 RID: 7360
		private readonly int[] inds;

		// Token: 0x04001CC1 RID: 7361
		private T current;

		// Token: 0x04001CC2 RID: 7362
		private int i;
	}
}
