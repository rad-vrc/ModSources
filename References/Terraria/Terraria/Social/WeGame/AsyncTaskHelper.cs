using System;
using System.Threading.Tasks;

namespace Terraria.Social.WeGame
{
	// Token: 0x0200015F RID: 351
	public class AsyncTaskHelper
	{
		// Token: 0x060019DF RID: 6623 RVA: 0x004E2AB4 File Offset: 0x004E0CB4
		private AsyncTaskHelper()
		{
			this._currentThreadRunner = new CurrentThreadRunner();
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x004E2AC8 File Offset: 0x004E0CC8
		public void RunAsyncTaskAndReply(Action task, Action replay)
		{
			Task.Factory.StartNew(delegate()
			{
				task();
				this._currentThreadRunner.Run(replay);
			});
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x004E2B07 File Offset: 0x004E0D07
		public void RunAsyncTask(Action task)
		{
			Task.Factory.StartNew(task);
		}

		// Token: 0x04001562 RID: 5474
		private CurrentThreadRunner _currentThreadRunner;
	}
}
