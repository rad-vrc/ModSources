using System;

namespace Terraria.ModLoader.Exceptions
{
	// Token: 0x020002AA RID: 682
	public class ValueNotTranslationKeyException : Exception
	{
		// Token: 0x06002CFA RID: 11514 RVA: 0x0052B390 File Offset: 0x00529590
		public ValueNotTranslationKeyException(string message) : base(message)
		{
			this.HelpLink = "https://github.com/tModLoader/tModLoader/pull/3302";
		}
	}
}
