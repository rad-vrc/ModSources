using System;
using System.Runtime.CompilerServices;

namespace Terraria.ModLoader
{
	// Token: 0x020001F1 RID: 497
	[CompilerFeatureRequired("RefStructs")]
	public ref struct RefReadOnlyArray<T>
	{
		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060026F6 RID: 9974 RVA: 0x00501B18 File Offset: 0x004FFD18
		public int Length
		{
			get
			{
				return this.array.Length;
			}
		}

		// Token: 0x17000465 RID: 1125
		public T this[int index]
		{
			get
			{
				return this.array[index];
			}
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x00501B30 File Offset: 0x004FFD30
		public RefReadOnlyArray(T[] array)
		{
			this.array = array;
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x00501B3C File Offset: 0x004FFD3C
		public ReadOnlySpan<T>.Enumerator GetEnumerator()
		{
			return this.array.GetEnumerator();
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x00501B5C File Offset: 0x004FFD5C
		public static implicit operator RefReadOnlyArray<T>(T[] array)
		{
			return new RefReadOnlyArray<T>(array);
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x00501B64 File Offset: 0x004FFD64
		public static implicit operator ReadOnlySpan<T>(RefReadOnlyArray<T> readOnlyArray)
		{
			return readOnlyArray.array;
		}

		// Token: 0x0400189D RID: 6301
		internal readonly T[] array;
	}
}
