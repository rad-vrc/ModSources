using System;
using System.Runtime.CompilerServices;

namespace Terraria.ModLoader
{
	// Token: 0x0200013A RID: 314
	[CompilerFeatureRequired("RefStructs")]
	public readonly ref struct ActiveEntityIterator<T> where T : Entity
	{
		// Token: 0x06001AA7 RID: 6823 RVA: 0x004CCFDF File Offset: 0x004CB1DF
		public ActiveEntityIterator(ReadOnlySpan<T> span)
		{
			this.span = span;
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x004CCFE8 File Offset: 0x004CB1E8
		public ActiveEntityIterator<T>.Enumerator GetEnumerator()
		{
			return new ActiveEntityIterator<T>.Enumerator(this.span.GetEnumerator());
		}

		// Token: 0x0400146B RID: 5227
		private readonly ReadOnlySpan<T> span;

		// Token: 0x020008A8 RID: 2216
		[CompilerFeatureRequired("RefStructs")]
		public ref struct Enumerator
		{
			// Token: 0x0600520C RID: 21004 RVA: 0x00698877 File Offset: 0x00696A77
			public Enumerator(ReadOnlySpan<T>.Enumerator enumerator)
			{
				this.enumerator = enumerator;
			}

			// Token: 0x170008CC RID: 2252
			// (get) Token: 0x0600520D RID: 21005 RVA: 0x00698880 File Offset: 0x00696A80
			public unsafe readonly T Current
			{
				get
				{
					ReadOnlySpan<T>.Enumerator enumerator = this.enumerator;
					return *enumerator.Current;
				}
			}

			// Token: 0x0600520E RID: 21006 RVA: 0x006988A0 File Offset: 0x00696AA0
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public unsafe bool MoveNext()
			{
				while (this.enumerator.MoveNext())
				{
					if (this.enumerator.Current->active)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x04006A45 RID: 27205
			private ReadOnlySpan<T>.Enumerator enumerator;
		}
	}
}
