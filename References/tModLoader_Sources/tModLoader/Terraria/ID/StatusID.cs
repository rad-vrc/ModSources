using System;
using ReLogic.Reflection;

namespace Terraria.ID
{
	// Token: 0x0200042B RID: 1067
	public class StatusID
	{
		// Token: 0x04004B5B RID: 19291
		public const int Ok = 0;

		// Token: 0x04004B5C RID: 19292
		public const int LaterVersion = 1;

		// Token: 0x04004B5D RID: 19293
		public const int UnknownError = 2;

		// Token: 0x04004B5E RID: 19294
		public const int EmptyFile = 3;

		// Token: 0x04004B5F RID: 19295
		public const int DecryptionError = 4;

		// Token: 0x04004B60 RID: 19296
		public const int BadSectionPointer = 5;

		// Token: 0x04004B61 RID: 19297
		public const int BadFooter = 6;

		// Token: 0x04004B62 RID: 19298
		public static readonly IdDictionary Search = IdDictionary.Create<StatusID, int>();
	}
}
