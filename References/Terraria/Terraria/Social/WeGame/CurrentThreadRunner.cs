using System;
using System.Windows.Threading;

namespace Terraria.Social.WeGame
{
	// Token: 0x0200015E RID: 350
	public class CurrentThreadRunner
	{
		// Token: 0x060019DD RID: 6621 RVA: 0x004E2A8C File Offset: 0x004E0C8C
		public CurrentThreadRunner()
		{
			this._dsipatcher = Dispatcher.CurrentDispatcher;
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x004E2A9F File Offset: 0x004E0C9F
		public void Run(Action f)
		{
			this._dsipatcher.BeginInvoke(f, new object[0]);
		}

		// Token: 0x04001561 RID: 5473
		private Dispatcher _dsipatcher;
	}
}
