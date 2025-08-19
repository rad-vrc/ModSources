using System;
using System.Runtime.CompilerServices;

namespace Terraria.ModLoader.Core
{
	// Token: 0x02000358 RID: 856
	[CompilerFeatureRequired("RefStructs")]
	public ref struct FilteredArrayEnumerator<T>
	{
		// Token: 0x06002FAC RID: 12204 RVA: 0x00536817 File Offset: 0x00534A17
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public FilteredArrayEnumerator(T[] arr, int[] inds)
		{
			this.arr = arr;
			this.inds = inds;
			this.current = default(T);
			this.i = 0;
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06002FAD RID: 12205 RVA: 0x0053683A File Offset: 0x00534A3A
		public T Current
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x00536844 File Offset: 0x00534A44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool MoveNext()
		{
			if (this.i >= this.inds.Length)
			{
				return false;
			}
			T[] array = this.arr;
			int[] array2 = this.inds;
			int num = this.i;
			this.i = num + 1;
			this.current = array[array2[num]];
			return true;
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x0053688D File Offset: 0x00534A8D
		public FilteredArrayEnumerator<T> GetEnumerator()
		{
			return this;
		}

		// Token: 0x04001CBB RID: 7355
		private readonly T[] arr;

		// Token: 0x04001CBC RID: 7356
		private readonly int[] inds;

		// Token: 0x04001CBD RID: 7357
		private T current;

		// Token: 0x04001CBE RID: 7358
		private int i;
	}
}
