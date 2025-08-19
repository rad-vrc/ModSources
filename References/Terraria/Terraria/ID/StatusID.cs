using System;
using ReLogic.Reflection;

namespace Terraria.ID
{
	// Token: 0x020001C2 RID: 450
	public class StatusID
	{
		// Token: 0x04003C0B RID: 15371
		public const int Ok = 0;

		// Token: 0x04003C0C RID: 15372
		public const int LaterVersion = 1;

		// Token: 0x04003C0D RID: 15373
		public const int UnknownError = 2;

		// Token: 0x04003C0E RID: 15374
		public const int EmptyFile = 3;

		// Token: 0x04003C0F RID: 15375
		public const int DecryptionError = 4;

		// Token: 0x04003C10 RID: 15376
		public const int BadSectionPointer = 5;

		// Token: 0x04003C11 RID: 15377
		public const int BadFooter = 6;

		// Token: 0x04003C12 RID: 15378
		public static readonly IdDictionary Search = IdDictionary.Create<StatusID, int>();
	}
}
