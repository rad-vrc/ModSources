using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Terraria.ModLoader
{
	// Token: 0x0200013E RID: 318
	public class AsyncProvider<T>
	{
		/// <remarks>
		///   Remember to provide your enumerator with
		///   `[EnumeratorCancellation] CancellationToken token = default`
		///   as argument to allow cancellation notification.
		/// </remarks>
		// Token: 0x06001AAF RID: 6831 RVA: 0x004CD054 File Offset: 0x004CB254
		public AsyncProvider(IAsyncEnumerable<T> provider)
		{
			AsyncProvider<T>.<>c__DisplayClass2_0 CS$<>8__locals1 = new AsyncProvider<T>.<>c__DisplayClass2_0();
			CS$<>8__locals1.provider = provider;
			base..ctor();
			CS$<>8__locals1.<>4__this = this;
			this._Channel = Channel.CreateUnbounded<T>();
			this.TokenSource = new CancellationTokenSource();
			delegate
			{
				AsyncProvider<T>.<>c__DisplayClass2_0.<<-ctor>b__0>d <<-ctor>b__0>d;
				<<-ctor>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<-ctor>b__0>d.<>4__this = CS$<>8__locals1;
				<<-ctor>b__0>d.<>1__state = -1;
				<<-ctor>b__0>d.<>t__builder.Start<AsyncProvider<T>.<>c__DisplayClass2_0.<<-ctor>b__0>d>(ref <<-ctor>b__0>d);
				return <<-ctor>b__0>d.<>t__builder.Task;
			}();
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x004CD0A1 File Offset: 0x004CB2A1
		public void Cancel()
		{
			this.TokenSource.Cancel();
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x004CD0AE File Offset: 0x004CB2AE
		public bool IsCancellationRequested
		{
			get
			{
				return this.TokenSource.IsCancellationRequested;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06001AB2 RID: 6834 RVA: 0x004CD0BC File Offset: 0x004CB2BC
		public AsyncProviderState State
		{
			get
			{
				Task completion = this._Channel.Reader.Completion;
				if (!completion.IsCompleted)
				{
					return AsyncProviderState.Loading;
				}
				if (completion.IsCanceled)
				{
					return AsyncProviderState.Canceled;
				}
				if (completion.IsFaulted)
				{
					return AsyncProviderState.Aborted;
				}
				return AsyncProviderState.Completed;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x004CD0F9 File Offset: 0x004CB2F9
		public Exception Exception
		{
			get
			{
				return this._Channel.Reader.Completion.Exception;
			}
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x004CD110 File Offset: 0x004CB310
		public IEnumerable<T> GetData()
		{
			T item;
			while (this._Channel.Reader.TryRead(out item))
			{
				yield return item;
			}
			yield break;
		}

		// Token: 0x04001473 RID: 5235
		private Channel<T> _Channel;

		// Token: 0x04001474 RID: 5236
		private CancellationTokenSource TokenSource;
	}
}
