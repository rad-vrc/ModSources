using System;

namespace Terraria.ModLoader
{
	// Token: 0x0200013C RID: 316
	public static class AsyncProviderStateExtensions
	{
		// Token: 0x06001AAE RID: 6830 RVA: 0x004CD042 File Offset: 0x004CB242
		public static bool IsFinished(this AsyncProviderState s)
		{
			return s == AsyncProviderState.Completed || s == AsyncProviderState.Canceled || s == AsyncProviderState.Aborted;
		}
	}
}
