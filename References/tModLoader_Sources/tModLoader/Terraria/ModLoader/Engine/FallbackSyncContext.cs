using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Terraria.ModLoader.Engine
{
	/// <summary>
	/// Provides a SynchronizationContext for running continuations on the Main thread in the Update loop, for platforms which don't initialized with one
	/// </summary>
	// Token: 0x020002AE RID: 686
	internal static class FallbackSyncContext
	{
		// Token: 0x06002D07 RID: 11527 RVA: 0x0052BA0A File Offset: 0x00529C0A
		public static void Update()
		{
			if (SynchronizationContext.Current == null)
			{
				SynchronizationContext.SetSynchronizationContext(FallbackSyncContext.ctx = new FallbackSyncContext.SyncContext());
				Logging.tML.Debug("Fallback synchronization context assigned");
			}
			FallbackSyncContext.SyncContext syncContext = FallbackSyncContext.ctx;
			if (syncContext == null)
			{
				return;
			}
			syncContext.Update();
		}

		// Token: 0x04001C1B RID: 7195
		private static FallbackSyncContext.SyncContext ctx;

		// Token: 0x02000A54 RID: 2644
		private class SyncContext : SynchronizationContext
		{
			// Token: 0x0600587F RID: 22655 RVA: 0x0069FEB8 File Offset: 0x0069E0B8
			public override void Send(SendOrPostCallback d, object state)
			{
				ManualResetEvent handle = new ManualResetEvent(false);
				Exception e = null;
				FallbackSyncContext.SyncContext.actions.Enqueue(delegate
				{
					try
					{
						d(state);
					}
					catch (Exception e2)
					{
						e = e2;
					}
					finally
					{
						handle.Set();
					}
				});
				handle.WaitOne();
				if (e != null)
				{
					throw e;
				}
			}

			// Token: 0x06005880 RID: 22656 RVA: 0x0069FF20 File Offset: 0x0069E120
			public override void Post(SendOrPostCallback d, object state)
			{
				FallbackSyncContext.SyncContext.actions.Enqueue(delegate
				{
					try
					{
						d(state);
					}
					catch (Exception e)
					{
						Logging.tML.Error("Posted event", e);
					}
				});
			}

			// Token: 0x06005881 RID: 22657 RVA: 0x0069FF57 File Offset: 0x0069E157
			public override SynchronizationContext CreateCopy()
			{
				return this;
			}

			// Token: 0x06005882 RID: 22658 RVA: 0x0069FF5C File Offset: 0x0069E15C
			internal void Update()
			{
				Action action;
				while (FallbackSyncContext.SyncContext.actions.TryDequeue(out action))
				{
					action();
				}
			}

			// Token: 0x04006CE8 RID: 27880
			private static ConcurrentQueue<Action> actions = new ConcurrentQueue<Action>();
		}
	}
}
